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
        private Func<int, IFormatter> formatterProvider;
        
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
        /// Gets the formatter currently used.
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

            private set { this.formatter = value; }
        }

        /// <summary>
        /// Gets or sets the formatter provider.
        /// The formatter changes dynamically as the indentation level in the translation unit changes, thus
        /// a new formatter needs to be created when <see cref="OnNestingLevelChanged"/> is fired.
        /// This property allows the user to inject the type of formatter to create everytime a new one is needed.
        /// </summary>
        /// <remarks>
        /// When a new formatter provider is set, we need to change <see cref="Formatter"/> too. The new formatter
        /// is fed with <see cref="NestingLevel"/> in order to match current indentation.
        /// </remarks>
        public Func<int, IFormatter> FormatterProvider
        {
            get
            {
                if (this.formatterProvider == null)
                {
                    this.formatterProvider = 
                        indentationLevel => new WhiteSpaceFormatter(indentationLevel);
                }

                return this.formatterProvider;
            }

            set
            {
                this.formatterProvider = value;
                this.formatter = this.formatterProvider(this.NestingLevel);
            }
        }

        /// <summary>
        /// The nesting level.
        /// </summary>
        public int NestingLevel
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
        /// When the indentation level changes, we need to apply a different formatter.
        /// When the nesting level changes, this procedure will adjust it in case the 
        /// automatic nesting level has been set to automatic.
        /// </summary>
        protected virtual void OnNestingLevelChanged()
        {
            this.nestingLevel = this.NestingLevel == AutomaticNestingLevel ? 0 : this.NestingLevel;
            this.Formatter = this.FormatterProvider(this.nestingLevel);
        }
    }
}
