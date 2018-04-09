/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Model
{
    public static class BundleExtensions
    {
        public static Bundle.EntryComponent AddResourceEntry(this Bundle b, Resource r, string fullUrl)
        {
            var newEntry = new Bundle.EntryComponent() { Resource = r, FullUrl = fullUrl };
            b.Entry.Add(newEntry);

            return newEntry;
        }

        public static Bundle.EntryComponent AddSearchEntry(this Bundle b, Resource r, string fullUrl, Bundle.SearchEntryMode searchEntryMode)
        {
            var newEntry = new Bundle.EntryComponent
            {
                Resource = r,
                FullUrl = fullUrl,
                Search = new Bundle.SearchComponent { Mode = searchEntryMode }
            };
            b.Entry.Add(newEntry);

            return newEntry;
        }

        public static Bundle.EntryComponent AddSearchEntry(this Bundle b, Resource r, string fullUrl, Bundle.SearchEntryMode searchEntryMode, decimal searchScore)
        {
            var newEntry = new Bundle.EntryComponent
            {
                Resource = r,
                FullUrl = fullUrl,
                Search = new Bundle.SearchComponent { Mode = searchEntryMode, Score = searchScore }
            };
            b.Entry.Add(newEntry);

            return newEntry;
        }


        /// <summary>
        /// Identifies if this entry is a deleted transaction (entry.Transaction.Method == DELETE)
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public static bool IsDeleted(this Bundle.EntryComponent entry)
        {
            if (entry.Request != null)
            {
                return entry.Request.Method == Bundle.HTTPVerb.DELETE;
            }

            return false;
        }


        public static bool HasResource(this Bundle.EntryComponent entry)
        {
            return entry.Resource != null;
        }        

        public static IEnumerable<Resource> GetResources(this Bundle bundle)
        {
            return bundle.Entry.Where(e => e.HasResource()).Select(e => e.Resource);
        }
        

        /// <summary>
        /// Find all entries in a Bundle that match the given reference.
        /// </summary>
        /// <param name="bundle">Bundle to search in</param>
        /// <param name="reference">An absolute reference to match against the fullUrl of the entries in the bundle</param>
        /// <param name="includeDeleted">Whether to include deleted entries in the search. Optional.</param>
        /// <returns>A list of Resources with the given identity, or an empty list if none were found.</returns>
        public static IEnumerable<Bundle.EntryComponent> FindEntry(this Bundle bundle, ResourceReference reference, bool includeDeleted = false)
        {
            return FindEntry(bundle, reference.Reference, includeDeleted);
        }


        public static bool IsTargetOf(this Bundle.EntryComponent entry, string reference)
        {
            // From the spec: If the reference is version specific (either relative or absolute), then remove the version from the URL 
            // before matching fullUrl, and then match the version based on Resource.meta.versionId.

            if (reference == null) throw Error.ArgumentNull(nameof(reference));
            if (!new Uri(reference, UriKind.RelativeOrAbsolute).IsAbsoluteUri) throw Error.Argument(nameof(reference), "uri should be absolute");

            string referencedVersion = ResourceIdentity.IsRestResourceIdentity(reference) ? (new ResourceIdentity(reference).VersionId) : null;
            reference = referencedVersion != null ? (new ResourceIdentity(reference).WithoutVersion().ToString()) : reference;
            var refRestUrl = new RestUrl(reference);

            return refRestUrl.IsSameUrl(new RestUrl(entry.FullUrl)) &&
                (referencedVersion == null || (entry.HasResource() && entry.Resource.VersionId == referencedVersion));
        }

        /// <summary>
        /// Find all entries in a Bundle that match the given reference.
        /// </summary>
        /// <param name="bundle">Bundle to search in</param>
        /// <param name="reference">An absolute reference to match against the fullUrl of the entries in the bundle</param>
        /// <param name="includeDeleted">Whether to include deleted entries in the search. Optional.</param>
        /// <returns>A list of Resources with the given identity, or an empty list if none were found.</returns>
        public static IEnumerable<Bundle.EntryComponent> FindEntry(this Bundle bundle, string reference, bool includeDeleted = false)
        {
            if (reference == null) throw Error.ArgumentNull(nameof(reference));
            if (bundle.Entry == null) return Enumerable.Empty<Bundle.EntryComponent>();
            if (!new Uri(reference, UriKind.RelativeOrAbsolute).IsAbsoluteUri) throw Error.Argument(nameof(reference), "uri should be absolute");

            string referencedVersion = ResourceIdentity.IsRestResourceIdentity(reference) ? (new ResourceIdentity(reference).VersionId) : null;
            reference = referencedVersion != null ? (new ResourceIdentity(reference).WithoutVersion().ToString()) : reference;
            var refRestUrl = new RestUrl(reference);
            var result = bundle.Entry.Where(be => new RestUrl(be.FullUrl).IsSameUrl(refRestUrl) && (includeDeleted == true || !be.IsDeleted()));

            if (referencedVersion != null)
                result = result.Where(be => be.HasResource() && be.Resource.Meta.VersionId == referencedVersion);

            return result;
        }

        /// <summary>
        /// Find all entries in a Bundle that match the given reference.
        /// </summary>
        /// <param name="bundle">Bundle to search in</param>
        /// <param name="reference">The identity to find</param>
        /// <param name="includeDeleted">Whether to include deleted entries in the search. Optional.</param>
        /// <returns>A list of Resources with the given identity, or an empty list if none were found.</returns>
        public static IEnumerable<Bundle.EntryComponent> FindEntry(this Bundle bundle, Uri reference, bool includeDeleted = false)
        {
            return FindEntry(bundle, reference.OriginalString, includeDeleted);
        }

        /// <summary>
        /// Find all entries in a Bundle with the given type/id/version
        /// </summary>
        /// <param name="bundle">Bundle to search in</param>
        /// <param name="type">Type of entry to find</param>
        /// <param name="id">Id of the entry to find</param>
        /// <param name="version">Version of the entry to find. Optional.</param>
        /// <param name="includeDeleted">Whether to include deleted entries in the search. Optional.</param>
        /// <returns>A list of Resources with the given identity, or an empty list if none were found.</returns>
        [Obsolete("Bundle Entries are now identified by their fullUrl, so cannot be referenced anymore by just the type and id. Use the other overloads instead")]
        public static IEnumerable<Bundle.EntryComponent> FindEntry(this Bundle bundle, string type, string id, string version = null, bool includeDeleted = false)
        {
            if (type == null) throw Error.ArgumentNull(nameof(type));
            if (id == null) throw Error.ArgumentNull(nameof(id));
            var identity = ResourceIdentity.Build(type,id,version);

            return FindEntry(bundle, identity, includeDeleted);
        }


        /// <summary>
        /// Filter all BundleEntries that have a given tag.
        /// </summary>
        /// <param name="entries">List of bundle entries to filter on</param>
        /// <param name="tag">Tag to filter Resources on</param>
        /// <returns>A list of BundleEntries having the given tag, or an empty list if none were found.</returns>
        public static IEnumerable<Resource> ByTag(this IEnumerable<Resource> entries, Coding tag)
        {
            return entries.Where(be => be.Meta != null && be.Meta.Tag != null && be.Meta.Tag.Contains(tag));
        }


        /// <summary>
        /// Filter all ResourceEntries that have a given tag.
        /// </summary>
        /// <typeparam name="T">Type of Resource to filter</typeparam>
        /// <param name="entries">List of resources to filter on</param>
        /// <param name="tag">Tag to filter Resources on</param>
        /// <returns>A list of typed ResourceEntries having the given tag, or an empty list if none were found.</returns>
        public static IEnumerable<T> ByTag<T>(this IEnumerable<Resource> entries, Coding tag) where T : Resource, new()
        {
            return entries.Where(be => be.Meta != null && be.Meta.Tag != null && be.Meta.Tag.Contains(tag) && be is T).Cast<T>();
        }

        /// <summary>
        /// Filter ResourceEntries containing a specific Resource type. No DeletedEntries are returned.
        /// </summary>
        /// <typeparam name="T">Type of Resource to filter</typeparam>
        /// <returns>All ResourceEntries containing the given type of resource, or an empty list if none were found.</returns>
        // Note: works for IEnumerable<ResourceEntry> too
        public static IEnumerable<T> ByResourceType<T>(this IEnumerable<Bundle.EntryComponent> bes) where T : Resource, new()
        {
            var t = from be in bes.Where(be => be.Resource is T && be.Resource != null)
                    select be.Resource as T;
            return t.Cast<T>();
        }

        public static ResourceIdentity GetResourceLocation(this Bundle.EntryComponent entry, string baseUrl = null)
        {
            return entry.FullUrl != null ?  new ResourceIdentity(entry.FullUrl) : null;
        }
    }

}
