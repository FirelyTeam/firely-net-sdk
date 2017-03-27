using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hl7.Fhir.Serialization
{
    public partial struct JsonDomFhirNavigator
    {
        public static IElementNavigator Create(JsonReader reader)
        {
            try
            {
                JObject doc = null;
                doc = SerializationUtil.JObjectFromReader(reader);
                var type = doc.GetCoreTypeFromObject();

                if (type == null)
                    throw Error.InvalidOperation("Root object has no type indication (resourceType) and therefore cannot be used to construct the navigator");

                return new JsonDomFhirNavigator(doc.GetCoreTypeFromObject(), doc);
            }
            catch (JsonException jec)
            {
                throw Error.Format("Cannot parse json: " + jec.Message);
            }
        }

        public static IElementNavigator Create(string json)
        {
            using (var reader = SerializationUtil.JsonReaderFromJsonText(json))
            {
                return Create(reader);
            }
        }
    }
}
