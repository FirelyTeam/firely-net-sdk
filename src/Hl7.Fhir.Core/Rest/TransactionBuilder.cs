/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Rest
{
    public class TransactionBuilder
    {
        public const string HISTORY = "_history";
        public const string METADATA = "metadata";
        public const string OPERATIONPREFIX = "$";

        private Bundle _result;
        private readonly Uri _baseUrl;

        public TransactionBuilder(string baseUrl, Bundle.BundleType type = Bundle.BundleType.Batch)
        {
            _result = new Bundle()
            {
                Type = type
            };

            _baseUrl = new Uri(baseUrl);
        }

        public TransactionBuilder(Uri baseUri, Bundle.BundleType type = Bundle.BundleType.Batch)
            : this(baseUri.OriginalString, type)
        {
        }


        internal enum InteractionType
        {
            Search,
            Unspecified,
            Read,
            VRead,
            Update,
            Delete,
            Create,
            Capabilities,
            History,
            Operation,
            Transaction
        }

        private Bundle.EntryComponent newEntry(Bundle.HTTPVerb method, InteractionType interactionType)
        {
            var newEntry = new Bundle.EntryComponent();
            newEntry.Request = new Bundle.RequestComponent();
            newEntry.Request.Method = method;
            newEntry.AddAnnotation(interactionType);

            return newEntry;
        }

        private void addEntry(Bundle.EntryComponent newEntry, RestUrl path)
        {
            newEntry.Request.Url = HttpUtil.MakeRelativeFromBase(path.Uri, _baseUrl).ToString();
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

        public TransactionBuilder Get(string url)
        {
            var entry = newEntry(Bundle.HTTPVerb.GET, InteractionType.Unspecified);
            var uri = new Uri(url,UriKind.RelativeOrAbsolute);

            if (uri.IsAbsoluteUri)
                addEntry(entry, new RestUrl(url));
            else
            {
                var absoluteUrl = HttpUtil.MakeAbsoluteToBase(uri, _baseUrl);
                addEntry(entry, new RestUrl(absoluteUrl));
            }
            return this;
        }

        public TransactionBuilder Get(Uri uri)
        {
            return Get(uri.OriginalString);
        }

        public TransactionBuilder Read(string resourceType, string id, string versionId = null, DateTimeOffset? ifModifiedSince = null)
        {
            var entry = newEntry(Bundle.HTTPVerb.GET, InteractionType.Read);
            entry.Request.IfNoneMatch = createIfMatchETag(versionId);
            entry.Request.IfModifiedSince = ifModifiedSince;
            var path = newRestUrl().AddPath(resourceType, id);
            addEntry(entry, path);

            return this;
        }

        public TransactionBuilder VRead(string resourceType, string id, string vid)
        {
            var entry = newEntry(Bundle.HTTPVerb.GET, InteractionType.VRead);
            var path = newRestUrl().AddPath(resourceType, id, HISTORY, vid);
            addEntry(entry, path);

            return this;
        }


        public TransactionBuilder Update(string id, Resource body, string versionId=null)
        {
            var entry = newEntry(Bundle.HTTPVerb.PUT, InteractionType.Update);
            entry.Resource = body;
            entry.Request.IfMatch = createIfMatchETag(versionId);
            var path = newRestUrl().AddPath(body.TypeName, id);
            addEntry(entry, path);

            return this;
        }

        public TransactionBuilder Update(SearchParams condition, Resource body, string versionId=null)
        {
            var entry = newEntry(Bundle.HTTPVerb.PUT, InteractionType.Update);
            entry.Resource = body;
            entry.Request.IfMatch = createIfMatchETag(versionId);
            var path = newRestUrl().AddPath(body.TypeName);
            path.AddParams(condition.ToUriParamList());
            addEntry(entry, path);

            return this;
        }

        private string createIfMatchETag(string versionId)
        {
            if (versionId == null) return versionId;

            //To not break our previous public interface, we need to make sure we don't double
            //convert to an eTag
            if (versionId.StartsWith("W/")) return versionId;

            return $"W/\"{versionId}\"";
        }

        public TransactionBuilder Delete(string resourceType, string id)
        {
            var entry = newEntry(Bundle.HTTPVerb.DELETE, InteractionType.Delete);
            var path = newRestUrl().AddPath(resourceType, id);
            addEntry(entry, path);

            return this;
        }

        public TransactionBuilder Delete(string resourceType, SearchParams condition)
        {
            var entry = newEntry(Bundle.HTTPVerb.DELETE, InteractionType.Delete);
            var path = newRestUrl().AddPath(resourceType);
            path.AddParams(condition.ToUriParamList());
            addEntry(entry, path);

            return this;
        }

        public TransactionBuilder Create(Resource body)
        {
            var entry = newEntry(Bundle.HTTPVerb.POST, InteractionType.Create);
            entry.Resource = body;
            var path = newRestUrl().AddPath(body.TypeName);
            addEntry(entry, path);

            return this;
        }

        public TransactionBuilder Create(Resource body, SearchParams condition)
        {
            var entry = newEntry(Bundle.HTTPVerb.POST, InteractionType.Create);
            entry.Resource = body;
            var path = newRestUrl().AddPath(body.TypeName);

            entry.Request.IfNoneExist = condition.ToUriParamList().ToQueryString();
            addEntry(entry, path);

            return this;
        }

        
        public TransactionBuilder CapabilityStatement(SummaryType? summary)
        {
            var entry =  newEntry(Bundle.HTTPVerb.GET, InteractionType.Capabilities);
            var path = newRestUrl().AddPath(METADATA);
            if (summary.HasValue)
                path.AddParam(SearchParams.SEARCH_PARAM_SUMMARY, summary.Value.ToString().ToLower());
            addEntry(entry, path);

            return this;
        }


        private void addHistoryEntry(RestUrl path, SummaryType? summaryOnly = null, int? pageSize=null, DateTimeOffset? since = null)
        {
            var entry = newEntry(Bundle.HTTPVerb.GET, InteractionType.History);

            if(summaryOnly.HasValue) path.AddParam(SearchParams.SEARCH_PARAM_SUMMARY, summaryOnly.Value.ToString().ToLower());
            if(pageSize.HasValue) path.AddParam(HttpUtil.HISTORY_PARAM_COUNT, pageSize.Value.ToString());
            if(since.HasValue) path.AddParam(HttpUtil.HISTORY_PARAM_SINCE, PrimitiveTypeConverter.ConvertTo<string>(since.Value));

            addEntry(entry, path);
        }

        public TransactionBuilder ResourceHistory(string resourceType, string id, SummaryType? summaryOnly = null, int? pageSize=null, DateTimeOffset? since = null)
        {
            var path = newRestUrl().AddPath(resourceType, id, HISTORY);
            addHistoryEntry(path, summaryOnly, pageSize, since);
            
            return this;
        }


        public TransactionBuilder CollectionHistory(string resourceType, SummaryType? summaryOnly = null, int? pageSize=null, DateTimeOffset? since = null)
        {            
            var path = newRestUrl().AddPath(resourceType, HISTORY);
            addHistoryEntry(path,summaryOnly, pageSize, since);
            
            return this;
        }


        public TransactionBuilder ServerHistory(SummaryType? summaryOnly = null, int? pageSize=null, DateTimeOffset? since = null)
        {
            var path = newRestUrl().AddPath(HISTORY);
            addHistoryEntry(path, summaryOnly, pageSize, since);

            return this;
        }

        private string paramValueToString(Parameters.ParameterComponent parameter)
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
                        if (ModelInfo.IsPrimitive(parameter.Value.GetType()))
                        {
                            return parameter.Value.ToString();
                        }
                        break;
                }
            }
            throw Error.InvalidOperation($"Parameter '{parameter.Name}' has a non-primitive type, which is not allowed.");
        }

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

        public TransactionBuilder EndpointOperation(RestUrl endpoint, string name, Parameters parameters, bool useGet = false)
        {          
            var path = new RestUrl(endpoint).AddPath(OPERATIONPREFIX + name);

            return EndpointOperation(path, parameters, useGet);
        }

        public TransactionBuilder ServerOperation(string name, Parameters parameters, bool useGet = false)
        {
            var path = newRestUrl().AddPath(OPERATIONPREFIX + name);
            return EndpointOperation(path, parameters, useGet);
        }

        public TransactionBuilder TypeOperation(string resourceType, string name, Parameters parameters, bool useGet = false)
        {
            var path = newRestUrl().AddPath(resourceType, OPERATIONPREFIX + name);
            return EndpointOperation(path,parameters, useGet);
        }

        public TransactionBuilder ResourceOperation(string resourceType, string id, string vid, string name, Parameters parameters, bool useGet = false)
        {
            var path = newRestUrl().AddPath(resourceType, id);
            if(vid != null) path.AddPath(HISTORY, vid);
            path.AddPath(OPERATIONPREFIX + name);

            return EndpointOperation(path, parameters, useGet);
        }


        public TransactionBuilder Search(SearchParams q = null, string resourceType = null)
        {
            var entry = newEntry(Bundle.HTTPVerb.GET, InteractionType.Search);
            var path = newRestUrl();
            if (resourceType != null) path.AddPath(resourceType);
            if(q != null) path.AddParams(q.ToUriParamList());
            addEntry(entry, path);

            return this;
        }

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
