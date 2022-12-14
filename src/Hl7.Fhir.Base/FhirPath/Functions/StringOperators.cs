/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Hl7.FhirPath.Functions
{
    internal static class StringOperators
    {
        public static string FpSubstring(this string me, long start, long? length)
        {
            var l = length ?? me.Length;

            if (start < 0 || start >= me.Length) return null;
            l = Math.Min(l, me.Length - start);

            return me.Substring((int)start, (int)l);
        }

        public static ITypedElement FpIndexOf(this string me, string fragment)
        {
            return ElementNode.ForPrimitive(me.IndexOf(fragment));
        }

        public static IEnumerable<ITypedElement> ToChars(this string me) =>
            me.ToCharArray().Select(c => ElementNode.ForPrimitive(c));

        public static string FpReplace(this string me, string find, string replace)
        {
            if (find == String.Empty)
            {
                // weird, but as specified:  "abc".replace("","x") = "xaxbxcx"
                return replace + String.Join(replace, me.ToCharArray()) + replace;
            }
            else
                return me.Replace(find, replace);
        }

        public static IEnumerable<ITypedElement> FpSplit(this string me, string seperator)
        {
            var results = me.Split(new[] { seperator }, StringSplitOptions.RemoveEmptyEntries);
            return results.Select(s => ElementNode.ForPrimitive(s));
        }

        public static string FpEncode(this string me, string encoding)
        {
            return encoding switch
            { 
                "base64" => EncodeBase64(me),
                "urlbase64" => EncodeUrlBase64(me),
                "hex" => EncodeHex(me),
                _ => throw new ArgumentException($"Unknown encoding '{encoding}'.", nameof(encoding))
            };
        }

        public static string FpDecode(this string me, string encoding)
        {
            return encoding switch
            {
                "base64" => DecodeBase64(me),
                "urlbase64" => DecodeUrlBase64(me),
                "hex" => DecodeHex(me),
                _ => throw new ArgumentException($"Unknown encoding '{encoding}'.", nameof(encoding))
            };
        }

        internal static string EncodeBase64(string data)
        {
            if (string.IsNullOrEmpty(data)) return data;

            byte[] toEncodeAsBytes = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(toEncodeAsBytes);
        }

        internal static string DecodeBase64(string data)
        {
            if (string.IsNullOrEmpty(data)) return data;

            byte[] decodedBase64 = Convert.FromBase64String(data);                
            return Encoding.UTF8.GetString(decodedBase64, 0, decodedBase64.Length);
        }

        internal static string EncodeUrlBase64(string data)
        {
            if (string.IsNullOrEmpty(data)) return data;

            // There's a function in System.Web (UrlTokenEncode), but don't need another dependency for this stuff.                
            var b64 = EncodeBase64(data);
            return b64
              //  .Trim('=')  // trim padding - in current draft this is not used
                .Replace('+', '-').Replace('/', '_'); // use alternative 62nd/63rd
        }

        internal static string DecodeUrlBase64(string data)
        {
            if (string.IsNullOrEmpty(data)) return data;

            // There's a function in System.Web (UrlTokenDecode), but don't need another dependency for this stuff.                
            var incoming = data.Replace('_', '/').Replace('-', '+');
           
            switch (incoming.Length % 4)
            {
                case 2: incoming += "=="; break;
                case 3: incoming += "="; break;
            }

            return DecodeBase64(incoming);            
        }

        internal static string EncodeHex(string data)
        {
            if (string.IsNullOrEmpty(data)) return data;

            return string.Concat(Encoding.UTF8.GetBytes(data)
                .Select(b => Convert.ToString(b, toBase: 16)));
        }

        internal static string DecodeHex(string data)
        {
            if (string.IsNullOrEmpty(data)) return data;
            if (data.Length % 2 != 0) throw new ArgumentException("Hex data should contain an even number of characters.");

            var bytes = hexStringToBytes(data).ToArray();
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);  // stupid netstd11

            static IEnumerable<byte> hexStringToBytes(string data)
            {
                for (int i = 0; i < data.Length; i += 2)
                {
                    var hexChar = data.Substring(i, 2);
                    yield return Convert.ToByte(hexChar, fromBase: 16);
                }
            }
        }

        public static string FpEscape(this string data, string encoding)
        {
            return encoding switch
            {
                "json" => EscapeJson(data),
                "html" => EscapeHtml(data),
                _ => throw new ArgumentException($"Unknown escaping method '{encoding}'.", nameof(encoding))
            };
        }

        public static string FpUnescape(this string data, string encoding)
        {
            return encoding switch
            {
                "json" => UnescapeJson(data),
                "html" => UnescapeHtml(data),
                _ => throw new ArgumentException($"Unknown escaping method '{encoding}'.", nameof(encoding))
            };
        }

        internal static string EscapeHtml(string data) => WebUtility.HtmlEncode(data);

        internal static string UnescapeHtml(string data) => WebUtility.HtmlDecode(data);

        internal static string EscapeJson(string data)
        {
            // Note: there are framework utility methods to do this,
            // but they all require expensive dependencies to be loaded.
            // This code is taken from Mono's implementation of one of these,
            // we should this to be ok.
            var sb = new StringBuilder();

            int start = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (needEscape(data, i))
                {
                    sb.Append(data, start, i - start);
                    switch (data[i])
                    {
                        case '\b': sb.Append("\\b"); break;
                        case '\f': sb.Append("\\f"); break;
                        case '\n': sb.Append("\\n"); break;
                        case '\r': sb.Append("\\r"); break;
                        case '\t': sb.Append("\\t"); break;
                        case '\"': sb.Append("\\\""); break;
                        case '\\': sb.Append("\\\\"); break;
                        case '/': sb.Append("\\/"); break;
                        default:
                            sb.Append("\\u");
                            sb.Append(((int)data[i]).ToString("x04"));
                            break;
                    }
                    start = i + 1;
                }
            }

            sb.Append(data, start, data.Length - start);
            return sb.ToString();

            static bool needEscape(string src, int i)
            {
                char c = src[i];
                return c < 32 || c == '"' || c == '\\'
                    // Broken lead surrogate
                    || (c >= '\uD800' && c <= '\uDBFF' &&
                        (i == src.Length - 1 || src[i + 1] < '\uDC00' || src[i + 1] > '\uDFFF'))
                    // Broken tail surrogate
                    || (c >= '\uDC00' && c <= '\uDFFF' &&
                        (i == 0 || src[i - 1] < '\uD800' || src[i - 1] > '\uDBFF'))
                    // To produce valid JavaScript
                    || c == '\u2028' || c == '\u2029';
            }
        }

        // This probably does a bit more than a JSON string will ever contain, but
        // it should also at least do what json requires. Good enough.
        internal static string UnescapeJson(string data) => Regex.Unescape(data);
    }
}
