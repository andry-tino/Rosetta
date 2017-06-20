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
        protected ConstructorDefinitionTranslationUnit(ModifierTokens modifiers)
            : base(IdentifierTranslationUnit.Empty, modifiers)
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
        /// <param name="modifiers"></param>
        /// <returns></returns>
        public static ConstructorDefinitionTranslationUnit Create(ModifierTokens modifiers)
        {
            return new ConstructorDefinitionTranslationUnit()
            {
                Modifiers = modifiers,
                Name = IdentifierTranslationUnit.Create(Lexems.ConstructorKeyword),
                ReturnType = null
            };
        }

        protected override string RenderedModifiers => this.Modifiers.ConvertToTypeScriptEquivalent().StripPublic().EmitOptionalVisibility();

        protected override bool ShouldRenderReturnType
        {
            get { return false; }
        }
    }
}
