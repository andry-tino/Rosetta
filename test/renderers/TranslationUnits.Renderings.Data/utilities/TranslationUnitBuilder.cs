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
        /// <param name="name"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildInterfaceTranslationUnit(VisibilityToken visibility, string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return InterfaceDeclarationTranslationUnit.Create(
                visibility, IdentifierTranslationUnit.Create(name));
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
        /// <param name="returnType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildMethodSignatureTranslationUnit(VisibilityToken visibility, string returnType, string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return MethodSignatureDeclarationTranslationUnit.Create(
                visibility, returnType == null ? null : TypeIdentifierTranslationUnit.Create(returnType),
                IdentifierTranslationUnit.Create(name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="returnType"></param>
        /// <param name="name"></param>
        /// <param name="getStatements"></param>
        /// <param name="setStatements"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildPropertyTranslationUnit(VisibilityToken visibility, string returnType, string name, ITranslationUnit[] getStatements = null, ITranslationUnit[] setStatements = null)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (returnType == null)
            {
                throw new ArgumentNullException(nameof(returnType));
            }

            PropertyDeclarationTranslationUnit translationUnit = PropertyDeclarationTranslationUnit.Create(
                visibility, TypeIdentifierTranslationUnit.Create(returnType), IdentifierTranslationUnit.Create(name), true, true);
            
            if (getStatements != null)
            {
                var statementsGroup = StatementsGroupTranslationUnit.Create();
                foreach (ITranslationUnit statement in getStatements)
                {
                    statementsGroup.AddStatement(statement);
                }
                translationUnit.SetGetAccessor(statementsGroup);
            }

            if (setStatements != null)
            {
                var statementsGroup = StatementsGroupTranslationUnit.Create();
                foreach (ITranslationUnit statement in setStatements)
                {
                    statementsGroup.AddStatement(statement);
                }
                translationUnit.SetSetAccessor(statementsGroup);
            }

            return translationUnit;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="statements"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildConstructorTranslationUnit(VisibilityToken visibility, ITranslationUnit[] statements = null)
        {
            ConstructorDeclarationTranslationUnit translationUnit = ConstructorDeclarationTranslationUnit.Create(visibility);

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
        /// <param name="value"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildNullLiteralTranslationUnit()
        {
            return LiteralTranslationUnit.Null;
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

        public static ITranslationUnit BuildReferenceTranslationUnit(string path)
        {
            return ReferenceTranslationUnit.Create(path);
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
        public static ITranslationUnit BuildExpressionTranslationUnit(ITranslationUnit invokeeName, ITranslationUnit[] arguments)
        {
            InvokationExpressionTranslationUnit translationUnit = InvokationExpressionTranslationUnit.Create(invokeeName);

            if (arguments != null)
            {
                foreach (ITranslationUnit argument in arguments)
                {
                    translationUnit.AddArgument(argument);
                }
            }

            return translationUnit;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invokeeName"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildObjectCreationExpressionTranslationUnit(ITranslationUnit type, ITranslationUnit[] arguments)
        {
            var translationUnit = ObjectCreationExpressionTranslationUnit.Create(type);

            if (arguments != null)
            {
                foreach (ITranslationUnit argument in arguments)
                {
                    translationUnit.AddArgument(argument);
                }
            }

            return translationUnit;
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ITranslationUnit BuildReturnStatementTranslationUnit(ITranslationUnit expression)
        {
            return ExpressionStatementTranslationUnit.CreateReturn(expression as ExpressionTranslationUnit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ITranslationUnit BuildReturnStatementTranslationUnit()
        {
            return ExpressionStatementTranslationUnit.CreateReturn();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ITranslationUnit BuildThrowStatementTranslationUnit(ITranslationUnit expression)
        {
            return ExpressionStatementTranslationUnit.CreateThrow(expression as ExpressionTranslationUnit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ITranslationUnit BuildThrowStatementTranslationUnit()
        {
            return ExpressionStatementTranslationUnit.CreateThrow();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="references"></param>
        /// <returns></returns>
        public static ITranslationUnit BuildReferencesGroupTranslationUnit(string[] references)
        {
            var translationUnit = ReferencesGroupTranslationUnit.Create();

            foreach (var reference in references)
            {
                translationUnit.AddStatement(ReferenceTranslationUnit.Create(reference));
            }

            return translationUnit;
        }

        #endregion
    }
}
