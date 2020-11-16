/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System;
using System.ComponentModel;

namespace Hl7.Fhir.Specification.Snapshot
{
    /// <summary>Configuration settings for the <see cref="SnapshotGenerator"/> class.</summary>
    public sealed class SnapshotGeneratorSettings
    {
        /// <summary>Default configuration settings for the <see cref="SnapshotGenerator"/> class.</summary>
        [Obsolete("Use the CreateDefault() method, as using this static member may cause threading issues.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly SnapshotGeneratorSettings Default = new SnapshotGeneratorSettings();

        /// <summary>Creates a new <see cref="SnapshotGeneratorSettings"/> instance with default property values.</summary>
        public static SnapshotGeneratorSettings CreateDefault() => new SnapshotGeneratorSettings();

        /// <summary>Default ctor.</summary>
        public SnapshotGeneratorSettings() { }

        /// <summary>Clone ctor. Generates a new instance with the same state as the specified instance.</summary>
        public SnapshotGeneratorSettings(SnapshotGeneratorSettings settings)
        {
            if (settings == null) { throw Error.ArgumentNull(nameof(settings)); }
            settings.CopyTo(this);
        }

        /// <summary>Returns an exact clone of the current configuration settings instance.</summary>
        public SnapshotGeneratorSettings Clone() => new SnapshotGeneratorSettings(this);

        /// <summary>Copy all configuration settings to another instance.</summary>
        public void CopyTo(SnapshotGeneratorSettings other)
        {
            if (other == null) { throw Error.ArgumentNull(nameof(other)); }
            other.GenerateSnapshotForExternalProfiles = GenerateSnapshotForExternalProfiles;
            other.ForceRegenerateSnapshots = ForceRegenerateSnapshots;
            other.GenerateExtensionsOnConstraints = GenerateExtensionsOnConstraints;
            other.GenerateAnnotationsOnConstraints = GenerateAnnotationsOnConstraints;
            other.GenerateElementIds = GenerateElementIds;
            // other.MergeTypeProfiles = MergeTypeProfiles;
        }

        /// <summary>
        /// If enabled (default), the snapshot generator will automatically generate the snapshot component
        /// of any referenced external profiles on demand if necessary.
        /// If disabled, then skip the merging of any external type profiles without a snapshot component.
        /// </summary>
        public bool GenerateSnapshotForExternalProfiles { get; set; } = true; // ExpandExternalProfiles

        /// <summary>
        /// Force expansion of all external profiles, disregarding any existing snapshot components.
        /// If enabled, the snapshot generator will re-generate the snapshot components of all the core resource and datatype profiles
        /// as well as of all other referenced external profiles.
        /// Re-generated snapshots are annotated to prevent duplicate re-generation (assuming the provided resource resolver uses caching).
        /// If disabled (default), then the snapshot generator relies on existing snapshot components, if they exist.
        /// </summary>
        public bool ForceRegenerateSnapshots { get; set; } = false; // ForceExpandAll

        /// <summary>
        /// Enable this setting to add a custom <see cref="SnapshotGeneratorExtensions.CONSTRAINED_BY_DIFF_EXT"/> extension
        /// to elements and properties in the snapshot that are constrained by the differential with respect to the base profile.
        /// <br />
        /// Note that this extension only applies to the containing profile and should NOT be inherited by derived profiles.
        /// The FHIR SDK snapshot generator explicitly removes and re-generates these extensions for each profile.
        /// The <seealso cref="SnapshotGeneratorExtensions"/> class provides utility methods to read and/or remove the generated extensions.
        /// </summary>
        public bool GenerateExtensionsOnConstraints { get; set; } = false; // MarkChanges

        /// <summary>Enable this setting to annotate all elements and properties in the snapshot that are constrained by the differential.</summary>
        /// <remarks>The <seealso cref="SnapshotGeneratorAnnotations"/> class provides utility methods to read and/or remove the generated annotations.</remarks>
        public bool GenerateAnnotationsOnConstraints { get; set; } = false; // AnnotateDifferentialConstraints

        /// <summary>Enable this setting to automatically generate missing element id values.</summary>
        public bool GenerateElementIds { get; set; } = true;

        // [WMR 20161004] Always try to merge element type profiles

        // <summary>
        // Enable this setting in order to merge custom element type profiles.
        // If enabled (default), the snapshot generator first merges constraints from custom type profiles before merging constraints from the base profile.
        // If disabled, the snapshot generator ignores custom type profiles and merges constraints from the base profile.
        // </summary>
        // <remarks>See GForge #9791</remarks>
        // public bool MergeTypeProfiles { get; set; }
    }
}
