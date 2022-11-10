/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Validation
{
    internal static class MinMaxValidationExtensions
    {
        internal static int Compare(IComparable instance, Element definition)
        {
            if (instance == null) throw Error.ArgumentNull(nameof(instance));
            if (definition == null) throw Error.ArgumentNull(nameof(definition));
            if (!(definition is PrimitiveType || definition is Quantity)) throw Error.Argument(nameof(definition), "Must be Primitive or Quantity");
            if (definition is PrimitiveType pr && pr.ObjectValue == null) throw Error.ArgumentNull(nameof(definition));

            if (instance is P.DateTime)
            {
                if (definition is FhirDateTime fdt)
                    return instance.CompareTo(fdt.Value is { } ? P.DateTime.Parse(fdt.Value) : null);
                else if (definition is Date d)
                    return instance.CompareTo(d.Value is { } ? P.DateTime.Parse(d.Value) : null);
                else if (definition is Instant ins)
                    return instance.CompareTo(ins.Value is { } ? P.DateTime.FromDateTimeOffset(ins.Value.Value) : null);
            }

            else if (instance is P.Time && definition is Time t)
                return instance.CompareTo(t.Value is { } ? P.Time.Parse(t.Value) : null);
            else if (instance is P.Date && definition is Date dt)
                return instance.CompareTo(dt.Value is { } ? P.Date.Parse(dt.Value) : null);

            else if (instance is decimal && definition is FhirDecimal d)
                return instance.CompareTo(d.Value.Value);

            else if (instance is long && definition is Integer i)
                return instance.CompareTo((long)i.Value.Value);
            else if (instance is long && definition is PositiveInt pi)
                return instance.CompareTo((long)pi.Value.Value);
            else if (instance is long && definition is UnsignedInt ui)
                return instance.CompareTo((long)ui.Value.Value);

            else if (instance is int && definition is Integer i32)
                return instance.CompareTo(i32.Value.Value);
            else if (instance is int && definition is PositiveInt pi32)
                return instance.CompareTo(pi32.Value.Value);
            else if (instance is int && definition is UnsignedInt ui32)
                return instance.CompareTo(ui32.Value.Value);


            else if (instance is string && definition is FhirString fs)
                return instance.CompareTo(fs.Value);

            else if (instance is P.Quantity && definition is Quantity q)
                return instance.CompareTo(q.ToQuantity());

            throw Error.NotSupported($"Value '{definition}' and instance value '{instance}' are of incompatible types and can not be compared");
        }

        internal static IComparable GetComparableValue(this ITypedElement instance, Type expectedType)
        {
            if (expectedType == typeof(Model.Quantity))
            {
                var q = instance.ParseQuantity();
                // These checks should probably be somewhere else since it has nothing to do with parsing
                if (q.Comparator != null)
                    throw Error.NotSupported("Cannot interpret quantities with a comparison");
                if (q.Value == null)
                    throw Error.NotSupported("Cannot interpret quantities without a value");
                if (q.System != P.Quantity.UCUM)
                    throw Error.NotSupported("Cannot compare quantities other than those from UCUM");

                return new P.Quantity(q.Value.Value, q.Unit);
            }
            else if (instance.Value is IComparable ic)
                return ic;
            else
                return null;
        }

        internal static OperationOutcome ValidateMinMaxValue(this Validator validator, ElementDefinition definition, ITypedElement instance)
        {
            var outcome = new OperationOutcome();

            if (definition.MinValue != null)
                outcome.Add(validateMinMaxValue(validator, definition.MinValue, instance, -1, "MinValue"));

            if (definition.MaxValue != null)
                outcome.Add(validateMinMaxValue(validator, definition.MaxValue, instance, 1, "MaxValue"));

            return outcome;
        }

        private static OperationOutcome validateMinMaxValue(Validator me, Element definition, ITypedElement instance,
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
