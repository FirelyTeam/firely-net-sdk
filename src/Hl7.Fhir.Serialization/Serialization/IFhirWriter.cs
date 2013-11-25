using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Serialization
{
    public interface IFhirWriter : IDisposable
    {
        void WriteStartRootObject(string name);
        void WriteEndRootObject();

        void WriteStartElement(string name);
        void WriteEndElement();

        void WriteStartComplexContent();
        void WriteEndComplexContent();

        void WritePrimitiveContents(string name, object value, XmlSerializationHint xmlFormatHint);

        void WriteStartArrayElement(string name);
        void WriteStartArrayMember(string name);
        void WriteEndArrayMember();
        void WriteEndArrayElement();
    }
}
