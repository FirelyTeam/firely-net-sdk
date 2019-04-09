/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using System;

namespace Hl7.Fhir.Specification
{
    public class TypeRootDefinitionSummary : IElementDefinitionSummary
    {
        public TypeRootDefinitionSummary(IStructureDefinitionSummary rootType, string elementName=null)
        {
            if (rootType == null) throw new ArgumentNullException(nameof(rootType));

            ElementName = elementName ?? rootType.TypeName;
            IsResource = rootType.IsResource;
            Type = new[] { rootType };
        }

        public string ElementName { get; private set; }

        public bool IsCollection => false;

        public bool IsChoiceElement => false;
        public bool IsResource { get; private set; }

        public bool IsRequired => false;

        public bool InSummary => true;
        public XmlRepresentation Representation => XmlRepresentation.XmlElement;

        public int Order => 0;

        public ITypeSerializationInfo[] Type { get; private set; }

        public string NonDefaultNamespace => null;
    }
}
