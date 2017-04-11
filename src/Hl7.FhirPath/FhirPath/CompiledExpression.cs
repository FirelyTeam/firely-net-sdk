using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.FhirPath.Parser;
using Hl7.FhirPath;
using Hl7.FhirPath.Expressions;
using Sprache;
using System.Text;
using System.Threading.Tasks;
using Hl7.FhirPath.Functions;
using Hl7.Fhir.ElementModel;

namespace Hl7.FhirPath
{
    public delegate IEnumerable<IElementNavigator> CompiledExpression(IElementNavigator root, IElementNavigator containerResource);

    public static class CompiledExpressionExtensions
    {
        public static object Scalar(this CompiledExpression evaluator, IElementNavigator input, IElementNavigator resource)
        {
            var result = evaluator(input, resource);
            if (result.Any())
                return evaluator(input, resource).Single().Value;
            else
                return null;
        }

        // For predicates, Empty is considered true
        public static bool Predicate(this CompiledExpression evaluator, IElementNavigator input, IElementNavigator resource)
        {
            var result = evaluator(input, resource).BooleanEval();

            if (result == null)
                return true;
            else
                return result.Value;
        }

        public static bool IsBoolean(this CompiledExpression evaluator, bool value, IElementNavigator input, IElementNavigator resource)
        {
            var result = evaluator(input, resource).BooleanEval();

            if (result == null)
                return false;
            else
                return result.Value == value;
        }
    }


}