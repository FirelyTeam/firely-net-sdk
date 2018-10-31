/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Configuration settings for the <see cref="WebResolver"/> class.</summary>
    public class WebResolverSettings
    {
        /// <summary>Creates a new <see cref="DirectorySourceSettings"/> instance with default property values.</summary>
        public static WebResolverSettings CreateDefault() => new WebResolverSettings();

        // Instance fields
        ParserSettings _parserSettings = ParserSettings.CreateDefault();

        /// <summary>Default constructor. Creates a new <see cref="WebResolverSettings"/> instance with default property values.</summary>
        public WebResolverSettings()
        {
            // See property declarations for default initializers
        }

        /// <summary>Clone constructor. Generates a new <see cref="WebResolverSettings"/> instance initialized from the state of the specified instance.</summary>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public WebResolverSettings(WebResolverSettings settings)
        {
            if (settings == null) { throw Error.ArgumentNull(nameof(settings)); }
            settings.CopyTo(this);
        }

        /// <summary>Copy all configuration settings to another instance.</summary>
        /// <param name="other">Another <see cref="WebResolverSettings"/> instance.</param>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public void CopyTo(WebResolverSettings other)
        {
            if (other == null) { throw Error.ArgumentNull(nameof(other)); }

            // Clone state
            other.ParserSettings = new ParserSettings(this.ParserSettings);
            other.TimeOut = this.TimeOut;
            // No use cloning event handler; delegates are immutable
            other.ExceptionHandler = this.ExceptionHandler;
        }

        /// <summary>Creates a new <see cref="WebResolverSettings"/> object that is a copy of the current instance.</summary>
        public WebResolverSettings Clone() => new WebResolverSettings(this);

        /// <summary>
        /// Gets or sets the configuration settings that control the behavior of the PoCo parser.
        /// <para>Never returns <c>null</c>. Assigning <c>null</c> reverts back to default settings.</para>
        /// </summary>
        /// <value>A <see cref="ParserSettings"/> instance.</value>
        public ParserSettings ParserSettings
        {
            get => _parserSettings;
            set => _parserSettings = value ?? ParserSettings.CreateDefault();
        }

        /// <summary>Gets or sets the request timeout of the internal <see cref="Hl7.Fhir.Rest.FhirClient"/> instance.</summary>
        public int TimeOut { get; set; } = WebResolver.DefaultTimeOut;

        #region IExceptionSource

        /// <summary>Gets or sets an optional <see cref="ExceptionNotificationHandler"/> for custom error handling.</summary>
        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        #endregion

    }
}
