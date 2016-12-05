/// <summary>
/// SingleLineCommentTranslationUnit.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Translation unit for single line comments.
    /// </summary>
    public class SingleLineCommentTranslationUnit : NestedElementTranslationUnit, ITranslationUnit
    {
        private readonly string comment;
        protected readonly string commentToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleLineCommentTranslationUnit"/> class.
        /// </summary>
        /// <param name="comment"></param>
        protected SingleLineCommentTranslationUnit(string comment, string commentToken) 
            : base()
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            if (commentToken == null)
            {
                throw new ArgumentNullException(nameof(commentToken));
            }

            this.comment = comment;
            this.commentToken = commentToken;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static SingleLineCommentTranslationUnit Create(string comment, CommentStyle style = CommentStyle.Normal)
        {
            return new SingleLineCommentTranslationUnit(comment, CreateToken(style));
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

            writer.Write("{0} {1}", 
                this.commentToken, 
                this.comment);

            return writer.ToString();
        }

        private static string CreateToken(CommentStyle style)
        {
            switch (style)
            {
                case CommentStyle.XmlStyle:
                    return Lexems.SingleLineXmlStyleComment;
                case CommentStyle.Normal:
                default:
                    return Lexems.SingleLineComment;
            }
        }

        #region Types

        /// <summary>
        /// Options for rendering comment.
        /// </summary>
        public enum CommentStyle
        {
            /// <summary>
            /// Normal comment.
            /// </summary>
            Normal,

            /// <summary>
            /// Xml style comment.
            /// </summary>
            XmlStyle
        }

        #endregion
    }
}
