/// <summary>
/// TestSuite.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings.Tests
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
        public void TestExpressions()
        {
            Test(new ExpressionsResourceDeployer());
        }

        [TestMethod]
        public void TestInterfaces()
        {
            Test(new InterfacesResourceDeployer());
        }

        [TestMethod]
        public void TestMembers()
        {
            Test(new MembersResourceDeployer());
        }

        [TestMethod]
        public void TestMethods()
        {
            Test(new MethodsResourceDeployer());
        }

        [TestMethod]
        public void TestMethodSignatures()
        {
            Test(new MethodSignaturesResourceDeployer());
        }

        [TestMethod]
        public void TestMixedExpressions()
        {
            Test(new MixedExpressionsResourceDeployer());
        }

        [TestMethod]
        public void TestModules()
        {
            Test(new ModulesResourceDeployer());
        }

        [TestMethod]
        public void TestProperties()
        {
            Test(new PropertiesResourceDeployer());
        }

        [TestMethod]
        public void TestReferences()
        {
            Test(new ReferencesResourceDeployer());
        }

        [TestMethod]
        public void TestStatements()
        {
            Test(new StatementsResourceDeployer());
        }

        [TestMethod]
        public void TestStatementsGroups()
        {
            Test(new StatementsGroupsResourceDeployer());
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
