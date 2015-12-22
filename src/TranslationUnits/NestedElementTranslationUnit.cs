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
        /// Utility method used to safely increment the nesting level of an <see cref="ITranslationUnit"/> only in case it supports
        /// nesting level.
        /// </summary>
        /// <param name="translationUnit">The <see cref="ITranslationUnit"/> whose nesting level should be incremented.</param>
        /// <param name="parentTranslationUnit">The <see cref="ITranslationUnit"/> which is supposed to be the parent.</param>
        /// <remarks>
        /// This will cause <paramref name="translationUnit"/> to be checked. In case it supports nestin level, then its
        /// nesting level is changed in order to have the nesting level of <paramref name="parentTranslationUnit"/> + 1.
        /// </remarks>
        public static void IncrementNestingLevel(ITranslationUnit translationUnit, ITranslationUnit parentTranslationUnit)
        {
            NestedElementTranslationUnit nestedTranslationUnit = translationUnit as NestedElementTranslationUnit;
            NestedElementTranslationUnit nestedParentTranslationUnit = parentTranslationUnit as NestedElementTranslationUnit;

            if (nestedTranslationUnit == null || nestedParentTranslationUnit == null)
            {
                return;
            }

            nestedTranslationUnit.NestingLevel = nestedParentTranslationUnit.NestingLevel + 1;
        }

        /// <summary>
        /// Ensures that compound <see cref="ITranslationUnit"/> will increment added unit's nesting level.
        /// TODO: Use this!
        /// </summary>
        /// <param name="translationUnit">The translation unit to add.</param>
        protected virtual void AddTranslationUnit(ITranslationUnit translationUnit)
        {
            if (translationUnit as NestedElementTranslationUnit != null)
            {
                ((NestedElementTranslationUnit)translationUnit).NestingLevel = this.NestingLevel + 1;
            }

            this.AddTranslationUnitCore(translationUnit);
        }

        /// <summary>
        /// Logic to provide in subclasses to add <see cref="ITranslationUnit"/> (for compound translation units).
        /// 
        /// TODO: Turn this into abstract and make all unit use one method for add compound units. 
        /// TODO: Add to ICompoundUnit interface and have classes distinguish uisng RTTI!
        /// </summary>
        /// <param name="translationUnit">The translation unit to add.</param>
        protected virtual void AddTranslationUnitCore(ITranslationUnit translationUnit) { }

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
