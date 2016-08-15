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
// Generated for FHIR v1.6.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Defines behaviour and contraints on the ValueSet Expansion operation
    /// </summary>
    [FhirType("ExpansionProfile", IsResource=true)]
    [DataContract]
    public partial class ExpansionProfile : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ExpansionProfile; } }
        [NotMapped]
        public override string TypeName { get { return "ExpansionProfile"; } }
        
        [FhirType("ContactComponent")]
        [DataContract]
        public partial class ContactComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ContactComponent"; } }
            
            /// <summary>
            /// Name of an individual to contact
            /// </summary>
            [FhirElement("name", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Name of an individual to contact
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                if (value == null)
                      NameElement = null; 
                    else
                        NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Contact details for individual or publisher
            /// </summary>
            [FhirElement("telecom", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ContactPoint> Telecom
            {
                get { if(_Telecom==null) _Telecom = new List<Hl7.Fhir.Model.ContactPoint>(); return _Telecom; }
                set { _Telecom = value; OnPropertyChanged("Telecom"); }
            }
            
            private List<Hl7.Fhir.Model.ContactPoint> _Telecom;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ContactComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.ContactPoint>(Telecom.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ContactComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ContactComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ContactComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("CodeSystemComponent")]
        [DataContract]
        public partial class CodeSystemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CodeSystemComponent"; } }
            
            /// <summary>
            /// Code systems to be included
            /// </summary>
            [FhirElement("include", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.ExpansionProfile.CodeSystemIncludeComponent Include
            {
                get { return _Include; }
                set { _Include = value; OnPropertyChanged("Include"); }
            }
            
            private Hl7.Fhir.Model.ExpansionProfile.CodeSystemIncludeComponent _Include;
            
            /// <summary>
            /// Code systems to be excluded
            /// </summary>
            [FhirElement("exclude", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.ExpansionProfile.CodeSystemExcludeComponent Exclude
            {
                get { return _Exclude; }
                set { _Exclude = value; OnPropertyChanged("Exclude"); }
            }
            
            private Hl7.Fhir.Model.ExpansionProfile.CodeSystemExcludeComponent _Exclude;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CodeSystemComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Include != null) dest.Include = (Hl7.Fhir.Model.ExpansionProfile.CodeSystemIncludeComponent)Include.DeepCopy();
                    if(Exclude != null) dest.Exclude = (Hl7.Fhir.Model.ExpansionProfile.CodeSystemExcludeComponent)Exclude.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CodeSystemComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CodeSystemComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Include, otherT.Include)) return false;
                if( !DeepComparable.Matches(Exclude, otherT.Exclude)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CodeSystemComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Include, otherT.Include)) return false;
                if( !DeepComparable.IsExactly(Exclude, otherT.Exclude)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("CodeSystemIncludeComponent")]
        [DataContract]
        public partial class CodeSystemIncludeComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CodeSystemIncludeComponent"; } }
            
            /// <summary>
            /// The code systems to be included
            /// </summary>
            [FhirElement("codeSystem", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ExpansionProfile.CodeSystemIncludeCodeSystemComponent> CodeSystem
            {
                get { if(_CodeSystem==null) _CodeSystem = new List<Hl7.Fhir.Model.ExpansionProfile.CodeSystemIncludeCodeSystemComponent>(); return _CodeSystem; }
                set { _CodeSystem = value; OnPropertyChanged("CodeSystem"); }
            }
            
            private List<Hl7.Fhir.Model.ExpansionProfile.CodeSystemIncludeCodeSystemComponent> _CodeSystem;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CodeSystemIncludeComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeSystem != null) dest.CodeSystem = new List<Hl7.Fhir.Model.ExpansionProfile.CodeSystemIncludeCodeSystemComponent>(CodeSystem.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CodeSystemIncludeComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CodeSystemIncludeComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeSystem, otherT.CodeSystem)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CodeSystemIncludeComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeSystem, otherT.CodeSystem)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("CodeSystemIncludeCodeSystemComponent")]
        [DataContract]
        public partial class CodeSystemIncludeCodeSystemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CodeSystemIncludeCodeSystemComponent"; } }
            
            /// <summary>
            /// The specific code system to be included
            /// </summary>
            [FhirElement("system", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri SystemElement
            {
                get { return _SystemElement; }
                set { _SystemElement = value; OnPropertyChanged("SystemElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _SystemElement;
            
            /// <summary>
            /// The specific code system to be included
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string System
            {
                get { return SystemElement != null ? SystemElement.Value : null; }
                set
                {
                if (value == null)
                      SystemElement = null; 
                    else
                        SystemElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("System");
                }
            }
            
            /// <summary>
            /// Specific version of the code system referred to
            /// </summary>
            [FhirElement("version", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VersionElement
            {
                get { return _VersionElement; }
                set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _VersionElement;
            
            /// <summary>
            /// Specific version of the code system referred to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Version
            {
                get { return VersionElement != null ? VersionElement.Value : null; }
                set
                {
                if (value == null)
                      VersionElement = null; 
                    else
                        VersionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Version");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CodeSystemIncludeCodeSystemComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SystemElement != null) dest.SystemElement = (Hl7.Fhir.Model.FhirUri)SystemElement.DeepCopy();
                    if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CodeSystemIncludeCodeSystemComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CodeSystemIncludeCodeSystemComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SystemElement, otherT.SystemElement)) return false;
                if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CodeSystemIncludeCodeSystemComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SystemElement, otherT.SystemElement)) return false;
                if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("CodeSystemExcludeComponent")]
        [DataContract]
        public partial class CodeSystemExcludeComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CodeSystemExcludeComponent"; } }
            
            /// <summary>
            /// The code systems to be excluded
            /// </summary>
            [FhirElement("codeSystem", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ExpansionProfile.CodeSystemExcludeCodeSystemComponent> CodeSystem
            {
                get { if(_CodeSystem==null) _CodeSystem = new List<Hl7.Fhir.Model.ExpansionProfile.CodeSystemExcludeCodeSystemComponent>(); return _CodeSystem; }
                set { _CodeSystem = value; OnPropertyChanged("CodeSystem"); }
            }
            
            private List<Hl7.Fhir.Model.ExpansionProfile.CodeSystemExcludeCodeSystemComponent> _CodeSystem;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CodeSystemExcludeComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeSystem != null) dest.CodeSystem = new List<Hl7.Fhir.Model.ExpansionProfile.CodeSystemExcludeCodeSystemComponent>(CodeSystem.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CodeSystemExcludeComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CodeSystemExcludeComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeSystem, otherT.CodeSystem)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CodeSystemExcludeComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeSystem, otherT.CodeSystem)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("CodeSystemExcludeCodeSystemComponent")]
        [DataContract]
        public partial class CodeSystemExcludeCodeSystemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CodeSystemExcludeCodeSystemComponent"; } }
            
            /// <summary>
            /// The specific code system to be excluded
            /// </summary>
            [FhirElement("system", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri SystemElement
            {
                get { return _SystemElement; }
                set { _SystemElement = value; OnPropertyChanged("SystemElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _SystemElement;
            
            /// <summary>
            /// The specific code system to be excluded
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string System
            {
                get { return SystemElement != null ? SystemElement.Value : null; }
                set
                {
                if (value == null)
                      SystemElement = null; 
                    else
                        SystemElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("System");
                }
            }
            
            /// <summary>
            /// Specific version of the code system referred to
            /// </summary>
            [FhirElement("version", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VersionElement
            {
                get { return _VersionElement; }
                set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _VersionElement;
            
            /// <summary>
            /// Specific version of the code system referred to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Version
            {
                get { return VersionElement != null ? VersionElement.Value : null; }
                set
                {
                if (value == null)
                      VersionElement = null; 
                    else
                        VersionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Version");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CodeSystemExcludeCodeSystemComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SystemElement != null) dest.SystemElement = (Hl7.Fhir.Model.FhirUri)SystemElement.DeepCopy();
                    if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CodeSystemExcludeCodeSystemComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CodeSystemExcludeCodeSystemComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SystemElement, otherT.SystemElement)) return false;
                if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CodeSystemExcludeCodeSystemComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SystemElement, otherT.SystemElement)) return false;
                if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("DesignationComponent")]
        [DataContract]
        public partial class DesignationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DesignationComponent"; } }
            
            /// <summary>
            /// Designations to be included
            /// </summary>
            [FhirElement("include", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.ExpansionProfile.DesignationIncludeComponent Include
            {
                get { return _Include; }
                set { _Include = value; OnPropertyChanged("Include"); }
            }
            
            private Hl7.Fhir.Model.ExpansionProfile.DesignationIncludeComponent _Include;
            
            /// <summary>
            /// Designations to be excluded
            /// </summary>
            [FhirElement("exclude", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.ExpansionProfile.DesignationExcludeComponent Exclude
            {
                get { return _Exclude; }
                set { _Exclude = value; OnPropertyChanged("Exclude"); }
            }
            
            private Hl7.Fhir.Model.ExpansionProfile.DesignationExcludeComponent _Exclude;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DesignationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Include != null) dest.Include = (Hl7.Fhir.Model.ExpansionProfile.DesignationIncludeComponent)Include.DeepCopy();
                    if(Exclude != null) dest.Exclude = (Hl7.Fhir.Model.ExpansionProfile.DesignationExcludeComponent)Exclude.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DesignationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DesignationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Include, otherT.Include)) return false;
                if( !DeepComparable.Matches(Exclude, otherT.Exclude)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DesignationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Include, otherT.Include)) return false;
                if( !DeepComparable.IsExactly(Exclude, otherT.Exclude)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("DesignationIncludeComponent")]
        [DataContract]
        public partial class DesignationIncludeComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DesignationIncludeComponent"; } }
            
            /// <summary>
            /// The designation to be included
            /// </summary>
            [FhirElement("designation", InSummary=true, Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ExpansionProfile.DesignationIncludeDesignationComponent> Designation
            {
                get { if(_Designation==null) _Designation = new List<Hl7.Fhir.Model.ExpansionProfile.DesignationIncludeDesignationComponent>(); return _Designation; }
                set { _Designation = value; OnPropertyChanged("Designation"); }
            }
            
            private List<Hl7.Fhir.Model.ExpansionProfile.DesignationIncludeDesignationComponent> _Designation;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DesignationIncludeComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Designation != null) dest.Designation = new List<Hl7.Fhir.Model.ExpansionProfile.DesignationIncludeDesignationComponent>(Designation.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DesignationIncludeComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DesignationIncludeComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Designation, otherT.Designation)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DesignationIncludeComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Designation, otherT.Designation)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("DesignationIncludeDesignationComponent")]
        [DataContract]
        public partial class DesignationIncludeDesignationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DesignationIncludeDesignationComponent"; } }
            
            /// <summary>
            /// Human language of the designation to be included
            /// </summary>
            [FhirElement("language", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Code LanguageElement
            {
                get { return _LanguageElement; }
                set { _LanguageElement = value; OnPropertyChanged("LanguageElement"); }
            }
            
            private Hl7.Fhir.Model.Code _LanguageElement;
            
            /// <summary>
            /// Human language of the designation to be included
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Language
            {
                get { return LanguageElement != null ? LanguageElement.Value : null; }
                set
                {
                if (value == null)
                      LanguageElement = null; 
                    else
                        LanguageElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Language");
                }
            }
            
            /// <summary>
            /// Designation use
            /// </summary>
            [FhirElement("use", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Use
            {
                get { return _Use; }
                set { _Use = value; OnPropertyChanged("Use"); }
            }
            
            private Hl7.Fhir.Model.Coding _Use;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DesignationIncludeDesignationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(LanguageElement != null) dest.LanguageElement = (Hl7.Fhir.Model.Code)LanguageElement.DeepCopy();
                    if(Use != null) dest.Use = (Hl7.Fhir.Model.Coding)Use.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DesignationIncludeDesignationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DesignationIncludeDesignationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(LanguageElement, otherT.LanguageElement)) return false;
                if( !DeepComparable.Matches(Use, otherT.Use)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DesignationIncludeDesignationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(LanguageElement, otherT.LanguageElement)) return false;
                if( !DeepComparable.IsExactly(Use, otherT.Use)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("DesignationExcludeComponent")]
        [DataContract]
        public partial class DesignationExcludeComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DesignationExcludeComponent"; } }
            
            /// <summary>
            /// The designation to be excluded
            /// </summary>
            [FhirElement("designation", InSummary=true, Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ExpansionProfile.DesignationExcludeDesignationComponent> Designation
            {
                get { if(_Designation==null) _Designation = new List<Hl7.Fhir.Model.ExpansionProfile.DesignationExcludeDesignationComponent>(); return _Designation; }
                set { _Designation = value; OnPropertyChanged("Designation"); }
            }
            
            private List<Hl7.Fhir.Model.ExpansionProfile.DesignationExcludeDesignationComponent> _Designation;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DesignationExcludeComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Designation != null) dest.Designation = new List<Hl7.Fhir.Model.ExpansionProfile.DesignationExcludeDesignationComponent>(Designation.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DesignationExcludeComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DesignationExcludeComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Designation, otherT.Designation)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DesignationExcludeComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Designation, otherT.Designation)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("DesignationExcludeDesignationComponent")]
        [DataContract]
        public partial class DesignationExcludeDesignationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DesignationExcludeDesignationComponent"; } }
            
            /// <summary>
            /// Human language of the designation to be excluded
            /// </summary>
            [FhirElement("language", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Code LanguageElement
            {
                get { return _LanguageElement; }
                set { _LanguageElement = value; OnPropertyChanged("LanguageElement"); }
            }
            
            private Hl7.Fhir.Model.Code _LanguageElement;
            
            /// <summary>
            /// Human language of the designation to be excluded
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Language
            {
                get { return LanguageElement != null ? LanguageElement.Value : null; }
                set
                {
                if (value == null)
                      LanguageElement = null; 
                    else
                        LanguageElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Language");
                }
            }
            
            /// <summary>
            /// Designation use
            /// </summary>
            [FhirElement("use", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Use
            {
                get { return _Use; }
                set { _Use = value; OnPropertyChanged("Use"); }
            }
            
            private Hl7.Fhir.Model.Coding _Use;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DesignationExcludeDesignationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(LanguageElement != null) dest.LanguageElement = (Hl7.Fhir.Model.Code)LanguageElement.DeepCopy();
                    if(Use != null) dest.Use = (Hl7.Fhir.Model.Coding)Use.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DesignationExcludeDesignationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DesignationExcludeDesignationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(LanguageElement, otherT.LanguageElement)) return false;
                if( !DeepComparable.Matches(Use, otherT.Use)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DesignationExcludeDesignationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(LanguageElement, otherT.LanguageElement)) return false;
                if( !DeepComparable.IsExactly(Use, otherT.Use)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Globally unique logical identifier for  expansion profile
        /// </summary>
        [FhirElement("url", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _UrlElement;
        
        /// <summary>
        /// Globally unique logical identifier for  expansion profile
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Url
        {
            get { return UrlElement != null ? UrlElement.Value : null; }
            set
            {
                if (value == null)
                  UrlElement = null; 
                else
                  UrlElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Url");
            }
        }
        
        /// <summary>
        /// Additional identifier for the expansion profile (e.g. an Object Identifier)
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// Logical identifier for this version of the expansion profile
        /// </summary>
        [FhirElement("version", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// Logical identifier for this version of the expansion profile
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Version
        {
            get { return VersionElement != null ? VersionElement.Value : null; }
            set
            {
                if (value == null)
                  VersionElement = null; 
                else
                  VersionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Version");
            }
        }
        
        /// <summary>
        /// Informal name for this expansion profile
        /// </summary>
        [FhirElement("name", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Informal name for this expansion profile
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Name
        {
            get { return NameElement != null ? NameElement.Value : null; }
            set
            {
                if (value == null)
                  NameElement = null; 
                else
                  NameElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Name");
            }
        }
        
        /// <summary>
        /// draft | active | retired
        /// </summary>
        [FhirElement("status", InSummary=true, Order=130)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ConformanceResourceStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.ConformanceResourceStatus> _StatusElement;
        
        /// <summary>
        /// draft | active | retired
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ConformanceResourceStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.ConformanceResourceStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// If for testing purposes, not real usage
        /// </summary>
        [FhirElement("experimental", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ExperimentalElement
        {
            get { return _ExperimentalElement; }
            set { _ExperimentalElement = value; OnPropertyChanged("ExperimentalElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _ExperimentalElement;
        
        /// <summary>
        /// If for testing purposes, not real usage
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Experimental
        {
            get { return ExperimentalElement != null ? ExperimentalElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  ExperimentalElement = null; 
                else
                  ExperimentalElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Experimental");
            }
        }
        
        /// <summary>
        /// Name of the publisher (organization or individual)
        /// </summary>
        [FhirElement("publisher", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PublisherElement
        {
            get { return _PublisherElement; }
            set { _PublisherElement = value; OnPropertyChanged("PublisherElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PublisherElement;
        
        /// <summary>
        /// Name of the publisher (organization or individual)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Publisher
        {
            get { return PublisherElement != null ? PublisherElement.Value : null; }
            set
            {
                if (value == null)
                  PublisherElement = null; 
                else
                  PublisherElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Publisher");
            }
        }
        
        /// <summary>
        /// Contact details of the publisher
        /// </summary>
        [FhirElement("contact", InSummary=true, Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ExpansionProfile.ContactComponent> Contact
        {
            get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.ExpansionProfile.ContactComponent>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<Hl7.Fhir.Model.ExpansionProfile.ContactComponent> _Contact;
        
        /// <summary>
        /// Date for given status
        /// </summary>
        [FhirElement("date", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// Date for given status
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
                  DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Date");
            }
        }
        
        /// <summary>
        /// Human language description of the expansion profile
        /// </summary>
        [FhirElement("description", InSummary=true, Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Description
        {
            get { return _Description; }
            set { _Description = value; OnPropertyChanged("Description"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Description;
        
        /// <summary>
        /// When the expansion profile imposes code system contraints
        /// </summary>
        [FhirElement("codeSystem", InSummary=true, Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.ExpansionProfile.CodeSystemComponent CodeSystem
        {
            get { return _CodeSystem; }
            set { _CodeSystem = value; OnPropertyChanged("CodeSystem"); }
        }
        
        private Hl7.Fhir.Model.ExpansionProfile.CodeSystemComponent _CodeSystem;
        
        /// <summary>
        /// Whether the expansion should include concept designations
        /// </summary>
        [FhirElement("includeDesignations", InSummary=true, Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean IncludeDesignationsElement
        {
            get { return _IncludeDesignationsElement; }
            set { _IncludeDesignationsElement = value; OnPropertyChanged("IncludeDesignationsElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _IncludeDesignationsElement;
        
        /// <summary>
        /// Whether the expansion should include concept designations
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? IncludeDesignations
        {
            get { return IncludeDesignationsElement != null ? IncludeDesignationsElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  IncludeDesignationsElement = null; 
                else
                  IncludeDesignationsElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("IncludeDesignations");
            }
        }
        
        /// <summary>
        /// When the expansion profile imposes designation contraints
        /// </summary>
        [FhirElement("designation", InSummary=true, Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.ExpansionProfile.DesignationComponent Designation
        {
            get { return _Designation; }
            set { _Designation = value; OnPropertyChanged("Designation"); }
        }
        
        private Hl7.Fhir.Model.ExpansionProfile.DesignationComponent _Designation;
        
        /// <summary>
        /// Include or exclude the value set definition in the expansion
        /// </summary>
        [FhirElement("includeDefinition", InSummary=true, Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean IncludeDefinitionElement
        {
            get { return _IncludeDefinitionElement; }
            set { _IncludeDefinitionElement = value; OnPropertyChanged("IncludeDefinitionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _IncludeDefinitionElement;
        
        /// <summary>
        /// Include or exclude the value set definition in the expansion
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? IncludeDefinition
        {
            get { return IncludeDefinitionElement != null ? IncludeDefinitionElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  IncludeDefinitionElement = null; 
                else
                  IncludeDefinitionElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("IncludeDefinition");
            }
        }
        
        /// <summary>
        /// Include or exclude inactive concepts in the expansion
        /// </summary>
        [FhirElement("includeInactive", InSummary=true, Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean IncludeInactiveElement
        {
            get { return _IncludeInactiveElement; }
            set { _IncludeInactiveElement = value; OnPropertyChanged("IncludeInactiveElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _IncludeInactiveElement;
        
        /// <summary>
        /// Include or exclude inactive concepts in the expansion
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? IncludeInactive
        {
            get { return IncludeInactiveElement != null ? IncludeInactiveElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  IncludeInactiveElement = null; 
                else
                  IncludeInactiveElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("IncludeInactive");
            }
        }
        
        /// <summary>
        /// Include or exclude nested codes in the value set expansion
        /// </summary>
        [FhirElement("excludeNested", InSummary=true, Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ExcludeNestedElement
        {
            get { return _ExcludeNestedElement; }
            set { _ExcludeNestedElement = value; OnPropertyChanged("ExcludeNestedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _ExcludeNestedElement;
        
        /// <summary>
        /// Include or exclude nested codes in the value set expansion
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? ExcludeNested
        {
            get { return ExcludeNestedElement != null ? ExcludeNestedElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  ExcludeNestedElement = null; 
                else
                  ExcludeNestedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("ExcludeNested");
            }
        }
        
        /// <summary>
        /// Include or exclude codes which cannot be rendered in user interfaces in the value set expansion
        /// </summary>
        [FhirElement("excludeNotForUI", InSummary=true, Order=250)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ExcludeNotForUIElement
        {
            get { return _ExcludeNotForUIElement; }
            set { _ExcludeNotForUIElement = value; OnPropertyChanged("ExcludeNotForUIElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _ExcludeNotForUIElement;
        
        /// <summary>
        /// Include or exclude codes which cannot be rendered in user interfaces in the value set expansion
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? ExcludeNotForUI
        {
            get { return ExcludeNotForUIElement != null ? ExcludeNotForUIElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  ExcludeNotForUIElement = null; 
                else
                  ExcludeNotForUIElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("ExcludeNotForUI");
            }
        }
        
        /// <summary>
        /// Include or exclude codes which are post coordinated expressions in the value set expansion
        /// </summary>
        [FhirElement("excludePostCoordinated", InSummary=true, Order=260)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ExcludePostCoordinatedElement
        {
            get { return _ExcludePostCoordinatedElement; }
            set { _ExcludePostCoordinatedElement = value; OnPropertyChanged("ExcludePostCoordinatedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _ExcludePostCoordinatedElement;
        
        /// <summary>
        /// Include or exclude codes which are post coordinated expressions in the value set expansion
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? ExcludePostCoordinated
        {
            get { return ExcludePostCoordinatedElement != null ? ExcludePostCoordinatedElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  ExcludePostCoordinatedElement = null; 
                else
                  ExcludePostCoordinatedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("ExcludePostCoordinated");
            }
        }
        
        /// <summary>
        /// Specify the language for the display element of codes in the value set expansion
        /// </summary>
        [FhirElement("displayLanguage", InSummary=true, Order=270)]
        [DataMember]
        public Hl7.Fhir.Model.Code DisplayLanguageElement
        {
            get { return _DisplayLanguageElement; }
            set { _DisplayLanguageElement = value; OnPropertyChanged("DisplayLanguageElement"); }
        }
        
        private Hl7.Fhir.Model.Code _DisplayLanguageElement;
        
        /// <summary>
        /// Specify the language for the display element of codes in the value set expansion
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string DisplayLanguage
        {
            get { return DisplayLanguageElement != null ? DisplayLanguageElement.Value : null; }
            set
            {
                if (value == null)
                  DisplayLanguageElement = null; 
                else
                  DisplayLanguageElement = new Hl7.Fhir.Model.Code(value);
                OnPropertyChanged("DisplayLanguage");
            }
        }
        
        /// <summary>
        /// Controls behaviour of the value set expand operation when value sets are too large to be completely expanded
        /// </summary>
        [FhirElement("limitedExpansion", InSummary=true, Order=280)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean LimitedExpansionElement
        {
            get { return _LimitedExpansionElement; }
            set { _LimitedExpansionElement = value; OnPropertyChanged("LimitedExpansionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _LimitedExpansionElement;
        
        /// <summary>
        /// Controls behaviour of the value set expand operation when value sets are too large to be completely expanded
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? LimitedExpansion
        {
            get { return LimitedExpansionElement != null ? LimitedExpansionElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  LimitedExpansionElement = null; 
                else
                  LimitedExpansionElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("LimitedExpansion");
            }
        }
        

        public static ElementDefinition.ConstraintComponent ExpansionProfile_EXP_1 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "codeSystem.all(include.empty() or exclude.empty())",
            Key = "exp-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "SHALL NOT have include and exclude",
            Xpath = "not(exists(f:include)) or not(exists(f:exclude))"
        };

        public static ElementDefinition.ConstraintComponent ExpansionProfile_EXP_2 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "designation.all(include.empty() or exclude.empty())",
            Key = "exp-2",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "SHALL NOT have include and exclude",
            Xpath = "not(exists(f:include)) or not(exists(f:exclude))"
        };

        public static ElementDefinition.ConstraintComponent ExpansionProfile_EXP_3 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "designation.include.designation.all(language.empty().not() or use.empty().not())",
            Key = "exp-3",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "SHALL have at least one of language or use",
            Xpath = "exists(f:language) or exists(f:use)"
        };

        public static ElementDefinition.ConstraintComponent ExpansionProfile_EXP_4 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "designation.exclude.designation.all(language.empty().not() or use.empty().not())",
            Key = "exp-4",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "SHALL have at least one of language or use",
            Xpath = "exists(f:language) or exists(f:use)"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(ExpansionProfile_EXP_1);
            InvariantConstraints.Add(ExpansionProfile_EXP_2);
            InvariantConstraints.Add(ExpansionProfile_EXP_3);
            InvariantConstraints.Add(ExpansionProfile_EXP_4);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ExpansionProfile;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.ConformanceResourceStatus>)StatusElement.DeepCopy();
                if(ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
                if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.ExpansionProfile.ContactComponent>(Contact.DeepCopy());
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(Description != null) dest.Description = (Hl7.Fhir.Model.Markdown)Description.DeepCopy();
                if(CodeSystem != null) dest.CodeSystem = (Hl7.Fhir.Model.ExpansionProfile.CodeSystemComponent)CodeSystem.DeepCopy();
                if(IncludeDesignationsElement != null) dest.IncludeDesignationsElement = (Hl7.Fhir.Model.FhirBoolean)IncludeDesignationsElement.DeepCopy();
                if(Designation != null) dest.Designation = (Hl7.Fhir.Model.ExpansionProfile.DesignationComponent)Designation.DeepCopy();
                if(IncludeDefinitionElement != null) dest.IncludeDefinitionElement = (Hl7.Fhir.Model.FhirBoolean)IncludeDefinitionElement.DeepCopy();
                if(IncludeInactiveElement != null) dest.IncludeInactiveElement = (Hl7.Fhir.Model.FhirBoolean)IncludeInactiveElement.DeepCopy();
                if(ExcludeNestedElement != null) dest.ExcludeNestedElement = (Hl7.Fhir.Model.FhirBoolean)ExcludeNestedElement.DeepCopy();
                if(ExcludeNotForUIElement != null) dest.ExcludeNotForUIElement = (Hl7.Fhir.Model.FhirBoolean)ExcludeNotForUIElement.DeepCopy();
                if(ExcludePostCoordinatedElement != null) dest.ExcludePostCoordinatedElement = (Hl7.Fhir.Model.FhirBoolean)ExcludePostCoordinatedElement.DeepCopy();
                if(DisplayLanguageElement != null) dest.DisplayLanguageElement = (Hl7.Fhir.Model.Code)DisplayLanguageElement.DeepCopy();
                if(LimitedExpansionElement != null) dest.LimitedExpansionElement = (Hl7.Fhir.Model.FhirBoolean)LimitedExpansionElement.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new ExpansionProfile());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ExpansionProfile;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(Description, otherT.Description)) return false;
            if( !DeepComparable.Matches(CodeSystem, otherT.CodeSystem)) return false;
            if( !DeepComparable.Matches(IncludeDesignationsElement, otherT.IncludeDesignationsElement)) return false;
            if( !DeepComparable.Matches(Designation, otherT.Designation)) return false;
            if( !DeepComparable.Matches(IncludeDefinitionElement, otherT.IncludeDefinitionElement)) return false;
            if( !DeepComparable.Matches(IncludeInactiveElement, otherT.IncludeInactiveElement)) return false;
            if( !DeepComparable.Matches(ExcludeNestedElement, otherT.ExcludeNestedElement)) return false;
            if( !DeepComparable.Matches(ExcludeNotForUIElement, otherT.ExcludeNotForUIElement)) return false;
            if( !DeepComparable.Matches(ExcludePostCoordinatedElement, otherT.ExcludePostCoordinatedElement)) return false;
            if( !DeepComparable.Matches(DisplayLanguageElement, otherT.DisplayLanguageElement)) return false;
            if( !DeepComparable.Matches(LimitedExpansionElement, otherT.LimitedExpansionElement)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ExpansionProfile;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(Description, otherT.Description)) return false;
            if( !DeepComparable.IsExactly(CodeSystem, otherT.CodeSystem)) return false;
            if( !DeepComparable.IsExactly(IncludeDesignationsElement, otherT.IncludeDesignationsElement)) return false;
            if( !DeepComparable.IsExactly(Designation, otherT.Designation)) return false;
            if( !DeepComparable.IsExactly(IncludeDefinitionElement, otherT.IncludeDefinitionElement)) return false;
            if( !DeepComparable.IsExactly(IncludeInactiveElement, otherT.IncludeInactiveElement)) return false;
            if( !DeepComparable.IsExactly(ExcludeNestedElement, otherT.ExcludeNestedElement)) return false;
            if( !DeepComparable.IsExactly(ExcludeNotForUIElement, otherT.ExcludeNotForUIElement)) return false;
            if( !DeepComparable.IsExactly(ExcludePostCoordinatedElement, otherT.ExcludePostCoordinatedElement)) return false;
            if( !DeepComparable.IsExactly(DisplayLanguageElement, otherT.DisplayLanguageElement)) return false;
            if( !DeepComparable.IsExactly(LimitedExpansionElement, otherT.LimitedExpansionElement)) return false;
            
            return true;
        }
        
    }
    
}
