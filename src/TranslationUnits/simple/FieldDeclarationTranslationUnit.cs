/// <summary>
/// FieldDeclarationTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Linq;
    using System.IO;
    using System.Collections.Generic;

    /// <summary>
    /// Class describing a method signature (no body).
    /// </summary>
    public class FieldDeclarationTranslationUnit : MemberTranslationUnit, ITranslationUnit
    {
        private ITranslationUnit type;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldDeclarationTranslationUnit"/> class.
        /// </summary>
        protected FieldDeclarationTranslationUnit()
            : this(IdentifierTranslationUnit.Empty, VisibilityToken.None)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldDeclarationTranslationUnit"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="visibility"></param>
        protected FieldDeclarationTranslationUnit(ITranslationUnit name, VisibilityToken visibility)
            : base(name, visibility)
        {
            this.type = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static FieldDeclarationTranslationUnit Create(VisibilityToken visibility, ITranslationUnit type, ITranslationUnit name)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return new FieldDeclarationTranslationUnit()
            {
                Visibility = visibility,
                Name = name,
                type = type
            };
        }

        /// <summary>
        /// Translate the unit into TypeScript.
        /// </summary>
        /// <returns></returns>
        public virtual string Translate()
        {
            FormatWriter writer = new FormatWriter()
            {
                Formatter = this.Formatter
            };

            // Opening declaration
            writer.Write("{0} {1} {2} {3}",
                text => ClassDeclarationCodePerfect.RefineDeclaration(text),
                TokenUtility.ToString(this.Visibility),
                this.Name.Translate(),
                Lexems.Colon,
                this.type.Translate());

            return writer.ToString();
        }
    }
}
