using Firely.Fhir.Packages;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Reads FHIR artifacts (Profiles, ValueSets, ...) from one or multiple FHIR packages. This functionaly is FHIR version agnostic.</summary>
    public class CommonFhirPackageSource : IAsyncResourceResolver, IArtifactSource
    {
        private Lazy<PackageContext> _context;
        private ModelInspector _provider;

        /// <summary>Create a new <see cref="CommonFhirPackageSource"/> instance to read FHIR artifacts from one or multiple FHIR packages of a specific FHIR version
        /// found in the paths passed to this function.</summary>
        /// <returns>A new <see cref="CommonFhirPackageSource"/> instance.</returns>
        /// <param name="provider">A <see cref="ModelInspector"/> used to parse the filecontents to FHIR resources, this is typically a <see cref="ModelInspector"/> containing the definitions of a specific FHIR version. </param>
        /// <param name="filePaths">A path to the FHIR package files.</param>
        public CommonFhirPackageSource(ModelInspector provider, params string[] filePaths)
        {
            _context = new Lazy<PackageContext>(() => TaskHelper.Await(() => createPackageContextFromFilesAsync(filePaths)));
            _provider = provider;
        }


        /// <summary>Create a new <see cref="CommonFhirPackageSource"/> instance to read FHIR artifacts from one or multiple FHIR packages of a specific FHIR version.</summary>
        /// <param name="provider">A <see cref="ModelInspector"/> used to parse the file contents to FHIR resources, this is typically a <see cref="ModelInspector"/> containing the definitions of a specific FHIR version. </param>
        /// <param name="packageServer">The package server from which to retrieve the FHIR packages</param>
        /// <param name="packageNames">The FHIR packages which are used to resolve artifacts from</param>
        public CommonFhirPackageSource(ModelInspector provider, string packageServer, string[] packageNames)
        {
            _context = new Lazy<PackageContext>(() => TaskHelper.Await(() => createPackageContextFromExternalSource(packageServer, packageNames)));
            _provider = provider;
        }

        private static async Task<PackageContext> createPackageContextFromExternalSource(string packageServer, string[] packageNames)
        {
            var client = PackageClient.Create(packageServer);
            var scopePath = getScopePath();
            _ = await initialize(scopePath, "Firely SDK Temp Package", "0.1.0", "Firely SDK", "Temporary package used for resolving artifacts from its dependencies", packageNames);
            return await createContext(scopePath, client);

        }

        private static async Task<PackageManifest> initialize(string path, string name, string version, string author, string description, string[] dependencies)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            var project = new FolderProject(path);

            var manifest = new PackageManifest(name, version)
            {
                Author = author,
                Description = description,
            };

            if (dependencies?.Any() == true)
            {
                var packageDependencies = createDependencies(dependencies);
                foreach (var packageDep in packageDependencies) manifest.AddDependency(packageDep);
            }

            await project.WriteManifest(manifest);
            return manifest;

            static IEnumerable<PackageDependency> createDependencies(string[] dependencies)
            {
                foreach (var dep in dependencies)
                {
                    var splitDependency = dep.Split('@');
                    if (splitDependency.Length == 1)
                        yield return new PackageDependency(dep, "latest");
                    else
                    {
                        var versionDep = "=" + splitDependency[1];
                        yield return new PackageDependency(splitDependency[0], versionDep);
                    }
                }
            }
        }

        private static async Task<PackageContext> createPackageContextFromFilesAsync(params string[] paths)
        {
            foreach (var path in paths)
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException($"File was not found: '{path}'.");
            }

            var scopePath = getScopePath();
            return await createContext(scopePath, client: null, paths);
        }

        private static string getScopePath()
        {
            var scopePath = Path.Combine(Path.GetTempPath(), "package-" + Path.GetRandomFileName());
            if (!Directory.Exists(scopePath))
            {
                Directory.CreateDirectory(scopePath);
            }
            return scopePath;
        }

        private static async Task<PackageContext> createContext(string scopePath, PackageClient? client, string[]? filePaths = null, bool localCache = false)
        {
            string? cache_folder = localCache ? scopePath : null;
            var cache = new DiskPackageCache(cache_folder);
            var project = new FolderProject(scopePath);
            var scope = new PackageContext(cache, project, client);

            //install packages from a package server
            if (client is not null)
            {
                var closure = await scope.Restore();

                if (closure.Missing.Any())
                {
                    var missingDeps = string.Join(", ", closure.Missing);
                    throw new FileNotFoundException($"Could not resolve all dependencies. Missing: {missingDeps}.");
                }
            }
            //install packages from local machine
            if (filePaths is not null && filePaths.Any())
            {
                foreach (var path in filePaths)
                {
                    await installPackageFromPath(scope, path);
                }
            }


            return scope;
        }

        private static async Task installPackageFromPath(PackageContext scope, string path)
        {
            var packageManifest = Packaging.ExtractManifestFromPackageFile(path);
            if (packageManifest is not null)
            {
                var reference = packageManifest.GetPackageReference();
                await scope.Cache.Install(reference, path);

                var dependency = new PackageDependency(reference.Name ?? "", reference.Version);
                if (reference.Found)
                {
                    var manifest = await scope.Project.ReadManifest();
                    var fhirVersion = packageManifest.GetFhirVersion();
                    if (fhirVersion is null)
                    {
                        throw new("Manifest doesn't contain a valid FHIR version");
                    }
                    manifest ??= ManifestFile.Create("temp", fhirVersion);
                    if (manifest.Name == reference.Name)
                    {
                        throw new("Skipped updating package manifest because it would cause the package to reference itself.");
                    }
                    else
                    {
                        manifest.AddDependency(dependency);
                        await scope.Project.WriteManifest(manifest);
                        await scope.Restore();
                    }
                }
            }

        }

        private Resource? toResource(string content)
        {
            try
            {
                var sourceNode = content.StartsWith("{")
                                ? FhirJsonNode.Parse(content)
                                : FhirXmlNode.Parse(content);

                return sourceNode.ToPoco(_provider) as Resource;
            }
            catch
            {
                return null;
            }
        }

        ///<inheritdoc/>
        public async Task<Resource?> ResolveByCanonicalUriAsync(string uri)
        {
            var content = await ResolveByCanonicalUriAsyncAsString(uri).ConfigureAwait(false);
            return content is null ? null : toResource(content);
        }

        //internal for test purposes
        internal async Task<string?> ResolveByCanonicalUriAsyncAsString(string uri)
        {
            (var url, var version) = splitCanonical(uri);
            return await _context.Value.GetFileContentByCanonical(url, version, resolveBestCandidate: true).ConfigureAwait(false);
        }


        private static (string url, string version) splitCanonical(string canonical)
        {
            if (canonical.EndsWith("|"))
                canonical = canonical.Substring(0, canonical.Length - 1);

            var position = canonical.LastIndexOf('|');

            return position == -1 ?
                (canonical, "")
                : (canonical.Substring(0, position), canonical.Substring(position + 1));
        }


        ///<inheritdoc/>
        public async Task<Resource?> ResolveByUriAsync(string uri)
        {
            var content = await ResolveByUriAsyncAsString(uri).ConfigureAwait(false);
            return content is null ? null : toResource(content);
        }

        //internal for test purposes
        internal async Task<string?> ResolveByUriAsyncAsString(string uri)
        {
            uri.SplitLeft('/').Deconstruct(out var resource, out var id);

            if (resource == null || id is null)
                return null;

            return await _context.Value.GetFileContentById(resource, id).ConfigureAwait(false);
        }

        ///<inheritdoc/>
        public IEnumerable<string> ListArtifactNames()
        {
            return _context.Value.GetFileNames();
        }

        ///<inheritdoc/>
        public Stream? LoadArtifactByName(string artifactName)
        {
            var content = TaskHelper.Await(() => _context.Value.GetFileContentByFileName(artifactName));
            return content == null ? null : new MemoryStream(Encoding.UTF8.GetBytes(content));
        }

        ///<inheritdoc/>
        public Stream? LoadArtifactByPath(string artifactPath)
        {
            var content = TaskHelper.Await(() => _context.Value.GetFileContentByFilePath(artifactPath));
            return content == null ? null : new MemoryStream(Encoding.UTF8.GetBytes(content));
        }

        ///<inheritdoc/>
        public IEnumerable<string> ListResourceUris(string? filter = null)
        {
            return _context.Value.ListCanonicalUris(filter);
        }

        ///<inheritdoc/>
        public async Task<Resource?> FindCodeSystemByValueSet(string valueSetUri)
        {
            var content = await FindCodeSystemByValueSetAsString(valueSetUri).ConfigureAwait(false);
            return content is null ? null : toResource(content);
        }

        //internal for test purposes
        internal async Task<string?> FindCodeSystemByValueSetAsString(string valueSetUri)
        {
            return await _context.Value.GetCodeSystemByValueSet(valueSetUri).ConfigureAwait(false);
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<Resource>> FindConceptMaps(string? sourceUri = null, string? targetUri = null)
        {
            var content = await FindConceptMapsAsStrings(sourceUri, targetUri).ConfigureAwait(false);
            return from item in content
                   let resource = toResource(item)
                   where resource is not null
                   select resource;
        }

        //internal for test purposes
        internal async Task<IEnumerable<string>> FindConceptMapsAsStrings(string? sourceUri = null, string? targetUri = null)
        {
            var cms = await _context.Value.GetConceptMapsBySourceAndTarget(sourceUri, targetUri).ConfigureAwait(false);
            return cms is not null ? cms : Enumerable.Empty<string>();
        }

        ///<inheritdoc/>
        public async Task<Resource?> FindNamingSystemByUniqueId(string uniqueId)
        {
            var content = await FindNamingSystemByUniqueIdAsString(uniqueId).ConfigureAwait(false);
            return content is null ? null : toResource(content);
        }

        //internal for test purposes
        internal async Task<string?> FindNamingSystemByUniqueIdAsString(string uniqueId)
        {
            return await _context.Value.GetNamingSystemByUniqueId(uniqueId).ConfigureAwait(false);
        }
    }
}

#nullable restore
