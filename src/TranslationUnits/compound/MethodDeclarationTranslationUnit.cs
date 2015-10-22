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
    /// Class describing methods.
    /// </summary>
    public class MethodDeclarationTranslationUnit : MemberTranslationUnit, ITranslationUnit, ICompoundTranslationUnit
    {
        private string returnType;

        // Inner units
        private IEnumerable<ITranslationUnit> arguments;
        private IEnumerable<ITranslationUnit> statements;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodDeclarationTranslationUnit"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="returnType"></param>
        /// <param name="visibility"></param>
        protected MethodDeclarationTranslationUnit() : base(string.Empty, VisibilityToken.None)
        {
            this.ReturnType = null;

            this.arguments = new List<ITranslationUnit>();
            this.statements = new List<ITranslationUnit>();
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
            writer.WriteLine("{0} {1} {2} {3} {4}", 
                TokenUtility.ToString(this.Visibility), 
                this.ReturnType, 
                this.Name, 
                SyntaxUtility.ToBracketEnclosedList(this.arguments.Select(unit => unit.Translate())),
                Lexems.OpenCurlyBracket);

            // The body, we render them as a list of semicolon/newline separated elements
            writer.WriteLine("{0}", SyntaxUtility.ToNewlineSemicolonSeparatedList(
                this.statements.Select(unit => unit.Translate())));

            // Closing declaration
            writer.WriteLine("{0}", Lexems.CloseCurlyBracket);

            return writer.ToString();
        }

        #region Compound translation unit methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translationUnit"></param>
        public void AddStatement(ITranslationUnit translationUnit)
        {
        }

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
