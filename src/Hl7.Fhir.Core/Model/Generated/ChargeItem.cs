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
// Generated for FHIR v4.0.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Item containing charge code(s) associated with the provision of healthcare provider products
    /// </summary>
    [FhirType("ChargeItem", IsResource=true)]
    [DataContract]
    public partial class ChargeItem : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ChargeItem; } }
        [NotMapped]
        public override string TypeName { get { return "ChargeItem"; } }
        
        /// <summary>
        /// Codes identifying the lifecycle stage of a ChargeItem.
        /// (url: http://hl7.org/fhir/ValueSet/chargeitem-status)
        /// </summary>
        [FhirEnumeration("ChargeItemStatus")]
        public enum ChargeItemStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/chargeitem-status)
            /// </summary>
            [EnumLiteral("planned", "http://hl7.org/fhir/chargeitem-status"), Description("Planned")]
            Planned,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/chargeitem-status)
            /// </summary>
            [EnumLiteral("billable", "http://hl7.org/fhir/chargeitem-status"), Description("Billable")]
            Billable,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/chargeitem-status)
            /// </summary>
            [EnumLiteral("not-billable", "http://hl7.org/fhir/chargeitem-status"), Description("Not billable")]
            NotBillable,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/chargeitem-status)
            /// </summary>
            [EnumLiteral("aborted", "http://hl7.org/fhir/chargeitem-status"), Description("Aborted")]
            Aborted,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/chargeitem-status)
            /// </summary>
            [EnumLiteral("billed", "http://hl7.org/fhir/chargeitem-status"), Description("Billed")]
            Billed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/chargeitem-status)
            /// </summary>
            [EnumLiteral("entered-in-error", "http://hl7.org/fhir/chargeitem-status"), Description("Entered in Error")]
            EnteredInError,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/chargeitem-status)
            /// </summary>
            [EnumLiteral("unknown", "http://hl7.org/fhir/chargeitem-status"), Description("Unknown")]
            Unknown,
        }

        [FhirType("PerformerComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class PerformerComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "PerformerComponent"; } }
            
            /// <summary>
            /// What type of performance was done
            /// </summary>
            [FhirElement("function", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Function
            {
                get { return _Function; }
                set { _Function = value; OnPropertyChanged("Function"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Function;
            
            /// <summary>
            /// Individual who was performing
            /// </summary>
            [FhirElement("actor", Order=50)]
            [CLSCompliant(false)]
			[References("Practitioner","PractitionerRole","Organization","CareTeam","Patient","Device","RelatedPerson")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Actor
            {
                get { return _Actor; }
                set { _Actor = value; OnPropertyChanged("Actor"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Actor;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PerformerComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Function != null) dest.Function = (Hl7.Fhir.Model.CodeableConcept)Function.DeepCopy();
                    if(Actor != null) dest.Actor = (Hl7.Fhir.Model.ResourceReference)Actor.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PerformerComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PerformerComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Function, otherT.Function)) return false;
                if( !DeepComparable.Matches(Actor, otherT.Actor)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PerformerComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Function, otherT.Function)) return false;
                if( !DeepComparable.IsExactly(Actor, otherT.Actor)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Function != null) yield return Function;
                    if (Actor != null) yield return Actor;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Function != null) yield return new ElementValue("function", Function);
                    if (Actor != null) yield return new ElementValue("actor", Actor);
                }
            }

            
        }
        
        
        /// <summary>
        /// Business Identifier for item
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Defining information about the code of this charge item
        /// </summary>
        [FhirElement("definitionUri", Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirUri> DefinitionUriElement
        {
            get { if(_DefinitionUriElement==null) _DefinitionUriElement = new List<Hl7.Fhir.Model.FhirUri>(); return _DefinitionUriElement; }
            set { _DefinitionUriElement = value; OnPropertyChanged("DefinitionUriElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirUri> _DefinitionUriElement;
        
        /// <summary>
        /// Defining information about the code of this charge item
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> DefinitionUri
        {
            get { return DefinitionUriElement != null ? DefinitionUriElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  DefinitionUriElement = null; 
                else
                  DefinitionUriElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                OnPropertyChanged("DefinitionUri");
            }
        }
        
        /// <summary>
        /// Resource defining the code of this ChargeItem
        /// </summary>
        [FhirElement("definitionCanonical", Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Canonical> DefinitionCanonicalElement
        {
            get { if(_DefinitionCanonicalElement==null) _DefinitionCanonicalElement = new List<Hl7.Fhir.Model.Canonical>(); return _DefinitionCanonicalElement; }
            set { _DefinitionCanonicalElement = value; OnPropertyChanged("DefinitionCanonicalElement"); }
        }
        
        private List<Hl7.Fhir.Model.Canonical> _DefinitionCanonicalElement;
        
        /// <summary>
        /// Resource defining the code of this ChargeItem
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> DefinitionCanonical
        {
            get { return DefinitionCanonicalElement != null ? DefinitionCanonicalElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  DefinitionCanonicalElement = null; 
                else
                  DefinitionCanonicalElement = new List<Hl7.Fhir.Model.Canonical>(value.Select(elem=>new Hl7.Fhir.Model.Canonical(elem)));
                OnPropertyChanged("DefinitionCanonical");
            }
        }
        
        /// <summary>
        /// planned | billable | not-billable | aborted | billed | entered-in-error | unknown
        /// </summary>
        [FhirElement("status", InSummary=true, Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ChargeItem.ChargeItemStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.ChargeItem.ChargeItemStatus> _StatusElement;
        
        /// <summary>
        /// planned | billable | not-billable | aborted | billed | entered-in-error | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ChargeItem.ChargeItemStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.ChargeItem.ChargeItemStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Part of referenced ChargeItem
        /// </summary>
        [FhirElement("partOf", Order=130)]
        [CLSCompliant(false)]
		[References("ChargeItem")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> PartOf
        {
            get { if(_PartOf==null) _PartOf = new List<Hl7.Fhir.Model.ResourceReference>(); return _PartOf; }
            set { _PartOf = value; OnPropertyChanged("PartOf"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _PartOf;
        
        /// <summary>
        /// A code that identifies the charge, like a billing code
        /// </summary>
        [FhirElement("code", InSummary=true, Order=140)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Code;
        
        /// <summary>
        /// Individual service was done for/to
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=150)]
        [CLSCompliant(false)]
		[References("Patient","Group")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Encounter / Episode associated with event
        /// </summary>
        [FhirElement("context", InSummary=true, Order=160)]
        [CLSCompliant(false)]
		[References("Encounter","EpisodeOfCare")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Context
        {
            get { return _Context; }
            set { _Context = value; OnPropertyChanged("Context"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Context;
        
        /// <summary>
        /// When the charged service was applied
        /// </summary>
        [FhirElement("occurrence", InSummary=true, Order=170, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Timing))]
        [DataMember]
        public Hl7.Fhir.Model.Element Occurrence
        {
            get { return _Occurrence; }
            set { _Occurrence = value; OnPropertyChanged("Occurrence"); }
        }
        
        private Hl7.Fhir.Model.Element _Occurrence;
        
        /// <summary>
        /// Who performed charged service
        /// </summary>
        [FhirElement("performer", Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ChargeItem.PerformerComponent> Performer
        {
            get { if(_Performer==null) _Performer = new List<Hl7.Fhir.Model.ChargeItem.PerformerComponent>(); return _Performer; }
            set { _Performer = value; OnPropertyChanged("Performer"); }
        }
        
        private List<Hl7.Fhir.Model.ChargeItem.PerformerComponent> _Performer;
        
        /// <summary>
        /// Organization providing the charged service
        /// </summary>
        [FhirElement("performingOrganization", Order=190)]
        [CLSCompliant(false)]
		[References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference PerformingOrganization
        {
            get { return _PerformingOrganization; }
            set { _PerformingOrganization = value; OnPropertyChanged("PerformingOrganization"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _PerformingOrganization;
        
        /// <summary>
        /// Organization requesting the charged service
        /// </summary>
        [FhirElement("requestingOrganization", Order=200)]
        [CLSCompliant(false)]
		[References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference RequestingOrganization
        {
            get { return _RequestingOrganization; }
            set { _RequestingOrganization = value; OnPropertyChanged("RequestingOrganization"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _RequestingOrganization;
        
        /// <summary>
        /// Organization that has ownership of the (potential, future) revenue
        /// </summary>
        [FhirElement("costCenter", Order=210)]
        [CLSCompliant(false)]
		[References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference CostCenter
        {
            get { return _CostCenter; }
            set { _CostCenter = value; OnPropertyChanged("CostCenter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _CostCenter;
        
        /// <summary>
        /// Quantity of which the charge item has been serviced
        /// </summary>
        [FhirElement("quantity", InSummary=true, Order=220)]
        [DataMember]
        public Quantity Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; OnPropertyChanged("Quantity"); }
        }
        
        private Quantity _Quantity;
        
        /// <summary>
        /// Anatomical location, if relevant
        /// </summary>
        [FhirElement("bodysite", InSummary=true, Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Bodysite
        {
            get { if(_Bodysite==null) _Bodysite = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Bodysite; }
            set { _Bodysite = value; OnPropertyChanged("Bodysite"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Bodysite;
        
        /// <summary>
        /// Factor overriding the associated rules
        /// </summary>
        [FhirElement("factorOverride", Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDecimal FactorOverrideElement
        {
            get { return _FactorOverrideElement; }
            set { _FactorOverrideElement = value; OnPropertyChanged("FactorOverrideElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDecimal _FactorOverrideElement;
        
        /// <summary>
        /// Factor overriding the associated rules
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public decimal? FactorOverride
        {
            get { return FactorOverrideElement != null ? FactorOverrideElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  FactorOverrideElement = null; 
                else
                  FactorOverrideElement = new Hl7.Fhir.Model.FhirDecimal(value);
                OnPropertyChanged("FactorOverride");
            }
        }
        
        /// <summary>
        /// Price overriding the associated rules
        /// </summary>
        [FhirElement("priceOverride", Order=250)]
        [DataMember]
        public Money PriceOverride
        {
            get { return _PriceOverride; }
            set { _PriceOverride = value; OnPropertyChanged("PriceOverride"); }
        }
        
        private Money _PriceOverride;
        
        /// <summary>
        /// Reason for overriding the list price/factor
        /// </summary>
        [FhirElement("overrideReason", Order=260)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString OverrideReasonElement
        {
            get { return _OverrideReasonElement; }
            set { _OverrideReasonElement = value; OnPropertyChanged("OverrideReasonElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _OverrideReasonElement;
        
        /// <summary>
        /// Reason for overriding the list price/factor
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string OverrideReason
        {
            get { return OverrideReasonElement != null ? OverrideReasonElement.Value : null; }
            set
            {
                if (value == null)
                  OverrideReasonElement = null; 
                else
                  OverrideReasonElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("OverrideReason");
            }
        }
        
        /// <summary>
        /// Individual who was entering
        /// </summary>
        [FhirElement("enterer", InSummary=true, Order=270)]
        [CLSCompliant(false)]
		[References("Practitioner","PractitionerRole","Organization","Patient","Device","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Enterer
        {
            get { return _Enterer; }
            set { _Enterer = value; OnPropertyChanged("Enterer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Enterer;
        
        /// <summary>
        /// Date the charge item was entered
        /// </summary>
        [FhirElement("enteredDate", InSummary=true, Order=280)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime EnteredDateElement
        {
            get { return _EnteredDateElement; }
            set { _EnteredDateElement = value; OnPropertyChanged("EnteredDateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _EnteredDateElement;
        
        /// <summary>
        /// Date the charge item was entered
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string EnteredDate
        {
            get { return EnteredDateElement != null ? EnteredDateElement.Value : null; }
            set
            {
                if (value == null)
                  EnteredDateElement = null; 
                else
                  EnteredDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("EnteredDate");
            }
        }
        
        /// <summary>
        /// Why was the charged  service rendered?
        /// </summary>
        [FhirElement("reason", Order=290)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Reason
        {
            get { if(_Reason==null) _Reason = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Reason; }
            set { _Reason = value; OnPropertyChanged("Reason"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Reason;
        
        /// <summary>
        /// Which rendered service is being charged?
        /// </summary>
        [FhirElement("service", Order=300)]
        [CLSCompliant(false)]
		[References("DiagnosticReport","ImagingStudy","Immunization","MedicationAdministration","MedicationDispense","Observation","Procedure","SupplyDelivery")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Service
        {
            get { if(_Service==null) _Service = new List<Hl7.Fhir.Model.ResourceReference>(); return _Service; }
            set { _Service = value; OnPropertyChanged("Service"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Service;
        
        /// <summary>
        /// Product charged
        /// </summary>
        [FhirElement("product", Order=310, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.CodeableConcept))]
        [DataMember]
        public Hl7.Fhir.Model.Element Product
        {
            get { return _Product; }
            set { _Product = value; OnPropertyChanged("Product"); }
        }
        
        private Hl7.Fhir.Model.Element _Product;
        
        /// <summary>
        /// Account to place this charge
        /// </summary>
        [FhirElement("account", InSummary=true, Order=320)]
        [CLSCompliant(false)]
		[References("Account")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Account
        {
            get { if(_Account==null) _Account = new List<Hl7.Fhir.Model.ResourceReference>(); return _Account; }
            set { _Account = value; OnPropertyChanged("Account"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Account;
        
        /// <summary>
        /// Comments made about the ChargeItem
        /// </summary>
        [FhirElement("note", Order=330)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        
        /// <summary>
        /// Further information supporting this charge
        /// </summary>
        [FhirElement("supportingInformation", Order=340)]
        [CLSCompliant(false)]
		[References()]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> SupportingInformation
        {
            get { if(_SupportingInformation==null) _SupportingInformation = new List<Hl7.Fhir.Model.ResourceReference>(); return _SupportingInformation; }
            set { _SupportingInformation = value; OnPropertyChanged("SupportingInformation"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _SupportingInformation;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ChargeItem;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(DefinitionUriElement != null) dest.DefinitionUriElement = new List<Hl7.Fhir.Model.FhirUri>(DefinitionUriElement.DeepCopy());
                if(DefinitionCanonicalElement != null) dest.DefinitionCanonicalElement = new List<Hl7.Fhir.Model.Canonical>(DefinitionCanonicalElement.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.ChargeItem.ChargeItemStatus>)StatusElement.DeepCopy();
                if(PartOf != null) dest.PartOf = new List<Hl7.Fhir.Model.ResourceReference>(PartOf.DeepCopy());
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Context != null) dest.Context = (Hl7.Fhir.Model.ResourceReference)Context.DeepCopy();
                if(Occurrence != null) dest.Occurrence = (Hl7.Fhir.Model.Element)Occurrence.DeepCopy();
                if(Performer != null) dest.Performer = new List<Hl7.Fhir.Model.ChargeItem.PerformerComponent>(Performer.DeepCopy());
                if(PerformingOrganization != null) dest.PerformingOrganization = (Hl7.Fhir.Model.ResourceReference)PerformingOrganization.DeepCopy();
                if(RequestingOrganization != null) dest.RequestingOrganization = (Hl7.Fhir.Model.ResourceReference)RequestingOrganization.DeepCopy();
                if(CostCenter != null) dest.CostCenter = (Hl7.Fhir.Model.ResourceReference)CostCenter.DeepCopy();
                if(Quantity != null) dest.Quantity = (Quantity)Quantity.DeepCopy();
                if(Bodysite != null) dest.Bodysite = new List<Hl7.Fhir.Model.CodeableConcept>(Bodysite.DeepCopy());
                if(FactorOverrideElement != null) dest.FactorOverrideElement = (Hl7.Fhir.Model.FhirDecimal)FactorOverrideElement.DeepCopy();
                if(PriceOverride != null) dest.PriceOverride = (Money)PriceOverride.DeepCopy();
                if(OverrideReasonElement != null) dest.OverrideReasonElement = (Hl7.Fhir.Model.FhirString)OverrideReasonElement.DeepCopy();
                if(Enterer != null) dest.Enterer = (Hl7.Fhir.Model.ResourceReference)Enterer.DeepCopy();
                if(EnteredDateElement != null) dest.EnteredDateElement = (Hl7.Fhir.Model.FhirDateTime)EnteredDateElement.DeepCopy();
                if(Reason != null) dest.Reason = new List<Hl7.Fhir.Model.CodeableConcept>(Reason.DeepCopy());
                if(Service != null) dest.Service = new List<Hl7.Fhir.Model.ResourceReference>(Service.DeepCopy());
                if(Product != null) dest.Product = (Hl7.Fhir.Model.Element)Product.DeepCopy();
                if(Account != null) dest.Account = new List<Hl7.Fhir.Model.ResourceReference>(Account.DeepCopy());
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(SupportingInformation != null) dest.SupportingInformation = new List<Hl7.Fhir.Model.ResourceReference>(SupportingInformation.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new ChargeItem());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ChargeItem;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(DefinitionUriElement, otherT.DefinitionUriElement)) return false;
            if( !DeepComparable.Matches(DefinitionCanonicalElement, otherT.DefinitionCanonicalElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(PartOf, otherT.PartOf)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Context, otherT.Context)) return false;
            if( !DeepComparable.Matches(Occurrence, otherT.Occurrence)) return false;
            if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
            if( !DeepComparable.Matches(PerformingOrganization, otherT.PerformingOrganization)) return false;
            if( !DeepComparable.Matches(RequestingOrganization, otherT.RequestingOrganization)) return false;
            if( !DeepComparable.Matches(CostCenter, otherT.CostCenter)) return false;
            if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.Matches(Bodysite, otherT.Bodysite)) return false;
            if( !DeepComparable.Matches(FactorOverrideElement, otherT.FactorOverrideElement)) return false;
            if( !DeepComparable.Matches(PriceOverride, otherT.PriceOverride)) return false;
            if( !DeepComparable.Matches(OverrideReasonElement, otherT.OverrideReasonElement)) return false;
            if( !DeepComparable.Matches(Enterer, otherT.Enterer)) return false;
            if( !DeepComparable.Matches(EnteredDateElement, otherT.EnteredDateElement)) return false;
            if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
            if( !DeepComparable.Matches(Service, otherT.Service)) return false;
            if( !DeepComparable.Matches(Product, otherT.Product)) return false;
            if( !DeepComparable.Matches(Account, otherT.Account)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(SupportingInformation, otherT.SupportingInformation)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ChargeItem;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(DefinitionUriElement, otherT.DefinitionUriElement)) return false;
            if( !DeepComparable.IsExactly(DefinitionCanonicalElement, otherT.DefinitionCanonicalElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(PartOf, otherT.PartOf)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
            if( !DeepComparable.IsExactly(Occurrence, otherT.Occurrence)) return false;
            if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
            if( !DeepComparable.IsExactly(PerformingOrganization, otherT.PerformingOrganization)) return false;
            if( !DeepComparable.IsExactly(RequestingOrganization, otherT.RequestingOrganization)) return false;
            if( !DeepComparable.IsExactly(CostCenter, otherT.CostCenter)) return false;
            if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.IsExactly(Bodysite, otherT.Bodysite)) return false;
            if( !DeepComparable.IsExactly(FactorOverrideElement, otherT.FactorOverrideElement)) return false;
            if( !DeepComparable.IsExactly(PriceOverride, otherT.PriceOverride)) return false;
            if( !DeepComparable.IsExactly(OverrideReasonElement, otherT.OverrideReasonElement)) return false;
            if( !DeepComparable.IsExactly(Enterer, otherT.Enterer)) return false;
            if( !DeepComparable.IsExactly(EnteredDateElement, otherT.EnteredDateElement)) return false;
            if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
            if( !DeepComparable.IsExactly(Service, otherT.Service)) return false;
            if( !DeepComparable.IsExactly(Product, otherT.Product)) return false;
            if( !DeepComparable.IsExactly(Account, otherT.Account)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(SupportingInformation, otherT.SupportingInformation)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				foreach (var elem in DefinitionUriElement) { if (elem != null) yield return elem; }
				foreach (var elem in DefinitionCanonicalElement) { if (elem != null) yield return elem; }
				if (StatusElement != null) yield return StatusElement;
				foreach (var elem in PartOf) { if (elem != null) yield return elem; }
				if (Code != null) yield return Code;
				if (Subject != null) yield return Subject;
				if (Context != null) yield return Context;
				if (Occurrence != null) yield return Occurrence;
				foreach (var elem in Performer) { if (elem != null) yield return elem; }
				if (PerformingOrganization != null) yield return PerformingOrganization;
				if (RequestingOrganization != null) yield return RequestingOrganization;
				if (CostCenter != null) yield return CostCenter;
				if (Quantity != null) yield return Quantity;
				foreach (var elem in Bodysite) { if (elem != null) yield return elem; }
				if (FactorOverrideElement != null) yield return FactorOverrideElement;
				if (PriceOverride != null) yield return PriceOverride;
				if (OverrideReasonElement != null) yield return OverrideReasonElement;
				if (Enterer != null) yield return Enterer;
				if (EnteredDateElement != null) yield return EnteredDateElement;
				foreach (var elem in Reason) { if (elem != null) yield return elem; }
				foreach (var elem in Service) { if (elem != null) yield return elem; }
				if (Product != null) yield return Product;
				foreach (var elem in Account) { if (elem != null) yield return elem; }
				foreach (var elem in Note) { if (elem != null) yield return elem; }
				foreach (var elem in SupportingInformation) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                foreach (var elem in DefinitionUriElement) { if (elem != null) yield return new ElementValue("definitionUri", elem); }
                foreach (var elem in DefinitionCanonicalElement) { if (elem != null) yield return new ElementValue("definitionCanonical", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                foreach (var elem in PartOf) { if (elem != null) yield return new ElementValue("partOf", elem); }
                if (Code != null) yield return new ElementValue("code", Code);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Context != null) yield return new ElementValue("context", Context);
                if (Occurrence != null) yield return new ElementValue("occurrence", Occurrence);
                foreach (var elem in Performer) { if (elem != null) yield return new ElementValue("performer", elem); }
                if (PerformingOrganization != null) yield return new ElementValue("performingOrganization", PerformingOrganization);
                if (RequestingOrganization != null) yield return new ElementValue("requestingOrganization", RequestingOrganization);
                if (CostCenter != null) yield return new ElementValue("costCenter", CostCenter);
                if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                foreach (var elem in Bodysite) { if (elem != null) yield return new ElementValue("bodysite", elem); }
                if (FactorOverrideElement != null) yield return new ElementValue("factorOverride", FactorOverrideElement);
                if (PriceOverride != null) yield return new ElementValue("priceOverride", PriceOverride);
                if (OverrideReasonElement != null) yield return new ElementValue("overrideReason", OverrideReasonElement);
                if (Enterer != null) yield return new ElementValue("enterer", Enterer);
                if (EnteredDateElement != null) yield return new ElementValue("enteredDate", EnteredDateElement);
                foreach (var elem in Reason) { if (elem != null) yield return new ElementValue("reason", elem); }
                foreach (var elem in Service) { if (elem != null) yield return new ElementValue("service", elem); }
                if (Product != null) yield return new ElementValue("product", Product);
                foreach (var elem in Account) { if (elem != null) yield return new ElementValue("account", elem); }
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                foreach (var elem in SupportingInformation) { if (elem != null) yield return new ElementValue("supportingInformation", elem); }
            }
        }

    }
    
}
