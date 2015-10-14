/// <summary>
/// ClassGenerator.WithInterfaces.cs
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
        /// 
        /// </summary>
        public string Interface1Name { get; set; }

        /// <summary>
        /// A very simle class with one interface for an executable.
        /// </summary>
        public string ClassWith1Interface
        {
            get
            {
                return string.Format(@"
                    using System;
                    using System.Collections;
                    using System.Linq;
                    using System.Text;

                    namespace HelloWorld
                    {{
                        interface {1}
                        {{
                        }}

                        class {0} : {1}
                        {{
                            static void Main(string[] args)
                            {{
                                Console.WriteLine(""Hello, World!"");
                            }}
                        }}
                    }}",
                this.Name, this.Interface1Name);
            }
        }

        /// <summary>
        /// Attributes for <see cref="ClassWith1Interface"/>.
        /// </summary>
        public IReadOnlyDictionary<string, string> ClassWith1InterfaceAttributes
        {
            get
            {
                var dictionary = new Dictionary<string, string>();
                dictionary.Add("ClassName", this.Name);
                dictionary.Add("Interface1Name", this.Interface1Name);

                return dictionary;
            }
        }
    }
}
