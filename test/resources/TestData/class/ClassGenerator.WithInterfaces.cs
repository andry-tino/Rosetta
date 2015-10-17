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
        /// Gets or sets tame of 1st interface.
        /// </summary>
        public string Interface1Name { get; set; }

        /// <summary>
        /// Gets or sets tame of 2nd interface.
        /// </summary>
        public string Interface2Name { get; set; }

        /// <summary>
        /// Gets or sets tame of 3rd interface.
        /// </summary>
        public string Interface3Name { get; set; }

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

        /// <summary>
        /// A very simle class with 3 interfaces for an executable.
        /// </summary>
        public string ClassWithManyInterfaces
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

                        interface {2}
                        {{
                        }}

                        interface {3}
                        {{
                        }}

                        class {0} : {1}, {2}, {3}
                        {{
                            static void Main(string[] args)
                            {{
                                Console.WriteLine(""Hello, World!"");
                            }}
                        }}
                    }}",
                this.Name, this.Interface1Name, this.Interface2Name, this.Interface3Name);
            }
        }

        /// <summary>
        /// Attributes for <see cref="ClassWithManyInterfaces"/>.
        /// </summary>
        public IReadOnlyDictionary<string, string> ClassWithManyInterfacesAttributes
        {
            get
            {
                var dictionary = new Dictionary<string, string>();
                dictionary.Add("ClassName", this.Name);
                dictionary.Add("Interface1Name", this.Interface1Name);
                dictionary.Add("Interface2Name", this.Interface2Name);
                dictionary.Add("Interface3Name", this.Interface3Name);

                return dictionary;
            }
        }
    }
}
