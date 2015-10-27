/// <summary>
/// NestedElementTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Class describing nested elements.
    /// </summary>
    public abstract class NestedElementTranslationUnit
    {
        /// <summary> Let the system decide the nesting level based on parent/child relationship. </summary>
        public const int AutomaticNestingLevel = -1;

        private int nestingLevel;
        private IFormatter formatter;

        /// <summary>
        /// The nesting level.
        /// </summary>
        protected int NestingLevel
        {
            get { return this.nestingLevel; }

            set
            {
                int oldValue = this.nestingLevel;
                this.nestingLevel = value;

                if (oldValue != value)
                {
                    this.OnNestingLevelChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the formatter.
        /// </summary>
        public IFormatter Formatter
        {
            get
            {
                if (this.formatter == null)
                {
                    this.formatter = new WhiteSpaceFormatter();
                }

                return this.formatter;
            }

            set { this.formatter = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NestedElementTranslationUnit"/>.
        /// </summary>
        protected NestedElementTranslationUnit() :
            this(AutomaticNestingLevel)
        {
            this.nestingLevel = AutomaticNestingLevel;
            this.formatter = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NestedElementTranslationUnit"/>.
        /// </summary>
        /// <param name="nestingLevel"></param>
        protected NestedElementTranslationUnit(int nestingLevel)
        {
            this.NestingLevel = nestingLevel;
        }

        /// <summary>
        /// When the indentation level changes, we need to apply a different formatter.
        /// </summary>
        protected virtual void OnNestingLevelChanged()
        {
            int indentationLevel = this.NestingLevel == AutomaticNestingLevel ? 0 : this.NestingLevel;
            this.Formatter = new WhiteSpaceFormatter(indentationLevel);
        }
    }
}
