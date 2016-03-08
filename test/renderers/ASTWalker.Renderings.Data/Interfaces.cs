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
            var source = @"
                interface IInterface1 {
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);
            Source.ProgramRoot = tree;

            SyntaxNode node = new NodeLocator(tree).LocateLast(typeof(InterfaceDeclarationSyntax));
            InterfaceDeclarationSyntax interfaceDeclarationNode = node as InterfaceDeclarationSyntax;

            // Creating the walker
            var astWalker = InterfaceASTWalker.Create(interfaceDeclarationNode);

            // Getting the translation unit
            ITranslationUnit translationUnit = astWalker.Walk();
            return translationUnit.Translate();
        }

        [RenderingResource("InterfaceWithExtendedInterfaces.ts")]
        public string RenderInterfaceWithExtendedInterfaces()
        {
            string source = @"
                interface IInterface2 { }
                interface IInterface3 { }
                interface IInterface4 { }
                public interface IInterface1 : IInterface2, IInterface3, IInterface4 {
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);
            Source.ProgramRoot = tree;

            SyntaxNode node = new NodeLocator(tree).LocateLast(typeof(InterfaceDeclarationSyntax));
            InterfaceDeclarationSyntax interfaceDeclarationNode = node as InterfaceDeclarationSyntax;

            // Creating the walker
            var astWalker = InterfaceASTWalker.Create(interfaceDeclarationNode);

            // Getting the translation unit
            ITranslationUnit translationUnit = astWalker.Walk();
            return translationUnit.Translate();
        }

        [RenderingResource("InterfaceWithMethods.ts")]
        public string RenderInterfaceWithMethods()
        {
            string source = @"
                public interface IInterface1 {
                    void Method1(int param1);
                    int Method2(int param1, string param2);
                    string Method3(int param1, string param2, bool param3);
                    void Method4(int param1, string param2, bool param3, double param4);
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);
            Source.ProgramRoot = tree;

            SyntaxNode node = new NodeLocator(tree).LocateLast(typeof(InterfaceDeclarationSyntax));
            InterfaceDeclarationSyntax interfaceDeclarationNode = node as InterfaceDeclarationSyntax;

            // Creating the walker
            var astWalker = InterfaceASTWalker.Create(interfaceDeclarationNode);

            // Getting the translation unit
            ITranslationUnit translationUnit = astWalker.Walk();
            return translationUnit.Translate();
        }

        [RenderingResource("InterfaceWithProperties.ts")]
        public string RenderInterfaceWithProperties()
        {
            string source = @"
                public interface IInterface1 {
                    int Property1 { get; set; }
                    string Property2 { get; set; }
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);
            Source.ProgramRoot = tree;

            SyntaxNode node = new NodeLocator(tree).LocateLast(typeof(InterfaceDeclarationSyntax));
            InterfaceDeclarationSyntax interfaceDeclarationNode = node as InterfaceDeclarationSyntax;

            // Creating the walker
            var astWalker = InterfaceASTWalker.Create(interfaceDeclarationNode);

            // Getting the translation unit
            ITranslationUnit translationUnit = astWalker.Walk();
            return translationUnit.Translate();
        }

        [RenderingResource("InterfaceWithGetterProperties.ts")]
        public string RenderInterfaceWithGetterProperties()
        {
            string source = @"
                public interface IInterface1 {
                    int Property1 { get; }
                    string Property2 { get; }
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);
            Source.ProgramRoot = tree;

            SyntaxNode node = new NodeLocator(tree).LocateLast(typeof(InterfaceDeclarationSyntax));
            InterfaceDeclarationSyntax interfaceDeclarationNode = node as InterfaceDeclarationSyntax;

            // Creating the walker
            var astWalker = InterfaceASTWalker.Create(interfaceDeclarationNode);

            // Getting the translation unit
            ITranslationUnit translationUnit = astWalker.Walk();
            return translationUnit.Translate();
        }

        [RenderingResource("InterfaceWithMethodsAndProperties.ts")]
        public string RenderInterfaceWithMethodsAndProperties()
        {
            string source = @"
                public interface IInterface1 {
                    string Method3(int param1, string param2, bool param3);
                    void Method4(int param1, string param2, bool param3, double param4);
                    int Property1 { get; }
                    string Property2 { get; }
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);
            Source.ProgramRoot = tree;

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
