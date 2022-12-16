/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable annotations
#nullable disable warnings


using Hl7.Fhir.Language;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.ElementModel
{
    internal class PrimitiveElement : ITypedElement, IElementDefinitionSummary, IStructureDefinitionSummary
    {
        // [20190827 EK] Hack, allow a Quantity as a "primitive" value in ITypedElement.Value for now, so
        // we can at least continue to integrate the changes from the dead branch into 2.0
        // We need to have Quantity implement ITypedElement itself.
        internal static PrimitiveElement ForQuantity(P.Quantity value)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));

            return new PrimitiveElement(value, TypeSpecifier.Quantity.FullName, "@QuantityAsPrimitiveValue@");
        }

        private PrimitiveElement(object value, string instanceType, string name)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
            InstanceType = instanceType ?? throw new ArgumentNullException(nameof(instanceType));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public PrimitiveElement(object value, string? name = null, bool useFullTypeName = false)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            var systemType = TypeSpecifier.ForNativeType(value.GetType());
            if (!TypeSpecifier.PrimitiveTypes.Contains(systemType))
                throw new ArgumentException("The supplied value cannot be represented with a System primitive.", nameof(value));

            if (!ElementNode.TryConvertToElementValue(value, out object? convertedValue))
                throw new NotSupportedException($"There is no known System type corresponding to the .NET type {value.GetType().Name} of this instance (with value '{value}').");

            Value = convertedValue!;
            InstanceType = useFullTypeName ? systemType.FullName : systemType.Name;
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

        /// <inheritdoc/>
        bool IElementDefinitionSummary.IsModifier => false;

        bool IElementDefinitionSummary.IsChoiceElement => false;

        bool IElementDefinitionSummary.IsResource => false;

        string? IElementDefinitionSummary.DefaultTypeName => null;

        ITypeSerializationInfo[] IElementDefinitionSummary.Type => new[] { this };

        string? IElementDefinitionSummary.NonDefaultNamespace => null;

        XmlRepresentation IElementDefinitionSummary.Representation => XmlRepresentation.XmlAttr;

        int IElementDefinitionSummary.Order => 0;

        string IStructureDefinitionSummary.TypeName => InstanceType;

        bool IStructureDefinitionSummary.IsAbstract => false;

        bool IStructureDefinitionSummary.IsResource => false;

        public override bool Equals(object obj) => obj is ITypedElement ite && Equals(ite.Value, Value);

        public override int GetHashCode() => Value?.GetHashCode() ?? 0;

        public ITypedElement Clone() => new PrimitiveElement(Value);

        public IEnumerable<ITypedElement> Children(string? name = null) => Enumerable.Empty<ITypedElement>();
        IReadOnlyCollection<IElementDefinitionSummary> IStructureDefinitionSummary.GetElements() =>
            new List<IElementDefinitionSummary>();

        public override string ToString() => Value != null ? PrimitiveTypeConverter.ConvertTo<string>(Value) : "";

    }
}
