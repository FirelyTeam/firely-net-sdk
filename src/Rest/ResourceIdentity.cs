using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Rest
{
    public class ResourceIdentity : Uri
    {
        public ResourceIdentity(string uri) : base(uri) {  }
        public ResourceIdentity(Uri uri) : base(uri.ToString()) { }
        internal ResourceIdentity(string uri, UriKind kind) : base(uri, kind) { }
        
        /// <summary>
        /// Creates an absolute Uri representing a Resource identitity for a given resource type and id.
        /// </summary>
        /// <param name="endpoint">Absolute path giving the FHIR service endpoint</param>
        /// <param name="collection">Name of the collection (resource type)</param>
        /// <param name="id">The resource's logical id</param>
        /// <returns></returns>
        public static ResourceIdentity Build(Uri endpoint, string collection, string id)
        {
            if (collection == null) Error.ArgumentNull("collection");
            if (id == null) Error.ArgumentNull("id");
            if (!endpoint.IsAbsoluteUri) Error.Argument("endpoint", "endpoint must be an absolute path");

            return new ResourceIdentity(construct(endpoint, collection, id));
        }

        /// <summary>
        /// Creates an absolute Uri representing a Resource identitity for a given resource type, id and version.
        /// </summary>
        /// <param name="endpoint">Absolute path giving the FHIR service endpoint</param>
        /// <param name="collection">Name of the collection (resource type)</param>
        /// <param name="id">The resource's logical id</param>
        /// <param name="vid">The resource's version id</param>
        /// <returns></returns>
        public static ResourceIdentity Build(Uri endpoint, string collection, string id, string vid)
        {
            if (collection == null) Error.ArgumentNull("collection");
            if (id == null) Error.ArgumentNull("id");
            if (vid == null) Error.ArgumentNull("vid");
            if (!endpoint.IsAbsoluteUri) Error.Argument("endpoint", "endpoint must be an absolute path");
            
            return new ResourceIdentity(construct(endpoint, collection, id, RestOperation.HISTORY, vid));
        }


        /// <summary>
        /// Creates an relative Uri representing a Resource identitity for a given resource type and id.
        /// </summary>
        /// <param name="collection">Name of the collection (resource type)</param>
        /// <param name="id">The resource's logical id</param>
        /// <returns></returns>
        public static ResourceIdentity Build(string collection, string id)
        {
            if (collection == null) Error.ArgumentNull("collection");
            if (id == null) Error.ArgumentNull("id");

            return new ResourceIdentity(string.Format("{0}/{1}", collection, id), UriKind.Relative);
        }


        /// <summary>
        /// Creates a relative Uri representing a Resource identitity for a given resource type, id and version.
        /// </summary>
        /// <param name="collection">Name of the collection (resource type)</param>
        /// <param name="id">The resource's logical id</param>
        /// <param name="vid">The resource's version id</param>
        /// <returns></returns>

        public static ResourceIdentity Build(string collection, string id, string vid)
        {
            if (collection == null) Error.ArgumentNull("collection");
            if (id == null) Error.ArgumentNull("id");
            if (vid == null) Error.ArgumentNull("vid");

            return new ResourceIdentity(
                string.Format("{0}/{1}/{2}/{3}", collection, id, RestOperation.HISTORY, vid),
                UriKind.Relative);
        }
        
        
        // Encure path ends in a '/'
        private static string delimit(string path)
        {
            return path.EndsWith(@"/") ? path : path + @"/";
        }


        private static Uri construct(Uri service, IEnumerable<string> components)
        {
            UriBuilder builder = new UriBuilder(service);
            string _path = delimit(builder.Path);
            string _components = string.Join("/", components).Trim('/');
            builder.Path = _path + _components;

            return builder.Uri;
        }

        private static Uri construct(Uri service, params string[] components)
        {
            return construct(service,(IEnumerable<string>)components);
        }

        private List<string> _components = null;

        private string getHost()
        {
            return this.GetComponents(UriComponents.Scheme | UriComponents.UserInfo |
                            UriComponents.Host | UriComponents.Port, UriFormat.SafeUnescaped);
        }

        private IEnumerable<string> splitPath()
        {
            return this.LocalPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
        }

        internal List<string> Components
        {
            get
            {
                if (_components == null)
                {
                    _components = splitPath().ToList();
                }
                return _components;
            }
        }

        /// <summary>
        /// The name of the resource as it occurs in the Resource url
        /// </summary>
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


        /// <summary>
        /// The logical id of the resource as it occurs in the Resource url
        /// </summary>
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


        /// <summary>
        /// The version id of the resource as it occurs in the Resource url
        /// </summary>
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

        public ResourceIdentity WithVersion(string version)
        {           
            int index = Components.IndexOf(RestOperation.HISTORY);

            if (index == -1)
                return new ResourceIdentity(construct(this, RestOperation.HISTORY, version));
            else
            {
                var path = construct(new Uri(getHost()), Components.Take(index).Concat( new string[] { RestOperation.HISTORY, version }));
                return new ResourceIdentity(path);
            }
        }

        public ResourceIdentity RemoveVersion()
        {
            int index = Components.IndexOf(RestOperation.HISTORY);

            if (index == -1)
                return this;
            else
            {
                var path = construct(new Uri(getHost()), Components.Take(index));
                return new ResourceIdentity(path);
            }
        }

        public Uri OperationPath()
        {
            // dit maakt de uri altijd relatief
            return ResourceIdentity.Build(this.Collection, this.Id, this.VersionId);
        }
    }

   
}
