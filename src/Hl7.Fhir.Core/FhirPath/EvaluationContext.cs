/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.FhirPath
{
    // TODO: Need to keep track of related contexts
    // $context - the original context (see below for usage)
    // $resource - the original container resource(e.g.skip contained resources, but do not go past a root resource into a bundle, if it is contained in a bundle)
	// $parent - the element that contains $context
    // $focus - a reference to the current focus.This is useful where you need to refer to the focus in a function parameter (e.g. $focus in criteria)
    public class EvaluationContext
    {
        // [WMR] Store variables as Memo<T>, i.e. evaluate expression and save result on first call
        // Polymorphic collection - each variable has it's own value type T
        // Important: each Evaluator instance should receive a new EvaluationContext instance - for variable scoping
        // => Copy ctor

        public IEnumerable<IFhirPathValue> Focus { get; private set; }

        private EvaluationContext()
        {
            // Must use factory methods
        }

        public static EvaluationContext NewContext(EvaluationContext parent, params IFhirPathValue[] values)
        {
            if (values == null) throw Error.ArgumentNull("values");

            return NewContext(parent, (IEnumerable<IFhirPathValue>)values);
        }

        public static EvaluationContext NewContext(EvaluationContext parent, object value)
        {
            if (value == null) throw Error.ArgumentNull("value");

            return NewContext(parent, new ConstantFhirPathNode(value));
        }

        public static EvaluationContext NewContext(EvaluationContext parent, IEnumerable<IFhirPathValue> values)
        {
            if (values == null) throw Error.ArgumentNull("values");

            //copy stuff from parent
            return new EvaluationContext() { Focus = values };
        }


    }

    public static class FocusExtensions
    {
        public static IEnumerable<IFhirPathElement> JustFhirPathElements(this IEnumerable<IFhirPathValue> focus)
        {
            return focus.OfType<IFhirPathElement>();
        }

        public static bool AsBooleanEvaluation(this IEnumerable<IFhirPathValue> focus)
        {
            // An empty result is considered "false"
            if (!focus.Any()) return false;

            // A result that looks like a single boolean should be interpreted as a boolean
            if (focus.Count() == 1)
            {
                try
                {
                    return PrimitiveTypeConverter.ConvertTo<bool>(focus.Single().ObjectValue);
                }
                catch
                {
                    return true;
                }
            }

            // Otherwise, we have "some" content, which we'll consider "true"
            return true;
        }
    }
}
