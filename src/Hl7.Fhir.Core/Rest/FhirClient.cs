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
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Rest;
using System.Threading;
using Hl7.Fhir.Introspection;
using System.Threading.Tasks;


namespace Hl7.Fhir.Rest
{
    public partial class FhirClient
    {
        private Requester _requester;

        /// <summary>
        /// Creates a new client using a default endpoint
        /// If the endpoint does not end with a slash (/), it will be added.
        /// </summary>
        /// <param name="endpoint">
        /// The URL of the server to connect to.<br/>
        /// If the trailing '/' is not present, then it will be appended automatically
        /// </param>
        /// <param name="verifyFhirVersion">
        /// If parameter is set to true the first time a request is made to the server a 
        /// conformance check will be made to check that the FHIR versions are compatible.
        /// When they are not compatible, a FhirException will be thrown.
        /// </param>
        public FhirClient(Uri endpoint, bool verifyFhirVersion = false)
        {
            if (endpoint == null) throw new ArgumentNullException("endpoint");

            if (!endpoint.OriginalString.EndsWith("/"))
                endpoint = new Uri(endpoint.OriginalString + "/");

            if (!endpoint.IsAbsoluteUri) throw new ArgumentException("endpoint", "Endpoint must be absolute");

            Endpoint = endpoint;
            _requester = new Requester(Endpoint);
            _requester.BeforeRequest = this.BeforeRequest;
            _requester.AfterResponse = this.AfterResponse;

            VerifyFhirVersion = verifyFhirVersion;
        }


        /// <summary>
        /// Creates a new client using a default endpoint
        /// If the endpoint does not end with a slash (/), it will be added.
        /// </summary>
        /// <param name="endpoint">
        /// The URL of the server to connect to.<br/>
        /// If the trailing '/' is not present, then it will be appended automatically
        /// </param>
        /// <param name="verifyFhirVersion">
        /// If parameter is set to true the first time a request is made to the server a 
        /// conformance check will be made to check that the FHIR versions are compatible.
        /// When they are not compatible, a FhirException will be thrown.
        /// </param>
        public FhirClient(string endpoint, bool verifyFhirVersion = false)
            : this(new Uri(endpoint), verifyFhirVersion)
        {
        }

        #region << Client Communication Defaults (PreferredFormat, UseFormatParam, Timeout, ReturnFullResource) >>
        public bool VerifyFhirVersion
        {
            get;
            set;
        }
        
        /// <summary>
        /// The preferred format of the content to be used when communicating with the FHIR server (XML or JSON)
        /// </summary>
        public ResourceFormat PreferredFormat
        {
            get     { return _requester.PreferredFormat; }
            set     { _requester.PreferredFormat = value; }
        }
        
        /// <summary>
        /// When passing the content preference, use the _format parameter instead of the request header
        /// </summary>
        public bool UseFormatParam 
        {
            get     { return _requester.UseFormatParameter; }
            set     { _requester.UseFormatParameter = value; }
        }

        /// <summary>
        /// The timeout (in milliseconds) to be used when making calls to the FHIR server
        /// </summary>
        public int Timeout
        {
            get { return _requester.Timeout; }
            set { _requester.Timeout = value; }
        }


        //private bool _returnFullResource = false;

        /// <summary>
        /// Should calls to Create, Update and transaction operations return the whole updated content?
        /// </summary>
        /// <remarks>Refer to specification section 2.1.0.5 (Managing Return Content)</remarks>
        public bool ReturnFullResource
        {
            get 
            {
                return _requester.Prefer == Prefer.ReturnRepresentation;
            }
            set 
            {
                _requester.Prefer = value==true ? Prefer.ReturnRepresentation : Prefer.ReturnMinimal; 
            }
        }


        /// <summary>
        /// The last transaction result that was executed on this connection to the FHIR server
        /// </summary>
        public Bundle.ResponseComponent LastResult
        {
            get { return _requester.LastResult != null ? _requester.LastResult.Response : null; }
        }

        public byte[] LastBody { get { return LastResult != null ? LastResult.GetBody() : null; } }
        public string LastBodyAsText { get { return LastResult != null ? LastResult.GetBodyAsText() : null; } }
        public Resource LastBodyAsResource { get { return _requester.LastResult != null ? _requester.LastResult.Resource : null; } }

        /// <summary>
        /// Returns the HttpWebRequest as it was last constructed to execute a call on the FhirClient
        /// </summary>
        public HttpWebRequest LastRequest { get { return _requester.LastRequest; } }

        /// <summary>
        /// Returns the HttpWebResponse as it was last received during a call on the FhirClient
        /// </summary>
        /// <remarks>Note that the FhirClient will have read the body data from the HttpWebResponse, so this is
        /// no longer available. Use LastBody, LastBodyAsText and LastBodyAsResource to get access to the received body (if any)</remarks>
        public HttpWebResponse LastResponse { get { return _requester.LastResponse; } }

        /// <summary>
        /// The default endpoint for use with operations that use discrete id/version parameters
        /// instead of explicit uri endpoints. This will always have a trailing "/"
        /// </summary>
        public Uri Endpoint
        {
            get;
            private set;
        }

        #endregion

        /// <summary>
        /// Fetches a typed resource from a FHIR resource endpoint.
        /// </summary>
        /// <param name="location">The url of the Resource to fetch. This can be a Resource id url or a version-specific
        /// Resource url.</param>
        /// <param name="ifNoneMatch">The (weak) ETag to use in a conditional read. Optional.</param>
        /// <param name="ifModifiedSince">Last modified since date in a conditional read. Optional. (refer to spec 2.1.0.5) If this is used, the client will throw an exception you need</param>
        /// <typeparam name="TResource">The type of resource to read. Resource or DomainResource is allowed if exact type is unknown</typeparam>
        /// <returns>
        /// The requested resource. This operation will throw an exception
        /// if the resource has been deleted or does not exist. 
        /// The specified may be relative or absolute, if it is an absolute
        /// url, it must reference an address within the endpoint.
        /// </returns>
        /// <remarks>Since ResourceLocation is a subclass of Uri, you may pass in ResourceLocations too.</remarks>
        /// <exception cref="FhirOperationException">This will occur if conditional request returns a status 304 and optionally an OperationOutcome</exception>
        public TResource Read<TResource>(Uri location, string ifNoneMatch=null, DateTimeOffset? ifModifiedSince=null) where TResource : Resource
        {
            if (location == null) throw Error.ArgumentNull("location");

            var id = verifyResourceIdentity(location, needId: true, needVid: false);
            Bundle tx;

            if (!id.HasVersion)
            {
                var ri = new TransactionBuilder(Endpoint).Read(id.ResourceType, id.Id, ifNoneMatch, ifModifiedSince);
                tx = ri.ToBundle();
            }
            else
            {
                tx = new TransactionBuilder(Endpoint).VRead(id.ResourceType, id.Id, id.VersionId).ToBundle();
            }

            return execute<TResource>(tx, HttpStatusCode.OK);
        }


        /// <summary>
        /// Fetches a typed resource from a FHIR resource endpoint.
        /// </summary>
        /// <param name="location">The url of the Resource to fetch as a string. This can be a Resource id url or a version-specific
        /// Resource url.</param>
        /// <param name="ifNoneMatch">The (weak) ETag to use in a conditional read. Optional.</param>
        /// <param name="ifModifiedSince">Last modified since date in a conditional read. Optional.</param>
        /// <typeparam name="TResource">The type of resource to read. Resource or DomainResource is allowed if exact type is unknown</typeparam>
        /// <returns>The requested resource</returns>
        /// <remarks>This operation will throw an exception
        /// if the resource has been deleted or does not exist. The specified may be relative or absolute, if it is an absolute
        /// url, it must reference an address within the endpoint.</remarks>
        public TResource Read<TResource>(string location, string ifNoneMatch = null, DateTimeOffset? ifModifiedSince = null) where TResource : Resource
        {
            return Read<TResource>(new Uri(location, UriKind.RelativeOrAbsolute), ifNoneMatch, ifModifiedSince);
        }


        /// <summary>
        /// Update (or create) a resource
        /// </summary>
        /// <param name="resource">The resource to update</param>
        /// <param name="versionAware">If true, asks the server to verify we are updating the latest version</param>
        /// <typeparam name="TResource">The type of resource that is being updated</typeparam>
        /// <returns>The body of the updated resource, unless ReturnFullResource is set to "false"</returns>
        /// <remarks>Throws an exception when the update failed, in particular when an update conflict is detected and the server returns a HTTP 409.
        /// If the resource does not yet exist - and the server allows client-assigned id's - a new resource with the given id will be
        /// created.</remarks>
        public TResource Update<TResource>(TResource resource, bool versionAware=false) where TResource : Resource
        {
            if (resource == null) throw Error.ArgumentNull("resource");
            if (resource.Id == null) throw Error.Argument("resource", "Resource needs a non-null Id to send the update to");

            var upd = new TransactionBuilder(Endpoint);

            if (versionAware && resource.HasVersionId)
                upd.Update(resource.Id, resource, ifMatch: resource.VersionId);
            else
                upd.Update(resource.Id, resource);

            return internalUpdate<TResource>(resource, upd.ToBundle());
        }


        /// <summary>
        /// Conditionally update (or create) a resource
        /// </summary>
        /// <param name="resource">The resource to update</param>
        /// <param name="condition">Criteria used to locate the resource to update</param>
        /// <param name="versionAware">If true, asks the server to verify we are updating the latest version</param>
        /// <typeparam name="TResource">The type of resource that is being updated</typeparam>
        /// <returns>The body of the updated resource, unless ReturnFullResource is set to "false"</returns>
        /// <remarks>Throws an exception when the update failed, in particular when an update conflict is detected and the server returns a HTTP 409.
        /// If the criteria passed in condition do not match a resource a new resource with a server assigned id will be created.</remarks>
        public TResource Update<TResource>(TResource resource, SearchParams condition, bool versionAware = false) where TResource : Resource
        {
            if (resource == null) throw Error.ArgumentNull("resource");
            if (condition == null) throw Error.ArgumentNull("condition");

            var upd = new TransactionBuilder(Endpoint);
                
            if (versionAware && resource.HasVersionId)
                upd.Update(condition, resource, ifMatch: resource.VersionId);
            else
                upd.Update(condition, resource);

            return internalUpdate<TResource>(resource, upd.ToBundle());
        }


        private TResource internalUpdate<TResource>(TResource resource, Bundle tx) where TResource : Resource
        {
            resource.ResourceBase = Endpoint;

            // This might be an update of a resource that doesn't yet exist, so accept a status Created too
            return execute<TResource>(tx, new[] { HttpStatusCode.Created, HttpStatusCode.OK });
        }


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

            var id = verifyResourceIdentity(location, needId: true, needVid: false);
            var tx = new TransactionBuilder(Endpoint).Delete(id.ResourceType, id.Id).ToBundle();

            execute<Resource>(tx, HttpStatusCode.NoContent);

            return;
        }

        public void Delete(string location)
        {
            Delete(new Uri(location, UriKind.Relative));
        }

        /// <summary>
        /// Delete a resource
        /// </summary>
        /// <param name="resource">The resource to delete</param>
        public void Delete(Resource resource)
        {
            if (resource == null) throw Error.ArgumentNull("entry");
            if (resource.Id == null) throw Error.Argument("entry", "Entry must have an id");

            Delete(resource.ResourceIdentity(Endpoint).WithoutVersion());
        }


        /// <summary>
        /// Conditionally delete a resource
        /// </summary>
        /// <param name="resourceType">The type of resource to delete</param>
        /// <param name="condition">Criteria to use to match the resource to delete.</param>
        public void Delete(string resourceType, SearchParams condition)
        {
            if (resourceType == null) throw Error.ArgumentNull("resourceType");
            if (condition == null) throw Error.ArgumentNull("condition");

            var tx = new TransactionBuilder(Endpoint).Delete(resourceType, condition).ToBundle();
            execute<Resource>(tx, HttpStatusCode.NoContent);

            return;
        }

        /// <summary>
        /// Create a resource on a FHIR endpoint
        /// </summary>
        /// <param name="resource">The resource instance to create</param>
        /// <returns>The resource as created on the server, or an exception if the create failed.</returns>
        /// <typeparam name="TResource">The type of resource to create</typeparam>
        public TResource Create<TResource>(TResource resource) where TResource : Resource
        {
            if (resource == null) throw Error.ArgumentNull("resource");

            var tx = new TransactionBuilder(Endpoint).Create(resource).ToBundle();

            return execute<TResource>(tx,new[] { HttpStatusCode.Created, HttpStatusCode.OK });
        }


        /// <summary>
        /// Conditionally Create a resource on a FHIR endpoint
        /// </summary>
        /// <param name="resource">The resource instance to create</param>
        /// <param name="condition">The criteria</param>
        /// <returns>The resource as created on the server, or an exception if the create failed.</returns>
        /// <typeparam name="TResource">The type of resource to create</typeparam>
        public TResource Create<TResource>(TResource resource, SearchParams condition) where TResource : Resource
        {
            if (resource == null) throw Error.ArgumentNull("resource");
            if (condition == null) throw Error.ArgumentNull("condition");

            var tx = new TransactionBuilder(Endpoint).Create(resource,condition).ToBundle();

            return execute<TResource>(tx, new[] { HttpStatusCode.Created, HttpStatusCode.OK });
        }

        /// <summary>
        /// Get a conformance statement for the system
        /// </summary>
        /// <returns>A Conformance resource. Throws an exception if the operation failed.</returns>
        public Conformance Conformance()
        {
            var tx = new TransactionBuilder(Endpoint).Conformance().ToBundle();          
            return execute<Conformance>(tx, HttpStatusCode.OK);
        }

       
        /// <summary>
        /// Retrieve the version history for a specific resource type
        /// </summary>
        /// <param name="resourceType">The type of Resource to get the history for</param>
        /// <param name="since">Optional. Returns only changes after the given date</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="summary">Optional. Asks the server to only provide the fields defined for the summary</param>        
        /// <returns>A bundle with the history for the indicated instance, may contain both 
        /// ResourceEntries and DeletedEntries.</returns>
	    public Bundle TypeHistory(string resourceType, DateTimeOffset? since = null, int? pageSize = null, bool summary = false)
        {          
            return internalHistory(resourceType, null, since, pageSize, summary);
        }

        /// <summary>
        /// Retrieve the version history for a specific resource type
        /// </summary>
        /// <param name="since">Optional. Returns only changes after the given date</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="summary">Optional. Asks the server to only provide the fields defined for the summary</param>
        /// <typeparam name="TResource">The type of Resource to get the history for</typeparam>
        /// <returns>A bundle with the history for the indicated instance, may contain both 
        /// ResourceEntries and DeletedEntries.</returns>
        public Bundle TypeHistory<TResource>(DateTimeOffset? since = null, int? pageSize = null, bool summary = false) where TResource : Resource, new()
        {
            string collection = typeof(TResource).GetCollectionName();
            return internalHistory(collection, null, since, pageSize, summary);
        }

        /// <summary>
        /// Retrieve the version history for a resource at a given location
        /// </summary>
        /// <param name="location">The address of the resource to get the history for</param>
        /// <param name="since">Optional. Returns only changes after the given date</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="summary">Optional. Asks the server to only provide the fields defined for the summary</param>
        /// <returns>A bundle with the history for the indicated instance, may contain both 
        /// ResourceEntries and DeletedEntries.</returns>
        public Bundle History(Uri location, DateTimeOffset? since = null, int? pageSize = null, bool summary = false)
        {
            if (location == null) throw Error.ArgumentNull("location");

            var id = verifyResourceIdentity(location, needId: true, needVid: false);
            return internalHistory(id.ResourceType, id.Id, since, pageSize, summary);
        }


        public Bundle History(string location, DateTimeOffset? since = null, int? pageSize = null, bool summary = false)
        {
            return History(new Uri(location, UriKind.Relative), since, pageSize, summary);
        }


        /// <summary>
        /// Retrieve the full version history of the server
        /// </summary>
        /// <param name="since">Optional. Returns only changes after the given date</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="summary">Indicates whether the returned resources should just contain the minimal set of elements</param>
        /// <returns>A bundle with the history for the indicated instance, may contain both 
        /// ResourceEntries and DeletedEntries.</returns>
        public Bundle WholeSystemHistory(DateTimeOffset? since = null, int? pageSize = null, bool summary = false)
        {
            return internalHistory(null, null, since, pageSize, summary);
        }

        private Bundle internalHistory(string resourceType = null, string id = null, DateTimeOffset? since = null, int? pageSize = null, bool summary = false)
        {
            TransactionBuilder history;

            if(resourceType == null)
                history = new TransactionBuilder(Endpoint).ServerHistory(summary,pageSize,since);
            else if(id == null)
                history = new TransactionBuilder(Endpoint).CollectionHistory(resourceType, summary,pageSize,since);
            else
                history = new TransactionBuilder(Endpoint).ResourceHistory(resourceType,id, summary,pageSize,since);

            return execute<Bundle>(history.ToBundle(), HttpStatusCode.OK);
        }

        
        /// <summary>
        /// Send a set of creates, updates and deletes to the server to be processed in one transaction
        /// </summary>
        /// <param name="bundle">The bundled creates, updates and deleted</param>
        /// <returns>A bundle as returned by the server after it has processed the transaction, or null
        /// if an error occurred.</returns>
        public Bundle Transaction(Bundle bundle)
        {
            if (bundle == null) throw new ArgumentNullException("bundle");

            var tx = new TransactionBuilder(Endpoint).Transaction(bundle).ToBundle();
            return execute<Bundle>(tx, HttpStatusCode.OK);
        }


        public Resource WholeSystemOperation(string operationName, Parameters parameters = null, bool useGet = false)
        {
            if (operationName == null) throw Error.ArgumentNull("operationName");
            return internalOperation(operationName, parameters: parameters, useGet: useGet);
        }

        public Resource TypeOperation<TResource>(string operationName, Parameters parameters = null, bool useGet = false) where TResource : Resource
        {
            if (operationName == null) throw Error.ArgumentNull("operationName");

            var typeName = ModelInfo.GetResourceNameForType(typeof(TResource));
            return TypeOperation(operationName, typeName, parameters, useGet: useGet);
        }

        public Resource TypeOperation(string operationName, string typeName, Parameters parameters = null, bool useGet = false)
        {
            if (operationName == null) throw Error.ArgumentNull("operationName");
            if (typeName == null) throw Error.ArgumentNull("typeName");

            return internalOperation(operationName, typeName, parameters: parameters, useGet: useGet);
        }

        public Resource InstanceOperation(Uri location, string operationName, Parameters parameters = null, bool useGet = false)
        {
            if (location == null) throw Error.ArgumentNull("location");
            if (operationName == null) throw Error.ArgumentNull("operationName");

            var id = verifyResourceIdentity(location, needId: true, needVid: false);

            return internalOperation(operationName, id.ResourceType, id.Id, id.VersionId, parameters, useGet);
        }

        public Resource Operation(Uri location, string operationName, Parameters parameters = null, bool useGet = false)
        {
            if (location == null) throw Error.ArgumentNull("location");
            if (operationName == null) throw Error.ArgumentNull("operationName");

            var tx = new TransactionBuilder(Endpoint).EndpointOperation(new RestUrl(location), operationName, parameters, useGet).ToBundle();

            return execute<Resource>(tx, HttpStatusCode.OK);
        }

        public Resource Operation(Uri operation, Parameters parameters = null, bool useGet = false)
        {
            if (operation == null) throw Error.ArgumentNull("operation");

            var tx = new TransactionBuilder(Endpoint).EndpointOperation(new RestUrl(operation), parameters, useGet).ToBundle();

            return execute<Resource>(tx, HttpStatusCode.OK);
        }

        private Resource internalOperation(string operationName, string type = null, string id = null, string vid = null, Parameters parameters = null, bool useGet = false)
        {
            // Brian: Not sure why we would create this parameters object as empty.
            //        I would imagine that a null parameters object is different to an empty one?
            // if (parameters == null) parameters = new Parameters();

            Bundle tx;

            if (type == null)
                tx = new TransactionBuilder(Endpoint).ServerOperation(operationName, parameters, useGet).ToBundle();
            else if (id == null)
                tx = new TransactionBuilder(Endpoint).TypeOperation(type, operationName, parameters, useGet).ToBundle();
            else
                tx = new TransactionBuilder(Endpoint).ResourceOperation(type, id, vid, operationName, parameters, useGet).ToBundle();

            return execute<Resource>(tx, HttpStatusCode.OK);
        }





        /// <summary>
        /// Invoke a general GET on the server. If the operation fails, then this method will throw an exception
        /// </summary>
        /// <param name="url">A relative or absolute url. If the url is absolute, it has to be located within the endpoint of the client.</param>
        /// <returns>A resource that is the outcome of the operation. The type depends on the definition of the operation at the given url</returns>
        /// <remarks>parameters to the method are simple, and are in the URL, and this is a GET operation</remarks>
        public Resource Get(Uri url)
        {
            if (url == null) throw Error.ArgumentNull("url");

            var tx = new TransactionBuilder(Endpoint).Get(url).ToBundle();
            return execute<Resource>(tx, HttpStatusCode.OK);
        }

        /// <summary>
        /// Invoke a general GET on the server. If the operation fails, then this method will throw an exception
        /// </summary>
        /// <param name="url">A relative or absolute url. If the url is absolute, it has to be located within the endpoint of the client.</param>
        /// <returns>A resource that is the outcome of the operation. The type depends on the definition of the operation at the given url</returns>
        /// <remarks>parameters to the method are simple, and are in the URL, and this is a GET operation</remarks>
        public Resource Get(string url)
        {
            if (url == null) throw Error.ArgumentNull("url");

            return Get(new Uri(url, UriKind.RelativeOrAbsolute));
        }



   
        ///// <summary>
        ///// Get all meta known by the FHIR server
        ///// </summary>
        ///// <returns>A ResourceMetaComponent with all tags, profiles etc. known by the system</returns>
        //public Meta WholeSystemMeta()
        //{
        //    return internalGetMeta(null, null, null);
        //}

        ///// <summary>
        ///// Get all meta known by the FHIR server for a given resource type
        ///// </summary>
        ///// <returns>A ResourceMetaComponent with all tags, profiles etc. known by the system for the given type</returns>
        //public Meta TypeMeta<TResource>() where TResource : Resource
        //{
        //    var typeName = ModelInfo.GetResourceNameForType(typeof(TResource));
        //    return internalGetMeta(typeName, null, null);
        //}

        ///// <summary>
        ///// Get all meta known by the FHIR server for a given resource type
        ///// </summary>
        ///// <returns>A ResourceMetaComponent with all tags, profiles etc. known by the system for the given type</returns>
        //public Meta TypeMeta(string type)
        //{
        //    if (type == null) throw Error.ArgumentNull("type");

        //    return internalGetMeta(type, null, null);
        //}

        ///// <summary>
        ///// Get the meta for a resource (or resource version) at a given location
        ///// </summary>
        ///// <param name="location">The url of the Resource to get the meta for. This can be a Resource id url or a version-specific
        ///// Resource url, and may be relative.</param>
        ///// <returns>A ResourceMetaComponent with all tags, profiles etc. known by the system for the given instance</returns>
        //public Meta Meta(Uri location)
        //{
        //    if (location == null) throw Error.ArgumentNull("location");

        //    var collection = getResourceTypeFromLocation(location);
        //    var id = getIdFromLocation(location);
        //    var version = new ResourceIdentity(location).VersionId;

        //    return internalGetMeta(collection, id, version);
        //}

        ///// <summary>
        ///// Get the meta for a resource (or resource version) at a given location
        ///// </summary>
        ///// <param name="location">The url of the Resource to get the meta for. This can be a Resource id url or a version-specific
        ///// Resource url, and may be relative.</param>
        ///// <returns>A ResourceMetaComponent with all tags, profiles etc. known by the system for the given instance</returns>
        //public Meta Meta(string location)
        //{
        //    var identity = new ResourceIdentity(location);
        //    return Meta(identity);
        //}


        //private Meta internalGetMeta(string collection, string id, string version)
        //{
        //    RestUrl location = new RestUrl(this.Endpoint);

        //    if (collection == null)
        //        location = location.ServerTags();
        //    else if(id == null)
        //        location = location.CollectionTags(collection);
        //    else
        //        location = location.ResourceTags(collection, id, version);

        //    var req = createFhirRequest(location.Uri, "GET");
        //    return doRequest(req, HttpStatusCode.OK, resp => resp.BodyAsMeta());
        //}


        ///// <summary>
        ///// Add meta to a resource at a given location
        ///// </summary>
        ///// <param name="location">The url of the Resource to affix the tags to. This can be a Resource id url or a version-specific id</param>
        ///// <param name="meta">Meta to add to the resource</param>
        ///// <remarks>Affixing mea to a resource (or version of the resource) is not considered an update, so does 
        ///// not create a new version.</remarks>
        //public void AffixMeta(Uri location, Meta meta)
        //{
        //    if (location == null) throw Error.ArgumentNull("location");
        //    if (meta == null) throw Error.ArgumentNull("meta");

        //    var collection = getResourceTypeFromLocation(location);
        //    var id = getIdFromLocation(location);
        //    var version = new ResourceIdentity(location).VersionId;

        //    var rl = new RestUrl(Endpoint).ResourceTags(collection, id, version);

        //    var req = createFhirRequest(rl.Uri,"POST");
        //    req.SetMeta(meta, PreferredFormat);
        //    doRequest(req, HttpStatusCode.OK, resp => true);
        //}



        ///// <summary>
        ///// Add meta to a resource at a given location
        ///// </summary>
        ///// <param name="location">The url of the Resource to affix the meta to. This can be a Resource id url or a version-specific id</param>
        ///// <param name="meta">Meta to add to the resource</param>
        ///// <remarks>Affixing meta to a resource (or version of the resource) is not considered an update, so does 
        ///// not create a new version.</remarks>
        //public void AffixMeta(string location, Meta meta)
        //{
        //    if (location == null) throw Error.ArgumentNull("location");
        //    if (meta == null) throw Error.ArgumentNull("meta");

        //    AffixMeta(new ResourceIdentity(location),meta);
        //}


        ///// <summary>
        ///// Remove meta from a resource at a given location
        ///// </summary>
        ///// <param name="location">The url of the Resource to remove the meta from. This can be a Resource id url or a version-specific</param>
        ///// <param name="tags">Meta to delete</param>
        ///// <remarks>Removing meta from a resource (or version of the resource) is not considered an update, 
        ///// so does not create a new version.</remarks>
        //public void DeleteMeta(Uri location, Meta meta)
        //{
        //    if (location == null) throw Error.ArgumentNull("location");
        //    if (meta == null) throw Error.ArgumentNull("meta");

        //    var collection = getResourceTypeFromLocation(location);
        //    var id = getIdFromLocation(location);
        //    var version = new ResourceIdentity(location).VersionId;

        //    var rl = new RestUrl(Endpoint).DeleteResourceTags(collection, id, version);

        //    var req = createFhirRequest(rl.Uri, "POST");
        //    req.SetMeta(meta, PreferredFormat);

        //    doRequest(req, new HttpStatusCode[] { HttpStatusCode.OK, HttpStatusCode.NoContent }, resp => true);
        //}


        private ResourceIdentity verifyResourceIdentity(Uri location, bool needId, bool needVid)
        {
            var result = new ResourceIdentity(location);

            if (result.ResourceType == null) throw Error.Argument("location", "Must be a FHIR REST url containing the resource type in its path");
            if (needId && result.Id == null) throw Error.Argument("location", "Must be a FHIR REST url containing the logical id in its path");
            if (needVid && !result.HasVersion) throw Error.Argument("location", "Must be a FHIR REST url containing the version id in its path");

            return result;
        }


        // TODO: Depending on type of response, update identity & always update lastupdated?

        private void updateIdentity(Resource resource, ResourceIdentity identity)
        {
            if (resource.Meta == null) resource.Meta = new Meta();

            if (resource.Id == null)
            {
                resource.Id = identity.Id;
                resource.VersionId = identity.VersionId;
            }
        }


        private void setResourceBase(Resource resource, string baseUri)
        {
            resource.ResourceBase = new Uri(baseUri);

            if (resource is Bundle)
            {
                var bundle = resource as Bundle;
                foreach (var entry in bundle.Entry.Where(e => e.Resource != null))
                    entry.Resource.ResourceBase = new Uri(baseUri, UriKind.RelativeOrAbsolute);
            }
        }


        /// <summary>
        /// Called just before the Http call is done
        /// </summary>
        public event BeforeRequestEventHandler OnBeforeRequest;

        /// <summary>
        /// Called just after the response was received
        /// </summary>
        public event AfterResponseEventHandler OnAfterResponse;

        /// <summary>
        /// Inspect or modify the HttpWebRequest just before the FhirClient issues a call to the server
        /// </summary>
        /// <param name="rawRequest">The request as it is about to be sent to the server</param>
        /// <param name="body">The data in the body of the request as it is about to be sent to the server</param>
        protected virtual void BeforeRequest(HttpWebRequest rawRequest, byte[] body) 
        {
            // Default implementation: call event
            if (OnBeforeRequest != null) OnBeforeRequest(this,new BeforeRequestEventArgs(rawRequest, body));
        }

        /// <summary>
        /// Inspect the HttpWebResponse as it came back from the server
        /// </summary>
        /// <remarks>You cannot read the body from the HttpWebResponse, since it has
        /// already been read by the framework. Use the body parameter instead.</remarks>
        protected virtual void AfterResponse(HttpWebResponse webResponse, byte[] body)
        {
            // Default implementation: call event
            if (OnAfterResponse != null) OnAfterResponse(this,new AfterResponseEventArgs(webResponse, body));
        }


        private TResource execute<TResource>(Bundle tx, HttpStatusCode expect) where TResource : Resource
        {
            return execute<TResource>(tx, new[] { expect });
        }

        private TResource execute<TResource>(Bundle tx, IEnumerable<HttpStatusCode> expect) where TResource : Resource
        {
            verifyServerVersion();

            var request = tx.Entry[0];
            var response = _requester.Execute(request);

            if (!expect.Select(sc => ((int)sc).ToString()).Contains(response.Response.Status))
            {
                HttpStatusCode code;
                Enum.TryParse<HttpStatusCode>(response.Response.Status, out code);
                throw new FhirOperationException("Operation concluded successfully, but the return status {0} was unexpected".FormatWith(response.Response.Status), code);
            }

            Resource result;

            // Special feature: if ReturnFullResource was requested (using the Prefer header), but the server did not return the resource
            // (or it returned an OperationOutcome) - explicitly go out to the server to get the resource and return it. 
            // This behavior is only valid for PUT and POST requests, where the server may device whether or not to return the full body of the alterend resource.
            var noRealBody = response.Resource == null || response.Resource is OperationOutcome;
            if (noRealBody && isPostOrPut(request) 
                && ReturnFullResource && response.Response.Location != null
                && new ResourceIdentity(response.Response.Location).IsRestResourceIdentity()) // Check that it isn't an operation too
            {
                result = Get(response.Response.Location);
            }
            else
                result = response.Resource;

            if (result == null) return null;
            
            // We have a success code (2xx), we have a body, but the body may not be of the type we expect.
            if (!(result is TResource))
            {
                // If this is an operationoutcome, that may still be allright. Keep the OperationOutcome in 
                // the LastResult, and return null as the result. Otherwise, throw.
                if (result is OperationOutcome)
                    return null;

                var message = String.Format("Operation {0} on {1} expected a body of type {2} but a {3} was returned", response.Request.Method,
                    response.Request.Url, typeof(TResource).Name, result.GetType().Name);
                throw new FhirOperationException(message, _requester.LastResponse.StatusCode);
            }
            else
                return result as TResource;
        }

        private bool isPostOrPut(Bundle.EntryComponent interaction)
        {
            var method = interaction.Request.Method;
            return method == Bundle.HTTPVerb.POST || method == Bundle.HTTPVerb.PUT;
        }


        private bool versionChecked = false;

        private void verifyServerVersion()
        {
            if (!VerifyFhirVersion) return;

            if (versionChecked) return;
            versionChecked = true;      // So we can now start calling Conformance() without getting into a loop

            Conformance conf = null;
            try
            {
                conf = Conformance();
            }
            catch (FormatException)
            {
                // Mmmm...cannot even read the body. Probably not so good.
                throw Error.NotSupported("Cannot read the conformance statement of the server to verify FHIR version compatibility");
            }

            if (!conf.FhirVersion.StartsWith(ModelInfo.Version))
            {
                throw Error.NotSupported("This client support FHIR version {0}, but the server uses version {1}".FormatWith(ModelInfo.Version, conf.FhirVersion));
            }
        }


#if (PORTABLE45 || NET45) && BRIAN
#region << Async operations >>


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
		public async Task<ValidateAsyncResult> TryValidateUpdateAsync<TResource>(TResource entry) where TResource : Resource, new()
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

			var collection = typeof(TResource).GetCollectionName();
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


#endregion
#endif
    }


    public delegate void BeforeRequestEventHandler(object sender, BeforeRequestEventArgs e);

    public class BeforeRequestEventArgs : EventArgs
    {
        public BeforeRequestEventArgs(HttpWebRequest rawRequest, byte[] body)
        {
            this.RawRequest = rawRequest;
            this.Body = body;
        }

        public HttpWebRequest RawRequest { get; internal set; }
        public byte[] Body { get; internal set; }
    }

    public delegate void AfterResponseEventHandler(object sender, AfterResponseEventArgs e);

    public class AfterResponseEventArgs : EventArgs
    {
        public AfterResponseEventArgs(HttpWebResponse webResponse, byte[] body)
        {
            this.RawResponse = webResponse;
            this.Body = body;
        }

        public HttpWebResponse RawResponse { get; internal set; }
        public byte[] Body { get; internal set; }
    }
}
