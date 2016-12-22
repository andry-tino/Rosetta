/// <summary>
/// MethodDefinitionTranslationUnit.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.Translation
{
    using System;

    using Rosetta.Translation;

    /// <summary>
    /// Class describing a method signature for definitions.
    /// 
    /// TODO: Move to a separate project, this is specific to ScriptSharp.
    /// </summary>
    public class ConstructorDefinitionTranslationUnit : MethodSignatureDeclarationTranslationUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorDefinitionTranslationUnit"/> class.
        /// </summary>
        protected ConstructorDefinitionTranslationUnit() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorDefinitionTranslationUnit"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="visibility"></param>
        protected ConstructorDefinitionTranslationUnit(VisibilityToken visibility)
            : base(IdentifierTranslationUnit.Empty, visibility)
        {
            this.Name = IdentifierTranslationUnit.Create(Lexems.ConstructorKeyword);
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="ConstructorDefinitionTranslationUnit"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public ConstructorDefinitionTranslationUnit(ConstructorDefinitionTranslationUnit other)
            : base(other)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibility"></param>
        /// <returns></returns>
        public static ConstructorDefinitionTranslationUnit Create(VisibilityToken visibility)
        {
            return new ConstructorDefinitionTranslationUnit()
            {
                Visibility = visibility,
                Name = IdentifierTranslationUnit.Create(Lexems.ConstructorKeyword),
                ReturnType = null
            };
        }

        protected override string RenderedVisibilityModifier
        {
            get
            {
                if (this.Visibility.HasFlag(VisibilityToken.Protected))
                {
                    // If protected, emit the visibility modifier
                    return this.Visibility.ConvertToTypeScriptEquivalent().EmitOptionalVisibility();
                }

                return string.Empty;
            }
        }

        protected override bool ShouldRenderReturnType
        {
            get { return false; }
        }
    }
}
