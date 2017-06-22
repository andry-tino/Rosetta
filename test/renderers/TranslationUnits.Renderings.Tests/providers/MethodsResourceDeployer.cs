/// <summary>
/// MethodsResourceDeployer.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Rosetta.Renderings;
    using TestData = Rosetta.Translation.Renderings.Data;

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
        public IEnumerable<TestResource> Provide()
        {
            // TODO: Loop through public methods using reflection
            return new[] 
            {
                new TestResource(typeof(TestData.Methods), "RenderEmptyMethodWithReturn", this.Assembly),
                new TestResource(typeof(TestData.Methods), "RenderSimpleEmptyMethod", this.Assembly)
            };
        }

        private Assembly Assembly => typeof(MethodsResourceDeployer).Assembly;
    }
}
