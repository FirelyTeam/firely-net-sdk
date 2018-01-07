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
// Generated for FHIR v3.2.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Information about a user's current session
    /// </summary>
    [FhirType("UserSession", IsResource=true)]
    [DataContract]
    public partial class UserSession : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.UserSession; } }
        [NotMapped]
        public override string TypeName { get { return "UserSession"; } }
        
        /// <summary>
        /// The status of the user session
        /// (url: http://hl7.org/fhir/ValueSet/usersession-status)
        /// </summary>
        [FhirEnumeration("UserSessionStatus")]
        public enum UserSessionStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/usersession-status)
            /// </summary>
            [EnumLiteral("activating", "http://hl7.org/fhir/usersession-status"), Description("Activating")]
            Activating,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/usersession-status)
            /// </summary>
            [EnumLiteral("active", "http://hl7.org/fhir/usersession-status"), Description("Active")]
            Active,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/usersession-status)
            /// </summary>
            [EnumLiteral("suspended", "http://hl7.org/fhir/usersession-status"), Description("Suspending")]
            Suspended,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/usersession-status)
            /// </summary>
            [EnumLiteral("closing", "http://hl7.org/fhir/usersession-status"), Description("Closing")]
            Closing,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/usersession-status)
            /// </summary>
            [EnumLiteral("closed", "http://hl7.org/fhir/usersession-status"), Description("Closed")]
            Closed,
        }

        /// <summary>
        /// The source of the status of the user session
        /// (url: http://hl7.org/fhir/ValueSet/usersession-status-source)
        /// </summary>
        [FhirEnumeration("UserSessionStatusSource")]
        public enum UserSessionStatusSource
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/usersession-status-source)
            /// </summary>
            [EnumLiteral("user", "http://hl7.org/fhir/usersession-status-source"), Description("User")]
            User,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/usersession-status-source)
            /// </summary>
            [EnumLiteral("system", "http://hl7.org/fhir/usersession-status-source"), Description("System")]
            System,
        }

        [FhirType("StatusComponent")]
        [DataContract]
        public partial class StatusComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "StatusComponent"; } }
            
            /// <summary>
            /// activating | active | suspended | closing | closed
            /// </summary>
            [FhirElement("code", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.UserSession.UserSessionStatus> CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.UserSession.UserSessionStatus> _CodeElement;
            
            /// <summary>
            /// activating | active | suspended | closing | closed
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.UserSession.UserSessionStatus? Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        CodeElement = null; 
                    else
                        CodeElement = new Code<Hl7.Fhir.Model.UserSession.UserSessionStatus>(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// user | system
            /// </summary>
            [FhirElement("source", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.UserSession.UserSessionStatusSource> SourceElement
            {
                get { return _SourceElement; }
                set { _SourceElement = value; OnPropertyChanged("SourceElement"); }
            }
            
            private Code<Hl7.Fhir.Model.UserSession.UserSessionStatusSource> _SourceElement;
            
            /// <summary>
            /// user | system
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.UserSession.UserSessionStatusSource? Source
            {
                get { return SourceElement != null ? SourceElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        SourceElement = null; 
                    else
                        SourceElement = new Code<Hl7.Fhir.Model.UserSession.UserSessionStatusSource>(value);
                    OnPropertyChanged("Source");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StatusComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Code<Hl7.Fhir.Model.UserSession.UserSessionStatus>)CodeElement.DeepCopy();
                    if(SourceElement != null) dest.SourceElement = (Code<Hl7.Fhir.Model.UserSession.UserSessionStatusSource>)SourceElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new StatusComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as StatusComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(SourceElement, otherT.SourceElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as StatusComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(SourceElement, otherT.SourceElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    if (SourceElement != null) yield return SourceElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeElement != null) yield return new ElementValue("code", false, CodeElement);
                    if (SourceElement != null) yield return new ElementValue("source", false, SourceElement);
                }
            }

            
        }
        
        
        [FhirType("ContextComponent")]
        [DataContract]
        public partial class ContextComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ContextComponent"; } }
            
            /// <summary>
            /// What type of context value
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TypeElement;
            
            /// <summary>
            /// What type of context value
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (value == null)
                        TypeElement = null; 
                    else
                        TypeElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Value of the context
            /// </summary>
            [FhirElement("value", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Quantity))]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ContextComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.FhirString)TypeElement.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ContextComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ContextComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ContextComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TypeElement != null) yield return TypeElement;
                    if (Value != null) yield return Value;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TypeElement != null) yield return new ElementValue("type", false, TypeElement);
                    if (Value != null) yield return new ElementValue("value", false, Value);
                }
            }

            
        }
        
        
        /// <summary>
        /// Business identifier
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// User engaged in the session
        /// </summary>
        [FhirElement("user", InSummary=true, Order=100)]
        [CLSCompliant(false)]
		[References("Device","Practitioner","Patient","RelatedPerson")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference User
        {
            get { return _User; }
            set { _User = value; OnPropertyChanged("User"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _User;
        
        /// <summary>
        /// Status of the session
        /// </summary>
        [FhirElement("status", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.UserSession.StatusComponent Status
        {
            get { return _Status; }
            set { _Status = value; OnPropertyChanged("Status"); }
        }
        
        private Hl7.Fhir.Model.UserSession.StatusComponent _Status;
        
        /// <summary>
        /// Where is the session
        /// </summary>
        [FhirElement("workstation", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Workstation
        {
            get { return _Workstation; }
            set { _Workstation = value; OnPropertyChanged("Workstation"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Workstation;
        
        /// <summary>
        /// What is the user's current focus
        /// </summary>
        [FhirElement("focus", Order=130)]
        [CLSCompliant(false)]
		[References()]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Focus
        {
            get { if(_Focus==null) _Focus = new List<Hl7.Fhir.Model.ResourceReference>(); return _Focus; }
            set { _Focus = value; OnPropertyChanged("Focus"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Focus;
        
        /// <summary>
        /// When was the session created
        /// </summary>
        [FhirElement("created", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.Instant CreatedElement
        {
            get { return _CreatedElement; }
            set { _CreatedElement = value; OnPropertyChanged("CreatedElement"); }
        }
        
        private Hl7.Fhir.Model.Instant _CreatedElement;
        
        /// <summary>
        /// When was the session created
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public DateTimeOffset? Created
        {
            get { return CreatedElement != null ? CreatedElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  CreatedElement = null; 
                else
                  CreatedElement = new Hl7.Fhir.Model.Instant(value);
                OnPropertyChanged("Created");
            }
        }
        
        /// <summary>
        /// When does the session expire
        /// </summary>
        [FhirElement("expires", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.Instant ExpiresElement
        {
            get { return _ExpiresElement; }
            set { _ExpiresElement = value; OnPropertyChanged("ExpiresElement"); }
        }
        
        private Hl7.Fhir.Model.Instant _ExpiresElement;
        
        /// <summary>
        /// When does the session expire
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public DateTimeOffset? Expires
        {
            get { return ExpiresElement != null ? ExpiresElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  ExpiresElement = null; 
                else
                  ExpiresElement = new Hl7.Fhir.Model.Instant(value);
                OnPropertyChanged("Expires");
            }
        }
        
        /// <summary>
        /// Additional information about the session
        /// </summary>
        [FhirElement("context", Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.UserSession.ContextComponent> Context
        {
            get { if(_Context==null) _Context = new List<Hl7.Fhir.Model.UserSession.ContextComponent>(); return _Context; }
            set { _Context = value; OnPropertyChanged("Context"); }
        }
        
        private List<Hl7.Fhir.Model.UserSession.ContextComponent> _Context;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as UserSession;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(User != null) dest.User = (Hl7.Fhir.Model.ResourceReference)User.DeepCopy();
                if(Status != null) dest.Status = (Hl7.Fhir.Model.UserSession.StatusComponent)Status.DeepCopy();
                if(Workstation != null) dest.Workstation = (Hl7.Fhir.Model.Identifier)Workstation.DeepCopy();
                if(Focus != null) dest.Focus = new List<Hl7.Fhir.Model.ResourceReference>(Focus.DeepCopy());
                if(CreatedElement != null) dest.CreatedElement = (Hl7.Fhir.Model.Instant)CreatedElement.DeepCopy();
                if(ExpiresElement != null) dest.ExpiresElement = (Hl7.Fhir.Model.Instant)ExpiresElement.DeepCopy();
                if(Context != null) dest.Context = new List<Hl7.Fhir.Model.UserSession.ContextComponent>(Context.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new UserSession());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as UserSession;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(User, otherT.User)) return false;
            if( !DeepComparable.Matches(Status, otherT.Status)) return false;
            if( !DeepComparable.Matches(Workstation, otherT.Workstation)) return false;
            if( !DeepComparable.Matches(Focus, otherT.Focus)) return false;
            if( !DeepComparable.Matches(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.Matches(ExpiresElement, otherT.ExpiresElement)) return false;
            if( !DeepComparable.Matches(Context, otherT.Context)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as UserSession;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(User, otherT.User)) return false;
            if( !DeepComparable.IsExactly(Status, otherT.Status)) return false;
            if( !DeepComparable.IsExactly(Workstation, otherT.Workstation)) return false;
            if( !DeepComparable.IsExactly(Focus, otherT.Focus)) return false;
            if( !DeepComparable.IsExactly(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.IsExactly(ExpiresElement, otherT.ExpiresElement)) return false;
            if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (Identifier != null) yield return Identifier;
				if (User != null) yield return User;
				if (Status != null) yield return Status;
				if (Workstation != null) yield return Workstation;
				foreach (var elem in Focus) { if (elem != null) yield return elem; }
				if (CreatedElement != null) yield return CreatedElement;
				if (ExpiresElement != null) yield return ExpiresElement;
				foreach (var elem in Context) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Identifier != null) yield return new ElementValue("identifier", false, Identifier);
                if (User != null) yield return new ElementValue("user", false, User);
                if (Status != null) yield return new ElementValue("status", false, Status);
                if (Workstation != null) yield return new ElementValue("workstation", false, Workstation);
                foreach (var elem in Focus) { if (elem != null) yield return new ElementValue("focus", true, elem); }
                if (CreatedElement != null) yield return new ElementValue("created", false, CreatedElement);
                if (ExpiresElement != null) yield return new ElementValue("expires", false, ExpiresElement);
                foreach (var elem in Context) { if (elem != null) yield return new ElementValue("context", true, elem); }
            }
        }

    }
    
}
