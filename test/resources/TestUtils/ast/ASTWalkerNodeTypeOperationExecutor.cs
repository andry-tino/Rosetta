/// <summary>
/// ASTWalkerNodeTypeOperationExecutor.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Tests.Utils
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Locates nodes in an AST.
    /// </summary>
    public class ASTWalkerNodeTypeOperationExecutor : CSharpSyntaxWalker
    {
        private readonly Type type;
        private readonly Action<SyntaxNode> operation;

        /// <summary>
        /// This variable if <code>true</code> indicates that we should navigate the whole tree. 
        /// If <code>false</code> it indicates that we should resurse ONLY for type <see cref="type"/>.
        /// </summary>
        private readonly bool recurse;

        /// <summary>
        /// Initializes a new instance of the <see cref="ASTWalkerNodeTypeOperationExecutor"/> class.
        /// </summary>
        /// <param name="node">The node to start the search from.</param>
        /// <param name="type">The type to look for.</param>
        /// <param name="operation">The operation to execute for each found node.</param>
        /// <param name="recurse">
        /// If <c>true</c>, the search process will proceed recursively inside nodes, otherwise only 
        /// the nodes of type <see cref="type"/> will be inspected.
        /// </param>
        /// <remarks>
        /// The walker will walk through all nodes as depth level.
        /// </remarks>
        public ASTWalkerNodeTypeOperationExecutor(SyntaxNode node, Type type, Action<SyntaxNode> operation, bool recurse = true) 
            : base(SyntaxWalkerDepth.Node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            this.Root = node;
            this.type = type;
            this.operation = operation;
            this.recurse = recurse;
        }

        /// <summary>
        /// Immutable object.
        /// </summary>
        public SyntaxNode Root { get; private set; }

        /// <summary>
        /// Starts the visiting process
        /// </summary>
        public void Start()
        {
            this.Visit(this.Root);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAccessorDeclaration(AccessorDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(AccessorDeclarationSyntax))) base.VisitAccessorDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAccessorList(AccessorListSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(AccessorListSyntax))) base.VisitAccessorList(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAliasQualifiedName(AliasQualifiedNameSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(AliasQualifiedNameSyntax))) base.VisitAliasQualifiedName(node);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAnonymousMethodExpression(AnonymousMethodExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(AnonymousMethodExpressionSyntax))) base.VisitAnonymousMethodExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAnonymousObjectCreationExpression(AnonymousObjectCreationExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(AnonymousObjectCreationExpressionSyntax))) base.VisitAnonymousObjectCreationExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAnonymousObjectMemberDeclarator(AnonymousObjectMemberDeclaratorSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(AnonymousObjectMemberDeclaratorSyntax))) base.VisitAnonymousObjectMemberDeclarator(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitArgument(ArgumentSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ArgumentSyntax))) base.VisitArgument(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitArgumentList(ArgumentListSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ArgumentListSyntax))) base.VisitArgumentList(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitArrayCreationExpression(ArrayCreationExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ArrayCreationExpressionSyntax))) base.VisitArrayCreationExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitArrayRankSpecifier(ArrayRankSpecifierSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ArrayRankSpecifierSyntax))) base.VisitArrayRankSpecifier(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitArrayType(ArrayTypeSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ArrayTypeSyntax))) base.VisitArrayType(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitArrowExpressionClause(ArrowExpressionClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ArrowExpressionClauseSyntax))) base.VisitArrowExpressionClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAssignmentExpression(AssignmentExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(AssignmentExpressionSyntax))) base.VisitAssignmentExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAttribute(AttributeSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(AttributeSyntax))) base.VisitAttribute(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAttributeArgument(AttributeArgumentSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(AttributeArgumentSyntax))) base.VisitAttributeArgument(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAttributeArgumentList(AttributeArgumentListSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(AttributeArgumentListSyntax))) base.VisitAttributeArgumentList(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAttributeList(AttributeListSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(AttributeListSyntax))) base.VisitAttributeList(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAttributeTargetSpecifier(AttributeTargetSpecifierSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(AttributeTargetSpecifierSyntax))) base.VisitAttributeTargetSpecifier(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAwaitExpression(AwaitExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(AwaitExpressionSyntax))) base.VisitAwaitExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitBadDirectiveTrivia(BadDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(BadDirectiveTriviaSyntax))) base.VisitBadDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitBaseExpression(BaseExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(BaseExpressionSyntax))) base.VisitBaseExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitBaseList(BaseListSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(BaseListSyntax))) base.VisitBaseList(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitBinaryExpression(BinaryExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(BinaryExpressionSyntax))) base.VisitBinaryExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitBlock(BlockSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(BlockSyntax))) base.VisitBlock(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitBracketedArgumentList(BracketedArgumentListSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(BracketedArgumentListSyntax))) base.VisitBracketedArgumentList(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitBracketedParameterList(BracketedParameterListSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(BracketedParameterListSyntax))) base.VisitBracketedParameterList(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitBreakStatement(BreakStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(BreakStatementSyntax))) base.VisitBreakStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCaseSwitchLabel(CaseSwitchLabelSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(CaseSwitchLabelSyntax))) base.VisitCaseSwitchLabel(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCastExpression(CastExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(CastExpressionSyntax))) base.VisitCastExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCatchClause(CatchClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(CatchClauseSyntax))) base.VisitCatchClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCatchDeclaration(CatchDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(CatchDeclarationSyntax))) base.VisitCatchDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCatchFilterClause(CatchFilterClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(CatchFilterClauseSyntax))) base.VisitCatchFilterClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCheckedExpression(CheckedExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(CheckedExpressionSyntax))) base.VisitCheckedExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCheckedStatement(CheckedStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(CheckedStatementSyntax))) base.VisitCheckedStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ClassDeclarationSyntax))) base.VisitClassDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitClassOrStructConstraint(ClassOrStructConstraintSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ClassOrStructConstraintSyntax))) base.VisitClassOrStructConstraint(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCompilationUnit(CompilationUnitSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(CompilationUnitSyntax))) base.VisitCompilationUnit(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitConditionalAccessExpression(ConditionalAccessExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ConditionalAccessExpressionSyntax))) base.VisitConditionalAccessExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitConditionalExpression(ConditionalExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ConditionalExpressionSyntax))) base.VisitConditionalExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitConstructorConstraint(ConstructorConstraintSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ConstructorConstraintSyntax))) base.VisitConstructorConstraint(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ConstructorDeclarationSyntax))) base.VisitConstructorDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitConstructorInitializer(ConstructorInitializerSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ConstructorInitializerSyntax))) base.VisitConstructorInitializer(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitContinueStatement(ContinueStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ContinueStatementSyntax))) base.VisitContinueStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitConversionOperatorDeclaration(ConversionOperatorDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ConversionOperatorDeclarationSyntax))) base.VisitConversionOperatorDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitConversionOperatorMemberCref(ConversionOperatorMemberCrefSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ConversionOperatorMemberCrefSyntax))) base.VisitConversionOperatorMemberCref(node);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCrefBracketedParameterList(CrefBracketedParameterListSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(CrefBracketedParameterListSyntax))) base.VisitCrefBracketedParameterList(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCrefParameter(CrefParameterSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(CrefParameterSyntax))) base.VisitCrefParameter(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCrefParameterList(CrefParameterListSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(CrefParameterListSyntax))) base.VisitCrefParameterList(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitDefaultExpression(DefaultExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(DefaultExpressionSyntax))) base.VisitDefaultExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitDefaultSwitchLabel(DefaultSwitchLabelSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(DefaultSwitchLabelSyntax))) base.VisitDefaultSwitchLabel(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitDefineDirectiveTrivia(DefineDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(DefineDirectiveTriviaSyntax))) base.VisitDefineDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitDelegateDeclaration(DelegateDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(DelegateDeclarationSyntax))) base.VisitDelegateDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitDestructorDeclaration(DestructorDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(DestructorDeclarationSyntax))) base.VisitDestructorDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitDocumentationCommentTrivia(DocumentationCommentTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(DocumentationCommentTriviaSyntax))) base.VisitDocumentationCommentTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitDoStatement(DoStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(DoStatementSyntax))) base.VisitDoStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitElementAccessExpression(ElementAccessExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ElementAccessExpressionSyntax))) base.VisitElementAccessExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitElementBindingExpression(ElementBindingExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ElementBindingExpressionSyntax))) base.VisitElementBindingExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitElifDirectiveTrivia(ElifDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ElifDirectiveTriviaSyntax))) base.VisitElifDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitElseClause(ElseClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ElseClauseSyntax))) base.VisitElseClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitElseDirectiveTrivia(ElseDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ElseDirectiveTriviaSyntax))) base.VisitElseDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitEmptyStatement(EmptyStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(EmptyStatementSyntax))) base.VisitEmptyStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitEndIfDirectiveTrivia(EndIfDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(EndIfDirectiveTriviaSyntax))) base.VisitEndIfDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitEndRegionDirectiveTrivia(EndRegionDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(EndRegionDirectiveTriviaSyntax))) base.VisitEndRegionDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitEnumDeclaration(EnumDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(EnumDeclarationSyntax))) base.VisitEnumDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitEnumMemberDeclaration(EnumMemberDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(EnumMemberDeclarationSyntax))) base.VisitEnumMemberDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitEqualsValueClause(EqualsValueClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(EqualsValueClauseSyntax))) base.VisitEqualsValueClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitErrorDirectiveTrivia(ErrorDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ErrorDirectiveTriviaSyntax))) base.VisitErrorDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitEventDeclaration(EventDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(EventDeclarationSyntax))) base.VisitEventDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitEventFieldDeclaration(EventFieldDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(EventFieldDeclarationSyntax))) base.VisitEventFieldDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitExplicitInterfaceSpecifier(ExplicitInterfaceSpecifierSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ExplicitInterfaceSpecifierSyntax))) base.VisitExplicitInterfaceSpecifier(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitExpressionStatement(ExpressionStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ExpressionStatementSyntax))) base.VisitExpressionStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitExternAliasDirective(ExternAliasDirectiveSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ExternAliasDirectiveSyntax))) base.VisitExternAliasDirective(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(FieldDeclarationSyntax))) base.VisitFieldDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitFinallyClause(FinallyClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(FinallyClauseSyntax))) base.VisitFinallyClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitFixedStatement(FixedStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(FixedStatementSyntax))) base.VisitFixedStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitForEachStatement(ForEachStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ForEachStatementSyntax))) base.VisitForEachStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitForStatement(ForStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ForStatementSyntax))) base.VisitForStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitFromClause(FromClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(FromClauseSyntax))) base.VisitFromClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitGenericName(GenericNameSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(GenericNameSyntax))) base.VisitGenericName(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitGlobalStatement(GlobalStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(GlobalStatementSyntax))) base.VisitGlobalStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitGotoStatement(GotoStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(GotoStatementSyntax))) base.VisitGotoStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitGroupClause(GroupClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(GroupClauseSyntax))) base.VisitGroupClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitIdentifierName(IdentifierNameSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(IdentifierNameSyntax))) base.VisitIdentifierName(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitIfDirectiveTrivia(IfDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(IfDirectiveTriviaSyntax))) base.VisitIfDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitIfStatement(IfStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(IfStatementSyntax))) base.VisitIfStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitImplicitArrayCreationExpression(ImplicitArrayCreationExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ImplicitArrayCreationExpressionSyntax))) base.VisitImplicitArrayCreationExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitImplicitElementAccess(ImplicitElementAccessSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ImplicitElementAccessSyntax))) base.VisitImplicitElementAccess(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitIncompleteMember(IncompleteMemberSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(IncompleteMemberSyntax))) base.VisitIncompleteMember(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitIndexerDeclaration(IndexerDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(IndexerDeclarationSyntax))) base.VisitIndexerDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitIndexerMemberCref(IndexerMemberCrefSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(IndexerMemberCrefSyntax))) base.VisitIndexerMemberCref(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitInitializerExpression(InitializerExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(InitializerExpressionSyntax))) base.VisitInitializerExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(InterfaceDeclarationSyntax))) base.VisitInterfaceDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitInterpolatedStringExpression(InterpolatedStringExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(InterpolatedStringExpressionSyntax))) base.VisitInterpolatedStringExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitInterpolatedStringText(InterpolatedStringTextSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(InterpolatedStringTextSyntax))) base.VisitInterpolatedStringText(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitInterpolation(InterpolationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(InterpolationSyntax))) base.VisitInterpolation(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitInterpolationAlignmentClause(InterpolationAlignmentClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(InterpolationAlignmentClauseSyntax))) base.VisitInterpolationAlignmentClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitInterpolationFormatClause(InterpolationFormatClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(InterpolationFormatClauseSyntax))) base.VisitInterpolationFormatClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(InvocationExpressionSyntax))) base.VisitInvocationExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitJoinClause(JoinClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(JoinClauseSyntax))) base.VisitJoinClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitJoinIntoClause(JoinIntoClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(JoinIntoClauseSyntax))) base.VisitJoinIntoClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitLabeledStatement(LabeledStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(LabeledStatementSyntax))) base.VisitLabeledStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitLetClause(LetClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(LetClauseSyntax))) base.VisitLetClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitLineDirectiveTrivia(LineDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(LineDirectiveTriviaSyntax))) base.VisitLineDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitLiteralExpression(LiteralExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(LiteralExpressionSyntax))) base.VisitLiteralExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(LocalDeclarationStatementSyntax))) base.VisitLocalDeclarationStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitLockStatement(LockStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(LockStatementSyntax))) base.VisitLockStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitMakeRefExpression(MakeRefExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(MakeRefExpressionSyntax))) base.VisitMakeRefExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(MemberAccessExpressionSyntax))) base.VisitMemberAccessExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitMemberBindingExpression(MemberBindingExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(MemberBindingExpressionSyntax))) base.VisitMemberBindingExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(MethodDeclarationSyntax))) base.VisitMethodDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitNameColon(NameColonSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(NameColonSyntax))) base.VisitNameColon(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitNameEquals(NameEqualsSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(NameEqualsSyntax))) base.VisitNameEquals(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitNameMemberCref(NameMemberCrefSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(NameMemberCrefSyntax))) base.VisitNameMemberCref(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(NamespaceDeclarationSyntax))) base.VisitNamespaceDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitNullableType(NullableTypeSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(NullableTypeSyntax))) base.VisitNullableType(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ObjectCreationExpressionSyntax))) base.VisitObjectCreationExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitOmittedArraySizeExpression(OmittedArraySizeExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(OmittedArraySizeExpressionSyntax))) base.VisitOmittedArraySizeExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitOmittedTypeArgument(OmittedTypeArgumentSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(OmittedTypeArgumentSyntax))) base.VisitOmittedTypeArgument(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitOperatorDeclaration(OperatorDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(OperatorDeclarationSyntax))) base.VisitOperatorDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitOperatorMemberCref(OperatorMemberCrefSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(OperatorMemberCrefSyntax))) base.VisitOperatorMemberCref(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitOrderByClause(OrderByClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(OrderByClauseSyntax))) base.VisitOrderByClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitOrdering(OrderingSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(OrderingSyntax))) base.VisitOrdering(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitParameter(ParameterSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ParameterSyntax))) base.VisitParameter(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitParameterList(ParameterListSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ParameterListSyntax))) base.VisitParameterList(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ParenthesizedExpressionSyntax))) base.VisitParenthesizedExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitParenthesizedLambdaExpression(ParenthesizedLambdaExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ParenthesizedLambdaExpressionSyntax))) base.VisitParenthesizedLambdaExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitPointerType(PointerTypeSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(PointerTypeSyntax))) base.VisitPointerType(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitPostfixUnaryExpression(PostfixUnaryExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(PostfixUnaryExpressionSyntax))) base.VisitPostfixUnaryExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitPragmaChecksumDirectiveTrivia(PragmaChecksumDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(PragmaChecksumDirectiveTriviaSyntax))) base.VisitPragmaChecksumDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitPragmaWarningDirectiveTrivia(PragmaWarningDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(PragmaWarningDirectiveTriviaSyntax))) base.VisitPragmaWarningDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitPredefinedType(PredefinedTypeSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(PredefinedTypeSyntax))) base.VisitPredefinedType(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitPrefixUnaryExpression(PrefixUnaryExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(PrefixUnaryExpressionSyntax))) base.VisitPrefixUnaryExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(PropertyDeclarationSyntax))) base.VisitPropertyDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitQualifiedCref(QualifiedCrefSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(QualifiedCrefSyntax))) base.VisitQualifiedCref(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitQualifiedName(QualifiedNameSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(QualifiedNameSyntax))) base.VisitQualifiedName(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitQueryBody(QueryBodySyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(QueryBodySyntax))) base.VisitQueryBody(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitQueryContinuation(QueryContinuationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(QueryContinuationSyntax))) base.VisitQueryContinuation(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitQueryExpression(QueryExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(QueryExpressionSyntax))) base.VisitQueryExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitReferenceDirectiveTrivia(ReferenceDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ReferenceDirectiveTriviaSyntax))) base.VisitReferenceDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitRefTypeExpression(RefTypeExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(RefTypeExpressionSyntax))) base.VisitRefTypeExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitRefValueExpression(RefValueExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(RefValueExpressionSyntax))) base.VisitRefValueExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitRegionDirectiveTrivia(RegionDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(RegionDirectiveTriviaSyntax))) base.VisitRegionDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitReturnStatement(ReturnStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ReturnStatementSyntax))) base.VisitReturnStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitSelectClause(SelectClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(SelectClauseSyntax))) base.VisitSelectClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitSimpleBaseType(SimpleBaseTypeSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(SimpleBaseTypeSyntax))) base.VisitSimpleBaseType(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitSimpleLambdaExpression(SimpleLambdaExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(SimpleLambdaExpressionSyntax))) base.VisitSimpleLambdaExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitSizeOfExpression(SizeOfExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(SizeOfExpressionSyntax))) base.VisitSizeOfExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitSkippedTokensTrivia(SkippedTokensTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(SkippedTokensTriviaSyntax))) base.VisitSkippedTokensTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitStackAllocArrayCreationExpression(StackAllocArrayCreationExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(StackAllocArrayCreationExpressionSyntax))) base.VisitStackAllocArrayCreationExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitStructDeclaration(StructDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(StructDeclarationSyntax))) base.VisitStructDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitSwitchSection(SwitchSectionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(SwitchSectionSyntax))) base.VisitSwitchSection(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitSwitchStatement(SwitchStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(SwitchStatementSyntax))) base.VisitSwitchStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitThisExpression(ThisExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ThisExpressionSyntax))) base.VisitThisExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitThrowStatement(ThrowStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(ThrowStatementSyntax))) base.VisitThrowStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitTryStatement(TryStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(TryStatementSyntax))) base.VisitTryStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitTypeArgumentList(TypeArgumentListSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(TypeArgumentListSyntax))) base.VisitTypeArgumentList(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitTypeConstraint(TypeConstraintSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(TypeConstraintSyntax))) base.VisitTypeConstraint(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitTypeCref(TypeCrefSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(TypeCrefSyntax))) base.VisitTypeCref(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitTypeOfExpression(TypeOfExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(TypeOfExpressionSyntax))) base.VisitTypeOfExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitTypeParameter(TypeParameterSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(TypeParameterSyntax))) base.VisitTypeParameter(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitTypeParameterConstraintClause(TypeParameterConstraintClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(TypeParameterConstraintClauseSyntax))) base.VisitTypeParameterConstraintClause(node);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitTypeParameterList(TypeParameterListSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(TypeParameterListSyntax))) base.VisitTypeParameterList(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitUndefDirectiveTrivia(UndefDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(UndefDirectiveTriviaSyntax))) base.VisitUndefDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitUnsafeStatement(UnsafeStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(UnsafeStatementSyntax))) base.VisitUnsafeStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitUsingDirective(UsingDirectiveSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(UsingDirectiveSyntax))) base.VisitUsingDirective(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitUsingStatement(UsingStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(UsingStatementSyntax))) base.VisitUsingStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitVariableDeclaration(VariableDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(VariableDeclarationSyntax))) base.VisitVariableDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitVariableDeclarator(VariableDeclaratorSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(VariableDeclaratorSyntax))) base.VisitVariableDeclarator(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitWarningDirectiveTrivia(WarningDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(WarningDirectiveTriviaSyntax))) base.VisitWarningDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitWhereClause(WhereClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(WhereClauseSyntax))) base.VisitWhereClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitWhileStatement(WhileStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(WhileStatementSyntax))) base.VisitWhileStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlCDataSection(XmlCDataSectionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(XmlCDataSectionSyntax))) base.VisitXmlCDataSection(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlComment(XmlCommentSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(XmlCommentSyntax))) base.VisitXmlComment(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlCrefAttribute(XmlCrefAttributeSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(XmlCrefAttributeSyntax))) base.VisitXmlCrefAttribute(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlElement(XmlElementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(XmlElementSyntax))) base.VisitXmlElement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlElementEndTag(XmlElementEndTagSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(XmlElementEndTagSyntax))) base.VisitXmlElementEndTag(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlElementStartTag(XmlElementStartTagSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(XmlElementStartTagSyntax))) base.VisitXmlElementStartTag(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlEmptyElement(XmlEmptyElementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(XmlEmptyElementSyntax))) base.VisitXmlEmptyElement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlName(XmlNameSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(XmlNameSyntax))) base.VisitXmlName(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlNameAttribute(XmlNameAttributeSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(XmlNameAttributeSyntax))) base.VisitXmlNameAttribute(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlPrefix(XmlPrefixSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(XmlPrefixSyntax))) base.VisitXmlPrefix(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlProcessingInstruction(XmlProcessingInstructionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(XmlProcessingInstructionSyntax))) base.VisitXmlProcessingInstruction(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlText(XmlTextSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(XmlTextSyntax))) base.VisitXmlText(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlTextAttribute(XmlTextAttributeSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(XmlTextAttributeSyntax))) base.VisitXmlTextAttribute(node);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitYieldStatement(YieldStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
            if (this.ShouldCallBaseMethod(typeof(YieldStatementSyntax))) base.VisitYieldStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns></returns>
        private bool ShouldCallBaseMethod(Type type) => this.recurse || type == this.type;

        private void OnNodeVisited(SyntaxNode node, bool condition)
        {
            Console.WriteLine("OnNodeVisited!");
            if (!condition)
            {
                return;
            }

            this.operation(node);
        }
    }
}
