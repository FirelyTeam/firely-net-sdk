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

        public void Expand(Profile profile)
        {
            // Assure there's a text element, even if there's none in the differential
            if (profile.Text == null)
            {
                profile.Text = new Narrative() { Status = Narrative.NarrativeStatus.Empty };
                profile.Text.Div = "<div xmlns='http://www.w3.org/1999/xhtml'>No narrative was supplied for this Profile</div>";
            }

            // We leave the globally defined Queries, Extensions and Mappings alone, the
            // only thing we expand are the Structures
            if (profile.Structure != null)
            {
                foreach (var structureToExpand in profile.Structure)
                {
                    expandStructure(structureToExpand);                   
                }
            }

            finalizeExtensions(profile);
        }

        private void finalizeExtensions(Profile snapshot)
        {
            // Just a friendly helper method to add mandatory fields that are more or less boilerplate
            if (snapshot.ExtensionDefn != null)
            {
                foreach (var extensionDefn in snapshot.ExtensionDefn)
                {
                    if (extensionDefn.Element != null && extensionDefn.Element[0].Definition != null)
                    {
                        if (extensionDefn.Element[0].Definition.Min == null) extensionDefn.Element[0].Definition.Min = 0;
                        if (extensionDefn.Element[0].Definition.Max == null) extensionDefn.Element[0].Definition.Max = "1";
                        if (extensionDefn.Element[0].Definition.IsModifier == null) extensionDefn.Element[0].Definition.IsModifier = false;
                    }
                }
            }
        }

        private void expandStructure(Profile.ProfileStructureComponent structure)
        {
            var expander = new StructureExpander(_loader);
            expander.Expand(structure);
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
