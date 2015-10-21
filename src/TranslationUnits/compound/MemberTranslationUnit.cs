/// <summary>
/// MemberTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Class describing scoped elements.
    /// </summary>
    public abstract class MemberTranslationUnit : ScopedElementTranslationUnit
    {
        /// <summary>
        /// The name of the member.
        /// </summary>
        protected string Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberTranslationUnit"/>.
        /// </summary>
        /// <param name="Name"></param>
        protected MemberTranslationUnit(string name) 
            : this(name, VisibilityToken.None)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberTranslationUnit"/>.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="visibility"></param>
        protected MemberTranslationUnit(string name, VisibilityToken visibility)
            : base(visibility)
        {
            this.Name = name;
        }
    }
}
