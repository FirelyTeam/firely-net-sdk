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
            ExpandExternalProfiles = false,
            ExpandUnconstrainedElements = false,
            MarkChanges = false,

            // Following settings concern controversial aspects, behavior is not well defined
            // Needs discussion/decision from HL7 FHIR community
            MergeTypeProfiles = true,
            NormalizeElementBase = false   // true in STU3
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
        /// Enable this setting to automatically generate the snapshot of external profiles on demand if necessary.
        /// If disabled (default), throw an exception for external type profiles without a snapshot component.
        /// </summary>
        public bool ExpandExternalProfiles { get; set; }

        // TODO: Use (timestamp) annotation to mark & detect already (forceably) re-expanded profiles

        // <summary>
        // EXPERIMENTAL!
        // Force expansion of all external profiles, disregarding any existing snapshot components.
        // </summary>
        // public bool ForceExpandAll { get; set; }

        /// <summary>
        /// Mark all elements in the snapshot that are constrained with respect to the base profile.
        /// The snapshot generator will decorate all changed elements with a special extension
        /// (canonical url "http://hl7.org/fhir/StructureDefinition/changedByDifferential").
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
        /// Enable this setting to normalize the ElementDefinition.Base component of inherited elements.
        /// </summary>
        /// <example>
        /// Path = 'Patient.name.given' => Base.Path = 'HumanName.given' (derived from parent element type = 'HumanName')
        /// </example>
        public bool NormalizeElementBase { get; set; }

    }
}
