using Hl7.Fhir.Model;

namespace Hl7.Fhir.Serialization
{
    public interface IFhirSerializationEngine
    {
        public Resource DeserializeFromXml(string data);
        public Resource DeserializeFromJson(string data);
        public byte[] SerializeToXml(Model.Base instance);
        public byte[] SerializeToJson(Model.Base instance);
    }
}