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
        /// Sets the visibility of the class.
        /// </summary>
        public VisibilityToken Visibility
        {
            set { this.visibility = value; }
        }

        public IEnumerable<ITranslationUnit> InnerUnits
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Translate()
        {
            return "public class a { {0} }";
        }

        public TranslationHost Host
        {
            get 
            {
                return new TranslationHost();
            }
        }

        public void AddPropertyDeclaration()
        {
        }
    }
}
