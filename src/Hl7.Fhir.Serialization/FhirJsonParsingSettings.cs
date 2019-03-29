/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */


using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Serialization
{
    /// <summary>Parser configuration settings for the <see cref="FhirJsonNode"/> class.</summary>
    public class FhirJsonParsingSettings
    {
        /// <summary>
        /// Do not raise exceptions for recoverable errors.
        /// </summary>
        public bool PermissiveParsing { get; set; } // = false;

        /// <summary>
        /// Allow DSTU2-style Json comment members.
        /// </summary>
        public bool AllowJsonComments { get; set; } // = false;

#if !NETSTANDARD1_1
        /// <summary>
        /// Validate narrative against the FHIR Xhtml schema.
        /// </summary>
        /// <remarks>Validation of xhtml is expensive, so turned off by default.</remarks>
        public bool ValidateFhirXhtml { get; set; } // = false;
#endif

        /// <summary>Default constructor. Creates a new <see cref="FhirJsonParsingSettings"/> instance with default property values.</summary>
        public FhirJsonParsingSettings() { }

        /// <summary>Clone constructor. Generates a new <see cref="FhirJsonParsingSettings"/> instance initialized from the state of the specified instance.</summary>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public FhirJsonParsingSettings(FhirJsonParsingSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));
            other.CopyTo(this);
        }

        /// <summary>Copy all configuration settings to another instance.</summary>
        /// <param name="other">Another <see cref="FhirJsonParsingSettings"/> instance.</param>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public void CopyTo(FhirJsonParsingSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));

            other.PermissiveParsing = PermissiveParsing;
            other.AllowJsonComments = AllowJsonComments;

#if !NETSTANDARD1_1
            other.ValidateFhirXhtml = ValidateFhirXhtml;
#endif
        }

        /// <summary>Creates a new <see cref="FhirJsonParsingSettings"/> object that is a copy of the current instance.</summary>
        public FhirJsonParsingSettings Clone() => new FhirJsonParsingSettings(this);

        /// <summary>Creates a new <see cref="FhirJsonParsingSettings"/> instance with default property values.</summary>
        public static FhirJsonParsingSettings CreateDefault() => new FhirJsonParsingSettings();
    }
}