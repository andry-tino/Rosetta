/// <summary>
/// MockedModuleTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Mocks
{
    using System;
    using System.Collections.Generic;

    using Rosetta.Translation;

    /// <summary>
    /// Mock for <see cref="ModuleTranslationUnit"/>.
    /// </summary>
    public class MockedModuleTranslationUnit : ModuleTranslationUnit
    {
        protected MockedModuleTranslationUnit(ModuleTranslationUnit original)
            : base(original)
        {
        }

        public static MockedModuleTranslationUnit Create(ModuleTranslationUnit moduleTranslationUnit)
        {
            return new MockedModuleTranslationUnit(moduleTranslationUnit);
        }

        public IEnumerable<ITranslationUnit> Classes
        {
            get { return this.classes; }
        }

        public IEnumerable<ITranslationUnit> Interfaces
        {
            get { return this.interfaces; }
        }

        public ITranslationUnit Name
        {
            get { return this.name; }
        }
    }
}
