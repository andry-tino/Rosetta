/// <summary>
/// ObjectClassTest.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Helpers.UnitTests
{
    using System;
    
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rosetta.Reflection.Helpers;
    using Rosetta.Reflection.Proxies;
    using Rosetta.Reflection.UnitTests;

    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class ObjectClassTest
    {
        [TestMethod]
        public void ClassBaseTypeWithImplicitInheritance()
        {
            Test(@"
                class MyClass {
                }
            ");
        }

        [TestMethod]
        public void ClassBaseTypeWithExplicitInheritance()
        {
            Test(@"
                class MyClass : System.Object {
                }
            ");

            Test(@"
                class MyClass : object {
                }
            ");
        }

        private static void Test(string source, bool expected = true)
        {
            // Assembling some code
            IAssemblyLoader assemblyLoader = new Utils.AsmlDasmlAssemblyLoader(source);

            // Loading the assembly
            IAssemblyProxy assembly = assemblyLoader.Load();

            // Locating the class
            ITypeInfoProxy classDefinition = assembly.LocateType("MyClass");
            Assert.IsNotNull(classDefinition);

            ITypeProxy baseClass = classDefinition.BaseType;
            Assert.IsNotNull(baseClass);

            var helper = new ObjectClass(baseClass);

            Assert.AreEqual(expected, helper.Is, "Object class check failed");
        }
    }
}
