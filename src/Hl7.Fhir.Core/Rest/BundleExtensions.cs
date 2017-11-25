/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Threading.Tasks;

namespace Hl7.Fhir.Rest
{
    public static class BundleExtensions
    {
        public static async Task<Model.DSTU2.Bundle> RefreshBundleAsync(this FhirDstu2Client client, Model.DSTU2.Bundle bundle)
        {
            if (bundle == null) throw Error.ArgumentNull(nameof(bundle));

            if (bundle.Type != BundleType.Searchset)
                throw Error.Argument("Refresh is only applicable to bundles of type 'searchset'");

            // Clone old bundle, without the entries (so, just the header)
            var result = (Model.DSTU2.Bundle) bundle.DeepCopy();

            result.Id = "urn:uuid:" + Guid.NewGuid().ToString("n");
            result.Meta = new Meta();
            result.Meta.LastUpdated = DateTimeOffset.Now;

            foreach (var entry in result.Entry)
            {
                if (entry.Resource != null)
                {
                    entry.Resource = await client.ReadAsync<Resource>(entry.FullUrl).ConfigureAwait(false);
                }
            }

            return result;
        }

        public static Model.DSTU2.Bundle RefreshBundle(this FhirDstu2Client client, Model.DSTU2.Bundle bundle)
        {
            return RefreshBundleAsync(client, bundle).WaitResult();
        }

        public static async Task<Model.STU3.Bundle> RefreshBundleAsync(this FhirStu3Client client, Model.STU3.Bundle bundle)
        {
            if (bundle == null) throw Error.ArgumentNull(nameof(bundle));

            if (bundle.Type != BundleType.Searchset)
                throw Error.Argument("Refresh is only applicable to bundles of type 'searchset'");

            // Clone old bundle, without the entries (so, just the header)
            var result = (Model.STU3.Bundle)bundle.DeepCopy();

            result.Id = "urn:uuid:" + Guid.NewGuid().ToString("n");
            result.Meta = new Meta();
            result.Meta.LastUpdated = DateTimeOffset.Now;

            foreach (var entry in result.Entry)
            {
                if (entry.Resource != null)
                {
                    entry.Resource = await client.ReadAsync<Resource>(entry.FullUrl).ConfigureAwait(false);
                }
            }

            return result;
        }

        public static Model.STU3.Bundle RefreshBundle(this FhirStu3Client client, Model.STU3.Bundle bundle)
        {
            return RefreshBundleAsync(client, bundle).WaitResult();
        }
    }
}
