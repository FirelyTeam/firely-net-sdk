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
    public class FhirRequest
    {
        public bool UseFormatParameter { get; set; }

        public FhirRequest(Uri location, string method = "GET",
             Action<FhirRequest, HttpWebRequest> beforeRequest = null, Action<FhirResponse,WebResponse> afterRequest = null)
        {
            if (location == null) throw Error.ArgumentNull("location");
            if (method == null) throw Error.ArgumentNull("method");
            if (!location.IsAbsoluteUri) throw Error.Argument("location", "Must be absolute uri");

            Location = location;
            Method = method;
            Timeout = 100*1000;       // Default timeout is 100 seconds
            
            _beforeRequest = beforeRequest;
            _afterRequest = afterRequest;
        }

        private Action<FhirRequest,HttpWebRequest> _beforeRequest;
        private Action<FhirResponse,WebResponse> _afterRequest;

        public string Method { get; private set; }
        public Uri Location { get; private set; }
        public int Timeout { get; set; }
        public byte[] Body { get; private set; }
        public string ContentType { get; private set; }
        public string CategoryHeader { get; private set; }
        public Uri ContentLocation { get; set; }
        public string ETag { get; set; }
        public string IfMatch { get; set; }

        public void SetBody(Resource data, ResourceFormat format)
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
                Body = format == ResourceFormat.Xml ?
                    FhirSerializer.SerializeToXmlBytes(data, summary: false) :
                    FhirSerializer.SerializeToJsonBytes(data, summary: false);

                ContentType = Hl7.Fhir.Rest.ContentType.BuildContentType(format, forBundle: false);
            }
        }

        public void SetMeta(Resource.ResourceMetaComponent data, ResourceFormat format)
        {
            if (data == null) throw Error.ArgumentNull("data");

            Body = format == ResourceFormat.Xml ?
                FhirSerializer.SerializeToXmlBytes(data, summary: false, root: "meta") :
                FhirSerializer.SerializeToJsonBytes(data, summary: false, root: "meta");

            ContentType = Hl7.Fhir.Rest.ContentType.BuildContentType(format, forBundle: false);
        }

    
        public string BodyAsString()
        {
            if (Body == null) return null;

            return (new StreamReader(new MemoryStream(Body), System.Text.Encoding.UTF8, true)).ReadToEnd();
        }

        public FhirResponse GetResponse(ResourceFormat? acceptFormat)
        {
			bool needsFormatParam = UseFormatParameter && acceptFormat.HasValue;

            var location = new RestUrl(Location);

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
                request.WriteBodyAsync(Body).Wait();
                request.ContentType = ContentType;
                if (ContentLocation != null)
                    request.Headers[HttpRequestHeader.ContentLocation] = ContentLocation.ToString();
            }

            FhirResponse fhirResponse = null;

#if !PORTABLE45
            request.Timeout = Timeout;
#endif

            if (_beforeRequest != null) 
                _beforeRequest(this,request);

            // Make sure the HttpResponse gets disposed!
            // using (HttpWebResponse webResponse = (HttpWebResponse)await request.GetResponseAsync(new TimeSpan(0, 0, 0, 0, Timeout)))
            using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponseNoEx())
            {
                try
                {
                    fhirResponse = FhirResponse.FromHttpWebResponse(webResponse).Result;
                    if (_afterRequest != null) _afterRequest(fhirResponse, webResponse);
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

        private HttpWebRequest createRequest(Uri location, string method)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(location);
            var agent = ".NET FhirClient for FHIR " + Model.ModelInfo.Version;
            request.Method = method;

			try
			{
#if PORTABLE45
				System.Reflection.PropertyInfo prop = request.GetType().GetRuntimeProperty("UserAgent");
#else
                System.Reflection.PropertyInfo prop = request.GetType().GetProperty("UserAgent");
#endif

				if (prop != null) prop.SetValue(request, agent, null);
			}
			catch (Exception)
			{
				// platform does not support UserAgent property...too bad
				try
				{
					request.Headers[HttpRequestHeader.UserAgent] = agent;
				}
				catch (ArgumentException)
				{
				}
			}

            return request;
        }
    }
}
