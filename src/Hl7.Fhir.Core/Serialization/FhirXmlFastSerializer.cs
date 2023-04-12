using Hl7.Fhir.Model;
using System;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public class FhirXmlFastSerializer : BaseFhirSerializer
    {
        public FhirXmlFastSerializer(Model.Version version) : base(version)
        {
        }

        public FhirXmlFastSerializer(SerializerSettings settings) : base(settings)
        {
        }

        public string SerializeToString(Base instance, Rest.SummaryType summary = Rest.SummaryType.False, string root = null, string[] elements = null) =>
           Utility.SerializationUtil.WriteXmlToString(xmlWriter => Serialize(instance, xmlWriter, summary, root, elements), Settings.Pretty);

        public byte[] SerializeToBytes(Base instance, Rest.SummaryType summary = Rest.SummaryType.False, string root = null, string[] elements = null) =>
           Utility.SerializationUtil.WriteXmlToBytes(xmlWriter => Serialize(instance, xmlWriter, summary, root, elements));

        public XDocument SerializeToDocument(Base instance, Rest.SummaryType summary = Rest.SummaryType.False, string root = null, string[] elements = null) =>
           Utility.SerializationUtil.WriteXmlToDocument(xmlWriter => Serialize(instance, xmlWriter, summary, root, elements));

        public void Serialize(Base instance, XmlWriter writer, Rest.SummaryType summary = Rest.SummaryType.False, string root = null, string[] elements = null)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            if (writer == null) throw new ArgumentNullException(nameof(writer));

            var target = new XmlSerializerTarget(writer, root);
            var serializerSink = new GenericSerializerSink(target, Settings.Version, summary, elements);
            instance.Serialize(serializerSink);
        }
    }
}
