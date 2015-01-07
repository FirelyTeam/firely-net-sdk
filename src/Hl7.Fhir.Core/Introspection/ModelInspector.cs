/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hl7.Fhir.Introspection
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

        public void Import(Assembly assembly)
        {
            if (assembly == null) throw Error.ArgumentNull("assembly");

#if PORTABLE45
			if (assembly.GetCustomAttribute<NotMappedAttribute>() != null) return;
#else
            if (Attribute.GetCustomAttribute(assembly, typeof(NotMappedAttribute)) != null) return;
#endif

#if PORTABLE45
			IEnumerable<Type> exportedTypes = assembly.ExportedTypes;
#else
			Type[] exportedTypes = assembly.GetExportedTypes();
#endif
			foreach (Type type in exportedTypes)
            {
                // Don't import types marked with [NotMapped]
#if PORTABLE45
				if (type.GetTypeInfo().GetCustomAttribute<NotMappedAttribute>() != null) continue;
#else
                if (Attribute.GetCustomAttribute(type, typeof(NotMappedAttribute)) != null) continue;
#endif

				if (type.IsEnum())
                {
                    // Map an enumeration
                    if (EnumMapping.IsMappableEnum(type))
                        ImportEnum(type);
                    else
                        Message.Info("Skipped enum {0} while doing inspection: not recognized as representing a FHIR enumeration", type.Name);
                }
                else
                {
                    // Map a Fhir Datatype
                    if (ClassMapping.IsMappableType(type))
                        ImportType(type);
                    else
                        Message.Info("Skipped type {0} while doing inspection: not recognized as representing a FHIR type", type.Name);
                }
            }
        }


        private object lockObject = new object();

        internal EnumMapping ImportEnum(Type type)
        {
            EnumMapping mapping = null;

            if (!EnumMapping.IsMappableEnum(type))
                throw Error.Argument("type", "Type {0} is not a mappable enumeration", type.Name);

            lock (lockObject)
            {
                mapping = FindEnumMappingByType(type);
                if (mapping != null) return mapping;

                mapping = EnumMapping.Create(type);
                _enumMappingsByType[type] = mapping;

                Message.Info("Created Enum mapping for newly encountered type {0}", type.Name);
            }

            return mapping;
        }


        internal ClassMapping ImportType(Type type)
        {
            ClassMapping mapping = null;

            if(!ClassMapping.IsMappableType(type))
                throw Error.Argument("type", "Type {0} is not a mappable Fhir datatype or resource", type.Name);

            lock (lockObject)
            {
                mapping = FindClassMappingByType(type);
                if (mapping != null) return mapping;

                mapping = ClassMapping.Create(type);
                _classMappingsByType[type] = mapping;
                Message.Info("Created Class mapping for newly encountered type {0} (FHIR type {1})", type.Name, mapping.Name);

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

            return mapping;
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
            if (!type.IsEnum()) throw Error.Argument("type", "Type {0} is not an enumeration", type.Name);

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

        public ClassMapping FindClassMappingByType(string typeName)
        {
            var result = FindClassMappingForResource(typeName);
            if (result != null) return result;

            return FindClassMappingForFhirDataType(typeName);
        }

        public ClassMapping FindClassMappingByType(Type type)
        {
            ClassMapping entry = null;
            var success = _classMappingsByType.TryGetValue(type, out entry);

            if (!success) return null;

            // Do an extra lookup via this mapping's name when this is a Resource. This will find possible
            // replacement mappings, when a later import for the same Fhir typename
            // was found.
            if (entry.IsResource)
            {
                return FindClassMappingForResource(entry.Name, entry.Profile);
            }
            else
                return entry;   // NB: no extra lookup for non-resource types
        }
    }

}
