using Hl7.Fhir.Model;
using Hl7.Fhir.Validation.Model;

namespace Hl7.Fhir.Specification.Validation.Model
{
    internal class ContactPointMapper : ITransferMapper<ContactPoint, UniContactPoint>
    {
        public static readonly ContactPointMapper Current = new ContactPointMapper();

        private static readonly EnumMapper<ContactPoint.ContactPointSystem, UniContactPointSystem> _systemMapper
            = new EnumMapper<ContactPoint.ContactPointSystem, UniContactPointSystem>(
                (ContactPoint.ContactPointSystem.Email, UniContactPointSystem.Email),
                (ContactPoint.ContactPointSystem.Fax, UniContactPointSystem.Fax),
                (ContactPoint.ContactPointSystem.Other, UniContactPointSystem.Other),
                (ContactPoint.ContactPointSystem.Pager, UniContactPointSystem.Pager),
                (ContactPoint.ContactPointSystem.Phone, UniContactPointSystem.Phone),
                (ContactPoint.ContactPointSystem.Sms, UniContactPointSystem.Sms),
                (ContactPoint.ContactPointSystem.Url, UniContactPointSystem.Url)
                );

        private static readonly EnumMapper<ContactPoint.ContactPointUse, UniContactPointUse> _useMapper
            = new EnumMapper<ContactPoint.ContactPointUse, UniContactPointUse>(
                (ContactPoint.ContactPointUse.Home, UniContactPointUse.Home),
                (ContactPoint.ContactPointUse.Mobile, UniContactPointUse.Mobile),
                (ContactPoint.ContactPointUse.Old, UniContactPointUse.Old),
                (ContactPoint.ContactPointUse.Work, UniContactPointUse.Work),
                (ContactPoint.ContactPointUse.Temp, UniContactPointUse.Temporary)
                );

        public void Transfer(MappingContext context, ContactPoint source, UniContactPoint target)
        {
            target.Period = source.Period.Map(context, PeriodMapper.Current);
            target.Rank = source.Rank;
            target.System = source.System.Map(context, _systemMapper);
            target.Use = source.Use.Map(context, _useMapper);
            target.Value = source.Value;
        }

        public void Transfer(MappingContext context, UniContactPoint source, ContactPoint target)
        {
            target.Period = source.Period.Map(context, PeriodMapper.Current);
            target.Rank = source.Rank;
            target.System = source.System.Map(context, _systemMapper);
            target.Use = source.Use.Map(context, _useMapper);
            target.Value = source.Value;
        }
    }
}
