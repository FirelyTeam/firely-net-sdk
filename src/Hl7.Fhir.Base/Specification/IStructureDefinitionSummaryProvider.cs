/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */


using Hl7.Fhir.Utility;
using System.Collections.Generic;

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

        /// <summary>
        /// If this modifies the meaning of other elements
        /// </summary>
        bool IsModifier { get; }

        ITypeSerializationInfo[] Type { get; }

        /// <summary>
        /// Logical Models where a choice type is represented by ElementDefinition.representation = typeAttr might define a default type (elementdefinition-defaulttype extension). null in most cases.
        /// </summary>
        string DefaultTypeName { get; }

        /// <summary>
        /// This is the namespace used for the xml node representing this element.
        /// Only need to be set if different from "http://hl7.org/fhir".
        /// </summary>
        string NonDefaultNamespace { get; }

        /// <summary>
        /// The kind of node used to represent this element in XML.
        /// Default is <see cref="XmlRepresentation.XmlElement"/>.
        /// </summary>
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

        IReadOnlyCollection<IElementDefinitionSummary> GetElements();
    }

    public interface IStructureDefinitionReference : ITypeSerializationInfo
    {
        string ReferredType { get; }
    }

    public interface IStructureDefinitionSummaryProvider
    {
        IStructureDefinitionSummary Provide(string canonical);
    }

    public static class TypeSerializationInfoExtensions
    {
        public static string GetTypeName(this ITypeSerializationInfo info)
        {
            switch (info)
            {
                case IStructureDefinitionReference tr:
                    return tr.ReferredType;
                case IStructureDefinitionSummary ct:
                    return ct.TypeName;
                default:
                    throw Error.NotSupported($"Don't know how to derive type information from type {info.GetType()}");
            }
        }

    }
}
