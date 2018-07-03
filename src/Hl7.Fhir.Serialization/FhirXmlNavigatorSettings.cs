/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public class FhirXmlNavigatorSettings
    {
        public XNamespace[] AllowedExternalNamespaces;
        public bool DisallowSchemaLocation;
    }
}