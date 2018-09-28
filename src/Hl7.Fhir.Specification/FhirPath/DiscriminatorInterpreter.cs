/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using Hl7.FhirPath;
using Hl7.FhirPath.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.FhirPath
{

    /// <summary>
    /// This visitor interprets a parsed FhirPath AST as a discriminator expression
    /// </summary>
    /// <remarks>Given a <see cref="ITypeSerializationInfo"/>, this interpreter will
    /// walk the definition tree and return
    /// a set of <see cref="ITypeSerializationInfo"/> representing the definitions
    /// selected by the discriminator expression.</remarks>
    internal class DiscriminatorInterpreter : ExpressionVisitor<ElementSchemaWalker>, IExceptionSource
    {
        public DiscriminatorInterpreter(ElementSchemaWalker root)
        {
            Root = root;
        }

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        public override ElementSchemaWalker VisitConstant(ConstantExpression expression)
        {
            throw Error.InvalidOperation("Internal error: VisitConstant() should never be invoked while walking the AST for a discriminator expression.");
        }

        /// <summary>
        /// Visit a function call appearing in a discriminator expression
        /// </summary>
        /// <remarks>May only be 'builtin.children', 'resolve' and 'extension'</remarks>
        public override ElementSchemaWalker VisitFunctionCall(FunctionCallExpression call)
        {
            var parentSet = call.Focus.Accept(this);

            if(call is ChildExpression childExpr)
                return parentSet.Children(childExpr.ChildName);

            switch (call.FunctionName)
            {
                case "resolve":
                    return verifyNoParams(call)
                        ? parentSet.Resolve()
                        : new ElementSchemaWalker();
                case "extension":
                    return tryGetSingleStringParameter(call, out var url)
                        ? parentSet.Extension(url)
                        : new ElementSchemaWalker();
                default:
                    raiseDiscriminatorFormat(call, $"Invocation of function '{call.FunctionName}' is not supported in discriminators.");
                    return new ElementSchemaWalker();
            }
        }

        private bool verifyNoParams(FunctionCallExpression call)
        {
            if (call.Arguments.Any())
            {
                raiseDiscriminatorFormat(call, $"Function '{call.FunctionName}' invocation should not have any parameters");
                return false;
            }

            return true;
        }

        private bool tryGetSingleStringParameter(FunctionCallExpression call, out string name)
        {
            if(call.Arguments.Count() == 1)
            {
                if(call.Arguments.Single() is ConstantExpression ce)
                {
                    if(ce.ExpressionType == Hl7.FhirPath.TypeInfo.String)
                    {
                        name = (string)ce.Value;
                        return true;
                    }
                }
            }

            raiseDiscriminatorFormat(call, $"Function '{call.FunctionName}' should be invoked with a single parameter or type string");
            name = null;
            return false;
        }

        private void raiseDiscriminatorFormat(object source, string message)
        {
            this.NotifyOrThrow(source, ExceptionNotification.Error(
                new DiscriminatorFormatException(message)));
        }

        public override ElementSchemaWalker VisitNewNodeListInit(NewNodeListInitExpression expression)
        {
            raiseDiscriminatorFormat(expression, "The empty set constructor '{}', is not supported in discriminators.");
            return new ElementSchemaWalker();
        }

        public ElementSchemaWalker Root { get; private set; }


        public override ElementSchemaWalker VisitVariableRef(VariableRefExpression expression)
        {
            if (expression.Name == "builtin.that")
                return Root;

            raiseDiscriminatorFormat(expression, $"Variable reference '{expression.Name}' is not supported in discriminators.");
            return new ElementSchemaWalker();
        }

        internal bool AssertIsSupportedRootExpression(Expression expr)
        {
            if (!(expr is FunctionCallExpression))
            {
                raiseDiscriminatorFormat(expr, $"Discriminators should be dotted paths with element selections.");
                return false;
            }

            return true;
        }
    }

    public static class DiscriminatorInterpeterExtensions
    {
        public static ElementSchemaWalker EvaluateDiscriminator(this Expression expr, ITypeSerializationInfo root, IExceptionSource ies = null)
        {
            var walker = new ElementSchemaWalker(root);
            return expr.EvaluateDiscriminator(walker, ies);
        }

        public static ElementSchemaWalker EvaluateDiscriminator(this Expression expr, ElementSchemaWalker walker, IExceptionSource ies = null)
        {
            var interpeter = new DiscriminatorInterpreter(walker);
            bool acceptable = interpeter.AssertIsSupportedRootExpression(expr);
            return acceptable ? expr.Accept(interpeter) : new ElementSchemaWalker();
        }

        public static ElementSchemaWalker Walk(this ElementSchemaWalker me, string discriminatorExpression)
        {
            var compiler = new FhirPathCompiler(new SymbolTable());
            var tree = compiler.Parse(discriminatorExpression);

            return tree.EvaluateDiscriminator(me);
        }

    }

}
