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
    public interface IElementSerializationInfo  // ElementDefinition
    {
        string ElementName { get; }
        bool MayRepeat { get; }

        bool IsChoiceElement { get; }
        bool IsContainedResource { get; }

        ITypeSerializationInfo[] Type { get; }

        // Attributes for XML support
        string NonDefaultNamespace { get; }
        XmlRepresentation Representation { get; }

        int Order { get; }
    }

    public interface ITypeSerializationInfo
    {
    }

    /// <summary>
    /// A class representing a complex type, with child elements. 
    /// </summary>
    /// <remarks>
    ///  In FHIR, this interface represents definitions of Resources, datatypes and BackboneElements. 
    ///  BackboneElements will always have the TypeName set to "BackboneElement" and IsAbstract to true.
    ///  </remarks>
    public interface IComplexTypeSerializationInfo : ITypeSerializationInfo
    {
        string TypeName { get; }
        bool IsAbstract { get; }

        IEnumerable<IElementSerializationInfo> GetChildren();
    }

    public interface ITypeReference : ITypeSerializationInfo
    {
        string ReferredType { get; }
    }

    public interface ISerializationInfoProvider
    {
        IComplexTypeSerializationInfo Provide(string canonical);
    }
}
