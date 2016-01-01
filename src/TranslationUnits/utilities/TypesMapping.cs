/// <summary>
/// TypesMapping.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Options for <see cref="TypesMapping"/>.
    /// </summary>
    public static class TypesMapping
    {
        /// <summary>
        /// If provided input is a basic type, it is converted into the corresponding one in TypeScript.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string MapType(string type)
        {
            if (type == typeof(string).Name)
            {
                return "string";
            }

            if (type == typeof(int).Name)
            {
                return "number";
            }
            if (type == typeof(double).Name)
            {
                return "number";
            }
            if (type == typeof(float).Name)
            {
                return "number";
            }

            if (type == typeof(bool).Name)
            {
                return "boolean";
            }

            if (type == typeof(object).Name)
            {
                return "any";
            }

            // No match, thus we return the input type with no change
            return type;
        }
    }
}
