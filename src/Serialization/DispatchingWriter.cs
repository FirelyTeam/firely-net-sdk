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
    internal class DispatchingWriter
    {
        private readonly IFhirWriter _current;
        private readonly ModelInspector _inspector;

        public DispatchingWriter(IFhirWriter data)
        {
            _current = data;
            _inspector = SerializationConfig.Inspector;
        }

        internal void Serialize(PropertyMapping prop, object instance, ComplexTypeWriter.SerializationMode mode)
        {
            if (prop == null) throw Error.ArgumentNull("prop");

            // ArrayMode avoid the dispatcher making nested calls into the RepeatingElementWriter again
            // when writing array elements. FHIR does not support nested arrays, and this avoids an endlessly
            // nesting series of dispatcher calls
            if (prop.IsCollection)
            {
                var elements = instance as IList;
                if (elements == null) throw Error.Argument("existing", "Can only write repeating elements from a type implementing IList");

                _current.WriteStartArray();

                foreach (var element in elements)
                    write(prop, element, mode);

                _current.WriteEndArray();
            }
            else
                write(prop, instance, mode);
        }

        private void write(PropertyMapping prop, object instance, ComplexTypeWriter.SerializationMode mode)
        {
            // If this is a primitive type, no classmappings and reflection is involved,
            // just serialize the primitive to the writer
            if (prop.IsPrimitive)
            {
                var writer = new PrimitiveValueWriter(_current);
                writer.Serialize(instance, prop.SerializationHint);
                return;
            }

            // A Choice property that contains a choice of any resource
            // (as used in Resource.contained)
            if (prop.Choice == ChoiceType.ResourceChoice)
            {
                var writer = new ResourceWriter(_current);
                writer.Serialize(instance, contained: true);
                return;
            }

            ClassMapping mapping = _inspector.ImportType(instance.GetType());

            if (mode == ComplexTypeWriter.SerializationMode.AllMembers || mode == ComplexTypeWriter.SerializationMode.NonValueElements)
            {
                var cplxWriter = new ComplexTypeWriter(_current);
                cplxWriter.Serialize(mapping, instance, mode);
            }
            else
            {
                object value = mapping.PrimitiveValueProperty.GetValue(instance);
                write(mapping.PrimitiveValueProperty, value, ComplexTypeWriter.SerializationMode.AllMembers);
            }
        }
    }
}
