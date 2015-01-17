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
using System.Linq;
using System.Net;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.IO;

namespace Hl7.Fhir.Rest
{
    internal class FhirRequest
    {
        public FhirRequest()
        {
            Method = "GET";
            Format = ResourceFormat.Xml;
            Timeout = 100*1000;       // Default timeout is 100 seconds            
        }

        public Action<FhirRequest, HttpWebRequest> BeforeRequest { get; internal set; }
        public Action<FhirResponse, WebResponse> AfterRequest { get; internal set; }

        public bool UseFormatParameter { get; internal set; }
        public string Method { get; internal set; }
        public Uri BaseUrl { get; internal set; }
        public Uri Path { get; internal set; }
        public int Timeout { get; internal set; }
        public byte[] Body { get; private set; }
        public string ContentType { get; private set; }
        public string CategoryHeader { get; private set; }
        public Uri ContentLocation { get; internal set; }
        public string ETag { get; internal set; }
        public string IfMatch { get; internal set; }
        public ResourceFormat Format { get; internal set; }

        public void SetBody(Resource data)
        {
            if (data == null) throw Error.ArgumentNull("data");

            if (data is Binary)
            {
                var bin = (Binary)data;
                Body = bin.Content;
                ContentType = bin.ContentType;
            }
            else
            {
                Body = Format == ResourceFormat.Xml ?
                    FhirSerializer.SerializeToXmlBytes(data, summary: false) :
                    FhirSerializer.SerializeToJsonBytes(data, summary: false);

                ContentType = Hl7.Fhir.Rest.ContentType.BuildContentType(Format, forBundle: false);
            }
        }

        public void SetMeta(Resource.ResourceMetaComponent data)
        {
            if (data == null) throw Error.ArgumentNull("data");

            Body = Format == ResourceFormat.Xml ?
                FhirSerializer.SerializeToXmlBytes(data, summary: false, root: "meta") :
                FhirSerializer.SerializeToJsonBytes(data, summary: false, root: "meta");

            ContentType = Hl7.Fhir.Rest.ContentType.BuildContentType(Format, forBundle: false);
        }

    
        public string BodyAsString()
        {
            if (Body == null) return null;

            return (new StreamReader(new MemoryStream(Body), System.Text.Encoding.UTF8, true)).ReadToEnd();
        }

        public FhirResponse GetResponse(ResourceFormat? acceptFormat)
        {
			bool needsFormatParam = UseFormatParameter && acceptFormat.HasValue;

            var location = new RestUrl(BaseUrl);            
            if(Path != null) location.AddPath(Path.OriginalString);

            if(needsFormatParam)
                location.AddParam(HttpUtil.RESTPARAM_FORMAT, Hl7.Fhir.Rest.ContentType.BuildFormatParam(acceptFormat.Value));

			System.Diagnostics.Debug.WriteLine("{0}: {1}", Method, location.Uri.OriginalString);

            var request = createRequest(location.Uri, Method);

            if(acceptFormat != null && !UseFormatParameter)
                request.Accept = Hl7.Fhir.Rest.ContentType.BuildContentType(acceptFormat.Value, forBundle: false);

            if (ETag != null)
                request.Headers["ETag"] = "\"" + ETag + "\"";

            if (IfMatch != null)
                request.Headers["If-Match"] = "\"" + IfMatch + "\"";

            if (Body != null)
            {
                request.WriteBody(Body);
                request.ContentType = ContentType;
                if (ContentLocation != null)
                    request.Headers[HttpRequestHeader.ContentLocation] = ContentLocation.ToString();
            }

            FhirResponse fhirResponse = null;

#if !PORTABLE45
            request.Timeout = Timeout;
#endif

            if (BeforeRequest != null) BeforeRequest(this,request);

            // Make sure the HttpResponse gets disposed!
            // using (HttpWebResponse webResponse = (HttpWebResponse)await request.GetResponseAsync(new TimeSpan(0, 0, 0, 0, Timeout)))
            using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponseNoEx())
            {
                try
                {
                    fhirResponse = FhirResponse.FromHttpWebResponse(webResponse);
                    if (AfterRequest != null) AfterRequest(fhirResponse, webResponse);
                }
                catch (AggregateException ae)
                {
                    if (ae.GetBaseException() is WebException)
                    {
                    }
                    throw ae.GetBaseException();
                }
            }

            return fhirResponse;
        }

        /// <summary>
        /// Flag to control the setting of the User Agent string (different platforms have different abilities)
        /// </summary>
        public static bool SetUserAgentUsingReflection = true;
        public static bool SetUserAgentUsingDirectHeaderManipulation = true;

        private HttpWebRequest createRequest(Uri location, string method)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(location);
            var agent = ".NET FhirClient for FHIR " + Model.ModelInfo.Version;
            request.Method = method;

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

            return request;
        }
    }
}
