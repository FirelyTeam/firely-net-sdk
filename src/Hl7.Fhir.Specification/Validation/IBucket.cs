/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System.Collections.Generic;
using Hl7.Fhir.Model.DSTU2;
using Hl7.Fhir.ElementModel;

namespace Hl7.Fhir.Validation
{

    internal interface IBucket
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidate"></param>
        /// <remarks><paramref value='sequential' /> ensures the slice only tries to match the list of candidates in order: the first non-matching candidate
        /// ends further evaluation of the list of canidates. This improves performance for ordered slices.</remarks>
        bool Add(ITypedElement candidate);

        /// <summary>
        /// The results of the last call to Receive().
        /// </summary>
        /// <remarks>Results are in the same order as the candidates passed in, and the result list will have the same number of elements.</remarks>
        //IList<SliceOutcome> Results { get; }

        //ElementDefinition Root { get; }

        string Name { get; }

        IList<ITypedElement> Members { get; }

        OperationOutcome Validate(Validator validator, ITypedElement errorLocation);
    }
}