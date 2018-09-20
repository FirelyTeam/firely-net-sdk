/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System;
using System.Linq;

namespace Hl7.Fhir.ElementModel
{
    public class MaskingNodeSettings
    {
        /// <summary>
        /// The different ways in which Bundles are masked.
        /// </summary>
        public enum PreserveBundleMode
        {
            /// <summary>
            /// All Bundles (including nested) are masked like any other resource 
            /// </summary>
            None,

            /// <summary>
            /// The Bundle at the root is preserved, nested bundles are masked
            /// </summary>
            Root,

            /// <summary>
            /// All Bundles (including nested) are exempt from masking
            /// </summary>
            All
        }


        /// <summary>
        /// Determines how Bundles are masked.
        /// </summary>
        public PreserveBundleMode PreserveBundle;

        /// <summary>
        /// Include top-level mandatory elements, including all their children
        /// </summary>
        public bool IncludeMandatory;

        /// <summary>
        /// Include all elements marked "in summary" in the definition of the element
        /// </summary>
        public bool IncludeInSummary;

        ///// <summary>
        ///// Include all elements marked "is modifier" in the definition of the element
        ///// </summary>
        //public bool IncludeIsModifier;

        /// <summary>
        /// Exclude all elements of type "Narrative"
        /// </summary>
        public bool ExcludeNarrative;

        /// <summary>
        /// Exclude all elements of type "Markdown"
        /// </summary>
        public bool ExcludeMarkdown;

        /// <summary>
        /// Start by including all elements
        /// </summary>
        public bool IncludeAll;

        /// <summary>
        /// List of names op top-level elements to include, including their children
        /// </summary>
        public string[] IncludeElements;

        /// <summary>
        /// List of top-level elements to exclude
        /// </summary>
        public string[] ExcludeElements;

        /// <summary>Default constructor. Creates a new <see cref="MaskingNodeSettings"/> instance with default property values.</summary>
        public MaskingNodeSettings() { }

        /// <summary>Clone constructor. Generates a new <see cref="MaskingNodeSettings"/> instance initialized from the state of the specified instance.</summary>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public MaskingNodeSettings(MaskingNodeSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));
            other.CopyTo(this);
        }

        /// <summary>Copy all configuration settings to another instance.</summary>
        /// <param name="other">Another <see cref="MaskingNodeSettings"/> instance.</param>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public void CopyTo(MaskingNodeSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));

            other.PreserveBundle = this.PreserveBundle;
            other.IncludeMandatory = this.IncludeMandatory;
            other.IncludeInSummary = this.IncludeInSummary;
            // other.IncludeIsModifier = this.IncludeIsModifier;
            other.ExcludeMarkdown = this.ExcludeMarkdown;
            other.ExcludeNarrative = this.ExcludeNarrative;
            other.IncludeAll = this.IncludeAll;
            other.IncludeElements = this.IncludeElements?.ToArray();
            other.ExcludeElements = this.ExcludeElements?.ToArray();
        }

        /// <summary>Creates a new <see cref="MaskingNodeSettings"/> object that is a copy of the current instance.</summary>
        public MaskingNodeSettings Clone() => new MaskingNodeSettings(this);

        /// <summary>Creates a new <see cref="MaskingNodeSettings"/> instance with default property values.</summary>
        public static MaskingNodeSettings CreateDefault() => new MaskingNodeSettings();
    }
}
