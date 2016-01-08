/// <summary>
/// ClassGenerator.SimpleClass.cs
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
        public string NamespaceName { get; set; }

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
                    {{
                        class {0}
                        {{
                            static void Main(string[] args)
                            {{
                            }}
                        }}
                    }}",
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

        /// <summary>
        /// A very simle class for an executable.
        /// </summary>
        public string VerySimpleClassInNamespace
        {
            get
            {
                return string.Format(@"
                    using System;
                    using System.Collections;
                    using System.Linq;
                    using System.Text;

                    namespace {0}
                    {{
                        class {1}
                        {{
                            static void Main(string[] args)
                            {{
                                Console.WriteLine(""Hello, World!"");
                            }}
                        }}
                    }}",
                this.NamespaceName,
                this.Name);
            }
        }

        /// <summary>
        /// Attributes for <see cref="VerySimpleClass"/>.
        /// </summary>
        public IReadOnlyDictionary<string, string> VerySimpleClassInNamespaceAttributes
        {
            get
            {
                var dictionary = new Dictionary<string, string>();
                dictionary.Add("NamespaceName", this.Name);
                dictionary.Add("ClassName", this.Name);

                return dictionary;
            }
        }

        /// <summary>
        /// A very simle class with empty methods.
        /// </summary>
        public string VerySimpleClassWithEmptyMethods
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
                        public class {0}
                        {{
                            public static void Method1() {{
                            }}

                            protected static void Method2() {{
                            }}

                            public void Method3() {{
                            }}

                            public int Method4() {{
                            }}

                            public string Method5() {{
                            }}

                            public object Method6(int param1) {{
                            }}

                            public object Method7(int param1, int param2) {{
                            }}
                        }}
                    }}",
                this.Name);
            }
        }

        /// <summary>
        /// Attributes for <see cref="VerySimpleClassWithEmptyMethods"/>.
        /// </summary>
        public IReadOnlyDictionary<string, string> VerySimpleClassWithEmptyMethodsAttributes
        {
            get
            {
                var dictionary = new Dictionary<string, string>();
                dictionary.Add("ClassName", this.Name);
                dictionary.Add("Method1Name", "Method1");
                dictionary.Add("Method2Name", "Method2");
                dictionary.Add("Method3Name", "Method3");
                dictionary.Add("Method4Name", "Method4");
                dictionary.Add("Method5Name", "Method5");
                dictionary.Add("Method6Name", "Method6");
                dictionary.Add("Method7Name", "Method7");
                dictionary.Add("Param1Name", "param1");
                dictionary.Add("Param2Name", "param2");

                return dictionary;
            }
        }

        /// <summary>
        /// A simple class.
        /// </summary>
        public string SimpleClass
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
                        class {0}
                        {{
                            private int field1;
                            public string field2;
                            int field3;

                            public void Method1() {{
                                string variable1;
                            }}

                            private void Method2() {{
                                int variable1;
                                string variable2;
                            }}
                        }}
                    }}",
                this.Name);
            }
        }

        /// <summary>
        /// Attributes for <see cref="SimpleClass"/>.
        /// </summary>
        public IReadOnlyDictionary<string, string> SimpleClassAttributes
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
