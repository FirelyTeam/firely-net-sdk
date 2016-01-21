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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Support;
using Hl7.Fhir.Introspection;

namespace Hl7.Fhir.Model
{
    public partial class ModelInfo
    {
        public class SearchParamDefinition
        {
            public string Resource { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public SearchParamType Type { get; set; }

            /// <summary>
            /// If this search parameter is a Composite, this array contains 
            /// the list of search parameters the param is a combination of
            /// </summary>
            public string[] CompositeParams { get; set; }

            /// <summary>
            /// One or more paths into the Resource instance that the search parameter 
            /// uses 
            /// </summary>
            public string[] Path { get; set; }

            /// <summary>
            /// The XPath expression for evaluating this search parameter
            /// </summary>
            public string XPath { get; set; }

            /// <summary>
            /// The FHIR Path expresssion that can be used to extract the data
            /// for this search parameter
            /// </summary>
            public string Expression { get; set; }

            /// <summary>
            /// If this is a reference, the possible types of resources that the
            /// parameters references to
            /// </summary>
            public ResourceType[] Target { get; set; }
        }


        public static string FhirTypeToFhirTypeName(FHIRDefinedType type)
        {
            return type.GetLiteral();
        }

        public static FHIRDefinedType? FhirTypeNameToFhirType(string name)
        {
            FHIRDefinedType result = FHIRDefinedType.Patient;

            if (Enum.TryParse<FHIRDefinedType>(name, ignoreCase: true, result: out result))
                return result;
            else
                return null;
        }

        public static Type GetTypeForFhirType(string name)
        {
            if (!FhirTypeToCsType.ContainsKey(name))
                return null;
            else
                return FhirTypeToCsType[name];
        }


        public static string GetFhirTypeNameForType(Type type)
        {
            if (!FhirCsTypeToString.ContainsKey(type))
                return null;
            else
                return FhirCsTypeToString[type];
        }

        [Obsolete("Use GetFhirTypeNameForType() instead")]
        public static string GetFhirTypeForType(Type type)
        {
            return GetFhirTypeNameForType(type);
        }

        public static bool IsKnownResource(string name)
        {
            return SupportedResources.Contains(name);
        }

        public static bool IsKnownResource(Type type)
        {
            var name = GetFhirTypeNameForType(type);

            return name != null && IsKnownResource(name);
        }

        [Obsolete("Use GetTypeForFhirType() which covers all types, not just resources")]
        public static Type GetTypeForResourceName(string name)
        {
            if (!IsKnownResource(name)) return null;

            return GetTypeForFhirType(name);
        }

        [Obsolete("Use GetFhirTypeNameForType() which covers all types, not just resources")]
        public static string GetResourceNameForType(Type type)
        {
            var name = GetFhirTypeForType(type);

            if (name != null && IsKnownResource(name))
                return name;
            else
                return null;
        }

        public static bool IsPrimitive(string name)
        {
            if (String.IsNullOrEmpty(name)) return false;

            return FhirTypeToCsType.ContainsKey(name) && Char.IsLower(name[0]);
        }

        public static bool IsPrimitive(Type type)
        {
            return IsPrimitive(type.Name);
        }

        public static bool IsPrimitive(FHIRDefinedType type)
        {
            return IsPrimitive(FhirTypeToFhirTypeName(type));
        }


        public static bool IsDataType(string name)
        {
            if (String.IsNullOrEmpty(name)) return false;

            return FhirTypeToCsType.ContainsKey(name) && !IsKnownResource(name) && !IsPrimitive(name);
        }


        public static bool IsDataType(Type type)
        {
            return IsDataType(type.Name);
        }

        public static bool IsDataType(FHIRDefinedType type)
        {
            return IsDataType(FhirTypeToFhirTypeName(type));
        }

        public static bool IsReference(string name)
        {
            return name == "Reference";
        }

        public static bool IsReference(Type type)
        {
            return IsReference(type.Name);
        }

        public static bool IsReference(FHIRDefinedType type)
        {
            return type == FHIRDefinedType.Reference;
        }


        public static bool IsConformanceResource(Type type)
        {
            return IsConformanceResource(type.Name);
        }

        public static bool IsConformanceResource(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;

            var t = FhirTypeNameToFhirType(name);

            if (t != null)
                return IsConformanceResource(t.Value);
            else
                return false;
        }

        public static bool IsConformanceResource(FHIRDefinedType type)
        {
            return ConformanceResources.Contains(type);
        }



        public static readonly FHIRDefinedType[] ConformanceResources = { FHIRDefinedType.Conformance, FHIRDefinedType.StructureDefinition, FHIRDefinedType.ValueSet,
            FHIRDefinedType.ConceptMap, FHIRDefinedType.DataElement, FHIRDefinedType.OperationDefinition, FHIRDefinedType.SearchParameter, FHIRDefinedType.NamingSystem,
             FHIRDefinedType.ImplementationGuide, FHIRDefinedType.TestScript };

        /// <summary>
        /// Is the given type a core Resource, Datatype or primitive
        /// </summary>
        public static bool IsCoreModelType(string name)
        {
            return IsKnownResource(name) || IsDataType(name) || IsPrimitive(name);
        }
    }

    public static class ModelInfoExtensions
    {
        public static string GetCollectionName(this Type type)
        {
            if (type.CanBeTreatedAsType(typeof(Resource)))
                return ModelInfo.GetFhirTypeNameForType(type);
            else
                throw new ArgumentException(String.Format(
                    "Cannot determine collection name, type {0} is not a resource type", type.Name));
        }
    }

}
