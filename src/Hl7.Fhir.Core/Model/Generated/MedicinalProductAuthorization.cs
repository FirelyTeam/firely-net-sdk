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
    /// The regulatory authorization of a medicinal product
    /// </summary>
    [FhirType("MedicinalProductAuthorization", IsResource=true)]
    [DataContract]
    public partial class MedicinalProductAuthorization : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.MedicinalProductAuthorization; } }
        [NotMapped]
        public override string TypeName { get { return "MedicinalProductAuthorization"; } }
        
        [FhirType("JurisdictionalAuthorizationComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class JurisdictionalAuthorizationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "JurisdictionalAuthorizationComponent"; } }
            
            /// <summary>
            /// The assigned number for the marketing authorization
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Identifier> Identifier
            {
                get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private List<Hl7.Fhir.Model.Identifier> _Identifier;
            
            /// <summary>
            /// Country of authorization
            /// </summary>
            [FhirElement("country", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Country
            {
                get { return _Country; }
                set { _Country = value; OnPropertyChanged("Country"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Country;
            
            /// <summary>
            /// Jurisdiction within a country
            /// </summary>
            [FhirElement("jurisdiction", InSummary=true, Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Jurisdiction
            {
                get { if(_Jurisdiction==null) _Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Jurisdiction; }
                set { _Jurisdiction = value; OnPropertyChanged("Jurisdiction"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Jurisdiction;
            
            /// <summary>
            /// The legal status of supply in a jurisdiction or region
            /// </summary>
            [FhirElement("legalStatusOfSupply", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept LegalStatusOfSupply
            {
                get { return _LegalStatusOfSupply; }
                set { _LegalStatusOfSupply = value; OnPropertyChanged("LegalStatusOfSupply"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _LegalStatusOfSupply;
            
            /// <summary>
            /// The start and expected end date of the authorization
            /// </summary>
            [FhirElement("validityPeriod", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Period ValidityPeriod
            {
                get { return _ValidityPeriod; }
                set { _ValidityPeriod = value; OnPropertyChanged("ValidityPeriod"); }
            }
            
            private Hl7.Fhir.Model.Period _ValidityPeriod;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as JurisdictionalAuthorizationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                    if(Country != null) dest.Country = (Hl7.Fhir.Model.CodeableConcept)Country.DeepCopy();
                    if(Jurisdiction != null) dest.Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(Jurisdiction.DeepCopy());
                    if(LegalStatusOfSupply != null) dest.LegalStatusOfSupply = (Hl7.Fhir.Model.CodeableConcept)LegalStatusOfSupply.DeepCopy();
                    if(ValidityPeriod != null) dest.ValidityPeriod = (Hl7.Fhir.Model.Period)ValidityPeriod.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new JurisdictionalAuthorizationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as JurisdictionalAuthorizationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(Country, otherT.Country)) return false;
                if( !DeepComparable.Matches(Jurisdiction, otherT.Jurisdiction)) return false;
                if( !DeepComparable.Matches(LegalStatusOfSupply, otherT.LegalStatusOfSupply)) return false;
                if( !DeepComparable.Matches(ValidityPeriod, otherT.ValidityPeriod)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as JurisdictionalAuthorizationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(Country, otherT.Country)) return false;
                if( !DeepComparable.IsExactly(Jurisdiction, otherT.Jurisdiction)) return false;
                if( !DeepComparable.IsExactly(LegalStatusOfSupply, otherT.LegalStatusOfSupply)) return false;
                if( !DeepComparable.IsExactly(ValidityPeriod, otherT.ValidityPeriod)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                    if (Country != null) yield return Country;
                    foreach (var elem in Jurisdiction) { if (elem != null) yield return elem; }
                    if (LegalStatusOfSupply != null) yield return LegalStatusOfSupply;
                    if (ValidityPeriod != null) yield return ValidityPeriod;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                    if (Country != null) yield return new ElementValue("country", Country);
                    foreach (var elem in Jurisdiction) { if (elem != null) yield return new ElementValue("jurisdiction", elem); }
                    if (LegalStatusOfSupply != null) yield return new ElementValue("legalStatusOfSupply", LegalStatusOfSupply);
                    if (ValidityPeriod != null) yield return new ElementValue("validityPeriod", ValidityPeriod);
                }
            }

            
        }
        
        
        [FhirType("ProcedureComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ProcedureComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ProcedureComponent"; } }
            
            /// <summary>
            /// Identifier for this procedure
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// Type of procedure
            /// </summary>
            [FhirElement("type", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Date of procedure
            /// </summary>
            [FhirElement("date", InSummary=true, Order=60, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.FhirDateTime))]
            [DataMember]
            public Hl7.Fhir.Model.Element Date
            {
                get { return _Date; }
                set { _Date = value; OnPropertyChanged("Date"); }
            }
            
            private Hl7.Fhir.Model.Element _Date;
            
            /// <summary>
            /// Applcations submitted to obtain a marketing authorization
            /// </summary>
            [FhirElement("application", InSummary=true, Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.MedicinalProductAuthorization.ProcedureComponent> Application
            {
                get { if(_Application==null) _Application = new List<Hl7.Fhir.Model.MedicinalProductAuthorization.ProcedureComponent>(); return _Application; }
                set { _Application = value; OnPropertyChanged("Application"); }
            }
            
            private List<Hl7.Fhir.Model.MedicinalProductAuthorization.ProcedureComponent> _Application;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ProcedureComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Date != null) dest.Date = (Hl7.Fhir.Model.Element)Date.DeepCopy();
                    if(Application != null) dest.Application = new List<Hl7.Fhir.Model.MedicinalProductAuthorization.ProcedureComponent>(Application.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ProcedureComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ProcedureComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Date, otherT.Date)) return false;
                if( !DeepComparable.Matches(Application, otherT.Application)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ProcedureComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Date, otherT.Date)) return false;
                if( !DeepComparable.IsExactly(Application, otherT.Application)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Identifier != null) yield return Identifier;
                    if (Type != null) yield return Type;
                    if (Date != null) yield return Date;
                    foreach (var elem in Application) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Date != null) yield return new ElementValue("date", Date);
                    foreach (var elem in Application) { if (elem != null) yield return new ElementValue("application", elem); }
                }
            }

            
        }
        
        
        /// <summary>
        /// Business identifier for the marketing authorization, as assigned by a regulator
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
        /// The medicinal product that is being authorized
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=100)]
        [CLSCompliant(false)]
		[References("MedicinalProduct","MedicinalProductPackaged")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// The country in which the marketing authorization has been granted
        /// </summary>
        [FhirElement("country", InSummary=true, Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Country
        {
            get { if(_Country==null) _Country = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Country; }
            set { _Country = value; OnPropertyChanged("Country"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Country;
        
        /// <summary>
        /// Jurisdiction within a country
        /// </summary>
        [FhirElement("jurisdiction", InSummary=true, Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Jurisdiction
        {
            get { if(_Jurisdiction==null) _Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Jurisdiction; }
            set { _Jurisdiction = value; OnPropertyChanged("Jurisdiction"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Jurisdiction;
        
        /// <summary>
        /// The status of the marketing authorization
        /// </summary>
        [FhirElement("status", InSummary=true, Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Status
        {
            get { return _Status; }
            set { _Status = value; OnPropertyChanged("Status"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Status;
        
        /// <summary>
        /// The date at which the given status has become applicable
        /// </summary>
        [FhirElement("statusDate", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime StatusDateElement
        {
            get { return _StatusDateElement; }
            set { _StatusDateElement = value; OnPropertyChanged("StatusDateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _StatusDateElement;
        
        /// <summary>
        /// The date at which the given status has become applicable
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string StatusDate
        {
            get { return StatusDateElement != null ? StatusDateElement.Value : null; }
            set
            {
                if (value == null)
                  StatusDateElement = null; 
                else
                  StatusDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("StatusDate");
            }
        }
        
        /// <summary>
        /// The date when a suspended the marketing or the marketing authorization of the product is anticipated to be restored
        /// </summary>
        [FhirElement("restoreDate", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime RestoreDateElement
        {
            get { return _RestoreDateElement; }
            set { _RestoreDateElement = value; OnPropertyChanged("RestoreDateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _RestoreDateElement;
        
        /// <summary>
        /// The date when a suspended the marketing or the marketing authorization of the product is anticipated to be restored
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string RestoreDate
        {
            get { return RestoreDateElement != null ? RestoreDateElement.Value : null; }
            set
            {
                if (value == null)
                  RestoreDateElement = null; 
                else
                  RestoreDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("RestoreDate");
            }
        }
        
        /// <summary>
        /// The beginning of the time period in which the marketing authorization is in the specific status shall be specified A complete date consisting of day, month and year shall be specified using the ISO 8601 date format
        /// </summary>
        [FhirElement("validityPeriod", InSummary=true, Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.Period ValidityPeriod
        {
            get { return _ValidityPeriod; }
            set { _ValidityPeriod = value; OnPropertyChanged("ValidityPeriod"); }
        }
        
        private Hl7.Fhir.Model.Period _ValidityPeriod;
        
        /// <summary>
        /// A period of time after authorization before generic product applicatiosn can be submitted
        /// </summary>
        [FhirElement("dataExclusivityPeriod", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.Period DataExclusivityPeriod
        {
            get { return _DataExclusivityPeriod; }
            set { _DataExclusivityPeriod = value; OnPropertyChanged("DataExclusivityPeriod"); }
        }
        
        private Hl7.Fhir.Model.Period _DataExclusivityPeriod;
        
        /// <summary>
        /// The date when the first authorization was granted by a Medicines Regulatory Agency
        /// </summary>
        [FhirElement("dateOfFirstAuthorization", InSummary=true, Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateOfFirstAuthorizationElement
        {
            get { return _DateOfFirstAuthorizationElement; }
            set { _DateOfFirstAuthorizationElement = value; OnPropertyChanged("DateOfFirstAuthorizationElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateOfFirstAuthorizationElement;
        
        /// <summary>
        /// The date when the first authorization was granted by a Medicines Regulatory Agency
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string DateOfFirstAuthorization
        {
            get { return DateOfFirstAuthorizationElement != null ? DateOfFirstAuthorizationElement.Value : null; }
            set
            {
                if (value == null)
                  DateOfFirstAuthorizationElement = null; 
                else
                  DateOfFirstAuthorizationElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("DateOfFirstAuthorization");
            }
        }
        
        /// <summary>
        /// Date of first marketing authorization for a company's new medicinal product in any country in the World
        /// </summary>
        [FhirElement("internationalBirthDate", InSummary=true, Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime InternationalBirthDateElement
        {
            get { return _InternationalBirthDateElement; }
            set { _InternationalBirthDateElement = value; OnPropertyChanged("InternationalBirthDateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _InternationalBirthDateElement;
        
        /// <summary>
        /// Date of first marketing authorization for a company's new medicinal product in any country in the World
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string InternationalBirthDate
        {
            get { return InternationalBirthDateElement != null ? InternationalBirthDateElement.Value : null; }
            set
            {
                if (value == null)
                  InternationalBirthDateElement = null; 
                else
                  InternationalBirthDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("InternationalBirthDate");
            }
        }
        
        /// <summary>
        /// The legal framework against which this authorization is granted
        /// </summary>
        [FhirElement("legalBasis", InSummary=true, Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept LegalBasis
        {
            get { return _LegalBasis; }
            set { _LegalBasis = value; OnPropertyChanged("LegalBasis"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _LegalBasis;
        
        /// <summary>
        /// Authorization in areas within a country
        /// </summary>
        [FhirElement("jurisdictionalAuthorization", InSummary=true, Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MedicinalProductAuthorization.JurisdictionalAuthorizationComponent> JurisdictionalAuthorization
        {
            get { if(_JurisdictionalAuthorization==null) _JurisdictionalAuthorization = new List<Hl7.Fhir.Model.MedicinalProductAuthorization.JurisdictionalAuthorizationComponent>(); return _JurisdictionalAuthorization; }
            set { _JurisdictionalAuthorization = value; OnPropertyChanged("JurisdictionalAuthorization"); }
        }
        
        private List<Hl7.Fhir.Model.MedicinalProductAuthorization.JurisdictionalAuthorizationComponent> _JurisdictionalAuthorization;
        
        /// <summary>
        /// Marketing Authorization Holder
        /// </summary>
        [FhirElement("holder", InSummary=true, Order=220)]
        [CLSCompliant(false)]
		[References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Holder
        {
            get { return _Holder; }
            set { _Holder = value; OnPropertyChanged("Holder"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Holder;
        
        /// <summary>
        /// Medicines Regulatory Agency
        /// </summary>
        [FhirElement("regulator", InSummary=true, Order=230)]
        [CLSCompliant(false)]
		[References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Regulator
        {
            get { return _Regulator; }
            set { _Regulator = value; OnPropertyChanged("Regulator"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Regulator;
        
        /// <summary>
        /// The regulatory procedure for granting or amending a marketing authorization
        /// </summary>
        [FhirElement("procedure", InSummary=true, Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.MedicinalProductAuthorization.ProcedureComponent Procedure
        {
            get { return _Procedure; }
            set { _Procedure = value; OnPropertyChanged("Procedure"); }
        }
        
        private Hl7.Fhir.Model.MedicinalProductAuthorization.ProcedureComponent _Procedure;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MedicinalProductAuthorization;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Country != null) dest.Country = new List<Hl7.Fhir.Model.CodeableConcept>(Country.DeepCopy());
                if(Jurisdiction != null) dest.Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(Jurisdiction.DeepCopy());
                if(Status != null) dest.Status = (Hl7.Fhir.Model.CodeableConcept)Status.DeepCopy();
                if(StatusDateElement != null) dest.StatusDateElement = (Hl7.Fhir.Model.FhirDateTime)StatusDateElement.DeepCopy();
                if(RestoreDateElement != null) dest.RestoreDateElement = (Hl7.Fhir.Model.FhirDateTime)RestoreDateElement.DeepCopy();
                if(ValidityPeriod != null) dest.ValidityPeriod = (Hl7.Fhir.Model.Period)ValidityPeriod.DeepCopy();
                if(DataExclusivityPeriod != null) dest.DataExclusivityPeriod = (Hl7.Fhir.Model.Period)DataExclusivityPeriod.DeepCopy();
                if(DateOfFirstAuthorizationElement != null) dest.DateOfFirstAuthorizationElement = (Hl7.Fhir.Model.FhirDateTime)DateOfFirstAuthorizationElement.DeepCopy();
                if(InternationalBirthDateElement != null) dest.InternationalBirthDateElement = (Hl7.Fhir.Model.FhirDateTime)InternationalBirthDateElement.DeepCopy();
                if(LegalBasis != null) dest.LegalBasis = (Hl7.Fhir.Model.CodeableConcept)LegalBasis.DeepCopy();
                if(JurisdictionalAuthorization != null) dest.JurisdictionalAuthorization = new List<Hl7.Fhir.Model.MedicinalProductAuthorization.JurisdictionalAuthorizationComponent>(JurisdictionalAuthorization.DeepCopy());
                if(Holder != null) dest.Holder = (Hl7.Fhir.Model.ResourceReference)Holder.DeepCopy();
                if(Regulator != null) dest.Regulator = (Hl7.Fhir.Model.ResourceReference)Regulator.DeepCopy();
                if(Procedure != null) dest.Procedure = (Hl7.Fhir.Model.MedicinalProductAuthorization.ProcedureComponent)Procedure.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new MedicinalProductAuthorization());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as MedicinalProductAuthorization;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Country, otherT.Country)) return false;
            if( !DeepComparable.Matches(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.Matches(Status, otherT.Status)) return false;
            if( !DeepComparable.Matches(StatusDateElement, otherT.StatusDateElement)) return false;
            if( !DeepComparable.Matches(RestoreDateElement, otherT.RestoreDateElement)) return false;
            if( !DeepComparable.Matches(ValidityPeriod, otherT.ValidityPeriod)) return false;
            if( !DeepComparable.Matches(DataExclusivityPeriod, otherT.DataExclusivityPeriod)) return false;
            if( !DeepComparable.Matches(DateOfFirstAuthorizationElement, otherT.DateOfFirstAuthorizationElement)) return false;
            if( !DeepComparable.Matches(InternationalBirthDateElement, otherT.InternationalBirthDateElement)) return false;
            if( !DeepComparable.Matches(LegalBasis, otherT.LegalBasis)) return false;
            if( !DeepComparable.Matches(JurisdictionalAuthorization, otherT.JurisdictionalAuthorization)) return false;
            if( !DeepComparable.Matches(Holder, otherT.Holder)) return false;
            if( !DeepComparable.Matches(Regulator, otherT.Regulator)) return false;
            if( !DeepComparable.Matches(Procedure, otherT.Procedure)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MedicinalProductAuthorization;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Country, otherT.Country)) return false;
            if( !DeepComparable.IsExactly(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.IsExactly(Status, otherT.Status)) return false;
            if( !DeepComparable.IsExactly(StatusDateElement, otherT.StatusDateElement)) return false;
            if( !DeepComparable.IsExactly(RestoreDateElement, otherT.RestoreDateElement)) return false;
            if( !DeepComparable.IsExactly(ValidityPeriod, otherT.ValidityPeriod)) return false;
            if( !DeepComparable.IsExactly(DataExclusivityPeriod, otherT.DataExclusivityPeriod)) return false;
            if( !DeepComparable.IsExactly(DateOfFirstAuthorizationElement, otherT.DateOfFirstAuthorizationElement)) return false;
            if( !DeepComparable.IsExactly(InternationalBirthDateElement, otherT.InternationalBirthDateElement)) return false;
            if( !DeepComparable.IsExactly(LegalBasis, otherT.LegalBasis)) return false;
            if( !DeepComparable.IsExactly(JurisdictionalAuthorization, otherT.JurisdictionalAuthorization)) return false;
            if( !DeepComparable.IsExactly(Holder, otherT.Holder)) return false;
            if( !DeepComparable.IsExactly(Regulator, otherT.Regulator)) return false;
            if( !DeepComparable.IsExactly(Procedure, otherT.Procedure)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (Subject != null) yield return Subject;
				foreach (var elem in Country) { if (elem != null) yield return elem; }
				foreach (var elem in Jurisdiction) { if (elem != null) yield return elem; }
				if (Status != null) yield return Status;
				if (StatusDateElement != null) yield return StatusDateElement;
				if (RestoreDateElement != null) yield return RestoreDateElement;
				if (ValidityPeriod != null) yield return ValidityPeriod;
				if (DataExclusivityPeriod != null) yield return DataExclusivityPeriod;
				if (DateOfFirstAuthorizationElement != null) yield return DateOfFirstAuthorizationElement;
				if (InternationalBirthDateElement != null) yield return InternationalBirthDateElement;
				if (LegalBasis != null) yield return LegalBasis;
				foreach (var elem in JurisdictionalAuthorization) { if (elem != null) yield return elem; }
				if (Holder != null) yield return Holder;
				if (Regulator != null) yield return Regulator;
				if (Procedure != null) yield return Procedure;
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (Subject != null) yield return new ElementValue("subject", Subject);
                foreach (var elem in Country) { if (elem != null) yield return new ElementValue("country", elem); }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return new ElementValue("jurisdiction", elem); }
                if (Status != null) yield return new ElementValue("status", Status);
                if (StatusDateElement != null) yield return new ElementValue("statusDate", StatusDateElement);
                if (RestoreDateElement != null) yield return new ElementValue("restoreDate", RestoreDateElement);
                if (ValidityPeriod != null) yield return new ElementValue("validityPeriod", ValidityPeriod);
                if (DataExclusivityPeriod != null) yield return new ElementValue("dataExclusivityPeriod", DataExclusivityPeriod);
                if (DateOfFirstAuthorizationElement != null) yield return new ElementValue("dateOfFirstAuthorization", DateOfFirstAuthorizationElement);
                if (InternationalBirthDateElement != null) yield return new ElementValue("internationalBirthDate", InternationalBirthDateElement);
                if (LegalBasis != null) yield return new ElementValue("legalBasis", LegalBasis);
                foreach (var elem in JurisdictionalAuthorization) { if (elem != null) yield return new ElementValue("jurisdictionalAuthorization", elem); }
                if (Holder != null) yield return new ElementValue("holder", Holder);
                if (Regulator != null) yield return new ElementValue("regulator", Regulator);
                if (Procedure != null) yield return new ElementValue("procedure", Procedure);
            }
        }

    }
    
}
