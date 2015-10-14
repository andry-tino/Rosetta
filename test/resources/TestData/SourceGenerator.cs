/// <summary>
/// SourceGenerator.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Tests.Data
{
    using System;
    using System.Collections.Generic;

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
        /// The interface #1 name.
        /// </summary>
        public static string Interface1Name { get; set; }

        /// <summary>
        /// Defines default values.
        /// </summary>
        static SourceGenerator()
        {
            Name = "MyClass";
            BaseName = "MyBaseClass";
            Interface1Name = "MyInterface1";
        }

        /// <summary>
        /// Generates the appropriate class given some options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>
        /// A <see cref="KeyValuePair"/> where the first element is the source 
        /// string and the second element is the set of attributes.
        /// </returns>
        /// 
        /// TODO: Abstract with a wrapper type wrapping around KeyValuePair.
        public static KeyValuePair<string, IReadOnlyDictionary<string, string>> 
            Generate(SourceOptions options = SourceOptions.None)
        {
            ClassGenerator classes = new ClassGenerator()
            {
                Name = Name
            };

            // Class with interface
            if (options.HasFlag(SourceOptions.ImplementsInterfaces))
            {
                classes.Interface1Name = Interface1Name;
                return new KeyValuePair<string, IReadOnlyDictionary<string, string>>(
                    classes.ClassWith1Interface, classes.ClassWith1InterfaceAttributes);
            }

            // Class with base class
            if (options.HasFlag(SourceOptions.HasInheritance))
            {
                classes.BaseClassName = BaseName;
                return new KeyValuePair<string, IReadOnlyDictionary<string, string>>(
                    classes.ClassWithBaseClass, classes.ClassWithBaseClassAttributes);
            }

            // Simple class
            return new KeyValuePair<string, IReadOnlyDictionary<string, string>>(
                classes.VerySimpleClass, classes.VerySimpleClassAttributes);
        }
    }
}
