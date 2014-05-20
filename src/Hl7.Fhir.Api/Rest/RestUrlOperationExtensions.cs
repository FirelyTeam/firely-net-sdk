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

namespace Hl7.Fhir.Rest
{
    internal static class RestUrlOperationExtensions
    {
        public static RestUrl WithMetadata(this RestUrl url)
        {
            return new RestUrl(url).AddPath(RestOperation.METADATA);
        }

        public static RestUrl ForCollection(this RestUrl url, string collection)
        {
            return new RestUrl(url).AddPath(collection);
        }

        public static RestUrl Validate(this RestUrl url, string collection, string id = null)
        {
            if(id != null)
                return new RestUrl(url).AddPath(collection, RestOperation.VALIDATE);
            else
                return new RestUrl(url).AddPath(collection, RestOperation.VALIDATE, id);
        }

        public static RestUrl Resource(this RestUrl url, string collection, string id)
        {
            return new RestUrl(url).AddPath(collection, id);
        }

        public static RestUrl CollectionHistory(this RestUrl url, string collection)
        {
            return new RestUrl(url).AddPath(collection, RestOperation.HISTORY);
        }

        public static RestUrl ResourceHistory(this RestUrl url, string collection, string id)
        {
            return new RestUrl(url).AddPath(collection, id, RestOperation.HISTORY);
        }

        public static RestUrl ServerHistory(this RestUrl url)
        {
            return new RestUrl(url).AddPath(RestOperation.HISTORY);
        }

        public static RestUrl Search(this RestUrl url, string collection=null)
        {
            if (collection != null)
                return new RestUrl(url).AddPath(collection, RestOperation.SEARCH);
            else
                return new RestUrl(url).AddPath(RestOperation.SEARCH);
        }

        public static RestUrl Search(this RestUrl url, Query q)
        {
            // The ResourceType is the only parameter that needs special handling,
            // since the others are all "normal" parameters. Just make sure we don't
            // include the special _type parameter on the REST url
            var result = url.Search(q.ResourceType);

            foreach (var par in q.Parameter)
            {
                var paramKey = Query.ExtractParamKey(par);
                if (paramKey != Query.SEARCH_PARAM_TYPE)
                {
                    result.AddParam(Query.ExtractParamKey(par),
                                Query.ExtractParamValue(par));
                }
            }

            return result;
        }

        public static RestUrl ToMailbox(this RestUrl url)
        {
            return new RestUrl(url).AddPath(RestOperation.MAILBOX);
        }

        public static RestUrl ToDocument(this RestUrl url)
        {
            return new RestUrl(url).AddPath(RestOperation.DOCUMENT);
        }


        public static RestUrl ServerTags(this RestUrl url)
        {
            return new RestUrl(url).AddPath(RestOperation.TAGS);
        }

        public static RestUrl CollectionTags(this RestUrl url, string collection)
        {
            return new RestUrl(url).AddPath(collection, RestOperation.TAGS);
        }

        public static RestUrl ResourceTags(this RestUrl url, string collection, string id, string vid=null)
        {
            if(vid == null)
                return new RestUrl(url).AddPath(collection, id, RestOperation.TAGS);
            else
                return new RestUrl(url).AddPath(collection, id, RestOperation.HISTORY, vid, RestOperation.TAGS);
        }

        public static RestUrl DeleteResourceTags(this RestUrl url, string collection, string id, string vid = null)
        {
            if (vid == null)
                return new RestUrl(url).AddPath(collection, id, RestOperation.TAGS, RestOperation.DELETE);
            else
                return new RestUrl(url).AddPath(collection, id, RestOperation.HISTORY, vid, RestOperation.TAGS, RestOperation.DELETE);
        }

    }
}
