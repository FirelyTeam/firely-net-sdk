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
using System.Xml;
using Hl7.Fhir.Support;
using Hl7.Fhir.Model;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    internal class XmlFhirWriter : IFhirWriter
    {
        private XmlWriter xw;

        public void EmitResourceTypeName(string name)
        {
        }

        public XmlFhirWriter(XmlWriter xwriter)
        {
            xw = xwriter;
        }

        public void WriteStartRootObject(string name)
        {
            WriteStartMember(name);
        }

        public void WriteEndRootObject()
        {
            WriteEndMember();
        }

        public void WriteStartMember(string name)
        {
            xw.WriteStartElement(name, Util.FHIRNS);            
        }

        public void WriteEndMember()
        {
            xw.WriteEndElement();
        }

        public void WriteStartComplexContent()
        {
            // Nothing
        }

        public void WriteEndComplexContent()
        {
            // Nothing
        }

        public void WritePrimitiveContents(string name, object value, XmlSerializationHint xmlFormatHint)
        {
            if (xmlFormatHint == XmlSerializationHint.None) xmlFormatHint = XmlSerializationHint.Attribute;

            var valueAsString = PrimitiveTypeConverter.Convert<string>(value);

            if (xmlFormatHint == XmlSerializationHint.Attribute)
                writePrimitiveAttribute(name, valueAsString);
            else if (xmlFormatHint == XmlSerializationHint.TextNode)
                xw.WriteString(valueAsString);
            else if (xmlFormatHint == XmlSerializationHint.XhtmlElement)
            {
                XNamespace xhtml = Support.Util.XHTMLNS;
                XElement xe = XElement.Parse(valueAsString);
                xe.Name = xhtml + xe.Name.LocalName;
                    
                // Write xhtml directly into the output stream,
                // the xhtml <div> becomes part of the elements
                // of the type, just like the other FHIR elements
                xw.WriteRaw(xe.ToString());
            }
            else
                throw new ArgumentException("Unsupported xmlFormatHint " + xmlFormatHint);
        }

        private void writePrimitiveAttribute(string name, string value)
        {
            xw.WriteAttributeString(name, value);
        }


        public void WriteStartArray(string name)
        {
            // Nothing
        }

        public void WriteStartArrayElement(string name)
        {
            WriteStartMember(name);
        }

        public void WriteEndArrayElement()
        {
            WriteEndMember();
        }

        public void WriteEndArray()
        {
            // Nothing
        }

        public void WriteArrayNull()
        {
            // Nothing - not supported in XML
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && xw != null) ((IDisposable)xw).Dispose();
        }
    }
}
