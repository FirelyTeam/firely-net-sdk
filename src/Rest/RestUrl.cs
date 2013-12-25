using Hl7.Fhir.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Rest
{

    //TODO: Support compartments

    internal class RestUrl
    {
        private Uri _endpoint;
        private UriBuilder _builder;
        private List<Tuple<string, string>> _parameters = new List<Tuple<string, string>>();

        internal RestUrl(Uri endpoint)
        {
            this._endpoint = endpoint;
            _builder = new UriBuilder(endpoint);
        }


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


        private static string delimit(string path)
        {
            return path.EndsWith(@"/") ? path : path + @"/";
        }
        private static string prefix(string path)
        {
            return path.StartsWith(@"/") ? path : @"/"+path;
        }
        internal RestUrl AddPath(params string[] components)
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

        /// <summary>
        /// Make a new ResourceLocation that represents a location after navigating to the specified path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <example>If the current path is "http://hl7.org/svc/patient", NavigatingTo("../observation") will 
        /// result in a ResourceLocation of "http://hl7.org/svc/observation"</example>
        public RestUrl NavigateTo(string path)
        {
            return NavigateTo(new Uri(path, UriKind.RelativeOrAbsolute));
        }

        public RestUrl NavigateTo(Uri path)
        {
            if (path.IsAbsoluteUri)
                throw new ArgumentException("Can only navigate to relative paths", "path");

            return new RestUrl(new Uri(this.Uri, path));
        }

        public override string ToString()
        {
            return AsString;
        }
    }

  

    
}
