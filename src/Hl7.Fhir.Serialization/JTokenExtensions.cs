/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */


using Newtonsoft.Json.Linq;

namespace Hl7.Fhir.Serialization
{
    internal static class JTokenExtensions
    {
        public static string GetResourceTypeFromObject(this JObject o)
        {
            var type = o[JsonSerializationDetails.RESOURCETYPE_MEMBER_NAME];

            return type is JValue typeValue && typeValue.Type == JTokenType.String ? 
                (string)typeValue.Value : 
                null;
        }
    }

}


