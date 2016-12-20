/// <summary>
/// MockedMethodDefinitionTranslationUnit.cs
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
    public class MockedMethodDefinitionTranslationUnit : MethodDefinitionTranslationUnit
    {
        protected MockedMethodDefinitionTranslationUnit(MethodDefinitionTranslationUnit original)
            : base(original)
        {
        }

        public static MockedMethodDefinitionTranslationUnit Create(MethodDefinitionTranslationUnit methodDefinitionTranslationUnit)
        {
            return new MockedMethodDefinitionTranslationUnit(methodDefinitionTranslationUnit);
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
