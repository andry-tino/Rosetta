/// <summary>
/// MockedConstructorDefinitionTranslationUnit.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.Translation
{
    using System;
    using System.Collections.Generic;

    using Rosetta.ScriptSharp.Definition.Translation;
    using Rosetta.Translation;

    /// <summary>
    /// Mock for <see cref="MethodDefinitionTranslationUnit"/>.
    /// </summary>
    public class MockedConstructorDefinitionTranslationUnit : ConstructorDefinitionTranslationUnit
    {
        protected MockedConstructorDefinitionTranslationUnit(ConstructorDefinitionTranslationUnit original)
            : base(original)
        {
        }

        public static MockedConstructorDefinitionTranslationUnit Create(ConstructorDefinitionTranslationUnit constructorDefinitionTranslationUnit)
        {
            return new MockedConstructorDefinitionTranslationUnit(constructorDefinitionTranslationUnit);
        }

        public new IEnumerable<ITranslationUnit> Arguments
        {
            get { return this.arguments; }
        }

        public new ITranslationUnit ReturnType
        {
            get { return this.returnType; }
        }
    }
}
