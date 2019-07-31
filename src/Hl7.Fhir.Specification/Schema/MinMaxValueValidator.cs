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

    internal static class MinMaxValueConfigurator
    {
        public static MinMaxValueValidator ToValidatable(this Element definition, MinMax elementName)
        {
            if (definition == null)
                throw new IncorrectElementDefinitionException("Not exptected");

            // Min/max are only defined for ordered types
            if (!definition.GetType().IsOrderedFhirType())
            {
                throw new IncorrectElementDefinitionException($"{elementName.GetLiteral()} was given in ElementDefinition, but type '{definition.TypeName}' is not an ordered type");
            }

            return new MinMaxValueValidator(definition, elementName);
        }
    }

    internal class MinMaxValueValidator : IValidatable
    {
        private readonly Element _value;
        private readonly MinMax _elementName;

        public MinMaxValueValidator(Element value, MinMax elementName)
        {
            _value = value;
            _elementName = elementName;
        }

        public OperationOutcome Validate(ITypedElement input, ValidationContext vc)
        {
            if (input == null) throw Error.ArgumentNull(nameof(input));

            var outcome = new OperationOutcome();

            if (_value != null)
                outcome.Add(validateMinMaxValue(_value, input, (_elementName == MinMax.MinValue ? -1 : 1), _elementName.GetLiteral()));

            return outcome;
        }

        private static OperationOutcome validateMinMaxValue(Element definition, ITypedElement instance,
                int comparisonOutcome, string elementName)
        {
            var outcome = new OperationOutcome();

            if (definition != null)
            {
                try
                {
                    var instanceValue = instance.GetComparableValue(definition.GetType());

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
            throw new NotImplementedException();
        }
    }
}
