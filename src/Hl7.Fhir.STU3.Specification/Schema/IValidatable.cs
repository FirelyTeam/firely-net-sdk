/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Specification.Schema
{
    /// <summary>
    /// Implemented by assertions that work on a single ITypedElement.
    /// </summary>
    internal interface IValidatable
    {
        OperationOutcome Validate(ITypedElement input, ValidationContext vc);
    }
}
