/// <summary>
/// Enums.cs
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
    using Rosetta.Translation;
    using Rosetta.Tests.Data;
    using Rosetta.Tests.Utils;

    /// <summary>
    /// 
    /// </summary>
    public class Enums
    {
        [RenderingResource("EmptyEnum.d.ts")]
        public string RenderEmptyEnum()
        {
            return GetTranslation(@"
                public enum Enum1 {
                }
            ");
        }

        [RenderingResource("EnumWithValues.d.ts")]
        public string RenderEnumWithValues()
        {
            return GetTranslation(@"
                public enum Enum1 {
                    Value1,
                    Value2
                }
            ");
        }

        [RenderingResource("EnumWithValuesWithEqualsExpr.d.ts")]
        public string RenderEnumWithValuesWithEqualsExpr()
        {
            return GetTranslation(@"
                public enum Enum1 {
                    Value1 = 0,
                    Value2 = 1
                }
            ");
        }

        private static string GetTranslation(string source)
        {
            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            SyntaxNode node = new NodeLocator(tree).LocateLast(typeof(CompilationUnitSyntax));
            CompilationUnitSyntax compilationUnitNode = node as CompilationUnitSyntax;

            // Creating the walker
            var astWalker = ProgramDefinitionASTWalker.Create(compilationUnitNode);

            // Getting the translation unit
            ITranslationUnit translationUnit = astWalker.Walk();
            return translationUnit.Translate();
        }
    }
}
