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
    /// <remarks>
    /// This transformer acts with some limitations.
    /// - File has one level of namespacing and no more.
    /// </remarks>
    public class ScriptNamespaceBasedASTTransformer : ClassWithAttributeInDifferentNamespaceASTTransformer, IASTTransformer
    {
        // Temporary quantities
        private List<TransformationInfo> transformationInfos;
        private List<SyntaxNode> removableNamespaces;

        private CSharpSyntaxTree tree;                          // Original tree
        private CSharpSyntaxTree newTree;                       // Transformed tree
        private CompilationUnitSyntax node;                     // Original node (from tree)
        private CompilationUnitSyntax newNode;                  // Transformed node
        private CSharpCompilation compilation;                  // Original compilation
        private CSharpCompilation newCompilation;               // New compilation

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
        /// <param name="node">The node which will be impacted by the transformation.</param>
        /// <param name="compilation">The compilation containing the semantic model associated to the node.</param>
        public void Transform(ref CSharpSyntaxTree tree, ref CSharpCompilation compilation)
        {
            if (tree == null)
            {
                throw new ArgumentNullException(nameof(tree), "A tree is needed!");
            }

            if (compilation == null)
            {
                throw new ArgumentNullException(nameof(compilation), "A compilation is needed!");
            }
            
            // 1. Initialize
            this.Initialize(tree, compilation);

            var res = GetTeeSymbols(this.tree, this.compilation.GetSemanticModel(this.tree)).ToArray(); // TBR

            // 2.1. Analyze
            this.RetrieveOverridenNamespaceNames();
            // 2.2. Rearrange
            this.ProcessOverridenNamespaceNames();
            // 2.3. Using directives handling for fixing references inside namespaces
            this.RetrieveAllUsingDirectivesAndCopyAtRootLevel();
            // 2.4. Tidy up
            this.CleanUpCompilationUnit();

            // 3. Update compilation and semantics
            this.UpdateCompilation();

            // 4. Updating references
            compilation = this.newCompilation;
            tree = this.newNode.SyntaxTree as CSharpSyntaxTree;

            //var diagnostics = this.compilation.GetSemanticModel(this.tree).GetDiagnostics(); // TBR, used to check errors
            //var newDiagnostics = this.newCompilation.GetSemanticModel(this.newTree).GetDiagnostics(); // TBR, used to check errors

            // 5. Clean resources
            this.CleanUp();
        }

        /// <summary>
        /// Transforms the tree.
        /// </summary>
        /// <param name="node">The node which will be impacted by the transformation.</param>
        public void Transform(ref CSharpSyntaxTree tree)
        {
            if (tree == null)
            {
                throw new ArgumentNullException(nameof(tree), "A tree is needed!");
            }

            // 1. Initialize
            this.Initialize(tree, compilation);

            // 2.1. Analyze
            this.RetrieveOverridenNamespaceNames();
            // 2.2. Rearrange
            this.ProcessOverridenNamespaceNames();
            // We don't perform using directives rearrangement like in other method as that is meaningful only when having a semantic model
            // 2.3. Tidy up
            this.CleanUpCompilationUnit();

            // 3. Updating references
            tree = this.newNode.SyntaxTree as CSharpSyntaxTree;

            // 4. Clean resources
            this.CleanUp();
        }

        /// <summary>
        /// Does not perform any operations yet on the AST.
        /// </summary>
        private void RetrieveOverridenNamespaceNames()
        {
            new MultiPurposeASTWalker(this.node,
                delegate (SyntaxNode astNode)
                {
                    var typeDeclarationNode = astNode as BaseTypeDeclarationSyntax;

                    // Recognizing only classes, enums and interfaces
                    var classNode = astNode as ClassDeclarationSyntax;
                    var enumNode = astNode as EnumDeclarationSyntax;
                    var interfaceNode = astNode as InterfaceDeclarationSyntax;
                    if (classNode == null && enumNode == null && interfaceNode == null)
                    {
                        return false;
                    }

                    var helper = new AttributeLists(typeDeclarationNode);
                    return RetrieveScriptNamespaceAttribute(helper) != null;
                },
                delegate (SyntaxNode astNode)
                {
                    var typeDeclarationNode = astNode as BaseTypeDeclarationSyntax;
                    var scriptNamespaceAttributeHelper = RetrieveScriptNamespaceAttribute(new AttributeLists(typeDeclarationNode));

                    AttributeListSyntax scriptNamespaceAttributeListSyntax = scriptNamespaceAttributeHelper.AttributeDecoration.AttributeList; // The list where ScriptNamespace belongs to
                    AttributeSyntax scriptNamespaceAttributeSyntax = scriptNamespaceAttributeHelper.AttributeDecoration.AttributeNode; // The ScriptNamespace attribute

                    // We create a new class node with the attribute removed
                    SeparatedSyntaxList<AttributeSyntax> newAttributeListSyntaxAttributes = scriptNamespaceAttributeListSyntax.Attributes.Remove(scriptNamespaceAttributeSyntax);
                    AttributeListSyntax newAttributeListSyntax = SyntaxFactory.AttributeList(newAttributeListSyntaxAttributes);

                    SyntaxList<AttributeListSyntax> newAttributeLists = typeDeclarationNode.AttributeLists.Remove(scriptNamespaceAttributeListSyntax);
                    newAttributeLists = newAttributeLists.Add(newAttributeListSyntax);

                    BaseTypeDeclarationSyntax newTypeDeclarationSyntax = typeDeclarationNode.RemoveNode(scriptNamespaceAttributeListSyntax, SyntaxRemoveOptions.KeepNoTrivia);

                    if (newTypeDeclarationSyntax as ClassDeclarationSyntax != null)
                    {
                        (newTypeDeclarationSyntax as ClassDeclarationSyntax).WithAttributeLists(newAttributeLists);
                    }
                    else if (newTypeDeclarationSyntax as InterfaceDeclarationSyntax != null)
                    {
                        (newTypeDeclarationSyntax as InterfaceDeclarationSyntax).WithAttributeLists(newAttributeLists);
                    }
                    else if (newTypeDeclarationSyntax as EnumDeclarationSyntax != null)
                    {
                        (newTypeDeclarationSyntax as EnumDeclarationSyntax).WithAttributeLists(newAttributeLists);
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
                        OriginalNamespace = this.compilation != null 
                            ? this.compilation.GetSemanticModel(this.tree).GetDeclaredSymbol(typeDeclarationNode).ContainingNamespace.ToString() 
                            : null,
                        OverridenNamespace = scriptNamespaceAttributeHelper.OverridenNamespace
                    };

                    this.transformationInfos.Add(info);
                })
                .Start();
        }

        // TBR, just debugging
        static IEnumerable<KeyValuePair<SyntaxNode, ISymbol>> GetTeeSymbols(SyntaxTree tree, SemanticModel model)
        {
            return tree.GetRoot().
                     DescendantNodesAndSelf()
                     /*.Where(node => node as ClassDeclarationSyntax != null || node as InterfaceDeclarationSyntax != null)*/
                     .Where(node => node.ToString().Contains("INotifyCompletion"))
                     .Select(node => new KeyValuePair<SyntaxNode, ISymbol>(node, model.GetSymbolInfo(node).Symbol ?? model.GetDeclaredSymbol(node)));
        }

        /// <summary>
        /// Causes the structure if the SAT to change. It can cause leftovers (empty structures) to be present.
        /// </summary>
        private void ProcessOverridenNamespaceNames()
        {
            CompilationUnitSyntax newNode = this.node;
            CSharpSyntaxTree newTree = this.tree;

            // Removing classes
            var removableNodes = new List<BaseTypeDeclarationSyntax>();
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

                // TODO: Use the cache here to feed values, the new node should be provided

                newNode = newNode.AddMembers(namespaceSyntax);
            }

            this.newNode = newNode;
        }

        /// <summary>
        /// A rearrangement of the AST and a replacement of classes into new namespaces need to happen, this will mess up the references, 
        /// thus we need to transplant the using directives in the new tree as well.
        /// 
        /// TODO: This approach generates a lot of duplicated using directives. It is not harmful. Make it better.
        /// </summary>
        private void RetrieveAllUsingDirectivesAndCopyAtRootLevel()
        {
            List<UsingDirectiveSyntax> usingDirectives = new List<UsingDirectiveSyntax>();

            // Collecting
            new MultiPurposeASTWalker(this.newNode,
                astNode => astNode as UsingDirectiveSyntax != null,
                delegate (SyntaxNode astNode)
                {
                    var usingNode = astNode as UsingDirectiveSyntax;

                    usingDirectives.Add(usingNode);
                })
                .Start();

            // Copying at root level in the new node
            newNode = newNode.AddUsings(usingDirectives.ToArray());

            // Add using directives for namespaces of types which has been overriden
            var additionalNamespaces = this.GetOriginalNamespaces().Select(ns => SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(ns)).NormalizeWhitespace());
            newNode = newNode.AddUsings(additionalNamespaces.ToArray());
        }

        /// <summary>
        /// Looks into <see cref="transformationInfos"/> and retrieves all <see cref="TransformationInfo.OriginalNamespace"/>.
        /// It filters out duoplicates and returns the collection.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> GetOriginalNamespaces()
        {
            return this.transformationInfos
                .Where(tinfo => tinfo.OriginalNamespace != null)
                .Select(tinfo => tinfo.OriginalNamespace).Distinct();
        }

        /// <summary>
        /// Cleans up empty structures from previous step.
        /// </summary>
        private void CleanUpCompilationUnit()
        {
            this.RetrieveEmptyNamespaces();
            this.RemoveEmptyNamespaces();
        }

        /// <summary>
        /// Updates the semantic model after all the changes.
        /// </summary>
        private void UpdateCompilation()
        {
            this.newTree = this.newNode.SyntaxTree as CSharpSyntaxTree;

            this.newCompilation = this.compilation.ReplaceSyntaxTree(this.tree, this.newTree);
            this.newCompilation.GetSemanticModel(this.newTree); // NOT NEEDED
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

        private void Initialize(CSharpSyntaxTree tree, CSharpCompilation compilation)
        {
            this.tree = tree;
            
            if (this.tree.GetRoot() as CompilationUnitSyntax == null)
            {
                throw new ArgumentException(nameof(node),
                    $"This class expects root nodes of type: {typeof(CompilationUnitSyntax).Name}!");
            }

            this.node = this.tree.GetRoot() as CompilationUnitSyntax;
            this.compilation = compilation;

            this.transformationInfos = new List<TransformationInfo>();
            this.removableNamespaces = new List<SyntaxNode>();
        }

        private void CleanUp()
        {
            this.node = null;
            this.compilation = null;

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
            /// The original node that is to be transformed.
            /// </summary>
            public BaseTypeDeclarationSyntax OriginalNode { get; set; }

            /// <summary>
            /// The transformed node.
            /// </summary>
            public BaseTypeDeclarationSyntax TransformedNode { get; set; }

            /// <summary>
            /// The original namespace.
            /// </summary>
            public string OriginalNamespace { get; set; }

            /// <summary>
            /// The new namespace which will be applied to the node.
            /// </summary>
            public string OverridenNamespace { get; set; }
        }

        #endregion
    }
}
