/// <summary>
/// ClassDeclarationTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for describing compound translation elements.
    /// </summary>
    public class ClassDeclarationTranslationUnit : ScopedElementTranslationUnit, 
        ITranslationUnit, ICompoundTranslationUnit, ITranslationInjector
    {
        // Inner units
        private IEnumerable<ITranslationUnit> memberDeclarations;
        private IEnumerable<ITranslationUnit> constructorDeclarations;
        private IEnumerable<ITranslationUnit> propertyDeclarations;
        private IEnumerable<ITranslationUnit> methodDeclarations;

        // Injected units
        private ITranslationUnit injectedBefore;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassDeclarationTranslationUnit"/> class.
        /// </summary>
        protected ClassDeclarationTranslationUnit() : base()
        {
            this.Name = IdentifierTranslationUnit.Empty;
            this.BaseClassName = null;
            this.Interfaces = new List<ITranslationUnit>();

            this.memberDeclarations = new List<ITranslationUnit>();
            this.constructorDeclarations = new List<ITranslationUnit>();
            this.propertyDeclarations = new List<ITranslationUnit>();
            this.methodDeclarations = new List<ITranslationUnit>();

            this.injectedBefore = null;
        }
        
        private ITranslationUnit Name { get; set; }
        private ITranslationUnit BaseClassName { get; set; }
        private IEnumerable<ITranslationUnit> Interfaces { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="name"></param>
        /// <param name="baseClassName"></param>
        /// <returns></returns>
        public static ClassDeclarationTranslationUnit Create(VisibilityToken visibility, ITranslationUnit name, ITranslationUnit baseClassName)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name), "Class name cannot be null!");
            }

            return new ClassDeclarationTranslationUnit()
            {
                Visibility = visibility,
                Name = name,
                BaseClassName = baseClassName
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="implementedInterface"></param>
        public void AddImplementedInterface(ITranslationUnit implementedInterface)
        {
            if (implementedInterface == null)
            {
                throw new ArgumentNullException(nameof(implementedInterface));
            }

            ((List<ITranslationUnit>)this.Interfaces).Add(implementedInterface);
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ITranslationUnit> InnerUnits
        {
            get
            {
                return 
                    this.constructorDeclarations
                    .Concat(this.constructorDeclarations)
                    .Concat(this.memberDeclarations)
                    .Concat(this.propertyDeclarations);
            }
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
            string classVisibility = TokenUtility.ToString(this.Visibility);
            string baseList = this.BuildClassInheritanceAndInterfaceImplementationList();

            if (this.injectedBefore == null)
            {
                writer.WriteLine("{0} class {1} {2} {3}",
                text => ClassDeclarationCodePerfect.RefineDeclaration(text),
                classVisibility,
                this.Name.Translate(),
                baseList,
                Lexems.OpenCurlyBracket);
            }
            else
            {
                writer.WriteLine("{0} {1} class {2} {3} {4}",
                text => ClassDeclarationCodePerfect.RefineDeclaration(text),
                this.injectedBefore.Translate(),
                classVisibility,
                this.Name.Translate(),
                baseList,
                Lexems.OpenCurlyBracket);
            }

            // Translating members first
            foreach (ITranslationUnit translationUnit in this.memberDeclarations)
            {
                if (translationUnit as NestedElementTranslationUnit != null)
                {
                    ((NestedElementTranslationUnit)translationUnit).NestingLevel = this.NestingLevel + 1;
                }
                writer.WriteLine(translationUnit.Translate());
            }

            // Then constructors
            foreach (ITranslationUnit translationUnit in this.constructorDeclarations)
            {
                if (translationUnit as NestedElementTranslationUnit != null)
                {
                    ((NestedElementTranslationUnit)translationUnit).NestingLevel = this.NestingLevel + 1;
                }
                writer.WriteLine(translationUnit.Translate());
            }

            // Then properties
            foreach (ITranslationUnit translationUnit in this.propertyDeclarations)
            {
                if (translationUnit as NestedElementTranslationUnit != null)
                {
                    ((NestedElementTranslationUnit)translationUnit).NestingLevel = this.NestingLevel + 1;
                }
                writer.WriteLine(translationUnit.Translate());
            }

            // Finally methods
            foreach (ITranslationUnit translationUnit in this.methodDeclarations)
            {
                if (translationUnit as NestedElementTranslationUnit != null)
                {
                    ((NestedElementTranslationUnit)translationUnit).NestingLevel = this.NestingLevel + 1;
                }
                writer.WriteLine(translationUnit.Translate());
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
        public void AddMemberDeclaration(ITranslationUnit translationUnit)
        {
            if (translationUnit == null)
            {
                throw new ArgumentNullException(nameof(translationUnit));
            }

            ((List<ITranslationUnit>)this.memberDeclarations).Add(translationUnit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translationUnit"></param>
        public void AddPropertyDeclaration(ITranslationUnit translationUnit)
        {
            if (translationUnit == null)
            {
                throw new ArgumentNullException(nameof(translationUnit));
            }

            ((List<ITranslationUnit>)this.propertyDeclarations).Add(translationUnit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translationUnit"></param>
        public void AddConstructorDeclaration(ITranslationUnit translationUnit)
        {
            if (translationUnit == null)
            {
                throw new ArgumentNullException(nameof(translationUnit));
            }

            ((List<ITranslationUnit>)this.constructorDeclarations).Add(translationUnit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translationUnit"></param>
        public void AddMethodDeclaration(ITranslationUnit translationUnit)
        {
            if (translationUnit == null)
            {
                throw new ArgumentNullException(nameof(translationUnit));
            }

            ((List<ITranslationUnit>)this.methodDeclarations).Add(translationUnit);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string BuildClassInheritanceAndInterfaceImplementationList()
        {
            string baseClass = string.Empty;
            if (this.BaseClassName != null)
            {
                baseClass = string.Format("{0} {1}", Lexems.ExtendsKeyword, this.BaseClassName.Translate());
            }

            string implementationList = this.Interfaces.Count() > 0 ? 
                string.Format("{0} {1}", Lexems.ImplementsKeyword, SyntaxUtility.ToTokenSeparatedList(
                    this.Interfaces.Select(unit => unit.Translate()), Lexems.Comma + " ")) : 
                string.Empty;
            
            return string.Format("{0} {1}", baseClass, implementationList);
        }
    }
}
