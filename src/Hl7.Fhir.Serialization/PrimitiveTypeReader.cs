using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;


namespace Hl7.Fhir.Serialization
{
    internal class PrimitiveTypeReader
    {
        private JToken _data;
        private ModelInspector _inspector;

        public PrimitiveTypeReader(ModelInspector inspector, JToken data)
        {
            _data = data;
            _inspector = inspector;
        }


        internal object Deserialize(ClassMapping mapping)
        {
            if (mapping == null) throw Error.ArgumentNull("mapping");
           
            if(mapping.ModelConstruct != FhirModelConstruct.PrimitiveType)
                throw Error.InvalidOperation("Can only handle primitive mappings");

            //TODO: is 'existing' compatible with the mapping?

            if (_data is JValue)
            {
                //TODO: For now, we create primitives using each primitive classes' Parse() method
                //if (existing == null)
                //    existing = BindingConfiguration.ModelClassFactories.InvokeFactory(mapping.ImplementingType);

                return read(mapping, (JValue)_data);
            }
            else
                throw Error.InvalidOperation("Trying to read a primitive, but reader is not at the start of an object");
        }


        private object read(ClassMapping mapping, JValue source)
        {
            object primitiveValue;

            if (source.Type == JTokenType.Integer)
                primitiveValue = (int)source.Value;
            else if (source.Type == JTokenType.Float)
                primitiveValue = (float)source.Value;
            else if (source.Type == JTokenType.Boolean)
                primitiveValue = (bool)source.Value;
            else if (source.Type == JTokenType.String)
                primitiveValue = (string)source.Value;
            else
                throw Error.InvalidOperation("Encountered a json primitive of type {0} while only string, boolean and number are allowed", source.Type);

            // Fix for stupid dotnet conversion true => "True"
            if (primitiveValue is bool)
                primitiveValue = (bool)primitiveValue ? "true" : "false";

            // anyway, we use the Parse() function on the primitive types, which takes a string...
            if (mapping.Name.StartsWith("Code`1") || mapping.Name.StartsWith("codeOfT"))
                return null;
            else
                return mapping.Parse(primitiveValue.ToString());

            /* TODO: map the values to one of the datatypes
                boolean	
                integer	
                decimal	
                base64Binary
                instant
                string
                uri
                date
                dateTime
                code
                oid	      
                uuid
                id 
             */
        }
    }

}
