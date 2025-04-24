#nullable enable
namespace Hl7.Fhir.Serialization;

/// <summary>
/// Enumerates the modes with which a deserializer can be configured
/// </summary>
public enum DeserializationMode
{
    /// <summary>
    /// Do not ignore any errors (default behaviour for most implementations)
    /// </summary>
    Strict,

    /// <summary>
    /// An issue is allowable for backwards compatibility if it could be caused because an older parser encounters data coming from a newer
    /// FHIR release. This means allowing unknown elements, attributes, codes and types in a choice element.
    /// </summary>
    BackwardsCompatible,

    /// <summary>
    /// An issue is a syntax issue when it is caused by a mistake in the FHIR rules for the use of xml and json.
    /// These issues, once parsed, are not reflected in the POCOs.
    /// </summary>
    SyntaxOnly,

    /// <summary>
    /// An issue is recoverable if all data present in the parsed data could be retrieved and
    /// captured in the POCO model, even if the syntax or the data was not fully FHIR compliant.
    /// </summary>
    Recoverable,

    /// <summary>
    /// Ignore all errors.
    /// </summary>
    Ostrich,
}