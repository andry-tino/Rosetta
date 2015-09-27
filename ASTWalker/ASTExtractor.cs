/// <summary>
/// ASTExtractor.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST
{
    using System;
    using Microsoft.CodeAnalysis.CSharp;

    /// <summary>
    /// Extracts.
    /// </summary>
    public static class ASTExtractor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static CSharpSyntaxTree Extract(this string source)
        {
            return (CSharpSyntaxTree)CSharpSyntaxTree.ParseText(source);
        }
    }
}
