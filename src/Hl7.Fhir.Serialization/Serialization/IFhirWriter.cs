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
        void EmitResourceTypeName(string name);

        void WriteStartRootObject(string name);
        void WriteEndRootObject();

        void WriteStartMember(string name);
        void WriteEndMember();

        void WriteStartComplexContent();
        void WriteEndComplexContent();

        void WritePrimitiveContents(string name, object value, XmlSerializationHint xmlFormatHint);

        void WriteStartArray(string name);
        void WriteStartArrayElement(string name);
        void WriteEndArrayElement();
        void WriteEndArray();
        void WriteArrayNull();
    }
}
