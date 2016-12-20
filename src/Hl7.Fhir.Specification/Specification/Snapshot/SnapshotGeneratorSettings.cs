/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using System;

namespace Hl7.Fhir.Specification.Snapshot
{
    /// <summary>Configuration settings for the <see cref="SnapshotGenerator"/> class.</summary>
    public sealed class SnapshotGeneratorSettings : ICloneable
    {
        /// <summary>Default configuration settings for the <see cref="SnapshotGenerator"/> class.</summary>
        public static readonly SnapshotGeneratorSettings Default = new SnapshotGeneratorSettings()
        {
            ExpandExternalProfiles = true,
            ForceExpandAll = false,                     // Only enable this when using a cached source...
            MarkChanges = false,                        // Enabled by Simplifier
            AnnotateDifferentialConstraints = false,    // For snapshot rendering
            GenerateElementIds = false                  // for STU3
            // MergeTypeProfiles = true
        };

        /// <summary>Default ctor.</summary>
        public SnapshotGeneratorSettings() { }

        /// <summary>Clone ctor. Generates a new instance with the same state as the specified instance.</summary>
        public SnapshotGeneratorSettings(SnapshotGeneratorSettings settings)
        {
            if (settings == null) { throw Error.ArgumentNull(nameof(settings)); }
            settings.CopyTo(this);
        }

        /// <summary>Returns an exact clone of the current configuration settings instance.</summary>
        public object Clone() => new SnapshotGeneratorSettings(this);

        /// <summary>Copy all configuration settings to another instance.</summary>
        public void CopyTo(SnapshotGeneratorSettings other)
        {
            if (other == null) { throw Error.ArgumentNull(nameof(other)); }
            other.ExpandExternalProfiles = ExpandExternalProfiles;
            other.ForceExpandAll = ForceExpandAll;
            other.MarkChanges = MarkChanges;
            other.AnnotateDifferentialConstraints = AnnotateDifferentialConstraints;
            other.GenerateElementIds = GenerateElementIds;
            // other.MergeTypeProfiles = MergeTypeProfiles;
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
        /// Enable this setting to annotate all elements and properties in the snapshot that are constrained by the differential
        /// using the <see cref="SnapshotGeneratorAnnotations.ConstrainedByDifferentialAnnotation"/>.
        /// </summary>
        public bool AnnotateDifferentialConstraints { get; set; }

        // [WMR 20161004] Always try to merge element type profiles

        // <summary>
        // Enable this setting in order to merge custom element type profiles.
        // If enabled (default), the snapshot generator first merges constraints from custom type profiles before merging constraints from the base profile.
        // If disabled, the snapshot generator ignores custom type profiles and merges constraints from the base profile.
        // </summary>
        // <remarks>See GForge #9791</remarks>
        // public bool MergeTypeProfiles { get; set; }

        // [WMR 20161115] New
        /// <summary>Enable this setting to automatically generate missing element id values.</summary>
        /// <remarks>The generated element ids conform to the STU3 FHIR specification.</remarks>
        public bool GenerateElementIds { get; set; }
    }
}
