/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Newtonsoft.Json.Linq;
using Hl7.Fhir.Introspection;

namespace Hl7.Fhir.Serialization
{
    internal class JsonDomFhirWriter : IFhirWriter
    {
        private JsonWriter jw;
        private JToken _current = null;
        private JToken _root = null;
 
        public JsonDomFhirWriter(JsonWriter jwriter)
        {
            jw = jwriter;
        }

        internal JsonDomFhirWriter()
        {
        }


        private JObject _createdRootObject;

        public void WriteStartRootObject(string name, string id, bool contained)
        {
            _createdRootObject = new JObject(new JProperty(JsonDomFhirReader.RESOURCETYPE_MEMBER_NAME, name));

            if (!contained)
                _root = _createdRootObject;                    
        }


        public void WriteEndRootObject(bool contained)
        {
            if (!contained)
            {
               // rewriteExtensionProperties(_root);

                if (jw != null) _root.WriteTo(jw);
            }
        }

        // This code implements the San Antonio timeframe DSTU2 Json serialization of extensions
        // (using full urls as property names). This has been retracted, but keep the code here
        // in case we change our mind again.

        //private void rewriteExtensionProperties(JToken current)
        //{
        //    if (current is JObject)
        //        rewriteExtensionProperties((JObject)current);
        //    else if (current is JArray)
        //        rewriteExtensionProperties((JArray)current);
        //}


        //private bool isExtensionProperty(string name)
        //{
        //    return name == "extension" || name == "modifierExtension";
        //}

        //private void rewriteExtensionProperties(JObject current)
        //{
        //    foreach (var property in current.Properties().ToList())  // Properties() is modified, so do a ToList()
        //    {
        //        if (property.Name == "extension")
        //        {
        //            var dstu1Extensions = (JArray)property.Value;
        //            var convertedExtensions = convertExtensionArray(dstu1Extensions);

        //            foreach (var extension in convertedExtensions) current.Add(extension);
        //            property.Remove();
        //        }
        //        else if (property.Name == "modifierExtension")
        //        {
        //            var dstu1Extensions = (JArray)property.Value;
        //            var convertedExtensions = convertExtensionArray(dstu1Extensions);

        //            var modifierObject = new JObject();
        //            var modifierProp = new JProperty("modifier", modifierObject);
        //            foreach (var extension in convertedExtensions) modifierObject.Add(extension);
        //            property.Remove();

        //            current.Add(modifierProp);
        //        }
        //        else
        //        {
        //            // not an extension property, so this is an actual element.
        //            rewriteExtensionProperties(property.Value);
        //        }
        //    }
        //}

        //private List<JProperty> convertExtensionArray(JArray dstu1Extensions)
        //{
        //    var result = new List<JProperty>();

        //    // DSTU2 extensions are grouped by url, so sort them together
        //    var urlGroups = dstu1Extensions.GroupBy(token => ((JObject)token)["url"].Value<string>());

        //    foreach (var urlGroup in urlGroups.ToList())
        //    {
        //        var dstu2extensions = new JArray();
        //        var dstu2extensionProperty = new JProperty(urlGroup.Key, dstu2extensions);

        //        foreach (var dstu1extension in urlGroup)
        //        {
        //            dstu1Extensions.Remove(dstu1extension);
        //            dstu2extensions.Add(dstu1extension);
        //            ((JObject)dstu1extension).Remove("url");
        //        }

        //        result.Add(dstu2extensionProperty);
        //        rewriteExtensionProperties(dstu2extensionProperty.Value);
        //    }

        //    return result;
        //}


        //private void rewriteExtensionProperties(JArray current)
        //{
        //    foreach (var element in current.Children())
        //        rewriteExtensionProperties(element);
        //}



        public void WriteStartProperty(string name)
        {
            var prop = new JProperty(name, null);
            ((JObject)_current).Add(prop);
            _current = prop;
        }

        public void WriteEndProperty()
        {
            var prop = (JProperty)_current;

            var parent = _current.Parent;

            if (prop.Value.Type == JTokenType.Null)
                _current.Remove();

            _current = parent;
        }

        public void WriteStartComplexContent()
        {
            // If this call was preceded by a call to WriteStartRootObject (when creating a resource or contained/nested resource)
            // use the special root JObject that was just created by that call instead of creating a new one.
            JObject newScope = _createdRootObject ?? new JObject();
            _createdRootObject = null;

            if (_current is JProperty)
                ((JProperty)_current).Value = newScope;
            else if (_current is JArray)
                ((JArray)_current).Add(newScope);
            
            _current = newScope;
        }

        public void WriteEndComplexContent()
        {
            var parent = _current.Parent;

            var obj = (JObject)_current;

            foreach(var child in obj.Children())
                if(child is JProperty && ((JProperty)child).Value.Type == JTokenType.Null)
                    child.Remove();

            if (obj.Count == 0)
            {
                if (parent is JArray)
                    obj.Replace(null);
                else if (parent is JProperty)
                    ((JProperty)parent).Value = null;
            }

            _current = parent;
        }

      
        public void WritePrimitiveContents(object value, XmlSerializationHint xmlFormatHint)
        {
            JValue val;

            if (value == null)
                val = null;
            else if (value is bool)
                val = new JValue(value);
            else if (value is Int32 || value is Int16)
                val = new JValue(value);
            else if (value is decimal)
                val = new JValue(value);
            else
                val = new JValue(PrimitiveTypeConverter.ConvertTo<string>(value));

            if (_current is JArray)
                ((JArray)_current).Add(val);
            else if (_current is JProperty)
                ((JProperty)_current).Value = val;
        }


        public void WriteStartArray()
        {
            var arr = new JArray();

            ((JProperty)_current).Value = arr;

            _current = arr;
        }

        public void WriteEndArray()
        {
            var arr = (JArray)_current;

            var parent = _current.Parent;

            if (arr.Count == 0 ||
                arr.All(arrelem => arrelem.Type == JTokenType.Null))
            {
                if (parent is JArray)
                    arr.Replace(null);
                else if (parent is JProperty)
                    ((JProperty)parent).Value = null;
            }
            _current = parent;
        }

        public bool HasValueElementSupport
        {
            get { return true; }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && jw != null) ((IDisposable)jw).Dispose();
        }
    }
}
