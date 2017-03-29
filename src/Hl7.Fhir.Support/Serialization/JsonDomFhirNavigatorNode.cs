/*  
* Copyright (c) 2017, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using Hl7.Fhir.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Serialization
{
    /*
        JToken
            JContainer
                JArray
                JConstructor
                JObject
                JProperty
            JValue
                JRaw
     */

    internal struct JsonNavigatorNode
    {
        public JValue JsonValue;
        public JObject JsonObject;

        public JsonNavigatorNode(string name, JObject content)
        {
            Name = name;
            JsonValue = null;
            JsonObject = content;
        }

        public JsonNavigatorNode(string name, JValue value)
        {
            Name = name;
            JsonValue = value;
            JsonObject = null;
        }

        public JsonNavigatorNode(string name, JValue value, JObject content)
        {
            Name = name;
            JsonValue = value;
            JsonObject = content;
        }

        private static JsonNavigatorNode build(string name, JToken main, JToken shadow)
        {
            if (!isNull(main))
            {
                if (!isNull(shadow))
                {
                    // There is a shadow property, main prop should be a simple JValue
                    if (main is JValue mainV)
                        return new JsonNavigatorNode(name, mainV, getShadowObject(shadow));
                    else
                        throw Error.Format($"Because '_{name}' exists, property '{name}' should be a primitive json value", main.Path);
                }
                else
                {
                    if (main is JValue mainV)
                        return new JsonNavigatorNode(name, mainV);
                    else if (main is JObject mainO)
                        return new JsonNavigatorNode(name, mainO);
                    else
                        throw Error.Format($"FHIR serialization only supports objects or primitive values, not a {main.Type}", main.Path);
                }
            }
            else
            {
                // No main property, just return the shadow prop                        
                return new JsonNavigatorNode(name, getShadowObject(shadow));
            }

            bool isNull(JToken t) => t == null || t.Type == JTokenType.Null;

            // Note: Per FHIR serialization rules, the shadow prop should always be a "null" or a JObject.
            JObject getShadowObject(JToken s) => (s as JObject) ?? throw Error.Format("Properties beginning with '_' must always be (arrays of) complex objects", s.Path);
        }

        public string Name { get; private set; }

        public string Type => JsonObject?.GetCoreTypeFromObject();

        public object Value
        {
            get
            {
                if (JsonValue != null)
                {
                    if (JsonValue.Value != null)
                    {
                        // Make sure the representation of this Json-typed value is turned
                        // into a string representation compatible with the XML serialization
                        return PrimitiveTypeConverter.ConvertTo<string>(JsonValue.Value);
                    }
                }

                return null;
            }
        }

        public IEnumerable<JsonNavigatorNode> GetChildren()
        {
            if (JsonObject == null || JsonObject.HasValues == false) yield break;

            // ToList() added explicitly here, we really need our own copy of the list of children
            var children = JsonObject.Children<JProperty>().Where(c => c.Name != JTokenExtensions.RESOURCETYPE_MEMBER_NAME).ToList();

            while (children.Any())
            {
                (string name, JProperty main, JProperty shadow) = getNextElementPair(children);

                IEnumerable<JsonNavigatorNode> nodes = enumerateElement(name, main, shadow);

                foreach (var node in nodes)
                    yield return node;

                if (main != null) children.Remove(main);
                if (shadow != null) children.Remove(shadow);
            }
        }

        private (string name, JProperty main, JProperty shadow) getNextElementPair(List<JProperty> children)
        {
            JProperty main, shadow;
            string name;

            var child = children.First();

            if (isMainProperty(child))
            {
                main = child;
                name = child.Name;
                var shadowPropName = makeShadowName(child);
                shadow = children.SingleOrDefault(c => c.Name == shadowPropName);
            }
            else
            {
                shadow = child;
                var mainPropName = deriveMainName(child);
                name = mainPropName;
                main = children.SingleOrDefault(c => c.Name == mainPropName);
            }

            return (name, main, shadow);

            bool isMainProperty(JProperty prop) => prop.Name[0] != '_';
            string makeShadowName(JProperty prop) => "_" + prop.Name;
            string deriveMainName(JProperty prop) => prop.Name.Substring(1);
        }

        private IEnumerable<JsonNavigatorNode> enumerateElement(string name, JProperty main, JProperty shadow)
        {
            validateCardinalities(main, shadow);

            var mains = makeList(main);
            var shadows = makeList(shadow);

            int length = Math.Max(mains.Length, shadows.Length);

            for (var index = 0; index < length; index++)
                yield return JsonNavigatorNode.build(name, at(mains, index), at(shadows, index));

            JToken at(JToken[] list, int i) => list.Length > i ? list[i] : null;

            JToken[] makeList(JProperty prop)
            {
                if (prop == null)
                    return new JToken[] { };
                else if (prop.Value is JArray)
                    return ((JArray)prop.Value).Children().ToArray();
                else
                    return new[] { prop.Value };
            }
        }

        private void validateCardinalities(JProperty main, JProperty shadow)
        {
            // If main and shadow exists, check whether the number of elements match up
            if (main != null && shadow != null)
            {
                var mainV = main.Value;
                var shadowV = shadow.Value;

                if (mainV.Type == JTokenType.Array && shadowV.Type != JTokenType.Array)
                    throw Error.Format($"Because property '{main.Name}' is an array, '{shadow.Name}' should also be an array", shadow.Path);
                if (mainV.Type != JTokenType.Array && shadowV.Type == JTokenType.Array)
                    throw Error.Format($"Because property '{shadow.Name}' is an array, '{main.Name}' should also be an array", main.Path);
                if (mainV.Type == JTokenType.Array && shadowV.Type == JTokenType.Array)
                    if (mainV.Count() != shadowV.Count())
                    {
                        throw Error.Format($"The arrays for property '{main.Name}' should have the same number of elements", shadow.Path);
                    }
            }
        }
    }
}
