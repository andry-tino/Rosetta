/// <summary>
/// MethodDeclarationTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Class describing methods.
    /// </summary>
    /// <remarks>
    /// Internal members protected for testability.
    /// </remarks>
    public class MethodDeclarationTranslationUnit : MethodSignatureDeclarationTranslationUnit
    {
        // TODO: Use StatementsGroupTranslationUnit
        protected IEnumerable<ITranslationUnit> statements;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodDeclarationTranslationUnit"/> class.
        /// </summary>
        protected MethodDeclarationTranslationUnit() : base(IdentifierTranslationUnit.Empty, ModifierTokens.None)
        {
            this.statements = new List<ITranslationUnit>();
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="MethodDeclarationTranslationUnit"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public MethodDeclarationTranslationUnit(MethodDeclarationTranslationUnit other)
            : base(other)
        {
            this.statements = other.statements;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modifiers"></param>
        /// <param name="returnType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public new static MethodDeclarationTranslationUnit Create(
            ModifierTokens modifiers, ITranslationUnit returnType, ITranslationUnit name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return new MethodDeclarationTranslationUnit()
            {
                Modifiers = modifiers,
                Name = name,
                ReturnType = returnType
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public override IEnumerable<ITranslationUnit> InnerUnits
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
        public override string Translate()
        {
            FormatWriter writer = new FormatWriter()
            {
                Formatter = this.Formatter
            };

            // Opening declaration: [<modifiers>] <method-name>(<params>) : <type> {
            // TODO: Handle case of no visibility specified
            string modifiers = this.RenderedModifiers;

            if (this.ShouldRenderReturnType)
            {
                writer.WriteLine("{0}{1}{2} {3} {4} {5}",
                modifiers,
                this.RenderedName,
                SyntaxUtility.ToBracketEnclosedList(this.Arguments.Select(unit => unit.Translate())),
                Lexems.Colon,
                this.ReturnType.Translate(),
                Lexems.OpenCurlyBracket);
            }
            else
            {
                writer.WriteLine("{0}{1}{2} {3}",
                modifiers,
                this.RenderedName,
                SyntaxUtility.ToBracketEnclosedList(this.Arguments.Select(unit => unit.Translate())),
                Lexems.OpenCurlyBracket);
            }

            // Statements
            // The body, we render them as a list of semicolon/newline separated elements
            foreach (ITranslationUnit statement in this.statements)
            {
                writer.WriteLine("{0}{1}", 
                    statement.Translate(),
                    ShouldRenderSemicolon(statement) ? Lexems.Semicolon : string.Empty);
            }

            // Closing declaration
            writer.WriteLine("{0}", Lexems.CloseCurlyBracket);

            return writer.ToString();
        }

        #region Compound translation unit methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translationUnit"></param>
        public void AddStatement(ITranslationUnit translationUnit)
        {
            if (translationUnit == null)
            {
                throw new ArgumentNullException(nameof(translationUnit));
            }

            if (translationUnit as NestedElementTranslationUnit != null)
            {
                ((NestedElementTranslationUnit)translationUnit).NestingLevel = this.NestingLevel + 1;
            }

            ((List<ITranslationUnit>)this.statements).Add(translationUnit);
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
