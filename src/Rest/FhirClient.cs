/*
  Copyright (c) 2011-2013, HL7, Inc.
  All rights reserved.
  
  Redistribution and use in source and binary forms, with or without modification, 
  are permitted provided that the following conditions are met:
  
   * Redistributions of source code must retain the above copyright notice, this 
     list of conditions and the following disclaimer.
   * Redistributions in binary form must reproduce the above copyright notice, 
     this list of conditions and the following disclaimer in the documentation 
     and/or other materials provided with the distribution.
   * Neither the name of HL7 nor the names of its contributors may be used to 
     endorse or promote products derived from this software without specific 
     prior written permission.
  
  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
  POSSIBILITY OF SUCH DAMAGE.

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Hl7.Fhir.Support.Search;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Rest;



namespace Hl7.Fhir.Rest
{
    public class FhirClient
    {
        private Endpoint _endpoint;

        /// <summary>
        /// Creates a new client using a default endpoint
        /// </summary>
        public FhirClient(Uri endpoint) : this()
        {
            if (endpoint == null) throw new ArgumentNullException("endpoint");
            _endpoint = new Endpoint(endpoint);
        }

        public FhirClient()
        {
            PreferredFormat = ResourceFormat.Xml;
        }

        /// <summary>
        /// The default endpoint for use with operations that use discrete id/version parameters
        /// instead of explicit uri endpoints.
        /// </summary>
        public Uri Endpoint 
        {
            get
            {
                return _endpoint != null ? _endpoint.Uri : null; 
            }
            set
            {
                _endpoint = new Endpoint(value);
            }
        }


        /// <summary>
        /// Fetches a typed resource from a FHIR resource endpoint.
        /// </summary>
        /// <param name="endpoint">The url of the Resource to fetch. This can be a Resource id url or a version-specific
        /// Resource url.</param>
        /// <typeparam name="TResource">The type of resource to fetch</typeparam>
        /// <returns>The requested resource as a ResourceEntry&lt;T&gt;. This operation will throw an exception
        /// if the resource has been deleted or does not exist</returns>
        public ResourceEntry<TResource> Fetch<TResource>(Uri endpoint) where TResource : Resource, new()
        {
            if (endpoint == null) throw new ArgumentNullException("endpoint");

            var req = createRequest(endpoint, false);

            return doRequest(req, HttpStatusCode.OK, () => resourceEntryFromResponse<TResource>());
        }



        /// <summary>
        /// Fetches a bundle from a FHIR resource endpoint. 
        /// </summary>
        /// <param name="endpoint">The url of the endpoint which returns a Bundle</param>
        /// <returns>The Bundle as received by performing a GET on the endpoint. This operation will throw an exception
        /// if the operation does not result in a HttpStatus OK.</returns>
        public Bundle FetchBundle(Uri endpoint)
        {
            if (endpoint == null) throw new ArgumentNullException("endpoint");

            var req = createRequest(endpoint, true);

            return doRequest(req, HttpStatusCode.OK, () => bundleFromResponse());
        }


        /// <summary>
        /// Get a conformance statement for the system
        /// </summary>
        /// <param name="useOptionsVerb">If true, uses the Http OPTIONS verb to get the conformance, otherwise uses the /metadata endpoint</param>
        /// <returns>A Conformance resource. Throws an exception if the operation failed.</returns>
        public ResourceEntry<Conformance> Conformance(bool useOptionsVerb = false)
        {
            if (Endpoint == null) throw new InvalidOperationException("Endpoint must be provided using either the Endpoint property or the FhirClient constructor");

            return Conformance(Endpoint, useOptionsVerb);
        }


        /// <summary>
        /// Get a conformance statement for the system
        /// </summary>
        /// <param name="useOptionsVerb">If true, uses the Http OPTIONS verb to get the conformance, otherwise uses the /metadata endpoint</param>
        /// <returns>A Conformance resource. Throws an exception if the operation failed.</returns>
        public ResourceEntry<Conformance> Conformance(Uri endpoint, bool useOptionsVerb = false)
        {
            if (endpoint == null) throw new ArgumentNullException("endpoint");

            RestUrl rl;

            if (useOptionsVerb)
                rl = new Endpoint(endpoint).AsRestUrl();
            else
                rl = new Endpoint(endpoint).WithMetadata();

            var req = createRequest(rl.Uri, false);
            req.Method = useOptionsVerb ? "OPTIONS" : "GET";

            return doRequest(req, HttpStatusCode.OK, () => resourceEntryFromResponse<Conformance>());
        }

       
        public ResourceEntry<TResource> Read<TResource>(string id, string versionId=null) where TResource : Resource, new()
        {
            if (Endpoint == null) throw new InvalidOperationException("Endpoint must be provided using either the Endpoint property or the FhirClient constructor");
            if (String.IsNullOrEmpty(id)) throw new ArgumentNullException("id");

            ResourceIdentity ri = buildResourceIdentityUrl<TResource>(id, versionId);

            return Fetch<TResource>(ri);
        }

        private ResourceIdentity buildResourceIdentityUrl<TResource>(string id, string versionId=null) where TResource : Resource, new()
        {
            var collection = typeof(TResource).GetCollectionName();
            ResourceIdentity ri;

            if (versionId != null)
                ri = ResourceIdentity.Build(Endpoint, collection, id, versionId);
            else
                ri = ResourceIdentity.Build(Endpoint, collection, id);
            return ri;
        }


        public ResourceEntry<TResource> VRead<TResource>(string id, string versionId) where TResource : Resource, new()
        {
            if (Endpoint == null) throw new InvalidOperationException("Endpoint must be provided using either the Endpoint property or the FhirClient constructor");
            if (String.IsNullOrEmpty(id)) throw new ArgumentNullException("id");
            if (String.IsNullOrEmpty(versionId)) throw new ArgumentNullException("versionId");
            
            return Read<TResource>(id,versionId);
        }


        /// <summary>
        /// Update (or create) a resource at a given endpoint
        /// </summary>
        /// <param name="entry">A ResourceEntry containing the resource to update</param>
        /// <param name="versionAware">Whether or not version aware update is used.</param>
        /// <typeparam name="TResource">The type of resource that is being updated</typeparam>
        /// <returns>The resource as updated on the server. Throws an exception when the update failed,
        /// in particular may throw an exception when the server returns a 409 when a conflict is detected
        /// while using version-aware updates or 412 if the server requires version-aware updates.</returns>
        public ResourceEntry<TResource> Update<TResource>(ResourceEntry<TResource> entry, bool versionAware = false)
                        where TResource : Resource, new()
        {
            if (entry == null) throw new ArgumentNullException("entry");
            if (entry.Resource == null) throw new ArgumentException("Entry does not contain a Resource to update", "entry");
            if (entry.Id == null) throw new ArgumentException("Entry needs a non-null entry.id to send the update to", "entry");
            if (versionAware && entry.SelfLink == null) throw new ArgumentException("When requesting version-aware updates, Entry.SelfLink may not be null.", "entry");

            string contentType = null;
            byte[] data = null;

            if (entry.Resource is Binary)
            {
                var bin = entry.Resource as Binary;
                data = bin.Content;
                contentType = bin.ContentType;
            }
            else
            {
                data = PreferredFormat == ResourceFormat.Xml ?
                    FhirSerializer.SerializeResourceToXmlBytes(entry.Resource) :
                    FhirSerializer.SerializeResourceToJsonBytes(entry.Resource);
                contentType = ContentType.BuildContentType(PreferredFormat, false);
            }

            var req = createRequest(entry.Id, false);

            req.Method = "PUT";
            req.ContentType = contentType;
            prepareRequest(req, data, entry.Tags);

            // If a version id is given, post the data to a version-specific url
            if (versionAware)
                req.Headers[HttpRequestHeader.ContentLocation] = entry.SelfLink.ToString();

            return doRequest(req, new HttpStatusCode[] { HttpStatusCode.Created, HttpStatusCode.OK }, 
                    () => resourceEntryFromResponse<TResource>() );
        }

 
        /// <summary>
        /// Delete a resource at the given endpoint
        /// </summary>
        /// <param name="id">endpoint of the resource to delete</param>
        /// <typeparam name="TResource">The type of the resource to delete</typeparam>
        /// <returns>Returns normally if delete succeeded, throws an exception otherwise, though this might
        /// just mean the server returned 404 (the resource didn't exist before) or 410 (the resource was
        /// already deleted).</returns>
        public void Delete(Uri endpoint)
        {
            if (endpoint == null) throw new ArgumentNullException("endpoint");

            var req = createRequest(endpoint, false);
            req.Method = "DELETE";

            doRequest(req, HttpStatusCode.NoContent, () => true );
        }


        public void Delete<TResource>(string id) where TResource : Resource, new()
        {
            if (Endpoint == null) throw new InvalidOperationException("Endpoint must be provided using either the Endpoint property or the FhirClient constructor");
            if (String.IsNullOrEmpty(id)) throw new ArgumentNullException("id");

            var rl = buildResourceIdentityUrl<TResource>(id);

            Delete(rl);
        }


        /// <summary>
        /// Create a resource
        /// </summary>
        /// <param name="collectionEndpoint">Endpoint where the resource is sent to be created</param>
        /// <param name="resource">The resource instance to create</param>
        /// <param name="tags">Optional. List of Tags to add to the created instance.</param>
        /// <returns>The resource as created on the server, or an exception if the create failed.</returns>
        /// <typeparam name="TResource">The type of resource to create</typeparam>
        /// <remarks><para>The returned resource need not be the same as the resources passed as a parameter,
        /// since the server may have updated or changed part of the data because of business rules.</para>
        /// </remarks>
        public ResourceEntry<TResource> Create<TResource>(Uri collectionEndpoint, TResource resource, IEnumerable<Tag> tags=null) where TResource : Resource, new()
        {
            if (collectionEndpoint == null) throw new ArgumentNullException("collectionEndpoint");
            if (resource == null) throw new ArgumentNullException("resource");

            string contentType = null;
            byte[] data = null;

            if (resource is Binary)
            {
                var bin = resource as Binary;
                data = bin.Content;
                contentType = bin.ContentType;
            }
            else
            {
                data = PreferredFormat == ResourceFormat.Xml ?
                    FhirSerializer.SerializeResourceToXmlBytes(resource) :
                    FhirSerializer.SerializeResourceToJsonBytes(resource);
                contentType = ContentType.BuildContentType(PreferredFormat, false);
            }

            var req = createRequest(collectionEndpoint, false);
            req.Method = "POST";
            req.ContentType = contentType;
            prepareRequest(req, data, tags);

            return doRequest(req, HttpStatusCode.Created, () => resourceEntryFromResponse<TResource>() );
        }

       
        public ResourceEntry<TResource> Create<TResource>(TResource resource, IEnumerable<Tag> tags=null) where TResource : Resource, new()
        {
            if (Endpoint == null) throw new InvalidOperationException("Endpoint must be provided using either the Endpoint property or the FhirClient constructor");
            if (resource == null) throw new ArgumentNullException("resource");

            string collection = typeof(TResource).GetCollectionName();
                // rem. ResourceLocation.GetCollectionNameForResource(typeof(TResource)); /mh

            Uri uri = _endpoint.ForCollection(collection).Uri;
            // //new ResourceLocation(Endpoint,collection);

            return Create<TResource>(uri, resource, tags);
        }

        public ResourceEntry<TResource> Create<TResource>(TResource resource, string id, IEnumerable<Tag> tags = null) where TResource : Resource, new()
        {
            if (Endpoint == null) throw new InvalidOperationException("Endpoint must be provided using either the Endpoint property or the FhirClient constructor");
            if (resource == null) throw new ArgumentNullException("resource");

            var collection = typeof(TResource).GetCollectionName();
            var rl = ResourceIdentity.Build(collection, id);
            
            var re = new ResourceEntry<TResource>();
            re.Id = rl;
            re.Resource = resource;

            return Update<TResource>(re);
        }


        /// <summary>
        /// Retrieve the version history for a specific resource instance
        /// </summary>
        /// <param name="id">The id of the resource to get the history for</param>
        /// <param name="since">Optional. Returns only changes after the given date</param>
        /// <param name="count">Optional. Asks server to limit the number of entries returned</param>
        /// <returns>A bundle with the history for the indicated instance, may contain both 
        /// ResourceEntries and DeletedEntries.</returns>
	    public Bundle History<TResource>(string id, DateTimeOffset? since = null, int? count = null ) where TResource : Resource, new()
        {
            if (_endpoint == null) throw new InvalidOperationException("Endpoint must be provided using either the Endpoint property or the FhirClient constructor");
            if (String.IsNullOrEmpty(id)) throw new ArgumentNullException("id");

            var collection = typeof(TResource).GetCollectionName();
            var rl = _endpoint.ResourceHistory(collection, id);

            if (since != null) rl.AddParam(HttpUtil.HISTORY_PARAM_SINCE, PrimitiveTypeConverter.Convert<string>(since.Value));
            if (count != null) rl.AddParam(HttpUtil.HISTORY_PARAM_COUNT, count.ToString());

            return FetchBundle(rl.Uri);           
        }


        /// <summary>
        /// Retrieve the version history for all resources of a certain type
        /// </summary>
        /// <param name="since">Optional. Returns only changes after the given date</param>
        /// <param name="count">Optional. Asks server to limit the number of entries returned</param>
        /// <returns>A bundle with the history for the indicated instance, may contain both 
        /// ResourceEntries and DeletedEntries.</returns>
        public Bundle History<TResource>(DateTimeOffset? since = null, int? count = null ) where TResource : Resource, new()
        {
            if (_endpoint == null) throw new InvalidOperationException("Endpoint must be provided using either the Endpoint property or the FhirClient constructor");

            var collection = typeof(TResource).GetCollectionName();
            var rl = _endpoint.CollectionHistory(collection);

            if (count != null) rl.AddParam(HttpUtil.HISTORY_PARAM_COUNT, count.ToString());
            
            return FetchBundle(rl.Uri);
        }


        /// <summary>
        /// Retrieve the version history of any resource on the server
        /// </summary>
        /// <param name="since">Optional. Returns only changes after the given date</param>
        /// <param name="count">Optional. Asks server to limit the number of entries returned</param>
        /// <returns>A bundle with the history for the indicated instance, may contain both 
        /// ResourceEntries and DeletedEntries.</returns>
        public Bundle History(DateTimeOffset? since = null, int? count = null )
        {
            if (_endpoint == null) throw new InvalidOperationException("Endpoint must be provided using either the Endpoint property or the FhirClient constructor");

            var rl = _endpoint.ServerHistory();

            if (since != null) rl.AddParam(HttpUtil.HISTORY_PARAM_SINCE, PrimitiveTypeConverter.Convert<string>(since.Value));
            if (count != null) rl.AddParam(HttpUtil.HISTORY_PARAM_COUNT, count.ToString());

            return FetchBundle(rl.Uri);
        }


        /// <summary>
        /// Validates whether the contents of the resource would be acceptable as an update
        /// </summary>
        /// <param name="entry">The entry containing the updated Resource to validate</param>
        /// <returns>null if validation succeeded, otherwise returns OperationOutcome detailing the validation errors.
        /// If the server returned an error, but did not return an OperationOutcome resource, an exception will be
        /// thrown.</returns>
        public OperationOutcome Validate<TResource>(ResourceEntry<TResource> entry) where TResource : Resource, new()
        {
            if (entry == null) throw new ArgumentNullException("entry");
            if (entry.Resource == null) throw new ArgumentException("Entry does not contain a Resource to validate", "entry");
            if (entry.Id == null) throw new ArgumentException("Entry needs a non-null entry.id to use for validation", "entry");

            string contentType = ContentType.BuildContentType(PreferredFormat, false);

            byte[] data = PreferredFormat == ResourceFormat.Xml ?
                FhirSerializer.SerializeResourceToXmlBytes(entry.Resource) :
                FhirSerializer.SerializeResourceToJsonBytes(entry.Resource);

            var collection = entry.Resource.GetCollectionName();

            var rl = _endpoint.Validate(collection,new ResourceIdentity(entry.Id).Id);

            var req = createRequest(rl.Uri, false);

            req.Method = "POST";
            req.ContentType = contentType;
            prepareRequest(req, data);

            try
            {
                doRequest(req, HttpStatusCode.OK, () => true);
                return null;
            }
            catch(FhirOperationException foe)
            {
                if(foe.Outcome != null)
                    return foe.Outcome;
                else
                    throw foe;
            }
        }

    
        /// <summary>
        /// Search for Resources at a given endpoint
        /// </summary>
        /// <param name="endpoint">The endpoint where the search request is sent.</param>
        /// <param name="count">The maximum number of resources to return</param>
        /// <param name="includes">Zero or more include paths</param>
        /// <typeparam name="TResource">The type of resource to list</typeparam>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        /// <remarks>The endpoint may be a FHIR server for server-wide search or a collection endpoint 
        /// (i.e. /patient) for searching within a certain type of resources. This operation supports include 
        /// parameters to include resources in the bundle that the returned resources refer to.</remarks>
        public Bundle Search(Uri endpoint, SearchParam[] criteria = null, string sort = null, string[] includes = null, int? count = null)
        {
            if (endpoint == null) throw new ArgumentNullException("endpoint");

            //var rl = new ResourceLocation(endpoint);
            var rest = this._endpoint.AsRestUrl();

            // Since there is confusion between using /resource/?param, /resource?param, use
            // the /resource/search?param instead
            /*
            if(rl.Collection != null)
                rl.Operation = FhirRestOperation.OPERATION_SEARCH;
            */

            if( count.HasValue )
                rest.Parameters.Add(HttpUtil.SEARCH_PARAM_COUNT, count.Value.ToString());

            if (sort != null)
                rest.Parameters.Add(HttpUtil.SEARCH_PARAM_SORT, sort);

            if (criteria != null)
            {
                foreach (var criterium in criteria)
                    rest.Parameters.Add(criterium.QueryKey, criterium.QueryValue);
            }

            if (includes != null)
            {
                foreach (string includeParam in includes)
                    rest.Parameters.Add(HttpUtil.SEARCH_PARAM_INCLUDE, includeParam);
            }

            return FetchBundle(rest.Uri);
        }

        public Bundle Search(Uri endpoint, string name, string value, string sort=null, string[] includes = null, int? count = null)
        {
            return Search(endpoint, new SearchParam[] { new SearchParam(name, value) }, sort, includes, count);
        }

        public Bundle Search(SearchParam[] criteria = null, string sort=null, string[] includes = null, int? count = null)
        {
            if (Endpoint == null) throw new InvalidOperationException("Endpoint must be provided using either the Endpoint property or the FhirClient constructor");

            return Search(Endpoint, criteria, sort, includes, count);
        }

        public Bundle Search(string name, string value, string sort=null, string[] includes = null, int? count = null)
        {
            return Search(new SearchParam[] { new SearchParam(name, value) }, sort, includes, count);
        }

        public Bundle Search(ResourceType resource, SearchParam[] criteria = null, string sort=null, string[] includes = null, int? count = null)
        {
            if (Endpoint == null) throw new InvalidOperationException("Endpoint must be provided using either the Endpoint property or the FhirClient constructor");

            //var collection = resource.ToString().ToLower();
            var rest = _endpoint.Collection(resource);

            return Search(rest.Uri, criteria, sort, includes, count);
        }

        public Bundle Search(ResourceType resource, string name, string value, string sort=null, string[] includes = null, int? count = null)
        {
            return Search(resource, new SearchParam[] { new SearchParam(name, value) }, sort, includes, count);
        }
  

        /// <summary>
        /// Search for resources based on a resource's id.
        /// </summary>
        /// <param name="id">The id of the resource to search for</param>
        /// <param name="includes">Zero or more include paths</param>
        /// <typeparam name="TResource">The type of resource to search for</typeparam>
        /// <returns>A Bundle with the BundleEntry as identified by the id parameter or an empty
        /// Bundle if the resource wasn't found.</returns>
        /// <remarks>This operation is similar to Read, but additionally,
        /// it is possible to specify include parameters to include resources in the bundle that the
        /// returned resource refers to.</remarks>
        public Bundle SearchById(ResourceType resource, string id, string[] includes=null, int? count=null)
        {
            return Search(resource, HttpUtil.SEARCH_PARAM_ID, id, null, includes, count);
        }


        /// <summary>
        /// Uses the FHIR paging mechanism to go navigate around a series of paged result Bundles
        /// </summary>
        /// <param name="current">The bundle as received from the last response</param>
        /// <param name="direction">Optional. Direction to browse to, default is the next page of results.</param>
        /// <returns>A bundle containing a new page of results based on the browse direction, or null if
        /// the server did not have more results in that direction.</returns>
        public Bundle Continue(Bundle current, PageDirection direction = PageDirection.Next)
        {
            if (current.Links == null) return null;

            Uri continueAt = null;

            switch (direction)
            {
                case PageDirection.First:
                    continueAt = current.Links.FirstLink; break;
                case PageDirection.Previous:
                    continueAt = current.Links.PreviousLink; break;
                case PageDirection.Next:
                    continueAt = current.Links.NextLink; break;
                case PageDirection.Last:
                    continueAt = current.Links.LastLink; break;
            }

            if (continueAt != null)
                return FetchBundle(continueAt);
            else
                return null;
        }


        /// <summary>
        /// Send a batched update to the server
        /// </summary>
        /// <param name="batch">The contents of the batch to be sent</param>
        /// <returns>A bundle as returned by the server after it has processed the updates in the batch, or null
        /// if an error occurred.</returns>
        public Bundle Batch(Bundle batch)
        {
            if (Endpoint == null) throw new InvalidOperationException("Endpoint must be provided using either the Endpoint property or the FhirClient constructor");
            if (batch == null) throw new ArgumentNullException("batch");

            byte[] data;
            string contentType = ContentType.BuildContentType(PreferredFormat, false);

            if (PreferredFormat == ResourceFormat.Json)
                data = FhirSerializer.SerializeBundleToJsonBytes(batch);
            else if (PreferredFormat == ResourceFormat.Xml)
                data = FhirSerializer.SerializeBundleToXmlBytes(batch);
            else
                throw new ArgumentException("Cannot encode a batch into format " + PreferredFormat.ToString());

            var req = createRequest(Endpoint, true);
            req.Method = "POST";
            req.ContentType = contentType;
            prepareRequest(req, data);

            return doRequest(req, HttpStatusCode.OK, () => bundleFromResponse());
        }


        /// <summary>
        /// Send a Bundle to a path on the server
        /// </summary>
        /// <param name="bundle">The contents of the Bundle to be sent</param>
        /// <param name="path">A path on the server to send the Bundle to</param>
        /// <returns>True if the bundle was successfully delivered, false otherwise</returns>
        /// <remarks>This method differs from Batch, in that it can be used to deliver a Bundle
        /// at the endpoint for messages, documents or binaries, instead of the batched update
        /// REST endpoint.</remarks>
        public bool DeliverBundle(Bundle bundle, string path)
        {
            if (Endpoint == null) throw new InvalidOperationException("Endpoint must be provided using either the Endpoint property or the FhirClient constructor");

            byte[] data;
            string contentType = ContentType.BuildContentType(PreferredFormat, false);

            if (PreferredFormat == ResourceFormat.Json)
                data = FhirSerializer.SerializeBundleToJsonBytes(bundle);
            else if (PreferredFormat == ResourceFormat.Xml)
                data = FhirSerializer.SerializeBundleToXmlBytes(bundle);
            else
                throw new ArgumentException("Cannot encode a batch into format " + PreferredFormat.ToString());

            var req = createRequest(Endpoint, true);
            req.Method = "POST";
            req.ContentType = contentType;
            prepareRequest(req, data);

            return doRequest(req, HttpStatusCode.OK, () => true);
        }




        public IEnumerable<Tag> GetTags()
        {
            if (_endpoint == null) throw new InvalidOperationException("Endpoint must be provided using either the Endpoint property or the FhirClient constructor");

            var rl = _endpoint.Tags();

            var req = createRequest(rl.Uri, true);

            return (doRequest(req, HttpStatusCode.OK, () => tagListFromResponse())).Category;
        }


        public IEnumerable<Tag> GetTags(ResourceType type, string id=null, string version=null)
        {
            if (Endpoint == null) throw new InvalidOperationException("Endpoint must be provided using either the Endpoint property or the FhirClient constructor");
            if (version != null && id == null) throw new ArgumentException("Must specify an id if you specify a version");

            
            //var rl = new ResourceLocation(Endpoint);
            //rl.Operation = FhirRestOperation.OPERATION_TAGS;

            RestUrl api;
            string collection = type.GetCollectionName();
            if (id == null)
                api = _endpoint.CollectionTags(collection);
            else 
                api = _endpoint.ResourceTags(collection, id,version);
            
            //rl.Id = id;
            //rl.VersionId = version;

            var req = createRequest(api.Uri, true);
            return (doRequest(req, HttpStatusCode.OK, () => tagListFromResponse())).Category;
        }


        public IEnumerable<Tag> AffixTags(IEnumerable<Tag> tags, ResourceType type, string id, string version=null)
        {
            if (Endpoint == null) throw new InvalidOperationException("Endpoint must be provided using either the Endpoint property or the FhirClient constructor");
            if (id == null) throw new ArgumentNullException("id");
            if (tags == null) throw new ArgumentNullException("tags");

            RestUrl rl = _endpoint.ResourceTags(type, id, version);

            var data = HttpUtil.TagListBody(new TagList(tags), PreferredFormat);
            
            var req = createRequest(rl.Uri, true);
            req.Method = "POST";
            req.ContentType = ContentType.BuildContentType(PreferredFormat, false);
            prepareRequest(req, data);

            return (doRequest(req, HttpStatusCode.OK, () => tagListFromResponse())).Category;
        }


        public void DeleteTags(IEnumerable<Tag> tags, ResourceType type, string id, string version = null)
        {
            if (Endpoint == null) throw new InvalidOperationException("Endpoint must be provided using either the Endpoint property or the FhirClient constructor");
            if (id == null) throw new ArgumentNullException("id");
            if (tags == null) throw new ArgumentNullException("tags");

            var rl = _endpoint.ResourceTags(type, id, version);
            var data = HttpUtil.TagListBody(new TagList(tags), PreferredFormat);
            var req = createRequest(rl.Uri, true);
            req.Method = "DELETE";
            req.ContentType = ContentType.BuildContentType(PreferredFormat, false);
            prepareRequest(req, data);

            doRequest(req, HttpStatusCode.OK, () => true);
        }

        public ResourceFormat PreferredFormat { get; set; }
        public bool UseFormatParam { get; set; }
        

        private HttpWebRequest createRequest(Uri location, bool forBundle)
        {
            //Uri endpoint = location;
            _endpoint = new Endpoint(location);
            RestUrl api = _endpoint.AsRestUrl();

            if (UseFormatParam)
            {
                api.Parameters.Add(HttpUtil.RESTPARAM_FORMAT, ContentType.BuildFormatParam(PreferredFormat));
            }
            Uri uri = api.Uri;


            var req = (HttpWebRequest)HttpWebRequest.Create(uri);
            var agent =  "FhirClient for FHIR " + Model.ModelInfo.Version;
            req.Method = "GET";
            req.Headers[HttpRequestHeader.UserAgent] = agent;

            if (!UseFormatParam)
                req.Accept = ContentType.BuildContentType(PreferredFormat, forBundle);

            return req;
        }


        public ResponseDetails LastResponseDetails { get; private set; }


        private TagList tagListFromResponse()
        {
            return HttpUtil.TagListResponse(LastResponseDetails.BodyAsString(), LastResponseDetails.ContentType);
        }

        private ResourceEntry<T> resourceEntryFromResponse<T>() where T : Resource, new()
        {
            string resourceText = null;
            byte[] data = null;

            if (typeof(T).IsAssignableFrom(typeof(Binary)))
                data = LastResponseDetails.Body;
            else
                resourceText = LastResponseDetails.BodyAsString();

            // Initialize a resource entry from the received data. Note: Location overrides ContentLocation
            ResourceEntry result = HttpUtil.SingleResourceResponse(resourceText, data,
                    LastResponseDetails.ContentType, LastResponseDetails.ResponseUri.ToString(),
                    LastResponseDetails.Location ?? LastResponseDetails.ContentLocation,
                    LastResponseDetails.Category, LastResponseDetails.LastModified);

            if (result.Resource is T)
                return (ResourceEntry<T>)result;
            else
                throw new FhirOperationException(
                    String.Format("Received a resource of type {0}, expected a {1} resource",
                                    result.Resource.GetType().Name, typeof(T).Name));
        }


        private Bundle bundleFromResponse()
        {
            return HttpUtil.BundleResponse(LastResponseDetails.BodyAsString(), LastResponseDetails.ContentType);
        }


        private T doRequest<T>(HttpWebRequest req, HttpStatusCode success, Func<T> onSuccess)
        {
            return doRequest(req, new HttpStatusCode[] { success }, onSuccess);
        }

        private T doRequest<T>(HttpWebRequest req, HttpStatusCode[] success, Func<T> onSuccess)
        {
            HttpWebResponse response = (HttpWebResponse)req.GetResponseNoEx();

            LastResponseDetails = ResponseDetails.FromHttpWebResponse(response);

            if ( success.Contains(LastResponseDetails.Result) ) 
                return onSuccess();
            else
            {
                // Try to parse the body as an OperationOutcome resource, but it is no
                // problem if it's something else, or there is no parseable body at all

                ResourceEntry<OperationOutcome> outcome = null;

                try
                {
                    outcome = resourceEntryFromResponse<OperationOutcome>();
                }
                catch
                {
                    // failed, too bad.
                }

                if (outcome != null)
                    throw new FhirOperationException("Operation failed with status code " + LastResponseDetails.Result, outcome.Resource);                        
                else
                    throw new FhirOperationException("Operation failed with status code " + LastResponseDetails.Result);
            }
        }


        private void prepareRequest(HttpWebRequest request, byte[] data, IEnumerable<Tag> tags=null)
        {
            if (tags != null)
                request.Headers[HttpUtil.CATEGORY] = HttpUtil.BuildCategoryHeader(tags);

            Stream outs = getRequestStream(request);

            outs.Write(data, 0, (int)data.Length);
            outs.Flush();
        }

       
        private Stream getRequestStream(HttpWebRequest request)
        {         
            Stream requestStream = null;
            AsyncCallback callBack = new AsyncCallback( ar => 
                    { 
                        var req = (HttpWebRequest)ar.AsyncState;
                        requestStream = req.EndGetRequestStream(ar);
                    });

            var async = request.BeginGetRequestStream(callBack, request);

            async.AsyncWaitHandle.WaitOne();

            return requestStream;
        }
    }

    public enum PageDirection
    {
        First,
        Previous,
        Next,
        Last
    }


}
