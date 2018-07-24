/* 
 * Copyright (c) 2018, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hl7.Fhir.Introspection
{
    public class PocoSerializationInfoProvider : ISerializationInfoProvider
    {
        public IComplexTypeSerializationInfo Provide(string canonical)
        {
            var isLocalType = !canonical.Contains("/");
            var typeName = canonical;

            if(!isLocalType)
            {
                // So, we have received a canonical url, not being a relative path
                // (know resource/datatype), we -for now- only know how to get a ClassMapping
                // for this, if it's a built-in T4 generated POCO, so there's no way
                // to find a mapping for this.
                return null;
            }

            Type csType = ModelInfo.GetTypeForFhirType(typeName);
            if (csType == null) return null;

            var classMapping = GetMappingForType(csType);
            if (classMapping == null) return null;

            return new PocoComplexTypeSerializationInfo(classMapping);
        }

        internal static ClassMapping GetMappingForType(Type elementType)
        {
            var inspector = Serialization.BaseFhirParser.Inspector;
            return inspector.ImportType(elementType);
        }
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

        public PocoTypeReferenceInfo(string canonical)
        {
            _referencedType = canonical;
        }

        public string ReferredType => _referencedType;
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

        private static ITypeSerializationInfo[] buildTypes(PropertyMapping pm)
        {
            if (pm.IsBackboneElement)
            {
                var mapping = PocoSerializationInfoProvider.GetMappingForType(pm.ImplementingType);
                return new ITypeSerializationInfo[] { new PocoComplexTypeSerializationInfo(mapping) };
            }
            else
            {              
                var names = pm.FhirType.Select(ft => getFhirTypeName(ft));
                return names.Select(n => (ITypeSerializationInfo)new PocoTypeReferenceInfo(n)).ToArray();
            }

            string getFhirTypeName(Type ft)
            {
                var map = PocoSerializationInfoProvider.GetMappingForType(ft);
                return map.IsCodeOfT ? "code" : map.Name;
            }
        }

        public string ElementName => _pm.Name;

        public bool MayRepeat => _pm.IsCollection;

        public XmlRepresentation Representation => _pm.SerializationHint;

        public bool IsChoiceElement => _pm.Choice == ChoiceType.DatatypeChoice;

        public bool IsContainedResource => _pm.Choice == ChoiceType.ResourceChoice;

        public int Order => _pm.Order;

        public ITypeSerializationInfo[] Type => _types.Value;

        public string NonDefaultNamespace => null;
    }
}
