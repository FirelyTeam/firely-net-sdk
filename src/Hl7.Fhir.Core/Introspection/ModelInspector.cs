/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hl7.Fhir.Introspection
{
    //TODO: Find out the right way to handle named resource-local component types (i.e. Patient.AnimalComponent)
    public class ModelInspector
    {
        // Index for easy lookup of resources, key is Tuple<version, upper resourcename, upper profile>
        private readonly Dictionary<Tuple<Model.Version, string, string>, ClassMapping> _resourceClasses = new Dictionary<Tuple<Model.Version, string, string>, ClassMapping>();

        // Index for easy lookup of datatypes, key is Tuple<version, upper typenanme>
        private readonly Dictionary<Tuple<Model.Version, string>, List<ClassMapping>> _dataTypeClasses = new Dictionary<Tuple<Model.Version, string>, List<ClassMapping>>();

        // Index for easy lookup of classmappings, key is Type
        private readonly Dictionary<Type, ClassMapping> _classMappingsByType = new Dictionary<Type, ClassMapping>();

        public void Import(Assembly assembly)
        {
            if (assembly == null) throw Error.ArgumentNull(nameof(assembly));

            if (assembly.GetCustomAttribute<NotMappedAttribute>() != null) return;

            IEnumerable<Type> exportedTypes = assembly.ExportedTypes;

            foreach (Type type in exportedTypes)
            {
                // Don't import types marked with [NotMapped]

                if (type.GetTypeInfo().GetCustomAttribute<NotMappedAttribute>() != null) continue;

                // Map a Fhir Datatype
                if (ClassMapping.IsMappableType(type))
                    ImportType(type);
                else
                    Message.Info("Skipped type {0} while doing inspection: not recognized as representing a FHIR type", type.Name);
            }
        }


        private object lockObject = new object();

        public ClassMapping ImportType(Type type)
        {
            ClassMapping mapping = null;

            lock (lockObject)
            {
                mapping = FindClassMappingByType(type);
                if (mapping != null) return mapping;

                if (!ClassMapping.IsMappableType(type))
                    throw Error.Argument(nameof(type), "Type {0} is not a mappable Fhir datatype or resource".FormatWith(type.Name));

                mapping = ClassMapping.Create(type);
                _classMappingsByType[type] = mapping;
                Message.Info("Created Class mapping for newly encountered type {0} (FHIR type {1})", type.Name, mapping.Name);

                if (mapping.IsResource)
                {
                    var key = buildResourceKey(mapping.Version, mapping.Name, mapping.Profile);
                    _resourceClasses.Add( key, mapping );
                }
                else
                {
                    // The same type name (eg Quantity) can correspond to multiple data types (eg SimpleQuantity, Money, Duration etc)
                    var key = Tuple.Create(mapping.Version, mapping.Name.ToUpperInvariant());
                    if (_dataTypeClasses.TryGetValue(key, out var mappings ))
                    {
                        mappings.Add(mapping);
                    }
                    else
                    {
                        _dataTypeClasses.Add(key, new List<ClassMapping> { mapping });
                    }
                }
            }

            return mapping;
        }


        private static Tuple<Model.Version, string, string> buildResourceKey(Model.Version version, string name, string profile)
        {
            var normalizedName = name.ToUpperInvariant();
            var normalizedProfile = profile != null ? profile.ToUpperInvariant() : null;

            return Tuple.Create(version, normalizedName, normalizedProfile);
        }

        public ClassMapping FindClassMappingForResource(Model.Version version, string name, string profile = null)
        {
            var key = buildResourceKey(version, name, profile);

            // Try finding a resource with the specified version & profile first
            if (_resourceClasses.TryGetValue(key, out var entry))
                return entry;

            // If that didn't work, try again for the common (all versions) resources
            var allVersionsKey = buildResourceKey(Model.Version.All, name, profile);
            if (_resourceClasses.TryGetValue(allVersionsKey, out entry))
                return entry;

            if (profile != null)
            {
                // If even that didn't work and we were looking for a specific profile, try again with no profile
                var noProfileKey = buildResourceKey(version, name, null);
                if (_resourceClasses.TryGetValue(noProfileKey, out entry))
                    return entry;

                // Finally, try for the comman (all versions) resources without a profile
                var noProfileAllVersionsKey = buildResourceKey(Model.Version.All, name, null);
                if (_resourceClasses.TryGetValue(noProfileAllVersionsKey, out entry))
                    return entry;
            }

            return null;
        }

        public ClassMapping FindClassMappingForFhirDataType(Model.Version version, string name, Type[] allowedTypes)
        {
            var allMappings = new List<ClassMapping>();
            var key = Tuple.Create(version, name.ToUpperInvariant());
            if (_dataTypeClasses.TryGetValue(key, out var versionMappings))
            {
                var allowedMapping = FindMapping(versionMappings);
                if (allowedMapping != null)
                {
                    return allowedMapping;
                }
                allMappings.AddRange(versionMappings);
            }

            var allVersionsKey = Tuple.Create(Model.Version.All, name.ToUpperInvariant());
            if (_dataTypeClasses.TryGetValue(allVersionsKey, out var allVersionsMappings))
            {
                var allowedMapping = FindMapping(allVersionsMappings);
                if (allowedMapping != null)
                {
                    return allowedMapping;
                }
                allMappings.AddRange(allVersionsMappings);
            }

            if (allMappings.Count == 1)
            {
                // We did not find a match, but there is only one type, so use that
                return allMappings[0];
            }

            return null;

            ClassMapping FindMapping( List<ClassMapping> mappings )
            {
                // If we know what the allowed types are look for one of those...
                if (allowedTypes == null || !allowedTypes.Any())
                {
                    return mappings.FirstOrDefault(mapping => mapping.NativeType.Name == name);
                }
                // Otherwise look for the type with the specified name - so that we use the Quantity overall type instead of the specific variants like SimpleQuantity
                return mappings.FirstOrDefault(mapping => allowedTypes.Contains(mapping.NativeType));
            }
        }

        public ClassMapping FindClassMappingByType(Model.Version version, string typeName)
        {
            var result = FindClassMappingForResource(version, typeName);
            if (result != null) return result;

            return FindClassMappingForFhirDataType(version, typeName, null);
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
                return FindClassMappingForResource(entry.Version, entry.Name, entry.Profile);
            }
            else
                return entry;   // NB: no extra lookup for non-resource types
        }
    }

}
