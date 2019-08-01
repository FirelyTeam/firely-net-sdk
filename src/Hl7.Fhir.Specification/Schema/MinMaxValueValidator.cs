/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using Hl7.Fhir.Validation.Schema;
using Newtonsoft.Json.Linq;
using System;

namespace Hl7.Fhir.Specification.Schema
{
    internal enum MinMax
    {
        [EnumLiteral("MinValue"), Description("Minimum Value")]
        MinValue,
        [EnumLiteral("MaxValue"), Description("Maximum Value")]
        MaxValue
    }

    /*
     * Element Id	ElementDefinition.minValue[x]
     * Definition	The minimum allowed value for the element. The value is inclusive. This is allowed for the types date, dateTime, instant, time, decimal, integer, and Quantity.
     * 
     * Cardinality	0..1
     * Type	        date|dateTime|instant|time|decimal|integer|positiveInt|unsignedInt|Quantity
     * Comments	    Except for date/date/instant, the type of the minValue[x] SHALL be the same as the specified type of the element. For the date/dateTime/instant values, the type of minValue[x] SHALL be either the same, or a Duration which specifies a relative time limit to the current time. The duration value is positive, and is subtracted from the current clock to determine the minimum allowable value. A minimum value for a Quantity is interpreted as an canonical minimum - e.g. you cannot provide 100mg if the minimum value is 10g.
     */

    /*
     *  Element Id	ElementDefinition.maxValue[x]
     *  Definition	The maximum allowed value for the element. The value is inclusive. This is allowed for the types date, dateTime, instant, time, decimal, integer, and Quantity.
     *  
     *  Cardinality	0..1
     *  Type	    date|dateTime|instant|time|decimal|integer|positiveInt|unsignedInt|Quantity
     *  Comments	Except for date/date/instant, the type of the maxValue[x] SHALL be the same as the specified type of the element. For the date/dateTime/instant values, the type of maxValue[x] SHALL be either the same, or a Duration which specifies a relative time limit to the current time. The duration value is positive, and is added to the current clock to determine the maximum allowable value. A maximum value for a Quantity is interpreted as an canonical maximum - e.g. you cannot provide 10g if the maximum value is 50mg.
     */

    internal class MinMaxValueValidator : IAssertion, IValidatable
    {
        private readonly ITypedElement _value;
        private readonly MinMax _minMaxType;

        public MinMaxValueValidator(ITypedElement value, MinMax minMaxType)
        {
            _value = value ?? throw new IncorrectElementDefinitionException($"{nameof(value)} cannot be null");
            _minMaxType = minMaxType;

            // Min/max are only defined for ordered types
            if (!IsOrderedType(_value.InstanceType))
            {
                throw new IncorrectElementDefinitionException($"{value.Name} was given in ElementDefinition, but type '{value.InstanceType}' is not an ordered type");
            }
        }

        private static OperationOutcome validateMinMaxValue(ITypedElement definition, ITypedElement instance,
                int comparisonOutcome, string elementName)
        {
            var outcome = new OperationOutcome();

            if (definition != null)
            {
                try
                {
                    var instanceValue = instance.GetComparableValue(definition.InstanceType);

                    if (instanceValue != null)
                    {
                        if (MinMaxValidationExtensions.Compare(instanceValue, definition) == comparisonOutcome)
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

            return outcome;
        }

        Assertions IValidatable.Validate(ITypedElement input, ValidationContext vc)
        {
            if (input == null) throw Error.ArgumentNull(nameof(input));

            var outcome = new OperationOutcome();

            if (_value != null)
                outcome.Add(validateMinMaxValue(_value, input, (_minMaxType == MinMax.MinValue ? -1 : 1), _minMaxType.GetLiteral()));
            return Assertions.Failure;
            //return outcome;
        }

        /// <summary>
        /// TODO Validation: this should be altered and moved to a more generic place, and should be more sophisticated
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool IsOrderedType(string type)
        {
            switch (type)
            {
                case "date":
                case "dateTime":
                case "instant":
                case "time":
                case "decimal":
                case "integer":
                case "positiveInt":
                case "unsignedInt":
                case "Quantity": return true;
                default:
                    return false;
            }
        }

        public JToken ToJson()
        {
            throw new NotImplementedException();
        }
    }
}
