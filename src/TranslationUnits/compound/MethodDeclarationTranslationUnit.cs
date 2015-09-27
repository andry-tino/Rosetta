/// <summary>
/// MethodDeclarationTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    /// <summary>
    /// Class describing methods.
    /// </summary>
    public class MethodDeclarationTranslationUnit : ScopedElementTranslationUnit, ITranslationUnit, ICompoundTranslationUnit
    {
        // Inner units
        private IEnumerable<ITranslationUnit> parameters;
        private IEnumerable<ITranslationUnit> bodyElements;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodDeclarationTranslationUnit"/> class.
        /// </summary>
        protected MethodDeclarationTranslationUnit() : base()
        {
            this.Visibility = VisibilityToken.None;
            this.Name = string.Empty;

            this.parameters = new List<ITranslationUnit>();
            this.bodyElements = new List<ITranslationUnit>();
        }
        
        private string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="name"></param>
        /// <param name="baseClassName"></param>
        /// <returns></returns>
        public static MethodDeclarationTranslationUnit Create(VisibilityToken visibility, string name)
        {
            return new MethodDeclarationTranslationUnit()
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

        #endregion
    }
}
