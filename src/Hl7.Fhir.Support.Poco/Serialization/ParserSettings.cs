/*
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Serialization
{
    /// <summary>Common parser configuration settings for BaseFhirParser and subclasses.</summary>
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

        /// <summary>
        /// Do not raise exceptions for recoverable errors.
        /// </summary>
        public bool PermissiveParsing { get; set; } = true;

        /// <summary>
        /// Allow to parse a FHIR dateTime values into an element of type date.
        /// </summary>
        /// <remarks>
        /// Needed for backward compatibility with old parser for resources which were saved and considered valid in the past.
        /// </remarks>>
        [Obsolete("Needed for backward compatibility with old parser for resources which were saved and considered valid in the past. " +
            "Should not be used in new code.")]
        public bool TruncateDateTimeToDate { get; set; } = false;

        /// <summary>
        /// A Handler to permit intercepting Exceptions during parsing
        /// </summary>
        public ExceptionNotificationHandler ExceptionHandler { get; set; }

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
            other.AcceptUnknownMembers = AcceptUnknownMembers;
            other.PermissiveParsing = PermissiveParsing;
#pragma warning disable CS0618 // Type or member is obsolete
            other.TruncateDateTimeToDate = TruncateDateTimeToDate;
#pragma warning restore CS0618 // Type or member is obsolete
            other.ExceptionHandler = ExceptionHandler;
        }

        /// <summary>
        /// Copy the necessary settings to PocoBuilderSettings
        /// </summary>
        /// <param name="settings">The instance where the settings are copied to.</param>
        public void CopyTo(PocoBuilderSettings settings)
        {
            if (settings == null) throw Error.ArgumentNull(nameof(settings));

            settings.AllowUnrecognizedEnums = AllowUnrecognizedEnums;
            settings.IgnoreUnknownMembers = AcceptUnknownMembers;
#pragma warning disable CS0618 // Type or member is obsolete
            settings.TruncateDateTimeToDate = TruncateDateTimeToDate;
#pragma warning restore CS0618 // Type or member is obsolete
            settings.ExceptionHandler = ExceptionHandler;
        }

        /// <summary>Creates a new <see cref="ParserSettings"/> object that is a copy of the current instance.</summary>
        public ParserSettings Clone() => new ParserSettings(this);

        /// <summary>Creates a new <see cref="ParserSettings"/> instance with default property values.</summary>
        public static ParserSettings CreateDefault() => new ParserSettings();
    }
}
