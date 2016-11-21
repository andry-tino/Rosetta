/// <summary>
/// ScriptNamespaceBasedASTTransformer.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST.Transformers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST.Helpers;
    using Rosetta.AST.Transformers;
    using Rosetta.AST.Utilities;
    using Rosetta.ScriptSharp.Definition.AST.Helpers;

    // TODO: Test cases where namespaces are nested with classes inside.

    /// <summary>
    /// Base class for rearrangement of namespaces for classes basing on ScriptSharp's <code>ScriptNamespace</code> attribute.
    /// </summary>
    public class ScriptNamespaceBasedASTTransformer : ClassWithAttributeInDifferentNamespaceASTTransformer
    {
        // Temporary quantities
        private List<TransformationInfo> transformationInfos;
        private List<SyntaxNode> removableNamespaces;
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

            // 1. Initialize
            this.Initialize(node);

            // 2.1 Analyze
            this.RetrieveOverridenNamespaceNames();
            // 2.2 Rearrange
            this.ProcessOverridenNamespaceNames();
            // 2.3 Tidy up
            this.CleanUpCompilationUnit();

            node = this.newNode;

            // 3 Clean resources
            this.CleanUp();
        }

        private void RetrieveOverridenNamespaceNames()
        {
            new MultiPurposeASTWalker(this.node,
                delegate (SyntaxNode astNode)
                {
                    var typeDeclarationNode = astNode as TypeDeclarationSyntax;

                    // Recognizing only classes and interfaces
                    var classNode = astNode as ClassDeclarationSyntax;
                    var interfaceNode = astNode as InterfaceDeclarationSyntax;
                    if (classNode == null && interfaceNode == null)
                    {
                        return false;
                    }

                    var helper = new AttributeLists(typeDeclarationNode);
                    return RetrieveScriptNamespaceAttribute(helper) != null;
                },
                delegate (SyntaxNode astNode)
                {
                    var typeDeclarationNode = astNode as TypeDeclarationSyntax;
                    var scriptNamespaceAttributeHelper = RetrieveScriptNamespaceAttribute(new AttributeLists(typeDeclarationNode));

                    AttributeListSyntax scriptNamespaceAttributeListSyntax = scriptNamespaceAttributeHelper.AttributeDecoration.AttributeList; // The list where ScriptNamespace belongs to
                    AttributeSyntax scriptNamespaceAttributeSyntax = scriptNamespaceAttributeHelper.AttributeDecoration.AttributeNode; // The ScriptNamespace attribute

                    // We create a new class node with the attribute removed
                    SeparatedSyntaxList<AttributeSyntax> newAttributeListSyntaxAttributes = scriptNamespaceAttributeListSyntax.Attributes.Remove(scriptNamespaceAttributeSyntax);
                    AttributeListSyntax newAttributeListSyntax = SyntaxFactory.AttributeList(newAttributeListSyntaxAttributes);

                    SyntaxList<AttributeListSyntax> newAttributeLists = typeDeclarationNode.AttributeLists.Remove(scriptNamespaceAttributeListSyntax);
                    newAttributeLists = newAttributeLists.Add(newAttributeListSyntax);

                    TypeDeclarationSyntax newTypeDeclarationSyntax = typeDeclarationNode.RemoveNode(scriptNamespaceAttributeListSyntax, SyntaxRemoveOptions.KeepNoTrivia);

                    if (newTypeDeclarationSyntax as ClassDeclarationSyntax != null)
                    {
                        (newTypeDeclarationSyntax as ClassDeclarationSyntax).WithAttributeLists(newAttributeLists);
                    }
                    else if (newTypeDeclarationSyntax as InterfaceDeclarationSyntax != null)
                    {
                        (newTypeDeclarationSyntax as InterfaceDeclarationSyntax).WithAttributeLists(newAttributeLists);
                    }
                    else
                    {
                        throw new InvalidOperationException("Not recognized type at rearrangement phase. Expecting classes and interfaces only!");
                    }

                    // Asserting that the overriden namespace has a proper value
                    if (string.IsNullOrEmpty(scriptNamespaceAttributeHelper.OverridenNamespace) || 
                        string.IsNullOrWhiteSpace(scriptNamespaceAttributeHelper.OverridenNamespace))
                    {
                        throw new InvalidOperationException("The ScriptNamespace attribute contains an overriden namespace value which cannot be accepted!");
                    }
                    
                    var info = new TransformationInfo()
                    {
                        OriginalNode = typeDeclarationNode,
                        TransformedNode = newTypeDeclarationSyntax,
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
            var removableNodes = new List<TypeDeclarationSyntax>();
            foreach (var info in this.transformationInfos)
            {
                removableNodes.Add(info.OriginalNode);
            }

            // Removing the classes in the array
            // These classes will have another namespace assigned, this will work in case the program defines a namespace or not
            newNode = newNode.RemoveNodes(removableNodes, SyntaxRemoveOptions.KeepNoTrivia);

            // Adding classes in new namespaces
            foreach (var info in this.transformationInfos)
            {
                var namespaceSyntax = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName(info.OverridenNamespace));
                namespaceSyntax = namespaceSyntax.AddMembers(info.TransformedNode);

                newNode = newNode.AddMembers(namespaceSyntax);
            }

            this.newNode = newNode;
        }

        private void CleanUpCompilationUnit()
        {
            this.RetrieveEmptyNamespaces();
            this.RemoveEmptyNamespaces();
        }

        private void RetrieveEmptyNamespaces()
        {
            new MultiPurposeASTWalker(this.newNode,
                astNode => astNode as NamespaceDeclarationSyntax != null,
                delegate (SyntaxNode astNode)
                {
                    var namespaceNode = astNode as NamespaceDeclarationSyntax;
                    var helper = new NamespaceDeclaration(namespaceNode);

                    if (helper.Types.Count() == 0)
                    {
                        this.removableNamespaces.Add(namespaceNode);
                    }
                })
                .Start();
        }

        private void RemoveEmptyNamespaces()
        {
            CompilationUnitSyntax newNode = this.newNode;

            foreach (var namespaceNode in this.removableNamespaces)
            {
                newNode = newNode.RemoveNode(namespaceNode, SyntaxRemoveOptions.KeepNoTrivia);
            }

            this.newNode = newNode;
        }

        private void Initialize(CSharpSyntaxNode node)
        {
            this.node = node as CompilationUnitSyntax;
            this.transformationInfos = new List<TransformationInfo>();
            this.removableNamespaces = new List<SyntaxNode>();
        }

        private void CleanUp()
        {
            this.node = null;

            this.transformationInfos.Clear();
            this.transformationInfos = null;

            this.removableNamespaces.Clear();
            this.removableNamespaces = null;
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
            public TypeDeclarationSyntax OriginalNode { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public TypeDeclarationSyntax TransformedNode { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string OverridenNamespace { get; set; }
        }

        #endregion
    }
}
