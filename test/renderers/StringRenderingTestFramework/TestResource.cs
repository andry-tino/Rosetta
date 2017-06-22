/// <summary>
/// TestResource.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Renderings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using Rosetta.Renderings.Utils;

    /// <summary>
    /// Describes a test resource.
    /// </summary>
    /// <remarks>
    /// This class has to be sealed as most of the operations 
    /// are performed in ctor, thus polymorphism cannot be relied on.
    /// </remarks>
    public sealed class TestResource
    {
        private readonly Assembly assembly;
        private readonly MethodInfo methodInfo;
        private readonly object containingClassInstance;

        private StringGenerator generator;

        // Cached values
        private string actualValue; // Generated via Reflection (costly)
        private string archetypeValue; // Generated via assembly manifest resource extraction (costly)

        /// <summary>
        /// Initializes a new instance of the <see cref="TestResource"/> class.
        /// </summary>
        /// <param name="containingClass">
        /// The <see cref="Type"/> representing the class containing the method 
        /// to use for generating the code to compare
        /// </param>
        /// <param name="methodName">The name of the method to look for in <paramref name="containingClass"/></param>
        /// <param name="assembly">
        /// The assembly where to find the archetype resource to retrieve. 
        /// If <code>null</code>, then the assembly containing <paramref name="containingClass"/> will 
        /// be used instead.
        /// </param>
        public TestResource(Type containingClass, string methodName, Assembly assembly = null)
        {
            if (containingClass == null)
            {
                throw new ArgumentNullException(nameof(containingClass));
            }
            if (methodName == null)
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            this.assembly = assembly == null ? containingClass.Assembly : assembly;
            this.methodInfo = containingClass.GetMethod(methodName);

             // Generating the instance of the class. 
             // Being a test class, it is supposed to have a parameterless constructor.
             ConstructorInfo containingClassCtor = containingClass.GetConstructor(Type.EmptyTypes);
            this.containingClassInstance = containingClassCtor.Invoke(new object[] { });

            // Generating quantities
            this.generator = () => 
            {
                object returnValue = this.methodInfo.Invoke(this.containingClassInstance, new object[] { });
                var str = returnValue as string;

                if (str == null)
                {
                    throw new InvalidOperationException("Method invocation failed");
                }

                return str;
            };

            this.EmbeddedResourceName = Utilities.RetrieveRenderingResourceAttributeValue(this.methodInfo.CustomAttributes);
            if (this.EmbeddedResourceName == null)
            {
                throw new InvalidOperationException("Could not retrieve embedded resource name");
            }
        }

        /// <summary>
        /// Gets the name of the embedded resource.
        /// </summary>
        public string EmbeddedResourceName { get; private set; }

        /// <summary>
        /// Gets the archetype.
        /// </summary>
        /// <remarks>
        /// The first time it is called, the <see cref="ResourceDeployer"/> is invoked to get the 
        /// resource from the assembly.
        /// </remarks>
        public string ArchetypeValue
        {
            get
            {
                if (this.archetypeValue == null)
                {
                    var deployer = new ResourceDeployer(this.EmbeddedResourceName, this.assembly);

                    if (!deployer.IsResourceAvailableInAssembly)
                    {
                        throw new InvalidOperationException($"Resource {this.EmbeddedResourceName} could not be found in assembly {this.assembly.FullName}");
                    }
                    if (deployer.IsResourceNameAmbiguous)
                    {
                        var message = new StringBuilder();
                        message.AppendLine($"Resource {this.EmbeddedResourceName} is ambiguous in assembly {this.assembly.FullName}.");
                        message.AppendLine($"{deployer.FoundResourceNames.Count()} resources found:");

                        foreach (var name in deployer.FoundResourceNames)
                        {
                            message.AppendLine($"- {name}");
                        }

                        throw new InvalidOperationException(message.ToString());
                    }

                    this.archetypeValue = deployer.ExtractResource();
                }

                return this.archetypeValue;
            }
        }

        /// <summary>
        /// Gets the generated string to compare to archetype.
        /// </summary>
        /// <remarks>
        /// The first time it is called, the underlying test method generating the string is invoked.
        /// </remarks>
        public string ActualValue
        {
            get
            {
                if (this.actualValue == null)
                {
                    // Invoking the underlying test method
                    this.actualValue = this.generator();
                }

                return this.actualValue;
            }
        }

        #region Types

        /// <summary>
        /// Represents the piece of logic to run in order to get the string to compare to archetype.
        /// </summary>
        /// <returns></returns>
        private delegate string StringGenerator();

        #endregion
    }
}
