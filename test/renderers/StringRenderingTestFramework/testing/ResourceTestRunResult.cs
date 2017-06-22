/// <summary>
/// ResourceTestRunResult.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Renderings
{
    using System;

    /// <summary>
    /// When running a test on a resource, associates the resource to the result of the test.
    /// </summary>
    public class ResourceTestRunResult
    {
        /// <summary>
        /// The <see cref="TestResource"/> the test was conducted on.
        /// </summary>
        public TestResource Resource { get; set; }

        /// <summary>
        /// The <see cref="CompareResult"/>.
        /// </summary>
        public CompareResult Result { get; set; }
    }
}
