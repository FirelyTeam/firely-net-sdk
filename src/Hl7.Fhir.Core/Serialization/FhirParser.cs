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
using Hl7.Fhir.Support;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;


namespace Hl7.Fhir.Serialization
{
    public class FhirParser
    {
        #region Helper methods / stream creation methods
        public static bool ProbeIsXml(string data)
        {
            Regex xml = new Regex("^<[^>]+>");

            return xml.IsMatch(data.TrimStart());
        }

        public static bool ProbeIsJson(string data)
        {
            return data.TrimStart().StartsWith("{");
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
        internal static string SanitizeXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                return xml;

            Dictionary<string, string> xr = GetXmlReplacements();

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
            //s.Stop();
            //lre = s.ElapsedMilliseconds;
            //System.Diagnostics.Trace.WriteLine(String.Format("String.Replace: {0}   StringBuilder.Replace: {1}  RegEx.Replace: {2}", lsr, lsb, lre));

            //if (string.Compare(resultRE, result, StringComparison.InvariantCulture) != 0)
            //    System.Diagnostics.Debug.Fail("Algorithms don't match (string/re)");
            //if (resultSB != result)
            //    System.Diagnostics.Debug.Fail("Algorithms don't match (string/sb)");
            return resultRE;
        }

#if PORTABLE45
        private static Regex _re = new Regex("(&[a-zA-Z0-9]+;)", RegexOptions.CultureInvariant);
#else
        private static Regex _re = new Regex("(&[a-zA-Z0-9]+;)", RegexOptions.Compiled | RegexOptions.CultureInvariant);
#endif
        private static Dictionary<string, string> _xmlReplacements;
        private static Dictionary<string, string> GetXmlReplacements()
        {
            if (_xmlReplacements != null)
                return _xmlReplacements;

            Dictionary<string, string> xr = new Dictionary<string, string>();
            xr.Add("&quot;", "&#34;");
            xr.Add("&amp;", "&#38;");
            xr.Add("&lt;", "&#60;");
            xr.Add("&gt;", "&#62;");
            xr.Add("&apos;", "&#39;");
            xr.Add("&OElig;", "&#338;");
            xr.Add("&oelig;", "&#339;"); 
            xr.Add("&Scaron;", "&#352;"); 
            xr.Add("&scaron;", "&#353;");
            xr.Add("&Yuml;", "&#376;");
            xr.Add("&circ;", "&#710;");
            xr.Add("&tilde;", "&#732;");
            xr.Add("&ensp;", "&#8194;");
            xr.Add("&emsp;", "&#8195;");
            xr.Add("&thinsp;", "&#8201;");
            xr.Add("&zwnj;", "&#8204;");
            xr.Add("&zwj;", "&#8205;");
            xr.Add("&lrm;", "&#8206;");
            xr.Add("&rlm;", "&#8207;");
            xr.Add("&ndash;", "&#8211;");
            xr.Add("&mdash;", "&#8212;");
            xr.Add("&lsquo;", "&#8216;");
            xr.Add("&rsquo;", "&#8217;");
            xr.Add("&sbquo;", "&#8218;");
            xr.Add("&ldquo;", "&#8220;");
            xr.Add("&rdquo;", "&#8221;");
            xr.Add("&bdquo;", "&#8222;");
            xr.Add("&dagger;", "&#8224;"); 
            xr.Add("&Dagger;", "&#8225;"); 
            xr.Add("&permil;", "&#8240;");
            xr.Add("&lsaquo;", "&#8249;");
            xr.Add("&rsaquo;", "&#8250;");
            xr.Add("&euro;", "&#8364;");
            xr.Add("&fnof;", "&#402;");
            xr.Add("&Alpha;", "&#913;");
            xr.Add("&Beta;", "&#914;");
            xr.Add("&Gamma;", "&#915;");
            xr.Add("&Delta;", "&#916;");
            xr.Add("&Epsilon;", "&#917;");
            xr.Add("&Zeta;", "&#918;");
            xr.Add("&Eta;", "&#919;");
            xr.Add("&Theta;", "&#920;");
            xr.Add("&Iota;", "&#921;");
            xr.Add("&Kappa;", "&#922;");
            xr.Add("&Lambda;", "&#923;");
            xr.Add("&Mu;", "&#924;");
            xr.Add("&Nu;", "&#925;");
            xr.Add("&Xi;", "&#926;");
            xr.Add("&Omicron;", "&#927;");
            xr.Add("&Pi;", "&#928;");
            xr.Add("&Rho;", "&#929;");
            xr.Add("&Sigma;", "&#931;");
            xr.Add("&Tau;", "&#932;");
            xr.Add("&Upsilon;", "&#933;");
            xr.Add("&Phi;", "&#934;");
            xr.Add("&Chi;", "&#935;");
            xr.Add("&Psi;", "&#936;");
            xr.Add("&Omega;", "&#937;");
            xr.Add("&alpha;", "&#945;");
            xr.Add("&beta;", "&#946;");
            xr.Add("&gamma;", "&#947;");
            xr.Add("&delta;", "&#948;");
            xr.Add("&epsilon;", "&#949;");
            xr.Add("&zeta;", "&#950;");
            xr.Add("&eta;", "&#951;");
            xr.Add("&theta;", "&#952;");
            xr.Add("&iota;", "&#953;");
            xr.Add("&kappa;", "&#954;");
            xr.Add("&lambda;", "&#955;");
            xr.Add("&mu;", "&#956;");
            xr.Add("&nu;", "&#957;");
            xr.Add("&xi;", "&#958;");
            xr.Add("&omicron;", "&#959;");
            xr.Add("&pi;", "&#960;");
            xr.Add("&rho;", "&#961;");
            xr.Add("&sigmaf;", "&#962;");
            xr.Add("&sigma;", "&#963;");
            xr.Add("&tau;", "&#964;");
            xr.Add("&upsilon;", "&#965;");
            xr.Add("&phi;", "&#966;");
            xr.Add("&chi;", "&#967;");
            xr.Add("&psi;", "&#968;");
            xr.Add("&omega;", "&#969;");
            xr.Add("&thetasym;", "&#977;");
            xr.Add("&upsih;", "&#978;");
            xr.Add("&piv;", "&#982;");
            xr.Add("&bull;", "&#8226;");
            xr.Add("&hellip;", "&#8230;");
            xr.Add("&prime;", "&#8242;");
            xr.Add("&Prime;", "&#8243;");
            xr.Add("&oline;", "&#8254;");
            xr.Add("&frasl;", "&#8260;");
            xr.Add("&weierp;", "&#8472;");
            xr.Add("&image;", "&#8465;");
            xr.Add("&real;", "&#8476;");
            xr.Add("&trade;", "&#8482;");
            xr.Add("&alefsym;", "&#8501;");
            xr.Add("&larr;", "&#8592;"); xr.Add("&uarr;", "&#8593;");
            xr.Add("&rarr;", "&#8594;"); xr.Add("&darr;", "&#8595;"); xr.Add("&harr;", "&#8596;");
            xr.Add("&crarr;", "&#8629;");
            xr.Add("&lArr;", "&#8656;"); xr.Add("&uArr;", "&#8657;");
            xr.Add("&rArr;", "&#8658;"); xr.Add("&dArr;", "&#8659;"); xr.Add("&hArr;", "&#8660;");
            xr.Add("&forall;", "&#8704;"); xr.Add("&part;", "&#8706;"); xr.Add("&exist;", "&#8707;");
            xr.Add("&empty;", "&#8709;"); xr.Add("&nabla;", "&#8711;"); xr.Add("&isin;", "&#8712;");
            xr.Add("&notin;", "&#8713;"); xr.Add("&ni;", "&#8715;"); xr.Add("&prod;", "&#8719;");
            xr.Add("&sum;", "&#8721;"); xr.Add("&minus;", "&#8722;"); xr.Add("&lowast;", "&#8727;");
            xr.Add("&radic;", "&#8730;"); xr.Add("&prop;", "&#8733;"); xr.Add("&infin;", "&#8734;");
            xr.Add("&ang;", "&#8736;"); xr.Add("&and;", "&#8743;"); xr.Add("&or;", "&#8744;");
            xr.Add("&cap;", "&#8745;"); xr.Add("&cup;", "&#8746;"); xr.Add("&int;", "&#8747;");
            xr.Add("&there4;", "&#8756;"); xr.Add("&sim;", "&#8764;"); xr.Add("&cong;", "&#8773;");
            xr.Add("&asymp;", "&#8776;"); xr.Add("&ne;", "&#8800;"); xr.Add("&equiv;", "&#8801;");
            xr.Add("&le;", "&#8804;"); xr.Add("&ge;", "&#8805;"); xr.Add("&sub;", "&#8834;");
            xr.Add("&sup;", "&#8835;"); xr.Add("&nsub;", "&#8836;"); xr.Add("&sube;", "&#8838;");
            xr.Add("&supe;", "&#8839;"); xr.Add("&oplus;", "&#8853;"); xr.Add("&otimes;", "&#8855;");
            xr.Add("&perp;", "&#8869;"); xr.Add("&sdot;", "&#8901;"); xr.Add("&lceil;", "&#8968;");
            xr.Add("&rceil;", "&#8969;"); xr.Add("&lfloor;", "&#8970;"); xr.Add("&rfloor;", "&#8971;");
            xr.Add("&lang;", "&#9001;"); xr.Add("&rang;", "&#9002;"); xr.Add("&loz;", "&#9674;");
            xr.Add("&spades;", "&#9824;"); xr.Add("&clubs;", "&#9827;"); xr.Add("&hearts;", "&#9829;");
            xr.Add("&diams;", "&#9830;");
            xr.Add("&nbsp;", "&#160;"); xr.Add("&iexcl;", "&#161;"); xr.Add("&cent;", "&#162;");
            xr.Add("&pound;", "&#163;"); xr.Add("&curren;", "&#164;"); xr.Add("&yen;", "&#165;");
            xr.Add("&brvbar;", "&#166;"); xr.Add("&sect;", "&#167;"); xr.Add("&uml;", "&#168;");
            xr.Add("&copy;", "&#169;"); xr.Add("&ordf;", "&#170;"); xr.Add("&laquo;", "&#171;");
            xr.Add("&not;", "&#172;"); xr.Add("&shy;", "&#173;"); xr.Add("&reg;", "&#174;");
            xr.Add("&macr;", "&#175;"); xr.Add("&deg;", "&#176;"); xr.Add("&plusmn;", "&#177;");
            xr.Add("&sup2;", "&#178;"); xr.Add("&sup3;", "&#179;"); xr.Add("&acute;", "&#180;");
            xr.Add("&micro;", "&#181;"); xr.Add("&para;", "&#182;"); xr.Add("&middot;", "&#183;");
            xr.Add("&cedil;", "&#184;"); xr.Add("&sup1;", "&#185;"); xr.Add("&ordm;", "&#186;");
            xr.Add("&raquo;", "&#187;"); xr.Add("&frac14;", "&#188;"); xr.Add("&frac12;", "&#189;");
            xr.Add("&frac34;", "&#190;"); xr.Add("&iquest;", "&#191;");
            xr.Add("&Agrave;", "&#192;");
            xr.Add("&Aacute;", "&#193;"); xr.Add("&Acirc;", "&#194;"); xr.Add("&Atilde;", "&#195;");
            xr.Add("&Auml;", "&#196;"); xr.Add("&Aring;", "&#197;"); xr.Add("&AElig;", "&#198;");
            xr.Add("&Ccedil;", "&#199;"); xr.Add("&Egrave;", "&#200;"); xr.Add("&Eacute;", "&#201;");
            xr.Add("&Ecirc;", "&#202;"); xr.Add("&Euml;", "&#203;"); xr.Add("&Igrave;", "&#204;");
            xr.Add("&Iacute;", "&#205;"); xr.Add("&Icirc;", "&#206;"); xr.Add("&Iuml;", "&#207;");
            xr.Add("&ETH;", "&#208;"); xr.Add("&Ntilde;", "&#209;"); xr.Add("&Ograve;", "&#210;");
            xr.Add("&Oacute;", "&#211;"); xr.Add("&Ocirc;", "&#212;"); xr.Add("&Otilde;", "&#213;");
            xr.Add("&Ouml;", "&#214;"); xr.Add("&times;", "&#215;"); xr.Add("&Oslash;", "&#216;");
            
            _xmlReplacements = xr;
            return xr;
        }

        public static XDocument XDocumentFromXml(string xml)
        {
            return XDocument.Parse(SanitizeXml(xml));
        }

        public static JObject JObjectFromJson(string json)
        {
            return JObject.Load(JsonReaderFromJson(json));
        }

        public static JsonReader JsonReaderFromJson(string json)
        {
            JsonReader reader = new JsonTextReader(new StringReader(json));
            reader.DateParseHandling = DateParseHandling.None;
            reader.FloatParseHandling = FloatParseHandling.Decimal;

            return reader;
        }

        public static XmlReader XmlReaderFromXml(string xml)
        {
            return XmlReader.Create(new StringReader(SanitizeXml(xml)));
        }

        public static XmlReader XmlReaderFromStream(Stream input)
        {
            if (input.Position != 0)
            {
                if (input.CanSeek)
                    input.Seek(0, SeekOrigin.Begin);
                else
                    throw Error.InvalidOperation("Stream is not at beginning, and seeking is not supported by this stream");
            }

            return XmlReader.Create(input);
        }

        public static IFhirReader FhirReaderFromXml(string xml)
        {
            return new XmlDomFhirReader(XmlReaderFromXml(xml));
        }

        public static IFhirReader FhirReaderFromJson(string json)
        {
            return new JsonDomFhirReader(JsonReaderFromJson(json));
        }

#endregion

        internal static Base Parse(IFhirReader reader, Type dataType = null)
        {
            if (dataType == null)
                return new ResourceReader(reader).Deserialize();
            else
                return new ComplexTypeReader(reader).Deserialize(dataType);
        }

        public static Resource ParseResourceFromXml(string xml)
        {
            return (Resource)ParseFromXml(xml);
        }

        public static Base ParseFromXml(string xml, Type dataType = null)
        {
            var reader = FhirReaderFromXml(xml);
            return Parse(reader, dataType);
        }

        public static Resource ParseResourceFromJson(string json)
        {
            return (Resource)ParseFromJson(json);
        }

        public static Base ParseFromJson(string json, Type dataType = null)
        {
            var reader = FhirReaderFromJson(json);
            return Parse(reader, dataType);
        }

        public static Resource ParseResource(XmlReader reader)
        {
            return (Resource)Parse(reader);
        }

        public static Resource ParseResource(JsonReader reader)
        {
            return (Resource)Parse(reader);
        }

        public static Base Parse(XmlReader reader, Type dataType = null)
        {
            var xmlReader = new XmlDomFhirReader(reader);
            return Parse(xmlReader);
        }

        public static Base Parse(JsonReader reader, Type dataType = null)
        {
            var jsonReader = new JsonDomFhirReader(reader);
            return Parse(jsonReader);
        }
    }
}
