/// <summary>
/// Interfaces.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Renderings.Data
{
    using System;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Symbols;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Text;

    using Rosetta.Renderings.Utils;
    using Rosetta.Translation;
    using Rosetta.Tests.Data;
    using Rosetta.Tests.Utils;

    /// <summary>
    /// 
    /// </summary>
    public class Interfaces
    {
        [RenderingResource("SimpleEmptyInterface.ts")]
        public string RenderSimpleEmptyInterface()
        {
            return GetTranslation(@"
                interface IInterface1 {
                }
            ");
        }

        [RenderingResource("InterfaceWithExtendedInterfaces.ts")]
        public string RenderInterfaceWithExtendedInterfaces()
        {
            return GetTranslation(@"
                interface IInterface2 { }
                interface IInterface3 { }
                interface IInterface4 { }
                public interface IInterface1 : IInterface2, IInterface3, IInterface4 {
                }
            ");
        }

        [RenderingResource("InterfaceWithMethods.ts")]
        public string RenderInterfaceWithMethods()
        {
            return GetTranslation(@"
                public interface IInterface1 {
                    void Method1(int param1);
                    int Method2(int param1, string param2);
                    string Method3(int param1, string param2, bool param3);
                    void Method4(int param1, string param2, bool param3, double param4);
                }
            ");
        }

        [RenderingResource("InterfaceWithProperties.ts")]
        public string RenderInterfaceWithProperties()
        {
            return GetTranslation(@"
                public interface IInterface1 {
                    int Property1 { get; set; }
                    string Property2 { get; set; }
                }
            ");
        }

        [RenderingResource("InterfaceWithGetterProperties.ts")]
        public string RenderInterfaceWithGetterProperties()
        {
            return GetTranslation(@"
                public interface IInterface1 {
                    int Property1 { get; }
                    string Property2 { get; }
                }
            ");
        }

        [RenderingResource("InterfaceWithMethodsAndProperties.ts")]
        public string RenderInterfaceWithMethodsAndProperties()
        {
            return GetTranslation(@"
                public interface IInterface1 {
                    string Method3(int param1, string param2, bool param3);
                    void Method4(int param1, string param2, bool param3, double param4);
                    int Property1 { get; }
                    string Property2 { get; }
                }
            ");
        }

        private static string GetTranslation(string source)
        {
            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            SyntaxNode node = new NodeLocator(tree).LocateLast(typeof(InterfaceDeclarationSyntax));
            InterfaceDeclarationSyntax interfaceDeclarationNode = node as InterfaceDeclarationSyntax;

            // Creating the walker
            var astWalker = InterfaceASTWalker.Create(interfaceDeclarationNode);

            // Getting the translation unit
            ITranslationUnit translationUnit = astWalker.Walk();
            return translationUnit.Translate();
        }
    }
}
