/*
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */
using Hl7.Fhir.ElementModel;
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
            IEnumerable<ITypedElement> focus,
            IEnumerable<ITypedElement> thisValue,
            ITypedElement index,
            IEnumerable<ITypedElement> totalValue,
            IEnumerable<ITypedElement> result,
            IEnumerable<KeyValuePair<string, IEnumerable<ITypedElement>>> variables);
    }
}
