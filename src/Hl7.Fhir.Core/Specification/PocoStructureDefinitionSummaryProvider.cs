/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Reflection;

namespace Hl7.Fhir.Specification
{
    public class PocoStructureDefinitionSummaryProvider : IStructureDefinitionSummaryProvider
    {
        [Obsolete("Please do not create an instance of this class directly anymore, instead call " +
            "ModelInfo.GetStructureDefinitionSummaryProvider.")]
        public PocoStructureDefinitionSummaryProvider()
        { 
        }

        public IStructureDefinitionSummary Provide(string canonical) => 
            ModelInfo.GetStructureDefinitionSummaryProvider().Provide(canonical);
     }


}
