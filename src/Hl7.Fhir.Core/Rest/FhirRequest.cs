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
            Timeout = 100000;       // Default timeout is 100 seconds
            
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

        public void SetBody(Resource resource, ResourceFormat format)
        {
            if (resource == null) throw Error.ArgumentNull("resource");

            if (resource is Binary)
            {
                var bin = (Binary)resource;
                Body = bin.Content;
                ContentType = bin.ContentType;
            }
            else
            {
                Body = format == ResourceFormat.Xml ?
                    FhirSerializer.SerializeResourceToXmlBytes(resource, summary: false) :
                    FhirSerializer.SerializeResourceToJsonBytes(resource, summary: false);

                ContentType = Hl7.Fhir.Rest.ContentType.BuildContentType(format, forBundle: false);
            }
        }

        public void SetBody(Bundle bundle, ResourceFormat format)
        {
            if (bundle == null) throw Error.ArgumentNull("bundle");

            Body = format == ResourceFormat.Xml ?
                FhirSerializer.SerializeBundleToXmlBytes(bundle, summary: false) :
                FhirSerializer.SerializeBundleToJsonBytes(bundle, summary: false);

            ContentType = Hl7.Fhir.Rest.ContentType.BuildContentType(format, forBundle: true);
        }

        public void SetBody(TagList tagList, ResourceFormat format)
        {
            if (tagList == null) throw Error.ArgumentNull("tagList");

            Body = format == ResourceFormat.Xml ?
                FhirSerializer.SerializeTagListToXmlBytes(tagList) :
                FhirSerializer.SerializeTagListToJsonBytes(tagList);

            ContentType = Hl7.Fhir.Rest.ContentType.BuildContentType(format, forBundle: false);
        }


        public string BodyAsString()
        {
            if (Body == null) return null;

            return (new StreamReader(new MemoryStream(Body), System.Text.Encoding.UTF8, true)).ReadToEnd();
        }

        public void SetTagsInHeader(IEnumerable<Tag> tags)
        {
            if (tags == null) throw Error.ArgumentNull("tags");

            CategoryHeader = HttpUtil.BuildCategoryHeader(tags);            
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

            if (Body != null)
            {
                request.WriteBody(Body);
                request.ContentType = ContentType;
                if(ContentLocation != null) request.Headers[HttpRequestHeader.ContentLocation] = ContentLocation.ToString();
            }

            if(CategoryHeader != null) request.Headers[HttpUtil.CATEGORY] = CategoryHeader;

            FhirResponse fhirResponse = null;

#if !PORTABLE45
            request.Timeout = Timeout;
#endif

            if (_beforeRequest != null) _beforeRequest(this,request);

            // Make sure the HttpResponse gets disposed!
            using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponseNoEx())
            {
                fhirResponse = FhirResponse.FromHttpWebResponse(webResponse);
                if (_afterRequest != null) _afterRequest(fhirResponse,webResponse);
            }

            return fhirResponse;
        }

#if PORTABLE45 || NET45
		public async Task<WebResponse> GetResponseAsync(ResourceFormat? acceptFormat)
		{
			bool needsFormatParam = UseFormatParameter && acceptFormat.HasValue;

			var location = new RestUrl(Location);

			if (needsFormatParam)
				location.AddParam(HttpUtil.RESTPARAM_FORMAT, Hl7.Fhir.Rest.ContentType.BuildFormatParam(acceptFormat.Value));

			System.Diagnostics.Debug.WriteLine("(async) {0}: {1}", Method, location.ToString());

			HttpWebRequest request = createRequest(location.Uri, Method);

			if (acceptFormat != null && !UseFormatParameter)
				request.Accept = Hl7.Fhir.Rest.ContentType.BuildContentType(acceptFormat.Value, forBundle: false);

			if (CategoryHeader != null) 
				request.Headers[HttpUtil.CATEGORY] = CategoryHeader;

			if (Body != null)
			{
				request.ContentType = ContentType;
				if (ContentLocation != null) 
					request.Headers[HttpRequestHeader.ContentLocation] = ContentLocation.ToString();
				await request.WriteBodyAsync(Body);
			}

			// Make sure the caller disposes the HttpResponse gets disposed...
            if (_beforeRequest != null) _beforeRequest(null,request);
            var webResponse = await request.GetResponseAsync(TimeSpan.FromMilliseconds(Timeout));
            if (_afterRequest != null) _afterRequest(null,webResponse);

            return webResponse;
		}
#endif

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
