/// <summary>
/// EnumDeclaration.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.AST.Helpers
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Helper for accessing enums in AST.
    /// </summary>
    public class EnumDeclaration : Rosetta.AST.Helpers.EnumDeclaration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumDeclaration"/> class.
        /// </summary>
        /// <param name="enumDeclarationNode"></param>
        public EnumDeclaration(EnumDeclarationSyntax enumDeclarationNode)
            : this(enumDeclarationNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumDeclaration"/> class.
        /// </summary>
        /// <param name="enumDeclarationNode"></param>
        /// <param name="semanticModel"></param>
        public EnumDeclaration(EnumDeclarationSyntax enumDeclarationNode, SemanticModel semanticModel)
            : base(enumDeclarationNode, semanticModel)
        {
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public override string Name
        {
            get
            {
                var attributes = new Rosetta.AST.Helpers.AttributeLists(this.EnumDeclarationSyntaxNode).Attributes;
                foreach (var attribute in attributes)
                {
                    if (ScriptNameAttributeDecoration.IsScriptNameAttributeDecoration(attribute))
                    {
                        var scriptNameAttributeDecoration = new ScriptNameAttributeDecoration(attribute);
                        return scriptNameAttributeDecoration.OverridenName ?? base.Name.ToScriptSharpName(scriptNameAttributeDecoration.PreserveCase);
                    }
                }
                //By default, Enum names are not CamelCased.
                return base.Name;
            }
        }
    }
}
