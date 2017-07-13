/// <summary>
/// GenerateTaskBase.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.BuildTask
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Rosetta.Diagnostics.Logging;
    using Rosetta.Executable;
    using Rosetta.Translation;
    using Rosetta.ScriptSharp.Definition.AST;

    /// <summary>
    /// The build task base.
    /// </summary>
    public abstract class GenerateTaskBase
    {
        protected const string extension = "d.ts";

        protected readonly IEnumerable<string> sourceFiles;
        protected readonly IEnumerable<string> references;
        protected readonly string assemblyPath;
        protected readonly string outputFolder;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateTaskBase"/> class.
        /// </summary>
        /// <param name="sourceFiles"></param>
        /// <param name="outputFolder"></param>
        /// <param name="assemblyPath"></param>
        /// <param name="references"></param>
        public GenerateTaskBase(IEnumerable<string> sourceFiles, string outputFolder, string assemblyPath = null, IEnumerable<string> references = null)
        {
            if (sourceFiles == null)
            {
                throw new ArgumentNullException(nameof(sourceFiles));
            }
            if (outputFolder == null)
            {
                throw new ArgumentNullException(nameof(outputFolder));
            }

            this.sourceFiles = sourceFiles;
            this.assemblyPath = assemblyPath;
            this.outputFolder = outputFolder;
            this.references = references;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract void Run();

        protected bool ReferencesDefined => this.references != null && this.references.Count() != 0;

        protected static string PerformFileConversion(ConversionArguments arguments)
        {
            var program = new ProgramWrapper(
                arguments.Source,
                arguments.AssemblyPath)
            {
                LogPath = new SysRegLogPathProvider().LogPath
            };

            return program.Output;
        }

        protected string GeneratePrependedText()
        {
            if (this.references == null)
            {
                return string.Empty;
            }

            ITranslationUnit references = CreateReferencesGroupTranslationUnit(this.references);
            return $"{references.Translate()}{Lexems.Newline}{Lexems.Newline}";
        }

        private static ITranslationUnit CreateReferencesGroupTranslationUnit(IEnumerable<string> paths)
        {
            // TODO: Change to use a factory
            var statementsGroup = ReferencesGroupTranslationUnit.Create();

            foreach (var path in paths)
            {
                statementsGroup.AddStatement(ReferenceTranslationUnit.Create(path));
            }

            return statementsGroup;
        }
    }
}
