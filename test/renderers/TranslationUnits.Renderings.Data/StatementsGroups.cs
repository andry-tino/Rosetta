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
        /// Renders a group of references.
        /// </summary>
        /// <returns></returns>
        [RenderingResource("ReferencesGroup.ts")]
        public string RenderReferencesGroup()
        {
            ITranslationUnit translationUnit = TranslationUnitBuilder.BuildReferencesGroupTranslationUnit(new[] 
            {
                "file1.ts",
                "file2.ts",
                "file3.ts"
            });

            return translationUnit.Translate();
        }
    }
}
