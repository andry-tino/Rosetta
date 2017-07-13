/// <summary>
/// TypeInfoLogger.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Diagnostics.Logging
{
    using System;

    using Rosetta.Diagnostics.Logging;
    using Rosetta.Reflection.Proxies;

    /// <summary>
    /// Logs operations concerning <see cref="ITypeInfoProxy"/> nodes.
    /// </summary>
    public class TypeInfoLogger
    {
        private readonly ITypeInfoProxy type;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeInfoLogger"/> class.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="logger"></param>
        public TypeInfoLogger(ITypeInfoProxy type, ILogger logger)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            
            this.type = type;
            this.logger = logger;
        }

        /// <summary>
        /// Logs info about the creation of an AST node when inspecting, through reflection, a <see cref="ITypeInfoProxy"/>.
        /// </summary>
        public void LogSyntaxNodeCreation(string nodeType = "Unknown")
        {
            this.Log(this.type.Name, $"Created as AST node (type: {nodeType})");
        }

        protected void Log(string typeName, string action)
        {
            this.logger.Log("AST Builder", "Reflection encountered type:", typeName, "Action:", action);
        }
    }
}
