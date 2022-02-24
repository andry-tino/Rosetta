/// <summary>
/// EnumMemberDeclaration.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.AST.Helpers
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Helper for accessing enum members in AST.
    /// </summary>
    public class EnumMemberDeclaration : Rosetta.AST.Helpers.EnumMemberDeclaration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumMemberDeclaration"/> class.
        /// </summary>
        /// <param name="enumMemberDeclarationNode"></param>
        public EnumMemberDeclaration(EnumMemberDeclarationSyntax enumMemberDeclarationNode)
            : this(enumMemberDeclarationNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumMemberDeclaration"/> class.
        /// </summary>
        /// <param name="enumMemberDeclarationNode"></param>
        /// <param name="semanticModel"></param>
        public EnumMemberDeclaration(EnumMemberDeclarationSyntax enumMemberDeclarationNode, SemanticModel semanticModel)
            : base(enumMemberDeclarationNode, semanticModel)
        {
        }
        
        /// <summary>
        /// Gets the name.
        /// </summary>
        public override string Name
        {
            get
            {
                var attributes = new Rosetta.AST.Helpers.AttributeLists(this.EnumMemberDeclarationSyntaxNode).Attributes;
                foreach (var attribute in attributes)
                {
                    if (ScriptNameAttributeDecoration.IsScriptNameAttributeDecoration(attribute))
                    {
                        var scriptNameAttributeDecoration = new ScriptNameAttributeDecoration(attribute);
                        return scriptNameAttributeDecoration.OverridenName ?? base.Name.ToScriptSharpName(scriptNameAttributeDecoration.PreserveCase);
                    }
                }
                //By default, EnumMembers are CamelCased
                return base.Name.ToScriptSharpName();
            }
        }
    }
}
