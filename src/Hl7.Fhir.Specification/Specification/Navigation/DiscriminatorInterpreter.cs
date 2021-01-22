/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Language;
using Hl7.Fhir.Model;
using Hl7.FhirPath.Expressions;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Navigation
{

    /// <summary>
    /// This visitor interprets a parsed FhirPath AST as a discriminator expression
    /// (see http://hl7.org/fhir/profiling.html#discriminator).
    /// </summary>
    /// <remarks>Given a <see cref="StructureDefinitionWalker"/>, this interpreter will
    /// walk the definition tree based on the discriminator tree visited.</remarks>
    internal class DiscriminatorInterpreter : ExpressionVisitor<IEnumerable<StructureDefinitionWalker>>
    {
        public DiscriminatorInterpreter(in StructureDefinitionWalker root)
        {
            Root = root;
        }

        public override IEnumerable<StructureDefinitionWalker> VisitConstant(ConstantExpression expression, SymbolTable _) =>
            throw new DiscriminatorFormatException("Discriminator paths cannot contain constants.");

        /// <summary>
        /// Visit a function call appearing in a discriminator expression
        /// </summary>
        /// <remarks>May only be 'builtin.children', 'resolve' and 'extension'</remarks>
        public override IEnumerable<StructureDefinitionWalker> VisitFunctionCall(FunctionCallExpression call, SymbolTable scope)
        {
            var parentSet = call.Focus.Accept(this, scope);

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
                case "as": // 'as()' for backwards compatibility only
                case "ofType":
                    var type = getSingleStringParameter(call);
                    if (!ModelInfo.IsCoreModelType(type))
                        throw new DiscriminatorFormatException($"Type '{type}' passed to {call.FunctionName}() is not a known FHIR type.");
                    return parentSet.OfType(type);
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
                    if (ce.ExpressionType == TypeSpecifier.String)
                    {
                        return (string)ce.Value;
                    }
                }
            }

            throw new DiscriminatorFormatException($"Function '{call.FunctionName}' should be invoked with a single parameter or type string");
        }

        public override IEnumerable<StructureDefinitionWalker> VisitNewNodeListInit(NewNodeListInitExpression expression, SymbolTable scope) =>
            throw new DiscriminatorFormatException("The empty set constructor '{}', is not supported in discriminators.");

        public StructureDefinitionWalker Root { get; private set; }

        public override IEnumerable<StructureDefinitionWalker> VisitVariableRef(VariableRefExpression expression, SymbolTable scope)
        {
            if (expression.Name == "builtin.this")
                return new[] { Root };
            if (expression.Name == "builtin.that")
                return new[] { Root };

            throw new DiscriminatorFormatException($"Variable reference '{expression.Name}' is not supported in discriminators.");
        }
    }
}
