/// <summary>
/// ConstructorDefinitionTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Class describing a method signature for definitions.
    /// 
    /// TODO: Move to a separate project, this is specific to ScriptSharp.
    /// </summary>
    public class ConstructorDefinitionTranslationUnit : ConstructorDeclarationTranslationUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorDefinitionTranslationUnit"/> class.
        /// </summary>
        protected ConstructorDefinitionTranslationUnit() : base()
        {
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
        /// <param name="returnType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ConstructorDefinitionTranslationUnit Create(VisibilityToken visibility, ITranslationUnit name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return new ConstructorDefinitionTranslationUnit()
            {
                Visibility = visibility,
                Name = name,
                ReturnType = null
            };
        }
    }
}
