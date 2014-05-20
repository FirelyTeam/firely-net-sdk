/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
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
using Hl7.Fhir.Search;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Rest;
using System.Threading;
#if PORTABLE45 || NET45
using System.Threading.Tasks;
#endif


namespace Hl7.Fhir.Rest
{
    public class FhirClient
    {
        private Uri _endpoint;

        /// <summary>
        /// Creates a new client using a default endpoint
        /// If the endpoint does not end with a slash (/), it will be added.
        /// </summary>
        public FhirClient(Uri endpoint)
        {
            if (endpoint == null) throw new ArgumentNullException("endpoint");

            if (!endpoint.OriginalString.EndsWith("/"))
                endpoint = new Uri(endpoint.OriginalString + "/");

            if (!endpoint.IsAbsoluteUri) throw new ArgumentException("endpoint", "Endpoint must be absolute");

            _endpoint = endpoint;
            PreferredFormat = ResourceFormat.Xml;
        }


        public FhirClient(string endpoint)
            : this(new Uri(endpoint))
        {
        }

        public ResourceFormat PreferredFormat { get; set; }
        public bool UseFormatParam { get; set; }

        public FhirResponse LastResponseDetails { get; private set; }

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

        private Uri makeAbsolute(Uri location=null)
        {
            // If called without a location, just return the base endpoint
            if (location == null) return Endpoint;

            // If the location is absolute, verify whether it is within the endpoint
            if (location.IsAbsoluteUri)
            {
                if (!new RestUrl(Endpoint).IsEndpointFor(location))
                    throw Error.Argument("location", "Url is not located on this FhirClient's endpoint");
            }
            else
            {
                // Else, make location absolute within the endpoint
                //location = new Uri(Endpoint, location);
                location = new RestUrl(Endpoint).AddPath(location).Uri;
            }

            return location;
        }

        /// <summary>
        /// Get a conformance statement for the system
        /// </summary>
        /// <param name="useOptionsVerb">If true, uses the Http OPTIONS verb to get the conformance, otherwise uses the /metadata endpoint</param>
        /// <returns>A Conformance resource. Throws an exception if the operation failed.</returns>
        public ResourceEntry<Conformance> Conformance(bool useOptionsVerb = false)
        {
            RestUrl url = useOptionsVerb ? new RestUrl(Endpoint) : new RestUrl(Endpoint).WithMetadata();

            var req = createFhirRequest(url.Uri, useOptionsVerb ? "OPTIONS" : "GET");
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
            if (resource == null) throw Error.ArgumentNull("resource");

            return internalCreate<TResource>(resource, tags, null, refresh);
        }

        /// <summary>
        /// Create a resource with a given id on the FHIR endpoint
        /// </summary>
        /// <param name="resource">The resource instance to create</param>
        /// <param name="id">Optional. A client-assigned logical id for the newly created resource.</param>
        /// <param name="tags">Optional. List of Tags to add to the created instance.</param>
        /// <param name="refresh">Optional. When true, fetches the newly created resource from the server.</param>
        /// <returns>A ResourceEntry containing the metadata (id, selflink) associated with the resource as created on the server, or an exception if the create failed.</returns>
        /// <typeparam name="TResource">The type of resource to create</typeparam>
        /// <remarks>The Create operation normally does not return the posted resource, but just its metadata. Specifying
        /// refresh=true ensures the return value contains the Resource as stored by the server.
        /// </remarks>
        public ResourceEntry<TResource> Create<TResource>(TResource resource, string id, IEnumerable<Tag> tags = null, bool refresh = false) where TResource : Resource, new()
        {
            if (resource == null) throw Error.ArgumentNull("resource");
            if (id == null) throw Error.ArgumentNull("id", "Must supply a client-assigned id");

            return internalCreate<TResource>(resource, tags, id, refresh);
        }

        private FhirRequest createFhirRequest(Uri location, string method="GET")
        {
            return new FhirRequest(location, method, BeforeRequest, AfterResponse);
        }

        private ResourceEntry<TResource> internalCreate<TResource>(TResource resource, IEnumerable<Tag> tags, string id, bool refresh) where TResource : Resource, new()
        {
            var collection = typeof(TResource).GetCollectionName();
            FhirRequest req = null;

            if (id == null)
            {
                // A normal create
                var rl = new RestUrl(Endpoint).ForCollection(collection);
                req = createFhirRequest(rl.Uri, "POST");
            }
            else
            {
                // A create at a specific id => PUT to that address
                var ri = ResourceIdentity.Build(Endpoint, collection, id);
                req = createFhirRequest(ri, "PUT");
            }

            req.SetBody(resource, PreferredFormat);
            if(tags != null) req.SetTagsInHeader(tags);
            FhirResponse response = doRequest(req, new HttpStatusCode[] { HttpStatusCode.Created, HttpStatusCode.OK }, r => r);

            ResourceEntry<TResource> entry = (ResourceEntry<TResource>) ResourceEntry.Create(resource);
            entry.Links.SelfLink = new ResourceIdentity(response.Location);
            entry.Id = new ResourceIdentity(response.Location).WithoutVersion();

			if (!String.IsNullOrEmpty(response.LastModified))
				entry.LastUpdated = DateTimeOffset.Parse(response.LastModified);

			if (!String.IsNullOrEmpty(response.Category))
				entry.Tags = HttpUtil.ParseCategoryHeader(response.Category);

            // If asked for it, immediately get the contents *we just posted*, so use the actually created version
            if (refresh) entry = Refresh(entry, versionSpecific: true);
            return entry;
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

            if (!versionSpecific)
                return Read<TResource>(entry.Id);
            else
                return Read<TResource>(entry.SelfLink);
        }

        /// <summary>
        /// Fetches a typed resource from a FHIR resource endpoint.
        /// </summary>
        /// <param name="location">The url of the Resource to fetch. This can be a Resource id url or a version-specific
        /// Resource url.</param>
        /// <typeparam name="TResource">The type of resource to read</typeparam>
        /// <returns>The requested resource as a ResourceEntry&lt;T&gt;. This operation will throw an exception
        /// if the resource has been deleted or does not exist. The specified may be relative or absolute, if it is an abolute
        /// url, it must reference an address within the endpoint.</returns>
        public ResourceEntry<TResource> Read<TResource>(Uri location) where TResource : Resource, new()
        {
            if (location == null) throw Error.ArgumentNull("location");

            var req = createFhirRequest(makeAbsolute(location));  
            
            return doRequest(req, HttpStatusCode.OK, resp => resp.BodyAsEntry<TResource>());
        }

        /// <summary>
        /// Fetches a typed resource from a FHIR resource endpoint.
        /// </summary>
        /// <param name="location">The url of the Resource to fetch as a string. This can be a Resource id url or a version-specific
        /// Resource url.</param>
        /// <typeparam name="TResource">The type of resource to read</typeparam>
        /// <returns>The requested resource as a ResourceEntry&lt;T&gt;. This operation will throw an exception
        /// if the resource has been deleted or does not exist. The specified may be relative or absolute, if it is an abolute
        /// url, it must reference an address within the endpoint.</returns>
        public ResourceEntry<TResource> Read<TResource>(string location) where TResource : Resource, new()
        {
            if (location == null) throw Error.ArgumentNull("location");
            return Read<TResource>(new Uri(location, UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Reads a resource from a FHIR resource endpoint.
        /// </summary>
        /// <param name="location">The url of the Resource to fetch. This can be a Resource id url or a version-specific
        /// Resource url.</param>
        /// <returns>The requested resource as an untyped ResourceEntry. The ResourceEntry.Resource, which is of type
        /// object, must be cast to the correct Resource type to access its properties.
        /// The specified may be relative or absolute, if it is an abolute
        /// url, it must reference an address within the endpoint.</returns>
        public ResourceEntry Read(Uri location)
        {
            if (location == null) throw Error.ArgumentNull("location");

            var collection = getCollectionFromLocation(location);

            var req = createFhirRequest(makeAbsolute(location));
            return doRequest(req, HttpStatusCode.OK, resp => resp.BodyAsEntry(collection));
        }

        /// <summary>
        /// Reads a resource from a FHIR resource endpoint.
        /// </summary>
        /// <param name="location">The url of the Resource to fetch as a string. This can be a Resource id url or a version-specific
        /// Resource url.</param>
        /// <returns>The requested resource as an untyped ResourceEntry. The ResourceEntry.Resource, which is of type
        /// object, must be cast to the correct Resource type to access its properties.
        /// The specified may be relative or absolute, if it is an abolute
        /// url, it must reference an address within the endpoint.</returns>
        public ResourceEntry Read(string location)
        {
            if (location == null) throw Error.ArgumentNull("location");
            return Read(new Uri(location, UriKind.RelativeOrAbsolute));
        }

        private static string getCollectionFromLocation(Uri location)
        {
            var collection = new ResourceIdentity(location).Collection;
            if (collection == null) throw Error.Argument("location", "Must be a FHIR REST url containing the resource type in its path");

            return collection;
        }

        private static string getIdFromLocation(Uri location)
        {
            var id = new ResourceIdentity(location).Id;
            if (id == null) throw Error.Argument("location", "Must be a FHIR REST url containing the logical id in its path");

            return id;
        }

        /// <summary>
        /// Update (or create) a resource at a given endpoint
        /// </summary>
        /// <param name="entry">A ResourceEntry containing the resource to update</param>
        /// <param name="refresh">Optional. When true, fetches the newly updated resource from the server.</param>
        /// <typeparam name="TResource">The type of resource that is being updated</typeparam>
        /// <returns>If refresh=true, 
        /// this function will return a ResourceEntry with all newly created data from the server. Otherwise
        /// the returned result will only contain a SelfLink if the update was actually a create.
        /// Throws an exception when the update failed,
        /// in particular when an update conflict is detected and the server returns a HTTP 409. When the ResourceEntry
        /// passed as the argument does not have a SelfLink, the server may return a HTTP 412 to indicate it
        /// requires version-aware updates.</returns>
        public ResourceEntry<TResource> Update<TResource>(ResourceEntry<TResource> entry, bool refresh=false)
                        where TResource : Resource, new()
        {
            if (entry == null) throw Error.ArgumentNull("entry");
            if (entry.Resource == null) throw Error.Argument("entry","Entry does not contain a Resource to update");
            if (entry.Id == null) throw Error.Argument("entry","Entry needs a non-null entry.id to send the update to");

            var req = createFhirRequest(entry.Id, "PUT");
            req.SetBody(entry.Resource,PreferredFormat);
            if(entry.Tags != null) req.SetTagsInHeader(entry.Tags);

            // Always supply the version we are updating if we have one. Servers may require this.
            if (entry.SelfLink != null) req.SetContentLocation(entry.SelfLink);

            // This might be an update of a resource that doesn't yet exist, so accept a status Created too
            FhirResponse response = doRequest(req, new HttpStatusCode[] { HttpStatusCode.Created, HttpStatusCode.OK }, r => r);
            var updated = new ResourceEntry<TResource>();
			var location = response.Location ?? response.ContentLocation ?? response.ResponseUri.OriginalString;

			if (!String.IsNullOrEmpty(location))
			{
				ResourceIdentity reqId = new ResourceIdentity(location);

				// Set the id to the location, without the version specific part
				updated.Id = reqId.WithoutVersion();

				// If the content location has version information, set to SelfLink to it
				if (reqId.VersionId != null)
					updated.SelfLink = reqId;
			}

			if (!String.IsNullOrEmpty(response.LastModified))
				updated.LastUpdated = DateTimeOffset.Parse(response.LastModified);

			if (!String.IsNullOrEmpty(response.Category))
				updated.Tags = HttpUtil.ParseCategoryHeader(response.Category);

			updated.Title = entry.Title;

            // If asked for it, immediately get the contents *we just posted*, so use the actually created version
            if (refresh) updated = Refresh(updated, versionSpecific: true);

            return updated;
        }

        /// <summary>
        /// Update (or create) a resource at a given endpoint
        /// </summary>
        /// <param name="location">The location where the resource must be posted</param>
        /// <param name="data">The resource to send as an update</param>
        /// <param name="refresh">Optional. When true, fetches the newly updated resource from the server.</param>
        /// <typeparam name="TResource">The type of resource that is being updated</typeparam>
        /// <returns>If refresh=true, this function will return a ResourceEntry with all newly created data from the server. 
        /// Otherwise
        /// the returned result will only contain a SelfLink if the update was actually a create.
        /// Throws an exception when the update failed,
        /// in particular when an update conflict is detected and the server returns a HTTP 409. When the ResourceEntry
        /// passed as the argument does not have a SelfLink, the server may return a HTTP 412 to indicate it
        /// requires version-aware updates.</returns>
        public ResourceEntry<TResource> Update<TResource>(Uri location, TResource data, bool refresh = false)
            where TResource : Resource, new()
        {
            if(location == null) Error.ArgumentNull("location");
            if(data == null) Error.ArgumentNull("data");

            ResourceEntry<TResource> entry = new ResourceEntry<TResource>(makeAbsolute(location), DateTimeOffset.Now, data);

            return Update(entry, refresh);
        }

        /// <summary>
        /// Update (or create) a resource at a given endpoint
        /// </summary>
        /// <param name="location">The location where the resource must be posted</param>
        /// <param name="data">The resource to send as an update</param>
        /// <param name="refresh">Optional. When true, fetches the newly updated resource from the server.</param>
        /// <typeparam name="TResource">The type of resource that is being updated</typeparam>
        /// <returns>If refresh=true, this function will return a ResourceEntry with all newly created data from the server. 
        /// Otherwise
        /// the returned result will only contain a SelfLink if the update was actually a create.
        /// Throws an exception when the update failed,
        /// in particular when an update conflict is detected and the server returns a HTTP 409. When the ResourceEntry
        /// passed as the argument does not have a SelfLink, the server may return a HTTP 412 to indicate it
        /// requires version-aware updates.</returns>
        public ResourceEntry<TResource> Update<TResource>(string location, TResource data, bool refresh = false)
            where TResource : Resource, new()
        {
            if (location == null) Error.ArgumentNull("location");
            if (data == null) Error.ArgumentNull("data");

            return Update<TResource>(new Uri(location, UriKind.RelativeOrAbsolute), data, refresh);

        }

        // TODO: Have Update() without generic params.

        /// <summary>
        /// Delete a resource at the given endpoint.
        /// </summary>
        /// <param name="location">endpoint of the resource to delete</param>
        /// <returns>Throws an exception when the delete failed, though this might
        /// just mean the server returned 404 (the resource didn't exist before) or 410 (the resource was
        /// already deleted).</returns>
        public void Delete(Uri location)
        {
            if (location == null) throw Error.ArgumentNull("location");

            var req = createFhirRequest(makeAbsolute(location), "DELETE");
            doRequest(req, HttpStatusCode.NoContent, resp => true);
        }

        public void Delete(string location)
        {
            Uri uri = new Uri(location, UriKind.Relative);
            Delete(uri);
        }

        /// <summary>
        /// Delete a resource represented by the entry
        /// </summary>
        /// <param name="entry">Entry containing the id of the resource to delete</param>
        /// <returns>Throws an exception when the delete failed, though this might
        /// just mean the server returned 404 (the resource didn't exist before) or 410 (the resource was
        /// already deleted).</returns>
        public void Delete(ResourceEntry entry)
        {
            if (entry == null) throw Error.ArgumentNull("entry");
            if (entry.Id == null) throw Error.Argument("entry", "Entry must have an id");

            Delete(entry.Id);
        }

        /// <summary>
        /// Retrieve the version history for a specific resource type
        /// </summary>
        /// <param name="since">Optional. Returns only changes after the given date</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <typeparam name="TResource">The type of Resource to get the history for</typeparam>
        /// <returns>A bundle with the history for the indicated instance, may contain both 
        /// ResourceEntries and DeletedEntries.</returns>
	    public Bundle TypeHistory<TResource>(DateTimeOffset? since = null, int? pageSize = null) where TResource : Resource, new()
        {          
            var collection = typeof(TResource).GetCollectionName();

            return internalHistory(collection, null, since, pageSize);
        }

        /// <summary>
        /// Retrieve the version history for a resource at a given location
        /// </summary>
        /// <param name="location">The address of the resource to get the history for</param>
        /// <param name="since">Optional. Returns only changes after the given date</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <returns>A bundle with the history for the indicated instance, may contain both 
        /// ResourceEntries and DeletedEntries.</returns>
        public Bundle History(Uri location, DateTimeOffset? since = null, int? pageSize = null)
        {
            if (location == null) throw Error.ArgumentNull("location");

            var collection = getCollectionFromLocation(location);
            var id = getIdFromLocation(location);

            return internalHistory(collection, id, since, pageSize);
        }

        public Bundle History(string location, DateTimeOffset? since = null, int? pageSize = null)
        {
            if (location == null) throw Error.ArgumentNull("location");
            Uri uri = new Uri(location, UriKind.Relative);

            return History(uri, since, pageSize);
        }

        /// <summary>
        /// Retrieve the version history for a resource in a ResourceEntry
        /// </summary>
        /// <param name="entry">The ResourceEntry representing the Resource to get the history for</param>
        /// <param name="since">Optional. Returns only changes after the given date</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <returns>A bundle with the history for the indicated instance, may contain both 
        /// ResourceEntries and DeletedEntries.</returns>
        public Bundle History(BundleEntry entry, DateTimeOffset? since = null, int? pageSize = null)
        {
            if (entry == null) throw Error.ArgumentNull("entry");

            return History(entry.Id, since, pageSize);
        }

        /// <summary>
        /// Retrieve the full version history of the server
        /// </summary>
        /// <param name="since">Optional. Returns only changes after the given date</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <returns>A bundle with the history for the indicated instance, may contain both 
        /// ResourceEntries and DeletedEntries.</returns>
        public Bundle WholeSystemHistory(DateTimeOffset? since = null, int? pageSize = null)
        {
            return internalHistory(null, null, since, pageSize);
        }

        private Bundle internalHistory(string collection = null, string id = null, DateTimeOffset? since = null, int? pageSize = null)
        {
            RestUrl location = null;

            if(collection == null)
                location = new RestUrl(Endpoint).ServerHistory();
            else
            {
                location = (id == null) ?
                    new RestUrl(_endpoint).CollectionHistory(collection) :
                    new RestUrl(_endpoint).ResourceHistory(collection, id);
            }

            if (since != null) location = location.AddParam(HttpUtil.HISTORY_PARAM_SINCE, PrimitiveTypeConverter.ConvertTo<string>(since.Value));
            if (pageSize != null) location = location.AddParam(HttpUtil.HISTORY_PARAM_COUNT, pageSize.ToString());

            return fetchBundle(location.Uri);
        }

        /// <summary>
        /// Fetches a bundle from a FHIR resource endpoint. 
        /// </summary>
        /// <param name="location">The url of the endpoint which returns a Bundle</param>
        /// <returns>The Bundle as received by performing a GET on the endpoint. This operation will throw an exception
        /// if the operation does not result in a HttpStatus OK.</returns>
        private Bundle fetchBundle(Uri location)
        {
            var req = createFhirRequest(makeAbsolute(location), "GET");
            return doRequest(req, HttpStatusCode.OK, resp => resp.BodyAsBundle());
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
            if (entry == null) throw Error.ArgumentNull("entry");
            if (entry.Resource == null) throw Error.Argument("entry","Entry does not contain a Resource to validate");
            if (entry.Id == null) throw Error.Argument("enry", "Entry needs a non-null entry.id to use for validation");

            var id = new ResourceIdentity(entry.Id);
            var url = new RestUrl(Endpoint).Validate(id.Collection, id.Id);
            result = doValidate(url.Uri, entry.Resource, entry.Tags);

            return result == null || !result.Success();
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
        public bool TryValidateCreate<TResource>(TResource resource, out OperationOutcome result, IEnumerable<Tag> tags = null) where TResource : Resource, new()
        {
            if (resource == null) throw new ArgumentNullException("resource");

            var collection = typeof(Resource).GetCollectionName();
            var url = new RestUrl(_endpoint).Validate(collection);

            result = doValidate(url.Uri, resource, tags);
            return result == null || !result.Success();
        }

        private OperationOutcome doValidate(Uri url, Resource data, IEnumerable<Tag> tags)
        {
            var req = createFhirRequest(url, "POST");

            req.SetBody(data, PreferredFormat);
            if(tags != null) req.SetTagsInHeader(tags);

            try
            {
                doRequest(req, HttpStatusCode.OK, resp => true);
                return null;
            }
            catch (FhirOperationException foe)
            {
                if (foe.Outcome != null)
                    return foe.Outcome;
                else
                    throw; // no need to include foe, framework does this and preserves the stack location (CA2200)
            }
        }

        /// <summary>
        /// Search for Resources based on criteria specified in a Query resource
        /// </summary>
        /// <param name="q">The Query resource containing the search parameters</param>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        public Bundle Search(Query q)
        {
            RestUrl url = new RestUrl(Endpoint);
            url = url.Search(q);

            return fetchBundle(url.Uri);
        }
        
        /// <summary>
        /// Search for Resources of a certain type that match the given criteria
        /// </summary>
        /// <param name="criteria">Optional. The search parameters to filter the resources on. Each
        /// given string is a combined key/value pair (separated by '=')</param>
        /// <param name="includes">Optional. A list of include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <typeparam name="TResource">The type of resource to list</typeparam>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        /// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
        /// of all resources of the given Resource type</remarks>
        public Bundle Search<TResource>(string[] criteria = null, string[] includes = null, int? pageSize = null) where TResource : Resource, new()
        {
            return Search(typeof(TResource).GetCollectionName(), criteria, includes, pageSize);
        }

        /// <summary>
        /// Search for Resources of a certain type that match the given criteria
        /// </summary>
        /// <param name="resource">The type of resource to search for</param>
        /// <param name="criteria">Optional. The search parameters to filter the resources on. Each
        /// given string is a combined key/value pair (separated by '=')</param>
        /// <param name="includes">Optional. A list of include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        /// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
        /// of all resources of the given Resource type</remarks>
        public Bundle Search(string resource, string[] criteria = null, string[] includes = null, int? pageSize = null)
        {
            if (resource == null) throw Error.ArgumentNull("resource");

            return Search(toQuery(resource, criteria, includes, pageSize));
        }

        /// <summary>
        /// Search for Resources across the whol server that match the given criteria
        /// </summary>
        /// <param name="criteria">Optional. The search parameters to filter the resources on. Each
        /// given string is a combined key/value pair (separated by '=')</param>
        /// <param name="includes">Optional. A list of include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        /// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
        /// of all resources of the given Resource type</remarks>
        public Bundle WholeSystemSearch(string[] criteria = null, string[] includes = null, int? pageSize = null)
        {
            return Search(toQuery(null, criteria, includes, pageSize));
        }

        /// <summary>
        /// Search for resources based on a resource's id.
        /// </summary>
        /// <param name="id">The id of the resource to search for</param>
        /// <param name="includes">Zero or more include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <typeparam name="TResource">The type of resource to search for</typeparam>
        /// <returns>A Bundle with the BundleEntry as identified by the id parameter or an empty
        /// Bundle if the resource wasn't found.</returns>
        /// <remarks>This operation is similar to Read, but additionally,
        /// it is possible to specify include parameters to include resources in the bundle that the
        /// returned resource refers to.</remarks>
        public Bundle SearchById<TResource>(string id, string[] includes = null, int? pageSize = null) where TResource : Resource, new()
        {
            if (id == null) throw Error.ArgumentNull("id");

            return SearchById(typeof(TResource).GetCollectionName(), id, includes, pageSize);
        }

        /// <summary>
        /// Search for resources based on a resource's id.
        /// </summary>
        /// <param name="resource">The type of resource to search for</param>
        /// <param name="id">The id of the resource to search for</param>
        /// <param name="includes">Zero or more include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <returns>A Bundle with the BundleEntry as identified by the id parameter or an empty
        /// Bundle if the resource wasn't found.</returns>
        /// <remarks>This operation is similar to Read, but additionally,
        /// it is possible to specify include parameters to include resources in the bundle that the
        /// returned resource refers to.</remarks>
        public Bundle SearchById(string resource, string id, string[] includes = null, int? pageSize = null)
        {
            if (resource == null) throw Error.ArgumentNull("resource");
            if (id == null) throw Error.ArgumentNull("id");

            string criterium = Query.SEARCH_PARAM_ID + "=" + id;
            return Search(toQuery(resource, new string[] { criterium }, includes, pageSize));
        }

        private Query toQuery(string collection = null, string[] criteria = null, string[] includes = null, int? pageSize = null)
        {
            Query q = new Query
            {
                ResourceType = collection,
                Count = pageSize
            };

            if (includes != null)
                foreach (var inc in includes) q.Includes.Add(inc);

            if (criteria != null)
            {
                foreach (var crit in criteria)
                {
                    var keyVal = crit.SplitLeft('=');
                    q.AddParameter(keyVal.Item1,keyVal.Item2);
                }
            }
            return q;
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
            if (current == null) throw Error.ArgumentNull("current");
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
                return fetchBundle(continueAt);
            else
                return null;
        }

        /// <summary>
        /// Send a set of creates, updates and deletes to the server to be processed in one transaction
        /// </summary>
        /// <param name="bundle">The bundled creates, updates and delted</param>
        /// <returns>A bundle as returned by the server after it has processed the transaction, or null
        /// if an error occurred.</returns>
        public Bundle Transaction(Bundle bundle)
        {
            if (bundle == null) throw new ArgumentNullException("bundle");

            var req = createFhirRequest(Endpoint, "POST");
            req.SetBody(bundle, PreferredFormat);
            return doRequest(req, HttpStatusCode.OK, resp => resp.BodyAsBundle());
        }

        /// <summary>
        /// Send a document bundle
        /// </summary>
        /// <param name="bundle">A bundle containing a Document</param>
        /// <remarks>The bundle must declare it is a Document, use Bundle.SetBundleType() to do so.</remarks>
        public void Document(Bundle bundle)
        {
            if (bundle == null) throw Error.ArgumentNull("bundle");
            if(bundle.GetBundleType() != BundleType.Document)
                throw Error.Argument("bundle", "The bundle passed to the Document endpoint needs to be a document (use SetBundleType to do so)");

            var url = new RestUrl(Endpoint).ToDocument();

            // Documents are merely "accepted"
            var req = createFhirRequest(url.Uri, "POST");
            req.SetBody(bundle, PreferredFormat);
            doRequest(req, HttpStatusCode.NoContent, resp => true );
        }

        /// <summary>
        /// Send a Document or Message bundle to a server's Mailbox
        /// </summary>
        /// <param name="bundle">The Document or Message be sent</param>
        /// <returns>A return message as a Bundle</returns>
        /// <remarks>The bundle must declare it is a Document or Message, use Bundle.SetBundleType() to do so.</remarks>       
        public Bundle DeliverToMailbox(Bundle bundle)
        {
            if (bundle == null) throw Error.ArgumentNull("bundle");
            if( bundle.GetBundleType() != BundleType.Document && bundle.GetBundleType() != BundleType.Message)
                throw Error.Argument("bundle", "The bundle passed to the Mailbox endpoint needs to be a document or message (use SetBundleType to do so)");

            var url = new RestUrl(_endpoint).ToMailbox();

            var req = createFhirRequest(url.Uri, "POST");
            req.SetBody(bundle, PreferredFormat);

            return doRequest(req, HttpStatusCode.OK, resp => resp.BodyAsBundle());
        }

        /// <summary>
        /// Get all tags known by the FHIR server
        /// </summary>
        /// <returns>A list of Tags</returns>
        public IEnumerable<Tag> WholeSystemTags()
        {
            return internalGetTags(null, null, null);
        }

        /// <summary>
        /// Get all tags known by the FHIR server for a given resource type
        /// </summary>
        /// <returns>A list of all Tags present on the server</returns>
        public IEnumerable<Tag> TypeTags<TResource>() where TResource : Resource, new()
        {
            return internalGetTags(typeof(TResource).GetCollectionName(), null, null);
        }

        /// <summary>
        /// Get all tags known by the FHIR server for a given resource type
        /// </summary>
        /// <returns>A list of Tags occuring for the given resource type</returns>
        public IEnumerable<Tag> TypeTags(string type)
        {
            if (type == null) throw Error.ArgumentNull("type");

            return internalGetTags(type, null, null);
        }

        /// <summary>
        /// Get the tags for a resource (or resource version) at a given location
        /// </summary>
        /// <param name="location">The url of the Resource to get the tags for. This can be a Resource id url or a version-specific
        /// Resource url, and may be relative.</param>
        /// <returns>A list of Tags for the resource instance</returns>
        public IEnumerable<Tag> Tags(Uri location)
        {
            if (location == null) throw Error.ArgumentNull("location");

            var collection = getCollectionFromLocation(location);
            var id = getIdFromLocation(location);
            var version = new ResourceIdentity(location).VersionId;

            return internalGetTags(collection, id, version);
        }

        /// <summary>
        /// Get the tags for a resource (or resource version) at a given location
        /// </summary>
        /// <param name="location">The location the Resource to get the tags for. 
        /// This can be a Resource id url or a version-specific Resource url, and may be relative</param>
        /// <returns>A list of Tags for the resource instance</returns>
        public IEnumerable<Tag> Tags(string location)
        {
            var identity = new ResourceIdentity(location);
            return internalGetTags(identity.Collection, identity.Id, identity.VersionId);
        }

        /// <summary>
        /// Get the tags for a resource (or resource version) at a given location
        /// </summary>
        /// <param name="id">The logical id for the resource</param>
        /// <param name="vid">The version identifier for the resrouce</param>
        /// <returns>A list of Tags for the resource instance</returns>
        public IEnumerable<Tag> Tags<TResource>(string id, string vid = null)
        {
            string collection = ModelInfo.GetResourceNameForType(typeof(TResource));
            return internalGetTags(collection, id, vid);
        }

        private IEnumerable<Tag> internalGetTags(string collection, string id, string version)
        {
            RestUrl location = new RestUrl(this.Endpoint);

            if(collection == null)
                location = location.ServerTags();
            else
            {
                if(id == null)
                    location = location.CollectionTags(collection);
                else
                    location = location.ResourceTags(collection, id, version);
            }

            var req = createFhirRequest(location.Uri, "GET");
            var result = doRequest(req, HttpStatusCode.OK, resp => resp.BodyAsTagList());
            return result.Category;
        }

        /// <summary>
        /// Add one or more tags to a resource at a given location
        /// </summary>
        /// <param name="location">The url of the Resource to affix the tags to. This can be a Resource id url or a version-specific</param>
        /// <param name="tags">List of tags to add to the resource</param>
        /// <remarks>Affixing tags to a resource (or version of the resource) is not considered an update, so does 
        /// not create a new version.</remarks>
        public void AffixTags(Uri location, IEnumerable<Tag> tags)
        {
            if (location == null) throw Error.ArgumentNull("location");
            if (tags == null) throw Error.ArgumentNull("tags");

            var collection = getCollectionFromLocation(location);
            var id = getIdFromLocation(location);
            var version = new ResourceIdentity(location).VersionId;

            var rl = new RestUrl(Endpoint).ResourceTags(collection, id, version);

            var req = createFhirRequest(rl.Uri,"POST");
            req.SetBody(new TagList(tags), PreferredFormat);
            
            doRequest(req, HttpStatusCode.OK, resp => true);
        }

        /// <summary>
        /// Remove one or more tags from a resource at a given location
        /// </summary>
        /// <param name="location">The url of the Resource to remove the tags from. This can be a Resource id url or a version-specific</param>
        /// <param name="tags">List of tags to delete</param>
        /// <remarks>Removing tags to a resource (or version of the resource) is not considered an update, 
        /// so does not create a new version.</remarks>
        public void DeleteTags(Uri location, IEnumerable<Tag> tags)
        {
            if (location == null) throw Error.ArgumentNull("location");
            if (tags == null) throw Error.ArgumentNull("tags");

            var collection = getCollectionFromLocation(location);
            var id = getIdFromLocation(location);
            var version = new ResourceIdentity(location).VersionId;

            var rl = new RestUrl(Endpoint).DeleteResourceTags(collection, id, version);

            var req = createFhirRequest(rl.Uri, "POST");
            req.SetBody(new TagList(tags), PreferredFormat);

            doRequest(req, new HttpStatusCode[] { HttpStatusCode.OK, HttpStatusCode.NoContent }, resp => true);
        }


        public event BeforeRequestEventHandler OnBeforeRequest;

        public event AfterResponseEventHandler OnAfterResponse;

        /// <summary>
        /// Inspect or modify the HttpWebRequest just before the FhirClient issues a call to the server
        /// </summary>
        /// <param name="request">The request as it is about to be sent to the server</param>
        protected virtual void BeforeRequest(HttpWebRequest request) 
        {
            // Default implementation: call event
            if (OnBeforeRequest != null) OnBeforeRequest(this,new BeforeRequestEventArgs(request));
        }

        /// <summary>
        /// Inspect the HttpWebResponse as it came back from the server 
        /// </summary>
        /// <param name="response"></param>
        protected virtual void AfterResponse(WebResponse response)
        {
            // Default implementation: call event
            if (OnAfterResponse != null) OnAfterResponse(this,new AfterResponseEventArgs(response));
        }



        private T doRequest<T>(FhirRequest request, HttpStatusCode success, Func<FhirResponse,T> onSuccess)
        {
            return doRequest<T>(request, new HttpStatusCode[] { success }, onSuccess);
        }

        private T doRequest<T>(FhirRequest request, HttpStatusCode[] success, Func<FhirResponse,T> onSuccess)
        {
            request.UseFormatParameter = this.UseFormatParam;
            var response = request.GetResponse(PreferredFormat);

            LastResponseDetails = response;

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
				{
					System.Diagnostics.Debug.WriteLine("------------------------------------------------------");
					System.Diagnostics.Debug.WriteLine(outcome.Text);
					foreach (var issue in outcome.Issue)
					{
						System.Diagnostics.Debug.WriteLine("	" + issue.Details);
					}
					System.Diagnostics.Debug.WriteLine("------------------------------------------------------");

					throw new FhirOperationException("Operation failed with status code " + LastResponseDetails.Result, outcome);
				}
				else
				{
					throw new FhirOperationException("Operation failed with status code " + LastResponseDetails.Result);
				}
            }
        }

#if PORTABLE45 || NET45
#region << Async operations >>
        /// <summary>
        /// Get a conformance statement for the system
        /// </summary>
        /// <param name="useOptionsVerb">If true, uses the Http OPTIONS verb to get the conformance, otherwise uses the /metadata endpoint</param>
        /// <returns>A Conformance resource. Throws an exception if the operation failed.</returns>
        public Task<ResourceEntry<Conformance>> ConformanceAsync(bool useOptionsVerb = false)
        {
            RestUrl url = useOptionsVerb ? new RestUrl(Endpoint) : new RestUrl(Endpoint).WithMetadata();

            var req = createFhirRequest(url.Uri, useOptionsVerb ? "OPTIONS" : "GET");
            return doRequestAsync(req, HttpStatusCode.OK, resp => resp.BodyAsEntry<Conformance>());
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
		public Task<ResourceEntry<TResource>> CreateAsync<TResource>(TResource resource, IEnumerable<Tag> tags = null, bool refresh = false) where TResource : Resource, new()
		{
			if (resource == null) throw Error.ArgumentNull("resource");

			return internalCreateAsync<TResource>(resource, tags, null, refresh);
		}

		/// <summary>
		/// Create a resource with a given id on the FHIR endpoint
		/// </summary>
		/// <param name="resource">The resource instance to create</param>
		/// <param name="id">Optional. A client-assigned logical id for the newly created resource.</param>
		/// <param name="tags">Optional. List of Tags to add to the created instance.</param>
		/// <param name="refresh">Optional. When true, fetches the newly created resource from the server.</param>
		/// <returns>A ResourceEntry containing the metadata (id, selflink) associated with the resource as created on the server, or an exception if the create failed.</returns>
		/// <typeparam name="TResource">The type of resource to create</typeparam>
		/// <remarks>The Create operation normally does not return the posted resource, but just its metadata. Specifying
		/// refresh=true ensures the return value contains the Resource as stored by the server.
		/// </remarks>
		public Task<ResourceEntry<TResource>> CreateAsync<TResource>(TResource resource, string id, IEnumerable<Tag> tags = null, bool refresh = false) where TResource : Resource, new()
		{
			if (resource == null) throw Error.ArgumentNull("resource");
			if (id == null) throw Error.ArgumentNull("id", "Must supply a client-assigned id");

			return internalCreateAsync<TResource>(resource, tags, id, refresh);
		}

		private async Task<ResourceEntry<TResource>> internalCreateAsync<TResource>(TResource resource, IEnumerable<Tag> tags, string id, bool refresh) where TResource : Resource, new()
		{
			var collection = typeof(TResource).GetCollectionName();
			FhirRequest req = null;

			if (id == null)
			{
				// A normal create
				var rl = new RestUrl(Endpoint).ForCollection(collection);
				req = createFhirRequest(rl.Uri, "POST");
			}
			else
			{
				// A create at a specific id => PUT to that address
				var ri = ResourceIdentity.Build(Endpoint, collection, id);
				req = createFhirRequest(ri, "PUT");
			}

			req.SetBody(resource, PreferredFormat);
			if (tags != null) req.SetTagsInHeader(tags);
			FhirResponse response = await doRequestAsync(req, new HttpStatusCode[] { HttpStatusCode.Created, HttpStatusCode.OK }, r => r);

			ResourceEntry<TResource> entry = (ResourceEntry<TResource>)ResourceEntry.Create(resource);
			entry.Links.SelfLink = new ResourceIdentity(response.Location);
			entry.Id = new ResourceIdentity(response.Location).WithoutVersion();

			if (!String.IsNullOrEmpty(response.LastModified))
				entry.LastUpdated = DateTimeOffset.Parse(response.LastModified);

			if (!String.IsNullOrEmpty(response.Category))
				entry.Tags = HttpUtil.ParseCategoryHeader(response.Category);

			// If asked for it, immediately get the contents *we just posted*, so use the actually created version
			if (refresh)
			{
				entry = await RefreshAsync(entry, versionSpecific: true);
			}
			return entry;
		}

		/// <summary>
		/// Refreshes the data and metadata for a given ResourceEntry.
		/// </summary>
		/// <param name="entry">The entry to refresh. It's id property will be used to fetch the latest version of the Resource.</param>
		/// <typeparam name="TResource">The type of resource to refresh</typeparam>
		/// <returns>A resource entry containing up-to-date data and metadata.</returns>
		public Task<ResourceEntry<TResource>> RefreshAsync<TResource>(ResourceEntry<TResource> entry) where TResource : Resource, new()
		{
			return RefreshAsync<TResource>(entry, false);
		}

		internal Task<ResourceEntry<TResource>> RefreshAsync<TResource>(ResourceEntry<TResource> entry, bool versionSpecific = false) where TResource : Resource, new()
		{
			if (entry == null) throw Error.ArgumentNull("entry");

			if (!versionSpecific)
				return ReadAsync<TResource>(entry.Id);
			else
				return ReadAsync<TResource>(entry.SelfLink);
		}

		/// <summary>
		/// Fetches a typed resource from a FHIR resource endpoint.
		/// </summary>
		/// <param name="location">The url of the Resource to fetch. This can be a Resource id url or a version-specific
		/// Resource url.</param>
		/// <typeparam name="TResource">The type of resource to read</typeparam>
		/// <returns>The requested resource as a ResourceEntry&lt;T&gt;. This operation will throw an exception
		/// if the resource has been deleted or does not exist. The specified may be relative or absolute, if it is an abolute
		/// url, it must reference an address within the endpoint.</returns>
		public Task<ResourceEntry<TResource>> ReadAsync<TResource>(Uri location) where TResource : Resource, new()
		{
			if (location == null) throw Error.ArgumentNull("location");

			var req = createFhirRequest(makeAbsolute(location));

			return doRequestAsync(req, HttpStatusCode.OK, resp => resp.BodyAsEntry<TResource>());
		}

		/// <summary>
		/// Fetches a typed resource from a FHIR resource endpoint.
		/// </summary>
		/// <param name="location">The url of the Resource to fetch as a string. This can be a Resource id url or a version-specific
		/// Resource url.</param>
		/// <typeparam name="TResource">The type of resource to read</typeparam>
		/// <returns>The requested resource as a ResourceEntry&lt;T&gt;. This operation will throw an exception
		/// if the resource has been deleted or does not exist. The specified may be relative or absolute, if it is an abolute
		/// url, it must reference an address within the endpoint.</returns>
		public Task<ResourceEntry<TResource>> ReadAsync<TResource>(string location) where TResource : Resource, new()
		{
			if (location == null) throw Error.ArgumentNull("location");
			return ReadAsync<TResource>(new Uri(location, UriKind.RelativeOrAbsolute));
		}

		/// <summary>
		/// Reads a resource from a FHIR resource endpoint.
		/// </summary>
		/// <param name="location">The url of the Resource to fetch. This can be a Resource id url or a version-specific
		/// Resource url.</param>
		/// <returns>The requested resource as an untyped ResourceEntry. The ResourceEntry.Resource, which is of type
		/// object, must be cast to the correct Resource type to access its properties.
		/// The specified may be relative or absolute, if it is an abolute
		/// url, it must reference an address within the endpoint.</returns>
		public Task<ResourceEntry> ReadAsync(Uri location)
		{
			if (location == null) throw Error.ArgumentNull("location");

			var collection = getCollectionFromLocation(location);

			var req = createFhirRequest(makeAbsolute(location));
			return doRequestAsync(req, HttpStatusCode.OK, resp => resp.BodyAsEntry(collection));
		}

		/// <summary>
		/// Reads a resource from a FHIR resource endpoint.
		/// </summary>
		/// <param name="location">The url of the Resource to fetch as a string. This can be a Resource id url or a version-specific
		/// Resource url.</param>
		/// <returns>The requested resource as an untyped ResourceEntry. The ResourceEntry.Resource, which is of type
		/// object, must be cast to the correct Resource type to access its properties.
		/// The specified may be relative or absolute, if it is an abolute
		/// url, it must reference an address within the endpoint.</returns>
		public Task<ResourceEntry> ReadAsync(string location)
		{
			if (location == null) throw Error.ArgumentNull("location");
			return ReadAsync(new Uri(location, UriKind.RelativeOrAbsolute));
		}

		/// <summary>
		/// Update (or create) a resource at a given endpoint
		/// </summary>
		/// <param name="entry">A ResourceEntry containing the resource to update</param>
		/// <param name="refresh">Optional. When true, fetches the newly updated resource from the server.</param>
		/// <typeparam name="TResource">The type of resource that is being updated</typeparam>
		/// <returns>If refresh=true, 
		/// this function will return a ResourceEntry with all newly created data from the server. Otherwise
		/// the returned result will only contain a SelfLink if the update was actually a create.
		/// Throws an exception when the update failed,
		/// in particular when an update conflict is detected and the server returns a HTTP 409. When the ResourceEntry
		/// passed as the argument does not have a SelfLink, the server may return a HTTP 412 to indicate it
		/// requires version-aware updates.</returns>
		public async Task<ResourceEntry<TResource>> UpdateAsync<TResource>(ResourceEntry<TResource> entry, bool refresh = false)
						where TResource : Resource, new()
		{
			if (entry == null) throw Error.ArgumentNull("entry");
			if (entry.Resource == null) throw Error.Argument("entry", "Entry does not contain a Resource to update");
			if (entry.Id == null) throw Error.Argument("entry", "Entry needs a non-null entry.id to send the update to");

			var req = createFhirRequest(entry.Id, "PUT");
			req.SetBody(entry.Resource, PreferredFormat);
			if (entry.Tags != null) req.SetTagsInHeader(entry.Tags);

			// Always supply the version we are updating if we have one. Servers may require this.
			if (entry.SelfLink != null) req.SetContentLocation(entry.SelfLink);

			// This might be an update of a resource that doesn't yet exist, so accept a status Created too
			FhirResponse response = await doRequestAsync(req, new HttpStatusCode[] { HttpStatusCode.Created, HttpStatusCode.OK }, r => r);
			var updated = new ResourceEntry<TResource>();
			var location = response.Location ?? response.ContentLocation ?? response.ResponseUri.OriginalString;

			if (!String.IsNullOrEmpty(location))
			{
				ResourceIdentity reqId = new ResourceIdentity(location);

				// Set the id to the location, without the version specific part
				updated.Id = reqId.WithoutVersion();

				// If the content location has version information, set to SelfLink to it
				if (reqId.VersionId != null)
					updated.SelfLink = reqId;
			}

			if (!String.IsNullOrEmpty(response.LastModified))
				updated.LastUpdated = DateTimeOffset.Parse(response.LastModified);

			if (!String.IsNullOrEmpty(response.Category))
				updated.Tags = HttpUtil.ParseCategoryHeader(response.Category);

			updated.Title = entry.Title;

			// If asked for it, immediately get the contents *we just posted*, so use the actually created version
			if (refresh)
			{
				updated = await RefreshAsync(updated, versionSpecific: true);
			}

			return updated;
		}

		/// <summary>
		/// Update (or create) a resource at a given endpoint
		/// </summary>
		/// <param name="location">The location where the resource must be posted</param>
		/// <param name="data">The resource to send as an update</param>
		/// <param name="refresh">Optional. When true, fetches the newly updated resource from the server.</param>
		/// <typeparam name="TResource">The type of resource that is being updated</typeparam>
		/// <returns>If refresh=true, this function will return a ResourceEntry with all newly created data from the server. 
		/// Otherwise
		/// the returned result will only contain a SelfLink if the update was actually a create.
		/// Throws an exception when the update failed,
		/// in particular when an update conflict is detected and the server returns a HTTP 409. When the ResourceEntry
		/// passed as the argument does not have a SelfLink, the server may return a HTTP 412 to indicate it
		/// requires version-aware updates.</returns>
		public Task<ResourceEntry<TResource>> UpdateAsync<TResource>(Uri location, TResource data, bool refresh = false)
			where TResource : Resource, new()
		{
			if (location == null) Error.ArgumentNull("location");
			if (data == null) Error.ArgumentNull("data");

			ResourceEntry<TResource> entry = new ResourceEntry<TResource>(makeAbsolute(location), DateTimeOffset.Now, data);

			return UpdateAsync(entry, refresh);
		}


		/// <summary>
		/// Update (or create) a resource at a given endpoint
		/// </summary>
		/// <param name="location">The location where the resource must be posted</param>
		/// <param name="data">The resource to send as an update</param>
		/// <param name="refresh">Optional. When true, fetches the newly updated resource from the server.</param>
		/// <typeparam name="TResource">The type of resource that is being updated</typeparam>
		/// <returns>If refresh=true, this function will return a ResourceEntry with all newly created data from the server. 
		/// Otherwise
		/// the returned result will only contain a SelfLink if the update was actually a create.
		/// Throws an exception when the update failed,
		/// in particular when an update conflict is detected and the server returns a HTTP 409. When the ResourceEntry
		/// passed as the argument does not have a SelfLink, the server may return a HTTP 412 to indicate it
		/// requires version-aware updates.</returns>
		public Task<ResourceEntry<TResource>> UpdateAsync<TResource>(string location, TResource data, bool refresh = false)
			where TResource : Resource, new()
		{
			if (location == null) Error.ArgumentNull("location");
			if (data == null) Error.ArgumentNull("data");

			return UpdateAsync<TResource>(new Uri(location, UriKind.RelativeOrAbsolute), data, refresh);
		}

		/// <summary>
		/// Delete a resource at the given endpoint.
		/// </summary>
		/// <param name="location">endpoint of the resource to delete</param>
		/// <returns>Throws an exception when the delete failed, though this might
		/// just mean the server returned 404 (the resource didn't exist before) or 410 (the resource was
		/// already deleted).</returns>
		public Task DeleteAsync(Uri location)
		{
			if (location == null) throw Error.ArgumentNull("location");

			var req = createFhirRequest(makeAbsolute(location), "DELETE");
			return doRequestAsync(req, HttpStatusCode.NoContent, resp => true);
		}

		public Task DeleteAsync(string location)
		{
			Uri uri = new Uri(location, UriKind.Relative);
			return DeleteAsync(uri);
		}

		/// <summary>
		/// Delete a resource represented by the entry
		/// </summary>
		/// <param name="entry">Entry containing the id of the resource to delete</param>
		/// <returns>Throws an exception when the delete failed, though this might
		/// just mean the server returned 404 (the resource didn't exist before) or 410 (the resource was
		/// already deleted).</returns>
		public Task DeleteAsync(ResourceEntry entry)
		{
			if (entry == null) throw Error.ArgumentNull("entry");
			if (entry.Id == null) throw Error.Argument("entry", "Entry must have an id");

			return DeleteAsync(entry.Id);
		}

		/// <summary>
		/// Retrieve the version history for a specific resource type
		/// </summary>
		/// <param name="since">Optional. Returns only changes after the given date</param>
		/// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
		/// <typeparam name="TResource">The type of Resource to get the history for</typeparam>
		/// <returns>A bundle with the history for the indicated instance, may contain both 
		/// ResourceEntries and DeletedEntries.</returns>
		public Task<Bundle> TypeHistoryAsync<TResource>(DateTimeOffset? since = null, int? pageSize = null) where TResource : Resource, new()
		{
			var collection = typeof(TResource).GetCollectionName();

			return internalHistoryAsync(collection, null, since, pageSize);
		}

		/// <summary>
		/// Retrieve the version history for a resource at a given location
		/// </summary>
		/// <param name="location">The address of the resource to get the history for</param>
		/// <param name="since">Optional. Returns only changes after the given date</param>
		/// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
		/// <returns>A bundle with the history for the indicated instance, may contain both 
		/// ResourceEntries and DeletedEntries.</returns>
		public Task<Bundle> HistoryAsync(Uri location, DateTimeOffset? since = null, int? pageSize = null)
		{
			if (location == null) throw Error.ArgumentNull("location");

			var collection = getCollectionFromLocation(location);
			var id = getIdFromLocation(location);

			return internalHistoryAsync(collection, id, since, pageSize);
		}

		public Task<Bundle> HistoryAsync(string location, DateTimeOffset? since = null, int? pageSize = null)
		{
			if (location == null) throw Error.ArgumentNull("location");
			Uri uri = new Uri(location, UriKind.Relative);

			return HistoryAsync(uri, since, pageSize);
		}

		/// <summary>
		/// Retrieve the version history for a resource in a ResourceEntry
		/// </summary>
		/// <param name="entry">The ResourceEntry representing the Resource to get the history for</param>
		/// <param name="since">Optional. Returns only changes after the given date</param>
		/// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
		/// <returns>A bundle with the history for the indicated instance, may contain both 
		/// ResourceEntries and DeletedEntries.</returns>
		public Task<Bundle> HistoryAsync(BundleEntry entry, DateTimeOffset? since = null, int? pageSize = null)
		{
			if (entry == null) throw Error.ArgumentNull("entry");

			return HistoryAsync(entry.Id, since, pageSize);
		}

		/// <summary>
		/// Retrieve the full version history of the server
		/// </summary>
		/// <param name="since">Optional. Returns only changes after the given date</param>
		/// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
		/// <returns>A bundle with the history for the indicated instance, may contain both 
		/// ResourceEntries and DeletedEntries.</returns>
		public Task<Bundle> WholeSystemHistoryAsync(DateTimeOffset? since = null, int? pageSize = null)
		{
			return internalHistoryAsync(null, null, since, pageSize);
		}

		private Task<Bundle> internalHistoryAsync(string collection = null, string id = null, DateTimeOffset? since = null, int? pageSize = null)
		{
			RestUrl location = null;

			if (collection == null)
				location = new RestUrl(Endpoint).ServerHistory();
			else
			{
				location = (id == null) ?
					new RestUrl(_endpoint).CollectionHistory(collection) :
					new RestUrl(_endpoint).ResourceHistory(collection, id);
			}

			if (since != null) location = location.AddParam(HttpUtil.HISTORY_PARAM_SINCE, PrimitiveTypeConverter.ConvertTo<string>(since.Value));
			if (pageSize != null) location = location.AddParam(HttpUtil.HISTORY_PARAM_COUNT, pageSize.ToString());

			return fetchBundleAsync(location.Uri);
		}
		/// <summary>
		/// Fetches a bundle from a FHIR resource endpoint. 
		/// </summary>
		/// <param name="location">The url of the endpoint which returns a Bundle</param>
		/// <returns>The Bundle as received by performing a GET on the endpoint. This operation will throw an exception
		/// if the operation does not result in a HttpStatus OK.</returns>
		private Task<Bundle> fetchBundleAsync(Uri location)
		{
			var req = createFhirRequest(makeAbsolute(location), "GET");
			return doRequestAsync(req, HttpStatusCode.OK, resp => resp.BodyAsBundle());
		}

		public struct ValidateAsyncResult
		{
			public ValidateAsyncResult(bool result, OperationOutcome outcome)
			{
				_result = result;
				_outcome = outcome;
			}

			private bool _result;
			private OperationOutcome _outcome;

			public bool Result { get {return _result;} }
			public OperationOutcome Outcome { get {return _outcome;} }
		}

		/// <summary>
		/// Validates whether the contents of the resource would be acceptable as an update
		/// </summary>
		/// <param name="entry">The entry containing the updated Resource to validate</param>
		/// <returns>True when validation was successful, false otherwise. Note that this function may still throw exceptions if non-validation related
		/// failures occur.</returns>
		public async Task<ValidateAsyncResult> TryValidateUpdateAsync<TResource>(ResourceEntry<TResource> entry) where TResource : Resource, new()
		{
			if (entry == null) throw Error.ArgumentNull("entry");
			if (entry.Resource == null) throw Error.Argument("entry", "Entry does not contain a Resource to validate");
			if (entry.Id == null) throw Error.Argument("enry", "Entry needs a non-null entry.id to use for validation");

			var id = new ResourceIdentity(entry.Id);
			var url = new RestUrl(Endpoint).Validate(id.Collection, id.Id);
			OperationOutcome validationResult = await doValidateAsync(url.Uri, entry.Resource, entry.Tags);
			ValidateAsyncResult result = new ValidateAsyncResult(validationResult == null || !validationResult.Success(), validationResult);
			return result;
		}

		/// <summary>
		/// Validates whether the contents of the resource would be acceptable as a create
		/// </summary>
		/// <typeparam name="TResource"></typeparam>
		/// <param name="resource">The entry containing the Resource data to use for the validation</param>
		/// <param name="tags">Optional list of tags to attach to the resource</param>
		/// <returns>True when validation was successful, false otherwise. Note that this function may still throw exceptions if non-validation related
		/// failures occur.</returns>
		public async Task<ValidateAsyncResult> TryValidateCreateAsync<TResource>(TResource resource, IEnumerable<Tag> tags = null) where TResource : Resource, new()
		{
			if (resource == null) throw new ArgumentNullException("resource");

			var collection = typeof(Resource).GetCollectionName();
			var url = new RestUrl(_endpoint).Validate(collection);

			OperationOutcome validationResult = await doValidateAsync(url.Uri, resource, tags);
			ValidateAsyncResult result = new ValidateAsyncResult(validationResult == null || !validationResult.Success(), validationResult);
			return result;
		}

		private async Task<OperationOutcome> doValidateAsync(Uri url, Resource data, IEnumerable<Tag> tags)
		{
			var req = createFhirRequest(url, "POST");

			req.SetBody(data, PreferredFormat);
			if (tags != null) req.SetTagsInHeader(tags);

			try
			{
				await doRequestAsync(req, HttpStatusCode.OK, resp => true);
				return null;
			}
			catch (FhirOperationException foe)
			{
				if (foe.Outcome != null)
					return foe.Outcome;
				else
					throw; // no need to include foe, framework does this and preserves the stack location (CA2200)
			}
		}

		/// <summary>
		/// Search for Resources based on criteria specified in a Query resource
		/// </summary>
		/// <param name="q">The Query resource containing the search parameters</param>
		/// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
		public Task<Bundle> SearchAsync(Query q)
		{
			RestUrl url = new RestUrl(Endpoint);
			url = url.Search(q);

			return fetchBundleAsync(url.Uri);
		}

		/// <summary>
		/// Search for Resources of a certain type that match the given criteria
		/// </summary>
		/// <param name="criteria">Optional. The search parameters to filter the resources on. Each
		/// given string is a combined key/value pair (separated by '=')</param>
		/// <param name="includes">Optional. A list of include paths</param>
		/// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
		/// <typeparam name="TResource">The type of resource to list</typeparam>
		/// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
		/// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
		/// of all resources of the given Resource type</remarks>
		public Task<Bundle> SearchAsync<TResource>(string[] criteria = null, string[] includes = null, int? pageSize = null) where TResource : Resource, new()
		{
			return SearchAsync(typeof(TResource).GetCollectionName(), criteria, includes, pageSize);
		}

		/// <summary>
		/// Search for Resources of a certain type that match the given criteria
		/// </summary>
		/// <param name="resource">The type of resource to search for</param>
		/// <param name="criteria">Optional. The search parameters to filter the resources on. Each
		/// given string is a combined key/value pair (separated by '=')</param>
		/// <param name="includes">Optional. A list of include paths</param>
		/// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
		/// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
		/// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
		/// of all resources of the given Resource type</remarks>
		public Task<Bundle> SearchAsync(string resource, string[] criteria = null, string[] includes = null, int? pageSize = null)
		{
			if (resource == null) throw Error.ArgumentNull("resource");

			return SearchAsync(toQuery(resource, criteria, includes, pageSize));
		}

		/// <summary>
		/// Search for Resources across the whol server that match the given criteria
		/// </summary>
		/// <param name="criteria">Optional. The search parameters to filter the resources on. Each
		/// given string is a combined key/value pair (separated by '=')</param>
		/// <param name="includes">Optional. A list of include paths</param>
		/// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
		/// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
		/// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
		/// of all resources of the given Resource type</remarks>
		public Task<Bundle> WholeSystemSearchAsync(string[] criteria = null, string[] includes = null, int? pageSize = null)
		{
			return SearchAsync(toQuery(null, criteria, includes, pageSize));
		}

		/// <summary>
		/// Search for resources based on a resource's id.
		/// </summary>
		/// <param name="id">The id of the resource to search for</param>
		/// <param name="includes">Zero or more include paths</param>
        /// <param name="pageSize">Optional maximum on the number of results returned by the server</param>
		/// <typeparam name="TResource">The type of resource to search for</typeparam>
		/// <returns>A Bundle with the BundleEntry as identified by the id parameter or an empty
		/// Bundle if the resource wasn't found.</returns>
		/// <remarks>This operation is similar to Read, but additionally,
		/// it is possible to specify include parameters to include resources in the bundle that the
		/// returned resource refers to.</remarks>
		public Task<Bundle> SearchByIdAsync<TResource>(string id, string[] includes = null, int? pageSize = null) where TResource : Resource, new()
		{
			if (id == null) throw Error.ArgumentNull("id");

			return SearchByIdAsync(typeof(TResource).GetCollectionName(), id, includes, pageSize);
		}

		/// <summary>
		/// Search for resources based on a resource's id.
		/// </summary>
		/// <param name="resource">The type of resource to search for</param>
		/// <param name="id">The id of the resource to search for</param>
		/// <param name="includes">Zero or more include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
		/// <returns>A Bundle with the BundleEntry as identified by the id parameter or an empty
		/// Bundle if the resource wasn't found.</returns>
		/// <remarks>This operation is similar to Read, but additionally,
		/// it is possible to specify include parameters to include resources in the bundle that the
		/// returned resource refers to.</remarks>
		public Task<Bundle> SearchByIdAsync(string resource, string id, string[] includes = null, int? pageSize = null)
		{
			if (resource == null) throw Error.ArgumentNull("resource");
			if (id == null) throw Error.ArgumentNull("id");

			string criterium = Query.SEARCH_PARAM_ID + "=" + id;
			return SearchAsync(toQuery(resource, new string[] { criterium }, includes, pageSize));
		}

		/// <summary>
		/// Uses the FHIR paging mechanism to go navigate around a series of paged result Bundles
		/// </summary>
		/// <param name="current">The bundle as received from the last response</param>
		/// <param name="direction">Optional. Direction to browse to, default is the next page of results.</param>
		/// <returns>A bundle containing a new page of results based on the browse direction, or null if
		/// the server did not have more results in that direction.</returns>
		public Task<Bundle> ContinueAsync(Bundle current, PageDirection direction = PageDirection.Next)
		{
			if (current == null) throw Error.ArgumentNull("current");
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
				return fetchBundleAsync(continueAt);
			else
				return null;
		}

		/// <summary>
		/// Send a set of creates, updates and deletes to the server to be processed in one transaction
		/// </summary>
		/// <param name="bundle">The bundled creates, updates and delted</param>
		/// <returns>A bundle as returned by the server after it has processed the transaction, or null
		/// if an error occurred.</returns>
		public Task<Bundle> TransactionAsync(Bundle bundle)
		{
			if (bundle == null) throw new ArgumentNullException("bundle");

			var req = createFhirRequest(Endpoint, "POST");
			req.SetBody(bundle, PreferredFormat);
			return doRequestAsync(req, HttpStatusCode.OK, resp => resp.BodyAsBundle());
		}

		/// <summary>
		/// Send a document bundle
		/// </summary>
		/// <param name="bundle">A bundle containing a Document</param>
		/// <remarks>The bundle must declare it is a Document, use Bundle.SetBundleType() to do so.</remarks>
		public Task DocumentAsync(Bundle bundle)
		{
			if (bundle == null) throw Error.ArgumentNull("bundle");
			if (bundle.GetBundleType() != BundleType.Document)
				throw Error.Argument("bundle", "The bundle passed to the Document endpoint needs to be a document (use SetBundleType to do so)");

			var url = new RestUrl(Endpoint).ToDocument();

			// Documents are merely "accepted"
			var req = createFhirRequest(url.Uri, "POST");
			req.SetBody(bundle, PreferredFormat);
			return doRequestAsync(req, HttpStatusCode.NoContent, resp => true);
		}

		/// <summary>
		/// Send a Document or Message bundle to a server's Mailbox
		/// </summary>
		/// <param name="bundle">The Document or Message be sent</param>
		/// <returns>A return message as a Bundle</returns>
		/// <remarks>The bundle must declare it is a Document or Message, use Bundle.SetBundleType() to do so.</remarks>       
		public Task<Bundle> DeliverToMailboxAsync(Bundle bundle)
		{
			if (bundle == null) throw Error.ArgumentNull("bundle");
			if (bundle.GetBundleType() != BundleType.Document && bundle.GetBundleType() != BundleType.Message)
				throw Error.Argument("bundle", "The bundle passed to the Mailbox endpoint needs to be a document or message (use SetBundleType to do so)");

			var url = new RestUrl(_endpoint).ToMailbox();

			var req = createFhirRequest(url.Uri, "POST");
			req.SetBody(bundle, PreferredFormat);

			return doRequestAsync(req, HttpStatusCode.OK, resp => resp.BodyAsBundle());
		}

		/// <summary>
		/// Get all tags known by the FHIR server
		/// </summary>
		/// <returns>A list of Tags</returns>
		public Task<IEnumerable<Tag>> WholeSystemTagsAsync()
		{
			return internalGetTagsAsync(null, null, null);
		}

		/// <summary>
		/// Get all tags known by the FHIR server for a given resource type
		/// </summary>
		/// <returns>A list of all Tags present on the server</returns>
		public Task<IEnumerable<Tag>> TypeTagsAsync<TResource>() where TResource : Resource, new()
		{
			return internalGetTagsAsync(typeof(TResource).GetCollectionName(), null, null);
		}

		/// <summary>
		/// Get all tags known by the FHIR server for a given resource type
		/// </summary>
		/// <returns>A list of Tags occuring for the given resource type</returns>
		public Task<IEnumerable<Tag>> TypeTagsAsync(string type)
		{
			if (type == null) throw Error.ArgumentNull("type");

			return internalGetTagsAsync(type, null, null);
		}

		/// <summary>
		/// Get the tags for a resource (or resource version) at a given location
		/// </summary>
		/// <param name="location">The url of the Resource to get the tags for. This can be a Resource id url or a version-specific
		/// Resource url.</param>
		/// <returns>A list of Tags for the resource instance</returns>
		public Task<IEnumerable<Tag>> TagsAsync(Uri location)
		{
			if (location == null) throw Error.ArgumentNull("location");

			var collection = getCollectionFromLocation(location);
			var id = getIdFromLocation(location);
			var version = new ResourceIdentity(location).VersionId;

			return internalGetTagsAsync(collection, id, version);
		}

		public Task<IEnumerable<Tag>> TagsAsync(string location)
		{
			var identity = new ResourceIdentity(location);
			return internalGetTagsAsync(identity.Collection, identity.Id, identity.VersionId);
		}

		public Task<IEnumerable<Tag>> TagsAsync<TResource>(string id, string vid = null)
		{
			string collection = ModelInfo.GetResourceNameForType(typeof(TResource));
			return internalGetTagsAsync(collection, id, vid);
		}

		private async Task<IEnumerable<Tag>> internalGetTagsAsync(string collection, string id, string version)
		{
			RestUrl location = new RestUrl(this.Endpoint);

			if (collection == null)
				location = location.ServerTags();
			else
			{
				if (id == null)
					location = location.CollectionTags(collection);
				else
					location = location.ResourceTags(collection, id, version);
			}

			var req = createFhirRequest(location.Uri, "GET");
			var result = await doRequestAsync(req, HttpStatusCode.OK, resp => resp.BodyAsTagList());
			return result.Category;
		}

		/// <summary>
		/// Add one or more tags to a resource at a given location
		/// </summary>
		/// <param name="location">The url of the Resource to affix the tags to. This can be a Resource id url or a version-specific url</param>
		/// <param name="tags"></param>
		/// <remarks>Affixing tags to a resource (or version of the resource) is not considered an update, so does not create a new version.</remarks>
		public Task AffixTagsAsync(Uri location, IEnumerable<Tag> tags)
		{
			if (location == null) throw Error.ArgumentNull("location");
			if (tags == null) throw Error.ArgumentNull("tags");

			var collection = getCollectionFromLocation(location);
			var id = getIdFromLocation(location);
			var version = new ResourceIdentity(location).VersionId;

			var rl = new RestUrl(Endpoint).ResourceTags(collection, id, version);

			var req = createFhirRequest(rl.Uri, "POST");
			req.SetBody(new TagList(tags), PreferredFormat);

			return doRequestAsync(req, HttpStatusCode.OK, resp => true);
		}

		/// <summary>
		/// Remove one or more tags from a resource at a given location
		/// </summary>
		/// <param name="location">The url of the Resource to remove the tags from. This can be a Resource id url or a version-specific</param>
		/// <param name="tags">List of tags to be removed</param>
		/// <remarks>Removing tags to a resource (or version of the resource) is not considered an update, 
        /// so does not create a new version.</remarks>
		public Task DeleteTagsAsync(Uri location, IEnumerable<Tag> tags)
		{
			if (location == null) throw Error.ArgumentNull("location");
			if (tags == null) throw Error.ArgumentNull("tags");

			var collection = getCollectionFromLocation(location);
			var id = getIdFromLocation(location);
			var version = new ResourceIdentity(location).VersionId;

			var rl = new RestUrl(Endpoint).DeleteResourceTags(collection, id, version);

			var req = createFhirRequest(rl.Uri, "POST");
			req.SetBody(new TagList(tags), PreferredFormat);

			return doRequestAsync(req, new HttpStatusCode[] { HttpStatusCode.OK, HttpStatusCode.NoContent }, resp => true);
		}

		private Task<T> doRequestAsync<T>(FhirRequest request, HttpStatusCode success, Func<FhirResponse, T> onSuccess)
		{
			return doRequestAsync<T>(request, new HttpStatusCode[] { success }, onSuccess);
		}

		private async Task<T> doRequestAsync<T>(FhirRequest request, HttpStatusCode[] success, Func<FhirResponse, T> onSuccess)
		{
			request.UseFormatParameter = this.UseFormatParam;
			FhirResponse result = null;
			{
				try
				{
					HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync(PreferredFormat);
					result = FhirResponse.FromHttpWebResponse(response);
					response.Dispose();
				}
				catch (WebException ex)
				{
					result = FhirResponse.FromHttpWebResponse(ex.Response as HttpWebResponse);
				}
			}

			LastResponseDetails = result;

			if (success.Contains(result.Result))
				return onSuccess(result);
			else
			{
				// Try to parse the body as an OperationOutcome resource, but it is no
				// problem if it's something else, or there is no parseable body at all

				OperationOutcome outcome = null;

				try
				{
					outcome = result.BodyAsEntry<OperationOutcome>().Resource;
				}
				catch
				{
					// failed, so the body does not contain an OperationOutcome.
					// Put the raw body as a message in the OperationOutcome as a fallback scenario
					var body = result.BodyAsString();
					if (!String.IsNullOrEmpty(body))
						outcome = OperationOutcome.ForMessage(body);
				}

				if (outcome != null)
				{
					System.Diagnostics.Debug.WriteLine("------------------------------------------------------");
					if (outcome != null && outcome.Text != null && !string.IsNullOrEmpty(outcome.Text.Div))
					{
						System.Diagnostics.Debug.WriteLine(outcome.Text.Div);
						System.Diagnostics.Debug.WriteLine("------------------------------------------------------");
					}
					foreach (var issue in outcome.Issue)
					{
						System.Diagnostics.Debug.WriteLine("	" + issue.Details);
					}
					System.Diagnostics.Debug.WriteLine("------------------------------------------------------");

					throw new FhirOperationException("Operation failed with status code " + LastResponseDetails.Result, outcome);
				}
				else
				{
					throw new FhirOperationException("Operation failed with status code " + LastResponseDetails.Result);
				}
			}
		}
#endregion
#endif
	}

    public enum PageDirection
    {
        First,
        Previous,
        Next,
        Last
    }


    public delegate void BeforeRequestEventHandler(object sender, BeforeRequestEventArgs e);

    public class BeforeRequestEventArgs : EventArgs
    {
        public BeforeRequestEventArgs(HttpWebRequest request)
        {
            this.Request = request;
        }

        public HttpWebRequest Request { get; internal set; }
    }

    public delegate void AfterResponseEventHandler(object sender, AfterResponseEventArgs e);

    public class AfterResponseEventArgs : EventArgs
    {
        public AfterResponseEventArgs(WebResponse response)
        {
            this.Response = response;
        }

        public WebResponse Response { get; internal set; }
    }
}
