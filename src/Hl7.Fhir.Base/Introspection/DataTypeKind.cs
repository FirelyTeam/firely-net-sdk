#nullable enable
namespace Hl7.Fhir.Introspection;

/// <summary>
/// The kind of datatype a ClassMapping represents.
/// </summary>
public enum DataTypeKind
{
    /// <summary>
    /// A FHIR Resource, e.g. Patient, Observation
    /// </summary>
    Resource,

    /// <summary>
    /// A named FHIR Complex type, e.g. HumanName, Address.
    /// </summary>
    Complex,

    /// <summary>
    /// A FHIR Primitive type, e.g. string, integer.
    /// </summary>
    /// <remarks>These are complex types with a primitive value.</remarks>
    Primitive
}