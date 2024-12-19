/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Serialization;

public abstract class BaseFhirSerializer(ModelInspector modelInspector)
{
    protected ITypedElement MakeElementStack(Base instance, SummaryType summary, string[]? elements, bool includeMandatoryInElementsSummary)
    {
        if (summary == SummaryType.False && elements == null) return instance.ToTypedElementLegacy(modelInspector);

        if (elements is not null && summary != SummaryType.False)
            throw Error.Argument("elements", "Elements parameter is supported only when summary is SummaryType.False or summary is not specified at all.");

        var patchedInstance = (Base)instance.DeepCopy();

        patchedInstance.AddSubsetted();

        var baseNav = new ScopedNode(patchedInstance.ToTypedElementLegacy(modelInspector));

        return summary switch
        {
            SummaryType.True => MaskingNode.ForSummary(baseNav),
            SummaryType.Text => MaskingNode.ForText(baseNav),
            SummaryType.Data => MaskingNode.ForData(baseNav),
            SummaryType.Count => MaskingNode.ForCount(baseNav),
            SummaryType.False => MaskingNode.ForElements(baseNav, elements, includeMandatoryInElementsSummary),
            _ => baseNav,
        };
    }
}