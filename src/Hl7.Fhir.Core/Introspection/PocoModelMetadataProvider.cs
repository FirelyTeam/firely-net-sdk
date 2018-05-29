/* 
 * Copyright (c) 2018, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hl7.Fhir.Introspection
{
    public class PocoModelMetadataProvider : IModelMetadataProvider
    {
        public static IComplexTypeSerializationInfo GetSerializationInfoForType(string typeName)
        {
            Type csType = ModelInfo.GetTypeForFhirType(typeName);
            if (csType == null) return null;

            var classMapping = GetMappingForType(csType);
            if (classMapping == null) return null;

            return new PocoComplexTypeSerializationInfo(classMapping);
        }

        IComplexTypeSerializationInfo IModelMetadataProvider.GetSerializationInfoForType(string typeName) => GetSerializationInfoForType(typeName);

        internal static ClassMapping GetMappingForType(Type elementType)
        {
            var inspector = Serialization.BaseFhirParser.Inspector;
            return inspector.ImportType(elementType);
        }

        public bool IsResource(string typeName) => ModelInfo.IsKnownResource(typeName) || typeName == "Resource" || typeName == "DomainResource";
    }


    internal struct PocoComplexTypeSerializationInfo : IComplexTypeSerializationInfo
    {
        private readonly ClassMapping _classMapping;

        public PocoComplexTypeSerializationInfo(ClassMapping classMapping)
        {
            _classMapping = classMapping;
        }

        public string TypeName => !_classMapping.IsBackbone ? _classMapping.Name : "BackboneElement";

        public bool IsAbstract => _classMapping.IsAbstract;

        public IEnumerable<IElementSerializationInfo> GetChildren() =>
            _classMapping.PropertyMappings.Select(pm =>
            (IElementSerializationInfo)new PocoElementSerializationInfo(pm));
    }

    internal struct PocoTypeReferenceInfo : ITypeReference
    {
        private readonly string _referencedType;

        public PocoTypeReferenceInfo(string referencedType)
        {
            _referencedType = referencedType;
        }

        public string TypeName => _referencedType;
    }


    internal struct PocoElementSerializationInfo : IElementSerializationInfo
    {
        private readonly PropertyMapping _pm;
        private readonly Lazy<ITypeSerializationInfo[]> _types;

        internal PocoElementSerializationInfo(PropertyMapping pm)
        {
            _pm = pm;
            _types = new Lazy<ITypeSerializationInfo[]>(() => buildTypes(pm));
        }

        public string ElementName => _pm.Name;

        public bool MayRepeat => _pm.IsCollection;

        public bool IsSimpleElement => _pm.SerializationHint == XmlSerializationHint.Attribute;

        private static ITypeSerializationInfo[] buildTypes(PropertyMapping pm)
        {
            if (pm.IsBackboneElement)
            {
                var mapping = PocoModelMetadataProvider.GetMappingForType(pm.ImplementingType);
                return new ITypeSerializationInfo[] { new PocoComplexTypeSerializationInfo(mapping) };
            }
            else
            {              
                var names = pm.FhirType.Select(ft => getFhirTypeName(ft));
                return names.Select(n => (ITypeSerializationInfo)new PocoTypeReferenceInfo(n)).ToArray();
            }

            string getFhirTypeName(Type ft)
            {
                var map = PocoModelMetadataProvider.GetMappingForType(ft);
                return map.IsCodeOfT ? "code" : map.Name;
            }
        }

        public bool IsChoiceElement => _pm.Choice == ChoiceType.DatatypeChoice;

        public bool IsContainedResource => _pm.Choice == ChoiceType.ResourceChoice;

        public ITypeSerializationInfo[] Type => _types.Value;
    }
}
