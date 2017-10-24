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

        public ParserSettings Settings { get; private set; }

        public ResourceWriter(IFhirWriter writer, ParserSettings settings)
        {
            _writer = writer;
            _inspector = BaseFhirParser.Inspector;
            Settings = settings;
        }

        public void Serialize(Resource instance, Rest.SummaryType summary, bool contained = false)
        {
            if (instance == null) throw Error.ArgumentNull(nameof(instance));

            var mapping = _inspector.ImportType(instance.GetType());
            if (mapping == null)
                throw Error.Format($"Asked to serialize unknown resource type '{instance.GetType()}'");

            _writer.WriteStartRootObject(mapping.Name, contained);

            var complexSerializer = new ComplexTypeWriter(_writer, Settings);
            Coding subsettedTag = null;
            bool createdMetaElement = false;
            if (summary != Rest.SummaryType.False && instance is Resource)
            {
                var resource = instance as Resource;

                if (resource.Meta == null)
                {
                    resource.Meta = new Meta();
                    createdMetaElement = true;
                }

                if (!resource.Meta.Tag.Any(t => t.System == "http://hl7.org/fhir/v3/ObservationValue" && t.Code == "SUBSETTED"))
                {
                    subsettedTag = new Coding("http://hl7.org/fhir/v3/ObservationValue", "SUBSETTED");
                    resource.Meta.Tag.Add(subsettedTag);
                }
            }
            complexSerializer.Serialize(mapping, instance, summary);

            Resource r = (instance as Resource);
            if (subsettedTag != null)
                r.Meta.Tag.Remove(subsettedTag);

            if (createdMetaElement)
                r.Meta = null; // remove the meta element again.

            _writer.WriteEndRootObject(contained);
        }
    }
}
