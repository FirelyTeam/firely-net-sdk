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
    internal class PrimitiveTypeReader
    {
        private IFhirReader _current;
        private ModelInspector _inspector;

        public PrimitiveTypeReader(ModelInspector inspector, IFhirReader data)
        {
            _current = data;
            _inspector = inspector;
        }


        internal object Deserialize(ClassMapping mapping, PropertyMapping prop, object existing)
        {
            if (mapping == null) throw Error.ArgumentNull("mapping");
           
            if(mapping.ModelConstruct != FhirModelConstruct.PrimitiveType)
                throw Error.InvalidOperation("Can only handle primitive mappings");
       
            PrimitiveElement result;

            if (existing == null)
            {
                var creationType = mapping.ImplementingType;
                if (prop != null && prop.IsEnumeratedProperty)
                    creationType = mapping.ImplementingType.MakeGenericType(prop.EnumType);

                existing = BindingConfiguration.ModelClassFactories.InvokeFactory(creationType);
            }
                
            result = existing as PrimitiveElement;
            if (result == null) throw Error.Argument("existing", "Can only read primitives into subvlasses of PrimitiveElement");

            if (_current.IsPrimitive())
            {
                return read(mapping, prop, result);
            }
            else if(_current.CurrentToken == TokenType.Object)       // A complex object representing the full Fhir primitive (an Element subclass)
            {
                var reader = new ComplexTypeReader(_inspector, _current);
                return reader.Deserialize(mapping, existing);
            }
            else
                throw Error.InvalidOperation("Trying to read a primitive, but reader is not at the start of a primitive or complex (element) object");
        }


        private PrimitiveElement read(ClassMapping mapping, PropertyMapping prop, PrimitiveElement existing)
        {
            object primitiveValue = _current.GetPrimitiveValue();
            object parsedValue = null;

            if (prop.IsEnumeratedProperty)
            {
                EnumHelper.TryParseEnum(primitiveValue.ToString(), prop.EnumType, out parsedValue);
            }
            else
            {
                parsedValue = PrimitiveTypeConverter.Convert(primitiveValue, prop.);
            }

            var valueProp = ReflectionHelper.FindPublicProperty(existing.GetType(), "Value");
            valueProp.SetValue(existing, parsedValue, null);

            return existing;
        }
    }

}
