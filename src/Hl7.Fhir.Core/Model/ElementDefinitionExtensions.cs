/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Rest;

namespace Hl7.Fhir.Model
{
    public static class ElementDefinitionExtensions
    {
        public static ElementDefinition Unbounded(this ElementDefinition ed)
        {
            ed.Max = "*";
            return ed;
        }

        public static ElementDefinition Prohibited(this ElementDefinition ed)
        {
            ed.Min = 0;
            ed.Max = "0";
            return ed;
        }

        public static ElementDefinition Required(this ElementDefinition ed, int min = 1, string max = "*")
        {
            ed.Min = min;
            ed.Max = max;
            return ed;
        }

        public static ElementDefinition OfType(this ElementDefinition ed, FHIRDefinedType type, string profile=null)
        {
            ed.Type.Clear();
            ed.Type.Add(new ElementDefinition.TypeRefComponent { Code = type, Profile = new[] { profile } });
            return ed;
        }

        public static ElementDefinition OrType(this ElementDefinition ed, FHIRDefinedType type, string profile = null)
        {
            ed.Type.Add(new ElementDefinition.TypeRefComponent { Code = type, Profile = new[] { profile } });
            return ed;
        }

        public static ElementDefinition Value(this ElementDefinition ed, Element fix=null, Element pattern=null )
        {
            ed.Fixed = fix;
            ed.Pattern = pattern;

            return ed;
        }

    }
}
