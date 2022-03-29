/// <summary>
/// TestSuite.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST.Renderings.Tests
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
        [Ignore] // Bug #49
        public void TestClassesWithScriptNamespace()
        {
            Test(new ClassesWithScriptNamespaceResourceDeployer());
        }

        [TestMethod]
        public void TestEnums()
        {
            Test(new EnumsResourceDeployer());
        }

        [TestMethod]
        [Ignore] // Bug #49
        public void TestEnumsWithScriptNamespace()
        {
            Test(new EnumsWithScriptNamespaceResourceDeployer());
        }

        [TestMethod]
        public void TestFields()
        {
            Test(new FieldsResourceDeployer());
        }

        [TestMethod]
        public void TestEvents()
        {
            Test(new EventsResourceDeployer());
        }

        [TestMethod]
        public void TestFieldsWithPreserveName()
        {
            Test(new FieldsWithPreserveNameResourceDeployer());
        }

        [TestMethod]
        public void TestInterfaces()
        {
            Test(new InterfacesResourceDeployer());
        }

        [TestMethod]
        [Ignore] // Bug #49
        public void TestInterfacesWithScriptNamespace()
        {
            Test(new InterfacesWithScriptNamespaceResourceDeployer());
        }

        [TestMethod]
        public void TestMethodsWithPreserveName()
        {
            Test(new MethodsWithPreserveNameResourceDeployer());
        }

        [TestMethod]
        public void TestPropertiesWithPreserveName()
        {
            Test(new PropertiesWithPreserveNameResourceDeployer());
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
