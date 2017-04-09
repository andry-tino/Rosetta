/// <summary>
/// ProgramWrapper.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.ScriptSharp
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Initiates the translation.
    /// </summary>
    public class ProgramWrapper : Rosetta.Reflection.ProgramWrapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramWrapper"/> class.
        /// </summary>
        /// <param name="assemblyPath"></param>
        public ProgramWrapper(string assemblyPath) 
            : base(assemblyPath)
        {
        }

        protected override IASTBuilder CreateASTBuilder(Assembly assembly) => new ASTBuilder(assembly);
    }
}
