/// <summary>
/// ClassDeclarationTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for describing compound translation elements.
    /// </summary>
    public class ClassDeclarationTranslationUnit : ITranslationUnit, ICompoundTranslationUnit
    {
        private VisibilityToken visibility;
        private string name;
        private string baseClassName;
        private IEnumerable<string> interfaces;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassDeclarationTranslationUnit"/> class.
        /// </summary>
        public ClassDeclarationTranslationUnit()
        {
            this.visibility = VisibilityToken.None;
            this.name = string.Empty;
            this.baseClassName = null;
            this.interfaces = new List<string>();
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
            return string.Format(
                "{0} class {1} { {0} }"
                ); // Call other translation units
        }

        /// <summary>
        /// Sets the visibility of the class.
        /// </summary>
        public VisibilityToken Visibility
        {
            set { this.visibility = value; }
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
