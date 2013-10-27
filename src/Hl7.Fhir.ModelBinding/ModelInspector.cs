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
        // Index for easy lookup of resources, Tuple is <upper resource, upper profile>
        private Dictionary<Tuple<string,string>,MappedModelClass> _resourceClasses = 
            new Dictionary<Tuple<string,string>,MappedModelClass>();


        public void Inspect(Assembly assembly)
        {
            if (assembly == null) throw Error.ArgumentNull("assembly");

            foreach (Type t in assembly.GetExportedTypes()) Inspect(t);
        }

        public void Inspect(Type type)
        {
            if (type == null) throw Error.ArgumentNull("type");

            //TODO: if FhirComposite (specify enumerated fhir datatype) attribute is applied
            //TODO: overwrite existing class for each resource
            //TODO: profile-specific versus generic Resource handler

            if(IsFhirResource(type))
            {
                var mapped = MappedModelClass.ForResource(type);
                var key = buildResourceKey(mapped.Name,mapped.Profile);

                _resourceClasses[key] = mapped;
            }
        }


        private static Tuple<string, string> buildResourceKey(string name, string profile)
        {
            var normalizedName = name.ToUpperInvariant();
            var normalizedProfile = profile != null ? profile.ToUpperInvariant() : null;

            return Tuple.Create(normalizedName, normalizedProfile);
        }

        public MappedModelClass GetMappedClassForResource(string name, string profile = null)
        {
            var key = buildResourceKey(name, profile);
            
            MappedModelClass entry = null;
            var sucess = _resourceClasses.TryGetValue(key, out entry);

            if (sucess)
                return entry;
            else
                return null;
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

   
}
