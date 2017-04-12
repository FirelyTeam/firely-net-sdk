// [WMR 20170412] For debugging purposes
#if DEBUG

using Hl7.Fhir.Model;
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

    }
}

#endif
