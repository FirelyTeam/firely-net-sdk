/*
  Copyright (c) 2011-2013, HL7, Inc.
  All rights reserved.
  
  Redistribution and use in source and binary forms, with or without modification, 
  are permitted provided that the following conditions are met:
  
   * Redistributions of source code must retain the above copyright notice, this 
     list of conditions and the following disclaimer.
   * Redistributions in binary form must reproduce the above copyright notice, 
     this list of conditions and the following disclaimer in the documentation 
     and/or other materials provided with the distribution.
   * Neither the name of HL7 nor the names of its contributors may be used to 
     endorse or promote products derived from this software without specific 
     prior written permission.
  
  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
  POSSIBILITY OF SUCH DAMAGE.
  

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
                val = new JValue(PrimitiveTypeConverter.Convert<string>(value));

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
