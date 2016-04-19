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
using System.Xml;
using Hl7.Fhir.Support;
using Hl7.Fhir.Model;
using System.Xml.Linq;
using Hl7.Fhir.Introspection;

namespace Hl7.Fhir.Serialization
{
    internal class XmlFhirWriter : IFhirWriter
    {
        private XmlWriter xw;

        public XmlFhirWriter(XmlWriter xwriter)
        {
            var settings = new XmlWriterSettings();
            settings.NewLineHandling = NewLineHandling.Entitize;

            xw = XmlWriter.Create(xwriter, settings);
        }

        public void WriteStartRootObject(string name, string id, bool contained = false)
        {
            if (contained)
                WriteStartComplexContent();

            WriteStartProperty(null, name);
        }

        public void WriteEndRootObject(bool contained=false)
        {
            if (contained)
                WriteEndComplexContent();
        }


        private string _currentMemberName = null;


        public void WriteStartProperty(PropertyMapping propMap, string name)
        {
            _currentMemberName = name;
        }

        public void WriteEndProperty()
        {

        }


        private Stack<string> _memberNameStack = new Stack<string>();

        public void WriteStartComplexContent()
        {
            if (_currentMemberName == null)
                throw Error.InvalidOperation("There is no current member name set while starting complex content");

            xw.WriteStartElement(_currentMemberName, XmlNs.FHIR);

            // A new complex element starts a new scope with its own members and member names
            _memberNameStack.Push(_currentMemberName);
            _currentMemberName = null;
        }

        public void WriteEndComplexContent()
        {
            _currentMemberName = _memberNameStack.Pop();
            xw.WriteEndElement();
        }


        public void WritePrimitiveContents(object value, XmlSerializationHint xmlFormatHint)
        {
            if (value == null) throw Error.ArgumentNull("value", "There's no support for null values in Xml Fhir serialization");

            if (xmlFormatHint == XmlSerializationHint.None) xmlFormatHint = XmlSerializationHint.Attribute;

            var valueAsString = PrimitiveTypeConverter.ConvertTo<string>(value);

            if (xmlFormatHint == XmlSerializationHint.Attribute)
                xw.WriteAttributeString(_currentMemberName, valueAsString);
            else if (xmlFormatHint == XmlSerializationHint.TextNode)
                xw.WriteString(valueAsString);
            else if (xmlFormatHint == XmlSerializationHint.XhtmlElement)
            {
                var sanitized = SerializationUtil.SanitizeXml(valueAsString);
                XElement xe = XElement.Parse(sanitized);
                xe.Name = XmlNs.XHTMLNS + xe.Name.LocalName;
                    
                // Write xhtml directly into the output stream,
                // the xhtml <div> becomes part of the elements
                // of the type, just like the other FHIR elements
                var ready = xe.ToString();
                xw.WriteRaw(ready);
            }
            else
                throw new ArgumentException("Unsupported xmlFormatHint " + xmlFormatHint);
        }

        public void WriteStartArray()
        {
            //nothing
        }

        public void WriteEndArray()
        {
            //nothing
        }

        public bool HasValueElementSupport
        {
            get { return false; }
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
