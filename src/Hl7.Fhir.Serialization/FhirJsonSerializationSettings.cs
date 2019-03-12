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
    /// <summary>Configuration settings for the <see cref="FhirJsonBuilder"/> class.</summary>
    public class FhirJsonSerializationSettings
    {
        /// <summary>
        /// When encountering a member without type information, just skip it instead of reporting an error.
        /// </summary>
        public bool IgnoreUnknownElements { get; set; } // = false;

        /// <summary>
        /// Format the json output when converted to a string.
        /// </summary>
        public bool Pretty { get; set; } // = false;

        /// <summary>Default constructor. Creates a new <see cref="FhirJsonSerializationSettings"/> instance with default property values.</summary>
        public FhirJsonSerializationSettings() {  }

        /// <summary>Clone constructor. Generates a new <see cref="FhirJsonSerializationSettings"/> instance initialized from the state of the specified instance.</summary>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public FhirJsonSerializationSettings(FhirJsonSerializationSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));
            other.CopyTo(this);
        }

        /// <summary>Copy all configuration settings to another instance.</summary>
        /// <param name="other">Another <see cref="FhirJsonSerializationSettings"/> instance.</param>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public void CopyTo(FhirJsonSerializationSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));

            other.IgnoreUnknownElements = IgnoreUnknownElements;
            other.Pretty = Pretty;
        }

        /// <summary>Creates a new <see cref="FhirJsonSerializationSettings"/> object that is a copy of the current instance.</summary>
        public FhirJsonSerializationSettings Clone() => new FhirJsonSerializationSettings(this);

        /// <summary>Creates a new <see cref="FhirJsonSerializationSettings"/> instance with default property values.</summary>
        public static FhirJsonSerializationSettings CreateDefault() => new FhirJsonSerializationSettings();
    }
}
