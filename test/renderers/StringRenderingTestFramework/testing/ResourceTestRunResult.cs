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
        /// Gets or sets the name associated to this run.
        /// </summary>
        public string TestRunName { get; set; }

        /// <summary>
        /// The <see cref="TestResource"/> the test was conducted on.
        /// </summary>
        public TestResource Resource { get; set; }

        /// <summary>
        /// The <see cref="CompareResult"/>.
        /// </summary>
        public CompareResult Result { get; set; }

        /// <summary>
        /// In case the test occurred into an exception, the issue is reported here.
        /// </summary>
        public Exception Exception { get; set; }
    }
}
