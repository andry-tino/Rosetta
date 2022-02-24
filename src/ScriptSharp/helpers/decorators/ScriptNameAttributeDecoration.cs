/// <summary>
/// PreserveNameAttributeDecoration.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.AST.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Rosetta.AST.Helpers;

    /// <summary>
    /// Decorates <see cref="AttributeDecoration"/>.
    /// </summary>
    /// <remarks>
    /// This is a syntax helper which is used to retrieve info on the ScriptName
    /// attribute from a mere syntax point of view.
    /// </remarks>
    public class ScriptNameAttributeDecoration
    {
        //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Event, Inherited = false, AllowMultiple = false)]
        public const string ScriptNameFullName = "ScriptName"; // TODO: Find namespace used by ScriptSharp for this class
        public const string ScriptNameName = "ScriptName";
        public const string PreserveCasePropertyName = "PreserveCase";
        public const string PreserveNamePropertyName = "PreserveName";

        private AttributeDecoration attribute;

        // Cached quantities
        private string overridenName;
        private readonly bool preserveCase;
        private readonly bool preserveName;

        /// <summary>
        /// Preserve name as is
        /// </summary>
        public bool PreserveCase => preserveCase;
        /// <summary>
        /// It prevents generating transforming private/internal member variable names from "Variable" to "_variable". Currently Rosetta doesn't do this transformation, so it is not implemented. https://github.com/nikhilk/scriptsharp/blob/0b2d2770ac4c94b4ae14e589f090e33c215df4b7/src/Core/Compiler/Compiler/MetadataBuilder.cs#L277</para>
        /// </summary>
        public bool PreserveName => preserveName;
        /// <summary>
        /// Gets the name of the overriden Script Name.
        /// </summary>
        public string OverridenName => overridenName;

        /// <summary>
        /// Initializes a new instance of the <see cref="PreserveNameAttributeDecoration"/> class.
        /// </summary>
        /// <param name="attribute"></param>
        public ScriptNameAttributeDecoration(AttributeDecoration attribute)
        {
            if (attribute == null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }
            this.attribute = attribute;

            if (!IsScriptNameAttributeDecoration(attribute))
            {
                throw new ArgumentException(nameof(attribute), "Not compatible!");
            }

            this.attribute = attribute;
            foreach (var argument in this.attribute.Arguments)
            {
                if (argument.LiteralExpression.SyntaxNode.Kind().ToString().Equals("StringLiteralExpression", StringComparison.Ordinal))
                {
                    this.overridenName = argument.LiteralExpression.Literal.ValueText;
                    break;
                } else if (argument.AttributeName.Equals(PreserveCasePropertyName, StringComparison.Ordinal))
                {
                    this.preserveCase = argument.GetAttributeValue() as bool? ?? true;
                } else if (argument.AttributeName.Equals(PreserveNamePropertyName, StringComparison.Ordinal))
                {
                    this.preserveName = argument.GetAttributeValue() as bool? ?? true;
                }
            }
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
        public static bool IsScriptNameAttributeDecoration(AttributeDecoration attribute)
        {
            return attribute.Name == ScriptNameFullName;
        }
    }
}
