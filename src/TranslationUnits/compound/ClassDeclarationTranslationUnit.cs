/// <summary>
/// ClassDeclarationTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for describing compound translation elements.
    /// </summary>
    public class ClassDeclarationTranslationUnit : ScopedElementTranslationUnit, ITranslationUnit, ICompoundTranslationUnit
    {
        // Inner units
        private IEnumerable<ITranslationUnit> memberDeclarations;
        private IEnumerable<ITranslationUnit> constructorDeclarations;
        private IEnumerable<ITranslationUnit> propertyDeclarations;
        private IEnumerable<ITranslationUnit> methodDeclarations;

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

            writer.WriteLine("{0} class {1} {2} {3}", 
                text => ClassDeclarationCodePerfect.RefineDeclaration(text), 
                classVisibility, 
                this.Name.Translate(),
                baseList, 
                Lexems.OpenCurlyBracket);

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
