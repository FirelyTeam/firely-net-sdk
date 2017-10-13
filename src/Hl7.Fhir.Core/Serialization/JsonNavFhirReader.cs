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
using Hl7.Fhir.Utility;
using Hl7.Fhir.ElementModel;

namespace Hl7.Fhir.Serialization
{
    public class JsonNavFhirReader : IFhirReader
    {
        public const string RESOURCETYPE_MEMBER_NAME = "resourceType";

        private IElementNavigator _current;

        public JsonNavFhirReader(IElementNavigator root)
        {
            _current = root;
        }

        internal JsonNavFhirReader(JObject root)
        {
            _current = JsonDomFhirNavigator.Create(root);
        }

        // [WMR 20160421] Caller can safely dispose reader after calling this ctor
        public JsonNavFhirReader(JsonReader reader)
        {
            reader.DateParseHandling = DateParseHandling.None;
            reader.FloatParseHandling = FloatParseHandling.Decimal;

            try
            {
                _current = JsonDomFhirNavigator.Create(reader);
                //rewriteExtensionProperties(_current);
            }
            catch (Exception e)
            {
                throw Error.Format("Cannot parse json: " + e.Message);
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
            return _current.Value;
        }

        public string GetResourceTypeName()
        {
            if (_current.Type != null) return _current.Type;

            throw Error.Format("Cannot determine type of resource to create from json input data: no member {0} was found".FormatWith(JsonDomFhirReader.RESOURCETYPE_MEMBER_NAME), this);
        }


        public IEnumerable<Tuple<string, IFhirReader>> GetMembers()
        {
            if (_current.Value != null)
                yield return Tuple.Create("value", (IFhirReader)new JsonNavFhirReader(_current));

            var children = _current.Children();
            foreach (var child in _current.Children())
                yield return Tuple.Create(child.Name, (IFhirReader)new JsonNavFhirReader(child));
        }


        public static IPositionInfo GetLineInfo(JToken obj)
        {
            return new JsonDomFhirReader(obj);
        }

        public int LineNumber => -1;

        public int LinePosition => -1;
    }
}
