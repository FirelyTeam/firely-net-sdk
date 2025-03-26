/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Validation;
using System;

#nullable enable

namespace Hl7.Fhir.Serialization;


/// <summary>
/// Contains contextual information for the property that is currently being deserialized and is passed
/// to delegate methods implementing parts of user-definable deserialization and validation logic.
/// </summary>
public readonly struct PropertyDeserializationContext
{
    internal PropertyDeserializationContext(
        Base objectInstance,
        Func<string> pathProducer,
        string propertyName,
        long? lineNumber,
        long? linePosition,
        PropertyMapping? propMapping,
        NarrativeValidationKind narrativeValidation
        )
    {
        PathProducer = pathProducer;
        ObjectInstance = objectInstance;
        PropertyName = propertyName;
        LineNumber = lineNumber;
        LinePosition = linePosition;
        ElementMapping = propMapping;
        NarrativeValidation = narrativeValidation;
    }

    /// <summary>
    /// The POCO this property is an element of.
    /// </summary>
    public Base ObjectInstance { get; }

    /// <summary>
    /// The property name for which an instance is currently being deserialized.
    /// </summary>
    public string PropertyName { get; }

    /// <summary>
    /// A function that returns the current instance location of the property being validated.
    /// </summary>
    public Func<string> PathProducer { get; }

    /// <summary>
    /// The approximate line number in the source data that is being deserialized.
    /// </summary>
    public long? LineNumber { get; }

    /// <summary>
    /// The approximate line position in the source data that is being deserialized.
    /// </summary>
    public long? LinePosition { get; }

    /// <summary>
    /// The metadata for the element that is currently being deserialized.
    /// </summary>
    public PropertyMapping? ElementMapping { get; }

    /// <summary>
    /// For performance reasons, validation of Xhtml again the rules specified in the FHIR
    /// specification for Narrative (http://hl7.org/fhir/narrative.html#2.4.0) is turned off by
    /// default. Set this property to any other value than <see cref="NarrativeValidationKind.None"/>
    /// to perform validation.
    /// </summary>
    public NarrativeValidationKind NarrativeValidation { get; } = NarrativeValidationKind.None;
}

/// <summary>
/// Contains contextual information for the instance that is currently being deserialized and is passed
/// to delegate methods implementing parts of user-definable deserialization and validation logic.
/// </summary>
public readonly struct InstanceDeserializationContext
{
    internal InstanceDeserializationContext(
        Func<string> pathProducer,
        long? lineNumber,
        long? linePosition,
        ClassMapping instanceMapping,
        NarrativeValidationKind narrativeValidation)
    {
        PathProducer = pathProducer;
        LineNumber = lineNumber;
        LinePosition = linePosition;
        InstanceMapping = instanceMapping;
        NarrativeValidation = narrativeValidation;
    }

    internal Func<string> PathProducer { get; }

    /// <summary>
    /// The approximate line number in the source data that is being deserialized.
    /// </summary>
    public long? LineNumber { get; }

    /// <summary>
    /// The approximate line position in the source data that is being deserialized.
    /// </summary>
    public long? LinePosition { get; }

    /// <summary>
    /// The metadata for the type of which the current property is part of.
    /// </summary>
    public ClassMapping InstanceMapping { get; }

    /// <summary>
    /// For performance reasons, validation of Xhtml again the rules specified in the FHIR
    /// specification for Narrative (http://hl7.org/fhir/narrative.html#2.4.0) is turned off by
    /// default. Set this property to any other value than <see cref="NarrativeValidationKind.None"/>
    /// to perform validation.
    /// </summary>
    public NarrativeValidationKind NarrativeValidation { get; } = NarrativeValidationKind.None;
}