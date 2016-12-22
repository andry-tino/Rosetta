/// <summary>
/// Program.ConvertFile.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.ScriptSharp.Definition.Runner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    using Rosetta.Executable;
    using Rosetta.ScriptSharp.Definition.AST;
    using Rosetta.Translation;

    /// <summary>
    /// Part of program responsible for translating one single file.
    /// </summary>
    internal partial class Program
    {
        protected const string Extension = "d.ts";

        protected virtual void ConvertFile()
        {
            var arguments = new ConversionArguments()
            {
                FilePath = this.filePath,
                AssemblyPath = this.assemblyPath,
                OutputDirectory = this.outputFolder,
                Extension = Extension,
                FileName = this.fileName,
                References = this.includes
            };

            if (this.includes.Count() > 0)
            {
                new FileAppendableContentConversionRunner(PerformFileConversion, arguments, this.GeneratePrependedText()).Run();
                return;
            }

            new FileConversionRunner(PerformFileConversion, arguments).Run();
        }

        protected static string PerformFileConversion(ConversionArguments arguments)
        {
            var program = new ProgramWrapper(
                arguments.Source,
                arguments.AssemblyPath);

            return program.Output;
        }

        private string GeneratePrependedText()
        {
            if (this.includes.Count() == 0)
            {
                return string.Empty;
            }

            ITranslationUnit references = CreateReferencesGroupTranslationUnit(this.includes);
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
