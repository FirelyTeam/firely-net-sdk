using Hl7.Fhir.Model;
using Hl7.Fhir.Validation.Model;

namespace Hl7.Fhir.Specification.Validation.Model
{
    internal class ContactDetailMapper : ITransferMapper<ContactDetail, UniContactDetail>
    {
        public static readonly ContactDetailMapper Current = new ContactDetailMapper();

        public void Transfer(MappingContext context, ContactDetail source, UniContactDetail target)
        {
            target.Name = source.Name;
            target.Telecoms = source.Telecom.Map(context, ContactPointMapper.Current);
        }

        public void Transfer(MappingContext context, UniContactDetail source, ContactDetail target)
        {
            target.Name = source.Name;
            target.Telecom = source.Telecoms.Map(context, ContactPointMapper.Current);
        }
    }
}
