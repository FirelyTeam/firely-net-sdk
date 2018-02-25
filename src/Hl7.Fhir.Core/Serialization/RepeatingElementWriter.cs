/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Utility;
using System.Collections;


namespace Hl7.Fhir.Serialization
{
    internal class RepeatingElementWriter
    {
        private IFhirWriter _writer;
        private ModelInspector _inspector;

        public ParserSettings Settings { get; private set; }

        public RepeatingElementWriter(IFhirWriter writer, ParserSettings settings)
        {
            _writer = writer;
            _inspector = BaseFhirParser.Inspector;
            Settings = settings;
        }

        public void Serialize(PropertyMapping prop, object instance, Rest.SummaryType summary, ComplexTypeWriter.SerializationMode mode)
        {
            if (prop == null) throw Error.ArgumentNull(nameof(prop));

            var elements = instance as IList;                       
            if(elements == null) throw Error.Argument(nameof(elements), "Can only write repeating elements from a type implementing IList");

            _writer.WriteStartArray();

            foreach(var element in elements)
            {
                var writer = new DispatchingWriter(_writer, Settings);
                writer.Serialize(prop, element, summary, mode);
            }

            _writer.WriteEndArray();
        }
    }
}
