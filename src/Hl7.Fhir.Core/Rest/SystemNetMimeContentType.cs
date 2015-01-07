//
// System.Net.Mime.ContentType.cs
//
// Authors:
//        Tim Coleman (tim@timcoleman.com)
//        John Luke (john.luke@gmail.com)
//
// Copyright (C) Tim Coleman, 2004
// Copyright (C) John Luke, 2005
//

//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System.Collections;
using System.Text;
using System.Collections.Generic;

#if PORTABLE45

namespace System.Net.Mime
{
    internal class ContentType
    {
#region Fields
        static Encoding utf8unmarked;

        string mediaType;
        Dictionary<string,string> parameters = new Dictionary<string,string>();

        #endregion // Fields

#region Constructors

        public ContentType()
        {
            mediaType = "application/octet-stream";
        }

        public ContentType(string contentType)
        {
            if (contentType == null)
                throw new ArgumentNullException("contentType");
            if (contentType.Length == 0)
                throw new ArgumentException("contentType");

            string[] split = contentType.Split(';');
            this.MediaType = split[0].Trim();
            for (int i = 1; i < split.Length; i++)
                Parse(split[i].Trim());
        }

        // parse key=value pairs like:
        // "charset=us-ascii"
        static char[] eq = new char[] { '=' };
        void Parse(string pair)
        {
            if (String.IsNullOrEmpty(pair))
                return;

            string[] split = pair.Split(eq, StringSplitOptions.RemoveEmptyEntries);
            string key = split[0].Trim();
            string val = (split.Length > 1) ? split[1].Trim() : "";
            int l = val.Length;
            if (l >= 2 && val[0] == '"' && val[l - 1] == '"')
                val = val.Substring(1, l - 2);
            parameters[key] = val;
        }

        #endregion // Constructors

#region Properties

        static Encoding UTF8Unmarked
        {
            get
            {
                if (utf8unmarked == null)
                    utf8unmarked = new UTF8Encoding(false);
                return utf8unmarked;
            }
        }

        public string Boundary
        {
            get { return parameters.ContainsKey("boundary") ? parameters["boundary"] : null; }
            set { parameters["boundary"] = value; }
        }

        public string CharSet
        {
            get { return parameters.ContainsKey("charset") ? parameters["charset"] : null; }
            set { parameters["charset"] = value; }
        }

        public string MediaType
        {
            get { return mediaType; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                if (value.Length < 1)
                    throw new ArgumentException();
                if (value.IndexOf('/') < 1)
                    throw new FormatException();
                if (value.IndexOf(';') != -1)
                    throw new FormatException();
                mediaType = value;
            }
        }

        public string Name
        {
            get { return parameters.ContainsKey("name") ? parameters["name"] : null; }
            set { parameters["name"] = value; }
        }

        public Dictionary<string,string> Parameters
        {
            get { return parameters; }
        }

        #endregion // Properties

#region Methods

        public override bool Equals(object obj)
        {
            return Equals(obj as ContentType);
        }

        bool Equals(ContentType other)
        {
            return other != null && ToString() == other.ToString();
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Encoding enc = CharSet != null ? Encoding.GetEncoding(CharSet) : Encoding.UTF8;
            sb.Append(MediaType);
            if (Parameters != null && Parameters.Count > 0)
            {
                foreach (var key in parameters.Keys)
                {
                    var value = parameters[key];

                    if (!String.IsNullOrEmpty(value))
                    {
                        sb.Append("; ");
                        sb.Append(key);
                        sb.Append("=");
                        sb.Append(WrapIfEspecialsExist(EncodeSubjectRFC2047(value as string, enc)));
                    }
                }
            }
            return sb.ToString();
        }

        // see RFC 2047
        static readonly char[] especials = { '(', ')', '<', '>', '@', ',', ';', ':', '<', '>', '/', '[', ']', '?', '.', '=' };

        static string WrapIfEspecialsExist(string s)
        {
            s = s.Replace("\"", "\\\"");
            if (s.IndexOfAny(especials) >= 0)
                return '"' + s + '"';
            else
                return s;
        }

        internal static Encoding GuessEncoding(string s)
        {
            for (int i = 0; i < s.Length; i++)
                if (s[i] >= '\u0080')
                    return UTF8Unmarked;
            return null;
        }

      
        static char[] hex = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        internal static string To2047(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte i in bytes)
            {
                if (i < 0x21 || i > 0x7E || i == '?' || i == '=' || i == '_')
                {
                    sb.Append('=');
                    sb.Append(hex[(i >> 4) & 0x0f]);
                    sb.Append(hex[i & 0x0f]);
                }
                else
                    sb.Append((char)i);
            }
            return sb.ToString();
        }

        internal static string EncodeSubjectRFC2047(string s, Encoding enc)
        {
            if (s == null || Encoding.GetEncoding("ascii").Equals(enc))
                return s;
            for (int i = 0; i < s.Length; i++)
                if (s[i] >= '\u0080')
                {
                    string quoted = To2047(enc.GetBytes(s));
                    return String.Concat("=?", enc.WebName, "?Q?", quoted, "?=");
                }
            return s;
        }

        #endregion // Methods
    }
}
#endif