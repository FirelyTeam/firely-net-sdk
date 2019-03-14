using System;

namespace Hl7.Fhir.Rest
{
    public class FhirStu3Client : FhirClient<Model.STU3.Bundle, Model.STU3.CapabilityStatement>
    {
        /// <summary>
        /// Creates a new client using a default endpoint
        /// If the endpoint does not end with a slash (/), it will be added.
        /// </summary>
        /// <param name="endpoint">
        /// The URL of the server to connect to.<br/>
        /// If the trailing '/' is not present, then it will be appended automatically
        /// </param>
        /// <param name="verifyFhirVersion">
        /// If parameter is set to true the first time a request is made to the server a 
        /// conformance check will be made to check that the FHIR versions are compatible.
        /// When they are not compatible, a FhirException will be thrown.
        /// </param>
        public FhirStu3Client(Uri endpoint, bool verifyFhirVersion = false) : 
            base(
                endpoint,
                new FhirVersionSettings(
                    Model.Version.STU3,
                    Model.STU3.ModelInfo.Version, 
                    (data, contentType) => new Model.STU3.Binary {  Content = data, ContentType = contentType }
                ),
                verifyFhirVersion
            )
        { }

        /// <summary>
        /// Creates a new client using a default endpoint
        /// If the endpoint does not end with a slash (/), it will be added.
        /// </summary>
        /// <param name="endpoint">
        /// The URL of the server to connect to.<br/>
        /// If the trailing '/' is not present, then it will be appended automatically
        /// </param>
        /// <param name="verifyFhirVersion">
        /// If parameter is set to true the first time a request is made to the server a 
        /// conformance check will be made to check that the FHIR versions are compatible.
        /// When they are not compatible, a FhirException will be thrown.
        /// </param>
        public FhirStu3Client(string endpoint, bool verifyFhirVersion = false)
            : this(new Uri(endpoint), verifyFhirVersion)
        { }

        public override string GetFhirTypeNameForType(Type type)
        {
            return Model.STU3.ModelInfo.GetFhirTypeNameForType(type);
        }
    }
}
