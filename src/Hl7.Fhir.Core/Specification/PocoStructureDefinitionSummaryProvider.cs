/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Model;

namespace Hl7.Fhir.Specification
{
    public class PocoStructureDefinitionSummaryProvider : IStructureDefinitionSummaryProvider
    {
        public IStructureDefinitionSummary Provide(string canonical) =>
            ModelInfo.ModelInspector.Provide(canonical);
    }
}
