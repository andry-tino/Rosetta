/// <summary>
/// RenderingResourceAttribute.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Renderings.Utils
{
    using System;
    using System.IO;

    /// <summary>
    /// 
    /// </summary>
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
