/// <summary>
/// TestSuite.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Renderings.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rosetta.Renderings;

    [TestClass]
    public class TestSuite
    {
        [TestMethod]
        public void TestClasses()
        {
            Test(new ClassesResourceDeployer());
        }

        [TestMethod]
        public void TestConstructors()
        {
            Test(new ConstructorsResourceDeployer());
        }

        [TestMethod]
        public void TestInterfaces()
        {
            Test(new InterfacesResourceDeployer());
        }

        [TestMethod]
        public void TestMethods()
        {
            Test(new MethodsResourceDeployer());
        }

        private void Test(IResourceProvider provider)
        {
            var runner = new TestRunner(provider.Provide());
            runner.Run();

            if (!runner.OverallResult.Value)
            {
                Assert.Fail(runner.ToString());
            }
        }
    }
}
