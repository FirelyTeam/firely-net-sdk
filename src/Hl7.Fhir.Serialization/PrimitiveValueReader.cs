using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization.Properties;
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

        public PrimitiveValueReader(ModelInspector inspector, IFhirReader data)
        {
            _current = data;
            _inspector = inspector;
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
            object parsedValue = null;
            
            if (nativeType.IsEnum)
            {
                EnumHelper.TryParseEnum(primitiveValue.ToString(), nativeType, out parsedValue);
            }
            else
            {
                var valueType = nativeType;
                parsedValue = PrimitiveTypeConverter.Convert(primitiveValue, valueType);
            }

            return parsedValue;
        }
    }

}
