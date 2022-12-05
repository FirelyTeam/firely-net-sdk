/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Specification.Terminology
{
    /// <summary>Configuration settings for the "ValueSetExpander" class.</summary>
    public class ValueSetExpanderSettings
    {
        /// <summary>Default value of the <see cref="MaxExpansionSize"/> property.</summary>
        public const int DefaultMaxExpansionSize = 500;

        [Obsolete("Use the CreateDefault() method, as using this static member may cause threading issues.")]
        public static ValueSetExpanderSettings Default = new();

        /// <summary>
        /// The <see cref="IResourceResolver"/> or <see cref="IAsyncResourceResolver" /> to use when a reference 
        /// to another valueset is encountered.
        /// </summary>
#pragma warning disable CS0618 // Type or member is obsolete
        public ISyncOrAsyncResourceResolver ValueSetSource { get; set; }
#pragma warning restore CS0618 // Type or member is obsolete

        /// <summary>
        /// The maximum number of concepts to include in an expansion before the expander raises an error.
        /// </summary>
        public int MaxExpansionSize { get; set; } = DefaultMaxExpansionSize;

        /// <summary>
        /// Controls whether concept designations are to be included or excluded in value set expansions
        /// </summary>
        public bool IncludeDesignations { get; set; }

        /// <summary>Default constructor. Creates a new <see cref="ValueSetExpanderSettings"/> instance with default property values.</summary>
        public ValueSetExpanderSettings() { }

        /// <summary>Clone constructor. Generates a new <see cref="ValueSetExpanderSettings"/> instance initialized from the state of the specified instance.</summary>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public ValueSetExpanderSettings(ValueSetExpanderSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));
            other.CopyTo(this);
        }

        /// <summary>Copy all configuration settings to another instance.</summary>
        /// <param name="other">Another <see cref="ValueSetExpanderSettings"/> instance.</param>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public void CopyTo(ValueSetExpanderSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));

            other.MaxExpansionSize = MaxExpansionSize;
            other.ValueSetSource = ValueSetSource;
            other.IncludeDesignations = IncludeDesignations;
        }

        /// <summary>Creates a new <see cref="ValueSetExpanderSettings"/> object that is a copy of the current instance.</summary>
        public ValueSetExpanderSettings Clone() => new ValueSetExpanderSettings(this);

        /// <summary>Creates a new <see cref="ValueSetExpanderSettings"/> instance with default property values.</summary>
        public static ValueSetExpanderSettings CreateDefault() => new ValueSetExpanderSettings();

    }
}
