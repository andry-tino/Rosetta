/// <summary>
/// ClassGenerator.WithMethodStatementContent.cs
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
        /// A simple class.
        /// </summary>
        public string ClassWithIfStatements
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
                            public void Method1() {{
                                if (true) {{
                                    string initVariable1 = ""Hello"";
                                }}
                            }}

                            private void Method2() {{
                                if (true) {{
                                    string initVariable1 = ""Hello"";
                                }} else if (false) {{
                                    string initVariable2 = ""Hello"";
                                }} else {{
                                    string initVariable3 = ""Hello"";
                                }}
                            }}

                            public bool Method3() {{
                                return true;
                            }}

                            public bool Method4() {{
                                return false;
                            }}

                            public void Method5() {{
                                throw null;
                            }}
                        }}
                    }}",
                this.Name);
            }
        }

        /// <summary>
        /// Attributes for <see cref="ClassWithIfStatements"/>.
        /// </summary>
        public IReadOnlyDictionary<string, string> ClassWithIfStatementsAttributes
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
