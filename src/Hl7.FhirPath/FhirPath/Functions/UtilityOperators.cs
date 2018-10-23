using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;

namespace Hl7.FhirPath.Functions
{
    internal static class UtilityOperators
    {
        static Action<string> WriteLine = (string s) => Debug.WriteLine(s);

        public static IEnumerable<ITypedElement> Extension(this IEnumerable<ITypedElement> focus, string url)
        {
            return focus.Navigate("extension").Where(es => es.Navigate("url").Single().IsEqualTo(new ConstantValue(url)));
        }

        public static IEnumerable<ITypedElement> Trace(this IEnumerable<ITypedElement> focus, string name)
        {
            WriteLine("=== Trace {0} ===".FormatWith(name));

            if (focus == null)
                WriteLine("(null)");
            else
            {
                WriteLine("Collection:".FormatWith(name));
                foreach (var element in focus)
                {
                    if (element.Value != null)
                        WriteLine("   " + element.Value.ToString());
                }
            }
            // todo: this is always false --mh
            //else if (focus is IValueProvider)
            //{
            //    var element = (IValueProvider)focus;
            //    WriteLine("Value:".FormatWith(name));

            //    if (element.Value != null)
            //    {
            //        WriteLine(element.Value.ToString());
            //    }
            //}
            //else
            //    WriteLine(focus.ToString());

            WriteLine(Environment.NewLine);

            return focus;
        }
    }
}
