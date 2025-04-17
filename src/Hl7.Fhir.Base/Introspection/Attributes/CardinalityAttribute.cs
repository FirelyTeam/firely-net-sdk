/*
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using COVE = Hl7.Fhir.Validation.CodedValidationException;

#nullable enable

namespace Hl7.Fhir.Introspection;

/// <summary>
/// Validates a List instance against the cardinality min/max rules.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class CardinalityAttribute : ValidatingFhirModelAttribute
{
    /// <summary>
    /// The minimum number of occurrences.
    /// </summary>
    public int Min { get; set; } = 0;

    /// <summary>
    /// The maximum number of occurences. Use <c>-1</c> for unlimited.
    /// </summary>
    public int Max { get; set; } = 1;

    /// <inheritdoc/>
    public override IReadOnlyCollection<CodedValidationException> Validate(object? value, PocoValidationContext validationContext)
    {
        if (value is null)
            return (Min == 0) ? [] :
                [COVE.MANDATORY_ELEMENT_MUST_BE_PRESENT(validationContext, validationContext.PathProducer(), Min)];

        var count = 1;

        if (ReflectionHelper.IsRepeatingElement(value, out var list))
        {
            if (list.Cast<object>().Any(item => item is null))
                return [COVE.REPEATING_ELEMENT_CANNOT_CONTAIN_NULL(validationContext)];
            count = list.Count;
        }

        if (count < Min)
            return [COVE.INCORRECT_CARDINALITY_MIN(validationContext, count, Min)];
        if (Max != -1 && count > Max)
            return [COVE.INCORRECT_CARDINALITY_MAX(validationContext, count, Max)];

        return [];
    }
}