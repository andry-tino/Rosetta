/// <summary>
/// PropertyDeclaration.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.AST.Helpers
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST.Helpers;

    /// <summary>
    ///Decorates <see cref="AttributeDecoration"/>.
    /// </summary>
    public class PropertyDeclaration : Rosetta.AST.Helpers.PropertyDeclaration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyDeclaration"/> class.
        /// </summary>
        /// <param name="classDeclarationNode"></param>
        public PropertyDeclaration(PropertyDeclarationSyntax propertyDeclarationNode)
            : this(propertyDeclarationNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyDeclaration"/> class.
        /// </summary>
        /// <param name="classDeclarationNode"></param>
        /// <param name="semanticModel"></param>
        public PropertyDeclaration(PropertyDeclarationSyntax propertyDeclarationNode, SemanticModel semanticModel)
            : base(propertyDeclarationNode, semanticModel)
        {
        }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        public override string Name {
            get {
                var attributes = new AttributeLists(this.PropertyDeclarationSyntaxNode).Attributes;
                foreach (var attribute in attributes)
                {
                    if (ScriptNameAttributeDecoration.IsScriptNameAttributeDecoration(attribute))
                    {
                        var scriptNameAttributeDecoration = new ScriptNameAttributeDecoration(attribute);
                        return scriptNameAttributeDecoration.OverridenName ?? base.Name.ToScriptSharpName(scriptNameAttributeDecoration.PreserveCase);
                    }
                }
                return this.ShouldPreserveName ? base.Name : base.Name.ToScriptSharpName();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel"></param>
        /// <returns></returns>
        protected override Rosetta.AST.Helpers.TypeReference CreateTypeReferenceHelper(TypeSyntax node, SemanticModel semanticModel)
        {
            return new Rosetta.ScriptSharp.AST.Helpers.TypeReference(node, semanticModel);
        }

        private bool ShouldPreserveName
        {
            get
            {
                var attributes = new AttributeLists(this.PropertyDeclarationSyntaxNode).Attributes;
                foreach (var attribute in attributes)
                {
                    if (PreserveNameAttributeDecoration.IsPreserveNameAttributeDecoration(attribute))
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
