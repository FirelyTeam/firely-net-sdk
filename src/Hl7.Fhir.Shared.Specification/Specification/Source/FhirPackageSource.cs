/* 
 * Copyright (c) 2022, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Source
{

    /// <summary>Reads FHIR version specific artifacts (Profiles, ValueSets, ...) from the FHIR Packages</summary>
    public class FhirPackageSource : IAsyncResourceResolver, IArtifactSource, IConformanceSource
    {
        private CommonFhirPackageSource _resolver;

        /// <summary>Create a new <see cref="FhirPackageSource"/> instance to read FHIR artifacts from the referenced FHIR packages.</summary>
        /// <param name="packageFilePaths">The file paths of the packages that are used as the source for resolving</param>
        public FhirPackageSource(string[] packageFilePaths)
        {
            var inspector = ModelInfo.ModelInspector;
            _resolver = new CommonFhirPackageSource(inspector, packageFilePaths);
        }

        /// <summary>Create a new <see cref="CommonFhirPackageSource"/> instance to read FHIR artifacts from one or multiple FHIR packages.</summary>
        /// <param name="packageServer">The package server from which to retrieve the FHIR packages</param>
        /// <param name="packageNames">The FHIR package names which are used to resolve artifacts from, example: hl7.fhir.r3.expansions@3.0.2</param>
        public FhirPackageSource(string packageServer, string[] packageNames)
        {
            var inspector = ModelInfo.ModelInspector;
            _resolver = new CommonFhirPackageSource(inspector, packageServer, packageNames);
        }

        /// <summary>
        /// Create a new <see cref="FhirPackageSource"/> that includes the all Core FHIR artifacts including the expanded value sets.
        /// </summary>
        /// <remarks>Needs an active internet connection for first installation, FHIR packages will be cached locally after</remarks>
        /// <returns>A new <see cref="FhirPackageSource"/> that includes all Core FHIR definitions/artifacts including the expanded value sets.</returns>
        public static FhirPackageSource CreateFhirCorePackageSource()
        {
            return new FhirPackageSource(CorePackageFileNames.FHIR_PACKAGE_SERVER, new string[] { CorePackageFileNames.FHIR_CORE_PACKAGE_NAME, CorePackageFileNames.FHIR_CORE_EXPANSIONS_PACKAGE_NAME });
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
