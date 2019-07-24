using Hl7.Fhir.Serialization;

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

        public ParserSettings ParserSettings = Hl7.Fhir.Serialization.ParserSettings.Default;
    }
}
