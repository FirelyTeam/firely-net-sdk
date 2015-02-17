/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
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
    internal static class BundleEntryExtensions
    {
        public static HttpWebRequest ToHttpRequest(this Bundle.BundleEntryComponent entry, Prefer bodyPreference, ResourceFormat format, bool useFormatParameter)
        {
            System.Diagnostics.Debug.WriteLine("{0}: {1}", entry.Transaction.Method, entry.Transaction.Url);

            var interaction = entry.Transaction;
            
            if (entry.Resource != null && !(interaction.Method == Bundle.HTTPVerb.POST || interaction.Method == Bundle.HTTPVerb.POST))
                throw Error.InvalidOperation("Cannot have a body on an Http " + interaction.Method.ToString());

            var location = new RestUrl(interaction.Url);

            if (useFormatParameter)
                location.AddParam(HttpUtil.RESTPARAM_FORMAT, Hl7.Fhir.Rest.ContentType.BuildFormatParam(format));

            var request = (HttpWebRequest)HttpWebRequest.Create(location.Uri);
            request.Method = interaction.Method.ToString();
            setAgent(request, ".NET FhirClient for FHIR " + Model.ModelInfo.Version);

            if (!useFormatParameter)
                request.Accept = Hl7.Fhir.Rest.ContentType.BuildContentType(format, forBundle: false);

            if (interaction.IfMatch != null) request.Headers["If-Match"] = interaction.IfMatch;
            if (interaction.IfNoneMatch != null) request.Headers["If-None-Match"] = interaction.IfNoneMatch;
            if (interaction.IfModifiedSince != null) request.IfModifiedSince = interaction.IfModifiedSince.Value.UtcDateTime;
            if (interaction.IfNoneExist != null) request.Headers["If-None-Exist"] = interaction.IfNoneExist;

            if (interaction.Method == Bundle.HTTPVerb.POST || interaction.Method == Bundle.HTTPVerb.PUT)
            {
                if (bodyPreference == Prefer.ReturnMinimal)
                    request.Headers["Prefer"] = bodyPreference == Prefer.ReturnMinimal ? "return=minimal" : "return=representation";
            }

            if (entry.Resource != null) setBodyAndContentType(request, entry.Resource, format);

            return request;
        }


        /// <summary>
        /// Flag to control the setting of the User Agent string (different platforms have different abilities)
        /// </summary>
        public static bool SetUserAgentUsingReflection = true;
        public static bool SetUserAgentUsingDirectHeaderManipulation = true;

        private static void setAgent(HttpWebRequest request, string agent)
        {
            bool userAgentSet = false;
            if (SetUserAgentUsingReflection)
            {
                try
                {
#if PORTABLE45
					System.Reflection.PropertyInfo prop = request.GetType().GetRuntimeProperty("UserAgent");
#else
                    System.Reflection.PropertyInfo prop = request.GetType().GetProperty("UserAgent");
#endif

                    if (prop != null)
                        prop.SetValue(request, agent, null);
                    userAgentSet = true;
                }
                catch (Exception)
                {
                    // This approach doesn't work on this platform, so don't try it again.
                    SetUserAgentUsingReflection = false;
                }
            }
            if (!userAgentSet && SetUserAgentUsingDirectHeaderManipulation)
            {
                // platform does not support UserAgent property...too bad
                try
                {
                    request.Headers[HttpRequestHeader.UserAgent] = agent;
                }
                catch (ArgumentException)
                {
                    SetUserAgentUsingDirectHeaderManipulation = false;
                }
            }
        }


        private static void setBodyAndContentType(HttpWebRequest request, Resource data, ResourceFormat format)
        {
            if (data == null) throw Error.ArgumentNull("data");

            if (data is Binary)
            {
                var bin = (Binary)data;
                request.WriteBody(bin.Content);
                request.ContentType = bin.ContentType;
            }
            else
            {
                var body = format == ResourceFormat.Xml ?
                    FhirSerializer.SerializeToXmlBytes(data, summary: false) :
                    FhirSerializer.SerializeToJsonBytes(data, summary: false);

                request.WriteBody(body);
                request.ContentType = Hl7.Fhir.Rest.ContentType.BuildContentType(format, forBundle: false);
            }
        }

        public static Bundle.BundleEntryComponent ToBundleEntry(this HttpWebResponse response)
        {
            var result = new Bundle.BundleEntryComponent();

            result.TransactionResponse = new Bundle.BundleEntryTransactionResponseComponent();
            result.TransactionResponse.Status = response.StatusCode.ToString();

            var contentType = getContentType(response);
            var charEncoding = getCharacterEncoding(response);

            result.TransactionResponse.Location = response.Headers[HttpUtil.LOCATION] ?? response.Headers[HttpUtil.CONTENTLOCATION];

#if PORTABLE45
                    result.TransactionResponse.LastModified = response.Headers[HttpUtil.LASTMODIFIED];
#else
                    result.TransactionResponse.LastModified = response.LastModified;
#endif
            result.TransactionResponse.Etag = getETag(response);
            
            var body = readBody(response);

            if (body != null)
            {
                OperationOutcome outcome = null;

                try
                {
                    outcome = response.BodyAsResource<OperationOutcome>();
                }
                catch
                {
                    // failed, so the body does not contain an OperationOutcome.
                    // Put the raw body as a message in the OperationOutcome as a fallback scenario
                    var body = response.BodyAsString();
                    if (!String.IsNullOrEmpty(body))
                        outcome = OperationOutcome.ForMessage(body);
                }

            }

            //                if (!String.IsNullOrEmpty(location))
            //{
            //    ResourceIdentity reqId = new ResourceIdentity(location);

            //    if (resource.Id == null) resource.Id = reqId.Id;
            //    if (resource.VersionId == null && reqId.HasVersion) resource.VersionId = reqId.VersionId;
            //    resource.ResourceBase = reqId.BaseUri;
            //}



        }


        private static string getETag(HttpWebResponse response)
        {
            var result = response.Headers[HttpUtil.ETAG];

            if(result != null)
            {
                if(result.StartsWith(@"W\")) result = result.Substring(2);
                result = result.Trim('\"');
            }

            return result;
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

        private static Encoding getCharacterEncoding(HttpWebResponse response)
        {
            Encoding result = null;

            if (!String.IsNullOrEmpty(response.ContentType))
            {
#if PORTABLE45
				var charset = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(response.ContentType).CharSet;
#else
                var charset = new System.Net.Mime.ContentType(response.ContentType).CharSet;
#endif

                if (!String.IsNullOrEmpty(charset))
                    result = Encoding.GetEncoding(charset);
            }
            return result;
        }

        private static byte[] readBody(HttpWebResponse response)
        {
            if (response.ContentLength > 0)
            {
                long contentlength = response.ContentLength;
                return HttpUtil.ReadAllFromStream(response.GetResponseStream(), (int)contentlength);
            }
            else
                return null;
        }



        private static bool isBinaryResponse(HttpWebResponse response)
        {
            var responseUri = response.ResponseUri.OriginalString;
            return responseUri.EndsWith("/Binary") || responseUri.EndsWith("/Binary?") || responseUri.Contains("/Binary/");
        }


        public static string decode(byte[] body, Encoding enc)
        {
            if (body == null) return null;
            if (enc == null) enc = Encoding.UTF8;

            return (new StreamReader(new MemoryStream(body), enc, true)).ReadToEnd();
        }

        private static Binary makeBinary(byte[] data, string contentType)
        {
            var binary = new Binary();

            binary.Content = data;
            binary.ContentType = contentType;

            return binary;
        }
   
        //  private Base parseBody(string contentType,
        //Func<string, Base> xmlParser, Func<string, Base> jsonParser)
        //  {
        //      Base result = null;

        //      ResourceFormat format = Hl7.Fhir.Rest.ContentType.GetResourceFormatFromContentType(contentType);

        //      switch (format)
        //      {
        //          case ResourceFormat.Json:
        //              result = jsonParser(BodyAsString());
        //              break;
        //          case ResourceFormat.Xml:
        //              result = xmlParser(BodyAsString());
        //              break;
        //          default:
        //              throw Error.Format("Cannot decode body: unrecognized content type " + contentType, null);
        //      }

        //      return result;
        //  }




        //public T BodyAsResource<T>() where T : Resource
        //{
        //    var result = BodyAsResource();

        //    if (!(result is T))
        //    {
        //        throw new FhirOperationException(
        //            String.Format("Received a resource of type {0} (FHIR: {1}), expected a {2} resource",
        //                            result.GetType().Name, result.TypeName, typeof(T).Name));
        //    }

        //    return (T)result;
        //}


        private static Resource getBodyAsResource(HttpWebResponse response)
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

            if (resource.Meta == null) resource.Meta = new Meta();
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
