/*  
* Copyright (c) 2016, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    internal static class JTokenExtensions
    {
        public const string RESOURCETYPE_MEMBER_NAME = "resourceType";

        public static string GetCoreTypeFromObject(this JObject o)
        {
            var type = o[RESOURCETYPE_MEMBER_NAME];

            if(type is JValue typeValue && typeValue.Type == JTokenType.String)
                return (string)typeValue.Value;
            else
                return null;
        }
    }

    public partial struct JsonDomFhirNavigator : IElementNavigator
    {
        internal struct JsonNavigatorNode
        {
            private JValue _value;
            private JObject _content;

            public JsonNavigatorNode(string name, JObject content)
            {
                Name = name;
                _value = null;
                _content = content;
            }

            public JsonNavigatorNode(string name, JValue value)
            {
                Name = name;
                _value = value;
                _content = null;
            }

            public JsonNavigatorNode(string name, JValue value, JObject content)
            {
                Name = name;
                _value = value;
                _content = content;
            }

            private static JsonNavigatorNode build(string name, JToken main, JToken shadow)
            {
                // Note: Per FHIR serialization rules, the shadow prop should always be a "null" or a JObject.
                if (!(shadow is JObject shadowObject))
                    throw Error.Format("Properties beginning with '_' must always be complex objects");

                if (main.Type != JTokenType.Null)
                {
                    if (shadow.Type != JTokenType.Null)
                    {
                        // There is a shadow property, main prop should be a simple JValue
                        return new JsonNavigatorNode(name, (JValue)main, shadowObject);
                    }
                    else
                    {
                        if (main is JValue)
                            return new JsonNavigatorNode(name, (JValue)main);
                        else
                            return new JsonNavigatorNode(name, (JObject)main);
                    }
                }
                else
                {
                    // No main property, just return the shadow prop                        
                    return new JsonNavigatorNode(name, shadowObject);
                }
            }

            public string Name { get; private set; }

            public string Type => throw new NotImplementedException();

            public object Value
            {
                get
                {
                    if(_value != null)
                    {
                        if(_value.Value != null)
                        {
                            // Make sure the representation of this Json-typed value is turned
                            // into a string representation compatible with the XML serialization
                            return PrimitiveTypeConverter.ConvertTo<string>(_value.Value);
                        }
                    }

                    return null;
                }
            }

            public IEnumerable<JsonNavigatorNode> GetChildren()
            {
                if (_content == null || _content.HasValues == false) yield break;

                // ToList() added explicitly here, we really need our own copy of the list of children
                var children = _content.Children<JProperty>().ToList();

                while(children.Any())
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
                // TODO: This won't work where arrays are not the same length, this needs to be checked first

                var mains = makeList(main);
                var shadows = makeList(shadow);

                return mains.Zip(shadows, (m,s) => JsonNavigatorNode.build(name,m,s));

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
        }

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

        internal JsonDomFhirNavigator(string root, JObject current)
        {
            _siblings = new[] { new JsonNavigatorNode(root, current) };
            _index = 0;
        }


        internal JsonNavigatorNode Current => _siblings[_index];

        public IElementNavigator Clone()
        {
            var copy = new JsonDomFhirNavigator();
            copy._siblings = _siblings;
            copy._index = _index;

            return copy;
        }

        private JsonNavigatorNode[] _siblings;
        private int _index;

        public string Name => Current.Name;

        public string Type => Current.Type;

        public object Value => Current.Value;

        public string Location => throw new NotImplementedException();

        public bool MoveToFirstChild()
        {
            var children = Current.GetChildren().ToArray();

            if (children.Length == 0) return false;

            _siblings = children;
            _index = 0;

            return true;
        }

        public bool MoveToNext()
        {
            if (_index + 1 >= _siblings.Length) return false;

             _index += 1;
             return true;
        }

        public int LineNumber => throw new NotImplementedException();

        public int LinePosition => throw new NotImplementedException();
        
        public override string ToString()
        {
            return Current.ToString();
        }

        public T GetSerializationDetails<T>() where T:class
        {
            throw new NotImplementedException();
            //if (typeof(T) == typeof(XmlSerializationDetails))
            //{
            //    var result = new XmlSerializationDetails();
                    
            //    return result as T;
            //}
            //else
            //    return null;
        }

    }
}


