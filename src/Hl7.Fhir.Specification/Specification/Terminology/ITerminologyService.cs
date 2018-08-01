/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
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
        /// <param name="canonical">Canonical url of the ValueSet (ValueSet.url)</param>
        /// <param name="context">The context of the value set, so that the service can resolve this to a value set to validate against.</param>
        /// <param name="valueSet">Allows the caller to send the valueset directly, instead of by providing the canonical or context.</param>
        /// <param name="code">The code to be validated</param>
        /// <param name="system">System for the code.</param>
        /// <param name="version">The version of the system to be used</param>
        /// <param name="display">If given, will check whether the given display is valid for the code</param>
        /// <param name="coding">The coding to validate (instead of code/system/version)</param>
        /// <param name="codeableConcept">A full codeable concept ro validate</param>
        /// <param name="date">The date for which the validation should be checked</param>
        /// <param name="abstract">If true, then an abstract code is allowed to be used in the context of the code that is being validated.</param>
        /// <param name="displayLanguage">Language to be used for description when validating the display property</param>
        /// <returns>An OperationOutcome with the result of the validation</returns>
        /// <remarks>See http://hl7.org/valueset-operations.html#validate-code for more information</remarks>
        OperationOutcome ValidateCode(string canonical = null, string context = null, ValueSet valueSet = null, string code = null,
                string system = null, string version = null, string display = null,
                Coding coding = null, CodeableConcept codeableConcept = null, FhirDateTime date = null,
                bool? @abstract = null, string displayLanguage = null);

    }
}
