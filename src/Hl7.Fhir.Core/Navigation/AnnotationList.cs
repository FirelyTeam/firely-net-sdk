/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Navigation
{
    internal class AnnotationList : List<Object>
    {
        public override string ToString()
        {
            if(this.Count > 0)
            {
                var result = new StringBuilder();

                result.Append("@[");
                result.Append(string.Join(", ", this.Select(o => o.ToString())));
                result.Append("]");

                return result.ToString();
            }

            return String.Empty;            
        }
    }


    internal static class AnnotationListExtensions
    {
        public static IEnumerable<Object> OfType(this IEnumerable<Object> me, Type t)
        {
            return me.Where(e => e.GetType() == t);
        }

        public static void RemoveOfType(this IList<Object> me, Type t)
        {
            var annotations = me.OfType(t).ToArray();

            foreach (var found in annotations)
                me.Remove(found);
        }
    }
}
