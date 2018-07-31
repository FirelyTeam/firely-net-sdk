/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */


using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification
{
    public interface IElementDefinitionSummary  // ElementDefinition
    {
        string ElementName { get; }
        bool IsCollection { get; }
        bool IsRequired { get; }

        bool InSummary { get; }

        bool IsChoiceElement { get; }
        bool IsResource { get; }

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
    ///  BackboneElements will have the TypeName set to "BackboneElement" (in resources) or "Element" (in datatypes)
    ///  and IsAbstract set to true.
    ///  </remarks>
    public interface IStructureDefinitionSummary : ITypeSerializationInfo
    {
        string TypeName { get; }
        bool IsAbstract { get; }
        bool IsResource { get; }

        IEnumerable<IElementDefinitionSummary> GetElements();
    }

    public interface IStructureDefinitionReference : ITypeSerializationInfo
    {
        string ReferredType { get; }
    }

    public interface IStructureDefinitionSummaryProvider
    {
        IStructureDefinitionSummary Provide(string canonical);
    }
}
