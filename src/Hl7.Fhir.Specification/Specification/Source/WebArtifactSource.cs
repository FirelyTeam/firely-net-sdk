/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
//using Hl7.Fhir.Rest;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Specification.Source
{
    public class WebArtifactSource : IArtifactSource
    {
        public System.IO.Stream LoadArtifactByName(string name)
        {
            throw new NotImplementedException();        // support only url-based artifacts
        }

        public IEnumerable<string> ListArtifactNames()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ConformanceInformation> ListConformanceResources()
        {
            throw new NotImplementedException();
        }

        public Hl7.Fhir.Model.Resource LoadConformanceResourceByUrl(string url)
        {
            if (url == null) throw Error.ArgumentNull("identifier");

            var id = new ResourceIdentity(url);

            var client = new FhirClient(id.BaseUri);
            client.Timeout = 5000;  //ms

            try
            {
                return client.Read<Resource>(id);
            }
            catch(FhirOperationException)
            {
                return null;
            }            
        }
    }
}
