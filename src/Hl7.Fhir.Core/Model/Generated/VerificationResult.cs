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
// Generated for FHIR v3.1.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Describes validation requirements, source(s), status and dates for one or more elements
    /// </summary>
    [FhirType("VerificationResult", IsResource=true)]
    [DataContract]
    public partial class VerificationResult : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.VerificationResult; } }
        [NotMapped]
        public override string TypeName { get { return "VerificationResult"; } }
        
        /// <summary>
        /// The frequency with which the target must be validated
        /// (url: http://hl7.org/fhir/ValueSet/need)
        /// </summary>
        [FhirEnumeration("need")]
        public enum need
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/need)
            /// </summary>
            [EnumLiteral("none", "http://hl7.org/fhir/need"), Description("None")]
            None,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/need)
            /// </summary>
            [EnumLiteral("initial", "http://hl7.org/fhir/need"), Description("Initial")]
            Initial,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/need)
            /// </summary>
            [EnumLiteral("periodic", "http://hl7.org/fhir/need"), Description("Periodic")]
            Periodic,
        }

        /// <summary>
        /// The validation status of the target
        /// (url: http://hl7.org/fhir/ValueSet/status)
        /// </summary>
        [FhirEnumeration("status")]
        public enum status
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/status)
            /// </summary>
            [EnumLiteral("attested", "http://hl7.org/fhir/status"), Description("Attested")]
            Attested,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/status)
            /// </summary>
            [EnumLiteral("validated", "http://hl7.org/fhir/status"), Description("Validated")]
            Validated,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/status)
            /// </summary>
            [EnumLiteral("in-process", "http://hl7.org/fhir/status"), Description("In process")]
            InProcess,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/status)
            /// </summary>
            [EnumLiteral("req-revalid", "http://hl7.org/fhir/status"), Description("Requires revalidation")]
            ReqRevalid,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/status)
            /// </summary>
            [EnumLiteral("val-fail", "http://hl7.org/fhir/status"), Description("Validation failed")]
            ValFail,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/status)
            /// </summary>
            [EnumLiteral("reval-fail", "http://hl7.org/fhir/status"), Description("Re-Validation failed")]
            RevalFail,
        }

        /// <summary>
        /// What the target is validated against
        /// (url: http://hl7.org/fhir/ValueSet/validation-type)
        /// </summary>
        [FhirEnumeration("validation_type")]
        public enum validation_type
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/validation-type)
            /// </summary>
            [EnumLiteral("nothing", "http://hl7.org/fhir/validation-type"), Description("Nothing")]
            Nothing,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/validation-type)
            /// </summary>
            [EnumLiteral("primary", "http://hl7.org/fhir/validation-type"), Description("Primary Source")]
            Primary,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/validation-type)
            /// </summary>
            [EnumLiteral("multiple", "http://hl7.org/fhir/validation-type"), Description("Multiple Sources")]
            Multiple,
        }

        /// <summary>
        /// The result if validation fails
        /// (url: http://hl7.org/fhir/ValueSet/failure-action)
        /// </summary>
        [FhirEnumeration("failure_action")]
        public enum failure_action
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/failure-action)
            /// </summary>
            [EnumLiteral("fatal", "http://hl7.org/fhir/failure-action"), Description("Fatal")]
            Fatal,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/failure-action)
            /// </summary>
            [EnumLiteral("warn", "http://hl7.org/fhir/failure-action"), Description("Warning")]
            Warn,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/failure-action)
            /// </summary>
            [EnumLiteral("rec-only", "http://hl7.org/fhir/failure-action"), Description("Record only")]
            RecOnly,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/failure-action)
            /// </summary>
            [EnumLiteral("none", "http://hl7.org/fhir/failure-action"), Description("None")]
            None,
        }

        /// <summary>
        /// Status of the validation of the target against the primary source
        /// (url: http://hl7.org/fhir/ValueSet/validation-status)
        /// </summary>
        [FhirEnumeration("validation_status")]
        public enum validation_status
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/validation-status)
            /// </summary>
            [EnumLiteral("successful", "http://hl7.org/fhir/validation-status"), Description("Successful")]
            Successful,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/validation-status)
            /// </summary>
            [EnumLiteral("failed", "http://hl7.org/fhir/validation-status"), Description("Failed")]
            Failed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/validation-status)
            /// </summary>
            [EnumLiteral("unknown", "http://hl7.org/fhir/validation-status"), Description("Unknown")]
            Unknown,
        }

        /// <summary>
        /// Ability of the primary source to push updates/alerts
        /// (url: http://hl7.org/fhir/ValueSet/can-push-updates)
        /// </summary>
        [FhirEnumeration("can_push_updates")]
        public enum can_push_updates
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/can-push-updates)
            /// </summary>
            [EnumLiteral("yes", "http://hl7.org/fhir/can-push-updates"), Description("Yes")]
            Yes,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/can-push-updates)
            /// </summary>
            [EnumLiteral("no", "http://hl7.org/fhir/can-push-updates"), Description("No")]
            No,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/can-push-updates)
            /// </summary>
            [EnumLiteral("undetermined", "http://hl7.org/fhir/can-push-updates"), Description("Undetermined")]
            Undetermined,
        }

        /// <summary>
        /// Type of alerts/updates the primary source can send
        /// (url: http://hl7.org/fhir/ValueSet/push-type-available)
        /// </summary>
        [FhirEnumeration("push_type_available")]
        public enum push_type_available
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/push-type-available)
            /// </summary>
            [EnumLiteral("specific", "http://hl7.org/fhir/push-type-available"), Description("Specific requested changes")]
            Specific,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/push-type-available)
            /// </summary>
            [EnumLiteral("any", "http://hl7.org/fhir/push-type-available"), Description("Any changes")]
            Any,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/push-type-available)
            /// </summary>
            [EnumLiteral("source", "http://hl7.org/fhir/push-type-available"), Description("As defined by source")]
            Source,
        }

        [FhirType("PrimarySourceComponent")]
        [DataContract]
        public partial class PrimarySourceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "PrimarySourceComponent"; } }
            
            /// <summary>
            /// URI of the primary source for validation
            /// </summary>
            [FhirElement("identifier", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// Reference to the primary source
            /// </summary>
            [FhirElement("organization", Order=50)]
            [CLSCompliant(false)]
			[References("Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Organization
            {
                get { return _Organization; }
                set { _Organization = value; OnPropertyChanged("Organization"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Organization;
            
            /// <summary>
            /// Type of primary source (License Board; Primary Education; Continuing Education; Postal Service; Relationship owner; Registration Authority; legal source; issuing source; authoritative source)
            /// </summary>
            [FhirElement("type", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Type
            {
                get { if(_Type==null) _Type = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Type;
            
            /// <summary>
            /// Method for communicating with the primary source (manual; API; Push)
            /// </summary>
            [FhirElement("validationProcess", InSummary=true, Order=70)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> ValidationProcess
            {
                get { if(_ValidationProcess==null) _ValidationProcess = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ValidationProcess; }
                set { _ValidationProcess = value; OnPropertyChanged("ValidationProcess"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _ValidationProcess;
            
            /// <summary>
            /// successful | failed | unknown
            /// </summary>
            [FhirElement("validationStatus", Order=80)]
            [DataMember]
            public Code<Hl7.Fhir.Model.VerificationResult.validation_status> ValidationStatusElement
            {
                get { return _ValidationStatusElement; }
                set { _ValidationStatusElement = value; OnPropertyChanged("ValidationStatusElement"); }
            }
            
            private Code<Hl7.Fhir.Model.VerificationResult.validation_status> _ValidationStatusElement;
            
            /// <summary>
            /// successful | failed | unknown
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.VerificationResult.validation_status? ValidationStatus
            {
                get { return ValidationStatusElement != null ? ValidationStatusElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ValidationStatusElement = null; 
                    else
                        ValidationStatusElement = new Code<Hl7.Fhir.Model.VerificationResult.validation_status>(value);
                    OnPropertyChanged("ValidationStatus");
                }
            }
            
            /// <summary>
            /// When the target was validated against the primary source
            /// </summary>
            [FhirElement("validationDate", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime ValidationDateElement
            {
                get { return _ValidationDateElement; }
                set { _ValidationDateElement = value; OnPropertyChanged("ValidationDateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _ValidationDateElement;
            
            /// <summary>
            /// When the target was validated against the primary source
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ValidationDate
            {
                get { return ValidationDateElement != null ? ValidationDateElement.Value : null; }
                set
                {
                    if (value == null)
                        ValidationDateElement = null; 
                    else
                        ValidationDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("ValidationDate");
                }
            }
            
            /// <summary>
            /// yes | no | undetermined
            /// </summary>
            [FhirElement("canPushUpdates", InSummary=true, Order=100)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.VerificationResult.can_push_updates> CanPushUpdatesElement
            {
                get { return _CanPushUpdatesElement; }
                set { _CanPushUpdatesElement = value; OnPropertyChanged("CanPushUpdatesElement"); }
            }
            
            private Code<Hl7.Fhir.Model.VerificationResult.can_push_updates> _CanPushUpdatesElement;
            
            /// <summary>
            /// yes | no | undetermined
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.VerificationResult.can_push_updates? CanPushUpdates
            {
                get { return CanPushUpdatesElement != null ? CanPushUpdatesElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        CanPushUpdatesElement = null; 
                    else
                        CanPushUpdatesElement = new Code<Hl7.Fhir.Model.VerificationResult.can_push_updates>(value);
                    OnPropertyChanged("CanPushUpdates");
                }
            }
            
            /// <summary>
            /// specific | any | source
            /// </summary>
            [FhirElement("pushTypeAvailable", Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Code<Hl7.Fhir.Model.VerificationResult.push_type_available>> PushTypeAvailableElement
            {
                get { if(_PushTypeAvailableElement==null) _PushTypeAvailableElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.VerificationResult.push_type_available>>(); return _PushTypeAvailableElement; }
                set { _PushTypeAvailableElement = value; OnPropertyChanged("PushTypeAvailableElement"); }
            }
            
            private List<Code<Hl7.Fhir.Model.VerificationResult.push_type_available>> _PushTypeAvailableElement;
            
            /// <summary>
            /// specific | any | source
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<Hl7.Fhir.Model.VerificationResult.push_type_available?> PushTypeAvailable
            {
                get { return PushTypeAvailableElement != null ? PushTypeAvailableElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        PushTypeAvailableElement = null; 
                    else
                        PushTypeAvailableElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.VerificationResult.push_type_available>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.VerificationResult.push_type_available>(elem)));
                    OnPropertyChanged("PushTypeAvailable");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PrimarySourceComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(Organization != null) dest.Organization = (Hl7.Fhir.Model.ResourceReference)Organization.DeepCopy();
                    if(Type != null) dest.Type = new List<Hl7.Fhir.Model.CodeableConcept>(Type.DeepCopy());
                    if(ValidationProcess != null) dest.ValidationProcess = new List<Hl7.Fhir.Model.CodeableConcept>(ValidationProcess.DeepCopy());
                    if(ValidationStatusElement != null) dest.ValidationStatusElement = (Code<Hl7.Fhir.Model.VerificationResult.validation_status>)ValidationStatusElement.DeepCopy();
                    if(ValidationDateElement != null) dest.ValidationDateElement = (Hl7.Fhir.Model.FhirDateTime)ValidationDateElement.DeepCopy();
                    if(CanPushUpdatesElement != null) dest.CanPushUpdatesElement = (Code<Hl7.Fhir.Model.VerificationResult.can_push_updates>)CanPushUpdatesElement.DeepCopy();
                    if(PushTypeAvailableElement != null) dest.PushTypeAvailableElement = new List<Code<Hl7.Fhir.Model.VerificationResult.push_type_available>>(PushTypeAvailableElement.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PrimarySourceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PrimarySourceComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(Organization, otherT.Organization)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(ValidationProcess, otherT.ValidationProcess)) return false;
                if( !DeepComparable.Matches(ValidationStatusElement, otherT.ValidationStatusElement)) return false;
                if( !DeepComparable.Matches(ValidationDateElement, otherT.ValidationDateElement)) return false;
                if( !DeepComparable.Matches(CanPushUpdatesElement, otherT.CanPushUpdatesElement)) return false;
                if( !DeepComparable.Matches(PushTypeAvailableElement, otherT.PushTypeAvailableElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PrimarySourceComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(Organization, otherT.Organization)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(ValidationProcess, otherT.ValidationProcess)) return false;
                if( !DeepComparable.IsExactly(ValidationStatusElement, otherT.ValidationStatusElement)) return false;
                if( !DeepComparable.IsExactly(ValidationDateElement, otherT.ValidationDateElement)) return false;
                if( !DeepComparable.IsExactly(CanPushUpdatesElement, otherT.CanPushUpdatesElement)) return false;
                if( !DeepComparable.IsExactly(PushTypeAvailableElement, otherT.PushTypeAvailableElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Identifier != null) yield return Identifier;
                    if (Organization != null) yield return Organization;
                    foreach (var elem in Type) { if (elem != null) yield return elem; }
                    foreach (var elem in ValidationProcess) { if (elem != null) yield return elem; }
                    if (ValidationStatusElement != null) yield return ValidationStatusElement;
                    if (ValidationDateElement != null) yield return ValidationDateElement;
                    if (CanPushUpdatesElement != null) yield return CanPushUpdatesElement;
                    foreach (var elem in PushTypeAvailableElement) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Identifier != null) yield return new ElementValue("identifier", false, Identifier);
                    if (Organization != null) yield return new ElementValue("organization", false, Organization);
                    foreach (var elem in Type) { if (elem != null) yield return new ElementValue("type", true, elem); }
                    foreach (var elem in ValidationProcess) { if (elem != null) yield return new ElementValue("validationProcess", true, elem); }
                    if (ValidationStatusElement != null) yield return new ElementValue("validationStatus", false, ValidationStatusElement);
                    if (ValidationDateElement != null) yield return new ElementValue("validationDate", false, ValidationDateElement);
                    if (CanPushUpdatesElement != null) yield return new ElementValue("canPushUpdates", false, CanPushUpdatesElement);
                    foreach (var elem in PushTypeAvailableElement) { if (elem != null) yield return new ElementValue("pushTypeAvailable", true, elem); }
                }
            }

            
        }
        
        
        [FhirType("AttestationComponent")]
        [DataContract]
        public partial class AttestationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "AttestationComponent"; } }
            
            /// <summary>
            /// The individual attesting to information
            /// </summary>
            [FhirElement("source", InSummary=true, Order=40)]
            [CLSCompliant(false)]
			[References("Practitioner")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Source
            {
                get { return _Source; }
                set { _Source = value; OnPropertyChanged("Source"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Source;
            
            /// <summary>
            /// The organization attesting to information
            /// </summary>
            [FhirElement("organization", InSummary=true, Order=50)]
            [CLSCompliant(false)]
			[References("Organization")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Organization
            {
                get { return _Organization; }
                set { _Organization = value; OnPropertyChanged("Organization"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Organization;
            
            /// <summary>
            /// Who is providing the attested information (owner; authorized representative; authorized intermediary; non-authorized source)
            /// </summary>
            [FhirElement("method", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Method
            {
                get { return _Method; }
                set { _Method = value; OnPropertyChanged("Method"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Method;
            
            /// <summary>
            /// The date the information was attested to
            /// </summary>
            [FhirElement("date", InSummary=true, Order=70)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Date DateElement
            {
                get { return _DateElement; }
                set { _DateElement = value; OnPropertyChanged("DateElement"); }
            }
            
            private Hl7.Fhir.Model.Date _DateElement;
            
            /// <summary>
            /// The date the information was attested to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Date
            {
                get { return DateElement != null ? DateElement.Value : null; }
                set
                {
                    if (value == null)
                        DateElement = null; 
                    else
                        DateElement = new Hl7.Fhir.Model.Date(value);
                    OnPropertyChanged("Date");
                }
            }
            
            /// <summary>
            /// A digital identity certificate associated with the attestation source
            /// </summary>
            [FhirElement("sourceIdentityCertificate", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SourceIdentityCertificateElement
            {
                get { return _SourceIdentityCertificateElement; }
                set { _SourceIdentityCertificateElement = value; OnPropertyChanged("SourceIdentityCertificateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _SourceIdentityCertificateElement;
            
            /// <summary>
            /// A digital identity certificate associated with the attestation source
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string SourceIdentityCertificate
            {
                get { return SourceIdentityCertificateElement != null ? SourceIdentityCertificateElement.Value : null; }
                set
                {
                    if (value == null)
                        SourceIdentityCertificateElement = null; 
                    else
                        SourceIdentityCertificateElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("SourceIdentityCertificate");
                }
            }
            
            /// <summary>
            /// A digital identity certificate associated with the proxy entity submitting attested information on behalf of the attestation source
            /// </summary>
            [FhirElement("proxyIdentityCertificate", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ProxyIdentityCertificateElement
            {
                get { return _ProxyIdentityCertificateElement; }
                set { _ProxyIdentityCertificateElement = value; OnPropertyChanged("ProxyIdentityCertificateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ProxyIdentityCertificateElement;
            
            /// <summary>
            /// A digital identity certificate associated with the proxy entity submitting attested information on behalf of the attestation source
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ProxyIdentityCertificate
            {
                get { return ProxyIdentityCertificateElement != null ? ProxyIdentityCertificateElement.Value : null; }
                set
                {
                    if (value == null)
                        ProxyIdentityCertificateElement = null; 
                    else
                        ProxyIdentityCertificateElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ProxyIdentityCertificate");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AttestationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Source != null) dest.Source = (Hl7.Fhir.Model.ResourceReference)Source.DeepCopy();
                    if(Organization != null) dest.Organization = (Hl7.Fhir.Model.ResourceReference)Organization.DeepCopy();
                    if(Method != null) dest.Method = (Hl7.Fhir.Model.CodeableConcept)Method.DeepCopy();
                    if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.Date)DateElement.DeepCopy();
                    if(SourceIdentityCertificateElement != null) dest.SourceIdentityCertificateElement = (Hl7.Fhir.Model.FhirString)SourceIdentityCertificateElement.DeepCopy();
                    if(ProxyIdentityCertificateElement != null) dest.ProxyIdentityCertificateElement = (Hl7.Fhir.Model.FhirString)ProxyIdentityCertificateElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new AttestationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AttestationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Source, otherT.Source)) return false;
                if( !DeepComparable.Matches(Organization, otherT.Organization)) return false;
                if( !DeepComparable.Matches(Method, otherT.Method)) return false;
                if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.Matches(SourceIdentityCertificateElement, otherT.SourceIdentityCertificateElement)) return false;
                if( !DeepComparable.Matches(ProxyIdentityCertificateElement, otherT.ProxyIdentityCertificateElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AttestationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
                if( !DeepComparable.IsExactly(Organization, otherT.Organization)) return false;
                if( !DeepComparable.IsExactly(Method, otherT.Method)) return false;
                if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.IsExactly(SourceIdentityCertificateElement, otherT.SourceIdentityCertificateElement)) return false;
                if( !DeepComparable.IsExactly(ProxyIdentityCertificateElement, otherT.ProxyIdentityCertificateElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Source != null) yield return Source;
                    if (Organization != null) yield return Organization;
                    if (Method != null) yield return Method;
                    if (DateElement != null) yield return DateElement;
                    if (SourceIdentityCertificateElement != null) yield return SourceIdentityCertificateElement;
                    if (ProxyIdentityCertificateElement != null) yield return ProxyIdentityCertificateElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Source != null) yield return new ElementValue("source", false, Source);
                    if (Organization != null) yield return new ElementValue("organization", false, Organization);
                    if (Method != null) yield return new ElementValue("method", false, Method);
                    if (DateElement != null) yield return new ElementValue("date", false, DateElement);
                    if (SourceIdentityCertificateElement != null) yield return new ElementValue("sourceIdentityCertificate", false, SourceIdentityCertificateElement);
                    if (ProxyIdentityCertificateElement != null) yield return new ElementValue("proxyIdentityCertificate", false, ProxyIdentityCertificateElement);
                }
            }

            
        }
        
        
        [FhirType("ValidatorComponent")]
        [DataContract]
        public partial class ValidatorComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ValidatorComponent"; } }
            
            /// <summary>
            /// URI of the validator
            /// </summary>
            [FhirElement("identifier", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// Reference to the organization validating information
            /// </summary>
            [FhirElement("organization", Order=50)]
            [CLSCompliant(false)]
			[References("Organization")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Organization
            {
                get { return _Organization; }
                set { _Organization = value; OnPropertyChanged("Organization"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Organization;
            
            /// <summary>
            /// A digital identity certificate associated with the validator
            /// </summary>
            [FhirElement("identityCertificate", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString IdentityCertificateElement
            {
                get { return _IdentityCertificateElement; }
                set { _IdentityCertificateElement = value; OnPropertyChanged("IdentityCertificateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _IdentityCertificateElement;
            
            /// <summary>
            /// A digital identity certificate associated with the validator
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string IdentityCertificate
            {
                get { return IdentityCertificateElement != null ? IdentityCertificateElement.Value : null; }
                set
                {
                    if (value == null)
                        IdentityCertificateElement = null; 
                    else
                        IdentityCertificateElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("IdentityCertificate");
                }
            }
            
            /// <summary>
            /// Date on which the validator last validated the information
            /// </summary>
            [FhirElement("dateValidated", Order=70)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Date DateValidatedElement
            {
                get { return _DateValidatedElement; }
                set { _DateValidatedElement = value; OnPropertyChanged("DateValidatedElement"); }
            }
            
            private Hl7.Fhir.Model.Date _DateValidatedElement;
            
            /// <summary>
            /// Date on which the validator last validated the information
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string DateValidated
            {
                get { return DateValidatedElement != null ? DateValidatedElement.Value : null; }
                set
                {
                    if (value == null)
                        DateValidatedElement = null; 
                    else
                        DateValidatedElement = new Hl7.Fhir.Model.Date(value);
                    OnPropertyChanged("DateValidated");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ValidatorComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(Organization != null) dest.Organization = (Hl7.Fhir.Model.ResourceReference)Organization.DeepCopy();
                    if(IdentityCertificateElement != null) dest.IdentityCertificateElement = (Hl7.Fhir.Model.FhirString)IdentityCertificateElement.DeepCopy();
                    if(DateValidatedElement != null) dest.DateValidatedElement = (Hl7.Fhir.Model.Date)DateValidatedElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ValidatorComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ValidatorComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(Organization, otherT.Organization)) return false;
                if( !DeepComparable.Matches(IdentityCertificateElement, otherT.IdentityCertificateElement)) return false;
                if( !DeepComparable.Matches(DateValidatedElement, otherT.DateValidatedElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ValidatorComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(Organization, otherT.Organization)) return false;
                if( !DeepComparable.IsExactly(IdentityCertificateElement, otherT.IdentityCertificateElement)) return false;
                if( !DeepComparable.IsExactly(DateValidatedElement, otherT.DateValidatedElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Identifier != null) yield return Identifier;
                    if (Organization != null) yield return Organization;
                    if (IdentityCertificateElement != null) yield return IdentityCertificateElement;
                    if (DateValidatedElement != null) yield return DateValidatedElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Identifier != null) yield return new ElementValue("identifier", false, Identifier);
                    if (Organization != null) yield return new ElementValue("organization", false, Organization);
                    if (IdentityCertificateElement != null) yield return new ElementValue("identityCertificate", false, IdentityCertificateElement);
                    if (DateValidatedElement != null) yield return new ElementValue("dateValidated", false, DateValidatedElement);
                }
            }

            
        }
        
        
        /// <summary>
        /// A resource that was validated
        /// </summary>
        [FhirElement("target", InSummary=true, Order=90)]
        [CLSCompliant(false)]
		[References("Any")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Target
        {
            get { if(_Target==null) _Target = new List<Hl7.Fhir.Model.ResourceReference>(); return _Target; }
            set { _Target = value; OnPropertyChanged("Target"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Target;
        
        /// <summary>
        /// The fhirpath location(s) within the resource that was validated
        /// </summary>
        [FhirElement("targetLocation", InSummary=true, Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> TargetLocationElement
        {
            get { if(_TargetLocationElement==null) _TargetLocationElement = new List<Hl7.Fhir.Model.FhirString>(); return _TargetLocationElement; }
            set { _TargetLocationElement = value; OnPropertyChanged("TargetLocationElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _TargetLocationElement;
        
        /// <summary>
        /// The fhirpath location(s) within the resource that was validated
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> TargetLocation
        {
            get { return TargetLocationElement != null ? TargetLocationElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  TargetLocationElement = null; 
                else
                  TargetLocationElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("TargetLocation");
            }
        }
        
        /// <summary>
        /// none | initial | periodic
        /// </summary>
        [FhirElement("need", InSummary=true, Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.VerificationResult.need> NeedElement
        {
            get { return _NeedElement; }
            set { _NeedElement = value; OnPropertyChanged("NeedElement"); }
        }
        
        private Code<Hl7.Fhir.Model.VerificationResult.need> _NeedElement;
        
        /// <summary>
        /// none | initial | periodic
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.VerificationResult.need? Need
        {
            get { return NeedElement != null ? NeedElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  NeedElement = null; 
                else
                  NeedElement = new Code<Hl7.Fhir.Model.VerificationResult.need>(value);
                OnPropertyChanged("Need");
            }
        }
        
        /// <summary>
        /// attested | validated | in-process | req-revalid | val-fail | reval-fail
        /// </summary>
        [FhirElement("status", InSummary=true, Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.VerificationResult.status> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.VerificationResult.status> _StatusElement;
        
        /// <summary>
        /// attested | validated | in-process | req-revalid | val-fail | reval-fail
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.VerificationResult.status? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.VerificationResult.status>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// When the validation status was updated
        /// </summary>
        [FhirElement("statusDate", InSummary=true, Order=130)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime StatusDateElement
        {
            get { return _StatusDateElement; }
            set { _StatusDateElement = value; OnPropertyChanged("StatusDateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _StatusDateElement;
        
        /// <summary>
        /// When the validation status was updated
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
        /// nothing | primary | multiple
        /// </summary>
        [FhirElement("validationType", InSummary=true, Order=140)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.VerificationResult.validation_type> ValidationTypeElement
        {
            get { return _ValidationTypeElement; }
            set { _ValidationTypeElement = value; OnPropertyChanged("ValidationTypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.VerificationResult.validation_type> _ValidationTypeElement;
        
        /// <summary>
        /// nothing | primary | multiple
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.VerificationResult.validation_type? ValidationType
        {
            get { return ValidationTypeElement != null ? ValidationTypeElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  ValidationTypeElement = null; 
                else
                  ValidationTypeElement = new Code<Hl7.Fhir.Model.VerificationResult.validation_type>(value);
                OnPropertyChanged("ValidationType");
            }
        }
        
        /// <summary>
        /// The primary process by which the target is validated (edit check; value set; primary source; multiple sources; standalone; in context)
        /// </summary>
        [FhirElement("validationProcess", InSummary=true, Order=150)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ValidationProcess
        {
            get { if(_ValidationProcess==null) _ValidationProcess = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ValidationProcess; }
            set { _ValidationProcess = value; OnPropertyChanged("ValidationProcess"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ValidationProcess;
        
        /// <summary>
        /// Frequency of revalidation
        /// </summary>
        [FhirElement("frequency", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.Timing Frequency
        {
            get { return _Frequency; }
            set { _Frequency = value; OnPropertyChanged("Frequency"); }
        }
        
        private Hl7.Fhir.Model.Timing _Frequency;
        
        /// <summary>
        /// The date/time validation was last completed (incl. failed validations)
        /// </summary>
        [FhirElement("lastPerformed", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime LastPerformedElement
        {
            get { return _LastPerformedElement; }
            set { _LastPerformedElement = value; OnPropertyChanged("LastPerformedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _LastPerformedElement;
        
        /// <summary>
        /// The date/time validation was last completed (incl. failed validations)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string LastPerformed
        {
            get { return LastPerformedElement != null ? LastPerformedElement.Value : null; }
            set
            {
                if (value == null)
                  LastPerformedElement = null; 
                else
                  LastPerformedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("LastPerformed");
            }
        }
        
        /// <summary>
        /// The date when target is next validated, if appropriate
        /// </summary>
        [FhirElement("nextScheduled", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.Date NextScheduledElement
        {
            get { return _NextScheduledElement; }
            set { _NextScheduledElement = value; OnPropertyChanged("NextScheduledElement"); }
        }
        
        private Hl7.Fhir.Model.Date _NextScheduledElement;
        
        /// <summary>
        /// The date when target is next validated, if appropriate
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string NextScheduled
        {
            get { return NextScheduledElement != null ? NextScheduledElement.Value : null; }
            set
            {
                if (value == null)
                  NextScheduledElement = null; 
                else
                  NextScheduledElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("NextScheduled");
            }
        }
        
        /// <summary>
        /// fatal | warn | rec-only | none
        /// </summary>
        [FhirElement("failureAction", InSummary=true, Order=190)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.VerificationResult.failure_action> FailureActionElement
        {
            get { return _FailureActionElement; }
            set { _FailureActionElement = value; OnPropertyChanged("FailureActionElement"); }
        }
        
        private Code<Hl7.Fhir.Model.VerificationResult.failure_action> _FailureActionElement;
        
        /// <summary>
        /// fatal | warn | rec-only | none
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.VerificationResult.failure_action? FailureAction
        {
            get { return FailureActionElement != null ? FailureActionElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  FailureActionElement = null; 
                else
                  FailureActionElement = new Code<Hl7.Fhir.Model.VerificationResult.failure_action>(value);
                OnPropertyChanged("FailureAction");
            }
        }
        
        /// <summary>
        /// Information about the primary source(s) involved in validation
        /// </summary>
        [FhirElement("primarySource", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.VerificationResult.PrimarySourceComponent> PrimarySource
        {
            get { if(_PrimarySource==null) _PrimarySource = new List<Hl7.Fhir.Model.VerificationResult.PrimarySourceComponent>(); return _PrimarySource; }
            set { _PrimarySource = value; OnPropertyChanged("PrimarySource"); }
        }
        
        private List<Hl7.Fhir.Model.VerificationResult.PrimarySourceComponent> _PrimarySource;
        
        /// <summary>
        /// Information about the entity attesting to information
        /// </summary>
        [FhirElement("attestation", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.VerificationResult.AttestationComponent Attestation
        {
            get { return _Attestation; }
            set { _Attestation = value; OnPropertyChanged("Attestation"); }
        }
        
        private Hl7.Fhir.Model.VerificationResult.AttestationComponent _Attestation;
        
        /// <summary>
        /// Information about the entity validating information
        /// </summary>
        [FhirElement("validator", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.VerificationResult.ValidatorComponent> Validator
        {
            get { if(_Validator==null) _Validator = new List<Hl7.Fhir.Model.VerificationResult.ValidatorComponent>(); return _Validator; }
            set { _Validator = value; OnPropertyChanged("Validator"); }
        }
        
        private List<Hl7.Fhir.Model.VerificationResult.ValidatorComponent> _Validator;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as VerificationResult;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Target != null) dest.Target = new List<Hl7.Fhir.Model.ResourceReference>(Target.DeepCopy());
                if(TargetLocationElement != null) dest.TargetLocationElement = new List<Hl7.Fhir.Model.FhirString>(TargetLocationElement.DeepCopy());
                if(NeedElement != null) dest.NeedElement = (Code<Hl7.Fhir.Model.VerificationResult.need>)NeedElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.VerificationResult.status>)StatusElement.DeepCopy();
                if(StatusDateElement != null) dest.StatusDateElement = (Hl7.Fhir.Model.FhirDateTime)StatusDateElement.DeepCopy();
                if(ValidationTypeElement != null) dest.ValidationTypeElement = (Code<Hl7.Fhir.Model.VerificationResult.validation_type>)ValidationTypeElement.DeepCopy();
                if(ValidationProcess != null) dest.ValidationProcess = new List<Hl7.Fhir.Model.CodeableConcept>(ValidationProcess.DeepCopy());
                if(Frequency != null) dest.Frequency = (Hl7.Fhir.Model.Timing)Frequency.DeepCopy();
                if(LastPerformedElement != null) dest.LastPerformedElement = (Hl7.Fhir.Model.FhirDateTime)LastPerformedElement.DeepCopy();
                if(NextScheduledElement != null) dest.NextScheduledElement = (Hl7.Fhir.Model.Date)NextScheduledElement.DeepCopy();
                if(FailureActionElement != null) dest.FailureActionElement = (Code<Hl7.Fhir.Model.VerificationResult.failure_action>)FailureActionElement.DeepCopy();
                if(PrimarySource != null) dest.PrimarySource = new List<Hl7.Fhir.Model.VerificationResult.PrimarySourceComponent>(PrimarySource.DeepCopy());
                if(Attestation != null) dest.Attestation = (Hl7.Fhir.Model.VerificationResult.AttestationComponent)Attestation.DeepCopy();
                if(Validator != null) dest.Validator = new List<Hl7.Fhir.Model.VerificationResult.ValidatorComponent>(Validator.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new VerificationResult());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as VerificationResult;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Target, otherT.Target)) return false;
            if( !DeepComparable.Matches(TargetLocationElement, otherT.TargetLocationElement)) return false;
            if( !DeepComparable.Matches(NeedElement, otherT.NeedElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(StatusDateElement, otherT.StatusDateElement)) return false;
            if( !DeepComparable.Matches(ValidationTypeElement, otherT.ValidationTypeElement)) return false;
            if( !DeepComparable.Matches(ValidationProcess, otherT.ValidationProcess)) return false;
            if( !DeepComparable.Matches(Frequency, otherT.Frequency)) return false;
            if( !DeepComparable.Matches(LastPerformedElement, otherT.LastPerformedElement)) return false;
            if( !DeepComparable.Matches(NextScheduledElement, otherT.NextScheduledElement)) return false;
            if( !DeepComparable.Matches(FailureActionElement, otherT.FailureActionElement)) return false;
            if( !DeepComparable.Matches(PrimarySource, otherT.PrimarySource)) return false;
            if( !DeepComparable.Matches(Attestation, otherT.Attestation)) return false;
            if( !DeepComparable.Matches(Validator, otherT.Validator)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as VerificationResult;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
            if( !DeepComparable.IsExactly(TargetLocationElement, otherT.TargetLocationElement)) return false;
            if( !DeepComparable.IsExactly(NeedElement, otherT.NeedElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(StatusDateElement, otherT.StatusDateElement)) return false;
            if( !DeepComparable.IsExactly(ValidationTypeElement, otherT.ValidationTypeElement)) return false;
            if( !DeepComparable.IsExactly(ValidationProcess, otherT.ValidationProcess)) return false;
            if( !DeepComparable.IsExactly(Frequency, otherT.Frequency)) return false;
            if( !DeepComparable.IsExactly(LastPerformedElement, otherT.LastPerformedElement)) return false;
            if( !DeepComparable.IsExactly(NextScheduledElement, otherT.NextScheduledElement)) return false;
            if( !DeepComparable.IsExactly(FailureActionElement, otherT.FailureActionElement)) return false;
            if( !DeepComparable.IsExactly(PrimarySource, otherT.PrimarySource)) return false;
            if( !DeepComparable.IsExactly(Attestation, otherT.Attestation)) return false;
            if( !DeepComparable.IsExactly(Validator, otherT.Validator)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Target) { if (elem != null) yield return elem; }
				foreach (var elem in TargetLocationElement) { if (elem != null) yield return elem; }
				if (NeedElement != null) yield return NeedElement;
				if (StatusElement != null) yield return StatusElement;
				if (StatusDateElement != null) yield return StatusDateElement;
				if (ValidationTypeElement != null) yield return ValidationTypeElement;
				foreach (var elem in ValidationProcess) { if (elem != null) yield return elem; }
				if (Frequency != null) yield return Frequency;
				if (LastPerformedElement != null) yield return LastPerformedElement;
				if (NextScheduledElement != null) yield return NextScheduledElement;
				if (FailureActionElement != null) yield return FailureActionElement;
				foreach (var elem in PrimarySource) { if (elem != null) yield return elem; }
				if (Attestation != null) yield return Attestation;
				foreach (var elem in Validator) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Target) { if (elem != null) yield return new ElementValue("target", true, elem); }
                foreach (var elem in TargetLocationElement) { if (elem != null) yield return new ElementValue("targetLocation", true, elem); }
                if (NeedElement != null) yield return new ElementValue("need", false, NeedElement);
                if (StatusElement != null) yield return new ElementValue("status", false, StatusElement);
                if (StatusDateElement != null) yield return new ElementValue("statusDate", false, StatusDateElement);
                if (ValidationTypeElement != null) yield return new ElementValue("validationType", false, ValidationTypeElement);
                foreach (var elem in ValidationProcess) { if (elem != null) yield return new ElementValue("validationProcess", true, elem); }
                if (Frequency != null) yield return new ElementValue("frequency", false, Frequency);
                if (LastPerformedElement != null) yield return new ElementValue("lastPerformed", false, LastPerformedElement);
                if (NextScheduledElement != null) yield return new ElementValue("nextScheduled", false, NextScheduledElement);
                if (FailureActionElement != null) yield return new ElementValue("failureAction", false, FailureActionElement);
                foreach (var elem in PrimarySource) { if (elem != null) yield return new ElementValue("primarySource", true, elem); }
                if (Attestation != null) yield return new ElementValue("attestation", false, Attestation);
                foreach (var elem in Validator) { if (elem != null) yield return new ElementValue("validator", true, elem); }
            }
        }

    }
    
}
