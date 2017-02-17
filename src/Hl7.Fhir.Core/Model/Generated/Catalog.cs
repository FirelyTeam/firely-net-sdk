using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.ComponentModel;

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

//
// Generated for FHIR v1.9.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Catalog document
    /// </summary>
    [FhirType("Catalog", IsResource=true)]
    [DataContract]
    public partial class Catalog : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Catalog; } }
        [NotMapped]
        public override string TypeName { get { return "Catalog"; } }
        
        [FhirType("DocumentComponent")]
        [DataContract]
        public partial class DocumentComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DocumentComponent"; } }
            
            /// <summary>
            /// Status of the catalog document: pre-submission, pending, approved, draft
            /// </summary>
            [FhirElement("status", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Status
            {
                get { return _Status; }
                set { _Status = value; OnPropertyChanged("Status"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Status;
            
            /// <summary>
            /// The entity that is issuing (sending, submitting, publishing) the catalog
            /// </summary>
            [FhirElement("provider", InSummary=true, Order=50)]
            [CLSCompliant(false)]
			[References("Organization")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Provider
            {
                get { return _Provider; }
                set { _Provider = value; OnPropertyChanged("Provider"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Provider;
            
            /// <summary>
            /// The type of content in the document
            /// </summary>
            [FhirElement("contentType", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept ContentType
            {
                get { return _ContentType; }
                set { _ContentType = value; OnPropertyChanged("ContentType"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _ContentType;
            
            /// <summary>
            /// How the content is intended to be used - overwriting, appending, complementing existing items
            /// </summary>
            [FhirElement("updateMode", InSummary=true, Order=70)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept UpdateMode
            {
                get { return _UpdateMode; }
                set { _UpdateMode = value; OnPropertyChanged("UpdateMode"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _UpdateMode;
            
            /// <summary>
            /// Unique identifier for the catalog document
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// The version of the bundle that is being transmitted
            /// </summary>
            [FhirElement("contentVersion", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier ContentVersion
            {
                get { return _ContentVersion; }
                set { _ContentVersion = value; OnPropertyChanged("ContentVersion"); }
            }
            
            private Hl7.Fhir.Model.Identifier _ContentVersion;
            
            /// <summary>
            /// The date when the catalog document is issued
            /// </summary>
            [FhirElement("issueDate", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime IssueDateElement
            {
                get { return _IssueDateElement; }
                set { _IssueDateElement = value; OnPropertyChanged("IssueDateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _IssueDateElement;
            
            /// <summary>
            /// The date when the catalog document is issued
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string IssueDate
            {
                get { return IssueDateElement != null ? IssueDateElement.Value : null; }
                set
                {
                    if (value == null)
                        IssueDateElement = null; 
                    else
                        IssueDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("IssueDate");
                }
            }
            
            /// <summary>
            /// The date from which the catalog content is expected to be active
            /// </summary>
            [FhirElement("validFrom", InSummary=true, Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime ValidFromElement
            {
                get { return _ValidFromElement; }
                set { _ValidFromElement = value; OnPropertyChanged("ValidFromElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _ValidFromElement;
            
            /// <summary>
            /// The date from which the catalog content is expected to be active
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ValidFrom
            {
                get { return ValidFromElement != null ? ValidFromElement.Value : null; }
                set
                {
                    if (value == null)
                        ValidFromElement = null; 
                    else
                        ValidFromElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("ValidFrom");
                }
            }
            
            /// <summary>
            /// The date until which the catalog content is expected to be active
            /// </summary>
            [FhirElement("validTo", InSummary=true, Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime ValidToElement
            {
                get { return _ValidToElement; }
                set { _ValidToElement = value; OnPropertyChanged("ValidToElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _ValidToElement;
            
            /// <summary>
            /// The date until which the catalog content is expected to be active
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ValidTo
            {
                get { return ValidToElement != null ? ValidToElement.Value : null; }
                set
                {
                    if (value == null)
                        ValidToElement = null; 
                    else
                        ValidToElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("ValidTo");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DocumentComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Status != null) dest.Status = (Hl7.Fhir.Model.CodeableConcept)Status.DeepCopy();
                    if(Provider != null) dest.Provider = (Hl7.Fhir.Model.ResourceReference)Provider.DeepCopy();
                    if(ContentType != null) dest.ContentType = (Hl7.Fhir.Model.CodeableConcept)ContentType.DeepCopy();
                    if(UpdateMode != null) dest.UpdateMode = (Hl7.Fhir.Model.CodeableConcept)UpdateMode.DeepCopy();
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(ContentVersion != null) dest.ContentVersion = (Hl7.Fhir.Model.Identifier)ContentVersion.DeepCopy();
                    if(IssueDateElement != null) dest.IssueDateElement = (Hl7.Fhir.Model.FhirDateTime)IssueDateElement.DeepCopy();
                    if(ValidFromElement != null) dest.ValidFromElement = (Hl7.Fhir.Model.FhirDateTime)ValidFromElement.DeepCopy();
                    if(ValidToElement != null) dest.ValidToElement = (Hl7.Fhir.Model.FhirDateTime)ValidToElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DocumentComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DocumentComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Status, otherT.Status)) return false;
                if( !DeepComparable.Matches(Provider, otherT.Provider)) return false;
                if( !DeepComparable.Matches(ContentType, otherT.ContentType)) return false;
                if( !DeepComparable.Matches(UpdateMode, otherT.UpdateMode)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(ContentVersion, otherT.ContentVersion)) return false;
                if( !DeepComparable.Matches(IssueDateElement, otherT.IssueDateElement)) return false;
                if( !DeepComparable.Matches(ValidFromElement, otherT.ValidFromElement)) return false;
                if( !DeepComparable.Matches(ValidToElement, otherT.ValidToElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DocumentComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Status, otherT.Status)) return false;
                if( !DeepComparable.IsExactly(Provider, otherT.Provider)) return false;
                if( !DeepComparable.IsExactly(ContentType, otherT.ContentType)) return false;
                if( !DeepComparable.IsExactly(UpdateMode, otherT.UpdateMode)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(ContentVersion, otherT.ContentVersion)) return false;
                if( !DeepComparable.IsExactly(IssueDateElement, otherT.IssueDateElement)) return false;
                if( !DeepComparable.IsExactly(ValidFromElement, otherT.ValidFromElement)) return false;
                if( !DeepComparable.IsExactly(ValidToElement, otherT.ValidToElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    // BackboneElement elements
                    foreach (var elem in ModifierExtension) { if (elem != null) yield return elem; }
                    // DocumentComponent elements
                    if (Status != null) yield return Status;
                    if (Provider != null) yield return Provider;
                    if (ContentType != null) yield return ContentType;
                    if (UpdateMode != null) yield return UpdateMode;
                    if (Identifier != null) yield return Identifier;
                    if (ContentVersion != null) yield return ContentVersion;
                    if (IssueDateElement != null) yield return IssueDateElement;
                    if (ValidFromElement != null) yield return ValidFromElement;
                    if (ValidToElement != null) yield return ValidToElement;
                }
            }
            
        }
        
        
        [FhirType("EntryComponent")]
        [DataContract]
        public partial class EntryComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "EntryComponent"; } }
            
            /// <summary>
            /// The type of item - medication, device, service, protocol or other
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// The item itself
            /// </summary>
            [FhirElement("referencedItem", InSummary=true, Order=50)]
            [CLSCompliant(false)]
			[References("Medication","Device","Procedure","CarePlan","Organization","Practitioner","HealthcareService","ServiceDefinition")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference ReferencedItem
            {
                get { return _ReferencedItem; }
                set { _ReferencedItem = value; OnPropertyChanged("ReferencedItem"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _ReferencedItem;
            
            /// <summary>
            /// Unique identifier of the catalog item
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// Any additional identifier(s) for the catalog item, in the same granularity or concept
            /// </summary>
            [FhirElement("additionalIdentifier", InSummary=true, Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Identifier> AdditionalIdentifier
            {
                get { if(_AdditionalIdentifier==null) _AdditionalIdentifier = new List<Hl7.Fhir.Model.Identifier>(); return _AdditionalIdentifier; }
                set { _AdditionalIdentifier = value; OnPropertyChanged("AdditionalIdentifier"); }
            }
            
            private List<Hl7.Fhir.Model.Identifier> _AdditionalIdentifier;
            
            /// <summary>
            /// Classification (category or class) of the item entry
            /// </summary>
            [FhirElement("classification", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Identifier> Classification
            {
                get { if(_Classification==null) _Classification = new List<Hl7.Fhir.Model.Identifier>(); return _Classification; }
                set { _Classification = value; OnPropertyChanged("Classification"); }
            }
            
            private List<Hl7.Fhir.Model.Identifier> _Classification;
            
            /// <summary>
            /// The status of the item, e.g. active, approved…
            /// </summary>
            [FhirElement("status", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Status
            {
                get { return _Status; }
                set { _Status = value; OnPropertyChanged("Status"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Status;
            
            /// <summary>
            /// The date from which this catalog entry is expected to be active
            /// </summary>
            [FhirElement("validFrom", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime ValidFromElement
            {
                get { return _ValidFromElement; }
                set { _ValidFromElement = value; OnPropertyChanged("ValidFromElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _ValidFromElement;
            
            /// <summary>
            /// The date from which this catalog entry is expected to be active
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ValidFrom
            {
                get { return ValidFromElement != null ? ValidFromElement.Value : null; }
                set
                {
                    if (value == null)
                        ValidFromElement = null; 
                    else
                        ValidFromElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("ValidFrom");
                }
            }
            
            /// <summary>
            /// The date until which this catalog entry is expected to be active
            /// </summary>
            [FhirElement("validTo", InSummary=true, Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime ValidToElement
            {
                get { return _ValidToElement; }
                set { _ValidToElement = value; OnPropertyChanged("ValidToElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _ValidToElement;
            
            /// <summary>
            /// The date until which this catalog entry is expected to be active
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ValidTo
            {
                get { return ValidToElement != null ? ValidToElement.Value : null; }
                set
                {
                    if (value == null)
                        ValidToElement = null; 
                    else
                        ValidToElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("ValidTo");
                }
            }
            
            /// <summary>
            /// Perhaps not needed
            /// </summary>
            [FhirElement("lastUpdated", InSummary=true, Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime LastUpdatedElement
            {
                get { return _LastUpdatedElement; }
                set { _LastUpdatedElement = value; OnPropertyChanged("LastUpdatedElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _LastUpdatedElement;
            
            /// <summary>
            /// Perhaps not needed
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string LastUpdated
            {
                get { return LastUpdatedElement != null ? LastUpdatedElement.Value : null; }
                set
                {
                    if (value == null)
                        LastUpdatedElement = null; 
                    else
                        LastUpdatedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("LastUpdated");
                }
            }
            
            /// <summary>
            /// Additional characteristics of the catalog entry
            /// </summary>
            [FhirElement("additionalCharacteristic", InSummary=true, Order=130)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> AdditionalCharacteristic
            {
                get { if(_AdditionalCharacteristic==null) _AdditionalCharacteristic = new List<Hl7.Fhir.Model.CodeableConcept>(); return _AdditionalCharacteristic; }
                set { _AdditionalCharacteristic = value; OnPropertyChanged("AdditionalCharacteristic"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _AdditionalCharacteristic;
            
            /// <summary>
            /// Additional classification of the catalog entry
            /// </summary>
            [FhirElement("additionalClassification", InSummary=true, Order=140)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> AdditionalClassification
            {
                get { if(_AdditionalClassification==null) _AdditionalClassification = new List<Hl7.Fhir.Model.CodeableConcept>(); return _AdditionalClassification; }
                set { _AdditionalClassification = value; OnPropertyChanged("AdditionalClassification"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _AdditionalClassification;
            
            /// <summary>
            /// An item that this catalog entry is related to
            /// </summary>
            [FhirElement("relatedItem", InSummary=true, Order=150)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Catalog.RelatedItemComponent> RelatedItem
            {
                get { if(_RelatedItem==null) _RelatedItem = new List<Hl7.Fhir.Model.Catalog.RelatedItemComponent>(); return _RelatedItem; }
                set { _RelatedItem = value; OnPropertyChanged("RelatedItem"); }
            }
            
            private List<Hl7.Fhir.Model.Catalog.RelatedItemComponent> _RelatedItem;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as EntryComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(ReferencedItem != null) dest.ReferencedItem = (Hl7.Fhir.Model.ResourceReference)ReferencedItem.DeepCopy();
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(AdditionalIdentifier != null) dest.AdditionalIdentifier = new List<Hl7.Fhir.Model.Identifier>(AdditionalIdentifier.DeepCopy());
                    if(Classification != null) dest.Classification = new List<Hl7.Fhir.Model.Identifier>(Classification.DeepCopy());
                    if(Status != null) dest.Status = (Hl7.Fhir.Model.CodeableConcept)Status.DeepCopy();
                    if(ValidFromElement != null) dest.ValidFromElement = (Hl7.Fhir.Model.FhirDateTime)ValidFromElement.DeepCopy();
                    if(ValidToElement != null) dest.ValidToElement = (Hl7.Fhir.Model.FhirDateTime)ValidToElement.DeepCopy();
                    if(LastUpdatedElement != null) dest.LastUpdatedElement = (Hl7.Fhir.Model.FhirDateTime)LastUpdatedElement.DeepCopy();
                    if(AdditionalCharacteristic != null) dest.AdditionalCharacteristic = new List<Hl7.Fhir.Model.CodeableConcept>(AdditionalCharacteristic.DeepCopy());
                    if(AdditionalClassification != null) dest.AdditionalClassification = new List<Hl7.Fhir.Model.CodeableConcept>(AdditionalClassification.DeepCopy());
                    if(RelatedItem != null) dest.RelatedItem = new List<Hl7.Fhir.Model.Catalog.RelatedItemComponent>(RelatedItem.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new EntryComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as EntryComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(ReferencedItem, otherT.ReferencedItem)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(AdditionalIdentifier, otherT.AdditionalIdentifier)) return false;
                if( !DeepComparable.Matches(Classification, otherT.Classification)) return false;
                if( !DeepComparable.Matches(Status, otherT.Status)) return false;
                if( !DeepComparable.Matches(ValidFromElement, otherT.ValidFromElement)) return false;
                if( !DeepComparable.Matches(ValidToElement, otherT.ValidToElement)) return false;
                if( !DeepComparable.Matches(LastUpdatedElement, otherT.LastUpdatedElement)) return false;
                if( !DeepComparable.Matches(AdditionalCharacteristic, otherT.AdditionalCharacteristic)) return false;
                if( !DeepComparable.Matches(AdditionalClassification, otherT.AdditionalClassification)) return false;
                if( !DeepComparable.Matches(RelatedItem, otherT.RelatedItem)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as EntryComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(ReferencedItem, otherT.ReferencedItem)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(AdditionalIdentifier, otherT.AdditionalIdentifier)) return false;
                if( !DeepComparable.IsExactly(Classification, otherT.Classification)) return false;
                if( !DeepComparable.IsExactly(Status, otherT.Status)) return false;
                if( !DeepComparable.IsExactly(ValidFromElement, otherT.ValidFromElement)) return false;
                if( !DeepComparable.IsExactly(ValidToElement, otherT.ValidToElement)) return false;
                if( !DeepComparable.IsExactly(LastUpdatedElement, otherT.LastUpdatedElement)) return false;
                if( !DeepComparable.IsExactly(AdditionalCharacteristic, otherT.AdditionalCharacteristic)) return false;
                if( !DeepComparable.IsExactly(AdditionalClassification, otherT.AdditionalClassification)) return false;
                if( !DeepComparable.IsExactly(RelatedItem, otherT.RelatedItem)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    // BackboneElement elements
                    foreach (var elem in ModifierExtension) { if (elem != null) yield return elem; }
                    // EntryComponent elements
                    if (Type != null) yield return Type;
                    if (ReferencedItem != null) yield return ReferencedItem;
                    if (Identifier != null) yield return Identifier;
                    foreach (var elem in AdditionalIdentifier) { if (elem != null) yield return elem; }
                    foreach (var elem in Classification) { if (elem != null) yield return elem; }
                    if (Status != null) yield return Status;
                    if (ValidFromElement != null) yield return ValidFromElement;
                    if (ValidToElement != null) yield return ValidToElement;
                    if (LastUpdatedElement != null) yield return LastUpdatedElement;
                    foreach (var elem in AdditionalCharacteristic) { if (elem != null) yield return elem; }
                    foreach (var elem in AdditionalClassification) { if (elem != null) yield return elem; }
                    foreach (var elem in RelatedItem) { if (elem != null) yield return elem; }
                }
            }
            
        }
        
        
        [FhirType("RelatedItemComponent")]
        [DataContract]
        public partial class RelatedItemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "RelatedItemComponent"; } }
            
            /// <summary>
            /// The type of relation to the related item
            /// </summary>
            [FhirElement("relationtype", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Relationtype
            {
                get { return _Relationtype; }
                set { _Relationtype = value; OnPropertyChanged("Relationtype"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Relationtype;
            
            /// <summary>
            /// The type of related item - medication, devices…
            /// </summary>
            [FhirElement("type", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// The reference to the related item
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=60)]
            [CLSCompliant(false)]
			[References("Medication","Device","Procedure","CarePlan","Organization","Practitioner","HealthcareService","ServiceDefinition")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Identifier;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RelatedItemComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Relationtype != null) dest.Relationtype = (Hl7.Fhir.Model.CodeableConcept)Relationtype.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.ResourceReference)Identifier.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new RelatedItemComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RelatedItemComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Relationtype, otherT.Relationtype)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RelatedItemComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Relationtype, otherT.Relationtype)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    // BackboneElement elements
                    foreach (var elem in ModifierExtension) { if (elem != null) yield return elem; }
                    // RelatedItemComponent elements
                    if (Relationtype != null) yield return Relationtype;
                    if (Type != null) yield return Type;
                    if (Identifier != null) yield return Identifier;
                }
            }
            
        }
        
        
        /// <summary>
        /// Unique identifier for the  catalog resource
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// Properties of the document - authorship, versions, etc
        /// </summary>
        [FhirElement("document", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Catalog.DocumentComponent Document
        {
            get { return _Document; }
            set { _Document = value; OnPropertyChanged("Document"); }
        }
        
        private Hl7.Fhir.Model.Catalog.DocumentComponent _Document;
        
        /// <summary>
        /// Each item of the catalog
        /// </summary>
        [FhirElement("entry", InSummary=true, Order=110)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Catalog.EntryComponent> Entry
        {
            get { if(_Entry==null) _Entry = new List<Hl7.Fhir.Model.Catalog.EntryComponent>(); return _Entry; }
            set { _Entry = value; OnPropertyChanged("Entry"); }
        }
        
        private List<Hl7.Fhir.Model.Catalog.EntryComponent> _Entry;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Catalog;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(Document != null) dest.Document = (Hl7.Fhir.Model.Catalog.DocumentComponent)Document.DeepCopy();
                if(Entry != null) dest.Entry = new List<Hl7.Fhir.Model.Catalog.EntryComponent>(Entry.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Catalog());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Catalog;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Document, otherT.Document)) return false;
            if( !DeepComparable.Matches(Entry, otherT.Entry)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Catalog;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Document, otherT.Document)) return false;
            if( !DeepComparable.IsExactly(Entry, otherT.Entry)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
				// Catalog elements
				if (Identifier != null) yield return Identifier;
				if (Document != null) yield return Document;
				foreach (var elem in Entry) { if (elem != null) yield return elem; }
            }
        }
    }
    
}
