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
    //TODO: Add functionality to resolve references between resources in the Bundle (see references.html)
    //TODO: Add functionality to make relative references absolute and vice versa using fhir-base url
    public static class BundleExtensions
    {
        /// <summary>
        /// Find all BundleEntries with the given id.
        /// </summary>
        /// <param name="bundle">Bundle to search in</param>B
        /// <param name="type">Type of resource to search for</param>
        /// <param name="id">The (deleted) resource's id to find</param>
        /// <param name="includeDeleted">Indicates whether the search should include deleted entries in the list</param>
        /// <returns>A list of BundleEntries with the given id, or an empty list if none were found.</returns>
        public static IEnumerable<Bundle.BundleEntryComponent> FindEntry(this Bundle bundle, string type, string id, bool includeDeleted = false)
        {
            if (id == null) throw Error.ArgumentNull("id");
            if (type == null) throw Error.ArgumentNull("resource");

            if (bundle.Entry == null) return Enumerable.Empty<Bundle.BundleEntryComponent>();

            var arg = ResourceIdentity.Build(type,id);

            return bundle.Entry.Where(be => (!includeDeleted || (includeDeleted && be.Deleted != null)) &&
                arg.refersTo(be.BuildUrlForEntry(bundle.Base)));
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


        internal static ResourceIdentity BuildUrlForEntry(this Bundle.BundleEntryComponent entry, string baseUrl = null)
        {
            var url = entry.Base ?? baseUrl;
            ResourceIdentity result;

            if (entry.Deleted != null)
                result = ResourceIdentity.Build(entry.Deleted.Type, entry.Deleted.ResourceId, entry.Deleted.VersionId);
            else
            {
                var versionId = entry.Resource.Meta != null && entry.Resource.Meta.VersionId != null ?
                    entry.Resource.Meta.VersionId : null;

                result = ResourceIdentity.Build(entry.Resource.TypeName, entry.Resource.Id, versionId);
            }

            if (url != null)
                return result.WithBase(url);
            else
                return result;
        }


        private static bool refersTo(this Uri me, Uri path)
        {
            if (me.IsAbsoluteUri && !path.IsAbsoluteUri) return false;  // Absolute path can never match a relative one
            if (!me.IsAbsoluteUri && path.IsAbsoluteUri) return me.IsWithin(path);  // IsBaseOf() is unusable

            // Either both absolute or both relative
            return me.ToString() == path.ToString();
        }

        /// <summary>
        /// Find all Resources in a Bundle with the given url.
        /// </summary>
        /// <param name="bundle">Bundle to search in</param>
        /// <param name="id">The resource's id to find</param>
        /// <returns>A list of Resources with the given id, or an empty list if none were found.</returns>
        public static IEnumerable<Bundle.BundleEntryComponent> FindEntryByUrl(this Bundle bundle, Uri reference, bool includeDeleted = false)
        {
            if (reference == null) throw Error.ArgumentNull("reference");
            if (bundle.Entry == null) return Enumerable.Empty<Bundle.BundleEntryComponent>();

            var url = reference.ToString();
            return bundle.Entry.Where(be => refersTo(reference, be.BuildUrlForEntry(bundle.Base)));
        }


        /// <summary>
        /// Find the BundleEntry with a given self-link id.
        /// </summary>
        /// <param name="entries">List of bundle entries to filter on</param>
        /// <param name="self">Sel-link id of the Resource, as given in the BundleEntry's link with rel='self'.</param>
        /// <returns>A list of BundleEntries with the given self-link id, or an empty list if none were found.</returns>
        public static Resource FindEntryByVersionUrl(this Bundle bundle, Uri version)
        {
            throw new NotImplementedException();
            // Cannot do this, unless we add a versionId to the Entry.Deleted element.
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
        /// <param name="res">List of resources to filter on</param>
        /// <param name="tag">Tag to filter Resources on</param>
        /// <returns>A list of typed ResourceEntries having the given tag, or an empty list if none were found.</returns>
        public static IEnumerable<T> ByTag<T>(this IEnumerable<Resource> entries, Coding tag) where T : Resource, new()
        {
            return entries.Where(be => be.Meta != null && be.Meta.Tag != null && be.Meta.Tag.Contains(tag) && be is T).Cast<T>();
        }
    }

}
