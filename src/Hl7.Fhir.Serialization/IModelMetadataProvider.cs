/*  
* Copyright (c) 2018, Furore (info@furore.com) and contributors 
* See the file CONTRIBUTORS for details. 
*  
* This file is licensed under the BSD 3-Clause license 
* available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE 
*/


using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Serialization
{
    public interface IElementSerializationInfo
    {
        string ElementName { get; }
        bool MayRepeat { get; }

        bool IsChoiceElement { get; }

        ITypeSerializationInfo[] Type { get; }
    }

    public static class IElementSerializationInfoExceptions
    {
        public static bool IsBackboneElement(this IElementSerializationInfo info) =>
            info.Type[0] is IComplexTypeSerializationInfo;
    }


    public interface ITypeSerializationInfo
    {
        string TypeName { get; }   
    }

    public interface IComplexTypeSerializationInfo : ITypeSerializationInfo
    {
        bool IsAbstract { get; }
        IEnumerable<IElementSerializationInfo> GetChildren();
    }

    public interface ITypeReference : ITypeSerializationInfo
    {
    }

    public interface IModelMetadataProvider
    {
        bool IsResource(string typeName);

        IComplexTypeSerializationInfo GetSerializationInfoForType(string typeName);
    }
}
