using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Rest
{
    internal static class EndpointExtensions
    {
        public static RestUrl WithMetadata(this Endpoint endpoint)
        {
            return endpoint.AsRestUrl().SetPath(RestOperation.METADATA);
        }

        public static RestUrl ForCollection(this Endpoint endpoint, string collection)
        {
            return endpoint.AsRestUrl().SetPath(collection);
        }
        public static RestUrl Collection(this Endpoint endpoint, ResourceType resource)
        {
            return endpoint.ForCollection(resource.GetCollectionName());
        }

        public static RestUrl Validate(this Endpoint endpoint, string collection, string id=null)
        {
            if(id != null)
                return endpoint.AsRestUrl().SetPath(collection, RestOperation.VALIDATE);
            else
                return endpoint.AsRestUrl().SetPath(collection, RestOperation.VALIDATE, id);
        }

        public static RestUrl Resource(this Endpoint endpoint, string collection, string id)
        {
            return endpoint.AsRestUrl().SetPath(collection, id);
        }
        public static RestUrl Resource(this Endpoint endpoint, ResourceType resource, string id)
        {
             return endpoint.Resource(resource.GetCollectionName(), id);
        }

        public static RestUrl CollectionHistory(this Endpoint endpoint, string collection)
        {
            return endpoint.AsRestUrl().SetPath(collection, RestOperation.HISTORY);
        }
        public static RestUrl CollectionHistory(this Endpoint endpoint, ResourceType resource)
        {
            return endpoint.CollectionHistory(resource.GetCollectionName());
        }

        public static RestUrl ResourceHistory(this Endpoint endpoint, string collection, string id)
        {
            return endpoint.AsRestUrl().SetPath(collection, id, RestOperation.HISTORY);
        }
        public static RestUrl ResourceHistory(this Endpoint endpoint, ResourceType resource, string id)
        {
            return endpoint.AsRestUrl().SetPath(resource.GetCollectionName(), id, RestOperation.HISTORY);
        }

        public static RestUrl ServerHistory(this Endpoint endpoint)
        {
            return endpoint.AsRestUrl().SetPath(RestOperation.HISTORY);
        }

        public static RestUrl Search(this Endpoint endpoint, string collection)
        {
            return endpoint.AsRestUrl().SetPath(collection, RestOperation.SEARCH);
        }
        public static RestUrl Search(this Endpoint endpoint, ResourceType resource)
        {
            return endpoint.AsRestUrl().SetPath(resource.GetCollectionName(), RestOperation.SEARCH);
        }

        public static RestUrl Tags(this Endpoint endpoint)
        {
            return endpoint.AsRestUrl().SetPath(RestOperation.TAGS);
        }

        public static RestUrl CollectionTags(this Endpoint endpoint, string collection)
        {
            return endpoint.AsRestUrl().SetPath(collection, RestOperation.TAGS);
        }
        public static RestUrl CollectionTags(this Endpoint endpoint, ResourceType resource)
        {
            return endpoint.AsRestUrl().SetPath(resource.GetCollectionName(), RestOperation.TAGS);
        }


        public static RestUrl ResourceTags(this Endpoint endpoint, string collection, string id, string vid=null)
        {
            if(vid == null)
                return endpoint.AsRestUrl().SetPath(collection, id, RestOperation.TAGS);
            else
                return endpoint.AsRestUrl().SetPath(collection, id, RestOperation.HISTORY, vid, RestOperation.TAGS);
        }

        public static RestUrl ResourceTags(this Endpoint endpoint, ResourceType resource, string id, string vid=null)
        {
            if(vid == null)
                return endpoint.AsRestUrl().SetPath(resource.GetCollectionName(), id, RestOperation.TAGS);
            else
                return endpoint.AsRestUrl().SetPath(resource.GetCollectionName(), id, RestOperation.HISTORY, vid, RestOperation.TAGS);
        }       
    }
}
