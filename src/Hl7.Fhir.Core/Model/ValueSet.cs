/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Model
{
        public partial class ValueSet : Hl7.Fhir.Model.DomainResource
        {
            [Obsolete("This property was renamed in DSTU2 to CodeSystem", true)]
            public CodeSystemComponent Define { get; set; }

    //        public static bool CodeEquals(string code, string value, bool caseSensitive)
    //        {
    //            return String.Equals(code, value,
    //                caseSensitive ? StringComparison.Ordinal :
    //                        StringComparison.OrdinalIgnoreCase);
    //        }


    //        public static IEnumerable<ValueSetDefineConceptComponent> GetFlattenedDefinedConcepts(
    //                        IEnumerable<ValueSetDefineConceptComponent> concepts)
    //        {
    //            foreach (var concept in concepts)
    //            {
    //                yield return concept;

    //                if (concept.Concept != null)
    //                    foreach (var childConcept in GetFlattenedDefinedConcepts(concept.Concept))
    //                        yield return childConcept;
    //            }
    //        }


    //        internal static ValueSetDefineConceptComponent GetDefinedConceptForCode(
    //                    IEnumerable<ValueSetDefineConceptComponent> concepts, string code, bool caseSensitive = true)
    //        {
    //            if (concepts != null)
    //                return GetFlattenedDefinedConcepts(concepts)
    //                    .FirstOrDefault(c => CodeEquals(c.Code, code, caseSensitive));
    //            else
    //                return null;
    //        }


    //        /// <summary>
    //        /// Searches for a concept, defined in this ValueSet, using its code
    //        /// </summary>
    //        /// <param name="code"></param>
    //        /// <returns>The concept, or null if there was no concept found with that code</returns>
    //        /// <remarks>The search will search nested concepts as well. 
    //        /// Whether the search is case-sensitive depends on the value of Define.CaseSensitive</remarks>
    //        public ValueSetDefineConceptComponent GetDefinedConceptForCode(string code)
    //        {
    //            if (this.Define != null && this.Define.Concept != null)
    //            {
    //                bool caseSensitive = Define.CaseSensitive.GetValueOrDefault();
    //                return GetDefinedConceptForCode(this.Define.Concept, code, caseSensitive);
    //            }
    //            else
    //                return null;
    //        }
    //    }
    }
}
