/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using COVE = Hl7.Fhir.Validation.CodedValidationException;

#nullable enable

namespace Hl7.Fhir.Validation
{
    /// <summary>
    /// Validates a List instance against the cardinality min/max rules.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class CardinalityAttribute : ValidationAttribute
    {
        public CardinalityAttribute()
        {
            Min = 0;
            Max = 1;
        }

        /// <summary>
        /// The minimum number of occurrences.
        /// </summary>
        public int Min { get; set; }

        /// <summary>
        /// The maximum number of occurences. Use <c>-1</c> for unlimited.
        /// </summary>
        public int Max { get; set; }

        /// <inheritdoc/>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return (Min == 0) ? ValidationResult.Success :
                    COVE.MANDATORY_ELEMENT_CANNOT_BE_NULL.AsResult(validationContext, Min);

            var count = 1;

            if (ReflectionHelper.IsRepeatingElement(value, out var list))
            {
                if (list.Cast<object>().Any(item => item is null))
                    return COVE.REPEATING_ELEMENT_CANNOT_CONTAIN_NULL.AsResult(validationContext);
                count = list.Count;
            }

            if (count < Min)
                return COVE.INCORRECT_CARDINALITY_MIN.AsResult(validationContext, count, Min);
            if (Max != -1 && count > Max)
                return COVE.INCORRECT_CARDINALITY_MAX.AsResult(validationContext, count, Max);

            return ValidationResult.Success;
        }
    }
}

#nullable restore