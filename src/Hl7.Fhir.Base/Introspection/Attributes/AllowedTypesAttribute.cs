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
/// <remarks>The allowed types can be given as .NET types (<see cref="Types"/>), as FHIR type
/// names (<see cref="TypeNames"/>), or both. The two lists are alternatives: a value is allowed
/// when its .NET type matches one of <see cref="Types"/> (which takes inheritance into account),
/// or when it resolves to a mapping whose name (or canonical) is listed in
/// <see cref="TypeNames"/>. Note that custom types can only be matched by name: all custom types
/// share the same dynamic .NET type, so a dynamic type listed in <see cref="Types"/> will never
/// match.</remarks>
[CLSCompliant(false)]
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class AllowedTypesAttribute(params Type[] types) : ValidatingFhirModelAttribute
{
    public AllowedTypesAttribute(bool openChoice) : this()
    {
        OpenChoice = openChoice;
    }

    public AllowedTypesAttribute(Type type) : this([type]) { }

    /// <summary>
    /// Creates an attribute that validates against a list of allowed FHIR type names, rather
    /// than .NET types. This is needed for custom types: these all share the same dynamic .NET
    /// type, so a list of .NET types cannot faithfully express such choices.
    /// </summary>
    public AllowedTypesAttribute(string[] typeNames) : this(types: [])
    {
        TypeNames = typeNames;
    }

    /// <summary>
    /// Creates an attribute that validates against allowed types given both as .NET types and
    /// as FHIR type names. The lists are alternatives: a value is allowed when it matches
    /// either of them (see the remarks on this class).
    /// </summary>
    public AllowedTypesAttribute(Type[] types, string[] typeNames) : this(types)
    {
        TypeNames = typeNames;
    }

    public bool OpenChoice { get; set; }

    /// <summary>
    /// The list of types that are allowed for the instance.
    /// </summary>
    public Type[]? Types { get; } = types;

    /// <summary>
    /// The list of FHIR type names (as registered with the <see cref="ModelInspector"/>) that are
    /// allowed for the instance. A mapping's canonical is accepted as well.
    /// </summary>
    /// <remarks>Matching is by exact mapping name: unlike <see cref="Types"/>, name-based
    /// matching does not take subtyping into account.</remarks>
    public string[]? TypeNames { get; }

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

    private IReadOnlyCollection<CodedValidationException> validateValue(object? item, PocoValidationContext context)
    {
        // An attribute without any constraints (no types, no type names, not an open choice)
        // validates nothing.
        if (Types is not { Length: > 0 } && TypeNames is not { Length: > 0 } && !OpenChoice) return [];

        if (item is null || isAllowedType(item, context)) return [];

        // For a single-type constraint we report a type mismatch (just like the .NET type-based
        // validation always did), everything else reports a non-allowed choice.
        return Types is { Length: 1 } && TypeNames is null
            ? [COVE.FromTypes(Types[0], item, context)]
            : [COVE.CHOICE_TYPE_NOT_ALLOWED(context, COVE.FhirTypeNameForObject(item))];
    }

    private bool isAllowedType(object item, PocoValidationContext context)
    {
        // The .NET type-based check. Note that a dynamic instance matching a dynamic type in the
        // list does not establish anything (all custom types share the same dynamic .NET type),
        // so those matches do not count - dynamic instances are matched by name below.
        if (Types is { Length: > 0 } &&
            Types.Any(t => t.IsInstanceOfType(item) &&
                           (item is not IDynamicType || !typeof(IDynamicType).IsAssignableFrom(t))))
            return true;

        // The name-based check: resolve the instance to its mapping (for dynamic instances this
        // uses their DynamicTypeName) and match the mapping's name or canonical.
        if (TypeNames is { Length: > 0 } && item is Base b &&
            context.ModelInspector.FindClassMapping(b) is { } mapping &&
            (TypeNames.Contains(mapping.Name) ||
             (mapping.Canonical is { } canonical && TypeNames.Contains(canonical))))
            return true;

        if (OpenChoice)
        {
            // An open choice allows any of the model's open types. A dynamic instance is allowed
            // when it resolves to a custom type that was registered with the inspector.
            return item is IDynamicType
                ? item is Base d && context.ModelInspector.FindClassMapping(d) is { IsCustomMapping: true }
                : context.ModelInspector.OpenTypes.Any(t => t.IsInstanceOfType(item));
        }

        return false;
    }
}