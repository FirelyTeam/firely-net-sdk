using Hl7.Fhir.Serialization.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Serialization
{
    public class DispatchingReader
    {
        private JToken _data;
        private ModelInspector _inspector;

        public DispatchingReader(ModelInspector inspector, JToken data)
        {
            _data = data;
            _inspector = inspector;
        }

        // no more arg polymorph -> caller (often the complex reader) must have determined type from instance by now
        // a.k.a. no repeating polymorph elements supported where every element is of a different type
        public object Deserialize(ClassMapping mapping, bool repeating, object existing=null)
        {
            if (mapping == null) throw Error.ArgumentNull("mapping");

            if(repeating)
            {
                var reader = new RepeatingElementReader(_inspector, _data);
                return reader.Deserialize(mapping, existing);
            }
            else if (mapping.ModelConstruct == FhirModelConstruct.PrimitiveType)
            {
                var reader = new PrimitiveTypeReader(_inspector, _data);
                return reader.Deserialize(mapping);
            }
            else if (mapping.ModelConstruct == FhirModelConstruct.ComplexType)
            {
                var reader = new ComplexTypeReader(_inspector, new JsonDomFhirReader(_data));
                return reader.Deserialize(mapping, existing);
            }
            else if(mapping.ModelConstruct == FhirModelConstruct.Resource)
            {
                var reader = new ResourceReader(_inspector, new JsonDomFhirReader(_data));
                return reader.Deserialize(existing);
            }
            else
                throw Error.InvalidOperation("Don't know how to handle members of type {0}", mapping.Name);
        }
    }
}
