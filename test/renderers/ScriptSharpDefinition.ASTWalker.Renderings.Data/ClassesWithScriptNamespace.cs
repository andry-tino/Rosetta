/// <summary>
/// ClassesWithScriptNamespace.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST.Renderings.Data
{
    using System;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Symbols;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Text;

    using Rosetta.AST;
    using Rosetta.Renderings.Utils;
    using Rosetta.ScriptSharp.Definition.AST;
    using Rosetta.ScriptSharp.Definition.AST.Transformers;
    using Rosetta.Translation;
    using Rosetta.Tests.Data;
    using Rosetta.Tests.Utils;

    /// <summary>
    /// 
    /// </summary>
    public class ClassesWithScriptNamespace
    {
        [RenderingResource("ScriptNamespace.SingleClass.d.ts")]
        public string RenderSingleClassWithScriptNamespace()
        {
            return GetTranslation(@"
                [ScriptNamespace(""NewNamespace"")]
                public class Class1 {
                    public void Method1(int param1) { }
                    public void Method2(int param1, string param2) { }
                    public void Method3(int param1, string param2, bool param3) { }
                    public void Method4(int param1, string param2, bool param3, double param4) { }
                }
            ");
        }

        [RenderingResource("ScriptNamespace.SingleNamespacedClass.d.ts")]
        public string RenderSingleNamespacedClassWithScriptNamespace()
        {
            return GetTranslation(@"
                namespace MyOriginalNamespace {
                    [ScriptNamespace(""NewNamespace"")]
                    public class Class1 {
                        public void Method1(int param1) { }
                        public void Method2(int param1, string param2) { }
                        public void Method3(int param1, string param2, bool param3) { }
                        public void Method4(int param1, string param2, bool param3, double param4) { }
                    }
                }
            ");
        }

        [RenderingResource("ScriptNamespace.TwoClasses.d.ts")]
        public string RenderTwoClassesWithScriptNamespace()
        {
            return GetTranslation(@"
                [ScriptNamespace(""NewNamespace1"")]
                public class Class1 {
                    public void Method1(int param1) { }
                    public void Method2(int param1, string param2) { }
                }
                [ScriptNamespace(""NewNamespace2"")]
                public class Class2 {
                    public void Method1(int param1) { }
                    public void Method2(int param1, string param2) { }
                }
            ");
        }

        [RenderingResource("ScriptNamespace.TwoNamespacedClasses.d.ts")]
        public string RenderTwoNamespacedClassesWithScriptNamespace()
        {
            return GetTranslation(@"
                namespace MyOriginalNamespace {
                    [ScriptNamespace(""NewNamespace1"")]
                    public class Class1 {
                        public void Method1(int param1) { }
                        public void Method2(int param1, string param2) { }
                    }
                    [ScriptNamespace(""NewNamespace2"")]
                    public class Class2 {
                        public void Method1(int param1) { }
                        public void Method2(int param1, string param2) { }
                    }
                }
            ");
        }

        [RenderingResource("ScriptNamespace.TwoMixedClasses.d.ts")]
        public string RenderTwoMixedClassesWithScriptNamespace()
        {
            return GetTranslation(@"
                [ScriptNamespace(""NewNamespace1"")]
                public class Class1 {
                    public void Method1(int param1) { }
                    public void Method2(int param1, string param2) { }
                }
                public class Class2 {
                    public void Method1(int param1) { }
                    public void Method2(int param1, string param2) { }
                }
            ");
        }

        [RenderingResource("ScriptNamespace.TwoMixedNamespacedClasses.d.ts")]
        public string RenderTwoMixedNamespacedClassesWithScriptNamespace()
        {
            return GetTranslation(@"
                namespace MyOriginalNamespace {
                    [ScriptNamespace(""NewNamespace1"")]
                    public class Class1 {
                        public void Method1(int param1) { }
                        public void Method2(int param1, string param2) { }
                    }
                    public class Class2 {
                        public void Method1(int param1) { }
                        public void Method2(int param1, string param2) { }
                    }
                }
            ");
        }

        private static string GetTranslation(string source)
        {
            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            var node = new NodeLocator(tree).LocateLast(typeof(CompilationUnitSyntax)) as CSharpSyntaxNode;

            // Transforming
            new ScriptNamespaceBasedASTTransformer().Transform(ref node);

            var programNode = node as CompilationUnitSyntax;

            // Creating the walker
            var astWalker = ProgramDefinitionASTWalker.Create(programNode);

            // Getting the translation unit
            ITranslationUnit translationUnit = astWalker.Walk();
            return translationUnit.Translate();
        }
    }
}
