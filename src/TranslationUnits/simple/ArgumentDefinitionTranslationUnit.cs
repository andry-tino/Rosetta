﻿/// <summary>
/// ArgumentDefinitionTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Translation unit for rendering arguments in methods, functions.
    /// Acts like a wrapper to <see cref="VariableDeclarationTranslationUnit"/>.
    /// </summary>
    public class ArgumentDefinitionTranslationUnit : ITranslationUnit
    {
        private VariableDeclarationTranslationUnit variableDeclaration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentDefinitionTranslationUnit"/> class.
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="name"></param>
        private ArgumentDefinitionTranslationUnit(ITranslationUnit typeName, ITranslationUnit name)
        {
            this.variableDeclaration = VariableDeclarationTranslationUnit.Create(typeName, name, null, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ArgumentDefinitionTranslationUnit Create(ITranslationUnit typeName, ITranslationUnit name)
        {
            return new ArgumentDefinitionTranslationUnit(typeName, name);
        }

        /// <summary>
        /// The nesting level.
        /// </summary>
        public int NestingLevel
        {
            get;
            set;
        }

        /// <summary>
        /// Translate the unit into TypeScript.
        /// </summary>
        /// <returns></returns>
        public string Translate()
        {
            return string.Format("{0}", 
                this.variableDeclaration.Translate());
        }
    }
}
