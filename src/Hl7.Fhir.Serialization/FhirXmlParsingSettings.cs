/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    /// <summary>Parser configuration settings for the <see cref="FhirXmlNode"/> class.</summary>
    public class FhirXmlParsingSettings
    {
        /// <summary>
        /// A list of namespaces which are allowed in addition to the normal FHIR namespaces. 
        /// </summary>
        /// <remarks>Normally, the only allowed namespaces are 'http://hl7.org/fhir' and the XHTML namespace.</remarks>
        public XNamespace[] AllowedExternalNamespaces { get; set; }

        /// <summary>
        /// Raise an errors when an xsi:schemaLocation attribute is found on the root.
        /// </summary>
        public bool DisallowSchemaLocation { get; set; } // = false;

        /// <summary>
        /// Do not raise exceptions for recoverable errors.
        /// </summary>
        public bool PermissiveParsing { get; set; } = true;

#if !NETSTANDARD1_1
        /// <summary>
        /// Validate narrative against the FHIR Xhtml schema.
        /// </summary>
        /// <remarks>Validation of xhtml is expensive, so turned off by default.</remarks>
        public bool ValidateFhirXhtml { get; set; } // = false;
#endif
        /// <summary>Default constructor. Creates a new <see cref="FhirXmlParsingSettings"/> instance with default property values.</summary>
        public FhirXmlParsingSettings() { }

        /// <summary>Clone constructor. Generates a new <see cref="FhirXmlParsingSettings"/> instance initialized from the state of the specified instance.</summary>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public FhirXmlParsingSettings(FhirXmlParsingSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));
            other.CopyTo(this);
        }

        /// <summary>Copy all configuration settings to another instance.</summary>
        /// <param name="other">Another <see cref="FhirXmlParsingSettings"/> instance.</param>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public void CopyTo(FhirXmlParsingSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));

            other.AllowedExternalNamespaces = (XNamespace[])AllowedExternalNamespaces?.Clone();
            other.DisallowSchemaLocation = DisallowSchemaLocation;
            other.PermissiveParsing = PermissiveParsing;

#if !NETSTANDARD1_1
            other.ValidateFhirXhtml = ValidateFhirXhtml;
#endif
        }

        /// <summary>Creates a new <see cref="FhirXmlParsingSettings"/> object that is a copy of the current instance.</summary>
        public FhirXmlParsingSettings Clone() => new FhirXmlParsingSettings(this);

        /// <summary>Creates a new <see cref="FhirJsonSerializationSettings"/> instance with default property values.</summary>
        public static FhirXmlParsingSettings CreateDefault() => new FhirXmlParsingSettings();
    }
}