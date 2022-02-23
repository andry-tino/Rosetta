/// <summary>
/// InterfaceDeclaration.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.ScriptSharp.AST.Helpers
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST.Helpers;

    /// <summary>
    /// Helper for accessing interface in AST
    /// </summary>
    public class InterfaceDeclaration : Rosetta.AST.Helpers.InterfaceDeclaration
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceDeclaration"/> class.
        /// </summary>
        /// <param name="interfaceDeclarationNode"></param>
        public InterfaceDeclaration(InterfaceDeclarationSyntax interfaceDeclarationNode)
            : base(interfaceDeclarationNode)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceDeclaration"/> class.
        /// </summary>
        /// <param name="interfaceDeclarationNode"></param>
        /// <param name="semanticModel"></param>
        public InterfaceDeclaration(InterfaceDeclarationSyntax interfaceDeclarationNode, SemanticModel semanticModel)
            : base(interfaceDeclarationNode, semanticModel)
        {
        }

        public override string Name
        {
            get
            {
                var attributes = new AttributeLists(this.InterfaceDeclarationNode).Attributes;
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
    }
}
