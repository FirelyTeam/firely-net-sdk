using Hl7.Fhir.Model;

namespace Hl7.Fhir.Specification.Tests;

public class TestElementBaseAnnotation(ElementDefinition baseElemDef)
{
    public ElementDefinition BaseElementDefinition { get; } = baseElemDef;
}