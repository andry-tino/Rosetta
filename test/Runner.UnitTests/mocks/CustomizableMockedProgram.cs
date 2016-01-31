/// <summary>
/// CustomizableMockedProgram.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Runner.UnitTests.Mocks
{
    using System;

    /// <summary>
    /// Mock for <see cref="Program"/>.
    /// This mock inhibits both <see cref="Program.EmitFiles"/> and <see cref="Program.PrepareFiles"/>.
    /// </summary>
    internal class CustomizableMockedProgram : MockedProgramBase
    {
        private Action emitFilesRoutine;
        private Action prepareFilesRoutine;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomizableMockedProgram"/> class.
        /// </summary>
        /// <param name="emitFilesRoutine">
        /// If <code>null</code>, <see cref="Program.EmitFiles"/> will be run normally.
        /// </param>
        /// <param name="prepareFilesRoutine">
        /// If <code>null</code>, <see cref="Program.PrepareFiles"/> will be run normally.
        /// </param>
        /// <param name="args"></param>
        public CustomizableMockedProgram(Action emitFilesRoutine, Action prepareFilesRoutine, string[] args)
            : base(args)
        {
            // The base constructor will do its job, but will not run 
            // the program giving us time to initialize internal variables
            this.prepareFilesRoutine = prepareFilesRoutine;
            this.emitFilesRoutine = emitFilesRoutine;

            // Now we call the run routine
            this.Run();
        }

        /// <summary>
        /// This is needed to allow us to properly construct mocks when having parameters to pass.
        /// The problem is that when calling the base class, then <see cref="Program.Run"/> is called,
        /// and after that we can start the derived class constructor.
        /// By using this approach, we prevent <see cref="Program"/> from calling <see cref="Program.Run"/>,
        /// then we can execute our ocnstructor, and then we call <see cref="Program.Run"/> ourselves!
        /// </summary>
        protected override void StartProgram()
        {
            // Do nothing
        }

        protected override void EmitFiles()
        {
            if (this.emitFilesRoutine == null)
            {
                base.EmitFiles();
                return;
            }

            this.emitFilesRoutine();
        }

        protected override void PrepareFiles()
        {
            if (this.prepareFilesRoutine == null)
            {
                base.PrepareFiles();
                return;
            }

            this.prepareFilesRoutine();
        }
    }
}
