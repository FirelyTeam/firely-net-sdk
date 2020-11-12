/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Hl7.Fhir.Specification
{
    public class PocoStructureDefinitionSummaryProvider : IStructureDefinitionSummaryProvider
    {
        public IStructureDefinitionSummary Provide(string canonical) =>
            ModelInfo.GetStructureDefinitionSummaryProvider().Provide(canonical);
    }
}
