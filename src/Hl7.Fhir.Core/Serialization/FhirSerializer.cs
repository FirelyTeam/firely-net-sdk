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
using Hl7.Fhir.Rest;

namespace Hl7.Fhir.Serialization
{
    public static class FhirSerializer
    {
        public static string SerializeResourceToXml(Resource resource, SummaryType summary = SummaryType.False)
        {
            return SerializeToXml(resource, summary);
        }

        public static string SerializeToXml(Base data, SummaryType summary = SummaryType.False, string root = null)
        {
            // [WMR 20160421] Explicit disposal
            return xmlWriterToString(xw => {
                using (var writer = new XmlFhirWriter(xw))
                {
                    Serialize(data, writer, summary, root);
                    xw.Flush();
                }
            });
        }

        public static byte[] SerializeResourceToXmlBytes(Resource resource, SummaryType summary = SummaryType.False)
        {
            return SerializeToXmlBytes(resource, summary);
        }

        public static byte[] SerializeToXmlBytes(Base instance, SummaryType summary = SummaryType.False, string root = null)
        {
            // [WMR 20160421] Explicit disposal
            return xmlWriterToBytes(xw => {
                using (var writer = new XmlFhirWriter(xw))
                {
                    Serialize(instance, writer, summary, root);
                    xw.Flush();
                }
            });
        }

        public static string SerializeResourceToJson(Resource resource, SummaryType summary = SummaryType.False)
        {
            return SerializeToJson(resource, summary);
        }


        public static string SerializeToJson(Base instance, SummaryType summary = SummaryType.False, string root=null)
        {
            // [WMR 20160421] Explicit disposal
            // return jsonWriterToString(jw => Serialize(instance, new JsonDomFhirWriter(jw), summary, root));
            return jsonWriterToString(jw => {
                using (var writer = new JsonDomFhirWriter(jw))
                {
                    Serialize(instance, writer, summary, root);
                    jw.Flush();
                }
            });
        }


        public static byte[] SerializeResourceToJsonBytes(Resource resource, SummaryType summary = SummaryType.False)
        {
            return SerializeToJsonBytes(resource, summary);
        }


        public static byte[] SerializeToJsonBytes(Base instance, SummaryType summary = SummaryType.False, string root = null)
        {
            // [WMR 20160421] Explicit disposal
            // return jsonWriterToBytes(jw => Serialize(instance, new JsonDomFhirWriter(jw), summary, root));
            return jsonWriterToBytes(jw => {
                using (var writer = new JsonDomFhirWriter(jw))
                {
                    Serialize(instance, writer, summary, root);
                    jw.Flush();
                }
            });
        }

        // [WMR 20160421] Caller is responsible for disposing writer
        public static void SerializeResource(Resource resource, XmlWriter writer, SummaryType summary = SummaryType.False)
        {
            Serialize(resource, new XmlFhirWriter(writer), summary);
        }

        // [WMR 20160421] Caller is responsible for disposing writer
        public static void SerializeResource(Resource resource, JsonWriter writer, SummaryType summary = SummaryType.False)
        {
            Serialize(resource, new JsonDomFhirWriter(writer), summary);
        }

        // [WMR 20160421] Caller is responsible for disposing writer
        internal static void Serialize(Base instance, IFhirWriter writer, SummaryType summary = SummaryType.False, string root = null)
        {
            new ResourceWriter(writer).Serialize(instance, summary, root: root);
        }

        private static byte[] xmlWriterToBytes(Action<XmlWriter> serializer)
        {
            // [WMR 20160421] Explicit disposal

            //MemoryStream stream = new MemoryStream();
            //XmlWriterSettings settings = new XmlWriterSettings { Encoding = new UTF8Encoding(false), OmitXmlDeclaration = true };
            //XmlWriter xw = XmlWriter.Create(stream, settings);
            //serializer(xw);
            //xw.Flush();
            //return stream.ToArray();

            using (MemoryStream stream = new MemoryStream())
            {
                XmlWriterSettings settings = new XmlWriterSettings { Encoding = new UTF8Encoding(false), OmitXmlDeclaration = true };
                using (XmlWriter xw = XmlWriter.Create(stream, settings))
                {
                    // [WMR 20160421] serializer action now calls Flush before disposing
                    serializer(xw);
                    // xw.Flush();
                    return stream.ToArray();
                }
            }
        }

        private static byte[] jsonWriterToBytes(Action<JsonWriter> serializer)
        {
            // [WMR 20160421] Explicit disposal

            //MemoryStream stream = new MemoryStream();
            //var sw = new StreamWriter(stream, new UTF8Encoding(false));
            //JsonWriter jw = new BetterDecimalJsonTextWriter(sw);
            //serializer(jw);
            //jw.Flush();
            //return stream.ToArray();

            using (MemoryStream stream = new MemoryStream())
            {
                using (var sw = new StreamWriter(stream, new UTF8Encoding(false)))
                using (JsonWriter jw = new BetterDecimalJsonTextWriter(sw))
                {
                    // [WMR 20160421] serializer action now calls Flush before disposing
                    serializer(jw);
                    // jw.Flush();
                    return stream.ToArray();
                }
            }
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

            // [WMR 20160421] Explicit disposal

            //StringWriter sw = new StringWriter(resultBuilder);
            //JsonWriter jw = new BetterDecimalJsonTextWriter(sw);
            //serializer(jw);
            //jw.Flush();
            //jw.Close();
            //return resultBuilder.ToString();

            using (StringWriter sw = new StringWriter(resultBuilder))
            using (JsonWriter jw = new BetterDecimalJsonTextWriter(sw))
            {
                // [WMR 20160421] serializer action now calls Flush before disposing
                serializer(jw);
                // jw.Flush();
                // jw.Close();
                return resultBuilder.ToString();
            }
        }


        private static string xmlWriterToString(Action<XmlWriter> serializer)
        {
            StringBuilder sb = new StringBuilder();

            // [WMR 20160421] Explicit disposal
            //XmlWriter xw = XmlWriter.Create(sb, new XmlWriterSettings { OmitXmlDeclaration = true });
            //serializer(xw);
            //xw.Flush();
            //return sb.ToString();

            using (XmlWriter xw = XmlWriter.Create(sb, new XmlWriterSettings { OmitXmlDeclaration = true }))
            {
                // [WMR 20160421] serializer action now calls Flush before disposing
                serializer(xw);
                // xw.Flush();
                return sb.ToString();
            }

        }
    }
}
