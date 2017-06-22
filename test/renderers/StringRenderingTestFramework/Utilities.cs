/// <summary>
/// Utils.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Renderings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using Rosetta.Renderings.Utils;

    /// <summary>
    /// Multi-purpose test utils.
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Converts a boolean test result into a string.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string ToTestPassResult(this ResourceTestRunResult result) => result.Exception != null ? "Exception" : (result.Result.Result ? "Pass" : "Fail");

        /// <summary>
        /// Prints a separator line.
        /// </summary>
        /// <param name="length"></param>
        /// <param name="char"></param>
        /// <returns></returns>
        public static string PrintSeparator(int length, string @char = "-")
        {
            var sb = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                sb.Append(@char);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Retrieves all public methods in a class container that have the <see cref="RenderingResourceAttribute"/> applied.
        /// </summary>
        /// <param name="containingClass"></param>
        /// <returns></returns>
        public static IEnumerable<MethodInfo> RetrieveAllTestMethodsInClassContainer(Type containingClass)
        {
            var methods = containingClass.GetMethods();
            var testMethods = methods.Where(method => RetrieveRenderingResourceAttribute(method.CustomAttributes) != null);

            return testMethods;
        }

        /// <summary>
        /// Retrieves the <see cref="RenderingResourceAttribute"/> attribute.
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static CustomAttributeData RetrieveRenderingResourceAttribute(IEnumerable<CustomAttributeData> attributes)
        {
            if (attributes.Count() == 0)
            {
                return null;
            }

            foreach (var attribute in attributes)
            {
                if (attribute.AttributeType == typeof(RenderingResourceAttribute))
                {
                    return attribute;
                }
            }

            return null; // Could not be found
        }

        /// <summary>
        /// Retrieves the <see cref="RenderingResourceAttribute"/> attribute parameter value.
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static string RetrieveRenderingResourceAttributeValue(IEnumerable<CustomAttributeData> attributes)
        {
            var attribute = RetrieveRenderingResourceAttribute(attributes);

            if (attribute == null)
            {
                return null;
            }

            // Get the value of the parameter `fileName`
            // First search among ctor arguments
            foreach (var arg in attribute.ConstructorArguments)
            {
                var value = arg.Value;

                if (value != null)
                {
                    return value as string;
                }
            }

            // First search among named arguments
            foreach (var arg in attribute.NamedArguments)
            {
                var name = arg.MemberName;

                if (name == RenderingResourceAttributePropertyName)
                {
                    var value = arg.TypedValue.Value;

                    if (value != null)
                    {
                        return value as string;
                    }
                }
            }

            return null; // Problems getting the value
        }

        private static string RenderingResourceAttributePropertyName => typeof(RenderingResourceAttribute).GetProperties().First().Name;
    }
}
