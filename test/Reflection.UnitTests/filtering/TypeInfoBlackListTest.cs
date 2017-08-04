/// <summary>
/// TypeInfoBlackListTest.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rosetta.Reflection.Proxies;

    /// <summary>
    /// Unit tests for <see cref="TypeInfoBlackList"/>.
    /// </summary>
    [TestClass]
    public class TypeInfoBlackListTest
    {
        [TestMethod]
        public void EmptyCollections()
        {
            Test(new ITypeInfoProxy[0], new ITypeInfoProxy[0], new string[0]);
        }

        [TestMethod]
        public void EmptyBlackList()
        {
            var types = new ITypeInfoProxy[] 
            {
                new MockedTypeInfoProxy("TypeA"),
                new MockedTypeInfoProxy("TypeB")
            };
            Test(types, types, new string[0]);
        }

        [TestMethod]
        public void EmptyInputCollection()
        {
            Test(new ITypeInfoProxy[0], new ITypeInfoProxy[0], new string[] { "TypeA", "TypeB" });
        }

        [TestMethod]
        public void NoBlackListed()
        {
            var types = new ITypeInfoProxy[]
            {
                new MockedTypeInfoProxy("TypeA"),
                new MockedTypeInfoProxy("TypeB")
            };
            Test(types, types, new string[] { "TypeC", "TypeD" });
        }

        [TestMethod]
        public void BlackListed()
        {
            MockedTypeInfoProxy typeA = new MockedTypeInfoProxy("TypeA"),
                                typeB = new MockedTypeInfoProxy("TypeB"),
                                typeC = new MockedTypeInfoProxy("TypeC");
            Test(new[] { typeA, typeB, typeC }, new[] { typeA, typeB }, new string[] { "TypeC", "TypeD" });
        }

        [TestMethod]
        public void BlackListedWithPartialNames()
        {
            MockedTypeInfoProxy typeA = new MockedTypeInfoProxy("TypeAAA"),
                                typeB = new MockedTypeInfoProxy("TypeBB"),
                                typeC = new MockedTypeInfoProxy("TypeA");
            Test(new[] { typeA, typeB, typeC }, new[] { typeA, typeB }, new string[] { "TypeA", "TypeB" });
        }

        private static void Test(IEnumerable<ITypeInfoProxy> originalTypes, IEnumerable<ITypeInfoProxy> expectedFilteredTypes, 
            IEnumerable<string> blackListedNames)
        {
            ITypeInfoFilter blackList = new TypeInfoBlackList(blackListedNames);
            var filteredTypes = blackList.Filter(originalTypes);

            Assert.AreEqual(expectedFilteredTypes.Count(), filteredTypes.Count(), "Mismatch in expected filtered types collection size");

            Assert.AreEqual(0, expectedFilteredTypes.Where(
                expectedFilteredType => filteredTypes.All(
                    filteredType => filteredType.FullName != expectedFilteredType.FullName)).Count(), 
                    $"Filtered types contains unexpected values. Expected: {PrintCollection(expectedFilteredTypes)}, Actual: {PrintCollection(filteredTypes)}");
        }

        private static string PrintCollection(IEnumerable<ITypeInfoProxy> types)
        {
            return types.Count() > 0 
                ? types.Select(type => type.FullName).Aggregate((a, b) => $"{a}, {b}") 
                : string.Empty;
        }

        #region Types

        private class MockedTypeInfoProxy : ITypeInfoProxy
        {
            private string name;

            public MockedTypeInfoProxy(string name)
            {
                this.name = name;
            }

            public string FullName => this.name;

            public string Name => this.name;

            public ITypeProxy BaseType => null;

            public IEnumerable<ICustomAttributeDataProxy> CustomAttributes => new ICustomAttributeDataProxy[0];

            public IEnumerable<IConstructorInfoProxy> DeclaredConstructors => new IConstructorInfoProxy[0];

            public IEnumerable<IFieldInfoProxy> DeclaredFields => new IFieldInfoProxy[0];

            public IEnumerable<IMethodInfoProxy> DeclaredMethods => new IMethodInfoProxy[0];

            public IEnumerable<IPropertyInfoProxy> DeclaredProperties => new IPropertyInfoProxy[0];
            
            public IEnumerable<ITypeProxy> ImplementedInterfaces => new ITypeProxy[0];

            public bool IsClass => true;

            public bool IsEnum => false;

            public bool IsInterface => false;

            public bool IsNotPublic => false;

            public bool IsPublic => true;

            public bool IsValueType => false;
            
            public string Namespace => string.Empty;
        }

        #endregion
    }
}
