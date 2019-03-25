using Hl7.Fhir.Introspection;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Model.R4
{
    public partial class Money : IMoney
    {
        [NotMapped]
        public string CurrencyCode
        {
            get { return Currency?.GetLiteral(); }
            set { Currency = EnumUtility.ParseLiteral<Currencies>( value ); }
        }
    }
}
