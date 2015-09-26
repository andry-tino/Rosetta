/// <summary>
/// ClassDeclarationTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.IO;
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
            this.Name = string.Empty;
            this.BaseClassName = null;
            this.Interfaces = new List<string>();

            this.memberDeclarations = new List<ITranslationUnit>();
            this.constructorDeclarations = new List<ITranslationUnit>();
            this.propertyDeclarations = new List<ITranslationUnit>();
            this.methodDeclarations = new List<ITranslationUnit>();
        }
        
        private string Name { get; set; }
        private string BaseClassName { get; set; }
        private IEnumerable<string> Interfaces { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="name"></param>
        /// <param name="baseClassName"></param>
        /// <returns></returns>
        public static ClassDeclarationTranslationUnit Create(VisibilityToken visibility, string name, string baseClassName)
        {
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
        /// <param name="interfaceName"></param>
        public void AddImplementedInterfaceName(string interfaceName)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ITranslationUnit> InnerUnits
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
        public string Translate()
        {
            StringWriter writer = new StringWriter();

            // Opening declaration
            writer.WriteLine("{0} class {1} {2}");

            // Translating members first
            foreach (ITranslationUnit translationUnit in this.memberDeclarations)
            {
                // TODO
            }

            // Then constructors
            foreach (ITranslationUnit translationUnit in this.constructorDeclarations)
            {
                // TODO
            }

            // Then properties
            foreach (ITranslationUnit translationUnit in this.propertyDeclarations)
            {
                // TODO
            }

            // Finally methods
            foreach (ITranslationUnit translationUnit in this.methodDeclarations)
            {
                // TODO
            }

            // Closing
            writer.WriteLine("{0}", Lexems.CloseCurlyBracket);

            return writer.ToString();
        }

        #region Compound translation unit methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translationUnit"></param>
        public void AddMemberDeclaration(ITranslationUnit translationUnit)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translationUnit"></param>
        public void AddPropertyDeclaration(ITranslationUnit translationUnit)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translationUnit"></param>
        public void AddConstructorDeclaration(ITranslationUnit translationUnit)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translationUnit"></param>
        public void AddMethodDeclaration(ITranslationUnit translationUnit)
        {
        }

        #endregion
    }
}
