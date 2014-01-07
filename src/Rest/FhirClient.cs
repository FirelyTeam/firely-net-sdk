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
using System.Threading.Tasks;
using System.Threading;



namespace Hl7.Fhir.Rest
{
    public class FhirClient
    {
        private Endpoint _endpoint;

        /// <summary>
        /// Creates a new client using a default endpoint
        /// </summary>
        public FhirClient(Uri endpoint)
        {
            if (endpoint == null) throw new ArgumentNullException("endpoint");
            _endpoint = new Endpoint(endpoint);
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
        }


        /// <summary>
        /// Fetches a bundle from a FHIR resource endpoint. 
        /// </summary>
        /// <param name="location">The url of the endpoint which returns a Bundle</param>
        /// <returns>The Bundle as received by performing a GET on the endpoint. This operation will throw an exception
        /// if the operation does not result in a HttpStatus OK.</returns>
        internal Bundle FetchBundle(Uri location)
        {
            assertEndpoint();
            assertServiceLocation(location, "location");

            var req = prepareRequest("GET", location, null, null, expectBundleResponse: true);
            return doRequest(req, HttpStatusCode.OK, () => bundleFromResponse());
        }


        /// <summary>
        /// Get a conformance statement for the system
        /// </summary>
        /// <param name="useOptionsVerb">If true, uses the Http OPTIONS verb to get the conformance, otherwise uses the /metadata endpoint</param>
        /// <returns>A Conformance resource. Throws an exception if the operation failed.</returns>
        public ResourceEntry<Conformance> Conformance(bool useOptionsVerb = false)
        {
            assertEndpoint();

            return Conformance(Endpoint, useOptionsVerb);
        }


        /// <summary>
        /// Get a conformance statement for the system
        /// </summary>
        /// <param name="useOptionsVerb">If true, uses the Http OPTIONS verb to get the conformance, otherwise uses the /metadata endpoint</param>
        /// <returns>A Conformance resource. Throws an exception if the operation failed.</returns>
        public ResourceEntry<Conformance> Conformance(Uri location, bool useOptionsVerb = false)
        {
            assertEndpoint();
            assertServiceLocation(location, "location");

            RestUrl url = useOptionsVerb ? _endpoint.AsRestUrl() : new Endpoint(location).WithMetadata();

            var req = prepareRequest(useOptionsVerb ? "OPTIONS" : "GET", url.Uri, null, null, expectBundleResponse:false);
            return doRequest(req, HttpStatusCode.OK, () => resourceEntryFromResponse<Conformance>());
        }


        public ResourceEntry<TResource> Read<TResource>(string id, string versionId=null) where TResource : Resource, new()
        {
            assertEndpoint();
            if (id == null) throw new ArgumentNullException("id");

            ResourceIdentity ri = buildResourceIdentityUrl(typeof(TResource).GetCollectionName(), id, versionId);
            return Read<TResource>(ri);
        }

        /// <summary>
        /// Fetches a typed resource from a FHIR resource endpoint.
        /// </summary>
        /// <param name="endpoint">The url of the Resource to fetch. This can be a Resource id url or a version-specific
        /// Resource url.</param>
        /// <typeparam name="TResource">The type of resource to fetch</typeparam>
        /// <returns>The requested resource as a ResourceEntry&lt;T&gt;. This operation will throw an exception
        /// if the resource has been deleted or does not exist</returns>
        public ResourceEntry<TResource> Read<TResource>(Uri location) where TResource : Resource, new()
        {
            if (location == null) throw new ArgumentNullException("location");
            assertEndpoint();
            assertServiceLocation(location, "location");

            var req = prepareRequest("GET", location, null, null, expectBundleResponse: false);
            return doRequest(req, HttpStatusCode.OK, () => resourceEntryFromResponse<TResource>());
        }

        private void assertEndpoint()
        {
            if (_endpoint == null) throw new InvalidOperationException("No service base url was provided when constructing the FhirClient");
        }
      
        private void assertServiceLocation(Uri location, string name)
        {
            if (location == null) return;

            if (!_endpoint.IsEndpointFor(location)) throw Error.Argument("Url in {0} is not located on this FhirClient's endpoint", name);
        }

        private ResourceIdentity buildResourceIdentityUrl(string collection, string id, string versionId=null)
        {
            ResourceIdentity ri;

            if (versionId != null)
                ri = ResourceIdentity.Build(Endpoint, collection, id, versionId);
            else
                ri = ResourceIdentity.Build(Endpoint, collection, id);
            return ri;
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
            if (entry == null) throw Error.ArgumentNull("entry");
            if (entry.Resource == null) throw Error.Argument("entry","Entry does not contain a Resource to update");
            if (entry.Id == null) throw Error.Argument("entry","Entry needs a non-null entry.id to send the update to");

            assertEndpoint();
            assertServiceLocation(entry.SelfLink, "entry.SelfLink");
            if (versionAware && entry.SelfLink == null) throw Error.Argument("entry", "When requesting version-aware updates, Entry.SelfLink may not be null.");

            var rId = new ResourceIdentity(entry.Id);
            var rVersionId = entry.SelfLink != null ? new ResourceIdentity(entry.SelfLink) : null;

            return Update<TResource>(entry.Resource,rId.Id,entry.Tags,versionAware ? rVersionId.Id : null);
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
        public ResourceEntry<TResource> Update<TResource>(TResource resource, string id, IEnumerable<Tag> tags, string versionId = null)
                        where TResource : Resource, new()
        {
            if (resource == null) throw Error.ArgumentNull("resource");
            assertEndpoint();

            var rId = buildResourceIdentityUrl(typeof(TResource).GetCollectionName(), id);

            var req = prepareRequest("PUT",rId,resource, tags, expectBundleResponse: false);

            // If a version id is given, post the data to a version-specific url
            if (versionId != null) req.Headers[HttpRequestHeader.ContentLocation] = rId.WithVersion(versionId).ToString();

            return doRequest(req, new HttpStatusCode[] { HttpStatusCode.Created, HttpStatusCode.OK },
                () => makeEntryFromHeaders(resource));
        }


        private HttpWebRequest prepareRequest(string method, Uri endpoint, object data, IEnumerable<Tag> tags, bool expectBundleResponse) 
        {
            byte[] body = null;

            RestUrl api = new RestUrl(endpoint);

            if (UseFormatParam)
            {
                api.AddParam(HttpUtil.RESTPARAM_FORMAT, ContentType.BuildFormatParam(PreferredFormat));
            }

            var req = initializeRequest(api.Uri, method);

            if (!UseFormatParam)
                req.Accept = ContentType.BuildContentType(PreferredFormat, forBundle: expectBundleResponse);

            if (data is Binary)
            {
                var bin = (Binary)data;
                body = bin.Content;
                req.ContentType = bin.ContentType;
            }
            else if(data is Resource) 
            {
                body = PreferredFormat == ResourceFormat.Xml ?
                    FhirSerializer.SerializeResourceToXmlBytes((Resource)data) :
                    FhirSerializer.SerializeResourceToJsonBytes((Resource)data);

                req.ContentType = ContentType.BuildContentType(PreferredFormat, false);
            }
            else if (data is Bundle)
            {
                body = PreferredFormat == ResourceFormat.Xml ?
                    FhirSerializer.SerializeBundleToXmlBytes((Bundle)data) :
                    FhirSerializer.SerializeBundleToJsonBytes((Bundle)data);

                req.ContentType = ContentType.BuildContentType(PreferredFormat, true);
            }
            else if(data is TagList)
            {
                body = PreferredFormat == ResourceFormat.Xml ?
                    FhirSerializer.SerializeTagListToXmlBytes((TagList)data) :
                    FhirSerializer.SerializeTagListToJsonBytes((TagList)data);
        
                req.ContentType = ContentType.BuildContentType(PreferredFormat, false);
            }

            if (tags != null) req.Headers[HttpUtil.CATEGORY] = HttpUtil.BuildCategoryHeader(tags);

            if(body != null) writeBody(req, body);
            return req;
        }

        private HttpWebRequest initializeRequest(Uri location, string method)
        {
            var req = (HttpWebRequest)HttpWebRequest.Create(location);
            var agent = ".NET FhirClient for FHIR " + Model.ModelInfo.Version;
            req.Method = method;

            try
            {
                System.Reflection.PropertyInfo prop = req.GetType().GetProperty("UserAgent");

                if (prop != null) prop.SetValue(req, agent, null);
            }
            catch (Exception) 
            { 
                // platform does not support UserAgent property...too bad
            }

            return req;
        }

        private void writeBody(HttpWebRequest request, byte[] data)
        {
            Stream outs = getRequestStream(request);

            outs.Write(data, 0, (int)data.Length);
            outs.Flush();
        }


        private Stream getRequestStream(HttpWebRequest request)
        {
            Stream requestStream = null;
            ManualResetEvent getRequestFinished = new ManualResetEvent(false);

            AsyncCallback callBack = new AsyncCallback(ar =>
            {
                var req = (HttpWebRequest)ar.AsyncState;
                requestStream = req.EndGetRequestStream(ar);
                getRequestFinished.Set();
            });

            var async = request.BeginGetRequestStream(callBack, request);

            getRequestFinished.WaitOne();
            //async.AsyncWaitHandle.WaitOne();

            return requestStream;
        }

 
        /// <summary>
        /// Delete a resource at the given endpoint
        /// </summary>
        /// <param name="id">endpoint of the resource to delete</param>
        /// <typeparam name="TResource">The type of the resource to delete</typeparam>
        /// <returns>Returns normally if delete succeeded, throws an exception otherwise, though this might
        /// just mean the server returned 404 (the resource didn't exist before) or 410 (the resource was
        /// already deleted).</returns>
        public void Delete<TResource>(string id) where TResource : Resource, new()
        {
            if (id == null) throw new ArgumentNullException("id");
            assertEndpoint();

            var url = buildResourceIdentityUrl(typeof(TResource).GetCollectionName(),id);

            var req = prepareRequest("DELETE", url, null, null, expectBundleResponse: false);
            doRequest(req, HttpStatusCode.NoContent, () => true);
        }

        public void Delete(ResourceEntry entry)
        {
            if (entry == null) throw Error.ArgumentNull("entry");
            if (entry.Id == null) throw Error.Argument("entry", "Entry must have an id");

            ResourceIdentity ri = new ResourceIdentity(entry.Id);

            var url = buildResourceIdentityUrl(ri.Collection, ri.Id);

            var req = prepareRequest("DELETE", url, null, null, expectBundleResponse: false);
            doRequest(req, HttpStatusCode.NoContent, () => true);
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
        public ResourceEntry<TResource> Create<TResource>(TResource resource, string id=null, IEnumerable<Tag> tags = null) where TResource : Resource, new()
        {
            if (resource == null) throw new ArgumentNullException("resource");
            assertEndpoint();
             
            var collection = typeof(TResource).GetCollectionName();

            if (id == null)
            {
                // A normal create
                var rl = _endpoint.ForCollection(collection);
                var req = prepareRequest("POST", rl.Uri, resource, tags, expectBundleResponse: false);
                return doRequest(req, HttpStatusCode.Created, () => makeEntryFromHeaders(resource));
            }
            else
            {
                // Given an id, this create turns into an update at a specific resource location
                return Update<TResource>(resource, id, tags);
            }
        }


        /// <summary>
        /// Retrieve the version history for a specific resource instance
        /// </summary>
        /// <param name="id">The id of the resource to get the history for</param>
        /// <param name="since">Optional. Returns only changes after the given date</param>
        /// <param name="count">Optional. Asks server to limit the number of entries returned</param>
        /// <returns>A bundle with the history for the indicated instance, may contain both 
        /// ResourceEntries and DeletedEntries.</returns>
	    public Bundle History<TResource>(string id=null, DateTimeOffset? since = null, int? count = null) where TResource : Resource, new()
        {
            assertEndpoint();
            
            var collection = typeof(TResource).GetCollectionName();

            var url = id==null ?
                _endpoint.CollectionHistory(collection) :
                _endpoint.ResourceHistory(collection, id);

            if (since != null) url.AddParam(HttpUtil.HISTORY_PARAM_SINCE, PrimitiveTypeConverter.Convert<string>(since.Value));
            if (count != null) url.AddParam(HttpUtil.HISTORY_PARAM_COUNT, count.ToString());

            return FetchBundle(url.Uri);           
        }


        /// <summary>
        /// Retrieve the version history of any resource on the server
        /// </summary>
        /// <param name="since">Optional. Returns only changes after the given date</param>
        /// <param name="count">Optional. Asks server to limit the number of entries returned</param>
        /// <returns>A bundle with the history for the indicated instance, may contain both 
        /// ResourceEntries and DeletedEntries.</returns>
        public Bundle WholeSystemHistory(DateTimeOffset? since = null, int? count = null)
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
        public OperationOutcome ValidateUpdate<TResource>(ResourceEntry<TResource> entry) where TResource : Resource, new()
        {
            if (entry == null) throw new ArgumentNullException("entry");
            if (entry.Resource == null) throw new ArgumentException("Entry does not contain a Resource to validate", "entry");
            if (entry.Id == null) throw new ArgumentException("Entry needs a non-null entry.id to use for validation", "entry");

            var id = new ResourceIdentity(entry.Id);
            var url = _endpoint.Validate(id.Collection, id.Id);
            return doValidate(url, entry.Resource, entry.Tags);
        }


        private OperationOutcome doValidate(RestUrl url, object data, IEnumerable<Tag> tags)
        {
            var req = prepareRequest("POST", url.Uri, data, tags, expectBundleResponse: false);

            try
            {
                doRequest(req, HttpStatusCode.OK, () => true);
                return null;
            }
            catch (FhirOperationException foe)
            {
                if (foe.Outcome != null)
                    return foe.Outcome;
                else
                    throw foe;
            }
        }

        public OperationOutcome ValidateCreate<TResource>(TResource resource, IEnumerable<Tag> tags) where TResource : Resource, new()
        {
            if (resource == null) throw new ArgumentNullException("resource");

            var collection = typeof(Resource).GetCollectionName();
            var url = _endpoint.Validate(collection);
            return doValidate(url, resource, tags);
        }


               
        
        private Bundle doSearch(string collection=null, SearchParam[] criteria = null, string sort = null, string[] includes = null, int? count = null)
        {
            assertEndpoint();

            RestUrl url = null;

            if (collection != null)
                // Since there is confusion between using /resource/?param, /resource?param, use
                // the /resource/search?param instead
                url = _endpoint.Search(collection);
            else
                url = _endpoint.AsRestUrl();

            if( count.HasValue )
                url.AddParam(HttpUtil.SEARCH_PARAM_COUNT, count.Value.ToString());

            if (sort != null)
                url.AddParam(HttpUtil.SEARCH_PARAM_SORT, sort);

            if (criteria != null)
            {
                foreach (var criterium in criteria)
                    url.AddParam(criterium.QueryKey, criterium.QueryValue);
            }

            if (includes != null)
            {
                foreach (string includeParam in includes)
                    url.AddParam(HttpUtil.SEARCH_PARAM_INCLUDE, includeParam);
            }

            return FetchBundle(url.Uri);
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
        public Bundle Search<TResource>(SearchParam[] criteria = null, string sort=null, string[] includes = null, int? count = null) where TResource : Resource, new()
        {                   
            return Search(typeof(TResource).GetCollectionName(), criteria, sort, includes,count);
        }

        public Bundle Search(string resource, SearchParam[] criteria = null, string sort = null, string[] includes = null, int? count = null)
        {
            return doSearch(resource, criteria, sort, includes, count);
        }

        public Bundle WholeSystemSearch(SearchParam[] criteria = null, string sort=null, string[] includes = null, int? count = null)
        {
            return doSearch(null, criteria,sort,includes,count);
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
        public Bundle SearchById<TResource>(string id, string sort=null, string[] includes=null, int? count=null) where TResource : Resource, new()
        {
            return SearchById(typeof(TResource).GetCollectionName(), id, sort, includes, count);
        }

        public Bundle SearchById(string resource, string id, string sort = null, string[] includes = null, int? count = null)
        {
            return doSearch(resource, new SearchParam[] { new SearchParam(HttpUtil.SEARCH_PARAM_ID, id) }, sort, includes, count);
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
        /// Send a batched of creates, updates and deletes to the server
        /// </summary>
        /// <param name="batch">The contents of the batch to be sent</param>
        /// <returns>A bundle as returned by the server after it has processed the updates in the batch, or null
        /// if an error occurred.</returns>
        public Bundle Transaction(Bundle bundle)
        {
            if (bundle == null) throw new ArgumentNullException("bundle");
            assertEndpoint();

            var url = _endpoint.AsRestUrl();

            var req = prepareRequest("POST", url.Uri, bundle, null, expectBundleResponse: true);
            return doRequest(req, HttpStatusCode.OK, () => bundleFromResponse());
        }


        public void DeliverToDocument(Bundle bundle)
        {
            if (bundle == null) throw Error.ArgumentNull("bundle");
            assertEndpoint();

            var url = _endpoint.AsRestUrl().AddPath("Document");

            if (bundle.GetBundleType() == BundleType.Document)
            {
                // Documents are merely "accepted"
                var req = prepareRequest("POST", url.Uri, bundle, null, expectBundleResponse: false);
                doRequest(req, HttpStatusCode.NoContent, () => true );
            }         
            else
            {
                throw Error.Argument("bundle", "The bundle passed to the Document endpoint needs to be a document (use SetBundleType to do so)");
            }            
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
        public Bundle DeliverToMailbox(Bundle bundle)
        {
            if (bundle == null) throw Error.ArgumentNull("bundle");
            assertEndpoint();

            var url = _endpoint.AsRestUrl().AddPath("Mailbox");

            if (bundle.GetBundleType() == BundleType.Document)
            {
                // Documents are merely "accepted"
                var req = prepareRequest("POST", url.Uri, bundle, null, expectBundleResponse: false);
                return doRequest(req, HttpStatusCode.NoContent, () => (Bundle)null);
            }
            else if (bundle.GetBundleType() == BundleType.Message)
            {
                // Messages, including Queries, expect a return message
                var req = prepareRequest("POST", url.Uri, bundle, null, expectBundleResponse: true);
                return doRequest(req, HttpStatusCode.OK, () => bundleFromResponse());
            }
            else
            {
                throw Error.Argument("bundle", "The bundle passed to the Mailbox endpoint needs to be a document or message (use SetBundleType to do so)");
            }            
        }


        public IEnumerable<Tag> GetTags()
        {
            assertEndpoint();

            var rl = _endpoint.Tags();

            var req = prepareRequest("GET", rl.Uri, null, null, expectBundleResponse: false);
            var result = doRequest(req, HttpStatusCode.OK, () => tagListFromResponse());

            return result.Category;
        }


        public IEnumerable<Tag> GetTags<TResource>(string id = null, string version = null) where TResource : Resource, new()
        {
            assertEndpoint();
            if (version != null && id == null) throw new ArgumentException("Must specify an id if you specify a version");

            RestUrl api;
            string collection = typeof(TResource).GetCollectionName();
            if (id == null)
                api = _endpoint.CollectionTags(collection);
            else
                api = _endpoint.ResourceTags(collection, id, version);

            var req = prepareRequest("GET", api.Uri, null, null, expectBundleResponse: false);
            var result = doRequest(req, HttpStatusCode.OK, () => tagListFromResponse());
            return result.Category;
        }


        public IEnumerable<Tag> AffixTags<TResource>(IEnumerable<Tag> tags, string id, string version=null) where TResource : Resource, new()
        {
            if (id == null) throw new ArgumentNullException("id");
            if (tags == null) throw new ArgumentNullException("tags");
            assertEndpoint();

            var collection = typeof(TResource).GetCollectionName();
            var rl = _endpoint.ResourceTags(collection, id, version);

            var req = prepareRequest("POST", rl.Uri, new TagList(tags), null, expectBundleResponse: false); 
            var result = doRequest(req, HttpStatusCode.OK, () => tagListFromResponse());
            return result.Category;
        }


        public void DeleteTags<TResource>(IEnumerable<Tag> tags, string id, string version = null) where TResource : Resource, new()
        {
            if (id == null) throw new ArgumentNullException("id");
            if (tags == null) throw new ArgumentNullException("tags");
            assertEndpoint();

            var collection = typeof(TResource).GetCollectionName();
            var rl = _endpoint.ResourceTags(collection, id, version);

            var req = prepareRequest("DELETE", rl.Uri, new TagList(tags), null, expectBundleResponse: false);
            doRequest(req, HttpStatusCode.OK, () => true);
        }

        public ResourceFormat PreferredFormat { get; set; }
        public bool UseFormatParam { get; set; }
        
   
        public ResponseDetails LastResponseDetails { get; private set; }


        private TagList tagListFromResponse()
        {
            return HttpUtil.TagListResponse(LastResponseDetails.BodyAsString(), LastResponseDetails.ContentType);
        }

        private ResourceEntry<T> resourceEntryFromResponse<T>() where T : Resource, new()
        {
            var result = resourceEntryFromResponse(typeof(T).GetCollectionName());

            if (result.Resource is T)
                return (ResourceEntry<T>)result;
            else
                throw new FhirOperationException(
                    String.Format("Received a resource of type {0}, expected a {1} resource",
                                    result.Resource.GetType().Name, typeof(T).Name));
        }

        private ResourceEntry resourceEntryFromResponse(string collection)
        {
            
            object data = null;

            if (collection == "Binary")
                data = LastResponseDetails.Body;
            else
                data = LastResponseDetails.BodyAsString();

            // Initialize a resource entry from the received data. Note: Location overrides ContentLocation
            ResourceEntry result = HttpUtil.CreateResourceEntry(data,
                    LastResponseDetails.ContentType, LastResponseDetails.ResponseUri.ToString(),
                    LastResponseDetails.Location ?? LastResponseDetails.ContentLocation,
                    LastResponseDetails.Category, LastResponseDetails.LastModified);

            return result;
        }

        private ResourceEntry<T> makeEntryFromHeaders<T>(T resource) where T:Resource, new()
        {
            return (ResourceEntry<T>)HttpUtil.CreateResourceEntry(resource,
                    LastResponseDetails.ResponseUri.ToString(),
                    LastResponseDetails.Location ?? LastResponseDetails.ContentLocation,
                    LastResponseDetails.Category, LastResponseDetails.LastModified);
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
           // var getResponseTask = Task.Factory.FromAsync<WebResponse>(req.BeginGetResponse,
          //      req.EndGetResponseNoEx, null); 

        //   HttpWebResponse response = (HttpWebResponse)(getResponseTask.ConfigureAwait(false).GetAwaiter().GetResult());

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
                    // failed, too bad, outcome will be null
                }

                if (outcome != null)
                    throw new FhirOperationException("Operation failed with status code " + LastResponseDetails.Result, outcome.Resource);                        
                else
                    throw new FhirOperationException("Operation failed with status code " + LastResponseDetails.Result);
            }
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
