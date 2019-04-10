/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */


using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Serialization
{
    public class SerializerSettings
    {
        /// <summary>
        /// Format the serialized xml or json output.
        /// </summary>
        public bool Pretty { get; set; } // = false;

        /// <summary>Default constructor. Creates a new <see cref="SerializerSettings"/> instance with default property values.</summary>
        public SerializerSettings() { }

        /// <summary>Clone constructor. Generates a new <see cref="SerializerSettings"/> instance initialized from the state of the specified instance.</summary>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public SerializerSettings(SerializerSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));
            other.CopyTo(this);
        }

        /// <summary>Copy all configuration settings to another instance.</summary>
        /// <param name="other">Another <see cref="SerializerSettings"/> instance.</param>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public void CopyTo(SerializerSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));

            other.Pretty = Pretty;
        }

        /// <summary>Creates a new <see cref="SerializerSettings"/> object that is a copy of the current instance.</summary>
        public SerializerSettings Clone() => new SerializerSettings(this);

        /// <summary>Creates a new <see cref="SerializerSettings"/> instance with default property values.</summary>
        public static SerializerSettings CreateDefault() => new SerializerSettings();
    }
}
