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

        bool IsChoiceElement { get; }

        ITypeSerializationInfo[] Type { get; }
    }


    public class ElementSerializationInfo : IElementSerializationInfo
    {
        public ElementSerializationInfo(string elementName, bool mayRepeat, ITypeSerializationInfo[] type)
        {
            ElementName = elementName ?? throw new ArgumentNullException(nameof(elementName));
            MayRepeat = mayRepeat;
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public string ElementName { get; private set; }

        public bool MayRepeat { get; private set; }

        public ITypeSerializationInfo[] Type { get; private set; }
    }


    public interface ITypeSerializationInfo
    {
        string TypeName { get; }   
    }
    public interface IComplexTypeSerializationInfo : ITypeSerializationInfo
    {
        IEnumerable<IElementSerializationInfo> GetChildren();
    }

    public interface ITypeReference : ITypeSerializationInfo
    {
        IComplexTypeSerializationInfo Resolve();
    }

    public interface IModelMetadataProvider
    {
        bool IsResource(string typeName);

        IComplexTypeSerializationInfo GetSerializationInfoForType(string typeName);
    }
}
