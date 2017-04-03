﻿/// <summary>
/// IASTBuilder.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection
{
    using System;

    using Microsoft.CodeAnalysis;

    /// <summary>
    /// Collects information about the generated AST.
    /// </summary>
    public class ASTInfo
    {
        /// <summary>
        /// Gets or sets the tree.
        /// </summary>
        public SyntaxTree Tree { get; set; }

        /// <summary>
        /// Gets or sets the compilation unit.
        /// This miht be null.
        /// </summary>
        public Compilation CompilationUnit { get; set; }

        /// <summary>
        /// Gets or sets the number of generated classes.
        /// </summary>
        public int ClassCount { get; set; }

        /// <summary>
        /// Gets or sets the number of generated structs.
        /// </summary>
        public int StructCount { get; set; }

        /// <summary>
        /// Gets or sets the number of generated enums.
        /// </summary>
        public int EnumCount { get; set; }

        /// <summary>
        /// Gets or sets the number of generated interfaces.
        /// </summary>
        public int InterfaceCount { get; set; }
    }
}
