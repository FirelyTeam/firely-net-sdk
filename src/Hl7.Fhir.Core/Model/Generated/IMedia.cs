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
    /// A photo, video, or audio recording acquired or used in healthcare. The actual content may be inline or provided by direct reference
    /// </summary>
    public partial interface IMedia : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Identifier(s) for the image
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// Imaging view, e.g. Lateral or Antero-posterior
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept View { get; set; }
    
        /// <summary>
        /// Who/What this Media is a record of
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Subject { get; set; }
    
        /// <summary>
        /// The person who generated the image
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Operator { get; set; }
    
        /// <summary>
        /// Height of the image in pixels (photo/video)
        /// </summary>
        Hl7.Fhir.Model.PositiveInt HeightElement { get; set; }
        
        /// <summary>
        /// Height of the image in pixels (photo/video)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? Height { get; set; }
    
        /// <summary>
        /// Width of the image in pixels (photo/video)
        /// </summary>
        Hl7.Fhir.Model.PositiveInt WidthElement { get; set; }
        
        /// <summary>
        /// Width of the image in pixels (photo/video)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? Width { get; set; }
    
        /// <summary>
        /// Number of frames if &gt; 1 (photo)
        /// </summary>
        Hl7.Fhir.Model.PositiveInt FramesElement { get; set; }
        
        /// <summary>
        /// Number of frames if &gt; 1 (photo)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        int? Frames { get; set; }
    
        /// <summary>
        /// Actual Media - reference or data
        /// </summary>
        Hl7.Fhir.Model.Attachment Content { get; set; }
    
    }

}
