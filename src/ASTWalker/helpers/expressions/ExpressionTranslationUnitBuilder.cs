/// <summary>
/// ExpressionTranslationUnitBuilder.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST.Utilities;
    using Rosetta.Translation;

    /// <summary>
    /// Builder responsible for creating the correct <see cref="ITranslationUnit"/> 
    /// from an expression syntax node.
    /// This is the main entry point for whatever AST walker which needs to create an expression.
    /// </summary>
    public sealed class ExpressionTranslationUnitBuilder
    {
        private readonly ExpressionSyntax node;
        private readonly SemanticModel semanticModel;

        // TODO: This class is a factory and should be moved there.

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTranslationUnitBuilder"/> class.
        /// </summary>
        /// <param name="node">The node</param>
        /// <param name="semanticModel">The semantic model</param>
        public ExpressionTranslationUnitBuilder(ExpressionSyntax node, SemanticModel semanticModel = null)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "A node is needed!");
            }

            this.node = node;
            this.semanticModel = semanticModel;
        }

        /// <summary>
        /// Builds the proper <see cref="ITranslationUnit"/> for the specific expression.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public ITranslationUnit Build()
        {
            switch (this.node.Kind())
            {
                // Binary expressions
                case SyntaxKind.AddExpression:
                case SyntaxKind.MultiplyExpression:
                case SyntaxKind.DivideExpression:
                case SyntaxKind.SubtractExpression:
                case SyntaxKind.EqualsExpression:
                case SyntaxKind.NotEqualsExpression:
                    var binaryExpression = this.node as BinaryExpressionSyntax;
                    if (binaryExpression == null)
                    {
                        throw new InvalidCastException("Unable to correctly cast expected binary expression to binary expression!");
                    }
                    return BuildBinaryExpressionTranslationUnit(binaryExpression, this.semanticModel);

                // Unary expressions
                case SyntaxKind.PostIncrementExpression:
                case SyntaxKind.PreIncrementExpression:
                case SyntaxKind.PostDecrementExpression:
                case SyntaxKind.PreDecrementExpression:
                case SyntaxKind.LogicalNotExpression:
                    var prefixUnaryExpression = this.node as PrefixUnaryExpressionSyntax;
                    var postfixUnaryExpression = this.node as PostfixUnaryExpressionSyntax;
                    if (prefixUnaryExpression == null && postfixUnaryExpression == null)
                    {
                        throw new InvalidCastException("Unable to correctly cast expected unary expression to unary expression!");
                    }
                    return prefixUnaryExpression != null ? 
                        BuildUnaryExpressionTranslationUnit(prefixUnaryExpression, this.semanticModel) : 
                        BuildUnaryExpressionTranslationUnit(postfixUnaryExpression, this.semanticModel);

                // Literal expressions
                case SyntaxKind.NumericLiteralExpression:
                case SyntaxKind.StringLiteralExpression:
                case SyntaxKind.NullLiteralExpression:
                case SyntaxKind.CharacterLiteralExpression:
                case SyntaxKind.FalseLiteralExpression:
                case SyntaxKind.TrueLiteralExpression:
                    var literalExpression = this.node as LiteralExpressionSyntax;
                    if (literalExpression == null)
                    {
                        throw new InvalidCastException("Unable to correctly cast expected literal expression to literal expression!");
                    }
                    return BuildLiteralExpressionTranslationUnit(literalExpression, this.semanticModel);

                // Identifiers
                case SyntaxKind.IdentifierName:
                    var identifierNameExpression = this.node as IdentifierNameSyntax;
                    if (identifierNameExpression == null)
                    {
                        throw new InvalidCastException("Unable to correctly cast expected identifier name expression to identifer name expression!");
                    }
                    return BuildIdentifierNameExpressionTranslationUnit(identifierNameExpression, this.semanticModel);

                // Invocation
                case SyntaxKind.InvocationExpression:
                    var invokationExpression = this.node as InvocationExpressionSyntax;
                    if (invokationExpression == null)
                    {
                        throw new InvalidCastException("Unable to correctly cast expected invokation expression to invokation expression!");
                    }
                    return BuildInvokationExpressionTranslationUnit(invokationExpression, this.semanticModel);

                // Object creation (new)
                case SyntaxKind.ObjectCreationExpression:
                    var objectCreationExpression = this.node as ObjectCreationExpressionSyntax;
                    if (objectCreationExpression == null)
                    {
                        throw new InvalidCastException("Unable to correctly cast expected object creation expression to object creation expression!");
                    }
                    return BuildObjectCreationExpressionTranslationUnit(objectCreationExpression, this.semanticModel);

                // Parenthetical
                case SyntaxKind.ParenthesizedExpression:
                    var parenthesizedExpression = this.node as ParenthesizedExpressionSyntax;
                    if (parenthesizedExpression == null)
                    {
                        throw new InvalidCastException("Unable to correctly cast expected parenthesized expression to parenthesized expression!");
                    }
                    return BuildParenthesizedExpressionTranslationUnit(parenthesizedExpression, this.semanticModel);

                // Member access expressions
                case SyntaxKind.SimpleMemberAccessExpression:
                    var memberAccessExpression = this.node as MemberAccessExpressionSyntax;
                    if (memberAccessExpression == null)
                    {
                        throw new InvalidCastException("Unable to correctly cast expected member access expression to member access expression!");
                    }
                    return BuildMemberAccessExpressionTranslationUnit(memberAccessExpression, this.semanticModel);

                // Assignment expressions
                case SyntaxKind.AddAssignmentExpression:
                case SyntaxKind.AndAssignmentExpression:
                case SyntaxKind.DivideAssignmentExpression:
                case SyntaxKind.ExclusiveOrAssignmentExpression:
                case SyntaxKind.LeftShiftAssignmentExpression:
                case SyntaxKind.ModuloAssignmentExpression:
                case SyntaxKind.MultiplyAssignmentExpression:
                case SyntaxKind.OrAssignmentExpression:
                case SyntaxKind.RightShiftAssignmentExpression:
                case SyntaxKind.SubtractAssignmentExpression:
                case SyntaxKind.SimpleAssignmentExpression:
                    var assignmentExpression = this.node as AssignmentExpressionSyntax;
                    if (assignmentExpression == null)
                    {
                        throw new InvalidCastException("Unable to correctly cast expected assignment expression to assignment expression!");
                    }
                    return BuildAssignmentExpressionTranslationUnit(assignmentExpression, this.semanticModel);
            }

            throw new InvalidOperationException(string.Format("Cannot build an expression for node type {0}!", this.node.Kind()));
        }

        #region Builder methods

        // TODO: Each one of this method is basically a factory, we should create those factories and use them here

        private static ITranslationUnit BuildBinaryExpressionTranslationUnit(BinaryExpressionSyntax expression, SemanticModel semanticModel)
        {
            OperatorToken token = OperatorToken.Undefined;

            switch (expression.Kind())
            {
                case SyntaxKind.AddExpression:
                    token = OperatorToken.Addition;
                    break;
                case SyntaxKind.MultiplyExpression:
                    token = OperatorToken.Multiplication;
                    break;
                case SyntaxKind.DivideExpression:
                    token = OperatorToken.Divide;
                    break;
                case SyntaxKind.SubtractExpression:
                    token = OperatorToken.Subtraction;
                    break;
                case SyntaxKind.EqualsExpression:
                    token = OperatorToken.LogicalEquals;
                    break;
                case SyntaxKind.NotEqualsExpression:
                    token = OperatorToken.NotEquals;
                    break;
            }

            if (token == OperatorToken.Undefined)
            {
                throw new InvalidOperationException("Binary operator could not be detected!");
            }

            BinaryExpression binaryExpressionHelper = new BinaryExpression(expression, semanticModel);
            ITranslationUnit leftHandOperand = new ExpressionTranslationUnitBuilder(binaryExpressionHelper.LeftHandOperand, semanticModel).Build();
            ITranslationUnit rightHandOperand = new ExpressionTranslationUnitBuilder(binaryExpressionHelper.RightHandOperand, semanticModel).Build();

            return BinaryExpressionTranslationUnit.Create(leftHandOperand, rightHandOperand, token);
        }

        private static ITranslationUnit BuildUnaryExpressionTranslationUnit(PrefixUnaryExpressionSyntax expression, SemanticModel semanticModel)
        {
            OperatorToken token = OperatorToken.Undefined;

            switch (expression.Kind())
            {
                case SyntaxKind.PreIncrementExpression:
                    token = OperatorToken.Increment;
                    break;
                case SyntaxKind.PreDecrementExpression:
                    token = OperatorToken.Decrement;
                    break;
                case SyntaxKind.LogicalNotExpression:
                    token = OperatorToken.LogicalNot;
                    break;
            }

            if (token == OperatorToken.Undefined)
            {
                throw new InvalidOperationException("Unary operator could not be detected!");
            }

            UnaryExpression unaryExpressionHelper = new UnaryExpression(expression, semanticModel);
            ITranslationUnit operand = new ExpressionTranslationUnitBuilder(unaryExpressionHelper.Operand, semanticModel).Build();

            return UnaryExpressionTranslationUnit.Create(operand, token, UnaryExpressionTranslationUnit.UnaryPosition.Prefix);
        }

        private static ITranslationUnit BuildUnaryExpressionTranslationUnit(PostfixUnaryExpressionSyntax expression, SemanticModel semanticModel)
        {
            OperatorToken token = OperatorToken.Undefined;

            switch (expression.Kind())
            {
                case SyntaxKind.PostIncrementExpression:
                    token = OperatorToken.Increment;
                    break;
                case SyntaxKind.PostDecrementExpression:
                    token = OperatorToken.Decrement;
                    break;
            }

            if (token == OperatorToken.Undefined)
            {
                throw new InvalidOperationException("Unary operator could not be detected!");
            }

            UnaryExpression unaryExpressionHelper = new UnaryExpression(expression, semanticModel);
            ITranslationUnit operand = new ExpressionTranslationUnitBuilder(unaryExpressionHelper.Operand, semanticModel).Build();

            return UnaryExpressionTranslationUnit.Create(operand, token, UnaryExpressionTranslationUnit.UnaryPosition.Postfix);
        }

        private static ITranslationUnit BuildLiteralExpressionTranslationUnit(LiteralExpressionSyntax expression, SemanticModel semanticModel)
        {
            SyntaxToken token = expression.Token;

            switch (token.Kind())
            {
                case SyntaxKind.NumericLiteralToken:
                    return LiteralTranslationUnit<int>.Create((int)token.Value);

                case SyntaxKind.StringLiteralToken:
                    return LiteralTranslationUnit<string>.Create((string)token.Value);

                case SyntaxKind.CharacterLiteralExpression:
                    return null;

                case SyntaxKind.TrueKeyword:
                case SyntaxKind.FalseKeyword:
                    return LiteralTranslationUnit<bool>.Create((bool)token.Value);

                case SyntaxKind.NullKeyword:
                    return LiteralTranslationUnit.Null;
            }

            throw new InvalidOperationException(string.Format("Cannot build a literal expression for token type {0}!", token.Kind()));
        }
        
        private static ITranslationUnit BuildObjectCreationExpressionTranslationUnit(ObjectCreationExpressionSyntax expression, SemanticModel semanticModel)
        {
            ObjectCreationExpression helper = new ObjectCreationExpression(expression, semanticModel);
            
            var translationUnit = ObjectCreationExpressionTranslationUnit.Create(
                TypeIdentifierTranslationUnit.Create(helper.Type.FullName.MapType())); // TODO: Create factory for TypeReference

            foreach (var argument in helper.Arguments)
            {
                var argumentTranslationUnit = new ExpressionTranslationUnitBuilder(argument.Expression, semanticModel).Build();

                translationUnit.AddArgument(argumentTranslationUnit);
            }

            return translationUnit;
        }

        private static ITranslationUnit BuildParenthesizedExpressionTranslationUnit(ParenthesizedExpressionSyntax expression, SemanticModel semanticModel)
        {
            ParenthesizedExpression parenthesizedExpressionHelper = new ParenthesizedExpression(expression, semanticModel);

            return ParenthesizedExpressionTranslationUnit.Create(
                new ExpressionTranslationUnitBuilder(parenthesizedExpressionHelper.Expression, semanticModel).Build());
        }

        private static ITranslationUnit BuildMemberAccessExpressionTranslationUnit(MemberAccessExpressionSyntax expression, SemanticModel semanticModel)
        {
            var thisExpression = expression.Expression as ThisExpressionSyntax;
            var baseExpression = expression.Expression as BaseExpressionSyntax;

            var helper = new MemberAccessExpression(expression, semanticModel);

            if (thisExpression != null)
            {
                return MemberAccessExpressionTranslationUnit.Create(
                    IdentifierTranslationUnit.Create(helper.MemberName),
                    MemberAccessExpressionTranslationUnit.MemberAccessMethod.This);
            }

            if (baseExpression != null)
            {
                return MemberAccessExpressionTranslationUnit.Create(
                    IdentifierTranslationUnit.Create(helper.MemberName),
                    MemberAccessExpressionTranslationUnit.MemberAccessMethod.Base);
            }

            throw new InvalidOperationException("Cannot build a member access expression as it is not a `this` expression, nor a `base` expression!");
        }

        private static ITranslationUnit BuildAssignmentExpressionTranslationUnit(AssignmentExpressionSyntax expression, SemanticModel semanticModel)
        {
            var helper = new AssignmentExpression(expression, semanticModel);

            return AssignmentExpressionTranslationUnit.Create(
                new ExpressionTranslationUnitBuilder(helper.LeftHand, semanticModel).Build(), 
                new ExpressionTranslationUnitBuilder(helper.RightHand, semanticModel).Build(), 
                helper.Operator);
        }

        private static ITranslationUnit BuildInvokationExpressionTranslationUnit(InvocationExpressionSyntax expression, SemanticModel semanticModel)
        {
            var helper = new InvokationExpression(expression, semanticModel);

            var translationUnit =  InvokationExpressionTranslationUnit.Create(
                new ExpressionTranslationUnitBuilder(helper.Expression, semanticModel).Build());

            foreach (var argument in helper.Arguments)
            {
                var argumentTranslationUnit = new ExpressionTranslationUnitBuilder(argument.Expression, semanticModel).Build();

                translationUnit.AddArgument(argumentTranslationUnit);
            }

            return translationUnit;
        }

        private static ITranslationUnit BuildIdentifierNameExpressionTranslationUnit(IdentifierNameSyntax expression, SemanticModel semanticModel)
        {
            var helper = new IdentifierExpression(expression, semanticModel);

            return IdentifierTranslationUnit.Create(helper.Identifier);
        }

        #endregion
    }
}
