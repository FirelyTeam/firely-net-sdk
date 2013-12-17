using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Rest
{
    public static class ResourceIdentityExtensions
    {
        public static ResourceIdentity Identity(this Endpoint endpoint, string collection, string id)
        {
            return ResourceIdentity.Build(endpoint.Uri, collection, id);
        }
        public static ResourceIdentity Identity(this Endpoint endpoint, string collection, string id, string vid)
        {
            return ResourceIdentity.Build(endpoint.Uri, collection, id, vid);
        }
    }
}
