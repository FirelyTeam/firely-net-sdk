/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public class FhirXmlNodeSettings
    {
        public XNamespace[] AllowedExternalNamespaces;
        public bool DisallowSchemaLocation;
        public bool PermissiveParsing;

#if NET_XSD_SCHEMA
        public bool ValidateFhirXhtml;
#endif

        public FhirXmlNodeSettings Clone() =>
            new FhirXmlNodeSettings
            {
                AllowedExternalNamespaces = (XNamespace[])AllowedExternalNamespaces?.Clone(),
                DisallowSchemaLocation = DisallowSchemaLocation,
                PermissiveParsing = PermissiveParsing,

#if NET_XSD_SCHEMA
                ValidateFhirXhtml = ValidateFhirXhtml
#endif
            };
    }
}