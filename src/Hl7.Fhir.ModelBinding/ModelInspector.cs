using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hl7.Fhir.ModelBinding
{
    public class ModelInspector
    {
        private Dictionary<Tuple<string,string>,MappedModelClass> _resourceClasses = 
            new Dictionary<Tuple<string,string>,MappedModelClass>();


        public void Inspect(Assembly assembly)
        {
            throw Error.NotImplemented();
        }

        public void Inspect(Type type)
        {
            //TODO: if FhirComposite (specify enumerated fhir datatype) attribute is applied
            //TODO: capture profile context
            //TODO: overwrite existing class for each resource
            //TODO: profile-specific versus generic Resource handler

            //if(IsFhirResource(type))
        }


        

        public static bool IsFhirResource(Type type)
        {
            return typeof(Resource).IsAssignableFrom(type)
                    || type.Name.EndsWith(MappedModelClass.RESOURCENAME_SUFFIX)
                    || type.IsDefined(typeof(FhirResourceAttribute),true);
        }

        public static bool IsFhirComplexType(Type type)
        {
            return typeof(ComplexElement).IsAssignableFrom(type)
                || type.IsDefined(typeof(FhirComplexTypeAttribute), true);
        }

        public static bool IsFhirPrimitive(Type type)
        {
            return typeof(PrimitiveElement).IsAssignableFrom(type)
                || type.IsDefined(typeof(FhirPrimitiveTypeAttribute), true)
                || type.IsDefined(typeof(FhirEnumerationAttribute), false)
                || isFhirPrimtiveAsNativeType(type);
        }

        private static bool isFhirPrimtiveAsNativeType(Type type)
        {
            return type == typeof(bool?) ||
                   type == typeof(int?) ||
                   type == typeof(decimal?) ||
                   type == typeof(byte[]) ||
                   type == typeof(DateTimeOffset?) ||
                   type == typeof(string);
        }

        public void Inspect(PropertyInfo property)
        {
            //TODO: convention: if nullable<primitive> or other supported primitive (e.g. string)
            //TODO: if FhirPrimitive attribute (specify enumerated fhir primitive type)
            //TODO: if enum
            //TODO: if IEnumerable<X> -> still primitive
            //TODO: [Ignore]
            //TODO: if type of member is recognized as a composite...
            //TODO: else ingored
        }
    }


    public enum FhirModelConstruct
    {
        PrimitiveType,
        ComplexType,
        Resource
    }

    internal sealed class MappedModelClass
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

        internal static string getProfile(Type type)
        {
            var attr = (FhirResourceAttribute)Attribute.GetCustomAttribute(type, typeof(FhirResourceAttribute));

            return attr != null ? attr.Profile : null;
        }

        internal static string getMappedResourceName(Type type)
        {
            var attr = (FhirResourceAttribute)Attribute.GetCustomAttribute(type, typeof(FhirResourceAttribute));

            if (attr != null)
                return attr.Name;
            else
            {
                var name = type.Name;
                if (name.EndsWith(RESOURCENAME_SUFFIX))
                    name = name.Substring(0, name.Length - RESOURCENAME_SUFFIX.Length);

                return name;
            }
        }

    }
}
