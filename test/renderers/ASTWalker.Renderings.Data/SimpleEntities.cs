/// <summary>
/// SimpleEntities.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Renderings.Data
{
    using System;

    using Rosetta.Renderings.Utils;
    using Rosetta.Translation;

    /// <summary>
    /// 
    /// </summary>
    public class SimpleEntities
    {
        [RenderingResource("SimpleEmptyClass.ts")]
        public string RenderSimpleEmptyClass()
        {
            // Use ASTWalker
            // Get TU and run Translate()
            return "";
        }
    }
}
