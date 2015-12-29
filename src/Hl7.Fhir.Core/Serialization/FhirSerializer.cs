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
using Hl7.Fhir.Model;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Xml;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Serialization
{
    public static class FhirSerializer
    {
        public static string SerializeResourceToXml(Resource resource, bool summary = false)
        {
            return SerializeToXml(resource, summary);
        }

        public static string SerializeToXml(Base data, bool summary = false, string root = null)
        {
            return xmlWriterToString(xw => Serialize(data, new XmlFhirWriter(xw), summary, root));
        }

        public static byte[] SerializeResourceToXmlBytes(Resource resource, bool summary = false)
        {
            return SerializeToXmlBytes(resource, summary);
        }

        public static byte[] SerializeToXmlBytes(Base instance, bool summary = false, string root = null)
        {
            return xmlWriterToBytes(xw => Serialize(instance, new XmlFhirWriter(xw), summary, root));
        }


        public static string SerializeResourceToJson(Resource resource, bool summary = false)
        {
            return SerializeToJson(resource, summary);
        }


        public static string SerializeToJson(Base instance, bool summary = false, string root=null)
        {
            return jsonWriterToString(jw => Serialize(instance, new JsonDomFhirWriter(jw), summary, root));
        }


        public static byte[] SerializeResourceToJsonBytes(Resource resource, bool summary = false)
        {
            return SerializeToJsonBytes(resource, summary);
        }


        public static byte[] SerializeToJsonBytes(Base instance, bool summary = false, string root = null)
        {
            return jsonWriterToBytes(jw => Serialize(instance, new JsonDomFhirWriter(jw), summary, root));
        }


        public static void SerializeResource(Resource resource, XmlWriter writer, bool summary = false)
        {
            Serialize(resource, new XmlFhirWriter(writer), summary);
        }

        public static void SerializeResource(Resource resource, JsonWriter writer, bool summary = false)
        {
            Serialize(resource, new JsonDomFhirWriter(writer), summary);
        }


        internal static void Serialize(Base instance, IFhirWriter writer, bool summary = false, string root = null)
        {
             new ResourceWriter(writer).Serialize(instance, summary, root: root);
        }


        private static byte[] xmlWriterToBytes(Action<XmlWriter> serializer)
        {
            MemoryStream stream = new MemoryStream();
            XmlWriterSettings settings = new XmlWriterSettings { Encoding = new UTF8Encoding(false), OmitXmlDeclaration = true };
            XmlWriter xw = XmlWriter.Create(stream, settings);

            serializer(xw);

            xw.Flush();

            return stream.ToArray();
        }

        private static byte[] jsonWriterToBytes(Action<JsonWriter> serializer)
        {
            MemoryStream stream = new MemoryStream();

            var sw = new StreamWriter(stream, new UTF8Encoding(false));
            JsonWriter jw = new BetterDecimalJsonTextWriter(sw);

            serializer(jw);

            jw.Flush();

            return stream.ToArray();
        }


        internal class BetterDecimalJsonTextWriter : JsonTextWriter
        {
            public BetterDecimalJsonTextWriter(TextWriter textWriter) : base(textWriter)
            {
            }

            public override void WriteValue(decimal value)
            {
                WriteRawValue(value.ToString(this.Culture));
            }

            public override void WriteValue(decimal? value)
            {
                if (value.HasValue)
                    WriteRawValue(value.Value.ToString(this.Culture));
                else
                    WriteNull();
            }
        }

        private static string jsonWriterToString(Action<JsonWriter> serializer)
        {
            StringBuilder resultBuilder = new StringBuilder();
            StringWriter sw = new StringWriter(resultBuilder);
            JsonWriter jw = new BetterDecimalJsonTextWriter(sw);

            serializer(jw);
            jw.Flush();
            jw.Close();

            return resultBuilder.ToString();
        }


        private static string xmlWriterToString(Action<XmlWriter> serializer)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriter xw = XmlWriter.Create(sb, new XmlWriterSettings { OmitXmlDeclaration = true });

            serializer(xw);

            xw.Flush();
           

            return sb.ToString();
        }
    }
}
