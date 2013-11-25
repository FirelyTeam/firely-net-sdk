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

        public void WriteStartRootObject(string name)
        {
        }

        public void EmitResourceTypeName(string name)
        {        
            WritePrimitiveContents(SerializationConfig.RESOURCETYPE_MEMBER_NAME, name, XmlSerializationHint.None);
        }

        public void WriteEndRootObject()
        {
         //   jw.WriteEndObject();
        }

        public void WriteStartMember(string name)
        {
            jw.WritePropertyName(name);
        }

        public void WriteEndMember()
        {
            // Nothing
        }

        public void WriteStartComplexContent()
        {
            jw.WriteStartObject();
        }

        public void WriteEndComplexContent()
        {
            jw.WriteEndObject();
        }

      
        public void WritePrimitiveContents(string name, object value, XmlSerializationHint xmlFormatHint)
        {
            WriteStartMember(name);

            if (value is bool)
                jw.WriteValue((bool)value);
            else if (value is Int32 || value is Int16)
                jw.WriteValue((int)value);
            else if (value is decimal)
                jw.WriteValue((decimal)value);
            else
                jw.WriteValue(PrimitiveTypeConverter.Convert<string>(value));
        }


        public void WriteStartArray(string name)
        {
            jw.WritePropertyName(name);
            jw.WriteStartArray();
        }

        public void WriteStartArrayElement(string name)
        {
            // Nothing
        }

        public void WriteEndArrayElement()
        {
            // Nothing
        }

        public void WriteEndArray()
        {
            jw.WriteEndArray();
        }

        public void WriteArrayNull()
        {
            jw.WriteNull();
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
