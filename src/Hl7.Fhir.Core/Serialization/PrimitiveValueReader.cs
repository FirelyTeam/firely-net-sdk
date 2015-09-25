/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
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
                 
            object primitiveValue = _current.GetPrimitiveValue();
            
            if (nativeType.IsEnum() && primitiveValue.GetType() == typeof(string))
            {
                var enumMapping = _inspector.FindEnumMappingByType(nativeType);

                if (enumMapping != null)
                {
                    var enumLiteral = (string)primitiveValue;
                    if (enumMapping.ContainsLiteral(enumLiteral))
                        return enumMapping.ParseLiteral((string)primitiveValue);
                    else
                        throw Error.Format("Literal {0} is not a valid value for enumeration {1}", _current, enumLiteral, enumMapping.Name);
                }
                else
                    throw Error.Format("Cannot find an enumeration mapping for enum " + nativeType.Name, _current);
            }

            try
            {
                return PrimitiveTypeConverter.ConvertTo(primitiveValue, nativeType);
            }
            catch (NotSupportedException exc)
            {
                // thrown when an unsupported conversion was required
                throw Error.Format(exc.Message, _current);
            }
        }
    }

}
