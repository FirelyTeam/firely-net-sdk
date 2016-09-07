/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

namespace Hl7.Fhir.Specification.Snapshot
{
    /// <summary>Configuration settings for the <see cref="SnapshotGenerator"/> class.</summary>
    public sealed class SnapshotGeneratorSettings
    {
        /// <summary>Default configuration settings for the <see cref="SnapshotGenerator"/> class.</summary>
        public static readonly SnapshotGeneratorSettings Default = new SnapshotGeneratorSettings()
        {
            ExpandExternalProfiles = true,
            ExpandUnconstrainedElements = false,
            MarkChanges = false,

            // Following settings concern controversial aspects, behavior is not well defined
            // Needs discussion/decision from HL7 FHIR community
            MergeTypeProfiles = true,
            NormalizeElementBase = true
        };

        /// <summary>Default ctor.</summary>
        public SnapshotGeneratorSettings() { }

        /// <summary>Clone ctor. Generates a new instance with the same state as the specified instance.</summary>
        public SnapshotGeneratorSettings(SnapshotGeneratorSettings settings)
        {
            ExpandExternalProfiles = settings.ExpandExternalProfiles;
            ExpandUnconstrainedElements = settings.ExpandUnconstrainedElements;
            MarkChanges = settings.MarkChanges;
            MergeTypeProfiles = settings.MergeTypeProfiles;
            NormalizeElementBase = settings.NormalizeElementBase;
        }

        /// <summary>
        /// If enabled (default), the snapshot generator will automatically generate the snapshot component of any referenced external profiles on demand if necessary.
        /// If disabled, then skip the merging of any external type profiles without a snapshot component.
        /// </summary>
        public bool ExpandExternalProfiles { get; set; }

        /// <summary>
        /// EXPERIMENTAL!
        /// Force expansion of all external profiles, disregarding any existing snapshot components.
        /// If enabled, the snapshot generator will re-generate the snapshot components of all the core resource and datatype profiles
        /// as well as of all other referenced external profiles.
        /// Re-generated snapshots are annotated to prevent duplicate re-generation (assuming a CachedArtifactSource).
        /// If disabled (default), then the snapshot generator relies on the existing snapshot components.
        /// </summary>
        public bool ForceExpandAll { get; set; }

        /// <summary>
        /// Enable this setting to mark all elements in the snapshot that are constrained with respect to the base profile.
        /// The snapshot generator will decorate all changed elements with a special extension
        /// (canonical url "http://hl7.org/fhir/StructureDefinition/changedByDifferential").
        /// <br />
        /// Note that this extension only applies to the containing profile and should NOT be inherited by derived profiles.
        /// The FHIR API snapshot generator explicitly removes and re-generates these extensions for each profile.
        /// </summary>
        public bool MarkChanges { get; set; }

        /// <summary>
        /// EXPERIMENTAL!
        /// Enable this setting to recursively expand all profile elements, regardless of wether differential constraints exist.
        /// By default, the snapshot generator only expands elements with matching differential constraints.
        /// </summary>
        /// <remarks>
        /// If you enable this setting, the size of the resulting snapshot component may grow significantly due to the additional redundant information.
        /// </remarks>
        public bool ExpandUnconstrainedElements { get; set; }

        /// <summary>
        /// EXPERIMENTAL!
        /// Enable this setting in order to merge custom element type profiles.
        /// If enabled (default), the snapshot generator first merges constraints from custom type profiles before merging constraints from the base profile.
        /// If disabled, the snapshot generator ignores custom type profiles and merges constraints from the base profile.
        /// </summary>
        /// <remarks>See GForge #9791</remarks>
        public bool MergeTypeProfiles { get; set; }

        /// <summary>
        /// EXPERIMENTAL!
        /// If enabled (default), then normalize the ElementDefinition.Base component of inherited elements in order to reference
        /// the element definition in the defining profile that originally introduces the element.
        /// If disabled, then the ElementDefinition.Base components are initialized from the associated element in the immediate base profile.
        /// </summary>
        /// <example>
        /// <code>
        /// Path = "Patient.meta" => Base.Path = "Resource.meta" (derived from parent element Resource)
        /// <br />
        /// Path = "Patient.name.given" => Base.Path = "HumanName.given" (derived from parent element type = 'HumanName')
        /// </code>
        /// </example>
        public bool NormalizeElementBase { get; set; }

    }
}
