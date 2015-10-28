/// <summary>
/// TestResource.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings.Tests
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    internal class TestResource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestResource"/> class.
        /// </summary>
        /// <param name="embeddedResourceName">The name of the embedded resource (no full name, just the resource name).</param>
        /// <param name="rendererClassName"></param>
        /// <param name="rendererClassMethodName"></param>
        public TestResource(string embeddedResourceName, Type rendererClassName, string rendererClassMethodName)
        {
            this.EmbeddedArchetypeResourceName = GetFullEmbeddedResourceName(embeddedResourceName);
            this.RendererClassName = rendererClassName;
            this.RendererClassMethodName = rendererClassMethodName;
        }

        /// <summary>
        /// Gets the full path to the embedded.
        /// </summary>
        public string EmbeddedArchetypeResourceName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Type RendererClassName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string RendererClassMethodName { get; private set; }

        private static string GetFullEmbeddedResourceName(string resourceName)
        {
            return null;
        }
    }
}
