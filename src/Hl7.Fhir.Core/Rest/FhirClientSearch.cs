/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Hl7.Fhir.Model;
using System.Net;
using System.Threading.Tasks;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Rest
{
    public partial class FhirClient
    {
        #region Search Execution

        /// <summary>
        /// Search for Resources based on criteria specified in a Query resource
        /// </summary>
        /// <param name="q">The Query resource containing the search parameters</param>
        /// <param name="resourceType">The type of resource to filter on (optional). If not specified, will search on all resource types.</param>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        public Task<Bundle> SearchAsync(SearchParams q, string resourceType = null)
        {
            var tx = new TransactionBuilder(Endpoint).Search(q,resourceType).ToBundle();
            return executeAsync<Bundle>(tx, HttpStatusCode.OK);
        }
        /// <summary>
        /// Search for Resources based on criteria specified in a Query resource
        /// </summary>
        /// <param name="q">The Query resource containing the search parameters</param>
        /// <param name="resourceType">The type of resource to filter on (optional). If not specified, will search on all resource types.</param>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        public Bundle Search(SearchParams q, string resourceType = null)
        {
            return SearchAsync(q, resourceType).WaitResult();
        }

        /// <summary>
        /// Search for Resources based on criteria specified in a Query resource
        /// </summary>
        /// <param name="q">The Query resource containing the search parameters</param>
        /// <param name="resourceType">The type of resource to filter on (optional). If not specified, will search on all resource types.</param>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        public Task<Bundle> SearchUsingPostAsync(SearchParams q, string resourceType = null)
        {
            var tx = new TransactionBuilder(Endpoint).SearchUsingPost(q, resourceType).ToBundle();
            return executeAsync<Bundle>(tx, new[] { HttpStatusCode.OK });
        }

        /// <summary>
        /// Search for Resources based on criteria specified in a Query resource
        /// </summary>
        /// <param name="q">The Query resource containing the search parameters</param>
        /// <param name="resourceType">The type of resource to filter on (optional). If not specified, will search on all resource types.</param>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        public Bundle SearchUsingPost(SearchParams q, string resourceType = null)
        {
            return SearchUsingPostAsync(q, resourceType).WaitResult();
        }

        #endregion

        #region Search by Parameters

        /// <summary>
        /// Search for Resources based on criteria specified in a Query resource
        /// </summary>
        /// <param name="q">The Query resource containing the search parameters</param>
        /// <typeparam name="TResource">The type of resource to filter on</typeparam>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        public Task<Bundle> SearchAsync<TResource>(SearchParams q)
            where TResource : Resource
        {
            // [WMR 20160421] GetResourceNameForType is obsolete
            // return Search(q, ModelInfo.GetResourceNameForType(typeof(TResource)));
            return SearchAsync(q, ModelInfo.GetFhirTypeNameForType(typeof(TResource)));
        }
        /// <summary>
        /// Search for Resources based on criteria specified in a Query resource
        /// </summary>
        /// <param name="q">The Query resource containing the search parameters</param>
        /// <typeparam name="TResource">The type of resource to filter on</typeparam>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        public Bundle Search<TResource>(SearchParams q) where TResource : Resource
        {
            return SearchAsync<TResource>(q).WaitResult();
        }

        public Task<Bundle> SearchUsingPostAsync<TResource>(SearchParams q) where TResource : Resource
        {
            return SearchUsingPostAsync(q, ModelInfo.GetFhirTypeNameForType(typeof(TResource)));
        }

        public Bundle SearchUsingPost<TResource>(SearchParams q) where TResource : Resource
        {
            return SearchUsingPostAsync(q, ModelInfo.GetFhirTypeNameForType(typeof(TResource))).WaitResult();
        }

        #endregion

        #region Generic Criteria Search

        /// <summary>
        /// Search for Resources of a certain type that match the given criteria
        /// </summary>
        /// <param name="criteria">Optional. The search parameters to filter the resources on. Each
        /// given string is a combined key/value pair (separated by '=')</param>
        /// <param name="includes">Optional. A list of include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="summary">Optional. Whether to include only return a summary of the resources in the Bundle</param>
        /// <param name="revIncludes">Optional. A list of reverse include paths</param>
        /// <typeparam name="TResource">The type of resource to list</typeparam>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        /// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
        /// of all resources of the given Resource type</remarks>
        public Task<Bundle> SearchAsync<TResource>(string[] criteria = null, string[] includes = null, int? pageSize = null, 
            SummaryType? summary = null, string[] revIncludes = null)
            where TResource : Resource, new()
        {
            // [WMR 20160421] GetResourceNameForType is obsolete
            // return Search(ModelInfo.GetResourceNameForType(typeof(TResource)), criteria, includes, pageSize, summary);
            return SearchAsync(ModelInfo.GetFhirTypeNameForType(typeof(TResource)), criteria, includes, pageSize, summary, revIncludes);
        }
        /// <summary>
        /// Search for Resources of a certain type that match the given criteria
        /// </summary>
        /// <param name="criteria">Optional. The search parameters to filter the resources on. Each
        /// given string is a combined key/value pair (separated by '=')</param>
        /// <param name="includes">Optional. A list of include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="summary">Optional. Whether to include only return a summary of the resources in the Bundle</param>
        /// <param name="revIncludes">Optional. A list of reverse include paths</param>
        /// <typeparam name="TResource">The type of resource to list</typeparam>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        /// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
        /// of all resources of the given Resource type</remarks>
        public Bundle Search<TResource>(string[] criteria = null, string[] includes = null, int? pageSize = null, 
            SummaryType? summary = null, string[] revIncludes = null)
            where TResource : Resource, new()
        {
            return SearchAsync<TResource>(criteria,includes,pageSize,summary, revIncludes).WaitResult();
        }

        /// <summary>
        /// Search for Resources of a certain type that match the given criteria
        /// </summary>
        /// <param name="criteria">Optional. The search parameters to filter the resources on. Each
        /// given string is a combined key/value pair (separated by '=')</param>
        /// <param name="includes">Optional. A list of include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="summary">Optional. Whether to include only return a summary of the resources in the Bundle</param>
        /// <param name="revIncludes">Optional. A list of reverse include paths</param>
        /// <typeparam name="TResource">The type of resource to list</typeparam>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        /// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
        /// of all resources of the given Resource type</remarks>
        public Task<Bundle> SearchUsingPostAsync<TResource>(string[] criteria = null, string[] includes = null, int? pageSize = null,
            SummaryType? summary = null, string[] revIncludes = null)
            where TResource : Resource, new()
        {
            return SearchUsingPostAsync(ModelInfo.GetFhirTypeNameForType(typeof(TResource)), criteria, includes, pageSize, summary, revIncludes);
        }
        /// <summary>
        /// Search for Resources of a certain type that match the given criteria
        /// </summary>
        /// <param name="criteria">Optional. The search parameters to filter the resources on. Each
        /// given string is a combined key/value pair (separated by '=')</param>
        /// <param name="includes">Optional. A list of include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="summary">Optional. Whether to include only return a summary of the resources in the Bundle</param>
        /// <param name="revIncludes">Optional. A list of reverse include paths</param>
        /// <typeparam name="TResource">The type of resource to list</typeparam>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        /// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
        /// of all resources of the given Resource type</remarks>
        public Bundle SearchUsingPost<TResource>(string[] criteria = null, string[] includes = null, int? pageSize = null,
            SummaryType? summary = null, string[] revIncludes = null)
            where TResource : Resource, new()
        {
            return SearchUsingPostAsync<TResource>(criteria, includes, pageSize, summary, revIncludes).WaitResult();
        }

        #endregion

        #region Non-Generic Criteria Search

        /// <summary>
        /// Search for Resources of a certain type that match the given criteria
        /// </summary>
        /// <param name="resource">The type of resource to search for</param>
        /// <param name="criteria">Optional. The search parameters to filter the resources on. Each
        /// given string is a combined key/value pair (separated by '=')</param>
        /// <param name="includes">Optional. A list of include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="summary">Optional. Whether to include only return a summary of the resources in the Bundle</param>
        /// <param name="revIncludes">Optional. A list of reverse include paths</param>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        /// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
        /// of all resources of the given Resource type</remarks>
        public Task<Bundle> SearchAsync(string resource, string[] criteria = null, string[] includes = null, int? pageSize = null, 
                SummaryType? summary = null, string[] revIncludes = null)
        {
            if (resource == null) throw Error.ArgumentNull(nameof(resource));

            return SearchAsync(toQuery(criteria, includes, pageSize, summary, revIncludes), resource);
        }
        /// <summary>
        /// Search for Resources of a certain type that match the given criteria
        /// </summary>
        /// <param name="resource">The type of resource to search for</param>
        /// <param name="criteria">Optional. The search parameters to filter the resources on. Each
        /// given string is a combined key/value pair (separated by '=')</param>
        /// <param name="includes">Optional. A list of include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="summary">Optional. Whether to include only return a summary of the resources in the Bundle</param>
        /// <param name="revIncludes">Optional. A list of reverse include paths</param>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        /// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
        /// of all resources of the given Resource type</remarks>
        public Bundle Search(string resource, string[] criteria = null, string[] includes = null, int? pageSize = null, 
            SummaryType? summary = null, string[] revIncludes = null)
        {
            return SearchAsync(resource, criteria, includes, pageSize, summary, revIncludes).WaitResult();
        }

        /// <summary>
        /// Search for Resources of a certain type that match the given criteria
        /// </summary>
        /// <param name="resource">The type of resource to search for</param>
        /// <param name="criteria">Optional. The search parameters to filter the resources on. Each
        /// given string is a combined key/value pair (separated by '=')</param>
        /// <param name="includes">Optional. A list of include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="summary">Optional. Whether to include only return a summary of the resources in the Bundle</param>
        /// <param name="revIncludes">Optional. A list of reverse include paths</param>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        /// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
        /// of all resources of the given Resource type</remarks>
        public Task<Bundle> SearchUsingPostAsync(string resource, string[] criteria = null, string[] includes = null, int? pageSize = null,
                SummaryType? summary = null, string[] revIncludes = null)
        {
            if (resource == null) throw Error.ArgumentNull(nameof(resource));

            return SearchUsingPostAsync(toQuery(criteria, includes, pageSize, summary, revIncludes), resource);
        }
        /// <summary>
        /// Search for Resources of a certain type that match the given criteria
        /// </summary>
        /// <param name="resource">The type of resource to search for</param>
        /// <param name="criteria">Optional. The search parameters to filter the resources on. Each
        /// given string is a combined key/value pair (separated by '=')</param>
        /// <param name="includes">Optional. A list of include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="summary">Optional. Whether to include only return a summary of the resources in the Bundle</param>
        /// <param name="revIncludes">Optional. A list of reverse include paths</param>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        /// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
        /// of all resources of the given Resource type</remarks>
        public Bundle SearchUsingPost(string resource, string[] criteria = null, string[] includes = null, int? pageSize = null,
            SummaryType? summary = null, string[] revIncludes = null)
        {
            return SearchUsingPostAsync(resource, criteria, includes, pageSize, summary, revIncludes).WaitResult();
        }

        #endregion

        #region Whole system search

        /// <summary>
        /// Search for Resources across the whole server that match the given criteria
        /// </summary>
        /// <param name="criteria">Optional. The search parameters to filter the resources on. Each
        /// given string is a combined key/value pair (separated by '=')</param>
        /// <param name="includes">Optional. A list of include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="summary">Optional. Whether to include only return a summary of the resources in the Bundle</param>
        /// <param name="revIncludes">Optional. A list of reverse include paths</param>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        /// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
        /// of all resources of the given Resource type</remarks>
        public Task<Bundle> WholeSystemSearchAsync(string[] criteria = null, string[] includes = null, int? pageSize = null, 
            SummaryType? summary = null, string[] revIncludes = null)
        {
            return SearchAsync(toQuery(criteria, includes, pageSize, summary, revIncludes));
        }

        /// <summary>
        /// Search for Resources across the whole server that match the given criteria
        /// </summary>
        /// <param name="criteria">Optional. The search parameters to filter the resources on. Each
        /// given string is a combined key/value pair (separated by '=')</param>
        /// <param name="includes">Optional. A list of include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="summary">Optional. Whether to include only return a summary of the resources in the Bundle</param>
        /// <param name="revIncludes">Optional. A list of reverse include paths</param>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        /// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
        /// of all resources of the given Resource type</remarks>
        public Bundle WholeSystemSearch(string[] criteria = null, string[] includes = null, int? pageSize = null, 
            SummaryType? summary = null, string[] revIncludes = null)
        {
            return WholeSystemSearchAsync(criteria, includes, pageSize, summary, revIncludes).WaitResult();
        }

        /// <summary>
        /// Search for Resources across the whole server that match the given criteria
        /// </summary>
        /// <param name="criteria">Optional. The search parameters to filter the resources on. Each
        /// given string is a combined key/value pair (separated by '=')</param>
        /// <param name="includes">Optional. A list of include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="summary">Optional. Whether to include only return a summary of the resources in the Bundle</param>
        /// <param name="revIncludes">Optional. A list of reverse include paths</param>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        /// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
        /// of all resources of the given Resource type</remarks>
        public Task<Bundle> WholeSystemSearchUsingPostAsync(string[] criteria = null, string[] includes = null, int? pageSize = null,
            SummaryType? summary = null, string[] revIncludes = null)
        {
            return SearchUsingPostAsync(toQuery(criteria, includes, pageSize, summary, revIncludes));
        }

        /// <summary>
        /// Search for Resources across the whole server that match the given criteria
        /// </summary>
        /// <param name="criteria">Optional. The search parameters to filter the resources on. Each
        /// given string is a combined key/value pair (separated by '=')</param>
        /// <param name="includes">Optional. A list of include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="summary">Optional. Whether to include only return a summary of the resources in the Bundle</param>
        /// <param name="revIncludes">Optional. A list of reverse include paths</param>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        /// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
        /// of all resources of the given Resource type</remarks>
        public Bundle WholeSystemSearchUsingPost(string[] criteria = null, string[] includes = null, int? pageSize = null,
            SummaryType? summary = null, string[] revIncludes = null)
        {
            return WholeSystemSearchUsingPostAsync(criteria, includes, pageSize, summary, revIncludes).WaitResult();
        }

        #endregion

        #region Generic Search by ID

        /// <summary>
        /// Search for resources based on a resource's id.
        /// </summary>
        /// <param name="id">The id of the resource to search for</param>
        /// <param name="includes">Zero or more include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="revIncludes">Optional. A list of reverse include paths</param>
        /// <typeparam name="TResource">The type of resource to search for</typeparam>
        /// <returns>A Bundle with the BundleEntry as identified by the id parameter or an empty
        /// Bundle if the resource wasn't found.</returns>
        /// <remarks>This operation is similar to Read, but additionally,
        /// it is possible to specify include parameters to include resources in the bundle that the
        /// returned resource refers to.</remarks>
        public Task<Bundle> SearchByIdAsync<TResource>(string id, string[] includes = null, int? pageSize = null,
                string[] revIncludes = null) where TResource : Resource, new()
        {
            if (id == null) throw Error.ArgumentNull(nameof(id));

            return SearchByIdAsync(typeof(TResource).GetCollectionName(), id, includes, pageSize, revIncludes);
        }

        /// <summary>
        /// Search for resources based on a resource's id.
        /// </summary>
        /// <param name="id">The id of the resource to search for</param>
        /// <param name="includes">Zero or more include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="revIncludes">Optional. A list of reverse include paths</param>
        /// <typeparam name="TResource">The type of resource to search for</typeparam>
        /// <returns>A Bundle with the BundleEntry as identified by the id parameter or an empty
        /// Bundle if the resource wasn't found.</returns>
        /// <remarks>This operation is similar to Read, but additionally,
        /// it is possible to specify include parameters to include resources in the bundle that the
        /// returned resource refers to.</remarks>
        public Bundle SearchById<TResource>(string id, string[] includes = null, int? pageSize = null, string[] revIncludes = null) where TResource : Resource, new()
        {
            return SearchByIdAsync<TResource>(id, includes, pageSize, revIncludes).WaitResult();
        }

        /// <summary>
        /// Search for resources based on a resource's id.
        /// </summary>
        /// <param name="id">The id of the resource to search for</param>
        /// <param name="includes">Zero or more include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="revIncludes">Optional. A list of reverse include paths</param>
        /// <typeparam name="TResource">The type of resource to search for</typeparam>
        /// <returns>A Bundle with the BundleEntry as identified by the id parameter or an empty
        /// Bundle if the resource wasn't found.</returns>
        /// <remarks>This operation is similar to Read, but additionally,
        /// it is possible to specify include parameters to include resources in the bundle that the
        /// returned resource refers to.</remarks>
        public Task<Bundle> SearchByIdUsingPostAsync<TResource>(string id, string[] includes = null, int? pageSize = null,
                string[] revIncludes = null) where TResource : Resource, new()
        {
            if (id == null) throw Error.ArgumentNull(nameof(id));

            return SearchByIdUsingPostAsync(typeof(TResource).GetCollectionName(), id, includes, pageSize, revIncludes);
        }

        /// <summary>
        /// Search for resources based on a resource's id.
        /// </summary>
        /// <param name="id">The id of the resource to search for</param>
        /// <param name="includes">Zero or more include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="revIncludes">Optional. A list of reverse include paths</param>
        /// <typeparam name="TResource">The type of resource to search for</typeparam>
        /// <returns>A Bundle with the BundleEntry as identified by the id parameter or an empty
        /// Bundle if the resource wasn't found.</returns>
        /// <remarks>This operation is similar to Read, but additionally,
        /// it is possible to specify include parameters to include resources in the bundle that the
        /// returned resource refers to.</remarks>
        public Bundle SearchByIdUsingPost<TResource>(string id, string[] includes = null, int? pageSize = null, string[] revIncludes = null) where TResource : Resource, new()
        {
            return SearchByIdUsingPostAsync<TResource>(id, includes, pageSize, revIncludes).WaitResult();
        }

        #endregion

        #region Non-Generic Search by Id

        /// <summary>
        /// Search for resources based on a resource's id.
        /// </summary>
        /// <param name="resource">The type of resource to search for</param>
        /// <param name="id">The id of the resource to search for</param>
        /// <param name="includes">Zero or more include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="revIncludes">Optional. A list of reverse include paths</param>
        /// <returns>A Bundle with the BundleEntry as identified by the id parameter or an empty
        /// Bundle if the resource wasn't found.</returns>
        /// <remarks>This operation is similar to Read, but additionally,
        /// it is possible to specify include parameters to include resources in the bundle that the
        /// returned resource refers to.</remarks>
        public Task<Bundle> SearchByIdAsync(string resource, string id, string[] includes = null, int? pageSize = null, string[] revIncludes = null)
        {
            if (resource == null) throw Error.ArgumentNull(nameof(resource));
            if (id == null) throw Error.ArgumentNull(nameof(id));

            string criterium = "_id=" + id;
            return SearchAsync(toQuery(new string[] { criterium }, includes, pageSize, summary: null, revIncludes: revIncludes), resource);
        }
        /// <summary>
        /// Search for resources based on a resource's id.
        /// </summary>
        /// <param name="resource">The type of resource to search for</param>
        /// <param name="id">The id of the resource to search for</param>
        /// <param name="includes">Zero or more include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="revIncludes">Optional. A list of reverse include paths</param>
        /// <returns>A Bundle with the BundleEntry as identified by the id parameter or an empty
        /// Bundle if the resource wasn't found.</returns>
        /// <remarks>This operation is similar to Read, but additionally,
        /// it is possible to specify include parameters to include resources in the bundle that the
        /// returned resource refers to.</remarks>
        public Bundle SearchById(string resource, string id, string[] includes = null, int? pageSize = null, string[] revIncludes = null)
        {
            return SearchByIdAsync(resource, id, includes, pageSize, revIncludes).WaitResult();
        }

        /// <summary>
        /// Search for resources based on a resource's id.
        /// </summary>
        /// <param name="resource">The type of resource to search for</param>
        /// <param name="id">The id of the resource to search for</param>
        /// <param name="includes">Zero or more include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="revIncludes">Optional. A list of reverse include paths</param>
        /// <returns>A Bundle with the BundleEntry as identified by the id parameter or an empty
        /// Bundle if the resource wasn't found.</returns>
        /// <remarks>This operation is similar to Read, but additionally,
        /// it is possible to specify include parameters to include resources in the bundle that the
        /// returned resource refers to.</remarks>
        public Task<Bundle> SearchByIdUsingPostAsync(string resource, string id, string[] includes = null, int? pageSize = null, string[] revIncludes = null)
        {
            if (resource == null) throw Error.ArgumentNull(nameof(resource));
            if (id == null) throw Error.ArgumentNull(nameof(id));

            string criterium = "_id=" + id;
            return SearchUsingPostAsync(toQuery(new string[] { criterium }, includes, pageSize, summary: null, revIncludes: revIncludes), resource);
        }
        /// <summary>
        /// Search for resources based on a resource's id.
        /// </summary>
        /// <param name="resource">The type of resource to search for</param>
        /// <param name="id">The id of the resource to search for</param>
        /// <param name="includes">Zero or more include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="revIncludes">Optional. A list of reverse include paths</param>
        /// <returns>A Bundle with the BundleEntry as identified by the id parameter or an empty
        /// Bundle if the resource wasn't found.</returns>
        /// <remarks>This operation is similar to Read, but additionally,
        /// it is possible to specify include parameters to include resources in the bundle that the
        /// returned resource refers to.</remarks>
        public Bundle SearchByIdUsingPost(string resource, string id, string[] includes = null, int? pageSize = null, string[] revIncludes = null)
        {
            return SearchByIdUsingPostAsync(resource, id, includes, pageSize, revIncludes).WaitResult();
        }

        #endregion

        #region Continue

        /// <summary>
        /// Uses the FHIR paging mechanism to go navigate around a series of paged result Bundles
        /// </summary>
        /// <param name="current">The bundle as received from the last response</param>
        /// <param name="direction">Optional. Direction to browse to, default is the next page of results.</param>
        /// <returns>A bundle containing a new page of results based on the browse direction, or null if
        /// the server did not have more results in that direction.</returns>
        public Task<Bundle> ContinueAsync(Bundle current, PageDirection direction = PageDirection.Next)
        {
            if (current == null) throw Error.ArgumentNull(nameof(current));
            if (current.Link == null) return null;

            Uri continueAt = null;

            switch (direction)
            {
                case PageDirection.First:
                    continueAt = current.FirstLink; break;
                case PageDirection.Previous:
                    continueAt = current.PreviousLink; break;
                case PageDirection.Next:
                    continueAt = current.NextLink; break;
                case PageDirection.Last:
                    continueAt = current.LastLink; break;
            }

            if (continueAt != null)
            {
                var tx = new TransactionBuilder(Endpoint).Get(continueAt).ToBundle();
                return executeAsync<Bundle>(tx, HttpStatusCode.OK);
            }
            else
            {
                // Return a null bundle, can not return simply null because this is a task
                Bundle nullValue = null;
                return System.Threading.Tasks.Task.FromResult(nullValue);
            }
        }
        /// <summary>
        /// Uses the FHIR paging mechanism to go navigate around a series of paged result Bundles
        /// </summary>
        /// <param name="current">The bundle as received from the last response</param>
        /// <param name="direction">Optional. Direction to browse to, default is the next page of results.</param>
        /// <returns>A bundle containing a new page of results based on the browse direction, or null if
        /// the server did not have more results in that direction.</returns>
        public Bundle Continue(Bundle current, PageDirection direction = PageDirection.Next)
        {
            return ContinueAsync(current, direction).WaitResult();
        }

        #endregion

        #region Private Methods

        private SearchParams toQuery(string[] criteria, string[] includes, int? pageSize, SummaryType? summary, string[] revIncludes)
        {
            var q = new SearchParams()
            {
                Count = pageSize
            };

            if (includes != null)
                foreach (var inc in includes) q.Include.Add(inc);

            if (revIncludes != null)
                foreach (var revInc in revIncludes) q.RevInclude.Add(revInc);

            if (criteria != null)
            {
                foreach (var crit in criteria)
                {
                    var keyVal = crit.SplitLeft('=');
                    q.Add(keyVal.Item1, keyVal.Item2);
                }
            }

            if (summary != null)
                q.Summary = summary.Value;

            return q;
        }

        #endregion
    }

    public enum PageDirection
    {
        First,
        Previous,
        Next,
        Last
    }
}
