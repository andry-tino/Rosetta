/// <summary>
/// MockedProgramBase.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Runner.UnitTests.Mocks
{
    using System;
    using Mono.Options;
    using Rosetta.Runner;

    /// <summary>
    /// Mock for <see cref="Program"/>.
    /// This class represents the base class for all mocks for <see cref="Program"/>.
    /// </summary>
    internal abstract class MockedProgramBase : Program
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MockedProgramBase"/> class.
        /// </summary>
        /// <param name="args"></param>
        public MockedProgramBase(string[] args) 
            : base(args)
        {
        }

        /// <summary>
        /// Gets the <see cref="OptionException"/> thrown, if any.
        /// </summary>
        public OptionException ThrownOptionException { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool HelpContentDisplayed { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ErrorHandled { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool NoFeasibleExecutionRoutineRun { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ProjectConversionRoutineRun { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool FileConversionRoutineRun { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool MainRunRoutineRun { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string FilePath
        {
            get { return this.filePath; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ProjectPath
        {
            get { return this.projectPath; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string OutputFolder
        {
            get { return this.outputFolder; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string FileName
        {
            get { return this.fileName; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Verbose
        {
            get { return this.verbose; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Help
        {
            get { return this.help; }
        }

        /// <summary>
        /// 
        /// </summary>
        public FileManager FileManager
        {
            get { return this.fileManager; }
        }

        protected override void Run()
        {
            this.MainRunRoutineRun = true;
            base.Run();
        }

        protected override void HandleNoFeasibleExecution()
        {
            this.NoFeasibleExecutionRoutineRun = true;
            base.HandleNoFeasibleExecution();
        }

        protected override void HandleError(Exception e)
        {
            this.ErrorHandled = true;
        }

        protected override void HandleOptionException(OptionException e)
        {
            this.ThrownOptionException = e;
        }

        protected override void ConvertFile()
        {
            this.FileConversionRoutineRun = true;
            base.ConvertFile();
        }

        protected override void ConvertProject()
        {
            this.ProjectConversionRoutineRun = true;
            base.ConvertProject();
        }

        protected override void ShowHelp()
        {
            this.HelpContentDisplayed = true;
        }
    }
}
