/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System;
using Hl7.Fhir.Specification;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Support.Model;
using Hl7.Fhir.Serialization;

namespace Hl7.Fhir.ElementModel
{
    internal class PrimitiveElement : ITypedElement, IElementDefinitionSummary, IStructureDefinitionSummary
    {
        public PrimitiveElement(object value, string name = null)
        {
            if (value == null)  throw new ArgumentNullException(nameof(value));

            Value = Primitives.ConvertToPrimitiveValue(value);
            InstanceType = Primitives.GetPrimitiveTypeName(value.GetType());
            Name = name ?? "@primitivevalue@";
        }

        public string Name { get; private set; }

        public object Value { get; private set; }

        public string InstanceType { get; private set; }

        public string Location => Name;

        public IElementDefinitionSummary Definition => this;

        string IElementDefinitionSummary.ElementName => Name;

        bool IElementDefinitionSummary.IsCollection => false;

        bool IElementDefinitionSummary.IsRequired => false;

        bool IElementDefinitionSummary.InSummary => false;

        bool IElementDefinitionSummary.IsChoiceElement => false;

        bool IElementDefinitionSummary.IsResource => false;


         ITypeSerializationInfo[] IElementDefinitionSummary.Type => new[] { this };

        string IElementDefinitionSummary.NonDefaultNamespace => null;

        XmlRepresentation IElementDefinitionSummary.Representation => XmlRepresentation.XmlAttr;

        int IElementDefinitionSummary.Order => 0;

        string IStructureDefinitionSummary.TypeName => InstanceType;

        bool IStructureDefinitionSummary.IsAbstract => false;

        bool IStructureDefinitionSummary.IsResource => false;

        public override bool Equals(object obj) => obj is ITypedElement ite ? Equals(ite.Value, Value) : false;

        public override int GetHashCode() => Value?.GetHashCode() ?? 0;

        public ITypedElement Clone() => new PrimitiveElement(Value);

        public IEnumerable<ITypedElement> Children(string name = null) => Enumerable.Empty<ITypedElement>();
        IReadOnlyCollection<IElementDefinitionSummary> IStructureDefinitionSummary.GetElements() => throw new NotImplementedException();

        public override string ToString() => Value != null ? PrimitiveTypeConverter.ConvertTo<string>(Value) : "";
        }
}
