/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */


using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Serialization
{
    public class FhirJsonNodeSettings
    {
        /// <summary>
        /// Do not raise exceptions for recoverable errors.
        /// </summary>
        public bool PermissiveParsing;

        /// <summary>
        /// Allow DSTU2-style Json comment members.
        /// </summary>
        public bool AllowJsonComments;

#if NET_XSD_SCHEMA
        /// <summary>
        /// Validate narrative against the FHIR Xhtml schema.
        /// </summary>
        /// <remarks>Validation of xhtml is expensive, so turned off by default.</remarks>
        public bool ValidateFhirXhtml;
#endif

        /// <summary>Default constructor. Creates a new <see cref="FhirJsonNodeSettings"/> instance with default property values.</summary>
        public FhirJsonNodeSettings() { }

        /// <summary>Clone constructor. Generates a new <see cref="FhirJsonNodeSettings"/> instance initialized from the state of the specified instance.</summary>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public FhirJsonNodeSettings(FhirJsonNodeSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));
            other.CopyTo(this);
        }

        /// <summary>Copy all configuration settings to another instance.</summary>
        /// <param name="other">Another <see cref="FhirJsonNodeSettings"/> instance.</param>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public void CopyTo(FhirJsonNodeSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));

            other.PermissiveParsing = PermissiveParsing;
            other.AllowJsonComments = AllowJsonComments;

#if NET_XSD_SCHEMA
            other.ValidateFhirXhtml = ValidateFhirXhtml;
#endif
        }

        /// <summary>Creates a new <see cref="FhirJsonNodeSettings"/> object that is a copy of the current instance.</summary>
        public FhirJsonNodeSettings Clone() => new FhirJsonNodeSettings(this);

        /// <summary>Creates a new <see cref="FhirJsonNodeSettings"/> instance with default property values.</summary>
        public static FhirJsonNodeSettings CreateDefault() => new FhirJsonNodeSettings();
    }
}