#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Hl7.Fhir.Rest;

public partial class BaseFhirClient
{
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
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual TResource? Read<TResource>(Uri location, string? ifNoneMatch = null,
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
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual TResource? Read<TResource>(string location, string? ifNoneMatch = null,
        DateTimeOffset? ifModifiedSince = null) where TResource : Resource
    {
        return ReadAsync<TResource>(location, ifNoneMatch, ifModifiedSince).WaitResult();
    }
    
    /// <summary>
    /// Refreshes the data in the resource passed as an argument by re-reading it from the server
    /// </summary>
    /// <typeparam name="TResource"></typeparam>
    /// <param name="current">The resource for which you want to get the most recent version.</param>
    /// <returns>A new instance of the resource, containing the most up-to-date data</returns>
    /// <remarks>This function will not overwrite the argument with new data, rather it will return a new instance
    /// which will have the newest data, leaving the argument intact.</remarks>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual TResource? Refresh<TResource>(TResource current) where TResource : Resource
    {
        return RefreshAsync<TResource>(current).WaitResult();
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
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual TResource? Update<TResource>(TResource resource, bool versionAware = false) where TResource : Resource
    {
        return UpdateAsync(resource, versionAware).WaitResult();
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
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual TResource? Update<TResource>(TResource resource, SearchParams condition, bool versionAware = false)
        where TResource : Resource
    {
        return ConditionalUpdateAsync(resource, condition, versionAware).WaitResult();
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
    [Obsolete("This overload will be replaced by ConditionalUpdateAsync(). Using the new method is recommended")]
    public virtual Task<TResource?> UpdateAsync<TResource>(TResource resource, SearchParams condition, bool versionAware = false, CancellationToken? ct = null) where TResource : Resource
    {
        return ConditionalUpdateAsync(resource, condition, versionAware, ct);
    }
    
    /// <summary>
    /// Conditionally delete a resource
    /// </summary>
    /// <param name="resourceType">The type of resource to delete</param>
    /// <param name="condition">Criteria to use to match the resource to delete.</param>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual void Delete(string resourceType, SearchParams condition)
    {
        DeleteAsync(resourceType, condition).WaitNoResult();
    }
        
    /// <summary>
    /// Delete a resource at the given endpoint.
    /// </summary>
    /// <param name="location">endpoint of the resource to delete</param>
    /// <returns>Throws an exception when the delete failed, though this might
    /// just mean the server returned 404 (the resource didn't exist before) or 410 (the resource was
    /// already deleted).</returns>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual void Delete(Uri location)
    {
        DeleteAsync(location).WaitNoResult();
    }
        
    /// <summary>
    /// Delete a resource
    /// </summary>
    /// <param name="resource">The resource to delete</param>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual void Delete(Resource resource)
    {
        DeleteAsync(resource).WaitNoResult();
    }
        
    /// <summary>
    /// Delete a resource at the given endpoint.
    /// </summary>
    /// <param name="location">endpoint of the resource to delete</param>
    /// <returns>Throws an exception when the delete failed, though this might
    /// just mean the server returned 404 (the resource didn't exist before) or 410 (the resource was
    /// already deleted).</returns>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual void Delete(string location)
    {
        DeleteAsync(location).WaitNoResult();
    }
    
    /// <summary>
    /// Conditionally delete a resource
    /// </summary>
    /// <param name="resourceType">The type of resource to delete</param>
    /// <param name="condition">Criteria to use to match the resource to delete.</param>
    /// <param name="ct"></param>
    [Obsolete("This overload will be replaced by ConditionalDeleteSingleAsync() and ConditionalDeleteMultipleAsync(). Using the new methods is recommended.")]
    public virtual async Task DeleteAsync(string resourceType, SearchParams condition, CancellationToken? ct = null)
    {
        if (resourceType == null) throw Error.ArgumentNull(nameof(resourceType));
        if (condition == null) throw Error.ArgumentNull(nameof(condition));

        var tx = new TransactionBuilder(Endpoint).ConditionalDeleteSingle(condition, resourceType).ToBundle();
        await executeAsync<Resource>(tx, new[] { HttpStatusCode.OK, HttpStatusCode.NoContent }, ct).ConfigureAwait(false);
    }
    
    /// <summary>
    /// Create a resource on a FHIR endpoint
    /// </summary>
    /// <param name="resource">The resource instance to create</param>
    /// <returns>The resource as created on the server, or an exception if the create failed.</returns>
    /// <typeparam name="TResource">The type of resource to create</typeparam>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual TResource? Create<TResource>(TResource resource) where TResource : Resource
    {
        return CreateAsync(resource).WaitResult();
    }
    
    /// <inheritdoc cref="CreateAsync{TResource}(TResource, SearchParams, CancellationToken?)"/>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual TResource? Create<TResource>(TResource resource, SearchParams condition) where TResource : Resource
    {
        return ConditionalCreateAsync(resource, condition).WaitResult();
    }
    
    /// <summary>
    /// Conditionally Create a resource on a FHIR endpoint
    /// </summary>
    /// <param name="resource">The resource instance to create</param>
    /// <param name="condition">The criteria</param>
    /// <param name="ct"></param>
    /// <returns>The resource as created on the server, or an exception if the create failed.</returns>
    /// <typeparam name="TResource">The type of resource to create</typeparam>
    [Obsolete("This overload will be replaced by ConditionalCreateAsync(). Using the new method is recommended")]
    public virtual Task<TResource?> CreateAsync<TResource>(TResource resource, SearchParams condition, CancellationToken? ct = null) where TResource : Resource
    {
        return ConditionalCreateAsync(resource, condition, ct);
    }
    
    ///<inheritdoc cref="PatchAsync(Uri, Parameters, CancellationToken?)"/>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Resource? Patch(Uri location, Parameters patchParameters)
    {
        return PatchAsync(location, patchParameters).WaitResult();
    }
    
    ///<inheritdoc cref="PatchAsync{TResource}(string, Parameters, string, CancellationToken?)"/>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual TResource? Patch<TResource>(string id, Parameters patchParameters, string? versionId = null) where TResource : Resource
    {
        return PatchAsync<TResource>(id, patchParameters, versionId).WaitResult();
    }
    
    ///<inheritdoc cref="PatchAsync{TResource}(SearchParams, Parameters, CancellationToken?)"/>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual TResource? Patch<TResource>(SearchParams condition, Parameters patchParameters) where TResource : Resource
    {
        return ConditionalPatchAsync<TResource>(condition, patchParameters).WaitResult();
    }
    
    /// <summary>
    /// Conditionally patch a resource on a FHIR Endpoint
    /// </summary>
    /// <typeparam name="TResource">Type of resource to patch</typeparam>
    /// <param name="condition">Criteria used to locate the resource to update</param>
    /// <param name="patchParameters">A Parameters resource that includes the patch operation(s) to perform</param>
    /// <param name="ct"></param>
    /// <returns>The patched resource</returns>
    [Obsolete("This overload will be replaced by ConditionalPatchAsync(). Using the new method is recommended")]
    public Task<TResource?> PatchAsync<TResource>(SearchParams condition, Parameters patchParameters, CancellationToken? ct = null) where TResource : Resource
    {
        return ConditionalPatchAsync<TResource>(condition, patchParameters, ct);
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
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public Bundle? TypeHistory(string resourceType, DateTimeOffset? since = null, int? pageSize = null, SummaryType? summary = null)
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
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public Bundle? TypeHistory<TResource>(DateTimeOffset? since = null, int? pageSize = null, SummaryType? summary = null) where TResource : Resource, new()
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
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public Bundle? History(Uri location, DateTimeOffset? since = null, int? pageSize = null, SummaryType? summary = null)
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
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public Bundle? History(string location, DateTimeOffset? since = null, int? pageSize = null, SummaryType? summary = null)
    {
        return HistoryAsync(location, since, pageSize, summary).WaitResult();
    }
    
    /// <summary>
    /// Retrieve the full version history of the server
    /// </summary>
    /// <param name="since">Optional. Returns only changes after the given date</param>
    /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
    /// <param name="summary">Optional. Indicates whether the returned resources should just contain the minimal set of elements</param>
    /// <returns>A bundle with the history for the indicated instance, may contain both 
    /// ResourceEntries and DeletedEntries.</returns>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public Bundle? WholeSystemHistory(DateTimeOffset? since = null, int? pageSize = null, SummaryType? summary = null)
    {
        return WholeSystemHistoryAsync(since, pageSize, summary).WaitResult();
    }
    
    /// <summary>
    /// Send a set of creates, updates and deletes to the server to be processed in one transaction
    /// </summary>
    /// <param name="bundle">The bundled creates, updates and deleted</param>
    /// <returns>A bundle as returned by the server after it has processed the transaction, or 
    /// a FhirOperationException will be thrown if an error occurred.</returns>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public Bundle? Transaction(Bundle bundle)
    {
        return TransactionAsync(bundle).WaitResult();
    }
    
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Resource? WholeSystemOperation(string operationName, Parameters? parameters = null, bool useGet = false)
    {
        return WholeSystemOperationAsync(operationName, parameters, useGet).WaitResult();
    }
    
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Resource? TypeOperation<TResource>(string operationName, Parameters? parameters = null,
        bool useGet = false) where TResource : Resource
    {
        return TypeOperationAsync<TResource>(operationName, parameters, useGet).WaitResult();
    }
    
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Resource? TypeOperation(string operationName, string typeName, Parameters? parameters = null, bool useGet = false)
    {
        return TypeOperationAsync(operationName, typeName, parameters, useGet).WaitResult();
    }
    
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Resource? InstanceOperation(Uri location, string operationName, Parameters? parameters = null, bool useGet = false)
    {
        return InstanceOperationAsync(location, operationName, parameters, useGet).WaitResult();
    }
    
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Resource? Operation(Uri location, string operationName, Parameters? parameters = null, bool useGet = false)
    {
        return OperationAsync(location, operationName, parameters, useGet).WaitResult();
    }
    
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Resource? Operation(Uri operation, Parameters? parameters = null, bool useGet = false)
    {
        return OperationAsync(operation, parameters, useGet).WaitResult();
    }
    
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Resource? ProcessMessage(Bundle messageBundle, bool async = false, string? responseUrl = null)
    {
        return ProcessMessageAsync(messageBundle, async, responseUrl).WaitResult();
    }
    
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    private Resource? internalOperation(string operationName, string? type = null, string? id = null,
        string? vid = null, Parameters? parameters = null, bool useGet = false) =>
        internalOperationAsync(operationName, type, id, vid, parameters, useGet).WaitResult();
    
    /// <summary>
    /// Invoke a general GET on the server. If the operation fails, then this method will throw an exception
    /// </summary>
    /// <param name="url">A relative or absolute url. If the url is absolute, it has to be located within the endpoint of the client.</param>
    /// <returns>A resource that is the outcome of the operation. The type depends on the definition of the operation at the given url</returns>
    /// <remarks>parameters to the method are simple, and are in the URL, and this is a GET operation</remarks>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Resource? Get(Uri url)
    {
        return GetAsync(url).WaitResult();
    }
    
    /// <summary>
    /// Invoke a general GET on the server. If the operation fails, then this method will throw an exception
    /// </summary>
    /// <param name="url">A relative or absolute url. If the url is absolute, it has to be located within the endpoint of the client.</param>
    /// <returns>A resource that is the outcome of the operation. The type depends on the definition of the operation at the given url</returns>
    /// <remarks>parameters to the method are simple, and are in the URL, and this is a GET operation</remarks>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Resource? Get(string url)
    {
        return GetAsync(url).WaitResult();
    }
    
    /// <summary>
    /// Search for Resources based on criteria specified in a Query resource
    /// </summary>
    /// <param name="q">The Query resource containing the search parameters</param>
    /// <param name="resourceType">The type of resource to filter on (optional). If not specified, will search on all resource types.</param>
    /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Bundle? Search(SearchParams q, string? resourceType = null)
    {
        return SearchAsync(q, resourceType).WaitResult();
    }
    
    /// <summary>
    /// Search for Resources based on criteria specified in a Query resource
    /// </summary>
    /// <param name="q">The Query resource containing the search parameters</param>
    /// <param name="resourceType">The type of resource to filter on (optional). If not specified, will search on all resource types.</param>
    /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Bundle? SearchUsingPost(SearchParams q, string? resourceType = null)
    {
        return SearchUsingPostAsync(q, resourceType).WaitResult();
    }
    
    /// <summary>
    /// Search for Resources based on criteria specified in a Query resource
    /// </summary>
    /// <param name="q">The Query resource containing the search parameters</param>
    /// <typeparam name="TResource">The type of resource to filter on</typeparam>
    /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Bundle? Search<TResource>(SearchParams q) where TResource : Resource
    {
        return SearchAsync<TResource>(q).WaitResult();
    }
    
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Bundle? SearchUsingPost<TResource>(SearchParams q) where TResource : Resource
    {
        return SearchUsingPostAsync(q, Inspector.GetFhirTypeNameForType(typeof(TResource))).WaitResult();
    }
    
    /// <summary>
    /// Search for Resources of a certain type that match the given criteria
    /// </summary>
    /// <param name="criteria">Optional. The search parameters to filter the resources on. Each
    /// given string is a combined key/value pair (separated by '=')</param>
    /// <param name="includes">Optional. A list of include paths</param>
    /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
    /// <param name="summary">Optional. Whether to include only return a summary of the resources in the Bundle</param>
    /// <param name="revIncludes">Optional. A list of reverse include paths</param>
    /// <typeparam name="TResource">The type of resource to list</typeparam>
    /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
    /// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
    /// of all resources of the given Resource type</remarks>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Bundle? Search<TResource>(string[]? criteria = null, string[]? includes = null, int? pageSize = null,
        SummaryType? summary = null, string[]? revIncludes = null)
        where TResource : Resource, new()
    {
        return SearchAsync<TResource>(criteria, includes, pageSize, summary, revIncludes).WaitResult();
    }
    
    /// <summary>
    /// Search for Resources of a certain type that match the given criteria
    /// </summary>
    /// <param name="criteria">Optional. The search parameters to filter the resources on. Each
    /// given string is a combined key/value pair (separated by '=')</param>
    /// <param name="includes">Optional. A list of include paths</param>
    /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
    /// <param name="summary">Optional. Whether to include only return a summary of the resources in the Bundle</param>
    /// <param name="revIncludes">Optional. A list of reverse include paths</param>
    /// <typeparam name="TResource">The type of resource to list</typeparam>
    /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
    /// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
    /// of all resources of the given Resource type</remarks>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Bundle? SearchUsingPost<TResource>(string[]? criteria = null, string[]? includes = null, int? pageSize = null,
        SummaryType? summary = null, string[]? revIncludes = null)
        where TResource : Resource, new()
    {
        return SearchUsingPostAsync<TResource>(criteria, includes, pageSize, summary, revIncludes).WaitResult();
    }

    ///<inheritdoc cref="SearchUsingPost{TResource}(string[], string[], int?, SummaryType?, string[])"/>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Bundle? SearchUsingPost<TResource>(string[] criteria, (string path, IncludeModifier modifier)[]? includes, int? pageSize,
        SummaryType? summary, (string path, IncludeModifier modifier)[]? revIncludes)
        where TResource : Resource, new()
    {
        return SearchUsingPostAsync<TResource>(criteria, includes, pageSize, summary, revIncludes).WaitResult();
    }
    
    /// <summary>
    /// Search for Resources of a certain type that match the given criteria
    /// </summary>
    /// <param name="resource">The type of resource to search for</param>
    /// <param name="criteria">Optional. The search parameters to filter the resources on. Each
    /// given string is a combined key/value pair (separated by '=')</param>
    /// <param name="includes">Optional. A list of include paths</param>
    /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
    /// <param name="summary">Optional. Whether to include only return a summary of the resources in the Bundle</param>
    /// <param name="revIncludes">Optional. A list of reverse include paths</param>
    /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
    /// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
    /// of all resources of the given Resource type</remarks>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Bundle? Search(string resource, string[]? criteria = null, string[]? includes = null, int? pageSize = null,
        SummaryType? summary = null, string[]? revIncludes = null)
    {
        return SearchAsync(resource, criteria, includes, pageSize, summary, revIncludes).WaitResult();
    }

    ///<inheritdoc cref="Search(string, string[], string[], int?, SummaryType?, string[])"/>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Bundle? Search(string resource, string[] criteria, (string path, IncludeModifier modifier)[] includes, int? pageSize,
        SummaryType? summary, (string path, IncludeModifier modifier)[] revIncludes)
    {
        return SearchAsync(resource, criteria, includes, pageSize, summary, revIncludes).WaitResult();
    }
    
    /// <summary>
    /// Search for Resources of a certain type that match the given criteria
    /// </summary>
    /// <param name="resource">The type of resource to search for</param>
    /// <param name="criteria">Optional. The search parameters to filter the resources on. Each
    /// given string is a combined key/value pair (separated by '=')</param>
    /// <param name="includes">Optional. A list of include paths</param>
    /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
    /// <param name="summary">Optional. Whether to include only return a summary of the resources in the Bundle</param>
    /// <param name="revIncludes">Optional. A list of reverse include paths</param>
    /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
    /// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
    /// of all resources of the given Resource type</remarks>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Bundle? SearchUsingPost(string resource, string[]? criteria = null, string[]? includes = null, int? pageSize = null,
        SummaryType? summary = null, string[]? revIncludes = null)
    {
        return SearchUsingPostAsync(resource, criteria, includes, pageSize, summary, revIncludes).WaitResult();
    }

    ///<inheritdoc cref="SearchUsingPost(string, string[], string[], int?, SummaryType?, string[])"/>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Bundle? SearchUsingPost(string resource, string[]? criteria, (string path, IncludeModifier modifier)[]? includes, int? pageSize,
        SummaryType? summary, (string path, IncludeModifier modifier)[]? revIncludes)
    {
        return SearchUsingPostAsync(resource, criteria, includes, pageSize, summary, revIncludes).WaitResult();
    }
    
    /// <summary>
    /// Search for Resources across the whole server that match the given criteria
    /// </summary>
    /// <param name="criteria">Optional. The search parameters to filter the resources on. Each
    /// given string is a combined key/value pair (separated by '=')</param>
    /// <param name="includes">Optional. A list of include paths</param>
    /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
    /// <param name="summary">Optional. Whether to include only return a summary of the resources in the Bundle</param>
    /// <param name="revIncludes">Optional. A list of reverse include paths</param>
    /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
    /// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
    /// of all resources of the given Resource type</remarks>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Bundle? WholeSystemSearch(string[]? criteria = null, string[]? includes = null, int? pageSize = null,
        SummaryType? summary = null, string[]? revIncludes = null)
    {
        return WholeSystemSearchAsync(criteria, includes, pageSize, summary, revIncludes).WaitResult();
    }

    ///<inheritdoc cref="WholeSystemSearch(string[], string[], int?, SummaryType?, string[])"/>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Bundle? WholeSystemSearch(string[]? criteria, (string path, IncludeModifier modifier)[]? includes, int? pageSize,
        SummaryType? summary, (string path, IncludeModifier modifier)[]? revIncludes)
    {
        return WholeSystemSearchAsync(criteria, includes, pageSize, summary, revIncludes).WaitResult();
    }
    
    /// <summary>
    /// Search for Resources across the whole server that match the given criteria
    /// </summary>
    /// <param name="criteria">Optional. The search parameters to filter the resources on. Each
    /// given string is a combined key/value pair (separated by '=')</param>
    /// <param name="includes">Optional. A list of include paths</param>
    /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
    /// <param name="summary">Optional. Whether to include only return a summary of the resources in the Bundle</param>
    /// <param name="revIncludes">Optional. A list of reverse include paths</param>
    /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
    /// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
    /// of all resources of the given Resource type</remarks>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Bundle? WholeSystemSearchUsingPost(string[]? criteria = null, string[]? includes = null, int? pageSize = null,
        SummaryType? summary = null, string[]? revIncludes = null)
    {
        return WholeSystemSearchUsingPostAsync(criteria, includes, pageSize, summary, revIncludes).WaitResult();
    }

    ///<inheritdoc cref="WholeSystemSearchUsingPost(string[], string[], int?, SummaryType?, string[])"/>       
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Bundle? WholeSystemSearchUsingPost(string[]? criteria, (string path, IncludeModifier modifier)[]? includes, int? pageSize,
        SummaryType? summary, (string path, IncludeModifier modifier)[]? revIncludes)
    {
        return WholeSystemSearchUsingPostAsync(criteria, includes, pageSize, summary, revIncludes).WaitResult();
    }
    
    /// <summary>
    /// Search for resources based on a resource's id.
    /// </summary>
    /// <param name="id">The id of the resource to search for</param>
    /// <param name="includes">Zero or more include paths</param>
    /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
    /// <param name="revIncludes">Optional. A list of reverse include paths</param>
    /// <typeparam name="TResource">The type of resource to search for</typeparam>
    /// <returns>A Bundle with the BundleEntry as identified by the id parameter or an empty
    /// Bundle if the resource wasn't found.</returns>
    /// <remarks>This operation is similar to Read, but additionally,
    /// it is possible to specify include parameters to include resources in the bundle that the
    /// returned resource refers to.</remarks>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Bundle? SearchById<TResource>(string id, string[]? includes = null, int? pageSize = null, string[]? revIncludes = null) where TResource : Resource, new()
    {
        return SearchByIdAsync<TResource>(id, includes, pageSize, revIncludes).WaitResult();
    }


    ///<inheritdoc cref="SearchById(string, string, string[], int?, string[])"/>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Bundle? SearchById<TResource>(string id, (string path, IncludeModifier modifier)[]? includes, int? pageSize,
        (string path, IncludeModifier modifier)[]? revIncludes) where TResource : Resource, new()
    {
        return SearchByIdAsync<TResource>(id, includes, pageSize, revIncludes).WaitResult();
    }
    
    /// <summary>
    /// Search for resources based on a resource's id.
    /// </summary>
    /// <param name="id">The id of the resource to search for</param>
    /// <param name="includes">Zero or more include paths</param>
    /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
    /// <param name="revIncludes">Optional. A list of reverse include paths</param>
    /// <typeparam name="TResource">The type of resource to search for</typeparam>
    /// <returns>A Bundle with the BundleEntry as identified by the id parameter or an empty
    /// Bundle if the resource wasn't found.</returns>
    /// <remarks>This operation is similar to Read, but additionally,
    /// it is possible to specify include parameters to include resources in the bundle that the
    /// returned resource refers to.</remarks>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Bundle? SearchByIdUsingPost<TResource>(string id, string[]? includes = null, int? pageSize = null, string[]? revIncludes = null) where TResource : Resource, new()
    {
        return SearchByIdUsingPostAsync<TResource>(id, includes, pageSize, revIncludes).WaitResult();
    }

    ///<inheritdoc cref="SearchByIdUsingPost{TResource}(string, string[], int?, string[])"/>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Bundle? SearchByIdUsingPost<TResource>(string id, (string path, IncludeModifier modifier)[]? includes, int? pageSize,
        (string path, IncludeModifier modifier)[] revIncludes) where TResource : Resource, new()
    {
        return SearchByIdUsingPostAsync<TResource>(id, includes, pageSize, revIncludes).WaitResult();
    }
    
    /// <summary>
    /// Search for resources based on a resource's id.
    /// </summary>
    /// <param name="resource">The type of resource to search for</param>
    /// <param name="id">The id of the resource to search for</param>
    /// <param name="includes">Zero or more include paths</param>
    /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
    /// <param name="revIncludes">Optional. A list of reverse include paths</param>
    /// <returns>A Bundle with the BundleEntry as identified by the id parameter or an empty
    /// Bundle if the resource wasn't found.</returns>
    /// <remarks>This operation is similar to Read, but additionally,
    /// it is possible to specify include parameters to include resources in the bundle that the
    /// returned resource refers to.</remarks>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Bundle? SearchById(string resource, string id, string[]? includes = null, int? pageSize = null, string[]? revIncludes = null)
    {
        return SearchByIdAsync(resource, id, includes, pageSize, revIncludes).WaitResult();
    }

    ///<inheritdoc cref="SearchById(string, string, string[], int?, string[])"/>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Bundle? SearchById(string resource, string id, (string path, IncludeModifier modifier)[]? includes, int? pageSize,
        (string path, IncludeModifier modifier)[]? revIncludes)
    {
        return SearchByIdAsync(resource, id, includes, pageSize, revIncludes).WaitResult();
    }
    
    /// <summary>
    /// Search for resources based on a resource's id.
    /// </summary>
    /// <param name="resource">The type of resource to search for</param>
    /// <param name="id">The id of the resource to search for</param>
    /// <param name="includes">Zero or more include paths</param>
    /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
    /// <param name="revIncludes">Optional. A list of reverse include paths</param>
    /// <returns>A Bundle with the BundleEntry as identified by the id parameter or an empty
    /// Bundle if the resource wasn't found.</returns>
    /// <remarks>This operation is similar to Read, but additionally,
    /// it is possible to specify include parameters to include resources in the bundle that the
    /// returned resource refers to.</remarks>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Bundle? SearchByIdUsingPost(string resource, string id, string[]? includes = null, int? pageSize = null, string[]? revIncludes = null)
    {
        return SearchByIdUsingPostAsync(resource, id, includes, pageSize, revIncludes).WaitResult();
    }

    ///<inheritdoc cref="SearchByIdUsingPost(string, string, string[], int?, string[])"/>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Bundle? SearchByIdUsingPost(string resource, string id, (string path, IncludeModifier modifier)[]? includes, int? pageSize,
        (string path, IncludeModifier modifier)[]? revIncludes)
    {
        return SearchByIdUsingPostAsync(resource, id, includes, pageSize, revIncludes).WaitResult();
    }
    
    /// <summary>
    /// Uses the FHIR paging mechanism to go navigate around a series of paged result Bundles
    /// </summary>
    /// <param name="current">The bundle as received from the last response</param>
    /// <param name="direction">Optional. Direction to browse to, default is the next page of results.</param>
    /// <returns>A bundle containing a new page of results based on the browse direction, or null if
    /// the server did not have more results in that direction.</returns>
    [Obsolete("Synchronous use of the FhirClient is strongly discouraged, use the asynchronous call instead.")]
    public virtual Bundle? Continue(Bundle current, PageDirection direction = PageDirection.Next)
    {
        return ContinueAsync(current, direction).WaitResult();
    }
}