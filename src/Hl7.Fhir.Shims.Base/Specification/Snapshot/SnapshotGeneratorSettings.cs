/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Specification.Snapshot
{
    /// <summary>
    /// Defines the merge strategy for element properties when both a profiled base and a typeref to a profiled datatype bring in changes.
    /// </summary>
    /// <remarks>
    /// This addresses the behavior difference described in FHIR-9791 (https://jira.hl7.org/browse/FHIR-9791).
    /// The specification does not prescribe a definitive merge order, and both strategies have valid use cases.
    /// </remarks>
    public enum TypeProfileMergeStrategy
    {
        /// <summary>
        /// Legacy .NET SDK behavior (default): Type profile constraints are merged first, then base profile constraints, then differential.
        /// Example: Patient.Address with type.profile = AddressNL
        /// Order: AddressNL constraints → Patient.Address base constraints → differential constraints
        /// Result: Base profile constraints can override type profile constraints.
        /// </summary>
        /// <remarks>
        /// This is the behavior that has been used by the .NET SDK since 2016.
        /// Choose this mode to maintain backward compatibility with existing .NET snapshots.
        /// </remarks>
        TypeFirst = 0,

        /// <summary>
        /// Base-first merge strategy: Base profile constraints are merged first, then type profile constraints, then differential.
        /// Example: Patient.Address with type.profile = AddressNL
        /// Order: Patient.Address base constraints → AddressNL constraints → differential constraints
        /// Result: Type profile constraints can override base profile constraints.
        /// </summary>
        /// <remarks>
        /// This mode is designed to align with Java HAPI FHIR implementation behavior.
        /// Choose this mode when interoperability with Java-generated snapshots is required.
        /// </remarks>
        BaseFirst = 1
    }

    /// <summary>Configuration settings for the <see cref="SnapshotGenerator"/> class.</summary>
    public sealed class SnapshotGeneratorSettings
    {
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
            other.TypeProfileMergeStrategy = TypeProfileMergeStrategy;
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

        /// <summary>
        /// Controls the merge strategy when both a profiled base and a typeref to a profiled datatype bring in changes to element properties.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This setting addresses FHIR-9791 (https://jira.hl7.org/browse/FHIR-9791), which discusses the ambiguity in merge order
        /// when an element has both base profile constraints and custom type profile constraints.
        /// </para>
        /// <para>
        /// Default value is <see cref="TypeProfileMergeStrategy.TypeFirst"/> to maintain backward compatibility with existing .NET SDK behavior.
        /// Use <see cref="TypeProfileMergeStrategy.BaseFirst"/> for compatibility with Java HAPI FHIR implementation.
        /// </para>
        /// <para>
        /// Example scenario: A profile constrains Patient.address with type.profile = "http://example.org/AddressNL"
        /// - TypeFirst: AddressNL constraints → Patient.address base constraints → differential
        /// - BaseFirst: Patient.address base constraints → AddressNL constraints → differential
        /// </para>
        /// </remarks>
        public TypeProfileMergeStrategy TypeProfileMergeStrategy { get; set; } = TypeProfileMergeStrategy.TypeFirst;
    }
}
