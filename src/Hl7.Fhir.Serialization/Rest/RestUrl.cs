using Hl7.Fhir.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Rest
{
    public class Endpoint
    {
        // A lot of extensions will be built upon this class.
        
        public Uri Uri { get; private set; }
        public Endpoint(Uri uri)
        {
            // todo: test if this uri is an Url.
            this.Uri = uri;
        }
        public Endpoint(string uri)
        {
            this.Uri = new Uri(uri);
        }
        public RestUrl NewRestUri()
        {
            return new RestUrl(this.Uri);
        }
        public override string ToString()
        {
            return Uri.ToString();
        }
    }

    public class RestUrl
    {
        private Uri endpoint;
        private UriBuilder builder;
        private RestUriParameters parameters = new RestUriParameters();
        public Uri Uri 
        { 
            get 
            {   builder.Query = parameters.AsString();
                return builder.Uri;
            } 
        }
        public string AsString
        {
            get
            {
                return Uri.ToString();
            }
        }
        internal RestUrl(Uri endpoint)
        {
            this.endpoint = endpoint;
            builder = new UriBuilder(endpoint);
        }
        private static string delimit(string path)
        {
            return path.EndsWith(@"/") ? path : path + @"/";
        }
        private static string prefix(string path)
        {
            return path.StartsWith(@"/") ? path : @"/"+path;
        }
        internal RestUrl Path(params string[] components)
        {
            string _components = string.Join("/", components).Trim('/');
            builder.Path = delimit(builder.Path)+ _components;
            return this;
        }
        public RestUrl Format(string contentType)
        {
            parameters.Add(HttpUtil.RESTPARAM_FORMAT, contentType);
            return this;
        }
        public RestUrl Param(string name, string value)
        {
            parameters.Add(name, value);
            return this;
        }
        public RestUrl Query(RestUriParameters query)
        {
            builder.Query = query.ToString(); 
            return this;
        }
        public RestUriParameters Parameters
        {
            get
            {
                return this.parameters;
            }
        }

        public override string ToString()
        {
            return AsString;
        }
    }

    public class RestUriParameters
    {
        // todo: Ewout had hier al een versie van onder ResourceLocation /mh

        private List<Tuple<string, string>> parameters = new List<Tuple<string, string>>();
        public void Add(string name, string value)
        {
            parameters.Add(new Tuple<string, string>(name, value));
        }
        public string AsString()
        {
            IEnumerable<string> items = parameters.Select(t => t.Item1 + "=" + t.Item2);
            return string.Join("&", items); 
            // todo: dit moet nog escaped worden. enzo.
        }
        public RestUriParameters Format(string contentType)
        {
            this.Add(HttpUtil.RESTPARAM_FORMAT, contentType);
            return this;
        }
    }

    
}
