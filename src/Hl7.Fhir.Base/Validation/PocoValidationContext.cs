/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using System;

#nullable enable

namespace Hl7.Fhir.Validation;


/// <summary>
/// Contains contextual information for the property that is currently being deserialized and is passed
/// to delegate methods implementing parts of user-definable deserialization and validation logic.
/// </summary>
public record PocoValidationContext
{
    public PocoValidationContext(Base objectInstance, ModelInspector modelInspector, Func<string> pathProducer, long? lineNumber, long? linePosition, NarrativeValidationKind narrativeValidation)
    {
        ObjectInstance = objectInstance;
        PathProducer = pathProducer;
        LineNumber = lineNumber;
        LinePosition = linePosition;
        NarrativeValidation = narrativeValidation; 
        ModelInspector = modelInspector;
    }

    /// <summary>
    /// The ModelInspector from which we will get the model information use while doing validation.
    /// </summary>
    public ModelInspector ModelInspector { get; set; }

    /// <summary>
    /// Name of the property being validated.
    /// </summary>
    public string? MemberName { get; set; }

    /// <summary>
    /// In the context of property validation this is the POCO this property is an element of. In the context
    /// of validating an object, this should be the same as the object passed to the validation function.
    /// </summary>
    public Base ObjectInstance { get; init; }

    /// <summary>
    /// A function that returns the current instance location of the property being validated.
    /// </summary>
    public Func<string> PathProducer { get; init; }

    /// <summary>
    /// The approximate line number in the source data that is being deserialized.
    /// </summary>
    public long? LineNumber { get; init; }

    /// <summary>
    /// The approximate line position in the source data that is being deserialized.
    /// </summary>
    public long? LinePosition { get; init; }

    /// <summary>
    /// For performance reasons, validation of Xhtml again the rules specified in the FHIR
    /// specification for Narrative (http://hl7.org/fhir/narrative.html#2.4.0) is turned off by
    /// default. Set this property to any other value than <see cref="NarrativeValidationKind.None"/>
    /// to perform validation.
    /// </summary>
    public NarrativeValidationKind NarrativeValidation { get; init; } = NarrativeValidationKind.None;

    /// <summary>
    /// When calling validation on an object, validate the object and the attributes on its properties,
    /// but do not validate the property values themselves, effectively stopping validation from recursing
    /// deeper into the object tree.
    /// </summary>
    public bool ValidateObjectOnly { get; init; }
}