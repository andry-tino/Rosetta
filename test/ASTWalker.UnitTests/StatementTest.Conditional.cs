/// <summary>
/// StatementTest.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.UnitTests
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Symbols;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Rosetta.Translation;
    using Rosetta.Tests.Data;
    using Rosetta.Tests.Utils;
    using Rosetta.AST.UnitTests.Mocks;

    /// <summary>
    /// Tests for <see cref="ConditionalStatementASTWalker"/> class.
    /// </summary>
    public partial class StatementTest
    {
        [TestMethod]
        [Ignore]
        public void EmptyIfStatement()
        {
            string source = @"
                public class Class1 {
                    public void Method1() {
                        if (true) {
                        }
                    }
                }
            ";
        }
    }
}
