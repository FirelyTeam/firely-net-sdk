using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir
{
    public static class BundleExtensions
    {
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
        /// <returns>A list of BundleEntries with the given id, or an empty list if none were found.</returns>
        public static IEnumerable<BundleEntry> ById(this IEnumerable<BundleEntry> bes, Uri id)
        {
            return bes.Where(be => Uri.Equals(be.Id, id));
        }


        /// <summary>
        /// Filter all ResourceEntries with the given id.
        /// </summary>
        /// <typeparam name="T">Type of Resource to filter</typeparam>
        /// <param name="id">Id of the Resource, as given in the ResourceEntry's id</param>
        /// <returns>A list of typed ResourceEntries with the given id, or an empty list if none were found.</returns>
        public static IEnumerable<ResourceEntry<T>> ById<T>(this IEnumerable<ResourceEntry<T>> res, Uri id) where T : Resource, new()
        {
            return res.Where(re => Uri.Equals(re.Id, id));
        }


        /// <summary>
        /// Find the BundleEntry with a given self-link id.
        /// </summary>
        /// <param name="self">Sel-link id of the Resource, as given in the BundleEntry's link with rel='self'.</param>
        /// <returns>A list of BundleEntries with the given self-link id, or an empty list if none were found.</returns>
        public static BundleEntry BySelfLink(this IEnumerable<BundleEntry> bes, Uri self)
        {
            return bes.FirstOrDefault(be => Uri.Equals(be.SelfLink, self));
        }


        /// <summary>
        /// Find the ResourceEntry with a given self-link id.
        /// </summary>
        /// <typeparam name="T">Type of Resource to find</typeparam>
        /// <param name="self">Sel-link id of the Resource, as given in the BundleEntry's link with rel='self'.</param>
        /// <returns>A list of ResourceEntries with the given self-link id. Returns
        /// the empty list if none were found.</returns>
        public static ResourceEntry<T> BySelfLink<T>(this IEnumerable<ResourceEntry<T>> res, Uri self) where T : Resource, new()
        {
            return res.FirstOrDefault(re => Uri.Equals(re.SelfLink, self));
        }


        /// <summary>
        /// Filter all BundleEntries that have a given tag.
        /// </summary>
        /// <param name="tag">Tag to filter Resources on</param>
        /// <returns>A list of BundleEntries having the given tag, or an empty list if none were found.</returns>
        public static IEnumerable<BundleEntry> ByTag(this IEnumerable<BundleEntry> bes, Tag tag)
        {
            return bes.Where(be => be.Tags != null && be.Tags.Contains(tag));
        }


        /// <summary>
        /// Filter all ResourceEntries that have a given tag.
        /// </summary>
        /// <typeparam name="T">Type of Resource to filter</typeparam>
        /// <param name="tag">Tag to filter Resources on</param>
        /// <returns>A list of typed ResourceEntries having the given tag, or an empty list if none were found.</returns>
        public static IEnumerable<ResourceEntry<T>> ByTag<T>(this IEnumerable<ResourceEntry<T>> res, Tag tag) where T : Resource, new()
        {
            return res.Where(re => re.Tags != null && re.Tags.Contains(tag));
        }
    }

}
