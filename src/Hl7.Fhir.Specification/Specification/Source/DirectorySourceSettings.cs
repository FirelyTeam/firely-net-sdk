/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Summary;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using System.IO;
using System.Linq;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Configuration settings for the <see cref="DirectorySource"/> class.</summary>
    public sealed class DirectorySourceSettings
    {
        /// <summary>Default value of the <see cref="FormatPreference"/> configuration setting.</summary>
        public const DirectorySource.DuplicateFilenameResolution DefaultFormatPreference = DirectorySource.DuplicateFilenameResolution.PreferXml;

        /// <summary>Default value of the <see cref="Masks"/> configuration setting (*.*)</summary>
        public readonly static string[] DefaultMasks = new[] { "*.*" };

        /// <summary>Creates a new <see cref="DirectorySourceSettings"/> instance with default property values.</summary>
        public static DirectorySourceSettings CreateDefault() => new DirectorySourceSettings();

        // Instance fields
        ParserSettings _parserSettings = ParserSettings.CreateDefault();
        FhirXmlParsingSettings _xmlParserSettings = FhirXmlParsingSettings.CreateDefault();
        FhirJsonParsingSettings _jsonParserSettings = FhirJsonParsingSettings.CreateDefault();

        /// <summary>Default constructor. Creates a new <see cref="DirectorySourceSettings"/> instance with default property values.</summary>
        public DirectorySourceSettings()
        {
            // See property declarations for default initializers
        }

        /// <summary>Clone constructor. Generates a new <see cref="DirectorySourceSettings"/> instance initialized from the state of the specified instance.</summary>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public DirectorySourceSettings(DirectorySourceSettings settings)
        {
            if (settings == null) { throw Error.ArgumentNull(nameof(settings)); }
            settings.CopyTo(this);
        }

        // Internal ctor
        internal DirectorySourceSettings(bool includeSubdirectories)
        {
            IncludeSubDirectories = includeSubdirectories;
        }

        /// <summary>Copy all configuration settings to another instance.</summary>
        /// <param name="other">Another <see cref="DirectorySourceSettings"/> instance.</param>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public void CopyTo(DirectorySourceSettings other)
        {
            if (other == null) { throw Error.ArgumentNull(nameof(other)); }

            // [WMR 20181025] Clone state
            other.IncludeSubDirectories = this.IncludeSubDirectories;
            other.Masks = (string[])this.Masks.Clone();
            other.Includes = (string[])this.Includes?.Clone();
            other.Excludes = (string[])this.Excludes?.Clone();
            other.FormatPreference = this.FormatPreference;
            other.MultiThreaded = this.MultiThreaded;
            other.SummaryDetailsHarvesters = (ArtifactSummaryHarvester[])this.SummaryDetailsHarvesters?.Clone();
            other.ExcludeSummariesForUnknownArtifacts = this.ExcludeSummariesForUnknownArtifacts;
            other.ParserSettings = new ParserSettings(this.ParserSettings);
            other.XmlParserSettings = new FhirXmlParsingSettings(this.XmlParserSettings);
            other.JsonParserSettings = new FhirJsonParsingSettings(this.JsonParserSettings);
        }

        /// <summary>Creates a new <see cref="DirectorySourceSettings"/> object that is a copy of the current instance.</summary>
        public DirectorySourceSettings Clone() => new DirectorySourceSettings(this);

        /// <summary>Returns the default content directory of the <see cref="DirectorySource"/>.</summary>
        public static string SpecificationDirectory
        {
            get
            {
                var codebase = AppDomain.CurrentDomain.BaseDirectory;
                return Directory.Exists(codebase) ? codebase : Directory.GetCurrentDirectory();
            }
        }

        /// <summary>
        /// Gets or sets a value that determines wether the <see cref="DirectorySource"/> should
        /// also include artifacts from (nested) subdirectories of the specified content directory.
        /// <para>
        /// Returns <c>false</c> by default.
        /// </para>
        /// </summary>
        /// <remarks>
        /// Enabling this setting requires the <see cref="DirectorySource"/> instance
        /// to recursively scan all xml and json files that exist in the target directory
        /// structure, which could unexpectedly turn into a long running operation.
        /// Therefore, consumers should usually try to avoid to enable this setting when
        /// the DirectorySource is targeting:
        /// <list type="bullet">
        /// <item>
        /// <description>Directories with many deeply nested subdirectories</description>
        /// </item>
        /// <item>
        /// <description>Common folders such as Desktop, My Documents etc.</description>
        /// </item>
        /// <item>
        /// <description>Drive root folders such as C:\</description>
        /// </item>
        /// </list>
        /// </remarks>
        public bool IncludeSubDirectories { get; set; } // = false;

        /// <summary>
        /// Gets or sets the search string to match against the names of files in the content directory.
        /// The source will only provide resources from files that match the specified mask.
        /// The source will ignore all files that don't match the specified mask.
        /// Multiple masks can be split by '|'.
        /// <para>
        /// Returns <c>"*.*"</c> by default.
        /// </para>
        /// </summary>
        /// <remarks>
        /// Mask filters are applied first, before any <see cref="Includes"/> and <see cref="Excludes"/> filters.
        /// </remarks>
        /// <value>
        /// Supported wildcards:
        /// <list type="table">
        /// <item>
        /// <term>*</term>
        /// <description>Matches zero or more characters within a file or directory name.</description>
        /// </item>
        /// <item>
        /// <term>?</term>
        /// <description>Matches any single character</description>
        /// </item>
        /// </list>
        /// </value>
        /// <example>
        /// <code>Mask = "v2*.*|*.StructureDefinition.*";</code>
        /// </example>
        public string Mask
        {
            get => String.Join("|", Masks);
            set { Masks = SplitMask(value); }
        }

        static string[] SplitMask(string mask) => mask?.Split('|').Select(s => s.Trim()).Where(s => !String.IsNullOrEmpty(s)).ToArray();

        /// <summary>
        /// Gets or sets an array of search strings to match against the names of files in the content directory.
        /// The source will only provide resources from files that match the specified mask.
        /// The source will ignore all files that don't match the specified mask.
        /// <para>
        /// Returns <c>{ "*.*" }</c> by default.
        /// </para>
        /// </summary>
        /// <remarks>
        /// Mask filters are applied first, before <see cref="Includes"/> and <see cref="Excludes"/> filters.
        /// </remarks>
        /// <value>
        /// Supported wildcards:
        /// <list type="table">
        /// <item>
        /// <term>*</term>
        /// <description>Matches zero or more characters within a file or directory name.</description>
        /// </item>
        /// <item>
        /// <term>?</term>
        /// <description>Matches any single character</description>
        /// </item>
        /// </list>
        /// </value>
        /// <example>
        /// <code>Masks = new string[] { "v2*.*", "*.StructureDefinition.*" };</code>
        /// </example>
        public string[] Masks { get; set; } = DefaultMasks;

        /// <summary>
        /// Gets or sets an array of search strings to match against the names of subdirectories of the content directory.
        /// The source will only provide resources from subdirectories that match the specified include mask(s).
        /// The source will ignore all subdirectories that don't match the specified include mask(s).
        /// </summary>
        /// <remarks>
        /// Include filters are applied after <see cref="Mask"/> filters and before <see cref="Excludes"/> filters.
        /// </remarks>
        /// <value>
        /// Supported wildcards:
        /// <list type="table">
        /// <item>
        /// <term>*</term>
        /// <description>Matches zero or more characters within a directory name.</description>
        /// </item>
        /// <item>
        /// <term>**</term>
        /// <description>
        /// Recursive wildcard.
        /// For example, <c>/hello/**/*</c> matches all descendants of <c>/hello</c>.
        /// </description>
        /// </item>
        /// </list>
        /// </value>
        /// <example>
        /// <code>Includes = new string[] { "profiles/**/*", "**/valuesets" };</code>
        /// </example>
        public string[] Includes { get; set; }

        /// <summary>
        /// Gets or sets an array of search strings to match against the names of subdirectories of the content directory.
        /// The source will ignore all subdirectories that match the specified exclude mask(s).
        /// The source will only provide resources from subdirectories that don't match the specified exclude mask(s).
        /// </summary>
        /// <remarks>
        /// Exclude filters are applied last, after any <see cref="Mask"/> and <see cref="Includes"/> filters.
        /// </remarks>
        /// <value>
        /// Supported wildcards:
        /// <list type="table">
        /// <item>
        /// <term>*</term>
        /// <description>Matches zero or more characters within a directory name.</description>
        /// </item>
        /// <item>
        /// <term>**</term>
        /// <description>
        /// Recursive wildcard.
        /// For example, <c>/hello/**/*</c> matches all descendants of <c>/hello</c>.
        /// </description>
        /// </item>
        /// </list>
        /// </value>
        /// <example>
        /// <code>Excludes = new string[] { "profiles/**/old", "temp/**/*" };</code>
        /// </example>
        public string[] Excludes { get; set; }

        /// <summary>Gets or sets a value that determines how to process duplicate files with multiple serialization formats.</summary>
        /// <remarks>The default value is <see cref="DirectorySource.DuplicateFilenameResolution.PreferXml"/>.</remarks>
        public DirectorySource.DuplicateFilenameResolution FormatPreference { get; set; } = DefaultFormatPreference;

        /// <summary>
        /// Determines if the <see cref="DirectorySource"/> instance should harvest artifact
        /// summary information in parallel on the thread pool.
        /// </summary>
        /// <remarks>
        /// By default, the <see cref="DirectorySource"/> harvests artifact summaries serially
        /// on the calling thread. However if this option is enabled, then the DirectorySource
        /// performs summary harvesting in parallel on the thread pool, in order to speed up
        /// the process. This is especially effective when the content directory contains many
        /// (nested) subfolders and files.
        /// </remarks>
        public bool MultiThreaded { get; set; } // = false;

        /// <summary>
        /// An array of <see cref="ArtifactSummaryHarvester"/> delegates for harvesting
        /// summary details from an artifact.
        /// </summary>
        /// <remarks>
        /// Allows consumers to harvest custom summary properties,
        /// depending on the resource type or other (previously harvested) information.
        /// <para>
        /// By default, if this array is null or empty, the
        /// <see cref="ArtifactSummaryGenerator"/> calls the built-in default harvesters
        /// as defined by the <see cref="ArtifactSummaryGenerator.ConformanceHarvesters"/> array.
        /// However if the caller specifies one or more harvester delegates, then the summary
        /// generator calls only the provided delegates, in the specified order.
        /// A custom delegate array may include one or more of the default harvesters.
        /// </para>
        /// </remarks>
        public ArtifactSummaryHarvester[] SummaryDetailsHarvesters { get; set; }

        // [WMR 20180813] NEW

        /// <summary>
        /// Determines the behavior of the internal <see cref="ArtifactSummaryGenerator"/>
        /// for handling non-parseable (invalid or non-FHIR) content files.
        /// <para>
        /// By default (<c>false</c>), the source will generate summaries for all files
        /// that exist in the specified content directory and match the specified mask,
        /// including files that cannot be parsed (e.g. invalid or non-FHIR content).
        /// </para>
        /// <para>
        /// If <c>true</c>, then the source will only generate summaries for valid
        /// FHIR artifacts that exist in the specified content directory and match the
        /// specified mask. Unparseable files are ignored and excluded from the result.
        /// </para>
        /// </summary>
        public bool ExcludeSummariesForUnknownArtifacts { get; set; } // = false;

        /// <summary>
        /// Gets or sets the configuration settings that control the behavior of the PoCo parser.
        /// <para>Never returns <c>null</c>. Assigning <c>null</c> reverts back to default settings.</para>
        /// </summary>
        /// <value>A <see cref="ParserSettings"/> instance.</value>
        public ParserSettings ParserSettings
        {
            get => _parserSettings;
            set => _parserSettings = value ?? ParserSettings.CreateDefault();
        }

        /// <summary>
        /// Gets the configuration settings that control the behavior of the XML parser.
        /// <para>Never returns <c>null</c>. Assigning <c>null</c> reverts back to default settings.</para>
        /// </summary>
        /// <value>A <see cref="FhirXmlParsingSettings"/> instance.</value>
        public FhirXmlParsingSettings XmlParserSettings
        {
            get => _xmlParserSettings;
            set => _xmlParserSettings = value?.Clone() ?? FhirXmlParsingSettings.CreateDefault();
        }


        /// <summary>
        /// Gets the configuration settings that control the behavior of the JSON parser.
        /// <para>Never returns <c>null</c>. Assigning <c>null</c> reverts back to default settings.</para>
        /// </summary>
        /// <value>A <see cref="FhirJsonParsingSettings"/> instance.</value>
        public FhirJsonParsingSettings JsonParserSettings
        {
            get => _jsonParserSettings;
            set => _jsonParserSettings = value?.Clone() ?? FhirJsonParsingSettings.CreateDefault();
        }

        
    }

}