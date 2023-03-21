/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Rest
{
    /// <summary>
    /// Builder to describe a FHIR transaction Bundle
    /// </summary>
    public class TransactionBuilder
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

        private static Bundle.EntryComponent newEntry(Bundle.HTTPVerb method, InteractionType interactionType)
        {
            var newEntry = new Bundle.EntryComponent
            {
                Request = new Bundle.RequestComponent() { Method = method }
            };

            newEntry.AddAnnotation(interactionType);

            return newEntry;
        }

        private void addEntry(Bundle.EntryComponent newEntry, RestUrl path)
        {
            var url = HttpUtil.MakeRelativeFromBase(path.Uri, _baseUrl);
            newEntry.Request.Url = url.OriginalString;
            _result.Entry.Add(newEntry);
        }

        private RestUrl newRestUrl()
        {
            return new RestUrl(_baseUrl);
        }

        public Bundle ToBundle()
        {
            return _result;
        }

        /// <summary>
        /// Add a "GET" entry to the transaction/batch
        /// </summary>
        /// <param name="url">relative or absolute url the transaction is supposed to "get"</param>
        /// <returns></returns>
        public TransactionBuilder Get(string url)
        {
            var entry = newEntry(Bundle.HTTPVerb.GET, InteractionType.Unspecified);
            var uri = new Uri(url, UriKind.RelativeOrAbsolute);

            if (uri.IsAbsoluteUri)
                addEntry(entry, new RestUrl(url));
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
        /// <param name="uri">relative or absolute uri of the resource the transaction is supposed to return</param>
        /// <returns></returns>
        public TransactionBuilder Get(Uri uri)
        {
            return Get(uri.OriginalString);
        }

        /// <summary>
        /// Add a "read" entry to the transaction/batch that returns a specific resource
        /// </summary>
        /// <param name="resourceType">type of the resource</param>
        /// <param name="id">id of the resource</param>
        /// <param name="versionId">optional version id of the resource</param>
        /// <param name="ifModifiedSince">optional date that specifies the resource is only to be returned if it has been modified after that date</param>
        /// <returns></returns>
        public TransactionBuilder Read(string resourceType, string id, string versionId = null, DateTimeOffset? ifModifiedSince = null)
        {
            var entry = newEntry(Bundle.HTTPVerb.GET, InteractionType.Read);
            entry.Request.IfNoneMatch = createIfMatchETag(versionId);
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
        /// <returns></returns>
        public TransactionBuilder VRead(string resourceType, string id, string vid)
        {
            var entry = newEntry(Bundle.HTTPVerb.GET, InteractionType.VRead);
            var path = newRestUrl().AddPath(resourceType, id, HISTORY, vid);
            addEntry(entry, path);

            return this;
        }

        /// <summary>
        /// Add an "update" entry to the transaction/batch
        /// </summary>
        /// <param name="id">id of the resource</param>
        /// <param name="body">The newer version of the resource to be updated</param>
        /// <param name="versionId">optional version id of the resource</param>
        /// <returns></returns>
        public TransactionBuilder Update(string id, Resource body, string versionId = null)
        {
            var entry = newEntry(Bundle.HTTPVerb.PUT, InteractionType.Update);
            entry.Resource = body;
            entry.Request.IfMatch = createIfMatchETag(versionId);
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
        /// <returns></returns>
        public TransactionBuilder Update(SearchParams condition, Resource body, string versionId = null)
        {
            var entry = newEntry(Bundle.HTTPVerb.PUT, InteractionType.Update);
            entry.Resource = body;
            entry.Request.IfMatch = createIfMatchETag(versionId);
            var path = newRestUrl().AddPath(body.TypeName);
            path.AddParams(condition.ToUriParamList());
            addEntry(entry, path);

            return this;
        }

        /// <summary>
        /// Add a "patch" entry to the transaction/batch
        /// </summary>
        /// <param name="resourceType">type of the resource to be patched</param>
        /// <param name="id">id of the resource to be patched</param>
        /// <param name="body">parameters resource that describes the patch operation</param>
        /// <param name="versionId">optional version id of the resource</param>
        /// <returns></returns>
        public TransactionBuilder Patch(string resourceType, string id, Parameters body, string versionId = null)
        {
            var entry = newEntry(Bundle.HTTPVerb.PATCH, InteractionType.Patch);
            entry.Resource = body;
            entry.Request.IfMatch = createIfMatchETag(versionId);
            var path = newRestUrl().AddPath(resourceType, id);
            addEntry(entry, path);

            return this;
        }

        /// <summary>
        /// Add a "patch" entry to the transaction/batch
        /// </summary>
        /// <param name="resourceType">type of the resource to be patched</param>
        /// <param name="condition">conditions on which the a resource is supposed to be patched</param>
        /// <param name="body">parameters resource that describes the patch operation</param>
        /// <param name="versionId">optional version id of the resource</param>
        /// <returns></returns>
        public TransactionBuilder Patch(string resourceType, SearchParams condition, Parameters body, string versionId = null)
        {
            var entry = newEntry(Bundle.HTTPVerb.PATCH, InteractionType.Patch);
            entry.Resource = body;
            entry.Request.IfMatch = createIfMatchETag(versionId);
            var path = newRestUrl().AddPath(resourceType);
            path.AddParams(condition.ToUriParamList());
            addEntry(entry, path);

            return this;
        }

        private static string createIfMatchETag(string versionId)
        {
            if (versionId == null) return versionId;

            //To not break our previous public interface, we need to make sure we don't double
            //convert to an eTag
            return versionId.StartsWith("W/") ? versionId : $"W/\"{versionId}\"";
        }

        /// <summary>
        /// Add a "delete" entry to the transaction/batch
        /// </summary>
        /// <param name="resourceType">type of the resource to be deleted</param>
        /// <param name="id">id of the resource to be deleted</param>
        /// <returns></returns>
        public TransactionBuilder Delete(string resourceType, string id)
        {
            var entry = newEntry(Bundle.HTTPVerb.DELETE, InteractionType.Delete);
            var path = newRestUrl().AddPath(resourceType, id);
            addEntry(entry, path);

            return this;
        }

        /// <summary>
        /// Add a "conditional delete" entry to the transaction/batch
        /// </summary>
        /// <param name="resourceType">type of the resource to be deleted</param>
        /// <param name="condition">conditions on which the resource should be deleted</param>
        /// <returns></returns>
        public TransactionBuilder Delete(string resourceType, SearchParams condition)
        {
            var entry = newEntry(Bundle.HTTPVerb.DELETE, InteractionType.Delete);
            var path = newRestUrl().AddPath(resourceType);
            path.AddParams(condition.ToUriParamList());
            addEntry(entry, path);

            return this;
        }

        /// <summary>
        /// Add a "create" entry to the transaction/batch
        /// </summary>
        /// <param name="body">the resource that is to be created</param>
        /// <returns></returns>
        public TransactionBuilder Create(Resource body)
        {
            var entry = newEntry(Bundle.HTTPVerb.POST, InteractionType.Create);
            entry.Resource = body;
            var path = newRestUrl().AddPath(body.TypeName);
            addEntry(entry, path);

            return this;
        }


        /// <summary>
        /// Add a "create" entry to the transaction/batch
        /// </summary>
        /// <param name="body">the resource that is to be created</param>
        /// <param name="condition">conditions on which the resource is supposed to be created</param>
        /// <returns></returns>
        public TransactionBuilder Create(Resource body, SearchParams condition)
        {
            var entry = newEntry(Bundle.HTTPVerb.POST, InteractionType.Create);
            entry.Resource = body;
            var path = newRestUrl().AddPath(body.TypeName);

            entry.Request.IfNoneExist = condition.ToUriParamList().ToQueryString();
            addEntry(entry, path);

            return this;
        }

        /// <summary>
        /// Add an entry to the transaction/batch that reads the CapabilityStatement of the server 
        /// </summary>
        /// <param name="summary">optional parameter that describes what kind of summary of the capability statement is to be returned</param>
        /// <returns></returns>
        public TransactionBuilder CapabilityStatement(SummaryType? summary)
        {
            var entry = newEntry(Bundle.HTTPVerb.GET, InteractionType.Capabilities);
            var path = newRestUrl().AddPath(METADATA);
            if (summary.HasValue)
                path.AddParam(SearchParams.SEARCH_PARAM_SUMMARY, summary.Value.ToString().ToLower());
            addEntry(entry, path);

            return this;
        }


        private void addHistoryEntry(RestUrl path, SummaryType? summaryOnly = null, int? pageSize = null, DateTimeOffset? since = null)
        {
            var entry = newEntry(Bundle.HTTPVerb.GET, InteractionType.History);

            if (summaryOnly.HasValue) path.AddParam(SearchParams.SEARCH_PARAM_SUMMARY, summaryOnly.Value.ToString().ToLower());
            if (pageSize.HasValue) path.AddParam(HttpUtil.HISTORY_PARAM_COUNT, pageSize.Value.ToString());
            if (since.HasValue) path.AddParam(HttpUtil.HISTORY_PARAM_SINCE, PrimitiveTypeConverter.ConvertTo<string>(since.Value));

            addEntry(entry, path);
        }

        /// <summary>
        /// Add an entry to request the history of a single resource to the transaction/batch
        /// </summary>
        /// <param name="resourceType">type of the resource</param>
        /// <param name="id">id of the resource</param>
        /// <param name="summaryOnly">whether to return just a summary of all historical entries</param>
        /// <param name="pageSize">page size of the response bundle</param>
        /// <param name="since">date/time of the earliest historical entry</param>
        /// <returns></returns>
        public TransactionBuilder ResourceHistory(string resourceType, string id, SummaryType? summaryOnly = null, int? pageSize = null, DateTimeOffset? since = null)
        {
            var path = newRestUrl().AddPath(resourceType, id, HISTORY);
            addHistoryEntry(path, summaryOnly, pageSize, since);

            return this;
        }

        /// <summary>
        /// Add an entry to request the history of all resources of a certain type to the transaction/batch
        /// </summary>
        /// <param name="resourceType">type of the resource</param>
        /// <param name="summaryOnly">whether to return just a summary of all historical entries</param>
        /// <param name="pageSize">page size of the response bundle</param>
        /// <param name="since">date/time of the earliest historical entry</param>
        /// <returns></returns>
        public TransactionBuilder CollectionHistory(string resourceType, SummaryType? summaryOnly = null, int? pageSize = null, DateTimeOffset? since = null)
        {
            var path = newRestUrl().AddPath(resourceType, HISTORY);
            addHistoryEntry(path, summaryOnly, pageSize, since);

            return this;
        }


        /// <summary>
        /// Add an entry to request the history of all resources of the server to the transaction/batch
        /// </summary>
        /// <param name="summaryOnly">whether to return just a summary of all historical entries</param>
        /// <param name="pageSize">page size of the response bundle</param>
        /// <param name="since">date/time of the earliest historical entry</param>
        /// <returns></returns>
        public TransactionBuilder ServerHistory(SummaryType? summaryOnly = null, int? pageSize = null, DateTimeOffset? since = null)
        {
            var path = newRestUrl().AddPath(HISTORY);
            addHistoryEntry(path, summaryOnly, pageSize, since);

            return this;
        }

        private static string paramValueToString(Parameters.ParameterComponent parameter)
        {
            if (parameter.Value != null)
            {
                switch (parameter.Value)
                {
                    case Identifier id:
                        return id.ToToken();
                    case Coding coding:
                        return coding.ToToken();
                    case ContactPoint contactPoint:
                        return contactPoint.ToToken();
                    case CodeableConcept codeableConcept:
                        return codeableConcept.ToToken();
                    default:
                        if (ModelInspector.Base.IsPrimitive(parameter.Value.GetType()))
                        {
                            return parameter.Value.ToString();
                        }
                        break;
                }
            }
            throw Error.InvalidOperation($"Parameter '{parameter.Name}' has a non-primitive type, which is not allowed.");
        }


        /// <summary>
        /// Add an entry to perform a FHIR operation on a certain endpoint of the server to the transaction/batch
        /// </summary>
        /// <param name="endpoint">The endpoint to perform the FHIR operation on</param>
        /// <param name="parameters">Parameters resource that describes the parameters of the operation</param>
        /// <param name="useGet">Whether to use a GET instead of POST to perform the operation</param>
        /// <returns></returns>
        public TransactionBuilder EndpointOperation(RestUrl endpoint, Parameters parameters, bool useGet = false)
        {
            var entry = newEntry(useGet ? Bundle.HTTPVerb.GET : Bundle.HTTPVerb.POST, InteractionType.Operation);
            var path = new RestUrl(endpoint);

            if (useGet)
            {
                if (parameters != null)
                {
                    foreach (var parameter in parameters.Parameter)
                    {
                        path.AddParam(parameter.Name, paramValueToString(parameter));
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
        /// <returns></returns>
        public TransactionBuilder EndpointOperation(RestUrl endpoint, string name, Parameters parameters, bool useGet = false)
        {
            var path = new RestUrl(endpoint).AddPath(OPERATIONPREFIX + name);

            return EndpointOperation(path, parameters, useGet);
        }

        /// <summary>
        /// Add an entry to perform a FHIR operation on the root of the server to the transaction/batch
        /// </summary>
        /// <param name="name">name of the operation to be performed</param>
        /// <param name="parameters">Parameters resource that describes the parameters of the operation</param>
        /// <param name="useGet">Whether to use a GET instead of POST to perform the operation</param>
        /// <returns></returns>
        public TransactionBuilder ServerOperation(string name, Parameters parameters, bool useGet = false)
        {
            var path = newRestUrl().AddPath(OPERATIONPREFIX + name);
            return EndpointOperation(path, parameters, useGet);
        }

        /// <summary>
        /// Add an entry to perform a FHIR operation on a certain resource type to the transaction/batch
        /// </summary>
        /// <param name="resourceType">resource type on which the operation is to be performed</param>
        /// <param name="name">name of the operation to be performed</param>
        /// <param name="parameters">Parameters resource that describes the parameters of the operation</param>
        /// <param name="useGet">Whether to use a GET instead of POST to perform the operation</param>
        /// <returns></returns>
        public TransactionBuilder TypeOperation(string resourceType, string name, Parameters parameters, bool useGet = false)
        {
            var path = newRestUrl().AddPath(resourceType, OPERATIONPREFIX + name);
            return EndpointOperation(path, parameters, useGet);
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
        /// <returns></returns>
        public TransactionBuilder ResourceOperation(string resourceType, string id, string vid, string name, Parameters parameters, bool useGet = false)
        {
            var path = newRestUrl().AddPath(resourceType, id);
            if (vid != null) path.AddPath(HISTORY, vid);
            path.AddPath(OPERATIONPREFIX + name);

            return EndpointOperation(path, parameters, useGet);
        }

        /// <summary>
        /// Add a "search" entry to the transaction/batch
        /// </summary>
        /// <param name="q">search parameters that describe the query to use</param>
        /// <param name="resourceType">resource type to be searched on</param>       
        /// <returns></returns>
        public TransactionBuilder Search(SearchParams q = null, string resourceType = null)
        {
            var entry = newEntry(Bundle.HTTPVerb.GET, InteractionType.Search);
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
        /// <returns></returns>
        public TransactionBuilder SearchUsingPost(SearchParams q, string resourceType = null)
        {
            if (q == null) throw new ArgumentNullException(nameof(q));

            var entry = newEntry(Bundle.HTTPVerb.POST, InteractionType.Search);
            var path = newRestUrl();
            if (resourceType != null) path.AddPath(resourceType);
            path.AddPath("_search");
            entry.Resource = q.ToParameters();
            addEntry(entry, path);

            return this;
        }

        /// <summary>
        /// Add a sub-transaction to the transaction/batch
        /// </summary>
        /// <param name="transaction">Bundle that describes the sub-transaction</param>
        /// <returns></returns>
        public TransactionBuilder Transaction(Bundle transaction)
        {
            var entry = newEntry(Bundle.HTTPVerb.POST, InteractionType.Transaction);
            entry.Resource = transaction;
            var url = _baseUrl.ToString();
            if (url.EndsWith("/"))  // in case of a transaction the url cannot end with a forward slash. Remove it here.
                url = url.TrimEnd('/');
            addEntry(entry, new RestUrl(url));

            return this;
        }
    }
}
