/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System.Collections.Generic;
using Hl7.ElementModel;
using System.Linq;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Validation
{

    internal interface IBucket
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidates"></param>
        /// <remarks><paramref value='sequential' /> ensures the slice only tries to match the list of candidates in order: the first non-matching candidate
        /// ends further evaluation of the list of canidates. This improves performance for ordered slices.</remarks>
        OperationOutcome Judge(IEnumerable<SliceCandidate> candidates);

        /// <summary>
        /// The results of the last call to Receive().
        /// </summary>
        /// <remarks>Results are in the same order as the candidates passed in, and the result list will have the same number of elements.</remarks>
        //IList<SliceOutcome> Results { get; }

        ElementDefinition Root { get; }
    }


    //internal static class IBucketExtensions
    //{
    //    public static int CountMembers(this IBucket bucket) => bucket.Results.Count(r => r.Membership == SliceMembership.Member);
    //}

}