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
    public class MethodDeclarationTranslationUnit : MemberTranslationUnit, ITranslationUnit, ICompoundTranslationUnit
    {
        private string returnType;

        // Inner units
        private IEnumerable<ITranslationUnit> parameters;
        private IEnumerable<ITranslationUnit> bodyElements;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodDeclarationTranslationUnit"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="returnType"></param>
        /// <param name="visibility"></param>
        protected MethodDeclarationTranslationUnit() : base(string.Empty, VisibilityToken.None)
        {
            this.ReturnType = null;

            this.parameters = new List<ITranslationUnit>();
            this.bodyElements = new List<ITranslationUnit>();
        }
        
        private string ReturnType
        {
            get { return this.returnType ?? Lexems.VoidReturnType; }
            set { this.returnType = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="returnType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static MethodDeclarationTranslationUnit Create(VisibilityToken visibility, string returnType, string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return new MethodDeclarationTranslationUnit()
            {
                Visibility = visibility,
                Name = name,
                ReturnType = returnType
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
            writer.WriteLine("{0} {1} {2}", TokenUtility.ToString(this.Visibility), this.ReturnType, this.Name);

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
