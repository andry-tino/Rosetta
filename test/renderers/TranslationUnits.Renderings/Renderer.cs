/// <summary>
/// Renderer.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings
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
        /// Gets the providers.
        /// </summary>
        protected override Type[] DataProviders
        {
            get
            {
                return new Type[] 
                {
                    DataProvider.ModulesMethodsProvider,
                    DataProvider.ClassesMethodsProvider,
                    DataProvider.MethodsMethodsProvider,
                    DataProvider.ConstructorsMethodsProvider,
                    DataProvider.MembersMethodsProvider,
                    DataProvider.BinaryExpressionsMethodsProvider,
                    DataProvider.UnaryExpressionsMethodsProvider,
                    DataProvider.CastExpressionsMethodsProvider,
                    DataProvider.ParenthesizedExpressionsMethodsProvider,
                    DataProvider.MemberAccessExpressionsMethodsProvider,
                    DataProvider.MixedExpressionsMethodsProvider,
                    DataProvider.StatementsMethodsProvider
                };
            }
        }
    }
}
