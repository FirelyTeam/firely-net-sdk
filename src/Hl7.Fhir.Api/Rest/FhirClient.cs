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
using System.Threading;



namespace Hl7.Fhir.Rest
{
    public class FhirClient
    {
        private Uri _endpoint;

        /// <summary>
        /// Creates a new client using a default endpoint
        /// </summary>
        public FhirClient(Uri endpoint)
        {
            if (endpoint == null) throw new ArgumentNullException("endpoint");
            if (!endpoint.IsAbsoluteUri) throw new ArgumentException("endpoint", "Endpoint must be absolute");

            _endpoint = endpoint;
            PreferredFormat = ResourceFormat.Xml;
        }

        public FhirClient(string endpoint)
            : this(new Uri(endpoint))
        {
        }


        /// <summary>
        /// Contact the endpoint's Conformance statement to configure the client
        /// to  the capabilities of the server
        /// </summary>
        //public void Configure()
        //{
        //    // Set preferred serialization format
        //    throw new NotImplementedException();
        //}
        

        /// <summary>
        /// The default endpoint for use with operations that use discrete id/version parameters
        /// instead of explicit uri endpoints.
        /// </summary>
        public Uri Endpoint 
        {
            get
            {
                return _endpoint != null ? _endpoint : null; 
            }
        }


        private Uri getRequestLocation(Uri location=null)
        {
            // Verify whether the client is properly initalized,
            if (_endpoint == null) throw new InvalidOperationException("No service base url was provided when constructing the FhirClient");

            // If called without a location, just return the base endpoint
            if (location == null) return Endpoint;


            // If the location is absolute, verify whether it is within the endpoint
            if (location.IsAbsoluteUri)
            {
                if (!new RestUrl(_endpoint).IsEndpointFor(location)) 
                    throw Error.Argument("location", "Url is not located on this FhirClient's endpoint");
            }
            else
                // Else, make location absolute within the endpoint
                location = new Uri(Endpoint, location);

            return location;
        }



        /// <summary>
        /// Fetches a bundle from a FHIR resource endpoint. 
        /// </summary>
        /// <param name="location">The url of the endpoint which returns a Bundle</param>
        /// <returns>The Bundle as received by performing a GET on the endpoint. This operation will throw an exception
        /// if the operation does not result in a HttpStatus OK.</returns>
        internal Bundle FetchBundle(Uri location)
        {
            var req = new FhirRequest(getRequestLocation(location), "GET");
            return doRequest(req, HttpStatusCode.OK, resp => resp.BodyAsBundle()); 
        }


        /// <summary>
        /// Get a conformance statement for the system
        /// </summary>
        /// <param name="useOptionsVerb">If true, uses the Http OPTIONS verb to get the conformance, otherwise uses the /metadata endpoint</param>
        /// <returns>A Conformance resource. Throws an exception if the operation failed.</returns>
        public ResourceEntry<Conformance> Conformance(bool useOptionsVerb = false)
        {
            var location = getRequestLocation();

            RestUrl url = useOptionsVerb ? new RestUrl(location) : new RestUrl(location).WithMetadata();

            var req = new FhirRequest(url.Uri, useOptionsVerb ? "OPTIONS" : "GET");
            return doRequest(req, HttpStatusCode.OK, resp => resp.BodyAsEntry<Conformance>());
        }


        /// <summary>
        /// Create a resource on a FHIR endpoint
        /// </summary>
        /// <param name="resource">The resource instance to create</param>
        /// <param name="tags">Optional. List of Tags to add to the created instance.</param>
        /// <param name="refresh">Optional. When true, fetches the newly created resource from the server.</param>
        /// <returns>A ResourceEntry containing the metadata (id, selflink) associated with the resource as created on the server, or an exception if the create failed.</returns>
        /// <typeparam name="TResource">The type of resource to create</typeparam>
        /// <remarks>The Create operation normally does not return the posted resource, but just its metadata. Specifying
        /// refresh=true ensures the return value contains the Resource as stored by the server.
        /// </remarks>
        public ResourceEntry<TResource> Create<TResource>(TResource resource, IEnumerable<Tag> tags = null, bool refresh = false) where TResource : Resource, new()
        {
            return internalCreate<TResource>(resource, tags, null, refresh);
        }


        /// <summary>
        /// Create a resource with a given id on the FHIR endpoint
        /// </summary>
        /// <param name="resource">The resource instance to create</param>
        /// <param name="id">Optional. A client-assigned id for the newly created resource.</param>
        /// <param name="tags">Optional. List of Tags to add to the created instance.</param>
        /// <param name="refresh">Optional. When true, fetches the newly created resource from the server.</param>
        /// <returns>A ResourceEntry containing the metadata (id, selflink) associated with the resource as created on the server, or an exception if the create failed.</returns>
        /// <typeparam name="TResource">The type of resource to create</typeparam>
        /// <remarks>The Create operation normally does not return the posted resource, but just its metadata. Specifying
        /// refresh=true ensures the return value contains the Resource as stored by the server.
        /// </remarks>
        public ResourceEntry<TResource> Create<TResource>(TResource resource, string id, IEnumerable<Tag> tags = null, bool refresh = false) where TResource : Resource, new()
        {
            if (id == null) throw Error.ArgumentNull("id", "Must supply a client-assigned id");

            return internalCreate<TResource>(resource, tags, id, refresh);
        }


        private ResourceEntry<TResource> internalCreate<TResource>(TResource resource, IEnumerable<Tag> tags, string id, bool refresh) where TResource : Resource, new()
        {
            if (resource == null) throw Error.ArgumentNull("resource");
            assertEndpoint();

            var collection = typeof(TResource).GetCollectionName();

            if (id == null)
            {
                // A normal create
                var rl = new RestUrl(_endpoint).ForCollection(collection);
                var req = prepareRequest("POST", rl.Uri, resource, tags, expectBundleResponse: false);
                var entry = doRequest(req, HttpStatusCode.Created, () => makeEntryFromHeaders<TResource>());

                // If asked for it, immediately get the contents *we just posted*, so use the actually created version
                if (refresh) entry = Refresh(entry, versionSpecific: true);
                return entry;
            }
            else
            {
                // Given an id, this create turns into an update at a specific resource location
                return Update<TResource>(resource, id, tags, refresh: refresh);
            }
        }

        /// <summary>
        /// Refreshes the data and metadata for a given ResourceEntry.
        /// </summary>
        /// <param name="entry">The entry to refresh. It's id property will be used to fetch the latest version of the Resource.</param>
        /// <typeparam name="TResource">The type of resource to refresh</typeparam>
        /// <returns>A resource entry containing up-to-date data and metadata.</returns>
        public ResourceEntry<TResource> Refresh<TResource>(ResourceEntry<TResource> entry) where TResource : Resource, new()
        {
            return Refresh<TResource>(entry, false);                
        }


        internal ResourceEntry<TResource> Refresh<TResource>(ResourceEntry<TResource> entry, bool versionSpecific = false) where TResource : Resource, new()
        {
            if (entry == null) throw Error.ArgumentNull("entry");
            assertEndpoint();

            if (!versionSpecific)
                return Read<TResource>(entry.Id);
            else
                return Read<TResource>(entry.SelfLink);
        }


        /// <summary>
        /// Fetches a typed resource from a FHIR resource endpoint.
        /// </summary>
        /// <param name="endpoint">The url of the Resource to fetch. This can be a Resource id url or a version-specific
        /// Resource url.</param>
        /// <typeparam name="TResource">The type of resource to read</typeparam>
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


        public ResourceEntry Read(Uri location)
        {
            if (location == null) throw new ArgumentNullException("location");
            assertEndpoint();
            assertServiceLocation(location, "location");

            var req = prepareRequest("GET", location, null, null, expectBundleResponse: false);

            var rl = new ResourceIdentity(location);

            return doRequest(req, HttpStatusCode.OK, () => resourceEntryFromResponse(rl.Collection));
        }


        public ResourceEntry<TResource> Read<TResource>(string id, string versionId=null) where TResource : Resource, new()
        {
            assertEndpoint();
            if (id == null) throw new ArgumentNullException("id");

            var ri = ResourceIdentity.Build(typeof(TResource).GetCollectionName(), id, versionId);
            return Read<TResource>(ri);
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
        public ResourceEntry<TResource> Update<TResource>(ResourceEntry<TResource> entry, bool versionAware = false, bool refresh=false)
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

            return Update<TResource>(entry.Resource, rId.Id, entry.Tags, versionAware ? rVersionId.Id : null, refresh);
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
        public ResourceEntry<TResource> Update<TResource>(TResource resource, string id, IEnumerable<Tag> tags, string versionId = null, bool refresh = false)
                        where TResource : Resource, new()
        {
            if (resource == null) throw Error.ArgumentNull("resource");
            assertEndpoint();

            var rId = ResourceIdentity.Build(typeof(TResource).GetCollectionName(), id);

            var req = prepareRequest("PUT",rId,resource, tags, expectBundleResponse: false);

            // If a version id is given, post the data to a version-specific url
            if (versionId != null) req.Headers[HttpRequestHeader.ContentLocation] = rId.WithVersion(versionId).ToString();

            var entry = doRequest(req, new HttpStatusCode[] { HttpStatusCode.Created, HttpStatusCode.OK },
                () => makeEntryFromHeaders<TResource>());

            // If asked for it, immediately get the contents *we just posted*, so use the actually created version
            if (!refresh) entry = Refresh(entry, versionSpecific: true);

            return entry;
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

            var url = ResourceIdentity.Build(typeof(TResource).GetCollectionName(),id);

            var req = prepareRequest("DELETE", url, null, null, expectBundleResponse: false);
            doRequest(req, HttpStatusCode.NoContent, () => true);
        }

        public void Delete(ResourceEntry entry)
        {
            if (entry == null) throw Error.ArgumentNull("entry");
            if (entry.Id == null) throw Error.Argument("entry", "Entry must have an id");

            ResourceIdentity ri = new ResourceIdentity(entry.Id);

            var url = ResourceIdentity.Build(ri.Collection, ri.Id);

            var req = prepareRequest("DELETE", url, null, null, expectBundleResponse: false);
            doRequest(req, HttpStatusCode.NoContent, () => true);
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
                new RestUrl(_endpoint).CollectionHistory(collection) :
                new RestUrl(_endpoint).ResourceHistory(collection, id);

            if (since != null) url.AddParam(HttpUtil.HISTORY_PARAM_SINCE, PrimitiveTypeConverter.ConvertTo<string>(since.Value));
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

            var rl = new RestUrl(_endpoint).ServerHistory();

            if (since != null) rl.AddParam(HttpUtil.HISTORY_PARAM_SINCE, PrimitiveTypeConverter.ConvertTo<string>(since.Value));
            if (count != null) rl.AddParam(HttpUtil.HISTORY_PARAM_COUNT, count.ToString());

            return FetchBundle(rl.Uri);
        }


        /// <summary>
        /// Validates whether the contents of the resource would be acceptable as an update
        /// </summary>
        /// <param name="entry">The entry containing the updated Resource to validate</param>
        /// <param name="result">Contains the OperationOutcome detailing why validation failed, or null if validation succeeded</param>
        /// <returns>True when validation was successful, false otherwise. Note that this function may still throw exceptions if non-validation related
        /// failures occur.</returns>
        public bool TryValidateUpdate<TResource>(ResourceEntry<TResource> entry, out OperationOutcome result) where TResource : Resource, new()
        {
            if (entry == null) throw new ArgumentNullException("entry");
            if (entry.Resource == null) throw new ArgumentException("Entry does not contain a Resource to validate", "entry");
            if (entry.Id == null) throw new ArgumentException("Entry needs a non-null entry.id to use for validation", "entry");

            var id = new ResourceIdentity(entry.Id);
            var url = new RestUrl(_endpoint).Validate(id.Collection, id.Id);
            result = doValidate(url, entry.Resource, entry.Tags);

            return result == null || !result.Success();
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

        /// <summary>
        /// Validates whether the contents of the resource would be acceptable as a create
        /// </summary>
        /// <typeparam name="TResource"></typeparam>
        /// <param name="resource">The entry containing the Resource data to use for the validation</param>
        /// <param name="result">Contains the OperationOutcome detailing why validation failed, or null if validation succeeded</param>
        /// <param name="tags">Optional list of tags to attach to the resource</param>
        /// <returns>True when validation was successful, false otherwise. Note that this function may still throw exceptions if non-validation related
        /// failures occur.</returns>
        public bool TryValidateCreate<TResource>(TResource resource, out OperationOutcome result, IEnumerable<Tag> tags=null) where TResource : Resource, new()
        {
            if (resource == null) throw new ArgumentNullException("resource");

            var collection = typeof(Resource).GetCollectionName();
            var url = new RestUrl(_endpoint).Validate(collection);

            result = doValidate(url, resource, tags);
            return result == null || !result.Success();
        }


               
        
        private Bundle doSearch(string collection=null, SearchParam[] criteria = null, string sort = null, string[] includes = null, int? count = null)
        {
            assertEndpoint();

            RestUrl url = null;

            if (collection != null)
                // Since there is confusion between using /resource/?param, /resource?param, use
                // the /resource/search?param instead
                url = new RestUrl(_endpoint).Search(collection);
            else
                url = new RestUrl(_endpoint);

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

            var req = prepareRequest("POST", _endpoint, bundle, null, expectBundleResponse: true);
            return doRequest(req, HttpStatusCode.OK, () => bundleFromResponse());
        }


        public void DeliverToDocument(Bundle bundle)
        {
            if (bundle == null) throw Error.ArgumentNull("bundle");
            assertEndpoint();

            var url = new RestUrl(_endpoint).AddPath("Document");

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

            var url = new RestUrl(_endpoint).AddPath("Mailbox");

            if (bundle.GetBundleType() == BundleType.Document)
            {
                // Documents are merely "accepted"
                var req = prepareRequest("POST", url.Uri, bundle, null, expectBundleResponse: false);
                //return doRequest(req, HttpStatusCode.NoContent, () => (Bundle)null);
                return doRequest(req, HttpStatusCode.OK, () => bundleFromResponse());
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

            var rl = new RestUrl(_endpoint).Tags();

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
                api = new RestUrl(_endpoint).CollectionTags(collection);
            else
                api = new RestUrl(_endpoint).ResourceTags(collection, id, version);

            var req = prepareRequest("GET", api.Uri, null, null, expectBundleResponse: false);
            var result = doRequest(req, HttpStatusCode.OK, () => tagListFromResponse());
            return result.Category;
        }


        public void AffixTags<TResource>(IEnumerable<Tag> tags, string id, string version=null) where TResource : Resource, new()
        {
            if (id == null) throw new ArgumentNullException("id");
            if (tags == null) throw new ArgumentNullException("tags");
            assertEndpoint();

            var collection = typeof(TResource).GetCollectionName();
            var rl = new RestUrl(_endpoint).ResourceTags(collection, id, version);

            var req = prepareRequest("POST", rl.Uri, new TagList(tags), null, expectBundleResponse: false); 
            doRequest(req, HttpStatusCode.OK, () => true);
        }



        public void DeleteTags<TResource>(IEnumerable<Tag> tags, string id, string version = null) where TResource : Resource, new()
        {
            if (id == null) throw new ArgumentNullException("id");
            if (tags == null) throw new ArgumentNullException("tags");
            assertEndpoint();

            var collection = typeof(TResource).GetCollectionName();
            var rl = new RestUrl(_endpoint).ResourceTags(collection, id, version);

            var req = prepareRequest("DELETE", rl.Uri, new TagList(tags), null, expectBundleResponse: false);
            doRequest(req, HttpStatusCode.NoContent, () => true);
        }

        public ResourceFormat PreferredFormat { get; set; }
        public bool UseFormatParam { get; set; }
        
   
        public FhirResponse LastResponseDetails { get; private set; }

        private T doRequest<T>(FhirRequest request, HttpStatusCode success, Func<FhirResponse,T> onSuccess)
        {
            return doRequest<T>(request, new HttpStatusCode[] { success }, onSuccess);
        }


        public T doRequest<T>(FhirRequest request, HttpStatusCode[] success, Func<FhirResponse,T> onSuccess)
        {
            var response = request.GetResponse(PreferredFormat);

            if (success.Contains(response.Result))
                return onSuccess(response);
            else
            {
                // Try to parse the body as an OperationOutcome resource, but it is no
                // problem if it's something else, or there is no parseable body at all

                OperationOutcome outcome = null;

                try
                {
                    outcome = response.BodyAsEntry<OperationOutcome>().Resource;
                }
                catch
                {
                    // failed, so the body does not contain an OperationOutcome.
                    // Put the raw body as a message in the OperationOutcome as a fallback scenario
                    var body = response.BodyAsString();
                    if( !String.IsNullOrEmpty(body) )
                        outcome = OperationOutcome.ForMessage(body);
                }

                if (outcome != null)
                    throw new FhirOperationException("Operation failed with status code " + LastResponseDetails.Result, outcome);
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
