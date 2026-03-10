/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


#nullable enable

using Hl7.Fhir.Validation;

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// A validator that will be run to validate values while deserializing a POCO.
    /// </summary>
    public interface IDeserializationValidator
    {
        /// <summary>
        /// Implements validation logic to be run on a property value just before that value is used 
        /// to initialize the property.
        /// </summary>
        /// <param name="instance">The instance to be validated.</param>
        /// <param name="context">The current context of deserialization, like the path and the type under deserialization.</param>
        /// <param name="reportedErrors">null, zero or more validation errors which will be aggregated in the final result of deserialization.</param>
        void ValidateProperty(
            ref object? instance,
            in PropertyDeserializationContext context,
            out CodedValidationException[]? reportedErrors);

        /// <summary>
        /// Implements validation logic to be run on a deserialized instance.
        /// </summary>
        /// <param name="instance">The instance to be validated</param>
        /// <param name="context">The current context of deserialization, like the path and the type under deserialization.</param>
        /// <param name="reportedErrors">null, zero or more validation errors which will be aggregated in the final result of deserialization.</param>
        void ValidateInstance(
            object? instance,
            in InstanceDeserializationContext context,
            out CodedValidationException[]? reportedErrors);
    }
}

#nullable restore
