/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Validation.Model
{   
    /// <summary>
    /// Represents a harvested valueset. This is a subset of FHIR's ValueSet that is normalized and
    /// simplified for use with the validator
    /// </summary>
    public class ValueSet
    {
        public Hl7.Fhir.Model.ValueSet Original;

        public string System { get; set; }

        public string Uri { get; set; }

        public List<string> Codes { get; set; }

        public ValueSet(Hl7.Fhir.Model.ValueSet inner)
        {
            Original = inner;
        }

        private bool conceptContains(List<Hl7.Fhir.Model.ValueSet.ConceptDefinitionComponent> concepts, string value)
        {
            if (concepts != null)
            foreach(var concept in concepts)
            {
                if (value == concept.Code) return true;
                if (conceptContains(concept.Concept, value)) return true;
            }
            
            return false;
        }

        private bool composeContains(Hl7.Fhir.Model.ValueSet.ConceptDefinitionComponent compose, string value)
        {
            //foreach(var conceptset in compose.Include)
            //{
            //    if (conceptset.Code.Contains(value)) return true;
            //}
            return false;
        }

        public bool Contains(string code)
        {
            //if (valueset.Define != null)
            //{
            //    if (ConceptContains(valueset.Define.Concept, code)) return true;
                
            //}
            //if (valueset.Compose != null)
            //{
            //    if (ComposeContains(valueset.Compose, code)) return true;
            //}
            return false;
        }

        public override string ToString()
        {
            return System.ToString();
        }


        public static ValueSet HarvestValueSet(Fhir.Model.ValueSet source)
        {
            ValueSet valueset = new ValueSet(source);

            //   todo: This now only works with "defines". 
            if (source.CodeSystem == null)
                throw Error.Argument("Valueset '{0}' is has no inline defined codes. The Validator currently does not support valuesets with Compose"
                                    .FormatWith(source.Url));

            valueset.System = source.CodeSystem.System;
            valueset.Uri = source.Url;

            if (source.CodeSystem.Concept != null)
            {
                foreach (var concept in source.CodeSystem.Concept)
                {
                    valueset.Codes.Add(concept.Code);
                }
            }
            return valueset;

        }
    }
     
}
