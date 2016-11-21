/// <summary>
/// InterfaceDeclarationTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Translation unit for describing interfaces.
    /// </summary>
    /// <remarks>
    /// Internal members protected for testability.
    /// </remarks>
    public class InterfaceDeclarationTranslationUnit : ScopedElementTranslationUnit,
        ITranslationUnit, ICompoundTranslationUnit, ITranslationInjector
    {
        // Inner units
        protected IEnumerable<ITranslationUnit> signatures;

        // Injected units
        protected ITranslationUnit injectedBefore;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceDeclarationTranslationUnit"/> class.
        /// </summary>
        protected InterfaceDeclarationTranslationUnit() : base()
        {
            this.Name = IdentifierTranslationUnit.Empty;
            this.Interfaces = new List<ITranslationUnit>();

            this.signatures = new List<ITranslationUnit>();

            this.injectedBefore = null;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="InterfaceDeclarationTranslationUnit"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public InterfaceDeclarationTranslationUnit(InterfaceDeclarationTranslationUnit other)
            : base(other)
        {
            this.Name = other.Name;
            this.Interfaces = other.Interfaces;

            this.signatures = other.signatures;

            this.injectedBefore = other.injectedBefore;
        }

        protected ITranslationUnit Name { get; set; }
        protected IEnumerable<ITranslationUnit> Interfaces { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the interface is part, as a member, of a namespace or module or not.
        /// </summary>
        public bool IsAtRootLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static InterfaceDeclarationTranslationUnit Create(VisibilityToken visibility, ITranslationUnit name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name), "Interface name cannot be null!");
            }

            return new InterfaceDeclarationTranslationUnit()
            {
                Visibility = visibility,
                Name = name
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="extendedInterface"></param>
        public void AddExtendedInterface(ITranslationUnit extendedInterface)
        {
            if (extendedInterface == null)
            {
                throw new ArgumentNullException(nameof(extendedInterface));
            }

            ((List<ITranslationUnit>)this.Interfaces).Add(extendedInterface);
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ITranslationUnit> InnerUnits
        {
            get
            {
                return this.signatures;
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
            string interfaceVisibility = this.RenderedVisibilityModifier;
            string extensionList = this.BuildInterfaceExtensionList();

            if (this.injectedBefore == null)
            {
                writer.WriteLine("{0}{1} {2} {3} {4}",
                text => ClassDeclarationCodePerfect.RefineDeclaration(text),
                interfaceVisibility,
                Lexems.InterfaceKeyword,
                this.Name.Translate(),
                extensionList,
                Lexems.OpenCurlyBracket);
            }
            else
            {
                writer.WriteLine("{0} {1}{2} {3} {4} {5}",
                text => ClassDeclarationCodePerfect.RefineDeclaration(text),
                this.injectedBefore.Translate(),
                interfaceVisibility,
                Lexems.InterfaceKeyword,
                this.Name.Translate(),
                extensionList,
                Lexems.OpenCurlyBracket);
            }

            // Signatures
            if (this.ShouldRenderSignatures)
            {
                foreach (ITranslationUnit translationUnit in this.signatures)
                {
                    writer.WriteLine("{0}{1}", translationUnit.Translate(), Lexems.Semicolon);
                }
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
        public void AddSignature(ITranslationUnit translationUnit)
        {
            if (translationUnit == null)
            {
                throw new ArgumentNullException(nameof(translationUnit));
            }

            if (translationUnit as NestedElementTranslationUnit != null)
            {
                ((NestedElementTranslationUnit)translationUnit).NestingLevel = this.NestingLevel + 1;
            }

            ((List<ITranslationUnit>)this.signatures).Add(translationUnit);
        }

        #endregion
        
        protected virtual bool ShouldRenderSignatures
        {
            get { return true; }
        }

        protected virtual string RenderedVisibilityModifier
        {
            get { return this.Visibility.ConvertToTypeScriptEquivalent().EmitOptionalVisibility(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string BuildInterfaceExtensionList()
        {
            string implementationList = this.Interfaces.Count() > 0 ?
                string.Format("{0} {1}", Lexems.ExtendsKeyword, SyntaxUtility.ToTokenSeparatedList(
                    this.Interfaces.Select(unit => unit.Translate()), Lexems.Comma + " ")) :
                string.Empty;

            return implementationList;
        }
    }
}
