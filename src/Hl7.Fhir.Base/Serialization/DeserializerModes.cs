#nullable enable
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Serialization;

/// <summary>
/// Enumerates the modes with which a deserializer can be configured. When a deserializer is configured with a specific mode,
/// it will not throw (on Deserialize) or return false (on TryDeserialize) when certain classes of error are encountered.
/// </summary>
/// <remarks>In any other mode that <see cref="Strict"/>, the list of errors reported by the deserializer will be incomplete,
/// as ignored errors are not reported or even stopped from being detected at all.</remarks>
public enum DeserializationMode
{
    /// <summary>
    /// Do not ignore any errors (default behaviour for most implementations). Will report all errors.
    /// </summary>
    Strict,

    /// <summary>
    /// In this mode, the deserializer will ignore <see cref="Recoverable"/> errors, as long as the data can
    /// be captured in the POCO model without using "overflow". This means that in this mode, after deserialization,
    /// all properties and primitive `Value` properties are guaranteed not to throw because of incorrect data.
    /// Also, no unknown elements will be not be allowed, so <see cref="Base.Overflow"/> can be ignored.
    /// </summary>
    NoOverflow,

    /// <summary>
    /// Less strict that <see cref="NoOverflow" />, this will ignore all errors as long as all data was captured
    /// the POCO model and overflow, even if the syntax or the data type was not fully FHIR compliant.
    /// </summary>
    Recoverable,

    /// <summary>
    /// An issue is allowable for backwards compatibility if it could be caused because an older parser encounters data
    /// coming from a newer FHIR release. This means allowing unknown elements, attributes, codes and types in a choice element.
    /// Note that this means data could end up in the overflow, and property access may throw.
    /// </summary>
    BackwardsCompatible,

    /// <summary>
    /// An issue is a syntax issue when it is raised by the parsing phase, and is caused by a mistake in the syntax rules
    /// for FHIR xml and json. These issues, once parsed, are not reflected in the POCOs and are not part of model validation.
    /// </summary>
    SyntaxOnly,

    /// <summary>
    /// Ignore all errors. Deserialization will never throw or return false. Overflow might be in use.
    /// </summary>
    Ostrich,
}