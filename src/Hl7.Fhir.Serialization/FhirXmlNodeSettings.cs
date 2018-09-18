/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization
{
    public class FhirXmlNodeSettings
    {
        /// <summary>
        /// A list of namespaces which are allowed in addition to the normal FHIR namespaces. 
        /// </summary>
        /// <remarks>Normally, the only allowed namespaces are 'http://hl7.org/fhir' and the XHTML namespace.</remarks>
        public XNamespace[] AllowedExternalNamespaces;

        /// <summary>
        /// Raise an errors when an xsi:schemaLocation attribute is found on the root.
        /// </summary>
        public bool DisallowSchemaLocation;

        /// <summary>
        /// Do not raise exceptions for recoverable errors.
        /// </summary>
        public bool PermissiveParsing;

#if NET_XSD_SCHEMA
        /// <summary>
        /// Validate narrative against the FHIR Xhtml schema.
        /// </summary>
        /// <remarks>Validation of xhtml is expensive, so turned off by default.</remarks>
        public bool ValidateFhirXhtml;
#endif
        /// <summary>Default constructor. Creates a new <see cref="FhirXmlNodeSettings"/> instance with default property values.</summary>
        public FhirXmlNodeSettings() { }

        /// <summary>Clone constructor. Generates a new <see cref="FhirXmlNodeSettings"/> instance initialized from the state of the specified instance.</summary>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public FhirXmlNodeSettings(FhirXmlNodeSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));
            other.CopyTo(this);
        }

        /// <summary>Copy all configuration settings to another instance.</summary>
        /// <param name="other">Another <see cref="FhirXmlNodeSettings"/> instance.</param>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public void CopyTo(FhirXmlNodeSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));

            other.AllowedExternalNamespaces = (XNamespace[])AllowedExternalNamespaces?.Clone();
            other.DisallowSchemaLocation = DisallowSchemaLocation;
            other.PermissiveParsing = PermissiveParsing;

#if NET_XSD_SCHEMA
            other.ValidateFhirXhtml = ValidateFhirXhtml;
#endif
        }

        /// <summary>Creates a new <see cref="FhirXmlNodeSettings"/> object that is a copy of the current instance.</summary>
        public FhirXmlNodeSettings Clone() => new FhirXmlNodeSettings(this);

        /// <summary>Creates a new <see cref="FhirJsonBuilderSettings"/> instance with default property values.</summary>
        public static FhirXmlNodeSettings CreateDefault() => new FhirXmlNodeSettings();
    }
}