/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// Implements <see cref="IAsyncResourceResolver" /> on top of an <see cref="IResourceResolver" />
    /// </summary>
    public class SyncToAsyncResolver : IAsyncResourceResolver
    {
        public IResourceResolver SyncResolver { get; private set; }

        public SyncToAsyncResolver(IResourceResolver sync) => SyncResolver = sync ?? throw new ArgumentNullException(nameof(sync));

        public Task<Resource> ResolveByUriAsync(string uri)
        {
            var result = SyncResolver.ResolveByUri(uri);

            return Task.FromResult(result);
        }

        public Task<Resource> ResolveByCanonicalUriAsync(string uri)
        {
            var result = SyncResolver.ResolveByCanonicalUri(uri);
            return Task.FromResult(result);
        }
    }

    public static class SyncToAsyncResolverExtensions
    {
        /// <summary>
        /// Converts a (possibly non-async) resource resolver to an async one.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        /// <remarks>Note that this async method will block on the possibly synchronous source. This method
        /// is meant for temporary backwards-compatiblity reasons only.</remarks>
#pragma warning disable CS0618 // Type or member is obsolete
        public static IAsyncResourceResolver AsAsync(this ISyncOrAsyncResourceResolver source) =>
#pragma warning restore CS0618 // Type or member is obsolete
            source is IAsyncResourceResolver ar ? ar
                : source is IResourceResolver sr ? new SyncToAsyncResolver(sr)
                    : throw new ArgumentException($"Argument {nameof(source)} is neither a {nameof(IResourceResolver)} nor " +
                        $"a {nameof(IAsyncResourceResolver)}");
    }

}