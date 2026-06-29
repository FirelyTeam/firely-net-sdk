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
    /// <remarks>
    /// The set of issues reported in Strict mode may expand across releases as new validations are added.
    /// Do not rely on a fixed set of error codes being raised in this mode.
    /// </remarks>
    Strict,

    /// <summary>
    /// In this mode, the deserializer will ignore <see cref="Recoverable"/> errors, but will still report
    /// errors that would cause data to end up in overflow (such as unknown elements or type mismatches).
    /// This means that if deserialization succeeds without throwing, all properties and primitive <c>Value</c>
    /// properties are guaranteed not to throw, and <see cref="Base.HasOverflow"/> is guaranteed to be
    /// <c>false</c> on the returned POCO.
    /// </summary>
    /// <remarks>
    /// Note that overflow data is still captured during parsing even in this mode. If an overflow-causing
    /// error is encountered, an exception is thrown, but the partial result — including any overflow — can
    /// be retrieved from the exception. Use <see cref="Recoverable"/> or <see cref="BackwardsCompatible"/>
    /// if you want unknown elements to be silently accepted into overflow without throwing.
    /// </remarks>
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