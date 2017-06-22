/// <summary>
/// ResourceTestRunResult.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Renderings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Executes a string rendering test.
    /// </summary>
    public class TestRunner
    {
        private readonly IEnumerable<TestResource> testResources;
        private readonly IStringComparer comparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestRunner"/> class.
        /// </summary>
        /// <param name="testResources"></param>
        /// <param name="comparer"></param>
        public TestRunner(IEnumerable<TestResource> testResources, IStringComparer comparer = null)
        {
            if (testResources == null)
            {
                throw new ArgumentNullException(nameof(testResources));
            }
            if (testResources.Count() == 0)
            {
                throw new ArgumentException("We need at least one resource to run the test", nameof(testResources));
            }

            this.testResources = testResources;
            this.comparer = comparer == null ? new NormalStringComparer() : comparer;
        }

        /// <summary>
        /// Gets the report after <see cref="Run"/> has been called.
        /// </summary>
        public string Report { get; private set; }

        /// <summary>
        /// Gets the summary after <see cref="Run"/> has been called.
        /// </summary>
        public string Summary { get; private set; }

        /// <summary>
        /// Gets the overall test result after <see cref="Run"/> has been called.
        /// </summary>
        public bool? OverallResult { get; private set; }

        /// <summary>
        /// Runs the test and generates the report.
        /// </summary>
        public void Run()
        {
            var results = new List<ResourceTestRunResult>();
            this.OverallResult = true;

            foreach (var testResource in this.testResources)
            {
                var actual = testResource.ActualValue; // Causes reflection to be invoked
                var archetype = testResource.ArchetypeValue; // Causes assembly resource to be extracted

                var result = this.comparer.Compare(actual, archetype);
                this.OverallResult = this.OverallResult.Value && result.Result;

                results.Add(new ResourceTestRunResult()
                {
                    Resource = testResource,
                    Result = result
                });
            }

            // Building report and summary
            var report = new StringBuilder();
            var summary = new StringBuilder();

            report.AppendLine("Showing test results:");
            report.AppendLine("Showing summary:");

            foreach (var result in results)
            {
                // Report
                report.AppendLine($"{result.Resource.EmbeddedResourceName} => {result.Result.Result.ToTestPassResult()}");

                // Summary
                if (!result.Result.Result)
                {
                    summary.AppendLine($"{result.Resource.EmbeddedResourceName}");
                    summary.AppendLine(Utilities.PrintSeparator(result.Resource.EmbeddedResourceName.Length));
                    summary.AppendLine(result.Result.Description);
                }
                else
                {
                    summary.AppendLine($"{result.Resource.EmbeddedResourceName} is OK");
                }
                summary.AppendLine();
            }

            this.Report = report.ToString();
            this.Summary = summary.ToString();
        }

        /// <summary>
        /// Use this method to display all the information regarding the test run.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (!this.OverallResult.HasValue)
            {
                // Test has not been run yet
                return "Test not run";
            }

            var message = new StringBuilder();

            message.AppendLine($"Displaying result for String-Rendering test");
            message.AppendLine($"===========================================");
            message.AppendLine();

            message.AppendLine($"Report");
            message.AppendLine($"------");
            message.AppendLine(this.Report);

            if (!this.OverallResult.Value)
            {
                message.AppendLine();

                message.AppendLine($"Summary");
                message.AppendLine($"-------");
                message.AppendLine(this.Summary);
            }

            return message.ToString();
        }
    }
}
