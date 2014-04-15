using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Hl7.Fhir.Rest
{
    internal class FhirRequest
    {
        public bool UseFormatParameter { get; set; }

        public FhirRequest(Uri location, string method = "GET")
        {
            if (location == null) throw Error.ArgumentNull("location");
            if (method == null) throw Error.ArgumentNull("method");
            if (!location.IsAbsoluteUri) throw Error.Argument("location", "Must be absolute uri");

            _location = location;
            _method = method;
        }

        private Uri _location;
        private string _method = "GET";
        private byte[] _body = null;
        private string _contentType = null;
        private string _categoryHeader = null;
        private string _contentLocation = null;

        public void SetBody(Resource resource, ResourceFormat format)
        {
            if (resource == null) throw Error.ArgumentNull("resource");

            if (resource is Binary)
            {
                var bin = (Binary)resource;
                _body = bin.Content;
                _contentType = bin.ContentType;
            }
            else
            {
                _body = format == ResourceFormat.Xml ?
                    FhirSerializer.SerializeResourceToXmlBytes(resource, summary: false) :
                    FhirSerializer.SerializeResourceToJsonBytes(resource, summary: false);

                _contentType = ContentType.BuildContentType(format, forBundle: false);
            }
        }

        public void SetBody(Bundle bundle, ResourceFormat format)
        {
            if (bundle == null) throw Error.ArgumentNull("bundle");

            _body = format == ResourceFormat.Xml ?
                FhirSerializer.SerializeBundleToXmlBytes(bundle, summary: false) :
                FhirSerializer.SerializeBundleToJsonBytes(bundle, summary: false);

            _contentType = ContentType.BuildContentType(format, forBundle: true);
        }

        public void SetBody(TagList tagList, ResourceFormat format)
        {
            if (tagList == null) throw Error.ArgumentNull("tagList");

            _body = format == ResourceFormat.Xml ?
                FhirSerializer.SerializeTagListToXmlBytes(tagList) :
                FhirSerializer.SerializeTagListToJsonBytes(tagList);

            _contentType = ContentType.BuildContentType(format, forBundle: false);
        }


        public void SetTagsInHeader(IEnumerable<Tag> tags)
        {
            if (tags == null) throw Error.ArgumentNull("tags");

            _categoryHeader = HttpUtil.BuildCategoryHeader(tags);            
        }

        public void SetContentLocation(Uri location)
        {
            _contentLocation = location.ToString();
        }

        public FhirResponse GetResponse(ResourceFormat? acceptFormat)
        {
            bool needsFormatParam = UseFormatParameter && acceptFormat.HasValue;

            var location = new RestUrl(_location);

            if(needsFormatParam)
                location.AddParam(HttpUtil.RESTPARAM_FORMAT, ContentType.BuildFormatParam(acceptFormat.Value));

            var request = createRequest(location.ToString(), _method);

            if(acceptFormat != null && !UseFormatParameter)
                request.Accept = ContentType.BuildContentType(acceptFormat.Value, forBundle: false);

            if (_body != null)
            {
                request.WriteBody(_body);
                request.ContentType = _contentType;
                if(_contentLocation != null) request.Headers[HttpRequestHeader.ContentLocation] = _contentLocation;
            }

            if(_categoryHeader != null) request.Headers[HttpUtil.CATEGORY] = _categoryHeader;

            FhirResponse result = null;

            // Make sure the HttpResponse gets disposed!
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponseNoEx())
            {
                result = FhirResponse.FromHttpWebResponse(response);
            }

            return result;
        }


        private HttpWebRequest createRequest(string location, string method)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(location);
            var agent = ".NET FhirClient for FHIR " + Model.ModelInfo.Version;
            request.Method = method;

            try
            {
                System.Reflection.PropertyInfo prop = request.GetType().GetProperty("UserAgent");

                if (prop != null) prop.SetValue(request, agent, null);
            }
            catch (Exception)
            {
                // platform does not support UserAgent property...too bad
            }

            return request;
        }


    }
}
