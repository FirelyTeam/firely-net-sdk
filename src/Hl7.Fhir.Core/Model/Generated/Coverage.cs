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
// Generated for FHIR v1.3.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Insurance or medical plan
    /// </summary>
    [FhirType("Coverage", IsResource=true)]
    [DataContract]
    public partial class Coverage : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Coverage; } }
        [NotMapped]
        public override string TypeName { get { return "Coverage"; } }
        
        /// <summary>
        /// Identifier for the plan issuer
        /// </summary>
        [FhirElement("issuer", InSummary=true, Order=90, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.ResourceReference))]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Element Issuer
        {
            get { return _Issuer; }
            set { _Issuer = value; OnPropertyChanged("Issuer"); }
        }
        
        private Hl7.Fhir.Model.Element _Issuer;
        
        /// <summary>
        /// BIN Number
        /// </summary>
        [FhirElement("bin", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString BinElement
        {
            get { return _BinElement; }
            set { _BinElement = value; OnPropertyChanged("BinElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _BinElement;
        
        /// <summary>
        /// BIN Number
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Bin
        {
            get { return BinElement != null ? BinElement.Value : null; }
            set
            {
                if(value == null)
                  BinElement = null; 
                else
                  BinElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Bin");
            }
        }
        
        /// <summary>
        /// Coverage start and end dates
        /// </summary>
        [FhirElement("period", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        
        private Hl7.Fhir.Model.Period _Period;
        
        /// <summary>
        /// Type of coverage
        /// </summary>
        [FhirElement("type", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.Coding _Type;
        
        /// <summary>
        /// Plan holder
        /// </summary>
        [FhirElement("planholder", Order=130, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.ResourceReference))]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Element Planholder
        {
            get { return _Planholder; }
            set { _Planholder = value; OnPropertyChanged("Planholder"); }
        }
        
        private Hl7.Fhir.Model.Element _Planholder;
        
        /// <summary>
        /// Plan Beneficiary
        /// </summary>
        [FhirElement("beneficiary", Order=140, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Identifier),typeof(Hl7.Fhir.Model.ResourceReference))]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Element Beneficiary
        {
            get { return _Beneficiary; }
            set { _Beneficiary = value; OnPropertyChanged("Beneficiary"); }
        }
        
        private Hl7.Fhir.Model.Element _Beneficiary;
        
        /// <summary>
        /// Patient relationship to planholder
        /// </summary>
        [FhirElement("relationship", Order=150)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Relationship
        {
            get { return _Relationship; }
            set { _Relationship = value; OnPropertyChanged("Relationship"); }
        }
        
        private Hl7.Fhir.Model.Coding _Relationship;
        
        /// <summary>
        /// The primary coverage ID
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// An identifier for the group
        /// </summary>
        [FhirElement("group", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString GroupElement
        {
            get { return _GroupElement; }
            set { _GroupElement = value; OnPropertyChanged("GroupElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _GroupElement;
        
        /// <summary>
        /// An identifier for the group
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Group
        {
            get { return GroupElement != null ? GroupElement.Value : null; }
            set
            {
                if(value == null)
                  GroupElement = null; 
                else
                  GroupElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Group");
            }
        }
        
        /// <summary>
        /// An identifier for the plan
        /// </summary>
        [FhirElement("plan", InSummary=true, Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PlanElement
        {
            get { return _PlanElement; }
            set { _PlanElement = value; OnPropertyChanged("PlanElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PlanElement;
        
        /// <summary>
        /// An identifier for the plan
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Plan
        {
            get { return PlanElement != null ? PlanElement.Value : null; }
            set
            {
                if(value == null)
                  PlanElement = null; 
                else
                  PlanElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Plan");
            }
        }
        
        /// <summary>
        /// An identifier for the subsection of the plan
        /// </summary>
        [FhirElement("subPlan", InSummary=true, Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString SubPlanElement
        {
            get { return _SubPlanElement; }
            set { _SubPlanElement = value; OnPropertyChanged("SubPlanElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _SubPlanElement;
        
        /// <summary>
        /// An identifier for the subsection of the plan
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string SubPlan
        {
            get { return SubPlanElement != null ? SubPlanElement.Value : null; }
            set
            {
                if(value == null)
                  SubPlanElement = null; 
                else
                  SubPlanElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("SubPlan");
            }
        }
        
        /// <summary>
        /// Dependent number
        /// </summary>
        [FhirElement("dependent", InSummary=true, Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.PositiveInt DependentElement
        {
            get { return _DependentElement; }
            set { _DependentElement = value; OnPropertyChanged("DependentElement"); }
        }
        
        private Hl7.Fhir.Model.PositiveInt _DependentElement;
        
        /// <summary>
        /// Dependent number
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? Dependent
        {
            get { return DependentElement != null ? DependentElement.Value : null; }
            set
            {
                if(value == null)
                  DependentElement = null; 
                else
                  DependentElement = new Hl7.Fhir.Model.PositiveInt(value);
                OnPropertyChanged("Dependent");
            }
        }
        
        /// <summary>
        /// The plan instance or sequence counter
        /// </summary>
        [FhirElement("sequence", InSummary=true, Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.PositiveInt SequenceElement
        {
            get { return _SequenceElement; }
            set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
        }
        
        private Hl7.Fhir.Model.PositiveInt _SequenceElement;
        
        /// <summary>
        /// The plan instance or sequence counter
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
        /// Eligibility exceptions
        /// </summary>
        [FhirElement("exception", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> Exception
        {
            get { if(_Exception==null) _Exception = new List<Hl7.Fhir.Model.Coding>(); return _Exception; }
            set { _Exception = value; OnPropertyChanged("Exception"); }
        }
        
        private List<Hl7.Fhir.Model.Coding> _Exception;
        
        /// <summary>
        /// Name of School
        /// </summary>
        [FhirElement("school", Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString SchoolElement
        {
            get { return _SchoolElement; }
            set { _SchoolElement = value; OnPropertyChanged("SchoolElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _SchoolElement;
        
        /// <summary>
        /// Name of School
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string School
        {
            get { return SchoolElement != null ? SchoolElement.Value : null; }
            set
            {
                if(value == null)
                  SchoolElement = null; 
                else
                  SchoolElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("School");
            }
        }
        
        /// <summary>
        /// Insurer network
        /// </summary>
        [FhirElement("network", InSummary=true, Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NetworkElement
        {
            get { return _NetworkElement; }
            set { _NetworkElement = value; OnPropertyChanged("NetworkElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NetworkElement;
        
        /// <summary>
        /// Insurer network
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Network
        {
            get { return NetworkElement != null ? NetworkElement.Value : null; }
            set
            {
                if(value == null)
                  NetworkElement = null; 
                else
                  NetworkElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Network");
            }
        }
        
        /// <summary>
        /// Contract details
        /// </summary>
        [FhirElement("contract", Order=250)]
        [References("Contract")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Contract
        {
            get { if(_Contract==null) _Contract = new List<Hl7.Fhir.Model.ResourceReference>(); return _Contract; }
            set { _Contract = value; OnPropertyChanged("Contract"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Contract;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Coverage;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Issuer != null) dest.Issuer = (Hl7.Fhir.Model.Element)Issuer.DeepCopy();
                if(BinElement != null) dest.BinElement = (Hl7.Fhir.Model.FhirString)BinElement.DeepCopy();
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                if(Planholder != null) dest.Planholder = (Hl7.Fhir.Model.Element)Planholder.DeepCopy();
                if(Beneficiary != null) dest.Beneficiary = (Hl7.Fhir.Model.Element)Beneficiary.DeepCopy();
                if(Relationship != null) dest.Relationship = (Hl7.Fhir.Model.Coding)Relationship.DeepCopy();
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(GroupElement != null) dest.GroupElement = (Hl7.Fhir.Model.FhirString)GroupElement.DeepCopy();
                if(PlanElement != null) dest.PlanElement = (Hl7.Fhir.Model.FhirString)PlanElement.DeepCopy();
                if(SubPlanElement != null) dest.SubPlanElement = (Hl7.Fhir.Model.FhirString)SubPlanElement.DeepCopy();
                if(DependentElement != null) dest.DependentElement = (Hl7.Fhir.Model.PositiveInt)DependentElement.DeepCopy();
                if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                if(Exception != null) dest.Exception = new List<Hl7.Fhir.Model.Coding>(Exception.DeepCopy());
                if(SchoolElement != null) dest.SchoolElement = (Hl7.Fhir.Model.FhirString)SchoolElement.DeepCopy();
                if(NetworkElement != null) dest.NetworkElement = (Hl7.Fhir.Model.FhirString)NetworkElement.DeepCopy();
                if(Contract != null) dest.Contract = new List<Hl7.Fhir.Model.ResourceReference>(Contract.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Coverage());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Coverage;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Issuer, otherT.Issuer)) return false;
            if( !DeepComparable.Matches(BinElement, otherT.BinElement)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(Planholder, otherT.Planholder)) return false;
            if( !DeepComparable.Matches(Beneficiary, otherT.Beneficiary)) return false;
            if( !DeepComparable.Matches(Relationship, otherT.Relationship)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(GroupElement, otherT.GroupElement)) return false;
            if( !DeepComparable.Matches(PlanElement, otherT.PlanElement)) return false;
            if( !DeepComparable.Matches(SubPlanElement, otherT.SubPlanElement)) return false;
            if( !DeepComparable.Matches(DependentElement, otherT.DependentElement)) return false;
            if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
            if( !DeepComparable.Matches(Exception, otherT.Exception)) return false;
            if( !DeepComparable.Matches(SchoolElement, otherT.SchoolElement)) return false;
            if( !DeepComparable.Matches(NetworkElement, otherT.NetworkElement)) return false;
            if( !DeepComparable.Matches(Contract, otherT.Contract)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Coverage;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Issuer, otherT.Issuer)) return false;
            if( !DeepComparable.IsExactly(BinElement, otherT.BinElement)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(Planholder, otherT.Planholder)) return false;
            if( !DeepComparable.IsExactly(Beneficiary, otherT.Beneficiary)) return false;
            if( !DeepComparable.IsExactly(Relationship, otherT.Relationship)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(GroupElement, otherT.GroupElement)) return false;
            if( !DeepComparable.IsExactly(PlanElement, otherT.PlanElement)) return false;
            if( !DeepComparable.IsExactly(SubPlanElement, otherT.SubPlanElement)) return false;
            if( !DeepComparable.IsExactly(DependentElement, otherT.DependentElement)) return false;
            if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
            if( !DeepComparable.IsExactly(Exception, otherT.Exception)) return false;
            if( !DeepComparable.IsExactly(SchoolElement, otherT.SchoolElement)) return false;
            if( !DeepComparable.IsExactly(NetworkElement, otherT.NetworkElement)) return false;
            if( !DeepComparable.IsExactly(Contract, otherT.Contract)) return false;
            
            return true;
        }
        
    }
    
}
