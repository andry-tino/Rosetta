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
    /// Class describing a variable declaration.
    /// </summary>
    /// <remarks>
    /// Internal members protected for testability.
    /// </remarks>
    public class VariableDeclarationTranslationUnit : NestedElementTranslationUnit, ITranslationUnit
    {
        protected ITranslationUnit type; // Can be null
        protected ITranslationUnit[] names;
        protected ITranslationUnit[] expressions; // Can be null

        // This unit is also responsible for declaring variables as parameters/arguments
        protected bool shouldRenderDeclarationKeyword;

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
            this.names = null;
            this.expressions = null;

            this.shouldRenderDeclarationKeyword = true;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="VariableDeclarationTranslationUnit"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public VariableDeclarationTranslationUnit(VariableDeclarationTranslationUnit other) 
            : base(other)
        {
            this.type = other.type;
            this.names = other.names;
            this.expressions = other.expressions;

            this.shouldRenderDeclarationKeyword = other.shouldRenderDeclarationKeyword;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public static VariableDeclarationTranslationUnit Create(
            ITranslationUnit type, ITranslationUnit[] names, ITranslationUnit[] expressions = null, 
            bool shouldRenderDeclarationKeyword = true)
        {
            if (names == null)
            {
                throw new ArgumentNullException(nameof(names));
            }
            if (names.Length == 0)
            {
                throw new ArgumentException(nameof(names), "At least one name needed!");
            }
            if (expressions != null && expressions.Length != names.Length)
            {
                throw new ArgumentException(nameof(expressions), "Number of expressions should match number of names!");
            }

            return new VariableDeclarationTranslationUnit()
            {
                names = names,
                type = type,
                expressions = expressions,
                shouldRenderDeclarationKeyword = shouldRenderDeclarationKeyword
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static VariableDeclarationTranslationUnit Create(
            ITranslationUnit type, ITranslationUnit name, ITranslationUnit expression = null, 
            bool shouldRenderDeclarationKeyword = true)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(names));
            }

            return new VariableDeclarationTranslationUnit()
            {
                names = new ITranslationUnit[] { name },
                type = type,
                expressions = expression == null ? null : new ITranslationUnit[] { expression },
                shouldRenderDeclarationKeyword = shouldRenderDeclarationKeyword
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
            
            // TODO: Improve this logic to make it more readable and efficient
            if (this.type != null)
            {
                if (this.Expression == null)
                {
                    // [var ]<name> : <type>
                    writer.Write("{0}{1} {2} {3}",
                        text => ClassDeclarationCodePerfect.RefineDeclaration(text),
                        this.shouldRenderDeclarationKeyword ? Lexems.VariableDeclaratorKeyword + " " : string.Empty,
                        this.names[0].Translate(),
                        Lexems.Colon,
                        this.type.Translate());
                }
                else
                {
                    // [var ]<name> : <type> = <expression>
                    writer.Write("{0}{1} {2} {3} {4} {5}",
                        text => ClassDeclarationCodePerfect.RefineDeclaration(text),
                        this.shouldRenderDeclarationKeyword ? Lexems.VariableDeclaratorKeyword + " " : string.Empty,
                        this.names[0].Translate(),
                        Lexems.Colon,
                        this.type.Translate(),
                        Lexems.EqualsSign,
                        this.Expression.Translate());
                }
            }
            else
            {
                if (this.Expression == null)
                {
                    // var <name>
                    writer.Write("{0} {1}",
                        text => ClassDeclarationCodePerfect.RefineDeclaration(text),
                        Lexems.VariableDeclaratorKeyword,
                        this.names[0].Translate());
                }
                else
                {
                    // var <name> = <expression>
                    writer.Write("{0} {1} {2} {3}",
                        text => ClassDeclarationCodePerfect.RefineDeclaration(text),
                        Lexems.VariableDeclaratorKeyword,
                        this.names[0].Translate(),
                        Lexems.EqualsSign,
                        this.Expression.Translate());
                }
            }

            return writer.ToString();
        }

        /// <summary>
        /// Until we support multiple declarations, we need to get the expression if it exists.
        /// </summary>
        private ITranslationUnit Expression
        {
            get { return this.expressions == null ? null : this.expressions[0]; }
        }

        // TODO: When supporting multiple declarations, add logic here
        private string TranslateSingleDeclaration()
        {
            return this.expressions.ToString();
        }
    }
}
