/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
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

namespace Hl7.Fhir.Model
{
    public static class BundleExtensions
    {
        /// <summary>
        /// Find all entries in a Bundle with the given ResourceIdentity.
        /// </summary>
        /// <param name="bundle">Bundle to search in</param>
        /// <param name="identity">The identity to find</param>
        /// <param name="includeDeleted">Whether to include deleted entries in the search. Optional.</param>
        /// <returns>A list of Resources with the given identity, or an empty list if none were found.</returns>
        public static IEnumerable<Bundle.BundleEntryComponent> FindEntry(this Bundle bundle, ResourceIdentity identity, bool includeDeleted = false)
        {
            if (identity == null) throw Error.ArgumentNull("reference");
            if (bundle.Entry == null) return Enumerable.Empty<Bundle.BundleEntryComponent>();
            
            return bundle.Entry.Where(be => be.GetResourceLocation(bundle.Base).IsTargetOf(identity) && (includeDeleted == true || !be.IsDeleted()));
        }

        /// <summary>
        /// Identifies if this entry is a deleted transaction (entry.Transaction.Method == DELETE)
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public static bool IsDeleted(this Bundle.BundleEntryComponent entry)
        {
            if (entry.Transaction != null)
            {
                return entry.Transaction.Method == Bundle.HTTPVerb.DELETE;
            }

            return false;
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
        public static IEnumerable<Bundle.BundleEntryComponent> FindEntry(this Bundle bundle, string type, string id, string version = null, bool includeDeleted = false)
        {
            if (type == null) throw Error.ArgumentNull("resource");
            if (id == null) throw Error.ArgumentNull("id");
            var identity = ResourceIdentity.Build(type,id,version);

            return FindEntry(bundle, identity, includeDeleted);
        }


        /// <summary>
        /// Find all entries in a Bundle with the given uri
        /// </summary>
        /// <param name="bundle">Bundle to search in</param>
        /// <param name="reference">The uri to find</param>
        /// <param name="includeDeleted">Whether to include deleted entries in the search. Optional.</param>
        /// <returns>A list of Resources with the given uri, or an empty list if none were found.</returns>
        public static IEnumerable<Bundle.BundleEntryComponent> FindEntry(this Bundle bundle, Uri reference, bool includeDeleted = false)
        {
            if (reference == null) throw Error.ArgumentNull("reference");
            var identity = new ResourceIdentity(reference);

            return FindEntry(bundle, identity, includeDeleted);
        }


        /// <summary>
        /// Find all Resources in a Bundle referred to by the given ResourceReference.
        /// </summary>
        /// <param name="bundle">Bundle to search in</param>
        /// <param name="reference">Reference to the entries you want to find</param>
        /// <param name="includeDeleted">Whether to include deleted entries in the search. Optional.</param>
        /// <returns>A list of Resources with the given reference, or an empty list if none were found.</returns>
        public static IEnumerable<Bundle.BundleEntryComponent> FindEntry(this Bundle bundle, ResourceReference reference, bool includeDeleted = false)
        {
            if (reference == null) throw Error.ArgumentNull("reference");
            var identity = new ResourceIdentity(reference.Reference);

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
        public static IEnumerable<T> ByResourceType<T>(this IEnumerable<Bundle.BundleEntryComponent> bes) where T : Resource, new()
        {
            var t = from be in bes.Where(be => be.Resource is T && be.Resource != null)
                    select be.Resource as T;
            return t.Cast<T>();
        }


        public static ResourceIdentity GetResourceLocation(this Bundle.BundleEntryComponent entry, string baseUrl = null)
        {
            if (entry.Resource == null) return null;

            var url = entry.Base ?? baseUrl;
            return entry.Resource.ResourceIdentity(url);
        }
    }

}
