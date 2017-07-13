// [WMR 20170412] For debugging purposes
#if DEBUG

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Hl7.Fhir.Specification.Tests
{
    public static class DebugExtensions
    {
        public static void DebugPrint(this IEnumerable<ElementDefinition> elemDefs)
        {
            var sb = new StringBuilder();
            foreach (var elemDef in elemDefs)
            {
                sb.DebugAppendLine(elemDef);
            }
            Debug.Print(sb.ToString());
        }

        public static void DebugPrint(this ElementDefinition elemDef)
        {
            if (elemDef == null) { return; }
            var sb = new StringBuilder();
            sb.Append(elemDef);
            Debug.Print(sb.ToString());
        }

        public static void DebugAppendLine(this StringBuilder sb, ElementDefinition elemDef)
        {
            sb.DebugAppend(elemDef);
            sb.AppendLine();
        }

        public static void DebugAppend(this StringBuilder sb, ElementDefinition elemDef)
        {
            if (elemDef == null) { return; }
            sb.Append(elemDef.Path);
            if (elemDef.SliceName != null)
            {
                sb.Append(" : '");
                sb.Append(elemDef.SliceName);
                sb.Append("'");
            }
            sb.DebugAppend(elemDef.Slicing);
        }

        public static void DebugAppend(this StringBuilder sb, ElementDefinition.SlicingComponent slicing)
        {
            if (slicing == null) { return; }
            sb.Append(" - Slicing: ");
            for (int i = 0, cnt = slicing.Discriminator.Count; i < cnt; i++)
            {
                if (i > 0)
                {
                    sb.Append(" | ");
                }
                DebugAppend(sb, slicing.Discriminator[i]);
            }
            // sb.Append(slicing.Rules);
            if (slicing.Ordered == true)
            {
                sb.Append(" (ordered)");
            }
            else if (slicing.Ordered == false)
            {
                sb.Append(" (unordered)");
            }
        }

        public static void DebugAppend(this StringBuilder sb, ElementDefinition.DiscriminatorComponent discriminator)
        {
            if (discriminator == null) { return; }
            sb.Append(discriminator.Type);
            if (discriminator.Path != null)
            {
                sb.Append(" '");
                sb.Append(discriminator.Path);
                sb.Append("'");
            }
        }

        // [WMR 20170711] Returns names of properties that don't match
        public static string DebugChanges(this ElementDefinition elem, ElementDefinition other)
        {
            if (other == null) { return "null"; }

            var l = new List<String>();
            if (elem.ElementId != other.ElementId) { l.Add(nameof(ElementDefinition.ElementId)); }
            if (!DeepComparable.IsExactly(elem.Extension, other.Extension)) { l.Add(nameof(ElementDefinition.Extension)); }

            if (!DeepComparable.IsExactly(elem.PathElement, other.PathElement)) { l.Add(nameof(ElementDefinition.Path)); }
            if (!DeepComparable.IsExactly(elem.RepresentationElement, other.RepresentationElement)) { l.Add(nameof(ElementDefinition.Representation)); }
            if (!DeepComparable.IsExactly(elem.SliceNameElement, other.SliceNameElement)) { l.Add(nameof(ElementDefinition.SliceName)); }
            if (!DeepComparable.IsExactly(elem.LabelElement, other.LabelElement)) { l.Add(nameof(ElementDefinition.Label)); }
            if (!DeepComparable.IsExactly(elem.Code, other.Code)) { l.Add(nameof(ElementDefinition.Code)); }
            if (!DeepComparable.IsExactly(elem.Slicing, other.Slicing)) { l.Add(nameof(ElementDefinition.Slicing)); }
            if (!DeepComparable.IsExactly(elem.ShortElement, other.ShortElement)) { l.Add(nameof(ElementDefinition.Short)); }
            if (!DeepComparable.IsExactly(elem.DefinitionElement, other.DefinitionElement)) { l.Add(nameof(ElementDefinition.Definition)); }
            if (!DeepComparable.IsExactly(elem.CommentElement, other.CommentElement)) { l.Add(nameof(ElementDefinition.Comment)); }
            if (!DeepComparable.IsExactly(elem.RequirementsElement, other.RequirementsElement)) { l.Add(nameof(ElementDefinition.Requirements)); }
            if (!DeepComparable.IsExactly(elem.AliasElement, other.AliasElement)) { l.Add(nameof(ElementDefinition.Alias)); }
            if (!DeepComparable.IsExactly(elem.MinElement, other.MinElement)) { l.Add(nameof(ElementDefinition.Min)); }
            if (!DeepComparable.IsExactly(elem.MaxElement, other.MaxElement)) { l.Add(nameof(ElementDefinition.Max)); }
            if (!DeepComparable.IsExactly(elem.Base, other.Base)) { l.Add(nameof(ElementDefinition.Base)); }
            if (!DeepComparable.IsExactly(elem.ContentReferenceElement, other.ContentReferenceElement)) { l.Add(nameof(ElementDefinition.ContentReference)); }
            if (!DeepComparable.IsExactly(elem.Type, other.Type)) { l.Add(nameof(ElementDefinition.Type)); }
            if (!DeepComparable.IsExactly(elem.DefaultValue, other.DefaultValue)) { l.Add(nameof(ElementDefinition.DefaultValue)); }
            if (!DeepComparable.IsExactly(elem.MeaningWhenMissingElement, other.MeaningWhenMissingElement)) { l.Add(nameof(ElementDefinition.MeaningWhenMissing)); }
            if (!DeepComparable.IsExactly(elem.OrderMeaningElement, other.OrderMeaningElement)) { l.Add(nameof(ElementDefinition.OrderMeaning)); }
            if (!DeepComparable.IsExactly(elem.Fixed, other.Fixed)) { l.Add(nameof(ElementDefinition.Fixed)); }
            if (!DeepComparable.IsExactly(elem.Pattern, other.Pattern)) { l.Add(nameof(ElementDefinition.Pattern)); }
            if (!DeepComparable.IsExactly(elem.Example, other.Example)) { l.Add(nameof(ElementDefinition.Example)); }
            if (!DeepComparable.IsExactly(elem.MinValue, other.MinValue)) { l.Add(nameof(ElementDefinition.MinValue)); }
            if (!DeepComparable.IsExactly(elem.MaxValue, other.MaxValue)) { l.Add(nameof(ElementDefinition.MaxValue)); }
            if (!DeepComparable.IsExactly(elem.MaxLengthElement, other.MaxLengthElement)) { l.Add(nameof(ElementDefinition.MaxLength)); }
            if (!DeepComparable.IsExactly(elem.ConditionElement, other.ConditionElement)) { l.Add(nameof(ElementDefinition.Condition)); }
            if (!DeepComparable.IsExactly(elem.Constraint, other.Constraint)) { l.Add(nameof(ElementDefinition.Constraint)); }
            if (!DeepComparable.IsExactly(elem.MustSupportElement, other.MustSupportElement)) { l.Add(nameof(ElementDefinition.MustSupport)); }
            if (!DeepComparable.IsExactly(elem.IsModifierElement, other.IsModifierElement)) { l.Add(nameof(ElementDefinition.IsModifier)); }
            if (!DeepComparable.IsExactly(elem.IsSummaryElement, other.IsSummaryElement)) { l.Add(nameof(ElementDefinition.IsSummary)); }
            if (!DeepComparable.IsExactly(elem.Binding, other.Binding)) { l.Add(nameof(ElementDefinition.Binding)); }
            if (!DeepComparable.IsExactly(elem.Mapping, other.Mapping)) { l.Add(nameof(ElementDefinition.Mapping)); }

            return string.Join(",", l);
        }
    }
}

#endif
