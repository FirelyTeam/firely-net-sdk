using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Hl7.Fhir.Rest
{
    public class ResponseDetails
    {
        public HttpStatusCode Result { get; set; }
        public string ContentType { get; set; }
        public Encoding CharacterEncoding { get; set; }

        public string ContentLocation { get; set; }
        public string Location { get; set; }
        public string LastModified { get; set; }
        public string Category { get; set; }

        public Uri ResponseUri { get; set; }

        public byte[] Body { get; set; }

        public HttpWebResponse Reponse { get; set; }

        public string BodyAsString()
        {
            if (Body == null) return null;

            Encoding enc = CharacterEncoding;

            // If no encoding is specified, default to utf8
            if (enc == null) enc = Encoding.UTF8;

            return (new StreamReader(new MemoryStream(Body), enc, true)).ReadToEnd();
        }



        public static ResponseDetails FromHttpWebResponse(HttpWebResponse response)
        {
            
            return new ResponseDetails
                {
                    ResponseUri = response.ResponseUri,
                    Result = response.StatusCode,
                    ContentType = getContentType(response),
                    CharacterEncoding = getContentEncoding(response),
                    ContentLocation = response.Headers[HttpUtil.CONTENTLOCATION],
                    Location = response.Headers[HttpUtil.LOCATION],                   
                  //  LastModified = response.LastModified,
                    LastModified = response.Headers[HttpUtil.LASTMODIFIED],
                    Category = response.Headers[HttpUtil.CATEGORY],
                    Body = readBody(response),
                    Reponse = response
                };
        }

        private static byte[] readBody(HttpWebResponse response)
        {
            return HttpUtil.ReadAllFromStream(response.GetResponseStream(),
                (int)response.ContentLength);
        }

        private static string getContentType(HttpWebResponse response)
        {
            if (!String.IsNullOrEmpty(response.ContentType))
            {
                return new System.Net.Mime.ContentType(response.ContentType).MediaType;
            }
            else
                return null;
        }

        private static Encoding getContentEncoding(HttpWebResponse response)
        {
            Encoding result = null;

            if (!String.IsNullOrEmpty(response.ContentType))
            {
                var charset = new System.Net.Mime.ContentType(response.ContentType).CharSet;
          
                if(!String.IsNullOrEmpty(charset))
                    result = Encoding.GetEncoding(charset);
            }
            return result;
        }
    }
}
