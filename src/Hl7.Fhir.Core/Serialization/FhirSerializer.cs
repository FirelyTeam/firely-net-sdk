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
    public class FhirSerializer
    {
        public string SerializeResourceToXml(Resource resource, bool summary = false)
        {
            return SerializeToXml(resource, summary);
        }

        public byte[] SerializeResourceToXmlBytes(Resource resource, bool summary = false)
        {
            return SerializeToXmlBytes(resource, summary);
        }

        public string SerializeResourceToJson(Resource resource, bool summary = false)
        {
            return SerializeToJson(resource, summary);
        }

        public byte[] SerializeResourceToJsonBytes(Resource resource, bool summary = false)
        {
            return SerializeToJsonBytes(resource, summary);
        }

        public void SerializeResource(Resource resource, XmlWriter writer, bool summary = false)
        {
            Serialize(resource, new XmlFhirWriter(writer), summary);
        }

        public void SerializeResource(Resource resource, JsonWriter writer, bool summary = false)
        {
            Serialize(resource, new JsonDomFhirWriter(writer), summary);
        }


        public string SerializeMetaToXml(Resource.ResourceMetaComponent meta)
        {
            throw Error.NotImplemented("Serializing <meta> is not yet implemented");
            //return SerializeToXml(list,false);
        }

        public byte[] SerializeMetaToXmlBytes(Resource.ResourceMetaComponent meta)
        {
            throw Error.NotImplemented("Serializing <meta> is not yet implemented");
            //return SerializeToXmlBytes(list, false);
        }

        public string SerializeMetaToJson(Resource.ResourceMetaComponent meta)
        {
            throw Error.NotImplemented("Serializing resourceType:meta is not yet implemented");
            //return SerializeToJson(list,false);
        }


        public byte[] SerializeMetaToJsonBytes(Resource.ResourceMetaComponent meta)
        {
            throw Error.NotImplemented("Serializing resourceType:meta is not yet implemented");
            //return SerializeToJsonBytes(list, false);
        }

        public void SerializeMeta(Resource.ResourceMetaComponent meta, XmlWriter xw)
        {
            throw Error.NotImplemented("Serializing <meta> is not yet implemented");
            //FhirSerializer.Serialize(list, new XmlFhirWriter(xw), false);
        }

        public void SerializeMeta(Resource.ResourceMetaComponent meta, JsonWriter jw)
        {
            throw Error.NotImplemented("Serializing resourceType:meta is not yet implemented");
            //FhirSerializer.Serialize(list, new JsonDomFhirWriter(jw), false);
        }


        internal void Serialize(Resource instance, IFhirWriter writer, bool summary = false)
        {
            new ResourceWriter(writer).Serialize(instance, summary);
        }

        internal string SerializeToXml(Resource instance, bool summary = false)
        {
            return xmlWriterToString(xw => Serialize(instance, new XmlFhirWriter(xw), summary));
        }

        internal string SerializeToJson(Resource instance, bool summary = false)
        {
            return jsonWriterToString(jw => Serialize(instance, new JsonDomFhirWriter(jw), summary));
        }

        internal byte[] SerializeToXmlBytes(Resource instance, bool summary = false)
        {
            return xmlWriterToBytes(xw => Serialize(instance, new XmlFhirWriter(xw), summary));
        }

        internal byte[] SerializeToJsonBytes(Resource instance, bool summary = false)
        {
            return jsonWriterToBytes(jw => Serialize(instance, new JsonDomFhirWriter(jw), summary));
        }

        private byte[] xmlWriterToBytes(Action<XmlWriter> serializer)
        {
            MemoryStream stream = new MemoryStream();
            XmlWriterSettings settings = new XmlWriterSettings { Encoding = new UTF8Encoding(false), OmitXmlDeclaration = true };
            XmlWriter xw = XmlWriter.Create(stream, settings);

            serializer(xw);

            xw.Flush();

            return stream.ToArray();
        }

        private byte[] jsonWriterToBytes(Action<JsonWriter> serializer)
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
