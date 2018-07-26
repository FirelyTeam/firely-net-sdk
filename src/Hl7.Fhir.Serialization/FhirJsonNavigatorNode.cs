/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Utility;
using Newtonsoft.Json;
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

    public partial class FhirJsonNavigator
    {
        internal struct JsonNavigatorNode
        {
            public JValue JsonValue;
            public JObject JsonObject;
            public bool IsArrayElement;

            private readonly FhirJsonNavigator _parent;

            public JsonNavigatorNode(string name, JValue value, JObject content, FhirJsonNavigator parent, bool isArrayElement)
            {
                Name = name;
                JsonValue = value;
                JsonObject = content;
                _parent = parent;
                IsArrayElement = isArrayElement;
            }

            public JToken PositionNode => (JToken)JsonValue ?? (JToken)JsonObject;

            private static JsonNavigatorNode? build(string name, JToken main, JToken shadow, bool isArrayElement, FhirJsonNavigator parent)
            {
                JValue value = null;
                JObject contents = null;

                if (main?.Type == JTokenType.Null && shadow?.Type == JTokenType.Null)
                {
                    if (!parent.PermissiveParsing)
                        parent.raiseFormatError($"The properties '{name}' and '_{name}' are both null, which is not allowed", main);
                    return null;
                }
                else if (main?.Type == JTokenType.Null && shadow == null)
                {
                    if (!parent.PermissiveParsing)
                        parent.raiseFormatError($"The property '{name}' cannot have just a null value", main);
                    return null;
                }
                else if (main == null && shadow?.Type == JTokenType.Null)
                {
                    if (!parent.PermissiveParsing)
                        parent.raiseFormatError($"The property '_{name}' cannot have just a null value", main);
                    return null;
                }

                if (main != null)
                {
                    switch (main)
                    {
                        case JValue val:
                            value = validateValue(val, name);
                            break;
                        case JObject obj:
                            contents = validateObject(obj, name);
                            break;
                        default:
                            if (!parent.PermissiveParsing)
                                parent.raiseFormatError($"The value for property '{name}' must be a value or an object, not a {main.Type}", main);
                            break;
                    }
                }

                if (shadow != null)
                {
                    switch (shadow)
                    {
                        case JValue val when val.Type == JTokenType.Null:
                            validateValue(val, $"_{name}");   // just report error, has no real value to return
                            break;
                        case JObject obj:
                            if (contents != null)
                                parent.raiseFormatError($"The '{name}' and '_{name}' properties cannot both contain complex data.", shadow);
                            else
                                contents = validateObject(obj, $"_{name}");
                            break;
                        default:
                            parent.raiseFormatError($"The value for property '_{name}' must be an object, not a {shadow.Type}", shadow);
                            break;
                    }
                }

                // This can only be true, if the logic just before left both value and contents == null because of errors
                // In that case, don't return any result from the build - which will make sure the caller skips
                // this property completely
                if (value == null && contents == null) return null;

                return new JsonNavigatorNode(name, value, contents, parent, isArrayElement);

                JValue validateValue(JValue v, string pName)
                {
                    if (v.Value is string s && String.IsNullOrWhiteSpace(s))
                    {
                        if (!parent.PermissiveParsing)
                            parent.raiseFormatError($"The property '{pName}' has an empty string value, which is not allowed.", v);
                        return null;
                    }
                    if (v.Type == JTokenType.Null)
                    {
                        if (!isArrayElement && !parent.PermissiveParsing)
                            parent.raiseFormatError($"The property '{pName}' has an 'null' value, which is only allowed in arrays.", v);
                        return null;
                    }
                    else
                        return v;
                }

                JObject validateObject(JObject o, string pName)
                {
                    if (o.Count == 0)
                    {
                        if (!parent.PermissiveParsing)
                            parent.raiseFormatError($"The object for property '{pName}' is empty, which is not allowed.", o);
                        return null;
                    }
                    else
                        return o;
                }
            }

            public string Name { get; private set; }

            public string ResourceType => JsonObject?.GetResourceTypeFromObject();

            public string Text
            {
                get
                {
                    if (JsonValue != null)
                    {
                        if (JsonValue.Value != null)
                        {
                            // Make sure the representation of this Json-typed value is turned
                            // into a string representation compatible with the XML serialization
                            if (JsonValue.Value is string s)
                                return s.Trim();
                            else
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
                // Note: this will create a lookup with a grouping that groups the main + shadow property
                // under the same name (which is the name without the _).
                var children = JsonObject.Children<JProperty>().ToLookup(jp => deriveMainName(jp));
                var processed = new HashSet<string>();

                foreach (var child in children)
                {
                    if (child.Key == JsonSerializationDetails.RESOURCETYPE_MEMBER_NAME) continue;
                    if (processed.Contains(child.Key)) continue;

                    (JProperty main, JProperty shadow) = getNextElementPair(child);
                    processed.Add(child.Key);

                    IEnumerable<JsonNavigatorNode> nodes = enumerateElement(child.Key, main, shadow);

                    foreach (var node in nodes)
                        yield return node;
                }

                string deriveMainName(JProperty prop)
                {
                    var name = prop.Name;
                    return name[0] == '_' ? name.Substring(1) : name;
                }
            }

            private (JProperty main, JProperty shadow) getNextElementPair(IGrouping<string, JProperty> child)
            {
                JProperty main = child.First(), shadow = child.Skip(1).FirstOrDefault();

                if (main.Name[0] != '_')
                    return (main, shadow);
                else
                    return (shadow, main);
            }

            private IEnumerable<JsonNavigatorNode> enumerateElement(string name, JProperty main, JProperty shadow)
            {
                validateCardinalities(main, shadow);

                // Even if main/shadow has errors (i.e. not both are an array, number of items are not the same
                // we should be getting some kind of minimal useable list from the next two statements and
                // continue parsing.
                var mains = makeList(main, out var wasArrayMain);
                var shadows = makeList(shadow, out var wasArrayShadow);
                bool isArrayElement = wasArrayMain | wasArrayShadow;

                int length = Math.Max(mains.Length, shadows.Length);

                for (var index = 0; index < length; index++)
                {
                    var result = JsonNavigatorNode.build(name, at(mains, index), at(shadows, index), isArrayElement, _parent);
                    if (result != null) yield return result.Value;
                }

                JToken at(JToken[] list, int i) => list.Length > i ? list[i] : null;

                JToken[] makeList(JProperty prop, out bool wasArray)
                {
                    wasArray = false;

                    if (prop == null)
                        return new JToken[] { };
                    else if (prop.Value is JArray array)
                    {
                        wasArray = true;
                        return array.ToArray();
                    }
                    else
                        return new[] { prop.Value };
                }
            }

            private void validateCardinalities(JProperty main, JProperty shadow)
            {
                if (_parent.PermissiveParsing) return;

                // If main and shadow exists, check whether the number of elements match up
                if (main != null && shadow != null)
                {
                    var mainV = main.Value;
                    var shadowV = shadow.Value;

                    if (mainV.Type == JTokenType.Array && shadowV.Type != JTokenType.Array)
                    {
                        _parent.raiseFormatError($"Because property '{main.Name}' is an array, '{shadow.Name}' should also be an array", shadow);
                    }
                    if (mainV.Type != JTokenType.Array && shadowV.Type == JTokenType.Array)
                    {
                        _parent.raiseFormatError($"Because property '{shadow.Name}' is an array, '{main.Name}' should also be an array", main);
                    }
                    if (mainV.Type == JTokenType.Array && shadowV.Type == JTokenType.Array && mainV.Count() != shadowV.Count())
                    {
                        _parent.raiseFormatError($"The arrays for property '{main.Name}' should have the same number of elements", shadow);
                    }
                }
            }
        }
    }
}
