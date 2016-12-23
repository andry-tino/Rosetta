/// <summary>
/// PreserveNameAttributeDecoration.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.AST.Helpers
{
    using System;

    using Rosetta.AST.Helpers;

    /// <summary>
    /// Decorates <see cref="AttributeDecoration"/>.
    /// </summary>
    /// <remarks>
    /// This is a syntax helper which is used to retrieve info on the PreserveNamespace
    /// attribute from a mere syntax point of view.
    /// </remarks>
    public class PreserveNameAttributeDecoration
    {
        public const string PreserveNameFullName = "PreserveName"; // TODO: Find namespace used by ScriptSharp for this class
        public const string PreserveNameName = "PreserveName";

        private AttributeDecoration attribute;

        // Cached quantities
        private string overridenNamespace;

        /// <summary>
        /// Initializes a new instance of the <see cref="PreserveNameAttributeDecoration"/> class.
        /// </summary>
        /// <param name="attribute"></param>
        public PreserveNameAttributeDecoration(AttributeDecoration attribute)
        {
            if (attribute == null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            if (!IsPreserveNameAttributeDecoration(attribute))
            {
                throw new ArgumentException(nameof(attribute), "Not compatible!");
            }

            this.attribute = attribute;
        }

        /// <summary>
        /// Gets the name of the attribute.
        /// </summary>
        public string Name
        {
            get { return this.attribute.Name; }
        }

        /// <summary>
        /// Gets the decorated <see cref="AttributeDecoration"/.>
        /// </summary>
        public AttributeDecoration AttributeDecoration
        {
            get { return this.attribute; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static bool IsPreserveNameAttributeDecoration(AttributeDecoration attribute)
        {
            return attribute.Name == PreserveNameFullName;
        }
    }
}
