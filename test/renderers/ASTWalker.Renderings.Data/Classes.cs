/// <summary>
/// Classes.cs
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
    public class Classes
    {
        [RenderingResource("SimpleEmptyClass.ts")]
        public string RenderSimpleEmptyClass()
        {
            var sourceInfo = SourceGenerator.Generate(SourceOptions.None);
            string source = sourceInfo.Key;

            return GetTranslation(source);
        }
        
        [RenderingResource("SimpleClass.ts")]
        public string RenderSimpleClass()
        {
            var sourceInfo = SourceGenerator.Generate(SourceOptions.None, ClassOptions.HasContent);
            string source = sourceInfo.Key;

            return GetTranslation(source);
        }
        
        [RenderingResource("SimpleEmptyClassWithConstructor.ts")]
        public string RenderSimpleEmptyClassWithConstructor()
        {
            return GetTranslation(@"
                class Class1 {
                    public Class1() { }
                }
            ");
        }

        [RenderingResource("ClassWithMethodsWithParameters.ts")]
        public string RenderClassWithMethodsWithParameters()
        {
            return GetTranslation(@"
                public class Class1 {
                    public void Method1(int param1) { }
                    public void Method2(int param1, string param2) { }
                    public void Method3(int param1, string param2, bool param3) { }
                    public void Method4(int param1, string param2, bool param3, double param4) { }
                }
            ");
        }

        [RenderingResource("ClassWithConstructorsWithParameters.ts")]
        public string RenderClassWithConstructorsWithParameters()
        {
            return GetTranslation(@"
                public class Class1 {
                    public Class1(int param1) { }
                    public Class1(int param1, string param2) { }
                    public Class1(int param1, string param2, bool param3) { }
                    public Class1(int param1, string param2, bool param3, double param4) { }
                }
            ");
        }

        [RenderingResource("ClassWithSimpleProperties.ts")]
        public string RenderClassWithSimpleProperty()
        {
            return GetTranslation(@"
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
            ");
        }
        
        [RenderingResource("ClassWithMethodExpression.ts")]
        public string RenderClassWithMethodExpression()
        {
            var sourceInfo = SourceGenerator.Generate(SourceOptions.None, ClassOptions.HasContent, FunctionOptions.HasExpressions);
            string source = sourceInfo.Key;

            return GetTranslation(source);
        }
        
        [RenderingResource("ClassWithMethodStatements.ts")]
        public string RenderClassWithMethodStatements()
        {
            var sourceInfo = SourceGenerator.Generate(SourceOptions.None, ClassOptions.HasContent, FunctionOptions.HasStatements);
            string source = sourceInfo.Key;

            return GetTranslation(source);
        }

        private static string GetTranslation(string source)
        {
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
    }
}
