#nullable enable

/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Hl7.Fhir.Utility;

public static class SerializationUtil
{
    private const string XML_XSD_RESOURCENAME = "xml.xsd";
    private const string XMLDSIGCORESCHEMA_XSD_RESOURCENAME = "xmldsig-core-schema.xsd";
    private const string FHIRXHTML_XSD_RESOURCENAME = "fhir-xhtml.xsd";

    /// <summary>
    /// A set of Xsd schemas used by the full FHIR schemaset. These include xml.xsd, xmldsig-core-schema.xsd and fhir-xhtml.xsd.
    /// </summary>
    public static readonly IncludedXsdSchemaSet BASEFHIRSCHEMAS =
        new(typeof(SerializationUtil).Assembly, XML_XSD_RESOURCENAME, XMLDSIGCORESCHEMA_XSD_RESOURCENAME, FHIRXHTML_XSD_RESOURCENAME);

    private readonly static Regex XML = new("""^\s*<[^>]+>""", RegexOptions.Compiled);
    private readonly static Regex JSON = new("""^\s*(\{\s*("[^"]+"\s*:.*)?\})\s*$""", RegexOptions.Compiled | RegexOptions.Singleline);

    public static bool ProbeIsXml(string data) => XML.IsMatch(data);

    public static bool ProbeIsFhirXml(string data) =>
        ProbeIsXml(data) && (data.Contains($"'{XmlNs.FHIR}'") || data.Contains($"\"{XmlNs.FHIR}\""));

    public static bool ProbeIsJson(string data) => JSON.IsMatch(data);

    public static bool ProbeIsFhirJson(string data) =>
        ProbeIsJson(data) && data.Contains($"\"resourceType\"");


    private static XDocument xDocumentFromReaderInternal(XmlReader reader)
    {
        try
        {
            return XDocument.Load(WrapXmlReader(reader, ignoreComments: false),
                LoadOptions.SetLineInfo);
        }
        catch (XmlException xec)
        {
            throw Error.Format("Invalid Xml encountered. Details: " + xec.Message, xec);
        }
    }

    public static XDocument XDocumentFromReader(XmlReader reader, bool ignoreComments = true)
        => xDocumentFromReaderInternal(WrapXmlReader(reader, ignoreComments));

    public static Task<XDocument> XDocumentFromReaderAsync(XmlReader reader, bool ignoreComments = true)
        => Task.FromResult(xDocumentFromReaderInternal(WrapXmlReader(reader, ignoreComments, async: true)));

    /// <inheritdoc cref="JObjectFromReaderAsync(JsonReader)" />
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

    public static async Task<JObject> JObjectFromReaderAsync(JsonReader reader)
    {
        // Override settings, sorry but this is vital
        reader.DateParseHandling = DateParseHandling.None;
        reader.FloatParseHandling = FloatParseHandling.Decimal;

        try
        {
            return await JObject.LoadAsync(reader,
                new JsonLoadSettings
                {
                    CommentHandling = CommentHandling.Ignore,
                    LineInfoHandling = LineInfoHandling.Load
                }).ConfigureAwait(false);
        }
        catch (JsonReaderException jre)
        {
            throw new FormatException($"Invalid Json encountered. Details: " + jre.Message, jre);
        }
    }

    /// <summary>
    /// Reads a string of Xml and returns a XDocument.
    /// </summary>
    /// <exception cref="FormatException">Thrown when the Xml parser detects an error.</exception>
    /// <remarks>Microsoft's Xml parsing infrastructure normally throws XmlExceptions when it encounters invalid Xml,
    /// but in this method specifically, we catch those exceptions and rethrow them as FormatExceptions. In retrospect,
    /// this is a potential source of confusion, but would be a breaking change to fix.</remarks>
    public static XDocument XDocumentFromXmlText(string xml, bool ignoreComments = true)
    {
        using var reader = XmlReaderFromXmlText(xml, ignoreComments);
        return xDocumentFromReaderInternal(reader);
    }

    public static XmlReader XmlReaderFromXmlText(string xml, bool ignoreComments = true)
        => WrapXmlReader(XmlReader.Create(new StringReader(SanitizeXml(xml))), ignoreComments);

    public static Task<XmlReader> XmlReaderFromXmlTextAsync(string xml, bool ignoreComments = true)
        => Task.FromResult(WrapXmlReader(XmlReader.Create(new StringReader(SanitizeXml(xml))), ignoreComments, async: true));

    public static JsonReader JsonReaderFromJsonText(string json)
        => jsonReaderFromTextReader(new StringReader(json));

    public static Utf8JsonReader Utf8JsonReaderFromJsonText(string json) =>
        new(Encoding.UTF8.GetBytes(json),
            new JsonReaderOptions { CommentHandling = JsonCommentHandling.Skip, AllowTrailingCommas = true });

    public static XmlReader XmlReaderFromStream(Stream input, bool ignoreComments = true)
        => WrapXmlReader(XmlReader.Create(input), ignoreComments);

    public static JsonReader JsonReaderFromStream(Stream input)
        => jsonReaderFromTextReader(new StreamReader(input));

    private static JsonReader jsonReaderFromTextReader(TextReader input)
    {
        return new JsonTextReader(input)
        {
            DateParseHandling = DateParseHandling.None,
            FloatParseHandling = FloatParseHandling.Decimal,
            CloseInput = false      // Unbelievable, the default is 'true' (and is false for the XmlReaderSettings :-()
        };
    }

    public static XmlReader WrapXmlReader(XmlReader xmlReader, bool ignoreComments = true, bool async = false)
    {
        var settings = new XmlReaderSettings
        {
            IgnoreComments = ignoreComments,
            IgnoreProcessingInstructions = true,
            IgnoreWhitespace = true,
            DtdProcessing = DtdProcessing.Prohibit,
            Async = async,
        };

        return XmlReader.Create(xmlReader, settings);
    }

    public static byte[] WriteXmlToBytes(Action<XmlWriter> serializer, bool pretty = false)
    {
        // [WMR 20160421] Explicit disposal
        using var stream = new MemoryStream();
        var settings = new XmlWriterSettings
        {
            Encoding = new UTF8Encoding(false),
            OmitXmlDeclaration = true,
            NewLineHandling = NewLineHandling.Entitize,
            Indent = pretty
        };

        using XmlWriter xw = XmlWriter.Create(stream, settings);
        serializer(xw);
        xw.Flush();
        return stream.ToArray();
    }

    public static async Task<byte[]> WriteXmlToBytesAsync(Func<XmlWriter, Task> serializer, bool pretty = false)
    {
        // [WMR 20160421] Explicit disposal
        using var stream = new MemoryStream();
        var settings = new XmlWriterSettings
        {
            Encoding = new UTF8Encoding(false),
            OmitXmlDeclaration = true,
            NewLineHandling = NewLineHandling.Entitize,
            Async = true,
            Indent = pretty
        };

        using var xw = XmlWriter.Create(stream, settings);
        await serializer(xw).ConfigureAwait(false);
        await xw.FlushAsync().ConfigureAwait(false);
        return stream.ToArray();
    }

    public static string WriteXmlToString(Action<XmlWriter> serializer, bool pretty = false)
    {
        StringBuilder sb = new();

        XmlWriterSettings settings = new()
        {
            OmitXmlDeclaration = true,
            NewLineHandling = NewLineHandling.Entitize,
            Indent = pretty
        };

        // [WMR 20160421] Explicit disposal
        using XmlWriter xw = XmlWriter.Create(sb, settings);
        serializer(xw);
        xw.Flush();
        return sb.ToString();
    }

    public static async Task<string> WriteXmlToStringAsync(Func<XmlWriter, Task> serializer, bool pretty = false)
    {
        var sb = new StringBuilder();

        var settings = new XmlWriterSettings
        {
            OmitXmlDeclaration = true,
            NewLineHandling = NewLineHandling.Entitize,
            Indent = pretty,
            Async = true,
        };

        // [WMR 20160421] Explicit disposal
        using XmlWriter xw = XmlWriter.Create(sb, settings);
        await serializer(xw).ConfigureAwait(false);
        await xw.FlushAsync().ConfigureAwait(false);
        return sb.ToString();
    }

    /// <inheritdoc cref="WriteXmlToDocumentAsync(Func{XmlWriter, Task})" />
    public static XDocument WriteXmlToDocument(Action<XmlWriter> serializer)
    {
        var doc = new XDocument();

        using XmlWriter xw = doc.CreateWriter();
        serializer(xw);
        xw.Flush();

        return doc;
    }

    public static async Task<XDocument> WriteXmlToDocumentAsync(Func<XmlWriter, Task> serializer)
    {
        var doc = new XDocument();

        using XmlWriter xw = doc.CreateWriter();
        await serializer(xw).ConfigureAwait(false);
        await xw.FlushAsync().ConfigureAwait(false);

        return doc;
    }

    public static string WriteJsonToString(Action<Utf8JsonWriter> serializer, bool pretty = false)
    {
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = pretty});
        serializer(writer);
        writer.Flush();
        return Encoding.UTF8.GetString(stream.ToArray());
    }

    public static string WriteJsonToString(Action<JsonWriter> serializer, bool pretty = false)
    {
        var resultBuilder = new StringBuilder();

        using var sw = new StringWriter(resultBuilder);
        using JsonWriter jw = CreateJsonTextWriter(sw);

        jw.Formatting = pretty ? Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None;
        serializer(jw);
        jw.Flush();
        return resultBuilder.ToString();
    }

    public static async Task<string> WriteJsonToStringAsync(Func<JsonWriter, Task> serializer, bool pretty = false)
    {
        var resultBuilder = new StringBuilder();

        await using var sw = new StringWriter(resultBuilder);
        using var jw = CreateJsonTextWriter(sw);

        jw.Formatting = pretty ? Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None;
        await serializer(jw).ConfigureAwait(false);
        await jw.FlushAsync().ConfigureAwait(false);
        return resultBuilder.ToString();
    }

    public static byte[] WriteJsonToBytes(Action<Utf8JsonWriter> serializer, bool pretty = false)
    {
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = pretty });
        serializer(writer);
        writer.Flush();
        return stream.ToArray();
    }

    public static byte[] WriteJsonToBytes(Action<JsonWriter> serializer, bool pretty = false)
    {
        using var stream = new MemoryStream();
        using var sw = new StreamWriter(stream, new UTF8Encoding(false));
        using var jw = CreateJsonTextWriter(sw);

        jw.Formatting = pretty ? Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None;
        serializer(jw);
        jw.Flush();
        sw.Flush();
        return stream.ToArray();
    }

    public static async Task<byte[]> WriteJsonToBytesAsync(Func<JsonWriter, Task> serializer, bool pretty = false)
    {
        using var stream = new MemoryStream();
        await using var sw = new StreamWriter(stream, new UTF8Encoding(false));
        using var jw = CreateJsonTextWriter(sw);

        jw.Formatting = pretty ? Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None;
        await serializer(jw).ConfigureAwait(false);
        await jw.FlushAsync().ConfigureAwait(false);
        await sw.FlushAsync().ConfigureAwait(false);
        return stream.ToArray();
    }

    /// <inheritdoc cref="WriteJsonToDocumentAsync(Func{JsonWriter, Task})" />
    public static JObject? WriteJsonToDocument(Action<JsonWriter> serializer)
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

        System.Diagnostics.Debug.Assert(doc.Count == 1);
        return doc.First as JObject;
    }

    public static async Task<JObject?> WriteJsonToDocumentAsync(Func<JsonWriter, Task> serializer)
    {
        // [WMR 20180409] Triggers runtime exception "Can not add Newtonsoft.Json.Linq.JObject to Newtonsoft.Json.Linq.JObject."
        // JsonDomFhirWriter.WriteEndProperty() => _root.WriteTo(jw) => jw.WriteStartObject() => exception...
        //var doc = new JObject();

        // JConstructor / JArray works, extract and return first child node
        var doc = new JArray();

        using (JsonWriter jw = doc.CreateWriter())
        {
            await serializer(jw).ConfigureAwait(false);
            await jw.FlushAsync().ConfigureAwait(false);
        }

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

        string resultRe;
        var matches = RE.Matches(xml);
        if (matches.Count > 0)
        {
            var sbRe = new StringBuilder();
            int currentPosition = 0;
            foreach (Match m in matches)
            {
                if (xr.TryGetValue(m.Value, out string? value))
                {
                    sbRe.Append(xml.Substring(currentPosition, m.Index - currentPosition));
                    sbRe.Append(value);
                    currentPosition = m.Index + m.Length;
                }
                // System.Diagnostics.Trace.WriteLine(String.Format("{0} - {1}: {2}", m.Index, m.Length, m.Value));
            }
            sbRe.Append(xml.Substring(currentPosition));
            resultRe = sbRe.ToString();
        }
        else
        {
            resultRe = xml;
        }

        return resultRe;
    }

    public static string[] RunFhirXhtmlSchemaValidation(XDocument doc)
    {
        if (!doc.Root!.AtXhtmlDiv())
            return [$"Root element of XHTML is not a <div> from the XHTML namespace ({XmlNs.XHTML})."];

        if (!hasContent(doc.Root!))
            return [$"The narrative SHALL have some non-whitespace content."];

        var result = new List<string>();
        doc.Validate(BASEFHIRSCHEMAS.CompiledSchemas, (_, a) => result.Add(a.Message));
        return result.ToArray();

        // content consist of xml elements with non-whitespace content (text or an image)
        static bool hasContent(XElement el)
            => el.DescendantsAndSelf().Any(e => !string.IsNullOrWhiteSpace(e.Value) || e.Name.LocalName == "img");
    }

    private static readonly Regex RE = new("(&[a-zA-Z0-9]+;)", RegexOptions.Compiled | RegexOptions.CultureInvariant);
    private static Dictionary<string, string>? _xmlReplacements;
    private static Dictionary<string, string> getXmlReplacements()
    {
        if (_xmlReplacements != null)
            return _xmlReplacements;

        Dictionary<string, string> xr = new()
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

    /// <summary>
    /// Add the SUBSETTED tag to the Meta of the resource and all its resource children. You need to use these
    /// tags when you are serializing a summary of a resource.
    /// </summary>
    /// <remarks>Will not add the tag to the root of a Bundle, since that is normally the container of
    /// the subsetted resources. If the resource already contained a SUBSETTED tag, they will not
    /// be reapplied.</remarks>
    public static void MarkSubsetted(Base instance)
    {
        addSubsetted(instance, true);
        return;

        static void addSubsetted(Base? instance, bool atRoot)
        {
            if (instance is null) return;
            var isBundleAtRoot = instance is Bundle && atRoot;

            if (instance is Resource resource && !isBundleAtRoot)
            {
                resource.Meta ??= new Meta();

                foreach (var item in SUBSETTED_TAGS)
                {
                    if (!resource.Meta.Tag.Any(t => t.System == item.System && t.Code == item.Code))
                    {
                        resource.Meta.Tag.Add((Coding)item.DeepCopy());
                    }
                }
            }

#pragma warning disable CS0618 // Type or member is obsolete
            foreach (var child in instance.Children())
#pragma warning restore CS0618 // Type or member is obsolete
                addSubsetted(child, atRoot: false);
        }
    }

    private static readonly Coding[] SUBSETTED_TAGS =
    [
        new("http://hl7.org/fhir/v3/ObservationValue", "SUBSETTED"), // STU3 Tag
        new("http://terminology.hl7.org/CodeSystem/v3-ObservationValue", "SUBSETTED") // Tag from R4 and higher
    ];

    /// <summary>
    /// Clones an instance, then marks it as subsetted.
    /// </summary>
    internal static Base MakeSubsettedClone(Base source)
    {
        var clone = source.DeepCopy();
        MarkSubsetted(clone);
        return clone;
    }
}