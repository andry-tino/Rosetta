/// <summary>
/// SourceGenerator.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Tests.Data
{
    using System;

    /// <summary>
    /// Acts like a factory.
    /// </summary>
    public static class SourceGenerator
    {
        /// <summary>
        /// The class name.
        /// </summary>
        public static string Name { get; set; }

        /// <summary>
        /// The base class name.
        /// </summary>
        public static string BaseName { get; set; }

        /// <summary>
        /// Defines default values.
        /// </summary>
        static SourceGenerator()
        {
            Name = "MyClass";
            BaseName = "MyBaseClass";
        }

        /// <summary>
        /// Generates the appropriate class given some options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public static string Generate(SourceOptions options = SourceOptions.None)
        {
            ClassGenerator classes = new ClassGenerator()
            {
                Name = Name
            };

            if (options.HasFlag(SourceOptions.HasInheritance))
            {
                return classes.ClassWithBaseClass;
            }

            return classes.VerySimpleClass;
        }
    }
}
