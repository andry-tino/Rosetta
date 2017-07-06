/// <summary>
/// Classes.cs
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
    public class Interfaces
    {
        [RenderingResource("EmptyInterface.d.ts")]
        public string RenderEmptyInterface()
        {
            return GetTranslation(@"
                public interface IInterface1 {
                }
            ");
        }

        [RenderingResource("InterfaceWithMethods.d.ts")]
        public string RenderInterfaceWithMethods()
        {
            // For definitions we need to tests that methods do not show up.
            return GetTranslation(@"
                public interface IInterface1 {
                    Method1();
                    Merhod2(int param1);
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
