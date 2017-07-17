/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Specification.Source
{
        internal class ConformanceScanInformation
        {
            public ResourceType ResourceType { get; set; }

            public string ResourceUri { get; set; }

            public string Canonical { get; set; }

            public string ValueSetSystem { get; set; }

            public string[] UniqueIds { get; set; }

            public string ConceptMapSource { get; set; }

            public string ConceptMapTarget { get; set; }

            public string Origin { get; set; }

            public override string ToString()
            {
                return "{0} resource with uri {1} (canonical {2}), read from {2}"
                    .FormatWith(ResourceType, ResourceUri ?? "(unknown)", Canonical ?? "(unknown)", Origin);
            }
        }
}
