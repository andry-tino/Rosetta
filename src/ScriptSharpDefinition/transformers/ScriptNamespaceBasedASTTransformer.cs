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
        // Temporary quantities
        private List<TransformationInfo> transformationInfos;
        private CompilationUnitSyntax node;
        private CompilationUnitSyntax newNode;

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

            this.RetrieveOverridenNamespaceNames();
            this.ProcessOverridenNamespaceNames();

            node = this.newNode;

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
                    var scriptNamespaceAttributeHelper = RetrieveScriptNamespaceAttribute(new AttributeLists(classNode));

                    AttributeListSyntax scriptNamespaceAttributeListSyntax = scriptNamespaceAttributeHelper.AttributeDecoration.AttributeList; // The list where ScriptNamespace belongs to
                    AttributeSyntax scriptNamespaceAttributeSyntax = scriptNamespaceAttributeHelper.AttributeDecoration.AttributeNode; // The ScriptNamespace attribute

                    // We create a new class node with the attribute removed
                    SeparatedSyntaxList<AttributeSyntax> newAttributeListSyntaxAttributes = scriptNamespaceAttributeListSyntax.Attributes.Remove(scriptNamespaceAttributeSyntax);
                    AttributeListSyntax newAttributeListSyntax = SyntaxFactory.AttributeList(newAttributeListSyntaxAttributes);

                    SyntaxList<AttributeListSyntax> newAttributeLists = classNode.AttributeLists.Remove(scriptNamespaceAttributeListSyntax);
                    newAttributeLists = newAttributeLists.Add(newAttributeListSyntax);

                    ClassDeclarationSyntax newClassSyntax = classNode.RemoveNode(scriptNamespaceAttributeListSyntax, SyntaxRemoveOptions.KeepNoTrivia);
                    newClassSyntax.WithAttributeLists(newAttributeLists);

                    // Asserting that the overriden namespace has a proper value
                    if (string.IsNullOrEmpty(scriptNamespaceAttributeHelper.OverridenNamespace) || 
                        string.IsNullOrWhiteSpace(scriptNamespaceAttributeHelper.OverridenNamespace))
                    {
                        throw new InvalidOperationException("The ScriptNamespace attribute contains an overriden namespace value which is not acceptable!");
                    }
                    
                    var info = new TransformationInfo()
                    {
                        OriginalClassNode = classNode,
                        TransformedClassNode = newClassSyntax,
                        OverridenNamespace = scriptNamespaceAttributeHelper.OverridenNamespace
                    };

                    this.transformationInfos.Add(info);
                })
                .Start();
        }

        private void ProcessOverridenNamespaceNames()
        {
            CompilationUnitSyntax newNode = this.node;

            // Removing classes
            var removableNodes = new List<ClassDeclarationSyntax>();
            foreach (var info in this.transformationInfos)
            {
                removableNodes.Add(info.OriginalClassNode);
            }

            // Removing the classes in the array
            // These classes will have another namespace assigned, this will work in case the program defines a namespace or not
            newNode = newNode.RemoveNodes(removableNodes, SyntaxRemoveOptions.KeepNoTrivia);

            // Adding classes in new namespaces
            foreach (var info in this.transformationInfos)
            {
                var namespaceSyntax = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName(info.OverridenNamespace));
                namespaceSyntax = namespaceSyntax.AddMembers(info.TransformedClassNode);

                newNode = newNode.AddMembers(namespaceSyntax);
            }

            this.newNode = newNode;
        }

        private void Initialize(CSharpSyntaxNode node)
        {
            this.node = node as CompilationUnitSyntax;
            this.transformationInfos = new List<TransformationInfo>();
        }

        private void CleanUp()
        {
            this.node = null;
            this.transformationInfos = null;
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

        #region Types

        /// <summary>
        /// A class for store basic tranformation info.
        /// </summary>
        private sealed class TransformationInfo
        {
            /// <summary>
            /// 
            /// </summary>
            public ClassDeclarationSyntax OriginalClassNode { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public ClassDeclarationSyntax TransformedClassNode { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string OverridenNamespace { get; set; }
        }

        #endregion
    }
}
