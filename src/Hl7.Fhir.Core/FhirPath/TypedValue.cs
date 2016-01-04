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

namespace Hl7.Fhir.FhirPath
{
    internal class TypedValue : IFhirPathValue
    {
        private object _original;

        public TypedValue(object value)
        {
            _original = value;
        }

        public object Value
        {
            get
            {
                //TODO: Add support for FHIR types representing these values
                if (_original is Boolean)
                    return _original;
                if (_original is String)
                    return _original;
                if (_original is Int32 || _original is Int16 || _original is UInt16 || _original is UInt32 || _original is Int64)
                    return Convert.ToInt64(_original);
                if (_original is float || _original is double || _original is Decimal)
                    return Convert.ToDecimal(_original);
                if (_original is PartialDateTime)
                    return _original;

                throw Error.NotSupported("Cannot process values of type {0} (with value {1}) in the FhirPath engine"
                        .FormatWith(_original.GetType().Name, _original.ToString()));
            }
        }
    }
}
