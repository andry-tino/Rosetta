/// <summary>
/// Members.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings.Data
{
    using System;

    using Rosetta.Renderings.Utils;

    /// <summary>
    /// 
    /// </summary>
    public class Members
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("IntPrivateMember.ts")]
        public string IntPrivateMember()
        {
            ITranslationUnit translationUnit = TranslationUnitBuilder.BuildMemberTranslationUnit(
                VisibilityToken.Private, "int", "intMember");

            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("StringPublicMember.ts")]
        public string StringPublicMember()
        {
            ITranslationUnit translationUnit = TranslationUnitBuilder.BuildMemberTranslationUnit(
                VisibilityToken.Public, "string", "stringMember");

            return translationUnit.Translate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("StringMember.ts")]
        public string StringMember()
        {
            ITranslationUnit translationUnit = TranslationUnitBuilder.BuildMemberTranslationUnit(
                VisibilityToken.None, "string", "stringMember");

            return translationUnit.Translate();
        }
    }
}
