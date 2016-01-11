/// <summary>
/// VisibilityConverter.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Converters
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Translation;

    /// <summary>
    /// Converter for properly converting C# visibilities into TypeScript visibilities.
    /// </summary>
    internal class VisibilityConverter
    {
        private SyntaxToken sourceVisibility;

        /// <summary>
        /// Initializes a new instance of the <see cref="VisibilityConverter"/> class.
        /// </summary>
        /// <param name="sourceVisibility"></param>
        public VisibilityConverter(SyntaxToken sourceVisibility)
        {
            if (sourceVisibility == null)
            {
                throw new ArgumentNullException(nameof(sourceVisibility));
            }

            this.sourceVisibility = sourceVisibility;
        }

        /// <summary>
        /// Gets the converted visibility.
        /// </summary>
        public VisibilityToken TargetVisibility
        {
            get
            {
                VisibilityToken targetVisibility = VisibilityToken.None;
                
                return targetVisibility;
            }
        }
    }
}
