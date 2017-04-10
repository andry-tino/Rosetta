/// <summary>
/// ReflectionContextFSAssemblyLoader.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Loads an assembly from the filesystem.
    /// </summary>
    /// <remarks>
    /// This class will only load the reflection context, types will be inspectable but not executable.
    /// </remarks>
    public class ReflectionContextFSAssemblyLoader : FSAssemblyLoader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectionContextFSAssemblyLoader"/> class.
        /// </summary>
        /// <param name="assemblyPath"></param>
        public ReflectionContextFSAssemblyLoader(string assemblyPath) 
            : base(assemblyPath)
        {
        }

        protected virtual Assembly LoadCore() => Assembly.LoadFrom(this.AssemblyPath);
    }
}
