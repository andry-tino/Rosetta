﻿/// <summary>
/// ReferencesResourceDeployer.cs
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
    internal class ReferencesResourceDeployer : IResourceProvider
    {
        /// <summary>
        /// Provides the necessary resources for processing comparison test.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TestResource> Provide() =>
            RenderingUtils.RetrieveAllTestMethodsInClassContainer(this.Container)
                .Select(method => method.Name)
                .Select(name => new TestResource(this.Container, name, this.Assembly));

        private Assembly Assembly => typeof(ReferencesResourceDeployer).Assembly;

        private Type Container => typeof(TestData.References);
    }
}
