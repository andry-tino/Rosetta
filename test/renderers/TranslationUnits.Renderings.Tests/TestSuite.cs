/// <summary>
/// TestSuite.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings.Tests
{
    using System;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rosetta.Renderings;

    [TestClass]
    public class TestSuite
    {
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
        }

        [ClassCleanup]
        public static void CleanUp()
        {
        }

        [TestMethod]
        public void TestMethods()
        {
            var runner = new TestRunner(new MethodsResourceDeployer().Provide());
            runner.Run();

            if (!runner.OverallResult.Value)
            {
                Assert.Fail(runner.ToString());
            }
        }
    }
}
