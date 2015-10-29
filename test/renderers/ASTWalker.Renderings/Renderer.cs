/// <summary>
/// Renderer.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Renderings
{
    using System;
    using System.IO;
    using System.Reflection;

    using Rosetta.Renderings.Utils;

    /// <summary>
    /// 
    /// </summary>
    internal partial class Renderer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Renderer"/> class.
        /// </summary>
        /// <param name="outputFolderPath"></param>
        public Renderer(string outputFolderPath)
        {
            if (outputFolderPath == null)
            {
                throw new ArgumentNullException(nameof(outputFolderPath));
            }

            this.OutputFolderPath = outputFolderPath;
        }

        /// <summary>
        /// 
        /// </summary>
        public string OutputFolderPath
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Render()
        {
            
        }

        /// <summary>
        /// Searches through all public methos decorated with the <see cref="RenderingResourceAttribute"/>
        /// attribute, gets translation and writes files on hard drive.
        /// </summary>
        /// <param name="type"></param>
        private void RenderFromMethodInfoArray(Type type)
        {
            var classInstance = Activator.CreateInstance(type);
            var methods = type.GetMethods();

            foreach (MethodInfo methodInfo in methods)
            {
                RenderingResourceAttribute attribute = null;

                // Check method is proper
                if (!CheckRenderMethod(methodInfo, out attribute))
                {
                    continue;
                }

                string fileName = attribute.FileName;
                if (fileName == null)
                {
                    continue;
                }

                string path = Path.Combine(this.OutputFolderPath, fileName);

                try
                {
                    string typeScript = (string)methodInfo.Invoke(classInstance, null);

                    using (StreamWriter writer = new StreamWriter(path))
                    {
                        writer.Write(typeScript);
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(
                        string.Format(
                            "An error occurred in method `{0}`!",
                            methodInfo.Name), e);
                }
            }
        }

        private static bool CheckRenderMethod(MethodInfo methodInfo, out RenderingResourceAttribute renderingResourceAttribute)
        {
            Attribute attribute = Attribute.GetCustomAttribute(methodInfo, typeof(RenderingResourceAttribute));
            if (attribute == null)
            {
                renderingResourceAttribute = null;
                return false;
            }

            // (Double) Check attribute
            renderingResourceAttribute = attribute as RenderingResourceAttribute;
            if (renderingResourceAttribute == null)
            {
                throw new InvalidOperationException(
                    string.Format("Expected a {0} type attribute!", typeof(RenderingResourceAttribute).Name));
            }

            // Check method visibility
            if (!methodInfo.IsPublic)
            {
                return false;
            }

            // Check method return type
            if (methodInfo.ReturnType != typeof(string))
            {
                throw new InvalidOperationException(
                    string.Format("Method marked with {0} attribute must return a string!",
                    typeof(RenderingResourceAttribute).Name));
            }

            return true;
        }
    }
}
