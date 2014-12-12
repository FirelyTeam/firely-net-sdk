/*
* Copyright (c) 2014, Furore (info@furore.com) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Model
{
   
    public class ValueSet
    {

        Hl7.Fhir.Model.ValueSet valueset;
        public Uri System { get; set; }

        public ValueSet(Uri system, Hl7.Fhir.Model.ValueSet inner)
        {
            this.valueset = inner;
            this.System = system;
        }

        private bool ConceptContains(List<Hl7.Fhir.Model.ValueSet.ValueSetDefineConceptComponent> concepts, string value)
        {
            if (concepts != null)
            foreach(var concept in concepts)
            {
                if (value == concept.Code) return true;
                if (ConceptContains(concept.Concept, value)) return true;
            }
            
            return false;
        }

        private bool ComposeContains(Hl7.Fhir.Model.ValueSet.ValueSetComposeComponent compose, string value)
        {
            foreach(var conceptset in compose.Include)
            {
                if (conceptset.Code.Contains(value)) return true;
            }
            return false;
        }

        public bool Contains(string code)
        {
            if (valueset.Define != null)
            {
                if (ConceptContains(valueset.Define.Concept, code)) return true;
                
            }
            if (valueset.Compose != null)
            {
                if (ComposeContains(valueset.Compose, code)) return true;
            }
            return false;
        }

        public override string ToString()
        {
            return System.ToString();
        }

    }
     
}
