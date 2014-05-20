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

namespace Hl7.Fhir.Model
{
    //TODO: Add functionality to resolve references between resources in the Bundle (see references.html)
    //TODO: Add functionality to make relative references absolute and vice versa using fhir-base url
    public static class BundleExtensions
    {
        public static IEnumerable<BundleEntry> FindEntryByReference(this Bundle bundle, Uri reference)
        {
            var id = reference;

            if (!id.IsAbsoluteUri)
            {
                if (bundle.Links.Base == null)
                    throw Error.Argument("reference", "Reference is a relative uri, so it needs a fhir-base link to be able to find entries by id or selflink");

                id = new Uri(bundle.Links.Base, id);
            }


            var byId = bundle.Entries.ById(id);
            var bySelf = bundle.Entries.BySelfLink(id);

            if(bySelf != null)
                return byId.Concat(new List<BundleEntry> { bySelf });
            else
                return byId;
        }

        /// <summary>
        /// Filter ResourceEntries containing a specific Resource type. No DeletedEntries are returned.
        /// </summary>
        /// <typeparam name="T">Type of Resource to filter</typeparam>
        /// <returns>All ResourceEntries containing the given type of resource, or an empty list if none were found.</returns>
        // Note: works for IEnumerable<ResourceEntry> too
        public static IEnumerable<ResourceEntry<T>> ByResourceType<T>(this IEnumerable<BundleEntry> bes) where T : Resource, new()
        {
            return bes.Where(be => be is ResourceEntry<T>).Cast<ResourceEntry<T>>();
        }

        /// <summary>
        /// Collect all ResourceEntries in the IEnumerable. No DeletedEntries are returned.
        /// </summary>
        /// <param name="bes"></param>
        public static IEnumerable<ResourceEntry> FilterResourceEntries(this IEnumerable<BundleEntry> bes)
        {
            return bes.Where(be => be is ResourceEntry).Cast<ResourceEntry>();
        }

        /// <summary>
        /// Collect all DeletedEntries in the IEnumerable. No ResourceEntries are returned.
        /// </summary>
        /// <param name="bes"></param>
        public static IEnumerable<DeletedEntry> FilterDeletedEntries(this IEnumerable<BundleEntry> bes)
        {
            return bes.Where(be => be is DeletedEntry).Cast<DeletedEntry>();
        }


        /// <summary>
        /// Filter all BundleEntries with the given id.
        /// </summary>
        /// <param name="id">Id of the Resource, as given in the BundleEntry's id</param>
        /// <param name="entries">List of bundle entries to filter on</param>
        /// <returns>A list of BundleEntries with the given id, or an empty list if none were found.</returns>
        public static IEnumerable<BundleEntry> ById(this IEnumerable<BundleEntry> entries, Uri id)
        {
            if (!id.IsAbsoluteUri) throw Error.Argument("id", "Id must be an absolute uri");

            return entries.Where(be => Uri.Equals(be.Id, id));
        }


        /// <summary>
        /// Filter all ResourceEntries with the given id.
        /// </summary>
        /// <typeparam name="T">Type of Resource to filter</typeparam>
        /// <param name="res">List of resources to filter on</param>
        /// <param name="id">Id of the Resource, as given in the ResourceEntry's id</param>
        /// <returns>A list of typed ResourceEntries with the given id, or an empty list if none were found.</returns>
        public static IEnumerable<ResourceEntry<T>> ById<T>(this IEnumerable<ResourceEntry<T>> res, Uri id) where T : Resource, new()
        {
            if (!id.IsAbsoluteUri) throw Error.Argument("id", "Id must be an absolute uri");

            return res.Where(re => Uri.Equals(re.Id, id));
        }


        /// <summary>
        /// Find the BundleEntry with a given self-link id.
        /// </summary>
        /// <param name="entries">List of bundle entries to filter on</param>
        /// <param name="self">Sel-link id of the Resource, as given in the BundleEntry's link with rel='self'.</param>
        /// <returns>A list of BundleEntries with the given self-link id, or an empty list if none were found.</returns>
        public static BundleEntry BySelfLink(this IEnumerable<BundleEntry> entries, Uri self)
        {
            if (!self.IsAbsoluteUri) throw Error.Argument("self", "Must be an absolute uri");

            return entries.FirstOrDefault(be => Uri.Equals(be.SelfLink, self));
        }


        /// <summary>
        /// Find the ResourceEntry with a given self-link id.
        /// </summary>
        /// <typeparam name="T">Type of Resource to find</typeparam>
        /// <param name="res">List of resources to filter on</param>
        /// <param name="self">Sel-link id of the Resource, as given in the BundleEntry's link with rel='self'.</param>
        /// <returns>A list of ResourceEntries with the given self-link id. Returns
        /// the empty list if none were found.</returns>
        public static ResourceEntry<T> BySelfLink<T>(this IEnumerable<ResourceEntry<T>> res, Uri self) where T : Resource, new()
        {
            if (!self.IsAbsoluteUri) throw Error.Argument("id", "Must be an absolute uri");

            return res.FirstOrDefault(re => Uri.Equals(re.SelfLink, self));
        }


        /// <summary>
        /// Filter all BundleEntries that have a given tag.
        /// </summary>
        /// <param name="entries">List of bundle entries to filter on</param>
        /// <param name="tag">Tag to filter Resources on</param>
        /// <returns>A list of BundleEntries having the given tag, or an empty list if none were found.</returns>
        public static IEnumerable<BundleEntry> ByTag(this IEnumerable<BundleEntry> entries, Tag tag)
        {
            return entries.Where(be => be.Tags != null && be.Tags.Contains(tag));
        }


        /// <summary>
        /// Filter all ResourceEntries that have a given tag.
        /// </summary>
        /// <typeparam name="T">Type of Resource to filter</typeparam>
        /// <param name="res">List of resources to filter on</param>
        /// <param name="tag">Tag to filter Resources on</param>
        /// <returns>A list of typed ResourceEntries having the given tag, or an empty list if none were found.</returns>
        public static IEnumerable<ResourceEntry<T>> ByTag<T>(this IEnumerable<ResourceEntry<T>> res, Tag tag) where T : Resource, new()
        {
            return res.Where(re => re.Tags != null && re.Tags.Contains(tag));
        }
    }

}
