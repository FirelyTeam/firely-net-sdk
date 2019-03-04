/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Extension methods for <see cref="IEnumerable{ArtifactSummary}"/> sequences.</summary>
    public static class ArtifactSummaryExtensions
    {
        /// <summary>Filter <see cref="ArtifactSummary"/> instances with errors.</summary>
        public static IEnumerable<ArtifactSummary> Errors(this IEnumerable<ArtifactSummary> summaries)
            => summaries.Where(s => s.IsFaulted);

        /// <summary>Filter <see cref="ArtifactSummary"/> instances for resources with the specified <see cref="ResourceType"/>.</summary>
        public static IEnumerable<ArtifactSummary> OfResourceType(this IEnumerable<ArtifactSummary> summaries, ResourceType resourceType)
            => summaries.Where(s => s.ResourceType == resourceType);

        /// <summary>
        /// Filter <see cref="ArtifactSummary"/> instances for resources with the specified <see cref="ResourceType"/>.
        /// If <paramref name="resourceType"/> equals <c>null</c>, then filter summaries for all valid FHIR resources.
        /// </summary>
        public static IEnumerable<ArtifactSummary> OfResourceType(this IEnumerable<ArtifactSummary> summaries, ResourceType? resourceType)
            => resourceType.HasValue
            ? summaries.Where(s => s.ResourceType == resourceType.Value)
            : summaries.Where(s => s.IsFhirResource);

        /// <summary>Filter <see cref="ArtifactSummary"/> instances for valid FHIR resources.</summary>
        public static IEnumerable<ArtifactSummary> FhirResources(this IEnumerable<ArtifactSummary> summaries)
            => summaries.Where(s => s.IsFhirResource);

        /// <summary>Find <see cref="ArtifactSummary"/> instances for <see cref="NamingSystem"/> resources with the specified uniqueId value.</summary>
        public static IEnumerable<ArtifactSummary> FindNamingSystems(this IEnumerable<ArtifactSummary> summaries, string uniqueId)
            => summaries.OfResourceType(ResourceType.NamingSystem).Where(ns => ns.HasNamingSystemUniqueId(uniqueId));

        /// <summary>Filter <see cref="ArtifactSummary"/> instances for conformance resources.</summary>
        public static IEnumerable<ArtifactSummary> ConformanceResources(this IEnumerable<ArtifactSummary> summaries)
            => summaries.Where(s => ModelInfo.IsConformanceResource(s.ResourceType));

        /// <summary>Find <see cref="ArtifactSummary"/> instances for conformance resources with the specified canonical url.</summary>
        public static IEnumerable<ArtifactSummary> FindConformanceResources(this IEnumerable<ArtifactSummary> summaries, string canonicalUrl)
        {
            var values = canonicalUrl.Split('|');
            if (values.Length > 2)
                throw Error.Argument("Url is not valid. The pipe occures more than once.");

            var version = values.Length == 2 ? values[1] : string.Empty;

            return summaries.ConformanceResources().Where(r => r.GetConformanceCanonicalUrl() == values[0] && 
                                                               (string.IsNullOrEmpty(version) || r.GetConformanceVersion() == version));
        }

        /// <summary>Find <see cref="ArtifactSummary"/> instances for <see cref="ValueSet"/> resources with the specified codeSystem system.</summary>
        public static IEnumerable<ArtifactSummary> FindValueSets(this IEnumerable<ArtifactSummary> summaries, string system)
            => summaries.OfResourceType(ResourceType.ValueSet).Where(r => r.GetValueSetSystem() == system);

        /// <summary>Find <see cref="ArtifactSummary"/> instances for <see cref="ConceptMap"/> resources with the specified source and/or target uri(s).</summary>
        public static IEnumerable<ArtifactSummary> FindConceptMaps(this IEnumerable<ArtifactSummary> summaries, string sourceUri = null, string targetUri = null)
        {
            IEnumerable<ArtifactSummary> result = summaries.OfResourceType(ResourceType.ConceptMap);
            if (sourceUri != null)
            {
                result = result.Where(cm => cm.GetConceptMapSource() == sourceUri);
            }
            if (targetUri != null)
            {
                result = result.Where(cm => cm.GetConceptMapTarget() == targetUri);
            }
            return result;
        }

        /// <summary>Find <see cref="ArtifactSummary"/> instance(s) for resources contained in the specified file.</summary>
        public static IEnumerable<ArtifactSummary> FromFile(this IEnumerable<ArtifactSummary> summaries, string filePath)
            => summaries.Where(s => StringComparer.OrdinalIgnoreCase.Equals(s.Origin, filePath));

        /// <summary>Resolve the <see cref="ArtifactSummary"/> for the resource with the specified uri.</summary>
        public static ArtifactSummary ResolveByUri(this IEnumerable<ArtifactSummary> summaries, string uri)
        {
            var matches = summaries.Where(r => r.ResourceUri == uri);
            return matches.SingleOrDefault(ResourceUriConflictExceptionFactory);
        }

        /// <summary>Resolve the <see cref="ArtifactSummary"/> for the comformance resource with the specified canonical uri.</summary>
        /// <exception cref="ResolvingConflictException">The <see cref="ISummarySource"/> instance encountered conflicting Conformance Resource artifacts with the same canonical url identifier.</exception>
        public static ArtifactSummary ResolveByCanonicalUri(this IEnumerable<ArtifactSummary> summaries, string canonicalUrl)
        {
            var matches = summaries.FindConformanceResources(canonicalUrl);
            return matches.SingleOrDefault(CanonicalUrlConflictExceptionFactory);
        }

        /// <summary>Resolve the <see cref="ArtifactSummary"/> for the NamingSystem resource with the specified uniqueId.</summary>
        public static ArtifactSummary ResolveNamingSystem(this IEnumerable<ArtifactSummary> summaries, string uniqueId)
            => summaries.FindNamingSystems(uniqueId).SingleOrDefault(NamingSystemUrlConflictExceptionFactory);

        /// <summary>Resolve the <see cref="ArtifactSummary"/> for the ValueSet resource with the specified codeSystem system.</summary>
        public static ArtifactSummary ResolveValueSet(this IEnumerable<ArtifactSummary> summaries, string system)
            => summaries.FindValueSets(system).SingleOrDefault(ValueSetSystemConflictExceptionFactory);

        /// <summary>Resolve the <see cref="ArtifactSummary"/> for the ConceptMap resource with the specified source and/or target uri(s).</summary>
        public static ArtifactSummary ResolveConceptMap(this IEnumerable<ArtifactSummary> summaries, string sourceUri = null, string targetUri = null)
            => summaries.FindConceptMaps(sourceUri, targetUri).SingleOrDefault(ConceptMapUrlConflictExceptionFactory);

        #region Private helpers

        static readonly Func<IEnumerable<ArtifactSummary>, Exception> ResourceUriConflictExceptionFactory
            = new Func<IEnumerable<ArtifactSummary>, Exception>(ToResourceUriConflictException);

        static ResolvingConflictException ToResourceUriConflictException(this IEnumerable<ArtifactSummary> summaries)
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

        static readonly Func<IEnumerable<ArtifactSummary>, Exception> CanonicalUrlConflictExceptionFactory
            = new Func<IEnumerable<ArtifactSummary>, Exception>(ToCanonicalUrlConflictException);

        static ResolvingConflictException ToCanonicalUrlConflictException(this IEnumerable<ArtifactSummary> summaries)
        {
            // Check for duplicate canonical urls
            var duplicates =
                from cr in summaries.ConformanceResources()
                let canonical = cr.GetConformanceCanonicalUrl()
                where canonical != null
                group cr by canonical into g
                where g.Count() > 1 // g.Skip(1).Any()
                select g;

            var conflicts = ToConflicts(duplicates);
            return ResolvingConflictException.CanonicalUrlConflict(conflicts);
        }

        static readonly Func<IEnumerable<ArtifactSummary>, Exception> NamingSystemUrlConflictExceptionFactory
            = new Func<IEnumerable<ArtifactSummary>, Exception>(ToNamingSystemUrlConflictException);

        static ResolvingConflictException ToNamingSystemUrlConflictException(this IEnumerable<ArtifactSummary> summaries)
        {
            // Check for duplicate NamingSystem.uniqueId values
            var duplicates =
                from ns in summaries.OfResourceType(ResourceType.NamingSystem)
                from ids in ns.GetNamingSystemUniqueId()
                where ids != null
                group ns by ids into g
                where g.Count() > 1 // g.Skip(1).Any()
                select g;

            var conflicts = ToConflicts(duplicates);
            return ResolvingConflictException.NamingSystemUniqueIdConflict(conflicts);
        }

        static readonly Func<IEnumerable<ArtifactSummary>, Exception> ValueSetSystemConflictExceptionFactory
            = new Func<IEnumerable<ArtifactSummary>, Exception>(ToValueSetSystemConflictException);

        static ResolvingConflictException ToValueSetSystemConflictException(this IEnumerable<ArtifactSummary> summaries)
        {
            // Check for duplicate ValueSet.system values
            var duplicates =
                from ns in summaries.OfResourceType(ResourceType.ValueSet)
                let system = ns.GetValueSetSystem()
                where system != null
                group ns by system into g
                where g.Count() > 1 // g.Skip(1).Any()
                select g;

            var conflicts = ToConflicts(duplicates);
            return ResolvingConflictException.ValueSetSystemConflict(conflicts);
        }

        static readonly Func<IEnumerable<ArtifactSummary>, Exception> ConceptMapUrlConflictExceptionFactory
            = new Func<IEnumerable<ArtifactSummary>, Exception>(ToConceptMapUrlConflictException);

        static ResolvingConflictException ToConceptMapUrlConflictException(this IEnumerable<ArtifactSummary> summaries)
        {
            // Check for duplicate concept map source/target urls
            var duplicates =
                from cm in summaries.OfResourceType(ResourceType.ConceptMap)
                from url in GetConceptMapUrls(cm)
                where url != null
                group cm by url into g
                where g.Count() > 1 // g.Skip(1).Any()
                select g;

            var conflicts = ToConflicts(duplicates);
            return ResolvingConflictException.ConceptMapUrlConflict(conflicts);
        }

        static IEnumerable<string> GetConceptMapUrls(ArtifactSummary conceptMapSummary)
        {
            var source = conceptMapSummary.GetConceptMapSource();
            if (source != null) { yield return source; }
            var target = conceptMapSummary.GetConceptMapTarget();
            if (target != null) { yield return target; }
        }

        static IEnumerable<ResolvingConflictException.ResolvingConflict> ToConflicts(IEnumerable<IGrouping<string, ArtifactSummary>> duplicates)
            => duplicates.Select(d => new ResolvingConflictException.ResolvingConflict(d.Key, d.Select(ci => ci.Origin)));

        // Based on System.Enumerable.SingleOrDefault, but with custom exception message
        static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source, Func<IEnumerable<TSource>, Exception> createException)
        {
            if (source == null) { throw Error.ArgumentNull("source"); }
            if (createException == null) { throw Error.ArgumentNull("createException"); }

            if (source is IList<TSource> list)
            {
                switch (list.Count)
                {
                    case 0:
                        return default(TSource);
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
                        return default(TSource);
                    }
                    TSource current = enumerator.Current;
                    if (!enumerator.MoveNext())
                    {
                        return current;
                    }
                }
            }
            //throw Error.MoreThanOneElement();
            throw createException(source);
        }

        #endregion
    }
}