/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Rest;

/// <summary>
/// Builder to describe a FHIR transaction Bundle
/// </summary>
public partial class TransactionBuilder
{
    public const string HISTORY = ResourceIdentity.HISTORY;
    public const string METADATA = "metadata";
    public const string OPERATIONPREFIX = "$";

    private readonly Bundle _result;
    private readonly Uri _baseUrl;

    /// <summary>
    /// Create a builder to describe a FHIR transaction Bundle
    /// </summary>
    /// <param name="baseUrl">URL of the FHIR server that is going to execute the transaction/batch</param>
    /// <param name="type">Specify that the server should perform a "batch" or a "transaction"</param>
    public TransactionBuilder(string baseUrl, Bundle.BundleType type = Bundle.BundleType.Batch)
    {
        _result = new Bundle()
        {
            Type = type
        };

        _baseUrl = new Uri(baseUrl);
    }

    /// <summary>
    /// Create a builder to describe a FHIR transaction Bundle
    /// </summary>
    /// <param name="baseUri">URL of the FHIR server that is going to execute the transaction/batch</param>
    /// <param name="type">Specify that the server should perform a "batch" or a "transaction"</param>
    public TransactionBuilder(Uri baseUri, Bundle.BundleType type = Bundle.BundleType.Batch)
        : this(baseUri.OriginalString, type)
    {
    }
        
    public Bundle ToBundle()
    {
        return _result;
    }

    #region Get

    /// <summary>
    /// Add a "GET" entry to the transaction/batch
    /// </summary>
    /// <param name="uri">relative or absolute uri of the resource the transaction is supposed to return</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    public TransactionBuilder Get(Uri uri, string? bundleEntryFullUrl = null)
    {
        var entry = newEntry(Bundle.HTTPVerb.GET, InteractionType.Unspecified, bundleEntryFullUrl);

        if (uri.IsAbsoluteUri)
            addEntry(entry, new RestUrl(uri));
        else
        {
            var absoluteUrl = HttpUtil.MakeAbsoluteToBase(uri, _baseUrl);
            addEntry(entry, new RestUrl(absoluteUrl));
        }
        return this;
    }

    /// <summary>
    /// Add a "GET" entry to the transaction/batch
    /// </summary>
    /// <param name="url">relative or absolute url the transaction is supposed to "get"</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>\
    public TransactionBuilder Get(string url, string? bundleEntryFullUrl = null)
    {
        return Get(new Uri(url, UriKind.RelativeOrAbsolute), bundleEntryFullUrl);
    }

    #endregion

    #region Read

    /// <summary>
    /// Add a "read" entry to the transaction/batch that returns a specific resource
    /// </summary>
    /// <param name="resourceType">type of the resource</param>
    /// <param name="id">id of the resource</param>
    /// <param name="versionId">optional version id of the resource</param>
    /// <param name="ifModifiedSince">optional date that specifies the resource is only to be returned if it has been modified after that date</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    public TransactionBuilder Read(string resourceType, string id, string? versionId = null, DateTimeOffset? ifModifiedSince = null, string? bundleEntryFullUrl = null)
    {
        var entry = newEntry(Bundle.HTTPVerb.GET, InteractionType.Read, bundleEntryFullUrl);
        entry.Request!.IfNoneMatch = createIfMatchETag(versionId);
        entry.Request.IfModifiedSince = ifModifiedSince;
        var path = newRestUrl().AddPath(resourceType, id);
        addEntry(entry, path);

        return this;
    }
        
    /// <summary>
    /// Add a "version read" entry to that transaction/batch
    /// </summary>
    /// <param name="resourceType">type of the resource</param>
    /// <param name="id">id of the resource</param>
    /// <param name="vid">version id of the resource</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    public TransactionBuilder VRead(string resourceType, string id, string vid, string? bundleEntryFullUrl = null)
    {
        var entry = newEntry(Bundle.HTTPVerb.GET, InteractionType.VRead, bundleEntryFullUrl);
        var path = newRestUrl().AddPath(resourceType, id, HISTORY, vid);
        addEntry(entry, path);

        return this;
    }

    #endregion
        
    #region Update
        
    /// <summary>
    /// Add an "update" entry to the transaction/batch
    /// </summary>
    /// <param name="id">id of the resource</param>
    /// <param name="body">The newer version of the resource to be updated</param>
    /// <param name="versionId">optional version id of the resource</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    public TransactionBuilder Update(string id, Resource body, string? versionId = null, string? bundleEntryFullUrl = null)
    {
        var entry = newEntry(Bundle.HTTPVerb.PUT, InteractionType.Update, bundleEntryFullUrl);
        entry.Resource = body;
        entry.Request!.IfMatch = createIfMatchETag(versionId);
        var path = newRestUrl().AddPath(body.TypeName, id);
        addEntry(entry, path);

        return this;
    }

    /// <summary>
    /// Add a "conditional update" entry to the transaction/batch
    /// </summary>
    /// <param name="condition">conditions on which a resource is supposed to be updated or not</param>
    /// <param name="body">the new version of the resource to be updated</param>
    /// <param name="versionId">optional version id of the resource</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    public TransactionBuilder ConditionalUpdate(SearchParams condition, Resource body, string? versionId = null, string? bundleEntryFullUrl = null)
    {
        var entry = newEntry(Bundle.HTTPVerb.PUT, InteractionType.ConditionalUpdate, bundleEntryFullUrl);
        entry.Resource = body;
        entry.Request!.IfMatch = createIfMatchETag(versionId);
        var path = newRestUrl().AddPath(body.TypeName);
        path.AddParams(condition.ToUriParamList());
        addEntry(entry, path);

        return this;
    }
        
    #endregion
        
    #region Patch

    /// <summary>
    /// Add a "patch" entry to the transaction/batch
    /// </summary>
    /// <param name="resourceType">type of the resource to be patched</param>
    /// <param name="id">id of the resource to be patched</param>
    /// <param name="body">parameters resource that describes the patch operation</param>
    /// <param name="versionId">optional version id of the resource</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    public TransactionBuilder Patch(string resourceType, string id, Parameters body, string? versionId = null, string? bundleEntryFullUrl = null)
    {
        var entry = newEntry(Bundle.HTTPVerb.PATCH, InteractionType.Patch, bundleEntryFullUrl);
        entry.Resource = body;
        entry.Request!.IfMatch = createIfMatchETag(versionId);
        var path = newRestUrl().AddPath(resourceType, id);
        addEntry(entry, path);

        return this;
    }

    /// <summary>
    /// Add a "conditional patch" entry to the transaction/batch
    /// </summary>
    /// <param name="resourceType">type of the resource to be patched</param>
    /// <param name="condition">conditions on which the a resource is supposed to be patched</param>
    /// <param name="body">parameters resource that describes the patch operation</param>
    /// <param name="versionId">optional version id of the resource</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    public TransactionBuilder ConditionalPatch(string resourceType, SearchParams condition, Parameters body, string? versionId = null, string? bundleEntryFullUrl = null)
    {
        var entry = newEntry(Bundle.HTTPVerb.PATCH, InteractionType.ConditionalPatch, bundleEntryFullUrl);
        entry.Resource = body;
        entry.Request!.IfMatch = createIfMatchETag(versionId);
        var path = newRestUrl().AddPath(resourceType);
        path.AddParams(condition.ToUriParamList());
        addEntry(entry, path);

        return this;
    }

    #endregion

    #region Delete

    /// <summary>
    /// Add a "delete" entry to the transaction/batch
    /// </summary>
    /// <param name="resourceType">type of the resource to be deleted</param>
    /// <param name="id">id of the resource to be deleted</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    public TransactionBuilder Delete(string resourceType, string id, string? bundleEntryFullUrl = null)
    {
        var entry = newEntry(Bundle.HTTPVerb.DELETE, InteractionType.Delete, bundleEntryFullUrl);
        var path = newRestUrl().AddPath(resourceType, id);
        addEntry(entry, path);

        return this;
    }
        
    /// <summary>
    /// Add a "conditional delete" entry to the transaction/batch
    /// </summary>
    /// <param name="resourceType">type of the resource to be deleted</param>
    /// <param name="condition">conditions on which the resource should be deleted</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <param name="versionId">the optional version id to match against for the if-match header</param>
    /// <returns></returns>
    public TransactionBuilder ConditionalDeleteSingle(SearchParams condition, string? resourceType = null, string? versionId = null, string? bundleEntryFullUrl = null)
    {
        var entry = newEntry(Bundle.HTTPVerb.DELETE, InteractionType.ConditionalDeleteSingle, bundleEntryFullUrl);
        var path = newRestUrl().AddPath(resourceType ?? "");
        path.AddParams(condition.ToUriParamList());
        entry.Request!.IfMatch = createIfMatchETag(versionId);
            
        addEntry(entry, path);

        return this;
    }
        
    /// <summary>
    /// Add a "conditional delete" entry to the transaction/batch
    /// </summary>
    /// <param name="resourceType">type of the resource to be deleted</param>
    /// <param name="condition">conditions on which the resource should be deleted</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    public TransactionBuilder ConditionalDeleteMultiple(SearchParams condition, string? resourceType = null, string? bundleEntryFullUrl = null)
    {
        var entry = newEntry(Bundle.HTTPVerb.DELETE, InteractionType.ConditionalDeleteMultiple, bundleEntryFullUrl);
        var path = newRestUrl().AddPath(resourceType ?? "");
        path.AddParams(condition.ToUriParamList());
        addEntry(entry, path);

        return this;
    }
        
    #endregion

    #region DeleteHistory

    /// <summary>
    /// Add a "delete-history" entry to the transaction/batch
    /// </summary>
    /// <param name="resourceType">the type of the resource of which the history should be deleted</param>
    /// <param name="id">the id of the resource of which the history should be deleted</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    public TransactionBuilder DeleteHistory(string resourceType, string id, string? bundleEntryFullUrl = null)
    {
        var entry = newEntry(Bundle.HTTPVerb.DELETE, InteractionType.DeleteHistory, bundleEntryFullUrl);
        var path = newRestUrl().AddPath(resourceType, id, HISTORY);
        addEntry(entry, path);

        return this;
    }

    /// <summary>
    /// Add a "delete-history-version" entry to the transaction/batch
    /// </summary>
    /// <param name="resourceType">the type of the resource of which the history should be deleted</param>
    /// <param name="id">the id of the resource of which the history should be deleted</param>
    /// <param name="vid">the specific historical version which should be deleted</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    public TransactionBuilder DeleteHistoryVersion(string resourceType, string id, string vid, string? bundleEntryFullUrl = null)
    {
        var entry = newEntry(Bundle.HTTPVerb.DELETE, InteractionType.DeleteHistoryVersion, bundleEntryFullUrl);
        var path = newRestUrl().AddPath(resourceType, id, HISTORY, vid);
        addEntry(entry, path);

        return this;
    }

    #endregion
        
    #region Create

    /// <summary>
    /// Add a "create" entry to the transaction/batch
    /// </summary>
    /// <param name="body">the resource that is to be created</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    public TransactionBuilder Create(Resource body, string? bundleEntryFullUrl = null)
    {
        var entry = newEntry(Bundle.HTTPVerb.POST, InteractionType.Create, bundleEntryFullUrl);
        entry.Resource = body;
        var path = newRestUrl().AddPath(body.TypeName);
        addEntry(entry, path);

        return this;
    }

    /// <summary>
    /// Add a "conditional create" entry to the transaction/batch
    /// </summary>
    /// <param name="body">the resource that is to be created</param>
    /// <param name="condition">conditions on which the resource is supposed to be created</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    public TransactionBuilder ConditionalCreate(Resource body, SearchParams condition, string? bundleEntryFullUrl = null)
    {
        var entry = newEntry(Bundle.HTTPVerb.POST, InteractionType.ConditionalCreate, bundleEntryFullUrl);
        entry.Resource = body;
        var path = newRestUrl().AddPath(body.TypeName);

        entry.Request!.IfNoneExist = condition.ToUriParamList().ToQueryString();
        addEntry(entry, path);

        return this;
    }
        
    #endregion

    #region Search

    /// <summary>
    /// Add a "search" entry to the transaction/batch
    /// </summary>
    /// <param name="q">search parameters that describe the query to use</param>
    /// <param name="resourceType">resource type to be searched on</param>       
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    public TransactionBuilder Search(SearchParams? q = null, string? resourceType = null, string? bundleEntryFullUrl = null)
    {
        var entry = newEntry(Bundle.HTTPVerb.GET, InteractionType.Search, bundleEntryFullUrl);
        var path = newRestUrl();
        if (resourceType != null) path.AddPath(resourceType);
        if (q != null) path.AddParams(q.ToUriParamList());
        addEntry(entry, path);

        return this;
    }

    /// <summary>
    /// Add a "search" entry to the transaction/batch that uses POST instead of GET to search.
    /// </summary>
    /// <param name="q">search parameters that describe the query to use</param>
    /// <param name="resourceType">resource type to be searched on</param>       
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    public TransactionBuilder SearchUsingPost(SearchParams q, string? resourceType = null, string? bundleEntryFullUrl = null)
    {
        if (q == null) throw new ArgumentNullException(nameof(q));

        var entry = newEntry(Bundle.HTTPVerb.POST, InteractionType.Search, bundleEntryFullUrl);
        var path = newRestUrl();
        if (resourceType != null) path.AddPath(resourceType);
        path.AddPath("_search");
        entry.Resource = q.ToParameters();
        addEntry(entry, path);

        return this;
    }

    #endregion
        
    #region CapabilityStatement

    /// <summary>
    /// Add an entry to the transaction/batch that reads the CapabilityStatement of the server 
    /// </summary>
    /// <param name="summary">optional parameter that describes what kind of summary of the capability statement is to be returned</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    public TransactionBuilder CapabilityStatement(SummaryType? summary, string? bundleEntryFullUrl = null)
    {
        var entry = newEntry(Bundle.HTTPVerb.GET, InteractionType.Capabilities, bundleEntryFullUrl);
        var path = newRestUrl().AddPath(METADATA);
        if (summary.HasValue)
            path.AddParam(SearchParams.SEARCH_PARAM_SUMMARY, summary.Value.ToString().ToLower());
        addEntry(entry, path);

        return this;
    }

    #endregion
        
    #region History
        
    /// <summary>
    /// Add an entry to request the history of a single resource to the transaction/batch
    /// </summary>
    /// <param name="resourceType">type of the resource</param>
    /// <param name="id">id of the resource</param>
    /// <param name="summaryOnly">whether to return just a summary of all historical entries</param>
    /// <param name="pageSize">page size of the response bundle</param>
    /// <param name="since">date/time of the earliest historical entry</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    public TransactionBuilder ResourceHistory(string resourceType, string id, SummaryType? summaryOnly = null, int? pageSize = null, DateTimeOffset? since = null, string? bundleEntryFullUrl = null)
    {
        var path = newRestUrl().AddPath(resourceType, id, HISTORY);
        addHistoryEntry(path, summaryOnly, pageSize, since, bundleEntryFullUrl);

        return this;
    }
        
    /// <summary>
    /// Add an entry to request the history of all resources of a certain type to the transaction/batch
    /// </summary>
    /// <param name="resourceType">type of the resource</param>
    /// <param name="summaryOnly">whether to return just a summary of all historical entries</param>
    /// <param name="pageSize">page size of the response bundle</param>
    /// <param name="since">date/time of the earliest historical entry</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    public TransactionBuilder CollectionHistory(string resourceType, SummaryType? summaryOnly = null, int? pageSize = null, DateTimeOffset? since = null, string? bundleEntryFullUrl = null)
    {
        var path = newRestUrl().AddPath(resourceType, HISTORY);
        addHistoryEntry(path, summaryOnly, pageSize, since, bundleEntryFullUrl);

        return this;
    }


    /// <summary>
    /// Add an entry to request the history of all resources of the server to the transaction/batch
    /// </summary>
    /// <param name="summaryOnly">whether to return just a summary of all historical entries</param>
    /// <param name="pageSize">page size of the response bundle</param>
    /// <param name="since">date/time of the earliest historical entry</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    public TransactionBuilder ServerHistory(SummaryType? summaryOnly = null, int? pageSize = null, DateTimeOffset? since = null, string? bundleEntryFullUrl = null)
    {
        var path = newRestUrl().AddPath(HISTORY);
        addHistoryEntry(path, summaryOnly, pageSize, since, bundleEntryFullUrl);

        return this;
    }
        
    #endregion

    #region CustomOperation

    /// <summary>
    /// Add an entry to perform a FHIR operation on a certain endpoint of the server to the transaction/batch
    /// </summary>
    /// <param name="endpoint">The endpoint to perform the FHIR operation on</param>
    /// <param name="parameters">Parameters resource that describes the parameters of the operation</param>
    /// <param name="useGet">Whether to use a GET instead of POST to perform the operation</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    public TransactionBuilder EndpointOperation(RestUrl endpoint, Parameters? parameters, bool useGet = false, string? bundleEntryFullUrl = null)
    {
        var entry = newEntry(useGet ? Bundle.HTTPVerb.GET : Bundle.HTTPVerb.POST, InteractionType.Operation, bundleEntryFullUrl);
        var path = new RestUrl(endpoint);

        if (useGet)
        {
            if (parameters != null)
            {
                foreach (var parameter in parameters.Parameter)
                {
                    path.AddParam(parameter.Name ?? throw new ArgumentException("Parameters should have a name.", nameof(parameters)),
                        paramValueToString(parameter));
                }
            }
        }
        else
        {
            entry.Resource = parameters;
        }
        addEntry(entry, path);
        return this;
    }

    /// <summary>
    /// Add an entry to perform a FHIR operation on a certain endpoint of the server to the transaction/batch
    /// </summary>
    /// <param name="endpoint">The endpoint to perform the FHIR operation on</param>
    /// <param name="name">name of the operation to be performed</param>
    /// <param name="parameters">Parameters resource that describes the parameters of the operation</param>
    /// <param name="useGet">Whether to use a GET instead of POST to perform the operation</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    public TransactionBuilder EndpointOperation(RestUrl endpoint, string name, Parameters? parameters, bool useGet = false, string? bundleEntryFullUrl = null)
    {
        var path = new RestUrl(endpoint).AddPath(OPERATIONPREFIX + name);

        return EndpointOperation(path, parameters, useGet, bundleEntryFullUrl);
    }
        
    /// <summary>
    /// Add an entry to perform a FHIR operation on the root of the server to the transaction/batch
    /// </summary>
    /// <param name="name">name of the operation to be performed</param>
    /// <param name="parameters">Parameters resource that describes the parameters of the operation</param>
    /// <param name="useGet">Whether to use a GET instead of POST to perform the operation</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    public TransactionBuilder ServerOperation(string name, Parameters? parameters, bool useGet = false, string? bundleEntryFullUrl = null)
    {
        var path = newRestUrl().AddPath(OPERATIONPREFIX + name);
        return EndpointOperation(path, parameters, useGet, bundleEntryFullUrl);
    }

    /// <summary>
    /// Add an entry to perform a FHIR operation on a certain resource type to the transaction/batch
    /// </summary>
    /// <param name="resourceType">resource type on which the operation is to be performed</param>
    /// <param name="name">name of the operation to be performed</param>
    /// <param name="parameters">Parameters resource that describes the parameters of the operation</param>
    /// <param name="useGet">Whether to use a GET instead of POST to perform the operation</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    public TransactionBuilder TypeOperation(string resourceType, string name, Parameters? parameters, bool useGet = false, string? bundleEntryFullUrl = null)
    {
        var path = newRestUrl().AddPath(resourceType, OPERATIONPREFIX + name);
        return EndpointOperation(path, parameters, useGet, bundleEntryFullUrl);
    }

    /// <summary>
    /// Add an entry to perform a FHIR operation on a certain resource to the transaction/batch
    /// </summary>
    /// <param name="resourceType">resource type of the resource on which the operation is to be performed</param>
    /// <param name="id">id of the resource</param>
    /// <param name="vid">version id of the resource</param>
    /// <param name="name">name of the operation to be performed</param>
    /// <param name="parameters">Parameters resource that describes the parameters of the operation</param>
    /// <param name="useGet">Whether to use a GET instead of POST to perform the operation</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    public TransactionBuilder ResourceOperation(string resourceType, string id, string? vid, string name, Parameters? parameters, bool useGet = false, string? bundleEntryFullUrl = null)
    {
        var path = newRestUrl().AddPath(resourceType, id);
        if (vid != null) path.AddPath(HISTORY, vid);
        path.AddPath(OPERATIONPREFIX + name);

        return EndpointOperation(path, parameters, useGet, bundleEntryFullUrl);
    }
        
    public TransactionBuilder ProcessMessage(Bundle messageBundle, bool async = false, string? responseUrl = null, string? bundleEntryFullUrl = null)
    {
        var entry = newEntry(Bundle.HTTPVerb.POST, InteractionType.Operation, bundleEntryFullUrl);
        var path = newRestUrl().AddPath(OPERATIONPREFIX + "process-message");
        if (async) path.AddParam("async", "true");
        if (responseUrl != null) path.AddParam("response-url", responseUrl);
        entry.Resource = messageBundle;
        addEntry(entry, path);

        return this;
    }

    #endregion
        
    #region Transaction
        
    /// <summary>
    /// Add a sub-transaction to the transaction/batch
    /// </summary>
    /// <param name="transaction">Bundle that describes the sub-transaction</param>
    /// <param name="bundleEntryFullUrl">Optional parameter to set the <c>fullUrl</c> of the <c>Bundle</c> entry.</param>
    /// <returns></returns>
    public TransactionBuilder Transaction(Bundle transaction, string? bundleEntryFullUrl = null)
    {
        var entry = newEntry(Bundle.HTTPVerb.POST, InteractionType.Transaction, bundleEntryFullUrl);
        entry.Resource = transaction;
        var url = _baseUrl.ToString();
        if (url.EndsWith("/"))  // in case of a transaction the url cannot end with a forward slash. Remove it here.
            url = url.TrimEnd('/');
        addEntry(entry, new RestUrl(url));

        return this;
    }

    #endregion
        
    #region Utilities

    private static Bundle.EntryComponent newEntry(Bundle.HTTPVerb method, InteractionType interactionType, string? fullUrl = null)
    {
        var newEntry = new Bundle.EntryComponent
        {
            Request = new Bundle.RequestComponent() { Method = method },
            FullUrl = fullUrl
        };

        newEntry.AddAnnotation(interactionType);

        return newEntry;
    }
        
    private static string? createIfMatchETag(string? versionId)
    {
        if (versionId == null) return versionId;

        //To not break our previous public interface, we need to make sure we don't double
        //convert to an eTag
        return versionId.StartsWith("W/") ? versionId : $"W/\"{versionId}\"";
    }

    private void addEntry(Bundle.EntryComponent newEntry, RestUrl path)
    {
        if(newEntry.Request is null) throw Error.InvalidOperation("Request component of the entry is not set.");

        var url = HttpUtil.MakeRelativeFromBase(path.Uri, _baseUrl);
        newEntry.Request.Url = url!.OriginalString;
        _result.Entry.Add(newEntry);
    }

    private RestUrl newRestUrl()
    {
        return new RestUrl(_baseUrl);
    }
        
    private void addHistoryEntry(RestUrl path, SummaryType? summaryOnly = null, int? pageSize = null, DateTimeOffset? since = null, string? bundleEntryFullUrl = null)
    {
        var entry = newEntry(Bundle.HTTPVerb.GET, InteractionType.History, bundleEntryFullUrl);

        if (summaryOnly.HasValue) path.AddParam(SearchParams.SEARCH_PARAM_SUMMARY, summaryOnly.Value.ToString().ToLower());
        if (pageSize.HasValue) path.AddParam(HttpUtil.HISTORY_PARAM_COUNT, pageSize.Value.ToString());
        if (since.HasValue) path.AddParam(HttpUtil.HISTORY_PARAM_SINCE, PrimitiveTypeConverter.ConvertTo<string>(since.Value));

        addEntry(entry, path);
    }
        
    private static string paramValueToString(Parameters.ParameterComponent parameter) => parameter.Value switch
    {
        Identifier id => id.ToToken(),
        Coding coding => coding.ToToken(),
        ContactPoint contactPoint => contactPoint.ToToken(),
        CodeableConcept codeableConcept => codeableConcept.ToToken(),
        not null when ModelInspector.Base.IsPrimitive(parameter.Value.GetType()) => parameter.Value.ToString()!,
        _ => throw Error.InvalidOperation($"Parameter '{parameter.Name}' has a non-primitive type, which is not allowed.")
    };

    #endregion
}
#nullable restore