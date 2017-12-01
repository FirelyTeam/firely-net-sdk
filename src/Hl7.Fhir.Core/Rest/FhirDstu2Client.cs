using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Rest
{
    public class FhirDstu2Client : FhirClient<Model.DSTU2.Bundle, Model.DSTU2.Conformance, Model.DSTU2.OperationOutcome>
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
        public FhirDstu2Client(Uri endpoint, bool verifyFhirVersion = false) :
            base(
                endpoint,
                Model.Version.DSTU2,
                Model.DSTU2.ModelInfo.Version,
                exception => Model.DSTU2.OperationOutcome.ForException(exception, Model.IssueType.Invalid),
                (data, contentType) => new Model.DSTU2.Binary { Content = data, ContentType = contentType },
                verifyFhirVersion
            )
        {}

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
        public FhirDstu2Client(string endpoint, bool verifyFhirVersion = false)
            : this(new Uri(endpoint), verifyFhirVersion)
        {}

        public override string GetFhirTypeNameForType(Type type)
        {
            return Model.DSTU2.ModelInfo.GetFhirTypeNameForType(type);
        }
    }
}
