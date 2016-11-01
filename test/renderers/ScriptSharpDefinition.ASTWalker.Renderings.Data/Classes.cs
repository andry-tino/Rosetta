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
    public class Classes
    {
        [RenderingResource("ClassWithMethodsWithParameters.d.ts")]
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

        [RenderingResource("ClassWithConstructorsWithParameters.d.ts")]
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

        [RenderingResource("ClassWithSimpleProperties.d.ts")]
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

        [RenderingResource("ClassWithSimpleMixedContent.d.ts")]
        public string RenderClassWithSimpleMixedContent()
        {
            return GetTranslation(@"
                public class Class1 {
                    public Class1(int param1) { }

                    public int Property1 {
                        get { return 1; }
                        set { }
                    }

                    public int Property2 {
                        get { return 2; }
                    }

                    public void Method2(int param1, string param2) { }

                    public void Method3(int param1, string param2, bool param3) { }

                    public int Method4(int param1, string param2, bool param3, string param4) { 
                        return 3;
                    }

                    public int Property3 {
                        set { }
                    }
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
