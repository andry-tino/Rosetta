/// <summary>
/// PropertyDeclarationTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Class describing properties.
    /// </summary>
    /// <remarks>
    /// Internal members protected for testability.
    /// </remarks>
    public class PropertyDeclarationTranslationUnit : MemberTranslationUnit, ITranslationUnit, ICompoundTranslationUnit
    {
        private const string ValueSetParameterName = "value";

        // Statements groups
        protected ITranslationUnit getStatements;
        protected ITranslationUnit setStatements;

        protected ITranslationUnit type;

        protected bool hasGet;
        protected bool hasSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyDeclarationTranslationUnit"/> class.
        /// </summary>
        protected PropertyDeclarationTranslationUnit() 
            : this(IdentifierTranslationUnit.Empty, IdentifierTranslationUnit.Empty, ModifierTokens.None)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyDeclarationTranslationUnit"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="returnType"></param>
        /// <param name="modifiers"></param>
        protected PropertyDeclarationTranslationUnit(ITranslationUnit name, ITranslationUnit returnType, ModifierTokens modifiers) 
            : base(name, modifiers)
        {
            // We create empty groups
            this.getStatements = StatementsGroupTranslationUnit.Create();
            this.setStatements = StatementsGroupTranslationUnit.Create();

            this.type = returnType;

            this.hasGet = true;
            this.hasSet = true;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="MethodDeclarationTranslationUnit"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public PropertyDeclarationTranslationUnit(PropertyDeclarationTranslationUnit other)
            : base((MemberTranslationUnit)other)
        {
            this.getStatements = other.getStatements;
            this.setStatements = other.setStatements;
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
        public static PropertyDeclarationTranslationUnit Create(
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

            return new PropertyDeclarationTranslationUnit()
            {
                Modifiers = modifiers,
                Name = name,
                type = type
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ITranslationUnit> InnerUnits
        {
            get
            {
                throw new NotImplementedException();
            }
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
                writer.WriteLine("{0}{1} {2}{3} {4} {5} {6}",
                    this.Modifiers.ConvertToTypeScriptEquivalent().EmitOptionalVisibility(),
                    Lexems.GetKeyword,
                    this.Name.Translate(),
                    Lexems.OpenRoundBracket + Lexems.CloseRoundBracket,
                    Lexems.Colon,
                    this.type.Translate(),
                    Lexems.OpenCurlyBracket);

                writer.WriteLine("{0}", 
                    this.getStatements.Translate());

                // Closing declaration
                writer.WriteLine("{0}", Lexems.CloseCurlyBracket);
            }

            if (this.hasSet)
            {
                var valueParameter = ArgumentDefinitionTranslationUnit.Create(
                    this.type, IdentifierTranslationUnit.Create("value"));

                // Opening declaration: [<visibility>] set <name>(value : <type>) {
                writer.WriteLine("{0}{1} {2}{3}{4}{5} {6}",
                    this.Modifiers.ConvertToTypeScriptEquivalent().EmitOptionalVisibility(),
                    Lexems.SetKeyword,
                    this.Name.Translate(),
                    Lexems.OpenRoundBracket,
                    valueParameter.Translate(),
                    Lexems.CloseRoundBracket,
                    Lexems.OpenCurlyBracket);

                writer.WriteLine("{0}",
                    this.setStatements.Translate());

                // Closing declaration
                writer.WriteLine("{0}", Lexems.CloseCurlyBracket);
            }

            return writer.ToString();
        }

        #region Compound translation unit methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translationUnit"></param>
        public void SetGetAccessor(ITranslationUnit translationUnit)
        {
            if (translationUnit == null)
            {
                throw new ArgumentNullException(nameof(translationUnit));
            }

            if (translationUnit as NestedElementTranslationUnit != null)
            {
                ((NestedElementTranslationUnit)translationUnit).NestingLevel = this.NestingLevel + 1;
            }

            this.getStatements = translationUnit;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translationUnit"></param>
        public void SetSetAccessor(ITranslationUnit translationUnit)
        {
            if (translationUnit == null)
            {
                throw new ArgumentNullException(nameof(translationUnit));
            }

            if (translationUnit as NestedElementTranslationUnit != null)
            {
                ((NestedElementTranslationUnit)translationUnit).NestingLevel = this.NestingLevel + 1;
            }

            this.setStatements = translationUnit;
        }

        #endregion

        protected static bool ShouldRenderSemicolon(ITranslationUnit statement)
        {
            var type = statement.GetType();

            var shouldNotRenderSemicolon = type == typeof(ConditionalStatementTranslationUnit);

            return !shouldNotRenderSemicolon;
        }
    }
}
