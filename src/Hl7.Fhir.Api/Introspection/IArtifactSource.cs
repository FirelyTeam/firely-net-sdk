/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
namespace Hl7.Fhir.Api.Introspection
{
    public interface IArtifactSource
    {
        void Prepare();
        global::System.IO.Stream ReadContentArtifact(string name);
        global::Hl7.Fhir.Model.Resource ReadResourceArtifact(Uri artifactId);
    }
}
