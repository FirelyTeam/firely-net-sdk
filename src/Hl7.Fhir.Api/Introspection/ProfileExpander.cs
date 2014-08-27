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
using System.Linq;
using System.Text;
using Hl7.Fhir.Introspection.Source;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Introspection
{
    public class ProfileExpander
    {
        public ProfileExpander()
        {
            _loader = new StructureLoader(new CachedArtifactSource(ArtifactResolver.CreateDefault()));
        }

        public ProfileExpander(IArtifactSource source)
        {
            _loader = new StructureLoader(source);
        }
        
        // Used to resolve references to datatypes that we need to expand to accomodate the constaints in the differential
        private StructureLoader _loader;

        public Profile Expand(Profile differential)
        {
            // Start by making a full copy of the differential
            var snapshot = (Profile)differential.DeepCopy();

            // We leave the globally defined Queries, Extensions and Mappings alone, the
            // only thing we expand are the Structures
            if (snapshot.Structure != null)
            {
                var differentialStructures = snapshot.Structure.ToList();

                foreach (var differentialStructure in differentialStructures)
                {
                    // keep the differential form in the snapshot profile, as an unpublished copy
                    stashDifferentialStructure(snapshot, differentialStructure);

                    // add the expanded differential structure in the new snapshot form
                    snapshot.Structure.Add( expandStructure(differentialStructure) );
                }
            }

            return snapshot;
        }

        private Profile.ProfileStructureComponent expandStructure(Profile.ProfileStructureComponent structure)
        {
            var expander = new StructureExpander(structure, _loader);
            var snapshot = expander.Expand(structure);

            return snapshot;
        }

        private void stashDifferentialStructure(Profile differential, Profile.ProfileStructureComponent structure)
        {
            structure.Name += "-differential";
            structure.SetStructureForm(StructureForm.Differential);
            structure.SetStructureBaseUri(StructureLoader.BuildBaseStructureUri(structure.TypeElement).ToString());
            structure.Publish = false;
        }
    }
}
