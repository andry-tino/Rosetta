/// <summary>
/// ProgramWrapper.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST;
    using Rosetta.AST.Transformers;
    using Rosetta.ScriptSharp.Definition.AST.Transformers;

    /// <summary>
    /// Acts like a wrapper for <see cref="ProgramDefinitionASTWalker"/> in order to provide 
    /// an easy interface for converting C# code.
    /// </summary>
    public class ProgramWrapper
    {
        private readonly ProgramDefinitionASTWalker walker;
        private string output;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramWrapper"/> class.
        /// </summary>
        /// <param name="source"></param>
        public ProgramWrapper(string source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);
            Source.ProgramRoot = tree;

            var node = tree.GetRoot();

            IASTTransformer transformer = new ScriptNamespaceBasedASTTransformer();
            transformer.Transform(ref node);

            // Creating the walker
            this.walker = ProgramDefinitionASTWalker.Create(node);
            this.output = null;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Output
        {
            get
            {
                if (this.output == null)
                {
                    this.output = this.walker.Walk().Translate();
                }

                return this.output;
            }
        }
    }
}
