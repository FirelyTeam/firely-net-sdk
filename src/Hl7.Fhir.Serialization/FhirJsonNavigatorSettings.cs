/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public class FhirJsonNavigatorSettings
    {
        public bool PermissiveParsing;
        public bool AllowJsonComments;

#if NET_XSD_SCHEMA
        public bool ValidateFhirXhtml;
#endif

        public FhirJsonNavigatorSettings Clone() =>
            new FhirJsonNavigatorSettings
            {
                PermissiveParsing = PermissiveParsing,
                AllowJsonComments = AllowJsonComments,
#if NET_XSD_SCHEMA
                ValidateFhirXhtml = ValidateFhirXhtml
#endif
            };


    }
}