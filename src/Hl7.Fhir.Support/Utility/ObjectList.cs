/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


using System;
using System.Collections.Generic;
using System.Linq;


namespace Hl7.Fhir.Utility
{
    public static class ObjectListExtensions
    {
        [Obsolete("This method is obsolete and will be removed in the next major release. Obsolete since 2021-09-22")]
        public static IEnumerable<object> OfType(this IEnumerable<object> me, Type t)
        {
            return me.Where(e => e.GetType() == t);
        }

        [Obsolete("This method is obsolete and will be removed in the next major release. Obsolete since 2021-09-22")]
        public static void RemoveOfType(this IList<object> me, Type t)
        {
            var annotations = me.OfType(t).ToArray();

            foreach (var found in annotations)
                me.Remove(found);
        }
    }
}