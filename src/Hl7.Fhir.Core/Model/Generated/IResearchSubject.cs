﻿using System;
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
// Generated for FHIR v4.0.0, v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Physical entity which is the primary unit of interest in the study
    /// </summary>
    public partial interface IResearchSubject : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Start and end of participation
        /// </summary>
        Hl7.Fhir.Model.Period Period { get; set; }
    
        /// <summary>
        /// Study subject is part of
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Study { get; set; }
    
        /// <summary>
        /// Who is part of study
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Individual { get; set; }
    
        /// <summary>
        /// What path should be followed
        /// </summary>
        Hl7.Fhir.Model.FhirString AssignedArmElement { get; set; }
        
        /// <summary>
        /// What path should be followed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string AssignedArm { get; set; }
    
        /// <summary>
        /// What path was followed
        /// </summary>
        Hl7.Fhir.Model.FhirString ActualArmElement { get; set; }
        
        /// <summary>
        /// What path was followed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string ActualArm { get; set; }
    
        /// <summary>
        /// Agreement to participate in study
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Consent { get; set; }
    
    }

}
