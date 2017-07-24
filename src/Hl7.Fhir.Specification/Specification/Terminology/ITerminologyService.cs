/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Specification.Terminology
{
    public interface ITerminologyService
    {
        /// <summary>
        /// Will check whether the a code is a member of the given valueset.
        /// </summary>
        /// <param name="uri">Canonical url of the ValueSet (ValueSet.url)</param>
        /// <param name="code">Code to check</param>
        /// <param name="system">System for the code. Optional.</param>
        /// <param name="display">If given, will check whether the given display is valid for the code</param>
        /// <param name="abstractAllowed">If true, then an abstract code is allowed to be used in the context of the code that is being validated.</param>
        /// <returns></returns>
        OperationOutcome ValidateCode(string uri, string code, string system, string display = null, bool abstractAllowed=false);

        /// <summary>
        /// Will check whether the a code is a member of the given valueset.
        /// </summary>
        /// <param name="vs">The valueset to validate the code against</param>
        /// <param name="code">Code to check</param>
        /// <param name="system">System for the code. Optional.</param>
        /// <param name="display">If given, will check whether the given display is valid for the code</param>
        /// <param name="abstractAllowed">If true, then an abstract code is allowed to be used in the context of the code that is being validated.</param>
        /// <returns></returns>
        OperationOutcome ValidateCode(ValueSet vs, string code, string system, string display = null, bool abstractAllowed = false);
    }
}
