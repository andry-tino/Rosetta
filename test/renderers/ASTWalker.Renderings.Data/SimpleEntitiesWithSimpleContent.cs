/// <summary>
/// SimpleEntitiesWithSimpleContent.cs
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
    public class SimpleEntitiesWithSimpleContent
    {
        [RenderingResource("ClassWithMethodsWithParameters.ts")]
        public string RenderClassWithMethodsWithParameters()
        {
            string source = @"
                public class Class1 {
                    public void Method1(int param1) { }
                    public void Method2(int param1, string param2) { }
                    public void Method3(int param1, string param2, bool param3) { }
                    public void Method4(int param1, string param2, bool param3, double param4) { }
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);
            Source.ProgramRoot = tree;

            SyntaxNode node = new NodeLocator(tree).LocateLast(typeof(ClassDeclarationSyntax));
            ClassDeclarationSyntax classDeclarationNode = node as ClassDeclarationSyntax;

            // Creating the walker
            var astWalker = ClassASTWalker.Create(classDeclarationNode);

            // Getting the translation unit
            ITranslationUnit translationUnit = astWalker.Walk();
            return translationUnit.Translate();
        }

        [RenderingResource("ClassWithConstructorsWithParameters.ts")]
        public string RenderClassWithConstructorsWithParameters()
        {
            string source = @"
                public class Class1 {
                    public Class1(int param1) { }
                    public Class1(int param1, string param2) { }
                    public Class1(int param1, string param2, bool param3) { }
                    public Class1(int param1, string param2, bool param3, double param4) { }
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);
            Source.ProgramRoot = tree;

            SyntaxNode node = new NodeLocator(tree).LocateLast(typeof(ClassDeclarationSyntax));
            ClassDeclarationSyntax classDeclarationNode = node as ClassDeclarationSyntax;

            // Creating the walker
            var astWalker = ClassASTWalker.Create(classDeclarationNode);

            // Getting the translation unit
            ITranslationUnit translationUnit = astWalker.Walk();
            return translationUnit.Translate();
        }

        [RenderingResource("ClassWithSimpleProperties.ts")]
        public string RenderClassWithSimpleProperty()
        {
            string source = @"
                public class Class1 {
                    public int Property1 {
                        get { return 1; }
                        set { }
                    }

                    public int Property2 {
                        get { return 2; }
                    }

                    public int Property3 {
                        set { }
                    }
                }
            ";

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);
            Source.ProgramRoot = tree;

            SyntaxNode node = new NodeLocator(tree).LocateFirst(typeof(ClassDeclarationSyntax));
            ClassDeclarationSyntax classDeclarationNode = node as ClassDeclarationSyntax;

            // Creating the walker
            var astWalker = ClassASTWalker.Create(classDeclarationNode);

            // Getting the translation unit
            ITranslationUnit translationUnit = astWalker.Walk();
            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("ClassWithMethodExpression.ts")]
        public string RenderClassWithMethodExpression()
        {
            var sourceInfo = SourceGenerator.Generate(SourceOptions.None, ClassOptions.HasContent, FunctionOptions.HasExpressions);
            string source = sourceInfo.Key;

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);
            Source.ProgramRoot = tree;

            SyntaxNode node = new NodeLocator(tree).LocateLast(typeof(ClassDeclarationSyntax));
            ClassDeclarationSyntax classDeclarationNode = node as ClassDeclarationSyntax;

            // Creating the walker
            var astWalker = ClassASTWalker.Create(classDeclarationNode);

            // Getting the translation unit
            ITranslationUnit translationUnit = astWalker.Walk();
            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("ClassWithMethodStatements.ts")]
        public string RenderClassWithMethodStatements()
        {
            var sourceInfo = SourceGenerator.Generate(SourceOptions.None, ClassOptions.HasContent, FunctionOptions.HasStatements);
            string source = sourceInfo.Key;

            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);
            Source.ProgramRoot = tree;

            SyntaxNode node = new NodeLocator(tree).LocateLast(typeof(ClassDeclarationSyntax));
            ClassDeclarationSyntax classDeclarationNode = node as ClassDeclarationSyntax;

            // Creating the walker
            var astWalker = ClassASTWalker.Create(classDeclarationNode);

            // Getting the translation unit
            ITranslationUnit translationUnit = astWalker.Walk();
            return translationUnit.Translate();
        }

        [RenderingResource("InterfaceWithMethods.ts")]
        public string RenderInterfaceWithMethods()
        {
            string source = @"
                public interface Interface1 {
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
                public interface Interface1 {
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
                public interface Interface1 {
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
                public interface Interface1 {
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
