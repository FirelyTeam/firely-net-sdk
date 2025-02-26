/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */
using System;
using System.Threading.Tasks;
using Hl7.Fhir.Model;

#nullable enable
namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Interface for resolving FHIR artifacts by (canonical) uri.</summary>
#pragma warning disable CS0618 // Type or member is obsolete
    public interface IResourceResolver : ISyncOrAsyncResourceResolver
#pragma warning restore CS0618 // Type or member is obsolete
    {
        /// <summary>Find a resource based on its relative or absolute uri.</summary>
        [Obsolete("This method does not provide information about the kind of error that lead to us returning null. Consider using TryResolveByUri instead.")]
        Resource? ResolveByUri(string uri);

        /// <summary>Find a (conformance) resource based on its canonical uri.</summary>
        /// <param name="uri">The canonical url of a (conformance) resource.</param>
        [Obsolete("This method does not provide information about the kind of error that lead to us returning null. Consider using TryResolveByCanonicalUri instead.")]
        Resource? ResolveByCanonicalUri(string uri);

        /// <summary>Find a resource based on its relative or absolute uri.</summary>
        /// <param name="uri">A resource uri.</param>
        /// <returns><see cref="ResolverResult"/> with an actual resource, or the <see cref="ResolverResult.Error"/>.</returns>
        ResolverResult TryResolveByUri(string uri)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            var resource = this.ResolveByUri(uri); 
#pragma warning restore CS0618 // Type or member is obsolete

            if (resource is not null) 
                return resource;
            
            return ResolverException.NotFound();
        }
        
        /// <summary>Find a (conformance) resource based on its canonical uri.</summary>
        /// <param name="uri">The canonical url of a (conformance) resource.</param>
        /// <returns><see cref="ResolverResult"/> with an actual resource, or the <see cref="ResolverResult.Error"/>.</returns>
        ResolverResult TryResolveByCanonicalUri(string uri)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            var resource = this.ResolveByCanonicalUri(uri); 
#pragma warning restore CS0618 // Type or member is obsolete

            if (resource is not null) 
                return resource;
            
            return ResolverException.NotFound();
        }
    }


#pragma warning disable CS0618 // Type or member is obsolete
    public interface IAsyncResourceResolver : ISyncOrAsyncResourceResolver
#pragma warning restore CS0618 // Type or member is obsolete
    {
        /// <summary>Find a resource based on its relative or absolute uri.</summary>
        /// <param name="uri">A resource uri.</param>
        [Obsolete("This method does not provide information about the kind of error that lead to us returning null. Consider using TryResolveByUriAsync instead.")]
        Task<Resource?> ResolveByUriAsync(string uri);


        /// <summary>Find a (conformance) resource based on its canonical uri.</summary>
        /// <param name="uri">The canonical url of a (conformance) resource.</param>
        [Obsolete("This method does not provide information about the kind of error that lead to us returning null. Consider using TryResolveByCanonicalUriAsync instead.")]
        Task<Resource?> ResolveByCanonicalUriAsync(string uri); // IConformanceResource

        /// <summary>Find a resource based on its relative or absolute uri.</summary>
        /// <param name="uri">A resource uri.</param>
        /// <returns><see cref="ResolverResult"/> with an actual resource, or the <see cref="ResolverResult.Error"/>.</returns>
        async Task<ResolverResult> TryResolveByUriAsync(string uri)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            var resource = await this.ResolveByUriAsync(uri).ConfigureAwait(false);
#pragma warning restore CS0618 // Type or member is obsolete

            if (resource is not null)
                return resource;

            return ResolverException.NotFound();
        }

        /// <summary>Find a (conformance) resource based on its canonical uri.</summary>
        /// <param name="uri">The canonical url of a (conformance) resource.</param>
        /// <returns><see cref="ResolverResult"/> with an actual resource, or the <see cref="ResolverResult.Error"/>.</returns>
        async Task<ResolverResult> TryResolveByCanonicalUriAsync(string uri)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            var resource = await this.ResolveByCanonicalUriAsync(uri).ConfigureAwait(false); 
#pragma warning restore CS0618 // Type or member is obsolete

            if (resource is not null) 
                return resource;
            
            return ResolverException.NotFound();
        }
    }

    /// <summary>
    /// Empty marker interface to allow sync-backwards compatible code to support both sync and async resolvers.
    /// </summary>
    [Obsolete("This marker interface is used for backwards-compatibility only and should not be used in your code. " +
        "Explicitly use IResourceResolver (also obsolete) or preferably IAsyncResourceResolver instead.")]
    public interface ISyncOrAsyncResourceResolver
    {
    }
}
