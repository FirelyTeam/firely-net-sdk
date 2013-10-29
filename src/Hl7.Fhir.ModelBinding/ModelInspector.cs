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
    //TODO: Specify type using enumerated fhir datatype
    //TODO: Find out the right way to handle named resource-local component types (i.e. Patient.AnimalComponent)
    public class ModelInspector
    {
        // Index for easy lookup of resources, key is Tuple<upper resourcename, upper profile>
        private Dictionary<Tuple<string,string>,MappedModelClass> _resourceClasses = 
            new Dictionary<Tuple<string,string>,MappedModelClass>();

        // Index for easy lookup of datatypes, key is upper typenanme
        private Dictionary<string, MappedModelClass> _dataTypeClasses = new Dictionary<string,MappedModelClass>();

        public void Inspect(Assembly assembly)
        {
            if (assembly == null) throw Error.ArgumentNull("assembly");

            foreach (Type t in assembly.GetExportedTypes()) Inspect(t);
        }

        public void Inspect(Type type)
        {
            if (type == null) throw Error.ArgumentNull("type");

            if(type.IsAbstract)
            {
                // Ignore this class
                Message.Info("Skipped type {0} while doing inspection: abstract classes can not be used as mapping targets", type.Name);
                return;
            }

            checkMutualExclusiveAttributes(type);

            if(IsFhirResource(type))
            {
                var mapped = MappedModelClass.ForResource(type);
                var key = buildResourceKey(mapped.Name,mapped.Profile);
                _resourceClasses[key] = mapped;
            }
            else if(IsFhirComplexType(type))
            {
                var mapped = MappedModelClass.ForComplexType(type);
                var key = mapped.Name.ToUpperInvariant();
                _dataTypeClasses[key] = mapped;
            }
            else if(IsFhirPrimitive(type))
            {
                var mapped = MappedModelClass.ForFhirPrimitive(type);
                var key = mapped.Name.ToUpperInvariant();
                _dataTypeClasses[key] = mapped;
            }
            else
            {
                // Ignore this class
                Message.Info("Skipped type {0} while doing inspection: not a resource, complex type or Fhir primitive", type.Name);
            }
        }

        private void checkMutualExclusiveAttributes(Type type)
        {
            if (IsFhirResource(type) && IsFhirComplexType(type))
                throw Error.Argument("type", "Type {0} cannot be both a Resource and a Complex datatype", type);
            if (IsFhirResource(type) && IsFhirPrimitive(type))
                throw Error.Argument("type", "Type {0} cannot be both a Resource and a Primitive datatype", type);
            if (IsFhirComplexType(type) && IsFhirPrimitive(type))
                throw Error.Argument("type", "Type {0} cannot be both a Complex and a Primitive datatype", type);
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
            var noProfileKey = buildResourceKey(name, null);

            MappedModelClass entry = null;

            // Try finding a resource with the specified profile first
            var success = _resourceClasses.TryGetValue(key, out entry);

            // If that didn't work, try again with no profile
            if(!success)
                success = _resourceClasses.TryGetValue(noProfileKey, out entry);

            if (success)
                return entry;
            else
                return null;
        }

        public MappedModelClass GetMappedClassForDataType(string name)
        {
            var key = name.ToUpperInvariant();

            MappedModelClass entry = null;
            var success = _dataTypeClasses.TryGetValue(key, out entry);

            if (success)
                return entry;
            else
                return null;
        }


        public static bool IsFhirResource(Type type)
        {
            return typeof(Resource).IsAssignableFrom(type)
                    || hasResourceNameSuffix(type)
                    || type.IsDefined(typeof(FhirResourceAttribute),true);
        }

        private static bool hasResourceNameSuffix(Type type)
        {
            // This means it *ends* in Resource, not just "Resource"
            return type.Name.EndsWith(MappedModelClass.RESOURCENAME_SUFFIX) && MappedModelClass.RESOURCENAME_SUFFIX != type.Name;
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
                || type.IsDefined(typeof(FhirEnumerationAttribute), false);
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
