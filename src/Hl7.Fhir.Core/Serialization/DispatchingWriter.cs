/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
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
        private readonly IFhirWriter _writer;
        private readonly ModelInspector _inspector;

        public DispatchingWriter(IFhirWriter data)
        {
            _writer = data;
            _inspector = BaseFhirParser.Inspector;
        }

        internal void Serialize(PropertyMapping prop, object instance, Rest.SummaryType summary, ComplexTypeWriter.SerializationMode mode)
        {
            if (prop == null) throw Error.ArgumentNull(nameof(prop));

            // ArrayMode avoid the dispatcher making nested calls into the RepeatingElementWriter again
            // when writing array elements. FHIR does not support nested arrays, and this avoids an endlessly
            // nesting series of dispatcher calls
            if (prop.IsCollection)
            {
                var elements = instance as IList;
                if (elements == null) throw Error.Argument(nameof(elements), "Can only write repeating elements from a type implementing IList");

                _writer.WriteStartArray();

                foreach (var element in elements)
                {
                    if (element != null)
                        write(prop, element, summary, mode);
                }

                _writer.WriteEndArray();
            }
            else
                write(prop, instance, summary, mode);
        }

        private void write(PropertyMapping property, object instance, Rest.SummaryType summary, ComplexTypeWriter.SerializationMode mode)
        {
            // If this is a primitive type, no classmappings and reflection is involved,
            // just serialize the primitive to the writer
            if (property.IsPrimitive)
            {
                var writer = new PrimitiveValueWriter(_writer);
                writer.Serialize(instance, property.SerializationHint);
                return;
            }

            // A Choice property that contains a choice of any resource
            // (as used in Resource.contained)
            if (property.Choice == ChoiceType.ResourceChoice)
            {
                var writer = new ResourceWriter(_writer);
                writer.Serialize(instance, summary, contained: true);
                return;
            }

            ClassMapping mapping = _inspector.ImportType(instance.GetType());

            summary = summary == Rest.SummaryType.Text && property.IsMandatoryElement && mapping.HasPrimitiveValueMember
                ? Rest.SummaryType.False
                : summary;

            if (mode == ComplexTypeWriter.SerializationMode.AllMembers || mode == ComplexTypeWriter.SerializationMode.NonValueElements)
            {
                var complexWriter = new ComplexTypeWriter(_writer);
                complexWriter.Serialize(mapping, instance, summary, mode);
            }
            else
            {
                object value = mapping.PrimitiveValueProperty.GetValue(instance);
                write(mapping.PrimitiveValueProperty, value, summary, ComplexTypeWriter.SerializationMode.AllMembers);
            }
        }
    }
}
