/// <summary>
/// LocalDeclarationStatementASTWalker.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Translation;
    using Rosetta.AST.Helpers;

    /// <summary>
    /// This is a walker for local eclarations.
    /// </summary>
    /// <remarks>
    /// This walker does not actually walks into more nodes. This is a dead-end walker.
    /// </remarks>
    public class LocalDeclarationStatementASTWalker : StatementASTWalker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalDeclarationStatementASTWalker"/> class.
        /// </summary>
        protected LocalDeclarationStatementASTWalker(CSharpSyntaxNode node) : base(node)
        {
            var declarationSyntaxNode = node as LocalDeclarationStatementSyntax;
            if (declarationSyntaxNode == null)
            {
                throw new ArgumentException(
                    string.Format("Specified node is not of type {0}",
                    typeof(LocalDeclarationStatementSyntax).Name));
            }
            
            var variableDeclaration = new VariableDeclaration(declarationSyntaxNode.Declaration);

            ExpressionSyntax expression = variableDeclaration.Expressions[0]; // This can contain null, so need to act accordingly
            ITranslationUnit expressionTranslationUnit = expression == null ? null : new ExpressionTranslationUnitBuilder(expression).Build();

            var variableDeclarationTranslationUnit = VariableDeclarationTranslationUnit.Create(
                IdentifierTranslationUnit.Create(variableDeclaration.Type),
                IdentifierTranslationUnit.Create(variableDeclaration.Names[0]),
                expressionTranslationUnit);

            this.statement = LocalDeclarationStatementTranslationUnit.Create(variableDeclarationTranslationUnit);
        }

        /// <summary>
        /// Factory method for class <see cref="LocalDeclarationStatementASTWalker"/>.
        /// </summary>
        /// <param name="node"><see cref="CSharpSyntaxNode"/> Used to initialize the walker.</param>
        /// <returns></returns>
        public static LocalDeclarationStatementASTWalker Create(CSharpSyntaxNode node)
        {
            return new LocalDeclarationStatementASTWalker(node);
        }

        protected override bool ShouldWalkInto
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the <see cref="ConditionalStatementTranslationUnit"/> associated to the AST walker.
        /// </summary>
        private ConditionalStatementTranslationUnit Statement
        {
            get { return this.statement as ConditionalStatementTranslationUnit; }
        }
    }
}
