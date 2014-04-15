using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

/*
  Copyright (c) 2011-2013, HL7, Inc.
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

//
// Generated on Tue, Apr 15, 2014 17:48+0200 for FHIR v0.80
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Describes the data produced by a device at a point in time
    /// </summary>
    [FhirType("DeviceObservationReport", IsResource=true)]
    [DataContract]
    public partial class DeviceObservationReport : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// null
        /// </summary>
        [FhirType("DeviceObservationReportVirtualDeviceComponent")]
        [DataContract]
        public partial class DeviceObservationReportVirtualDeviceComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Describes the compartment
            /// </summary>
            [FhirElement("code", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// Groups related data items
            /// </summary>
            [FhirElement("channel", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.DeviceObservationReport.DeviceObservationReportVirtualDeviceChannelComponent> Channel
            {
                get { return _Channel; }
                set { _Channel = value; OnPropertyChanged("Channel"); }
            }
            private List<Hl7.Fhir.Model.DeviceObservationReport.DeviceObservationReportVirtualDeviceChannelComponent> _Channel;
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("DeviceObservationReportVirtualDeviceChannelMetricComponent")]
        [DataContract]
        public partial class DeviceObservationReportVirtualDeviceChannelMetricComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// The data for the metric
            /// </summary>
            [FhirElement("observation", InSummary=true, Order=40)]
            [References("Observation")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Observation
            {
                get { return _Observation; }
                set { _Observation = value; OnPropertyChanged("Observation"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Observation;
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("DeviceObservationReportVirtualDeviceChannelComponent")]
        [DataContract]
        public partial class DeviceObservationReportVirtualDeviceChannelComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Describes the channel
            /// </summary>
            [FhirElement("code", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// Piece of data reported by device
            /// </summary>
            [FhirElement("metric", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.DeviceObservationReport.DeviceObservationReportVirtualDeviceChannelMetricComponent> Metric
            {
                get { return _Metric; }
                set { _Metric = value; OnPropertyChanged("Metric"); }
            }
            private List<Hl7.Fhir.Model.DeviceObservationReport.DeviceObservationReportVirtualDeviceChannelMetricComponent> _Metric;
            
        }
        
        
        /// <summary>
        /// When the data values are reported
        /// </summary>
        [FhirElement("instant", Order=70)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Instant InstantElement
        {
            get { return _InstantElement; }
            set { _InstantElement = value; OnPropertyChanged("InstantElement"); }
        }
        private Hl7.Fhir.Model.Instant _InstantElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public DateTimeOffset? Instant
        {
            get { return InstantElement != null ? InstantElement.Value : null; }
            set
            {
                if(value == null)
                  InstantElement = null; 
                else
                  InstantElement = new Hl7.Fhir.Model.Instant(value);
                OnPropertyChanged("Instant");
            }
        }
        
        /// <summary>
        /// As assigned by the source device
        /// </summary>
        [FhirElement("identifier", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// Identifies/describes where the data came from
        /// </summary>
        [FhirElement("source", Order=90)]
        [References("Device")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Source
        {
            get { return _Source; }
            set { _Source = value; OnPropertyChanged("Source"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Source;
        
        /// <summary>
        /// Subject of the measurement
        /// </summary>
        [FhirElement("subject", Order=100)]
        [References("Patient","Device","Location")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// A medical-related subsystem of a medical device
        /// </summary>
        [FhirElement("virtualDevice", Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.DeviceObservationReport.DeviceObservationReportVirtualDeviceComponent> VirtualDevice
        {
            get { return _VirtualDevice; }
            set { _VirtualDevice = value; OnPropertyChanged("VirtualDevice"); }
        }
        private List<Hl7.Fhir.Model.DeviceObservationReport.DeviceObservationReportVirtualDeviceComponent> _VirtualDevice;
        
    }
    
}
