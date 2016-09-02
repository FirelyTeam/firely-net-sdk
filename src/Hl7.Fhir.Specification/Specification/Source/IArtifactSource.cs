/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.IO;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Specification.Source
{
    public interface IArtifactSource
    {
        Stream LoadArtifactByName(string artifactName);
        IEnumerable<string> ListArtifactNames();

        IEnumerable<ConformanceInformation> ListConformanceResources();
        Hl7.Fhir.Model.Resource LoadConformanceResourceByUrl(string url);
    }


    public class ConformanceInformation
    {
        public ResourceType Type { get; set; }

        public string Canonical { get; set; }

        [Obsolete("Please use the equivalent, but less ambiguous Canonical property instead")]
        public string Url
        {
            get { return Canonical; }
            set { Canonical = Url; }
        }
                
        public string Name { get; set; }

        public string ValueSetSystem { get; set; }

        public string Origin { get; set; }

        public override string ToString()
        {
            return String.Format("{0} resource with id {1} ({2}), read from {3}", Type.ToString(), Canonical, Name, Origin);
        }

        public static ConformanceInformation FromResource(Resource r, string origin)
        {
            var ci = new ConformanceInformation();

            ci.Origin = origin;
            ci.Type = r.ResourceType;

            var cr = r as IConformanceResource;
            if (cr != null)
            {
                ci.Name = cr.Name;
                ci.Canonical = cr.Url;
            }

            var vs = r as ValueSet;
            if (vs != null)
            {
                ci.ValueSetSystem = vs.CodeSystem != null ? vs.CodeSystem.System : null;
            }

            return ci;
        }
    }
}
