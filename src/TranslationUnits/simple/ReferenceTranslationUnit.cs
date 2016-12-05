/// <summary>
/// ReferenceTranslationUnit.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Translation unit for references.
    /// </summary>
    public class ReferenceTranslationUnit : NestedElementTranslationUnit, ITranslationUnit
    {
        private readonly string path;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceTranslationUnit"/> class.
        /// </summary>
        /// <param name="path"></param>
        private ReferenceTranslationUnit(string path) 
            : base()
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            this.path = path;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static ReferenceTranslationUnit Create(string path)
        {
            return new ReferenceTranslationUnit(path);
        }

        /// <summary>
        /// Translate the unit into TypeScript.
        /// </summary>
        /// <returns></returns>
        public string Translate()
        {
            FormatWriter writer = new FormatWriter()
            {
                Formatter = this.Formatter
            };

            string commentBody = $"<reference path=\"{this.path}\" />";
            ITranslationUnit commentTranslationUnit = SingleLineCommentTranslationUnit.Create(commentBody, 
                SingleLineCommentTranslationUnit.CommentStyle.XmlStyle);

            writer.Write("{0}", commentTranslationUnit.Translate());

            return writer.ToString();
        }
    }
}
