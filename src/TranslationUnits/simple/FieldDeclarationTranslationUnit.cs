/// <summary>
/// FieldDeclarationTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

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
            this.Type = null;
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
                Type = type
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
            string fieldVisibility = this.RenderedVisibilityModifier;

            writer.Write("{0}{1} {2} {3}",
                text => ClassDeclarationCodePerfect.RefineDeclaration(text),
                this.Visibility.ConvertToTypeScriptEquivalent().EmitOptionalVisibility(),
                this.Name.Translate(),
                Lexems.Colon,
                this.type.Translate());

            return writer.ToString();
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        protected ITranslationUnit Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        protected virtual string RenderedVisibilityModifier
        {
            get { return TokenUtility.EmitOptionalVisibility(this.Visibility); }
        }
    }
}
