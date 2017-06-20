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
    public class MethodDefinitionTranslationUnit : MethodSignatureDeclarationTranslationUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodDefinitionTranslationUnit"/> class.
        /// </summary>
        protected MethodDefinitionTranslationUnit() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodDefinitionTranslationUnit"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="modifiers"></param>
        protected MethodDefinitionTranslationUnit(ITranslationUnit name, ModifierTokens modifiers) 
            : base(name, modifiers)
        {
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="MethodDefinitionTranslationUnit"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public MethodDefinitionTranslationUnit(MethodDefinitionTranslationUnit other)
            : base(other)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="returnType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static new MethodDefinitionTranslationUnit Create(ModifierTokens modifiers, ITranslationUnit returnType, ITranslationUnit name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return new MethodDefinitionTranslationUnit()
            {
                Modifiers = modifiers,
                Name = name,
                ReturnType = returnType
            };
        }
        
        protected override string RenderedModifiers => this.Modifiers.ConvertToTypeScriptEquivalent().StripPublic().EmitOptionalVisibility();

        // This is in order to prevent errors in case of implicitAllowAny
        protected override bool ShouldRenderReturnType => true;
    }
}
