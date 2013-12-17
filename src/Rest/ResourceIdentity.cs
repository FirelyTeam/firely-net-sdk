using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Rest
{
    public class ResourceIdentity : Uri
    {
        private Uri endpoint;

        public ResourceIdentity(string uri) : base(uri) {  }
        public ResourceIdentity(Uri endpoint) : base(endpoint.ToString()) { }

        public void Build(Uri endpoint, string collection, string id)
        {
            construct(endpoint, collection, id);
        }
        public void Build(Uri endpoint, string collection, string id, string vid)
        {
            construct(endpoint, collection, id, RestOperation.HISTORY, vid);
        }
        public void Build(string collection, string id)
        {
            construct(endpoint, collection, id);
        }
        public void Build(string collection, string id, string vid)
        {
            construct(endpoint, collection, id, RestOperation.HISTORY, vid);
        }
        private static string delimit(string path)
        {
            return path.EndsWith(@"/") ? path : path + @"/";
        }
        private static Uri construct(Uri service, params string[] components)
        {
            UriBuilder builder = new UriBuilder(service);
            string _path = delimit(builder.Path);
            string _components = string.Join("/", components).Trim('/');
            builder.Path = _path + _components;

            return builder.Uri;
        }

        private List<string> _components = null;
        private IEnumerable<string> GetComponents()
        {
            return this.LocalPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
        }
        public List<string> Components
        {
            get
            {
                if (_components == null)
                {
                    _components = GetComponents().ToList();
                }
                return _components;
            }
        }

        public string Collection
        {
            get 
            {
                int index = Components.IndexOf(RestOperation.HISTORY);
                if (index >= 2)
                {
                    return Components[index - 2];
                }
                else if (Components.Count >= 2)
                {
                    return Components[Components.Count - 2];
                }
                else
                {
                    return null;
                }
                    
            }
            
        }
        public string Id
        {
            get
            {
                int index = Components.IndexOf(RestOperation.HISTORY);
                if (index >= 2)
                {
                    return Components[index - 1];
                }
                else if (index == -1 && Components.Count >= 2)
                {
                    return Components[Components.Count - 1];
                }
                else
                {
                    return null;
                }

            }

        }
        public string VersionId
        {
            get
            {
                int index = Components.IndexOf(RestOperation.HISTORY);
                if (index >= 2 && Components.Count >= 4)
                {
                    return Components[index + 1];
                }
                else
                {
                    return null;
                }

            }

        }
    }
}
