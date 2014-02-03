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
// Generated on Mon, Feb 3, 2014 11:56+0100 for FHIR v0.80
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Information summarized from a list of other resources
    /// </summary>
    [FhirType("List", IsResource=true)]
    [DataContract]
    public partial class List : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// The processing mode that applies to this list
        /// </summary>
        [FhirEnumeration("ListMode")]
        public enum ListMode
        {
            [EnumLiteral("working")]
            Working, // This list is the master list, maintained in an ongoing fashion with regular updates as the real world list it is tracking changes.
            [EnumLiteral("snapshot")]
            Snapshot, // This list was prepared as a snapshot. It should not be assumed to be current.
            [EnumLiteral("changes")]
            Changes, // The list is prepared as a statement of changes that have been made or recommended.
        }
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ListEntryComponent")]
        [DataContract]
        public partial class ListEntryComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Workflow information about this item
            /// </summary>
            [FhirElement("flag", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Flag { get; set; }
            
            /// <summary>
            /// If this item is actually marked as deleted
            /// </summary>
            [FhirElement("deleted", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean DeletedElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// When item added to list
            /// </summary>
            [FhirElement("date", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Actual entry
            /// </summary>
            [FhirElement("item", Order=70)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Item { get; set; }
            
        }
        
        
        /// <summary>
        /// Business identifier
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        
        /// <summary>
        /// What the purpose of this list is
        /// </summary>
        [FhirElement("code", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code { get; set; }
        
        /// <summary>
        /// If all resources have the same subject
        /// </summary>
        [FhirElement("subject", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject { get; set; }
        
        /// <summary>
        /// Who and/or what defined the list contents
        /// </summary>
        [FhirElement("source", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Source { get; set; }
        
        /// <summary>
        /// When the list was prepared
        /// </summary>
        [FhirElement("date", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Whether items in the list have a meaningful order
        /// </summary>
        [FhirElement("ordered", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean OrderedElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// working | snapshot | changes
        /// </summary>
        [FhirElement("mode", Order=130)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.List.ListMode> ModeElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Entries in the list
        /// </summary>
        [FhirElement("entry", Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.List.ListEntryComponent> Entry { get; set; }
        
        /// <summary>
        /// Why list is empty
        /// </summary>
        [FhirElement("emptyReason", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept EmptyReason { get; set; }
        
    }
    
}
