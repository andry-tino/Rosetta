/// <summary>
/// DifferenceDisplayerFactory.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.ThirdParty.Google.DiffMatchPatch
{
    using System;

    /// <summary>
    /// Creates the proper instance of <see cref="IDifferenceDisplayer"/>.
    /// </summary>
    internal class DifferenceDisplayerFactory
    {
        private readonly DifferenceDisplayMode mode;

        /// <summary>
        /// Initializes a new instance of the <see cref="DifferenceDisplayerFactory"/> class.
        /// </summary>
        /// <param name="mode"></param>
        public DifferenceDisplayerFactory(DifferenceDisplayMode mode)
        {
            this.mode = mode;
        }

        /// <summary>
        /// Creates an instance of a class implementing the <see cref="IDifferenceDisplayer"/> interface.
        /// </summary>
        /// <returns>A <see cref="IDifferenceDisplayer"/></returns>
        public IDifferenceDisplayer Create()
        {
            switch (this.mode)
            {
                case DifferenceDisplayMode.HTML:
                    return null; // TODO: Create this displayer
                case DifferenceDisplayMode.PlainTextSeparated:
                default:
                    return new PlainTextSeparatedDifferenceDisplayer();
            }
        }
    }
}
