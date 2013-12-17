using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Rest
{
    public class ResourceIdentity : Uri
    {
        public ResourceIdentity(string uri) : base(uri) {  }
        internal ResourceIdentity(string uri, UriKind kind) : base(uri.ToString(), kind) { }
        internal ResourceIdentity(Uri uri) : base(uri.ToString()) { }

        public static ResourceIdentity Build(Uri endpoint, string collection, string id)
        {
            return new ResourceIdentity(construct(endpoint, collection, id));
        }
        public static ResourceIdentity Build(Uri endpoint, string collection, string id, string vid)
        {
            return new ResourceIdentity(construct(endpoint, collection, id, RestOperation.HISTORY, vid));
        }
        
        public static ResourceIdentity Build(string collection, string id)
        {
            return new ResourceIdentity(string.Format("{0}/{1}", collection, id), UriKind.Relative);
        }
        public static ResourceIdentity Build(string collection, string id, string vid)
        {
            return new ResourceIdentity(
                string.Format("{0}/{1}/{2}/{3}", collection, id, RestOperation.HISTORY, vid),
                UriKind.Relative);
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
