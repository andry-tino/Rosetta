/// <summary>
/// SimpleClass.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Tests
{
    using System;

    /// <summary>
    /// Simple class.
    /// </summary>
    internal partial class ClassGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        public string BaseClassName { get; set; }

        /// <summary>
        /// A very simle class for an executable.
        /// </summary>
        public string ClassWithBaseClass
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
                        class {0} : {1}
                        {
                            static void Main(string[] args)
                            {
                                Console.WriteLine(""Hello, World!"");
                            }
                        }
                    }",
                this.Name, this.BaseClassName);
            }
        }
    }
}
