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
    public partial class FhirSerializer
    {
        internal static void Serialize(object instance, IFhirWriter writer, bool summary=false)
        {
            new ResourceWriter(writer).Serialize(instance, summary);
        }

        internal static string SerializeToXml(object instance, bool summary=false)
        {
            return xmlWriterToString(xw => FhirSerializer.Serialize(instance, new XmlFhirWriter(xw), summary));
        }

        internal static string SerializeToJson(object instance, bool summary=false)
        {
            return jsonWriterToString(jw => FhirSerializer.Serialize(instance, new JsonDomFhirWriter(jw), summary));
        }

        internal static byte[] SerializeToXmlBytes(object instance, bool summary=false)
        {
            return xmlWriterToBytes(xw => FhirSerializer.Serialize(instance, new XmlFhirWriter(xw), summary));
        }

        internal static byte[] SerializeToJsonBytes(object instance, bool summary=false)
        {
            return jsonWriterToBytes(jw => FhirSerializer.Serialize(instance, new JsonDomFhirWriter(jw), summary));
        }

        public static string SerializeResourceToXml(Resource resource, bool summary=false)
        {
            return SerializeToXml(resource, summary);
        }

        public static string SerializeTagListToXml(TagList list)
        {
            return SerializeToXml(list,false);
        }

        public static byte[] SerializeResourceToXmlBytes(Resource resource, bool summary=false)
        {
            return SerializeToXmlBytes(resource, summary);
        }

        public static byte[] SerializeTagListToXmlBytes(TagList list)
        {
            return SerializeToXmlBytes(list, false);
        }

        public static string SerializeResourceToJson(Resource resource, bool summary=false)
        {
            return SerializeToJson(resource, summary);
        }

        public static string SerializeTagListToJson(TagList list)
        {
            return SerializeToJson(list,false);
        }

        public static byte[] SerializeResourceToJsonBytes(Resource resource, bool summary=false)
        {
            return SerializeToJsonBytes(resource, summary);
        }

        public static byte[] SerializeTagListToJsonBytes(TagList list)
        {
            return SerializeToJsonBytes(list, false);
        }


        public static void SerializeResource(Resource resource, JsonWriter writer, bool summary=false)
        {
            FhirSerializer.Serialize(resource, new JsonDomFhirWriter(writer), summary);
        }

        public static void SerializeTagList(TagList list, JsonWriter jw)
        {
            FhirSerializer.Serialize(list, new JsonDomFhirWriter(jw), false);
        }

        public static void SerializeTagList(TagList list, XmlWriter xw)
        {
            FhirSerializer.Serialize(list, new XmlFhirWriter(xw), false);
        }

        public static void SerializeResource(Resource resource, XmlWriter writer, bool summary=false)
        {
            FhirSerializer.Serialize(resource, new XmlFhirWriter(writer), summary);
        }


        public static void SerializeBundle(Bundle bundle, JsonWriter writer, bool summary=false)
        {
            BundleJsonSerializer.WriteTo(bundle, writer, summary);
        }

        public static void SerializeBundle(Bundle bundle, XmlWriter writer, bool summary=false)
        {
            BundleXmlSerializer.WriteTo(bundle, writer, summary);
        }

        public static string SerializeBundleToJson(Bundle bundle, bool summary=false)
        {
            return jsonWriterToString(jw => BundleJsonSerializer.WriteTo(bundle, jw, summary));
        }

        public static string SerializeBundleToXml(Bundle bundle, bool summary=false)
        {
            return xmlWriterToString(xw => BundleXmlSerializer.WriteTo(bundle, xw, summary));
        }

        public static byte[] SerializeBundleToJsonBytes(Bundle bundle, bool summary=false)
        {
            return jsonWriterToBytes(jw => BundleJsonSerializer.WriteTo(bundle, jw, summary));
        }

        public static byte[] SerializeBundleToXmlBytes(Bundle bundle, bool summary=false)
        {
            return xmlWriterToBytes(xw => BundleXmlSerializer.WriteTo(bundle, xw, summary));
        }

        public static void SerializeBundleEntry(BundleEntry entry, JsonWriter writer, bool summary=false)
        {
            BundleJsonSerializer.WriteTo(entry, writer, summary);
        }

        public static void SerializeBundleEntry(BundleEntry entry, XmlWriter writer, bool summary=false)
        {
            BundleXmlSerializer.WriteTo(entry, writer,summary);
        }

        public static string SerializeBundleEntryToJson(BundleEntry entry, bool summary=false)
        {
            return jsonWriterToString(jw=>BundleJsonSerializer.WriteTo(entry, jw, summary));
        }

        public static string SerializeBundleEntryToXml(BundleEntry entry, bool summary=false)
        {
            return xmlWriterToString(xw => BundleXmlSerializer.WriteTo(entry, xw, summary));
        }

        public static byte[] SerializeBundleEntryToJsonBytes(BundleEntry entry, bool summary=false)
        {
            return jsonWriterToBytes(jw => BundleJsonSerializer.WriteTo(entry, jw, summary));
        }

        public static byte[] SerializeBundleEntryToXmlBytes(BundleEntry entry, bool summary=false)
        {
            return xmlWriterToBytes(xw => BundleXmlSerializer.WriteTo(entry, xw, summary));
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
