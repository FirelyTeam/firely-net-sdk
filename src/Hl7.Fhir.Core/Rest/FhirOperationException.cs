/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hl7.Fhir;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System.Net;
using System.IO;
using Newtonsoft.Json;



namespace Hl7.Fhir.Rest
{
    public class FhirOperationException : Exception
    {
        public OperationOutcome Outcome { get; set; }
        /// <summary>
        /// The HTTP Status Code that resulted in this Exception
        /// </summary>
        /// <remarks>
        /// </remarks>
        public HttpStatusCode Status { get; set; }

        public FhirOperationException(string message, HttpStatusCode status)
            : base(message)
        {
            Status = status;
        }


        public FhirOperationException(string message, HttpStatusCode status, Exception inner)
            : base(message, inner)
        {
            Status = status;
        }

        public FhirOperationException(string message, HttpStatusCode status, OperationOutcome outcome, Exception inner)
            : base(message, inner)
        {
            Outcome = outcome;
            Status = status;
        }

        public FhirOperationException(string message, HttpStatusCode status, OperationOutcome outcome)
            : base(message)
        {
            Outcome = outcome;
            Status = status;
        }

        //public FhirOperationException(string message, OperationOutcome outcome, Exception inner, HttpStatusCode status)
        //    : base(message, inner)
        //{
        //    Outcome = outcome;
        //    Status = status;
        //}

        //public FhirOperationException(string message, OperationOutcome outcome, HttpStatusCode status)
        //    : base(message)
        //{
        //    Outcome = outcome;
        //    Status = status;
        //}
    }
}
