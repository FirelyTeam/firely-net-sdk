/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */


using Newtonsoft.Json.Linq;

namespace Hl7.Fhir.Serialization
{
    internal static class JTokenExtensions
    {
        public static JProperty GetResourceTypePropertyFromObject(this JObject o, string myName)
        {
            return o.Property(JsonSerializationDetails.RESOURCETYPE_MEMBER_NAME) switch
            {
                JProperty { Value.Type: JTokenType.String } stringProp when !isException(myName) => stringProp,
                _ => null
            };

            // Hack to support R4 ExampleScenario.instance.resourceType and R5 Subscription.filterBy.resourceType element
            static bool isException(string myName) => myName is "filterBy" or "instance";
        }
    }
}


