/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Support;
using Hl7.Fhir.Serialization;

namespace Hl7.Fhir.Rest
{
    public class TransactionBuilder
    {
        public const string HISTORY = "_history";
        public const string METADATA = "metadata";
        public const string OPERATIONPREFIX = "$";

        private Bundle _result;
        private string _baseUrl;

        public TransactionBuilder(string baseUrl)
        {
            _result = new Bundle();
            _baseUrl = baseUrl;           
        }

        public TransactionBuilder(Uri baseUri)
            : this(baseUri.OriginalString)
        {
        }


        private Bundle.EntryComponent newEntry(Bundle.HTTPVerb method)
        {
            var newEntry = new Bundle.EntryComponent();
            newEntry.Request = new Bundle.RequestComponent();
            newEntry.Request.Method = method;

            return newEntry;
        }

        private void addEntry(Bundle.EntryComponent newEntry, RestUrl path)
        {
            newEntry.Request.Url = path.Uri.ToString();
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
            var entry = newEntry(Bundle.HTTPVerb.GET);
            var uri = new Uri(url,UriKind.RelativeOrAbsolute);

            if (uri.IsAbsoluteUri)
                addEntry(entry, new RestUrl(url));
            else
            {
                var absoluteUrl = _baseUrl;
                if(!absoluteUrl.EndsWith("/")) absoluteUrl += "/";
                absoluteUrl += url;
                addEntry(entry, new RestUrl(absoluteUrl));
            }

            return this;
        }

        public TransactionBuilder Get(Uri uri)
        {
            return Get(uri.OriginalString);
        }

        public TransactionBuilder Read(string resourceType, string id, string ifNoneMatch = null, DateTimeOffset? ifModifiedSince = null)
        {
            var entry = newEntry(Bundle.HTTPVerb.GET);
            entry.Request.IfNoneMatch = ifNoneMatch;
            entry.Request.IfModifiedSince = ifModifiedSince;
            var path = newRestUrl().AddPath(resourceType, id);
            addEntry(entry, path);

            return this;
        }

        public TransactionBuilder VRead(string resourceType, string id, string vid)
        {
            var entry = newEntry(Bundle.HTTPVerb.GET);
            var path = newRestUrl().AddPath(resourceType, id, HISTORY, vid);
            addEntry(entry, path);

            return this;
        }


        public TransactionBuilder Update(string id, Resource body, string ifMatch=null)
        {
            var entry = newEntry(Bundle.HTTPVerb.PUT);
            entry.Resource = body;
            entry.Request.IfMatch = ifMatch;
            var path = newRestUrl().AddPath(body.TypeName, id);
            addEntry(entry, path);

            return this;
        }

        public TransactionBuilder Update(SearchParams condition, Resource body, string ifMatch=null)
        {
            var entry = newEntry(Bundle.HTTPVerb.PUT);
            entry.Resource = body;
            var path = newRestUrl().AddPath(body.TypeName);
            path.AddParams(condition.ToUriParamList());
            addEntry(entry, path);

            return this;
        }


        public TransactionBuilder Delete(string resourceType, string id)
        {
            var entry = newEntry(Bundle.HTTPVerb.DELETE);
            var path = newRestUrl().AddPath(resourceType, id);
            addEntry(entry, path);

            return this;
        }

        public TransactionBuilder Delete(string resourceType, SearchParams condition)
        {
            var entry = newEntry(Bundle.HTTPVerb.DELETE);
            var path = newRestUrl().AddPath(resourceType);
            path.AddParams(condition.ToUriParamList());
            addEntry(entry, path);

            return this;
        }

        public TransactionBuilder Create(Resource body)
        {
            var entry = newEntry(Bundle.HTTPVerb.POST);
            entry.Resource = body;
            var path = newRestUrl().AddPath(body.TypeName);
            addEntry(entry, path);

            return this;
        }

        public TransactionBuilder Create(Resource body, SearchParams condition)
        {
            var entry = newEntry(Bundle.HTTPVerb.POST);
            entry.Resource = body;
            var path = newRestUrl().AddPath(body.TypeName);

            var nonExist = new RestUrl(path);
            nonExist.AddParams(condition.ToUriParamList());
            entry.Request.IfNoneExist = nonExist.ToString();
            addEntry(entry, path);

            return this;
        }

        
        public TransactionBuilder Conformance()
        {
            var entry =  newEntry(Bundle.HTTPVerb.GET);
            var path = newRestUrl().AddPath(METADATA);
            addEntry(entry, path);

            return this;
        }


        private void addHistoryEntry(RestUrl path, bool? summaryOnly = null, int? pageSize=null, DateTimeOffset? since = null)
        {
            var entry = newEntry(Bundle.HTTPVerb.GET);

            if(summaryOnly.HasValue) path.AddParam(SearchParams.SEARCH_PARAM_SUMMARY, PrimitiveTypeConverter.ConvertTo<string>(summaryOnly.Value));
            if(pageSize.HasValue) path.AddParam(HttpUtil.HISTORY_PARAM_COUNT, pageSize.Value.ToString());
            if(since.HasValue) path.AddParam(HttpUtil.HISTORY_PARAM_SINCE, PrimitiveTypeConverter.ConvertTo<string>(since.Value));

            addEntry(entry, path);
        }

        public TransactionBuilder ResourceHistory(string resourceType, string id, bool? summaryOnly = null, int? pageSize=null, DateTimeOffset? since = null)
        {
            var path = newRestUrl().AddPath(resourceType, id, HISTORY);
            addHistoryEntry(path, summaryOnly, pageSize, since);
            
            return this;
        }


        public TransactionBuilder CollectionHistory(string resourceType, bool? summaryOnly = null, int? pageSize=null, DateTimeOffset? since = null)
        {            
            var path = newRestUrl().AddPath(resourceType, HISTORY);
            addHistoryEntry(path,summaryOnly, pageSize, since);
            
            return this;
        }


        public TransactionBuilder ServerHistory(bool? summaryOnly = null, int? pageSize=null, DateTimeOffset? since = null)
        {
            var path = newRestUrl().AddPath(HISTORY);
            addHistoryEntry(path, summaryOnly, pageSize, since);

            return this;
        }

        public TransactionBuilder EndpointOperation(RestUrl endpoint, Parameters parameters, bool useGet = false)
        {
            var entry = newEntry(useGet ? Bundle.HTTPVerb.GET : Bundle.HTTPVerb.POST);
            entry.Resource = parameters;
            var path = new RestUrl(endpoint);
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
            var entry = newEntry(Bundle.HTTPVerb.GET);
            var path = newRestUrl();
            if (resourceType != null) path.AddPath(resourceType);
            if(q != null) path.AddParams(q.ToUriParamList());
            addEntry(entry, path);

            return this;
        }


        public TransactionBuilder Transaction(Bundle transaction)
        {
            var entry = newEntry(Bundle.HTTPVerb.POST);
            entry.Resource = transaction;
            addEntry(entry, newRestUrl());

            return this;
        }
    }
}
