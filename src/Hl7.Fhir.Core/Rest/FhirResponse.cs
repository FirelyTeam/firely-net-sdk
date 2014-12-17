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
                    ETag = response.Headers[HttpUtil.ETAG],
                    Body = await readBody(response),
                    // Response = response
                };
        }

        private static Task<byte[]> readBody(HttpWebResponse response)
        {
            long contentlength = response.ContentLength;
            return HttpUtil.ReadAllFromStream(response.GetResponseStream(),
                (int)contentlength);
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

        public T BodyAsEntry<T>() where T : Resource, new()
        {
            var result = BodyAsEntry(typeof(T).GetCollectionName());

            if (result is T)
            {
                T entry = (T)result;
                var location = Location ?? ContentLocation ?? ResponseUri.OriginalString;

                if (!String.IsNullOrEmpty(location))
                {
                    ResourceIdentity ri = new ResourceIdentity(location);
                    entry.ResourceBase = ri.Endpoint.OriginalString;
                }
                return entry;
            }
            else
            {
                throw new FhirOperationException(
                    String.Format("Received a resource of type {0} (FHIR: {1}), expected a {2} resource",
                                    result.GetType().Name, result.TypeName, typeof(T).Name));
            }
        }


        public Resource BodyAsEntry(string collection)
        {
            return createResourceEntry(collection);
        }

        public Bundle BodyAsBundle()
        {
            Bundle result = BodyAsEntry<Bundle>();
            // When we get a bundle we need to set the base location of each item
            foreach (var entry in result.Entry.Where(e => e.Resource != null))
            {
                entry.Resource.ResourceBase = result.ResourceBase;
            }
            return result;
        }


        private static Binary makeBinary(byte[] data, string contentType)
        {
            var binary = new Binary();

            binary.Content = data;
            binary.ContentType = contentType;

            return binary;
        }


        private Resource createResourceEntry(string resourceType)
        {
            Resource resource = null;

            if (resourceType == "Binary")
                resource = makeBinary(Body, ContentType);
            else
            {
                resource = parseBody<Resource>(BodyAsString(), ContentType,
                    b => FhirParser.ParseResourceFromXml(b),
                    b => FhirParser.ParseResourceFromJson(b));
            }

            if (resource.Meta != null)
                resource.Meta = new Resource.ResourceMetaComponent();
            //   ResourceEntry result = ResourceEntry.Create(resource);

            var location = Location ?? ContentLocation ?? ResponseUri.OriginalString;

            if (!String.IsNullOrEmpty(location))
            {
                ResourceIdentity reqId = new ResourceIdentity(location);

                // Set the id to the location, without the version specific part
                resource.Id = reqId.Id; // WithoutVersion();

                // If the content location has version information, set to SelfLink to it
                if (reqId.VersionId != null)
                {
                    resource.Meta.VersionId = reqId.VersionId;
                }
            }

            if (!String.IsNullOrEmpty(LastModified))
                resource.Meta.LastUpdated = DateTimeOffset.Parse(LastModified);

            //if (!String.IsNullOrEmpty(Category))
            //    result.Tags = HttpUtil.ParseCategoryHeader(Category);

            // result.Title = "A " + resource.GetType().Name + " resource";

            return resource;
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
