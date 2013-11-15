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
    //TODO: Find out the right way to handle named resource-local component types (i.e. Patient.AnimalComponent)
    public class ModelInspector
    {
        // Index for easy lookup of resources, key is Tuple<upper resourcename, upper profile>
        private Dictionary<Tuple<string,string>,ClassMapping> _resourceClasses = new Dictionary<Tuple<string,string>,ClassMapping>();

        // Index for easy lookup of datatypes, key is upper typenanme
        private Dictionary<string, ClassMapping> _dataTypeClasses = new Dictionary<string,ClassMapping>();

        // Index for easy lookup of classmappings, key is Type
        private Dictionary<Type, ClassMapping> _classMappingsByType = new Dictionary<Type, ClassMapping>();

        // Index for easy lookup of enummappings, key is Type
        private Dictionary<Type, EnumMapping> _enumMappingsByType = new Dictionary<Type, EnumMapping>();


        // List of all imported types.
        private List<Type> _importedTypes = new List<Type>();

        public void Import(Assembly assembly)
        {
            if (assembly == null) throw Error.ArgumentNull("assembly");

            if (Attribute.GetCustomAttribute(assembly, typeof(NotMappedAttribute)) != null) return;

            foreach (Type t in assembly.GetExportedTypes()) Import(t);
        }

        public void Import(Type type)
        {
            if (type == null) throw Error.ArgumentNull("type");
            if (Attribute.GetCustomAttribute(type, typeof(NotMappedAttribute)) != null) return;

            if(!_importedTypes.Contains(type)) _importedTypes.Add(type);

            _importedTypes.Add(type);

            if (type.IsEnum)
                processEnum(type);
            else
                processType(type);
        }


        //public void Process()
        //{
        //    _resourceClasses.Clear();
        //    _dataTypeClasses.Clear();
        //    _classMappingsByType.Clear();

        //    foreach (var type in _importedTypes) processType(type);

        //    // Once all classes have been inspected, process their properties
        //    // (if you process properties before all types are inspected, you won't be able
        //    // to find the classmappings the properties may refer to) 
        //    foreach(ClassMapping mapping in _classMappingsByType.Values)
        //    {
        //        mapping.inspectProperties(this);                
        //    }

        //    Message.Info("Finished processing {0} classes. Found {1} resources, {2} complex datatypes, {3} primitive/enum datatypes",
        //        _importedTypes.Count, _resourceClasses.Count, _dataTypeClasses.Values.Count(m => m.ModelConstruct == FhirModelConstruct.ComplexType),
        //         _dataTypeClasses.Values.Count(map => map.ModelConstruct == FhirModelConstruct.PrimitiveType));
        //}

        private void processEnum(Type type)
        {
            if (Attribute.GetCustomAttribute(type, typeof(NotMappedAttribute)) != null) return;

            if (!EnumMapping.IsMappableEnum(type))
            {
                Message.Info("Skipped enum {0} while doing inspection: not recognized as representing a FHIR enumeration", type.Name);
                return;
            }

            var mapping = EnumMapping.Create(type);
            _enumMappingsByType[type] = mapping;
        }


        private void processType(Type type)
        {
            if(!ClassMapping.IsMappableType(type))
            {
                Message.Info("Skipped type {0} while doing inspection: not recognized as representing a FHIR type", type.Name);
                return;
            }

            var mapping = ClassMapping.Create(type);
            _classMappingsByType[type] = mapping;

            if (mapping.IsResource)
            {
                var key = buildResourceKey(mapping.Name, mapping.Profile);
                _resourceClasses[key] = mapping;
            }
            else
            {
                var key = mapping.Name.ToUpperInvariant();
                _dataTypeClasses[key] = mapping;
            }
        }


        private static Tuple<string, string> buildResourceKey(string name, string profile)
        {
            var normalizedName = name.ToUpperInvariant();
            var normalizedProfile = profile != null ? profile.ToUpperInvariant() : null;

            return Tuple.Create(normalizedName, normalizedProfile);
        }

        public EnumMapping FindEnumMappingByType(Type type)
        {
            if (type == null) throw Error.ArgumentNull("type");
            if (!type.IsEnum) throw Error.Argument("type", "Type {0} is not an enumeration", type.Name);

            EnumMapping entry = null;

            // Try finding a resource with the specified profile first
            var success = _enumMappingsByType.TryGetValue(type, out entry);

            if (success)
                return entry;
            else
                return null;
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

        public ClassMapping FindClassMappingByType(Type type)
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
