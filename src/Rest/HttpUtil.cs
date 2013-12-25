/*
  Copyright (c) 2011-2013, HL7, Inc.
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


using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Rest
{
    public static class HttpUtil
    {
        public const string CONTENTLOCATION = "Content-Location";
        public const string LOCATION = "Location";
        public const string LASTMODIFIED = "Last-Modified";
        public const string CATEGORY = "Category";

        public const string RESTPARAM_FORMAT = "_format";

        public const string SEARCH_PARAM_ID = "_id";
        public const string SEARCH_PARAM_COUNT = "_count";
        public const string SEARCH_PARAM_INCLUDE = "_include";
        public const string HISTORY_PARAM_SINCE = "_since";
        public const string SEARCH_PARAM_SORT = "_sort";
        public const string HISTORY_PARAM_COUNT = SEARCH_PARAM_COUNT;

        public static byte[] ReadAllFromStream(Stream s, int contentLength)
        {
            if (contentLength == 0) return null;

            //int bufferSize = contentLength < 4096 ? contentLength : 4096;
            int bufferSize = 4096;

            byte[] byteBuffer = new byte[bufferSize];
            MemoryStream buffer = new MemoryStream();

            int readLen = s.Read(byteBuffer, 0, byteBuffer.Length);

            while (readLen > 0)
            {
                buffer.Write(byteBuffer, 0, readLen);
                readLen = s.Read(byteBuffer, 0, byteBuffer.Length);
            }

            //do
            //{
            //    readLen = s.Read(byteBuffer, 0, byteBuffer.Length);
            //    if (readLen > 0) buffer.Write(byteBuffer, 0, readLen);
            //} while (buffer.Length < contentLength);

            return buffer.ToArray();
        }

        public static IEnumerable<string> SplitNotInQuotes(char c, string value)
        {
            var categories = Regex.Split(value, c + "(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)")
                                .Select(s => s.Trim())
                                .Where(s => !String.IsNullOrEmpty(s));
            return categories;
        }

        public static ICollection<Tag> ParseCategoryHeader(string value)
        {
            if (String.IsNullOrEmpty(value)) return new List<Tag>();

            var result = new List<Tag>();

            var categories = SplitNotInQuotes(',', value);

            foreach (var category in categories)
            {
                var values = SplitNotInQuotes(';', category);

                if (values.Count() >= 1)
                {
                    var term = values.First();

                    var pars = values.Skip(1).Select( v =>
                        { 
                            var vsplit = v.Split('=');
                            var item1 = vsplit[0].Trim();
                            var item2 = vsplit.Length > 1 ? vsplit[1].Trim() : null;
                            return new Tuple<string,string>(item1,item2);
                        });

                    var scheme = new Uri(pars.Where(t => t.Item1 == "scheme").Select(t => t.Item2.Trim('\"')).FirstOrDefault(), UriKind.RelativeOrAbsolute);
                    var label = pars.Where(t => t.Item1 == "label").Select(t => t.Item2.Trim('\"')).FirstOrDefault();
                       
                    result.Add(new Tag(term,scheme,label));
                }
            }

            return result;
        }

        
       
        public static string BuildCategoryHeader(IEnumerable<Tag> tags)
        {
            var result = new List<string>();
            foreach(var tag in tags)
            {                
                StringBuilder sb = new StringBuilder();

                if (!String.IsNullOrEmpty(tag.Term))
                {
                    if (tag.Term.Contains(",") || tag.Term.Contains(";"))
                        throw new ArgumentException("Found tag containing ',' or ';' - this will produce an inparsable Category header");
                    sb.Append(tag.Term);
                }

                if (!String.IsNullOrEmpty(tag.Label))
                    sb.AppendFormat("; label=\"{0}\"", tag.Label);

                sb.AppendFormat("; scheme=\"{0}\"", tag.Scheme.ToString());
                result.Add(sb.ToString());
            }

            return String.Join(", ", result);
        }

        public static Binary MakeBinary(byte[] data, string contentType)
        {
            var binary = new Binary();

            binary.Content = data;
            binary.ContentType = contentType;
            //Note: binaries don't have Text narrative
            //binary.Text = new Narrative()
            //{
            //    Status = Narrative.NarrativeStatus.Generated,
            //    Div = new XElement(XNamespace.Get(XHTMLNS) + "div",
            //                "Binary content of type " + contentType).ToString()
            //};

            return binary;
        }


        internal static ResourceEntry CreateResourceEntry(Resource resource,
            string requestUri = null, string location = null,
            string category = null, string lastModified = null)
        {
            ResourceEntry result = ResourceEntry.Create(resource);
            string versionIdInRequestUri = null;

            if (!String.IsNullOrEmpty(requestUri))
            {
                ResourceIdentity reqId = new ResourceIdentity(requestUri);
                versionIdInRequestUri = reqId.VersionId;
                result.Id = reqId.WithoutVersion(); 
            }

            if (!String.IsNullOrEmpty(location))
                result.SelfLink = new Uri(location, UriKind.Absolute);
            else
            {
                // Try to get the SelfLink from the requestUri (might contain specific version id)
                if (!String.IsNullOrEmpty(versionIdInRequestUri))
                {
                    result.SelfLink = new ResourceIdentity(result.Id).WithVersion(versionIdInRequestUri);
                }
            }

            if (!String.IsNullOrEmpty(lastModified))
                result.LastUpdated = DateTimeOffset.Parse(lastModified);

            if (!String.IsNullOrEmpty(category))
                result.Tags = ParseCategoryHeader(category);

            result.Title = "A " + resource.GetType().Name + " resource";

            return result;
        }

        internal static ResourceEntry CreateResourceEntry(object data, string contentType,
            string requestUri = null, string location = null,
            string category = null, string lastModified = null)
        {
            Resource resource = null;

            if(data is string)
            {
                resource = parseBody<Resource>((string)data, contentType,
                    b => FhirParser.ParseResourceFromXml(b),
                    b => FhirParser.ParseResourceFromJson(b));
            }
            else if(data is byte[])
                resource = MakeBinary((byte[])data, contentType);
            else
                throw Error.Argument("data", "Data may be a byte[] (Binary data) or string (resource body)");

            return CreateResourceEntry(resource, requestUri, location, category, lastModified);
        }



        public static Bundle BundleResponse(string body, string contentType)
        {
            return parseBody<Bundle>(body, contentType,
                (b) => FhirParser.ParseBundleFromXml(b),
                (b) => FhirParser.ParseBundleFromJson(b));
        }


        public static TagList TagListResponse(string body, string contentType)
        {
            return parseBody(body, contentType,
                (b) => FhirParser.ParseTagListFromXml(b),
                (b) => FhirParser.ParseTagListFromJson(b));
        }

        private static byte[] serializeBody<T>(T data, ResourceFormat format, Func<T, byte[]> xmlSerializer, Func<T, byte[]> jsonSerializer)
        {
            var isBundle = data is Bundle;

            if (format == ResourceFormat.Json)
                return jsonSerializer(data); // FhirSerializer.SerializeBundleToJsonBytes(bundle);
            else if (format == ResourceFormat.Xml)
                return xmlSerializer(data);   // FhirSerializer.SerializeBundleToXmlBytes(bundle);
            else
                throw new ArgumentException("Cannot encode a batch into format " + format.ToString());

        }

        private static T parseBody<T>(string body, string contentType, 
                    Func<string, T> xmlParser, Func<string, T> jsonParser) where T : class
        {
            T result = null;

            ResourceFormat format = ContentType.GetResourceFormatFromContentType(contentType);

            switch (format)
            {
                case ResourceFormat.Json:
                    result = jsonParser(body); 
                    break;
                case ResourceFormat.Xml:
                    result = xmlParser(body);
                    break;
                default:
                    throw Error.Format("Cannot decode body: unrecognized content type " + contentType);
            }

            return result;
        }

    }
}
