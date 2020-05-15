using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Utility;

/*
    Copyright (c) 2011+, HL7, Inc.
    All rights reserved.

    Redistribution and use in source and binary forms, with or without modification, 
    are permitted provided that the following conditions are met:

    * Redistributions of source code must retain the above copyright notice, this 
        list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright notice, 
        this list of conditions and the following disclaimer in the documentation 
        and/or other materials provided with the distribution.
    * Neither the name of HL7 nor the names of its contributors may be used to 
        endorse or promote products derived from this software without specific 
        prior written permission.

    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
    ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
    WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
    IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
    INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
    NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
    PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
    WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
    ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
    POSSIBILITY OF SUCH DAMAGE.


*/
#pragma warning disable 1591 // suppress XML summary warnings

//
// Generated for FHIR v4.0.1, v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A healthcare consumer's  choices to permit or deny recipients or roles to perform actions for specific purposes and periods of time
    /// </summary>
    public partial interface IConsent : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// draft | proposed | active | rejected | inactive | entered-in-error
        /// </summary>
        Code<Hl7.Fhir.Model.ConsentState> StatusElement { get; set; }
        
        /// <summary>
        /// draft | proposed | active | rejected | inactive | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.ConsentState? Status { get; set; }
    
        /// <summary>
        /// Classification of the consent statement - for indexing/retrieval
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Category { get; set; }
    
        /// <summary>
        /// Who the consent applies to
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Patient { get; set; }
    
        /// <summary>
        /// When this Consent was created or indexed
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime DateTimeElement { get; set; }
        
        /// <summary>
        /// When this Consent was created or indexed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string DateTime { get; set; }
    
        /// <summary>
        /// Custodian of the consent
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> Organization { get; set; }
    
        /// <summary>
        /// Source from which this consent is taken
        /// </summary>
        Hl7.Fhir.Model.Element Source { get; set; }
    
        /// <summary>
        /// Policies covered by this consent
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IConsentPolicyComponent> Policy { get; }
    
    }
    
    public partial interface IConsentPolicyComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Enforcement source for policy
        /// </summary>
        Hl7.Fhir.Model.FhirUri AuthorityElement { get; set; }
        
        /// <summary>
        /// Enforcement source for policy
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Authority { get; set; }
    
        /// <summary>
        /// Specific policy covered by this consent
        /// </summary>
        Hl7.Fhir.Model.FhirUri UriElement { get; set; }
        
        /// <summary>
        /// Specific policy covered by this consent
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Uri { get; set; }
    
    }

}
