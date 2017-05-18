/// <summary>
/// Class.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.UnitTests
{
    using System;
    using System.IO;
    using System.Linq;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    using Rosetta.Reflection.Proxies;
    using Rosetta.Tests.Utils;

    /// <summary>
    /// Utilities.
    /// </summary>
    internal static class Utils
    {
        /// <summary>
        /// From source code, generates the assembly via Roslyn and generates the AST from the assembly using the <see cref="IASTBuilder"/>.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static SyntaxNode ExtractASTRoot(this string source)
        {
            IAssemblyProxy assembly = new AsmlDasmlAssemblyLoader(source).Load();

            var builder = new ASTBuilder(assembly);
            var astInfo = builder.Build();

            // Getting the AST node
            var generatedTree = astInfo.Tree as CSharpSyntaxTree;

            if (generatedTree == null)
            {
                throw new InvalidOperationException("Invalid generated tree");
            }

            return generatedTree.GetRoot();
        }

        /// <summary>
        /// Finds a <see cref="ITypeInfoProxy"/> into an <see cref="IAssemblyProxy"/> by looking at the name (partial match).
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static ITypeInfoProxy LocateType(this IAssemblyProxy assembly, string typeName)
        {
            foreach (var type in assembly.DefinedTypes)
            {
                if (type.Name.Contains(typeName))
                {
                    return type;
                }
            }

            return null;
        }

        /// <summary>
        /// Finds a <see cref="IMethodInfoProxy"/> into an <see cref="ITypeInfoProxy"/> by looking at the name (partial match).
        /// </summary>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public static IMethodInfoProxy LocateMethod(this ITypeInfoProxy type, string methodName)
        {
            foreach (var method in type.DeclaredMethods)
            {
                if (method.Name.Contains(methodName))
                {
                    return method;
                }
            }

            return null;
        }

        /// <summary>
        /// Finds a <see cref="IPropertyInfoProxy"/> into an <see cref="ITypeInfoProxy"/> by looking at the name (partial match).
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IPropertyInfoProxy LocateProperty(this ITypeInfoProxy type, string propertyName)
        {
            foreach (var property in type.DeclaredProperties)
            {
                if (property.Name.Contains(propertyName))
                {
                    return property;
                }
            }

            return null;
        }

        /// <summary>
        /// Finds a <see cref="IConstructorInfoProxy"/> into an <see cref="ITypeInfoProxy"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="argsNum"></param>
        /// <returns></returns>
        public static IConstructorInfoProxy LocateConstructor(this ITypeInfoProxy type, int argsNum)
        {
            foreach (var ctor in type.DeclaredConstructors)
            {
                if ((argsNum == 0 && ctor.Parameters == null) || ctor.Parameters.Count() == argsNum)
                {
                    return ctor;
                }
            }

            return null;
        }

        /// <summary>
        /// Checks that a modifier is present in the list.
        /// </summary>
        /// <param name="modifiers"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static bool CheckModifier(this SyntaxTokenList modifiers, SyntaxKind kind)
        {
            foreach (var modifier in modifiers)
            {
                if (modifier.Kind() == kind)
                {
                    return true;
                }
            }

            return false;
        }

        #region Types

        /// <summary>
        /// Assembly loader based on <see cref="AsmlDasml"/>.
        /// </summary>
        public class AsmlDasmlAssemblyLoader : IAssemblyLoader
        {
            private readonly string source;

            public AsmlDasmlAssemblyLoader(string source)
            {
                if (source == null)
                {
                    throw new ArgumentNullException(nameof(source));
                }

                this.source = source;
            }

            public Stream RawAssembly => null;

            public IAssemblyProxy Load()
            {
                return AsmlDasml.Assemble(this.source, stream => new MonoStreamAssemblyLoader(stream).Load()) as IAssemblyProxy;
            }
        }

        #endregion
    }
}
