/// <summary>
/// MockedEnumTranslationUnit.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.Translation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Mock for <see cref="EnumTranslationUnit"/>.
    /// </summary>
    public class MockedEnumTranslationUnit : EnumTranslationUnit
    {
        protected MockedEnumTranslationUnit(EnumTranslationUnit original)
            : base(original)
        {
        }
        
        public static MockedEnumTranslationUnit Create(EnumTranslationUnit enumTranslationUnit)
        {
            return new MockedEnumTranslationUnit(enumTranslationUnit);
        }
        
        public IEnumerable<ITranslationUnit> Members
        {
            get { return this.members; }
        }
    }
}
