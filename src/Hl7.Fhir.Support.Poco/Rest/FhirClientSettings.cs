using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Rest
{
    public class FhirClientSettings
    {
        /// <summary>
        /// Whether or not to ask the server for a CapabilityStatement and verify FHIR version compatibility before
        /// issuing requests to the server.
        /// </summary>
        public bool VerifyFhirVersion;

        /// <summary>
        /// The preferred format of the content to be used when communicating with the FHIR server (XML or JSON)
        /// </summary>
        public ResourceFormat PreferredFormat = ResourceFormat.Xml;

        /// <summary>
        /// When passing the content preference, use the _format parameter instead of the request header
        /// </summary>
        public bool UseFormatParameter;

        /// <summary>
        /// When <see langword="true"/> the MIME-type parameter fhirVersion will be added the Accept header. This is necessary 
        /// when the FHIR server supports multiple FHIR versions.
        /// </summary>
        public bool UseFhirVersionInAcceptHeader = false;

        /// <summary>
        /// The timeout (in milliseconds) to be used when making calls to the FHIR server
        /// </summary>
        public int Timeout = 100 * 1000;

        /// <summary>
        /// Should calls to Create, Update and transaction operations return the whole updated content, 
        /// or an OperationOutcome?
        /// </summary>
        /// <remarks>Refer to specification section 2.1.0.5 (Managing Return Content)</remarks>
        /// <remarks>Setting this to null, will ensure the client does not send a Preferred header</remarks>
        public Prefer? PreferredReturn = null;

        /// <summary>
        /// Should server return which search parameters were supported after executing a search?
        /// If true, the server should return an error for any unknown or unsupported parameter, otherwise
        /// the server may ignore any unknown or unsupported parameter.
        /// </summary>
        public SearchParameterHandling? PreferredParameterHandling = null;

        /// <summary>
        /// This will do 2 things:
        /// 1. Add the header Accept-Encoding: gzip, deflate
        /// 2. decompress any responses that have Content-Encoding: gzip (or deflate)
        /// </summary>
        public bool PreferCompressedResponses;

        /// <summary>
        /// Compress any Request bodies 
        /// (warning, if a server does not handle compressed requests you will get a 415 response)
        /// </summary>
        public bool CompressRequestBody;

        public ParserSettings ParserSettings = ParserSettings.CreateDefault();

        public FhirClientSettings() { }

        /// <summary>Clone constructor. Generates a new <see cref="FhirClientSettings"/> instance initialized from the state of the specified instance.</summary>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public FhirClientSettings(FhirClientSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));
            other.CopyTo(this);
        }

        /// <summary>Copy all configuration settings to another instance.</summary>
        /// <param name="other">Another <see cref="FhirClientSettings"/> instance.</param>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public void CopyTo(FhirClientSettings other)
        {
            if (other == null) throw Error.ArgumentNull(nameof(other));

            other.CompressRequestBody = CompressRequestBody;
            other.ParserSettings = ParserSettings;
            other.PreferCompressedResponses = PreferCompressedResponses;
            other.PreferredFormat = PreferredFormat;
            other.PreferredReturn = PreferredReturn;
            other.Timeout = Timeout;
            other.UseFormatParameter = UseFormatParameter;
            other.UseFhirVersionInAcceptHeader = UseFhirVersionInAcceptHeader;
            other.VerifyFhirVersion = VerifyFhirVersion;
            other.PreferredParameterHandling = PreferredParameterHandling;
        }

        /// <summary>Creates a new <see cref="FhirClientSettings"/> object that is a copy of the current instance.</summary>
        public FhirClientSettings Clone() => new FhirClientSettings(this);

        /// <summary>Creates a new <see cref="FhirClientSettings"/> instance with default property values.</summary>
        public static FhirClientSettings CreateDefault() => new FhirClientSettings();
    }
}

