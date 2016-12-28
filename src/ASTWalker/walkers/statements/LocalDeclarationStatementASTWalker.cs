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
        /// <param name="node"></param>
        /// <param name="statement"></param>
        /// <param name="semanticModel">The semantic model.</param>
        protected LocalDeclarationStatementASTWalker(CSharpSyntaxNode node, LocalDeclarationStatementTranslationUnit localDeclarationStatement, SemanticModel semanticModel) 
            : base(node, semanticModel)
        {
            var declarationSyntaxNode = node as LocalDeclarationStatementSyntax;
            if (declarationSyntaxNode == null)
            {
                throw new ArgumentException(
                    string.Format("Specified node is not of type {0}",
                    typeof(LocalDeclarationStatementSyntax).Name));
            }

            if (localDeclarationStatement == null)
            {
                throw new ArgumentNullException(nameof(localDeclarationStatement));
            }

            // Node assigned in base, no need to assign it here
            this.statement = localDeclarationStatement;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="LocalDeclarationStatementASTWalker"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public LocalDeclarationStatementASTWalker(LocalDeclarationStatementASTWalker other)
            : base(other)
        {
        }

        /// <summary>
        /// Factory method for class <see cref="LocalDeclarationStatementASTWalker"/>.
        /// </summary>
        /// <param name="node"><see cref="CSharpSyntaxNode"/> Used to initialize the walker.</param>
        /// <param name="semanticModel">The semantic model.</param>
        /// <returns></returns>
        public static LocalDeclarationStatementASTWalker Create(CSharpSyntaxNode node, SemanticModel semanticModel = null)
        {
            // TODO: Use TranslationUnitFactory in order to have AST walkers decoupled from helpers 
            //       via factories (which will be using helpers)

            var variableDeclaration = new VariableDeclaration((node as LocalDeclarationStatementSyntax).Declaration, semanticModel);

            ExpressionSyntax expression = variableDeclaration.Expressions[0]; // This can contain null, so need to act accordingly
            ITranslationUnit expressionTranslationUnit = expression == null 
                ? null 
                : new ExpressionTranslationUnitBuilder(expression, semanticModel).Build();

            var variableDeclarationTranslationUnit = VariableDeclarationTranslationUnit.Create(
                TypeIdentifierTranslationUnit.Create(variableDeclaration.Type.Name),
                IdentifierTranslationUnit.Create(variableDeclaration.Names[0]),
                expressionTranslationUnit);

            var statement = LocalDeclarationStatementTranslationUnit.Create(variableDeclarationTranslationUnit);

            return new LocalDeclarationStatementASTWalker(node, statement, semanticModel);
        }

        protected override bool ShouldWalkInto
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the <see cref="LocalDeclarationStatementTranslationUnit"/> associated to the AST walker.
        /// </summary>
        /// <remarks>
        /// Protected for testability.
        /// </remarks>
        protected LocalDeclarationStatementTranslationUnit Statement
        {
            get { return this.statement as LocalDeclarationStatementTranslationUnit; }
        }
    }
}
