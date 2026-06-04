/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Utility;
using System;
using System.Diagnostics;
using System.Net;
using Tasks = System.Threading.Tasks;

#nullable enable

namespace Hl7.Fhir.Specification.Source;

/// <summary>Fetches FHIR artifacts (Profiles, ValueSets, ...) from a FHIR server.</summary>
[DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
public class WebResolver : IResourceResolver, IAsyncResourceResolver
{
    /// <summary>Default request timeout in milliseconds.</summary>
    public const int DefaultTimeOut = 5000;
    private readonly Func<Uri, FhirClient> _clientFactory;

    /// <summary>
    /// Gets the runtime <see cref="Exception"/> from the last call to the
    /// <see cref="ResolveByUri(string)"/> method, if any, or <c>null</c> otherwise.
    /// </summary>
    public Exception? LastError { get; private set; }

    /// <summary>Default constructor.</summary>
    public WebResolver() : this(ep => new FhirClient(ep))
    {
        // Nothing
    }

    /// <summary>Create a new <see cref="WebResolver"/> instance that supports a custom <see cref="FhirClient"/> implementation.</summary>
    /// <param name="fhirClientFactory">
    /// Factory function that should create a new <see cref="FhirClient"/> instance for the specified <see cref="Uri"/>.
    /// If this parameter equals <c>null</c>, then the new instance creates a default <see cref="FhirClient"/> instance.
    /// </param>
    public WebResolver(Func<Uri, FhirClient> fhirClientFactory)
    {
        _clientFactory = fhirClientFactory ?? throw Error.ArgumentNull(nameof(fhirClientFactory));
    }


    public Resource? ResolveByUri(string uri) => TryResolveByUri(uri).Value;

    public Resource? ResolveByCanonicalUri(string uri) => ResolveByUri(uri);

    /// <summary>
    /// Uses provided <paramref name="uri"/> to construct <see cref="ResourceIdentity"/> and then tries to read the resource from that source.
    /// </summary>
    /// <param name="uri">Uri to attempt resolving on</param>
    /// <returns><see cref="ResolverResult"/> with an actual resource, or the <see cref="ResolverResult.Error"/>.</returns>
    /// <exception cref="ArgumentNullException">Provided <paramref name="uri"/> is <c>null</c>.</exception>
    public ResolverResult TryResolveByUri(string uri)
    {
        if (uri == null) throw Error.ArgumentNull(nameof(uri));
        if (!ResourceIdentity.IsRestResourceIdentity(uri))
            return ResolverException.NotValidResourceIdentity(uri);

        var id = new ResourceIdentity(uri);
        var client = _clientFactory(id.BaseUri);

        try
        {
            var resultResource = TaskHelper.Await(() => client.ReadAsync<Resource>(id));
            if (resultResource is null)
                return ResolverException.NotFound(client.LastResult?.Outcome as OperationOutcome);

            resultResource.SetOrigin(uri);
            LastError = null;
            return resultResource;
        }
        catch (FhirOperationException foe)
        {
            LastError = foe;
            return ResolverException.OperationFailed("Error occurred during Fhir operation", foe);
        }
        catch (WebException we)
        {
            LastError = we;
            return ResolverException.OperationFailed("Error occurred during web operation", we);
        }
        // Other runtime exceptions are fatal...
    }

    /// <inheritdoc cref="TryResolveByUri"/>
    public ResolverResult TryResolveByCanonicalUri(string uri) => TryResolveByUri(uri);
    public Tasks.Task<Resource?> ResolveByUriAsync(string uri) => Tasks.Task.FromResult(ResolveByUri(uri));
    public Tasks.Task<Resource?> ResolveByCanonicalUriAsync(string uri) => Tasks.Task.FromResult(ResolveByCanonicalUri(uri));
    /// <inheritdoc cref="TryResolveByUri"/>
    public Tasks.Task<ResolverResult> TryResolveByUriAsync(string uri) => Tasks.Task.FromResult(TryResolveByUri(uri));
    /// <inheritdoc cref="TryResolveByUri"/>
    public Tasks.Task<ResolverResult> TryResolveByCanonicalUriAsync(string uri) => Tasks.Task.FromResult(TryResolveByUri(uri));

    // Allow derived classes to override
    // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    protected internal virtual string DebuggerDisplay
        => $"{GetType().Name}"
           + (LastError != null ? $" LastError: '{LastError.Message}'" : null);


}