/// <summary>
/// EnumsWithScriptNamespace.cs
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
    using Rosetta.ScriptSharp.AST.Transformers;
    using Rosetta.ScriptSharp.Definition.AST;
    using Rosetta.Translation;
    using Rosetta.Tests.Data;
    using Rosetta.Tests.Utils;

    /// <summary>
    /// 
    /// </summary>
    public class EnumsWithScriptNamespace
    {
        [RenderingResource("ScriptNamespace.SingleEnum.d.ts")]
        public string RenderSingleEnumWithScriptNamespace()
        {
            return GetTranslation(@"
                [ScriptNamespace(""NewNamespace"")]
                public enum Enum1 {
                    Value1
                }
            ");
        }

        [RenderingResource("ScriptNamespace.SingleNamespacedEnum.d.ts")]
        public string RenderSingleNamespacedEnumWithScriptNamespace()
        {
            return GetTranslation(@"
                namespace MyOriginalNamespace {
                    [ScriptNamespace(""NewNamespace"")]
                    public enum Enum1 {
                        Value1
                    }
                }
            ");
        }

        [RenderingResource("ScriptNamespace.TwoEnums.d.ts")]
        public string RenderTwoEnumsWithScriptNamespace()
        {
            return GetTranslation(@"
                [ScriptNamespace(""NewNamespace1"")]
                public enum Enum1 {
                    Value1
                }
                [ScriptNamespace(""NewNamespace2"")]
                public enum Enum2 {
                    Value1
                }
            ");
        }

        [RenderingResource("ScriptNamespace.TwoNamespacedEnums.d.ts")]
        public string RenderTwoNamespacedEnumsWithScriptNamespace()
        {
            return GetTranslation(@"
                namespace MyOriginalNamespace {
                    [ScriptNamespace(""NewNamespace1"")]
                    public enum Enum1 {
                        Value1
                    }
                    [ScriptNamespace(""NewNamespace2"")]
                    public enum Enum2 {
                        Value1
                    }
                }
            ");
        }

        [RenderingResource("ScriptNamespace.TwoMixedEnums.d.ts")]
        public string RenderTwoMixedEnumsWithScriptNamespace()
        {
            return GetTranslation(@"
                [ScriptNamespace(""NewNamespace1"")]
                public enum Enum1 {
                    Value1
                }
                public enum Enum2 {
                    Value1
                }
            ");
        }

        [RenderingResource("ScriptNamespace.TwoMixedNamespacedEnums.d.ts")]
        public string RenderTwoMixedNamespacedEnumsWithScriptNamespace()
        {
            return GetTranslation(@"
                namespace MyOriginalNamespace {
                    [ScriptNamespace(""NewNamespace1"")]
                    public enum Enum1 {
                        Value1
                    }
                    public enum Enum2 {
                        Value1
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
