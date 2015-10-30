/// <summary>
/// Renderer.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Renderings
{
    using System;

    using Rosetta.Renderings.Utils;

    /// <summary>
    /// 
    /// </summary>
    internal class Renderer : RendererBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Renderer"/> class.
        /// </summary>
        /// <param name="outputFolderPath"></param>
        public Renderer(string outputFolderPath)
            : base(outputFolderPath)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        protected override Type[] DataProviders
        {
            get
            {
                return new Type[]
                {
                    
                };
            }
        }
    }
}
