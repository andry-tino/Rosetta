/// <summary>
/// FieldDeclaration.cs
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
    /// Helper for parameters.
    /// </summary>
    public class FieldDeclaration : Rosetta.AST.Helpers.FieldDeclaration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldDeclaration"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        public FieldDeclaration(FieldDeclarationSyntax syntaxNode)
            : this(syntaxNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldDeclaration"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        /// <param name="semanticModel"></param>
        public FieldDeclaration(FieldDeclarationSyntax syntaxNode, SemanticModel semanticModel)
            : base(syntaxNode, semanticModel)
        {
        }

        /// <summary>
        /// Gets the name of the variable.
        /// </summary>
        public override string Name {
            get {
                var attributes = new AttributeLists(this.FieldDeclarationSyntaxNode).Attributes;
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
        /// Creates the variable declaration helper.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Must return a type derived from <see cref="VariableDeclaration"/>.
        /// </remarks>
        protected override Rosetta.AST.Helpers.VariableDeclaration CreateVariableDeclarationHelper(VariableDeclarationSyntax node, SemanticModel semanticModel)
        {
            return new Rosetta.ScriptSharp.AST.Helpers.VariableDeclaration(node, semanticModel);
        }

        /// <summary>
        /// Legacy PreserveName attribute. AT some point in time, ScriptSharp migrated to ScriptName attribute. We want to support both.
        /// </summary>
        private bool ShouldPreserveName
        {
            get
            {
                var attributes = new AttributeLists(this.FieldDeclarationSyntaxNode).Attributes;
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
