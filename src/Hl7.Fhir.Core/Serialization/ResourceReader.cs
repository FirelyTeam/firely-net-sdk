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
    public class ResourceReader
    {       
        private IFhirReader _reader;
        private ModelInspector _inspector;

        public ResourceReader(IFhirReader reader)
        {
            _reader = reader;
            _inspector = SerializationConfig.Inspector;
        }

        public Resource Deserialize(Resource existing=null)
        {
            // If there's no a priori knowledge of the type of Resource we will encounter,
            // we'll have to determine from the data itself. 
            var resourceTypeName = _reader.GetResourceTypeName();
            var mapping = _inspector.FindClassMappingForResource(resourceTypeName);

            if (mapping == null)
                throw Error.Format("Asked to deserialize unknown resource '" + resourceTypeName + "'", _reader);
             
            // Delegate the actual work to the ComplexTypeReader, since
            // the serialization of Resources and ComplexTypes are virtually the same
            var cplxReader = new ComplexTypeReader(_reader);
            return (Resource)cplxReader.Deserialize(mapping, existing);
        }
    }
}
