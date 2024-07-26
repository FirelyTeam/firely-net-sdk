﻿#nullable enable

/* 
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using static Hl7.Fhir.Rest.HttpContentParsers;

namespace Hl7.Fhir.Rest;

public partial class BaseFhirClient : IDisposable
{
    #region Properties

    private readonly Lazy<List<HttpStatusCode>> _200Responses = new(() => Enum.GetValues(typeof(HttpStatusCode)).Cast<HttpStatusCode>().Where(n => (int)n > 199 && (int)n < 300).ToList());

    private string fhirVersion => Settings?.ExplicitFhirVersion ?? Inspector.FhirVersion ??
        throw new ArgumentException("The FHIR version to use cannot be derived from the assembly metadata, " +
                                    $"use {nameof(FhirClientSettings)}.{nameof(FhirClientSettings.ExplicitFhirVersion)} instead.");

    internal readonly ModelInspector Inspector;

    internal HttpClientRequester Requester { get; init; }

    /// <summary>
    /// Default request headers that can be modified to persist default headers to internal client.
    /// </summary>
    public HttpRequestHeaders? RequestHeaders { get; protected set; }

    /// <summary>
    /// The default endpoint for use with operations that use discrete id/version parameters
    /// instead of explicit uri endpoints. This will always have a trailing "/"
    /// </summary>
    public Uri Endpoint
    {
        get;
        protected set;
    }

    public FhirClientSettings Settings { get; set; }

    /// <summary>
    /// The last transaction result that was executed on this connection to the FHIR server
    /// </summary>
    public Bundle.ResponseComponent? LastResult { get; private set; }

    /// <summary>
    /// The raw body returned by the last http request.
    /// </summary>
    public virtual byte[]? LastBody { get; private set; }

    /// <summary>
    /// The body returned by the last http request as text (or null if it could not be parsed as text).
    /// </summary>
    public virtual string? LastBodyAsText { get; private set; }

    /// <summary>
    /// The body returned by the last http request as a FHIR resource (or null if the body did not have a FHIR payload).
    /// </summary>
    public virtual Resource? LastBodyAsResource { get; private set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new client using a default endpoint
    /// If the endpoint does not end with a slash (/), it will be added.
    /// </summary>
    /// <remarks>
    /// If the messageHandler is provided then it must be disposed by the caller
    /// </remarks>
    /// <param name="endpoint">
    /// The URL of the server to connect to.<br/>
    /// If the trailing '/' is not present, then it will be appended automatically
    /// </param>
    /// <param name="messageHandler"></param>
    /// <param name="inspector"></param>
    /// <param name="settings"></param>
    public BaseFhirClient(Uri endpoint, HttpMessageHandler? messageHandler, ModelInspector inspector, FhirClientSettings? settings = null)
        : this(endpoint, inspector,
            settings ?? new(),
            new(endpoint, (settings ?? new()).Timeout, messageHandler ?? makeDefaultHandler(), messageHandler == null))
    {
        // Nothing
    }

    /// <summary>
    /// Creates a new client using a default endpoint
    /// If the endpoint does not end with a slash (/), it will be added.
    /// </summary>
    /// <remarks>
    /// The httpClient must be disposed by the caller
    /// </remarks>
    /// <param name="endpoint">
    /// The URL of the server to connect to.<br/>
    /// If the trailing '/' is not present, then it will be appended automatically
    /// </param>
    /// <param name="settings"></param>
    /// <param name="httpClient"></param>
    /// <param name="inspector"></param>
    public BaseFhirClient(Uri endpoint, HttpClient httpClient, ModelInspector inspector, FhirClientSettings? settings = null)
        : this(endpoint, inspector, settings ?? new(), new(endpoint, httpClient))
    {
        // Nothing
    }

    internal BaseFhirClient(Uri endpoint, ModelInspector inspector, FhirClientSettings settings, HttpClientRequester requester)
    {
        Inspector = inspector;
        Settings = settings;
        Endpoint = getValidatedEndpoint(endpoint);

        Requester = requester;

        // Expose default request headers to user.
        RequestHeaders = requester.Client.DefaultRequestHeaders;
    }

    public BaseFhirClient(Uri endpoint, ModelInspector inspector, FhirClientSettings? settings = null) :
        this(endpoint, (HttpMessageHandler?)null, inspector, settings)
    {
        // nothing
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
    /// <param name="ct" />
    /// <typeparam name="TResource">The type of resource to read. Resource or DomainResource is allowed if exact type is unknown</typeparam>
    /// <returns>
    /// The requested resource. This operation will throw an exception
    /// if the resource has been deleted or does not exist. 
    /// The specified may be relative or absolute, if it is an absolute
    /// url, it must reference an address within the endpoint.
    /// </returns>
    /// <remarks>Since ResourceLocation is a subclass of Uri, you may pass in ResourceLocations too.</remarks>
    /// <exception cref="FhirOperationException">This will occur if conditional request returns a status 304 and optionally an OperationOutcome</exception>
    public virtual Task<TResource?> ReadAsync<TResource>(Uri location, string? ifNoneMatch = null, DateTimeOffset? ifModifiedSince = null, CancellationToken? ct = null) where TResource : Resource
    {
        if (location == null) throw Error.ArgumentNull(nameof(location));

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

        return executeAsync<TResource>(tx, HttpStatusCode.OK, ct);
    }

    /// <summary>
    /// Fetches a typed resource from a FHIR resource endpoint.
    /// </summary>
    /// <param name="location">The url of the Resource to fetch as a string. This can be a Resource id url or a version-specific
    /// Resource url.</param>
    /// <param name="ifNoneMatch">The (weak) ETag to use in a conditional read. Optional.</param>
    /// <param name="ifModifiedSince">Last modified since date in a conditional read. Optional.</param>
    /// <param name="ct"></param>
    /// <typeparam name="TResource">The type of resource to read. Resource or DomainResource is allowed if exact type is unknown</typeparam>
    /// <returns>The requested resource</returns>
    /// <remarks>This operation will throw an exception
    /// if the resource has been deleted or does not exist. The specified may be relative or absolute, if it is an absolute
    /// url, it must reference an address within the endpoint.</remarks>
    public virtual Task<TResource?> ReadAsync<TResource>(string location, string? ifNoneMatch = null, DateTimeOffset? ifModifiedSince = null, CancellationToken? ct = null) where TResource : Resource
    {
        return ReadAsync<TResource>(new Uri(location, UriKind.RelativeOrAbsolute), ifNoneMatch, ifModifiedSince, ct);
    }

    #endregion

    #region Refresh

    /// <summary>
    /// Refreshes the data in the resource passed as an argument by re-reading it from the server
    /// </summary>
    /// <typeparam name="TResource"></typeparam>
    /// <param name="current">The resource for which you want to get the most recent version.</param>
    /// <param name="ct"></param>
    /// <returns>A new instance of the resource, containing the most up-to-date data</returns>
    /// <remarks>This function will not overwrite the argument with new data, rather it will return a new instance
    /// which will have the newest data, leaving the argument intact.</remarks>
    public virtual Task<TResource?> RefreshAsync<TResource>(TResource current, CancellationToken? ct = null) where TResource : Resource
    {
        if (current == null) throw Error.ArgumentNull(nameof(current));

        return ReadAsync<TResource>(ResourceIdentity.Build(current.TypeName, current.Id), ct: ct);
    }

    #endregion

    #region Update

    /// <summary>
    /// Update (or create) a resource
    /// </summary>
    /// <param name="resource">The resource to update</param>
    /// <param name="versionAware">If true, asks the server to verify we are updating the latest version</param>
    /// <param name="ct"></param>
    /// <typeparam name="TResource">The type of resource that is being updated</typeparam>
    /// <returns>The body of the updated resource, unless ReturnFullResource is set to "false"</returns>
    /// <remarks>Throws an exception when the update failed, in particular when an update conflict is detected and the server returns a HTTP 409.
    /// If the resource does not yet exist - and the server allows client-assigned id's - a new resource with the given id will be
    /// created.</remarks>
    public virtual Task<TResource?> UpdateAsync<TResource>(TResource resource, bool versionAware = false, CancellationToken? ct = null) where TResource : Resource
    {
        if (resource == null) throw Error.ArgumentNull(nameof(resource));
        if (resource.Id == null) throw Error.Argument(nameof(resource), "Resource needs a non-null Id to send the update to");

        var upd = new TransactionBuilder(Endpoint);

        if (versionAware && resource.HasVersionId)
            upd.Update(resource.Id, resource, versionId: resource.VersionId);
        else
            upd.Update(resource.Id, resource);

        return internalUpdateAsync(resource, upd.ToBundle(), ct);
    }

    /// <summary>
    /// Conditionally update (or create) a resource
    /// </summary>
    /// <param name="resource">The resource to update</param>
    /// <param name="condition">Criteria used to locate the resource to update</param>
    /// <param name="versionAware">If true, asks the server to verify we are updating the latest version</param>
    /// <param name="ct"></param>
    /// <typeparam name="TResource">The type of resource that is being updated</typeparam>
    /// <returns>The body of the updated resource, unless ReturnFullResource is set to "false"</returns>
    /// <remarks>Throws an exception when the update failed, in particular when an update conflict is detected and the server returns a HTTP 409.
    /// If the criteria passed in condition do not match a resource a new resource with a server assigned id will be created.</remarks>
    public virtual Task<TResource?> ConditionalUpdateAsync<TResource>(TResource resource, SearchParams condition, bool versionAware = false, CancellationToken? ct = null) where TResource : Resource
    {
        if (resource == null) throw Error.ArgumentNull(nameof(resource));
        if (condition == null) throw Error.ArgumentNull(nameof(condition));

        var upd = new TransactionBuilder(Endpoint);

        if (versionAware && resource.HasVersionId)
            upd.ConditionalUpdate(condition, resource, versionId: resource.VersionId);
        else
            upd.ConditionalUpdate(condition, resource);

        return internalUpdateAsync(resource, upd.ToBundle(), ct);
    }

    private Task<TResource?> internalUpdateAsync<TResource>(TResource resource, Bundle tx, CancellationToken? ct) where TResource : Resource
    {
        resource.ResourceBase = Endpoint;

        // This might be an update of a resource that doesn't yet exist, so accept a status Created too
        return executeAsync<TResource>(tx, new[] { HttpStatusCode.Created, HttpStatusCode.OK }, ct);
    }

    private TResource? internalUpdate<TResource>(TResource resource, Bundle tx, CancellationToken? ct) where TResource : Resource
    {
        return internalUpdateAsync(resource, tx, ct).WaitResult();
    }

    #endregion

    #region Delete

    /// <summary>
    /// Delete a resource at the given endpoint.
    /// </summary>
    /// <param name="location">endpoint of the resource to delete</param>
    /// <param name="ct"></param>
    /// <returns>Throws an exception when the delete failed, though this might
    /// just mean the server returned 404 (the resource didn't exist before) or 410 (the resource was
    /// already deleted).</returns>
    public virtual async Task DeleteAsync(Uri location, CancellationToken? ct = null)
    {
        if (location == null) throw Error.ArgumentNull(nameof(location));

        var id = verifyResourceIdentity(location, needId: true, needVid: false);
        var tx = new TransactionBuilder(Endpoint).Delete(id.ResourceType, id.Id).ToBundle();

        await executeAsync<Resource>(tx, new[] { HttpStatusCode.OK, HttpStatusCode.NoContent }, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Delete a resource at the given endpoint.
    /// </summary>
    /// <param name="location">endpoint of the resource to delete</param>
    /// <param name="ct"></param>
    /// <returns>Throws an exception when the delete failed, though this might
    /// just mean the server returned 404 (the resource didn't exist before) or 410 (the resource was
    /// already deleted).</returns>
    public virtual Task DeleteAsync(string location, CancellationToken? ct = null)
    {
        return DeleteAsync(new Uri(location, UriKind.Relative), ct);
    }

    /// <summary>
    /// Delete a resource
    /// </summary>
    /// <param name="resource">The resource to delete</param>
    /// <param name="ct"></param>
    public virtual async Task DeleteAsync(Resource resource, CancellationToken? ct = null)
    {
        if (resource == null) throw Error.ArgumentNull(nameof(resource));
        if (resource.Id == null) throw Error.Argument(nameof(resource), "Entry must have an id");

        await DeleteAsync(resource.ResourceIdentity(Endpoint).WithoutVersion(), ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Conditionally delete a single resource
    /// </summary>
    /// <param name="condition">Criteria to use to match the resource to delete</param>
    /// <param name="resourceType">The type of resource to delete (optional)</param>
    /// <param name="versionId">The versionId to be used for the if-match header</param>
    /// <param name="ct"></param>
    public virtual async Task ConditionalDeleteSingleAsync(SearchParams condition, string? resourceType = null, string? versionId = null, CancellationToken? ct = null)
    {
        var tx = new TransactionBuilder(Endpoint).ConditionalDeleteSingle(condition, resourceType, versionId).ToBundle();
        await executeAsync<Resource>(tx, new[] { HttpStatusCode.OK, HttpStatusCode.NoContent }, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Conditionally delete multiple resources
    /// </summary>
    /// <param name="condition">The search criteria to match the resources to be deleted.</param>
    /// <param name="resourceType">The type of resource to delete (optional)</param>
    /// <param name="ct"></param>
    public virtual async Task ConditionalDeleteMultipleAsync(SearchParams condition, string? resourceType = null, CancellationToken? ct = null)
    {
        var tx = new TransactionBuilder(Endpoint).ConditionalDeleteMultiple(condition, resourceType).ToBundle();
        await executeAsync<Resource>(tx, new[] { HttpStatusCode.OK, HttpStatusCode.NoContent }, ct).ConfigureAwait(false);
    }

    #endregion

    #region Create

    /// <summary>
    /// Create a resource on a FHIR endpoint
    /// </summary>
    /// <param name="resource">The resource instance to create</param>
    /// <param name="ct"></param>
    /// <returns>The resource as created on the server, or an exception if the create failed.</returns>
    /// <typeparam name="TResource">The type of resource to create</typeparam>
    public virtual Task<TResource?> CreateAsync<TResource>(TResource resource, CancellationToken? ct = null) where TResource : Resource
    {
        if (resource == null) throw Error.ArgumentNull(nameof(resource));

        var tx = new TransactionBuilder(Endpoint).Create(resource).ToBundle();

        return executeAsync<TResource>(tx, new[] { HttpStatusCode.Created, HttpStatusCode.OK }, ct);
    }

    /// <summary>
    /// Conditionally Create a resource on a FHIR endpoint
    /// </summary>
    /// <param name="resource">The resource instance to create</param>
    /// <param name="condition">The criteria</param>
    /// <param name="ct"></param>
    /// <returns>The resource as created on the server, or an exception if the create failed.</returns>
    /// <typeparam name="TResource">The type of resource to create</typeparam>
    public virtual Task<TResource?> ConditionalCreateAsync<TResource>(TResource resource, SearchParams condition, CancellationToken? ct = null) where TResource : Resource
    {
        if (resource == null) throw Error.ArgumentNull(nameof(resource));
        if (condition == null) throw Error.ArgumentNull(nameof(condition));

        var tx = new TransactionBuilder(Endpoint).ConditionalCreate(resource, condition).ToBundle();

        return executeAsync<TResource>(tx, new[] { HttpStatusCode.Created, HttpStatusCode.OK }, ct);
    }

    #endregion

    #region Patch

    /// <summary>
    /// Patch a resource on a FHIR Endpoint
    /// </summary>
    /// <param name="location">Location of the resource</param>
    /// <param name="patchParameters">A Parameters resource that includes the patch operation(s) to perform</param>
    /// <param name="ct"></param>
    /// <returns>The patched resource</returns>
    public virtual Task<Resource?> PatchAsync(Uri location, Parameters patchParameters, CancellationToken? ct = null)
    {
        if (location == null) throw Error.ArgumentNull(nameof(location));

        var id = BaseFhirClient.verifyResourceIdentity(location, needId: true, needVid: false);

        var tx = new TransactionBuilder(Endpoint);

        if (id.HasVersion)
            tx.Patch(id.ResourceType, id.Id, patchParameters, id.VersionId);
        else
            tx.Patch(id.ResourceType, id.Id, patchParameters);

        return executeAsync<Resource>(tx.ToBundle(), new[] { HttpStatusCode.Created, HttpStatusCode.OK }, ct);
    }

    /// <summary>
    /// Patch a resource on a FHIR Endpoint
    /// </summary>
    /// <typeparam name="TResource">Type of resource to patch</typeparam>
    /// <param name="id">Id of the resource to patch</param>
    /// <param name="patchParameters">A Parameters resource that includes the patch operation(s) to perform</param>
    /// <param name="versionId">version id of the resource to patch</param>
    /// <param name="ct"></param>
    /// <returns>The patched resource</returns>
    public virtual Task<TResource?> PatchAsync<TResource>(string id, Parameters patchParameters, string? versionId = null, CancellationToken? ct = null) where TResource : Resource
    {
        if (id == null) throw Error.ArgumentNull(nameof(id));

        var tx = new TransactionBuilder(Endpoint);
        var resourceType = typeNameOrDie<TResource>();

        if (!string.IsNullOrEmpty(versionId))
            tx.Patch(resourceType, id, patchParameters, versionId);
        else
            tx.Patch(resourceType, id, patchParameters);

        return executeAsync<TResource>(tx.ToBundle(), new[] { HttpStatusCode.Created, HttpStatusCode.OK }, ct);
    }

    public virtual Task<TResource?> PatchAsync<TResource>(string id, string patchDocument, ResourceFormat format, CancellationToken? ct = null) where TResource : Resource
    {
        if (id == null) throw Error.ArgumentNull(nameof(id));

        var resourceType = typeNameOrDie<TResource>();
        var url = new RestUrl(Endpoint).AddPath(resourceType, id);

        var request = new HttpRequestMessage(new("PATCH"), url.Uri).WithFormatParameter(format);

        request.Content = new StringContent(patchDocument);
        request.Content.Headers.ContentType = new MediaTypeHeaderValue(format switch
        {
            ResourceFormat.Json => "application/json-patch+json",
            ResourceFormat.Xml => "application/xml-patch+xml",
            _ => throw Error.Argument(nameof(format), "Unsupported format")
        });

        return executeAsync<TResource>(request, new[] { HttpStatusCode.Created, HttpStatusCode.OK }, ct);
    }

    /// <summary>
    /// Conditionally patch a resource on a FHIR Endpoint
    /// </summary>
    /// <typeparam name="TResource">Type of resource to patch</typeparam>
    /// <param name="condition">Criteria used to locate the resource to update</param>
    /// <param name="patchParameters">A Parameters resource that includes the patch operation(s) to perform</param>
    /// <param name="ct"></param>
    /// <returns>The patched resource</returns>
    public Task<TResource?> ConditionalPatchAsync<TResource>(SearchParams condition, Parameters patchParameters, CancellationToken? ct = null) where TResource : Resource
    {
        var tx = new TransactionBuilder(Endpoint);
        var resourceType = typeNameOrDie<TResource>();
        tx.ConditionalPatch(resourceType, condition, patchParameters);

        return executeAsync<TResource>(tx.ToBundle(), new[] { HttpStatusCode.Created, HttpStatusCode.OK }, ct);
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
    /// <param name="ct"></param>        
    /// <returns>A bundle with the history for the indicated instance, may contain both 
    /// ResourceEntries and DeletedEntries.</returns>
    public Task<Bundle?> TypeHistoryAsync(string resourceType, DateTimeOffset? since = null, int? pageSize = null, SummaryType? summary = null, CancellationToken? ct = null)
    {
        return internalHistoryAsync(resourceType, null, since, pageSize, summary, ct);
    }

    /// <summary>
    /// Retrieve the version history for a specific resource type
    /// </summary>
    /// <param name="since">Optional. Returns only changes after the given date</param>
    /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
    /// <param name="summary">Optional. Asks the server to only provide the fields defined for the summary</param>
    /// <param name="ct"></param>
    /// <typeparam name="TResource">The type of Resource to get the history for</typeparam>
    /// <returns>A bundle with the history for the indicated instance, may contain both 
    /// ResourceEntries and DeletedEntries.</returns>
    public Task<Bundle?> TypeHistoryAsync<TResource>(DateTimeOffset? since = null, int? pageSize = null, SummaryType? summary = null, CancellationToken? ct = null) where TResource : Resource, new()
    {
        return internalHistoryAsync(typeNameOrDie<TResource>(), null, since, pageSize, summary, ct);
    }

    /// <summary>
    /// Retrieve the version history for a resource at a given location
    /// </summary>
    /// <param name="location">The address of the resource to get the history for</param>
    /// <param name="since">Optional. Returns only changes after the given date</param>
    /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
    /// <param name="summary">Optional. Asks the server to only provide the fields defined for the summary</param>
    /// <param name="ct"></param>
    /// <returns>A bundle with the history for the indicated instance, may contain both 
    /// ResourceEntries and DeletedEntries.</returns>
    public Task<Bundle?> HistoryAsync(Uri location, DateTimeOffset? since = null, int? pageSize = null, SummaryType? summary = null, CancellationToken? ct = null)
    {
        if (location == null) throw Error.ArgumentNull(nameof(location));

        var id = BaseFhirClient.verifyResourceIdentity(location, needId: true, needVid: false);
        return internalHistoryAsync(id.ResourceType, id.Id, since, pageSize, summary, ct);
    }

    /// <summary>
    /// Retrieve the version history for a resource at a given location
    /// </summary>
    /// <param name="location">The address of the resource to get the history for</param>
    /// <param name="since">Optional. Returns only changes after the given date</param>
    /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
    /// <param name="summary">Optional. Asks the server to only provide the fields defined for the summary</param>
    /// <param name="ct"></param>
    /// <returns>A bundle with the history for the indicated instance, may contain both 
    /// ResourceEntries and DeletedEntries.</returns>
    public Task<Bundle?> HistoryAsync(string location, DateTimeOffset? since = null, int? pageSize = null, SummaryType? summary = null, CancellationToken? ct = null)
    {
        return HistoryAsync(new Uri(location, UriKind.Relative), since, pageSize, summary, ct);
    }

    /// <summary>
    /// Retrieve the full version history of the server
    /// </summary>
    /// <param name="since">Optional. Returns only changes after the given date</param>
    /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
    /// <param name="summary">Optional. Indicates whether the returned resources should just contain the minimal set of elements</param>
    /// <param name="ct"></param>
    /// <returns>A bundle with the history for the indicated instance, may contain both 
    /// ResourceEntries and DeletedEntries.</returns>
    public Task<Bundle?> WholeSystemHistoryAsync(DateTimeOffset? since = null, int? pageSize = null, SummaryType? summary = null, CancellationToken? ct = null)
    {
        return internalHistoryAsync(null, null, since, pageSize, summary, ct);
    }

    private Task<Bundle?> internalHistoryAsync(string? resourceType = null, string? id = null, DateTimeOffset? since = null, int? pageSize = null, SummaryType? summary = null, CancellationToken? ct = null)
    {
        TransactionBuilder history;

        if (resourceType == null)
            history = new TransactionBuilder(Endpoint).ServerHistory(summary, pageSize, since);
        else if (id == null)
            history = new TransactionBuilder(Endpoint).CollectionHistory(resourceType, summary, pageSize, since);
        else
            history = new TransactionBuilder(Endpoint).ResourceHistory(resourceType, id, summary, pageSize, since);

        return executeAsync<Bundle>(history.ToBundle(), HttpStatusCode.OK, ct);
    }

    #endregion

    #region DeleteHistory

    /// <summary>
    /// Delete a resource's history (all historic versions except current)
    /// </summary>
    /// <param name="location">The location of the resource of which the history should be deleted</param>
    /// <param name="ct"></param>
    public async Task DeleteHistoryAsync(Uri location, CancellationToken? ct = null)
    {
        if (location == null) throw Error.ArgumentNull(nameof(location));

        var id = verifyResourceIdentity(location, needId: true, needVid: false);
        var tx = new TransactionBuilder(Endpoint).DeleteHistory(id.ResourceType, id.Id).ToBundle();

        await executeAsync<Resource>(tx, new[] { HttpStatusCode.OK, HttpStatusCode.NoContent }, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Delete a resource's history (all historic versions except current)
    /// </summary>
    /// <param name="location">The location of the resource of which the history should be deleted</param>
    /// <param name="ct"></param>
    public async Task DeleteHistoryAsync(string location, CancellationToken? ct = null)
    {
        await DeleteHistoryAsync(new Uri(location, UriKind.Relative), ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Delete a specific historical version of a resource
    /// </summary>
    /// <param name="location">The location of the resource of which the history should be deleted. Should contain the version ID to be deleted</param>
    /// <param name="ct"></param>
    /// <exception cref="ArgumentException"></exception>
    public async Task DeleteHistoryVersionAsync(Uri location, CancellationToken? ct = null)
    {
        if (location == null) throw Error.ArgumentNull(nameof(location));

        var id = verifyResourceIdentity(location, needId: true, needVid: true);
        var tx = new TransactionBuilder(Endpoint).DeleteHistoryVersion(id.ResourceType, id.Id, id.VersionId).ToBundle();

        await executeAsync<Resource>(tx, new[] { HttpStatusCode.OK, HttpStatusCode.NoContent }, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Delete a specific historical version of a resource
    /// </summary>
    /// <param name="location">The location of the resource of which the history should be deleted. Should contain the version ID to be deleted</param>
    /// <param name="ct"></param>
    /// <exception cref="ArgumentException"></exception>
    public async Task DeleteHistoryVersionAsync(string location, CancellationToken? ct = null)
    {
        await DeleteHistoryVersionAsync(new Uri(location, UriKind.Relative), ct).ConfigureAwait(false);
    }

    #endregion

    #region Transaction

    /// <summary>
    /// Send a set of creates, updates and deletes to the server to be processed in one transaction
    /// </summary>
    /// <param name="bundle">The bundled creates, updates and deleted</param>
    /// <param name="ct"></param>
    /// <returns>A bundle as returned by the server after it has processed the transaction, or 
    /// a FhirOperationException will be thrown if an error occurred.</returns>
    public Task<Bundle?> TransactionAsync(Bundle bundle, CancellationToken? ct = null)
    {
        if (bundle == null) throw new ArgumentNullException(nameof(bundle));

        var tx = new TransactionBuilder(Endpoint).Transaction(bundle).ToBundle();
        return executeAsync<Bundle>(tx, HttpStatusCode.OK, ct);
    }

    #endregion

    #region Operation

    public virtual Task<Resource?> WholeSystemOperationAsync(string operationName, Parameters? parameters = null, bool useGet = false, CancellationToken? ct = null)
    {
        if (operationName == null) throw Error.ArgumentNull(nameof(operationName));
        return internalOperationAsync(operationName, parameters: parameters, useGet: useGet, ct: ct);
    }

    public virtual Task<Resource?> TypeOperationAsync<TResource>(string operationName, Parameters? parameters = null, bool useGet = false, CancellationToken? ct = null)
        where TResource : Resource
    {
        if (operationName == null) throw Error.ArgumentNull(nameof(operationName));
        var typeName = typeNameOrDie<TResource>();

        return TypeOperationAsync(operationName, typeName, parameters, useGet: useGet, ct);
    }

    public virtual Task<Resource?> TypeOperationAsync(string operationName, string typeName, Parameters? parameters = null, bool useGet = false, CancellationToken? ct = null)
    {
        if (operationName == null) throw Error.ArgumentNull(nameof(operationName));
        if (typeName == null) throw Error.ArgumentNull(nameof(typeName));

        return internalOperationAsync(operationName, typeName, parameters: parameters, useGet: useGet, ct: ct);
    }

    public virtual Task<Resource?> InstanceOperationAsync(Uri location, string operationName, Parameters? parameters = null, bool useGet = false, CancellationToken? ct = null)
    {
        if (location == null) throw Error.ArgumentNull(nameof(location));
        if (operationName == null) throw Error.ArgumentNull(nameof(operationName));

        var id = BaseFhirClient.verifyResourceIdentity(location, needId: true, needVid: false);

        return internalOperationAsync(operationName, id.ResourceType, id.Id, id.VersionId, parameters, useGet, ct);
    }

    public virtual Task<Resource?> OperationAsync(Uri location, string operationName, Parameters? parameters = null, bool useGet = false, CancellationToken? ct = null)
    {
        if (location == null) throw Error.ArgumentNull(nameof(location));
        if (operationName == null) throw Error.ArgumentNull(nameof(operationName));

        var tx = new TransactionBuilder(Endpoint).EndpointOperation(new RestUrl(location), operationName, parameters, useGet).ToBundle();

        //operation responses are expected to return 2xx codes.
        return executeAsync<Resource>(tx, _200Responses.Value, ct);
    }

    public virtual Task<Resource?> OperationAsync(Uri operation, Parameters? parameters = null, bool useGet = false, CancellationToken? ct = null)
    {
        if (operation == null) throw Error.ArgumentNull(nameof(operation));

        var tx = new TransactionBuilder(Endpoint).EndpointOperation(new RestUrl(operation), parameters, useGet).ToBundle();

        //operation responses are expected to return 2xx codes.
        return executeAsync<Resource>(tx, _200Responses.Value, ct);
    }

    public virtual Task<Bundle?> ProcessMessageAsync(Bundle bundle, bool async = false, string? responseUrl = null, CancellationToken? ct = null)
    {
        if (bundle == null) throw new ArgumentNullException(nameof(bundle));

        var tx = new TransactionBuilder(Endpoint).ProcessMessage(bundle, async, responseUrl).ToBundle();

        return executeAsync<Bundle>(tx, new[] { HttpStatusCode.OK, HttpStatusCode.Accepted, HttpStatusCode.NoContent }, ct);
    }

    private Task<Resource?> internalOperationAsync(string operationName, string? type = null, string? id = null, string? vid = null,
        Parameters? parameters = null, bool useGet = false, CancellationToken? ct = null)
    {
        // Brian: Not sure why we would create this parameters object as empty.
        //        I would imagine that a null parameters object is different to an empty one?
        // EK: What else could we do?  POST an empty body?  We cannot use GET unless the caller indicates this is an
        // idempotent call....
        // MV: (related to issue #419): we only provide an empty parameter when we are not performing a GET operation. In r4 it will be allowed 
        //     to provide an empty body in POST operations. In that case the line of code can be deleted.
        if (parameters == null && !useGet) parameters = new Parameters();

        Bundle tx;

        if (type == null)
            tx = new TransactionBuilder(Endpoint).ServerOperation(operationName, parameters, useGet).ToBundle();
        else if (id == null)
            tx = new TransactionBuilder(Endpoint).TypeOperation(type, operationName, parameters, useGet).ToBundle();
        else
            tx = new TransactionBuilder(Endpoint).ResourceOperation(type, id, vid, operationName, parameters, useGet).ToBundle();

        //operation responses are expected to return 2xx codes.
        return executeAsync<Resource>(tx, _200Responses.Value, ct);
    }

    #endregion

    #region Get

    /// <summary>
    /// Invoke a general GET on the server. If the operation fails, then this method will throw an exception
    /// </summary>
    /// <param name="url">A relative or absolute url. If the url is absolute, it has to be located within the endpoint of the client.</param>
    /// <param name="ct"></param>
    /// <returns>A resource that is the outcome of the operation. The type depends on the definition of the operation at the given url</returns>
    /// <remarks>parameters to the method are simple, and are in the URL, and this is a GET operation</remarks>
    public async Task<Resource?> GetAsync(Uri url, CancellationToken? ct = null)
    {
        if (url == null) throw Error.ArgumentNull(nameof(url));

        var tx = new TransactionBuilder(Endpoint).Get(url).ToBundle();
        return await executeAsync<Resource>(tx, HttpStatusCode.OK, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Invoke a general GET on the server. If the operation fails, then this method will throw an exception
    /// </summary>
    /// <param name="url">A relative or absolute url. If the url is absolute, it has to be located within the endpoint of the client.</param>
    /// <param name="ct"></param>
    /// <returns>A resource that is the outcome of the operation. The type depends on the definition of the operation at the given url</returns>
    /// <remarks>parameters to the method are simple, and are in the URL, and this is a GET operation</remarks>
    public virtual Task<Resource?> GetAsync(string url, CancellationToken? ct = null)
    {
        if (url == null) throw Error.ArgumentNull(nameof(url));

        return GetAsync(new Uri(url, UriKind.RelativeOrAbsolute), ct);
    }

    #endregion

    #region ExecuteAsync

    public Task<TResource?> executeAsync<TResource>(Model.Bundle tx, HttpStatusCode expect, CancellationToken? ct) where TResource : Model.Resource
    {
        return executeAsync<TResource>(tx, new[] { expect }, ct);
    }

    private async Task<TResource?> executeAsync<TResource>(Bundle tx, IEnumerable<HttpStatusCode> expect, CancellationToken? ct) where TResource : Resource
    {
        var cancellation = ct ?? CancellationToken.None;

        cancellation.ThrowIfCancellationRequested();

        await verifyServerVersion(cancellation).ConfigureAwait(false);

        var request = tx.Entry[0];
        var maybeBinaryInteraction = new ResourceIdentity(request.Request.Url).ResourceType == "Binary";
        var requestMessage = request.ToHttpRequestMessage(
            Requester.BaseUrl,
            getSerializationEngine(),
            Settings.UseFhirVersionInAcceptHeader ? fhirVersion : null,
            Settings,
            maybeBinaryInteraction);

        using var responseMessage = await Requester.ExecuteAsync(requestMessage, cancellation).ConfigureAwait(false);

        return await extractResourceFromHttpResponse<TResource>(expect, responseMessage, entryComponent: request, useBinaryProtocol: maybeBinaryInteraction);
    }

    private async Task<TResource?> executeAsync<TResource>(HttpRequestMessage request, IEnumerable<HttpStatusCode> expect, CancellationToken? ct) where TResource : Resource
    {
        var cancellation = ct ?? CancellationToken.None;

        cancellation.ThrowIfCancellationRequested();

        using var responseMessage = await Requester.ExecuteAsync(request, cancellation).ConfigureAwait(false);

        return await extractResourceFromHttpResponse<TResource>(expect, responseMessage, request);
    }

    #endregion

    #region Utilities

    // Create our own and add decompression strategy in default handler.
    private static HttpClientHandler makeDefaultHandler() =>
        new()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        };

    private static Uri getValidatedEndpoint(Uri endpoint)
    {
        if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));

        endpoint = new Uri(endpoint.OriginalString.EnsureEndsWith("/"));

        if (!endpoint.IsAbsoluteUri) throw new ArgumentException("Endpoint must be absolute", nameof(endpoint));

        return endpoint;
    }
    private static ResourceIdentity verifyResourceIdentity(Uri location, bool needId, bool needVid)
    {
        var result = new ResourceIdentity(location);

        if (result.ResourceType == null) throw Error.Argument(nameof(location), "Must be a FHIR REST url containing the resource type in its path");
        if (needId && result.Id == null) throw Error.Argument(nameof(location), "Must be a FHIR REST url containing the logical id in its path");
        if (needVid && !result.HasVersion) throw Error.Argument(nameof(location), "Must be a FHIR REST url containing the version id in its path");

        return result;
    }

    // either msg or entryComponent should be set
    private async Task<TResource?> extractResourceFromHttpResponse<TResource>(IEnumerable<HttpStatusCode> expect, HttpResponseMessage responseMessage, HttpRequestMessage? msg = null, Bundle.EntryComponent? entryComponent = null, bool useBinaryProtocol = false) where TResource : Resource
    {
        if (msg is null && entryComponent is null) throw new ArgumentException("Either msg or entryComponent should be set");
        // Validate the response and throw the appropriate exceptions. Also, if we have *not* verified the FHIR version
        // of the server, add a suggestion about this in the (legacy) parsing exception.

        (LastResult, LastBody, LastBodyAsText, LastBodyAsResource, var issue) =
            await ValidateResponse(responseMessage, expect, getSerializationEngine(), !Settings.VerifyFhirVersion, fhirVersion, useBinaryProtocol)
                .ConfigureAwait(false);

        // If an error occurred while trying to interpret and validate the response, we will bail out now.
        if (issue is not null) throw issue;

        // If the response is an operation outcome, add it to response.outcome.
        // This is necessary for when a client uses return=OperationOutcome as a prefer header.
        // See also issue #1681.
        if (LastBodyAsResource is OperationOutcome oo)
            LastResult.Outcome = oo;

        // If the full representation was requested (using the Prefer header), but the server did not return the resource
        // (or it returned an OperationOutcome) - explicitly go out to the server to get the resource and return it. 
        // This behavior is only valid for PUT, POST and PATCH requests, where the server may device whether or not to return
        // the full body of the altered resource.
        var noRealBody = LastBodyAsResource is null || (LastBodyAsResource is OperationOutcome && string.IsNullOrEmpty(LastBodyAsResource.Id));
        var shouldFetchFullRepresentation = noRealBody
                                            && (msg is not null ? isPostOrPutOrPatch(msg.Method) : isPostOrPutOrPatch(entryComponent!))
                                            && Settings.ReturnPreference == ReturnPreference.Representation
                                            && LastResult.Location is { } fetchLocation
                                            && new ResourceIdentity(fetchLocation).IsRestResourceIdentity(); // Check that it isn't an operation too

        // NOTE: Since these lines may call GetAsync(), the executeAsync() method we're in might get called "recursively",
        // and all state (e.g. Last Result etc) will be overwritten from this point on.
        var execResult = shouldFetchFullRepresentation ?
            await GetAsync(LastResult.Location).ConfigureAwait(false) : LastBodyAsResource;

        // We have a success code (2xx), we have a body, but the body may not be of the type we expect.
        return execResult switch
        {
            // We have the expected resource type, fine!
            TResource resource => resource,

            // If this is an operationoutcome, that may still be all right. The OperationOutcome has 
            // been stored in the LastResult, and return null as the result of the function.
            OperationOutcome => null,

            // If there's nothing to return, return null. Note that the LastBodyXXXX properties can still contain useful data.
            null => null,

            // Unexpected response type in the body, throw.
            _ => throw new FhirOperationException(entryComponent is not null ? unexpectedBodyTypeForBundle(entryComponent.Request, execResult) : unexpectedBodyTypeForMessage(msg!, execResult), responseMessage.StatusCode)
        };

        static string unexpectedBodyTypeForBundle(Bundle.RequestComponent rc, Resource result) => $"Operation {rc.Method} on {rc.Url} " +
                                                                        $"expected a body of type {typeof(TResource).Name} but a {result.GetType().Name} was returned.";

        static string unexpectedBodyTypeForMessage(HttpRequestMessage msg, Resource result) => $"Operation {msg.Method} on {msg.RequestUri} " +
                                                                        $"expected a body of type {typeof(TResource).Name} but a {result.GetType().Name} was returned.";
    }

    /// <summary>
    /// Validates the <see cref="HttpResponseMessage"/> and throws the appropriate exceptions.
    /// It also simulates the exception-throwing behaviour of the original TypedElement-based parsers.
    /// </summary>        
    /// <exception cref="FhirOperationException">The body content type could not be handled or the response status indicated failure, or we received an unexpected success status.</exception>
    /// <exception cref="FormatException">Thrown when the original ITypedElement-based parsers are used and a parse exception occurred.</exception>
    /// <exception cref="DeserializationFailedException">Thrown when a newer parsers is used and a parse exception occurred.</exception>
    /// <seealso cref="HttpContentParsers.ExtractResponseData(HttpResponseMessage, IFhirSerializationEngine, bool, FhirRelease)"/>
    internal static async Task<ResponseData> ValidateResponse(
        HttpResponseMessage responseMessage,
        IEnumerable<HttpStatusCode> expect,
        IFhirSerializationEngine engine,
        bool suggestVersionOnParsingError,
        string fhirVersion,
        bool useBinaryProtocol)
    {
        var fhirRelease = FhirReleaseParser.Parse(fhirVersion);

        var responseData = (await responseMessage.ExtractResponseData(engine, useBinaryProtocol, fhirRelease).ConfigureAwait(false))
            .TranslateUnsupportedBodyTypeException(responseMessage.StatusCode)
            .TranslateLegacyParserException(suggestVersionOnParsingError ? fhirVersion : null);

        // If extracting the data caused an issue, return it immediately
        if (responseData.Issue is not null)
            return responseData;

        // Body is ok, but the operation failed, signal this with a FhirOperationException.
        else if (!responseMessage.IsSuccessStatusCode)
            return responseData with
            {
                Issue = FhirOperationException.BuildFhirOperationException(responseMessage.StatusCode, responseData.BodyResource, responseData.BodyText)
            };

        // Body ok, and operation succeeded, but if the result was unexpected, report failure anyway.
        else if (!expect.Contains(responseMessage.StatusCode))
            return responseData with
            {
                Issue = new FhirOperationException(
                    $"Operation concluded successfully, but the return status {responseMessage.StatusCode} was unexpected",
                    responseMessage.StatusCode)
            };

        // We're all good!               
        else
            return responseData;
    }

    private static bool isPostOrPutOrPatch(Bundle.EntryComponent interaction) =>
        interaction.Request.Method is Bundle.HTTPVerb.POST or Bundle.HTTPVerb.PUT or Bundle.HTTPVerb.PATCH;

    private static bool isPostOrPutOrPatch(HttpMethod method) =>
        method == HttpMethod.Post || method == HttpMethod.Put || method == new HttpMethod("PATCH");

    private bool _versionChecked = false;


    private IFhirSerializationEngine getSerializationEngine()
    {
        return Settings.SerializationEngine ?? FhirSerializationEngineFactory.Legacy.FromParserSettings(Inspector, Settings.ParserSettings ?? new());
    }

    private async Task verifyServerVersion(CancellationToken ct)
    {
        if (!Settings.VerifyFhirVersion) return;

        if (_versionChecked) return;
        _versionChecked = true;      // So we can now start calling Conformance() without getting into a loop

        string? serverVersion;
        var settings = Settings;

        try
        {
            Settings = Settings.Clone();
            Settings.ParserSettings = new() { AllowUnrecognizedEnums = true };
            serverVersion = await getFhirVersionOfServer(ct).ConfigureAwait(false);
        }
        catch (FormatException fe)
        {
            // Mmmm...cannot even read the body. Probably not so good.
            throw Error.NotSupported($"Cannot read the conformance statement of the server to verify FHIR version compatibility: {fe.Message}");
        }
        finally
        {
            // put back the original settings
            Settings = settings;
        }

        if (serverVersion == null)
        {
            throw Error.NotSupported($"This CapabilityStatement of the server doesn't state its FHIR version");
        }
        else if (!SemVersion.CheckMinorVersionCompatibility(fhirVersion, serverVersion))
        {
            throw Error.NotSupported($"This client supports FHIR version {fhirVersion} but the server uses version {serverVersion}");
        }

    }

    private async Task<string?> getFhirVersionOfServer(CancellationToken ct)
    {
        var tx = new TransactionBuilder(Endpoint).CapabilityStatement(SummaryType.True).ToBundle();
        var capabilityStatement = await executeAsync<Resource>(tx, HttpStatusCode.OK, ct).ConfigureAwait(false);
        if (capabilityStatement is null) return null;

        return capabilityStatement.AsReadOnlyDictionary().TryGetValue("fhirVersion", out var value) && value is PrimitiveType pt && pt.ObjectValue is string version
            ? version
            : null;
    }

    private string typeNameOrDie<TResource>() => Inspector.GetFhirTypeNameForType(typeof(TResource)) ??
                                                 throw new ArgumentException($"Type parameter {nameof(TResource)} is not a known resource.");



    #endregion

    #region IDisposable Support
    protected bool disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                if (Requester is IDisposable disposableRequester)
                {
                    disposableRequester.Dispose();
                }
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    #endregion
}

#nullable restore