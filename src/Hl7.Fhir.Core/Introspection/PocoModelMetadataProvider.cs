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

        internal PocoElementSerializationInfo(PropertyMapping pm)
        {
            _pm = pm;
        }

        public string ElementName => _pm.Name;

        public bool MayRepeat => _pm.IsCollection;

        public ITypeSerializationInfo[] Type
        {
            get
            {
                if (_pm.ImplementingType.CanBeTreatedAsType(typeof(BackboneElement)))
                {
                    var mapping = PocoModelMetadataProvider.GetMappingForType(_pm.ImplementingType);
                    return new ITypeSerializationInfo[] { new PocoComplexTypeSerializationInfo(mapping) };
                }
                else
                {
                    var names = _pm.FhirType.Select(ft => getFhirTypeName(ft));
                    return names.Select(n => (ITypeSerializationInfo)new PocoTypeReferenceInfo(n)).ToArray();
                }

                string getFhirTypeName(Type implementingType)
                {
                    var attr = implementingType.GetTypeInfo().GetCustomAttribute<FhirTypeAttribute>();

                    //this is better, but slower
                    //if (isCodeOfT(implementingType)) return "code";
                    if (attr?.Name == "codeOfT") return "code";
                    return attr?.Name ?? implementingType.Name;
                }

                //bool isCodeOfT(Type t) => ReflectionHelper.IsClosedGenericType(t) &&
                //            ReflectionHelper.IsConstructedFromGenericTypeDefinition(t, typeof(Code<>));
            }
        }

        public bool IsChoiceElement => _pm.Choice == ChoiceType.DatatypeChoice;
    }
}
