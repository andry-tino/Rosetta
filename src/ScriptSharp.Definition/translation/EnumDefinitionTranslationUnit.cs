/// <summary>
/// EnumDefinitionTranslationUnit.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.Translation
{
    using System;

    using Rosetta.Translation;

    /// <summary>
    /// Translation unit for describing interfaces for definitions.
    /// 
    /// TODO: Move to a separate project, this is specific to ScriptSharp.
    /// </summary>
    public class EnumDefinitionTranslationUnit : EnumTranslationUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumDefinitionTranslationUnit"/> class.
        /// </summary>
        protected EnumDefinitionTranslationUnit() : base()
        {
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="EnumDefinitionTranslationUnit"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public EnumDefinitionTranslationUnit(EnumDefinitionTranslationUnit other)
            : base(other)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static new EnumDefinitionTranslationUnit Create(VisibilityToken visibility, ITranslationUnit name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name), "Enum name cannot be null!");
            }

            return new EnumDefinitionTranslationUnit()
            {
                Visibility = visibility,
                Name = name
            };
        }

        protected override string RenderedVisibilityModifier
        {
            get
            {
                return this.IsAtRootLevel
                    ? $"{Lexems.DeclareKeyword}{Lexems.Whitespace}" // In this case, the containing structure will add the exposing keyword
                    : string.Empty;
            }
        }

        protected override bool ShouldRenderMembers
        {
            get { return true; }
        }
    }
}
