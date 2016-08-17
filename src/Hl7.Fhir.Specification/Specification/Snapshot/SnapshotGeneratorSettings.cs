// [WMR 20160815] New: expand all complex elements (even without any diff constraints)
#define EXPANDALL

/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;

namespace Hl7.Fhir.Specification.Snapshot
{
    /// <summary>Configuration settings for the <see cref="SnapshotGenerator"/> class.</summary>
    public sealed class SnapshotGeneratorSettings
    {
        /// <summary>Default configuration settings for the <see cref="SnapshotGenerator"/> class.</summary>
        public static readonly SnapshotGeneratorSettings Default = new SnapshotGeneratorSettings()
        {
            MarkChanges = false,
            MergeTypeProfiles = true,
            IgnoreUnresolvedProfiles = false,
            ExpandExternalProfiles = false,
            RewriteElementBase = false,
            NormalizeElementBase = false    // true in STU3
#if EXPANDALL
            , ExpandAll = false
#endif
        };

        /// <summary>
        /// Mark all elements in the snapshot that are constrained with respect to the base profile.
        /// The snapshot generator will decorate all changed elements with a special extension
        /// (canonical url "http://hl7.org/fhir/StructureDefinition/changedByDifferential").
        /// </summary>
        public bool MarkChanges { get; set; }

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
        /// Enable this setting to ignore unknown or invalid element type profiles.
        /// If disabled (default), throw an exception for unknown or invalid element type profiles.
        /// </summary>
        public bool IgnoreUnresolvedProfiles { get; set; }

        /// <summary>
        /// EXPERIMENTAL!
        /// Enable this setting to automatically expand any required external profiles on demand if necessary.
        /// If disabled (default), throw an exception for external type profiles without a snapshot component.
        /// </summary>
        public bool ExpandExternalProfiles { get; set; }

        /// <summary>
        /// Enable this setting to rewrite all ElementDefinition.Base components by tracking the base hierarchy.
        /// If disabled (default), the snapshot inherits existing Base components present in base resource.
        /// </summary>
        /// <remarks>
        /// This setting is useful to correct errors in the core profile definitions.
        /// </remarks>
        public bool RewriteElementBase { get; set; }

        /// <summary>
        /// EXPERIMENTAL!
        /// Enable this setting to normalize the ElementDefinition.Base component of inherited elements.
        /// </summary>
        /// <example>
        /// Path = 'Patient.name.given' => Base.Path = 'HumanName.given' (derived from parent element type = 'HumanName')
        /// </example>
        public bool NormalizeElementBase { get; set; }

#if EXPANDALL
        /// <summary>
        /// EXPERIMENTAL!
        /// Enable this setting to recursively expand all profile elements, regardless of wether differential constraints exist.
        /// By default, the snapshot generator only expands elements with matching differential constraints.
        /// </summary>
        /// <remarks>
        /// If you enable this setting, the size of the resulting snapshot component may grow significantly due to the additional redundant information.
        /// </remarks>
        public bool ExpandAll { get; set; }
#endif
    }
}
