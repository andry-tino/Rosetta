/// <summary>
/// ASTBuilder.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection
{
    using System;
    using System.Collections.Generic;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Reflection.Factories;
    using Rosetta.Reflection.Helpers;
    using Rosetta.Reflection.Proxies;

    /// <summary>
    /// Builds an AST from an assembly.
    /// This class will return also the semantic model associated to the generated tree connected to the assembly the tree was generated from.
    /// </summary>
    /// <remarks>
    /// Limitations:
    /// - Supports only classes, interfaces, enums and structs.
    /// - Does not support nested types. Only types in namespaces are supported.
    /// </remarks>
    public partial class ASTBuilder : IASTBuilder
    {
        private readonly IAssemblyProxy assembly;

        // Cached quantities
        private ASTInfo astInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ASTBuilder"/> class.
        /// This class builds a plain tree from all the types in the assembly.
        /// </summary>
        /// <param name="assembly">The path to the assembly.</param>
        public ASTBuilder(IAssemblyProxy assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            this.assembly = assembly;
        }

        /// <summary>
        /// Builds the AST from the assembly.
        /// </summary>
        /// <returns>A <see cref="SyntaxTree"/> mapping the types in the assembly.</returns>
        public ASTInfo Build()
        {
            if (this.astInfo == null)
            {
                this.astInfo = this.BuildASTInfo();
            }

            return this.astInfo;
        }

        private ASTInfo BuildASTInfo()
        {
            IEnumerable<ITypeInfoProxy> types = null;
            try
            {
                types = this.assembly.DefinedTypes;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while trying to load defined types", ex);
            }

            var nodes = new List<MemberDeclarationSyntax>();
            var numberOfClasses = 0;
            var numberOfInterfaces = 0;
            var numberOfEnums = 0;
            var numberOfStructs = 0;

            foreach (var type in types)
            {
                if (type.IsClass) { nodes.Add(this.BuildClassNode(type)); numberOfClasses++; continue; }

                if (type.IsValueType) { nodes.Add(this.BuildStructNode(type)); numberOfStructs++; continue; }

                if (type.IsInterface) { nodes.Add(this.BuildInterfaceNode(type)); numberOfInterfaces++; continue; }

                if (type.IsEnum) { nodes.Add(this.BuildEnumNode(type)); numberOfEnums++; continue; }
            }

            // Use collected nodes to build a tree containing them all in the root.
            var compilationUnit = SyntaxFactory.CompilationUnit().AddMembers(nodes.ToArray());
            var tree = compilationUnit.SyntaxTree;

            return new ASTInfo()
            {
                Tree = tree,
                SemanticModel = this.RetrieveSemanticModel(tree),
                ClassCount = numberOfStructs,
                InterfaceCount = numberOfInterfaces,
                EnumCount = numberOfEnums,
                StructCount = numberOfStructs
            };
        }

        private MemberDeclarationSyntax BuildClassNode(ITypeInfoProxy type)
        {
            return this.BuildNode(type, this.BuildClassNodeCore(type));
        }

        private MemberDeclarationSyntax BuildStructNode(ITypeInfoProxy type)
        {
            return this.BuildNode(type, this.BuildInterfaceNodeCore(type));
        }

        private MemberDeclarationSyntax BuildEnumNode(ITypeInfoProxy type)
        {
            // TODO
            return this.BuildNode(type, SyntaxFactory.EnumDeclaration);
        }

        private MemberDeclarationSyntax BuildInterfaceNode(ITypeInfoProxy type)
        {
            // TODO
            return this.BuildNode(type, SyntaxFactory.InterfaceDeclaration);
        }

        private SemanticModel RetrieveSemanticModel(SyntaxTree tree)
        {
            var mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);

            var compilation = CSharpCompilation.Create("RosettaReflectionCompilation",
                syntaxTrees: new[] { tree },
                references: new[] { mscorlib });

            return compilation.GetSemanticModel(tree);
        }

        private MemberDeclarationSyntax BuildClassNodeCore(ITypeInfoProxy type)
        {
            return new ClassDeclarationSyntaxFactory(type).Create() as MemberDeclarationSyntax;
        }

        private MemberDeclarationSyntax BuildInterfaceNodeCore(ITypeInfoProxy type)
        {
            return new InterfaceDeclarationSyntaxFactory(type).Create() as MemberDeclarationSyntax;
        }

        // TODO: Remove this once all core methods have been built
        private MemberDeclarationSyntax BuildNode(ITypeInfoProxy type, RoslynNodeFactory factory)
        {
            var helper = this.CreateNamespaceHelper(type);

            MemberDeclarationSyntax node = null;

            var namespaceNode = helper.Exists ? SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(helper.FullName)) : null;
            var typeNode = factory(type.Name); // Creates the node to put in the namespace (if any)
            
            if (namespaceNode != null)
            {
                namespaceNode = namespaceNode.AddMembers(typeNode);
                node = namespaceNode;
            }
            else
            {
                node = typeNode;
            }

            return node;
        }

        /// <summary>
        /// Places the built node in the right namespace.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="typeNode"></param>
        /// <returns></returns>
        private MemberDeclarationSyntax BuildNode(ITypeInfoProxy type, MemberDeclarationSyntax typeNode)
        {
            var helper = this.CreateNamespaceHelper(type);
            var namespaceNode = helper.Exists ? SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(helper.FullName)) : null;

            MemberDeclarationSyntax node = null;

            if (namespaceNode != null)
            {
                namespaceNode = namespaceNode.AddMembers(typeNode);
                node = namespaceNode;
            }
            else
            {
                node = typeNode;
            }

            return node;
        }

        protected virtual Namespace CreateNamespaceHelper(ITypeInfoProxy type)
        {
            return new Namespace(type);
        }

        #region Types

        // TODO: Remove it once we get rid of old implementation of BuildNode
        protected delegate MemberDeclarationSyntax RoslynNodeFactory(string typeName);

        #endregion
    }
}
