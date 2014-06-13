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
using Hl7.Fhir.Rest;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Api.Introspection
{
    public class WebArtifactSource : IArtifactSource
    {
        public void Prepare()
        {
            return;     // Nothing to prepare
        }

        public System.IO.Stream ReadContentArtifact(string name)
        {
            throw new NotImplementedException();        // support only url-based artifacts
        }

        public Model.Resource ReadResourceArtifact(Uri artifactId)
        {
            if (artifactId == null) throw Error.ArgumentNull("artifactId");
            if (!artifactId.IsAbsoluteUri) Error.Argument("artifactId", "Uri must be absolute");

            var id = new ResourceIdentity(artifactId);

            var client = new FhirClient(id.Endpoint);

            try
            {
                var artifactEntry = client.Read(id);

                return artifactEntry != null ? artifactEntry.Resource : null;
            }
            catch (Exception)
            {
                return null;
            }
            
        }
    }
}
