/// <summary>
/// RenderingResourceAttribute.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Renderings.Utils
{
    using System;
    using System.IO;

    /// <summary>
    /// This class is used by the test framework to specify the resource to publish.
    /// </summary>
    /// <remarks>
    /// Reflection parses instances of this type, so a certain structure is expected.
    /// - Constructor must accept only one (string) argument.
    /// - Only one field is expected of type which is used with the same purpose as the 
    /// only one argument of the constructor.
    /// </remarks>
    [System.AttributeUsage(
        System.AttributeTargets.Method,
        AllowMultiple = false)]
    public class RenderingResourceAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public RenderingResourceAttribute(string fileName)
        {
            this.FileName = fileName;
        }

        /// <summary>
        /// 
        /// </summary>
        public string FileName
        {
            get;
            set;
        }
    }
}
