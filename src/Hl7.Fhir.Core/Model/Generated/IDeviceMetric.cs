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
    /// Measurement, calculation or setting capability of a medical device
    /// </summary>
    public partial interface IDeviceMetric : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Type of metric
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Type { get; set; }
    
        /// <summary>
        /// Unit of metric
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Unit { get; set; }
    
        /// <summary>
        /// Describes the link to the source Device
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Source { get; set; }
    
        /// <summary>
        /// Describes the link to the parent DeviceComponent
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Parent { get; set; }
    
        /// <summary>
        /// black | red | green | yellow | blue | magenta | cyan | white
        /// </summary>
        Code<Hl7.Fhir.Model.DeviceMetricColor> ColorElement { get; set; }
        
        /// <summary>
        /// black | red | green | yellow | blue | magenta | cyan | white
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.DeviceMetricColor? Color { get; set; }
    
        /// <summary>
        /// measurement | setting | calculation | unspecified
        /// </summary>
        Code<Hl7.Fhir.Model.DeviceMetricCategory> CategoryElement { get; set; }
        
        /// <summary>
        /// measurement | setting | calculation | unspecified
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.DeviceMetricCategory? Category { get; set; }
    
        /// <summary>
        /// Describes the measurement repetition time
        /// </summary>
        Hl7.Fhir.Model.ITiming MeasurementPeriod { get; }
    
        /// <summary>
        /// Describes the calibrations that have been performed or that are required to be performed
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.IDeviceMetricCalibrationComponent> Calibration { get; }
    
    }
    
    public partial interface IDeviceMetricCalibrationComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// unspecified | offset | gain | two-point
        /// </summary>
        Code<Hl7.Fhir.Model.DeviceMetricCalibrationType> TypeElement { get; set; }
        
        /// <summary>
        /// unspecified | offset | gain | two-point
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.DeviceMetricCalibrationType? Type { get; set; }
    
        /// <summary>
        /// not-calibrated | calibration-required | calibrated | unspecified
        /// </summary>
        Code<Hl7.Fhir.Model.DeviceMetricCalibrationState> StateElement { get; set; }
        
        /// <summary>
        /// not-calibrated | calibration-required | calibrated | unspecified
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        Hl7.Fhir.Model.DeviceMetricCalibrationState? State { get; set; }
    
        /// <summary>
        /// Describes the time last calibration has been performed
        /// </summary>
        Hl7.Fhir.Model.Instant TimeElement { get; set; }
        
        /// <summary>
        /// Describes the time last calibration has been performed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        DateTimeOffset? Time { get; set; }
    
    }

}
