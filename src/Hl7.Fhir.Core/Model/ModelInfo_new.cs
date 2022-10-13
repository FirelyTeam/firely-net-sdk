/*
  Copyright (c) 2011-2012, HL7, Inc
  All rights reserved.
  
  Redistribution and use in source and binary forms, with or without modification, 
  are permitted provided that the following conditions are met:
  
   * Redistributions of source code must retain the above copyright notice, this 
     list of conditions and the following disclaimer.
   * Redistributions in binary form must reproduce the above copyright notice, 
     this list of conditions and the following disclaimer in the documentation 
     and/or other materials provided with the distribution.
   * Neither the name of HL7 nor the names of its contributors may be used to 
     endorse or promote products derived from this software without specific 
     prior written permission.
  
  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
  POSSIBILITY OF SUCH DAMAGE.
  

*/

using System;
using System.Linq;

namespace Hl7.Fhir.Model
{
    public partial class ModelInfoNEW
    {
        public static string Version => "4.3.0";

        public static readonly Uri FhirCoreProfileBaseUri = new Uri(@"http://hl7.org/fhir/StructureDefinition/");

        public static bool IsInstanceTypeFor(string superclass, string subclass) => true;
        public static bool IsInstanceTypeFor(Type superclass, Type subclass) => true;
        public static bool IsInstanceTypeFor(FHIRAllTypes superclass, FHIRAllTypes subclass) => true;

        public static Canonical CanonicalUriForFhirCoreType(string typename) => new();
        public static Canonical CanonicalUriForFhirCoreType(Type type) => new();
        public static Canonical CanonicalUriForFhirCoreType(FHIRAllTypes type) => new();

        public static FHIRAllTypes? FhirTypeNameToFhirType(string typeName) => null;

        public static string FhirTypeToFhirTypeName(FHIRAllTypes type) => null;
        public static string GetFhirTypeNameForType(Type type) => null;

        public static ResourceType? FhirTypeNameToResourceType(string typeName) => null;

        public static bool IsDataType(string name) => true;

        public static bool IsPrimitive(string name) => true;
        public static bool IsPrimitive(Type type) => true;
        public static bool IsPrimitive(FHIRAllTypes type) => true;

        public static bool IsBindable(string type) => true;
        public static bool IsCoreModelType(string name) => true;



        public static bool IsConformanceResource(Type type) => true;
        public static bool IsConformanceResource(string name) => true;
        public static bool IsConformanceResource(ResourceType type) => true;
        public static bool IsConformanceResource(ResourceType? type) => true;



        public static bool CheckMinorVersionCompatibility(string externalVersion)
        {
            if (string.IsNullOrEmpty(externalVersion))
            {
                throw new ArgumentNullException();
            }

            var minorFhirVersion = getMajorAndMinorVersion(Version);
            var externalMinorVersion = getMajorAndMinorVersion(externalVersion);

            if (string.IsNullOrEmpty(minorFhirVersion) || string.IsNullOrEmpty(externalVersion))
            {
                return false;
            }
            else
            {
                return minorFhirVersion == externalMinorVersion;
            }
        }
        private static string getMajorAndMinorVersion(string version)
        {
            var versionnumbers = version.Split('.');
            if (versionnumbers.Count() >= 2)
            {
                return string.Join(".", versionnumbers[0], versionnumbers[1]);
            }
            else
            {
                return null;
            }
        }
    }
}
