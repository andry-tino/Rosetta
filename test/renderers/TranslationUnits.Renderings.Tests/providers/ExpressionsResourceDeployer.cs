/// <summary>
/// ExpressionsResourceDeployer.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Translation.Renderings.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Rosetta.Renderings;
    using TestData = Rosetta.Translation.Renderings.Data;
    using RenderingUtils = Rosetta.Renderings.Utilities;

    /// <summary>
    /// 
    /// </summary>
    internal class ExpressionsResourceDeployer : IResourceProvider
    {
        /// <summary>
        /// Provides the necessary resources for processing comparison test.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TestResource> Provide() => this.Containers.Select(
            container => RenderingUtils.RetrieveAllTestMethodsInClassContainer(container)
                .Select(method => method.Name)
                .Select(name => new TestResource(container, name, this.Assembly))).SelectMany(resource => resource);

        private Assembly Assembly => typeof(ExpressionsResourceDeployer).Assembly;

        private IEnumerable<Type> Containers => new[]
        {
            typeof(TestData.BinaryExpressions.Expressions),
            typeof(TestData.CastExpressions.Expressions),
            typeof(TestData.InvokationExpressions.Expressions),
            typeof(TestData.LiteralExpressions.Expressions),
            typeof(TestData.MemberAccessExpressions.Expressions),
            typeof(TestData.ObjectCreationExpressions.Expressions),
            typeof(TestData.ParenthesizedExpressions.Expressions),
            typeof(TestData.UnaryExpressions.Expressions)
        };
    }
}
