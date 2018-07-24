/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Text;
using Hl7.Fhir.Model;
using System.IO;
using Newtonsoft.Json;
using System.Xml;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Utility;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public class BaseFhirSerializer
    {
        public ParserSettings Settings { get; private set; }

        public BaseFhirSerializer(ParserSettings settings)
        {
            if (settings == null) throw Error.ArgumentNull(nameof(settings));
            Settings = settings;
        }

        public BaseFhirSerializer()
        {
            Settings = new ParserSettings();
        }
#pragma warning disable 612, 618
        // [WMR 20160421] Caller is responsible for disposing writer
        internal protected void Serialize(Base instance, IFhirWriter writer, SummaryType summary = SummaryType.False, string root = null)
        {
            if (instance is Resource resource)
                new ResourceWriter(writer, Settings).Serialize(resource, summary);
            else
                new ComplexTypeWriter(writer, Settings).Serialize(instance, summary, root: root);
        }
#pragma warning restore 612, 618
    }

    public class FhirXmlSerializer : BaseFhirSerializer
    {
        public FhirXmlSerializer() : base()
        {
        }

        public FhirXmlSerializer(ParserSettings settings) : base(settings)
        {
        }

        public string SerializeToString(Base data, SummaryType summary = SummaryType.False, string root = null)
        {
            // [WMR 20160421] Explicit disposal
            return SerializationUtil.WriteXmlToString(xw =>
            {
                using (var writer = new XmlFhirWriter(xw))
                {
                    Serialize(data, writer, summary, root);
                    xw.Flush();
                }
            });
        }

        public byte[] SerializeToBytes(Base instance, SummaryType summary = SummaryType.False, string root = null)
        {
            // [WMR 20160421] Explicit disposal
            return SerializationUtil.WriteXmlToBytes(xw =>
            {
                using (var writer = new XmlFhirWriter(xw))
                {
                    Serialize(instance, writer, summary, root);
                    xw.Flush();
                }
            });
        }

#if NET45
        // [WMR 20180409] NEW
        // https://github.com/ewoutkramer/fhir-net-api/issues/545
        public XDocument SerializeToDocument(Base instance, SummaryType summary = SummaryType.False, string root = null)
        {
            return xmlWriterToDocument(xw =>
            {
                using (var writer = new XmlFhirWriter(xw))
                {
                    Serialize(instance, writer, summary, root);
                    xw.Flush();
                }
            });
        }
#endif

        // [WMR 20160421] Caller is responsible for disposing writer
        public void Serialize(Base instance, XmlWriter writer, SummaryType summary = SummaryType.False) => Serialize(instance, new XmlFhirWriter(writer), summary);
    }

    public class FhirJsonSerializer : BaseFhirSerializer
    {
        public FhirJsonSerializer() : base()
        {
        }

        public FhirJsonSerializer(ParserSettings settings) : base(settings)
        {
        }

        public string SerializeToString(Base instance, SummaryType summary = SummaryType.False)
        {
            // [WMR 20160421] Explicit disposal
            // return jsonWriterToString(jw => Serialize(instance, new JsonDomFhirWriter(jw), summary, root));
            return SerializationUtil.WriteJsonToString(jw =>
            {
                using (var writer = new JsonDomFhirWriter(jw))
                {
                    Serialize(instance, writer, summary, null);
                    jw.Flush();
                }
            });
        }

        public byte[] SerializeToBytes(Base instance, SummaryType summary = SummaryType.False)
        {
            // [WMR 20160421] Explicit disposal
            // return jsonWriterToBytes(jw => Serialize(instance, new JsonDomFhirWriter(jw), summary, root));
            return SerializationUtil.WriteJsonToBytes(jw =>
            {
                using (var writer = new JsonDomFhirWriter(jw))
                {
                    Serialize(instance, writer, summary, null);
                    jw.Flush();
                }
            });
        }

        // [WMR 20180409] NEW
        // https://github.com/ewoutkramer/fhir-net-api/issues/545
        public JObject SerializeToDocument(Base instance, SummaryType summary = SummaryType.False)
        {
            return jsonWriterToDocument(jw =>
            {
                using (var writer = new JsonDomFhirWriter(jw))
                {
                    Serialize(instance, writer, summary, null);
                    jw.Flush();
                }
            });
        }

        // [WMR 20160421] Caller is responsible for disposing writer
        public void Serialize(Base instance, JsonWriter writer, SummaryType summary = SummaryType.False) => Serialize(instance, new JsonDomFhirWriter(writer), summary);
    }

    [Obsolete("Obsolete. Instead, create a new FhirXmlSerializer or FhirJsonSerializer instance and call one of the serialization methods.")]
    public static class FhirSerializer
    {
        private static FhirXmlSerializer _xmlSerializer = new FhirXmlSerializer();
        private static FhirJsonSerializer _jsonSerializer = new FhirJsonSerializer();

        [Obsolete("Create a new FhirXmlSerializer and call SerializeToString()")]
        public static string SerializeResourceToXml(Resource resource, SummaryType summary = SummaryType.False) => _xmlSerializer.SerializeToString(resource, summary);

        [Obsolete("Create a new FhirXmlSerializer and call SerializeToString()")]
        public static string SerializeToXml(Base data, SummaryType summary = SummaryType.False, string root = null) => _xmlSerializer.SerializeToString(data, summary, root);

        [Obsolete("Create a new FhirXmlSerializer and call SerializeToBytes()")]
        public static byte[] SerializeResourceToXmlBytes(Resource resource, SummaryType summary = SummaryType.False) => _xmlSerializer.SerializeToBytes(resource, summary);

        [Obsolete("Create a new FhirXmlSerializer and call SerializeToBytes()")]
        public static byte[] SerializeToXmlBytes(Base instance, SummaryType summary = SummaryType.False, string root = null) => _xmlSerializer.SerializeToBytes(instance, summary, root);

        [Obsolete("Create a new FhirXmlSerializer and call Serialize()")]
        public static void SerializeResource(Resource resource, XmlWriter writer, SummaryType summary = SummaryType.False) => _xmlSerializer.Serialize(resource, writer, summary);

        [Obsolete("Create a new FhirJsonSerializer and call SerializeToString()")]
        public static string SerializeResourceToJson(Resource resource, SummaryType summary = SummaryType.False) => _jsonSerializer.SerializeToString(resource, summary);

        [Obsolete("Create a new FhirJsonSerializer and call SerializeToString()")]
        public static string SerializeToJson(Base instance, SummaryType summary = SummaryType.False) => _jsonSerializer.SerializeToString(instance, summary);

        [Obsolete("Create a new FhirJsonSerializer and call SerializeToBytes()")]
        public static byte[] SerializeResourceToJsonBytes(Resource resource, SummaryType summary = SummaryType.False) => _jsonSerializer.SerializeToBytes(resource, summary);

        [Obsolete("Create a new FhirJsonSerializer and call SerializeToBytes()")]
        public static byte[] SerializeToJsonBytes(Base instance, SummaryType summary = SummaryType.False) => _jsonSerializer.SerializeToBytes(instance, summary);

        [Obsolete("Create a new FhirJsonSerializer and call Serialize()")]
        public static void SerializeResource(Resource resource, JsonWriter writer, SummaryType summary = SummaryType.False) => _jsonSerializer.Serialize(resource, writer, summary);
    }
}
