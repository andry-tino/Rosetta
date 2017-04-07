/// <summary>
/// ASTBuilder.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Reflection.Helpers;

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
        private readonly Assembly assembly;

        // Cached quantities
        private ASTInfo astInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ASTBuilder"/> class.
        /// This class builds a plain tree from all the types in the assembly.
        /// </summary>
        /// <param name="assembly">The path to the assembly.</param>
        public ASTBuilder(Assembly assembly)
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
            if (this.astInfo != null)
            {
                return this.astInfo;
            }

            var types = this.assembly.DefinedTypes;

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
                CompilationUnit = this.BuildCompilationUnit(tree),
                ClassCount = numberOfStructs,
                InterfaceCount = numberOfInterfaces,
                EnumCount = numberOfEnums,
                StructCount = numberOfStructs
            };
        }

        private MemberDeclarationSyntax BuildClassNode(System.Reflection.TypeInfo type)
        {
            return this.BuildNode(type, SyntaxFactory.ClassDeclaration);
        }

        private MemberDeclarationSyntax BuildStructNode(System.Reflection.TypeInfo type)
        {
            return this.BuildNode(type, SyntaxFactory.StructDeclaration);
        }

        private MemberDeclarationSyntax BuildEnumNode(System.Reflection.TypeInfo type)
        {
            return this.BuildNode(type, SyntaxFactory.EnumDeclaration);
        }

        private MemberDeclarationSyntax BuildInterfaceNode(System.Reflection.TypeInfo type)
        {
            return this.BuildNode(type, SyntaxFactory.InterfaceDeclaration);
        }

        private Compilation BuildCompilationUnit(SyntaxTree tree)
        {
            var references = new List<MetadataReference>();
            
            using (var stream = new MemoryStream())
            {
                var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bf.Serialize(stream, this.assembly);

                var metadataReference = MetadataReference.CreateFromStream(stream);
                references.Add(metadataReference);
            }
            
            return CSharpCompilation.Create("GeneratedCompilation", new[] { tree }, references);
        }

        private MemberDeclarationSyntax BuildNode(System.Reflection.TypeInfo type, RoslynNodeFactory factory)
        {
            var helper = this.CreateNamespaceHelper(type);

            MemberDeclarationSyntax node = null;

            var namespaceNode = helper.Exists ? SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(helper.FullName)) : null;
            var typeNode = factory(type.Name);

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

        protected virtual Namespace CreateNamespaceHelper(System.Reflection.TypeInfo type)
        {
            return new Namespace(type);
        }

        #region Types

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        protected delegate MemberDeclarationSyntax RoslynNodeFactory(string typeName);

        #endregion
    }
}
