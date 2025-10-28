/*
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.FhirPath.Expressions;
using System.Collections.Generic;

namespace Hl7.FhirPath
{
    /// <summary>
    /// An interface for tracing FHIRPath expression results during evaluation.
    /// </summary>
    public interface IDebugTracer
    {
        void TraceCall(Expression expr,
            int contextId,
            IEnumerable<PocoNode> focus,
            IEnumerable<PocoNode> thisValue,
            PocoNode index,
            IEnumerable<PocoNode> totalValue,
            IEnumerable<PocoNode> result,
            IEnumerable<KeyValuePair<string, IEnumerable<PocoNode>>> variables);
    }
}
