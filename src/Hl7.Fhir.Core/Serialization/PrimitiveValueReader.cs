/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Utility;
using System;


namespace Hl7.Fhir.Serialization
{
#pragma warning disable 612, 618
    internal class PrimitiveValueReader
    {
        private readonly ISourceNode _current;
        private readonly ModelInspector _inspector;

        public PrimitiveValueReader(ISourceNode data)
        {
            _current = data;
            _inspector = BaseFhirParser.Inspector;
        }


        internal object Deserialize(Type nativeType)
        {
            if (nativeType == null) throw Error.ArgumentNull(nameof(nativeType));

            object primitiveValue = _current.Text;

            if (nativeType.IsEnum() && primitiveValue.GetType() == typeof(string))
            {
                // Don't try to parse enums in the parser -> it's been moved to the Code<T> type
                return primitiveValue;
            }

            try
            {
                return PrimitiveTypeConverter.ConvertTo(primitiveValue, nativeType);
            }
            catch (NotSupportedException exc)
            {
                // thrown when an unsupported conversion was required
                throw Error.Format(exc.Message, _current.Location);
            }
        }
    }
#pragma warning restore 612,618
}
