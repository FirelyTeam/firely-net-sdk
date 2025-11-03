/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using COVE = Hl7.Fhir.Validation.CodedValidationException;

#nullable enable

namespace Hl7.Fhir.Introspection;

/// <summary>
/// Validates the type of a property against the allowed type choices.
/// </summary>
[CLSCompliant(false)]
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class AllowedTypesAttribute(params Type[] types) : ValidatingFhirModelAttribute
{
    public AllowedTypesAttribute(bool openChoice) : this()
    {
        OpenChoice = openChoice;
    }
    
    public AllowedTypesAttribute(Type type) : this([type]) { }
    
    public bool OpenChoice { get; set; }

    /// <summary>
    /// The list of types that are allowed for the instance.
    /// </summary>
    public Type[]? Types { get; } = types;

    /// <inheritdoc />
    public override IReadOnlyCollection<CodedValidationException> Validate(object? value, PocoValidationContext validationContext)
    {
        if (value is null) return [];

        IReadOnlyCollection<CodedValidationException> result = [];

        if (value is IReadOnlyCollection<Base> list)
        {
            foreach (var item in list)
            {
                result = validateValue(item, validationContext);
                if (result.Any()) break;
            }
        }
        else
        {
            result = validateValue(value, validationContext);
        }

        return result;
    }

    private IReadOnlyCollection<CodedValidationException> validateValue(object? item, PocoValidationContext context) =>
        Types switch
        {
            { Length: > 1 } when item is not null && !Types.Any(t => t.IsInstanceOfType(item)) =>
                [COVE.CHOICE_TYPE_NOT_ALLOWED(context, COVE.FhirTypeNameForObject(item))],
            { Length: 1 } when !Types[0].IsInstanceOfType(item) =>
                [COVE.FromTypes(Types[0], item, context)],
            _ when this.OpenChoice => !context.ModelInspector.OpenTypes.Any(t => t.IsInstanceOfType(item) && item is not IDynamicType) 
                ? [COVE.CHOICE_TYPE_NOT_ALLOWED(context, COVE.FhirTypeNameForObject(item))]
                : [],
            _ => []
        };

}