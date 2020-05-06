using Hl7.Fhir.Model;
using Hl7.Fhir.Validation.Model;

namespace Hl7.Fhir.Specification.Validation.Model
{
    internal class CodeableConceptMapper : ITransferMapper<CodeableConcept, UniCodeableConcept>
    {
        public static readonly CodeableConceptMapper Current = new CodeableConceptMapper();

        public void Transfer(MappingContext context, CodeableConcept source, UniCodeableConcept target)
        {
            target.Text = source.Text;
            target.Codings = source.Coding.Map(context, CodingMapper.Current);
        }

        public void Transfer(MappingContext context, UniCodeableConcept source, CodeableConcept target)
        {
            target.Text = source.Text;
            target.Coding = source.Codings.Map(context, CodingMapper.Current);
        }
    }
}
