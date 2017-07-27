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
    public class ResourceWriter
    {
        private IFhirWriter _writer;
        private ModelInspector _inspector;

        public ResourceWriter(IFhirWriter writer)
        {
            _writer = writer;
            _inspector = BaseFhirParser.Inspector;
        }

        public void Serialize(object instance, Rest.SummaryType summary, bool contained = false, string root = null)
        {
            if (instance == null) throw Error.ArgumentNull(nameof(instance));

            var mapping = _inspector.ImportType(instance.GetType());

            var rootName = root ?? mapping.Name;

            _writer.WriteStartRootObject(rootName, contained);

            var complexSerializer = new ComplexTypeWriter(_writer);

            Coding subsettedTag = null;

            if (summary != Rest.SummaryType.False && instance is Resource)
            {
                var resource = instance as Resource;

                if (resource.Meta == null)
                    resource.Meta = new Meta();

                if (resource.Meta.Tag.Any(t => t.System == "http://hl7.org/fhir/v3/ObservationValue" && t.Code == "SUBSETTED"))
                {
                    subsettedTag = new Coding("http://hl7.org/fhir/v3/ObservationValue", "SUBSETTED");
                    resource.Meta.Tag.Add(subsettedTag);
                }
            }

            complexSerializer.Serialize(mapping, instance, summary);

            if (subsettedTag != null)
            {
                var resource = instance as Resource;
                resource.Meta.Tag.Remove(subsettedTag);
            }

            _writer.WriteEndRootObject(contained);
        }
    }
}
