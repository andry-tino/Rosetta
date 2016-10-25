/// <summary>
/// ModuleTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Linq;
    using System.IO;
    using System.Collections.Generic;

    /// <summary>
    /// Class describing modules.
    /// </summary>
    /// <remarks>
    /// Internal members protected for testability.
    /// </remarks>
    public class ModuleTranslationUnit : NestedElementTranslationUnit, ITranslationUnit, ICompoundTranslationUnit
    {
        protected IEnumerable<ITranslationUnit> classes;
        protected IEnumerable<ITranslationUnit> interfaces;

        protected ITranslationUnit name;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleTranslationUnit"/> class.
        /// </summary>
        protected ModuleTranslationUnit() 
            : this(AutomaticNestingLevel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleTranslationUnit"/> class.
        /// </summary>
        /// <param name="nestingLevel"></param>
        protected ModuleTranslationUnit(int nestingLevel) 
            : this(IdentifierTranslationUnit.Empty, nestingLevel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleTranslationUnit"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="nestingLevel"></param>
        protected ModuleTranslationUnit(ITranslationUnit name, int nestingLevel) 
            : base(nestingLevel)
        {
            this.classes = new List<ITranslationUnit>();
            this.interfaces = new List<ITranslationUnit>();

            this.name = name;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="ModuleTranslationUnit"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public ModuleTranslationUnit(ModuleTranslationUnit other) 
            : base()
        {
            this.classes = other.classes;
            this.interfaces = other.interfaces;

            this.name = other.name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ModuleTranslationUnit Create(ITranslationUnit name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return new ModuleTranslationUnit(name, AutomaticNestingLevel);
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ITranslationUnit> InnerUnits
        {
            get
            {
                return this.classes.Concat(this.interfaces);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the module is part, as a member, of a namespace or module or not.
        /// </summary>
        public bool IsAtRootLevel { get; set; }

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
            writer.WriteLine("{0} {1} {2} {3}",
                text => ClassDeclarationCodePerfect.RefineDeclaration(text),
                this.RenderedModuleAccessorKeyword,
                Lexems.ModuleKeyword,
                this.name.Translate(),
                Lexems.OpenCurlyBracket);

            // We render classes first
            var lastClass = this.classes.Count() > 0 ? this.classes.Last() : null;
            foreach (ITranslationUnit translationUnit in this.classes)
            {
                writer.WriteLine(translationUnit.Translate());

                if ((object)translationUnit != (object)lastClass)
                {
                    writer.WriteLine(string.Empty);
                }
            }

            // Then, interfaces
            var lastInterface = this.interfaces.Count() > 0 ? this.interfaces.Last() : null;
            foreach (ITranslationUnit translationUnit in this.interfaces)
            {
                // TODO: Handle with injection like in classes
                writer.WriteLine("{0} {1}",
                    Lexems.ExportKeyword,
                    translationUnit.Translate());

                if ((object)translationUnit != (object)lastInterface)
                {
                    writer.WriteLine(string.Empty);
                }
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
        public void AddClass(ITranslationUnit translationUnit)
        {
            if (translationUnit == null)
            {
                throw new ArgumentNullException(nameof(translationUnit));
            }

            if (translationUnit as NestedElementTranslationUnit != null)
            {
                ((NestedElementTranslationUnit)translationUnit).NestingLevel = this.NestingLevel + 1;
            }

            // Classes need injection for observing indentation
            if (translationUnit as ITranslationInjector != null)
            {
                ((ITranslationInjector)translationUnit).InjectedTranslationUnitBefore = IdentifierTranslationUnit.Create(Lexems.ExportKeyword);
            }

            ((List<ITranslationUnit>)this.classes).Add(translationUnit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translationUnit"></param>
        public void AddInterface(ITranslationUnit translationUnit)
        {
            if (translationUnit == null)
            {
                throw new ArgumentNullException(nameof(translationUnit));
            }

            if (translationUnit as NestedElementTranslationUnit != null)
            {
                ((NestedElementTranslationUnit)translationUnit).NestingLevel = this.NestingLevel + 1;
            }

            ((List<ITranslationUnit>)this.interfaces).Add(translationUnit);
        }

        #endregion

        protected virtual string RenderedModuleAccessorKeyword
        {
            get { return Lexems.ExportKeyword; }
        }
    }
}
