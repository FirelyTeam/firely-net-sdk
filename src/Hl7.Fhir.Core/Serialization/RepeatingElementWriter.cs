/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Support;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;


namespace Hl7.Fhir.Serialization
{
    internal class RepeatingElementWriter
    {
        private IFhirWriter _writer;
        private ModelInspector _inspector;

        public RepeatingElementWriter(IFhirWriter writer)
        {
            _writer = writer;
            _inspector = SerializationConfig.Inspector;
        }

        public void Serialize(PropertyMapping prop, object instance, bool summary, ComplexTypeWriter.SerializationMode mode)
        {
            if (prop == null) throw Error.ArgumentNull("prop");

            var elements = instance as IList;                       
            if(elements == null) throw Error.Argument("existing", "Can only write repeating elements from a type implementing IList");

            _writer.WriteStartArray();

            foreach(var element in elements)
            {
                var writer = new DispatchingWriter(_writer);
                writer.Serialize(prop, element, summary, mode);
            }

            _writer.WriteEndArray();
        }
    }
}
