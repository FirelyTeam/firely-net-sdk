/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;

#nullable enable

namespace Hl7.Fhir.Serialization;

public readonly struct ObjectValueDeserializationContext
{
    internal ObjectValueDeserializationContext(
        Base objectInstance,
        PathStack path,
        long lineNumber,
        long linePosition)
    {
        PathStack = path;
        ObjectInstance = objectInstance;
        LineNumber = lineNumber;
        LinePosition = linePosition;
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
    /// The approximate line number in the source data that is being deserialized.
    /// </summary>
    public long LineNumber { get; }

    /// <summary>
    /// The approximate line position in the source data that is being deserialized.
    /// </summary>
    public long LinePosition { get; }
}


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
        PropertyMapping propMapping)
    {
        PathStack = path;
        ObjectInstance = objectInstance;
        PropertyName = propertyName;
        LineNumber = lineNumber;
        LinePosition = linePosition;
        ElementMapping = propMapping;
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
        ClassMapping instanceMapping)
    {
        PathStack = path;
        LineNumber = lineNumber;
        LinePosition = linePosition;
        InstanceMapping = instanceMapping;
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
}