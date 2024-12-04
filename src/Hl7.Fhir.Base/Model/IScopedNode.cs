using Hl7.Fhir.ElementModel;

namespace Hl7.Fhir.Model;

/// <summary>
/// An element within a tree of typed FHIR data with also a parent element.
/// </summary>
/// <remarks>
/// This interface represents FHIR data as a tree of elements, including type information either present in
/// the instance or derived from fully aware of the FHIR definitions and types
/// </remarks>
#pragma warning disable CS0618 // Type or member is obsolete
public partial interface IScopedNode : ITypedElement, IShortPathGenerator
#pragma warning restore CS0618 // Type or member is obsolete
{
    
}