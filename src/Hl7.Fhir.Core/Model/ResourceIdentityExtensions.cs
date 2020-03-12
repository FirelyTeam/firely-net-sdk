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
            if (r.Id == null) return null;

            var result = Hl7.Fhir.Rest.ResourceIdentity.Build(r.TypeName, r.Id, r.VersionId);

            if (!string.IsNullOrEmpty(baseUrl))
                return result.WithBase(baseUrl);

            if (r.ResourceBase != null)
                return result.WithBase(r.ResourceBase);
            else
                return result;
        }

        public static ResourceIdentity ResourceIdentity(this Resource r, Uri baseUrl)
        {
            return r.ResourceIdentity(baseUrl.OriginalString);
        }
    }
}
