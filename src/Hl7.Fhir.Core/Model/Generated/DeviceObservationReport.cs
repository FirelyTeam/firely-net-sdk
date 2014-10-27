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
// Generated on Thu, Oct 23, 2014 14:22+0200 for FHIR v0.0.82
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DeviceObservationReportVirtualDeviceComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Channel != null) dest.Channel = new List<Hl7.Fhir.Model.DeviceObservationReport.DeviceObservationReportVirtualDeviceChannelComponent>(Channel.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DeviceObservationReportVirtualDeviceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DeviceObservationReportVirtualDeviceComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Channel, otherT.Channel)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DeviceObservationReportVirtualDeviceComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Channel, otherT.Channel)) return false;
                
                return true;
            }
            
        }
        
        
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DeviceObservationReportVirtualDeviceChannelMetricComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Observation != null) dest.Observation = (Hl7.Fhir.Model.ResourceReference)Observation.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DeviceObservationReportVirtualDeviceChannelMetricComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DeviceObservationReportVirtualDeviceChannelMetricComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Observation, otherT.Observation)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DeviceObservationReportVirtualDeviceChannelMetricComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Observation, otherT.Observation)) return false;
                
                return true;
            }
            
        }
        
        
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DeviceObservationReportVirtualDeviceChannelComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Metric != null) dest.Metric = new List<Hl7.Fhir.Model.DeviceObservationReport.DeviceObservationReportVirtualDeviceChannelMetricComponent>(Metric.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DeviceObservationReportVirtualDeviceChannelComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DeviceObservationReportVirtualDeviceChannelComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Metric, otherT.Metric)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DeviceObservationReportVirtualDeviceChannelComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Metric, otherT.Metric)) return false;
                
                return true;
            }
            
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
        
        /// <summary>
        /// When the data values are reported
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as DeviceObservationReport;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(InstantElement != null) dest.InstantElement = (Hl7.Fhir.Model.Instant)InstantElement.DeepCopy();
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(Source != null) dest.Source = (Hl7.Fhir.Model.ResourceReference)Source.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(VirtualDevice != null) dest.VirtualDevice = new List<Hl7.Fhir.Model.DeviceObservationReport.DeviceObservationReportVirtualDeviceComponent>(VirtualDevice.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new DeviceObservationReport());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as DeviceObservationReport;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(InstantElement, otherT.InstantElement)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Source, otherT.Source)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(VirtualDevice, otherT.VirtualDevice)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as DeviceObservationReport;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(InstantElement, otherT.InstantElement)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(VirtualDevice, otherT.VirtualDevice)) return false;
            
            return true;
        }
        
    }
    
}
