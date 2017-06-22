/// <summary>
/// ClassesResourceDeployer.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings.Tests
{
    using System;
    using System.Collections.Generic;

    using Rosetta.Renderings;

    /// <summary>
    /// 
    /// </summary>
    internal class ClassesResourceDeployer : IResourceProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClassesResourceDeployer"/> class.
        /// </summary>
        public ClassesResourceDeployer()
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
