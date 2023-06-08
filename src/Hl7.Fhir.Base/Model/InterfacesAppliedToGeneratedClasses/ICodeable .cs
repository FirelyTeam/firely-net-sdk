using Hl7.Fhir.Generation;

namespace Hl7.Fhir.Model
{
    [ApplyInterfaceToGeneratedClasses]
    public interface ICodeable
    {
        CodeableConcept Code { get; set; }
    }
}