/// <summary>
/// ClassGenerator.WithMethodExpressionContent.cs
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
        public string ClassWithMethodArithmeticExpressions
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
                            public {0}() {{
                                string initVariable1 = ""Hello"";
                            }}

                            public void Method1() {{
                                string initVariable1 = ""Hello"";
                            }}

                            void Method2() {{
                                int initVariable1 = 1 + 4;
                                int initVariable2 = 2 * 4 + (3 / 2);
                            }}

                            private void Method3() {{
                                bool initVariable1 = !false;
                                bool initVariable2 = !true;
                            }}

                            private void Method4() {{
                                int initVariable1 = 1++;
                                int initVariable2 = ++1;
                                int initVariable1 = 1--;
                                int initVariable2 = --1;
                            }}

                            private void Method5() {{
                                int initVariable1 = (1);
                            }}

                            void Method6() {{
                                bool initVariable1 = true == false;
                                bool initVariable2 = true != false;
                                bool initVariable3 = 1 == 2;
                                bool initVariable4 = 1 != 2;
                                bool initVariable5 = ""hello"" == ""Hello"";
                                bool initVariable6 = ""hello"" != ""Hello"";
                            }}

                            private void Method7() {{
                                bool initVariable1 = true;
                                initVariable1 = false;
                                int initVariable2 = 1;
                                initVariable2 = 0;
                                string initVariable3 = ""hello"";
                                initVariable3 = ""hello!"";
                            }}
                        }}
                    }}",
                this.Name);
            }
        }

        /// <summary>
        /// Attributes for <see cref="ClassWithMethodArithmeticExpressions"/>.
        /// </summary>
        public IReadOnlyDictionary<string, string> ClassWithMethodArithmeticExpressionsAttributes
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
