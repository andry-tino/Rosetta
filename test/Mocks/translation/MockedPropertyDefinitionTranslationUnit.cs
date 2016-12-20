/// <summary>
/// MockedPropertyDefinitionTranslationUnit.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.Translation
{
    using System;
    using System.Collections.Generic;

    using Rosetta.ScriptSharp.Definition.Translation;
    using Rosetta.Translation;

    /// <summary>
    /// Mock for <see cref="PropertyDefinitionTranslationUnit"/>.
    /// </summary>
    public class MockedPropertyDefinitionTranslationUnit : PropertyDefinitionTranslationUnit
    {
        protected MockedPropertyDefinitionTranslationUnit(PropertyDefinitionTranslationUnit original)
            : base(original)
        {
        }

        public static MockedPropertyDefinitionTranslationUnit Create(PropertyDefinitionTranslationUnit propertyDefinitionTranslationUnit)
        {
            return new MockedPropertyDefinitionTranslationUnit(propertyDefinitionTranslationUnit);
        }

        public ITranslationUnit Type
        {
            get { return this.type; }
        }
    }
}
