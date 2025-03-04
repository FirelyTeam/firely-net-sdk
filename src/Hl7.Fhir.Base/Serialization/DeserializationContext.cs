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
        PathStack path,
        string propertyName,
        long lineNumber,
        long linePosition,
        PropertyMapping propMapping,
        NarrativeValidationKind narrativeValidation
        )
    {
        PathStack = path;
        ObjectInstance = objectInstance;
        PropertyName = propertyName;
        LineNumber = lineNumber;
        LinePosition = linePosition;
        ElementMapping = propMapping;
        NarrativeValidation = narrativeValidation;
    }

    internal PathStack PathStack { get; }

    /// <summary>
    /// The dotted path leading to this element from the root (has no indexers and includes the value virtual property on primitives)
    /// </summary>
    public string Path => PathStack.GetPath();

    /// <summary>
    /// The POCO this property is an element of.
    /// </summary>
    public Base ObjectInstance { get; }

    /// <summary>
    /// The property name for which an instance is currently being deserialized.
    /// </summary>
    public string PropertyName { get; }

    /// <summary>
    /// The approximate line number in the source data that is being deserialized.
    /// </summary>
    public long LineNumber { get; }

    /// <summary>
    /// The approximate line position in the source data that is being deserialized.
    /// </summary>
    public long LinePosition { get; }

    /// <summary>
    /// The metadata for the element that is currently being deserialized.
    /// </summary>
    public PropertyMapping ElementMapping { get; }

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
        PathStack path,
        long lineNumber,
        long linePosition,
        ClassMapping instanceMapping,
        NarrativeValidationKind narrativeValidation)
    {
        PathStack = path;
        LineNumber = lineNumber;
        LinePosition = linePosition;
        InstanceMapping = instanceMapping;
        NarrativeValidation = narrativeValidation;
    }

    internal PathStack PathStack { get; }

    /// <summary>
    /// The dotted FhirPath path leading to this element from the root.
    /// </summary>
    public string Path => PathStack.GetPath();

    /// <summary>
    /// The approximate line number in the source data that is being deserialized.
    /// </summary>
    public long LineNumber { get; }

    /// <summary>
    /// The approximate line position in the source data that is being deserialized.
    /// </summary>
    public long LinePosition { get; }

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