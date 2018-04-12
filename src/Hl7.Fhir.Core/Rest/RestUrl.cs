/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Rest
{
    public class RestUrl
    {
        private UriBuilder _builder;
        private UriParamList _parameters = new UriParamList();

        public RestUrl(RestUrl url) : this(url.Uri)
        {
        }

        public RestUrl(Uri url)
        {
            if (!url.IsAbsoluteUri) throw Error.Argument(nameof(url), "Must be an absolute url");

            if (url.Scheme != "http" && url.Scheme != "https")
                Error.Argument(nameof(url), "RestUrl must be a http(s) url");

            _builder = new UriBuilder(url);

            if (!String.IsNullOrEmpty(_builder.Query))
                _parameters = UriParamList.FromQueryString(_builder.Query); 
        }

        public RestUrl(string endpoint) : this(new Uri(endpoint,UriKind.Absolute))
        {
        }

        public Uri Uri 
        { 
            get
            {
                _builder.Query = _parameters.ToQueryString();
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
              

        /// <summary>
        /// Add additional components to the end of the RestUrl
        /// </summary>
        /// <param name="components">one or more path components to add</param>
        /// <returns>The current RestUrl, so multiple AddPath statements can be combined in a fluent way.</returns>
        /// <example>If the current path is "http://hl7.org/svc", then adding ("fhir", "Patient") would
        /// return in a new RestUrl "http://hl7.org/svc/fhir/Patient"</example>
        public RestUrl AddPath(params string[] components)
        {
            string _components = string.Join("/", components).Trim('/');
            _builder.Path = delimit(_builder.Path)+ _components;
            return this;
        }


        public RestUrl ClearParams()
        {
            _parameters.Clear();
            return this;
        }


        public RestUrl SetParam(string name, string value)
        {
            var ix = _parameters.FindIndex(p => p.Item1 == name);
            var tp = Tuple.Create(name, value);
            if (ix == -1)
                _parameters.Add(tp);
            else
            {
                _parameters.RemoveAll(p => p.Item1 == name);
                _parameters.Insert(ix, tp);
            }

            return this;
        }

        public RestUrl ClearParam(string name)
        {
            _parameters.RemoveAll(p => p.Item1 == name);
            return this;
        }


        /// <summary>
        /// Add a query parameter to the RestUrl
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public RestUrl AddParam(string name, string value)
        {
            if (name == null) throw Error.ArgumentNull(nameof(name));
            if (value == null) throw Error.ArgumentNull(nameof(value));

            return AddParam(Tuple.Create(name, value));
        }

        public RestUrl AddParam(Tuple<string, string> keyValue)
        {
            if (keyValue == null) throw Error.ArgumentNull(nameof(keyValue));

            _parameters.Add(keyValue);

            return this;
        }

        public RestUrl AddParams(IEnumerable<Tuple<string, string>> keyValues)
        {
            if (keyValues == null) throw Error.ArgumentNull(nameof(keyValues));

            _parameters.AddRange(keyValues);

            return this;
        }

        /// <summary>
        /// Tests to see if this RestURL is contained at the base address provided in <paramref name="other"/>
        /// (This is a case insensitive test)
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true when the other is the starting portion of the given <paramref name="other"/> URL</returns>
        public bool IsEndpointFor(Uri other)
        {
            return IsEndpointFor(other.ToString());
        }

        /// <summary>
        /// Tests to see if this RestURL is contained at the base address provided in <paramref name="other"/>
        /// (This is a case insensitive test)
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true when the other is the starting portion of the given <paramref name="other"/> URL</returns>
        public bool IsEndpointFor(string other)
        {
            var baseAddress = this.Uri.ToString();

            // HACK! To support Fiddler2 on Win8, localhost needs to be spelled out as localhost.fiddler, but still functions as localhost
            baseAddress = baseAddress.Replace("localhost.fiddler", "localhost");
            other = other.Replace("localhost.fiddler", "localhost");

            return new Uri(other, UriKind.RelativeOrAbsolute).IsWithin(new Uri(baseAddress, UriKind.Absolute));
        }

        /// <summary>
        /// Implements comparison of two RestUrls based on the FHIR rules set out in http.html#2.1.0.1
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsSameUrl(RestUrl other)
        {
            var meUri = new RestUrl(this).ClearParams().ToString().RemovePrefix("http://").RemovePrefix("https://");
            var otherUri = new RestUrl(other).ClearParams().ToString().RemovePrefix("http://").RemovePrefix("https://");

            return meUri == otherUri;
        }



        /// <summary>
        /// Returs a new RestUrl that represents a location after navigating to the specified path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <example>If the current path is "http://hl7.org/svc/patient", NavigatingTo("../observation") will 
        /// result in a ResourceLocation of "http://hl7.org/svc/observation" whereas if the current path is
        /// "http://hl7.org/svc/ (note the slash), NavigatingTo("../observation") will 
        /// result in a ResourceLocation of "http://hl7.org/svc/observation" 
        /// </example>
        public RestUrl NavigateTo(string path)
        {
            if (path == null) throw Error.ArgumentNull(nameof(path));

            return NavigateTo(new Uri(path, UriKind.RelativeOrAbsolute));
        }

        public RestUrl NavigateTo(Uri path)
        {
            if (path == null) throw Error.ArgumentNull(nameof(path));

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
