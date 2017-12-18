using Newtonsoft.Json.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    public interface IJsonSerializable
    {
        JObject ToJson();
    }
}
