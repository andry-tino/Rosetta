/// <summary>
/// EnumTranslationUnit.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Describes enums.
    /// </summary>
    /// <remarks>
    /// Internal members protected for testability.
    /// </remarks>
    public class EnumTranslationUnit : ScopedElementTranslationUnit,
        ITranslationUnit, ICompoundTranslationUnit, ITranslationInjector
    {
        // Inner units
        protected IEnumerable<ITranslationUnit> members;

        // Injected units
        protected ITranslationUnit injectedBefore;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumTranslationUnit"/> class.
        /// </summary>
        protected EnumTranslationUnit() : base()
        {
            this.Name = IdentifierTranslationUnit.Empty;
            this.members = new List<ITranslationUnit>();

            this.injectedBefore = null;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="EnumTranslationUnit"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public EnumTranslationUnit(EnumTranslationUnit other)
            : base(other)
        {
            this.Name = other.Name;
            this.members = other.members;

            this.injectedBefore = other.injectedBefore;
        }

        protected ITranslationUnit Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the class is part, as a member, of a namespace or module or not.
        /// </summary>
        public bool IsAtRootLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static EnumTranslationUnit Create(VisibilityToken visibility, ITranslationUnit name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name), "Enum name cannot be null!");
            }

            return new EnumTranslationUnit()
            {
                Visibility = visibility,
                Name = name
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ITranslationUnit> InnerUnits
        {
            get { return this.members; }
        }

        /// <summary>
        /// Sets the <see cref="ITranslationUnit"/> to concatenate 
        /// before the translation of the main one.
        /// </summary>
        public ITranslationUnit InjectedTranslationUnitBefore
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.injectedBefore = value;
            }
        }

        /// <summary>
        /// Sets the <see cref="ITranslationUnit"/> to concatenate 
        /// after the translation of the main one.
        /// </summary>
        public ITranslationUnit InjectedTranslationUnitAfter
        {
            set
            {
                throw new NotImplementedException("This class does not support injection after the main translation!");
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

            // Opening declaration
            string enumVisibility = this.RenderedVisibilityModifier;

            if (this.injectedBefore == null)
            {
                writer.WriteLine("{0}{1} {2} {3}",
                text => ClassDeclarationCodePerfect.RefineDeclaration(text),
                enumVisibility,
                Lexems.EnumKeyword,
                this.Name.Translate(),
                Lexems.OpenCurlyBracket);
            }
            else
            {
                writer.WriteLine("{0} {1}{2} {3} {4}",
                text => ClassDeclarationCodePerfect.RefineDeclaration(text),
                this.injectedBefore.Translate(),
                enumVisibility,
                Lexems.EnumKeyword,
                this.Name.Translate(),
                Lexems.OpenCurlyBracket);
            }

            if (this.ShouldRenderMembers)
            {
                var lastMember = this.members.Count() > 0 ? this.members.Last() : null;
                foreach (ITranslationUnit translationUnit in this.members)
                {
                    if ((object)translationUnit == (object)lastMember)
                    {
                        writer.WriteLine("{0}", translationUnit.Translate());
                    }
                    else
                    {
                        writer.WriteLine("{0}{1}", translationUnit.Translate(), Lexems.Comma);
                    }
                }
            }

            // Closing
            writer.WriteLine("{0}",
                text => ClassDeclarationCodePerfect.RefineDeclaration(text),
                Lexems.CloseCurlyBracket);

            return writer.ToString();
        }

        #region Compound translation unit methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translationUnit"></param>
        public void AddMember(ITranslationUnit translationUnit)
        {
            if (translationUnit == null)
            {
                throw new ArgumentNullException(nameof(translationUnit));
            }

            if (translationUnit as NestedElementTranslationUnit != null)
            {
                ((NestedElementTranslationUnit)translationUnit).NestingLevel = this.NestingLevel + 1;
            }

            ((List<ITranslationUnit>)this.members).Add(translationUnit);
        }

        #endregion
        
        protected virtual string RenderedVisibilityModifier
        {
            get { return this.Visibility.ConvertToTypeScriptEquivalent().EmitOptionalVisibility(); }
        }

        protected virtual bool ShouldRenderMembers
        {
            get { return true; }
        }
    }
}
