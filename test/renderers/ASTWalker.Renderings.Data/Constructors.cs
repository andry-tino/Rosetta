/// <summary>
/// Constructors.cs
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
    public class Constructors
    {
        [RenderingResource("EmptyConstructor.ts")]
        public string RenderEmptyConstructor()
        {
            return GetTranslation(@"
                public class Class1 {
                    public Class1() { }
                }
            ");
        }

        [RenderingResource("SimpleConstructorWithVariableDeclarations.ts")]
        public string RenderSimpleConstructorWithVariableDeclarations()
        {
            return GetTranslation(@"
                public class Class1 {
                    public Class1() { 
                        string variable1;
                        int variable2;
                    }
                }
            ");
        }

        [RenderingResource("EmptyConstructorWith1Argument.ts")]
        public string RenderEmptyConstructorWith1Argument()
        {
            return GetTranslation(@"
                public class Class1 {
                    public Class1(string param1) { }
                }
            ");
        }

        [RenderingResource("EmptyConstructorWith2Arguments.ts")]
        public string RenderEmptyConstructorWith2Arguments()
        {
            return GetTranslation(@"
                public class Class1 {
                    public Class1(string param1, int param2) { }
                }
            ");
        }

        [RenderingResource("EmptyConstructorWith3Arguments.ts")]
        public string RenderEmptyConstructorWith3Arguments()
        {
            return GetTranslation(@"
                public class Class1 {
                    public Class1(string param1, int param2, bool param3) { }
                }
            ");
        }

        [RenderingResource("EmptyConstructorWithManyArguments.ts")]
        public string RenderEmptyConstructorWithManyArgument()
        {
            return GetTranslation(@"
                public class Class1 {
                    public Class1(string param1, int param2, bool param3, string param4, int param5) { }
                }
            ");
        }

        private static string GetTranslation(string source)
        {
            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            SyntaxNode node = new NodeLocator(tree).LocateLast(typeof(ConstructorDeclarationSyntax));
            var constructorDeclarationNode = node as ConstructorDeclarationSyntax;

            // Creating the walker
            var astWalker = ConstructorASTWalker.Create(constructorDeclarationNode);

            // Getting the translation unit
            ITranslationUnit translationUnit = astWalker.Walk();
            return translationUnit.Translate();
        }
    }
}
