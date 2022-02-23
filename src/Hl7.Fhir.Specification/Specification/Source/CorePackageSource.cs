using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

#nullable enable
namespace Hl7.Fhir.Specification.Source
{


    /// <summary>Reads FHIR artifacts (Profiles, ValueSets, ...) from the FHIR Core package</summary>
    public class CorePackageSource : IAsyncResourceResolver, IArtifactSource, IConformanceSource
    {
        private FhirPackageResolver _resolver;

        /// <summary>Create a new <see cref="CorePackageSource"/> instance to read FHIR artifacts from references FHIR packages.</summary>
        /// <param name="packageFilePaths">The file paths of the packages that are used as the source for resolving</param>
        public CorePackageSource(string[] packageFilePaths)
        {
            var inspector = ModelInfo.ModelInspector;
            _resolver = new FhirPackageResolver(inspector, packageFilePaths);
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
