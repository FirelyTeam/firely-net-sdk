using Hl7.Fhir.Utility;

namespace Hl7.Fhir.ElementModel;

internal class TypedElementAnnotatedProvider(IAnnotated originalElement)
{
    internal IAnnotated OriginalElement { get; } = originalElement;
}