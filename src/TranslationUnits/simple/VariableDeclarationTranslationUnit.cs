/// <summary>
/// VariableDeclarationTranslationUnit.cs
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
    public class VariableDeclarationTranslationUnit : NestedElementTranslationUnit, ITranslationUnit
    {
        private ITranslationUnit type;
        private ITranslationUnit name;

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableDeclarationTranslationUnit"/> class.
        /// </summary>
        protected VariableDeclarationTranslationUnit() : this(AutomaticNestingLevel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableDeclarationTranslationUnit"/> class.
        /// </summary>
        /// <param name="nestingLevel"></param>
        protected VariableDeclarationTranslationUnit(int nestingLevel)
            : base(nestingLevel)
        {
            this.type = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static VariableDeclarationTranslationUnit Create(ITranslationUnit type, ITranslationUnit name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return new VariableDeclarationTranslationUnit()
            {
                name = name,
                type = type
            };
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

            // Opening declaration
            if (this.type != null)
            {
                writer.Write("{0} {1} {2} {3}",
                text => ClassDeclarationCodePerfect.RefineDeclaration(text),
                Lexems.VariableDeclaratorKeyword,
                this.name.Translate(),
                Lexems.Colon,
                this.type.Translate());
            }
            else
            {
                writer.Write("{0} {1}",
                text => ClassDeclarationCodePerfect.RefineDeclaration(text),
                Lexems.VariableDeclaratorKeyword,
                this.name.Translate());
            }

            return writer.ToString();
        }
    }
}
