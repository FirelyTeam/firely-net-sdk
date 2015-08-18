using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

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
// Generated on Tue, Aug 18, 2015 10:39+0200 for FHIR v0.5.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Describes a set of tests
    /// </summary>
    [FhirType("TestScript", IsResource=true)]
    [DataContract]
    public partial class TestScript : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.TestScript; } }
        [NotMapped]
        public override string TypeName { get { return "TestScript"; } }
        
        /// <summary>
        /// The type of direction to use for assertion.
        /// </summary>
        [FhirEnumeration("AssertionDirectionType")]
        public enum AssertionDirectionType
        {
            /// <summary>
            /// Default value. Assertion is evaluated on the response.
            /// </summary>
            [EnumLiteral("response")]
            Response,
            /// <summary>
            /// Not equals comparison.
            /// </summary>
            [EnumLiteral("request")]
            Request,
        }
        
        /// <summary>
        /// The type of response code to use for assertion.
        /// </summary>
        [FhirEnumeration("AssertionResponseTypes")]
        public enum AssertionResponseTypes
        {
            /// <summary>
            /// Response code is 200.
            /// </summary>
            [EnumLiteral("okay")]
            Okay,
            /// <summary>
            /// Response code is 201.
            /// </summary>
            [EnumLiteral("created")]
            Created,
            /// <summary>
            /// Response code is 204.
            /// </summary>
            [EnumLiteral("noContent")]
            NoContent,
            /// <summary>
            /// Response code is 304.
            /// </summary>
            [EnumLiteral("notModified")]
            NotModified,
            /// <summary>
            /// Response code is 400.
            /// </summary>
            [EnumLiteral("bad")]
            Bad,
            /// <summary>
            /// Response code is 403.
            /// </summary>
            [EnumLiteral("forbidden")]
            Forbidden,
            /// <summary>
            /// Response code is 404.
            /// </summary>
            [EnumLiteral("notFound")]
            NotFound,
            /// <summary>
            /// Response code is 405.
            /// </summary>
            [EnumLiteral("methodNotAllowed")]
            MethodNotAllowed,
            /// <summary>
            /// Response code is 409.
            /// </summary>
            [EnumLiteral("conflict")]
            Conflict,
            /// <summary>
            /// Response code is 410.
            /// </summary>
            [EnumLiteral("gone")]
            Gone,
            /// <summary>
            /// Response code is 412.
            /// </summary>
            [EnumLiteral("preconditionFailed")]
            PreconditionFailed,
            /// <summary>
            /// Response code is 422.
            /// </summary>
            [EnumLiteral("unprocessable")]
            Unprocessable,
        }
        
        /// <summary>
        /// The type of operator to use for assertion.
        /// </summary>
        [FhirEnumeration("AssertionOperatorType")]
        public enum AssertionOperatorType
        {
            /// <summary>
            /// Default value. Equals comparison.
            /// </summary>
            [EnumLiteral("equals")]
            Equals,
            /// <summary>
            /// Not equals comparison.
            /// </summary>
            [EnumLiteral("notEquals")]
            NotEquals,
            /// <summary>
            /// Compare value within a known set of values.
            /// </summary>
            [EnumLiteral("in")]
            In,
            /// <summary>
            /// Compare value not within a known set of values.
            /// </summary>
            [EnumLiteral("notIn")]
            NotIn,
            /// <summary>
            /// Compare value to be greater than a known value.
            /// </summary>
            [EnumLiteral("greaterThan")]
            GreaterThan,
            /// <summary>
            /// Compare value to be less than a known value.
            /// </summary>
            [EnumLiteral("lessThan")]
            LessThan,
            /// <summary>
            /// Compare value is empty.
            /// </summary>
            [EnumLiteral("empty")]
            Empty,
            /// <summary>
            /// Compare value is not empty.
            /// </summary>
            [EnumLiteral("notEmpty")]
            NotEmpty,
            /// <summary>
            /// Compare value string contains a known value.
            /// </summary>
            [EnumLiteral("contains")]
            Contains,
            /// <summary>
            /// Compare value string does not contain a known value.
            /// </summary>
            [EnumLiteral("notContains")]
            NotContains,
        }
        
        /// <summary>
        /// The content or mime type.
        /// </summary>
        [FhirEnumeration("ContentType")]
        public enum ContentType
        {
            /// <summary>
            /// XML content-type corresponding to the application/xml+fhir mime-type
            /// </summary>
            [EnumLiteral("xml")]
            Xml,
            /// <summary>
            /// JSON content-type corresponding to the application/json+fhir mime-type
            /// </summary>
            [EnumLiteral("json")]
            Json,
        }
        
        [FhirType("TestScriptSetupActionOperationRequestHeaderComponent")]
        [DataContract]
        public partial class TestScriptSetupActionOperationRequestHeaderComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TestScriptSetupActionOperationRequestHeaderComponent"; } }
            
            /// <summary>
            /// Header field name
            /// </summary>
            [FhirElement("field", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString FieldElement
            {
                get { return _FieldElement; }
                set { _FieldElement = value; OnPropertyChanged("FieldElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _FieldElement;
            
            /// <summary>
            /// Header field name
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Field
            {
                get { return FieldElement != null ? FieldElement.Value : null; }
                set
                {
                    if(value == null)
                      FieldElement = null; 
                    else
                      FieldElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Field");
                }
            }
            
            /// <summary>
            /// Header value
            /// </summary>
            [FhirElement("value", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ValueElement;
            
            /// <summary>
            /// Header value
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Value
            {
                get { return ValueElement != null ? ValueElement.Value : null; }
                set
                {
                    if(value == null)
                      ValueElement = null; 
                    else
                      ValueElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Value");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestScriptSetupActionOperationRequestHeaderComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(FieldElement != null) dest.FieldElement = (Hl7.Fhir.Model.FhirString)FieldElement.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TestScriptSetupActionOperationRequestHeaderComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestScriptSetupActionOperationRequestHeaderComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(FieldElement, otherT.FieldElement)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestScriptSetupActionOperationRequestHeaderComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(FieldElement, otherT.FieldElement)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("TestScriptMetadataLinkComponent")]
        [DataContract]
        public partial class TestScriptMetadataLinkComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TestScriptMetadataLinkComponent"; } }
            
            /// <summary>
            /// URL to the specification
            /// </summary>
            [FhirElement("url", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UrlElement
            {
                get { return _UrlElement; }
                set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _UrlElement;
            
            /// <summary>
            /// URL to the specification
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Url
            {
                get { return UrlElement != null ? UrlElement.Value : null; }
                set
                {
                    if(value == null)
                      UrlElement = null; 
                    else
                      UrlElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Url");
                }
            }
            
            /// <summary>
            /// Short description
            /// </summary>
            [FhirElement("description", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Short description
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Description
            {
                get { return DescriptionElement != null ? DescriptionElement.Value : null; }
                set
                {
                    if(value == null)
                      DescriptionElement = null; 
                    else
                      DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestScriptMetadataLinkComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TestScriptMetadataLinkComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestScriptMetadataLinkComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestScriptMetadataLinkComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("TestScriptTeardownComponent")]
        [DataContract]
        public partial class TestScriptTeardownComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TestScriptTeardownComponent"; } }
            
            /// <summary>
            /// Action
            /// </summary>
            [FhirElement("action", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestScript.TestScriptTeardownActionComponent> Action
            {
                get { if(_Action==null) _Action = new List<Hl7.Fhir.Model.TestScript.TestScriptTeardownActionComponent>(); return _Action; }
                set { _Action = value; OnPropertyChanged("Action"); }
            }
            
            private List<Hl7.Fhir.Model.TestScript.TestScriptTeardownActionComponent> _Action;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestScriptTeardownComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Action != null) dest.Action = new List<Hl7.Fhir.Model.TestScript.TestScriptTeardownActionComponent>(Action.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TestScriptTeardownComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestScriptTeardownComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Action, otherT.Action)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestScriptTeardownComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("TestScriptTestActionComponent")]
        [DataContract]
        public partial class TestScriptTestActionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TestScriptTestActionComponent"; } }
            
            /// <summary>
            /// Operation
            /// </summary>
            [FhirElement("operation", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.TestScript.TestScriptSetupActionOperationComponent Operation
            {
                get { return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            
            private Hl7.Fhir.Model.TestScript.TestScriptSetupActionOperationComponent _Operation;
            
            /// <summary>
            /// Assertion
            /// </summary>
            [FhirElement("assert", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.TestScript.TestScriptSetupActionAssertComponent Assert
            {
                get { return _Assert; }
                set { _Assert = value; OnPropertyChanged("Assert"); }
            }
            
            private Hl7.Fhir.Model.TestScript.TestScriptSetupActionAssertComponent _Assert;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestScriptTestActionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Operation != null) dest.Operation = (Hl7.Fhir.Model.TestScript.TestScriptSetupActionOperationComponent)Operation.DeepCopy();
                    if(Assert != null) dest.Assert = (Hl7.Fhir.Model.TestScript.TestScriptSetupActionAssertComponent)Assert.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TestScriptTestActionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestScriptTestActionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Operation, otherT.Operation)) return false;
                if( !DeepComparable.Matches(Assert, otherT.Assert)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestScriptTestActionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Operation, otherT.Operation)) return false;
                if( !DeepComparable.IsExactly(Assert, otherT.Assert)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("TestScriptContactComponent")]
        [DataContract]
        public partial class TestScriptContactComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TestScriptContactComponent"; } }
            
            /// <summary>
            /// Name of a individual to contact
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
            /// Name of a individual to contact
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if(value == null)
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
                var dest = other as TestScriptContactComponent;
                
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
                return CopyTo(new TestScriptContactComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestScriptContactComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestScriptContactComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("TestScriptSetupActionAssertComponent")]
        [DataContract]
        public partial class TestScriptSetupActionAssertComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TestScriptSetupActionAssertComponent"; } }
            
            /// <summary>
            /// Assertion label
            /// </summary>
            [FhirElement("label", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString LabelElement
            {
                get { return _LabelElement; }
                set { _LabelElement = value; OnPropertyChanged("LabelElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _LabelElement;
            
            /// <summary>
            /// Assertion label
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Label
            {
                get { return LabelElement != null ? LabelElement.Value : null; }
                set
                {
                    if(value == null)
                      LabelElement = null; 
                    else
                      LabelElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Label");
                }
            }
            
            /// <summary>
            /// Assertion description
            /// </summary>
            [FhirElement("description", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Assertion description
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Description
            {
                get { return DescriptionElement != null ? DescriptionElement.Value : null; }
                set
                {
                    if(value == null)
                      DescriptionElement = null; 
                    else
                      DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            /// <summary>
            /// response | request
            /// </summary>
            [FhirElement("direction", InSummary=true, Order=60)]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestScript.AssertionDirectionType> DirectionElement
            {
                get { return _DirectionElement; }
                set { _DirectionElement = value; OnPropertyChanged("DirectionElement"); }
            }
            
            private Code<Hl7.Fhir.Model.TestScript.AssertionDirectionType> _DirectionElement;
            
            /// <summary>
            /// response | request
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.TestScript.AssertionDirectionType? Direction
            {
                get { return DirectionElement != null ? DirectionElement.Value : null; }
                set
                {
                    if(value == null)
                      DirectionElement = null; 
                    else
                      DirectionElement = new Code<Hl7.Fhir.Model.TestScript.AssertionDirectionType>(value);
                    OnPropertyChanged("Direction");
                }
            }
            
            /// <summary>
            /// Id of fixture used to compare the "sourceId/path" evaluations to
            /// </summary>
            [FhirElement("compareToSourceId", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CompareToSourceIdElement
            {
                get { return _CompareToSourceIdElement; }
                set { _CompareToSourceIdElement = value; OnPropertyChanged("CompareToSourceIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CompareToSourceIdElement;
            
            /// <summary>
            /// Id of fixture used to compare the "sourceId/path" evaluations to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string CompareToSourceId
            {
                get { return CompareToSourceIdElement != null ? CompareToSourceIdElement.Value : null; }
                set
                {
                    if(value == null)
                      CompareToSourceIdElement = null; 
                    else
                      CompareToSourceIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("CompareToSourceId");
                }
            }
            
            /// <summary>
            /// XPath or JSONPath expression against fixture used to compare the "sourceId/path" evaluations to
            /// </summary>
            [FhirElement("compareToSourcePath", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CompareToSourcePathElement
            {
                get { return _CompareToSourcePathElement; }
                set { _CompareToSourcePathElement = value; OnPropertyChanged("CompareToSourcePathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CompareToSourcePathElement;
            
            /// <summary>
            /// XPath or JSONPath expression against fixture used to compare the "sourceId/path" evaluations to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string CompareToSourcePath
            {
                get { return CompareToSourcePathElement != null ? CompareToSourcePathElement.Value : null; }
                set
                {
                    if(value == null)
                      CompareToSourcePathElement = null; 
                    else
                      CompareToSourcePathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("CompareToSourcePath");
                }
            }
            
            /// <summary>
            /// xml | json
            /// </summary>
            [FhirElement("contentType", InSummary=true, Order=90)]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestScript.ContentType> ContentTypeElement
            {
                get { return _ContentTypeElement; }
                set { _ContentTypeElement = value; OnPropertyChanged("ContentTypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.TestScript.ContentType> _ContentTypeElement;
            
            /// <summary>
            /// xml | json
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.TestScript.ContentType? ContentType
            {
                get { return ContentTypeElement != null ? ContentTypeElement.Value : null; }
                set
                {
                    if(value == null)
                      ContentTypeElement = null; 
                    else
                      ContentTypeElement = new Code<Hl7.Fhir.Model.TestScript.ContentType>(value);
                    OnPropertyChanged("ContentType");
                }
            }
            
            /// <summary>
            /// The header field
            /// </summary>
            [FhirElement("headerField", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString HeaderFieldElement
            {
                get { return _HeaderFieldElement; }
                set { _HeaderFieldElement = value; OnPropertyChanged("HeaderFieldElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _HeaderFieldElement;
            
            /// <summary>
            /// The header field
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string HeaderField
            {
                get { return HeaderFieldElement != null ? HeaderFieldElement.Value : null; }
                set
                {
                    if(value == null)
                      HeaderFieldElement = null; 
                    else
                      HeaderFieldElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("HeaderField");
                }
            }
            
            /// <summary>
            /// MinimumId
            /// </summary>
            [FhirElement("minimumId", InSummary=true, Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString MinimumIdElement
            {
                get { return _MinimumIdElement; }
                set { _MinimumIdElement = value; OnPropertyChanged("MinimumIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _MinimumIdElement;
            
            /// <summary>
            /// MinimumId
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string MinimumId
            {
                get { return MinimumIdElement != null ? MinimumIdElement.Value : null; }
                set
                {
                    if(value == null)
                      MinimumIdElement = null; 
                    else
                      MinimumIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("MinimumId");
                }
            }
            
            /// <summary>
            /// Navigation Links
            /// </summary>
            [FhirElement("navigationLinks", InSummary=true, Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean NavigationLinksElement
            {
                get { return _NavigationLinksElement; }
                set { _NavigationLinksElement = value; OnPropertyChanged("NavigationLinksElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _NavigationLinksElement;
            
            /// <summary>
            /// Navigation Links
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? NavigationLinks
            {
                get { return NavigationLinksElement != null ? NavigationLinksElement.Value : null; }
                set
                {
                    if(value == null)
                      NavigationLinksElement = null; 
                    else
                      NavigationLinksElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("NavigationLinks");
                }
            }
            
            /// <summary>
            /// equals | notEquals | in | notIn | greaterThan | lessThan | empty | notEmpty | contains | notContains
            /// </summary>
            [FhirElement("operator", InSummary=true, Order=130)]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestScript.AssertionOperatorType> OperatorElement
            {
                get { return _OperatorElement; }
                set { _OperatorElement = value; OnPropertyChanged("OperatorElement"); }
            }
            
            private Code<Hl7.Fhir.Model.TestScript.AssertionOperatorType> _OperatorElement;
            
            /// <summary>
            /// equals | notEquals | in | notIn | greaterThan | lessThan | empty | notEmpty | contains | notContains
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.TestScript.AssertionOperatorType? Operator
            {
                get { return OperatorElement != null ? OperatorElement.Value : null; }
                set
                {
                    if(value == null)
                      OperatorElement = null; 
                    else
                      OperatorElement = new Code<Hl7.Fhir.Model.TestScript.AssertionOperatorType>(value);
                    OnPropertyChanged("Operator");
                }
            }
            
            /// <summary>
            /// XPath or JSONPath expression
            /// </summary>
            [FhirElement("path", InSummary=true, Order=140)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PathElement
            {
                get { return _PathElement; }
                set { _PathElement = value; OnPropertyChanged("PathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PathElement;
            
            /// <summary>
            /// XPath or JSONPath expression
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Path
            {
                get { return PathElement != null ? PathElement.Value : null; }
                set
                {
                    if(value == null)
                      PathElement = null; 
                    else
                      PathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Path");
                }
            }
            
            /// <summary>
            /// Resource type
            /// </summary>
            [FhirElement("resource", InSummary=true, Order=150)]
            [DataMember]
            public Hl7.Fhir.Model.Code ResourceElement
            {
                get { return _ResourceElement; }
                set { _ResourceElement = value; OnPropertyChanged("ResourceElement"); }
            }
            
            private Hl7.Fhir.Model.Code _ResourceElement;
            
            /// <summary>
            /// Resource type
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Resource
            {
                get { return ResourceElement != null ? ResourceElement.Value : null; }
                set
                {
                    if(value == null)
                      ResourceElement = null; 
                    else
                      ResourceElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Resource");
                }
            }
            
            /// <summary>
            /// okay | created | noContent | notModified | bad | forbidden | notFound | methodNotAllowed | conflict | gone | preconditionFailed | unprocessable
            /// </summary>
            [FhirElement("response", InSummary=true, Order=160)]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestScript.AssertionResponseTypes> ResponseElement
            {
                get { return _ResponseElement; }
                set { _ResponseElement = value; OnPropertyChanged("ResponseElement"); }
            }
            
            private Code<Hl7.Fhir.Model.TestScript.AssertionResponseTypes> _ResponseElement;
            
            /// <summary>
            /// okay | created | noContent | notModified | bad | forbidden | notFound | methodNotAllowed | conflict | gone | preconditionFailed | unprocessable
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.TestScript.AssertionResponseTypes? Response
            {
                get { return ResponseElement != null ? ResponseElement.Value : null; }
                set
                {
                    if(value == null)
                      ResponseElement = null; 
                    else
                      ResponseElement = new Code<Hl7.Fhir.Model.TestScript.AssertionResponseTypes>(value);
                    OnPropertyChanged("Response");
                }
            }
            
            /// <summary>
            /// Response Code
            /// </summary>
            [FhirElement("responseCode", InSummary=true, Order=170)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ResponseCodeElement
            {
                get { return _ResponseCodeElement; }
                set { _ResponseCodeElement = value; OnPropertyChanged("ResponseCodeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ResponseCodeElement;
            
            /// <summary>
            /// Response Code
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ResponseCode
            {
                get { return ResponseCodeElement != null ? ResponseCodeElement.Value : null; }
                set
                {
                    if(value == null)
                      ResponseCodeElement = null; 
                    else
                      ResponseCodeElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ResponseCode");
                }
            }
            
            /// <summary>
            /// Fixture Id
            /// </summary>
            [FhirElement("sourceId", InSummary=true, Order=180)]
            [DataMember]
            public Hl7.Fhir.Model.Id SourceIdElement
            {
                get { return _SourceIdElement; }
                set { _SourceIdElement = value; OnPropertyChanged("SourceIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _SourceIdElement;
            
            /// <summary>
            /// Fixture Id
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string SourceId
            {
                get { return SourceIdElement != null ? SourceIdElement.Value : null; }
                set
                {
                    if(value == null)
                      SourceIdElement = null; 
                    else
                      SourceIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("SourceId");
                }
            }
            
            /// <summary>
            /// Validate Profile Id
            /// </summary>
            [FhirElement("validateProfileId", InSummary=true, Order=190)]
            [DataMember]
            public Hl7.Fhir.Model.Id ValidateProfileIdElement
            {
                get { return _ValidateProfileIdElement; }
                set { _ValidateProfileIdElement = value; OnPropertyChanged("ValidateProfileIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _ValidateProfileIdElement;
            
            /// <summary>
            /// Validate Profile Id
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ValidateProfileId
            {
                get { return ValidateProfileIdElement != null ? ValidateProfileIdElement.Value : null; }
                set
                {
                    if(value == null)
                      ValidateProfileIdElement = null; 
                    else
                      ValidateProfileIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("ValidateProfileId");
                }
            }
            
            /// <summary>
            /// The value to compare to
            /// </summary>
            [FhirElement("value", InSummary=true, Order=200)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ValueElement;
            
            /// <summary>
            /// The value to compare to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Value
            {
                get { return ValueElement != null ? ValueElement.Value : null; }
                set
                {
                    if(value == null)
                      ValueElement = null; 
                    else
                      ValueElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Value");
                }
            }
            
            /// <summary>
            /// Warning Only
            /// </summary>
            [FhirElement("warningOnly", InSummary=true, Order=210)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean WarningOnlyElement
            {
                get { return _WarningOnlyElement; }
                set { _WarningOnlyElement = value; OnPropertyChanged("WarningOnlyElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _WarningOnlyElement;
            
            /// <summary>
            /// Warning Only
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? WarningOnly
            {
                get { return WarningOnlyElement != null ? WarningOnlyElement.Value : null; }
                set
                {
                    if(value == null)
                      WarningOnlyElement = null; 
                    else
                      WarningOnlyElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("WarningOnly");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestScriptSetupActionAssertComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(LabelElement != null) dest.LabelElement = (Hl7.Fhir.Model.FhirString)LabelElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(DirectionElement != null) dest.DirectionElement = (Code<Hl7.Fhir.Model.TestScript.AssertionDirectionType>)DirectionElement.DeepCopy();
                    if(CompareToSourceIdElement != null) dest.CompareToSourceIdElement = (Hl7.Fhir.Model.FhirString)CompareToSourceIdElement.DeepCopy();
                    if(CompareToSourcePathElement != null) dest.CompareToSourcePathElement = (Hl7.Fhir.Model.FhirString)CompareToSourcePathElement.DeepCopy();
                    if(ContentTypeElement != null) dest.ContentTypeElement = (Code<Hl7.Fhir.Model.TestScript.ContentType>)ContentTypeElement.DeepCopy();
                    if(HeaderFieldElement != null) dest.HeaderFieldElement = (Hl7.Fhir.Model.FhirString)HeaderFieldElement.DeepCopy();
                    if(MinimumIdElement != null) dest.MinimumIdElement = (Hl7.Fhir.Model.FhirString)MinimumIdElement.DeepCopy();
                    if(NavigationLinksElement != null) dest.NavigationLinksElement = (Hl7.Fhir.Model.FhirBoolean)NavigationLinksElement.DeepCopy();
                    if(OperatorElement != null) dest.OperatorElement = (Code<Hl7.Fhir.Model.TestScript.AssertionOperatorType>)OperatorElement.DeepCopy();
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    if(ResourceElement != null) dest.ResourceElement = (Hl7.Fhir.Model.Code)ResourceElement.DeepCopy();
                    if(ResponseElement != null) dest.ResponseElement = (Code<Hl7.Fhir.Model.TestScript.AssertionResponseTypes>)ResponseElement.DeepCopy();
                    if(ResponseCodeElement != null) dest.ResponseCodeElement = (Hl7.Fhir.Model.FhirString)ResponseCodeElement.DeepCopy();
                    if(SourceIdElement != null) dest.SourceIdElement = (Hl7.Fhir.Model.Id)SourceIdElement.DeepCopy();
                    if(ValidateProfileIdElement != null) dest.ValidateProfileIdElement = (Hl7.Fhir.Model.Id)ValidateProfileIdElement.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                    if(WarningOnlyElement != null) dest.WarningOnlyElement = (Hl7.Fhir.Model.FhirBoolean)WarningOnlyElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TestScriptSetupActionAssertComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestScriptSetupActionAssertComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(LabelElement, otherT.LabelElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(DirectionElement, otherT.DirectionElement)) return false;
                if( !DeepComparable.Matches(CompareToSourceIdElement, otherT.CompareToSourceIdElement)) return false;
                if( !DeepComparable.Matches(CompareToSourcePathElement, otherT.CompareToSourcePathElement)) return false;
                if( !DeepComparable.Matches(ContentTypeElement, otherT.ContentTypeElement)) return false;
                if( !DeepComparable.Matches(HeaderFieldElement, otherT.HeaderFieldElement)) return false;
                if( !DeepComparable.Matches(MinimumIdElement, otherT.MinimumIdElement)) return false;
                if( !DeepComparable.Matches(NavigationLinksElement, otherT.NavigationLinksElement)) return false;
                if( !DeepComparable.Matches(OperatorElement, otherT.OperatorElement)) return false;
                if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.Matches(ResourceElement, otherT.ResourceElement)) return false;
                if( !DeepComparable.Matches(ResponseElement, otherT.ResponseElement)) return false;
                if( !DeepComparable.Matches(ResponseCodeElement, otherT.ResponseCodeElement)) return false;
                if( !DeepComparable.Matches(SourceIdElement, otherT.SourceIdElement)) return false;
                if( !DeepComparable.Matches(ValidateProfileIdElement, otherT.ValidateProfileIdElement)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
                if( !DeepComparable.Matches(WarningOnlyElement, otherT.WarningOnlyElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestScriptSetupActionAssertComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(LabelElement, otherT.LabelElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(DirectionElement, otherT.DirectionElement)) return false;
                if( !DeepComparable.IsExactly(CompareToSourceIdElement, otherT.CompareToSourceIdElement)) return false;
                if( !DeepComparable.IsExactly(CompareToSourcePathElement, otherT.CompareToSourcePathElement)) return false;
                if( !DeepComparable.IsExactly(ContentTypeElement, otherT.ContentTypeElement)) return false;
                if( !DeepComparable.IsExactly(HeaderFieldElement, otherT.HeaderFieldElement)) return false;
                if( !DeepComparable.IsExactly(MinimumIdElement, otherT.MinimumIdElement)) return false;
                if( !DeepComparable.IsExactly(NavigationLinksElement, otherT.NavigationLinksElement)) return false;
                if( !DeepComparable.IsExactly(OperatorElement, otherT.OperatorElement)) return false;
                if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.IsExactly(ResourceElement, otherT.ResourceElement)) return false;
                if( !DeepComparable.IsExactly(ResponseElement, otherT.ResponseElement)) return false;
                if( !DeepComparable.IsExactly(ResponseCodeElement, otherT.ResponseCodeElement)) return false;
                if( !DeepComparable.IsExactly(SourceIdElement, otherT.SourceIdElement)) return false;
                if( !DeepComparable.IsExactly(ValidateProfileIdElement, otherT.ValidateProfileIdElement)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
                if( !DeepComparable.IsExactly(WarningOnlyElement, otherT.WarningOnlyElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("TestScriptSetupComponent")]
        [DataContract]
        public partial class TestScriptSetupComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TestScriptSetupComponent"; } }
            
            /// <summary>
            /// Capabiltities that are assumed to function correctly on the FHIR server being tested
            /// </summary>
            [FhirElement("metadata", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.TestScript.TestScriptMetadataComponent Metadata
            {
                get { return _Metadata; }
                set { _Metadata = value; OnPropertyChanged("Metadata"); }
            }
            
            private Hl7.Fhir.Model.TestScript.TestScriptMetadataComponent _Metadata;
            
            /// <summary>
            /// Action
            /// </summary>
            [FhirElement("action", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestScript.TestScriptSetupActionComponent> Action
            {
                get { if(_Action==null) _Action = new List<Hl7.Fhir.Model.TestScript.TestScriptSetupActionComponent>(); return _Action; }
                set { _Action = value; OnPropertyChanged("Action"); }
            }
            
            private List<Hl7.Fhir.Model.TestScript.TestScriptSetupActionComponent> _Action;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestScriptSetupComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Metadata != null) dest.Metadata = (Hl7.Fhir.Model.TestScript.TestScriptMetadataComponent)Metadata.DeepCopy();
                    if(Action != null) dest.Action = new List<Hl7.Fhir.Model.TestScript.TestScriptSetupActionComponent>(Action.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TestScriptSetupComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestScriptSetupComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Metadata, otherT.Metadata)) return false;
                if( !DeepComparable.Matches(Action, otherT.Action)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestScriptSetupComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Metadata, otherT.Metadata)) return false;
                if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("TestScriptSetupActionComponent")]
        [DataContract]
        public partial class TestScriptSetupActionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TestScriptSetupActionComponent"; } }
            
            /// <summary>
            /// An operation
            /// </summary>
            [FhirElement("operation", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.TestScript.TestScriptSetupActionOperationComponent Operation
            {
                get { return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            
            private Hl7.Fhir.Model.TestScript.TestScriptSetupActionOperationComponent _Operation;
            
            /// <summary>
            /// Assertion
            /// </summary>
            [FhirElement("assert", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.TestScript.TestScriptSetupActionAssertComponent Assert
            {
                get { return _Assert; }
                set { _Assert = value; OnPropertyChanged("Assert"); }
            }
            
            private Hl7.Fhir.Model.TestScript.TestScriptSetupActionAssertComponent _Assert;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestScriptSetupActionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Operation != null) dest.Operation = (Hl7.Fhir.Model.TestScript.TestScriptSetupActionOperationComponent)Operation.DeepCopy();
                    if(Assert != null) dest.Assert = (Hl7.Fhir.Model.TestScript.TestScriptSetupActionAssertComponent)Assert.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TestScriptSetupActionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestScriptSetupActionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Operation, otherT.Operation)) return false;
                if( !DeepComparable.Matches(Assert, otherT.Assert)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestScriptSetupActionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Operation, otherT.Operation)) return false;
                if( !DeepComparable.IsExactly(Assert, otherT.Assert)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("TestScriptFixtureComponent")]
        [DataContract]
        public partial class TestScriptFixtureComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TestScriptFixtureComponent"; } }
            
            /// <summary>
            /// Whether or not to implicitly create the fixture during setup
            /// </summary>
            [FhirElement("autocreate", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean AutocreateElement
            {
                get { return _AutocreateElement; }
                set { _AutocreateElement = value; OnPropertyChanged("AutocreateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _AutocreateElement;
            
            /// <summary>
            /// Whether or not to implicitly create the fixture during setup
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Autocreate
            {
                get { return AutocreateElement != null ? AutocreateElement.Value : null; }
                set
                {
                    if(value == null)
                      AutocreateElement = null; 
                    else
                      AutocreateElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Autocreate");
                }
            }
            
            /// <summary>
            /// Whether or not to implicitly delete the fixture during teardown
            /// </summary>
            [FhirElement("autodelete", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean AutodeleteElement
            {
                get { return _AutodeleteElement; }
                set { _AutodeleteElement = value; OnPropertyChanged("AutodeleteElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _AutodeleteElement;
            
            /// <summary>
            /// Whether or not to implicitly delete the fixture during teardown
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Autodelete
            {
                get { return AutodeleteElement != null ? AutodeleteElement.Value : null; }
                set
                {
                    if(value == null)
                      AutodeleteElement = null; 
                    else
                      AutodeleteElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Autodelete");
                }
            }
            
            /// <summary>
            /// Reference of the resource
            /// </summary>
            [FhirElement("resource", InSummary=true, Order=60)]
            [References()]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Resource
            {
                get { return _Resource; }
                set { _Resource = value; OnPropertyChanged("Resource"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Resource;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestScriptFixtureComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(AutocreateElement != null) dest.AutocreateElement = (Hl7.Fhir.Model.FhirBoolean)AutocreateElement.DeepCopy();
                    if(AutodeleteElement != null) dest.AutodeleteElement = (Hl7.Fhir.Model.FhirBoolean)AutodeleteElement.DeepCopy();
                    if(Resource != null) dest.Resource = (Hl7.Fhir.Model.ResourceReference)Resource.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TestScriptFixtureComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestScriptFixtureComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(AutocreateElement, otherT.AutocreateElement)) return false;
                if( !DeepComparable.Matches(AutodeleteElement, otherT.AutodeleteElement)) return false;
                if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestScriptFixtureComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(AutocreateElement, otherT.AutocreateElement)) return false;
                if( !DeepComparable.IsExactly(AutodeleteElement, otherT.AutodeleteElement)) return false;
                if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("TestScriptMetadataCapabilitiesComponent")]
        [DataContract]
        public partial class TestScriptMetadataCapabilitiesComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TestScriptMetadataCapabilitiesComponent"; } }
            
            /// <summary>
            /// Required capabilities
            /// </summary>
            [FhirElement("required", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean RequiredElement
            {
                get { return _RequiredElement; }
                set { _RequiredElement = value; OnPropertyChanged("RequiredElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _RequiredElement;
            
            /// <summary>
            /// Required capabilities
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Required
            {
                get { return RequiredElement != null ? RequiredElement.Value : null; }
                set
                {
                    if(value == null)
                      RequiredElement = null; 
                    else
                      RequiredElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Required");
                }
            }
            
            /// <summary>
            /// Validated capabilities
            /// </summary>
            [FhirElement("validated", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ValidatedElement
            {
                get { return _ValidatedElement; }
                set { _ValidatedElement = value; OnPropertyChanged("ValidatedElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _ValidatedElement;
            
            /// <summary>
            /// Validated capabilities
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Validated
            {
                get { return ValidatedElement != null ? ValidatedElement.Value : null; }
                set
                {
                    if(value == null)
                      ValidatedElement = null; 
                    else
                      ValidatedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Validated");
                }
            }
            
            /// <summary>
            /// The description of the capabilities
            /// </summary>
            [FhirElement("description", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// The description of the capabilities
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Description
            {
                get { return DescriptionElement != null ? DescriptionElement.Value : null; }
                set
                {
                    if(value == null)
                      DescriptionElement = null; 
                    else
                      DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            /// <summary>
            /// Which server these requirements apply to
            /// </summary>
            [FhirElement("destination", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Integer DestinationElement
            {
                get { return _DestinationElement; }
                set { _DestinationElement = value; OnPropertyChanged("DestinationElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _DestinationElement;
            
            /// <summary>
            /// Which server these requirements apply to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Destination
            {
                get { return DestinationElement != null ? DestinationElement.Value : null; }
                set
                {
                    if(value == null)
                      DestinationElement = null; 
                    else
                      DestinationElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Destination");
                }
            }
            
            /// <summary>
            /// Links to the FHIR specification
            /// </summary>
            [FhirElement("link", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirUri> LinkElement
            {
                get { if(_LinkElement==null) _LinkElement = new List<Hl7.Fhir.Model.FhirUri>(); return _LinkElement; }
                set { _LinkElement = value; OnPropertyChanged("LinkElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirUri> _LinkElement;
            
            /// <summary>
            /// Links to the FHIR specification
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Link
            {
                get { return LinkElement != null ? LinkElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      LinkElement = null; 
                    else
                      LinkElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                    OnPropertyChanged("Link");
                }
            }
            
            /// <summary>
            /// Required Conformance
            /// </summary>
            [FhirElement("conformance", InSummary=true, Order=90)]
            [References("Conformance")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Conformance
            {
                get { return _Conformance; }
                set { _Conformance = value; OnPropertyChanged("Conformance"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Conformance;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestScriptMetadataCapabilitiesComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(RequiredElement != null) dest.RequiredElement = (Hl7.Fhir.Model.FhirBoolean)RequiredElement.DeepCopy();
                    if(ValidatedElement != null) dest.ValidatedElement = (Hl7.Fhir.Model.FhirBoolean)ValidatedElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(DestinationElement != null) dest.DestinationElement = (Hl7.Fhir.Model.Integer)DestinationElement.DeepCopy();
                    if(LinkElement != null) dest.LinkElement = new List<Hl7.Fhir.Model.FhirUri>(LinkElement.DeepCopy());
                    if(Conformance != null) dest.Conformance = (Hl7.Fhir.Model.ResourceReference)Conformance.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TestScriptMetadataCapabilitiesComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestScriptMetadataCapabilitiesComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(RequiredElement, otherT.RequiredElement)) return false;
                if( !DeepComparable.Matches(ValidatedElement, otherT.ValidatedElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(DestinationElement, otherT.DestinationElement)) return false;
                if( !DeepComparable.Matches(LinkElement, otherT.LinkElement)) return false;
                if( !DeepComparable.Matches(Conformance, otherT.Conformance)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestScriptMetadataCapabilitiesComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(RequiredElement, otherT.RequiredElement)) return false;
                if( !DeepComparable.IsExactly(ValidatedElement, otherT.ValidatedElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(DestinationElement, otherT.DestinationElement)) return false;
                if( !DeepComparable.IsExactly(LinkElement, otherT.LinkElement)) return false;
                if( !DeepComparable.IsExactly(Conformance, otherT.Conformance)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("TestScriptSetupActionOperationComponent")]
        [DataContract]
        public partial class TestScriptSetupActionOperationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TestScriptSetupActionOperationComponent"; } }
            
            /// <summary>
            /// The operation type that will be executed
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.Coding _Type;
            
            /// <summary>
            /// Resource type
            /// </summary>
            [FhirElement("resource", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Code ResourceElement
            {
                get { return _ResourceElement; }
                set { _ResourceElement = value; OnPropertyChanged("ResourceElement"); }
            }
            
            private Hl7.Fhir.Model.Code _ResourceElement;
            
            /// <summary>
            /// Resource type
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Resource
            {
                get { return ResourceElement != null ? ResourceElement.Value : null; }
                set
                {
                    if(value == null)
                      ResourceElement = null; 
                    else
                      ResourceElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Resource");
                }
            }
            
            /// <summary>
            /// Operation label
            /// </summary>
            [FhirElement("label", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString LabelElement
            {
                get { return _LabelElement; }
                set { _LabelElement = value; OnPropertyChanged("LabelElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _LabelElement;
            
            /// <summary>
            /// Operation label
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Label
            {
                get { return LabelElement != null ? LabelElement.Value : null; }
                set
                {
                    if(value == null)
                      LabelElement = null; 
                    else
                      LabelElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Label");
                }
            }
            
            /// <summary>
            /// Operation description
            /// </summary>
            [FhirElement("description", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Operation description
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Description
            {
                get { return DescriptionElement != null ? DescriptionElement.Value : null; }
                set
                {
                    if(value == null)
                      DescriptionElement = null; 
                    else
                      DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            /// <summary>
            /// xml | json
            /// </summary>
            [FhirElement("accept", InSummary=true, Order=80)]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestScript.ContentType> AcceptElement
            {
                get { return _AcceptElement; }
                set { _AcceptElement = value; OnPropertyChanged("AcceptElement"); }
            }
            
            private Code<Hl7.Fhir.Model.TestScript.ContentType> _AcceptElement;
            
            /// <summary>
            /// xml | json
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.TestScript.ContentType? Accept
            {
                get { return AcceptElement != null ? AcceptElement.Value : null; }
                set
                {
                    if(value == null)
                      AcceptElement = null; 
                    else
                      AcceptElement = new Code<Hl7.Fhir.Model.TestScript.ContentType>(value);
                    OnPropertyChanged("Accept");
                }
            }
            
            /// <summary>
            /// xml | json
            /// </summary>
            [FhirElement("contentType", InSummary=true, Order=90)]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestScript.ContentType> ContentTypeElement
            {
                get { return _ContentTypeElement; }
                set { _ContentTypeElement = value; OnPropertyChanged("ContentTypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.TestScript.ContentType> _ContentTypeElement;
            
            /// <summary>
            /// xml | json
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.TestScript.ContentType? ContentType
            {
                get { return ContentTypeElement != null ? ContentTypeElement.Value : null; }
                set
                {
                    if(value == null)
                      ContentTypeElement = null; 
                    else
                      ContentTypeElement = new Code<Hl7.Fhir.Model.TestScript.ContentType>(value);
                    OnPropertyChanged("ContentType");
                }
            }
            
            /// <summary>
            /// Which server to perform the operation on
            /// </summary>
            [FhirElement("destination", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.Integer DestinationElement
            {
                get { return _DestinationElement; }
                set { _DestinationElement = value; OnPropertyChanged("DestinationElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _DestinationElement;
            
            /// <summary>
            /// Which server to perform the operation on
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Destination
            {
                get { return DestinationElement != null ? DestinationElement.Value : null; }
                set
                {
                    if(value == null)
                      DestinationElement = null; 
                    else
                      DestinationElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Destination");
                }
            }
            
            /// <summary>
            /// Whether or not to send the request url in encoded format
            /// </summary>
            [FhirElement("encodeRequestUrl", InSummary=true, Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean EncodeRequestUrlElement
            {
                get { return _EncodeRequestUrlElement; }
                set { _EncodeRequestUrlElement = value; OnPropertyChanged("EncodeRequestUrlElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _EncodeRequestUrlElement;
            
            /// <summary>
            /// Whether or not to send the request url in encoded format
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? EncodeRequestUrl
            {
                get { return EncodeRequestUrlElement != null ? EncodeRequestUrlElement.Value : null; }
                set
                {
                    if(value == null)
                      EncodeRequestUrlElement = null; 
                    else
                      EncodeRequestUrlElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("EncodeRequestUrl");
                }
            }
            
            /// <summary>
            /// Params
            /// </summary>
            [FhirElement("params", InSummary=true, Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ParamsElement
            {
                get { return _ParamsElement; }
                set { _ParamsElement = value; OnPropertyChanged("ParamsElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ParamsElement;
            
            /// <summary>
            /// Params
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Params
            {
                get { return ParamsElement != null ? ParamsElement.Value : null; }
                set
                {
                    if(value == null)
                      ParamsElement = null; 
                    else
                      ParamsElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Params");
                }
            }
            
            /// <summary>
            /// Each operation can have one ore more header elements
            /// </summary>
            [FhirElement("requestHeader", InSummary=true, Order=130)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestScript.TestScriptSetupActionOperationRequestHeaderComponent> RequestHeader
            {
                get { if(_RequestHeader==null) _RequestHeader = new List<Hl7.Fhir.Model.TestScript.TestScriptSetupActionOperationRequestHeaderComponent>(); return _RequestHeader; }
                set { _RequestHeader = value; OnPropertyChanged("RequestHeader"); }
            }
            
            private List<Hl7.Fhir.Model.TestScript.TestScriptSetupActionOperationRequestHeaderComponent> _RequestHeader;
            
            /// <summary>
            /// Response Id
            /// </summary>
            [FhirElement("responseId", InSummary=true, Order=140)]
            [DataMember]
            public Hl7.Fhir.Model.Id ResponseIdElement
            {
                get { return _ResponseIdElement; }
                set { _ResponseIdElement = value; OnPropertyChanged("ResponseIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _ResponseIdElement;
            
            /// <summary>
            /// Response Id
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ResponseId
            {
                get { return ResponseIdElement != null ? ResponseIdElement.Value : null; }
                set
                {
                    if(value == null)
                      ResponseIdElement = null; 
                    else
                      ResponseIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("ResponseId");
                }
            }
            
            /// <summary>
            /// Fixture Id of body for PUT and POST requests
            /// </summary>
            [FhirElement("sourceId", InSummary=true, Order=150)]
            [DataMember]
            public Hl7.Fhir.Model.Id SourceIdElement
            {
                get { return _SourceIdElement; }
                set { _SourceIdElement = value; OnPropertyChanged("SourceIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _SourceIdElement;
            
            /// <summary>
            /// Fixture Id of body for PUT and POST requests
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string SourceId
            {
                get { return SourceIdElement != null ? SourceIdElement.Value : null; }
                set
                {
                    if(value == null)
                      SourceIdElement = null; 
                    else
                      SourceIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("SourceId");
                }
            }
            
            /// <summary>
            /// Id of fixture used for extracting the [id],  [type], and [vid] for GET requests
            /// </summary>
            [FhirElement("targetId", InSummary=true, Order=160)]
            [DataMember]
            public Hl7.Fhir.Model.Id TargetIdElement
            {
                get { return _TargetIdElement; }
                set { _TargetIdElement = value; OnPropertyChanged("TargetIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _TargetIdElement;
            
            /// <summary>
            /// Id of fixture used for extracting the [id],  [type], and [vid] for GET requests
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string TargetId
            {
                get { return TargetIdElement != null ? TargetIdElement.Value : null; }
                set
                {
                    if(value == null)
                      TargetIdElement = null; 
                    else
                      TargetIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("TargetId");
                }
            }
            
            /// <summary>
            /// Request URL
            /// </summary>
            [FhirElement("url", InSummary=true, Order=170)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString UrlElement
            {
                get { return _UrlElement; }
                set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _UrlElement;
            
            /// <summary>
            /// Request URL
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Url
            {
                get { return UrlElement != null ? UrlElement.Value : null; }
                set
                {
                    if(value == null)
                      UrlElement = null; 
                    else
                      UrlElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Url");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestScriptSetupActionOperationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                    if(ResourceElement != null) dest.ResourceElement = (Hl7.Fhir.Model.Code)ResourceElement.DeepCopy();
                    if(LabelElement != null) dest.LabelElement = (Hl7.Fhir.Model.FhirString)LabelElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(AcceptElement != null) dest.AcceptElement = (Code<Hl7.Fhir.Model.TestScript.ContentType>)AcceptElement.DeepCopy();
                    if(ContentTypeElement != null) dest.ContentTypeElement = (Code<Hl7.Fhir.Model.TestScript.ContentType>)ContentTypeElement.DeepCopy();
                    if(DestinationElement != null) dest.DestinationElement = (Hl7.Fhir.Model.Integer)DestinationElement.DeepCopy();
                    if(EncodeRequestUrlElement != null) dest.EncodeRequestUrlElement = (Hl7.Fhir.Model.FhirBoolean)EncodeRequestUrlElement.DeepCopy();
                    if(ParamsElement != null) dest.ParamsElement = (Hl7.Fhir.Model.FhirString)ParamsElement.DeepCopy();
                    if(RequestHeader != null) dest.RequestHeader = new List<Hl7.Fhir.Model.TestScript.TestScriptSetupActionOperationRequestHeaderComponent>(RequestHeader.DeepCopy());
                    if(ResponseIdElement != null) dest.ResponseIdElement = (Hl7.Fhir.Model.Id)ResponseIdElement.DeepCopy();
                    if(SourceIdElement != null) dest.SourceIdElement = (Hl7.Fhir.Model.Id)SourceIdElement.DeepCopy();
                    if(TargetIdElement != null) dest.TargetIdElement = (Hl7.Fhir.Model.Id)TargetIdElement.DeepCopy();
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirString)UrlElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TestScriptSetupActionOperationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestScriptSetupActionOperationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(ResourceElement, otherT.ResourceElement)) return false;
                if( !DeepComparable.Matches(LabelElement, otherT.LabelElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(AcceptElement, otherT.AcceptElement)) return false;
                if( !DeepComparable.Matches(ContentTypeElement, otherT.ContentTypeElement)) return false;
                if( !DeepComparable.Matches(DestinationElement, otherT.DestinationElement)) return false;
                if( !DeepComparable.Matches(EncodeRequestUrlElement, otherT.EncodeRequestUrlElement)) return false;
                if( !DeepComparable.Matches(ParamsElement, otherT.ParamsElement)) return false;
                if( !DeepComparable.Matches(RequestHeader, otherT.RequestHeader)) return false;
                if( !DeepComparable.Matches(ResponseIdElement, otherT.ResponseIdElement)) return false;
                if( !DeepComparable.Matches(SourceIdElement, otherT.SourceIdElement)) return false;
                if( !DeepComparable.Matches(TargetIdElement, otherT.TargetIdElement)) return false;
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestScriptSetupActionOperationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(ResourceElement, otherT.ResourceElement)) return false;
                if( !DeepComparable.IsExactly(LabelElement, otherT.LabelElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(AcceptElement, otherT.AcceptElement)) return false;
                if( !DeepComparable.IsExactly(ContentTypeElement, otherT.ContentTypeElement)) return false;
                if( !DeepComparable.IsExactly(DestinationElement, otherT.DestinationElement)) return false;
                if( !DeepComparable.IsExactly(EncodeRequestUrlElement, otherT.EncodeRequestUrlElement)) return false;
                if( !DeepComparable.IsExactly(ParamsElement, otherT.ParamsElement)) return false;
                if( !DeepComparable.IsExactly(RequestHeader, otherT.RequestHeader)) return false;
                if( !DeepComparable.IsExactly(ResponseIdElement, otherT.ResponseIdElement)) return false;
                if( !DeepComparable.IsExactly(SourceIdElement, otherT.SourceIdElement)) return false;
                if( !DeepComparable.IsExactly(TargetIdElement, otherT.TargetIdElement)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("TestScriptTestComponent")]
        [DataContract]
        public partial class TestScriptTestComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TestScriptTestComponent"; } }
            
            /// <summary>
            /// The name of this test
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
            /// The name of this test
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if(value == null)
                      NameElement = null; 
                    else
                      NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Short description of the test
            /// </summary>
            [FhirElement("description", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Short description of the test
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Description
            {
                get { return DescriptionElement != null ? DescriptionElement.Value : null; }
                set
                {
                    if(value == null)
                      DescriptionElement = null; 
                    else
                      DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            /// <summary>
            /// Capabiltities that are assumed to function correctly on the FHIR server being tested
            /// </summary>
            [FhirElement("metadata", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.TestScript.TestScriptMetadataComponent Metadata
            {
                get { return _Metadata; }
                set { _Metadata = value; OnPropertyChanged("Metadata"); }
            }
            
            private Hl7.Fhir.Model.TestScript.TestScriptMetadataComponent _Metadata;
            
            /// <summary>
            /// Action
            /// </summary>
            [FhirElement("action", InSummary=true, Order=70)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestScript.TestScriptTestActionComponent> Action
            {
                get { if(_Action==null) _Action = new List<Hl7.Fhir.Model.TestScript.TestScriptTestActionComponent>(); return _Action; }
                set { _Action = value; OnPropertyChanged("Action"); }
            }
            
            private List<Hl7.Fhir.Model.TestScript.TestScriptTestActionComponent> _Action;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestScriptTestComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Metadata != null) dest.Metadata = (Hl7.Fhir.Model.TestScript.TestScriptMetadataComponent)Metadata.DeepCopy();
                    if(Action != null) dest.Action = new List<Hl7.Fhir.Model.TestScript.TestScriptTestActionComponent>(Action.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TestScriptTestComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestScriptTestComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Metadata, otherT.Metadata)) return false;
                if( !DeepComparable.Matches(Action, otherT.Action)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestScriptTestComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Metadata, otherT.Metadata)) return false;
                if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("TestScriptVariableComponent")]
        [DataContract]
        public partial class TestScriptVariableComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TestScriptVariableComponent"; } }
            
            /// <summary>
            /// Variable name
            /// </summary>
            [FhirElement("name", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Variable name
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if(value == null)
                      NameElement = null; 
                    else
                      NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Header field name
            /// </summary>
            [FhirElement("headerField", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString HeaderFieldElement
            {
                get { return _HeaderFieldElement; }
                set { _HeaderFieldElement = value; OnPropertyChanged("HeaderFieldElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _HeaderFieldElement;
            
            /// <summary>
            /// Header field name
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string HeaderField
            {
                get { return HeaderFieldElement != null ? HeaderFieldElement.Value : null; }
                set
                {
                    if(value == null)
                      HeaderFieldElement = null; 
                    else
                      HeaderFieldElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("HeaderField");
                }
            }
            
            /// <summary>
            /// XPath or JSONPath against the fixture body
            /// </summary>
            [FhirElement("path", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PathElement
            {
                get { return _PathElement; }
                set { _PathElement = value; OnPropertyChanged("PathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PathElement;
            
            /// <summary>
            /// XPath or JSONPath against the fixture body
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Path
            {
                get { return PathElement != null ? PathElement.Value : null; }
                set
                {
                    if(value == null)
                      PathElement = null; 
                    else
                      PathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Path");
                }
            }
            
            /// <summary>
            /// Fixture Id
            /// </summary>
            [FhirElement("sourceId", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Id SourceIdElement
            {
                get { return _SourceIdElement; }
                set { _SourceIdElement = value; OnPropertyChanged("SourceIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _SourceIdElement;
            
            /// <summary>
            /// Fixture Id
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string SourceId
            {
                get { return SourceIdElement != null ? SourceIdElement.Value : null; }
                set
                {
                    if(value == null)
                      SourceIdElement = null; 
                    else
                      SourceIdElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("SourceId");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestScriptVariableComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(HeaderFieldElement != null) dest.HeaderFieldElement = (Hl7.Fhir.Model.FhirString)HeaderFieldElement.DeepCopy();
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    if(SourceIdElement != null) dest.SourceIdElement = (Hl7.Fhir.Model.Id)SourceIdElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TestScriptVariableComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestScriptVariableComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(HeaderFieldElement, otherT.HeaderFieldElement)) return false;
                if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.Matches(SourceIdElement, otherT.SourceIdElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestScriptVariableComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(HeaderFieldElement, otherT.HeaderFieldElement)) return false;
                if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.IsExactly(SourceIdElement, otherT.SourceIdElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("TestScriptTeardownActionComponent")]
        [DataContract]
        public partial class TestScriptTeardownActionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TestScriptTeardownActionComponent"; } }
            
            /// <summary>
            /// Operation
            /// </summary>
            [FhirElement("operation", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.TestScript.TestScriptSetupActionOperationComponent Operation
            {
                get { return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            
            private Hl7.Fhir.Model.TestScript.TestScriptSetupActionOperationComponent _Operation;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestScriptTeardownActionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Operation != null) dest.Operation = (Hl7.Fhir.Model.TestScript.TestScriptSetupActionOperationComponent)Operation.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TestScriptTeardownActionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestScriptTeardownActionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Operation, otherT.Operation)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestScriptTeardownActionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Operation, otherT.Operation)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("TestScriptMetadataComponent")]
        [DataContract]
        public partial class TestScriptMetadataComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TestScriptMetadataComponent"; } }
            
            /// <summary>
            /// Links to the FHIR specification
            /// </summary>
            [FhirElement("link", InSummary=true, Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestScript.TestScriptMetadataLinkComponent> Link
            {
                get { if(_Link==null) _Link = new List<Hl7.Fhir.Model.TestScript.TestScriptMetadataLinkComponent>(); return _Link; }
                set { _Link = value; OnPropertyChanged("Link"); }
            }
            
            private List<Hl7.Fhir.Model.TestScript.TestScriptMetadataLinkComponent> _Link;
            
            /// <summary>
            /// Capabiltities that are assumed to function correctly on the FHIR server being tested
            /// </summary>
            [FhirElement("capabilities", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestScript.TestScriptMetadataCapabilitiesComponent> Capabilities
            {
                get { if(_Capabilities==null) _Capabilities = new List<Hl7.Fhir.Model.TestScript.TestScriptMetadataCapabilitiesComponent>(); return _Capabilities; }
                set { _Capabilities = value; OnPropertyChanged("Capabilities"); }
            }
            
            private List<Hl7.Fhir.Model.TestScript.TestScriptMetadataCapabilitiesComponent> _Capabilities;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestScriptMetadataComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Link != null) dest.Link = new List<Hl7.Fhir.Model.TestScript.TestScriptMetadataLinkComponent>(Link.DeepCopy());
                    if(Capabilities != null) dest.Capabilities = new List<Hl7.Fhir.Model.TestScript.TestScriptMetadataCapabilitiesComponent>(Capabilities.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TestScriptMetadataComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestScriptMetadataComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Link, otherT.Link)) return false;
                if( !DeepComparable.Matches(Capabilities, otherT.Capabilities)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestScriptMetadataComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Link, otherT.Link)) return false;
                if( !DeepComparable.IsExactly(Capabilities, otherT.Capabilities)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Literal URL used to reference this TestScript
        /// </summary>
        [FhirElement("url", InSummary=true, Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _UrlElement;
        
        /// <summary>
        /// Literal URL used to reference this TestScript
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Url
        {
            get { return UrlElement != null ? UrlElement.Value : null; }
            set
            {
                if(value == null)
                  UrlElement = null; 
                else
                  UrlElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Url");
            }
        }
        
        /// <summary>
        /// Logical id for this version of the TestScript
        /// </summary>
        [FhirElement("version", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// Logical id for this version of the TestScript
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Version
        {
            get { return VersionElement != null ? VersionElement.Value : null; }
            set
            {
                if(value == null)
                  VersionElement = null; 
                else
                  VersionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Version");
            }
        }
        
        /// <summary>
        /// Informal name for this TestScript
        /// </summary>
        [FhirElement("name", InSummary=true, Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Informal name for this TestScript
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Name
        {
            get { return NameElement != null ? NameElement.Value : null; }
            set
            {
                if(value == null)
                  NameElement = null; 
                else
                  NameElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Name");
            }
        }
        
        /// <summary>
        /// draft | active | retired
        /// </summary>
        [FhirElement("status", InSummary=true, Order=120)]
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
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.ConformanceResourceStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// If for testing purposes, not real usage
        /// </summary>
        [FhirElement("experimental", InSummary=true, Order=130)]
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
                if(value == null)
                  ExperimentalElement = null; 
                else
                  ExperimentalElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Experimental");
            }
        }
        
        /// <summary>
        /// Name of the publisher (Organization or individual)
        /// </summary>
        [FhirElement("publisher", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PublisherElement
        {
            get { return _PublisherElement; }
            set { _PublisherElement = value; OnPropertyChanged("PublisherElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PublisherElement;
        
        /// <summary>
        /// Name of the publisher (Organization or individual)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Publisher
        {
            get { return PublisherElement != null ? PublisherElement.Value : null; }
            set
            {
                if(value == null)
                  PublisherElement = null; 
                else
                  PublisherElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Publisher");
            }
        }
        
        /// <summary>
        /// Contact details of the publisher
        /// </summary>
        [FhirElement("contact", InSummary=true, Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.TestScript.TestScriptContactComponent> Contact
        {
            get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.TestScript.TestScriptContactComponent>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<Hl7.Fhir.Model.TestScript.TestScriptContactComponent> _Contact;
        
        /// <summary>
        /// Date for this version of the TestScript
        /// </summary>
        [FhirElement("date", InSummary=true, Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// Date for this version of the TestScript
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
        /// Natural language description of the TestScript
        /// </summary>
        [FhirElement("description", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// Natural language description of the TestScript
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Description
        {
            get { return DescriptionElement != null ? DescriptionElement.Value : null; }
            set
            {
                if(value == null)
                  DescriptionElement = null; 
                else
                  DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Description");
            }
        }
        
        /// <summary>
        /// Content intends to support these contexts
        /// </summary>
        [FhirElement("useContext", InSummary=true, Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> UseContext
        {
            get { if(_UseContext==null) _UseContext = new List<Hl7.Fhir.Model.CodeableConcept>(); return _UseContext; }
            set { _UseContext = value; OnPropertyChanged("UseContext"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _UseContext;
        
        /// <summary>
        /// Scope and Usage this Test Script is for
        /// </summary>
        [FhirElement("requirements", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString RequirementsElement
        {
            get { return _RequirementsElement; }
            set { _RequirementsElement = value; OnPropertyChanged("RequirementsElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _RequirementsElement;
        
        /// <summary>
        /// Scope and Usage this Test Script is for
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Requirements
        {
            get { return RequirementsElement != null ? RequirementsElement.Value : null; }
            set
            {
                if(value == null)
                  RequirementsElement = null; 
                else
                  RequirementsElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Requirements");
            }
        }
        
        /// <summary>
        /// Use and/or Publishing restrictions
        /// </summary>
        [FhirElement("copyright", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CopyrightElement
        {
            get { return _CopyrightElement; }
            set { _CopyrightElement = value; OnPropertyChanged("CopyrightElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _CopyrightElement;
        
        /// <summary>
        /// Use and/or Publishing restrictions
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Copyright
        {
            get { return CopyrightElement != null ? CopyrightElement.Value : null; }
            set
            {
                if(value == null)
                  CopyrightElement = null; 
                else
                  CopyrightElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Copyright");
            }
        }
        
        /// <summary>
        /// Required capability that is assumed to function correctly on the FHIR server being tested
        /// </summary>
        [FhirElement("metadata", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.TestScript.TestScriptMetadataComponent Metadata
        {
            get { return _Metadata; }
            set { _Metadata = value; OnPropertyChanged("Metadata"); }
        }
        
        private Hl7.Fhir.Model.TestScript.TestScriptMetadataComponent _Metadata;
        
        /// <summary>
        /// Whether or not the tests apply to more than one FHIR server
        /// </summary>
        [FhirElement("multiserver", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean MultiserverElement
        {
            get { return _MultiserverElement; }
            set { _MultiserverElement = value; OnPropertyChanged("MultiserverElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _MultiserverElement;
        
        /// <summary>
        /// Whether or not the tests apply to more than one FHIR server
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Multiserver
        {
            get { return MultiserverElement != null ? MultiserverElement.Value : null; }
            set
            {
                if(value == null)
                  MultiserverElement = null; 
                else
                  MultiserverElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Multiserver");
            }
        }
        
        /// <summary>
        /// Fixture in the test script - by reference (uri)
        /// </summary>
        [FhirElement("fixture", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.TestScript.TestScriptFixtureComponent> Fixture
        {
            get { if(_Fixture==null) _Fixture = new List<Hl7.Fhir.Model.TestScript.TestScriptFixtureComponent>(); return _Fixture; }
            set { _Fixture = value; OnPropertyChanged("Fixture"); }
        }
        
        private List<Hl7.Fhir.Model.TestScript.TestScriptFixtureComponent> _Fixture;
        
        /// <summary>
        /// Reference of the validation profile
        /// </summary>
        [FhirElement("profile", Order=240)]
        [References()]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Profile
        {
            get { if(_Profile==null) _Profile = new List<Hl7.Fhir.Model.ResourceReference>(); return _Profile; }
            set { _Profile = value; OnPropertyChanged("Profile"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Profile;
        
        /// <summary>
        /// Variable
        /// </summary>
        [FhirElement("variable", Order=250)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.TestScript.TestScriptVariableComponent> Variable
        {
            get { if(_Variable==null) _Variable = new List<Hl7.Fhir.Model.TestScript.TestScriptVariableComponent>(); return _Variable; }
            set { _Variable = value; OnPropertyChanged("Variable"); }
        }
        
        private List<Hl7.Fhir.Model.TestScript.TestScriptVariableComponent> _Variable;
        
        /// <summary>
        /// A series of required setup operations before tests are executed
        /// </summary>
        [FhirElement("setup", Order=260)]
        [DataMember]
        public Hl7.Fhir.Model.TestScript.TestScriptSetupComponent Setup
        {
            get { return _Setup; }
            set { _Setup = value; OnPropertyChanged("Setup"); }
        }
        
        private Hl7.Fhir.Model.TestScript.TestScriptSetupComponent _Setup;
        
        /// <summary>
        /// A test in this script
        /// </summary>
        [FhirElement("test", Order=270)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.TestScript.TestScriptTestComponent> Test
        {
            get { if(_Test==null) _Test = new List<Hl7.Fhir.Model.TestScript.TestScriptTestComponent>(); return _Test; }
            set { _Test = value; OnPropertyChanged("Test"); }
        }
        
        private List<Hl7.Fhir.Model.TestScript.TestScriptTestComponent> _Test;
        
        /// <summary>
        /// A series of required clean up steps
        /// </summary>
        [FhirElement("teardown", Order=280)]
        [DataMember]
        public Hl7.Fhir.Model.TestScript.TestScriptTeardownComponent Teardown
        {
            get { return _Teardown; }
            set { _Teardown = value; OnPropertyChanged("Teardown"); }
        }
        
        private Hl7.Fhir.Model.TestScript.TestScriptTeardownComponent _Teardown;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as TestScript;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.ConformanceResourceStatus>)StatusElement.DeepCopy();
                if(ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
                if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.TestScript.TestScriptContactComponent>(Contact.DeepCopy());
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(UseContext != null) dest.UseContext = new List<Hl7.Fhir.Model.CodeableConcept>(UseContext.DeepCopy());
                if(RequirementsElement != null) dest.RequirementsElement = (Hl7.Fhir.Model.FhirString)RequirementsElement.DeepCopy();
                if(CopyrightElement != null) dest.CopyrightElement = (Hl7.Fhir.Model.FhirString)CopyrightElement.DeepCopy();
                if(Metadata != null) dest.Metadata = (Hl7.Fhir.Model.TestScript.TestScriptMetadataComponent)Metadata.DeepCopy();
                if(MultiserverElement != null) dest.MultiserverElement = (Hl7.Fhir.Model.FhirBoolean)MultiserverElement.DeepCopy();
                if(Fixture != null) dest.Fixture = new List<Hl7.Fhir.Model.TestScript.TestScriptFixtureComponent>(Fixture.DeepCopy());
                if(Profile != null) dest.Profile = new List<Hl7.Fhir.Model.ResourceReference>(Profile.DeepCopy());
                if(Variable != null) dest.Variable = new List<Hl7.Fhir.Model.TestScript.TestScriptVariableComponent>(Variable.DeepCopy());
                if(Setup != null) dest.Setup = (Hl7.Fhir.Model.TestScript.TestScriptSetupComponent)Setup.DeepCopy();
                if(Test != null) dest.Test = new List<Hl7.Fhir.Model.TestScript.TestScriptTestComponent>(Test.DeepCopy());
                if(Teardown != null) dest.Teardown = (Hl7.Fhir.Model.TestScript.TestScriptTeardownComponent)Teardown.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new TestScript());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as TestScript;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.Matches(RequirementsElement, otherT.RequirementsElement)) return false;
            if( !DeepComparable.Matches(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.Matches(Metadata, otherT.Metadata)) return false;
            if( !DeepComparable.Matches(MultiserverElement, otherT.MultiserverElement)) return false;
            if( !DeepComparable.Matches(Fixture, otherT.Fixture)) return false;
            if( !DeepComparable.Matches(Profile, otherT.Profile)) return false;
            if( !DeepComparable.Matches(Variable, otherT.Variable)) return false;
            if( !DeepComparable.Matches(Setup, otherT.Setup)) return false;
            if( !DeepComparable.Matches(Test, otherT.Test)) return false;
            if( !DeepComparable.Matches(Teardown, otherT.Teardown)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as TestScript;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.IsExactly(RequirementsElement, otherT.RequirementsElement)) return false;
            if( !DeepComparable.IsExactly(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.IsExactly(Metadata, otherT.Metadata)) return false;
            if( !DeepComparable.IsExactly(MultiserverElement, otherT.MultiserverElement)) return false;
            if( !DeepComparable.IsExactly(Fixture, otherT.Fixture)) return false;
            if( !DeepComparable.IsExactly(Profile, otherT.Profile)) return false;
            if( !DeepComparable.IsExactly(Variable, otherT.Variable)) return false;
            if( !DeepComparable.IsExactly(Setup, otherT.Setup)) return false;
            if( !DeepComparable.IsExactly(Test, otherT.Test)) return false;
            if( !DeepComparable.IsExactly(Teardown, otherT.Teardown)) return false;
            
            return true;
        }
        
    }
    
}
