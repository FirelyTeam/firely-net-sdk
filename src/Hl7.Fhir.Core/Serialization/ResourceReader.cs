/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
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
    internal class ResourceReader
    {
#pragma warning disable 612, 618
        private ISourceNode _reader;
        private ModelInspector _inspector;

        public ParserSettings Settings { get; private set; }

        internal ResourceReader(ISourceNode reader, ParserSettings settings)
        {
            _reader = reader;
            _inspector = BaseFhirParser.Inspector;
            Settings = settings;
        }
#pragma warning restore 612,618

        public Resource Deserialize(Resource existing=null)
        {
            // If there's no a priori knowledge of the type of Resource we will encounter,
            // we'll have to determine from the data itself. 
            var resourceTypeName = _reader.GetResourceTypeIndicator();
            var mapping = _inspector.FindClassMappingForResource(resourceTypeName);

            if (mapping == null)
                throw Error.Format("Asked to deserialize unknown resource '" + resourceTypeName + "'", _reader.Location);
             
            // Delegate the actual work to the ComplexTypeReader, since
            // the serialization of Resources and ComplexTypes are virtually the same
            var cplxReader = new ComplexTypeReader(_reader, Settings);
            return (Resource)cplxReader.Deserialize(mapping, existing);
        }
    }
}
