/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Hl7.Fhir.Model
{
    /// <summary>
    /// The base class for all FHIR primitives, which are defined in the datatype section of FHIR.
    /// These FHIR primitives all contain a "real" primitive value and optionally an id or one or more
    /// extensions.
    /// </summary>
    [Serializable]
    [FhirType("PrimitiveType", "http://hl7.org/fhir/StructureDefinition/PrimitiveType")]
    [DataContract]
    public abstract class PrimitiveType : DataType
    {
        /// <inheritdoc />
        public override string TypeName { get { return "PrimitiveType"; } }

        /// <summary>
        /// The value of the primitive, stored as an object. Will generally contain the same value as the
        /// `Value` property and allows the user to retrieve a primitive value regardless of actual type.
        /// </summary>
        /// <remarks>Both <c>Value</c> and <c>ObjectValue</c> may contain invalid values according to the 
        /// primitive's official domain. E.g. <c>Value</c> is a <c>string</c> for <see cref="FhirDateTime"/>,
        /// and may contain illegally formatted values. Additionally, the deserializers will use this property
        /// to store the original serialized string form of the value in the wire format when a parsing error is
        /// encountered.</remarks>
        public object? ObjectValue { get; set; }

        /// <inheritdoc/>
        public override string? ToString()
        {
            // The primitive can exist without a value (when there is an extension present)
            // so we need to be able to handle when there is no extension present
            return ObjectValue is null ? null : PrimitiveTypeConverter.ConvertTo<string>(ObjectValue);
        }

        /// <inheritdoc/>
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            if (other is PrimitiveType dest)
            {
                base.CopyTo(dest);
                if (ObjectValue != null) dest.ObjectValue = ObjectValue;
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", nameof(other));
        }

        /// <inheritdoc/>
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo((IDeepCopyable)Activator.CreateInstance(GetType())!);
        }

        /// <inheritdoc/>
        public override bool Matches(IDeepComparable other) => IsExactly(other);

        /// <inheritdoc/>
        public override bool IsExactly(IDeepComparable other)
        {
            if (other is not PrimitiveType otherT) return false;

            if (!base.IsExactly(other)) return false;

            var otherValue = otherT.ObjectValue;

            if (ObjectValue is byte[] bytes && otherValue is byte[] bytesOther)
                return Enumerable.SequenceEqual(bytes, bytesOther);
            else
                return Equals(ObjectValue, otherT.ObjectValue);
        }

        /// <inheritdoc/>
        [IgnoreDataMember]
        public override IEnumerable<Base> Children => base.Children;

        /// <inheritdoc/>
        [IgnoreDataMember]
        public override IEnumerable<ElementValue> NamedChildren => base.NamedChildren;

        /// <inheritdoc/>
        protected override bool TryGetValue(string key, out object? value)
        {
            if (key == "value")
            {
                value = ObjectValue;
                return value is not null;
            }
            else
                return base.TryGetValue(key, out value);
        }

        /// <inheritdoc/>
        protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
        {
            if (ObjectValue is not null) yield return new KeyValuePair<string, object>("value", ObjectValue);
            foreach (var kvp in base.GetElementPairs()) yield return kvp;
        }

        /// <summary>
        /// Returns true if the primitive has any child elements (currently in FHIR this can
        /// be only the element id and zero or more extensions).
        /// </summary>
        public bool HasElements => ElementId is not null || Extension?.Any() == true;
    }
}

#nullable restore