using Hl7.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Hl7.FluentPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Validation
{
    internal static class MinMaxValidationExtensions
    {
        public static Hl7.FluentPath.Quantity ParseQuantity(this IElementNavigator instance)
        {
            var value = instance.GetChildrenByName("value").SingleOrDefault()?.Value as decimal?;
            var comp = instance.GetChildrenByName("comparator").SingleOrDefault()?.Value as string;
            var system = instance.GetChildrenByName("system").SingleOrDefault()?.Value as string;
            var code = instance.GetChildrenByName("code").SingleOrDefault()?.Value as string;

            if (comp != null)
                throw Error.NotSupported("Cannot interpret quantities with a comparison");
            if (value == null)
                throw Error.NotSupported("Cannot interpret quantities without a value");

            return new Hl7.FluentPath.Quantity(value.Value, code, system);
        }


        internal static int Compare(IComparable instance, Element definition)
        {
            if (instance == null) throw Error.ArgumentNull(nameof(instance));
            if (definition == null) throw Error.ArgumentNull(nameof(definition));
            if (!(definition is Primitive || definition is Model.Quantity)) throw Error.Argument(nameof(definition), "Must be Primitive or Quantity");
            if (definition is Primitive && ((Primitive)definition).ObjectValue == null) throw Error.ArgumentNull(nameof(definition));

            if (instance is PartialDateTime)
            {
                if (definition is FhirDateTime)
                    return instance.CompareTo(((FhirDateTime)definition).ToPartialDateTime());
                else if (definition is Date)
                    return instance.CompareTo(((Date)definition).ToPartialDateTime());
                else if (definition is Instant)
                    return instance.CompareTo(((Instant)definition).ToPartialDateTime());
            }

            else if (instance is Hl7.FluentPath.Time && definition is Model.Time)
                return instance.CompareTo(((Model.Time)definition).ToTime());

            else if (instance is decimal && definition is FhirDecimal)
                return instance.CompareTo(((FhirDecimal)definition).Value.Value);

            else if (instance is long && definition is Integer)
                return instance.CompareTo((long)((Integer)definition).Value.Value);

            else if (instance is string && definition is FhirString)
                return instance.CompareTo(((FhirString)definition).Value);

            else if (instance is Hl7.FluentPath.Quantity && definition is Model.Quantity)
                return instance.CompareTo(((Model.Quantity)definition).ToQuantity());

            throw Error.NotSupported($"Value '{definition}' and instance value '{instance}' are of incompatible types and can not be compared");
        }

        internal static IComparable GetComparableValue(this IElementNavigator instance)
        {
            if (instance.TypeName == "Quantity")
                return instance.ParseQuantity();
            else if (instance.Value is IComparable)
                return (IComparable)instance.Value;
            else
                return null;
        }

        internal static OperationOutcome ValidateMinMaxValue(this Validator validator, ElementDefinition definition, IElementNavigator instance)
        {
            var outcome = new OperationOutcome();

            if(definition.MinValue != null)
                outcome.Add(validateMinMaxValue(definition.MinValue, instance, -1, "MinValue"));

            if(definition.MaxValue != null)
                outcome.Add(validateMinMaxValue(definition.MaxValue, instance, 1, "MaxValue"));

            return outcome;
        }

        private static OperationOutcome validateMinMaxValue(Element definition, IElementNavigator instance,
                        int comparisonOutcome, string elementName)
        {
            var outcome = new OperationOutcome();

            if (definition != null)
            {
                // Min/max are only defined for ordered types
                if (outcome.Verify(() => definition.GetType().IsOrderedFhirType(),
                    $"{elementName} was given in ElementDefinition, but type '{definition.TypeName}' is not an ordered type", Issue.PROFILE_ELEMENTDEF_MIN_MAX_USES_UNORDERED_TYPE, instance))
                {
                    try
                    {
                        var instanceValue = instance.GetComparableValue();

                        if (instanceValue != null)
                        {
                            if (Compare(instanceValue, definition) == comparisonOutcome)
                            {
                                var label = comparisonOutcome == -1 ? "smaller than" :
                                                comparisonOutcome == 0 ? "equal to" :
                                                    "larger than";
                                var issue = comparisonOutcome == -1 ? Issue.CONTENT_ELEMENT_PRIMITIVE_VALUE_TOO_SMALL :
                                            Issue.CONTENT_ELEMENT_PRIMITIVE_VALUE_TOO_LARGE;

                                outcome.Info($"Instance value '{instanceValue}' is {label} {elementName} '{definition}'", issue, instance);
                            }
                        }
                    }
                    catch (NotSupportedException ns)
                    {
                        outcome.Info($"Comparing the instance against the {elementName} failed: {ns.Message}", Issue.UNSUPPORTED_MIN_MAX_QUANTITY, instance);
                    }
                }
            }

            return outcome;
        }
    }
}
