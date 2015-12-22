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


        public static Type GetTypeForFhirType(string name)
        {
            if (!FhirTypeToCsType.ContainsKey(name))
                return null;
            else
                return FhirTypeToCsType[name];
        }


        public static string GetFhirTypeForType(Type type)
        {
            if (!FhirCsTypeToString.ContainsKey(type))
                return null;
            else
                return FhirCsTypeToString[type];
        }


        public static bool IsKnownResource(string name)
        {
            return SupportedResources.Contains(name);
        }

        public static bool IsKnownResource(Type type)
        {
            var name = GetFhirTypeForType(type);

            return name != null && IsKnownResource(name);
        }

        public static Type GetTypeForResourceName(string name)
        {
            if (!IsKnownResource(name)) return null;

            return GetTypeForFhirType(name);
        }

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
            return FhirTypeToCsType.ContainsKey(name) && Char.IsLower(name[0]);
        }

        public static bool IsPrimitive(Type type)
        {
            return IsPrimitive(type.Name);
        }

        public static bool IsDataType(string name)
        {
            return FhirTypeToCsType.ContainsKey(name) && !IsKnownResource(name) && !IsPrimitive(name);
        }


        public static bool IsDataType(Type type)
        {
            return IsDataType(type.Name);
        }

        public static bool IsReference(string name)
        {
            return name == "Reference";
        }

        public static bool IsReference(Type type)
        {
            return IsReference(type.Name);
        }

        public static bool IsConformanceResource(Type type)
        {
            return IsConformanceResource(type.Name);
        }

        public static bool IsConformanceResource(string name)
        {
            return ConformanceResources.Contains(name);
        }

        public static readonly string[] ConformanceResources = { "Conformance", "StructureDefinition", "ValueSet", "ConceptMap",
                "DataElement", "OperationDefinition", "SearchParameter", "NamingSystem", "ImplementationGuide", "TestScript" };

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
            // if (typeof(Resource).IsAssignableFrom(type))
            if (type.CanBeTreatedAsType(typeof(Resource)))
                return ModelInfo.GetResourceNameForType(type);
            else
                throw new ArgumentException(String.Format(
                    "Cannot determine collection name, type {0} is not a resource type", type.Name));
        }
    }

}
