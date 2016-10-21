/// <summary>
/// MultiPurposeASTWalker.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Utilities
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Executes operations depending on conditions on nodes.
    /// </summary>
    public class MultiPurposeASTWalker : CSharpSyntaxWalker
    {
        private readonly SyntaxNode root;

        private readonly Func<SyntaxNode, bool> condition;
        private readonly Action<SyntaxNode> operation;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPurposeASTWalker"/> class.
        /// </summary>
        /// <param name="node">The root to traverse.</param>
        /// <param name="condition">The condition to react to when traversing nodes.</param>
        /// <param name="operation">The operation to perform when condition is met when traversing a node.</param>
        /// <remarks>
        /// The walker will walk through all nodes as depth level.
        /// </remarks>
        public MultiPurposeASTWalker(SyntaxNode node, Func<SyntaxNode, bool> condition, Action<SyntaxNode> operation)
            : base(SyntaxWalkerDepth.Node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            this.root = node;
            this.condition = condition;
            this.operation = operation;
        }

        /// <summary>
        /// Starts the visiting process
        /// </summary>
        public void Start()
        {
            this.Visit(this.root);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAccessorDeclaration(AccessorDeclarationSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitAccessorDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAccessorList(AccessorListSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitAccessorList(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAliasQualifiedName(AliasQualifiedNameSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitAliasQualifiedName(node);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAnonymousMethodExpression(AnonymousMethodExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitAnonymousMethodExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAnonymousObjectCreationExpression(AnonymousObjectCreationExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitAnonymousObjectCreationExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAnonymousObjectMemberDeclarator(AnonymousObjectMemberDeclaratorSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitAnonymousObjectMemberDeclarator(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitArgument(ArgumentSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitArgument(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitArgumentList(ArgumentListSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitArgumentList(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitArrayCreationExpression(ArrayCreationExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitArrayCreationExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitArrayRankSpecifier(ArrayRankSpecifierSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitArrayRankSpecifier(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitArrayType(ArrayTypeSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitArrayType(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitArrowExpressionClause(ArrowExpressionClauseSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitArrowExpressionClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAssignmentExpression(AssignmentExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitAssignmentExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAttribute(AttributeSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitAttribute(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAttributeArgument(AttributeArgumentSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitAttributeArgument(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAttributeArgumentList(AttributeArgumentListSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitAttributeArgumentList(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAttributeList(AttributeListSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitAttributeList(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAttributeTargetSpecifier(AttributeTargetSpecifierSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitAttributeTargetSpecifier(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAwaitExpression(AwaitExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitAwaitExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitBadDirectiveTrivia(BadDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitBadDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitBaseExpression(BaseExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitBaseExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitBaseList(BaseListSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitBaseList(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitBinaryExpression(BinaryExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitBinaryExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitBlock(BlockSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitBlock(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitBracketedArgumentList(BracketedArgumentListSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitBracketedArgumentList(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitBracketedParameterList(BracketedParameterListSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitBracketedParameterList(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitBreakStatement(BreakStatementSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitBreakStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCaseSwitchLabel(CaseSwitchLabelSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitCaseSwitchLabel(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCastExpression(CastExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitCastExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCatchClause(CatchClauseSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitCatchClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCatchDeclaration(CatchDeclarationSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitCatchDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCatchFilterClause(CatchFilterClauseSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitCatchFilterClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCheckedExpression(CheckedExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitCheckedExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCheckedStatement(CheckedStatementSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitCheckedStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitClassDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitClassOrStructConstraint(ClassOrStructConstraintSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitClassOrStructConstraint(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCompilationUnit(CompilationUnitSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitCompilationUnit(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitConditionalAccessExpression(ConditionalAccessExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitConditionalAccessExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitConditionalExpression(ConditionalExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitConditionalExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitConstructorConstraint(ConstructorConstraintSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitConstructorConstraint(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitConstructorDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitConstructorInitializer(ConstructorInitializerSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitConstructorInitializer(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitContinueStatement(ContinueStatementSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitContinueStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitConversionOperatorDeclaration(ConversionOperatorDeclarationSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitConversionOperatorDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitConversionOperatorMemberCref(ConversionOperatorMemberCrefSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitConversionOperatorMemberCref(node);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCrefBracketedParameterList(CrefBracketedParameterListSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitCrefBracketedParameterList(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCrefParameter(CrefParameterSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitCrefParameter(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCrefParameterList(CrefParameterListSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitCrefParameterList(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitDefaultExpression(DefaultExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitDefaultExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitDefaultSwitchLabel(DefaultSwitchLabelSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitDefaultSwitchLabel(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitDefineDirectiveTrivia(DefineDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitDefineDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitDelegateDeclaration(DelegateDeclarationSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitDelegateDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitDestructorDeclaration(DestructorDeclarationSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitDestructorDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitDocumentationCommentTrivia(DocumentationCommentTriviaSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitDocumentationCommentTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitDoStatement(DoStatementSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitDoStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitElementAccessExpression(ElementAccessExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitElementAccessExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitElementBindingExpression(ElementBindingExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitElementBindingExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitElifDirectiveTrivia(ElifDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitElifDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitElseClause(ElseClauseSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitElseClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitElseDirectiveTrivia(ElseDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitElseDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitEmptyStatement(EmptyStatementSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitEmptyStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitEndIfDirectiveTrivia(EndIfDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitEndIfDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitEndRegionDirectiveTrivia(EndRegionDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitEndRegionDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitEnumDeclaration(EnumDeclarationSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitEnumDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitEnumMemberDeclaration(EnumMemberDeclarationSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitEnumMemberDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitEqualsValueClause(EqualsValueClauseSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitEqualsValueClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitErrorDirectiveTrivia(ErrorDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitErrorDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitEventDeclaration(EventDeclarationSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitEventDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitEventFieldDeclaration(EventFieldDeclarationSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitEventFieldDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitExplicitInterfaceSpecifier(ExplicitInterfaceSpecifierSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitExplicitInterfaceSpecifier(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitExpressionStatement(ExpressionStatementSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitExpressionStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitExternAliasDirective(ExternAliasDirectiveSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitExternAliasDirective(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitFieldDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitFinallyClause(FinallyClauseSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitFinallyClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitFixedStatement(FixedStatementSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitFixedStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitForEachStatement(ForEachStatementSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitForEachStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitForStatement(ForStatementSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitForStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitFromClause(FromClauseSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitFromClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitGenericName(GenericNameSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitGenericName(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitGlobalStatement(GlobalStatementSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitGlobalStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitGotoStatement(GotoStatementSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitGotoStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitGroupClause(GroupClauseSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitGroupClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitIdentifierName(IdentifierNameSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitIdentifierName(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitIfDirectiveTrivia(IfDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitIfDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitIfStatement(IfStatementSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitIfStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitImplicitArrayCreationExpression(ImplicitArrayCreationExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitImplicitArrayCreationExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitImplicitElementAccess(ImplicitElementAccessSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitImplicitElementAccess(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitIncompleteMember(IncompleteMemberSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitIncompleteMember(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitIndexerDeclaration(IndexerDeclarationSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitIndexerDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitIndexerMemberCref(IndexerMemberCrefSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitIndexerMemberCref(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitInitializerExpression(InitializerExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitInitializerExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitInterfaceDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitInterpolatedStringExpression(InterpolatedStringExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitInterpolatedStringExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitInterpolatedStringText(InterpolatedStringTextSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitInterpolatedStringText(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitInterpolation(InterpolationSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitInterpolation(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitInterpolationAlignmentClause(InterpolationAlignmentClauseSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitInterpolationAlignmentClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitInterpolationFormatClause(InterpolationFormatClauseSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitInterpolationFormatClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitInvocationExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitJoinClause(JoinClauseSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitJoinClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitJoinIntoClause(JoinIntoClauseSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitJoinIntoClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitLabeledStatement(LabeledStatementSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitLabeledStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitLetClause(LetClauseSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitLetClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitLineDirectiveTrivia(LineDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitLineDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitLiteralExpression(LiteralExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitLiteralExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitLocalDeclarationStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitLockStatement(LockStatementSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitLockStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitMakeRefExpression(MakeRefExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitMakeRefExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitMemberAccessExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitMemberBindingExpression(MemberBindingExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitMemberBindingExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitMethodDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitNameColon(NameColonSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitNameColon(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitNameEquals(NameEqualsSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitNameEquals(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitNameMemberCref(NameMemberCrefSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitNameMemberCref(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitNamespaceDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitNullableType(NullableTypeSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitNullableType(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitObjectCreationExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitOmittedArraySizeExpression(OmittedArraySizeExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitOmittedArraySizeExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitOmittedTypeArgument(OmittedTypeArgumentSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitOmittedTypeArgument(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitOperatorDeclaration(OperatorDeclarationSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitOperatorDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitOperatorMemberCref(OperatorMemberCrefSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitOperatorMemberCref(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitOrderByClause(OrderByClauseSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitOrderByClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitOrdering(OrderingSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitOrdering(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitParameter(ParameterSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitParameter(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitParameterList(ParameterListSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitParameterList(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitParenthesizedExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitParenthesizedLambdaExpression(ParenthesizedLambdaExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitParenthesizedLambdaExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitPointerType(PointerTypeSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitPointerType(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitPostfixUnaryExpression(PostfixUnaryExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitPostfixUnaryExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitPragmaChecksumDirectiveTrivia(PragmaChecksumDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitPragmaChecksumDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitPragmaWarningDirectiveTrivia(PragmaWarningDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitPragmaWarningDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitPredefinedType(PredefinedTypeSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitPredefinedType(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitPrefixUnaryExpression(PrefixUnaryExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitPrefixUnaryExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitPropertyDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitQualifiedCref(QualifiedCrefSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitQualifiedCref(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitQualifiedName(QualifiedNameSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitQualifiedName(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitQueryBody(QueryBodySyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitQueryBody(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitQueryContinuation(QueryContinuationSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitQueryContinuation(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitQueryExpression(QueryExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitQueryExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitReferenceDirectiveTrivia(ReferenceDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitReferenceDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitRefTypeExpression(RefTypeExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitRefTypeExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitRefValueExpression(RefValueExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitRefValueExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitRegionDirectiveTrivia(RegionDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitRegionDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitReturnStatement(ReturnStatementSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitReturnStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitSelectClause(SelectClauseSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitSelectClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitSimpleBaseType(SimpleBaseTypeSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitSimpleBaseType(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitSimpleLambdaExpression(SimpleLambdaExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitSimpleLambdaExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitSizeOfExpression(SizeOfExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitSizeOfExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitSkippedTokensTrivia(SkippedTokensTriviaSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitSkippedTokensTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitStackAllocArrayCreationExpression(StackAllocArrayCreationExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitStackAllocArrayCreationExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitStructDeclaration(StructDeclarationSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitStructDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitSwitchSection(SwitchSectionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitSwitchSection(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitSwitchStatement(SwitchStatementSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitSwitchStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitThisExpression(ThisExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitThisExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitThrowStatement(ThrowStatementSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitThrowStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitTryStatement(TryStatementSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitTryStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitTypeArgumentList(TypeArgumentListSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitTypeArgumentList(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitTypeConstraint(TypeConstraintSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitTypeConstraint(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitTypeCref(TypeCrefSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitTypeCref(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitTypeOfExpression(TypeOfExpressionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitTypeOfExpression(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitTypeParameter(TypeParameterSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitTypeParameter(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitTypeParameterConstraintClause(TypeParameterConstraintClauseSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitTypeParameterConstraintClause(node);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitTypeParameterList(TypeParameterListSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitTypeParameterList(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitUndefDirectiveTrivia(UndefDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitUndefDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitUnsafeStatement(UnsafeStatementSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitUnsafeStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitUsingDirective(UsingDirectiveSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitUsingDirective(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitUsingStatement(UsingStatementSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitUsingStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitVariableDeclaration(VariableDeclarationSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitVariableDeclaration(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitVariableDeclarator(VariableDeclaratorSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitVariableDeclarator(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitWarningDirectiveTrivia(WarningDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitWarningDirectiveTrivia(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitWhereClause(WhereClauseSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitWhereClause(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitWhileStatement(WhileStatementSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitWhileStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlCDataSection(XmlCDataSectionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitXmlCDataSection(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlComment(XmlCommentSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitXmlComment(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlCrefAttribute(XmlCrefAttributeSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitXmlCrefAttribute(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlElement(XmlElementSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitXmlElement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlElementEndTag(XmlElementEndTagSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitXmlElementEndTag(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlElementStartTag(XmlElementStartTagSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitXmlElementStartTag(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlEmptyElement(XmlEmptyElementSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitXmlEmptyElement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlName(XmlNameSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitXmlName(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlNameAttribute(XmlNameAttributeSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitXmlNameAttribute(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlPrefix(XmlPrefixSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitXmlPrefix(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlProcessingInstruction(XmlProcessingInstructionSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitXmlProcessingInstruction(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlText(XmlTextSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitXmlText(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlTextAttribute(XmlTextAttributeSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitXmlTextAttribute(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitYieldStatement(YieldStatementSyntax node)
        {
            this.OnNodeVisited(node);
            base.VisitYieldStatement(node);
        }

        private void OnNodeVisited(SyntaxNode node)
        {
            if (!this.condition(node))
            {
                return;
            }

            this.operation(node);
        }
    }
}
