using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.ModelBinding
{
    public class MappedModelClass
    {
        internal const string RESOURCENAME_SUFFIX = "Resource";

        public FhirModelConstruct ModelConstruct;

        public string Name { get; set; }

        public string Profile { get; set; }
        public Type ImplementingType { get; set; }

        public static MappedModelClass ForResource(Type t)
        {
            var result = new MappedModelClass();
            result.ModelConstruct = FhirModelConstruct.Resource;
            result.Name = getMappedResourceName(t);
            result.Profile = getProfile(t);
            result.ImplementingType = t;

            return result;
        }


        public static MappedModelClass ForComplexType(Type t)
        {
            var result = new MappedModelClass();
            result.ModelConstruct = FhirModelConstruct.ComplexType;
            result.Name = getMappedComplexTypeName(t);
            result.Profile = null;  // No support for profiled datatypes
            result.ImplementingType = t;

            return result;
        }

        public static MappedModelClass ForFhirPrimitive(Type t)
        {
            var result = new MappedModelClass();
            result.ModelConstruct = FhirModelConstruct.PrimitiveType;
            result.Name = getMappedPrimitiveTypeName(t);
            result.Profile = null;  // No support for profiled datatypes
            result.ImplementingType = t;

            return result;
        }


        private static string getProfile(Type type)
        {
            var attr = (FhirResourceAttribute)Attribute.GetCustomAttribute(type, typeof(FhirResourceAttribute));

            return attr != null ? attr.Profile : null;
        }

        private static string getMappedResourceName(Type type)
        {
            var attr = (FhirResourceAttribute)Attribute.GetCustomAttribute(type, typeof(FhirResourceAttribute));

            if (attr != null)
            {
                return attr.Name;
            }                
            else
            {
                var name = type.Name;
                if (name.EndsWith(RESOURCENAME_SUFFIX))
                    name = name.Substring(0, name.Length - RESOURCENAME_SUFFIX.Length);

                return name;
            }
        }


        private static string getMappedComplexTypeName(Type type)
        {
            var attr = (FhirComplexTypeAttribute)Attribute.GetCustomAttribute(type, typeof(FhirComplexTypeAttribute));

            if (attr != null)
                return attr.Name;
            else
                return type.Name;
        }

        private static string getMappedPrimitiveTypeName(Type type)
        {
            var attr = (FhirPrimitiveTypeAttribute)Attribute.GetCustomAttribute(type, typeof(FhirPrimitiveTypeAttribute));

            if (attr != null)
                return attr.Name;
            else
                return type.Name;
        }
    }
}
