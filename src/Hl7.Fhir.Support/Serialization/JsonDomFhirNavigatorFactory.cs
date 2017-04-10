/*  
* Copyright (c) 2017, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hl7.Fhir.Serialization
{
    public partial struct JsonDomFhirNavigator
    {
        public static IElementNavigator Create(JsonReader reader, string rootName=null)
        {
            try
            {
                JObject doc = null;
                doc = SerializationUtil.JObjectFromReader(reader);
                var type = doc.GetCoreTypeFromObject();

                if (type == null && rootName == null)
                    throw Error.InvalidOperation("Root object has no type indication (resourceType) and therefore cannot be used to construct the navigator. Alternatively, specify a rootName using the parameter.");

                return new JsonDomFhirNavigator(rootName ?? doc.GetCoreTypeFromObject(), doc);
            }
            catch (JsonException jec)
            {
                throw Error.Format("Cannot parse json: " + jec.Message);
            }
        }

        public static IElementNavigator Create(string json, string rootName = null)
        {
            using (var reader = SerializationUtil.JsonReaderFromJsonText(json))
            {
                return Create(reader, rootName);
            }
        }
    }
}
