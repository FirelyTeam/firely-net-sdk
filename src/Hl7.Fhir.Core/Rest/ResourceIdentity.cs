/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
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
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Utility;
#if DOTNETFW
using System.Runtime.Serialization;
#endif
using Hl7.Fhir.Model;

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
    ///   * Urn: the "base", in this case either urn:oid: or urn:uuid: and the "logical id", both are required
    ///   * Anchor: just the "logical id"
    /// 
    /// </summary>
#if DOTNETFW
    [SerializableAttribute]
#endif
    [System.Diagnostics.DebuggerDisplay(@"\{ResourceType={ResourceType} Id={Id} VersionId={VersionId} Base={BaseUri} ToString={ToString()}")]
    public class ResourceIdentity : Uri
    {
        /// <summary>
        /// Creates an Resource Identity instance for a Resource given a resource's location.
        /// </summary>
        /// <param name="uri">Relative or absolute location of a Resource</param>
        /// <returns></returns>
        public ResourceIdentity(string uri) : base(uri, UriKind.RelativeOrAbsolute) 
        {
            parseComponents(uri);
        }

        /// <summary>
        /// Creates an Resource Identity instance for a Resource given a resource's location.
        /// </summary>
        /// <param name="uri">Relative or absolute location of a Resource</param>
        /// <returns></returns>
        public ResourceIdentity(Uri uri) : base(uri.OriginalString, UriKind.RelativeOrAbsolute) 
        {
            parseComponents(this.OriginalString);
        }

        private ResourceIdentity(string uri, UriKind kind) : base(uri, kind) { }

        private static string constructUri(Uri baseUri, string resourceType, string id, string versionId, ResourceIdentityForm form)
        {
            if (form == ResourceIdentityForm.AbsoluteRestUrl || form == ResourceIdentityForm.RelativeRestUrl)
            {
                if (baseUri == null)
                {
                    return versionId != null ?
                        string.Format("{0}/{1}/{2}/{3}", resourceType, id, TransactionBuilder.HISTORY, versionId) :
                        string.Format("{0}/{1}", resourceType, id);
                }
                else
                {
                    if (versionId != null)
                        return construct(baseUri, resourceType, id, TransactionBuilder.HISTORY, versionId);
                    else
                        return construct(baseUri, resourceType, id);
                }
            }
            else if (form == ResourceIdentityForm.Urn)
            {
                return baseUri + id;
            }
            else if (form == ResourceIdentityForm.Local)
            {
                return "#" + id;
            }
            else
            {
                throw Error.NotImplemented("Unsupported uri kind " + form.ToString());
            }

        }

        private ResourceIdentity(Uri baseUri, string resourceType, string id, string versionId, ResourceIdentityForm form) : 
            base(constructUri(baseUri,resourceType,id,versionId, form), UriKind.RelativeOrAbsolute)
        {
            BaseUri = baseUri;
            ResourceType = resourceType;
            Id = id;
            VersionId = versionId;
            Form = form;
        }

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
            if (baseUrl == null) throw Error.ArgumentNull(nameof(baseUrl));
            if (!baseUrl.IsAbsoluteUri) throw Error.Argument(nameof(baseUrl), "Base must be an absolute path");
            if (resourceType == null) throw Error.ArgumentNull(nameof(resourceType));
            if (id == null) throw Error.ArgumentNull(nameof(id));

            return new ResourceIdentity(baseUrl, resourceType, id, vid, ResourceIdentityForm.AbsoluteRestUrl);
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
            if (resourceType == null) throw Error.ArgumentNull(nameof(resourceType));
            if (id == null) throw Error.ArgumentNull(nameof(id));

            return new ResourceIdentity(null, resourceType, id, vid, ResourceIdentityForm.RelativeRestUrl);
        }


        /// <summary>
        /// Creates an absolute urn representing the Resource identitity outside of a REST context
        /// </summary>
        /// <param name="urnType">Type of urn to create, either OID or UUID</param>
        /// <param name="id">The resource's logical id</param>
        /// <returns></returns>
        public static ResourceIdentity Build(UrnType urnType, string id)
        {
            //if (baseUrn == null) throw Error.ArgumentNull(nameof(baseUrn));
            //if (!baseUrn.IsAbsoluteUri) throw Error.Argument(nameof(baseUrn), "Base must be an absolute path");
            //if (!isUrn(baseUrn.OriginalString)) throw Error.Argument(nameof(baseUrn), "Base must be a urn:oid: or urn:uuid:");
            if (id == null) throw Error.ArgumentNull(nameof(id));

            Uri baseUrn;

            if (urnType == UrnType.OID)
                baseUrn = new Uri("urn:oid:");
            else
                baseUrn = new Uri("urn:uuid:");

            return new ResourceIdentity(baseUrn, null, id, null, ResourceIdentityForm.Urn);
        }


        /// <summary>
        /// Creates an absolute urn representing the Resource identitity outside of a REST context
        /// </summary>
        /// <param name="baseUrn">An urn, either an urn:oid: or urn:uuid:</param>
        /// <param name="id">The resource's logical id</param>
        /// <returns></returns>
        public static ResourceIdentity Build(Uri baseUrn, string id)
        {
            if (baseUrn == null) throw Error.ArgumentNull(nameof(baseUrn));
            if (!baseUrn.IsAbsoluteUri) throw Error.Argument(nameof(baseUrn), "Base must be an absolute path");
            if (!isUrn(baseUrn.OriginalString)) throw Error.Argument(nameof(baseUrn), "Base must be a urn:oid: or urn:uuid:");
            if (id == null) throw Error.ArgumentNull(nameof(id));

            return new ResourceIdentity(baseUrn, null, id, null, ResourceIdentityForm.Urn);
        }


        /// <summary>
        /// Creates an local id that can be the target of an anchored reference to a contained resource elative url representing a Resource identitity for a given resource type, id and optional version.
        /// </summary>
        /// <param name="id">The resource's logical id</param>
        /// <returns></returns>
        public static ResourceIdentity Build(string id)
        {
            if (id == null) throw Error.ArgumentNull(nameof(id));

            return new ResourceIdentity(null, null, id, null, ResourceIdentityForm.Local);
        }


        public static ResourceIdentity Core(FHIRAllTypes type)
        {
            return ResourceIdentity.Core(type.GetLiteral());
        }


        public const string CORE_BASE_URL = "http://hl7.org/fhir/StructureDefinition/";

        public static ResourceIdentity Core(string type)
        {
            return new ResourceIdentity(CORE_BASE_URL + type);
        }


        private static bool isAbsoluteUrl(string url)
        {
            try
            {
                var uri = new Uri(url,UriKind.RelativeOrAbsolute);
                return uri.IsAbsoluteUri;
            }
            catch
            {
                return false;
            }
        }

        private static bool isAbsoluteRestUrl(string url)
        {
            return isAbsoluteUrl(url) && new Uri(url).Scheme != "urn";
        }

        private static bool isRelativeRestUrl(string url)
        {
            return !isAbsoluteUrl(url) && !isLocal(url) && !isUrn(url);
        }

        private static bool isUrn(string uri)
        {
            return uri.StartsWith("urn:oid:") || uri.StartsWith("urn:uuid:");
            //return uri.StartsWith("urn:uuid:");   // Only UUID as per bundle.html#6.7.4
        }

        private static bool isLocal(string url)
        {
            return url.StartsWith("#");
        }

        // Encure path ends in a '/'
        private static string delimit(string path)
        {
            return path.EndsWith(@"/") ? path : path + @"/";
        }

        private static string construct(Uri endpoint, IEnumerable<string> components)
        {
            UriBuilder builder = new UriBuilder(endpoint);
            string _path = delimit(builder.Path);
            string _components = string.Join("/", components).Trim('/');
            builder.Path = _path + _components;

            return builder.Uri.ToString();
        }

        private static string construct(Uri endpoint, params string[] components)
        {
            return construct(endpoint, (IEnumerable<string>)components);
        }


        public ResourceIdentityForm Form { get; private set; }

        public bool IsAbsoluteRestUrl{ get{ return Form == ResourceIdentityForm.AbsoluteRestUrl; } }

        public bool IsRelativeRestUrl{ get { return Form == ResourceIdentityForm.RelativeRestUrl; } }
       
        public bool IsUrn { get  { return Form == ResourceIdentityForm.Urn; } }

        public bool IsLocal { get { return Form == ResourceIdentityForm.Local; } }


        [Obsolete("Recommend using the ResourceBase property")]
        public Uri Endpoint
        {
            get { return BaseUri; }
        }


        private void parseComponents(string url)
        {
            if (isRelativeRestUrl(url) || isAbsoluteRestUrl(url))
            {
                Form = ResourceIdentityForm.Undetermined;

                var uri = new Uri(url, UriKind.RelativeOrAbsolute);
                string localPath = uri.IsAbsoluteUri ? uri.LocalPath : url;

                var components = localPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                int count = components.Length;

                if (count < 2) return;  // unparseable               

                var history = -1;
                for(var index=0; index<count; index++) 
                    if(components[index] == TransactionBuilder.HISTORY) history = index;
                if (history > -1 && history == count - 1) return; // illegal use, there's just a _history component, but no version id

                int resourceTypePos = (history > -1) ? history - 2 : count - 2;

                ResourceType = components[resourceTypePos];
                if (!ModelInfo.IsKnownResource(ResourceType))
                {
                    ResourceType = null;
                    return;
                }

                Id = components[resourceTypePos + 1];

                if (uri.IsAbsoluteUri)
                {
                    var baseUri = url.Substring(0, url.IndexOf("/" + ResourceType + "/"));
                    if (!baseUri.EndsWith("/")) baseUri += "/";
                    BaseUri = new Uri(baseUri,UriKind.Absolute);
                }

                if (history != -1 && count >= 4 && history < count - 1)
                {
                    VersionId = components[history + 1];
                }

                Form = uri.IsAbsoluteUri ? ResourceIdentityForm.AbsoluteRestUrl : ResourceIdentityForm.RelativeRestUrl;
            }
            else if (isUrn(url))
            {
                int lastSep = 8;

                if (url.StartsWith("urn:uuid:")) lastSep = 8;
                if (url.StartsWith("urn:oid:")) lastSep = 7;

                BaseUri = new Uri(url.Substring(0, lastSep+1), UriKind.Absolute);
                Id = OriginalString.Substring(lastSep + 1);
                Form = ResourceIdentityForm.Urn;
            }
            else if (isLocal(url))
            {
                Id =  OriginalString.Substring(1);        // strip '#'
                Form = ResourceIdentityForm.Local;
            }
        }

        /// <summary>
        /// This is the ResourceIdentity's base, either an url which is the FHIR service endpoint where the resource is located or
        /// an urn (urn:oid or urn:uuid).
        /// </summary>
        public Uri BaseUri
        {
            get; private set;
        }


        [Obsolete("Use the ResourceType instead")]
        public string Collection
        {
            get { return ResourceType; }
        }

        /// <summary>
        /// The name of the resource as it occurs in the Resource url
        /// </summary>
        public string ResourceType
        {
            get; private set;
        }


        /// <summary>
        /// The logical id of the resource as it occurs in the Resource url (without the Version)
        /// </summary>
        public string Id
        {
            get; private set;
        }


        /// <summary>
        /// The version id of the resource as it occurs in the Resource url
        /// </summary>
        public string VersionId
        {
            get; private set;
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
            if (IsAbsoluteRestUrl || IsRelativeRestUrl)
            {
                if (BaseUri == null)
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
            if (IsAbsoluteRestUrl)
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
            if (IsAbsoluteRestUrl || IsRelativeRestUrl)
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
        /// <param name="baseUri"></param>
        /// <returns></returns>
        public ResourceIdentity WithBase(string baseUri)
        {
            if (IsAbsoluteRestUrl || (IsRelativeRestUrl && isAbsoluteRestUrl(baseUri)))
            {
                if (!baseUri.EndsWith("/")) baseUri += "/";
                return ResourceIdentity.Build(new Uri(baseUri, UriKind.Absolute), this.ResourceType, this.Id, this.VersionId);
            }
            else if (isUrn(baseUri))
            {
                return ResourceIdentity.Build(new Uri(baseUri, UriKind.Absolute), this.Id);
            }
            else
                throw Error.InvalidOperation("Cannot give a base to anything else than a relative or absolute url");
        }


        public ResourceIdentity WithBase(Uri baseUri)
        {
            return WithBase(baseUri.OriginalString);
        }


        /// <summary>
        /// Find out whether one ResourceIdentity references another ResourceIdentity
        /// </summary>
        /// <param name="reference"></param>
        /// <returns></returns>
        public bool IsTargetOf(ResourceIdentity reference)
        {
            if (reference.BaseUri != null)
            {
                //TODO: According to the spec, this comparison should ignore http/https
                //(see http.html#2.1.0.1, under the header 'identity')
                if (BaseUri != reference.BaseUri) return false;
            }

            if (ResourceType != reference.ResourceType) return false;
            if (Id != reference.Id) return false;

            if (reference.VersionId != null)
            {
                if (VersionId != reference.VersionId) return false;
            }

            return true;
        }

        public bool IsTargetOf(string reference)
        {
            return IsTargetOf(new ResourceIdentity(reference));
        }

        public bool IsTargetOf(FhirUri reference)
        {
            return IsTargetOf(new ResourceIdentity(reference.Value));
        }

        public bool IsTargetOf(Uri reference)
        {
            return IsTargetOf(new ResourceIdentity(reference.OriginalString));
        }

        public static bool IsRestResourceIdentity(string url)
        {
            return HttpUtil.IsRestResourceIdentity(url);
        }

        public static bool IsRestResourceIdentity(Uri uri)
        {
            if (uri == null) return false;

            return HttpUtil.IsRestResourceIdentity(uri.OriginalString);
        }

    }

    public enum ResourceIdentityForm
    {
        AbsoluteRestUrl,
        RelativeRestUrl,
        Urn,
        Local,
        Undetermined
    }

    public enum UrnType
    {
        UUID,
        OID
    }

   
}
