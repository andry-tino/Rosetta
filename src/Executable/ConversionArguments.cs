/// <summary>
/// ConversionArguments.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.Executable
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A class providing all possible conversion parameters needed.
    /// </summary>
    /// <remarks>
    /// This class does not include validation logic. Validation of the different arguments might occurr 
    /// at different times, thus it is not performed here.
    /// </remarks>
    public class ConversionArguments : ICloneable
    {
        /// <summary>
        /// Gets or sets the source to be converted.
        /// </summary>
        /// <remarks>
        /// Usually it can be left <code>null</code> depending on the context.
        /// </remarks>
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the new file name to give to a source file from which we generate a new file after converting it.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the extension to use for generated files
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Gets or sets the path to the path of the file to convert.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the path to the assembly in case a semantic model is needed.
        /// </summary>
        public string AssemblyPath { get; set; }

        /// <summary>
        /// Gets or sets the directory which will be traversed in order to look for source files.
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        /// Gets or sets the directory into which converted files will be saved.
        /// </summary>
        public string OutputDirectory { get; set; }

        /// <summary>
        /// Gets or sets the collection of references which will be included in the final file or bundle as file includes.
        /// </summary>
        /// <remarks>
        /// The paths in the collection will not be modified, nor normalized but inserted in the final code as they are.
        /// </remarks>
        public IEnumerable<string> References { get; set; }

        /// <summary>
        /// Clones the object.
        /// </summary>
        /// <returns>A cloned <see cref="ConversionArguments"/>.</returns>
        public object Clone()
        {
            return new ConversionArguments()
            {
                Source = this.Source?.Clone() as string,
                FileName = this.FileName?.Clone() as string,
                Extension = this.Extension?.Clone() as string,
                FilePath = this.FilePath?.Clone() as string,
                AssemblyPath = this.AssemblyPath?.Clone() as string,
                Directory = this.Directory?.Clone() as string,
                OutputDirectory = this.OutputDirectory?.Clone() as string,
                References = this.References?.Select(reference => reference?.Clone() as string)
            };
        }
    }

    /// <summary>
    /// A class collecting some very useful <see cref="ConversionProvider"/>s.
    /// </summary>
    public static class ConversionProviders
    {
        /// <summary>
        /// Just returns an empty string.
        /// </summary>
        public static ConversionProvider EmptyConversionProvider = arguments => string.Empty;
    }
}
