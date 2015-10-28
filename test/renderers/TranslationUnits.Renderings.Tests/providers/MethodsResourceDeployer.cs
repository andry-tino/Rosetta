/// <summary>
/// MethodsResourceDeployer.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings.Tests
{
    using System;
    using System.Collections.Generic;

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
            return null;
        }
    }
}
