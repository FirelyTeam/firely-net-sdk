/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
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

        public static ElementDefinition Required(this ElementDefinition ed, int min = 1, string max = "1")
        {
            ed.Min = min;
            ed.Max = max;
            return ed;
        }

        public static ElementDefinition OfType(this ElementDefinition ed, FHIRDefinedType type, string profile=null, IEnumerable<ElementDefinition.AggregationMode> aggs=null)
        {
            ed.Type.Clear();
            ed.OrType(type, profile, aggs);

            return ed;
        }

        public static ElementDefinition OrType(this ElementDefinition ed, FHIRDefinedType type, string profile = null, IEnumerable<ElementDefinition.AggregationMode> aggs = null)
        {
            var newType = new ElementDefinition.TypeRefComponent { Code = type };
            if (profile != null) newType.Profile = new[] { profile };
            if(aggs != null) newType.Aggregation = aggs.Cast<ElementDefinition.AggregationMode?>();

            ed.Type.Add(newType);

            return ed;
        }

        public static ElementDefinition Value(this ElementDefinition ed, Element fix=null, Element pattern=null )
        {
            ed.Fixed = fix;
            ed.Pattern = pattern;

            return ed;
        }

        public static ElementDefinition WithBinding(this ElementDefinition ed, string valueSetUri, BindingStrength strength)
        {
            var binding = new ElementDefinition.BindingComponent
            {
                ValueSet = new ResourceReference(valueSetUri),
                Strength = strength
            };

            ed.Binding = binding;

            return ed;
        }

    }
}
