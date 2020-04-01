/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;
using System.Linq;
using Hl7.Fhir.Utility;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// Implements <see cref="IResourceResolverAsync" /> on top of an <see cref="IResourceResolver" />
    /// </summary>
    public class SyncToAsyncResolver : IResourceResolverAsync, IResourceResolver
    {
        public IResourceResolver SyncResolver { get; private set; }

        public SyncToAsyncResolver(IResourceResolver sync) => SyncResolver = sync ?? throw new ArgumentNullException(nameof(sync));

        public Resource ResolveByUri(string uri) => SyncResolver.ResolveByUri(uri);
        public Resource ResolveByCanonicalUri(string uri) => SyncResolver.ResolveByCanonicalUri(uri);
        public Task<Resource> ResolveByUriAsync(string uri)
        {
            var result = ResolveByUri(uri);
            return Task.FromResult(result);
        }

        public Task<Resource> ResolveByCanonicalUriAsync(string uri)
        {
            var result = ResolveByCanonicalUri(uri);
            return Task.FromResult(result);
        }
    }
}
