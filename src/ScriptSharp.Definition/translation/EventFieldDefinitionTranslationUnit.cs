/// <summary>
/// FieldDefinitionTranslationUnit.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.Translation
{
    using System;

    using Rosetta.Translation;

    /// <summary>
    /// Class describing a method signature (no body).
    /// </summary>
    public class EventFieldDefinitionTranslationUnit : FieldDeclarationTranslationUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventFieldDefinitionTranslationUnit"/> class.
        /// </summary>
        protected EventFieldDefinitionTranslationUnit() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventFieldDefinitionTranslationUnit"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="visibility"></param>
        protected EventFieldDefinitionTranslationUnit(ITranslationUnit name, ModifierTokens visibility) : base(name, visibility)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modifiers"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static new EventFieldDefinitionTranslationUnit Create(ModifierTokens modifiers, ITranslationUnit type, ITranslationUnit name)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return new EventFieldDefinitionTranslationUnit()
            {
                Modifiers = modifiers,
                Name = name,
                Type = type
            };
        }

        public override string Translate()
        {
            FormatWriter writer = new FormatWriter()
            {
                Formatter = this.Formatter
            };

            // Opening declaration
            string fieldVisibility = this.RenderedVisibilityModifier;

            //For Event, 2 functions are generated
            //add_onGetItemMetaData: function SparkleXrm_GridEditor_DataViewBase$add_onGetItemMetaData(value)
            //remove_onGetItemMetaData: function SparkleXrm_GridEditor_DataViewBase$remove_onGetItemMetaData(value)
            //function add_onGetItemMetaData(value);
            //function remove_onGetItemMetaData(value);
            writer.WriteLine("{0}add_{1}{2}: {3}{4}",
                        this.Modifiers.ConvertToTypeScriptEquivalent().EmitOptionalVisibility(),
                        this.RenderedName,
                        string.Format("{0}{1}: {2}{3}", Lexems.OpenRoundBracket, "value", Lexems.FunctionType, Lexems.CloseRoundBracket),
                        Lexems.VoidReturnType,
                        Lexems.Semicolon);
            writer.WriteLine("{0}remove_{1}{2}: {3}{4}",
                        this.Modifiers.ConvertToTypeScriptEquivalent().EmitOptionalVisibility(),
                        this.RenderedName,
                        string.Format("{0}{1}: {2}{3}", Lexems.OpenRoundBracket, "value", Lexems.FunctionType, Lexems.CloseRoundBracket),
                        Lexems.VoidReturnType,
                        Lexems.Semicolon);

            return writer.ToString();
        }
    }
}
