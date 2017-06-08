/// <summary>
/// Class.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.UnitTests
{
    using System;
    
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class ASTBuilderTest
    {
        [TestMethod]
        public void GeneratedTypesNumber()
        {
            var astInfo = (@"
                namespace Root.MyNamespace1 {
                    public class MyClass1 {
                    }
                    public class MyClass2 {
                    }

                    public interface MyInterface1 {
                    }
                    public interface MyInterface2 {
                    }
                    public interface MyInterface3 {
                    }

                    public enum MyEnum1 {
                    }
                    public enum MyEnum2 {
                    }
                }
            ").BuildAST();

            Assert.IsTrue(astInfo.ClassCount >= 2, "Number of classes does not match"); // Mono adds class `Program` and `<Module>`
            Assert.AreEqual(3, astInfo.InterfaceCount, "Number of interfaces does not match");
            Assert.AreEqual(2, astInfo.EnumCount, "Number of enums does not match");
            Assert.AreEqual(0, astInfo.StructCount, "Number of structs does not match");
        }

        [TestMethod]
        public void GeneratedTypesNumberWithMixedVisibilities()
        {
            var astInfo = (@"
                namespace Root.MyNamespace1 {
                    public class MyClass1 {
                    }
                    class MyClass2 {
                    }

                    public interface MyInterface1 {
                    }
                    interface MyInterface2 {
                    }
                    public interface MyInterface3 {
                    }

                    public enum MyEnum1 {
                    }
                    enum MyEnum2 {
                    }
                }
            ").BuildAST();

            Assert.IsTrue(astInfo.ClassCount >= 2, "Number of classes does not match"); // Mono adds class `Program` and `<Module>`
            Assert.AreEqual(3, astInfo.InterfaceCount, "Number of interfaces does not match");
            Assert.AreEqual(2, astInfo.EnumCount, "Number of enums does not match");
            Assert.AreEqual(0, astInfo.StructCount, "Number of structs does not match");
        }
    }
}
