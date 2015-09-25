/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Serialization
{
    public class JsonDomFhirReader : IFhirReader
    {
        public const string RESOURCETYPE_MEMBER_NAME = "resourceType";

        private JToken _current;

        internal JToken Current { get { return _current; } }            // just for while refactoring

        internal JsonDomFhirReader(JToken root)
        {
            _current = root;
        }

        public JsonDomFhirReader(JsonReader reader)
        {
            reader.DateParseHandling = DateParseHandling.None;
            reader.FloatParseHandling = FloatParseHandling.Decimal;

            try
            {
                _current = JObject.Load(reader);
                //rewriteExtensionProperties(_current);
            }
            catch (Exception e)
            {
                throw Error.Format("Cannot parse json: " + e.Message, null);
            }
        }

        
        // This code implements the San Antonio timeframe DSTU2 Json serialization of extensions
        // (using full urls as property names). This has been retracted, but keep the code here
        // in case we change our mind again.

        //private void rewriteExtensionProperties(JToken current, bool nested=false)
        //{
        //    if (current is JObject)
        //        rewriteExtensionProperties((JObject)current, nested);
        //    else if (current is JArray)
        //        rewriteExtensionProperties((JArray)current, nested);
        //}


        //private bool isExtensionProperty(string name, bool nested)
        //{
        //    if(name.StartsWith("http://") || name.StartsWith("urn:")) return true;

        //    if (!nested) 
        //        return false;
        //    else
        //        return name != "url" && name != "id" && !name.StartsWith("value");
        //}

        //private void rewriteExtensionProperties(JObject current, bool nested=false)
        //{
        //    var extensionMembers = new List<JProperty>();
        //    var modifierExtensionMembers = new List<JProperty>();

        //    foreach (var property in current.Properties()) 
        //    {
        //        if (isExtensionProperty(property.Name,nested))
        //        {
        //            if(!(property.Value is JArray))
        //                throw Error.Format("Found extension '{0}', but its value is not an array", this, property.Name);
        //            extensionMembers.Add(property);
        //        }
        //        else if (property.Name == "modifier")
        //        {
        //            if (nested) throw Error.Format("'modifier' cannot be used within an extension", this);
        //            if (!(property.Value is JObject)) throw Error.Format("A 'modifier' property should contain a json object", this);

        //            var modifiers = (JObject)property.Value;

        //            foreach (var modifier in modifiers.Properties())
        //            {
        //                if (!isExtensionProperty(modifier.Name, nested: false)) throw Error.Format("All properties within 'modifier' should have a url as its name", this);
        //                modifierExtensionMembers.Add(modifier);
        //            }
        //        }
        //        else
        //        {
        //            // not an extension property, so this is an actual element, and introduces a new scope
        //            // for extensions...we're no longer looking for nested extension names
        //            rewriteExtensionProperties(property.Value, nested: false);
        //        }
        //    }

        //    if(extensionMembers.Any())
        //    {
        //        foreach (var prop in extensionMembers) { current.Remove(prop.Name); }; // List<>.Foreach not in PCL (http://cureos.blogspot.com.au/2014/05/pcl-tips-and-tricks-listforeach.html)
        //        var extProp = convertToExtensionProperty("extension",extensionMembers); 
        //        current.Add(extProp);
        //        rewriteExtensionProperties(extProp.Value, nested:true);      // The Extension.value[x] themselves might contain data that has extension, so recurse
        //    }
        //    if(modifierExtensionMembers.Any())
        //    {
        //        current.Remove("modifier");
        //        var mextProp = convertToExtensionProperty("modifierExtension", modifierExtensionMembers);
        //        current.Add(mextProp);
        //        rewriteExtensionProperties(mextProp.Value, nested:true); // The Extension.value[x] themselves might contain data that has extension, so recurse
        //    }
        //}

        //private JProperty convertToExtensionProperty(string name, List<JProperty> extensionMembers)
        //{
        //    var extArray = new JArray();
        //    var extProperty = new JProperty(name, extArray);

        //    foreach (var ext in extensionMembers)
        //    {
        //        var extensionObjectArray = (JArray)ext.Value;
        //        foreach (var extensionObject in extensionObjectArray)
        //        {
        //            if (!(extensionObject is JObject))
        //                throw Error.Format("Extension '{0}' contains an array element that is not a complex object", this, ext.Name);

        //            var extensionJObject = (JObject)extensionObject;
        //            extensionJObject.Add(new JProperty("url", ext.Name));
        //            extArray.Add(extensionJObject);
        //        }
        //    }

        //    return extProperty;
        //}


        //private void rewriteExtensionProperties(JArray current, bool nested=false)
        //{
        //    foreach (var element in current.Children())
        //        rewriteExtensionProperties(element,nested);
        //}

        public object GetPrimitiveValue()
        {
            if (_current is JValue)
                return ((JValue)_current).Value;
            else if(_current is JObject)
            {
                // Hack: primitives have been expanded to objects by the JsonTreeRewriter, so if this is the case, just get the "value" property
                var cplx = _current as JObject;
                JToken value = null;

                if (cplx.TryGetValue(JsonTreeRewriter.PRIMITIVE_PROP_NAME, out value))
                {
                    var nestedReader = new JsonDomFhirReader(value);
                    return nestedReader.GetPrimitiveValue();
                }
                else
                    throw Error.Format("Tried to read a primitive value while reader is at a complex object", this);
            }
            else
                throw Error.Format("Tried to read a primitive value while reader is not at a json primitive", this);
        }

        public string GetResourceTypeName()
        {
            if (!(_current is JObject))
                throw Error.Format("Cannot read resource type: reader not at a complex object", this);

            var resourceTypeMember = ((JObject)_current)[JsonDomFhirReader.RESOURCETYPE_MEMBER_NAME];

            if (resourceTypeMember != null)
            {
                if (resourceTypeMember is JValue)
                {
                    var memberValue = (JValue)resourceTypeMember;

                    if (memberValue.Type == JTokenType.String)
                    {
                        return (string)memberValue.Value;
                    }
                }

                throw Error.Format("resourceType should be a primitive string json value", this);
            }

            throw Error.Format("Cannot determine type of resource to create from json input data: no member {0} was found", this, 
                            JsonDomFhirReader.RESOURCETYPE_MEMBER_NAME);
        }


        public IEnumerable<Tuple<string, IFhirReader>> GetMembers()
        {
            var complex = _current as JObject;
 
            if (complex == null)
                throw Error.Format("Need to be at a complex object to list child members", this);

            var members = JsonTreeRewriter.ExpandComplexObject(complex);

            foreach(var member in members)
            {
                var memberName = member.Name;

                //When enumerating properties for a complex object, make sure not to let resourceType get through
                if(memberName != JsonDomFhirReader.RESOURCETYPE_MEMBER_NAME)
                {
                    IFhirReader nestedReader = new JsonDomFhirReader(member.Value);

                    // Remove [nn] appendix that was there to allow a JObject have repeating members
                    // with the same name
                    var pos = memberName.IndexOf('[');
                    if (pos > 0) memberName = memberName.Substring(0, pos);

                    yield return Tuple.Create(memberName,nestedReader);
                }
            }
        }


        public static IPostitionInfo GetLineInfo(JToken obj)
        {
            return new JsonDomFhirReader(obj);
        }

        public int LineNumber
        {
            get
            {
                var li = (IJsonLineInfo)_current;

                if (!li.HasLineInfo()) return -1;
                    
                return li.LineNumber;
            }
        }

        public int LinePosition
        {
            get
            {
                var li = (IJsonLineInfo)_current;

                if (!li.HasLineInfo()) return -1;

                return li.LinePosition;
            }
        }
    }
}
