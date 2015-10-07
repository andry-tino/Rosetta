/// <summary>
/// SimpleClass.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Tests
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Simple class.
    /// </summary>
    internal partial class ClassGenerator
    {
        /// <summary>
        /// A very simle class for an executable.
        /// </summary>
        public string VerySimpleClass
        {
            get
            {
                return string.Format(@"
                    using System;
                    using System.Collections;
                    using System.Linq;
                    using System.Text;

                    namespace HelloWorld
                    {
                        class {0}
                        {
                            static void Main(string[] args)
                            {
                                Console.WriteLine(""Hello, World!"");
                            }
                        }
                    }",
                this.Name);
            }
        }

        /// <summary>
        /// Attributes for <see cref="VerySimpleClass"/>.
        /// </summary>
        public IReadOnlyDictionary<string, string> VerySimpleClassAttributes
        {
            get
            {
                var dictionary = new Dictionary<string, string>();
                dictionary.Add("ClassName", this.Name);

                return dictionary;
            }
        }
    }
}
