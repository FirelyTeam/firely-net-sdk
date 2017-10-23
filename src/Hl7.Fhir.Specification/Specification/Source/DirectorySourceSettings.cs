/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.IO;
using System.Linq;

namespace Hl7.Fhir.Specification.Source
{
    // cf. SnapshotGeneratorSettings, ValidationSettings

    /// <summary>Configuration settings for the <see cref="DirectorySource"/> class.</summary>
    public sealed class DirectorySourceSettings
#if DOTNETFW
        : ICloneable
#endif
    {
        // public static readonly ArtifactSummaryHarvester DefaultHarvester = DefaultArtifactSummaryHarvester.Harvest;
        // public static readonly NavigatorStreamFactory DefaultStreamFactory = DefaultNavigatorStreamFactory.Create;

        /// <summary>Returns the default configuration settings for the <see cref="DirectorySource"/> class.</summary>
        public static DirectorySourceSettings Default = new DirectorySourceSettings()
            {
                StreamFactory = DefaultNavigatorStreamFactory.Create,
                Harvester = DefaultArtifactSummaryHarvester.Harvest,
                IncludeSubDirectories = false,
                Masks = new[] { "*.*" }
        };

        /// <summary>Creates a new <see cref="DirectorySourceSettings"/> instance.</summary>
        public DirectorySourceSettings()
        {

        }

        /// <summary>Clone ctor. Generates a new <see cref="DirectorySourceSettings"/> instance with the same state as the specified instance.</summary>
        public DirectorySourceSettings(DirectorySourceSettings settings)
        {
            if (settings == null) { throw Error.ArgumentNull(nameof(settings)); }
            settings.CopyTo(this);
        }

        public void CopyTo(DirectorySourceSettings other)
        {
            other.IncludeSubDirectories = this.IncludeSubDirectories;
            other.StreamFactory = this.StreamFactory;
            other.Harvester = this.Harvester;
        }

        /// <summary>Returns an exact clone of the current configuration settings instance.</summary>
        public object Clone() => new DirectorySourceSettings(this);

        // <summary>Gets or sets the full path of the target directory for the <see cref="DirectorySource"/>.</summary>
        // public string ContentDirectory { get; set; }

        /// <summary>Returns the default content directory of the <see cref="DirectorySource"/>.</summary>
        public static string SpecificationDirectory
        {
            get
            {
#if DOTNETFW
                var codebase = AppDomain.CurrentDomain.BaseDirectory;
#else
                var codebase = AppContext.BaseDirectory;
#endif
                return Directory.Exists(codebase) ? codebase : Directory.GetCurrentDirectory();
            }
        }

        /// <summary>
        /// Gets or sets a value that determines wether the <see cref="DirectorySource"/> should also
        /// recursively scan all subdirectories of the specified content directory.
        /// </summary>
        public bool IncludeSubDirectories { get; set; } // = false;

        /// <summary>
        /// Gets or sets the search string to match against the names of files in the content directory.
        /// The source will only provide resources from files that match the specified mask.
        /// The source will ignore all files that don't match the specified mask.
        /// Multiple masks can be split by '|'.
        /// </summary>
        /// <remarks>
        /// Mask filters are applied first, before any <see cref="Includes"/> and <see cref="Excludes"/> filters.
        /// </remarks>
        /// <value>
        /// Supported wildcards:
        /// <list type="bullet">
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

        internal static string[] SplitMask(string mask) => mask?.Split('|').Select(s => s.Trim()).Where(s => !String.IsNullOrEmpty(s)).ToArray();

        /// <summary>
        /// Gets or sets an array of search strings to match against the names of files in the content directory.
        /// The source will only provide resources from files that match the specified mask.
        /// The source will ignore all files that don't match the specified mask.
        /// </summary>
        /// <remarks>
        /// Mask filters are applied first, before <see cref="Includes"/> and <see cref="Excludes"/> filters.
        /// </remarks>
        /// <value>
        /// Supported wildcards:
        /// <list type="bullet">
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
        public string[] Masks { get; set; } // = new[] { "*.*" }

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
        /// <list type="bullet">
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
        /// <list type="bullet">
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

        /// <summary>
        /// Gets or sets a value that determines how to process duplicate files with multiple serialization formats.
        /// </summary>
        public DirectorySource.DuplicateFilenameResolution FormatPreference { get; set; } = DirectorySource.DuplicateFilenameResolution.PreferXml;

        /// <summary>
        /// Gets or sets the <see cref="NavigatorStreamFactory"/> delegate that the <see cref="DirectorySource"/>
        /// uses to create <see cref="INavigatorStream"/> instances for harvesting summary information from
        /// FHIR artifacts, independent of the underlying resource serialization format.
        /// By default, this delegate calls the <see cref="DefaultNavigatorStreamFactory.Create"/> method.
        /// </summary>
        public NavigatorStreamFactory StreamFactory { get; set; } = DefaultNavigatorStreamFactory.Create;

        /// <summary>
        /// Gets or sets the <see cref="ArtifactSummaryHarvester"/> delegate that the <see cref="DirectorySource"/>
        /// uses to harvest summary information from all the available FHIR artifacts,
        /// independent of the underlying resource serialization format.
        /// By default, this delegate calls the <see cref="DefaultArtifactSummaryHarvester.Harvest"/> method.
        /// </summary>
        public ArtifactSummaryHarvester Harvester { get; set; } = DefaultArtifactSummaryHarvester.Harvest;

    }

}
