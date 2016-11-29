/// <summary>
/// InterfacesWithScriptNamespace.cs
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
    public class InterfacesWithScriptNamespace
    {
        [RenderingResource("ScriptNamespace.SingleInterface.d.ts")]
        public string RenderSingleInterfaceWithScriptNamespace()
        {
            return GetTranslation(@"
                [ScriptNamespace(""NewNamespace"")]
                public interface IInterface1 {
                    void Method1(int param1);
                }
            ");
        }

        [RenderingResource("ScriptNamespace.SingleNamespacedInterface.d.ts")]
        public string RenderSingleNamespacedInterfaceWithScriptNamespace()
        {
            return GetTranslation(@"
                namespace MyOriginalNamespace {
                    [ScriptNamespace(""NewNamespace"")]
                    public interface IInterface1 {
                        void Method1(int param1);
                    }
                }
            ");
        }

        [RenderingResource("ScriptNamespace.TwoInterfaces.d.ts")]
        public string RenderTwoInterfacesWithScriptNamespace()
        {
            return GetTranslation(@"
                [ScriptNamespace(""NewNamespace1"")]
                public interface IInterface1 {
                    void Method1(int param1);
                }
                [ScriptNamespace(""NewNamespace2"")]
                public interface IInterface2 {
                    void Method1(int param1);
                }
            ");
        }

        [RenderingResource("ScriptNamespace.TwoNamespacedInterfaces.d.ts")]
        public string RenderTwoNamespacedInterfacesWithScriptNamespace()
        {
            return GetTranslation(@"
                namespace MyOriginalNamespace {
                    [ScriptNamespace(""NewNamespace1"")]
                    public interface IInterface1 {
                        void Method1(int param1);
                    }
                    [ScriptNamespace(""NewNamespace2"")]
                    public interface IInterface2 {
                        void Method1(int param1);
                    }
                }
            ");
        }

        [RenderingResource("ScriptNamespace.TwoMixedInterfaces.d.ts")]
        public string RenderTwoMixedInterfacesWithScriptNamespace()
        {
            return GetTranslation(@"
                [ScriptNamespace(""NewNamespace1"")]
                public interface IInterface1 {
                    void Method1(int param1);
                }
                public interface IInterface2 {
                    void Method1(int param1);
                }
            ");
        }

        [RenderingResource("ScriptNamespace.TwoMixedNamespacedInterfaces.d.ts")]
        public string RenderTwoMixedNamespacedInterfacesWithScriptNamespace()
        {
            return GetTranslation(@"
                namespace MyOriginalNamespace {
                    [ScriptNamespace(""NewNamespace1"")]
                    public interface IInterface1 {
                        void Method1(int param1);
                    }
                    public interface IInterface2 {
                        void Method1(int param1);
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
            new ScriptNamespaceBasedASTTransformer().Transform(ref tree);

            var programNode = node as CompilationUnitSyntax;

            // Creating the walker
            var astWalker = ProgramDefinitionASTWalker.Create(programNode);

            // Getting the translation unit
            ITranslationUnit translationUnit = astWalker.Walk();
            return translationUnit.Translate();
        }
    }
}
