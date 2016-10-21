/// <summary>
/// ScriptNamespaceBasedASTTransformer.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST.Transformers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    
    using Rosetta.AST.Transformers;
    using Rosetta.AST.Utilities;

    /// <summary>
    /// Base class for rearrangement of namespaces for classes basing on ScriptSharp's <code>ScriptNamespace</code> attribute.
    /// </summary>
    public class ScriptNamespaceBasedASTTransformer : ClassWithAttributeInDifferentNamespaceASTTransformer
    {
        private const string scriptNamespaceFullName = "ScriptNamespace";

        private List<KeyValuePair<ClassDeclarationSyntax, string>> classDeclarations;
        private CSharpSyntaxNode node;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassWithAttributeInDifferentNamespaceASTTransformer"/> class.
        /// </summary>
        public ScriptNamespaceBasedASTTransformer()
            : base(string.Empty, scriptNamespaceFullName)
        {
        }

        /// <summary>
        /// Transforms the tree.
        /// </summary>
        /// <param name="node"></param>
        public override void Transform(ref CSharpSyntaxNode node)
        {
            if (node as CompilationUnitSyntax == null)
            {
                throw new ArgumentException(nameof(node), 
                    $"This class can only handle nodes of type: {typeof(CompilationUnitSyntax).Name}!");
            }

            this.Initialize(node);

            var members = new SyntaxList<MemberDeclarationSyntax>();

            var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName("MyNamespace"));
            members.Add(@namespace);

            CompilationUnitSyntax c = SyntaxFactory.CompilationUnit(
                new SyntaxList<ExternAliasDirectiveSyntax>(), 
                new SyntaxList<UsingDirectiveSyntax>(), 
                new SyntaxList<AttributeListSyntax>(), 
                members);

            this.RetrieveOverridenNamespaceNames();

            this.CleanUp();
        }

        private void RetrieveOverridenNamespaceNames()
        {
            new MultiPurposeASTWalker(this.node,
                delegate (SyntaxNode astNode)
                {
                    var classNode = astNode as ClassDeclarationSyntax;
                    if (classNode == null)
                    {
                        return false;
                    }
                    
                    return IsScriptNamespaceAttributePresent(classNode.AttributeLists);
                },
                delegate (SyntaxNode astNode)
                {
                    var couple = new KeyValuePair<ClassDeclarationSyntax, string>(
                        astNode as ClassDeclarationSyntax, 
                        ""); // TODO: Get the value: (astNode as ClassDeclarationSyntax).AttributeLists[0].Attributes[0].ArgumentList.Arguments[0].GetText()

                    this.classDeclarations.Add(couple);
                }
                ).Start();
        }

        private void Initialize(CSharpSyntaxNode node)
        {
            this.node = node;
            this.classDeclarations = new List<KeyValuePair<ClassDeclarationSyntax, string>>();
        }

        private void CleanUp()
        {
            this.node = null;
            this.classDeclarations = null;
        }

        private static bool IsScriptNamespaceAttributePresent(SyntaxList<AttributeListSyntax> attributeLists)
        {
            foreach (var attributeList in attributeLists)
            {
                if (IsScriptNamespaceAttributePresent(attributeList))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsScriptNamespaceAttributePresent(AttributeListSyntax attributes)
        {
            foreach (var attribute in attributes.Attributes)
            {
                if (IsAttributeNameCompatible(attribute.Name.ToString()))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsAttributeNameCompatible(string name)
        {
            return name.Contains("ScriptNamespace");
        }
    }
}
