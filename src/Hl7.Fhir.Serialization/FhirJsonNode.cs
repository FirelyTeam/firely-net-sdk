/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification;
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
    internal class FhirJsonNode : ISourceNode, IAnnotated, IExceptionSource
    {
        public FhirJsonNode(JObject current, string nodeName, FhirJsonNavigatorSettings settings = null)
        {
            Name = nodeName;
            Location = Name;
            JsonValue = null;
            JsonObject = current;
            ArrayIndex = null;

            _settings = settings?.Clone() ?? new FhirJsonNavigatorSettings();
        }


        private FhirJsonNode(FhirJsonNode parent, string name, JValue value, JObject content, int? arrayIndex, string location)
        {
            Name = name;
            JsonValue = value;
            JsonObject = content;
            ArrayIndex = arrayIndex;
            Location = location;

            _settings = parent._settings;
            ExceptionHandler = parent.ExceptionHandler;
        }

        private readonly FhirJsonNavigatorSettings _settings;
        public readonly JValue JsonValue;
        public readonly JObject JsonObject;
        public readonly int? ArrayIndex;

        public string Name { get; private set; }
        public string Location { get; private set; }

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        public bool AllowJsonComments => _settings.AllowJsonComments;
        public bool PermissiveParsing => _settings.PermissiveParsing;
#if NET_XSD_SCHEMA
        public bool ValidateFhirXhtml => _settings.ValidateFhirXhtml;
#endif


        public JToken PositionNode => (JToken)JsonValue ?? (JToken)JsonObject;

        private FhirJsonNode build(string name, JToken main, JToken shadow, bool isArrayElement, int index)
        {
            JValue value = null;
            JObject contents = null;

            if (main?.Type == JTokenType.Null && shadow?.Type == JTokenType.Null)
            {
                if (!PermissiveParsing)
                    raiseFormatError($"The properties '{name}' and '_{name}' are both null, which is not allowed", main);
                return null;
            }
            else if (main?.Type == JTokenType.Null && shadow == null)
            {
                if (!PermissiveParsing)
                    raiseFormatError($"The property '{name}' cannot have just a null value", main);
                return null;
            }
            else if (main == null && shadow?.Type == JTokenType.Null)
            {
                if (!PermissiveParsing)
                    raiseFormatError($"The property '_{name}' cannot have just a null value", main);
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
                        if (!PermissiveParsing)
                            raiseFormatError($"The value for property '{name}' must be a value or an object, not a {main.Type}", main);
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
                            raiseFormatError($"The '{name}' and '_{name}' properties cannot both contain complex data.", shadow);
                        else
                            contents = validateObject(obj, $"_{name}");
                        break;
                    default:
                        raiseFormatError($"The value for property '_{name}' must be an object, not a {shadow.Type}", shadow);
                        break;
                }
            }

            // This can only be true, if the logic just before left both value and contents == null because of errors
            // In that case, don't return any result from the build - which will make sure the caller skips
            // this property completely
            if (value == null && contents == null) return null;

            var location = $"{Location}.{name}[{index}]";
            return new FhirJsonNode(this, name, value, contents, isArrayElement ? index : (int?)null, location);

            JValue validateValue(JValue v, string pName)
            {
                if (v.Value is string s && String.IsNullOrWhiteSpace(s))
                {
                    if (!PermissiveParsing)
                        raiseFormatError($"The property '{pName}' has an empty string value, which is not allowed.", v);
                    return null;
                }
                if (v.Type == JTokenType.Null)
                {
                    if (!isArrayElement && !PermissiveParsing)
                        raiseFormatError($"The property '{pName}' has an 'null' value, which is only allowed in arrays.", v);
                    return null;
                }
                else
                    return v;
            }

            JObject validateObject(JObject o, string pName)
            {
                if (o.Count == 0)
                {
                    if (!PermissiveParsing)
                        raiseFormatError($"The object for property '{pName}' is empty, which is not allowed.", o);
                    return null;
                }
                else
                    return o;
            }
        }

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

        public IEnumerable<ISourceNode> Children(string name = null)
        {
            if (JsonObject == null || JsonObject.HasValues == false) yield break;

            // ToList() added explicitly here, we really need our own copy of the list of children
            // Note: this will create a lookup with a grouping that groups the main + shadow property
            // under the same name (which is the name without the _).
            var children = JsonObject.Children<JProperty>().ToLookup(jp => deriveMainName(jp));
            var processed = new HashSet<string>();

            var prefixMatch = name?.EndsWith("*") ?? false;

            var scanChildren = name == null ? children :
                children.Where(n => n.Key == name ||
                        (prefixMatch && n.Key.StartsWith(name)));     // prefix scan (choice types)

            foreach (var child in scanChildren)
            {
                if (isResourceTypeIndicator(child)) continue;
                if (processed.Contains(child.Key)) continue;

                (JProperty main, JProperty shadow) = getNextElementPair(child);

                if (child.Key == "fhir_comments")
                {
                    if (!AllowJsonComments && !PermissiveParsing)
                        raiseFormatError("The 'fhir_comments' feature is disabled.", main ?? shadow);
                    continue;      // ignore pre-DSTU2 Json comments
                }

                processed.Add(child.Key);

                var nodes = enumerateElement(child.Key, main, shadow);

                foreach (var node in nodes)
                    yield return node;
            }

            string deriveMainName(JProperty prop)
            {
                var n = prop.Name;
                return n[0] == '_' ? n.Substring(1) : n;
            }
        }

        private bool isResourceTypeIndicator(IGrouping<string, JProperty> child)
        {
            if (child.Key != JsonSerializationDetails.RESOURCETYPE_MEMBER_NAME) return false;

            return child.First().Value.Type == JTokenType.String;
        }

        private (JProperty main, JProperty shadow) getNextElementPair(IGrouping<string, JProperty> child)
        {
            JProperty main = child.First(), shadow = child.Skip(1).FirstOrDefault();

            if (main.Name[0] != '_')
                return (main, shadow);
            else
                return (shadow, main);
        }

        private IEnumerable<FhirJsonNode> enumerateElement(string name, JProperty main, JProperty shadow)
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
                var result = build(name, at(mains, index), at(shadows, index), isArrayElement, index);
                if (result != null) yield return result;
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
            if (PermissiveParsing) return;

            // If main and shadow exists, check whether the number of elements match up
            if (main != null && shadow != null)
            {
                var mainV = main.Value;
                var shadowV = shadow.Value;

                if (mainV.Type == JTokenType.Array && shadowV.Type != JTokenType.Array)
                {
                    raiseFormatError($"Because property '{main.Name}' is an array, '{shadow.Name}' should also be an array", shadow);
                }
                if (mainV.Type != JTokenType.Array && shadowV.Type == JTokenType.Array)
                {
                    raiseFormatError($"Because property '{shadow.Name}' is an array, '{main.Name}' should also be an array", main);
                }
                if (mainV.Type == JTokenType.Array && shadowV.Type == JTokenType.Array && mainV.Count() != shadowV.Count())
                {
                    raiseFormatError($"The arrays for property '{main.Name}' should have the same number of elements", shadow);
                }
            }
        }

        private void raiseFormatError(string message, JToken node)
        {
            var (lineNumber, linePosition) = getPosition(node);
            ExceptionHandler.NotifyOrThrow(this, ExceptionNotification.Error(Error.Format(message, lineNumber, linePosition)));
        }

        private (int lineNumber, int linePosition) getPosition(JToken node)
        {
            if (node is IJsonLineInfo jli)
                return (jli.LineNumber, jli.LinePosition);
            else
                return (-1, -1);
        }

        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(FhirJsonNavigator))
                return new[] { this };
#pragma warning disable 612, 618
            else if (type == typeof(AdditionalStructuralRule) && !PermissiveParsing)
                return additionalTypeRules();
#pragma warning restore 612, 618
            else if (type == typeof(JsonSerializationDetails))
            {
                var (lineNumber, linePosition) = getPosition((JToken)JsonValue ?? (JToken)JsonObject);

                return new[]
                {
                    new JsonSerializationDetails()
                    {
                        OriginalValue = JsonValue?.Value,
                        LineNumber = lineNumber,
                        LinePosition = linePosition,
                        ArrayIndex = ArrayIndex
                    }
                };
            }
            else if (type == typeof(ResourceTypeIndicator))
            {
                return new[]
                {
                    new ResourceTypeIndicator
                    {
                        ResourceType = JsonObject?.GetResourceTypeFromObject()
                    }
                };
            }
            else
                return Enumerable.Empty<object>();
        }

#pragma warning disable 612, 618
        private IEnumerable<AdditionalStructuralRule> additionalTypeRules()
        {
            yield return checkArrayUse;

#if NET_XSD_SCHEMA
            yield return checkXhtml;

            object checkXhtml(IElementNode nav, IExceptionSource ies, object _)
            {
                if (nav.Type == "xhtml" && ValidateFhirXhtml)
                    FhirXmlNode.ValidateXhtml((string)nav.Value, ies, nav);

                return null;
            }
#endif

            object checkArrayUse(IElementNode nav, IExceptionSource ies, object _)
            {
                var sdSummary = nav.GetElementDefinitionSummary();
                var serializationDetails = nav.GetJsonSerializationDetails();
                if (sdSummary == null || serializationDetails == null) return null;

                if (sdSummary.IsCollection && serializationDetails.ArrayIndex == null)
                    ies.ExceptionHandler.NotifyOrThrow(nav, ExceptionNotification.Error(
                        new StructuralTypeException($"Since element '{nav.Name}' repeats, an array must be used here.")));

                if (!sdSummary.IsCollection && serializationDetails.ArrayIndex != null)
                {
                    // only report this once on the first of the group
                    if (serializationDetails.ArrayIndex == 0)
                    {
                        ies.ExceptionHandler.NotifyOrThrow(nav, ExceptionNotification.Error(
                            new StructuralTypeException($"Element '{nav.Name}' does not repeat, so an array must not be used here.")));
                    }
                }

                return null;
            }
        }
#pragma warning restore 612, 618

    }
}
