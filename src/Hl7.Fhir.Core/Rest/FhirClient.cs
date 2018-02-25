/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace Hl7.Fhir.Rest
{
    public abstract partial class FhirClient<TBundle, TMetadata, TOperationOutcome> : IFhirClient<TBundle, TMetadata>
        where TBundle : Resource, IBundle
        where TMetadata : Resource, IMetadata
        where TOperationOutcome : Resource
    {
        private Requester<TOperationOutcome> _requester;

        public FhirClient(Uri endpoint, string fhirVersion, Func<Exception, TOperationOutcome> operationOutcomeFromException, Func<byte[], string, Resource> makeBinaryResource, bool verifyFhirVersion)
        {
            if (endpoint == null) throw new ArgumentNullException("endpoint");

            if (!endpoint.OriginalString.EndsWith("/"))
                endpoint = new Uri(endpoint.OriginalString + "/");

            if (!endpoint.IsAbsoluteUri) throw new ArgumentException("endpoint", "Endpoint must be absolute");

            Endpoint = endpoint;

            _requester = new Requester<TOperationOutcome>(Endpoint, fhirVersion, operationOutcomeFromException, makeBinaryResource)
            {
                BeforeRequest = this.BeforeRequest,
                AfterResponse = this.AfterResponse
            };

            VerifyFhirVersion = verifyFhirVersion;
        }

        public abstract string GetFhirTypeNameForType(Type type);

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
            get => _requester.Prefer == Prefer.ReturnRepresentation;
            set => _requester.Prefer = value ? Prefer.ReturnRepresentation : Prefer.ReturnMinimal;
        }

#if NET_COMPRESSION
        /// <summary>
        /// This will do 2 things:
        /// 1. Add the header Accept-Encoding: gzip, deflate
        /// 2. decompress any responses that have Content-Encoding: gzip (or deflate)
        /// </summary>
        public bool PreferCompressedResponses 
        {
            get { return _requester.PreferCompressedResponses; }
            set { _requester.PreferCompressedResponses = value; }
        }
        /// <summary>
        /// Compress any Request bodies 
        /// (warning, if a server does not handle compressed requests you will get a 415 response)
        /// </summary>
        public bool CompressRequestBody
        {
            get { return _requester.CompressRequestBody; }
            set { _requester.CompressRequestBody = value; }
        }
#endif


        /// <summary>
        /// The last transaction result that was executed on this connection to the FHIR server
        /// </summary>
        public Response LastResult => _requester.LastResult;

        public ParserSettings ParserSettings
        {
            get { return _requester.ParserSettings;  }
            set { _requester.ParserSettings = value;  }
        }


        public byte[] LastBody => LastResult?.Body;
        public string LastBodyAsText => LastResult?.GetBodyAsText();
        public Resource LastBodyAsResource => _requester.LastResult?.Resource;

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

        #region Read

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
        public Task<TResource> ReadAsync<TResource>(Uri location, string ifNoneMatch=null, DateTimeOffset? ifModifiedSince=null) where TResource : Resource
        {
            if (location == null) throw Error.ArgumentNull(nameof(location));

            var id = verifyResourceIdentity(location, needId: true, needVid: false);
            Request request;

            if (!id.HasVersion)
            {
                var ri = new RequestsBuilder(Endpoint).Read(id.ResourceType, id.Id, ifNoneMatch, ifModifiedSince);
                request = ri.ToRequest();
            }
            else
            {
                request = new RequestsBuilder(Endpoint).VRead(id.ResourceType, id.Id, id.VersionId).ToRequest();
            }

            return executeAsync<TResource>(request, HttpStatusCode.OK);
        }
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
        public TResource Read<TResource>(Uri location, string ifNoneMatch = null,
            DateTimeOffset? ifModifiedSince = null) where TResource : Resource
        {
            return ReadAsync<TResource>(location, ifNoneMatch, ifModifiedSince).WaitResult();
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
        public Task<TResource> ReadAsync<TResource>(string location, string ifNoneMatch = null, DateTimeOffset? ifModifiedSince = null) where TResource : Resource
        {
            return ReadAsync<TResource>(new Uri(location, UriKind.RelativeOrAbsolute), ifNoneMatch, ifModifiedSince);
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
        public TResource Read<TResource>(string location, string ifNoneMatch = null,
            DateTimeOffset? ifModifiedSince = null) where TResource : Resource
        {
            return ReadAsync<TResource>(location, ifNoneMatch, ifModifiedSince).WaitResult();
        }

        #endregion

        #region Refresh

        /// <summary>
        /// Refreshes the data in the resource passed as an argument by re-reading it from the server
        /// </summary>
        /// <typeparam name="TResource"></typeparam>
        /// <param name="current">The resource for which you want to get the most recent version.</param>
        /// <returns>A new instance of the resource, containing the most up-to-date data</returns>
        /// <remarks>This function will not overwrite the argument with new data, rather it will return a new instance
        /// which will have the newest data, leaving the argument intact.</remarks>
        public Task<TResource> RefreshAsync<TResource>(TResource current) where TResource : Resource
        {
            if (current == null) throw Error.ArgumentNull(nameof(current));

            return ReadAsync<TResource>(ResourceIdentity.Build(current.TypeName, current.Id));
        }
        /// <summary>
        /// Refreshes the data in the resource passed as an argument by re-reading it from the server
        /// </summary>
        /// <typeparam name="TResource"></typeparam>
        /// <param name="current">The resource for which you want to get the most recent version.</param>
        /// <returns>A new instance of the resource, containing the most up-to-date data</returns>
        /// <remarks>This function will not overwrite the argument with new data, rather it will return a new instance
        /// which will have the newest data, leaving the argument intact.</remarks>
        public TResource Refresh<TResource>(TResource current) where TResource : Resource
        {
            return RefreshAsync<TResource>(current).WaitResult();
        }

        #endregion

        #region Update

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
        public Task<TResource> UpdateAsync<TResource>(TResource resource, bool versionAware=false) where TResource : Resource
        {
            if (resource == null) throw Error.ArgumentNull(nameof(resource));
            if (resource.Id == null) throw Error.Argument(nameof(resource), "Resource needs a non-null Id to send the update to");

            var upd = new RequestsBuilder(Endpoint);

            if (versionAware && resource.HasVersionId)
                upd.Update(resource.Id, resource, versionId: resource.VersionId);
            else
                upd.Update(resource.Id, resource);

            return internalUpdateAsync<TResource>(resource, upd.ToRequest());
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
        public TResource Update<TResource>(TResource resource, bool versionAware = false) where TResource : Resource
        {
            return UpdateAsync<TResource>(resource, versionAware).WaitResult();
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
        public Task<TResource> UpdateAsync<TResource>(TResource resource, SearchParams condition, bool versionAware = false) where TResource : Resource
        {
            if (resource == null) throw Error.ArgumentNull(nameof(resource));
            if (condition == null) throw Error.ArgumentNull(nameof(condition));

            var upd = new RequestsBuilder(Endpoint);
                
            if (versionAware && resource.HasVersionId)
                upd.Update(condition, resource, versionId: resource.VersionId);
            else
                upd.Update(condition, resource);

            return internalUpdateAsync(resource, upd.ToRequest());
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
        public TResource Update<TResource>(TResource resource, SearchParams condition, bool versionAware = false)
            where TResource : Resource
        {
            return UpdateAsync(resource, condition, versionAware).WaitResult();
        }
        private Task<TResource> internalUpdateAsync<TResource>(TResource resource, Request request) where TResource : Resource
        {
            resource.ResourceBase = Endpoint;

            // This might be an update of a resource that doesn't yet exist, so accept a status Created too
            return executeAsync<TResource>(request, new[] { HttpStatusCode.Created, HttpStatusCode.OK });
        }
        private TResource internalUpdate<TResource>(TResource resource, Request request) where TResource : Resource
        {
            return internalUpdateAsync(resource, request).WaitResult();
        }
        #endregion

        #region Delete

        /// <summary>
        /// Delete a resource at the given endpoint.
        /// </summary>
        /// <param name="location">endpoint of the resource to delete</param>
        /// <returns>Throws an exception when the delete failed, though this might
        /// just mean the server returned 404 (the resource didn't exist before) or 410 (the resource was
        /// already deleted).</returns>
        public async Task DeleteAsync(Uri location)
        {
            if (location == null) throw Error.ArgumentNull(nameof(location));

            var id = verifyResourceIdentity(location, needId: true, needVid: false);
            var tx = new RequestsBuilder(Endpoint).Delete(id.ResourceType, id.Id).ToRequest();

            await executeAsync<Resource>(tx, new[] { HttpStatusCode.OK, HttpStatusCode.NoContent }).ConfigureAwait(false);
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
            DeleteAsync(location).WaitNoResult();
        }
        /// <summary>
        /// Delete a resource at the given endpoint.
        /// </summary>
        /// <param name="location">endpoint of the resource to delete</param>
        /// <returns>Throws an exception when the delete failed, though this might
        /// just mean the server returned 404 (the resource didn't exist before) or 410 (the resource was
        /// already deleted).</returns>
        public Task DeleteAsync(string location)
        {
            return DeleteAsync(new Uri(location, UriKind.Relative));
        }
        /// <summary>
        /// Delete a resource at the given endpoint.
        /// </summary>
        /// <param name="location">endpoint of the resource to delete</param>
        /// <returns>Throws an exception when the delete failed, though this might
        /// just mean the server returned 404 (the resource didn't exist before) or 410 (the resource was
        /// already deleted).</returns>
        public void Delete(string location)
        {
            DeleteAsync(location).WaitNoResult();
        }


        /// <summary>
        /// Delete a resource
        /// </summary>
        /// <param name="resource">The resource to delete</param>
        public async Task DeleteAsync(Resource resource)
        {
            if (resource == null) throw Error.ArgumentNull(nameof(resource));
            if (resource.Id == null) throw Error.Argument(nameof(resource), "Entry must have an id");

            await DeleteAsync(resource.ResourceIdentity(Endpoint).WithoutVersion()).ConfigureAwait(false);
        }
        /// <summary>
        /// Delete a resource
        /// </summary>
        /// <param name="resource">The resource to delete</param>
        public void Delete(Resource resource)
        {
            DeleteAsync(resource).WaitNoResult();
        }

        /// <summary>
        /// Conditionally delete a resource
        /// </summary>
        /// <param name="resourceType">The type of resource to delete</param>
        /// <param name="condition">Criteria to use to match the resource to delete.</param>
        public async Task DeleteAsync(string resourceType, SearchParams condition)
        {
            if (resourceType == null) throw Error.ArgumentNull(nameof(resourceType));
            if (condition == null) throw Error.ArgumentNull(nameof(condition));

            var tx = new RequestsBuilder(Endpoint).Delete(resourceType, condition).ToRequest();
            await executeAsync<Resource>(tx,new[]{ HttpStatusCode.OK, HttpStatusCode.NoContent}).ConfigureAwait(false);
        }
        /// <summary>
        /// Conditionally delete a resource
        /// </summary>
        /// <param name="resourceType">The type of resource to delete</param>
        /// <param name="condition">Criteria to use to match the resource to delete.</param>
        public void Delete(string resourceType, SearchParams condition)
        {
            DeleteAsync(resourceType, condition).WaitNoResult();
        }

        #endregion
        
        #region Create
        
        /// <summary>
        /// Create a resource on a FHIR endpoint
        /// </summary>
        /// <param name="resource">The resource instance to create</param>
        /// <returns>The resource as created on the server, or an exception if the create failed.</returns>
        /// <typeparam name="TResource">The type of resource to create</typeparam>
        public Task<TResource> CreateAsync<TResource>(TResource resource) where TResource : Resource
        {
            if (resource == null) throw Error.ArgumentNull(nameof(resource));

            var request = new RequestsBuilder(Endpoint).Create(resource).ToRequest();

            return executeAsync<TResource>(request,new[] { HttpStatusCode.Created, HttpStatusCode.OK });
        }
        /// <summary>
        /// Create a resource on a FHIR endpoint
        /// </summary>
        /// <param name="resource">The resource instance to create</param>
        /// <returns>The resource as created on the server, or an exception if the create failed.</returns>
        /// <typeparam name="TResource">The type of resource to create</typeparam>
        public TResource Create<TResource>(TResource resource) where TResource : Resource
        {
            return CreateAsync(resource).WaitResult();
        }

        /// <summary>
        /// Conditionally Create a resource on a FHIR endpoint
        /// </summary>
        /// <param name="resource">The resource instance to create</param>
        /// <param name="condition">The criteria</param>
        /// <returns>The resource as created on the server, or an exception if the create failed.</returns>
        /// <typeparam name="TResource">The type of resource to create</typeparam>
        public Task<TResource> CreateAsync<TResource>(TResource resource, SearchParams condition) where TResource : Resource
        {
            if (resource == null) throw Error.ArgumentNull(nameof(resource));
            if (condition == null) throw Error.ArgumentNull(nameof(condition));

            var tx = new RequestsBuilder(Endpoint).Create(resource,condition).ToRequest();

            return executeAsync<TResource>(tx, new[] { HttpStatusCode.Created, HttpStatusCode.OK });
        }
        public TResource Create<TResource>(TResource resource, SearchParams condition) where TResource : Resource
        {
            return CreateAsync(resource, condition).WaitResult();
        }
        
        #endregion
        
        #region Metadata

        /// <summary>
        /// Get the system metadata
        /// </summary>
        /// <returns>A Conformance or CapabilityStatement resource. Throws an exception if the operation failed.</returns>
        public Task<TMetadata> MetadataAsync(SummaryType? summary = null)
        {
            var tx = new RequestsBuilder(Endpoint).GetMetadata(summary).ToRequest();
            return executeAsync<TMetadata>(tx, HttpStatusCode.OK);
        }

        public TMetadata Metadata(SummaryType? summary = null)
        {
            return MetadataAsync(summary).WaitResult();
        }

        #endregion

        #region History

        /// <summary>
        /// Retrieve the version history for a specific resource type
        /// </summary>
        /// <param name="resourceType">The type of Resource to get the history for</param>
        /// <param name="since">Optional. Returns only changes after the given date</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="summary">Optional. Asks the server to only provide the fields defined for the summary</param>        
        /// <returns>A bundle with the history for the indicated instance, may contain both 
        /// ResourceEntries and DeletedEntries.</returns>
        public Task<TBundle> TypeHistoryAsync(string resourceType, DateTimeOffset? since = null, int? pageSize = null, SummaryType summary = SummaryType.False)
        {          
            return internalHistoryAsync(resourceType, null, since, pageSize, summary);
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
        public TBundle TypeHistory(string resourceType, DateTimeOffset? since = null, int? pageSize = null, SummaryType summary = SummaryType.False)
        {
            return TypeHistoryAsync(resourceType, since, pageSize, summary).WaitResult();
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
        public Task<TBundle> TypeHistoryAsync<TResource>(DateTimeOffset? since = null, int? pageSize = null, SummaryType summary = SummaryType.False) where TResource : Resource, new()
        {
            string collection = GetCollectionName(typeof(TResource));
            return internalHistoryAsync(collection, null, since, pageSize, summary);
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
        public TBundle TypeHistory<TResource>(DateTimeOffset? since = null, int? pageSize = null, SummaryType summary = SummaryType.False) where TResource : Resource, new()
        {
            return TypeHistoryAsync<TResource>(since, pageSize, summary).WaitResult();
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
        public Task<TBundle> HistoryAsync(Uri location, DateTimeOffset? since = null, int? pageSize = null, SummaryType summary = SummaryType.False)
        {
            if (location == null) throw Error.ArgumentNull(nameof(location));

            var id = verifyResourceIdentity(location, needId: true, needVid: false);
            return internalHistoryAsync(id.ResourceType, id.Id, since, pageSize, summary);
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
        public TBundle History(Uri location, DateTimeOffset? since = null, int? pageSize = null, SummaryType summary = SummaryType.False)
        {
            return HistoryAsync(location, since, pageSize, summary).WaitResult();
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
        public Task<TBundle> HistoryAsync(string location, DateTimeOffset? since = null, int? pageSize = null, SummaryType summary = SummaryType.False)
        {
            return HistoryAsync(new Uri(location, UriKind.Relative), since, pageSize, summary);
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
        public TBundle History(string location, DateTimeOffset? since = null, int? pageSize = null, SummaryType summary = SummaryType.False)
        {
            return HistoryAsync(location, since, pageSize, summary).WaitResult();
        }

        /// <summary>
        /// Retrieve the full version history of the server
        /// </summary>
        /// <param name="since">Optional. Returns only changes after the given date</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="summary">Indicates whether the returned resources should just contain the minimal set of elements</param>
        /// <returns>A bundle with the history for the indicated instance, may contain both 
        /// ResourceEntries and DeletedEntries.</returns>
        public Task<TBundle> WholeSystemHistoryAsync(DateTimeOffset? since = null, int? pageSize = null, SummaryType summary = SummaryType.False)
        {
            return internalHistoryAsync(null, null, since, pageSize, summary);
        }
        /// <summary>
        /// Retrieve the full version history of the server
        /// </summary>
        /// <param name="since">Optional. Returns only changes after the given date</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="summary">Indicates whether the returned resources should just contain the minimal set of elements</param>
        /// <returns>A bundle with the history for the indicated instance, may contain both 
        /// ResourceEntries and DeletedEntries.</returns>
        public TBundle WholeSystemHistory(DateTimeOffset? since = null, int? pageSize = null, SummaryType summary = SummaryType.False)
        {
            return WholeSystemHistoryAsync(since, pageSize, summary).WaitResult();
        }
        private Task<TBundle> internalHistoryAsync(string resourceType = null, string id = null, DateTimeOffset? since = null, int? pageSize = null, SummaryType summary = SummaryType.False)
        {
            RequestsBuilder history;

            if(resourceType == null)
                history = new RequestsBuilder(Endpoint).ServerHistory(summary,pageSize,since);
            else if(id == null)
                history = new RequestsBuilder(Endpoint).CollectionHistory(resourceType, summary,pageSize,since);
            else
                history = new RequestsBuilder(Endpoint).ResourceHistory(resourceType,id, summary,pageSize,since);

            return executeAsync<TBundle>(history.ToRequest(), HttpStatusCode.OK);
        }
        private TBundle internalHistory(string resourceType = null, string id = null, DateTimeOffset? since = null,
            int? pageSize = null, SummaryType summary = SummaryType.False)
        {
            return internalHistoryAsync(resourceType, id, since, pageSize, summary).WaitResult();
        }

        #endregion

        #region Transaction

        /// <summary>
        /// Send a set of creates, updates and deletes to the server to be processed in one transaction
        /// </summary>
        /// <param name="bundle">The bundled creates, updates and deleted</param>
        /// <returns>A bundle as returned by the server after it has processed the transaction, or null
        /// if an error occurred.</returns>
        public Task<TBundle> TransactionAsync(TBundle bundle)
        {
            if (bundle == null) throw new ArgumentNullException(nameof(bundle));

            var request = new RequestsBuilder(Endpoint).Transaction(bundle).ToRequest();
            return executeAsync<TBundle>(request, HttpStatusCode.OK);
        }
        /// <summary>
        /// Send a set of creates, updates and deletes to the server to be processed in one transaction
        /// </summary>
        /// <param name="bundle">The bundled creates, updates and deleted</param>
        /// <returns>A bundle as returned by the server after it has processed the transaction, or null
        /// if an error occurred.</returns>
        public TBundle Transaction(TBundle bundle)
        {
            return TransactionAsync(bundle).WaitResult();
        }

        #endregion

        #region Operation

        public Task<Resource> WholeSystemOperationAsync(string operationName, Parameters parameters = null, bool useGet = false)
        {
            if (operationName == null) throw Error.ArgumentNull(nameof(operationName));
            return internalOperationAsync(operationName, parameters: parameters, useGet: useGet);
        }

        public Resource WholeSystemOperation(string operationName, Parameters parameters = null, bool useGet = false)
        {
            return WholeSystemOperationAsync(operationName, parameters, useGet).WaitResult();
        }


        public Task<Resource> TypeOperationAsync<TResource>(string operationName, Parameters parameters = null, bool useGet = false) 
            where TResource : Resource
        {
            if (operationName == null) throw Error.ArgumentNull(nameof(operationName));

            // [WMR 20160421] GetResourceNameForType is obsolete
            // var typeName = ModelInfo.GetResourceNameForType(typeof(TResource));
            var typeName = GetFhirTypeNameForType(typeof(TResource));

            return TypeOperationAsync(operationName, typeName, parameters, useGet: useGet);
        }
        public Resource TypeOperation<TResource>(string operationName, Parameters parameters = null,
            bool useGet = false) where TResource : Resource
        {
            return TypeOperationAsync<TResource>(operationName, parameters, useGet).WaitResult();
        }
            


        public Task<Resource> TypeOperationAsync(string operationName, string typeName, Parameters parameters = null, bool useGet = false)
        {
            if (operationName == null) throw Error.ArgumentNull(nameof(operationName));
            if (typeName == null) throw Error.ArgumentNull(nameof(typeName));

            return internalOperationAsync(operationName, typeName, parameters: parameters, useGet: useGet);
        }
        public Resource TypeOperation(string operationName, string typeName, Parameters parameters = null, bool useGet = false)
        {
            return TypeOperationAsync(operationName, typeName, parameters, useGet).WaitResult();
        }



        public Task<Resource> InstanceOperationAsync(Uri location, string operationName, Parameters parameters = null, bool useGet = false)
        {
            if (location == null) throw Error.ArgumentNull(nameof(location));
            if (operationName == null) throw Error.ArgumentNull(nameof(operationName));

            var id = verifyResourceIdentity(location, needId: true, needVid: false);

            return internalOperationAsync(operationName, id.ResourceType, id.Id, id.VersionId, parameters, useGet);
        }
        public Resource InstanceOperation(Uri location, string operationName, Parameters parameters = null, bool useGet = false)
        {
            return InstanceOperationAsync(location, operationName, parameters, useGet).WaitResult();
        }



        public Task<Resource> OperationAsync(Uri location, string operationName, Parameters parameters = null, bool useGet = false)
        {
            if (location == null) throw Error.ArgumentNull(nameof(location));
            if (operationName == null) throw Error.ArgumentNull(nameof(operationName));

            var tx = new RequestsBuilder(Endpoint).EndpointOperation(new RestUrl(location), operationName, parameters, useGet).ToRequest();

            return executeAsync<Resource>(tx, HttpStatusCode.OK);
        }
        public Resource Operation(Uri location, string operationName, Parameters parameters = null, bool useGet = false)
        {
            return OperationAsync(location, operationName, parameters, useGet).WaitResult();
        }

        
        public Task<Resource> OperationAsync(Uri operation, Parameters parameters = null, bool useGet = false)
        {
            if (operation == null) throw Error.ArgumentNull(nameof(operation));

            var tx = new RequestsBuilder(Endpoint).EndpointOperation(new RestUrl(operation), parameters, useGet).ToRequest();

            return executeAsync<Resource>(tx, HttpStatusCode.OK);
        }
        public Resource Operation(Uri operation, Parameters parameters = null, bool useGet = false)
        {
            return OperationAsync(operation, parameters, useGet).WaitResult();
        }



        private Task<Resource> internalOperationAsync(string operationName, string type = null, string id = null, string vid = null, 
            Parameters parameters = null, bool useGet = false)
        {
            // Brian: Not sure why we would create this parameters object as empty.
            //        I would imagine that a null parameters object is different to an empty one?
            // EK: What else could we do?  POST an empty body?  We cannot use GET unless the caller indicates this is an
            // idempotent call....
            // MV: (related to issue #419): we only provide an empty parameter when we are not performing a GET operation. In r4 it will be allowed 
            //     to provide an empty body in POST operations. In that case the line of code can be deleted.
            if (parameters == null && !useGet) parameters = new Parameters();

            Request request;

            if (type == null)
                request = new RequestsBuilder(Endpoint).ServerOperation(operationName, parameters, useGet).ToRequest();
            else if (id == null)
                request = new RequestsBuilder(Endpoint).TypeOperation(type, operationName, parameters, useGet).ToRequest();
            else
                request = new RequestsBuilder(Endpoint).ResourceOperation(type, id, vid, operationName, parameters, useGet).ToRequest();

            return executeAsync<Resource>(request, HttpStatusCode.OK);
        }

        private Resource internalOperation(string operationName, string type = null, string id = null,
            string vid = null, Parameters parameters = null, bool useGet = false)
        {
            return internalOperationAsync(operationName, type, id, vid, parameters, useGet).WaitResult();
        }

        #endregion
        
        #region Get

        /// <summary>
        /// Invoke a general GET on the server. If the operation fails, then this method will throw an exception
        /// </summary>
        /// <param name="url">A relative or absolute url. If the url is absolute, it has to be located within the endpoint of the client.</param>
        /// <returns>A resource that is the outcome of the operation. The type depends on the definition of the operation at the given url</returns>
        /// <remarks>parameters to the method are simple, and are in the URL, and this is a GET operation</remarks>
        public Resource Get(Uri url)
        {
            return GetAsync(url).WaitResult();
        }
        /// <summary>
        /// Invoke a general GET on the server. If the operation fails, then this method will throw an exception
        /// </summary>
        /// <param name="url">A relative or absolute url. If the url is absolute, it has to be located within the endpoint of the client.</param>
        /// <returns>A resource that is the outcome of the operation. The type depends on the definition of the operation at the given url</returns>
        /// <remarks>parameters to the method are simple, and are in the URL, and this is a GET operation</remarks>
        public async Task<Resource> GetAsync(Uri url)
        {
            if (url == null) throw Error.ArgumentNull(nameof(url));

            var tx = new RequestsBuilder(Endpoint).Get(url).ToRequest();
            return await executeAsync<Resource>(tx, HttpStatusCode.OK).ConfigureAwait(false);
        }
        /// <summary>
        /// Invoke a general GET on the server. If the operation fails, then this method will throw an exception
        /// </summary>
        /// <param name="url">A relative or absolute url. If the url is absolute, it has to be located within the endpoint of the client.</param>
        /// <returns>A resource that is the outcome of the operation. The type depends on the definition of the operation at the given url</returns>
        /// <remarks>parameters to the method are simple, and are in the URL, and this is a GET operation</remarks>
        public Resource Get(string url)
        {
            return GetAsync(url).WaitResult();
        }
        /// <summary>
        /// Invoke a general GET on the server. If the operation fails, then this method will throw an exception
        /// </summary>
        /// <param name="url">A relative or absolute url. If the url is absolute, it has to be located within the endpoint of the client.</param>
        /// <returns>A resource that is the outcome of the operation. The type depends on the definition of the operation at the given url</returns>
        /// <remarks>parameters to the method are simple, and are in the URL, and this is a GET operation</remarks>
        public Task<Resource> GetAsync(string url)
        {
            if (url == null) throw Error.ArgumentNull(nameof(url));

            return GetAsync(new Uri(url, UriKind.RelativeOrAbsolute));
        }

        #endregion
        

   

        private ResourceIdentity verifyResourceIdentity(Uri location, bool needId, bool needVid)
        {
            var result = new ResourceIdentity(location);

            if (result.ResourceType == null) throw Error.Argument(nameof(location), "Must be a FHIR REST url containing the resource type in its path");
            if (needId && result.Id == null) throw Error.Argument(nameof(location), "Must be a FHIR REST url containing the logical id in its path");
            if (needVid && !result.HasVersion) throw Error.Argument(nameof(location), "Must be a FHIR REST url containing the version id in its path");

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

        /// <summary>
        /// Called just before the Http call is done
        /// </summary>
        public event EventHandler<BeforeRequestEventArgs> OnBeforeRequest;

        /// <summary>
        /// Called just after the response was received
        /// </summary>
        public event EventHandler<AfterResponseEventArgs> OnAfterResponse;

        /// <summary>
        /// Inspect or modify the HttpWebRequest just before the FhirClient issues a call to the server
        /// </summary>
        /// <param name="rawRequest">The request as it is about to be sent to the server</param>
        /// <param name="body">The data in the body of the request as it is about to be sent to the server</param>
        protected virtual void BeforeRequest(HttpWebRequest rawRequest, byte[] body) 
        {
            // Default implementation: call event
            OnBeforeRequest?.Invoke(this, new BeforeRequestEventArgs(rawRequest, body));
        }

        /// <summary>
        /// Inspect the HttpWebResponse as it came back from the server
        /// </summary>
        /// <remarks>You cannot read the body from the HttpWebResponse, since it has
        /// already been read by the framework. Use the body parameter instead.</remarks>
        protected virtual void AfterResponse(HttpWebResponse webResponse, byte[] body)
        {
            // Default implementation: call event
            OnAfterResponse?.Invoke(this, new AfterResponseEventArgs(webResponse, body));
        }

        // Original
        private TResource execute<TResource>(Request request, HttpStatusCode expect) where TResource : Resource
        {
            return executeAsync<TResource>(request, new[] { expect }).WaitResult();
        }
        public Task<TResource> executeAsync<TResource>(Request request, HttpStatusCode expect) where TResource : Resource
        {
            return executeAsync<TResource>(request, new[] { expect });
        }
        // Original
        private TResource execute<TResource>(Request request, IEnumerable<HttpStatusCode> expect) where TResource : Resource
        {
            return executeAsync<TResource>(request,  expect).WaitResult();
        }

        private async Task<TResource> executeAsync<TResource>(Request request, IEnumerable<HttpStatusCode> expect) where TResource : Resource
        {
            verifyServerVersion();

            var response = await _requester.ExecuteAsync(request).ConfigureAwait(false);

            if (!expect.Select(sc => ((int)sc).ToString()).Contains(response.Status))
            {
                Enum.TryParse<HttpStatusCode>(response.Status, out HttpStatusCode code);
                throw new FhirOperationException<TOperationOutcome>("Operation concluded successfully, but the return status {0} was unexpected".FormatWith(response.Status), code);
            }

            Resource result;

            // Special feature: if ReturnFullResource was requested (using the Prefer header), but the server did not return the resource
            // (or it returned an OperationOutcome) - explicitly go out to the server to get the resource and return it. 
            // This behavior is only valid for PUT and POST requests, where the server may device whether or not to return the full body of the alterend resource.
            var noRealBody = response.Resource == null || (response.Resource is TOperationOutcome && string.IsNullOrEmpty(response.Resource.Id));
            if (noRealBody && request.IsPostOrPut()
                && ReturnFullResource && response.Location != null
                && new ResourceIdentity(response.Location).IsRestResourceIdentity()) // Check that it isn't an operation too
            {
                result = await GetAsync(response.Location).ConfigureAwait(false);
            }
            else
                result = response.Resource;

            if (result == null) return null;

            // We have a success code (2xx), we have a body, but the body may not be of the type we expect.
            if (!(result is TResource))
            {
                // If this is an operationoutcome, that may still be allright. Keep the OperationOutcome in 
                // the LastResult, and return null as the result. Otherwise, throw.
                if (result is TOperationOutcome)
                    return null;

                var message = String.Format("Operation {0} on {1} expected a body of type {2} but a {3} was returned", request.Method,
                    request.Url, typeof(TResource).Name, result.GetType().Name);
                throw new FhirOperationException<TOperationOutcome>(message, _requester.LastResponse.StatusCode);
            }
            else
                return result as TResource;
        }

        private bool versionChecked = false;

        private void verifyServerVersion()
        {
            if (!VerifyFhirVersion) return;

            if (versionChecked) return;
            versionChecked = true;      // So we can now start calling Conformance() without getting into a loop

            TMetadata metadata = null;
            try
            {
                metadata = Metadata();
            }
            catch (FormatException)
            {
                // Mmmm...cannot even read the body. Probably not so good.
                throw Error.NotSupported("Cannot read the metadata of the server to verify FHIR version compatibility");
            }

            if (!metadata.FhirVersion.StartsWith(_requester.FhirVersion))
            {
                throw Error.NotSupported("This client support FHIR version {0}, but the server uses version {1}".FormatWith(_requester.FhirVersion, metadata.FhirVersion));
            }
        }

        private string GetCollectionName(Type type)
        {
            if (type.CanBeTreatedAsType(typeof(Resource)))
                return GetFhirTypeNameForType(type);
            else
                throw new ArgumentException(String.Format(
                    "Cannot determine collection name, type {0} is not a resource type", type.Name));
        }
    }

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
