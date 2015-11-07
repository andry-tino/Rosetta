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
    public class ModuleTranslationUnit : NestedElementTranslationUnit, ITranslationUnit, ICompoundTranslationUnit
    {
        private IEnumerable<ITranslationUnit> classes;
        private IEnumerable<ITranslationUnit> interfaces;

        private ITranslationUnit name;

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
                Lexems.ExportKeyword,
                Lexems.ModuleKeyword,
                this.name.Translate(),
                Lexems.OpenCurlyBracket);

            // We render classes first
            foreach (ITranslationUnit translationUnit in this.classes)
            {
                if (translationUnit as NestedElementTranslationUnit != null)
                {
                    ((NestedElementTranslationUnit)translationUnit).NestingLevel = this.NestingLevel + 1;
                }

                // Classes need injection for observing indentation
                if (translationUnit as ITranslationInjector != null)
                {
                    ((ITranslationInjector)translationUnit).InjectedTranslationUnitBefore = IdentifierTranslationUnit.Create(Lexems.ExportKeyword);
                }

                writer.WriteLine(translationUnit.Translate());
            }

            // Then, interfaces
            foreach (ITranslationUnit translationUnit in this.interfaces)
            {
                if (translationUnit as NestedElementTranslationUnit != null)
                {
                    ((NestedElementTranslationUnit)translationUnit).NestingLevel = this.NestingLevel + 1;
                }
                writer.WriteLine("{0} {1}",
                    Lexems.ExportKeyword,
                    translationUnit.Translate());
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

            ((List<ITranslationUnit>)this.interfaces).Add(translationUnit);
        }

        #endregion
    }
}
