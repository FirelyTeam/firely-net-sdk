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

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Interface for resolving FHIR artifacts by (canonical) uri.</summary>
#pragma warning disable CS0618 // Type or member is obsolete
    public interface IResourceResolver : ISyncOrAsyncResourceResolver
#pragma warning restore CS0618 // Type or member is obsolete
    {
        /// <summary>Find a resource based on its relative or absolute uri.</summary>
        /// <param name="uri">A resource uri.</param>
        Resource ResolveByUri(string uri);


        /// <summary>Find a (conformance) resource based on its canonical uri.</summary>
        /// <param name="uri">The canonical url of a (conformance) resource.</param>
        Resource ResolveByCanonicalUri(string uri);
    }


#pragma warning disable CS0618 // Type or member is obsolete
    public interface IAsyncResourceResolver : ISyncOrAsyncResourceResolver
#pragma warning restore CS0618 // Type or member is obsolete
    {
        /// <summary>Find a resource based on its relative or absolute uri.</summary>
        /// <param name="uri">A resource uri.</param>
        Task<Resource> ResolveByUriAsync(string uri);


        /// <summary>Find a (conformance) resource based on its canonical uri.</summary>
        /// <param name="uri">The canonical url of a (conformance) resource.</param>
        Task<Resource> ResolveByCanonicalUriAsync(string uri); // IConformanceResource
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
