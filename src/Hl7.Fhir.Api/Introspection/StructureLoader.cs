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
using Hl7.Fhir.Introspection.Source;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Introspection
{
    internal class StructureLocator
    {
        public StructureLocator(IArtifactSource source)
        {
            ArtifactSource = source;
            _cachedSource = new CachedArtifactSource(ArtifactSource);
        }

        public IArtifactSource ArtifactSource { get; private set; }
        private IArtifactSource _cachedSource;
       
        public Profile.ProfileStructureComponent Locate(Uri structureUri, Code type)
        {
            if(!structureUri.IsAbsoluteUri) throw Error.Argument("Reference to structure must be an absolute url");

            var name = structureUri.ToString();

            var uriWithoutAnchor = new UriBuilder(structureUri);
            uriWithoutAnchor.Fragment = null;

            var profile = loadProfile(uriWithoutAnchor.Uri);

            if(profile.Structure == null || profile.Structure.Count == 0) return null;

            var structureName = structureUri.Fragment.TrimStart('#');

            if (!String.IsNullOrEmpty(structureName))
            {
                // We have an explicit structure name in the address, try to fetch it from the resource
                return profile.Structure.Where(str => str.Name == structureName && str.Type == type.Value).SingleOrDefault();
            }
            else
            {
                // No explicit structure name, locate a single structure that's the type we're looking for.
                return profile.Structure.Where(str => str.Type == type.Value).SingleOrDefault();
            }
        }


        private Profile loadProfile(Uri profileUri)
        {
            var profile = _cachedSource.ReadResourceArtifact(profileUri) as Profile;

            if (profile == null) return null;

            if(profile.GetProfileLocation() == null)
                profile.SetProfileLocation(profileUri);

            return profile;
        }
    }
}
