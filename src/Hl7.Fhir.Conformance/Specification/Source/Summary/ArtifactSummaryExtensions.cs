/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Extension methods for <see cref="IEnumerable{ArtifactSummary}"/> sequences.</summary>
    public static class ArtifactSummaryExtensions
    {
        /// <summary>Filter <see cref="ArtifactSummary"/> instances with errors.</summary>
        public static IEnumerable<ArtifactSummary> Errors(this IEnumerable<ArtifactSummary> summaries)
            => summaries.Where(s => s.IsFaulted);

        /// <summary>
        /// Filter <see cref="ArtifactSummary"/> instances for resources with the specified <paramref name="resourceTypeName"/>.
        /// If <paramref name="resourceTypeName"/> equals <c>null</c>, then filter summaries for all valid FHIR resources.
        /// </summary>
        public static IEnumerable<ArtifactSummary> OfResourceType(this IEnumerable<ArtifactSummary> summaries, string? resourceTypeName)
            => resourceTypeName is not null
            ? summaries.Where(s => s.ResourceTypeName == resourceTypeName)
            : summaries.Where(s => s.IsFhirResource);

        /// <summary>Filter <see cref="ArtifactSummary"/> instances for valid FHIR resources.</summary>
        public static IEnumerable<ArtifactSummary> FhirResources(this IEnumerable<ArtifactSummary> summaries)
            => summaries.Where(s => s.IsFhirResource);

        /// <summary>Find <see cref="ArtifactSummary"/> instances for <c>NamingSystem</c> resources with the specified uniqueId value.</summary>
        public static IEnumerable<ArtifactSummary> FindNamingSystems(this IEnumerable<ArtifactSummary> summaries, string uniqueId)
            => summaries.OfResourceType(FhirTypeNames.NAMINGSYSTEM_NAME).Where(ns => ns.HasNamingSystemUniqueId(uniqueId));

        /// <summary>Filter <see cref="ArtifactSummary"/> instances for conformance resources.</summary>
        public static IEnumerable<ArtifactSummary> ConformanceResources(this IEnumerable<ArtifactSummary> summaries, IModelInfo modelInfo)
            => summaries.Where(s => s.ResourceTypeName is not null && modelInfo.IsConformanceResource(s.ResourceTypeName));

        /// <summary>Find <see cref="ArtifactSummary"/> instances for conformance resources with the specified canonical url.</summary>
        public static IEnumerable<ArtifactSummary> FindConformanceResources(this IEnumerable<ArtifactSummary> summaries, string canonicalUrl, IModelInfo modelInfo)
        {
            var values = canonicalUrl.Split('|');
            if (values.Length > 2)
                throw Error.Argument("Url is not valid. The pipe occures more than once.");

            var version = values.Length == 2 ? values[1] : string.Empty;

            return summaries.ConformanceResources(modelInfo).Where(r => r.GetConformanceCanonicalUrl() == values[0] &&
                                                               (string.IsNullOrEmpty(version) || Canonical.MatchesVersion(r.GetConformanceVersion(), version)));
        }

        /// <summary>Filter <see cref="ArtifactSummary"/> instances for <see cref="CodeSystem"/> resources with the specified valueSet uri.</summary>
        public static IEnumerable<ArtifactSummary> FindCodeSystems(this IEnumerable<ArtifactSummary> summaries, string valueSetUri)
            => summaries.OfResourceType(FhirTypeNames.CODESYSTEM_NAME).Where(r => r.GetCodeSystemValueSet() == valueSetUri);

        /// <summary>Find <see cref="ArtifactSummary"/> instances for <c>ConceptMap</c> resources with the specified source and/or target uri(s).</summary>
        public static IEnumerable<ArtifactSummary> FindConceptMaps(this IEnumerable<ArtifactSummary> summaries, string? sourceUri = null, string? targetUri = null)
        {
            IEnumerable<ArtifactSummary> result = summaries.OfResourceType(FhirTypeNames.CONCEPTMAP_NAME);
            if (sourceUri is not null)
            {
                result = result.Where(cm => cm.GetConceptMapSource() == sourceUri);
            }
            if (targetUri is not null)
            {
                result = result.Where(cm => cm.GetConceptMapTarget() == targetUri);
            }
            return result;
        }

        /// <summary>Find <see cref="ArtifactSummary"/> instance(s) for resources contained in the specified file.</summary>
        public static IEnumerable<ArtifactSummary> FromFile(this IEnumerable<ArtifactSummary> summaries, string filePath)
            => summaries.Where(s => StringComparer.OrdinalIgnoreCase.Equals(s.Origin, filePath));

        /// <summary>Resolve the <see cref="ArtifactSummary"/> for the resource with the specified uri.</summary>
        public static ArtifactSummary? ResolveByUri(this IEnumerable<ArtifactSummary> summaries, string uri, IModelInfo modelInfo)
        {
            var matches = summaries.Where(r => r.ResourceUri == uri);
            return matches.SingleOrDefault(modelInfo, ResourceUriConflictExceptionFactory);
        }

        /// <summary>Resolve the <see cref="ArtifactSummary"/> for the comformance resource with the specified canonical uri.</summary>
        /// <exception cref="ResolvingConflictException">
        /// The <see cref="ISummarySource"/> instance encountered conflicting Conformance Resource artifacts with the same canonical url identifier.
        /// </exception>
        public static ArtifactSummary? ResolveByCanonicalUri(this IEnumerable<ArtifactSummary> summaries, string canonicalUrl, IModelInfo modelInfo)
        {
            var matches = summaries.FindConformanceResources(canonicalUrl, modelInfo);
            return matches.SingleOrDefault(modelInfo, CanonicalUrlConflictExceptionFactory);
        }

        /// <summary>Resolve the <see cref="ArtifactSummary"/> for the <c>NamingSystem</c> resource with the specified uniqueId.</summary>
        public static ArtifactSummary? ResolveNamingSystem(this IEnumerable<ArtifactSummary> summaries, string uniqueId, IModelInfo modelInfo)
            => summaries.FindNamingSystems(uniqueId).SingleOrDefault(modelInfo, NamingSystemUrlConflictExceptionFactory);

        /// <summary>Resolve the <see cref="ArtifactSummary"/> for the <see cref="CodeSystem"/> resource with the specified ValueSet uri.</summary>
        public static ArtifactSummary? ResolveCodeSystem(this IEnumerable<ArtifactSummary> summaries, string uri, IModelInfo modelInfo)
            => summaries.FindCodeSystems(uri).SingleOrDefault(modelInfo, CodeSystemConflictExceptionFactory);

        /// <summary>Resolve the <see cref="ArtifactSummary"/> for the <c>ConceptMap</c> resource with the specified source and/or target uri(s).</summary>
        public static ArtifactSummary? ResolveConceptMap(this IEnumerable<ArtifactSummary> summaries, IModelInfo modelInfo, string? sourceUri = null, string? targetUri = null)
            => summaries.FindConceptMaps(sourceUri, targetUri).SingleOrDefault(modelInfo, ConceptMapUrlConflictExceptionFactory);

        #region Private helpers

        private static readonly Func<IEnumerable<ArtifactSummary>, IModelInfo, Exception> ResourceUriConflictExceptionFactory
            = new Func<IEnumerable<ArtifactSummary>, IModelInfo, Exception>(ToResourceUriConflictException);

        private static ResolvingConflictException ToResourceUriConflictException(this IEnumerable<ArtifactSummary> summaries, IModelInfo modelInfo)
        {
            // Check for duplicate resource uris
            var duplicates =
                from s in summaries
                let uri = s.ResourceUri
                where uri != null
                group s by uri into g
                where g.Count() > 1 // g.Skip(1).Any()
                select g;

            var conflicts = ToConflicts(duplicates);
            return ResolvingConflictException.ResourceUriConflict(conflicts);
        }

        private static readonly Func<IEnumerable<ArtifactSummary>, IModelInfo, Exception> CanonicalUrlConflictExceptionFactory
            = new Func<IEnumerable<ArtifactSummary>, IModelInfo, Exception>(ToCanonicalUrlConflictException);

        private static ResolvingConflictException ToCanonicalUrlConflictException(this IEnumerable<ArtifactSummary> summaries, IModelInfo modelInfo)
        {
            // Check for duplicate canonical urls
            var duplicates =
                from cr in summaries.ConformanceResources(modelInfo)
                let canonical = cr.GetConformanceCanonicalUrl()
                where canonical != null
                group cr by canonical into g
                where g.Count() > 1 // g.Skip(1).Any()
                select g;

            var conflicts = ToConflicts(duplicates);
            return ResolvingConflictException.CanonicalUrlConflict(conflicts);
        }

        private static readonly Func<IEnumerable<ArtifactSummary>, IModelInfo, Exception> NamingSystemUrlConflictExceptionFactory
            = new Func<IEnumerable<ArtifactSummary>, IModelInfo, Exception>(ToNamingSystemUrlConflictException);

        private static ResolvingConflictException ToNamingSystemUrlConflictException(this IEnumerable<ArtifactSummary> summaries, IModelInfo modelInfo)
        {
            // Check for duplicate NamingSystem.uniqueId values
            var duplicates =
                from ns in summaries.OfResourceType(FhirTypeNames.NAMINGSYSTEM_NAME)
                from ids in ns.GetNamingSystemUniqueId() ?? Enumerable.Empty<string>()
                where ids != null
                group ns by ids into g
                where g.Count() > 1 // g.Skip(1).Any()
                select g;

            var conflicts = ToConflicts(duplicates);
            return ResolvingConflictException.NamingSystemUniqueIdConflict(conflicts);
        }

        private static readonly Func<IEnumerable<ArtifactSummary>, IModelInfo, Exception> CodeSystemConflictExceptionFactory
                 = new Func<IEnumerable<ArtifactSummary>, IModelInfo, Exception>(ToCodeSystemConflictException);

        private static ResolvingConflictException ToCodeSystemConflictException(this IEnumerable<ArtifactSummary> summaries, IModelInfo modelInfo)
        {
            // Check for duplicate CodeSystem.valueSet values
            var duplicates =
                from cs in summaries.OfResourceType(FhirTypeNames.CODESYSTEM_NAME)
                let system = cs.GetCodeSystemValueSet()
                where system != null
                group cs by system into g
                where g.Count() > 1 // g.Skip(1).Any()
                select g;

            var conflicts = ToConflicts(duplicates);
            return ResolvingConflictException.CodeSystemConflict(conflicts);
        }

        private static readonly Func<IEnumerable<ArtifactSummary>, IModelInfo, Exception> ConceptMapUrlConflictExceptionFactory
            = new Func<IEnumerable<ArtifactSummary>, IModelInfo, Exception>(ToConceptMapUrlConflictException);

        private static ResolvingConflictException ToConceptMapUrlConflictException(this IEnumerable<ArtifactSummary> summaries, IModelInfo modelInfo)
        {
            // Check for duplicate concept map source/target urls
            var duplicates =
                from cm in summaries.OfResourceType(FhirTypeNames.CONCEPTMAP_NAME)
                from url in GetConceptMapUrls(cm)
                where url != null
                group cm by url into g
                where g.Count() > 1 // g.Skip(1).Any()
                select g;

            var conflicts = ToConflicts(duplicates);
            return ResolvingConflictException.ConceptMapUrlConflict(conflicts);
        }

        private static IEnumerable<string> GetConceptMapUrls(ArtifactSummary conceptMapSummary)
        {
            var source = conceptMapSummary.GetConceptMapSource();
            if (source != null) { yield return source; }
            var target = conceptMapSummary.GetConceptMapTarget();
            if (target != null) { yield return target; }
        }

        private static IEnumerable<ResolvingConflictException.ResolvingConflict> ToConflicts(IEnumerable<IGrouping<string, ArtifactSummary>> duplicates)
            => duplicates.Select(d => new ResolvingConflictException.ResolvingConflict(d.Key, d.Select(ci => ci.Origin)));

        // Based on System.Enumerable.SingleOrDefault, but with custom exception message
        private static TSource? SingleOrDefault<TSource>(this IEnumerable<TSource> source, IModelInfo modelInfo, Func<IEnumerable<TSource>, IModelInfo, Exception> createException)
        {
            if (source == null) { throw Error.ArgumentNull("source"); }
            if (createException == null) { throw Error.ArgumentNull("createException"); }

            if (source is IList<TSource> list)
            {
                switch (list.Count)
                {
                    case 0:
                        return default;
                    case 1:
                        return list[0];
                }
            }
            else
            {
                using (IEnumerator<TSource> enumerator = source.GetEnumerator())
                {
                    if (!enumerator.MoveNext())
                    {
                        return default;
                    }
                    TSource current = enumerator.Current;
                    if (!enumerator.MoveNext())
                    {
                        return current;
                    }
                }
            }
            //throw Error.MoreThanOneElement();
            throw createException(source, modelInfo);
        }

        #endregion
    }
}
#nullable restore