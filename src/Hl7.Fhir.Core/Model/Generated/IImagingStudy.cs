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
    /// A set of images produced in single study (one or more series of references images)
    /// </summary>
    public partial interface IImagingStudy : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Other identifiers for the study
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// When the study was started
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime StartedElement { get; set; }
        
        /// <summary>
        /// When the study was started
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Started { get; set; }
    
        /// <summary>
        /// Referring physician (0008,0090)
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Referrer { get; set; }
    
        /// <summary>
        /// Number of Study Related Series
        /// </summary>
        Hl7.Fhir.Model.UnsignedInt NumberOfSeriesElement { get; set; }
        
        /// <summary>
        /// Number of Study Related Series
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? NumberOfSeries { get; set; }
    
        /// <summary>
        /// Number of Study Related Instances
        /// </summary>
        Hl7.Fhir.Model.UnsignedInt NumberOfInstancesElement { get; set; }
        
        /// <summary>
        /// Number of Study Related Instances
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? NumberOfInstances { get; set; }
    
        /// <summary>
        /// Institution-generated description
        /// </summary>
        Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        /// <summary>
        /// Institution-generated description
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
        /// <summary>
        /// Each study has one or more series of instances
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IImagingStudySeriesComponent> Series { get; }
    
    }
    
    public partial interface IImagingStudySeriesComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Numeric identifier of this series
        /// </summary>
        Hl7.Fhir.Model.UnsignedInt NumberElement { get; set; }
        
        /// <summary>
        /// Numeric identifier of this series
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? Number { get; set; }
    
        /// <summary>
        /// The modality of the instances in the series
        /// </summary>
        Hl7.Fhir.Model.Coding Modality { get; set; }
    
        /// <summary>
        /// A description of the series
        /// </summary>
        Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        /// <summary>
        /// A description of the series
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Description { get; set; }
    
        /// <summary>
        /// Number of Series Related Instances
        /// </summary>
        Hl7.Fhir.Model.UnsignedInt NumberOfInstancesElement { get; set; }
        
        /// <summary>
        /// Number of Series Related Instances
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? NumberOfInstances { get; set; }
    
        /// <summary>
        /// Body part examined
        /// </summary>
        Hl7.Fhir.Model.Coding BodySite { get; set; }
    
        /// <summary>
        /// Body part laterality
        /// </summary>
        Hl7.Fhir.Model.Coding Laterality { get; set; }
    
        /// <summary>
        /// When the series started
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime StartedElement { get; set; }
        
        /// <summary>
        /// When the series started
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Started { get; set; }
    
        /// <summary>
        /// A single SOP instance from the series
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IImagingStudyInstanceComponent> Instance { get; }
    
    }
    
    public partial interface IImagingStudyInstanceComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// The number of this instance in the series
        /// </summary>
        Hl7.Fhir.Model.UnsignedInt NumberElement { get; set; }
        
        /// <summary>
        /// The number of this instance in the series
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? Number { get; set; }
    
        /// <summary>
        /// Description of instance
        /// </summary>
        Hl7.Fhir.Model.FhirString TitleElement { get; set; }
        
        /// <summary>
        /// Description of instance
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Title { get; set; }
    
    }

}
