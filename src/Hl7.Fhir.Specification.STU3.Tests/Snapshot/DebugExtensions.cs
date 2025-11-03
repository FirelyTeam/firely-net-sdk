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
            if (!elem.Extension.IsExactly(other.Extension)) { l.Add(nameof(ElementDefinition.Extension)); }
            if (!elem.PathElement.IsExactly(other.PathElement)) { l.Add(nameof(ElementDefinition.Path)); }
            if (!elem.RepresentationElement.IsExactly(other.RepresentationElement)) { l.Add(nameof(ElementDefinition.Representation)); }
            if (!elem.SliceNameElement.IsExactly(other.SliceNameElement)) { l.Add(nameof(ElementDefinition.SliceName)); }
            if (!elem.LabelElement.IsExactly(other.LabelElement)) { l.Add(nameof(ElementDefinition.Label)); }
            if (!elem.Code.IsExactly(other.Code)) { l.Add(nameof(ElementDefinition.Code)); }
            if (!elem.Slicing.IsExactly(other.Slicing)) { l.Add(nameof(ElementDefinition.Slicing)); }
            if (!elem.ShortElement.IsExactly(other.ShortElement)) { l.Add(nameof(ElementDefinition.Short)); }
            if (!elem.DefinitionElement.IsExactly(other.DefinitionElement)) { l.Add(nameof(ElementDefinition.Definition)); }
            if (!elem.CommentElement.IsExactly(other.CommentElement)) { l.Add(nameof(ElementDefinition.Comment)); }
            if (!elem.RequirementsElement.IsExactly(other.RequirementsElement)) { l.Add(nameof(ElementDefinition.Requirements)); }
            if (!elem.AliasElement.IsExactly(other.AliasElement)) { l.Add(nameof(ElementDefinition.Alias)); }
            if (!elem.MinElement.IsExactly(other.MinElement)) { l.Add(nameof(ElementDefinition.Min)); }
            if (!elem.MaxElement.IsExactly(other.MaxElement)) { l.Add(nameof(ElementDefinition.Max)); }
            if (!elem.Base.IsExactly(other.Base)) { l.Add(nameof(ElementDefinition.Base)); }
            if (!elem.ContentReferenceElement.IsExactly(other.ContentReferenceElement)) { l.Add(nameof(ElementDefinition.ContentReference)); }
            if (!elem.Type.IsExactly(other.Type)) { l.Add(nameof(ElementDefinition.Type)); }
            if (!elem.DefaultValue.IsExactly(other.DefaultValue)) { l.Add(nameof(ElementDefinition.DefaultValue)); }
            if (!elem.MeaningWhenMissingElement.IsExactly(other.MeaningWhenMissingElement)) { l.Add(nameof(ElementDefinition.MeaningWhenMissing)); }
            if (!elem.OrderMeaningElement.IsExactly(other.OrderMeaningElement)) { l.Add(nameof(ElementDefinition.OrderMeaning)); }
            if (!elem.Fixed.IsExactly(other.Fixed)) { l.Add(nameof(ElementDefinition.Fixed)); }
            if (!elem.Pattern.IsExactly(other.Pattern)) { l.Add(nameof(ElementDefinition.Pattern)); }
            if (!elem.Example.IsExactly(other.Example)) { l.Add(nameof(ElementDefinition.Example)); }
            if (!elem.MinValue.IsExactly(other.MinValue)) { l.Add(nameof(ElementDefinition.MinValue)); }
            if (!elem.MaxValue.IsExactly(other.MaxValue)) { l.Add(nameof(ElementDefinition.MaxValue)); }
            if (!elem.MaxLengthElement.IsExactly(other.MaxLengthElement)) { l.Add(nameof(ElementDefinition.MaxLength)); }
            if (!elem.ConditionElement.IsExactly(other.ConditionElement)) { l.Add(nameof(ElementDefinition.Condition)); }
            if (!elem.Constraint.IsExactly(other.Constraint)) { l.Add(nameof(ElementDefinition.Constraint)); }
            if (!elem.MustSupportElement.IsExactly(other.MustSupportElement)) { l.Add(nameof(ElementDefinition.MustSupport)); }
            if (!elem.IsModifierElement.IsExactly(other.IsModifierElement)) { l.Add(nameof(ElementDefinition.IsModifier)); }
            if (!elem.IsSummaryElement.IsExactly(other.IsSummaryElement)) { l.Add(nameof(ElementDefinition.IsSummary)); }
            if (!elem.Binding.IsExactly(other.Binding)) { l.Add(nameof(ElementDefinition.Binding)); }
            if (!elem.Mapping.IsExactly(other.Mapping)) { l.Add(nameof(ElementDefinition.Mapping)); }

            return string.Join(",", l);
        }
    }
}

#endif