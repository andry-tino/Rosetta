/// <summary>
/// MethodDeclarationTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Class describing a method signature (no body).
    /// </summary>
    /// <remarks>
    /// Internal members protected for testability.
    /// </remarks>
    public class MethodSignatureDeclarationTranslationUnit : MemberTranslationUnit, ITranslationUnit, ICompoundTranslationUnit
    {
        protected ITranslationUnit returnType;
        
        protected IEnumerable<ITranslationUnit> arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodSignatureDeclarationTranslationUnit"/> class.
        /// </summary>
        protected MethodSignatureDeclarationTranslationUnit() 
            : this(IdentifierTranslationUnit.Empty, ModifierTokens.None)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodSignatureDeclarationTranslationUnit"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="visibility"></param>
        protected MethodSignatureDeclarationTranslationUnit(ITranslationUnit name, ModifierTokens visibility) 
            : base(name, visibility)
        {
            this.ReturnType = null;
            this.arguments = new List<ITranslationUnit>();
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="MethodDeclarationTranslationUnit"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public MethodSignatureDeclarationTranslationUnit(MethodSignatureDeclarationTranslationUnit other) 
            : base((MemberTranslationUnit)other)
        {
            this.returnType = other.returnType;
            this.arguments = other.arguments;
        }

        protected ITranslationUnit ReturnType
        {
            get { return this.returnType ?? TypeIdentifierTranslationUnit.Void; }
            set { this.returnType = value; }
        }

        /// <summary>
        /// Gets a value indicating whether the return type is <code>void</code>.
        /// </summary>
        /// <remarks>
        /// The translation unit will not emit a type if return type was not specified at construction time. 
        /// However, if a translation unit was specified for return type and that translation unit translates 
        /// into `void`, then `void` will be rendered as this is the intended behavior.
        /// </remarks>
        protected bool IsVoid => this.returnType == null;

        protected IEnumerable<ITranslationUnit> Arguments
        {
            get { return this.arguments; }
            set { this.arguments = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modifiers"></param>
        /// <param name="returnType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static MethodSignatureDeclarationTranslationUnit Create(ModifierTokens modifiers, ITranslationUnit returnType, ITranslationUnit name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return new MethodSignatureDeclarationTranslationUnit()
            {
                Modifiers = modifiers,
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
            FormatWriter writer = new FormatWriter()
            {
                Formatter = this.Formatter
            };

            // Opening declaration: [<visibility>] <method-name>(<params>) : <type>
            string modifiers = this.RenderedModifiers;

            if (this.ShouldRenderReturnType)
            {
                writer.WriteLine("{0}{1}{2} {3} {4}",
                    modifiers,
                    this.RenderedName,
                    SyntaxUtility.ToBracketEnclosedList(this.arguments.Select(unit => unit.Translate())),
                    Lexems.Colon,
                    this.ReturnType.Translate());
            }
            else
            {
                writer.WriteLine("{0}{1}{2}",
                    modifiers,
                    this.RenderedName,
                    SyntaxUtility.ToBracketEnclosedList(this.arguments.Select(unit => unit.Translate())));
            }

            return writer.ToString();
        }

        #region Compound translation unit methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="translationUnit"></param>
        public void AddArgument(ITranslationUnit translationUnit)
        {
            if (translationUnit == null)
            {
                throw new ArgumentNullException(nameof(translationUnit));
            }

            ((List<ITranslationUnit>)this.arguments).Add(translationUnit);
        }

        #endregion

        protected virtual string RenderedModifiers => this.Modifiers.ConvertToTypeScriptEquivalent().EmitOptionalVisibility();

        protected virtual bool ShouldRenderReturnType => !this.IsVoid;

        protected virtual string RenderedName => this.Name.Translate();
    }
}
