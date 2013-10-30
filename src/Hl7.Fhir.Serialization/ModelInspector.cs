using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hl7.Fhir.Serialization
{
    //TODO: Specify type using enumerated fhir datatype
    //TODO: Find out the right way to handle named resource-local component types (i.e. Patient.AnimalComponent)
    public class ModelInspector
    {
        // Index for easy lookup of resources, key is Tuple<upper resourcename, upper profile>
        private Dictionary<Tuple<string,string>,ClassMapping> _resourceClasses = new Dictionary<Tuple<string,string>,ClassMapping>();

        // Index for easy lookup of datatypes, key is upper typenanme
        private Dictionary<string, ClassMapping> _dataTypeClasses = new Dictionary<string,ClassMapping>();

        // Index for easy lookup of classmappings, key is Type
        private Dictionary<Type, ClassMapping> _classMappingsByType = new Dictionary<Type, ClassMapping>();

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

            if (ClassMapping.IsFhirResource(type))
            {
                var mapped = ClassMapping.CreateForResource(type);
                var key = buildResourceKey(mapped.Name, mapped.Profile);
                addProps(mapped, type);
                _resourceClasses[key] = mapped;
                _classMappingsByType[type] = mapped;
            }
            else if (ClassMapping.IsFhirComplexType(type))
            {
                var mapped = ClassMapping.CreateForComplexType(type);
                var key = mapped.Name.ToUpperInvariant();
                addProps(mapped, type);
                _dataTypeClasses[key] = mapped;
                _classMappingsByType[type] = mapped;
            }
            else if (ClassMapping.IsFhirPrimitive(type))
            {
                var mapped = ClassMapping.CreateForFhirPrimitive(type);
                var key = mapped.Name.ToUpperInvariant();
                addProps(mapped, type);
                _dataTypeClasses[key] = mapped;
                _classMappingsByType[type] = mapped;
            }
            else
            {
                // Ignore this class
                Message.Info("Skipped type {0} while doing inspection: not a resource, complex type or Fhir primitive", type.Name);
            }
        }

        private void addProps(ClassMapping mapped, Type type)
        {
            var propCollection = new List<PropertyMapping>();

            foreach (var property in ReflectionHelper.FindPublicProperties(type))
            {
                var mappedProperty = Inspect(property);

                if (mappedProperty != null) propCollection.Add(mappedProperty);
            }

            mapped.AddElements(propCollection);
        }


        internal PropertyMapping Inspect(PropertyInfo property)
        {
            if (property == null) throw Error.ArgumentNull("property");

            if (Attribute.GetCustomAttribute(property, typeof(NotMappedAttribute)) != null) return null;

            PropertyMapping element = null;

            bool success = PropertyMapping.TryCreateFromProperty(property, this, out element);

            if (!success)
            {
                Message.Info("Skipped member {0} in type {1} while doing inspection: not a mappable property",
                        property.Name, property.DeclaringType.Name);
                return null;
            }

            return element;
        }


        private void checkMutualExclusiveAttributes(Type type)
        {
            if (ClassMapping.IsFhirResource(type) && ClassMapping.IsFhirComplexType(type))
                throw Error.Argument("type", "Type {0} cannot be both a Resource and a Complex datatype", type);
            if (ClassMapping.IsFhirResource(type) && ClassMapping.IsFhirPrimitive(type))
                throw Error.Argument("type", "Type {0} cannot be both a Resource and a Primitive datatype", type);
            if (ClassMapping.IsFhirComplexType(type) && ClassMapping.IsFhirPrimitive(type))
                throw Error.Argument("type", "Type {0} cannot be both a Complex and a Primitive datatype", type);
        }

        private static Tuple<string, string> buildResourceKey(string name, string profile)
        {
            var normalizedName = name.ToUpperInvariant();
            var normalizedProfile = profile != null ? profile.ToUpperInvariant() : null;

            return Tuple.Create(normalizedName, normalizedProfile);
        }

        public ClassMapping FindClassMappingForResource(string name, string profile = null)
        {
            var key = buildResourceKey(name, profile);
            var noProfileKey = buildResourceKey(name, null);

            ClassMapping entry = null;

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

        public ClassMapping FindClassMappingForFhirDataType(string name)
        {
            var key = name.ToUpperInvariant();

            ClassMapping entry = null;
            var success = _dataTypeClasses.TryGetValue(key, out entry);

            if (success)
                return entry;
            else
                return null;
        }

        public ClassMapping FindClassMappingByImplementingType(Type type)
        {
            ClassMapping entry = null;
            var success = _classMappingsByType.TryGetValue(type, out entry);

            if (success)
                return entry;
            else
                return null;
        }
    }
}
