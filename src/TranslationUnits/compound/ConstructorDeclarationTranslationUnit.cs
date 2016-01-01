/// <summary>
/// ConstructorDeclarationTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Class describing constructors.
    /// </summary>
    public class ConstructorDeclarationTranslationUnit : MethodDeclarationTranslationUnit
    {
        private IEnumerable<ITranslationUnit> initializers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorDeclarationTranslationUnit"/> class.
        /// </summary>
        protected ConstructorDeclarationTranslationUnit() : base()
        {
            this.initializers = new List<ITranslationUnit>();
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="ConstructorDeclarationTranslationUnit"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public ConstructorDeclarationTranslationUnit(ConstructorDeclarationTranslationUnit other)
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
        public new static ConstructorDeclarationTranslationUnit Create(VisibilityToken visibility, ITranslationUnit name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return new ConstructorDeclarationTranslationUnit()
            {
                Visibility = visibility,
                Name = name,
                ReturnType = null
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

            // Opening declaration
            writer.WriteLine("{0} {1}{2} {3}",
                TokenUtility.ToString(this.Visibility),
                this.Name.Translate(),
                SyntaxUtility.ToBracketEnclosedList(this.Arguments.Select(unit => unit.Translate())),
                Lexems.OpenCurlyBracket);

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

        // TODO

        #endregion
    }
}
