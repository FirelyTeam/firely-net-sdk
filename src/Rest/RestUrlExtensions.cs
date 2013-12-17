using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Rest
{
    public static class RestUriExtensions
    {
        public static RestUrl Collection(this Endpoint endpoint,  string collection)
        {
            return endpoint.NewRestUrl().Path(collection);
        }
        public static RestUrl Collection(this Endpoint endpoint, ResourceType resource)
        {
            return endpoint.Collection(resource.GetCollectionName());
        }

        public static RestUrl Resource(this Endpoint endpoint, string collection, string id)
        {
            return endpoint.NewRestUrl().Path(collection, id);
        }
        public static RestUrl Resource(this Endpoint endpoint, ResourceType resource, string id)
        {
             return endpoint.Resource(resource.GetCollectionName(), id);
        }

        public static RestUrl CollectionHistory(this Endpoint endpoint, string collection)
        {
            return endpoint.NewRestUrl().Path(collection, RestOperation.HISTORY);
        }
        public static RestUrl CollectionHistory(this Endpoint endpoint, ResourceType resource)
        {
            return endpoint.CollectionHistory(resource.GetCollectionName());
        }

        public static RestUrl ResourceHistory(this Endpoint endpoint, string collection, string id)
        {
            return endpoint.NewRestUrl().Path(collection, id, RestOperation.HISTORY);
        }
        public static RestUrl ResourceHistory(this Endpoint endpoint, ResourceType resource, string id)
        {
            return endpoint.NewRestUrl().Path(resource.GetCollectionName(), id, RestOperation.HISTORY);
        }

        public static RestUrl Search(this Endpoint endpoint, string collection)
        {
            return endpoint.NewRestUrl().Path(collection, RestOperation.SEARCH);
        }
        public static RestUrl Search(this Endpoint endpoint, ResourceType resource)
        {
            return endpoint.NewRestUrl().Path(resource.GetCollectionName(), RestOperation.SEARCH);
        }

        public static RestUrl Tags(this Endpoint endpoint)
        {
            return endpoint.NewRestUrl().Path(RestOperation.TAGS);
        }

        public static RestUrl CollectionTags(this Endpoint endpoint, string collection)
        {
            return endpoint.NewRestUrl().Path(collection, RestOperation.TAGS);
        }
        public static RestUrl CollectionTags(this Endpoint endpoint, ResourceType resource)
        {
            return endpoint.NewRestUrl().Path(resource.GetCollectionName(), RestOperation.TAGS);
        }


        public static RestUrl ResourceTags(this Endpoint endpoint, string collection, string id)
        {
            return endpoint.NewRestUrl().Path(collection, id, RestOperation.TAGS);
        }
        public static RestUrl ResourceTags(this Endpoint endpoint, ResourceType resource, string id)
        {
            return endpoint.NewRestUrl().Path(resource.GetCollectionName(), id, RestOperation.TAGS);
        }
        
        public static RestUrl ResourceHistoryTags(this Endpoint endpoint, string collection, string id, string vid)
        {
            return endpoint.NewRestUrl().Path(collection, id, RestOperation.HISTORY, vid, RestOperation.TAGS);
        } 
        public static RestUrl ResourceHistoryTags(this Endpoint endpoint, ResourceType resource, string id, string vid)
        {
            return endpoint.NewRestUrl().Path(resource.GetCollectionName(), id, RestOperation.HISTORY, vid, RestOperation.TAGS);
        } 
    }
}
