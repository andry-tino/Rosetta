/// <summary>
/// Methods.cs
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
    public class Methods
    {
        [RenderingResource("EmptyMethod.ts")]
        public string RenderEmptyMethod()
        {
            return GetTranslation(@"
                public class Class1 {
                    public void Method1() { }
                }
            ");
        }

        [RenderingResource("EmptyMethodWithReturn.ts")]
        public string RenderEmptyMethodWithReturn()
        {
            return GetTranslation(@"
                public class Class1 {
                    public int Method1() { 
                        return 0;
                    }
                }
            ");
        }

        [RenderingResource("SimpleMethodWithVariableDeclarations.ts")]
        public string RenderSimpleMethodWithVariableDeclarations()
        {
            return GetTranslation(@"
                public class Class1 {
                    public void Method1() { 
                        string variable1;
                        int variable2;
                    }
                }
            ");
        }

        [RenderingResource("EmptyMethodWith1Argument.ts")]
        public string RenderEmptyMethodWith1Argument()
        {
            return GetTranslation(@"
                public class Class1 {
                    public void Method1(string param1) { }
                }
            ");
        }

        [RenderingResource("EmptyMethodWith2Arguments.ts")]
        public string RenderEmptyMethodWith2Arguments()
        {
            return GetTranslation(@"
                public class Class1 {
                    public void Method1(string param1, int param2) { }
                }
            ");
        }

        [RenderingResource("EmptyMethodWith3Arguments.ts")]
        public string RenderEmptyMethodWith3Arguments()
        {
            return GetTranslation(@"
                public class Class1 {
                    public void Method1(string param1, int param2, bool param3) { }
                }
            ");
        }

        [RenderingResource("EmptyMethodWithManyArguments.ts")]
        public string RenderEmptyMethodWithManyArgument()
        {
            return GetTranslation(@"
                public class Class1 {
                    public void Method1(string param1, int param2, bool param3, string param4, int param5) { }
                }
            ");
        }

        private static string GetTranslation(string source)
        {
            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            SyntaxNode node = new NodeLocator(tree).LocateLast(typeof(MethodDeclarationSyntax));
            var methodDeclarationNode = node as MethodDeclarationSyntax;

            // Creating the walker
            var astWalker = MethodASTWalker.Create(methodDeclarationNode);

            // Getting the translation unit
            ITranslationUnit translationUnit = astWalker.Walk();
            return translationUnit.Translate();
        }
    }
}
