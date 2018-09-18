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
    public class FhirJsonBuilderSettings
    {
        /// <summary>
        /// When encountering a member without type information, just skip it instead of reporting an error.
        /// </summary>
        public bool IgnoreUnknownElements;

        /// <summary>
        /// Format the json output when converted to a string.
        /// </summary>
        public bool Pretty;

        /// <summary>Default constructor. Creates a new <see cref="FhirJsonBuilderSettings"/> instance with default property values.</summary>
        public FhirJsonBuilderSettings() {  }

        /// <summary>Clone constructor. Generates a new <see cref="FhirJsonBuilderSettings"/> instance initialized from the state of the specified instance.</summary>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public FhirJsonBuilderSettings(FhirJsonBuilderSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));
            other.CopyTo(this);
        }

        /// <summary>Copy all configuration settings to another instance.</summary>
        /// <param name="other">Another <see cref="FhirJsonBuilderSettings"/> instance.</param>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public void CopyTo(FhirJsonBuilderSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));

            other.IgnoreUnknownElements = IgnoreUnknownElements;
            other.Pretty = Pretty;
        }

        /// <summary>Creates a new <see cref="FhirJsonBuilderSettings"/> object that is a copy of the current instance.</summary>
        public FhirJsonBuilderSettings Clone() => new FhirJsonBuilderSettings(this);

        /// <summary>Creates a new <see cref="FhirJsonBuilderSettings"/> instance with default property values.</summary>
        public static FhirJsonBuilderSettings CreateDefault() => new FhirJsonBuilderSettings();
    }
}
