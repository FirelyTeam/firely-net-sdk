/*
  Copyright (c) 2011-2012, HL7, Inc
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
        public static string SerializeResourceToXml(Resource resource, bool summary = false)
        {
            return xmlWriterToString( xw => FhirSerializer.SerializeResource(resource, new XmlFhirWriter(xw), summary) );
        }

        internal static void SerializeResource(Resource resource, IFhirWriter writer, bool summary)
        {
            new ResourceWriter(writer).Serialize(resource);
        }

        public static string SerializeTagListToXml(IEnumerable<Tag> list)
        {
            return xmlWriterToString(xw => TagListSerializer.SerializeTagList(list, xw));
        }

        public static byte[] SerializeResourceToXmlBytes(Resource resource, bool summary = false)
        {
            return xmlWriterToBytes(xw => FhirSerializer.SerializeResource(resource, new XmlFhirWriter(xw), summary));
        }

        public static byte[] SerializeTagListToXmlBytes(IEnumerable<Tag> list)
        {
            return xmlWriterToBytes(xw => TagListSerializer.SerializeTagList(list, xw));
        }

        public static string SerializeResourceToJson(Resource resource, bool summary = false)
        {
            return jsonWriterToString(jw => FhirSerializer.SerializeResource(resource, new JsonDomFhirWriter(jw), summary));
        }

        public static string SerializeTagListToJson(IList<Tag> list)
        {
            return jsonWriterToString(jw => TagListSerializer.SerializeTagList(list, jw));
        }

        public static byte[] SerializeResourceToJsonBytes(Resource resource, bool summary = false)
        {
            return jsonWriterToBytes(jw => FhirSerializer.SerializeResource(resource, new JsonDomFhirWriter(jw), summary));
        }

        public static byte[] SerializeTagListToJsonBytes(IEnumerable<Tag> list)
        {
            return jsonWriterToBytes(jw => TagListSerializer.SerializeTagList(list, jw));
        }


        public static void SerializeResource(Resource resource, JsonWriter writer, bool summary = false)
        {
            FhirSerializer.SerializeResource(resource, new JsonDomFhirWriter(writer), summary);
        }

        public static void SerializeTagList(IList<Tag> list, JsonWriter jw)
        {
            TagListSerializer.SerializeTagList(list, jw);
        }

        public static void SerializeTagList(IList<Tag> list, XmlWriter xw)
        {
            TagListSerializer.SerializeTagList(list, xw);
        }

        public static void SerializeResource(Resource resource, XmlWriter writer, bool summary = false)
        {
            FhirSerializer.SerializeResource(resource, new XmlFhirWriter(writer), summary);
        }

        public static void SerializeBundle(Bundle bundle, JsonWriter writer, bool summary = false)
        {
            BundleJsonSerializer.WriteTo(bundle, writer, summary);
        }

        public static void SerializeBundle(Bundle bundle, XmlWriter writer, bool summary = false)
        {
            BundleXmlSerializer.WriteTo(bundle, writer, summary);
        }

        public static string SerializeBundleToJson(Bundle bundle, bool summary = false)
        {
            return jsonWriterToString(jw => BundleJsonSerializer.WriteTo(bundle, jw, summary));
        }

        public static string SerializeBundleToXml(Bundle bundle, bool summary = false)
        {
            return xmlWriterToString(xw => BundleXmlSerializer.WriteTo(bundle, xw, summary));
        }

        public static byte[] SerializeBundleToJsonBytes(Bundle bundle, bool summary = false)
        {
            return jsonWriterToBytes(jw => BundleJsonSerializer.WriteTo(bundle, jw, summary));
        }

        public static byte[] SerializeBundleToXmlBytes(Bundle bundle, bool summary = false)
        {
            return xmlWriterToBytes(xw => BundleXmlSerializer.WriteTo(bundle, xw, summary));
        }

        public static void SerializeBundleEntry(Bundle entry, JsonWriter writer, bool summary = false)
        {
            BundleJsonSerializer.WriteTo(entry, writer, summary);
        }

        public static void SerializeBundleEntry(Bundle entry, XmlWriter writer, bool summary = false)
        {
            BundleXmlSerializer.WriteTo(entry, writer,summary);
        }

        public static string SerializeBundleEntryToJson(BundleEntry entry, bool summary = false)
        {
            return jsonWriterToString(jw=>BundleJsonSerializer.WriteTo(entry, jw, summary));
        }

        public static string SerializeBundleEntryToXml(BundleEntry entry, bool summary = false)
        {
            return xmlWriterToString(xw => BundleXmlSerializer.WriteTo(entry, xw, summary));
        }

        public static byte[] SerializeBundleEntryToJsonBytes(BundleEntry entry, bool summary = false)
        {
            return jsonWriterToBytes(jw => BundleJsonSerializer.WriteTo(entry, jw, summary));
        }

        public static byte[] SerializeBundleEntryToXmlBytes(BundleEntry entry, bool summary = false)
        {
            return xmlWriterToBytes(xw => BundleXmlSerializer.WriteTo(entry, xw, summary));
        }

        public static XElement SerializeResourceAsXElement(Resource resource, bool summary = false)
        {
            return XElement.Parse(SerializeResourceToXml(resource, summary));
        }

        private static byte[] xmlWriterToBytes(Action<XmlWriter> serializer)
        {
            MemoryStream stream = new MemoryStream();
            XmlWriterSettings settings = new XmlWriterSettings { Encoding = new UTF8Encoding(false) };
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
            XmlWriter xw = XmlWriter.Create(sb);

            serializer(xw);

            xw.Flush();
           

            return sb.ToString();
        }
    }
}
