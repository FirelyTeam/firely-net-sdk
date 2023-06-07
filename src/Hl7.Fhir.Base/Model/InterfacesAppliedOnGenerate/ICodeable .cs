using Hl7.Fhir.Generation;

namespace Hl7.Fhir.Model
{
    [ApplyInterfaceToClassesOnGenerate]
    public interface ICodeable
    {
        CodeableConcept Code { get; set; }
    }
}