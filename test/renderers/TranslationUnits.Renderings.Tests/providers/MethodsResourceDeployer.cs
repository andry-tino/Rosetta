/// <summary>
/// MethodsResourceDeployer.cs
/// Andrea Tino - 2015
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
    internal class MethodsResourceDeployer : IResourceProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodsResourceDeployer"/> class.
        /// </summary>
        public MethodsResourceDeployer()
        {
        }

        /// <summary>
        /// Provides the necessary resources for processing comparison test.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TestResource> Provide() => 
            RenderingUtils.RetrieveAllTestMethodsInClassContainer(typeof(TestData.Methods))
                .Select(method => method.Name)
                .Select(name => new TestResource(typeof(TestData.Methods), name, this.Assembly));

        private Assembly Assembly => typeof(MethodsResourceDeployer).Assembly;
    }
}
