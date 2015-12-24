/// <summary>
/// StatementsGroups.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings.Data
{
    using System;

    using Rosetta.Renderings.Utils;

    /// <summary>
    /// 
    /// </summary>
    public class StatementsGroups
    {
        /// <summary>
        /// TODO: Implement
        /// </summary>
        /// <returns></returns>
        [RenderingResource("SimpleGroup.ts")]
        public string RenderSimpleGroup()
        {
            ITranslationUnit translationUnit = null;

            return translationUnit.Translate();
        }
    }
}
