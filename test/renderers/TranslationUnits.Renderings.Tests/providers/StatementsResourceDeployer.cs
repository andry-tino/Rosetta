/// <summary>
/// StatementsResourceDeployer.cs
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
    internal class StatementsResourceDeployer : IResourceProvider
    {
        /// <summary>
        /// Provides the necessary resources for processing comparison test.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TestResource> Provide() => this.Containers.Select(
            container => RenderingUtils.RetrieveAllTestMethodsInClassContainer(container)
                .Select(method => method.Name)
                .Select(name => new TestResource(container, name, this.Assembly))).SelectMany(resource => resource);

        private Assembly Assembly => typeof(StatementsResourceDeployer).Assembly;

        private IEnumerable<Type> Containers => new[]
        {
            typeof(TestData.ConditionalStatements.Statements),
            typeof(TestData.ExpressionStatements.Statements),
            typeof(TestData.KeywordStatements.Statements)
        };
    }
}
