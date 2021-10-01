using Hl7.Fhir.Model;
using System.IO;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Source
{
    public class CorePackageSource : IAsyncResourceResolver
    {
        private const string PACKAGENAME = "hl7.fhir.r3.core-3.0.2.tgz";
        private NpmPackageResolver _resolver;

        public CorePackageSource()
        {
            var inspector = ModelInfo.ModelInspector;
            var corePackagePath = Path.Combine(Directory.GetCurrentDirectory(), PACKAGENAME);
            _resolver = new NpmPackageResolver(corePackagePath, inspector);
        }
        public async Task<Resource> ResolveByCanonicalUriAsync(string uri)
        {
            return await _resolver.ResolveByCanonicalUriAsync(uri);
        }
        public async Task<Resource> ResolveByUriAsync(string uri)
        {
            return await _resolver.ResolveByCanonicalUriAsync(uri);
        }
    }
}
