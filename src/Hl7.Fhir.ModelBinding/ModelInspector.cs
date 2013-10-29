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

            if (Attribute.GetCustomAttribute(assembly, typeof(NotMappedAttribute)) != null) return;

            foreach (Type t in assembly.GetExportedTypes()) Inspect(t);
        }

        public void Inspect(Type type)
        {
            if (type == null) throw Error.ArgumentNull("type");

            if (Attribute.GetCustomAttribute(type, typeof(NotMappedAttribute)) != null) return;

            if (type.IsAbstract)
            {
                // Ignore this class
                Message.Info("Skipped type {0} while doing inspection: abstract classes can not be used as mapping targets", type.Name);
                return;
            }

            checkMutualExclusiveAttributes(type);

            if (MappedModelClass.IsFhirResource(type))
            {
                var mapped = MappedModelClass.CreateForResource(type);
                var key = buildResourceKey(mapped.Name, mapped.Profile);
                addProps(mapped, type);
                _resourceClasses[key] = mapped;
            }
            else if (MappedModelClass.IsFhirComplexType(type))
            {
                var mapped = MappedModelClass.CreateForComplexType(type);
                var key = mapped.Name.ToUpperInvariant();
                addProps(mapped, type);
                _dataTypeClasses[key] = mapped;
            }
            else if (MappedModelClass.IsFhirPrimitive(type))
            {
                var mapped = MappedModelClass.CreateForFhirPrimitive(type);
                var key = mapped.Name.ToUpperInvariant();
                addProps(mapped, type);
                _dataTypeClasses[key] = mapped;
            }
            else
            {
                // Ignore this class
                Message.Info("Skipped type {0} while doing inspection: not a resource, complex type or Fhir primitive", type.Name);
            }
        }

        private void addProps(MappedModelClass mapped, Type type)
        {
            var propCollection = new List<MappedModelElement>();

            foreach (var property in ReflectionHelper.FindPublicProperties(type))
            {
                var mappedProperty = Inspect(property);

                if (mappedProperty != null) propCollection.Add(mappedProperty);
            }

            mapped.AddElements(propCollection);
        }


        internal MappedModelElement Inspect(PropertyInfo property)
        {
            if (property == null) throw Error.ArgumentNull("property");

            if (Attribute.GetCustomAttribute(property, typeof(NotMappedAttribute)) != null) return null;

            if (!MappedModelElement.IsMappableElement(property))
            {
                Message.Info("Skipped member {0} in type {1} while doing inspection: not a mappable property",
                        property.Name, property.DeclaringType.Name);
                return null;
            }

            return MappedModelElement.Create(property);
        }


        private void checkMutualExclusiveAttributes(Type type)
        {
            if (MappedModelClass.IsFhirResource(type) && MappedModelClass.IsFhirComplexType(type))
                throw Error.Argument("type", "Type {0} cannot be both a Resource and a Complex datatype", type);
            if (MappedModelClass.IsFhirResource(type) && MappedModelClass.IsFhirPrimitive(type))
                throw Error.Argument("type", "Type {0} cannot be both a Resource and a Primitive datatype", type);
            if (MappedModelClass.IsFhirComplexType(type) && MappedModelClass.IsFhirPrimitive(type))
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
    }
}
