using Hl7.Fhir.ElementModel;
using System;

namespace Hl7.Fhir.Model;

/// <summary>
/// An element within a tree of typed FHIR data with also a parent element.
/// </summary>
/// <remarks>
/// This interface represents FHIR data as a tree of elements, including type information either present in
/// the instance or derived from fully aware of the FHIR definitions and types
/// </remarks>
[Obsolete("use PocoNode instead")]
public interface IScopedNode : ITypedElement;