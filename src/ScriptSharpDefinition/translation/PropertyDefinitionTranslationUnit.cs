/// <summary>
/// PropertyDefinitionTranslationUnit.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.Translation
{
    using System;

    using Rosetta.Translation;

    /// <summary>
    /// Class describing properties.
    /// </summary>
    /// <remarks>
    /// Internal members protected for testability.
    /// </remarks>
    public class PropertyDefinitionTranslationUnit : MemberTranslationUnit, ITranslationUnit
    {
        private const string ValueSetParameterName = "value";

        protected ITranslationUnit type;

        protected bool hasGet;
        protected bool hasSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyDefinitionTranslationUnit"/> class.
        /// </summary>
        protected PropertyDefinitionTranslationUnit()
            : this(IdentifierTranslationUnit.Empty, IdentifierTranslationUnit.Empty, VisibilityToken.None)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyDefinitionTranslationUnit"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="returnType"></param>
        /// <param name="visibility"></param>
        protected PropertyDefinitionTranslationUnit(ITranslationUnit name, ITranslationUnit returnType, VisibilityToken visibility)
            : base(name, visibility)
        {
            this.type = returnType;

            this.hasGet = true;
            this.hasSet = true;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="PropertyDefinitionTranslationUnit"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public PropertyDefinitionTranslationUnit(PropertyDefinitionTranslationUnit other)
            : base((MemberTranslationUnit)other)
        {
            this.type = other.type;
            this.hasGet = other.hasGet;
            this.hasSet = other.hasSet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="hasGet"></param>
        /// <param name="hasSet"></param>
        /// <returns></returns>
        public static PropertyDefinitionTranslationUnit Create(
            VisibilityToken visibility, ITranslationUnit type, ITranslationUnit name, bool hasGet = true, bool hasSet = true)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return new PropertyDefinitionTranslationUnit()
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
        public string Translate()
        {
            FormatWriter writer = new FormatWriter()
            {
                Formatter = this.Formatter
            };

            if (this.hasGet)
            {
                // Opening declaration: [<visibility>] get <name>() : <type> {
                // TODO: Handle case of no visibility specified
                writer.WriteLine("{0}{1}{2}{3} {4} {5}{6}",
                    this.RenderedVisibilityModifier,
                    Lexems.GetKeyword,
                    this.Name.Translate(),
                    Lexems.OpenRoundBracket + Lexems.CloseRoundBracket,
                    Lexems.Colon,
                    this.type.Translate(),
                    this.hasSet ? Lexems.Semicolon : string.Empty); // TODO: Find a better way for this
            }

            if (this.hasSet)
            {
                var valueParameter = ArgumentDefinitionTranslationUnit.Create(
                    this.type, IdentifierTranslationUnit.Create("value"));

                // Opening declaration: [<visibility>] set <name>(value : <type>) {
                writer.WriteLine("{0}{1}{2}{3}{4}{5}",
                    this.RenderedVisibilityModifier,
                    Lexems.SetKeyword,
                    this.Name.Translate(),
                    Lexems.OpenRoundBracket,
                    valueParameter.Translate(),
                    Lexems.CloseRoundBracket);
            }

            return writer.ToString();
        }

        protected virtual string RenderedVisibilityModifier
        {
            get
            {
                if (this.Visibility.HasFlag(VisibilityToken.Protected))
                {
                    // If protected, emit the visibility modifier
                    return TokenUtility.EmitOptionalVisibility(this.Visibility);
                }

                return string.Empty;
            }
        }
    }
}
