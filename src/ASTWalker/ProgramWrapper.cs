/// <summary>
/// ProgramWrapper.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Acts like a wrapper for <see cref="ProgramASTWalker"/> in order to provide 
    /// an easy interface for converting C# code.
    /// </summary>
    public class ProgramWrapper
    {
        private readonly ProgramASTWalker walker;
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

            var node = tree.GetRoot() as CompilationUnitSyntax;

            // Creating the walker
            this.walker = ProgramASTWalker.Create(node);
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
