/// <summary>
/// IResourceProvider.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Renderings
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface IResourceProvider
    {
        /// <summary>
        /// Provides the necessary resources for processing comparison test.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TestResource> Provide();
    }
}
