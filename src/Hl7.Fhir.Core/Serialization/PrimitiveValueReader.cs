/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model.Primitives;
using Hl7.Fhir.Utility;
using System;
using System.Diagnostics;

namespace Hl7.Fhir.Serialization
{
#pragma warning disable 612, 618
    internal class PrimitiveValueReader
    {
        private readonly ITypedElement _current;
        private readonly ModelInspector _inspector;

        public PrimitiveValueReader(ITypedElement data)
        {
            _current = data;
            _inspector = BaseFhirParser.Inspector;
        }


        internal object Deserialize(Type nativeType)
        {
            if (nativeType == null) throw Error.ArgumentNull(nameof(nativeType));

            object primitiveValue = _current.Value;

            if (nativeType.IsEnum()) return primitiveValue;

            try
            {
                if (primitiveValue is PartialDateTime || primitiveValue is PartialTime)
                    return PrimitiveTypeConverter.ConvertTo(primitiveValue.ToString(), nativeType);                    
                else
                    return PrimitiveTypeConverter.ConvertTo(primitiveValue, nativeType);
            }
            catch (NotSupportedException exc)
            {
                // thrown when an unsupported conversion was required
                ComplexTypeReader.RaiseFormatError("Not supported - " + exc.Message, _current.Location);
                throw;  // just to satisfy the compiler - RaiseFormatError throws.
            }
        }
    }
#pragma warning restore 612,618
}
