﻿/// <summary>
/// ClassDefinitionASTWalker.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST;
    using Rosetta.ScriptSharp.Definition.Translation;
    using Rosetta.ScriptSharp.Definition.AST.Factories;

    /// <summary>
    /// Walks a class AST node but only those areas relevant to definition.
    /// 
    /// TODO: Move to a separate project, this is specific to ScriptSharp.
    /// </summary>
    public class ClassDefinitionASTWalker : ClassASTWalker
    {
        protected bool generateTranslationUniOnProtectedMembers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassDefinitionASTWalker"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="classDefinition"></param>
        /// <param name="semanticModel">The semantic model.</param>
        protected ClassDefinitionASTWalker(CSharpSyntaxNode node, ClassDefinitionTranslationUnit classDefinition, SemanticModel semanticModel) 
            : base(node, classDefinition, semanticModel)
        {
            this.generateTranslationUniOnProtectedMembers = true;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="ClassDefinitionASTWalker"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public ClassDefinitionASTWalker(ClassDefinitionASTWalker other) 
            : base(other)
        {
            this.generateTranslationUniOnProtectedMembers = other.generateTranslationUniOnProtectedMembers;
        }

        /// <summary>
        /// Factory method for class <see cref="ClassASTWalker"/>.
        /// </summary>
        /// <param name="node"><see cref="CSharpSyntaxNode"/> Used to initialize the walker.</param>
        /// <param name="context">The walking context.</param>
        /// <param name="semanticModel">The semantic model.</param>
        /// <returns></returns>
        public static new ClassDefinitionASTWalker Create(CSharpSyntaxNode node, ASTWalkerContext context = null, SemanticModel semanticModel = null)
        {
            return new ClassDefinitionASTWalker(
                node,
                new ClassDefinitionTranslationUnitFactory(node, semanticModel).Create() as ClassDefinitionTranslationUnit,
                semanticModel)
            {
                Context = context
            };
        }

        #region CSharpSyntaxWalker overrides

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            var fieldDefinitionTranslationUnit = new FieldDefinitionTranslationUnitFactory(node, this.semanticModel, this.generateTranslationUniOnProtectedMembers).Create();
            if (fieldDefinitionTranslationUnit == null)
            {
                // When the factory returns null, then the member is not exposed, thus we do not generate it in the translation tree
                return;
            }

            this.classDeclaration.AddMemberDeclaration(fieldDefinitionTranslationUnit);

            this.LogVisitFieldDeclaration(node); // Logging

            this.InvokeFieldDeclarationVisited(this, new WalkerEventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            var propertyDefinitionTranslationUnit = new PropertyDefinitionTranslationUnitFactory(node, this.semanticModel, this.generateTranslationUniOnProtectedMembers).Create();
            if (propertyDefinitionTranslationUnit == null)
            {
                // When the factory returns null, then the member is not exposed, thus we do not generate it in the translation tree
                return;
            }

            this.classDeclaration.AddPropertyDeclaration(propertyDefinitionTranslationUnit);

            this.LogVisitPropertyDeclaration(node); // Logging

            this.InvokePropertyDeclarationVisited(this, new WalkerEventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// This will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var methodDefinitionTranslationUnit = new MethodDefinitionTranslationUnitFactory(node, this.semanticModel, this.generateTranslationUniOnProtectedMembers).Create();
            if (methodDefinitionTranslationUnit == null)
            {
                // When the factory returns null, then the member is not exposed, thus we do not generate it in the translation tree
                return;
            }

            this.classDeclaration.AddMethodDeclaration(methodDefinitionTranslationUnit);

            this.LogVisitMethodDeclaration(node); // Logging

            this.InvokeMethodDeclarationVisited(this, new WalkerEventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            var constructorDefinitionTranslationUnit = new ConstructorDefinitionTranslationUnitFactory(node, this.semanticModel, this.generateTranslationUniOnProtectedMembers).Create();
            if (constructorDefinitionTranslationUnit == null)
            {
                // When the factory returns null, then the member is not exposed, thus we do not generate it in the translation tree
                return;
            }

            this.classDeclaration.AddConstructorDeclaration(constructorDefinitionTranslationUnit);

            this.LogVisitConstructorDeclaration(node); // Logging

            this.InvokeConstructorDeclarationVisited(this, new WalkerEventArgs());
        }

        #endregion

        protected override void OnContextChanged()
        {
            this.ApplyContextDependenciesToTranslationUnit();
        }

        private void ApplyContextDependenciesToTranslationUnit()
        {
            if (this.Context == null)
            {
                // When a context is not available, we consider the class defined at root level
                this.ClassDefinition.IsAtRootLevel = true;

                return;
            }

            this.ClassDefinition.IsAtRootLevel = this.Context.Originator.GetType() == typeof(ProgramDefinitionASTWalker);
        }

        private ClassDefinitionTranslationUnit ClassDefinition
        {
            get { return this.classDeclaration as ClassDefinitionTranslationUnit; }
        }
    }
}
