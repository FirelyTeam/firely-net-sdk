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

            var pmid = mapping.FindMappedElementByName("id");
            var id = (Id)pmid.GetValue(instance);
            // ??resource Parameter has no id?? so check for null
            var idvalue = id != null ? id.Value : null;
            _writer.WriteStartRootObject(rootName, idvalue, contained);

            var complexSerializer = new ComplexTypeWriter(_writer);
            Coding subsettedTag = null;
            if (summary != Rest.SummaryType.False && instance is Resource)
            {
                Resource r = (instance as Resource);
                if (r != null)
                {
                    // If we are subsetting the instance during serialization, ensure that there 
                    // is a meta element with that subsetting in it
                    // (Helps make it easier to create conformant instances)
                    if (r.Meta == null)
                        r.Meta = new Meta();
                    if (r.Meta.Tag.Where(t => t.System == "http://hl7.org/fhir/v3/ObservationValue" && t.Code == "SUBSETTED").Count() == 0)
                    {
                        subsettedTag = new Coding("http://hl7.org/fhir/v3/ObservationValue", "SUBSETTED");
                        r.Meta.Tag.Add(subsettedTag);
                    }
                }
            }
            complexSerializer.Serialize(mapping, instance, summary);

            if (subsettedTag != null)
            {
                Resource r = (instance as Resource);
                r.Meta.Tag.Remove(subsettedTag);
            }

            _writer.WriteEndRootObject(contained);
        }
    }
}
