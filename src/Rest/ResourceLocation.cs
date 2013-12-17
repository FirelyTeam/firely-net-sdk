using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Rest
{ 
    public class ResourceLocation
    {
        public static readonly Uri SERVICE = new Uri("http://localhost/");

        private static readonly string[] locationOperations = new string[] 
            { 
                RestOperation.HISTORY, 
                RestOperation.TAGS 
            };

        public ResourceLocation(Uri uri) : this(SERVICE, uri) { }
        public ResourceLocation(string uri) : this(SERVICE, new Uri(uri)) { }
        public ResourceLocation() : this(SERVICE, null) { }


        public static bool IsLocationOperation(string s)
        {
            return locationOperations.Contains(s);
        }

        public static ResourceLocation Construct(Uri service, params string[] components)
        {
            Uri uri = ConstructUri(service, components);
            return new ResourceLocation(service, uri);
        }
        public static ResourceLocation Construct(params string[] components)
        {
            return Construct(SERVICE, components);
        }
        public static ResourceLocation ForUri(Uri uri)
        {
            return new ResourceLocation(uri);
        }
        public static ResourceLocation ForUri(Uri service, Uri uri)
        {
            return new ResourceLocation(service, uri);
        }
        public static ResourceLocation ForUri(string uri)
        {
            return new ResourceLocation(new Uri(uri));
        }
        public static ResourceLocation ForUri(Uri service, string uri)
        {
            return new ResourceLocation(service, new Uri(uri));
        }
        public static ResourceLocation ForType(string type)
        {
            return ResourceLocation.Construct(type);
        }
        public static ResourceLocation ForResource(string type, string id)
        {
            return ResourceLocation.Construct(type, id);
        }
        public static ResourceLocation ForResource(Uri service, string type, string id)
        {
            return Construct(service, type, id);
        }
        public static ResourceLocation ForHistory(string type, string id, string versionId)
        {
            return ResourceLocation.Construct(type, id, RestOperation.HISTORY, versionId);
        }
        public static ResourceLocation ForValidate(string type, string id)
        {
            return ResourceLocation.Construct(type, RestOperation.VALIDATE, id);
        }

        public string Collection
        {
            get
            {
                deconstruct();
                return _type;
            }
            set
            {
                deconstruct();
                _type = value;
                state = State.AsProperties;
            }
        }
        public string Id
        {
            get
            {
                deconstruct();
                return _id;
            }
            set
            {
                deconstruct();
                _id = value;
                state = State.AsProperties;
            }
        }
        public string VersionId
        {
            get
            {
                deconstruct();
                return _vid;
            }
            set
            {
                deconstruct();
                _vid = value;
                state = State.AsProperties;
            }
        }

        public Uri ServiceUri
        {
            get { return _service; }
        }
        public string OperationPath
        {
            get
            {
                deconstruct();
                return constructPath();
            }
        }
        public Uri OperationUri
        {
            get
            {
                return new Uri(OperationPath, UriKind.Relative);
            }
        }
        public string Operation
        {
            get
            {
                deconstruct();
                return _operation;
            }
            set
            {
                deconstruct();
                _operation = value;
                state = State.AsProperties;
            }
        }
        public string QueryString
        {
            get
            {
                deconstruct();
                return this.query;
            }
            set
            {
                deconstruct();
                this.query = value.TrimStart('?');
                state = State.AsProperties;
            }
        }
        public Uri Uri
        {
            get
            {
                construct();
                return this.uri;
            }
            set
            {
                this.uri = value;
                reset();
                state = (value != null) ? State.AsUri : State.AsProperties;

            }
        }

        public string AsString
        {
            get
            {
                return this.Uri.ToString();
            }
        }

        public override string ToString()
        {
            return AsString;
        }


        private enum State { AsUri, Synchronized, AsProperties }


        
        
        private Uri uri;
        private State state;
        private string query;
        private Uri _service;
        private string _type;
        private string _id;
        private string _operation;
        private string _vid;

        private static Uri ConstructUri(Uri service, params string[] components)
        {
            UriBuilder builder = new UriBuilder(service);
            string _path = delimit(builder.Path);
            string _components = string.Join("/", components).Trim('/');
            builder.Path = _path + _components;

            return builder.Uri;
        }
        private void reset()
        {
            _type = null;
            _id = null;
            _operation = null;
            _vid = null;
            query = null;
        }
        private void deconstructPath(Uri uri)
        {
            reset();
            var components = new Components(_service, uri);
            _type = components[0];
            _id = components[1];
            _operation = components[2];
            _vid = components[3];

        }
        private string constructPath()
        {
            var components = new Components();
            components[0] = _type;
            components[1] = _id;
            if (_vid != null)
            {
                components[2] = RestOperation.HISTORY;
                components[3] = _vid;
            }
            return components.ToPath();
        }
        private void deconstruct()
        {
            if (state == State.AsUri)
            {
                if (uri.IsAbsoluteUri)
                {
                    deconstructPath(uri);
                    this.query = uri.Query.TrimStart('?');
                    state = State.Synchronized;
                }
                else
                {
                    throw Error.InvalidOperation("Cannot deconstruct foreign URI.");
                }
            }
        }
        private void construct()
        {
            if (state == State.AsProperties)
            { 
                UriBuilder builder = new UriBuilder(_service);
                string _path = delimit(builder.Path);
                string _components = constructPath();
                builder.Path = _path + _components;
                builder.Query = this.query;
                this.uri = builder.Uri;
                this.state = State.Synchronized;
            }
        }
        private static string delimit(string path)
        {
            return path.EndsWith(@"/") ? path : path + @"/";
        }

        private ResourceLocation(Uri service, Uri uri)
        {
            if (uri == null)
            {
                this._service = service;
                this.Uri = uri;
            }
            else if (uri.IsAbsoluteUri)
            {
                if (service.IsBaseOf(uri))
                {
                    this._service = service;
                    this.Uri = uri;
                }
                else
                {
                    this._service = null;
                    this.Uri = uri; // throw new InvalidOperationException("If the url is not from this server, the service URI must be provided.");
                }
            }
            else 
            {
                this._service = service;// relative uri....
                string relativeUri = uri.ToString();
                this.Uri = new Uri(service, relativeUri);
            }
        }

        private class Components
        {
            List<string> components = new List<string>();
            public Components(Uri service, Uri uri)
            {
                if (service.IsBaseOf(uri))
                {
                    int n = getComponents(service).Count();
                    components = getComponents(uri).Skip(n).ToList();
                }
            }
            public Components()
            {

            }
            public Components Add(string value)
            {
                components.Add(value);
                return this;
            }

            private IEnumerable<string> getComponents(Uri uri)
            {
                return uri.LocalPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            }
            public string ToPath()
            {
                return string.Join("/", components);
            }
            public string this[int index]
            {
                get
                {
                    return (components.Count >= index + 1) ? components[index] : null;
                }
                set
                {
                    if (value != null)
                    {
                        while (components.Count < index + 1) components.Add(null);
                        components[index] = value;
                    }

                }
            }
        }
    }
}
