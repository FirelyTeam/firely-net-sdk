using Hl7.Fhir.Model;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

#nullable enable
namespace Hl7.Fhir.Specification.Source
{
    public class CorePackageSource : IAsyncResourceResolver, IArtifactSource
    {
        private const string PACKAGENAME = "hl7.fhir.r3.core-3.0.2.tgz";
        private const string PACKAGENAME_EXPANSIONS = "hl7.fhir.r3.expansions-3.0.2.tgz";
        private NpmPackageResolver _resolver;

        public CorePackageSource()
        {
            var inspector = ModelInfo.ModelInspector;
            var corePackagePath = Path.Combine(Directory.GetCurrentDirectory(), PACKAGENAME);
            var coreExpansionPackagePath = Path.Combine(Directory.GetCurrentDirectory(), PACKAGENAME_EXPANSIONS);
            _resolver = new NpmPackageResolver(inspector, corePackagePath, coreExpansionPackagePath);
        }

        public async Task<Resource?> ResolveByCanonicalUriAsync(string uri)
        {
            return await _resolver.ResolveByCanonicalUriAsync(uri);
        }
        public async Task<Resource?> ResolveByUriAsync(string uri)
        {
            return await _resolver.ResolveByCanonicalUriAsync(uri);
        }
        public IEnumerable<string> ListArtifactNames()
        {
            return _resolver.ListArtifactNames();
        }
        public Stream? LoadArtifactByName(string artifactName)
        {
            return _resolver.LoadArtifactByName(artifactName);
        }

    }
}

#nullable restore
