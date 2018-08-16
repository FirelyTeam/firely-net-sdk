/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

#if NET_XSD_SCHEMA
using System.Xml.Schema;
#endif

namespace Hl7.Fhir.Utility
{
    public static class SerializationUtil
    {
        public static bool ProbeIsXml(string data)
        {
            Regex xml = new Regex("^<[^>]+>");

            return xml.IsMatch(data.TrimStart());
        }

        public static bool ProbeIsJson(string data) => data.TrimStart().StartsWith("{");


        private static XDocument XDocumentFromReaderInternal(XmlReader reader, bool ignoreComments = false)
        {
            try
            {
                return XDocument.Load(SerializationUtil.WrapXmlReader(reader, ignoreComments: false),
                            LoadOptions.SetLineInfo);
            }
            catch (XmlException xec)
            {
                throw Error.Format("Invalid Xml encountered. Details: " + xec.Message, xec);
            }
        }

        public static XDocument XDocumentFromReader(XmlReader reader, bool ignoreComments = true)
            => XDocumentFromReaderInternal(WrapXmlReader(reader, ignoreComments));

        public static JObject JObjectFromReader(JsonReader reader)
        {
            // Override settings, sorry but this is vital
            reader.DateParseHandling = DateParseHandling.None;
            reader.FloatParseHandling = FloatParseHandling.Decimal;

            try
            {
                return JObject.Load(reader,
                    new JsonLoadSettings
                    {
                        CommentHandling = CommentHandling.Ignore,
                        LineInfoHandling = LineInfoHandling.Load
                    });
            }
            catch (JsonReaderException jre)
            {
                throw new FormatException($"Invalid Json encountered. Details: " + jre.Message, jre);
            }
        }


        public static XDocument XDocumentFromXmlText(string xml, bool ignoreComments = true)
        {
            using (var reader = XmlReaderFromXmlText(xml))
            {
                return XDocumentFromReaderInternal(reader);
            }
        }

        public static JObject JObjectFromJsonText(string json)
        {
            using (var reader = JsonReaderFromJsonText(json))
            {
                return JObjectFromReader(reader);
            }
        }


        public static XmlReader XmlReaderFromXmlText(string xml, bool ignoreComments = true)
            => WrapXmlReader(XmlReader.Create(new StringReader(SerializationUtil.SanitizeXml(xml))), ignoreComments);

        public static JsonReader JsonReaderFromJsonText(string json)
            => JsonReaderFromTextReader(new StringReader(json));

        public static XmlReader XmlReaderFromStream(Stream input, bool ignoreComments = true)
            => WrapXmlReader(XmlReader.Create(input), ignoreComments);

        public static JsonReader JsonReaderFromStream(Stream input)
            => JsonReaderFromTextReader(new StreamReader(input));

        private static JsonReader JsonReaderFromTextReader(TextReader input)
        {
            return new JsonTextReader(input)
            {
                DateParseHandling = DateParseHandling.None,
                FloatParseHandling = FloatParseHandling.Decimal,
                CloseInput = false      // Unbelievable, the default is 'true' (and is false for the XmlReaderSettings :-()
            };
        }


        public static XmlReader WrapXmlReader(XmlReader xmlReader, bool ignoreComments = true)
        {
            var settings = new XmlReaderSettings
            {
                IgnoreComments = ignoreComments,
                IgnoreProcessingInstructions = true,
                IgnoreWhitespace = true,
                DtdProcessing = DtdProcessing.Prohibit,
            };

            return XmlReader.Create(xmlReader, settings);
        }

        public static byte[] WriteXmlToBytes(Action<XmlWriter> serializer)
        {
            // [WMR 20160421] Explicit disposal
            using (MemoryStream stream = new MemoryStream())
            {
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Encoding = new UTF8Encoding(false),
                    OmitXmlDeclaration = true,
                    NewLineHandling = NewLineHandling.Entitize
                };

                using (XmlWriter xw = XmlWriter.Create(stream, settings))
                {
                    serializer(xw);
                    xw.Flush();
                    return stream.ToArray();
                }
            }
        }

        public static string WriteXmlToString(Action<XmlWriter> serializer, bool pretty=false)
        {
            StringBuilder sb = new StringBuilder();

            XmlWriterSettings settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                NewLineHandling = NewLineHandling.Entitize,
                Indent = pretty
            };

            // [WMR 20160421] Explicit disposal
            using (XmlWriter xw = XmlWriter.Create(sb, settings))
            {
                serializer(xw);
                xw.Flush();
                return sb.ToString();
            }
        }

        public static XDocument WriteXmlToDocument(Action<XmlWriter> serializer)
        {
            var doc = new XDocument();

            using (XmlWriter xw = doc.CreateWriter())
            {
                serializer(xw);
                xw.Flush();
            }

            return doc;
        }

        public static string WriteJsonToString(Action<JsonWriter> serializer, bool pretty=false)
        {
            StringBuilder resultBuilder = new StringBuilder();

            // [WMR 20160421] Explicit disposal
            using (StringWriter sw = new StringWriter(resultBuilder))
            using (JsonWriter jw = SerializationUtil.CreateJsonTextWriter(sw))
            {
                jw.Formatting = pretty ? Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None;
                serializer(jw);
                jw.Flush();
                return resultBuilder.ToString();
            }
        }

        public static byte[] WriteJsonToBytes(Action<JsonWriter> serializer)
        {
            // [WMR 20160421] Explicit disposal
            using (MemoryStream stream = new MemoryStream())
            {
                using (var sw = new StreamWriter(stream, new UTF8Encoding(false)))
                using (JsonWriter jw = SerializationUtil.CreateJsonTextWriter(sw))
                {
                    serializer(jw);
                    jw.Flush();
                    sw.Flush();
                    return stream.ToArray();
                }
            }
        }

        public static JObject WriteJsonToDocument(Action<JsonWriter> serializer)
        {
            // [WMR 20180409] Triggers runtime exception "Can not add Newtonsoft.Json.Linq.JObject to Newtonsoft.Json.Linq.JObject."
            // JsonDomFhirWriter.WriteEndProperty() => _root.WriteTo(jw) => jw.WriteStartObject() => exception...
            //var doc = new JObject();

            // JConstructor / JArray works, extract and return first child node
            var doc = new JArray();

            using (JsonWriter jw = doc.CreateWriter())
            {
                serializer(jw);
                jw.Flush();
            }

            //return doc;
            System.Diagnostics.Debug.Assert(doc.Count == 1);
            return doc.First as JObject;
        }

        public static JsonWriter CreateJsonTextWriter(TextWriter writer) => new BetterDecimalJsonTextWriter(writer);

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



        /// <summary>
        /// Replace all the XML specific special characters with the XHTML equivalents
        /// </summary>
        /// <remarks>
        /// this is based on own research plus combining with results shown here:
        /// http://www.codeproject.com/Articles/298519/Fast-Token-Replacement-in-Csharp
        /// The RegEx approach does not require multiple passes or string creations
        /// while replacing all the items.
        /// It occurs in O(n) StringBuilder concatenations + O(n) dictionary lookups
        /// </remarks>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string SanitizeXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                return xml;

            Dictionary<string, string> xr = getXmlReplacements();

            //s.Reset();
            //s.Start();
            string resultRE;
            var matches = _re.Matches(xml);
            if (matches.Count > 0)
            {
                StringBuilder sbRE = new StringBuilder();
                int currentPosition = 0;
                foreach (Match m in matches)
                {
                    if (xr.ContainsKey(m.Value))
                    {
                        sbRE.Append(xml.Substring(currentPosition, m.Index - currentPosition));
                        sbRE.Append(xr[m.Value]);
                        currentPosition = m.Index + m.Length;
                    }
                    // System.Diagnostics.Trace.WriteLine(String.Format("{0} - {1}: {2}", m.Index, m.Length, m.Value));
                }
                sbRE.Append(xml.Substring(currentPosition));
                resultRE = sbRE.ToString();
            }
            else
            {
                resultRE = xml;
            }

            return resultRE;
        }

        //#if NET_FILESYSTEM
        //        public static void JoinFiles(string[] inputFilePaths, string outputFilePath)
        //        {
        //            using (var outputStream = File.Create(outputFilePath))
        //            {
        //                foreach (var inputFilePath in inputFilePaths)
        //                {
        //                    using (var inputStream = File.OpenRead(inputFilePath))
        //                    {
        //                        // Buffer size can be passed as the second argument.
        //                        inputStream.CopyTo(outputStream);
        //                    }
        //                }
        //            }
        //        }
        //#endif

#if NET_XSD_SCHEMA
        public static string[] RunFhirXhtmlSchemaValidation(string xmlText)
        {
            try
            {
                var doc = SerializationUtil.XDocumentFromXmlText(xmlText);
                return RunFhirXhtmlSchemaValidation(doc);
            }
            catch (FormatException fe)
            {
                return new[] { fe.Message };
            }
        }

        public static string[] RunFhirXhtmlSchemaValidation(XDocument doc)
        {
            var result = new List<string>();

            if (!doc.Root.AtXhtmlDiv())
                return new[] { $"Root element of XHTML is not a <div> from the XHTML namespace ({XmlNs.XHTML})." };

            doc.Validate(_xhtmlSchemaSet.Value, (s, a) => result.Add(a.Message));
            return result.ToArray();
        }

        private static Lazy<XmlSchemaSet> _xhtmlSchemaSet = new Lazy<XmlSchemaSet>(compileXhtmlSchema, true);

        private static XmlSchemaSet compileXhtmlSchema()
        {
            XmlSchemaSet schemas = new XmlSchemaSet();

            var schema = new StringReader(Serialization.Properties.Resources.xml);
            schemas.Add(null, XmlReader.Create(schema));   // null = use schema namespace as specified in schema file

            schema = new StringReader(Serialization.Properties.Resources.fhir_xhtml);
            schemas.Add(null, XmlReader.Create(schema));   // null = use schema namespace as specified in schema file

            schemas.Compile();

            return schemas;
        }
#endif



#if NET_REGEX_COMPILE
        private static Regex _re = new Regex("(&[a-zA-Z0-9]+;)", RegexOptions.Compiled | RegexOptions.CultureInvariant);
#else
        private static Regex _re = new Regex("(&[a-zA-Z0-9]+;)", RegexOptions.CultureInvariant);
#endif
        private static Dictionary<string, string> _xmlReplacements;
        private static Dictionary<string, string> getXmlReplacements()
        {
            if (_xmlReplacements != null)
                return _xmlReplacements;

            Dictionary<string, string> xr = new Dictionary<string, string>
            {
                { "&quot;", "&#34;" },
                { "&amp;", "&#38;" },
                { "&lt;", "&#60;" },
                { "&gt;", "&#62;" },
                { "&apos;", "&#39;" },
                { "&OElig;", "&#338;" },
                { "&oelig;", "&#339;" },
                { "&Scaron;", "&#352;" },
                { "&scaron;", "&#353;" },
                { "&Yuml;", "&#376;" },
                { "&circ;", "&#710;" },
                { "&tilde;", "&#732;" },
                { "&ensp;", "&#8194;" },
                { "&emsp;", "&#8195;" },
                { "&thinsp;", "&#8201;" },
                { "&zwnj;", "&#8204;" },
                { "&zwj;", "&#8205;" },
                { "&lrm;", "&#8206;" },
                { "&rlm;", "&#8207;" },
                { "&ndash;", "&#8211;" },
                { "&mdash;", "&#8212;" },
                { "&lsquo;", "&#8216;" },
                { "&rsquo;", "&#8217;" },
                { "&sbquo;", "&#8218;" },
                { "&ldquo;", "&#8220;" },
                { "&rdquo;", "&#8221;" },
                { "&bdquo;", "&#8222;" },
                { "&dagger;", "&#8224;" },
                { "&Dagger;", "&#8225;" },
                { "&permil;", "&#8240;" },
                { "&lsaquo;", "&#8249;" },
                { "&rsaquo;", "&#8250;" },
                { "&euro;", "&#8364;" },
                { "&fnof;", "&#402;" },
                { "&Alpha;", "&#913;" },
                { "&Beta;", "&#914;" },
                { "&Gamma;", "&#915;" },
                { "&Delta;", "&#916;" },
                { "&Epsilon;", "&#917;" },
                { "&Zeta;", "&#918;" },
                { "&Eta;", "&#919;" },
                { "&Theta;", "&#920;" },
                { "&Iota;", "&#921;" },
                { "&Kappa;", "&#922;" },
                { "&Lambda;", "&#923;" },
                { "&Mu;", "&#924;" },
                { "&Nu;", "&#925;" },
                { "&Xi;", "&#926;" },
                { "&Omicron;", "&#927;" },
                { "&Pi;", "&#928;" },
                { "&Rho;", "&#929;" },
                { "&Sigma;", "&#931;" },
                { "&Tau;", "&#932;" },
                { "&Upsilon;", "&#933;" },
                { "&Phi;", "&#934;" },
                { "&Chi;", "&#935;" },
                { "&Psi;", "&#936;" },
                { "&Omega;", "&#937;" },
                { "&alpha;", "&#945;" },
                { "&beta;", "&#946;" },
                { "&gamma;", "&#947;" },
                { "&delta;", "&#948;" },
                { "&epsilon;", "&#949;" },
                { "&zeta;", "&#950;" },
                { "&eta;", "&#951;" },
                { "&theta;", "&#952;" },
                { "&iota;", "&#953;" },
                { "&kappa;", "&#954;" },
                { "&lambda;", "&#955;" },
                { "&mu;", "&#956;" },
                { "&nu;", "&#957;" },
                { "&xi;", "&#958;" },
                { "&omicron;", "&#959;" },
                { "&pi;", "&#960;" },
                { "&rho;", "&#961;" },
                { "&sigmaf;", "&#962;" },
                { "&sigma;", "&#963;" },
                { "&tau;", "&#964;" },
                { "&upsilon;", "&#965;" },
                { "&phi;", "&#966;" },
                { "&chi;", "&#967;" },
                { "&psi;", "&#968;" },
                { "&omega;", "&#969;" },
                { "&thetasym;", "&#977;" },
                { "&upsih;", "&#978;" },
                { "&piv;", "&#982;" },
                { "&bull;", "&#8226;" },
                { "&hellip;", "&#8230;" },
                { "&prime;", "&#8242;" },
                { "&Prime;", "&#8243;" },
                { "&oline;", "&#8254;" },
                { "&frasl;", "&#8260;" },
                { "&weierp;", "&#8472;" },
                { "&image;", "&#8465;" },
                { "&real;", "&#8476;" },
                { "&trade;", "&#8482;" },
                { "&alefsym;", "&#8501;" },
                { "&larr;", "&#8592;" },
                { "&uarr;", "&#8593;" },
                { "&rarr;", "&#8594;" },
                { "&darr;", "&#8595;" },
                { "&harr;", "&#8596;" },
                { "&crarr;", "&#8629;" },
                { "&lArr;", "&#8656;" },
                { "&uArr;", "&#8657;" },
                { "&rArr;", "&#8658;" },
                { "&dArr;", "&#8659;" },
                { "&hArr;", "&#8660;" },
                { "&forall;", "&#8704;" },
                { "&part;", "&#8706;" },
                { "&exist;", "&#8707;" },
                { "&empty;", "&#8709;" },
                { "&nabla;", "&#8711;" },
                { "&isin;", "&#8712;" },
                { "&notin;", "&#8713;" },
                { "&ni;", "&#8715;" },
                { "&prod;", "&#8719;" },
                { "&sum;", "&#8721;" },
                { "&minus;", "&#8722;" },
                { "&lowast;", "&#8727;" },
                { "&radic;", "&#8730;" },
                { "&prop;", "&#8733;" },
                { "&infin;", "&#8734;" },
                { "&ang;", "&#8736;" },
                { "&and;", "&#8743;" },
                { "&or;", "&#8744;" },
                { "&cap;", "&#8745;" },
                { "&cup;", "&#8746;" },
                { "&int;", "&#8747;" },
                { "&there4;", "&#8756;" },
                { "&sim;", "&#8764;" },
                { "&cong;", "&#8773;" },
                { "&asymp;", "&#8776;" },
                { "&ne;", "&#8800;" },
                { "&equiv;", "&#8801;" },
                { "&le;", "&#8804;" },
                { "&ge;", "&#8805;" },
                { "&sub;", "&#8834;" },
                { "&sup;", "&#8835;" },
                { "&nsub;", "&#8836;" },
                { "&sube;", "&#8838;" },
                { "&supe;", "&#8839;" },
                { "&oplus;", "&#8853;" },
                { "&otimes;", "&#8855;" },
                { "&perp;", "&#8869;" },
                { "&sdot;", "&#8901;" },
                { "&lceil;", "&#8968;" },
                { "&rceil;", "&#8969;" },
                { "&lfloor;", "&#8970;" },
                { "&rfloor;", "&#8971;" },
                { "&lang;", "&#9001;" },
                { "&rang;", "&#9002;" },
                { "&loz;", "&#9674;" },
                { "&spades;", "&#9824;" },
                { "&clubs;", "&#9827;" },
                { "&hearts;", "&#9829;" },
                { "&diams;", "&#9830;" },
                { "&nbsp;", "&#160;" },
                { "&iexcl;", "&#161;" },
                { "&cent;", "&#162;" },
                { "&pound;", "&#163;" },
                { "&curren;", "&#164;" },
                { "&yen;", "&#165;" },
                { "&brvbar;", "&#166;" },
                { "&sect;", "&#167;" },
                { "&uml;", "&#168;" },
                { "&copy;", "&#169;" },
                { "&ordf;", "&#170;" },
                { "&laquo;", "&#171;" },
                { "&not;", "&#172;" },
                { "&shy;", "&#173;" },
                { "&reg;", "&#174;" },
                { "&macr;", "&#175;" },
                { "&deg;", "&#176;" },
                { "&plusmn;", "&#177;" },
                { "&sup2;", "&#178;" },
                { "&sup3;", "&#179;" },
                { "&acute;", "&#180;" },
                { "&micro;", "&#181;" },
                { "&para;", "&#182;" },
                { "&middot;", "&#183;" },
                { "&cedil;", "&#184;" },
                { "&sup1;", "&#185;" },
                { "&ordm;", "&#186;" },
                { "&raquo;", "&#187;" },
                { "&frac14;", "&#188;" },
                { "&frac12;", "&#189;" },
                { "&frac34;", "&#190;" },
                { "&iquest;", "&#191;" },
                { "&Agrave;", "&#192;" },
                { "&Aacute;", "&#193;" },
                { "&Acirc;", "&#194;" },
                { "&Atilde;", "&#195;" },
                { "&Auml;", "&#196;" },
                { "&Aring;", "&#197;" },
                { "&AElig;", "&#198;" },
                { "&Ccedil;", "&#199;" },
                { "&Egrave;", "&#200;" },
                { "&Eacute;", "&#201;" },
                { "&Ecirc;", "&#202;" },
                { "&Euml;", "&#203;" },
                { "&Igrave;", "&#204;" },
                { "&Iacute;", "&#205;" },
                { "&Icirc;", "&#206;" },
                { "&Iuml;", "&#207;" },
                { "&ETH;", "&#208;" },
                { "&Ntilde;", "&#209;" },
                { "&Ograve;", "&#210;" },
                { "&Oacute;", "&#211;" },
                { "&Ocirc;", "&#212;" },
                { "&Otilde;", "&#213;" },
                { "&Ouml;", "&#214;" },
                { "&times;", "&#215;" },
                { "&Oslash;", "&#216;" }
            };

            _xmlReplacements = xr;
            return xr;
        }
    }
}
