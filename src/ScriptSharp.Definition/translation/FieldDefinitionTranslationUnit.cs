﻿/// <summary>
/// FieldDefinitionTranslationUnit.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.Translation
{
    using System;

    using Rosetta.Translation;

    /// <summary>
    /// Class describing a method signature (no body).
    /// 
    /// TODO: Move to a separate project, this is specific to ScriptSharp.
    /// </summary>
    public class FieldDefinitionTranslationUnit : FieldDeclarationTranslationUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldDefinitionTranslationUnit"/> class.
        /// </summary>
        protected FieldDefinitionTranslationUnit() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldDefinitionTranslationUnit"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="visibility"></param>
        protected FieldDefinitionTranslationUnit(ITranslationUnit name, ModifierTokens visibility) : base(name, visibility)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modifiers"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static new FieldDefinitionTranslationUnit Create(ModifierTokens modifiers, ITranslationUnit type, ITranslationUnit name)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return new FieldDefinitionTranslationUnit()
            {
                Modifiers = modifiers,
                Name = name,
                Type = type
            };
        }

        protected override string RenderedVisibilityModifier
        {
            get
            {
                if (this.Modifiers.HasFlag(ModifierTokens.Protected))
                {
                    // If protected, emit the visibility modifier
                    return this.Modifiers.ConvertToTypeScriptEquivalent().EmitOptionalVisibility();
                }

                return string.Empty;
            }
        }
    }
}
