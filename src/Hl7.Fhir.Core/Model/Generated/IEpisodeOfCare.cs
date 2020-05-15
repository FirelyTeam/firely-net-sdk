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
// Generated for FHIR v1.0.2, v4.0.1, v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// An association of a Patient with an Organization and  Healthcare Provider(s) for a period of time that the Organization assumes some level of responsibility
    /// </summary>
    public partial interface IEpisodeOfCare : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Identifier(s) for the EpisodeOfCare
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// Past list of status codes
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IEpisodeOfCareStatusHistoryComponent> StatusHistory { get; }
    
        /// <summary>
        /// Type/class  - e.g. specialist referral, disease management
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Type { get; set; }
    
        /// <summary>
        /// Patient for this episode of care
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Patient { get; set; }
    
        /// <summary>
        /// Organization that assumes care
        /// </summary>
        Hl7.Fhir.Model.ResourceReference ManagingOrganization { get; set; }
    
        /// <summary>
        /// Interval during responsibility is assumed
        /// </summary>
        Hl7.Fhir.Model.Period Period { get; set; }
    
        /// <summary>
        /// Originating Referral Request(s)
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> ReferralRequest { get; set; }
    
        /// <summary>
        /// Care manager/care co-ordinator for the patient
        /// </summary>
        Hl7.Fhir.Model.ResourceReference CareManager { get; set; }
    
    }
    
    public partial interface IEpisodeOfCareStatusHistoryComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Period for the status
        /// </summary>
        Hl7.Fhir.Model.Period Period { get; set; }
    
    }

}
