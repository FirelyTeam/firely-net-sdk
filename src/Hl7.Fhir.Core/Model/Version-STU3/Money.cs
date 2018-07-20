using Hl7.Fhir.Introspection;

namespace Hl7.Fhir.Model.STU3
{
    public partial class Money : IMoney
    {
        [NotMapped]
        public string Currency
        {
            get { return Code; }
            set { Code = value; }
        }
    }
}
