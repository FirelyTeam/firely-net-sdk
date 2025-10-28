/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using System.Collections.Generic;

namespace Hl7.Fhir.Validation;

/// <summary>
/// A validator that will be run to validate values while deserializing a POCO.
/// </summary>
public interface IPocoValidator
{
    /// <summary>
    /// Implements validation logic to be run on a property value just before that value is used
    /// to initialize the property.
    /// </summary>
    /// <param name="name">Name of the property to validate, as specified in the FHIR specification.</param>
    /// <param name="propertyValue">The value for the property (can be Base or a List of Base).</param>
    /// <param name="propertyMapping">Metadata for the property to use for validation.</param>
    /// <param name="context">The current context of validation, like the path info and optional line positions.</param>
    /// <returns>Zero or more validation errors.</returns>
    /// <remarks>This validation is for Base/List of Base properties and is not supposed to handle validating a
    /// Primitive's Value/ObjectValue properties, which is done during <see cref="ValidateObject"/> instead.</remarks>
    IReadOnlyCollection<CodedValidationException> ValidateProperty(
        string name,
        object? propertyValue,
        PropertyMapping? propertyMapping,
        PocoValidationContext context);

    /// <summary>
    /// Implements validation logic to be run on a POCO instance.
    /// </summary>
    /// <param name="instance">The instance to be validated</param>
    /// <param name="classMapping">Metadata for the instance to use for validation.</param>
    /// <param name="context">The current context of validation, like the path info and optional line positions.</param>
    /// <returns>Zero or more validation errors.</returns>
    IReadOnlyCollection<CodedValidationException> ValidateObject(
        Base instance,
        ClassMapping classMapping,
        PocoValidationContext context);
}