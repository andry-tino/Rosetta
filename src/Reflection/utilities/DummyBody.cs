/// <summary>
/// DummyBody.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection
{
    using System;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Class containing utilities for generating body code which is always correctly compilable.
    /// </summary>
    public static class DummyBody
    {
        /// <summary>
        /// Generates the dummy body for methods.
        /// </summary>
        /// <returns></returns>
        public static BlockSyntax GenerateForMerhod()
        {
            return SyntaxFactory.Block(GenerateNotImplementedThrowable());
        }

        /// <summary>
        /// Generates the dummy body for properties.
        /// </summary>
        /// <returns></returns>
        public static BlockSyntax GenerateForProperty()
        {
            return SyntaxFactory.Block(GenerateNotImplementedThrowable());
        }

        private static StatementSyntax GenerateNotImplementedThrowable() => SyntaxFactory.ParseStatement("throw new System.NotImplementedException();");

        private static StatementSyntax GenerateNullReturn() => SyntaxFactory.ParseStatement("return null;");
    }
}
