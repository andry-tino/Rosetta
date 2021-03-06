﻿/// <summary>
/// ClassDefinitionTranslationUnit.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.Translation
{
    using System;

    using Rosetta.Translation;

    /// <summary>
    /// Interface for describing compound translation elements.
    /// 
    /// TODO: Move to a separate project, this is specific to ScriptSharp.
    /// </summary>
    /// <remarks>
    /// Internal members protected for testability.
    /// </remarks>
    public class ClassDefinitionTranslationUnit : ClassDeclarationTranslationUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClassDefinitionTranslationUnit"/> class.
        /// </summary>
        protected ClassDefinitionTranslationUnit() : base()
        {
            this.IsAtRootLevel = false;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="ClassDefinitionTranslationUnit"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public ClassDefinitionTranslationUnit(ClassDefinitionTranslationUnit other)
            : base(other)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="name"></param>
        /// <param name="baseClassName"></param>
        /// <returns></returns>
        public static new ClassDefinitionTranslationUnit Create(ModifierTokens visibility, ITranslationUnit name, ITranslationUnit baseClassName)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name), "Class name cannot be null!");
            }

            return new ClassDefinitionTranslationUnit()
            {
                Modifiers = visibility,
                Name = name,
                BaseClassName = baseClassName
            };
        }

        protected override string RenderedMethodDeclarationAfterSeparator
        {
            get { return Lexems.Semicolon; }
        }

        protected override string RenderedPropertyDeclarationAfterSeparator
        {
            get { return Lexems.Semicolon; }
        }

        protected override string RenderedConstructorDeclarationAfterSeparator
        {
            get { return Lexems.Semicolon; }
        }

        protected override string RenderedVisibilityModifier
        {
            get
            {
                return this.IsAtRootLevel
                    ? $"{Lexems.DeclareKeyword}{Lexems.Whitespace}" // In this case, the containing structure will add the exposing keyword
                    : string.Empty;
            }
        }
    }
}
