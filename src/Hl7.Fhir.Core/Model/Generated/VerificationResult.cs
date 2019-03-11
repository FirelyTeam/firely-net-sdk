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
        /// The validation status of the target
        /// (url: http://hl7.org/fhir/ValueSet/verificationresult-status)
        /// </summary>
        [FhirEnumeration("status")]
        public enum status
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/CodeSystem/status)
            /// </summary>
            [EnumLiteral("attested", "http://hl7.org/fhir/CodeSystem/status"), Description("Attested")]
            Attested,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/CodeSystem/status)
            /// </summary>
            [EnumLiteral("validated", "http://hl7.org/fhir/CodeSystem/status"), Description("Validated")]
            Validated,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/CodeSystem/status)
            /// </summary>
            [EnumLiteral("in-process", "http://hl7.org/fhir/CodeSystem/status"), Description("In process")]
            InProcess,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/CodeSystem/status)
            /// </summary>
            [EnumLiteral("req-revalid", "http://hl7.org/fhir/CodeSystem/status"), Description("Requires revalidation")]
            ReqRevalid,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/CodeSystem/status)
            /// </summary>
            [EnumLiteral("val-fail", "http://hl7.org/fhir/CodeSystem/status"), Description("Validation failed")]
            ValFail,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/CodeSystem/status)
            /// </summary>
            [EnumLiteral("reval-fail", "http://hl7.org/fhir/CodeSystem/status"), Description("Re-Validation failed")]
            RevalFail,
        }

        [FhirType("PrimarySourceComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class PrimarySourceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "PrimarySourceComponent"; } }
            
            /// <summary>
            /// Reference to the primary source
            /// </summary>
            [FhirElement("who", Order=40)]
            [CLSCompliant(false)]
			[References("Organization","Practitioner","PractitionerRole")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Who
            {
                get { return _Who; }
                set { _Who = value; OnPropertyChanged("Who"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Who;
            
            /// <summary>
            /// Type of primary source (License Board; Primary Education; Continuing Education; Postal Service; Relationship owner; Registration Authority; legal source; issuing source; authoritative source)
            /// </summary>
            [FhirElement("type", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Type
            {
                get { if(_Type==null) _Type = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Type;
            
            /// <summary>
            /// Method for exchanging information with the primary source
            /// </summary>
            [FhirElement("communicationMethod", InSummary=true, Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> CommunicationMethod
            {
                get { if(_CommunicationMethod==null) _CommunicationMethod = new List<Hl7.Fhir.Model.CodeableConcept>(); return _CommunicationMethod; }
                set { _CommunicationMethod = value; OnPropertyChanged("CommunicationMethod"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _CommunicationMethod;
            
            /// <summary>
            /// successful | failed | unknown
            /// </summary>
            [FhirElement("validationStatus", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept ValidationStatus
            {
                get { return _ValidationStatus; }
                set { _ValidationStatus = value; OnPropertyChanged("ValidationStatus"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _ValidationStatus;
            
            /// <summary>
            /// When the target was validated against the primary source
            /// </summary>
            [FhirElement("validationDate", Order=80)]
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
            [FhirElement("canPushUpdates", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept CanPushUpdates
            {
                get { return _CanPushUpdates; }
                set { _CanPushUpdates = value; OnPropertyChanged("CanPushUpdates"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _CanPushUpdates;
            
            /// <summary>
            /// specific | any | source
            /// </summary>
            [FhirElement("pushTypeAvailable", Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> PushTypeAvailable
            {
                get { if(_PushTypeAvailable==null) _PushTypeAvailable = new List<Hl7.Fhir.Model.CodeableConcept>(); return _PushTypeAvailable; }
                set { _PushTypeAvailable = value; OnPropertyChanged("PushTypeAvailable"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _PushTypeAvailable;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PrimarySourceComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Who != null) dest.Who = (Hl7.Fhir.Model.ResourceReference)Who.DeepCopy();
                    if(Type != null) dest.Type = new List<Hl7.Fhir.Model.CodeableConcept>(Type.DeepCopy());
                    if(CommunicationMethod != null) dest.CommunicationMethod = new List<Hl7.Fhir.Model.CodeableConcept>(CommunicationMethod.DeepCopy());
                    if(ValidationStatus != null) dest.ValidationStatus = (Hl7.Fhir.Model.CodeableConcept)ValidationStatus.DeepCopy();
                    if(ValidationDateElement != null) dest.ValidationDateElement = (Hl7.Fhir.Model.FhirDateTime)ValidationDateElement.DeepCopy();
                    if(CanPushUpdates != null) dest.CanPushUpdates = (Hl7.Fhir.Model.CodeableConcept)CanPushUpdates.DeepCopy();
                    if(PushTypeAvailable != null) dest.PushTypeAvailable = new List<Hl7.Fhir.Model.CodeableConcept>(PushTypeAvailable.DeepCopy());
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
                if( !DeepComparable.Matches(Who, otherT.Who)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(CommunicationMethod, otherT.CommunicationMethod)) return false;
                if( !DeepComparable.Matches(ValidationStatus, otherT.ValidationStatus)) return false;
                if( !DeepComparable.Matches(ValidationDateElement, otherT.ValidationDateElement)) return false;
                if( !DeepComparable.Matches(CanPushUpdates, otherT.CanPushUpdates)) return false;
                if( !DeepComparable.Matches(PushTypeAvailable, otherT.PushTypeAvailable)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PrimarySourceComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Who, otherT.Who)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(CommunicationMethod, otherT.CommunicationMethod)) return false;
                if( !DeepComparable.IsExactly(ValidationStatus, otherT.ValidationStatus)) return false;
                if( !DeepComparable.IsExactly(ValidationDateElement, otherT.ValidationDateElement)) return false;
                if( !DeepComparable.IsExactly(CanPushUpdates, otherT.CanPushUpdates)) return false;
                if( !DeepComparable.IsExactly(PushTypeAvailable, otherT.PushTypeAvailable)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Who != null) yield return Who;
                    foreach (var elem in Type) { if (elem != null) yield return elem; }
                    foreach (var elem in CommunicationMethod) { if (elem != null) yield return elem; }
                    if (ValidationStatus != null) yield return ValidationStatus;
                    if (ValidationDateElement != null) yield return ValidationDateElement;
                    if (CanPushUpdates != null) yield return CanPushUpdates;
                    foreach (var elem in PushTypeAvailable) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Who != null) yield return new ElementValue("who", Who);
                    foreach (var elem in Type) { if (elem != null) yield return new ElementValue("type", elem); }
                    foreach (var elem in CommunicationMethod) { if (elem != null) yield return new ElementValue("communicationMethod", elem); }
                    if (ValidationStatus != null) yield return new ElementValue("validationStatus", ValidationStatus);
                    if (ValidationDateElement != null) yield return new ElementValue("validationDate", ValidationDateElement);
                    if (CanPushUpdates != null) yield return new ElementValue("canPushUpdates", CanPushUpdates);
                    foreach (var elem in PushTypeAvailable) { if (elem != null) yield return new ElementValue("pushTypeAvailable", elem); }
                }
            }

            
        }
        
        
        [FhirType("AttestationComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class AttestationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "AttestationComponent"; } }
            
            /// <summary>
            /// The individual or organization attesting to information
            /// </summary>
            [FhirElement("who", InSummary=true, Order=40)]
            [CLSCompliant(false)]
			[References("Practitioner","PractitionerRole","Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Who
            {
                get { return _Who; }
                set { _Who = value; OnPropertyChanged("Who"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Who;
            
            /// <summary>
            /// When the who is asserting on behalf of another (organization or individual)
            /// </summary>
            [FhirElement("onBehalfOf", InSummary=true, Order=50)]
            [CLSCompliant(false)]
			[References("Organization","Practitioner","PractitionerRole")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference OnBehalfOf
            {
                get { return _OnBehalfOf; }
                set { _OnBehalfOf = value; OnPropertyChanged("OnBehalfOf"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _OnBehalfOf;
            
            /// <summary>
            /// The method by which attested information was submitted/retrieved
            /// </summary>
            [FhirElement("communicationMethod", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept CommunicationMethod
            {
                get { return _CommunicationMethod; }
                set { _CommunicationMethod = value; OnPropertyChanged("CommunicationMethod"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _CommunicationMethod;
            
            /// <summary>
            /// The date the information was attested to
            /// </summary>
            [FhirElement("date", InSummary=true, Order=70)]
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
            
            /// <summary>
            /// Proxy signature
            /// </summary>
            [FhirElement("proxySignature", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.Signature ProxySignature
            {
                get { return _ProxySignature; }
                set { _ProxySignature = value; OnPropertyChanged("ProxySignature"); }
            }
            
            private Hl7.Fhir.Model.Signature _ProxySignature;
            
            /// <summary>
            /// Attester signature
            /// </summary>
            [FhirElement("sourceSignature", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.Signature SourceSignature
            {
                get { return _SourceSignature; }
                set { _SourceSignature = value; OnPropertyChanged("SourceSignature"); }
            }
            
            private Hl7.Fhir.Model.Signature _SourceSignature;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AttestationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Who != null) dest.Who = (Hl7.Fhir.Model.ResourceReference)Who.DeepCopy();
                    if(OnBehalfOf != null) dest.OnBehalfOf = (Hl7.Fhir.Model.ResourceReference)OnBehalfOf.DeepCopy();
                    if(CommunicationMethod != null) dest.CommunicationMethod = (Hl7.Fhir.Model.CodeableConcept)CommunicationMethod.DeepCopy();
                    if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.Date)DateElement.DeepCopy();
                    if(SourceIdentityCertificateElement != null) dest.SourceIdentityCertificateElement = (Hl7.Fhir.Model.FhirString)SourceIdentityCertificateElement.DeepCopy();
                    if(ProxyIdentityCertificateElement != null) dest.ProxyIdentityCertificateElement = (Hl7.Fhir.Model.FhirString)ProxyIdentityCertificateElement.DeepCopy();
                    if(ProxySignature != null) dest.ProxySignature = (Hl7.Fhir.Model.Signature)ProxySignature.DeepCopy();
                    if(SourceSignature != null) dest.SourceSignature = (Hl7.Fhir.Model.Signature)SourceSignature.DeepCopy();
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
                if( !DeepComparable.Matches(Who, otherT.Who)) return false;
                if( !DeepComparable.Matches(OnBehalfOf, otherT.OnBehalfOf)) return false;
                if( !DeepComparable.Matches(CommunicationMethod, otherT.CommunicationMethod)) return false;
                if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.Matches(SourceIdentityCertificateElement, otherT.SourceIdentityCertificateElement)) return false;
                if( !DeepComparable.Matches(ProxyIdentityCertificateElement, otherT.ProxyIdentityCertificateElement)) return false;
                if( !DeepComparable.Matches(ProxySignature, otherT.ProxySignature)) return false;
                if( !DeepComparable.Matches(SourceSignature, otherT.SourceSignature)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AttestationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Who, otherT.Who)) return false;
                if( !DeepComparable.IsExactly(OnBehalfOf, otherT.OnBehalfOf)) return false;
                if( !DeepComparable.IsExactly(CommunicationMethod, otherT.CommunicationMethod)) return false;
                if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.IsExactly(SourceIdentityCertificateElement, otherT.SourceIdentityCertificateElement)) return false;
                if( !DeepComparable.IsExactly(ProxyIdentityCertificateElement, otherT.ProxyIdentityCertificateElement)) return false;
                if( !DeepComparable.IsExactly(ProxySignature, otherT.ProxySignature)) return false;
                if( !DeepComparable.IsExactly(SourceSignature, otherT.SourceSignature)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Who != null) yield return Who;
                    if (OnBehalfOf != null) yield return OnBehalfOf;
                    if (CommunicationMethod != null) yield return CommunicationMethod;
                    if (DateElement != null) yield return DateElement;
                    if (SourceIdentityCertificateElement != null) yield return SourceIdentityCertificateElement;
                    if (ProxyIdentityCertificateElement != null) yield return ProxyIdentityCertificateElement;
                    if (ProxySignature != null) yield return ProxySignature;
                    if (SourceSignature != null) yield return SourceSignature;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Who != null) yield return new ElementValue("who", Who);
                    if (OnBehalfOf != null) yield return new ElementValue("onBehalfOf", OnBehalfOf);
                    if (CommunicationMethod != null) yield return new ElementValue("communicationMethod", CommunicationMethod);
                    if (DateElement != null) yield return new ElementValue("date", DateElement);
                    if (SourceIdentityCertificateElement != null) yield return new ElementValue("sourceIdentityCertificate", SourceIdentityCertificateElement);
                    if (ProxyIdentityCertificateElement != null) yield return new ElementValue("proxyIdentityCertificate", ProxyIdentityCertificateElement);
                    if (ProxySignature != null) yield return new ElementValue("proxySignature", ProxySignature);
                    if (SourceSignature != null) yield return new ElementValue("sourceSignature", SourceSignature);
                }
            }

            
        }
        
        
        [FhirType("ValidatorComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ValidatorComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ValidatorComponent"; } }
            
            /// <summary>
            /// Reference to the organization validating information
            /// </summary>
            [FhirElement("organization", Order=40)]
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
            [FhirElement("identityCertificate", Order=50)]
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
            /// Validator signature
            /// </summary>
            [FhirElement("attestationSignature", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Signature AttestationSignature
            {
                get { return _AttestationSignature; }
                set { _AttestationSignature = value; OnPropertyChanged("AttestationSignature"); }
            }
            
            private Hl7.Fhir.Model.Signature _AttestationSignature;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ValidatorComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Organization != null) dest.Organization = (Hl7.Fhir.Model.ResourceReference)Organization.DeepCopy();
                    if(IdentityCertificateElement != null) dest.IdentityCertificateElement = (Hl7.Fhir.Model.FhirString)IdentityCertificateElement.DeepCopy();
                    if(AttestationSignature != null) dest.AttestationSignature = (Hl7.Fhir.Model.Signature)AttestationSignature.DeepCopy();
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
                if( !DeepComparable.Matches(Organization, otherT.Organization)) return false;
                if( !DeepComparable.Matches(IdentityCertificateElement, otherT.IdentityCertificateElement)) return false;
                if( !DeepComparable.Matches(AttestationSignature, otherT.AttestationSignature)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ValidatorComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Organization, otherT.Organization)) return false;
                if( !DeepComparable.IsExactly(IdentityCertificateElement, otherT.IdentityCertificateElement)) return false;
                if( !DeepComparable.IsExactly(AttestationSignature, otherT.AttestationSignature)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Organization != null) yield return Organization;
                    if (IdentityCertificateElement != null) yield return IdentityCertificateElement;
                    if (AttestationSignature != null) yield return AttestationSignature;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Organization != null) yield return new ElementValue("organization", Organization);
                    if (IdentityCertificateElement != null) yield return new ElementValue("identityCertificate", IdentityCertificateElement);
                    if (AttestationSignature != null) yield return new ElementValue("attestationSignature", AttestationSignature);
                }
            }

            
        }
        
        
        /// <summary>
        /// A resource that was validated
        /// </summary>
        [FhirElement("target", InSummary=true, Order=90)]
        [CLSCompliant(false)]
		[References()]
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
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Need
        {
            get { return _Need; }
            set { _Need = value; OnPropertyChanged("Need"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Need;
        
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
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept ValidationType
        {
            get { return _ValidationType; }
            set { _ValidationType = value; OnPropertyChanged("ValidationType"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _ValidationType;
        
        /// <summary>
        /// The primary process by which the target is validated (edit check; value set; primary source; multiple sources; standalone; in context)
        /// </summary>
        [FhirElement("validationProcess", InSummary=true, Order=150)]
        [Cardinality(Min=0,Max=-1)]
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
        /// The date/time validation was last completed (including failed validations)
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
        /// The date/time validation was last completed (including failed validations)
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
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept FailureAction
        {
            get { return _FailureAction; }
            set { _FailureAction = value; OnPropertyChanged("FailureAction"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _FailureAction;
        
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
                if(Need != null) dest.Need = (Hl7.Fhir.Model.CodeableConcept)Need.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.VerificationResult.status>)StatusElement.DeepCopy();
                if(StatusDateElement != null) dest.StatusDateElement = (Hl7.Fhir.Model.FhirDateTime)StatusDateElement.DeepCopy();
                if(ValidationType != null) dest.ValidationType = (Hl7.Fhir.Model.CodeableConcept)ValidationType.DeepCopy();
                if(ValidationProcess != null) dest.ValidationProcess = new List<Hl7.Fhir.Model.CodeableConcept>(ValidationProcess.DeepCopy());
                if(Frequency != null) dest.Frequency = (Hl7.Fhir.Model.Timing)Frequency.DeepCopy();
                if(LastPerformedElement != null) dest.LastPerformedElement = (Hl7.Fhir.Model.FhirDateTime)LastPerformedElement.DeepCopy();
                if(NextScheduledElement != null) dest.NextScheduledElement = (Hl7.Fhir.Model.Date)NextScheduledElement.DeepCopy();
                if(FailureAction != null) dest.FailureAction = (Hl7.Fhir.Model.CodeableConcept)FailureAction.DeepCopy();
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
            if( !DeepComparable.Matches(Need, otherT.Need)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(StatusDateElement, otherT.StatusDateElement)) return false;
            if( !DeepComparable.Matches(ValidationType, otherT.ValidationType)) return false;
            if( !DeepComparable.Matches(ValidationProcess, otherT.ValidationProcess)) return false;
            if( !DeepComparable.Matches(Frequency, otherT.Frequency)) return false;
            if( !DeepComparable.Matches(LastPerformedElement, otherT.LastPerformedElement)) return false;
            if( !DeepComparable.Matches(NextScheduledElement, otherT.NextScheduledElement)) return false;
            if( !DeepComparable.Matches(FailureAction, otherT.FailureAction)) return false;
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
            if( !DeepComparable.IsExactly(Need, otherT.Need)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(StatusDateElement, otherT.StatusDateElement)) return false;
            if( !DeepComparable.IsExactly(ValidationType, otherT.ValidationType)) return false;
            if( !DeepComparable.IsExactly(ValidationProcess, otherT.ValidationProcess)) return false;
            if( !DeepComparable.IsExactly(Frequency, otherT.Frequency)) return false;
            if( !DeepComparable.IsExactly(LastPerformedElement, otherT.LastPerformedElement)) return false;
            if( !DeepComparable.IsExactly(NextScheduledElement, otherT.NextScheduledElement)) return false;
            if( !DeepComparable.IsExactly(FailureAction, otherT.FailureAction)) return false;
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
				if (Need != null) yield return Need;
				if (StatusElement != null) yield return StatusElement;
				if (StatusDateElement != null) yield return StatusDateElement;
				if (ValidationType != null) yield return ValidationType;
				foreach (var elem in ValidationProcess) { if (elem != null) yield return elem; }
				if (Frequency != null) yield return Frequency;
				if (LastPerformedElement != null) yield return LastPerformedElement;
				if (NextScheduledElement != null) yield return NextScheduledElement;
				if (FailureAction != null) yield return FailureAction;
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
                foreach (var elem in Target) { if (elem != null) yield return new ElementValue("target", elem); }
                foreach (var elem in TargetLocationElement) { if (elem != null) yield return new ElementValue("targetLocation", elem); }
                if (Need != null) yield return new ElementValue("need", Need);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (StatusDateElement != null) yield return new ElementValue("statusDate", StatusDateElement);
                if (ValidationType != null) yield return new ElementValue("validationType", ValidationType);
                foreach (var elem in ValidationProcess) { if (elem != null) yield return new ElementValue("validationProcess", elem); }
                if (Frequency != null) yield return new ElementValue("frequency", Frequency);
                if (LastPerformedElement != null) yield return new ElementValue("lastPerformed", LastPerformedElement);
                if (NextScheduledElement != null) yield return new ElementValue("nextScheduled", NextScheduledElement);
                if (FailureAction != null) yield return new ElementValue("failureAction", FailureAction);
                foreach (var elem in PrimarySource) { if (elem != null) yield return new ElementValue("primarySource", elem); }
                if (Attestation != null) yield return new ElementValue("attestation", Attestation);
                foreach (var elem in Validator) { if (elem != null) yield return new ElementValue("validator", elem); }
            }
        }

    }
    
}
