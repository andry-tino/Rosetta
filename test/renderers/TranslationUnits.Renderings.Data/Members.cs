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
        [RenderingResource("NumberPrivateMember.ts")]
        public string IntPrivateMember()
        {
            ITranslationUnit translationUnit = TranslationUnitBuilder.BuildMemberTranslationUnit(
                VisibilityToken.Private, Lexems.NumberType, "intMember");

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
                VisibilityToken.Public, Lexems.StringType, "stringMember");

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
                VisibilityToken.None, Lexems.StringType, "stringMember");

            return translationUnit.Translate();
        }
    }
}
