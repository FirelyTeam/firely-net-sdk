using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Rest
{
    internal static class FhirRestOperationExtensions
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

        public static RestUrl Search(this RestUrl url, string collection)
        {
            return new RestUrl(url).AddPath(collection, RestOperation.SEARCH);
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
