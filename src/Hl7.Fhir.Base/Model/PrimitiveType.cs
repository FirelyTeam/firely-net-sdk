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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;

namespace Hl7.Fhir.Model
{
    public partial class PrimitiveType
    {
        /// <summary>
        /// The value of the primitive, stored as an object. Will generally contain the same value as the
        /// `Value` property and allows the user to retrieve a primitive value regardless of actual type.
        /// </summary>
        /// <remarks>Both <c>Value</c> and <c>ObjectValue</c> may contain invalid values according to the 
        /// primitive's official domain. E.g. <c>Value</c> is a <c>string</c> for <see cref="FhirDateTime"/>,
        /// and may contain illegally formatted values. Additionally, the deserializers will use this property
        /// to store the original serialized string form of the value in the wire format when a parsing error is
        /// encountered.</remarks>

        private object? _objectValue;
        public object? ObjectValue
        {
            get => _objectValue;

            set
            {
                if (!ReferenceEquals(value, _objectValue))
                {
                    _objectValue = value;
                    OnObjectValueChanged();
                }
            }
        }

        protected virtual void OnObjectValueChanged()
        {
        }

        /// <inheritdoc/>
        public override string? ToString()
        {
            // The primitive can exist without a value (when there is an extension present)
            // so we need to be able to handle when there is no extension present
            return ObjectValue is null ? null : PrimitiveTypeConverter.ConvertTo<string>(ObjectValue);
        }

        /// <inheritdoc/>
        protected override bool TryGetValue(string key, [NotNullWhen(true)] out object? value)
        {
            if (key == "value")
            {
                value = ObjectValue;
                return value is not null;
            }
            else
                return base.TryGetValue(key, out value);
        }

        protected override Base SetValue(string key, object? value)
        {
            switch (key)
            {
                case "value":
                    ObjectValue = value;
                    return this;
                default:
                    return base.SetValue(key, value);
            }
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