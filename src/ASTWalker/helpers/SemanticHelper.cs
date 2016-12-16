/// <summary>
/// SemanticHelper.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;

    /// <summary>
    /// Generic semantic helper.
    /// </summary>
    public abstract class SemanticHelper
    {
        private readonly object semanticObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticHelper"/> class.
        /// </summary>
        /// <param name="semanticObject">The Roslyn semantic object.</param>
        public SemanticHelper(object semanticObject)
        {
            if (semanticObject == null)
            {
                throw new ArgumentNullException(nameof(semanticObject), "A semantic object is necessary!");
            }

            this.semanticObject = semanticObject;
        }

        protected object SemanticObject => this.semanticObject;
    }
}
