/// <summary>
/// ArgumentDefinitionTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Translation unit for rendering arguments in methods, functions.
    /// </summary>
    public class ArgumentDefinitionTranslationUnit : ITranslationUnit
    {
        private string typeName;
        private string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentDefinitionTranslationUnit"/> class.
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="name"></param>
        private ArgumentDefinitionTranslationUnit(string typeName, string name)
        {
            this.typeName = typeName;
            this.name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ArgumentDefinitionTranslationUnit Create(string typeName, string name)
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
            return string.Format("{0} {1}", this.typeName, this.name);
        }
    }
}
