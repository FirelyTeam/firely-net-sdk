using Hl7.Fhir.Introspection;
using Hl7.Fhir.Properties;
using Hl7.Fhir.Support;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Serialization
{
    public class DispatchingWriter
    {
        private readonly IFhirWriter _current;
        private readonly ModelInspector _inspector;
        private readonly bool _arrayMode;

        public DispatchingWriter(IFhirWriter data, bool arrayMode = false)
        {
            _current = data;
            _inspector = SerializationConfig.Inspector;
            _arrayMode = arrayMode;
        }

        public void Serialize(PropertyMapping prop, string memberName, object instance)
        {
            if (prop == null) throw Error.ArgumentNull("prop");

            // ArrayMode avoid the dispatcher making nested calls into the RepeatingElementWriter again
            // when writing array elements. FHIR does not support nested arrays, and this avoids an endlessly
            // nesting series of dispatcher calls
            if (!_arrayMode && prop.IsCollection)
            {
                var writer = new RepeatingElementWriter(_current);
                writer.Serialize(prop, memberName, instance);
                return;
            }

            // If this is a primitive type, no classmappings and reflection is involved,
            // just serialize the primitive to the writer
            if(prop.IsPrimitive)
            {
                var writer = new PrimitiveValueWriter(_current);
                writer.Serialize(memberName, instance, prop.SerializationHint);
                return;
            }

            // A Choice property that contains a choice of any resource
            // (as used in Resource.contained)
            if(prop.HasAnyResourceWildcard)
            {
                var writer = new ResourceWriter(_current);
                writer.Serialize(instance);
                return;
            }

            ClassMapping mapping = _inspector.ImportType(instance.GetType());           

            var cplxWriter = new ComplexTypeWriter(_current);
            cplxWriter.Serialize(mapping, instance);
        }
    }
}
