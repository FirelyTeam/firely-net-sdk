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
// Generated for FHIR v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Request to perform some action on or in regards to an existing resource
    /// </summary>
    [FhirType("ProcessRequest", IsResource=true)]
    [DataContract]
    public partial class ProcessRequest : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ProcessRequest; } }
        [NotMapped]
        public override string TypeName { get { return "ProcessRequest"; } }
        
        /// <summary>
        /// List of allowable action which this resource can request.
        /// (url: http://hl7.org/fhir/ValueSet/actionlist)
        /// </summary>
        [FhirEnumeration("ActionList")]
        public enum ActionList
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/actionlist)
            /// </summary>
            [EnumLiteral("cancel", "http://hl7.org/fhir/actionlist"), Description("Cancel, Reverse or Nullify")]
            Cancel,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/actionlist)
            /// </summary>
            [EnumLiteral("poll", "http://hl7.org/fhir/actionlist"), Description("Poll")]
            Poll,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/actionlist)
            /// </summary>
            [EnumLiteral("reprocess", "http://hl7.org/fhir/actionlist"), Description("Re-Process")]
            Reprocess,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/actionlist)
            /// </summary>
            [EnumLiteral("status", "http://hl7.org/fhir/actionlist"), Description("Status Check")]
            Status,
        }

        [FhirType("ItemsComponent")]
        [DataContract]
        public partial class ItemsComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ItemsComponent"; } }
            
            /// <summary>
            /// Service instance
            /// </summary>
            [FhirElement("sequenceLinkId", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Integer SequenceLinkIdElement
            {
                get { return _SequenceLinkIdElement; }
                set { _SequenceLinkIdElement = value; OnPropertyChanged("SequenceLinkIdElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _SequenceLinkIdElement;
            
            /// <summary>
            /// Service instance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? SequenceLinkId
            {
                get { return SequenceLinkIdElement != null ? SequenceLinkIdElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        SequenceLinkIdElement = null; 
                    else
                        SequenceLinkIdElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("SequenceLinkId");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ItemsComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceLinkIdElement != null) dest.SequenceLinkIdElement = (Hl7.Fhir.Model.Integer)SequenceLinkIdElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ItemsComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ItemsComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceLinkIdElement, otherT.SequenceLinkIdElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ItemsComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceLinkIdElement, otherT.SequenceLinkIdElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceLinkIdElement != null) yield return SequenceLinkIdElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceLinkIdElement != null) yield return new ElementValue("sequenceLinkId", SequenceLinkIdElement);
                }
            }

            
        }
        
        
        /// <summary>
        /// Business Identifier
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
        [DataMember]
        public Code<Hl7.Fhir.Model.FinancialResourceStatusCodes> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.FinancialResourceStatusCodes> _StatusElement;
        
        /// <summary>
        /// active | cancelled | draft | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.FinancialResourceStatusCodes? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.FinancialResourceStatusCodes>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// cancel | poll | reprocess | status
        /// </summary>
        [FhirElement("action", Order=110)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ProcessRequest.ActionList> ActionElement
        {
            get { return _ActionElement; }
            set { _ActionElement = value; OnPropertyChanged("ActionElement"); }
        }
        
        private Code<Hl7.Fhir.Model.ProcessRequest.ActionList> _ActionElement;
        
        /// <summary>
        /// cancel | poll | reprocess | status
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ProcessRequest.ActionList? Action
        {
            get { return ActionElement != null ? ActionElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  ActionElement = null; 
                else
                  ActionElement = new Code<Hl7.Fhir.Model.ProcessRequest.ActionList>(value);
                OnPropertyChanged("Action");
            }
        }
        
        /// <summary>
        /// Party which is the target of the request
        /// </summary>
        [FhirElement("target", Order=120)]
        [CLSCompliant(false)]
		[References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Target
        {
            get { return _Target; }
            set { _Target = value; OnPropertyChanged("Target"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Target;
        
        /// <summary>
        /// Creation date
        /// </summary>
        [FhirElement("created", Order=130)]
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
                if (value == null)
                  CreatedElement = null; 
                else
                  CreatedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Created");
            }
        }
        
        /// <summary>
        /// Responsible practitioner
        /// </summary>
        [FhirElement("provider", Order=140)]
        [CLSCompliant(false)]
		[References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Provider
        {
            get { return _Provider; }
            set { _Provider = value; OnPropertyChanged("Provider"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Provider;
        
        /// <summary>
        /// Responsible organization
        /// </summary>
        [FhirElement("organization", Order=150)]
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
        /// Reference to the Request resource
        /// </summary>
        [FhirElement("request", Order=160)]
        [CLSCompliant(false)]
		[References()]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Request
        {
            get { return _Request; }
            set { _Request = value; OnPropertyChanged("Request"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Request;
        
        /// <summary>
        /// Reference to the Response resource
        /// </summary>
        [FhirElement("response", Order=170)]
        [CLSCompliant(false)]
		[References()]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Response
        {
            get { return _Response; }
            set { _Response = value; OnPropertyChanged("Response"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Response;
        
        /// <summary>
        /// Remove history
        /// </summary>
        [FhirElement("nullify", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean NullifyElement
        {
            get { return _NullifyElement; }
            set { _NullifyElement = value; OnPropertyChanged("NullifyElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _NullifyElement;
        
        /// <summary>
        /// Remove history
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Nullify
        {
            get { return NullifyElement != null ? NullifyElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  NullifyElement = null; 
                else
                  NullifyElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Nullify");
            }
        }
        
        /// <summary>
        /// Reference number/string
        /// </summary>
        [FhirElement("reference", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ReferenceElement
        {
            get { return _ReferenceElement; }
            set { _ReferenceElement = value; OnPropertyChanged("ReferenceElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ReferenceElement;
        
        /// <summary>
        /// Reference number/string
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Reference
        {
            get { return ReferenceElement != null ? ReferenceElement.Value : null; }
            set
            {
                if (value == null)
                  ReferenceElement = null; 
                else
                  ReferenceElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Reference");
            }
        }
        
        /// <summary>
        /// Items to re-adjudicate
        /// </summary>
        [FhirElement("item", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ProcessRequest.ItemsComponent> Item
        {
            get { if(_Item==null) _Item = new List<Hl7.Fhir.Model.ProcessRequest.ItemsComponent>(); return _Item; }
            set { _Item = value; OnPropertyChanged("Item"); }
        }
        
        private List<Hl7.Fhir.Model.ProcessRequest.ItemsComponent> _Item;
        
        /// <summary>
        /// Resource type(s) to include
        /// </summary>
        [FhirElement("include", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> IncludeElement
        {
            get { if(_IncludeElement==null) _IncludeElement = new List<Hl7.Fhir.Model.FhirString>(); return _IncludeElement; }
            set { _IncludeElement = value; OnPropertyChanged("IncludeElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _IncludeElement;
        
        /// <summary>
        /// Resource type(s) to include
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Include
        {
            get { return IncludeElement != null ? IncludeElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  IncludeElement = null; 
                else
                  IncludeElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Include");
            }
        }
        
        /// <summary>
        /// Resource type(s) to exclude
        /// </summary>
        [FhirElement("exclude", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> ExcludeElement
        {
            get { if(_ExcludeElement==null) _ExcludeElement = new List<Hl7.Fhir.Model.FhirString>(); return _ExcludeElement; }
            set { _ExcludeElement = value; OnPropertyChanged("ExcludeElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _ExcludeElement;
        
        /// <summary>
        /// Resource type(s) to exclude
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Exclude
        {
            get { return ExcludeElement != null ? ExcludeElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  ExcludeElement = null; 
                else
                  ExcludeElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Exclude");
            }
        }
        
        /// <summary>
        /// Selection period
        /// </summary>
        [FhirElement("period", Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        
        private Hl7.Fhir.Model.Period _Period;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ProcessRequest;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.FinancialResourceStatusCodes>)StatusElement.DeepCopy();
                if(ActionElement != null) dest.ActionElement = (Code<Hl7.Fhir.Model.ProcessRequest.ActionList>)ActionElement.DeepCopy();
                if(Target != null) dest.Target = (Hl7.Fhir.Model.ResourceReference)Target.DeepCopy();
                if(CreatedElement != null) dest.CreatedElement = (Hl7.Fhir.Model.FhirDateTime)CreatedElement.DeepCopy();
                if(Provider != null) dest.Provider = (Hl7.Fhir.Model.ResourceReference)Provider.DeepCopy();
                if(Organization != null) dest.Organization = (Hl7.Fhir.Model.ResourceReference)Organization.DeepCopy();
                if(Request != null) dest.Request = (Hl7.Fhir.Model.ResourceReference)Request.DeepCopy();
                if(Response != null) dest.Response = (Hl7.Fhir.Model.ResourceReference)Response.DeepCopy();
                if(NullifyElement != null) dest.NullifyElement = (Hl7.Fhir.Model.FhirBoolean)NullifyElement.DeepCopy();
                if(ReferenceElement != null) dest.ReferenceElement = (Hl7.Fhir.Model.FhirString)ReferenceElement.DeepCopy();
                if(Item != null) dest.Item = new List<Hl7.Fhir.Model.ProcessRequest.ItemsComponent>(Item.DeepCopy());
                if(IncludeElement != null) dest.IncludeElement = new List<Hl7.Fhir.Model.FhirString>(IncludeElement.DeepCopy());
                if(ExcludeElement != null) dest.ExcludeElement = new List<Hl7.Fhir.Model.FhirString>(ExcludeElement.DeepCopy());
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new ProcessRequest());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ProcessRequest;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ActionElement, otherT.ActionElement)) return false;
            if( !DeepComparable.Matches(Target, otherT.Target)) return false;
            if( !DeepComparable.Matches(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.Matches(Provider, otherT.Provider)) return false;
            if( !DeepComparable.Matches(Organization, otherT.Organization)) return false;
            if( !DeepComparable.Matches(Request, otherT.Request)) return false;
            if( !DeepComparable.Matches(Response, otherT.Response)) return false;
            if( !DeepComparable.Matches(NullifyElement, otherT.NullifyElement)) return false;
            if( !DeepComparable.Matches(ReferenceElement, otherT.ReferenceElement)) return false;
            if( !DeepComparable.Matches(Item, otherT.Item)) return false;
            if( !DeepComparable.Matches(IncludeElement, otherT.IncludeElement)) return false;
            if( !DeepComparable.Matches(ExcludeElement, otherT.ExcludeElement)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ProcessRequest;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ActionElement, otherT.ActionElement)) return false;
            if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
            if( !DeepComparable.IsExactly(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.IsExactly(Provider, otherT.Provider)) return false;
            if( !DeepComparable.IsExactly(Organization, otherT.Organization)) return false;
            if( !DeepComparable.IsExactly(Request, otherT.Request)) return false;
            if( !DeepComparable.IsExactly(Response, otherT.Response)) return false;
            if( !DeepComparable.IsExactly(NullifyElement, otherT.NullifyElement)) return false;
            if( !DeepComparable.IsExactly(ReferenceElement, otherT.ReferenceElement)) return false;
            if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
            if( !DeepComparable.IsExactly(IncludeElement, otherT.IncludeElement)) return false;
            if( !DeepComparable.IsExactly(ExcludeElement, otherT.ExcludeElement)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (StatusElement != null) yield return StatusElement;
				if (ActionElement != null) yield return ActionElement;
				if (Target != null) yield return Target;
				if (CreatedElement != null) yield return CreatedElement;
				if (Provider != null) yield return Provider;
				if (Organization != null) yield return Organization;
				if (Request != null) yield return Request;
				if (Response != null) yield return Response;
				if (NullifyElement != null) yield return NullifyElement;
				if (ReferenceElement != null) yield return ReferenceElement;
				foreach (var elem in Item) { if (elem != null) yield return elem; }
				foreach (var elem in IncludeElement) { if (elem != null) yield return elem; }
				foreach (var elem in ExcludeElement) { if (elem != null) yield return elem; }
				if (Period != null) yield return Period;
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (ActionElement != null) yield return new ElementValue("action", ActionElement);
                if (Target != null) yield return new ElementValue("target", Target);
                if (CreatedElement != null) yield return new ElementValue("created", CreatedElement);
                if (Provider != null) yield return new ElementValue("provider", Provider);
                if (Organization != null) yield return new ElementValue("organization", Organization);
                if (Request != null) yield return new ElementValue("request", Request);
                if (Response != null) yield return new ElementValue("response", Response);
                if (NullifyElement != null) yield return new ElementValue("nullify", NullifyElement);
                if (ReferenceElement != null) yield return new ElementValue("reference", ReferenceElement);
                foreach (var elem in Item) { if (elem != null) yield return new ElementValue("item", elem); }
                foreach (var elem in IncludeElement) { if (elem != null) yield return new ElementValue("include", elem); }
                foreach (var elem in ExcludeElement) { if (elem != null) yield return new ElementValue("exclude", elem); }
                if (Period != null) yield return new ElementValue("period", Period);
            }
        }

    }
    
}
