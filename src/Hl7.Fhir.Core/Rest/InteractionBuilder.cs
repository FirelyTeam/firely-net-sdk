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
    public interface IBuilder
    {
        Bundle.BundleEntryComponent Build();
    }

    public interface IReadBuilder : IBuilder
    {        
        IBuilder IfNoneMatch(string eTag);
        IBuilder IfModifiedSince(DateTimeOffset dto);
    }

    public interface IUpdateBuilder : IBuilder
    {
        IBuilder IfMatch(string eTag);
    }

    public interface IHistoryBuilder : IBuilder
    {
        IHistoryBuilder SummaryOnly();
        IHistoryBuilder PageSize(int size);
        IHistoryBuilder Since(DateTimeOffset since);
    }


    public class InteractionBuilder : IReadBuilder, IUpdateBuilder, IHistoryBuilder
    {
        public const string HISTORY = "_history";
        public const string METADATA = "metadata";
        public const string OPERATIONPREFIX = "$";

        private Bundle.BundleEntryComponent _result;
        private RestUrl _path;

        public InteractionBuilder(string baseUrl)
        {
            _result = new Bundle.BundleEntryComponent();
            _result.Transaction = new Bundle.BundleEntryTransactionComponent();
            
            _path = new RestUrl(baseUrl);
        }

        public InteractionBuilder(Uri baseUri) : this(baseUri.OriginalString)
        {
        }


        Bundle.BundleEntryComponent IBuilder.Build()
        {
            _result.Transaction.Url = _path.Uri.ToString();
            return _result;
        }


        public static Bundle.BundleEntryComponent Get(string url)
        {
            var result = new Bundle.BundleEntryComponent();
            result.Transaction = new Bundle.BundleEntryTransactionComponent();
            result.Transaction.Method = Bundle.HTTPVerb.GET;
            result.Transaction.Url = url;

            return result;
        }

        public static Bundle.BundleEntryComponent Get(Uri uri)
        {
            return Get(uri.OriginalString);
        }

        public IReadBuilder Read(string resourceType, string id)
        {
            _result.Transaction.Method = Bundle.HTTPVerb.GET;
            _path = _path.AddPath(resourceType, id); 
            return this;
        }

        IBuilder IReadBuilder.IfNoneMatch(string eTag)
        {
            _result.Transaction.IfNoneMatch = eTag;
            return this;
        }

        IBuilder IReadBuilder.IfModifiedSince(DateTimeOffset since)
        {
            _result.Transaction.IfModifiedSince = since;
            return this;
        }

        public IBuilder VRead(string resourceType, string id, string vid)
        {
            _result.Transaction.Method = Bundle.HTTPVerb.GET;
            _path = _path.AddPath(resourceType, id, HISTORY, vid);
            return this;
        }


        public IUpdateBuilder Update(string resourceType, string id, Resource body)
        {
            _result.Transaction.Method = Bundle.HTTPVerb.PUT;
            _result.Resource = body;
            _path = _path.AddPath(resourceType, id);
            return this;
        }

        IBuilder IUpdateBuilder.IfMatch(string eTag)
        {
            _result.Transaction.IfMatch = eTag;
            return this;
        }

        public IBuilder Delete(string resourceType, string id)
        {
            _result.Transaction.Method = Bundle.HTTPVerb.DELETE;
            _path = _path.AddPath(resourceType, id);
            return this;
        }

        public IBuilder Create(string resourceType, Resource body)
        {
            _result.Transaction.Method = Bundle.HTTPVerb.POST;
            _result.Resource = body;
            _path = _path.AddPath(resourceType);
            return this;
        }

        public IBuilder Conformance()
        {
            _result.Transaction.Method = Bundle.HTTPVerb.GET;
            _path = _path.AddPath(METADATA);
            return this;
        }


        public IHistoryBuilder ResourceHistory(string resourceType, string id)
        {
            _result.Transaction.Method = Bundle.HTTPVerb.GET;
            _path = _path.AddPath(resourceType, id, HISTORY);
            return this;
        }


        public IHistoryBuilder CollectionHistory(string resourceType)
        {
            _result.Transaction.Method = Bundle.HTTPVerb.GET;
            _path = _path.AddPath(resourceType, HISTORY);
            return this;
        }


        public IHistoryBuilder ServerHistory()
        {
            _result.Transaction.Method = Bundle.HTTPVerb.GET;
            _path = _path.AddPath(HISTORY);
            return this;
        }

        IHistoryBuilder IHistoryBuilder.SummaryOnly()
        {
            _path = _path.AddParam(SearchParams.SEARCH_PARAM_SUMMARY, true.ConvertTo<string>());
            return this;
        }

        IHistoryBuilder IHistoryBuilder.PageSize(int size)
        {
            _path = _path.AddParam(HttpUtil.HISTORY_PARAM_COUNT, size.ToString());
            return this;
        }

        IHistoryBuilder IHistoryBuilder.Since(DateTimeOffset since)
        {
            _path = _path.AddParam(HttpUtil.HISTORY_PARAM_SINCE, since.ConvertTo<string>());
            return this;
        }

        public IBuilder ServerOperation(string name, Parameters parameters)
        {
            _result.Transaction.Method = Bundle.HTTPVerb.POST;
            _result.Resource = parameters;
            _path = _path.AddPath(OPERATIONPREFIX + name);
            return this;
        }

        public IBuilder TypeOperation(string resourceType, string name, Parameters parameters)
        {
            _result.Transaction.Method = Bundle.HTTPVerb.POST;
            _result.Resource = parameters;
            _path = _path.AddPath(resourceType,OPERATIONPREFIX + name);
            return this;
        }

        public IBuilder ResourceOperation(string resourceType, string id, string vid, string name, Parameters parameters)
        {
            _result.Transaction.Method = Bundle.HTTPVerb.POST;
            _result.Resource = parameters;
            _path = _path.AddPath(resourceType, id);
            if(vid != null) _path = _path.AddPath(resourceType, vid);
            _path = _path.AddPath(OPERATIONPREFIX + name);
            return this;
        }


        public IBuilder Search(SearchParams q, string resourceType = null)
        {
            _result.Transaction.Method = Bundle.HTTPVerb.GET;
            if (resourceType != null) _path = _path.AddPath(resourceType);

            foreach (var par in q.ToUriParamList())
            {
                _path.AddParam(par.Item1, par.Item2);
            }

            return this;
        }


        public IBuilder Search(string resourceType = null)
        {
            _result.Transaction.Method = Bundle.HTTPVerb.GET;
            if (resourceType != null) _path = _path.AddPath(resourceType);

            return this;
        }

      

    }
}
