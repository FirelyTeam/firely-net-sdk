/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if !PORTABLE45 || NET45
using System.Runtime.Serialization;
using Hl7.Fhir.Model;
#endif

namespace Hl7.Fhir.Rest
{
    /// <summary>
    /// ResourceIdentity represents a Resource's Uri and can be used to construct such uri's or retrieve parts of the uri. 
    /// 
    /// ResourceIdentities exist in three forms: 
    ///   * As an url, where urls may be relative or absolute. The url form can be used in RESTful 
    ///     calls to a FHIR server to represent a specific instance of a resource on that server. 
    ///   * As the target of an http anchor, in which case the identity is just an id. This kind of id is used when
    ///     the resource is contained within another resource and the id is "internal": valid only within the scope of
    ///     that container resource. It can be referenced by just its id, prefixed by the http anchor '#' character.
    ///   * As an urn, useful when you need to identify a resource outside of the context of a RESTful exchange,
    ///     such as in a Document or Message or when the resource has not yet been assigned an id, like in a
    ///     batch transaction.
    /// 
    /// Depending on the form of ResourceIdentity, the identity consists of the following parts:
    /// 
    ///   * Url: the "base", in this case the server's base FHIR service endpoint; the "resource type"; the "logical id" and the "version id",
    ///     all parts are optional, except the logical id.
    ///   * Urn: the "base", in this case either a uuid/oid urn (e.g. urn:oid: or urn:uuid:) and the "logical id", both are required
    ///   * Anchor: just the "logical id"
    /// 
    /// </summary>
#if !PORTABLE45 || NET45
    [SerializableAttribute]
#endif
    [System.Diagnostics.DebuggerDisplay(@"\{Collection={Collection} Id={Id} VersionId={VersionId} Endpoint={Endpoint}}")]
    public class ResourceIdentity : Uri
    {
        /// <summary>
        /// Creates an Resource Identity instance for a Resource given a resource's location.
        /// </summary>
        /// <param name="uri">Relative or absolute location of a Resource</param>
        /// <returns></returns>
        public ResourceIdentity(string uri) : base(uri, UriKind.RelativeOrAbsolute) { }

        /// <summary>
        /// Creates an Resource Identity instance for a Resource given a resource's location.
        /// </summary>
        /// <param name="uri">Relative or absolute location of a Resource</param>
        /// <returns></returns>
        public ResourceIdentity(Uri uri) : base(uri.ToString(), UriKind.RelativeOrAbsolute) { }


        internal ResourceIdentity(string uri, UriKind kind) : base(uri, kind) { }

        #region << Serialization Implementation >>
#if !PORTABLE45 || NET45
        // The default serialization is all that is required as this class does
        // not contain any of it's own properties that are not contained in the actual Uri
        protected ResourceIdentity(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        protected virtual new void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
#endif
        #endregion

        /// <summary>
        /// Creates an absolute url representing a Resource identitity for a given resource type, id and optional version.
        /// </summary>
        /// <param name="baseUrl">Absolute path giving the FHIR service endpoint</param>
        /// <param name="resourceType">Name of the collection (resource type)</param>
        /// <param name="id">The resource's logical id</param>
        /// <param name="vid">The resource's version id</param>
        /// <returns></returns>
        public static ResourceIdentity Build(Uri baseUrl, string resourceType, string id, string vid = null)
        {
            if (baseUrl == null) throw Error.ArgumentNull("baseUrl");
            if (!baseUrl.IsAbsoluteUri) throw Error.Argument("baseUrl", "Base must be an absolute path");
            if (resourceType == null) throw Error.ArgumentNull("resourceType");
            if (id == null) throw Error.ArgumentNull("id");

            if (vid != null)
                return new ResourceIdentity(construct(baseUrl, resourceType, id, RestOperation.HISTORY, vid));
            else
                return new ResourceIdentity(construct(baseUrl, resourceType, id));
        }


        /// <summary>
        /// Creates a relative url representing a Resource identitity for a given resource type, id and optional version.
        /// </summary>
        /// <param name="resourceType">The resource type</param>
        /// <param name="id">The resource's logical id</param>
        /// <param name="vid">The resource's version id</param>
        /// <returns></returns>

        public static ResourceIdentity Build(string resourceType, string id, string vid = null)
        {
            if (resourceType == null) throw Error.ArgumentNull("resourceType");
            if (id == null) throw Error.ArgumentNull("id");

            string url = vid != null ?
                string.Format("{0}/{1}/{2}/{3}", resourceType, id, RestOperation.HISTORY, vid) :
                string.Format("{0}/{1}", resourceType, id);

            return new ResourceIdentity(url, UriKind.Relative);
        }


        /// <summary>
        /// Creates an absolute urn representing the Resource identitity outside of a REST context
        /// </summary>
        /// <param name="baseUrn">An urn, either an urn:oid or urn:uuid</param>
        /// <param name="id">The resource's logical id</param>
        /// <returns></returns>
        public static ResourceIdentity Build(Uri baseUrn, string id)
        {
            if (baseUrn == null) throw Error.ArgumentNull("baseUrn");
            if (!baseUrn.IsAbsoluteUri) throw Error.Argument("baseUrn", "Base must be an absolute path");
            if (!IsFhirUrn(baseUrn.OriginalString)) throw Error.Argument("baseUrn", "Base must be a urn:oid: or urn:uuid:");
            if (id == null) throw Error.ArgumentNull("id");

            return new ResourceIdentity(baseUrn.OriginalString + ":" + id);
        }


        internal static bool IsFhirUrn(string uri)
        {
            return uri.StartsWith("urn:oid:") || uri.StartsWith("urn:uuid:");
        }

        /// <summary>
        /// Creates an local id that can be the target of an anchored reference to a contained resource elative url representing a Resource identitity for a given resource type, id and optional version.
        /// </summary>
        /// <param name="collection">Name of the collection (resource type)</param>
        /// <param name="id">The resource's logical id</param>
        /// <param name="vid">The resource's version id</param>
        /// <returns></returns>
        public static ResourceIdentity Build(string id)
        {
            if (id == null) throw Error.ArgumentNull("id");

            return new ResourceIdentity("#" + id, UriKind.Relative);
        }


        public bool IsFullRestUrl
        {
            get { return this.IsAbsoluteUri && this.Scheme != "urn"; }
        }

        public bool IsRelativeRestUrl
        {
            get { return !this.IsAbsoluteUri && !IsLocal && !IsUrn; }
        }

        public bool IsUrn
        {
            get { return IsFhirUrn(OriginalString); }
        }

        public bool IsLocal
        {
            get { return OriginalString.StartsWith("#"); }
        }

        // Encure path ends in a '/'
        private static string delimit(string path)
        {
            return path.EndsWith(@"/") ? path : path + @"/";
        }

        private static Uri construct(Uri endpoint, IEnumerable<string> components)
        {
            UriBuilder builder = new UriBuilder(endpoint);
            string _path = delimit(builder.Path);
            string _components = string.Join("/", components).Trim('/');
            builder.Path = _path + _components;

            return builder.Uri;
        }

        private static Uri construct(Uri endpoint, params string[] components)
        {
            return construct(endpoint, (IEnumerable<string>)components);
        }

        private List<string> _components = null;

        private IEnumerable<string> splitPath()
        {
            string path = (this.IsAbsoluteUri) ? this.LocalPath : this.ToString();
            return path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
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


        [Obsolete]
        public Uri Endpoint
        {
            get { return BaseUri; }
        }


        /// <summary>
        /// This is the ResourceIdentity's base, either an url which is the FHIR service endpoint where the resource is located or
        /// an urn (urn:oid or urn:uuid).
        /// </summary>
        public Uri BaseUri
        {
            get
            {
                if (IsRelativeRestUrl || IsFullRestUrl)
                {
                    int count = Components.Count;

                    if (count < 2)
                        return null;

                    int index = Components.IndexOf(RestOperation.HISTORY);
                    int n = (index > 0) ? index - 2 : count - 2;
                    IEnumerable<string> _components = Components.Skip(n);
                    string path = string.Join("/", _components).Trim('/');
                    string s = this.ToString();
                    string endpoint = s.Remove(s.LastIndexOf(path));

                    return (endpoint.Length > 0) ? new Uri(endpoint, UriKind.Absolute) : null;
                }
                else if (IsUrn)
                {
                    var lastSep = OriginalString.LastIndexOf(':');
                    return new Uri(OriginalString.Substring(0,lastSep), UriKind.Absolute);
                }
                else
                    return null;
                
            }
        }


        [Obsolete]
        public string Collection
        {
            get { return ResourceType; }
        }

        /// <summary>
        /// The name of the resource as it occurs in the Resource url
        /// </summary>
        public string ResourceType
        {
            get
            {
                if (IsRelativeRestUrl || IsFullRestUrl)
                {
                    int index = Components.IndexOf(RestOperation.HISTORY);
                    if (index > -1 && index == Components.Count - 1) return null; // illegal use, there's just a _history component, but no version id

                    string collectionName = null;
                    if (index >= 2)
                    {
                        collectionName = Components[index - 2];
                    }
                    else if (Components.Count > 2)
                    {
                        collectionName = Components[Components.Count - 2];
                    }
                    else if (Components.Count == 2 && index == -1)
                    {
                        collectionName = Components[0];
                    }

                    return collectionName;
                }
                else
                    return null;

            }
        }


        /// <summary>
        /// The logical id of the resource as it occurs in the Resource url
        /// </summary>
        public string Id
        {
            get
            {
                if (IsFullRestUrl || IsRelativeRestUrl)
                {
                    int index = Components.IndexOf(RestOperation.HISTORY);
                    if (index > -1 && index == Components.Count - 1) return null; // illegal use, there's just a _history component, but no version id

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
                else if (IsUrn)
                {
                    var lastSep = OriginalString.LastIndexOf(':');
                    return OriginalString.Substring(lastSep + 1);
                }
                else
                {
                    return OriginalString.Substring(1);        // strip '#'
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
                if (index > -1 && index == Components.Count - 1) return null; // illegal use, there's just a _history component, but no version id

                if (index >= 2 && Components.Count >= 4 && index < Components.Count - 1)
                {
                    return Components[index + 1];
                }
                else
                {
                    return null;
                }
            }
        }


        /// <summary>
        /// Indicates whether this ResourceIdentity is version-specific (has a _history part)
        /// </summary>
        public bool HasVersion
        {
            get
            {
                return VersionId != null;
            }
        }

        public bool HasBaseUri
        {
            get
            {
                return BaseUri != null;
            }
        }

        /// <summary>
        /// Returns a new ResourceIdentity made specific for the given version
        /// </summary>
        /// <param name="version">The version to add to the ResourceIdentity (part after the _history/)</param>
        /// <returns></returns>
        public ResourceIdentity WithVersion(string version)
        {
            if (IsFullRestUrl || IsRelativeRestUrl)
            {
                Uri endpoint = this.BaseUri;

                if (endpoint == null)
                    return ResourceIdentity.Build(this.ResourceType, this.Id, version);
                else
                    return ResourceIdentity.Build(this.BaseUri, this.ResourceType, this.Id, version);
            }
            else
                throw Error.InvalidOperation("Versions can only be used when the ResourceIdentity is a (relative) url");
        }


        /// <summary>
        /// Returns a Uri that is a relative version of a url form ResourceIdentity
        /// </summary>
        public ResourceIdentity MakeRelative()
        {
            if (IsFullRestUrl)
                return ResourceIdentity.Build(this.ResourceType, this.Id, this.VersionId);
            else if (IsRelativeRestUrl)
                return this;
            else
                throw Error.InvalidOperation("Only ResourceIdentities which are urls can be made relative");
        }


        /// <summary>
        /// Turns a version-specific ResourceIdentity into a non-version-specific ResourceIdentity
        /// </summary>
        /// <returns></returns>
        public ResourceIdentity WithoutVersion()
        {
            if (IsFullRestUrl || IsRelativeRestUrl)
            {
                Uri endpoint = this.BaseUri;

                if (endpoint == null)
                    return ResourceIdentity.Build(this.ResourceType, this.Id);
                else
                    return ResourceIdentity.Build(endpoint, this.ResourceType, this.Id);
            }
            else
                throw Error.InvalidOperation("Only ResourceIdentities which are urls can have version ids");
        }


        /// <summary>
        /// Relocate an absolute identity to a new base, or make a relative identity absolute to a base
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public ResourceIdentity WithBase(string baseUri)
        {
            if (IsFhirUrn(OriginalString) && !IsFhirUrn(baseUri))
                throw Error.InvalidOperation("Cannot turn an urn form ResourceIdentity into url form by changing the base");

            if (IsFhirUrn(baseUri))
                return ResourceIdentity.Build(new Uri(baseUri, UriKind.Absolute), this.Id);
            else if (IsFullRestUrl || IsRelativeRestUrl)
                return ResourceIdentity.Build(new Uri(baseUri, UriKind.Absolute), this.ResourceType, this.Id, this.VersionId);
            else
                throw Error.InvalidOperation("Cannot give a base to a local id");
        }

        public bool IsTargetOf(string reference)
        {
            if (IsFhirUrn(reference))
                return reference == this.OriginalString;

            if (reference.StartsWith("#"))
                return reference == this.OriginalString;

            var refUri = new Uri(reference, UriKind.RelativeOrAbsolute);

            if (this.IsAbsoluteUri && !refUri.IsAbsoluteUri) return false;  // Absolute path can never match a relative one
            if (!this.IsAbsoluteUri && refUri.IsAbsoluteUri) return this.IsWithin(refUri);  // IsBaseOf() is unusable

            // Either both absolute or both relative
            return this.OriginalString == reference;
        }

        public bool IsTargetOf(FhirUri reference)
        {
            return IsTargetOf(reference.Value);
        }

        public bool IsTargetOf(Uri reference)
        {
            return IsTargetOf(reference.OriginalString);
        }

        public bool IsTargetOf(ResourceReference reference)
        {
            return IsTargetOf(reference.Reference);
        }
    }
}
