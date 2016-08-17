using Hl7.ElementModel;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.FluentPath.Functions
{
    internal static class UtilityOperators
    {
        public static IEnumerable<IValueProvider> Extension(this IEnumerable<IValueProvider> focus, string url)
        {
            return focus.Navigate("extension").Where(es => es.Navigate("url").Single().IsEqualTo(new ConstantValue(url)));
        }

        public static IEnumerable<IValueProvider> Trace(this IEnumerable<IValueProvider> focus, string name)
        {
            System.Diagnostics.Trace.WriteLine("=== Trace {0} ===".FormatWith(name));

            if (focus == null)
                System.Diagnostics.Trace.WriteLine("(null)");

            else if (focus is IEnumerable<IValueProvider>)
            {
                System.Diagnostics.Trace.WriteLine("Collection:".FormatWith(name));
                foreach (var element in (IEnumerable<IValueProvider>)focus)
                {
                    if (element.Value != null)
                        System.Diagnostics.Trace.WriteLine("   " + element.Value.ToString());
                }
            }
            else if (focus is IValueProvider)
            {
                var element = (IValueProvider)focus;
                System.Diagnostics.Trace.WriteLine("Value:".FormatWith(name));

                if (element.Value != null)
                {
                    System.Diagnostics.Trace.WriteLine(element.Value.ToString());
                }
            }
            else
                System.Diagnostics.Trace.WriteLine(focus.ToString());

            System.Diagnostics.Trace.WriteLine(Environment.NewLine);

            return focus;
        }
    }
}
