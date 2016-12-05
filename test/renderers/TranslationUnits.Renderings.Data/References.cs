/// <summary>
/// References.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.Translation.Renderings.Data
{
    using System;

    using Rosetta.Renderings.Utils;

    /// <summary>
    /// 
    /// </summary>
    public class References
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("ReferenceToFile.ts")]
        public string RenderReferenceToFile()
        {
            ITranslationUnit translationUnit = TranslationUnitBuilder.BuildReferenceTranslationUnit("referencedFile.ts");

            return translationUnit.Translate();
        }
    }
}
