/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System.Net;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Specification.Source
{
    public class WebResolver : IResourceResolver
    {
        /// <summary>Default constructor.</summary>
        public WebResolver()
        {
        }

        Func<Uri, FhirClient> _clientFactory;

        /// <summary>Create a new <see cref="WebResolver"/> instance that supports a custom <see cref="FhirClient"/> implementation.</summary>
        /// <param name="fhirClientFactory">
        /// Factory function that should create a new <see cref="FhirClient"/> instance for the specified <see cref="Uri"/>.
        /// If this parameter equals <c>null</c>, then the new instance creates a default <see cref="FhirClient"/> instance.
        /// </param>
        public WebResolver(Func<Uri, FhirClient> fhirClientFactory) { _clientFactory = fhirClientFactory; }


        public Hl7.Fhir.Model.Resource ResolveByUri(string uri)
        {
            if (uri == null) throw Error.ArgumentNull(nameof(uri));

            if (!ResourceIdentity.IsRestResourceIdentity(uri)) return null;     // Weakness in FhirClient, need to have the base :-(  So return null if we cannot determine it.

            var id = new ResourceIdentity(uri);

            // [WMR 20150810] Use custom FhirClient factory if specified
            var client = _clientFactory != null ? _clientFactory(id.BaseUri) : new FhirClient(id.BaseUri) { Timeout = 5000 };

            try
            {
                var resultResource = client.Read<Resource>(id);
                resultResource.SetOrigin(uri);
                return resultResource;
            }
            catch (FhirOperationException)
            {
                return null;
            }
            catch (WebException)
            {
                return null;
            }

        }

        public Resource ResolveByCanonicalUri(string uri)
        {
            return ResolveByUri(uri);
        }
    }
}
