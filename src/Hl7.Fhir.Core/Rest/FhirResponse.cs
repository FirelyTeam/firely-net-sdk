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
    internal class FhirResponse
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
            get
            {
                return ResponseUri.OriginalString.EndsWith("/Binary") || ResponseUri.OriginalString.EndsWith("/Binary?") ||
                  ResponseUri.OriginalString.Contains("/Binary/");
            }
        }

        public static FhirResponse FromHttpWebResponse(HttpWebResponse response)
        {
            return new FhirResponse
                {
                    ResponseUri = response.ResponseUri,
                    Result = response.StatusCode,
                    ContentType = getContentType(response),
                    CharacterEncoding = getContentEncoding(response),
                    ContentLocation = response.Headers[HttpUtil.CONTENTLOCATION],
                    Location = response.Headers[HttpUtil.LOCATION],                   
#if PORTABLE45
                    LastModified = response.Headers[HttpUtil.LASTMODIFIED],
#else
                    LastModified = response.LastModified.ToString(),
#endif
                    ETag = response.Headers[HttpUtil.ETAG] != null ? response.Headers[HttpUtil.ETAG].Trim('\"') : null,
                    Body = readBody(response)
                };
        }

        private static byte[] readBody(HttpWebResponse response)
        {
            long contentlength = response.ContentLength;
            return HttpUtil.ReadAllFromStream(response.GetResponseStream(), (int)contentlength);
        }

        private static string getContentType(HttpWebResponse response)
        {
            if (!String.IsNullOrEmpty(response.ContentType))
            {
#if PORTABLE45
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
#if PORTABLE45
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


        public Resource.ResourceMetaComponent BodyAsMeta()
        {
            return (Resource.ResourceMetaComponent)parseBody(ContentType,
                        b => (Resource.ResourceMetaComponent)FhirParser.ParseFromXml(b, typeof(Resource.ResourceMetaComponent)),
                        b => (Resource.ResourceMetaComponent)FhirParser.ParseFromJson(b, typeof(Resource.ResourceMetaComponent)));
        }



        public Resource BodyAsResource()
        {
            Resource resource = null;

            if (IsBinaryResponse)
                resource = makeBinary(Body, ContentType);
            else
            {
                resource = (Resource)parseBody(ContentType,
                    b => FhirParser.ParseResourceFromXml(b),
                    b => FhirParser.ParseResourceFromJson(b));
            }

            if (resource.Meta == null) resource.Meta = new Resource.ResourceMetaComponent();

            var location = Location ?? ContentLocation;

            if (!String.IsNullOrEmpty(location))
            {
                ResourceIdentity reqId = new ResourceIdentity(location);

                if(resource.Id == null) resource.Id = reqId.Id;
                if (resource.VersionId == null && reqId.HasVersion) resource.VersionId = reqId.VersionId;
                resource.ResourceBase = reqId.BaseUri;
            }

            if (!String.IsNullOrEmpty(ETag) && !resource.HasVersionId)
                resource.VersionId = ETag;

            if (!String.IsNullOrEmpty(LastModified) && (resource.Meta != null && resource.Meta.LastUpdated == null))
            {
                DateTimeOffset result;
                DateTime dtResult;
                if (DateTimeOffset.TryParse(LastModified, out result))
                {
                    resource.Meta.LastUpdated = result;
                }
                else if (DateTime.TryParse(LastModified, out dtResult))
                {
                    resource.Meta.LastUpdated = dtResult;
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
    

        private Base parseBody(string contentType,
          Func<string, Base> xmlParser, Func<string, Base> jsonParser)
        {
            Base result = null;

            ResourceFormat format = Hl7.Fhir.Rest.ContentType.GetResourceFormatFromContentType(contentType);

            switch (format)
            {
                case ResourceFormat.Json:
                    result = jsonParser(BodyAsString());
                    break;
                case ResourceFormat.Xml:
                    result = xmlParser(BodyAsString());
                    break;
                default:
                    throw Error.Format("Cannot decode body: unrecognized content type " + contentType, null);
            }

            return result;
        }

        public ResourceIdentity GetIdentityFromHeaders()
        {
            var location = Location ?? ContentLocation;

            if (!String.IsNullOrEmpty(location))
            {
                ResourceIdentity reqId = new ResourceIdentity(location);

                if (reqId.VersionId == null && !String.IsNullOrEmpty(ETag))
                {
                    Debug.WriteLine("Result did not have version nor location, using ETag instead");
                    reqId = reqId.WithVersion(ETag);
                }

                return reqId;
            }

            return null;
        }
    }
}
