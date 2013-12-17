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

namespace Hl7.Fhir.Serialization
{
    internal class JsonFhirWriter : IFhirWriter
    {
        private JsonWriter jw;
        
        public JsonFhirWriter(JsonWriter jwriter)
        {
            jw = jwriter;
        }

        private string _rootType;
        
        public void WriteStartRootObject(string name, bool contained)
        {
            _rootType = name;
        }


        public void WriteEndRootObject(bool contained)
        {
         //   jw.WriteEndObject();
        }

        public void WriteStartProperty(string name)
        {
            jw.WritePropertyName(name);
        }

        public void WriteEndProperty()
        {

        }

        public void WriteStartComplexContent()
        {
            jw.WriteStartObject();

            // When this is the first complex scope when writing a resource,
            // emit a type member
            if(_rootType != null)
            {
                WriteStartProperty(SerializationConfig.RESOURCETYPE_MEMBER_NAME);
                WritePrimitiveContents(_rootType, XmlSerializationHint.None);
                _rootType = null;
            }
        }

        public void WriteEndComplexContent()
        {
            jw.WriteEndObject();
        }

      
        public void WritePrimitiveContents(object value, XmlSerializationHint xmlFormatHint)
        {
            if (value == null)
                jw.WriteNull();
            else if (value is bool)
                jw.WriteValue((bool)value);
            else if (value is Int32 || value is Int16)
                jw.WriteValue((int)value);
            else if (value is decimal)
                jw.WriteValue((decimal)value);
            else
                jw.WriteValue(PrimitiveTypeConverter.Convert<string>(value));
        }


        public void WriteStartArray()
        {         
            jw.WriteStartArray();
        }

        public void WriteEndArray()
        {
            jw.WriteEndArray();
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
