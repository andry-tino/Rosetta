/// <summary>
/// ConditionalStatement.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Helper for accessing parenthesized expressions in AST.
    /// </summary>
    internal class ConditionalStatement : Helper
    {
        // Cached values
        private bool? hasElseBlock;
        private int? blocksNumber;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalStatement"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        public ConditionalStatement(IfStatementSyntax syntaxNode)
            : this(syntaxNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalStatement"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        /// <param name="semanticModel"></param>
        public ConditionalStatement(IfStatementSyntax syntaxNode, SemanticModel semanticModel)
            : base(syntaxNode, semanticModel)
        {
            this.hasElseBlock = null;
            this.blocksNumber = null;
        }

        /// <summary>
        /// Gets an indication whether the ELSE block is present or not.
        /// </summary>
        public bool HasElseBlock
        {
            get
            {
                if (!this.hasElseBlock.HasValue)
                {
                    this.CalculateCachedQuantities();
                }

                return this.hasElseBlock.Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int BlocksNumber
        {
            get
            {
                if (!this.blocksNumber.HasValue)
                {
                    this.CalculateCachedQuantities();
                }

                return this.blocksNumber.Value;
            }
        }

        private void CalculateCachedQuantities()
        {
            var ifStatement = this.IfStatementSyntaxNode;
            var blocksNum = 0;
            var hasFinalElse = false;

            while (true)
            {
                blocksNum++;

                if (ifStatement.Else == null)
                {
                    // No final else, end of ifs
                    break;
                }

                var ifElseStatement = ifStatement.Else.Statement as IfStatementSyntax;
                if (ifElseStatement == null)
                {
                    hasFinalElse = true;
                    break;
                }

                // One more if else
                ifStatement = ifElseStatement;
            }

            this.hasElseBlock = hasFinalElse;
            this.blocksNumber = blocksNum;
        }

        private IfStatementSyntax IfStatementSyntaxNode
        {
            get { return this.syntaxNode as IfStatementSyntax; }
        }
    }
}
