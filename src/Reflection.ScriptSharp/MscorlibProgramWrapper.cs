/// <summary>
/// MscorlibProgramWrapper.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.ScriptSharp
{
    using System;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    using Rosetta.AST;
    using Rosetta.Reflection.Proxies;
    using Rosetta.ScriptSharp.Definition.AST;

    /// <summary>
    /// Initiates the translation.
    /// </summary>
    public class MscorlibProgramWrapper : Rosetta.Reflection.ProgramWrapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MscorlibProgramWrapper"/> class.
        /// </summary>
        /// <param name="assemblyPath"></param>
        public MscorlibProgramWrapper() : base(null)
        {
        }

        protected override IASTBuilder CreateASTBuilder(IAssemblyProxy assembly) => new ASTBuilder(assembly, true) { Logger = this.Logger };

        protected override IAssemblyLoader CreateAssemblyLoader(string assemblyPath) => new MscorlibAssemblyLoader();

        protected override IASTWalker CreateASTWalker(CSharpSyntaxNode node, SemanticModel semanticModel)
        {
            var walker = ProgramDefinitionASTWalker.Create(node, null, semanticModel);
            walker.Logger = this.Logger;

            return walker;
        }
    }
}
