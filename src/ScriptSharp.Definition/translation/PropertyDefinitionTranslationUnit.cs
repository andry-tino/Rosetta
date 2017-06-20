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
            : this(IdentifierTranslationUnit.Empty, IdentifierTranslationUnit.Empty, ModifierTokens.None)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyDefinitionTranslationUnit"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="returnType"></param>
        /// <param name="visibility"></param>
        protected PropertyDefinitionTranslationUnit(ITranslationUnit name, ITranslationUnit returnType, ModifierTokens visibility)
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
        /// <param name="modifiers"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="hasGet"></param>
        /// <param name="hasSet"></param>
        /// <returns></returns>
        public static PropertyDefinitionTranslationUnit Create(
            ModifierTokens modifiers, ITranslationUnit type, ITranslationUnit name, bool hasGet = true, bool hasSet = true)
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
                Modifiers = modifiers,
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
                // Opening declaration: [<modifiers>] get <name>() : <type> {
                // TODO: Handle case of no visibility specified
                writer.WriteLine("{0}{1}{2} {3} {4}{5}",
                    this.RenderedModifiers,
                    this.RenderedGetterMethodName,
                    Lexems.OpenRoundBracket + Lexems.CloseRoundBracket,
                    Lexems.Colon,
                    this.type.Translate(),
                    this.hasSet ? Lexems.Semicolon : string.Empty); // TODO: Find a better way for this
            }

            if (this.hasSet)
            {
                var valueParameter = ArgumentDefinitionTranslationUnit.Create(
                    this.type, IdentifierTranslationUnit.Create("value"));

                // Opening declaration: [<modifiers>] set <name>(value : <type>) : void {
                // Emitting `void` in order to prevent errors in case of implicitAllowAny
                writer.WriteLine("{0}{1}{2}{3}{4} {5} {6}",
                    this.RenderedModifiers,
                    this.RenderedSetterMethodName,
                    Lexems.OpenRoundBracket,
                    valueParameter.Translate(),
                    Lexems.CloseRoundBracket,
                    Lexems.Colon,
                    Lexems.VoidReturnType);
            }

            return writer.ToString();
        }
        
        protected virtual string RenderedModifiers => this.Modifiers.ConvertToTypeScriptEquivalent().StripPublic().EmitOptionalVisibility();

        private string RenderedName => this.Name.Translate();

        private string RenderedGetterMethodName => $"{Lexems.GetKeyword}_{this.RenderedName}";

        private string RenderedSetterMethodName => $"{Lexems.SetKeyword}_{this.RenderedName}";
    }
}
