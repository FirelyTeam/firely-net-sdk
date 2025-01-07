#nullable enable
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Model;

public static class BaseExtensions
{
    [Obsolete("Use EnumerateElements() instead. Note that with EnumerateElements(), the elements 'div' and 'id' are not FhirStrings, but XHtml and FhirUri respectively.")]
    public static IEnumerable<Base> Children(this Base instance)
    {
        foreach (var element in instance.EnumerateElements())
        {
            switch (element.Key, element.Value)
            {
                case ("div", XHtml xhtml):
                    yield return new FhirString(xhtml.Value);
                    break;
                case ("id", FhirUri id):
                    yield return new FhirString(id.Value);
                    break;
                case (_, IEnumerable<Base> list):
                    foreach (var item in list)
                        yield return item;
                    break;
                case("value", _) when instance is PrimitiveType:
                    yield break;
                default:
                    yield return (Base)element.Value;
                    break;
            }
        }
    }


    internal static ITypedElement MakeElementStack(this Base instance, ModelInspector modelInspector, SummaryType summary, string[]? elements, bool includeMandatoryInElementsSummary)
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

    /// <summary>
    /// Add the SUBSETTED tag to the Meta of the resource and all its resource children.
    /// </summary>
    /// <remarks>Will not add the tag to the root of a Bundle, since that is normally the container of
    /// the subsetted resources. If the resource already contained a SUBSETTED tag, they will not
    /// be reapplied.</remarks>
    public static void AddSubsetted(this Base instance)
    {
        addSubsetted(instance, true);
        return;

        static void addSubsetted(Base instance, bool atRoot)
        {
            var isBundleAtRoot = instance is Bundle && atRoot;

            if (instance is Resource resource && !isBundleAtRoot)
            {
                resource.Meta ??= new Meta();

                foreach (var item in SUBSETTED_TAGS)
                {
                    if (!resource.Meta.Tag.Any(t => t.System == item.System && t.Code == item.Code))
                    {
                        resource.Meta.Tag.Add((Coding)item.DeepCopy());
                    }
                }
            }

#pragma warning disable CS0618 // Type or member is obsolete
            foreach (var child in instance.Children())
#pragma warning restore CS0618 // Type or member is obsolete
                addSubsetted(child, atRoot: false);
        }
    }

    private static readonly Coding[] SUBSETTED_TAGS =
    [
        new("http://hl7.org/fhir/v3/ObservationValue", "SUBSETTED"), // STU3 Tag
        new("http://terminology.hl7.org/CodeSystem/v3-ObservationValue", "SUBSETTED") // Tag from R4 and higher
    ];

}