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

        public void WriteStartRootObject(string name, bool contained)
        {
            _createdRootObject = new JObject(new JProperty(JsonSerializationDetails.RESOURCETYPE_MEMBER_NAME, name));

            if (!contained)
                _root = _createdRootObject;                    
        }


        public void WriteEndRootObject(bool contained)
        {
            WriteEndProperty();
            //if (!contained)
            //{
            //   // rewriteExtensionProperties(_root);

            //    if (jw != null) _root.WriteTo(jw);
            //}
        }


        public void WriteStartProperty(string name)
        {
            // If there is no "current" node - we're trying to write a "root" property, such things
            // only exist in XML, and we can represent them simply by a JObject that will follow
            // when WriteStartComplexContent is called after this call
            if (_current != null)
            {
                var prop = new JProperty(name, null);
                ((JObject)_current).Add(prop);
                _current = prop;
            }
            else
            {
                // Note that this is much like WriteStartRootObject, except
                // we don't want the resourceType member when writing just an element
                _createdRootObject = new JObject();
                _root = _createdRootObject;
            }
        }

        public void WriteEndProperty()
        {
            if (_current is JProperty prop)
            {
                var parent = _current.Parent;

                if (prop.Value.Type == JTokenType.Null)
                    _current.Remove();

                _current = parent;
            }
            else if(_current == _createdRootObject)
                _root.WriteTo(jw);
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
