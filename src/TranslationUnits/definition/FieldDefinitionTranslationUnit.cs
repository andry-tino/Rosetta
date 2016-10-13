/// <summary>
/// FieldDefinitionTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Class describing a method signature (no body).
    /// 
    /// TODO: Move to a separate project, this is specific to ScriptSharp.
    /// </summary>
    public class FieldDefinitionTranslationUnit : FieldDeclarationTranslationUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldDefinitionTranslationUnit"/> class.
        /// </summary>
        protected FieldDefinitionTranslationUnit() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldDefinitionTranslationUnit"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="visibility"></param>
        protected FieldDefinitionTranslationUnit(ITranslationUnit name, VisibilityToken visibility) : base(name, visibility)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static FieldDefinitionTranslationUnit Create(VisibilityToken visibility, ITranslationUnit type, ITranslationUnit name)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return new FieldDefinitionTranslationUnit()
            {
                Visibility = visibility,
                Name = name,
                Type = type
            };
        }
    }
}
