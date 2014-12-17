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

        public static byte[] SerializeResourceToXmlBytes(Resource resource, bool summary = false)
        {
            return SerializeToXmlBytes(resource, summary);
        }

        public static string SerializeResourceToJson(Resource resource, bool summary = false)
        {
            return SerializeToJson(resource, summary);
        }

        public static byte[] SerializeResourceToJsonBytes(Resource resource, bool summary = false)
        {
            return SerializeToJsonBytes(resource, summary);
        }

        public static void SerializeResource(Resource resource, XmlWriter writer, bool summary = false)
        {
            Serialize(resource, new XmlFhirWriter(writer), summary);
        }

        public static void SerializeResource(Resource resource, JsonWriter writer, bool summary = false)
        {
            Serialize(resource, new JsonDomFhirWriter(writer), summary);
        }


        public static string SerializeMetaToXml(Resource.ResourceMetaComponent meta)
        {
            throw Error.NotImplemented("Serializing <meta> is not yet implemented");
            //return SerializeToXml(list,false);
        }

        public static byte[] SerializeMetaToXmlBytes(Resource.ResourceMetaComponent meta)
        {
            throw Error.NotImplemented("Serializing <meta> is not yet implemented");
            //return SerializeToXmlBytes(list, false);
        }

        public static string SerializeMetaToJson(Resource.ResourceMetaComponent meta)
        {
            throw Error.NotImplemented("Serializing resourceType:meta is not yet implemented");
            //return SerializeToJson(list,false);
        }


        public static byte[] SerializeMetaToJsonBytes(Resource.ResourceMetaComponent meta)
        {
            throw Error.NotImplemented("Serializing resourceType:meta is not yet implemented");
            //return SerializeToJsonBytes(list, false);
        }

        public static void SerializeMeta(Resource.ResourceMetaComponent meta, XmlWriter xw)
        {
            throw Error.NotImplemented("Serializing <meta> is not yet implemented");
            //FhirSerializer.Serialize(list, new XmlFhirWriter(xw), false);
        }

        public static void SerializeMeta(Resource.ResourceMetaComponent meta, JsonWriter jw)
        {
            throw Error.NotImplemented("Serializing resourceType:meta is not yet implemented");
            //FhirSerializer.Serialize(list, new JsonDomFhirWriter(jw), false);
        }


        internal static void Serialize(Resource instance, IFhirWriter writer, bool summary = false)
        {
            new ResourceWriter(writer).Serialize(instance, summary);
        }

        internal static string SerializeToXml(Resource instance, bool summary = false)
        {
            return xmlWriterToString(xw => Serialize(instance, new XmlFhirWriter(xw), summary));
        }

        internal static string SerializeToJson(Resource instance, bool summary = false)
        {
            return jsonWriterToString(jw => Serialize(instance, new JsonDomFhirWriter(jw), summary));
        }

        internal static byte[] SerializeToXmlBytes(Resource instance, bool summary = false)
        {
            return xmlWriterToBytes(xw => Serialize(instance, new XmlFhirWriter(xw), summary));
        }

        internal static byte[] SerializeToJsonBytes(Resource instance, bool summary = false)
        {
            return jsonWriterToBytes(jw => Serialize(instance, new JsonDomFhirWriter(jw), summary));
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
            JsonWriter jw = new JsonTextWriter(sw);

            serializer(jw);

            jw.Flush();

            return stream.ToArray();
        }

        private static string jsonWriterToString(Action<JsonWriter> serializer)
        {
            StringBuilder resultBuilder = new StringBuilder();
            StringWriter sw = new StringWriter(resultBuilder);
            JsonWriter jw = new JsonTextWriter(sw);

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
