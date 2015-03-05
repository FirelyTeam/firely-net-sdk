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
    public class TransactionBuilder : IReadEntryBuilder, IUpdateEntryBuilder, IHistoryEntryBuilder, IInteractionBuilder
    {
        public const string HISTORY = "_history";
        public const string METADATA = "metadata";
        public const string OPERATIONPREFIX = "$";

        private Bundle _result;

        private Bundle.BundleEntryComponent _newEntry;
        private RestUrl _path;

        public TransactionBuilder(string baseUrl)
        {
            _result = new Bundle();
            _result.Base = baseUrl;

            addEntry();            
        }

        public TransactionBuilder(Uri baseUri)
            : this(baseUri.OriginalString)
        {
        }

        private void addEntry()
        {
            if (_newEntry != null)
            {
                _newEntry.Transaction.Url = _path.Uri.ToString();
                _result.Entry.Add(_newEntry);
            }

            _newEntry = new Bundle.BundleEntryComponent();
            _newEntry.Transaction = new Bundle.BundleEntryTransactionComponent();
            _path = new RestUrl(_result.Base);
        }

        Bundle IEntryBuilder.Build()
        {
            addEntry();
            return _result;
        }

        IInteractionBuilder IEntryBuilder.Add()
        {
            addEntry();
            return this;
        }

        public IEntryBuilder Get(string url)
        {
            _newEntry.Transaction.Method = Bundle.HTTPVerb.GET;

            var uri = new Uri(url,UriKind.RelativeOrAbsolute);

            if (uri.IsAbsoluteUri)
                _path = new RestUrl(url);
            else
                _path = new RestUrl(_path.ToString() + url);

            return this;
        }

        public IEntryBuilder Get(Uri uri)
        {
            return Get(uri.OriginalString);
        }

        public IReadEntryBuilder Read(string resourceType, string id)
        {
            _newEntry.Transaction.Method = Bundle.HTTPVerb.GET;
            _path = _path.AddPath(resourceType, id); 
            return this;
        }

        IEntryBuilder IReadEntryBuilder.IfNoneMatch(string eTag)
        {
            _newEntry.Transaction.IfNoneMatch = eTag;
            return this;
        }

        IEntryBuilder IReadEntryBuilder.IfModifiedSince(DateTimeOffset since)
        {
            _newEntry.Transaction.IfModifiedSince = since;
            return this;
        }

        public IEntryBuilder VRead(string resourceType, string id, string vid)
        {
            _newEntry.Transaction.Method = Bundle.HTTPVerb.GET;
            _path = _path.AddPath(resourceType, id, HISTORY, vid);
            return this;
        }


        public IUpdateEntryBuilder Update(string id, Resource body)
        {
            _newEntry.Transaction.Method = Bundle.HTTPVerb.PUT;
            _newEntry.Resource = body;
            _path = _path.AddPath(body.TypeName, id);
            return this;
        }

        public IUpdateEntryBuilder Update(SearchParams condition, Resource body)
        {
            _newEntry.Transaction.Method = Bundle.HTTPVerb.PUT;
            _newEntry.Resource = body;
            _path = _path.AddPath(body.TypeName);
            _path.AddParams(condition.ToUriParamList());

            return this;
        }


        IEntryBuilder IUpdateEntryBuilder.IfMatch(string eTag)
        {
            _newEntry.Transaction.IfMatch = eTag;
            return this;
        }

        public IEntryBuilder Delete(string resourceType, string id)
        {
            _newEntry.Transaction.Method = Bundle.HTTPVerb.DELETE;
            _path = _path.AddPath(resourceType, id);
            return this;
        }

        public IEntryBuilder Delete(string resourceType, SearchParams condition)
        {
            _newEntry.Transaction.Method = Bundle.HTTPVerb.DELETE;
            _path = _path.AddPath(resourceType);
            _path.AddParams(condition.ToUriParamList());

            return this;
        }

        public IEntryBuilder Create(Resource body)
        {
            _newEntry.Transaction.Method = Bundle.HTTPVerb.POST;
            _newEntry.Resource = body;
            _path = _path.AddPath(body.TypeName);
            return this;
        }

        public IEntryBuilder Create(Resource body, SearchParams condition)
        {
            _newEntry.Transaction.Method = Bundle.HTTPVerb.POST;
            _newEntry.Resource = body;
            _path = _path.AddPath(body.TypeName);

            var nonExist = new RestUrl(_path);
            nonExist.AddParams(condition.ToUriParamList());
            _newEntry.Transaction.IfNoneExist = nonExist.ToString();

            return this;
        }

        
        public IEntryBuilder Conformance()
        {
            _newEntry.Transaction.Method = Bundle.HTTPVerb.GET;
            _path = _path.AddPath(METADATA);
            return this;
        }


        public IHistoryEntryBuilder ResourceHistory(string resourceType, string id)
        {
            _newEntry.Transaction.Method = Bundle.HTTPVerb.GET;
            _path = _path.AddPath(resourceType, id, HISTORY);
            return this;
        }


        public IHistoryEntryBuilder CollectionHistory(string resourceType)
        {
            _newEntry.Transaction.Method = Bundle.HTTPVerb.GET;
            _path = _path.AddPath(resourceType, HISTORY);
            return this;
        }


        public IHistoryEntryBuilder ServerHistory()
        {
            _newEntry.Transaction.Method = Bundle.HTTPVerb.GET;
            _path = _path.AddPath(HISTORY);
            return this;
        }

        IHistoryEntryBuilder IHistoryEntryBuilder.SummaryOnly()
        {
            _path = _path.AddParam(SearchParams.SEARCH_PARAM_SUMMARY, true.ConvertTo<string>());
            return this;
        }

        IHistoryEntryBuilder IHistoryEntryBuilder.PageSize(int size)
        {
            _path = _path.AddParam(HttpUtil.HISTORY_PARAM_COUNT, size.ToString());
            return this;
        }

        IHistoryEntryBuilder IHistoryEntryBuilder.Since(DateTimeOffset since)
        {
            _path = _path.AddParam(HttpUtil.HISTORY_PARAM_SINCE, since.ConvertTo<string>());
            return this;
        }

        public IEntryBuilder ServerOperation(string name, Parameters parameters)
        {
            _newEntry.Transaction.Method = Bundle.HTTPVerb.POST;
            _newEntry.Resource = parameters;
            _path = _path.AddPath(OPERATIONPREFIX + name);
            return this;
        }

        public IEntryBuilder TypeOperation(string resourceType, string name, Parameters parameters)
        {
            _newEntry.Transaction.Method = Bundle.HTTPVerb.POST;
            _newEntry.Resource = parameters;
            _path = _path.AddPath(resourceType,OPERATIONPREFIX + name);
            return this;
        }

        public IEntryBuilder ResourceOperation(string resourceType, string id, string vid, string name, Parameters parameters)
        {
            _newEntry.Transaction.Method = Bundle.HTTPVerb.POST;
            _newEntry.Resource = parameters;
            _path = _path.AddPath(resourceType, id);
            if(vid != null) _path = _path.AddPath(resourceType, vid);
            _path = _path.AddPath(OPERATIONPREFIX + name);
            return this;
        }


        public IEntryBuilder Search(SearchParams q, string resourceType = null)
        {
            _newEntry.Transaction.Method = Bundle.HTTPVerb.GET;
            if (resourceType != null) _path = _path.AddPath(resourceType);

            _path.AddParams(q.ToUriParamList());

            return this;
        }


        public IEntryBuilder Search(string resourceType = null)
        {
            _newEntry.Transaction.Method = Bundle.HTTPVerb.GET;
            if (resourceType != null) _path = _path.AddPath(resourceType);

            return this;
        }


        public IEntryBuilder Transaction(Bundle transaction)
        {
            _newEntry.Transaction.Method = Bundle.HTTPVerb.POST;
            _newEntry.Resource = transaction;

            return this;
        }
    }
}
