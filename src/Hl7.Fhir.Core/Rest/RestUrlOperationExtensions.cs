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

        public static RestUrl ForResourceType(this RestUrl url, string collection)
        {
            return new RestUrl(url).AddPath(collection);
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

        public static RestUrl ServerOperation(this RestUrl url, string name)
        {
            return new RestUrl(url).AddPath(RestOperation.OPERATIONPREFIX + name);
        }

        public static RestUrl CollectionOperation(this RestUrl url, string collection, string name)
        {
            return new RestUrl(url).AddPath(collection, RestOperation.OPERATIONPREFIX + name);
        }

        public static RestUrl ResourceOperation(this RestUrl url, string collection, string id, string name)
        {
            return new RestUrl(url).AddPath(collection, id, RestOperation.OPERATIONPREFIX + name);
        }

        public static RestUrl Search(this RestUrl url, string resourceType=null)
        {
           if (resourceType != null)
              return new RestUrl(url).AddPath(resourceType, RestOperation.SEARCH);
            else
              return  new RestUrl(url).AddPath(RestOperation.SEARCH);
        }

        public static RestUrl Search(this RestUrl url, SearchParams q, string resourceType=null)
        {
            RestUrl result = Search(url,resourceType);

            foreach (var par in q.ToUriParamList())
            {
                result.AddParam(par.Item1, par.Item2);
            }

            return result;
        }

        public static RestUrl ServerTags(this RestUrl url)
        {
            return new RestUrl(url).AddPath(RestOperation.META);
        }

        public static RestUrl CollectionTags(this RestUrl url, string collection)
        {
            return new RestUrl(url).AddPath(collection, RestOperation.META);
        }

        public static RestUrl ResourceTags(this RestUrl url, string collection, string id, string vid=null)
        {
            if(vid == null)
                return new RestUrl(url).AddPath(collection, id, RestOperation.META);
            else
                return new RestUrl(url).AddPath(collection, id, RestOperation.HISTORY, vid, RestOperation.META);
        }

        public static RestUrl DeleteResourceTags(this RestUrl url, string collection, string id, string vid = null)
        {
            if (vid == null)
                return new RestUrl(url).AddPath(collection, id, RestOperation.META, RestOperation.DELETE);
            else
                return new RestUrl(url).AddPath(collection, id, RestOperation.HISTORY, vid, RestOperation.META, RestOperation.DELETE);
        }

    }
}
