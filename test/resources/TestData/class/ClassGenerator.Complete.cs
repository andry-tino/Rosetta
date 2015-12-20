/// <summary>
/// ClassGenerator.Complete.cs
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
        public string ClassComplete
        {
            get
            {
                return string.Format(@"
                    using System;
                    using System.Collections;
                    using System.Linq;
                    using System.Text;

                    namespace Namespace1
                    {{
                        public interface {0} {{
                        }}

                        public class {0} {{
                        }}

                        public class {0} : {1}, {1}
                        {{
                            private int field1;
                            private string field2;

                            public void Method1() {{
                                string initVariable1 = ""Hello"";
                            }}

                            private void Method2() {{
                                int initVariable1 = 1 + 4;
                                int initVariable2 = 2 * 4 + (3 / 2);
                            }}
                        }}
                    }}",
                this.Name);
            }
        }

        /// <summary>
        /// Attributes for <see cref="ClassComplete"/>.
        /// </summary>
        public IReadOnlyDictionary<string, string> ClassCompleteAttributes
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
