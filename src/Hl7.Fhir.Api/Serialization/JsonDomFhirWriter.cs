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
 
        public JsonDomFhirWriter(JsonWriter jwriter)
        {
            jw = jwriter;
        }

        internal JsonDomFhirWriter()
        {
        }

        private string _rootType;
        
        public void WriteStartRootObject(string name, bool contained)
        {
            _rootType = name;
        }


        public void WriteEndRootObject(bool contained)
        {
        }

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


        internal JObject Result;

        public void WriteStartComplexContent()
        {
            if (_current == null)
                _current = new JObject();
            else
            {
                JObject obj = new JObject();

                if (_current is JProperty)
                    ((JProperty)_current).Value = obj;
                else if (_current is JArray)
                    ((JArray)_current).Add(obj);

                _current = obj;
            }

            // When this is the first complex scope when writing a resource,
            // emit a type member
            if(_rootType != null)
            {
                ((JObject)_current).Add(new JProperty(JsonDomFhirReader.RESOURCETYPE_MEMBER_NAME, _rootType));
                _rootType = null;
            }
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
            if (parent == null)
            {
                Result = (JObject)_current;
                if(jw != null) _current.WriteTo(jw);
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
