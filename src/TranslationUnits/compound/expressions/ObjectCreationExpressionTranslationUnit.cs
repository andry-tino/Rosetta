/// <summary>
/// ObjectCreationExpressionTranslationUnit.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class describing member access syntaxes.
    /// </summary>
    public class ObjectCreationExpressionTranslationUnit : ExpressionTranslationUnit, ICompoundTranslationUnit
    {
        private ITranslationUnit type;
        private IEnumerable<ITranslationUnit> arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectCreationExpressionTranslationUnit"/> class.
        /// </summary>
        protected ObjectCreationExpressionTranslationUnit()
            : this(AutomaticNestingLevel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectCreationExpressionTranslationUnit"/> class.
        /// </summary>
        /// <param name="nestingLevel"></param>
        protected ObjectCreationExpressionTranslationUnit(int nestingLevel)
            : base(nestingLevel)
        {
            this.arguments = null;
            this.arguments = new List<ITranslationUnit>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="member"></param>
        /// <param name="accessMethod"></param>
        /// <returns></returns>
        public new static ObjectCreationExpressionTranslationUnit Create(ITranslationUnit typeName)
        {
            if (typeName == null)
            {
                throw new ArgumentNullException(nameof(typeName));
            }

            return new ObjectCreationExpressionTranslationUnit(AutomaticNestingLevel)
            {
                type = typeName
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ITranslationUnit> InnerUnits
        {
            get
            {
                return this.arguments;
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

            // Invokation: <expression>([<params>])
            writer.WriteLine("{0} {1}{2}",
                Lexems.NewKeyword,
                this.type.Translate(),
                SyntaxUtility.ToBracketEnclosedList(this.arguments.Select(unit => unit.Translate()))
                );

            return writer.ToString();
        }

        #region Compound translation unit methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translationUnit"></param>
        public void AddArgument(ITranslationUnit translationUnit)
        {
            if (translationUnit == null)
            {
                throw new ArgumentNullException(nameof(translationUnit));
            }

            ((List<ITranslationUnit>)this.arguments).Add(translationUnit);
        }

        #endregion
    }
}
