/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Api.Properties;
using Hl7.Fhir.Support;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;


namespace Hl7.Fhir.Serialization
{
    internal class PrimitiveValueReader
    {
        private IFhirReader _current;
        private ModelInspector _inspector;

        public PrimitiveValueReader(IFhirReader data)
        {
            _current = data;
            _inspector = SerializationConfig.Inspector;
        }


        internal object Deserialize(Type nativeType)
        {
            if (nativeType == null) throw Error.ArgumentNull("nativeType");
                 
            if (_current.IsPrimitive())
            {
                return read(nativeType);
            }
            else
                throw Error.Format("Trying to read a value, but reader is not at the start of a primitive", _current);
        }


        private object read(Type nativeType)
        {
            object primitiveValue = _current.GetPrimitiveValue();
            
            if (nativeType.IsEnum() && primitiveValue.GetType() == typeof(string))
            {
                var enumMapping = _inspector.FindEnumMappingByType(nativeType);

                if (enumMapping != null)
                    return enumMapping.ParseLiteral((string)primitiveValue);
            }

            try
            {
                return PrimitiveTypeConverter.Convert(primitiveValue, nativeType);
            }
            catch (NotSupportedException exc)
            {
                // thrown when an unsupported conversion was required
                throw Error.Format(exc.Message, _current);
            }
        }
    }

}
