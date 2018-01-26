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

    using Rosetta.Diagnostics.Logging;
    using Rosetta.Reflection.Diagnostics.Logging;
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
    public class ASTBuilder : IASTBuilder
    {
        private readonly IAssemblyProxy assembly;
        private readonly bool retrieveSemanticModel;

        // Cached quantities
        private ASTInfo astInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ASTBuilder"/> class.
        /// This class builds a plain tree from all the types in the assembly.
        /// </summary>
        /// <param name="assembly">The path to the assembly.</param>
        /// <param name="retrieveSemanticModel">
        /// A value indicating whether the Mscorlib assembly should be 
        /// loaded and its semantic model employed.
        /// </param>
        public ASTBuilder(IAssemblyProxy assembly, bool retrieveSemanticModel = true)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            this.assembly = assembly;
            this.retrieveSemanticModel = retrieveSemanticModel;
        }

        /// <summary>
        /// Sets the logger to use.
        /// </summary>
        public ILogger Logger { protected get; set; }

        /// <summary>
        /// Gets a value indicating whether a logger is active or not.
        /// </summary>
        public bool IsLoggingEnabled => this.Logger != null;

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

        protected IAssemblyProxy Assembly => this.assembly;

        private IEnumerable<ITypeInfoProxy> RetrieveTypesFromAssembly()
        {
            IEnumerable<ITypeInfoProxy> types = this.Assembly.DefinedTypes;

            // FIlter out blacklisted types
            types = new DotNetAssemblyTypeInfoBlackList().Filter(types);

            return types;
        }

        private ASTInfo BuildASTInfo()
        {
            IEnumerable<ITypeInfoProxy> types = null;
            try
            {
                types = this.RetrieveTypesFromAssembly();
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

            // Some types, like enum, are identified in metadata as classes inheriting from 
            // System.Enum, so we need to pay attention to that
            foreach (var type in types)
            {
                if (type.IsNativeClassType()) { nodes.Add(this.BuildClassNode(type)); numberOfClasses++; continue; }

                if (type.IsNativeStructType()) { nodes.Add(this.BuildStructNode(type)); numberOfStructs++; continue; }

                if (type.IsInterface) { nodes.Add(this.BuildInterfaceNode(type)); numberOfInterfaces++; continue; }

                if (type.IsNativeEnumType()) { nodes.Add(this.BuildEnumNode(type)); numberOfEnums++; continue; }
            }

            // Use collected nodes to build a tree containing them all in the root.
            var compilationUnit = SyntaxFactory.CompilationUnit().AddMembers(nodes.ToArray());
            var tree = compilationUnit.SyntaxTree;

            return new ASTInfo()
            {
                Tree = tree,
                SemanticModel = this.RetrieveSemanticModel(tree),
                ClassCount = numberOfClasses,
                InterfaceCount = numberOfInterfaces,
                EnumCount = numberOfEnums,
                StructCount = numberOfStructs
            };
        }

        private MemberDeclarationSyntax BuildClassNode(ITypeInfoProxy type)
        {
            if (this.IsLoggingEnabled)
            {
                new TypeInfoLogger(type, this.Logger).LogSyntaxNodeCreation("Class");
            }

            return this.BuildNode(type, this.BuildClassNodeCore(type));
        }

        private MemberDeclarationSyntax BuildStructNode(ITypeInfoProxy type)
        {
            if (this.IsLoggingEnabled)
            {
                new TypeInfoLogger(type, this.Logger).LogSyntaxNodeCreation("Struct");
            }

            // TODO
            return this.BuildNode(type, SyntaxFactory.StructDeclaration);
        }

        private MemberDeclarationSyntax BuildEnumNode(ITypeInfoProxy type)
        {
            if (this.IsLoggingEnabled)
            {
                new TypeInfoLogger(type, this.Logger).LogSyntaxNodeCreation("Enum");
            }

            return this.BuildNode(type, this.BuildEnumNodeCore(type));
        }

        private MemberDeclarationSyntax BuildInterfaceNode(ITypeInfoProxy type)
        {
            if (this.IsLoggingEnabled)
            {
                new TypeInfoLogger(type, this.Logger).LogSyntaxNodeCreation("Interface");
            }

            return this.BuildNode(type, this.BuildInterfaceNodeCore(type));
        }

        private SemanticModel RetrieveSemanticModel(SyntaxTree tree)
        {
            if (!this.retrieveSemanticModel)
            {
                return null;
            }

            PortableExecutableReference mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);

            var compilation = CSharpCompilation.Create("RosettaReflectionCompilation",
                syntaxTrees: new[] { tree },
                references: new[] { mscorlib });

            return compilation.GetSemanticModel(tree);
        }

        private MemberDeclarationSyntax BuildClassNodeCore(ITypeInfoProxy type)
        {
            return this.CreateClassDeclarationSyntaxFactory(type).Create() as MemberDeclarationSyntax;
        }

        private MemberDeclarationSyntax BuildInterfaceNodeCore(ITypeInfoProxy type)
        {
            return this.CreateInterfaceDeclarationSyntaxFactory(type).Create() as MemberDeclarationSyntax;
        }

        private MemberDeclarationSyntax BuildEnumNodeCore(ITypeInfoProxy type)
        {
            return this.CreateEnumDeclarationSyntaxFactory(type).Create() as MemberDeclarationSyntax;
        }

        // TODO: Remove this once all core methods have been built. 
        //       This thing only generates the Roslyn node without any further processing
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

        protected virtual ISyntaxFactory CreateClassDeclarationSyntaxFactory(ITypeInfoProxy typeInfo) 
            => new ClassDeclarationSyntaxFactory(typeInfo);

        protected virtual ISyntaxFactory CreateInterfaceDeclarationSyntaxFactory(ITypeInfoProxy typeInfo)
            => new InterfaceDeclarationSyntaxFactory(typeInfo);

        protected virtual ISyntaxFactory CreateEnumDeclarationSyntaxFactory(ITypeInfoProxy typeInfo)
            => new EnumDeclarationSyntaxFactory(typeInfo);

        #region Types

        // TODO: Remove it once we get rid of old implementation of BuildNode
        protected delegate MemberDeclarationSyntax RoslynNodeFactory(string typeName);

        #endregion
    }
}
