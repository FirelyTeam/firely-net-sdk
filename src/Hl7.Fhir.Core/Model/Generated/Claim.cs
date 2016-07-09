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
// Generated for FHIR v1.5.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Claim, Pre-determination or Pre-authorization
    /// </summary>
    [FhirType("Claim", IsResource=true)]
    [DataContract]
    public partial class Claim : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Claim; } }
        [NotMapped]
        public override string TypeName { get { return "Claim"; } }
        
        /// <summary>
        /// A code specifying the state of the resource instance.
        /// (url: http://hl7.org/fhir/ValueSet/claim-status)
        /// </summary>
        [FhirEnumeration("ClaimStatus")]
        public enum ClaimStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/claim-status)
            /// </summary>
            [EnumLiteral("active"), Description("Active")]
            Active,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/claim-status)
            /// </summary>
            [EnumLiteral("cancelled"), Description("Cancelled")]
            Cancelled,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/claim-status)
            /// </summary>
            [EnumLiteral("draft"), Description("Draft")]
            Draft,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/claim-status)
            /// </summary>
            [EnumLiteral("entered-in-error"), Description("Entered In Error")]
            EnteredInError,
        }

        /// <summary>
        /// Complete, proposed, exploratory, other
        /// (url: http://hl7.org/fhir/ValueSet/claim-use)
        /// </summary>
        [FhirEnumeration("Use")]
        public enum Use
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/claim-use)
            /// </summary>
            [EnumLiteral("complete"), Description("Complete")]
            Complete,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/claim-use)
            /// </summary>
            [EnumLiteral("proposed"), Description("Proposed")]
            Proposed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/claim-use)
            /// </summary>
            [EnumLiteral("exploratory"), Description("Exploratory")]
            Exploratory,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/claim-use)
            /// </summary>
            [EnumLiteral("other"), Description("Other")]
            Other,
        }

        [FhirType("RelatedClaimComponent")]
        [DataContract]
        public partial class RelatedClaimComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "RelatedClaimComponent"; } }
            
            /// <summary>
            /// Reference to the related claim
            /// </summary>
            [FhirElement("claim", Order=40, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Claim
            {
                get { return _Claim; }
                set { _Claim = value; OnPropertyChanged("Claim"); }
            }
            
            private Hl7.Fhir.Model.Element _Claim;
            
            /// <summary>
            /// How the reference claim is related
            /// </summary>
            [FhirElement("relationship", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Relationship
            {
                get { return _Relationship; }
                set { _Relationship = value; OnPropertyChanged("Relationship"); }
            }
            
            private Hl7.Fhir.Model.Coding _Relationship;
            
            /// <summary>
            /// Related file or case reference
            /// </summary>
            [FhirElement("reference", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Reference
            {
                get { return _Reference; }
                set { _Reference = value; OnPropertyChanged("Reference"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Reference;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RelatedClaimComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Claim != null) dest.Claim = (Hl7.Fhir.Model.Element)Claim.DeepCopy();
                    if(Relationship != null) dest.Relationship = (Hl7.Fhir.Model.Coding)Relationship.DeepCopy();
                    if(Reference != null) dest.Reference = (Hl7.Fhir.Model.Identifier)Reference.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new RelatedClaimComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RelatedClaimComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Claim, otherT.Claim)) return false;
                if( !DeepComparable.Matches(Relationship, otherT.Relationship)) return false;
                if( !DeepComparable.Matches(Reference, otherT.Reference)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RelatedClaimComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Claim, otherT.Claim)) return false;
                if( !DeepComparable.IsExactly(Relationship, otherT.Relationship)) return false;
                if( !DeepComparable.IsExactly(Reference, otherT.Reference)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("PayeeComponent")]
        [DataContract]
        public partial class PayeeComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "PayeeComponent"; } }
            
            /// <summary>
            /// Type of party: Subscriber, Provider, other
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.Coding _Type;
            
            /// <summary>
            /// organization | patient | practitioner | relatedperson
            /// </summary>
            [FhirElement("resourceType", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Coding ResourceType
            {
                get { return _ResourceType; }
                set { _ResourceType = value; OnPropertyChanged("ResourceType"); }
            }
            
            private Hl7.Fhir.Model.Coding _ResourceType;
            
            /// <summary>
            /// Party to receive the payable
            /// </summary>
            [FhirElement("party", Order=60, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Party
            {
                get { return _Party; }
                set { _Party = value; OnPropertyChanged("Party"); }
            }
            
            private Hl7.Fhir.Model.Element _Party;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PayeeComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                    if(ResourceType != null) dest.ResourceType = (Hl7.Fhir.Model.Coding)ResourceType.DeepCopy();
                    if(Party != null) dest.Party = (Hl7.Fhir.Model.Element)Party.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PayeeComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PayeeComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(ResourceType, otherT.ResourceType)) return false;
                if( !DeepComparable.Matches(Party, otherT.Party)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PayeeComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(ResourceType, otherT.ResourceType)) return false;
                if( !DeepComparable.IsExactly(Party, otherT.Party)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("SpecialConditionComponent")]
        [DataContract]
        public partial class SpecialConditionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "SpecialConditionComponent"; } }
            
            /// <summary>
            /// Category of information
            /// </summary>
            [FhirElement("category", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.Coding _Category;
            
            /// <summary>
            /// Type of information
            /// </summary>
            [FhirElement("code", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.Coding _Code;
            
            /// <summary>
            /// When it occurred
            /// </summary>
            [FhirElement("timing", Order=60, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.Period))]
            [DataMember]
            public Hl7.Fhir.Model.Element Timing
            {
                get { return _Timing; }
                set { _Timing = value; OnPropertyChanged("Timing"); }
            }
            
            private Hl7.Fhir.Model.Element _Timing;
            
            /// <summary>
            /// Additional Data
            /// </summary>
            [FhirElement("value", Order=70, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirString),typeof(Quantity))]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SpecialConditionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.Coding)Category.DeepCopy();
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.Coding)Code.DeepCopy();
                    if(Timing != null) dest.Timing = (Hl7.Fhir.Model.Element)Timing.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SpecialConditionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SpecialConditionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Timing, otherT.Timing)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SpecialConditionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Timing, otherT.Timing)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("DiagnosisComponent")]
        [DataContract]
        public partial class DiagnosisComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DiagnosisComponent"; } }
            
            /// <summary>
            /// Number to covey order of diagnosis
            /// </summary>
            [FhirElement("sequence", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceElement;
            
            /// <summary>
            /// Number to covey order of diagnosis
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if(value == null)
                      SequenceElement = null; 
                    else
                      SequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// Patient's diagnosis
            /// </summary>
            [FhirElement("diagnosis", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Diagnosis
            {
                get { return _Diagnosis; }
                set { _Diagnosis = value; OnPropertyChanged("Diagnosis"); }
            }
            
            private Hl7.Fhir.Model.Coding _Diagnosis;
            
            /// <summary>
            /// Type of Diagnosis
            /// </summary>
            [FhirElement("type", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Type
            {
                get { if(_Type==null) _Type = new List<Hl7.Fhir.Model.Coding>(); return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _Type;
            
            /// <summary>
            /// Diagnosis Related Group
            /// </summary>
            [FhirElement("drg", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Drg
            {
                get { return _Drg; }
                set { _Drg = value; OnPropertyChanged("Drg"); }
            }
            
            private Hl7.Fhir.Model.Coding _Drg;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DiagnosisComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(Diagnosis != null) dest.Diagnosis = (Hl7.Fhir.Model.Coding)Diagnosis.DeepCopy();
                    if(Type != null) dest.Type = new List<Hl7.Fhir.Model.Coding>(Type.DeepCopy());
                    if(Drg != null) dest.Drg = (Hl7.Fhir.Model.Coding)Drg.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DiagnosisComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DiagnosisComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(Diagnosis, otherT.Diagnosis)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Drg, otherT.Drg)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DiagnosisComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(Diagnosis, otherT.Diagnosis)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Drg, otherT.Drg)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ProcedureComponent")]
        [DataContract]
        public partial class ProcedureComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ProcedureComponent"; } }
            
            /// <summary>
            /// Procedure sequence for reference
            /// </summary>
            [FhirElement("sequence", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceElement;
            
            /// <summary>
            /// Procedure sequence for reference
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if(value == null)
                      SequenceElement = null; 
                    else
                      SequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// When the procedure was performed
            /// </summary>
            [FhirElement("date", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime DateElement
            {
                get { return _DateElement; }
                set { _DateElement = value; OnPropertyChanged("DateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _DateElement;
            
            /// <summary>
            /// When the procedure was performed
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
            /// Patient's list of procedures performed
            /// </summary>
            [FhirElement("procedure", Order=60, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.ResourceReference))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Procedure
            {
                get { return _Procedure; }
                set { _Procedure = value; OnPropertyChanged("Procedure"); }
            }
            
            private Hl7.Fhir.Model.Element _Procedure;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ProcedureComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                    if(Procedure != null) dest.Procedure = (Hl7.Fhir.Model.Element)Procedure.DeepCopy();
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
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.Matches(Procedure, otherT.Procedure)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ProcedureComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.IsExactly(Procedure, otherT.Procedure)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("CoverageComponent")]
        [DataContract]
        public partial class CoverageComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CoverageComponent"; } }
            
            /// <summary>
            /// Service instance identifier
            /// </summary>
            [FhirElement("sequence", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceElement;
            
            /// <summary>
            /// Service instance identifier
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if(value == null)
                      SequenceElement = null; 
                    else
                      SequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// Is the focal Coverage
            /// </summary>
            [FhirElement("focal", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean FocalElement
            {
                get { return _FocalElement; }
                set { _FocalElement = value; OnPropertyChanged("FocalElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _FocalElement;
            
            /// <summary>
            /// Is the focal Coverage
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Focal
            {
                get { return FocalElement != null ? FocalElement.Value : null; }
                set
                {
                    if(value == null)
                      FocalElement = null; 
                    else
                      FocalElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Focal");
                }
            }
            
            /// <summary>
            /// Insurance information
            /// </summary>
            [FhirElement("coverage", Order=60, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.ResourceReference))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Coverage
            {
                get { return _Coverage; }
                set { _Coverage = value; OnPropertyChanged("Coverage"); }
            }
            
            private Hl7.Fhir.Model.Element _Coverage;
            
            /// <summary>
            /// Business agreement
            /// </summary>
            [FhirElement("businessArrangement", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString BusinessArrangementElement
            {
                get { return _BusinessArrangementElement; }
                set { _BusinessArrangementElement = value; OnPropertyChanged("BusinessArrangementElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _BusinessArrangementElement;
            
            /// <summary>
            /// Business agreement
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string BusinessArrangement
            {
                get { return BusinessArrangementElement != null ? BusinessArrangementElement.Value : null; }
                set
                {
                    if(value == null)
                      BusinessArrangementElement = null; 
                    else
                      BusinessArrangementElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("BusinessArrangement");
                }
            }
            
            /// <summary>
            /// Pre-Authorization/Determination Reference
            /// </summary>
            [FhirElement("preAuthRef", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> PreAuthRefElement
            {
                get { if(_PreAuthRefElement==null) _PreAuthRefElement = new List<Hl7.Fhir.Model.FhirString>(); return _PreAuthRefElement; }
                set { _PreAuthRefElement = value; OnPropertyChanged("PreAuthRefElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _PreAuthRefElement;
            
            /// <summary>
            /// Pre-Authorization/Determination Reference
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> PreAuthRef
            {
                get { return PreAuthRefElement != null ? PreAuthRefElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      PreAuthRefElement = null; 
                    else
                      PreAuthRefElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("PreAuthRef");
                }
            }
            
            /// <summary>
            /// Adjudication results
            /// </summary>
            [FhirElement("claimResponse", Order=90)]
            [References("ClaimResponse")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference ClaimResponse
            {
                get { return _ClaimResponse; }
                set { _ClaimResponse = value; OnPropertyChanged("ClaimResponse"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _ClaimResponse;
            
            /// <summary>
            /// Original version
            /// </summary>
            [FhirElement("originalRuleset", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.Coding OriginalRuleset
            {
                get { return _OriginalRuleset; }
                set { _OriginalRuleset = value; OnPropertyChanged("OriginalRuleset"); }
            }
            
            private Hl7.Fhir.Model.Coding _OriginalRuleset;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CoverageComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(FocalElement != null) dest.FocalElement = (Hl7.Fhir.Model.FhirBoolean)FocalElement.DeepCopy();
                    if(Coverage != null) dest.Coverage = (Hl7.Fhir.Model.Element)Coverage.DeepCopy();
                    if(BusinessArrangementElement != null) dest.BusinessArrangementElement = (Hl7.Fhir.Model.FhirString)BusinessArrangementElement.DeepCopy();
                    if(PreAuthRefElement != null) dest.PreAuthRefElement = new List<Hl7.Fhir.Model.FhirString>(PreAuthRefElement.DeepCopy());
                    if(ClaimResponse != null) dest.ClaimResponse = (Hl7.Fhir.Model.ResourceReference)ClaimResponse.DeepCopy();
                    if(OriginalRuleset != null) dest.OriginalRuleset = (Hl7.Fhir.Model.Coding)OriginalRuleset.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CoverageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CoverageComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(FocalElement, otherT.FocalElement)) return false;
                if( !DeepComparable.Matches(Coverage, otherT.Coverage)) return false;
                if( !DeepComparable.Matches(BusinessArrangementElement, otherT.BusinessArrangementElement)) return false;
                if( !DeepComparable.Matches(PreAuthRefElement, otherT.PreAuthRefElement)) return false;
                if( !DeepComparable.Matches(ClaimResponse, otherT.ClaimResponse)) return false;
                if( !DeepComparable.Matches(OriginalRuleset, otherT.OriginalRuleset)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CoverageComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(FocalElement, otherT.FocalElement)) return false;
                if( !DeepComparable.IsExactly(Coverage, otherT.Coverage)) return false;
                if( !DeepComparable.IsExactly(BusinessArrangementElement, otherT.BusinessArrangementElement)) return false;
                if( !DeepComparable.IsExactly(PreAuthRefElement, otherT.PreAuthRefElement)) return false;
                if( !DeepComparable.IsExactly(ClaimResponse, otherT.ClaimResponse)) return false;
                if( !DeepComparable.IsExactly(OriginalRuleset, otherT.OriginalRuleset)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("AccidentComponent")]
        [DataContract]
        public partial class AccidentComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "AccidentComponent"; } }
            
            /// <summary>
            /// When the accident occurred see information codes see information codes
            /// </summary>
            [FhirElement("date", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Date DateElement
            {
                get { return _DateElement; }
                set { _DateElement = value; OnPropertyChanged("DateElement"); }
            }
            
            private Hl7.Fhir.Model.Date _DateElement;
            
            /// <summary>
            /// When the accident occurred see information codes see information codes
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
                      DateElement = new Hl7.Fhir.Model.Date(value);
                    OnPropertyChanged("Date");
                }
            }
            
            /// <summary>
            /// The nature of the accident
            /// </summary>
            [FhirElement("type", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.Coding _Type;
            
            /// <summary>
            /// Accident Place
            /// </summary>
            [FhirElement("location", Order=60, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Location
            {
                get { return _Location; }
                set { _Location = value; OnPropertyChanged("Location"); }
            }
            
            private Hl7.Fhir.Model.Element _Location;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AccidentComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.Date)DateElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                    if(Location != null) dest.Location = (Hl7.Fhir.Model.Element)Location.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new AccidentComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AccidentComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Location, otherT.Location)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AccidentComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ItemComponent")]
        [DataContract]
        public partial class ItemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ItemComponent"; } }
            
            /// <summary>
            /// Service instance
            /// </summary>
            [FhirElement("sequence", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceElement;
            
            /// <summary>
            /// Service instance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if(value == null)
                      SequenceElement = null; 
                    else
                      SequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("careTeam", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Claim.CareTeamComponent> CareTeam
            {
                get { if(_CareTeam==null) _CareTeam = new List<Hl7.Fhir.Model.Claim.CareTeamComponent>(); return _CareTeam; }
                set { _CareTeam = value; OnPropertyChanged("CareTeam"); }
            }
            
            private List<Hl7.Fhir.Model.Claim.CareTeamComponent> _CareTeam;
            
            /// <summary>
            /// Applicable diagnoses
            /// </summary>
            [FhirElement("diagnosisLinkId", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> DiagnosisLinkIdElement
            {
                get { if(_DiagnosisLinkIdElement==null) _DiagnosisLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _DiagnosisLinkIdElement; }
                set { _DiagnosisLinkIdElement = value; OnPropertyChanged("DiagnosisLinkIdElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _DiagnosisLinkIdElement;
            
            /// <summary>
            /// Applicable diagnoses
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> DiagnosisLinkId
            {
                get { return DiagnosisLinkIdElement != null ? DiagnosisLinkIdElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      DiagnosisLinkIdElement = null; 
                    else
                      DiagnosisLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("DiagnosisLinkId");
                }
            }
            
            /// <summary>
            /// Revenue or cost center code
            /// </summary>
            [FhirElement("revenue", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Revenue
            {
                get { return _Revenue; }
                set { _Revenue = value; OnPropertyChanged("Revenue"); }
            }
            
            private Hl7.Fhir.Model.Coding _Revenue;
            
            /// <summary>
            /// Type of service or product
            /// </summary>
            [FhirElement("category", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.Coding _Category;
            
            /// <summary>
            /// Billing Code
            /// </summary>
            [FhirElement("service", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Service
            {
                get { return _Service; }
                set { _Service = value; OnPropertyChanged("Service"); }
            }
            
            private Hl7.Fhir.Model.Coding _Service;
            
            /// <summary>
            /// Service/Product billing modifiers
            /// </summary>
            [FhirElement("modifier", Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Modifier
            {
                get { if(_Modifier==null) _Modifier = new List<Hl7.Fhir.Model.Coding>(); return _Modifier; }
                set { _Modifier = value; OnPropertyChanged("Modifier"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _Modifier;
            
            /// <summary>
            /// Program specific reason for item inclusion
            /// </summary>
            [FhirElement("programCode", Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> ProgramCode
            {
                get { if(_ProgramCode==null) _ProgramCode = new List<Hl7.Fhir.Model.Coding>(); return _ProgramCode; }
                set { _ProgramCode = value; OnPropertyChanged("ProgramCode"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _ProgramCode;
            
            /// <summary>
            /// Date or dates of Service
            /// </summary>
            [FhirElement("serviced", Order=120, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.Period))]
            [DataMember]
            public Hl7.Fhir.Model.Element Serviced
            {
                get { return _Serviced; }
                set { _Serviced = value; OnPropertyChanged("Serviced"); }
            }
            
            private Hl7.Fhir.Model.Element _Serviced;
            
            /// <summary>
            /// Place of service
            /// </summary>
            [FhirElement("location", Order=130, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Location
            {
                get { return _Location; }
                set { _Location = value; OnPropertyChanged("Location"); }
            }
            
            private Hl7.Fhir.Model.Element _Location;
            
            /// <summary>
            /// Count of Products or Services
            /// </summary>
            [FhirElement("quantity", Order=140)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Quantity;
            
            /// <summary>
            /// Fee, charge or cost per point
            /// </summary>
            [FhirElement("unitPrice", Order=150)]
            [DataMember]
            public Money UnitPrice
            {
                get { return _UnitPrice; }
                set { _UnitPrice = value; OnPropertyChanged("UnitPrice"); }
            }
            
            private Money _UnitPrice;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            [FhirElement("factor", Order=160)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal FactorElement
            {
                get { return _FactorElement; }
                set { _FactorElement = value; OnPropertyChanged("FactorElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _FactorElement;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Factor
            {
                get { return FactorElement != null ? FactorElement.Value : null; }
                set
                {
                    if(value == null)
                      FactorElement = null; 
                    else
                      FactorElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Factor");
                }
            }
            
            /// <summary>
            /// Difficulty scaling factor
            /// </summary>
            [FhirElement("points", Order=170)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal PointsElement
            {
                get { return _PointsElement; }
                set { _PointsElement = value; OnPropertyChanged("PointsElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _PointsElement;
            
            /// <summary>
            /// Difficulty scaling factor
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Points
            {
                get { return PointsElement != null ? PointsElement.Value : null; }
                set
                {
                    if(value == null)
                      PointsElement = null; 
                    else
                      PointsElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Points");
                }
            }
            
            /// <summary>
            /// Total item cost
            /// </summary>
            [FhirElement("net", Order=180)]
            [DataMember]
            public Money Net
            {
                get { return _Net; }
                set { _Net = value; OnPropertyChanged("Net"); }
            }
            
            private Money _Net;
            
            /// <summary>
            /// Unique Device Identifier
            /// </summary>
            [FhirElement("udi", Order=190)]
            [References("Device")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Udi
            {
                get { if(_Udi==null) _Udi = new List<Hl7.Fhir.Model.ResourceReference>(); return _Udi; }
                set { _Udi = value; OnPropertyChanged("Udi"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Udi;
            
            /// <summary>
            /// Service Location
            /// </summary>
            [FhirElement("bodySite", Order=200)]
            [DataMember]
            public Hl7.Fhir.Model.Coding BodySite
            {
                get { return _BodySite; }
                set { _BodySite = value; OnPropertyChanged("BodySite"); }
            }
            
            private Hl7.Fhir.Model.Coding _BodySite;
            
            /// <summary>
            /// Service Sub-location
            /// </summary>
            [FhirElement("subSite", Order=210)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> SubSite
            {
                get { if(_SubSite==null) _SubSite = new List<Hl7.Fhir.Model.Coding>(); return _SubSite; }
                set { _SubSite = value; OnPropertyChanged("SubSite"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _SubSite;
            
            /// <summary>
            /// Additional items
            /// </summary>
            [FhirElement("detail", Order=220)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Claim.DetailComponent> Detail
            {
                get { if(_Detail==null) _Detail = new List<Hl7.Fhir.Model.Claim.DetailComponent>(); return _Detail; }
                set { _Detail = value; OnPropertyChanged("Detail"); }
            }
            
            private List<Hl7.Fhir.Model.Claim.DetailComponent> _Detail;
            
            /// <summary>
            /// Prosthetic details
            /// </summary>
            [FhirElement("prosthesis", Order=230)]
            [DataMember]
            public Hl7.Fhir.Model.Claim.ProsthesisComponent Prosthesis
            {
                get { return _Prosthesis; }
                set { _Prosthesis = value; OnPropertyChanged("Prosthesis"); }
            }
            
            private Hl7.Fhir.Model.Claim.ProsthesisComponent _Prosthesis;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ItemComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(CareTeam != null) dest.CareTeam = new List<Hl7.Fhir.Model.Claim.CareTeamComponent>(CareTeam.DeepCopy());
                    if(DiagnosisLinkIdElement != null) dest.DiagnosisLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(DiagnosisLinkIdElement.DeepCopy());
                    if(Revenue != null) dest.Revenue = (Hl7.Fhir.Model.Coding)Revenue.DeepCopy();
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.Coding)Category.DeepCopy();
                    if(Service != null) dest.Service = (Hl7.Fhir.Model.Coding)Service.DeepCopy();
                    if(Modifier != null) dest.Modifier = new List<Hl7.Fhir.Model.Coding>(Modifier.DeepCopy());
                    if(ProgramCode != null) dest.ProgramCode = new List<Hl7.Fhir.Model.Coding>(ProgramCode.DeepCopy());
                    if(Serviced != null) dest.Serviced = (Hl7.Fhir.Model.Element)Serviced.DeepCopy();
                    if(Location != null) dest.Location = (Hl7.Fhir.Model.Element)Location.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(UnitPrice != null) dest.UnitPrice = (Money)UnitPrice.DeepCopy();
                    if(FactorElement != null) dest.FactorElement = (Hl7.Fhir.Model.FhirDecimal)FactorElement.DeepCopy();
                    if(PointsElement != null) dest.PointsElement = (Hl7.Fhir.Model.FhirDecimal)PointsElement.DeepCopy();
                    if(Net != null) dest.Net = (Money)Net.DeepCopy();
                    if(Udi != null) dest.Udi = new List<Hl7.Fhir.Model.ResourceReference>(Udi.DeepCopy());
                    if(BodySite != null) dest.BodySite = (Hl7.Fhir.Model.Coding)BodySite.DeepCopy();
                    if(SubSite != null) dest.SubSite = new List<Hl7.Fhir.Model.Coding>(SubSite.DeepCopy());
                    if(Detail != null) dest.Detail = new List<Hl7.Fhir.Model.Claim.DetailComponent>(Detail.DeepCopy());
                    if(Prosthesis != null) dest.Prosthesis = (Hl7.Fhir.Model.Claim.ProsthesisComponent)Prosthesis.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ItemComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ItemComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(CareTeam, otherT.CareTeam)) return false;
                if( !DeepComparable.Matches(DiagnosisLinkIdElement, otherT.DiagnosisLinkIdElement)) return false;
                if( !DeepComparable.Matches(Revenue, otherT.Revenue)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(Service, otherT.Service)) return false;
                if( !DeepComparable.Matches(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.Matches(ProgramCode, otherT.ProgramCode)) return false;
                if( !DeepComparable.Matches(Serviced, otherT.Serviced)) return false;
                if( !DeepComparable.Matches(Location, otherT.Location)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.Matches(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.Matches(PointsElement, otherT.PointsElement)) return false;
                if( !DeepComparable.Matches(Net, otherT.Net)) return false;
                if( !DeepComparable.Matches(Udi, otherT.Udi)) return false;
                if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
                if( !DeepComparable.Matches(SubSite, otherT.SubSite)) return false;
                if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
                if( !DeepComparable.Matches(Prosthesis, otherT.Prosthesis)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ItemComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(CareTeam, otherT.CareTeam)) return false;
                if( !DeepComparable.IsExactly(DiagnosisLinkIdElement, otherT.DiagnosisLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(Revenue, otherT.Revenue)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(Service, otherT.Service)) return false;
                if( !DeepComparable.IsExactly(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.IsExactly(ProgramCode, otherT.ProgramCode)) return false;
                if( !DeepComparable.IsExactly(Serviced, otherT.Serviced)) return false;
                if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.IsExactly(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.IsExactly(PointsElement, otherT.PointsElement)) return false;
                if( !DeepComparable.IsExactly(Net, otherT.Net)) return false;
                if( !DeepComparable.IsExactly(Udi, otherT.Udi)) return false;
                if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
                if( !DeepComparable.IsExactly(SubSite, otherT.SubSite)) return false;
                if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
                if( !DeepComparable.IsExactly(Prosthesis, otherT.Prosthesis)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("CareTeamComponent")]
        [DataContract]
        public partial class CareTeamComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CareTeamComponent"; } }
            
            /// <summary>
            /// Provider individual or organization
            /// </summary>
            [FhirElement("provider", Order=40, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.ResourceReference))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Provider
            {
                get { return _Provider; }
                set { _Provider = value; OnPropertyChanged("Provider"); }
            }
            
            private Hl7.Fhir.Model.Element _Provider;
            
            /// <summary>
            /// Billing provider
            /// </summary>
            [FhirElement("responsible", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ResponsibleElement
            {
                get { return _ResponsibleElement; }
                set { _ResponsibleElement = value; OnPropertyChanged("ResponsibleElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _ResponsibleElement;
            
            /// <summary>
            /// Billing provider
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Responsible
            {
                get { return ResponsibleElement != null ? ResponsibleElement.Value : null; }
                set
                {
                    if(value == null)
                      ResponsibleElement = null; 
                    else
                      ResponsibleElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Responsible");
                }
            }
            
            /// <summary>
            /// Role on the team
            /// </summary>
            [FhirElement("role", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Role
            {
                get { return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            
            private Hl7.Fhir.Model.Coding _Role;
            
            /// <summary>
            /// Type, classification or Specialization
            /// </summary>
            [FhirElement("qualification", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Qualification
            {
                get { return _Qualification; }
                set { _Qualification = value; OnPropertyChanged("Qualification"); }
            }
            
            private Hl7.Fhir.Model.Coding _Qualification;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CareTeamComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Provider != null) dest.Provider = (Hl7.Fhir.Model.Element)Provider.DeepCopy();
                    if(ResponsibleElement != null) dest.ResponsibleElement = (Hl7.Fhir.Model.FhirBoolean)ResponsibleElement.DeepCopy();
                    if(Role != null) dest.Role = (Hl7.Fhir.Model.Coding)Role.DeepCopy();
                    if(Qualification != null) dest.Qualification = (Hl7.Fhir.Model.Coding)Qualification.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CareTeamComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CareTeamComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Provider, otherT.Provider)) return false;
                if( !DeepComparable.Matches(ResponsibleElement, otherT.ResponsibleElement)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                if( !DeepComparable.Matches(Qualification, otherT.Qualification)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CareTeamComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Provider, otherT.Provider)) return false;
                if( !DeepComparable.IsExactly(ResponsibleElement, otherT.ResponsibleElement)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
                if( !DeepComparable.IsExactly(Qualification, otherT.Qualification)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("DetailComponent")]
        [DataContract]
        public partial class DetailComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DetailComponent"; } }
            
            /// <summary>
            /// Service instance
            /// </summary>
            [FhirElement("sequence", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceElement;
            
            /// <summary>
            /// Service instance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if(value == null)
                      SequenceElement = null; 
                    else
                      SequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// Revenue or cost center code
            /// </summary>
            [FhirElement("revenue", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Revenue
            {
                get { return _Revenue; }
                set { _Revenue = value; OnPropertyChanged("Revenue"); }
            }
            
            private Hl7.Fhir.Model.Coding _Revenue;
            
            /// <summary>
            /// Type of service or product
            /// </summary>
            [FhirElement("category", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.Coding _Category;
            
            /// <summary>
            /// Billing Code
            /// </summary>
            [FhirElement("service", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Service
            {
                get { return _Service; }
                set { _Service = value; OnPropertyChanged("Service"); }
            }
            
            private Hl7.Fhir.Model.Coding _Service;
            
            /// <summary>
            /// Service/Product billing modifiers
            /// </summary>
            [FhirElement("modifier", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Modifier
            {
                get { if(_Modifier==null) _Modifier = new List<Hl7.Fhir.Model.Coding>(); return _Modifier; }
                set { _Modifier = value; OnPropertyChanged("Modifier"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _Modifier;
            
            /// <summary>
            /// Program specific reason for item inclusion
            /// </summary>
            [FhirElement("programCode", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> ProgramCode
            {
                get { if(_ProgramCode==null) _ProgramCode = new List<Hl7.Fhir.Model.Coding>(); return _ProgramCode; }
                set { _ProgramCode = value; OnPropertyChanged("ProgramCode"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _ProgramCode;
            
            /// <summary>
            /// Count of Products or Services
            /// </summary>
            [FhirElement("quantity", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Quantity;
            
            /// <summary>
            /// Fee, charge or cost per point
            /// </summary>
            [FhirElement("unitPrice", Order=110)]
            [DataMember]
            public Money UnitPrice
            {
                get { return _UnitPrice; }
                set { _UnitPrice = value; OnPropertyChanged("UnitPrice"); }
            }
            
            private Money _UnitPrice;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            [FhirElement("factor", Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal FactorElement
            {
                get { return _FactorElement; }
                set { _FactorElement = value; OnPropertyChanged("FactorElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _FactorElement;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Factor
            {
                get { return FactorElement != null ? FactorElement.Value : null; }
                set
                {
                    if(value == null)
                      FactorElement = null; 
                    else
                      FactorElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Factor");
                }
            }
            
            /// <summary>
            /// Difficulty scaling factor
            /// </summary>
            [FhirElement("points", Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal PointsElement
            {
                get { return _PointsElement; }
                set { _PointsElement = value; OnPropertyChanged("PointsElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _PointsElement;
            
            /// <summary>
            /// Difficulty scaling factor
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Points
            {
                get { return PointsElement != null ? PointsElement.Value : null; }
                set
                {
                    if(value == null)
                      PointsElement = null; 
                    else
                      PointsElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Points");
                }
            }
            
            /// <summary>
            /// Total additional item cost
            /// </summary>
            [FhirElement("net", Order=140)]
            [DataMember]
            public Money Net
            {
                get { return _Net; }
                set { _Net = value; OnPropertyChanged("Net"); }
            }
            
            private Money _Net;
            
            /// <summary>
            /// Unique Device Identifier
            /// </summary>
            [FhirElement("udi", Order=150)]
            [References("Device")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Udi
            {
                get { if(_Udi==null) _Udi = new List<Hl7.Fhir.Model.ResourceReference>(); return _Udi; }
                set { _Udi = value; OnPropertyChanged("Udi"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Udi;
            
            /// <summary>
            /// Additional items
            /// </summary>
            [FhirElement("subDetail", Order=160)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Claim.SubDetailComponent> SubDetail
            {
                get { if(_SubDetail==null) _SubDetail = new List<Hl7.Fhir.Model.Claim.SubDetailComponent>(); return _SubDetail; }
                set { _SubDetail = value; OnPropertyChanged("SubDetail"); }
            }
            
            private List<Hl7.Fhir.Model.Claim.SubDetailComponent> _SubDetail;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DetailComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(Revenue != null) dest.Revenue = (Hl7.Fhir.Model.Coding)Revenue.DeepCopy();
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.Coding)Category.DeepCopy();
                    if(Service != null) dest.Service = (Hl7.Fhir.Model.Coding)Service.DeepCopy();
                    if(Modifier != null) dest.Modifier = new List<Hl7.Fhir.Model.Coding>(Modifier.DeepCopy());
                    if(ProgramCode != null) dest.ProgramCode = new List<Hl7.Fhir.Model.Coding>(ProgramCode.DeepCopy());
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(UnitPrice != null) dest.UnitPrice = (Money)UnitPrice.DeepCopy();
                    if(FactorElement != null) dest.FactorElement = (Hl7.Fhir.Model.FhirDecimal)FactorElement.DeepCopy();
                    if(PointsElement != null) dest.PointsElement = (Hl7.Fhir.Model.FhirDecimal)PointsElement.DeepCopy();
                    if(Net != null) dest.Net = (Money)Net.DeepCopy();
                    if(Udi != null) dest.Udi = new List<Hl7.Fhir.Model.ResourceReference>(Udi.DeepCopy());
                    if(SubDetail != null) dest.SubDetail = new List<Hl7.Fhir.Model.Claim.SubDetailComponent>(SubDetail.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DetailComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DetailComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(Revenue, otherT.Revenue)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(Service, otherT.Service)) return false;
                if( !DeepComparable.Matches(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.Matches(ProgramCode, otherT.ProgramCode)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.Matches(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.Matches(PointsElement, otherT.PointsElement)) return false;
                if( !DeepComparable.Matches(Net, otherT.Net)) return false;
                if( !DeepComparable.Matches(Udi, otherT.Udi)) return false;
                if( !DeepComparable.Matches(SubDetail, otherT.SubDetail)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DetailComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(Revenue, otherT.Revenue)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(Service, otherT.Service)) return false;
                if( !DeepComparable.IsExactly(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.IsExactly(ProgramCode, otherT.ProgramCode)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.IsExactly(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.IsExactly(PointsElement, otherT.PointsElement)) return false;
                if( !DeepComparable.IsExactly(Net, otherT.Net)) return false;
                if( !DeepComparable.IsExactly(Udi, otherT.Udi)) return false;
                if( !DeepComparable.IsExactly(SubDetail, otherT.SubDetail)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("SubDetailComponent")]
        [DataContract]
        public partial class SubDetailComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "SubDetailComponent"; } }
            
            /// <summary>
            /// Service instance
            /// </summary>
            [FhirElement("sequence", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceElement;
            
            /// <summary>
            /// Service instance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if(value == null)
                      SequenceElement = null; 
                    else
                      SequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// Revenue or cost center code
            /// </summary>
            [FhirElement("revenue", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Revenue
            {
                get { return _Revenue; }
                set { _Revenue = value; OnPropertyChanged("Revenue"); }
            }
            
            private Hl7.Fhir.Model.Coding _Revenue;
            
            /// <summary>
            /// Type of service or product
            /// </summary>
            [FhirElement("category", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.Coding _Category;
            
            /// <summary>
            /// Billing Code
            /// </summary>
            [FhirElement("service", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Service
            {
                get { return _Service; }
                set { _Service = value; OnPropertyChanged("Service"); }
            }
            
            private Hl7.Fhir.Model.Coding _Service;
            
            /// <summary>
            /// Service/Product billing modifiers
            /// </summary>
            [FhirElement("modifier", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Modifier
            {
                get { if(_Modifier==null) _Modifier = new List<Hl7.Fhir.Model.Coding>(); return _Modifier; }
                set { _Modifier = value; OnPropertyChanged("Modifier"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _Modifier;
            
            /// <summary>
            /// Program specific reason for item inclusion
            /// </summary>
            [FhirElement("programCode", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> ProgramCode
            {
                get { if(_ProgramCode==null) _ProgramCode = new List<Hl7.Fhir.Model.Coding>(); return _ProgramCode; }
                set { _ProgramCode = value; OnPropertyChanged("ProgramCode"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _ProgramCode;
            
            /// <summary>
            /// Count of Products or Services
            /// </summary>
            [FhirElement("quantity", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Quantity;
            
            /// <summary>
            /// Fee, charge or cost per point
            /// </summary>
            [FhirElement("unitPrice", Order=110)]
            [DataMember]
            public Money UnitPrice
            {
                get { return _UnitPrice; }
                set { _UnitPrice = value; OnPropertyChanged("UnitPrice"); }
            }
            
            private Money _UnitPrice;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            [FhirElement("factor", Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal FactorElement
            {
                get { return _FactorElement; }
                set { _FactorElement = value; OnPropertyChanged("FactorElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _FactorElement;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Factor
            {
                get { return FactorElement != null ? FactorElement.Value : null; }
                set
                {
                    if(value == null)
                      FactorElement = null; 
                    else
                      FactorElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Factor");
                }
            }
            
            /// <summary>
            /// Difficulty scaling factor
            /// </summary>
            [FhirElement("points", Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal PointsElement
            {
                get { return _PointsElement; }
                set { _PointsElement = value; OnPropertyChanged("PointsElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _PointsElement;
            
            /// <summary>
            /// Difficulty scaling factor
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Points
            {
                get { return PointsElement != null ? PointsElement.Value : null; }
                set
                {
                    if(value == null)
                      PointsElement = null; 
                    else
                      PointsElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Points");
                }
            }
            
            /// <summary>
            /// Net additional item cost
            /// </summary>
            [FhirElement("net", Order=140)]
            [DataMember]
            public Money Net
            {
                get { return _Net; }
                set { _Net = value; OnPropertyChanged("Net"); }
            }
            
            private Money _Net;
            
            /// <summary>
            /// Unique Device Identifier
            /// </summary>
            [FhirElement("udi", Order=150)]
            [References("Device")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Udi
            {
                get { if(_Udi==null) _Udi = new List<Hl7.Fhir.Model.ResourceReference>(); return _Udi; }
                set { _Udi = value; OnPropertyChanged("Udi"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Udi;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SubDetailComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(Revenue != null) dest.Revenue = (Hl7.Fhir.Model.Coding)Revenue.DeepCopy();
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.Coding)Category.DeepCopy();
                    if(Service != null) dest.Service = (Hl7.Fhir.Model.Coding)Service.DeepCopy();
                    if(Modifier != null) dest.Modifier = new List<Hl7.Fhir.Model.Coding>(Modifier.DeepCopy());
                    if(ProgramCode != null) dest.ProgramCode = new List<Hl7.Fhir.Model.Coding>(ProgramCode.DeepCopy());
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(UnitPrice != null) dest.UnitPrice = (Money)UnitPrice.DeepCopy();
                    if(FactorElement != null) dest.FactorElement = (Hl7.Fhir.Model.FhirDecimal)FactorElement.DeepCopy();
                    if(PointsElement != null) dest.PointsElement = (Hl7.Fhir.Model.FhirDecimal)PointsElement.DeepCopy();
                    if(Net != null) dest.Net = (Money)Net.DeepCopy();
                    if(Udi != null) dest.Udi = new List<Hl7.Fhir.Model.ResourceReference>(Udi.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SubDetailComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SubDetailComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(Revenue, otherT.Revenue)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(Service, otherT.Service)) return false;
                if( !DeepComparable.Matches(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.Matches(ProgramCode, otherT.ProgramCode)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.Matches(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.Matches(PointsElement, otherT.PointsElement)) return false;
                if( !DeepComparable.Matches(Net, otherT.Net)) return false;
                if( !DeepComparable.Matches(Udi, otherT.Udi)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SubDetailComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(Revenue, otherT.Revenue)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(Service, otherT.Service)) return false;
                if( !DeepComparable.IsExactly(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.IsExactly(ProgramCode, otherT.ProgramCode)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.IsExactly(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.IsExactly(PointsElement, otherT.PointsElement)) return false;
                if( !DeepComparable.IsExactly(Net, otherT.Net)) return false;
                if( !DeepComparable.IsExactly(Udi, otherT.Udi)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ProsthesisComponent")]
        [DataContract]
        public partial class ProsthesisComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ProsthesisComponent"; } }
            
            /// <summary>
            /// Is this the initial service
            /// </summary>
            [FhirElement("initial", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean InitialElement
            {
                get { return _InitialElement; }
                set { _InitialElement = value; OnPropertyChanged("InitialElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _InitialElement;
            
            /// <summary>
            /// Is this the initial service
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Initial
            {
                get { return InitialElement != null ? InitialElement.Value : null; }
                set
                {
                    if(value == null)
                      InitialElement = null; 
                    else
                      InitialElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Initial");
                }
            }
            
            /// <summary>
            /// Initial service Date
            /// </summary>
            [FhirElement("priorDate", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Date PriorDateElement
            {
                get { return _PriorDateElement; }
                set { _PriorDateElement = value; OnPropertyChanged("PriorDateElement"); }
            }
            
            private Hl7.Fhir.Model.Date _PriorDateElement;
            
            /// <summary>
            /// Initial service Date
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string PriorDate
            {
                get { return PriorDateElement != null ? PriorDateElement.Value : null; }
                set
                {
                    if(value == null)
                      PriorDateElement = null; 
                    else
                      PriorDateElement = new Hl7.Fhir.Model.Date(value);
                    OnPropertyChanged("PriorDate");
                }
            }
            
            /// <summary>
            /// Prosthetic Material
            /// </summary>
            [FhirElement("priorMaterial", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Coding PriorMaterial
            {
                get { return _PriorMaterial; }
                set { _PriorMaterial = value; OnPropertyChanged("PriorMaterial"); }
            }
            
            private Hl7.Fhir.Model.Coding _PriorMaterial;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ProsthesisComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(InitialElement != null) dest.InitialElement = (Hl7.Fhir.Model.FhirBoolean)InitialElement.DeepCopy();
                    if(PriorDateElement != null) dest.PriorDateElement = (Hl7.Fhir.Model.Date)PriorDateElement.DeepCopy();
                    if(PriorMaterial != null) dest.PriorMaterial = (Hl7.Fhir.Model.Coding)PriorMaterial.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ProsthesisComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ProsthesisComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(InitialElement, otherT.InitialElement)) return false;
                if( !DeepComparable.Matches(PriorDateElement, otherT.PriorDateElement)) return false;
                if( !DeepComparable.Matches(PriorMaterial, otherT.PriorMaterial)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ProsthesisComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(InitialElement, otherT.InitialElement)) return false;
                if( !DeepComparable.IsExactly(PriorDateElement, otherT.PriorDateElement)) return false;
                if( !DeepComparable.IsExactly(PriorMaterial, otherT.PriorMaterial)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("MissingTeethComponent")]
        [DataContract]
        public partial class MissingTeethComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "MissingTeethComponent"; } }
            
            /// <summary>
            /// Tooth Code
            /// </summary>
            [FhirElement("tooth", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Tooth
            {
                get { return _Tooth; }
                set { _Tooth = value; OnPropertyChanged("Tooth"); }
            }
            
            private Hl7.Fhir.Model.Coding _Tooth;
            
            /// <summary>
            /// Indicates whether it was extracted or other reason
            /// </summary>
            [FhirElement("reason", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Reason
            {
                get { return _Reason; }
                set { _Reason = value; OnPropertyChanged("Reason"); }
            }
            
            private Hl7.Fhir.Model.Coding _Reason;
            
            /// <summary>
            /// Date tooth was extracted if known
            /// </summary>
            [FhirElement("extractionDate", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Date ExtractionDateElement
            {
                get { return _ExtractionDateElement; }
                set { _ExtractionDateElement = value; OnPropertyChanged("ExtractionDateElement"); }
            }
            
            private Hl7.Fhir.Model.Date _ExtractionDateElement;
            
            /// <summary>
            /// Date tooth was extracted if known
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ExtractionDate
            {
                get { return ExtractionDateElement != null ? ExtractionDateElement.Value : null; }
                set
                {
                    if(value == null)
                      ExtractionDateElement = null; 
                    else
                      ExtractionDateElement = new Hl7.Fhir.Model.Date(value);
                    OnPropertyChanged("ExtractionDate");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MissingTeethComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Tooth != null) dest.Tooth = (Hl7.Fhir.Model.Coding)Tooth.DeepCopy();
                    if(Reason != null) dest.Reason = (Hl7.Fhir.Model.Coding)Reason.DeepCopy();
                    if(ExtractionDateElement != null) dest.ExtractionDateElement = (Hl7.Fhir.Model.Date)ExtractionDateElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new MissingTeethComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as MissingTeethComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Tooth, otherT.Tooth)) return false;
                if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
                if( !DeepComparable.Matches(ExtractionDateElement, otherT.ExtractionDateElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as MissingTeethComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Tooth, otherT.Tooth)) return false;
                if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
                if( !DeepComparable.IsExactly(ExtractionDateElement, otherT.ExtractionDateElement)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Claim number
        /// </summary>
        [FhirElement("identifier", Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// active | cancelled | draft | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Claim.ClaimStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Claim.ClaimStatus> _StatusElement;
        
        /// <summary>
        /// active | cancelled | draft | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Claim.ClaimStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Claim.ClaimStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Type or discipline
        /// </summary>
        [FhirElement("type", Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.Coding _Type;
        
        /// <summary>
        /// Finer grained claim type information
        /// </summary>
        [FhirElement("subType", Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> SubType
        {
            get { if(_SubType==null) _SubType = new List<Hl7.Fhir.Model.Coding>(); return _SubType; }
            set { _SubType = value; OnPropertyChanged("SubType"); }
        }
        
        private List<Hl7.Fhir.Model.Coding> _SubType;
        
        /// <summary>
        /// Current specification followed
        /// </summary>
        [FhirElement("ruleset", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Ruleset
        {
            get { return _Ruleset; }
            set { _Ruleset = value; OnPropertyChanged("Ruleset"); }
        }
        
        private Hl7.Fhir.Model.Coding _Ruleset;
        
        /// <summary>
        /// Original specification followed
        /// </summary>
        [FhirElement("originalRuleset", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.Coding OriginalRuleset
        {
            get { return _OriginalRuleset; }
            set { _OriginalRuleset = value; OnPropertyChanged("OriginalRuleset"); }
        }
        
        private Hl7.Fhir.Model.Coding _OriginalRuleset;
        
        /// <summary>
        /// Creation date
        /// </summary>
        [FhirElement("created", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime CreatedElement
        {
            get { return _CreatedElement; }
            set { _CreatedElement = value; OnPropertyChanged("CreatedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _CreatedElement;
        
        /// <summary>
        /// Creation date
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Created
        {
            get { return CreatedElement != null ? CreatedElement.Value : null; }
            set
            {
                if(value == null)
                  CreatedElement = null; 
                else
                  CreatedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Created");
            }
        }
        
        /// <summary>
        /// Period for charge submission
        /// </summary>
        [FhirElement("billablePeriod", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.Period BillablePeriod
        {
            get { return _BillablePeriod; }
            set { _BillablePeriod = value; OnPropertyChanged("BillablePeriod"); }
        }
        
        private Hl7.Fhir.Model.Period _BillablePeriod;
        
        /// <summary>
        /// Target
        /// </summary>
        [FhirElement("insurer", Order=170, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Insurer
        {
            get { return _Insurer; }
            set { _Insurer = value; OnPropertyChanged("Insurer"); }
        }
        
        private Hl7.Fhir.Model.Element _Insurer;
        
        /// <summary>
        /// Responsible provider
        /// </summary>
        [FhirElement("provider", Order=180, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Provider
        {
            get { return _Provider; }
            set { _Provider = value; OnPropertyChanged("Provider"); }
        }
        
        private Hl7.Fhir.Model.Element _Provider;
        
        /// <summary>
        /// Responsible organization
        /// </summary>
        [FhirElement("organization", Order=190, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Organization
        {
            get { return _Organization; }
            set { _Organization = value; OnPropertyChanged("Organization"); }
        }
        
        private Hl7.Fhir.Model.Element _Organization;
        
        /// <summary>
        /// complete | proposed | exploratory | other
        /// </summary>
        [FhirElement("use", Order=200)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Claim.Use> Use_Element
        {
            get { return _Use_Element; }
            set { _Use_Element = value; OnPropertyChanged("Use_Element"); }
        }
        
        private Code<Hl7.Fhir.Model.Claim.Use> _Use_Element;
        
        /// <summary>
        /// complete | proposed | exploratory | other
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Claim.Use? Use_
        {
            get { return Use_Element != null ? Use_Element.Value : null; }
            set
            {
                if(value == null)
                  Use_Element = null; 
                else
                  Use_Element = new Code<Hl7.Fhir.Model.Claim.Use>(value);
                OnPropertyChanged("Use_");
            }
        }
        
        /// <summary>
        /// Desired processing priority
        /// </summary>
        [FhirElement("priority", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Priority
        {
            get { return _Priority; }
            set { _Priority = value; OnPropertyChanged("Priority"); }
        }
        
        private Hl7.Fhir.Model.Coding _Priority;
        
        /// <summary>
        /// Funds requested to be reserved
        /// </summary>
        [FhirElement("fundsReserve", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.Coding FundsReserve
        {
            get { return _FundsReserve; }
            set { _FundsReserve = value; OnPropertyChanged("FundsReserve"); }
        }
        
        private Hl7.Fhir.Model.Coding _FundsReserve;
        
        /// <summary>
        /// Author
        /// </summary>
        [FhirElement("enterer", Order=230, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Enterer
        {
            get { return _Enterer; }
            set { _Enterer = value; OnPropertyChanged("Enterer"); }
        }
        
        private Hl7.Fhir.Model.Element _Enterer;
        
        /// <summary>
        /// Servicing Facility
        /// </summary>
        [FhirElement("facility", Order=240, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Facility
        {
            get { return _Facility; }
            set { _Facility = value; OnPropertyChanged("Facility"); }
        }
        
        private Hl7.Fhir.Model.Element _Facility;
        
        /// <summary>
        /// Related Claims which may be revelant to processing this claimn
        /// </summary>
        [FhirElement("related", Order=250)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Claim.RelatedClaimComponent> Related
        {
            get { if(_Related==null) _Related = new List<Hl7.Fhir.Model.Claim.RelatedClaimComponent>(); return _Related; }
            set { _Related = value; OnPropertyChanged("Related"); }
        }
        
        private List<Hl7.Fhir.Model.Claim.RelatedClaimComponent> _Related;
        
        /// <summary>
        /// Prescription
        /// </summary>
        [FhirElement("prescription", Order=260, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Prescription
        {
            get { return _Prescription; }
            set { _Prescription = value; OnPropertyChanged("Prescription"); }
        }
        
        private Hl7.Fhir.Model.Element _Prescription;
        
        /// <summary>
        /// Original Prescription
        /// </summary>
        [FhirElement("originalPrescription", Order=270, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element OriginalPrescription
        {
            get { return _OriginalPrescription; }
            set { _OriginalPrescription = value; OnPropertyChanged("OriginalPrescription"); }
        }
        
        private Hl7.Fhir.Model.Element _OriginalPrescription;
        
        /// <summary>
        /// Party to be paid any benefits payable
        /// </summary>
        [FhirElement("payee", Order=280)]
        [DataMember]
        public Hl7.Fhir.Model.Claim.PayeeComponent Payee
        {
            get { return _Payee; }
            set { _Payee = value; OnPropertyChanged("Payee"); }
        }
        
        private Hl7.Fhir.Model.Claim.PayeeComponent _Payee;
        
        /// <summary>
        /// Treatment Referral
        /// </summary>
        [FhirElement("referral", Order=290, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Referral
        {
            get { return _Referral; }
            set { _Referral = value; OnPropertyChanged("Referral"); }
        }
        
        private Hl7.Fhir.Model.Element _Referral;
        
        /// <summary>
        /// 
        /// </summary>
        [FhirElement("information", Order=300)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Claim.SpecialConditionComponent> Information
        {
            get { if(_Information==null) _Information = new List<Hl7.Fhir.Model.Claim.SpecialConditionComponent>(); return _Information; }
            set { _Information = value; OnPropertyChanged("Information"); }
        }
        
        private List<Hl7.Fhir.Model.Claim.SpecialConditionComponent> _Information;
        
        /// <summary>
        /// Diagnosis
        /// </summary>
        [FhirElement("diagnosis", Order=310)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Claim.DiagnosisComponent> Diagnosis
        {
            get { if(_Diagnosis==null) _Diagnosis = new List<Hl7.Fhir.Model.Claim.DiagnosisComponent>(); return _Diagnosis; }
            set { _Diagnosis = value; OnPropertyChanged("Diagnosis"); }
        }
        
        private List<Hl7.Fhir.Model.Claim.DiagnosisComponent> _Diagnosis;
        
        /// <summary>
        /// Procedures performed
        /// </summary>
        [FhirElement("procedure", Order=320)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Claim.ProcedureComponent> Procedure
        {
            get { if(_Procedure==null) _Procedure = new List<Hl7.Fhir.Model.Claim.ProcedureComponent>(); return _Procedure; }
            set { _Procedure = value; OnPropertyChanged("Procedure"); }
        }
        
        private List<Hl7.Fhir.Model.Claim.ProcedureComponent> _Procedure;
        
        /// <summary>
        /// The subject of the Products and Services
        /// </summary>
        [FhirElement("patient", Order=330, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.ResourceReference))]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Element Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        
        private Hl7.Fhir.Model.Element _Patient;
        
        /// <summary>
        /// Insurance or medical plan
        /// </summary>
        [FhirElement("coverage", Order=340)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Claim.CoverageComponent> Coverage
        {
            get { if(_Coverage==null) _Coverage = new List<Hl7.Fhir.Model.Claim.CoverageComponent>(); return _Coverage; }
            set { _Coverage = value; OnPropertyChanged("Coverage"); }
        }
        
        private List<Hl7.Fhir.Model.Claim.CoverageComponent> _Coverage;
        
        /// <summary>
        /// 
        /// </summary>
        [FhirElement("accident", Order=350)]
        [DataMember]
        public Hl7.Fhir.Model.Claim.AccidentComponent Accident
        {
            get { return _Accident; }
            set { _Accident = value; OnPropertyChanged("Accident"); }
        }
        
        private Hl7.Fhir.Model.Claim.AccidentComponent _Accident;
        
        /// <summary>
        /// Period unable to work
        /// </summary>
        [FhirElement("employmentImpacted", Order=360)]
        [DataMember]
        public Hl7.Fhir.Model.Period EmploymentImpacted
        {
            get { return _EmploymentImpacted; }
            set { _EmploymentImpacted = value; OnPropertyChanged("EmploymentImpacted"); }
        }
        
        private Hl7.Fhir.Model.Period _EmploymentImpacted;
        
        /// <summary>
        /// Period in hospital
        /// </summary>
        [FhirElement("hospitalization", Order=370)]
        [DataMember]
        public Hl7.Fhir.Model.Period Hospitalization
        {
            get { return _Hospitalization; }
            set { _Hospitalization = value; OnPropertyChanged("Hospitalization"); }
        }
        
        private Hl7.Fhir.Model.Period _Hospitalization;
        
        /// <summary>
        /// Goods and Services
        /// </summary>
        [FhirElement("item", Order=380)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Claim.ItemComponent> Item
        {
            get { if(_Item==null) _Item = new List<Hl7.Fhir.Model.Claim.ItemComponent>(); return _Item; }
            set { _Item = value; OnPropertyChanged("Item"); }
        }
        
        private List<Hl7.Fhir.Model.Claim.ItemComponent> _Item;
        
        /// <summary>
        /// Total claim cost
        /// </summary>
        [FhirElement("total", Order=390)]
        [DataMember]
        public Money Total
        {
            get { return _Total; }
            set { _Total = value; OnPropertyChanged("Total"); }
        }
        
        private Money _Total;
        
        /// <summary>
        /// Only if type = oral
        /// </summary>
        [FhirElement("missingTeeth", Order=400)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Claim.MissingTeethComponent> MissingTeeth
        {
            get { if(_MissingTeeth==null) _MissingTeeth = new List<Hl7.Fhir.Model.Claim.MissingTeethComponent>(); return _MissingTeeth; }
            set { _MissingTeeth = value; OnPropertyChanged("MissingTeeth"); }
        }
        
        private List<Hl7.Fhir.Model.Claim.MissingTeethComponent> _MissingTeeth;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Claim;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Claim.ClaimStatus>)StatusElement.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                if(SubType != null) dest.SubType = new List<Hl7.Fhir.Model.Coding>(SubType.DeepCopy());
                if(Ruleset != null) dest.Ruleset = (Hl7.Fhir.Model.Coding)Ruleset.DeepCopy();
                if(OriginalRuleset != null) dest.OriginalRuleset = (Hl7.Fhir.Model.Coding)OriginalRuleset.DeepCopy();
                if(CreatedElement != null) dest.CreatedElement = (Hl7.Fhir.Model.FhirDateTime)CreatedElement.DeepCopy();
                if(BillablePeriod != null) dest.BillablePeriod = (Hl7.Fhir.Model.Period)BillablePeriod.DeepCopy();
                if(Insurer != null) dest.Insurer = (Hl7.Fhir.Model.Element)Insurer.DeepCopy();
                if(Provider != null) dest.Provider = (Hl7.Fhir.Model.Element)Provider.DeepCopy();
                if(Organization != null) dest.Organization = (Hl7.Fhir.Model.Element)Organization.DeepCopy();
                if(Use_Element != null) dest.Use_Element = (Code<Hl7.Fhir.Model.Claim.Use>)Use_Element.DeepCopy();
                if(Priority != null) dest.Priority = (Hl7.Fhir.Model.Coding)Priority.DeepCopy();
                if(FundsReserve != null) dest.FundsReserve = (Hl7.Fhir.Model.Coding)FundsReserve.DeepCopy();
                if(Enterer != null) dest.Enterer = (Hl7.Fhir.Model.Element)Enterer.DeepCopy();
                if(Facility != null) dest.Facility = (Hl7.Fhir.Model.Element)Facility.DeepCopy();
                if(Related != null) dest.Related = new List<Hl7.Fhir.Model.Claim.RelatedClaimComponent>(Related.DeepCopy());
                if(Prescription != null) dest.Prescription = (Hl7.Fhir.Model.Element)Prescription.DeepCopy();
                if(OriginalPrescription != null) dest.OriginalPrescription = (Hl7.Fhir.Model.Element)OriginalPrescription.DeepCopy();
                if(Payee != null) dest.Payee = (Hl7.Fhir.Model.Claim.PayeeComponent)Payee.DeepCopy();
                if(Referral != null) dest.Referral = (Hl7.Fhir.Model.Element)Referral.DeepCopy();
                if(Information != null) dest.Information = new List<Hl7.Fhir.Model.Claim.SpecialConditionComponent>(Information.DeepCopy());
                if(Diagnosis != null) dest.Diagnosis = new List<Hl7.Fhir.Model.Claim.DiagnosisComponent>(Diagnosis.DeepCopy());
                if(Procedure != null) dest.Procedure = new List<Hl7.Fhir.Model.Claim.ProcedureComponent>(Procedure.DeepCopy());
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.Element)Patient.DeepCopy();
                if(Coverage != null) dest.Coverage = new List<Hl7.Fhir.Model.Claim.CoverageComponent>(Coverage.DeepCopy());
                if(Accident != null) dest.Accident = (Hl7.Fhir.Model.Claim.AccidentComponent)Accident.DeepCopy();
                if(EmploymentImpacted != null) dest.EmploymentImpacted = (Hl7.Fhir.Model.Period)EmploymentImpacted.DeepCopy();
                if(Hospitalization != null) dest.Hospitalization = (Hl7.Fhir.Model.Period)Hospitalization.DeepCopy();
                if(Item != null) dest.Item = new List<Hl7.Fhir.Model.Claim.ItemComponent>(Item.DeepCopy());
                if(Total != null) dest.Total = (Money)Total.DeepCopy();
                if(MissingTeeth != null) dest.MissingTeeth = new List<Hl7.Fhir.Model.Claim.MissingTeethComponent>(MissingTeeth.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Claim());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Claim;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(SubType, otherT.SubType)) return false;
            if( !DeepComparable.Matches(Ruleset, otherT.Ruleset)) return false;
            if( !DeepComparable.Matches(OriginalRuleset, otherT.OriginalRuleset)) return false;
            if( !DeepComparable.Matches(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.Matches(BillablePeriod, otherT.BillablePeriod)) return false;
            if( !DeepComparable.Matches(Insurer, otherT.Insurer)) return false;
            if( !DeepComparable.Matches(Provider, otherT.Provider)) return false;
            if( !DeepComparable.Matches(Organization, otherT.Organization)) return false;
            if( !DeepComparable.Matches(Use_Element, otherT.Use_Element)) return false;
            if( !DeepComparable.Matches(Priority, otherT.Priority)) return false;
            if( !DeepComparable.Matches(FundsReserve, otherT.FundsReserve)) return false;
            if( !DeepComparable.Matches(Enterer, otherT.Enterer)) return false;
            if( !DeepComparable.Matches(Facility, otherT.Facility)) return false;
            if( !DeepComparable.Matches(Related, otherT.Related)) return false;
            if( !DeepComparable.Matches(Prescription, otherT.Prescription)) return false;
            if( !DeepComparable.Matches(OriginalPrescription, otherT.OriginalPrescription)) return false;
            if( !DeepComparable.Matches(Payee, otherT.Payee)) return false;
            if( !DeepComparable.Matches(Referral, otherT.Referral)) return false;
            if( !DeepComparable.Matches(Information, otherT.Information)) return false;
            if( !DeepComparable.Matches(Diagnosis, otherT.Diagnosis)) return false;
            if( !DeepComparable.Matches(Procedure, otherT.Procedure)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Coverage, otherT.Coverage)) return false;
            if( !DeepComparable.Matches(Accident, otherT.Accident)) return false;
            if( !DeepComparable.Matches(EmploymentImpacted, otherT.EmploymentImpacted)) return false;
            if( !DeepComparable.Matches(Hospitalization, otherT.Hospitalization)) return false;
            if( !DeepComparable.Matches(Item, otherT.Item)) return false;
            if( !DeepComparable.Matches(Total, otherT.Total)) return false;
            if( !DeepComparable.Matches(MissingTeeth, otherT.MissingTeeth)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Claim;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(SubType, otherT.SubType)) return false;
            if( !DeepComparable.IsExactly(Ruleset, otherT.Ruleset)) return false;
            if( !DeepComparable.IsExactly(OriginalRuleset, otherT.OriginalRuleset)) return false;
            if( !DeepComparable.IsExactly(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.IsExactly(BillablePeriod, otherT.BillablePeriod)) return false;
            if( !DeepComparable.IsExactly(Insurer, otherT.Insurer)) return false;
            if( !DeepComparable.IsExactly(Provider, otherT.Provider)) return false;
            if( !DeepComparable.IsExactly(Organization, otherT.Organization)) return false;
            if( !DeepComparable.IsExactly(Use_Element, otherT.Use_Element)) return false;
            if( !DeepComparable.IsExactly(Priority, otherT.Priority)) return false;
            if( !DeepComparable.IsExactly(FundsReserve, otherT.FundsReserve)) return false;
            if( !DeepComparable.IsExactly(Enterer, otherT.Enterer)) return false;
            if( !DeepComparable.IsExactly(Facility, otherT.Facility)) return false;
            if( !DeepComparable.IsExactly(Related, otherT.Related)) return false;
            if( !DeepComparable.IsExactly(Prescription, otherT.Prescription)) return false;
            if( !DeepComparable.IsExactly(OriginalPrescription, otherT.OriginalPrescription)) return false;
            if( !DeepComparable.IsExactly(Payee, otherT.Payee)) return false;
            if( !DeepComparable.IsExactly(Referral, otherT.Referral)) return false;
            if( !DeepComparable.IsExactly(Information, otherT.Information)) return false;
            if( !DeepComparable.IsExactly(Diagnosis, otherT.Diagnosis)) return false;
            if( !DeepComparable.IsExactly(Procedure, otherT.Procedure)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Coverage, otherT.Coverage)) return false;
            if( !DeepComparable.IsExactly(Accident, otherT.Accident)) return false;
            if( !DeepComparable.IsExactly(EmploymentImpacted, otherT.EmploymentImpacted)) return false;
            if( !DeepComparable.IsExactly(Hospitalization, otherT.Hospitalization)) return false;
            if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
            if( !DeepComparable.IsExactly(Total, otherT.Total)) return false;
            if( !DeepComparable.IsExactly(MissingTeeth, otherT.MissingTeeth)) return false;
            
            return true;
        }
        
    }
    
}
