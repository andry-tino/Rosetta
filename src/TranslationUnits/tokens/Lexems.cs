/// <summary>
/// Lexems.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Container for lexems.
    /// </summary>
    public static class Lexems
    {
        /// <summary>
        /// String rendering constant for open curly bracket token. 
        /// </summary>
        public const string OpenCurlyBracket = "{";

        /// <summary>
        /// String rendering constant for close curly bracket token. 
        /// </summary>
        public const string CloseCurlyBracket = "}";

        /// <summary>
        /// String rendering constant for open round bracket token. 
        /// </summary>
        public const string OpenRoundBracket = "(";

        /// <summary>
        /// String rendering constant for close round bracket token. 
        /// </summary>
        public const string CloseRoundBracket = ")";

        /// <summary>
        /// String rendering constant for open square bracket token. 
        /// </summary>
        public const string OpenSquareBracket = "[";

        /// <summary>
        /// String rendering constant for close square bracket token. 
        /// </summary>
        public const string CloseSquareBracket = "]";

        /// <summary>
        /// String rendering constant for open angle bracket token. 
        /// </summary>
        public const string OpenAngularBracket = "<";

        /// <summary>
        /// String rendering constant for close angle bracket token. 
        /// </summary>
        public const string CloseAngularBracket = ">";

        /// <summary>
        /// String rendering constant for comma token. 
        /// </summary>
        public const string Comma = ",";

        /// <summary>
        /// String rendering constant for dot token. 
        /// </summary>
        public const string Dot = ".";

        /// <summary>
        /// String rendering constant for semicolon token. 
        /// </summary>
        public const string Semicolon = ";";

        /// <summary>
        /// String rendering constant for colon token. 
        /// </summary>
        public const string Colon = ":";

        /// <summary>
        /// String rendering constant for single quote token. 
        /// </summary>
        public const string SingleQuote = "'";

        /// <summary>
        /// String rendering constant for double quote token. 
        /// </summary>
        public const string DoubleQuote = "\"";

        /// <summary>
        /// String rendering constant for double equal token. 
        /// </summary>
        public const string LogicalEquals = "==";

        /// <summary>
        /// String rendering constant for inequal token. 
        /// </summary>
        public const string LogicalNotEquals = "!=";

        /// <summary>
        /// String rendering constant for not token. 
        /// </summary>
        public const string LogicalNot = "!";

        /// <summary>
        /// String rendering constant for assignment token. 
        /// </summary>
        public const string EqualsSign = "=";

        /// <summary>
        /// String rendering constant for plus token. 
        /// </summary>
        public const string Plus = "+";

        /// <summary>
        /// String rendering constant for plus assignment token. 
        /// </summary>
        public const string PlusAssign = "+=";

        /// <summary>
        /// String rendering constant for minus token. 
        /// </summary>
        public const string Minus = "-";

        /// <summary>
        /// String rendering constant for minus assignment token. 
        /// </summary>
        public const string MinusAssign = "-=";

        /// <summary>
        /// String rendering constant for star token. 
        /// </summary>
        public const string Times = "*";

        /// <summary>
        /// String rendering constant for star assignment token. 
        /// </summary>
        public const string TimesAssign = "*=";

        /// <summary>
        /// String rendering constant for slash token. 
        /// </summary>
        public const string Divide = "/";

        /// <summary>
        /// String rendering constant for slash assignment token. 
        /// </summary>
        public const string DivideAssign = "/=";

        /// <summary>
        /// String rendering constant for modulo token. 
        /// </summary>
        public const string Modulo = "%";

        /// <summary>
        /// String rendering constant for modulo assignment token. 
        /// </summary>
        public const string ModuloAssign = "%=";

        /// <summary>
        /// String rendering constant for ampersand token. 
        /// </summary>
        public const string And = "&";

        /// <summary>
        /// String rendering constant for ameprsand assignment token. 
        /// </summary>
        public const string AndAssign = "&=";

        /// <summary>
        /// String rendering constant for pipe token. 
        /// </summary>
        public const string Or = "|";

        /// <summary>
        /// String rendering constant for pipe assignment token. 
        /// </summary>
        public const string OrAssign = "|=";

        /// <summary>
        /// String rendering constant for XOR token. 
        /// </summary>
        public const string Xor = "^";

        /// <summary>
        /// String rendering constant for XOR assignment token. 
        /// </summary>
        public const string XorAssign = "^=";

        /// <summary>
        /// String rendering constant for stream left token. 
        /// </summary>
        public const string LeftShift = "<<";

        /// <summary>
        /// String rendering constant for stream left assignment token. 
        /// </summary>
        public const string LeftShiftAssign = "<<=";

        /// <summary>
        /// String rendering constant for stream right token. 
        /// </summary>
        public const string RightShift = ">>";

        /// <summary>
        /// String rendering constant for stream right assignment token. 
        /// </summary>
        public const string RightShiftAssign = ">>=";

        /// <summary>
        /// String rendering constant for increment token. 
        /// </summary>
        public const string PlusPlus = "++";

        /// <summary>
        /// String rendering constant for decrement token. 
        /// </summary>
        public const string MinusMinus = "--";

        /// <summary>
        /// String rendering constant for single line comment. 
        /// </summary>
        public const string SingleLineComment = "//";

        /// <summary>
        /// String rendering constant for single line comment like in XML comments. 
        /// </summary>
        public const string SingleLineXmlStyleComment = "///";

        /// <summary>
        /// String rendering constant for newline token. 
        /// </summary>
        public static string Newline = Environment.NewLine;

        /// <summary>
        /// String rendering constant for whitespace token. 
        /// </summary>
        public static string Whitespace = " ";

        /// <summary>
        /// String rendering constant for class keyword token. 
        /// </summary>
        public const string ClassKeyword = "class";

        /// <summary>
        /// String rendering constant for enum keyword token. 
        /// </summary>
        public const string EnumKeyword = "enum";

        /// <summary>
        /// String rendering constant for interface keyword token. 
        /// </summary>
        public const string InterfaceKeyword = "interface";

        /// <summary>
        /// String rendering constant for void keyword token. 
        /// </summary>
        public const string VoidReturnType = "void";

        /// <summary>
        /// String rendering constant for constructor keyword token. 
        /// </summary>
        public const string ConstructorKeyword = "constructor";

        /// <summary>
        /// String rendering constant for extends keyword token. 
        /// </summary>
        public const string ExtendsKeyword = "extends";

        /// <summary>
        /// String rendering constant for implements keyword token. 
        /// </summary>
        public const string ImplementsKeyword = "implements";

        /// <summary>
        /// String rendering constant for module keyword token. 
        /// </summary>
        public const string ModuleKeyword = "module";

        /// <summary>
        /// String rendering constant for export keyword token. 
        /// </summary>
        public const string ExportKeyword = "export";

        /// <summary>
        /// String rendering constant for declare keyword token. 
        /// </summary>
        public const string DeclareKeyword = "declare";

        /// <summary>
        /// String rendering constant for break keyword token. 
        /// </summary>
        public const string BreakKeyword = "break";

        /// <summary>
        /// String rendering constant for continue keyword token. 
        /// </summary>
        public const string ContinueKeyword = "continue";

        /// <summary>
        /// String rendering constant for return keyword token. 
        /// </summary>
        public const string ReturnKeyword = "return";

        /// <summary>
        /// String rendering constant for throw keyword token. 
        /// </summary>
        public const string ThrowKeyword = "throw";

        /// <summary>
        /// String rendering constant for var keyword token. 
        /// </summary>
        public const string VariableDeclaratorKeyword = "var";

        /// <summary>
        /// String rendering constant for new keyword token. 
        /// </summary>
        public const string NewKeyword = "new";

        /// <summary>
        /// String rendering constant for this keyword token. 
        /// </summary>
        public const string ThisKeyword = "this";

        /// <summary>
        /// String rendering constant for get keyword token. 
        /// </summary>
        public const string GetKeyword = "get";

        /// <summary>
        /// String rendering constant for set keyword token. 
        /// </summary>
        public const string SetKeyword = "set";

        /// <summary>
        /// String rendering constant for super keyword token. 
        /// </summary>
        public const string BaseKeyword = "super";

        /// <summary>
        /// String rendering constant for if keyword token. 
        /// </summary>
        public const string IfKeyword = "if";

        /// <summary>
        /// String rendering constant for else-if keyword token. 
        /// </summary>
        public const string ElseIfKeyword = "else if";

        /// <summary>
        /// String rendering constant for else keyword token. 
        /// </summary>
        public const string ElseKeyword = "else";

        /// <summary>
        /// String rendering constant for true keyword token. 
        /// </summary>
        public const string TrueKeyword = "true";

        /// <summary>
        /// String rendering constant for false keyword token. 
        /// </summary>
        public const string FalseKeyword = "false";

        /// <summary>
        /// String rendering constant for null keyword token. 
        /// </summary>
        public const string NullKeyword = "null";

        /// <summary>
        /// String rendering constant for number keyword token. 
        /// </summary>
        public const string NumberType = "number";

        /// <summary>
        /// String rendering constant for string keyword token. 
        /// </summary>
        public const string StringType = "string";

        /// <summary>
        /// String rendering constant for boolean keyword token. 
        /// </summary>
        public const string BooleanType = "boolean";

        /// <summary>
        /// String rendering constant for any keyword token. 
        /// </summary>
        public const string AnyType = "any";

        /// <summary>
        /// String rendering constant for Function keyword token.
        /// </summary>
        public const string FunctionType = "Function";
    }
}
