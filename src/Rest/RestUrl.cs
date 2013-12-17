using Hl7.Fhir.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Rest
{
    public class RestUrl
    {
        private Uri _endpoint;
        private UriBuilder _builder;
        private List<Tuple<string, string>> _parameters = new List<Tuple<string, string>>();

        public Uri Uri 
        { 
            get
            {
                _builder.Query = QueryParam.Join(_parameters);
                return _builder.Uri;
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
            this._endpoint = endpoint;
            _builder = new UriBuilder(endpoint);
        }
        private static string delimit(string path)
        {
            return path.EndsWith(@"/") ? path : path + @"/";
        }
        private static string prefix(string path)
        {
            return path.StartsWith(@"/") ? path : @"/"+path;
        }
        internal RestUrl SetPath(params string[] components)
        {
            string _components = string.Join("/", components).Trim('/');
            _builder.Path = delimit(_builder.Path)+ _components;
            return this;
        }

        public RestUrl AddParam(string name, string value)
        {
            _parameters.Add(Tuple.Create(name, value));
            return this;
        }

        public override string ToString()
        {
            return AsString;
        }
    }

  

    
}
