/// <summary>
/// ResourceDeployer.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Renderings
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// 
    /// </summary>
    public class ResourceDeployer
    {
        private readonly string resourceName;
        private readonly Assembly assembly;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceDeployer"/> class.
        /// </summary>
        /// <param name="resourceName">
        /// The name of the resource to look for in the assembly.
        /// The name can be partial. As resources arstored in assembly using a FQN, 
        /// the resource can have different names depending on which assembly it is stored in. 
        /// It is possible to pass a partial name for the resource (like the file name) 
        /// and the class will try to find the resource.
        /// </param>
        /// <param name="assembly">The assembly to look into. If not specified the current executing assembly is used.</param>
        public ResourceDeployer(string resourceName, Assembly assembly = null)
        {
            if (resourceName == null)
            {
                throw new ArgumentNullException(nameof(resourceName));
            }

            this.resourceName = resourceName;
            this.assembly = assembly == null ? Assembly.GetExecutingAssembly() : assembly;
        }

        /// <summary>
        /// Gets a value indicating whether the resource is available in the assembly.
        /// </summary>
        public bool IsResourceAvailableInAssembly => this.FoundResourceNames.Count() > 0;

        /// <summary>
        /// Gets a value indicating whether the resource is available in the assembly.
        /// </summary>
        public bool IsResourceNameAmbiguous => this.FoundResourceNames.Count() > 1;

        /// <summary>
        /// Gets a collection of all the resources that have been found with the name provided at construction time.
        /// </summary>
        public IEnumerable<string> FoundResourceNames
        {
            get
            {
                var names = this.assembly.GetManifestResourceNames();
                var foundNames = names.Where(name => name.Contains(this.resourceName) || this.resourceName.Contains(name));

                return foundNames;
            }
        }

        /// <summary>
        /// Extracts a resource from an assembly and returns its content.
        /// </summary>
        /// <returns>A <see cref="string"/> representing the resource content.</returns>
        /// <remarks>
        /// If more resources are found, the first is selected. Use <see cref="IsResourceNameAmbiguous"/>
        /// to check whether there was an ambiguity to resolve.
        /// </remarks>
        public string ExtractResource()
        {
            string content = null;
            using (var resource = this.GetResource(this.FoundResourceNames.First()))
            {
                using (var reader = new StreamReader(resource))
                {
                    content = reader.ReadToEnd();
                }
            }

            return content;
        }

        /// <summary>
        /// Extracts a resource from an assembly and saves it to the specified path.
        /// </summary>
        /// <param name="path">The path where to save the extracted resource.</param>
        public void WriteResourceToFile(string path)
        {
            using (var resource = this.GetResource(this.FoundResourceNames.First()))
            {
                using (var file = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    resource.CopyTo(file);
                }
            }
        }

        protected Stream GetResource(string name) => this.assembly.GetManifestResourceStream(name);
    }
}
