using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

#nullable enable
namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// File names of the FHIR packages shipped with the SDK
    /// </summary>
    public static class CorePackageFileNames
    {
        public const string CORE_PACKAGENAME = "hl7.fhir.r5.core.tgz";
        public const string EXPANSIONS_PACKAGENAME = "hl7.fhir.r5.expansions.tgz";
    }

    /// <summary>Reads FHIR artifacts (Profiles, ValueSets, ...) from the FHIR Core package</summary>
    public class CorePackageSource : IAsyncResourceResolver, IArtifactSource, IConformanceSource
    {

        private FhirPackageResolver _resolver;

        /// <summary>Create a new <see cref="CorePackageSource()"/> instance to read FHIR artifacts from the FHIR core package.</summary>
        /// <returns>A new <see cref="CorePackageSource()"/> instance.</returns>
        public CorePackageSource()
        {
            var inspector = ModelInfo.ModelInspector;
            var corePackagePath = Path.Combine(Directory.GetCurrentDirectory(), CorePackageFileNames.CORE_PACKAGENAME);
            var coreExpansionPackagePath = Path.Combine(Directory.GetCurrentDirectory(), CorePackageFileNames.EXPANSIONS_PACKAGENAME);
            _resolver = new FhirPackageResolver(inspector, corePackagePath, coreExpansionPackagePath);
        }

        ///<inheritdoc/>
        public async Task<Resource?> ResolveByCanonicalUriAsync(string uri)
        {
            return await _resolver.ResolveByCanonicalUriAsync(uri).ConfigureAwait(false);
        }

        ///<inheritdoc/>
        public async Task<Resource?> ResolveByUriAsync(string uri)
        {
            return await _resolver.ResolveByUriAsync(uri).ConfigureAwait(false);
        }

        ///<inheritdoc/>
        public IEnumerable<string> ListArtifactNames()
        {
            return _resolver.ListArtifactNames();
        }

        ///<inheritdoc/>
        public Stream? LoadArtifactByName(string artifactName)
        {
            return _resolver.LoadArtifactByName(artifactName);
        }
        public Stream? LoadArtifactByPath(string filePath)
        {
            return _resolver.LoadArtifactByPath(filePath);
        }

        ///<inheritdoc/>
        public IEnumerable<string> ListResourceUris(ResourceType? filter = null)
        {
            return _resolver.ListResourceUris(filter?.GetLiteral());
        }

        ///<inheritdoc/>
        public async Task<CodeSystem?> FindCodeSystemByValueSetAsync(string valueSetUri)
        {
            var resource = await _resolver.FindCodeSystemByValueSet(valueSetUri).ConfigureAwait(false);
            return resource as CodeSystem;
        }

        ///<inheritdoc/>
        public CodeSystem? FindCodeSystemByValueSet(string valueSetUri)
        {
            return TaskHelper.Await(() => FindCodeSystemByValueSetAsync(valueSetUri));
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<ConceptMap>?> FindConceptMapsAsync(string? sourceUri = null, string? targetUri = null)
        {
            var resources = await _resolver.FindConceptMaps(sourceUri, targetUri).ConfigureAwait(false);
            return resources == null ? null : resources.Cast<ConceptMap>();
        }

        ///<inheritdoc/>
        public IEnumerable<ConceptMap>? FindConceptMaps(string? sourceUri = null, string? targetUri = null)
        {
            return TaskHelper.Await(() => FindConceptMapsAsync(sourceUri, targetUri));
        }

        ///<inheritdoc/>
        public async Task<NamingSystem?> FindNamingSystemAsync(string uniqueId)
        {
            var resource = await _resolver.FindNamingSystemByUniqueId(uniqueId).ConfigureAwait(false);
            return resource as NamingSystem;
        }

        ///<inheritdoc/>
        public NamingSystem? FindNamingSystem(string valueSetUri)
        {
            return TaskHelper.Await(() => FindNamingSystemAsync(valueSetUri));
        }

        ///<inheritdoc/>
        public Resource? ResolveByUri(string uri)
        {
            return TaskHelper.Await(() => ResolveByUriAsync(uri));
        }

        ///<inheritdoc/>
        public Resource? ResolveByCanonicalUri(string uri)
        {
            return TaskHelper.Await(() => ResolveByCanonicalUriAsync(uri));
        }
    }
}

#nullable restore
