/// <summary>
/// CompareResult.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Renderings
{
    using System;

    /// <summary>
    /// Describes the result of one comparison.
    /// </summary>
    public struct CompareResult
    {
        private string description;

        /// <summary>
        /// The hesit of the compare. <code>true</code> if the 
        /// compare was successful, <code>false</code> otherwise.
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// A description of why the compare failed. If <see cref="Result"/> is 
        /// <code>true</code>, this field is <code>null</code>.
        /// </summary>
        public string Description
        {
            get
            {
                if (this.Result)
                {
                    return null;
                }

                return this.description;
            }

            set { this.description = value; }
        }
    }
}
