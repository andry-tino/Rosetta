/// <summary>
/// MockedProgram.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Runner.UnitTests.Mocks
{
    using System;

    /// <summary>
    /// Mock for <see cref="Program"/>.
    /// This mock inhibits both <see cref="Program.EmitFiles"/> and <see cref="Program.PrepareFiles"/>.
    /// </summary>
    internal class MockedProgram : MockedProgramBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MockedProgram"/> class.
        /// </summary>
        /// <param name="args"></param>
        public MockedProgram(string[] args)
            : base(args)
        {
        }

        protected override void EmitFiles()
        {
            // Do nothing, we do not want to proceed with actual translation
        }

        protected override void PrepareFiles()
        {
            // Do nothing, we do not want to proceed with actual translation
        }
    }
}
