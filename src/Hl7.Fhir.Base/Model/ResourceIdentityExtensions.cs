using Hl7.Fhir.Rest;
using System;

namespace Hl7.Fhir.Model
{
    public static class ResourceIdentityExtensions
    {
        /// <summary>
        /// Returns the entire URI of the location that this resource was retrieved from
        /// </summary>
        /// <remarks>
        /// It is not stored, but reconstructed from the components of the resource
        /// </remarks>
        /// <returns></returns>
        public static ResourceIdentity ResourceIdentity(this Resource r, string baseUrl = null)
        {
            if (r.Id is null) return null;

            var result = Rest.ResourceIdentity.Build(r.TypeName, r.Id, r.VersionId);

            if (!string.IsNullOrEmpty(baseUrl))
                return result.WithBase(baseUrl);

            return r.ResourceBase != null ? result.WithBase(r.ResourceBase) : result;
        }

        public static ResourceIdentity ResourceIdentity(this Resource r, Uri baseUrl) => r.ResourceIdentity(baseUrl.OriginalString);
    }
}
