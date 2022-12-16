/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Specification
{
    public class ElementDefinitionSummary : IElementDefinitionSummary
    {
        private ElementDefinitionSummary() { }

        public ElementDefinitionSummary(string elementName, bool isCollection, bool isChoice,
            bool isResource, XmlRepresentation representation, ITypeSerializationInfo[] type,
            int order, string nonDefaultNS, bool inSummary, bool isRequired)
            : this(elementName, isCollection, isChoice, isResource, representation, null, type, order, nonDefaultNS, inSummary, isRequired) { }

        public ElementDefinitionSummary(string elementName, bool isCollection, bool isChoice,
            bool isResource, XmlRepresentation representation, string defaultType, ITypeSerializationInfo[] type,
            int order, string nonDefaultNS, bool inSummary, bool isRequired)
        {
            ElementName = elementName ?? throw new ArgumentNullException(nameof(elementName));
            IsCollection = isCollection;
            IsChoiceElement = isChoice;
            IsResource = isResource;
            Representation = representation;
            DefaultTypeName = defaultType;
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Order = order;
            NonDefaultNamespace = nonDefaultNS;
            InSummary = inSummary;
            IsRequired = isRequired;
        }

        public ElementDefinitionSummary(IElementDefinitionSummary source)
        {
            ElementName = source.ElementName;
            IsCollection = source.IsCollection;
            IsChoiceElement = source.IsChoiceElement;
            IsResource = source.IsResource;
            Representation = source.Representation;
            DefaultTypeName = source.DefaultTypeName;
            Type = source.Type;
            Order = source.Order;
            NonDefaultNamespace = source.NonDefaultNamespace;
            InSummary = source.InSummary;
            IsModifier = source.IsModifier;
            IsRequired = source.IsRequired;
        }

        public static ElementDefinitionSummary ForRoot(IStructureDefinitionSummary rootType, string rootName = null) =>
            new ElementDefinitionSummary(rootName ?? rootType.TypeName, isCollection: false, isChoice: false,
                isResource: rootType.IsResource,
                representation: XmlRepresentation.XmlElement,
                defaultType: null,
                type: new[] { rootType }, order: 0, nonDefaultNS: null, inSummary: true, isRequired: false);

        public string ElementName { get; private set; }

        public bool IsCollection { get; private set; }

        public bool IsChoiceElement { get; private set; }
        public bool IsResource { get; private set; }

        public bool IsRequired { get; private set; }

        public bool InSummary { get; private set; }

        /// <inheritdoc/>
        public bool IsModifier { get; private set; }

        public XmlRepresentation Representation { get; private set; }

        public int Order { get; private set; }
        public string DefaultTypeName { get; private set; }
        public ITypeSerializationInfo[] Type { get; private set; }

        public string NonDefaultNamespace { get; }

    }


    public static class ElementSerializationInfoExtensions
    {
        public static ElementDefinitionSummary GetElementDefinitionSummary(this IAnnotated ann) =>
            ann.TryGetAnnotation<ElementDefinitionSummary>(out var rt) ? rt : null;
    }


}
