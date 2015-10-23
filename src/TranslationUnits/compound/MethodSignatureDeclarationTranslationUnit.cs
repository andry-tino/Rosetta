/// <summary>
/// MethodDeclarationTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Linq;
    using System.IO;
    using System.Collections.Generic;

    /// <summary>
    /// Class describing a method signature (no body).
    /// </summary>
    public class MethodSignatureDeclarationTranslationUnit : MemberTranslationUnit, ITranslationUnit, ICompoundTranslationUnit
    {
        private ITranslationUnit returnType;

        // Inner units
        private IEnumerable<ITranslationUnit> arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodDeclarationTranslationUnit"/> class.
        /// </summary>
        protected MethodSignatureDeclarationTranslationUnit() 
            : this(IdentifierTranslationUnit.Empty, VisibilityToken.None)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodDeclarationTranslationUnit"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="visibility"></param>
        protected MethodSignatureDeclarationTranslationUnit(ITranslationUnit name, VisibilityToken visibility) 
            : base(name, visibility)
        {
            this.ReturnType = null;
            this.arguments = new List<ITranslationUnit>();
        }

        protected ITranslationUnit ReturnType
        {
            get { return this.returnType ?? IdentifierTranslationUnit.Void; }
            set { this.returnType = value; }
        }

        protected IEnumerable<ITranslationUnit> Arguments
        {
            get { return this.arguments; }
            set { this.arguments = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="returnType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static MethodSignatureDeclarationTranslationUnit Create(VisibilityToken visibility, ITranslationUnit returnType, ITranslationUnit name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return new MethodSignatureDeclarationTranslationUnit()
            {
                Visibility = visibility,
                Name = name,
                ReturnType = returnType
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual IEnumerable<ITranslationUnit> InnerUnits
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
        public virtual string Translate()
        {
            StringWriter writer = new StringWriter();

            // Opening declaration
            writer.WriteLine("{0} {1} {2} {3}",
                TokenUtility.ToString(this.Visibility),
                this.ReturnType.Translate(),
                this.Name.Translate(),
                SyntaxUtility.ToBracketEnclosedList(this.arguments.Select(unit => unit.Translate())));

            return writer.ToString();
        }

        #region Compound translation unit methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translationUnit"></param>
        public void AddArgument(ITranslationUnit translationUnit)
        {
        }

        #endregion
    }
}
