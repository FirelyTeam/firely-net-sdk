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


    public interface ITypeReferenceSerializationInfo : ITypeSerializationInfo
    {
        string TypeName { get; }
    }

    public interface IComplexTypeSerializationInfo : ITypeSerializationInfo
    {
        IEnumerable<IElementSerializationInfo> GetChildren();
    }

    public interface ITypeSerializationInfo
    {
    }

    public interface IModelMetadataProvider
    {
        bool IsResource(string typeName);

        IComplexTypeSerializationInfo GetSerializationInfoForType(string typeName);
    }
}
