/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Rest
{
    public class RequestsBuilder
    {
        public const string HISTORY = "_history";
        public const string METADATA = "metadata";
        public const string OPERATIONPREFIX = "$";

        private List<Request> _result;
        private readonly Uri _baseUrl;

        public RequestsBuilder(string baseUrl)
        {
            _result = new List<Request>();
            _baseUrl = new Uri(baseUrl);
        }

        public RequestsBuilder(Uri baseUri)
            : this(baseUri.OriginalString)
        {
        }

        private Request newRequest(HTTPVerb method, InteractionType interactionType)
        {
            var newRequest = new Request();
            newRequest.Method = method;
            newRequest.Interaction = interactionType;
            return newRequest;
        }

        private void addRequest(Request newRequest, RestUrl path)
        {
            newRequest.Url = HttpUtil.MakeRelativeFromBase(path.Uri, _baseUrl).ToString();
            _result.Add(newRequest);
        }

        private RestUrl newRestUrl()
        {
            return new RestUrl(_baseUrl);
        }

        public Request ToRequest()
        {
            return _result.FirstOrDefault();
        }

        public List<Request> ToRequests()
        {
            return _result;
        }

        public RequestsBuilder Get(string url)
        {
            var request = newRequest(HTTPVerb.GET, InteractionType.Unspecified);
            var uri = new Uri(url,UriKind.RelativeOrAbsolute);

            if (uri.IsAbsoluteUri)
                addRequest(request, new RestUrl(url));
            else
            {
                var absoluteUrl = HttpUtil.MakeAbsoluteToBase(uri, _baseUrl);
                addRequest(request, new RestUrl(absoluteUrl));
            }
            return this;
        }

        public RequestsBuilder Get(Uri uri)
        {
            return Get(uri.OriginalString);
        }

        public RequestsBuilder Read(string resourceType, string id, string versionId = null, DateTimeOffset? ifModifiedSince = null)
        {
            var request = newRequest(HTTPVerb.GET, InteractionType.Read);
            request.IfNoneMatch = createIfMatchETag(versionId);
            request.IfModifiedSince = ifModifiedSince;
            var path = newRestUrl().AddPath(resourceType, id);
            addRequest(request, path);

            return this;
        }

        public RequestsBuilder VRead(string resourceType, string id, string vid)
        {
            var request = newRequest(HTTPVerb.GET, InteractionType.VRead);
            var path = newRestUrl().AddPath(resourceType, id, HISTORY, vid);
            addRequest(request, path);

            return this;
        }


        public RequestsBuilder Update(string id, Resource body, string versionId=null)
        {
            var request = newRequest(HTTPVerb.PUT, InteractionType.Update);
            request.Resource = body;
            request.IfMatch = createIfMatchETag(versionId);
            var path = newRestUrl().AddPath(body.TypeName, id);
            addRequest(request, path);

            return this;
        }

        public RequestsBuilder Update(SearchParams condition, Resource body, string versionId=null)
        {
            var request = newRequest(HTTPVerb.PUT, InteractionType.Update);
            request.Resource = body;
            request.IfMatch = createIfMatchETag(versionId);
            var path = newRestUrl().AddPath(body.TypeName);
            path.AddParams(condition.ToUriParamList());
            addRequest(request, path);

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

        public RequestsBuilder Delete(string resourceType, string id)
        {
            var request = newRequest(HTTPVerb.DELETE, InteractionType.Delete);
            var path = newRestUrl().AddPath(resourceType, id);
            addRequest(request, path);

            return this;
        }

        public RequestsBuilder Delete(string resourceType, SearchParams condition)
        {
            var request = newRequest(HTTPVerb.DELETE, InteractionType.Delete);
            var path = newRestUrl().AddPath(resourceType);
            path.AddParams(condition.ToUriParamList());
            addRequest(request, path);

            return this;
        }

        public RequestsBuilder Create(Resource body)
        {
            var request = newRequest(HTTPVerb.POST, InteractionType.Create);
            request.Resource = body;
            var path = newRestUrl().AddPath(body.TypeName);
            addRequest(request, path);

            return this;
        }

        public RequestsBuilder Create(Resource body, SearchParams condition)
        {
            var request = newRequest(HTTPVerb.POST, InteractionType.Create);
            request.Resource = body;
            var path = newRestUrl().AddPath(body.TypeName);

            request.IfNoneExist = condition.ToUriParamList().ToQueryString();
            addRequest(request, path);

            return this;
        }

        public RequestsBuilder GetMetadata(SummaryType? summary)
        {
            var request =  newRequest(HTTPVerb.GET, InteractionType.Capabilities);
            var path = newRestUrl().AddPath(METADATA);
            if (summary.HasValue)
                path.AddParam(SearchParams.SEARCH_PARAM_SUMMARY, summary.Value.ToString().ToLower());
            addRequest(request, path);

            return this;
        }

        private void addHistoryEntry(RestUrl path, SummaryType? summaryOnly = null, int? pageSize=null, DateTimeOffset? since = null)
        {
            var request = newRequest(HTTPVerb.GET, InteractionType.History);

            if(summaryOnly.HasValue) path.AddParam(SearchParams.SEARCH_PARAM_SUMMARY, summaryOnly.Value.ToString().ToLower());
            if(pageSize.HasValue) path.AddParam(HttpUtil.HISTORY_PARAM_COUNT, pageSize.Value.ToString());
            if(since.HasValue) path.AddParam(HttpUtil.HISTORY_PARAM_SINCE, PrimitiveTypeConverter.ConvertTo<string>(since.Value));

            addRequest(request, path);
        }

        public RequestsBuilder ResourceHistory(string resourceType, string id, SummaryType? summaryOnly = null, int? pageSize=null, DateTimeOffset? since = null)
        {
            var path = newRestUrl().AddPath(resourceType, id, HISTORY);
            addHistoryEntry(path, summaryOnly, pageSize, since);
            
            return this;
        }

        public RequestsBuilder CollectionHistory(string resourceType, SummaryType? summaryOnly = null, int? pageSize=null, DateTimeOffset? since = null)
        {            
            var path = newRestUrl().AddPath(resourceType, HISTORY);
            addHistoryEntry(path,summaryOnly, pageSize, since);
            
            return this;
        }

        public RequestsBuilder ServerHistory(SummaryType? summaryOnly = null, int? pageSize=null, DateTimeOffset? since = null)
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
                    case Model.DSTU2.Identifier id:
                        return id.ToToken();
                    case Model.STU3.Identifier id:
                        return id.ToToken();
                    case Model.R4.Identifier id:
                        return id.ToToken();
                    case Coding coding:
                        return coding.ToToken();
                    case Model.DSTU2.ContactPoint contactPoint:
                        return contactPoint.ToToken();
                    case Model.STU3.ContactPoint contactPoint:
                        return contactPoint.ToToken();
                    case Model.R4.ContactPoint contactPoint:
                        return contactPoint.ToToken();
                    case CodeableConcept codeableConcept:
                        return codeableConcept.ToToken();
                    default:
                        if (Model.DSTU2.ModelInfo.IsPrimitive(parameter.Value.GetType()) 
                            || Model.STU3.ModelInfo.IsPrimitive(parameter.Value.GetType()) 
                            || Model.R4.ModelInfo.IsPrimitive(parameter.Value.GetType()))
                        {
                            return parameter.Value.ToString();
                        }
                        break;
                }
            }
            throw Error.InvalidOperation($"Parameter '{parameter.Name}' has a non-primitive type, which is not allowed.");
        }

        public RequestsBuilder EndpointOperation(RestUrl endpoint, Parameters parameters, bool useGet = false)
        {
            var request = newRequest(useGet ? HTTPVerb.GET : HTTPVerb.POST, InteractionType.Operation);
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
                request.Resource = parameters;
            }

            addRequest(request, path);

            return this;
        }

        public RequestsBuilder EndpointOperation(RestUrl endpoint, string name, Parameters parameters, bool useGet = false)
        {          
            var path = new RestUrl(endpoint).AddPath(OPERATIONPREFIX + name);

            return EndpointOperation(path, parameters, useGet);
        }

        public RequestsBuilder ServerOperation(string name, Parameters parameters, bool useGet = false)
        {
            var path = newRestUrl().AddPath(OPERATIONPREFIX + name);
            return EndpointOperation(path, parameters, useGet);
        }

        public RequestsBuilder TypeOperation(string resourceType, string name, Parameters parameters, bool useGet = false)
        {
            var path = newRestUrl().AddPath(resourceType, OPERATIONPREFIX + name);
            return EndpointOperation(path,parameters, useGet);
        }

        public RequestsBuilder ResourceOperation(string resourceType, string id, string vid, string name, Parameters parameters, bool useGet = false)
        {
            var path = newRestUrl().AddPath(resourceType, id);
            if(vid != null) path.AddPath(HISTORY, vid);
            path.AddPath(OPERATIONPREFIX + name);

            return EndpointOperation(path, parameters, useGet);
        }

        public RequestsBuilder Search(SearchParams q = null, string resourceType = null)
        {
            var request = newRequest(HTTPVerb.GET, InteractionType.Search);
            var path = newRestUrl();
            if (resourceType != null) path.AddPath(resourceType);
            if(q != null) path.AddParams(q.ToUriParamList());
            addRequest(request, path);

            return this;
        }

        public RequestsBuilder SearchUsingPost(SearchParams q, string resourceType = null)
        {
            if (q == null) throw new ArgumentNullException(nameof(q));

            var request = newRequest(HTTPVerb.POST, InteractionType.Search);
            var path = newRestUrl();
            if (resourceType != null) path.AddPath(resourceType);
            path.AddPath("_search");
            request.Resource = q.ToParameters();
            addRequest(request, path);

            return this;
        }

        public RequestsBuilder Transaction<TBundle>(TBundle transaction) where TBundle : Resource
        {
            var request = newRequest(HTTPVerb.POST, InteractionType.Transaction);
            request.Resource = transaction;
            var url = _baseUrl.ToString();
            if (url.EndsWith("/"))  // in case of a transaction the url cannot end with a forward slash. Remove it here.
                url = url.TrimEnd('/');
            addRequest(request, new RestUrl(url));

            return this;
        }
    }
}
