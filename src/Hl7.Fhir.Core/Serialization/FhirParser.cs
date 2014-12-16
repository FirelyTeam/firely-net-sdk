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
        public static bool ProbeIsXml(string data)
        {
            Regex xml = new Regex("^<[^>]+>");

            return xml.IsMatch(data.TrimStart());
        }

        public static bool ProbeIsJson(string data)
        {
            return data.TrimStart().StartsWith("{");
        }

        internal static string SanitizeXml(string xml)
        {
            return xml
                .Replace("&quot;", "&#34;").Replace("&amp;", "&#38;").Replace("&lt;", "&#60;").Replace("&gt;", "&#62;").Replace("&apos;", "&#39;").Replace("&OElig;", "&#338;").Replace("&oelig;", "&#339;").Replace("&Scaron;", "&#352;").Replace("&scaron;", "&#353;").Replace("&Yuml;", "&#376;").Replace("&circ;", "&#710;").Replace("&tilde;", "&#732;").Replace("&ensp;", "&#8194;").Replace("&emsp;", "&#8195;").Replace("&thinsp;", "&#8201;").Replace("&zwnj;", "&#8204;").Replace("&zwj;", "&#8205;").Replace("&lrm;", "&#8206;").Replace("&rlm;", "&#8207;").Replace("&ndash;", "&#8211;").Replace("&mdash;", "&#8212;").Replace("&lsquo;", "&#8216;").Replace("&rsquo;", "&#8217;").Replace("&sbquo;", "&#8218;").Replace("&ldquo;", "&#8220;").Replace("&rdquo;", "&#8221;").Replace("&bdquo;", "&#8222;").Replace("&dagger;", "&#8224;").Replace("&Dagger;", "&#8225;").Replace("&permil;", "&#8240;").Replace("&lsaquo;", "&#8249;").Replace("&rsaquo;", "&#8250;").Replace("&euro;", "&#8364;")
                .Replace("&fnof;", "&#402;").Replace("&Alpha;", "&#913;").Replace("&Beta;", "&#914;").Replace("&Gamma;", "&#915;").Replace("&Delta;", "&#916;").Replace("&Epsilon;", "&#917;").Replace("&Zeta;", "&#918;").Replace("&Eta;", "&#919;").Replace("&Theta;", "&#920;").Replace("&Iota;", "&#921;").Replace("&Kappa;", "&#922;").Replace("&Lambda;", "&#923;").Replace("&Mu;", "&#924;").Replace("&Nu;", "&#925;").Replace("&Xi;", "&#926;").Replace("&Omicron;", "&#927;").Replace("&Pi;", "&#928;").Replace("&Rho;", "&#929;").Replace("&Sigma;", "&#931;").Replace("&Tau;", "&#932;").Replace("&Upsilon;", "&#933;").Replace("&Phi;", "&#934;").Replace("&Chi;", "&#935;").Replace("&Psi;", "&#936;").Replace("&Omega;", "&#937;").Replace("&alpha;", "&#945;").Replace("&beta;", "&#946;").Replace("&gamma;", "&#947;").Replace("&delta;", "&#948;").Replace("&epsilon;", "&#949;").Replace("&zeta;", "&#950;").Replace("&eta;", "&#951;").Replace("&theta;", "&#952;").Replace("&iota;", "&#953;").Replace("&kappa;", "&#954;").Replace("&lambda;", "&#955;").Replace("&mu;", "&#956;").Replace("&nu;", "&#957;").Replace("&xi;", "&#958;").Replace("&omicron;", "&#959;").Replace("&pi;", "&#960;").Replace("&rho;", "&#961;").Replace("&sigmaf;", "&#962;").Replace("&sigma;", "&#963;").Replace("&tau;", "&#964;").Replace("&upsilon;", "&#965;").Replace("&phi;", "&#966;").Replace("&chi;", "&#967;").Replace("&psi;", "&#968;").Replace("&omega;", "&#969;").Replace("&thetasym;", "&#977;").Replace("&upsih;", "&#978;").Replace("&piv;", "&#982;").Replace("&bull;", "&#8226;").Replace("&hellip;", "&#8230;").Replace("&prime;", "&#8242;").Replace("&Prime;", "&#8243;").Replace("&oline;", "&#8254;").Replace("&frasl;", "&#8260;").Replace("&weierp;", "&#8472;").Replace("&image;", "&#8465;").Replace("&real;", "&#8476;").Replace("&trade;", "&#8482;").Replace("&alefsym;", "&#8501;").Replace("&larr;", "&#8592;").Replace("&uarr;", "&#8593;").Replace("&rarr;", "&#8594;").Replace("&darr;", "&#8595;").Replace("&harr;", "&#8596;").Replace("&crarr;", "&#8629;").Replace("&lArr;", "&#8656;").Replace("&uArr;", "&#8657;").Replace("&rArr;", "&#8658;").Replace("&dArr;", "&#8659;").Replace("&hArr;", "&#8660;").Replace("&forall;", "&#8704;").Replace("&part;", "&#8706;").Replace("&exist;", "&#8707;").Replace("&empty;", "&#8709;").Replace("&nabla;", "&#8711;").Replace("&isin;", "&#8712;").Replace("&notin;", "&#8713;").Replace("&ni;", "&#8715;").Replace("&prod;", "&#8719;").Replace("&sum;", "&#8721;").Replace("&minus;", "&#8722;").Replace("&lowast;", "&#8727;").Replace("&radic;", "&#8730;").Replace("&prop;", "&#8733;").Replace("&infin;", "&#8734;").Replace("&ang;", "&#8736;").Replace("&and;", "&#8743;").Replace("&or;", "&#8744;").Replace("&cap;", "&#8745;").Replace("&cup;", "&#8746;").Replace("&int;", "&#8747;").Replace("&there4;", "&#8756;").Replace("&sim;", "&#8764;").Replace("&cong;", "&#8773;").Replace("&asymp;", "&#8776;").Replace("&ne;", "&#8800;").Replace("&equiv;", "&#8801;").Replace("&le;", "&#8804;").Replace("&ge;", "&#8805;").Replace("&sub;", "&#8834;").Replace("&sup;", "&#8835;").Replace("&nsub;", "&#8836;").Replace("&sube;", "&#8838;").Replace("&supe;", "&#8839;").Replace("&oplus;", "&#8853;").Replace("&otimes;", "&#8855;").Replace("&perp;", "&#8869;").Replace("&sdot;", "&#8901;").Replace("&lceil;", "&#8968;").Replace("&rceil;", "&#8969;").Replace("&lfloor;", "&#8970;").Replace("&rfloor;", "&#8971;").Replace("&lang;", "&#9001;").Replace("&rang;", "&#9002;").Replace("&loz;", "&#9674;").Replace("&spades;", "&#9824;").Replace("&clubs;", "&#9827;").Replace("&hearts;", "&#9829;").Replace("&diams;", "&#9830;")
                .Replace("&nbsp;", "&#160;").Replace("&iexcl;", "&#161;").Replace("&cent;", "&#162;").Replace("&pound;", "&#163;").Replace("&curren;", "&#164;").Replace("&yen;", "&#165;").Replace("&brvbar;", "&#166;").Replace("&sect;", "&#167;").Replace("&uml;", "&#168;").Replace("&copy;", "&#169;").Replace("&ordf;", "&#170;").Replace("&laquo;", "&#171;").Replace("&not;", "&#172;").Replace("&shy;", "&#173;").Replace("&reg;", "&#174;").Replace("&macr;", "&#175;").Replace("&deg;", "&#176;").Replace("&plusmn;", "&#177;").Replace("&sup2;", "&#178;").Replace("&sup3;", "&#179;").Replace("&acute;", "&#180;").Replace("&micro;", "&#181;").Replace("&para;", "&#182;").Replace("&middot;", "&#183;").Replace("&cedil;", "&#184;").Replace("&sup1;", "&#185;").Replace("&ordm;", "&#186;").Replace("&raquo;", "&#187;").Replace("&frac14;", "&#188;").Replace("&frac12;", "&#189;").Replace("&frac34;", "&#190;").Replace("&iquest;", "&#191;").Replace("&Agrave;", "&#192;").Replace("&Aacute;", "&#193;").Replace("&Acirc;", "&#194;").Replace("&Atilde;", "&#195;").Replace("&Auml;", "&#196;").Replace("&Aring;", "&#197;").Replace("&AElig;", "&#198;").Replace("&Ccedil;", "&#199;").Replace("&Egrave;", "&#200;").Replace("&Eacute;", "&#201;").Replace("&Ecirc;", "&#202;").Replace("&Euml;", "&#203;").Replace("&Igrave;", "&#204;").Replace("&Iacute;", "&#205;").Replace("&Icirc;", "&#206;").Replace("&Iuml;", "&#207;").Replace("&ETH;", "&#208;").Replace("&Ntilde;", "&#209;").Replace("&Ograve;", "&#210;").Replace("&Oacute;", "&#211;").Replace("&Ocirc;", "&#212;").Replace("&Otilde;", "&#213;").Replace("&Ouml;", "&#214;").Replace("&times;", "&#215;").Replace("&Oslash;", "&#216;")                                  
                ;
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

        public Resource ParseResourceFromXml(string xml)
        {
            var reader = FhirReaderFromXml(xml);
            return new ResourceReader(reader).Deserialize();
        }

        public Resource ParseResourceFromJson(string json)
        {
            var reader = FhirReaderFromJson(json);
            return new ResourceReader(reader).Deserialize();
        }

        public Resource ParseResource(XmlReader reader)
        {
            var xmlReader = new XmlDomFhirReader(reader);
            return new ResourceReader(xmlReader).Deserialize();
        }

        public Resource ParseResource(JsonReader reader)
        {
            var jsonReader = new JsonDomFhirReader(reader);
            return new ResourceReader(jsonReader).Deserialize();
        }

        public Resource.ResourceMetaComponent ParseMetaFromXml(string xml)
        {
            throw Error.NotImplemented("Parsing <meta> is not yet implemented");

            //return ParseFromXml<TagList>(xml);
        }

        public Resource.ResourceMetaComponent ParseMetaFromJson(string json)
        {
            throw Error.NotImplemented("Parsing resourceType:meta is not yet implemented");
            //return ParseFromJson<TagList>(json);
        }

        //public Resource.ResourceMetaComponent ParseMeta(XmlReader reader)
        //{
        //    throw Error.NotImplemented("Parsing <meta> is not yet implemented");

        //    //return ParseFromXml<TagList>(xml);
        //}

        public Resource.ResourceMetaComponent ParseMeta(JsonReader reader)
        {
            throw Error.NotImplemented("Parsing <meta> is not yet implemented");

            //return ParseFromXml<TagList>(xml);
        }

        public Parameters ParseQueryFromUriParameters(string resource, IEnumerable<Tuple<String, String>> parameters)
        {
            return ParametersParser.Load(resource, parameters);
        }
    }
}
