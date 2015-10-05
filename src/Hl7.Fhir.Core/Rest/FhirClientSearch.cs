/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Rest;
using System.Threading;
using Hl7.Fhir.Introspection;
using System.Threading.Tasks;


namespace Hl7.Fhir.Rest
{
    public partial class FhirClient
    {
        /// <summary>
        /// Search for Resources based on criteria specified in a Query resource
        /// </summary>
        /// <param name="q">The Query resource containing the search parameters</param>
        /// <param name="resourceType">The type of resource to filter on (optional). If not specified, will search on all resource types.</param>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        public Bundle Search(SearchParams q, string resourceType = null)
        {
            var tx = new TransactionBuilder(Endpoint).Search(q,resourceType).ToBundle();
            return execute<Bundle>(tx, HttpStatusCode.OK);
        }

        /// <summary>
        /// Search for Resources based on criteria specified in a Query resource
        /// </summary>
        /// <param name="q">The Query resource containing the search parameters</param>
        /// <typeparam name="TResource">The type of resource to filter on</typeparam>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        public Bundle Search<TResource>(SearchParams q)
            where TResource : Resource
        {
            return Search(q, ModelInfo.GetResourceNameForType(typeof(TResource)));
        }

        /// <summary>
        /// Search for Resources of a certain type that match the given criteria
        /// </summary>
        /// <param name="criteria">Optional. The search parameters to filter the resources on. Each
        /// given string is a combined key/value pair (separated by '=')</param>
        /// <param name="includes">Optional. A list of include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="summary">Optional. Whether to include only return a summary of the resources in the Bundle</param>
        /// <typeparam name="TResource">The type of resource to list</typeparam>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        /// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
        /// of all resources of the given Resource type</remarks>
        public Bundle Search<TResource>(string[] criteria = null, string[] includes = null, int? pageSize = null, SummaryType summary = SummaryType.False)
            where TResource : Resource, new()
        {
            return Search(ModelInfo.GetResourceNameForType(typeof(TResource)), criteria, includes, pageSize, summary);
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
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        /// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
        /// of all resources of the given Resource type</remarks>
        public Bundle Search(string resource, string[] criteria = null, string[] includes = null, int? pageSize = null, SummaryType summary = SummaryType.False)
        {
            if (resource == null) throw Error.ArgumentNull("resource");

            return Search(toQuery(criteria, includes, pageSize, summary), resource);
        }



        /// <summary>
        /// Search for Resources across the whol server that match the given criteria
        /// </summary>
        /// <param name="criteria">Optional. The search parameters to filter the resources on. Each
        /// given string is a combined key/value pair (separated by '=')</param>
        /// <param name="includes">Optional. A list of include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <param name="summary">Optional. Whether to include only return a summary of the resources in the Bundle</param>
        /// <returns>A Bundle with all resources found by the search, or an empty Bundle if none were found.</returns>
        /// <remarks>All parameters are optional, leaving all parameters empty will return an unfiltered list 
        /// of all resources of the given Resource type</remarks>
        public Bundle WholeSystemSearch(string[] criteria = null, string[] includes = null, int? pageSize = null, SummaryType summary = SummaryType.False)
        {
            return Search(toQuery(criteria, includes, pageSize, summary));
        }

        /// <summary>
        /// Search for resources based on a resource's id.
        /// </summary>
        /// <param name="id">The id of the resource to search for</param>
        /// <param name="includes">Zero or more include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <typeparam name="TResource">The type of resource to search for</typeparam>
        /// <returns>A Bundle with the BundleEntry as identified by the id parameter or an empty
        /// Bundle if the resource wasn't found.</returns>
        /// <remarks>This operation is similar to Read, but additionally,
        /// it is possible to specify include parameters to include resources in the bundle that the
        /// returned resource refers to.</remarks>
        public Bundle SearchById<TResource>(string id, string[] includes = null, int? pageSize = null) where TResource : Resource, new()
        {
            if (id == null) throw Error.ArgumentNull("id");

            return SearchById(typeof(TResource).GetCollectionName(), id, includes, pageSize);
        }

        /// <summary>
        /// Search for resources based on a resource's id.
        /// </summary>
        /// <param name="resource">The type of resource to search for</param>
        /// <param name="id">The id of the resource to search for</param>
        /// <param name="includes">Zero or more include paths</param>
        /// <param name="pageSize">Optional. Asks server to limit the number of entries per page returned</param>
        /// <returns>A Bundle with the BundleEntry as identified by the id parameter or an empty
        /// Bundle if the resource wasn't found.</returns>
        /// <remarks>This operation is similar to Read, but additionally,
        /// it is possible to specify include parameters to include resources in the bundle that the
        /// returned resource refers to.</remarks>
        public Bundle SearchById(string resource, string id, string[] includes = null, int? pageSize = null)
        {
            if (resource == null) throw Error.ArgumentNull("resource");
            if (id == null) throw Error.ArgumentNull("id");

            string criterium = "_id=" + id;
            return Search(toQuery(new string[] { criterium }, includes, pageSize, SummaryType.False), resource);
        }

        private SearchParams toQuery(string[] criteria, string[] includes, int? pageSize, SummaryType summary)
        {
            var q = new SearchParams();
            
            q.Count = pageSize;

            if (includes != null)
                foreach (var inc in includes) q.Include.Add(inc);

            if (criteria != null)
            {
                foreach (var crit in criteria)
                {
                    var keyVal = crit.SplitLeft('=');
                    q.Add(keyVal.Item1,keyVal.Item2);
                }
            }

            q.Summary = summary;

            return q;
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
            if (current == null) throw Error.ArgumentNull("current");
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
                return execute<Bundle>(tx, HttpStatusCode.OK);                
            }
            else
                return null;
        }   
    }

    public enum PageDirection
    {
        First,
        Previous,
        Next,
        Last
    }
}
