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
    /// <summary>Configuration settings for the <see cref="PocoBuilder"/> class.</summary>
    public class PocoBuilderSettings
    {
        /// <summary>
        /// Do not throw when encountering values not parseable as a member of an enumeration in a Poco.
        /// </summary>
        public bool AllowUnrecognizedEnums { get; set; } // = false

        /// <summary>
        /// Do not throw when the data has an element that does not map to a property in the Poco.
        /// </summary>
        public bool IgnoreUnknownMembers { get; set; } // = false

        /// <summary>Default constructor. Creates a new <see cref="PocoBuilderSettings"/> instance with default property values.</summary>
        public PocoBuilderSettings() { }

        /// <summary>Clone constructor. Generates a new <see cref="PocoBuilderSettings"/> instance initialized from the state of the specified instance.</summary>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public PocoBuilderSettings(PocoBuilderSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));
            other.CopyTo(this);
        }

        /// <summary>Copy all configuration settings to another instance.</summary>
        /// <param name="other">Another <see cref="PocoBuilderSettings"/> instance.</param>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public void CopyTo(PocoBuilderSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));

            other.AllowUnrecognizedEnums = AllowUnrecognizedEnums;
            other.IgnoreUnknownMembers = IgnoreUnknownMembers;
        }

        /// <summary>Creates a new <see cref="PocoBuilderSettings"/> object that is a copy of the current instance.</summary>
        public PocoBuilderSettings Clone() => new PocoBuilderSettings(this);

        /// <summary>Creates a new <see cref="PocoBuilderSettings"/> instance with default property values.</summary>
        public static PocoBuilderSettings CreateDefault() => new PocoBuilderSettings();
    }
}
