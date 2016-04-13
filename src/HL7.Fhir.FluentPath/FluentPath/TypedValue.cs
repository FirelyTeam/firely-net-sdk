/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.FluentPath
{
    internal class ConstantValue : IFluentPathValue
    {
        private object _original;

        public ConstantValue(object value)
        {
            _original = value;

            if (_original is Boolean)
                Value = _original;
            else if (_original is String)
                Value = _original;
            else if (_original is Uri)
                Value = ((Uri)_original).OriginalString;
            else if (_original is Int32 || _original is Int16 || _original is UInt16 || _original is UInt32 || _original is Int64 || _original is UInt64)
                Value = Convert.ToInt64(_original);
            else if (_original is float || _original is double || _original is Decimal)
                Value = Convert.ToDecimal(_original);
            else if (_original is DateTimeOffset)
                Value = PartialDateTime.FromDateTime((DateTimeOffset)_original);
            else if (_original is DateTime)
                Value = PartialDateTime.FromDateTime((DateTime)_original);
            else if (_original is PartialDateTime)
                Value = _original;
            else if (_original is Time)
                Value = _original;
            else
                throw Error.NotSupported("Don't know how to convert an instance of .NET type {0} (with value '{1}') to a FluentPath constant"
                    .FormatWith(_original.GetType().Name, _original.ToString()));
        }

        public object Value
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is IFluentPathValue)
                return Object.Equals((obj as IFluentPathValue).Value,Value);
            else
                return false;
        }

        public override int GetHashCode()
        {
            if (Value != null)
                return Value.GetHashCode();
            else
                return 0;
        }
    }
}
