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
        private Type type;
        private Action<SyntaxNode> operation;

        /// <summary>
        /// Initializes a new instance of the <see cref="ASTWalkerNodeTypeOperationExecutor"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="type"></param>
        /// <param name="operation"></param>
        public ASTWalkerNodeTypeOperationExecutor(SyntaxNode node, Type type, Action<SyntaxNode> operation) : base(SyntaxWalkerDepth.StructuredTrivia)
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAccessorList(AccessorListSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAliasQualifiedName(AliasQualifiedNameSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAnonymousMethodExpression(AnonymousMethodExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAnonymousObjectCreationExpression(AnonymousObjectCreationExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAnonymousObjectMemberDeclarator(AnonymousObjectMemberDeclaratorSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitArgument(ArgumentSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitArgumentList(ArgumentListSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitArrayCreationExpression(ArrayCreationExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitArrayRankSpecifier(ArrayRankSpecifierSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitArrayType(ArrayTypeSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitArrowExpressionClause(ArrowExpressionClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAssignmentExpression(AssignmentExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAttribute(AttributeSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAttributeArgument(AttributeArgumentSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAttributeArgumentList(AttributeArgumentListSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAttributeList(AttributeListSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAttributeTargetSpecifier(AttributeTargetSpecifierSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitAwaitExpression(AwaitExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitBadDirectiveTrivia(BadDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitBaseExpression(BaseExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitBaseList(BaseListSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitBinaryExpression(BinaryExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitBlock(BlockSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitBracketedArgumentList(BracketedArgumentListSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitBracketedParameterList(BracketedParameterListSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitBreakStatement(BreakStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCaseSwitchLabel(CaseSwitchLabelSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCastExpression(CastExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCatchClause(CatchClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCatchDeclaration(CatchDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCatchFilterClause(CatchFilterClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCheckedExpression(CheckedExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCheckedStatement(CheckedStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitClassOrStructConstraint(ClassOrStructConstraintSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCompilationUnit(CompilationUnitSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitConditionalAccessExpression(ConditionalAccessExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitConditionalExpression(ConditionalExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitConstructorConstraint(ConstructorConstraintSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitConstructorInitializer(ConstructorInitializerSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitContinueStatement(ContinueStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitConversionOperatorDeclaration(ConversionOperatorDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitConversionOperatorMemberCref(ConversionOperatorMemberCrefSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCrefBracketedParameterList(CrefBracketedParameterListSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCrefParameter(CrefParameterSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitCrefParameterList(CrefParameterListSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitDefaultExpression(DefaultExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitDefaultSwitchLabel(DefaultSwitchLabelSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitDefineDirectiveTrivia(DefineDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitDelegateDeclaration(DelegateDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitDestructorDeclaration(DestructorDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitDocumentationCommentTrivia(DocumentationCommentTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitDoStatement(DoStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitElementAccessExpression(ElementAccessExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitElementBindingExpression(ElementBindingExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitElifDirectiveTrivia(ElifDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitElseClause(ElseClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitElseDirectiveTrivia(ElseDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitEmptyStatement(EmptyStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitEndIfDirectiveTrivia(EndIfDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitEndRegionDirectiveTrivia(EndRegionDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitEnumDeclaration(EnumDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitEnumMemberDeclaration(EnumMemberDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitEqualsValueClause(EqualsValueClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitErrorDirectiveTrivia(ErrorDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitEventDeclaration(EventDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitEventFieldDeclaration(EventFieldDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitExplicitInterfaceSpecifier(ExplicitInterfaceSpecifierSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitExpressionStatement(ExpressionStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitExternAliasDirective(ExternAliasDirectiveSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitFinallyClause(FinallyClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitFixedStatement(FixedStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitForEachStatement(ForEachStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitForStatement(ForStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitFromClause(FromClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitGenericName(GenericNameSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitGlobalStatement(GlobalStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitGotoStatement(GotoStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitGroupClause(GroupClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitIdentifierName(IdentifierNameSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitIfDirectiveTrivia(IfDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitIfStatement(IfStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitImplicitArrayCreationExpression(ImplicitArrayCreationExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitImplicitElementAccess(ImplicitElementAccessSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitIncompleteMember(IncompleteMemberSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitIndexerDeclaration(IndexerDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitIndexerMemberCref(IndexerMemberCrefSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitInitializerExpression(InitializerExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitInterpolatedStringExpression(InterpolatedStringExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitInterpolatedStringText(InterpolatedStringTextSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitInterpolation(InterpolationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitInterpolationAlignmentClause(InterpolationAlignmentClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitInterpolationFormatClause(InterpolationFormatClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitJoinClause(JoinClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitJoinIntoClause(JoinIntoClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitLabeledStatement(LabeledStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitLetClause(LetClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitLineDirectiveTrivia(LineDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitLiteralExpression(LiteralExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitLockStatement(LockStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitMakeRefExpression(MakeRefExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitMemberBindingExpression(MemberBindingExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitNameColon(NameColonSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitNameEquals(NameEqualsSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitNameMemberCref(NameMemberCrefSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitNullableType(NullableTypeSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitOmittedArraySizeExpression(OmittedArraySizeExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitOmittedTypeArgument(OmittedTypeArgumentSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitOperatorDeclaration(OperatorDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitOperatorMemberCref(OperatorMemberCrefSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitOrderByClause(OrderByClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitOrdering(OrderingSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitParameter(ParameterSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitParameterList(ParameterListSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitParenthesizedLambdaExpression(ParenthesizedLambdaExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitPointerType(PointerTypeSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitPostfixUnaryExpression(PostfixUnaryExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitPragmaChecksumDirectiveTrivia(PragmaChecksumDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitPragmaWarningDirectiveTrivia(PragmaWarningDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitPredefinedType(PredefinedTypeSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitPrefixUnaryExpression(PrefixUnaryExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitQualifiedCref(QualifiedCrefSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitQualifiedName(QualifiedNameSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitQueryBody(QueryBodySyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitQueryContinuation(QueryContinuationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitQueryExpression(QueryExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitReferenceDirectiveTrivia(ReferenceDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitRefTypeExpression(RefTypeExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitRefValueExpression(RefValueExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitRegionDirectiveTrivia(RegionDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitReturnStatement(ReturnStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitSelectClause(SelectClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitSimpleBaseType(SimpleBaseTypeSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitSimpleLambdaExpression(SimpleLambdaExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitSizeOfExpression(SizeOfExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitSkippedTokensTrivia(SkippedTokensTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitStackAllocArrayCreationExpression(StackAllocArrayCreationExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitStructDeclaration(StructDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitSwitchSection(SwitchSectionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitSwitchStatement(SwitchStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitThisExpression(ThisExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitThrowStatement(ThrowStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitTryStatement(TryStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitTypeArgumentList(TypeArgumentListSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitTypeConstraint(TypeConstraintSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitTypeCref(TypeCrefSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitTypeOfExpression(TypeOfExpressionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitTypeParameter(TypeParameterSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitTypeParameterConstraintClause(TypeParameterConstraintClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitTypeParameterList(TypeParameterListSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitUndefDirectiveTrivia(UndefDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitUnsafeStatement(UnsafeStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitUsingDirective(UsingDirectiveSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitUsingStatement(UsingStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitVariableDeclaration(VariableDeclarationSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitVariableDeclarator(VariableDeclaratorSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitWarningDirectiveTrivia(WarningDirectiveTriviaSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitWhereClause(WhereClauseSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitWhileStatement(WhileStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlCDataSection(XmlCDataSectionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlComment(XmlCommentSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlCrefAttribute(XmlCrefAttributeSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlElement(XmlElementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlElementEndTag(XmlElementEndTagSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlElementStartTag(XmlElementStartTagSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlEmptyElement(XmlEmptyElementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlName(XmlNameSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlNameAttribute(XmlNameAttributeSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlPrefix(XmlPrefixSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlProcessingInstruction(XmlProcessingInstructionSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlText(XmlTextSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitXmlTextAttribute(XmlTextAttributeSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        sealed public override void VisitYieldStatement(YieldStatementSyntax node)
        {
            this.OnNodeVisited(node, this.type.IsInstanceOfType(node));
        }

        private void OnNodeVisited(SyntaxNode node, bool condition)
        {
            if (!condition)
            {
                return;
            }

            this.operation(node);
        }
    }
}
