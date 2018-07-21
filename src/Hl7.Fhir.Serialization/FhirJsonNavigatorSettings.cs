/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/

using Hl7.Fhir.Utility;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public class FhirJsonNavigatorSettings
    {
        public bool PermissiveParsing;
        public bool AllowJsonComments;
    }
}