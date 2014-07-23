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
    internal class StructureLoader
    {
        public StructureLoader(IArtifactSource source)
        {
            ArtifactSource = source;
        }

        public IArtifactSource ArtifactSource { get; private set; }
        private IArtifactSource _cachedSource;
       
        public Profile.ProfileStructureComponent Locate(Uri structureUri, Code type)
        {
            if(!structureUri.IsAbsoluteUri) throw Error.Argument("Reference to structure must be an absolute url");

            // The structure Uri might be of the form <url-to-profile>#<structure name>
            // First split off the <url-to-profile> and load the profile
            var uriWithoutAnchor = new UriBuilder(structureUri);
            uriWithoutAnchor.Fragment = null;

            var profile = loadProfile(uriWithoutAnchor.Uri);

            // If the profile was not found, return without doing work
            if(profile == null || profile.Structure == null || profile.Structure.Count == 0) return null;

            // Determine the <structure name>
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


        /// <summary>
        /// All base structures (resources + datatypes) that come with the spec have a specific pre-defined
        /// (but currently symbolic) url where they can be found. Locate base structures using that url
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Profile.ProfileStructureComponent LocateBaseStructure(Code type)
        {
            return Locate(BuildBaseStructureUri(type),type);
        }


        public static Uri BuildBaseStructureUri(Code type)
        {
            string fullReference = CoreZipArtifactSource.CORE_SPEC_PROFILE_URI_PREFIX + type.Value.ToLower();
            return new Uri(fullReference);
        }

        private Profile loadProfile(Uri profileUri)
        {
            var profile = ArtifactSource.ReadResourceArtifact(profileUri) as Profile;

            if (profile == null) return null;

            // Do preprocessing
            //if(profile.GetProfileLocation() == null)
            //    profile.SetProfileLocation(profileUri);

            return profile;
        }
    }
}
