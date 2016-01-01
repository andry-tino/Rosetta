/// <summary>
/// TypeIdentifierTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Translation unit for identifiers.
    /// </summary>
    public class TypeIdentifierTranslationUnit : ITranslationUnit
    {
        private string typeName;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeIdentifierTranslationUnit"/> class.
        /// </summary>
        /// <param name="typeName"></param>
        private TypeIdentifierTranslationUnit(string typeName)
        {
            this.typeName = ValidateName(typeName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TypeIdentifierTranslationUnit Create(string typeName)
        {
            return new TypeIdentifierTranslationUnit(typeName);
        }

        /// <summary>
        /// 
        /// </summary>
        public static TypeIdentifierTranslationUnit Void
        {
            get { return Create(Lexems.VoidReturnType); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static TypeIdentifierTranslationUnit Number
        {
            get { return Create("number"); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static TypeIdentifierTranslationUnit String
        {
            get { return Create("string"); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static TypeIdentifierTranslationUnit Boolean
        {
            get { return Create("boolean"); }
        }

        /// <summary>
        /// Translate the unit into TypeScript.
        /// </summary>
        /// <returns></returns>
        public string Translate()
        {
            return TypesMapping.MapType(this.typeName);
        }

        #region Validation

        private static string ValidateName(string name)
        {
            if (name.Contains(" "))
            {
                throw new ArgumentException("Type names cannot contain spaces", nameof(name));
            }

            return name;
        }

        #endregion
    }
}
