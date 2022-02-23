/// <summary>
/// BaseTypeReference.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.AST.Helpers
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    

    /// <summary>
    ///Decorates <see cref="AttributeDecoration"/>.
    /// </summary>
    public class ClassDeclaration : Rosetta.AST.Helpers.ClassDeclaration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClassDeclaration"/> class.
        /// </summary>
        /// <param name="classDeclarationNode"></param>
        public ClassDeclaration(ClassDeclarationSyntax classDeclarationNode)
            : this(classDeclarationNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassDeclaration"/> class.
        /// </summary>
        /// <param name="classDeclarationNode"></param>
        /// <param name="semanticModel"></param>
        public ClassDeclaration(ClassDeclarationSyntax classDeclarationNode, SemanticModel semanticModel)
            : base(classDeclarationNode, semanticModel)
        {
        }

        public override string Name
        {
            get
            {
                var attributes = new Rosetta.AST.Helpers.AttributeLists(this.ClassDeclarationSyntaxNode).Attributes;
                foreach (var attribute in attributes)
                {
                    if (ScriptNameAttributeDecoration.IsScriptNameAttributeDecoration(attribute))
                    {
                        var scriptNameAttributeDecoration = new ScriptNameAttributeDecoration(attribute);
                        return scriptNameAttributeDecoration.OverridenName ?? base.Name;
                    }
                }
                return base.Name;
            }
        }

        protected ClassDeclarationSyntax ClassDeclarationSyntaxNode
        {
            get { return this.SyntaxNode as ClassDeclarationSyntax; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel"></param>
        /// <param name="typeKind"></param>
        /// <returns></returns>
        protected override Rosetta.AST.Helpers.BaseTypeReference CreateBaseTypeReferenceHelper(BaseTypeSyntax node, SemanticModel semanticModel, TypeKind typeKind)
        {
            return new Rosetta.ScriptSharp.AST.Helpers.BaseTypeReference(node, semanticModel, typeKind);
        }
    }
}
