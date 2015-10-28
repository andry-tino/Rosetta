/// <summary>
/// IResourceProvider.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings.Tests
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    internal interface IResourceProvider
    {
        /// <summary>
        /// Provides the necessary resources for processing comparison test.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TestResource> Provide();
    }
}
