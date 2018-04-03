using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.ElementModel
{
    public class PocoSerializationInfoProvider : IModelMetadataProvider
    {
        private static Hl7.Fhir.Introspection.ClassMapping GetMappingForType(Type elementType)
        {
            var inspector = Serialization.BaseFhirParser.Inspector;
            return inspector.ImportType(elementType);
        }

        public IComplexTypeSerializationInfo GetSerializationInfoForType(string typeName)
        {
            Type csType = ModelInfo.GetTypeForFhirType(typeName);
            if (csType == null) return null;

            var classMapping = GetMappingForType(csType);
            if (classMapping == null) return null;

            return new PocoComplexTypeSerializationInfo(classMapping);
        }

        public bool IsResource(string typeName) => ModelInfo.IsKnownResource(typeName);
    }

    internal struct PocoComplexTypeSerializationInfo : IComplexTypeSerializationInfo
    {
        private readonly ClassMapping _classMapping;

        public PocoComplexTypeSerializationInfo(ClassMapping classMapping)
        {
            _classMapping = classMapping;
        }

        public string TypeName => _classMapping.Name;

        public IEnumerable<IElementSerializationInfo> GetChildren() => _classMapping.PropertyMappings.Select(pm => (IElementSerializationInfo)new PocoElementSerializationInfo(pm));
    }

    internal struct PocoElementSerializationInfo : IElementSerializationInfo
    {
        private readonly PropertyMapping _pm;

        public PocoElementSerializationInfo(PropertyMapping pm)
        {
            _pm = pm;
        }

        public string ElementName => _pm.Name;

        public bool MayRepeat => _pm.IsCollection;

        public IComplexTypeSerializationInfo[] Type
        {
            get
            {
                var types = (_pm.ChoiceTypes?.Any() == true) ? _pm.ChoiceTypes : new[] { _pm.ElementType };
                return types.Select(tp => createSerializationInfoForMember(tp)).ToArray();

                IComplexTypeSerializationInfo createSerializationInfoForMember(Type tp)
                {
                    var inspector = Serialization.BaseFhirParser.Inspector;
                    var classMapping = inspector.ImportType(tp);

                    return new PocoComplexTypeSerializationInfo(classMapping);
                }
            }
        }

    }
}
