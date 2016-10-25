/// <summary>
/// ModuleDefinitionTranslationUnit.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Class describing definition for modules.
    /// </summary>
    /// <remarks>
    /// Internal members protected for testability.
    /// </remarks>
    public class ModuleDefinitionTranslationUnit : ModuleTranslationUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleDefinitionTranslationUnit"/> class.
        /// </summary>
        protected ModuleDefinitionTranslationUnit()
            : this(AutomaticNestingLevel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleDefinitionTranslationUnit"/> class.
        /// </summary>
        /// <param name="nestingLevel"></param>
        protected ModuleDefinitionTranslationUnit(int nestingLevel)
            : this(IdentifierTranslationUnit.Empty, nestingLevel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleDefinitionTranslationUnit"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="nestingLevel"></param>
        protected ModuleDefinitionTranslationUnit(ITranslationUnit name, int nestingLevel)
            : base(name, nestingLevel)
        {
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="ModuleDefinitionTranslationUnit"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public ModuleDefinitionTranslationUnit(ModuleDefinitionTranslationUnit other)
            : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ModuleDefinitionTranslationUnit Create(ITranslationUnit name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return new ModuleDefinitionTranslationUnit(name, AutomaticNestingLevel);
        }

        protected override string RenderedModuleAccessorKeyword
        {
            get
            {
                return this.IsAtRootLevel 
                    ? Lexems.DeclareKeyword 
                    : Lexems.ExportKeyword;
            }
        }
    }
}
