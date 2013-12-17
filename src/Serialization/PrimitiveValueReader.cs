using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Properties;
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
                throw Error.InvalidOperation("Trying to read a value, but reader is not at the start of a primitive");
        }


        private object read(Type nativeType)
        {
            object primitiveValue = _current.GetPrimitiveValue();
            
            if (nativeType.IsEnum && primitiveValue.GetType() == typeof(string))
            {
                var enumMapping = _inspector.FindEnumMappingByType(nativeType);

                if (enumMapping != null)
                    return enumMapping.ParseLiteral((string)primitiveValue);
            }

            return PrimitiveTypeConverter.Convert(primitiveValue, nativeType);
        }
    }

}
