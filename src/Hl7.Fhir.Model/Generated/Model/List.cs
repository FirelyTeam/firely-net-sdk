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
// Generated on Thu, Apr 24, 2014 12:29+0200 for FHIR v0.80
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Information summarized from a list of other resources
    /// </summary>
    [FhirType("List", IsResource=true)]
    [DataContract]
    public partial class List : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// The processing mode that applies to this list
        /// </summary>
        [FhirEnumeration("ListMode")]
        public enum ListMode
        {
            /// <summary>
            /// This list is the master list, maintained in an ongoing fashion with regular updates as the real world list it is tracking changes.
            /// </summary>
            [EnumLiteral("working")]
            Working,
            /// <summary>
            /// This list was prepared as a snapshot. It should not be assumed to be current.
            /// </summary>
            [EnumLiteral("snapshot")]
            Snapshot,
            /// <summary>
            /// The list is prepared as a statement of changes that have been made or recommended.
            /// </summary>
            [EnumLiteral("changes")]
            Changes,
        }
        
        [FhirType("ListEntryComponent")]
        [DataContract]
        public partial class ListEntryComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Workflow information about this item
            /// </summary>
            [FhirElement("flag", InSummary=true, Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Flag
            {
                get { return _Flag; }
                set { _Flag = value; OnPropertyChanged("Flag"); }
            }
            private List<Hl7.Fhir.Model.CodeableConcept> _Flag;
            
            /// <summary>
            /// If this item is actually marked as deleted
            /// </summary>
            [FhirElement("deleted", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean DeletedElement
            {
                get { return _DeletedElement; }
                set { _DeletedElement = value; OnPropertyChanged("DeletedElement"); }
            }
            private Hl7.Fhir.Model.FhirBoolean _DeletedElement;
            
            /// <summary>
            /// If this item is actually marked as deleted
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Deleted
            {
                get { return DeletedElement != null ? DeletedElement.Value : null; }
                set
                {
                    if(value == null)
                      DeletedElement = null; 
                    else
                      DeletedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Deleted");
                }
            }
            
            /// <summary>
            /// When item added to list
            /// </summary>
            [FhirElement("date", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime DateElement
            {
                get { return _DateElement; }
                set { _DateElement = value; OnPropertyChanged("DateElement"); }
            }
            private Hl7.Fhir.Model.FhirDateTime _DateElement;
            
            /// <summary>
            /// When item added to list
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Date
            {
                get { return DateElement != null ? DateElement.Value : null; }
                set
                {
                    if(value == null)
                      DateElement = null; 
                    else
                      DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("Date");
                }
            }
            
            /// <summary>
            /// Actual entry
            /// </summary>
            [FhirElement("item", InSummary=true, Order=70)]
            [References()]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Item
            {
                get { return _Item; }
                set { _Item = value; OnPropertyChanged("Item"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Item;
            
        }
        
        
        /// <summary>
        /// Business identifier
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// What the purpose of this list is
        /// </summary>
        [FhirElement("code", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        private Hl7.Fhir.Model.CodeableConcept _Code;
        
        /// <summary>
        /// If all resources have the same subject
        /// </summary>
        [FhirElement("subject", Order=90)]
        [References("Patient","Group","Device","Location")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Who and/or what defined the list contents
        /// </summary>
        [FhirElement("source", Order=100)]
        [References("Practitioner","Patient","Device")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Source
        {
            get { return _Source; }
            set { _Source = value; OnPropertyChanged("Source"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Source;
        
        /// <summary>
        /// When the list was prepared
        /// </summary>
        [FhirElement("date", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// When the list was prepared
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Date
        {
            get { return DateElement != null ? DateElement.Value : null; }
            set
            {
                if(value == null)
                  DateElement = null; 
                else
                  DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Date");
            }
        }
        
        /// <summary>
        /// Whether items in the list have a meaningful order
        /// </summary>
        [FhirElement("ordered", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean OrderedElement
        {
            get { return _OrderedElement; }
            set { _OrderedElement = value; OnPropertyChanged("OrderedElement"); }
        }
        private Hl7.Fhir.Model.FhirBoolean _OrderedElement;
        
        /// <summary>
        /// Whether items in the list have a meaningful order
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Ordered
        {
            get { return OrderedElement != null ? OrderedElement.Value : null; }
            set
            {
                if(value == null)
                  OrderedElement = null; 
                else
                  OrderedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Ordered");
            }
        }
        
        /// <summary>
        /// working | snapshot | changes
        /// </summary>
        [FhirElement("mode", Order=130)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.List.ListMode> ModeElement
        {
            get { return _ModeElement; }
            set { _ModeElement = value; OnPropertyChanged("ModeElement"); }
        }
        private Code<Hl7.Fhir.Model.List.ListMode> _ModeElement;
        
        /// <summary>
        /// working | snapshot | changes
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.List.ListMode? Mode
        {
            get { return ModeElement != null ? ModeElement.Value : null; }
            set
            {
                if(value == null)
                  ModeElement = null; 
                else
                  ModeElement = new Code<Hl7.Fhir.Model.List.ListMode>(value);
                OnPropertyChanged("Mode");
            }
        }
        
        /// <summary>
        /// Entries in the list
        /// </summary>
        [FhirElement("entry", Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.List.ListEntryComponent> Entry
        {
            get { return _Entry; }
            set { _Entry = value; OnPropertyChanged("Entry"); }
        }
        private List<Hl7.Fhir.Model.List.ListEntryComponent> _Entry;
        
        /// <summary>
        /// Why list is empty
        /// </summary>
        [FhirElement("emptyReason", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept EmptyReason
        {
            get { return _EmptyReason; }
            set { _EmptyReason = value; OnPropertyChanged("EmptyReason"); }
        }
        private Hl7.Fhir.Model.CodeableConcept _EmptyReason;
        
    }
    
}
