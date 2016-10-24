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

    using Rosetta.AST.Helpers;
    using Rosetta.AST.Transformers;
    using Rosetta.AST.Utilities;
    using Rosetta.ScriptSharp.Definition.AST.Helpers;

    /// <summary>
    /// Base class for rearrangement of namespaces for classes basing on ScriptSharp's <code>ScriptNamespace</code> attribute.
    /// </summary>
    public class ScriptNamespaceBasedASTTransformer : ClassWithAttributeInDifferentNamespaceASTTransformer
    {
        private List<KeyValuePair<ClassDeclarationSyntax, string>> classDeclarations;
        private CSharpSyntaxNode node;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassWithAttributeInDifferentNamespaceASTTransformer"/> class.
        /// </summary>
        public ScriptNamespaceBasedASTTransformer()
            : base(string.Empty, ScriptNamespaceAttributeDecoration.ScriptNamespaceFullName)
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

                    var helper = new AttributeLists(classNode);
                    return RetrieveScriptNamespaceAttribute(helper) != null;
                },
                delegate (SyntaxNode astNode)
                {
                    var classNode = astNode as ClassDeclarationSyntax;
                    var helper = RetrieveScriptNamespaceAttribute(new AttributeLists(classNode));

                    var couple = new KeyValuePair<ClassDeclarationSyntax, string>(
                        astNode as ClassDeclarationSyntax, 
                        helper.OverridenNamespace);

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
        
        private static ScriptNamespaceAttributeDecoration RetrieveScriptNamespaceAttribute(AttributeLists helper)
        {
            foreach (var attribute in helper.Attributes)
            {
                if (ScriptNamespaceAttributeDecoration.IsScriptNamespaceAttributeDecoration(attribute))
                {
                    return new ScriptNamespaceAttributeDecoration(attribute);
                }
            }

            return null;
        }
    }
}
