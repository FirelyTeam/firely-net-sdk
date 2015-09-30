/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Support;
using System.Net;

namespace Hl7.Fhir.Specification.Source
{
    public class WebArtifactSource : IArtifactSource
    {
        /// <summary>Default constructor.</summary>
        public WebArtifactSource()  { }

        Func<Uri, FhirClient> _clientFactory;

        /// <summary>Create a new <see cref="WebArtifactSource"/> instance that supports a custom <see cref="FhirClient"/> implementation.</summary>
        /// <param name="fhirClientFactory">
        /// Factory function that should create a new <see cref="FhirClient"/> instance for the specified <see cref="Uri"/>.
        /// If this parameter equals <c>null</c>, then the new instance creates a default <see cref="FhirClient"/> instance.
        /// </param>
        public WebArtifactSource(Func<Uri, FhirClient> fhirClientFactory) { _clientFactory = fhirClientFactory; }

        public System.IO.Stream LoadArtifactByName(string name)
        {
            throw new NotImplementedException();        // support only url-based artifacts
        }

        public IEnumerable<string> ListArtifactNames()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ConformanceInformation> ListConformanceResources()
        {
            throw new NotImplementedException();
        }

        public Hl7.Fhir.Model.Resource LoadConformanceResourceByUrl(string url)
        {
            if (url == null) throw Error.ArgumentNull("identifier");

            if (!ResourceIdentity.IsRestResourceIdentity(url)) return null;     // Weakness in FhirClient, need to have the base :-(  So return null if we cannot determine it.

            var id = new ResourceIdentity(url);

            // [WMR 20150810] Use custom FhirClient factory if specified
            var client = _clientFactory != null ? _clientFactory(id.BaseUri) : new FhirClient(id.BaseUri) { Timeout = 5000 };

            try
            {
                return client.Read<Resource>(id);
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
    }
}
