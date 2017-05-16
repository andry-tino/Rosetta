/// <summary>
/// Renderer.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings
{
    using System;
    using System.Linq;

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
                    DataProvider.InterfacesMethodsProvider,
                    DataProvider.MethodsMethodsProvider,
                    DataProvider.MethodSignaturesMethodsProvider,
                    DataProvider.PropertiesMethodsProvider,
                    DataProvider.ReferencesMethodsProvider,
                    DataProvider.ConstructorsMethodsProvider,
                    DataProvider.MembersMethodsProvider,
                    DataProvider.BinaryExpressionsMethodsProvider,
                    DataProvider.UnaryExpressionsMethodsProvider,
                    DataProvider.CastExpressionsMethodsProvider,
                    DataProvider.ParenthesizedExpressionsMethodsProvider,
                    DataProvider.MemberAccessExpressionsMethodsProvider,
                    DataProvider.InvokationExpressionsMethodsProvider,
                    DataProvider.ObjectCreationExpressionsMethodsProvider,
                    DataProvider.MixedExpressionsMethodsProvider
                }
                .Union(DataProvider.StatementsMethodsProvider)
                .ToArray();
            }
        }
    }
}
