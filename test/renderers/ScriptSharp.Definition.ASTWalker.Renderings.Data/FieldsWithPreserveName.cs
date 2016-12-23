/// <summary>
/// FieldsWithPreserveName.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST.Renderings.Data
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST;
    using Rosetta.Renderings.Utils;
    using Rosetta.ScriptSharp.Definition.AST;
    using Rosetta.Translation;
    using Rosetta.Tests.Utils;

    /// <summary>
    /// 
    /// </summary>
    public class FieldsWithPreserveName
    {
        [RenderingResource("PreserveName.Field.d.ts")]
        public string RenderFieldWithPreserveName()
        {
            return GetTranslation(@"
                public class MyClass {
                    [PreserveName]
                    public int MyField;
                }
            ");
        }

        private static string GetTranslation(string source)
        {
            // Getting the AST node
            CSharpSyntaxTree tree = ASTExtractor.Extract(source);

            var node = new NodeLocator(tree).LocateLast(typeof(CompilationUnitSyntax)) as CSharpSyntaxNode;
            var programNode = node as CompilationUnitSyntax;

            // Creating the walker
            var astWalker = ProgramDefinitionASTWalker.Create(programNode);

            // Getting the translation unit
            ITranslationUnit translationUnit = astWalker.Walk();
            return translationUnit.Translate();
        }
    }
}
