using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Serialization
{
    public interface IElementSerializationInfo
    {
        string ElementName { get; }
        bool MayRepeat { get; }

        ITypeSerializationInfo[] Type { get; }
    }


    public interface ITypeSerializationInfo
    {

    }
    public interface IComplexTypeSerializationInfo : ITypeSerializationInfo
    {
        string TypeName { get; }
        IEnumerable<IElementSerializationInfo> GetChildren();
    }

    public interface ITypeReference : ITypeSerializationInfo
    {
        string ReferencedType { get; }
    }

    public interface IModelMetadataProvider
    {
        bool IsResource(string typeName);

        IComplexTypeSerializationInfo GetSerializationInfoForType(string typeName);
    }
}
