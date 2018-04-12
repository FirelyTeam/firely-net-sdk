/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Model
{
    public partial class CodeSystem
    {
        public const string CONCEPTPROPERTY_PREFIX = "http://hl7.org/fhir/concept-properties";
        public const string CONCEPTPROPERTY_STATUS = CONCEPTPROPERTY_PREFIX + "#status";
        public const string CONCEPTPROPERTY_RETIREMENT_DATE = CONCEPTPROPERTY_PREFIX + "#retirementDate";
        public const string CONCEPTPROPERTY_DEPRECATION_DATE = CONCEPTPROPERTY_PREFIX + "#deprecationDate";
        public const string CONCEPTPROPERTY_PARENT = CONCEPTPROPERTY_PREFIX + "#parent";
        public const string CONCEPTPROPERTY_NOT_SELECTABLE = CONCEPTPROPERTY_PREFIX + "#notSelectable";  // boolean

        // These properties seem to be in use, but are overlapping with #status?
        public const string CONCEPTPROPERTY_INACTIVE = CONCEPTPROPERTY_PREFIX + "#inactive";   // boolean
        public const string CONCEPTPROPERTY_DEPRECATED = CONCEPTPROPERTY_PREFIX + "#deprecated";   // datetime
    }

    public static class CodeSystemExtensions
    {
        public static CodeSystem.ConceptPropertyComponent[] ListConceptProperties(this CodeSystem.ConceptDefinitionComponent concept, CodeSystem system, string uri)
        {
            // First, map the url to a code for this codesystem
            string code = system.Property.SingleOrDefault(p => p.Uri == uri)?.Code;

            // Then, query the property
            if (code != null)
                return concept.Property.Where(p => p.Code == code).ToArray();

            return new CodeSystem.ConceptPropertyComponent[0];
        }       
    }
}
