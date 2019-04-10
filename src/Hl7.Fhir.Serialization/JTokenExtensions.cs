/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */


using Newtonsoft.Json.Linq;

namespace Hl7.Fhir.Serialization
{
    internal static class JTokenExtensions
    {
        public static JProperty GetResourceTypePropertyFromObject(this JObject o, string myName) =>
            !(o.Property(JsonSerializationDetails.RESOURCETYPE_MEMBER_NAME) is JProperty type) ?
                null
                : type.Value.Type == JTokenType.String && myName != "instance" ? type : null;
        // Hack to support R4 ExampleScenario.instance.resourceType element
    }
}


