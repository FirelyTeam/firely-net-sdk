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
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Specification.Expansion
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

            // Assure there's a text element, even if there's none in the differential
            if (snapshot.Text == null)
            {
                snapshot.Text = new Narrative() { Status = Narrative.NarrativeStatus.Empty };
                snapshot.Text.Div = "<div xmlns='http://www.w3.org/1999/xhtml'>No narrative was supplied for this Profile</div>";
            }

            // We leave the globally defined Queries, Extensions and Mappings alone, the
            // only thing we expand are the Structures
            if (snapshot.Structure != null)
            {
                var differentialStructures = snapshot.Structure.ToList();

                foreach (var differentialStructure in differentialStructures)
                {
                    // keep the differential form in the snapshot profile, as an unpublished copy
                    //stashDifferentialStructure(snapshot, differentialStructure);
                    
                    // Instead of keeping it, remove the original differential form
                    snapshot.Structure.Remove(differentialStructure);

                    // add the expanded differential structure in the new snapshot form
                    snapshot.Structure.Add( expandStructure(differentialStructure) );
                    
                }
            }

            finalizeExtensions(snapshot);

            return snapshot;
        }

        private void finalizeExtensions(Profile snapshot)
        {
            // Just a friendly helper method to add mandatory fields that are more or less boilerplate
            if (snapshot.ExtensionDefn != null)
            {
                foreach (var extensionDefn in snapshot.ExtensionDefn)
                {
                    if (extensionDefn.Definition != null)
                    {
                        if (extensionDefn.Definition.Min == null) extensionDefn.Definition.Min = 0;
                        if (extensionDefn.Definition.Max == null) extensionDefn.Definition.Max = "1";
                        if (extensionDefn.Definition.IsModifier == null) extensionDefn.Definition.IsModifier = false;
                    }
                }
            }
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
