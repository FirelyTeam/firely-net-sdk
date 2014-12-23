/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Hl7.Fhir.Rest
{
    public class FhirResponse
    {
        public HttpStatusCode Result { get; set; }
        public string ContentType { get; set; }
        public Encoding CharacterEncoding { get; set; }

        public string ContentLocation { get; set; }
        public string Location { get; set; }
        public string LastModified { get; set; }
        public string ETag { get; set; }

        public Uri ResponseUri { get; set; }

        public byte[] Body { get; set; }

		// Can't hold onto this as it gets disposed pretty quick.
        //public HttpWebResponse Response { get; set; }


        public bool IsBinaryResponse
        {
            get { return new ResourceIdentity(ResponseUri).ResourceType == ModelInfo.GetResourceNameForType(typeof(Binary)); }
        }

        public static async Task<FhirResponse> FromHttpWebResponse(HttpWebResponse response)
        {
            
            return new FhirResponse
                {
                    ResponseUri = response.ResponseUri,
                    Result = response.StatusCode,
                    ContentType = getContentType(response),
                    CharacterEncoding = getContentEncoding(response),
                    ContentLocation = response.Headers[HttpUtil.CONTENTLOCATION],
                    Location = response.Headers[HttpUtil.LOCATION],                   
                    LastModified = response.Headers[HttpUtil.LASTMODIFIED],
                    ETag = response.Headers[HttpUtil.ETAG] != null ? response.Headers[HttpUtil.ETAG].Trim('\"') : null,
                    Body = await readBody(response),
                    // Response = response
                };
        }

        private static Task<byte[]> readBody(HttpWebResponse response)
        {
            long contentlength = response.ContentLength;
            return HttpUtil.ReadAllFromStream(response.GetResponseStream(), (int)contentlength);
        }

        private static string getContentType(HttpWebResponse response)
        {
            if (!String.IsNullOrEmpty(response.ContentType))
            {
#if PORTABLE45 && !NET45
				return System.Net.Http.Headers.MediaTypeHeaderValue.Parse(response.ContentType).MediaType;
#else
				return new System.Net.Mime.ContentType(response.ContentType).MediaType;
#endif
            }
            else
                return null;
        }

        private static Encoding getContentEncoding(HttpWebResponse response)
        {
            Encoding result = null;

            if (!String.IsNullOrEmpty(response.ContentType))
			{
#if PORTABLE45 && !NET45
				var charset = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(response.ContentType).CharSet;
#else
				var charset = new System.Net.Mime.ContentType(response.ContentType).CharSet;
#endif

                if(!String.IsNullOrEmpty(charset))
                    result = Encoding.GetEncoding(charset);
            }
            return result;
        }


        public string BodyAsString()
        {
            if (Body == null) return null;

            Encoding enc = CharacterEncoding;

            // If no encoding is specified, default to utf8
            if (enc == null) enc = Encoding.UTF8;

            return (new StreamReader(new MemoryStream(Body), enc, true)).ReadToEnd();
        }


		//public TagList BodyAsTagList()
		//{
		//	return parseBody(BodyAsString(), ContentType,
		//				(b) => FhirParser.ParseTagListFromXml(b),
		//				(b) => FhirParser.ParseTagListFromJson(b));
		//}

        public T BodyAsResource<T>() where T : Resource
        {
            var result = BodyAsResource();

            if (!(result is T))
            {
                throw new FhirOperationException(
                    String.Format("Received a resource of type {0} (FHIR: {1}), expected a {2} resource",
                                    result.GetType().Name, result.TypeName, typeof(T).Name));
            }

            return (T)result;
        }


        public Resource BodyAsResource()
        {
            Resource resource = null;

            if (IsBinaryResponse)
                resource = makeBinary(Body, ContentType);
            else
            {
                resource = parseBody<Resource>(BodyAsString(), ContentType,
                    b => FhirParser.ParseResourceFromXml(b),
                    b => FhirParser.ParseResourceFromJson(b));
            }

            if (resource.Meta == null) resource.Meta = new Resource.ResourceMetaComponent();

            var location = Location ?? ContentLocation ?? ResponseUri.OriginalString;

            if (!String.IsNullOrEmpty(location))
            {
                ResourceIdentity reqId = new ResourceIdentity(location);
                resource.Id = reqId.Id;
                resource.ResourceBase = reqId.BaseUri;
            }

            if (!String.IsNullOrEmpty(ETag))
            {
                resource.Meta.VersionId = ETag;
                var id = new ResourceIdentity(location);

                if (id.HasVersion &&  ETag != id.VersionId)
                    throw new ApplicationException(String.Format("Version IDs from the server don't match: [{0}], [{1}]", ETag, id.VersionId));
            }
            else
            {
                var id = new ResourceIdentity(location);
                if(id.HasVersion)
                {
                    Trace.WriteLine(String.Format("Result did not have an ETag on the HTTP Header, using the (Content)Location instead"));
                    resource.Meta.VersionId = id.VersionId;
                }
            }

            if (!String.IsNullOrEmpty(LastModified))
                resource.Meta.LastUpdated = DateTimeOffset.Parse(LastModified);

            if (resource is Bundle)
            {
                var bundle = (Bundle)resource;
                foreach (var entry in bundle.Entry.Where(e => e.Resource != null))
                {
                    entry.Resource.ResourceBase = bundle.ResourceBase;
                }

            }
            return resource;
        }

        private static Binary makeBinary(byte[] data, string contentType)
        {
            var binary = new Binary();

            binary.Content = data;
            binary.ContentType = contentType;

            return binary;
        }
    

        private static T parseBody<T>(string body, string contentType,
          Func<string, T> xmlParser, Func<string, T> jsonParser) where T : class
        {
            T result = null;

            ResourceFormat format = Hl7.Fhir.Rest.ContentType.GetResourceFormatFromContentType(contentType);

            switch (format)
            {
                case ResourceFormat.Json:
                    result = jsonParser(body);
                    break;
                case ResourceFormat.Xml:
                    result = xmlParser(body);
                    break;
                default:
                    throw Error.Format("Cannot decode body: unrecognized content type " + contentType, null);
            }

            return result;
        }
    }
}
