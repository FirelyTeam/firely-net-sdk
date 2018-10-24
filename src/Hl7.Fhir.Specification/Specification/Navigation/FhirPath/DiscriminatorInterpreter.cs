/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Utility;
using Hl7.FhirPath.Expressions;
using System.Linq;

namespace Hl7.Fhir.Specification.Navigation.FhirPath
{

    /// <summary>
    /// This visitor interprets a parsed FhirPath AST as a discriminator expression
    /// </summary>
    /// <remarks>Given a <see cref="StructureDefinitionSchemaWalker"/>, this interpreter will
    /// walk the definition tree based on the discriminator tree visited.</remarks>
    internal class DiscriminatorInterpreter : ExpressionVisitor<StructureDefinitionSchemaWalker>
    {
        public DiscriminatorInterpreter(StructureDefinitionSchemaWalker root)
        {
            Root = root ?? throw Error.ArgumentNull(nameof(root));
        }

        public override StructureDefinitionSchemaWalker VisitConstant(ConstantExpression expression) =>
            throw Error.InvalidOperation("Internal error: VisitConstant() should never be invoked while walking the AST for a discriminator expression.");

        /// <summary>
        /// Visit a function call appearing in a discriminator expression
        /// </summary>
        /// <remarks>May only be 'builtin.children', 'resolve' and 'extension'</remarks>
        public override StructureDefinitionSchemaWalker VisitFunctionCall(FunctionCallExpression call)
        {
            var parentSet = call.Focus.Accept(this);

            if (call is ChildExpression childExpr)
                return parentSet.Child(childExpr.ChildName);

            switch (call.FunctionName)
            {
                case "resolve":
                    verifyNoParams(call);
                    return parentSet.Resolve();
                case "extension":
                    var url = getSingleStringParameter(call);
                    return parentSet.Extension(url);
                case "ofType":
                    var type = getSingleStringParameter(call);
                    return parentSet.OfType(type);
                case "slice":
                    var name = getSingleStringParameter(call);
                    return parentSet.Slice(name);
                default:
                    throw new DiscriminatorFormatException($"Invocation of function '{call.FunctionName}' is not supported in discriminators.");
            }
        }

        private void verifyNoParams(FunctionCallExpression call)
        {
            if (call.Arguments.Any())
                throw new DiscriminatorFormatException($"Function '{call.FunctionName}' invocation should not have any parameters");
        }

        private string getSingleStringParameter(FunctionCallExpression call)
        {
            if (call.Arguments.Count() == 1)
            {
                if (call.Arguments.Single() is ConstantExpression ce)
                {
                    if (ce.ExpressionType == Hl7.FhirPath.TypeInfo.String)
                    {
                        return (string)ce.Value;
                    }
                }
            }

            throw new DiscriminatorFormatException($"Function '{call.FunctionName}' should be invoked with a single parameter or type string");
        }

        public override StructureDefinitionSchemaWalker VisitNewNodeListInit(NewNodeListInitExpression expression) =>
            throw new DiscriminatorFormatException("The empty set constructor '{}', is not supported in discriminators.");

        public StructureDefinitionSchemaWalker Root { get; private set; }


        public override StructureDefinitionSchemaWalker VisitVariableRef(VariableRefExpression expression)
        {
            if (expression.Name == "builtin.that")
                return Root;

            throw new DiscriminatorFormatException($"Variable reference '{expression.Name}' is not supported in discriminators.");
        }

        internal void AssertSupportedRootExpression(Expression expr)
        {
            if (!(expr is FunctionCallExpression))
                throw new DiscriminatorFormatException($"Discriminators should be dotted paths with element selections.");
        }
    }
}
