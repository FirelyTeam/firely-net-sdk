/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System;
using System.Diagnostics;
using System.Net;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Fetches FHIR artifacts (Profiles, ValueSets, ...) from a FHIR server.</summary>
    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
    public class WebResolver : IResourceResolver
    {
        /// <summary>Default request timeout in milliseconds.</summary>
        public const int DefaultTimeOut = 5000;

        readonly Func<Uri, FhirClient> _clientFactory;

        /// <summary>Default constructor.</summary>
        public WebResolver() { }

        /// <summary>Create a new <see cref="WebResolver"/> instance that supports a custom <see cref="FhirClient"/> implementation.</summary>
        /// <param name="fhirClientFactory">
        /// Factory function that should create a new <see cref="FhirClient"/> instance for the specified <see cref="Uri"/>.
        /// If this parameter equals <c>null</c>, then the new instance creates a default <see cref="FhirClient"/> instance.
        /// </param>
        public WebResolver(Func<Uri, FhirClient> fhirClientFactory)
        {
            _clientFactory = fhirClientFactory ?? throw Error.ArgumentNull(nameof(fhirClientFactory));
        }

        /// <summary>Gets or sets the request timeout of the internal <see cref="FhirClient"/> instance.</summary>
        public int TimeOut { get; set; } = DefaultTimeOut;

        /// <summary>
        /// Gets the runtime <see cref="Exception"/> from the last call to the
        /// <see cref="ResolveByUri(string)"/> method, if any, or <c>null</c> otherwise.
        /// </summary>
        public Exception LastError { get; private set; }

        public Resource ResolveByUri(string uri)
        {
            if (uri == null) throw Error.ArgumentNull(nameof(uri));
            if (!ResourceIdentity.IsRestResourceIdentity(uri))
            {
                // Weakness in FhirClient, need to have the base :-(  So return null if we cannot determine it.
                return null;     
            }

            var id = new ResourceIdentity(uri);
            var client = _clientFactory?.Invoke(id.BaseUri)
                         ?? new FhirClient(id.BaseUri) { Timeout = this.TimeOut };

            try
            {
                var resultResource = client.Read<Resource>(id);
                resultResource.SetOrigin(uri);
                LastError = null;
                return resultResource;
            }
            catch (FhirOperationException foe)
            {
                LastError = foe;
                return null;
            }
            catch (WebException we)
            {
                LastError = we;
                return null;
            }
            // Other runtime exceptions are fatal...
        }

        public Resource ResolveByCanonicalUri(string uri)
        {
            return ResolveByUri(uri);
        }

        // Allow derived classes to override
        // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal protected virtual string DebuggerDisplay
            => $"{GetType().Name}"
            + (LastError != null ? $" LastError: '{LastError.Message}'" : null);


    }
}
