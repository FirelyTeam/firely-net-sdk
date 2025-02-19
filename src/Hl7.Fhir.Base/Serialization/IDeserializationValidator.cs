/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Validation;

namespace Hl7.Fhir.Serialization;

/// <summary>
/// A validator that will be run to validate values while deserializing a POCO.
/// </summary>
public interface IDeserializationValidator
{
    /// <summary>
    /// Implements validation logic to be run on a property value just before that value is used
    /// to initialize the property.
    /// </summary>
    /// <param name="propertyValue">The value for the property (can be Base or a List of Base).</param>
    /// <param name="context">The current context of deserialization, like the path and the type under deserialization.</param>
    /// <param name="reportedErrors">null, zero or more validation errors which will be aggregated in the final result of deserialization.</param>
    /// <remarks>This validation will not be called on Primitive's Value/ObjectValue properties, which is done during <see cref="ValidateInstance"/>
    /// instead.</remarks>
    void ValidateProperty(
        object? propertyValue,
        in PropertyDeserializationContext context,
        out CodedValidationException[]? reportedErrors);

    /// <summary>
    /// Implements validation logic to be run on a deserialized instance.
    /// </summary>
    /// <param name="instance">The instance to be validated</param>
    /// <param name="context">The current context of deserialization, like the path and the type under deserialization.</param>
    /// <param name="reportedErrors">null, zero or more validation errors which will be aggregated in the final result of deserialization.</param>
    void ValidateInstance(
        Base instance,
        in InstanceDeserializationContext context,
        out CodedValidationException[]? reportedErrors);
}