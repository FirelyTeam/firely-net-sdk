/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Serialization
{
    public class ParserSettings
    {
        [Obsolete("Due to a bug, the Default has always been ignored, so it is now officially deprecated")]
        public static readonly ParserSettings Default = new ParserSettings() { AcceptUnknownMembers = false, AllowUnrecognizedEnums = false, DisallowXsiAttributesOnRoot = true };

        /// <summary>
        /// Raise an error when an xsi:schemaLocation is encountered.
        /// </summary>
        public bool DisallowXsiAttributesOnRoot { get; set; }

        /// <summary>
        /// Do not throw when encountering values not parseable as a member of an enumeration in a Poco.
        /// </summary>
        public bool AllowUnrecognizedEnums { get; set; }

        /// <summary>
        /// Do not throw when the data has an element that does not map to a property in the Poco.
        /// </summary>
        public bool AcceptUnknownMembers { get; set; }

        /// <summary>Default constructor. Creates a new <see cref="ParserSettings"/> instance with default property values.</summary>
        public ParserSettings() { }

        /// <summary>Clone constructor. Generates a new <see cref="ParserSettings"/> instance initialized from the state of the specified instance.</summary>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public ParserSettings(ParserSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));
            other.CopyTo(this);
        }

        /// <summary>Copy all configuration settings to another instance.</summary>
        /// <param name="other">Another <see cref="ParserSettings"/> instance.</param>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public void CopyTo(ParserSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));

            other.DisallowXsiAttributesOnRoot = DisallowXsiAttributesOnRoot;
            other.AllowUnrecognizedEnums = AllowUnrecognizedEnums;
            other.AcceptUnknownMembers = AllowUnrecognizedEnums;
        }

        /// <summary>Creates a new <see cref="ParserSettings"/> object that is a copy of the current instance.</summary>
        public ParserSettings Clone() => new ParserSettings(this);

        /// <summary>Creates a new <see cref="ParserSettings"/> instance with default property values.</summary>
        public static ParserSettings CreateDefault() => new ParserSettings();
    }
}
