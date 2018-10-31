/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Specification.Source;
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
        public SnapshotGeneratorSettings()
        {
            // See property declarations for default initializers
        }

        /// <summary>Clone ctor. Generates a new instance with the same state as the specified instance.</summary>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public SnapshotGeneratorSettings(SnapshotGeneratorSettings settings)
        {
            if (settings == null) { throw Error.ArgumentNull(nameof(settings)); }
            settings.CopyTo(this);
        }

        /// <summary>Creates a new <see cref="SnapshotGeneratorSettings"/> object that is a copy of the current instance.</summary>
        public SnapshotGeneratorSettings Clone() => new SnapshotGeneratorSettings(this);

        /// <summary>Copy all configuration settings to another instance.</summary>
        /// <param name="other">Another <see cref="SnapshotGeneratorSettings"/> instance.</param>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
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
        /// Determines if the <see cref="SnapshotGenerator"/> should automatically
        /// (re-)generate snapshots for all referenced external profiles if necessary.
        /// </summary>
        /// <remarks>
        /// If this setting is disabled, then the snapshot generator will
        /// not merge any external type profiles without a snapshot component.
        /// </remarks>
        public bool GenerateSnapshotForExternalProfiles { get; set; } = true;

        /// <summary>
        /// Determines if the <see cref="SnapshotGenerator"/> should always discard
        /// any existing snapshot component and always (re-)generate the snapshot.
        /// </summary>
        /// <remarks>
        /// If this setting is enabled, then the snapshot generator will re-generate
        /// the snapshot components of all the core resource and datatype profiles
        /// as well as of all other referenced external profiles.
        /// Re-generated snapshots are annotated to prevent duplicate re-generation
        /// (assuming the provided resource resolver uses caching).
        /// If this setting is disabled (default), then the snapshot generator relies
        /// on existing snapshot components, if they exist.
        /// <para>
        /// Only enable this option when the specified resolver is a <see cref="CachedResolver"/>.
        /// </para>
        /// </remarks>
        public bool ForceRegenerateSnapshots { get; set; } // = false;

        /// <summary>
        /// Determines if the <see cref="SnapshotGenerator"/> should assign the custom
        /// <see cref="SnapshotGeneratorExtensions.CONSTRAINED_BY_DIFF_EXT"/> extension
        /// to snapshot elements and properties that are constrained by the differential.
        /// </summary>
        /// <remarks>
        /// Enable this setting to add a custom
        /// <see cref="SnapshotGeneratorExtensions.CONSTRAINED_BY_DIFF_EXT"/> extension
        /// to elements and properties in the snapshot that are constrained by the
        /// differential with respect to the base profile.
        /// <para>
        /// Note that this extension only applies to the containing profile and should NOT
        /// be inherited by derived profiles. The FHIR API snapshot generator explicitly
        /// removes and re-generates these extensions for each profile.
        /// The <seealso cref="SnapshotGeneratorExtensions"/> class provides utility methods
        /// to read and/or remove the generated extensions.
        /// </para>
        /// </remarks>
        public bool GenerateExtensionsOnConstraints { get; set; } // = false;

        /// <summary>Enable this setting to annotate all elements and properties in the snapshot that are constrained by the differential.</summary>
        /// <remarks>The <seealso cref="SnapshotGeneratorAnnotations"/> class provides utility methods to read and/or remove the generated annotations.</remarks>
        public bool GenerateAnnotationsOnConstraints { get; set; } // = false;

        /// <summary>Enable this setting to automatically generate element ids for the snapshot.</summary>
        /// <remarks>
        /// The generated element ids conform to the STU3 FHIR specification.
        /// Do NOT enable this setting for DSTU2!
        /// </remarks>
        public bool GenerateElementIds { get; set; } // = false;

        // [WMR 20161004] Always try to merge element type profiles

        // <summary>
        // Enable this setting in order to merge custom element type profiles.
        // If enabled (default), the snapshot generator first merges constraints from custom type profiles before merging constraints from the base profile.
        // If disabled, the snapshot generator ignores custom type profiles and merges constraints from the base profile.
        // </summary>
        // <remarks>See GForge #9791</remarks>
        // public bool MergeTypeProfiles { get; set; } // = true
    }
}
