using Hl7.Fhir.Introspection;

namespace Hl7.Fhir.Model.STU3
{
    public partial class Money : IMoney
    {
        [NotMapped]
        public string CurrencyCode
        {
            get { return Code; }
            set { Code = value; }
        }
    }
}
