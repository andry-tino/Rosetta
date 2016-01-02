/// <summary>
/// TranslationUnitBuilder.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings.Data
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public static class TranslationUnitBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildModuleTranslationUnit(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return ModuleTranslationUnit.Create(IdentifierTranslationUnit.Create(name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="name"></param>
        /// <param name="baseClassName"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildClassTranslationUnit(VisibilityToken visibility, string name, string baseClassName)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return ClassDeclarationTranslationUnit.Create(
                visibility, IdentifierTranslationUnit.Create(name),
                baseClassName == null ? null : IdentifierTranslationUnit.Create(baseClassName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="returnType"></param>
        /// <param name="name"></param>
        /// <param name="statements"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildMethodTranslationUnit(VisibilityToken visibility, string returnType, string name, ITranslationUnit[] statements = null)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            MethodDeclarationTranslationUnit translationUnit =  MethodDeclarationTranslationUnit.Create(
                visibility, returnType == null ? null : TypeIdentifierTranslationUnit.Create(returnType),
                IdentifierTranslationUnit.Create(name));

            if (statements != null)
            {
                foreach (ITranslationUnit statement in statements)
                {
                    translationUnit.AddStatement(statement);
                }
            }

            return translationUnit;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="name"></param>
        /// <param name="statements"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildConstructorTranslationUnit(VisibilityToken visibility, string name, ITranslationUnit[] statements = null)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            ConstructorDeclarationTranslationUnit translationUnit = ConstructorDeclarationTranslationUnit.Create(
                visibility, IdentifierTranslationUnit.Create(name));

            if (statements != null)
            {
                foreach (ITranslationUnit statement in statements)
                {
                    translationUnit.AddStatement(statement);
                }
            }

            return translationUnit;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildMemberTranslationUnit(VisibilityToken visibility, string type, string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return FieldDeclarationTranslationUnit.Create(
                visibility, TypeIdentifierTranslationUnit.Create(type), IdentifierTranslationUnit.Create(name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildLiteralTranslationUnit(int value)
        {
            return LiteralTranslationUnit<int>.Create(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildLiteralTranslationUnit(string value)
        {
            return LiteralTranslationUnit<string>.Create(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildLiteralTranslationUnit(bool value)
        {
            return LiteralTranslationUnit<bool>.Create(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildVariableDeclarationTranslationUnit(string type, string name, ITranslationUnit expression = null)
        {
            return VariableDeclarationTranslationUnit.Create(
                type == null ? null : TypeIdentifierTranslationUnit.Create(type), 
                IdentifierTranslationUnit.Create(name),
                expression);
        }

        #region Expressions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operatorToken"></param>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildExpressionTranslationUnit(OperatorToken operatorToken, int number1, int number2)
        {
            return BinaryExpressionTranslationUnit.Create(
                LiteralTranslationUnit<int>.Create(number1), LiteralTranslationUnit<int>.Create(number2), operatorToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operatorToken"></param>
        /// <param name="number"></param>
        /// <param name="postfix"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildExpressionTranslationUnit(OperatorToken operatorToken, int number, bool postfix = true)
        {
            return UnaryExpressionTranslationUnit.Create(
                LiteralTranslationUnit<int>.Create(number), operatorToken, 
                postfix ? UnaryExpressionTranslationUnit.UnaryPosition.Postfix : UnaryExpressionTranslationUnit.UnaryPosition.Prefix);
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="type"></param>
        /// <param name="variableName"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildExpressionTranslationUnit(string type, string variableName)
        {
            return CastExpressionTranslationUnit.Create(
                TypeIdentifierTranslationUnit.Create(type), 
                IdentifierTranslationUnit.Create(variableName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildExpressionTranslationUnit(ITranslationUnit expression)
        {
            return ParenthesizedExpressionTranslationUnit.Create(expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildExpressionTranslationUnit(string memberName, 
            MemberAccessExpressionTranslationUnit.MemberAccessMethod accessMethod)
        {
            return MemberAccessExpressionTranslationUnit.Create(IdentifierTranslationUnit.Create(memberName), accessMethod);
        }

        #endregion

        #region Statements

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testExpression"></param>
        /// <param name="bodyStatements"></param>
        /// <param name="elseStatements"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildIfStatementTranslationUnit(ITranslationUnit testExpression, IEnumerable<ITranslationUnit> bodyStatements, IEnumerable<ITranslationUnit> elseStatements = null)
        {
            var translationUnit = ConditionalStatementTranslationUnit.Create(1, elseStatements != null);

            translationUnit.SetTestExpression(testExpression, 0);

            foreach (var statement in bodyStatements)
            {
                translationUnit.SetStatementInConditionalBlock(statement, 0);
            }

            if (elseStatements != null)
            {
                foreach (var statement in elseStatements)
                {
                    translationUnit.SetStatementInElseBlock(statement);
                }
            }

            return translationUnit;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ITranslationUnit BuildBreakStatementTranslationUnit()
        {
            return KeywordStatementTranslationUnit.Break;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ITranslationUnit BuildContinueStatementTranslationUnit()
        {
            return KeywordStatementTranslationUnit.Continue;
        }

        #endregion
    }
}
