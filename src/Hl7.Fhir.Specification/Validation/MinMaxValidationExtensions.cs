/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Validation
{
    internal static class MinMaxValidationExtensions
    {
        internal static int Compare(IComparable instance, Element definition)
        {
            if (instance == null) throw Error.ArgumentNull(nameof(instance));
            if (definition == null) throw Error.ArgumentNull(nameof(definition));
            if (!(definition is Primitive || definition is Quantity)) throw Error.Argument(nameof(definition), "Must be Primitive or Quantity");
            if (definition is Primitive && ((Primitive)definition).ObjectValue == null) throw Error.ArgumentNull(nameof(definition));

            if (instance is Model.Primitives.PartialDateTime)
            {
                if (definition is FhirDateTime)
                    return instance.CompareTo(((FhirDateTime)definition).ToPartialDateTime());
                else if (definition is Date)
                    return instance.CompareTo(((Date)definition).ToPartialDateTime());
                else if (definition is Instant)
                    return instance.CompareTo(((Instant)definition).ToPartialDateTime());
            }

            else if (instance is Model.Primitives.PartialTime && definition is Time)
                return instance.CompareTo(((Time)definition).ToTime());

            else if (instance is decimal && definition is FhirDecimal)
                return instance.CompareTo(((FhirDecimal)definition).Value.Value);

            else if (instance is long && definition is Integer)
                return instance.CompareTo((long)((Integer)definition).Value.Value);

            else if (instance is string && definition is FhirString)
                return instance.CompareTo(((FhirString)definition).Value);

            else if (instance is Model.Primitives.Quantity && definition is Quantity)
                return instance.CompareTo(((Quantity)definition).ToQuantity());

            throw Error.NotSupported($"Value '{definition}' and instance value '{instance}' are of incompatible types and can not be compared");
        }

        internal static IComparable GetComparableValue(this IElementNavigator instance, Type expectedType)
        {
            if (expectedType == typeof(Model.Quantity))
            {
                var q = instance.ParseQuantity();
                // These checks should probably be somewhere else since it has nothing to do with parsing
                if (q.Comparator != null)
                    throw Error.NotSupported("Cannot interpret quantities with a comparison");
                if (q.Value == null)
                    throw Error.NotSupported("Cannot interpret quantities without a value");

                return new Model.Primitives.Quantity(q.Value.Value, q.Unit, q.System ?? Model.Primitives.Quantity.UCUM);
            }
            else if (instance.Value is IComparable)
                return (IComparable)instance.Value;
            else
                return null;
        }

        internal static OperationOutcome ValidateMinMaxValue(this Validator validator, ElementDefinition definition, IElementNavigator instance)
        {
            var outcome = new OperationOutcome();

            if(definition.MinValue != null)
                outcome.Add(validateMinMaxValue(validator, definition.MinValue, instance, -1, "MinValue"));

            if(definition.MaxValue != null)
                outcome.Add(validateMinMaxValue(validator, definition.MaxValue, instance, 1, "MaxValue"));

            return outcome;
        }

        private static OperationOutcome validateMinMaxValue(Validator me, Element definition, IElementNavigator instance,
                        int comparisonOutcome, string elementName)
        {
            var outcome = new OperationOutcome();

            if (definition != null)
            {
                // Min/max are only defined for ordered types
                if (definition.GetType().IsOrderedFhirType())
                {
                    try
                    {
                        var instanceValue = instance.GetComparableValue(definition.GetType());

                        if (instanceValue != null)
                        {
                            if (Compare(instanceValue, definition) == comparisonOutcome)
                            {
                                var label = comparisonOutcome == -1 ? "smaller than" :
                                                comparisonOutcome == 0 ? "equal to" :
                                                    "larger than";
                                var issue = comparisonOutcome == -1 ? Issue.CONTENT_ELEMENT_PRIMITIVE_VALUE_TOO_SMALL :
                                            Issue.CONTENT_ELEMENT_PRIMITIVE_VALUE_TOO_LARGE;

                                outcome.AddIssue($"Instance value '{instanceValue}' is {label} {elementName} '{definition}'", issue, instance);
                            }
                        }
                    }
                    catch (NotSupportedException ns)
                    {
                        outcome.AddIssue($"Comparing the instance against the {elementName} failed: {ns.Message}", Issue.UNSUPPORTED_MIN_MAX_QUANTITY, instance);
                    }
                }
                else
                    me.Trace(outcome, $"{elementName} was given in ElementDefinition, but type '{definition.TypeName}' is not an ordered type", Issue.PROFILE_ELEMENTDEF_MIN_MAX_USES_UNORDERED_TYPE, instance);
            }

            return outcome;
        }
    }
}
