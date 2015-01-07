/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
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
    public class ResourceWriter
    {       
        private IFhirWriter _writer;
        private ModelInspector _inspector;

        public ResourceWriter(IFhirWriter writer)
        {
            _writer = writer;
            _inspector = SerializationConfig.Inspector;
        }

        public void Serialize(object instance, bool summary, bool contained = false, string root = null)
        {
            if (instance == null) throw Error.ArgumentNull("instance");

            var mapping = _inspector.ImportType(instance.GetType());

            var rootName = root ?? mapping.Name;

            _writer.WriteStartRootObject(rootName,contained);

            var complexSerializer = new ComplexTypeWriter(_writer);
            complexSerializer.Serialize(mapping, instance, summary);

            _writer.WriteEndRootObject(contained);
        }
    }
}
