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
// Generated on Tue, Jun 16, 2015 00:04+0200 for FHIR v0.5.0
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
        /// The allowable operation types.
        /// </summary>
        [FhirEnumeration("TestOperationType")]
        public enum TestOperationType
        {
            /// <summary>
            /// Read the current state of the resource.
            /// </summary>
            [EnumLiteral("read")]
            Read,
            /// <summary>
            /// Read the state of a specific version of the resource.
            /// </summary>
            [EnumLiteral("vread")]
            Vread,
            /// <summary>
            /// Update an existing resource by its id (or create it if it is new).
            /// </summary>
            [EnumLiteral("update")]
            Update,
            /// <summary>
            /// Delete a resource.
            /// </summary>
            [EnumLiteral("delete")]
            Delete,
            /// <summary>
            /// Retrieve the update history for a particular resource or resource type.
            /// </summary>
            [EnumLiteral("history")]
            History,
            /// <summary>
            /// Create a new resource with a server assigned id.
            /// </summary>
            [EnumLiteral("create")]
            Create,
            /// <summary>
            /// Search based on some filter criteria.
            /// </summary>
            [EnumLiteral("search")]
            Search,
            /// <summary>
            /// Update, create or delete a set of resources as a single transaction.
            /// </summary>
            [EnumLiteral("transaction")]
            Transaction,
            /// <summary>
            /// Get a conformance statement for the system.
            /// </summary>
            [EnumLiteral("conformance")]
            Conformance,
            /// <summary>
            /// Tag operations.
            /// </summary>
            [EnumLiteral("tags")]
            Tags,
            /// <summary>
            /// Not currently supported test operation.
            /// </summary>
            [EnumLiteral("mailbox")]
            Mailbox,
            /// <summary>
            /// Not currently supported test operation.
            /// </summary>
            [EnumLiteral("document")]
            Document,
            /// <summary>
            /// Make an assertion against the result of the last non-assertion operation.
            /// </summary>
            [EnumLiteral("assertion")]
            Assertion,
            /// <summary>
            /// Make a negative or false assertion against the result of the last non-assertion operation.
            /// </summary>
            [EnumLiteral("assertion_false")]
            AssertionFalse,
            /// <summary>
            /// Run an assertion function as a warning (instead of a failture) against the result of the last non-assertion operation.
            /// </summary>
            [EnumLiteral("assertion_warning")]
            AssertionWarning,
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
        
        [FhirType("TestScriptTestMetadataComponent")]
        [DataContract]
        public partial class TestScriptTestMetadataComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TestScriptTestMetadataComponent"; } }
            
            /// <summary>
            /// Link this test to the specification
            /// </summary>
            [FhirElement("link", InSummary=true, Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestScript.TestScriptTestMetadataLinkComponent> Link
            {
                get { if(_Link==null) _Link = new List<Hl7.Fhir.Model.TestScript.TestScriptTestMetadataLinkComponent>(); return _Link; }
                set { _Link = value; OnPropertyChanged("Link"); }
            }
            
            private List<Hl7.Fhir.Model.TestScript.TestScriptTestMetadataLinkComponent> _Link;
            
            /// <summary>
            /// Required capability that is assumed to function correctly on the FHIR server being tested
            /// </summary>
            [FhirElement("requires", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestScript.TestScriptTestMetadataRequiresComponent> Requires
            {
                get { if(_Requires==null) _Requires = new List<Hl7.Fhir.Model.TestScript.TestScriptTestMetadataRequiresComponent>(); return _Requires; }
                set { _Requires = value; OnPropertyChanged("Requires"); }
            }
            
            private List<Hl7.Fhir.Model.TestScript.TestScriptTestMetadataRequiresComponent> _Requires;
            
            /// <summary>
            /// Capability being verified
            /// </summary>
            [FhirElement("validates", InSummary=true, Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestScript.TestScriptTestMetadataValidatesComponent> Validates
            {
                get { if(_Validates==null) _Validates = new List<Hl7.Fhir.Model.TestScript.TestScriptTestMetadataValidatesComponent>(); return _Validates; }
                set { _Validates = value; OnPropertyChanged("Validates"); }
            }
            
            private List<Hl7.Fhir.Model.TestScript.TestScriptTestMetadataValidatesComponent> _Validates;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestScriptTestMetadataComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Link != null) dest.Link = new List<Hl7.Fhir.Model.TestScript.TestScriptTestMetadataLinkComponent>(Link.DeepCopy());
                    if(Requires != null) dest.Requires = new List<Hl7.Fhir.Model.TestScript.TestScriptTestMetadataRequiresComponent>(Requires.DeepCopy());
                    if(Validates != null) dest.Validates = new List<Hl7.Fhir.Model.TestScript.TestScriptTestMetadataValidatesComponent>(Validates.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TestScriptTestMetadataComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestScriptTestMetadataComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Link, otherT.Link)) return false;
                if( !DeepComparable.Matches(Requires, otherT.Requires)) return false;
                if( !DeepComparable.Matches(Validates, otherT.Validates)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestScriptTestMetadataComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Link, otherT.Link)) return false;
                if( !DeepComparable.IsExactly(Requires, otherT.Requires)) return false;
                if( !DeepComparable.IsExactly(Validates, otherT.Validates)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("TestScriptTeardownOperationComponent")]
        [DataContract]
        public partial class TestScriptTeardownOperationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TestScriptTeardownOperationComponent"; } }
            
            /// <summary>
            /// read | vread | update | delete | history | create | search | transaction | conformance | tags | mailbox | document | assertion | assertion_false | assertion_warning
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestScript.TestOperationType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.TestScript.TestOperationType> _TypeElement;
            
            /// <summary>
            /// read | vread | update | delete | history | create | search | transaction | conformance | tags | mailbox | document | assertion | assertion_false | assertion_warning
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.TestScript.TestOperationType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.TestScript.TestOperationType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// The id of the fixture used as the body in a PUT or POST
            /// </summary>
            [FhirElement("source", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Id SourceElement
            {
                get { return _SourceElement; }
                set { _SourceElement = value; OnPropertyChanged("SourceElement"); }
            }
            
            private Hl7.Fhir.Model.Id _SourceElement;
            
            /// <summary>
            /// The id of the fixture used as the body in a PUT or POST
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Source
            {
                get { return SourceElement != null ? SourceElement.Value : null; }
                set
                {
                    if(value == null)
                      SourceElement = null; 
                    else
                      SourceElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Source");
                }
            }
            
            /// <summary>
            /// The id of the fixture used as the target of a PUT or POST, or the id of the fixture used to store the results of a GET
            /// </summary>
            [FhirElement("target", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Id TargetElement
            {
                get { return _TargetElement; }
                set { _TargetElement = value; OnPropertyChanged("TargetElement"); }
            }
            
            private Hl7.Fhir.Model.Id _TargetElement;
            
            /// <summary>
            /// The id of the fixture used as the target of a PUT or POST, or the id of the fixture used to store the results of a GET
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Target
            {
                get { return TargetElement != null ? TargetElement.Value : null; }
                set
                {
                    if(value == null)
                      TargetElement = null; 
                    else
                      TargetElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Target");
                }
            }
            
            /// <summary>
            /// Which server to perform the operation on
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
            /// Arguments to an operation
            /// </summary>
            [FhirElement("parameter", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> ParameterElement
            {
                get { if(_ParameterElement==null) _ParameterElement = new List<Hl7.Fhir.Model.FhirString>(); return _ParameterElement; }
                set { _ParameterElement = value; OnPropertyChanged("ParameterElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _ParameterElement;
            
            /// <summary>
            /// Arguments to an operation
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Parameter
            {
                get { return ParameterElement != null ? ParameterElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      ParameterElement = null; 
                    else
                      ParameterElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("Parameter");
                }
            }
            
            /// <summary>
            /// Response id
            /// </summary>
            [FhirElement("responseId", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Id ResponseIdElement
            {
                get { return _ResponseIdElement; }
                set { _ResponseIdElement = value; OnPropertyChanged("ResponseIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _ResponseIdElement;
            
            /// <summary>
            /// Response id
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
            /// xml | json
            /// </summary>
            [FhirElement("contentType", InSummary=true, Order=100)]
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestScriptTeardownOperationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.TestScript.TestOperationType>)TypeElement.DeepCopy();
                    if(SourceElement != null) dest.SourceElement = (Hl7.Fhir.Model.Id)SourceElement.DeepCopy();
                    if(TargetElement != null) dest.TargetElement = (Hl7.Fhir.Model.Id)TargetElement.DeepCopy();
                    if(DestinationElement != null) dest.DestinationElement = (Hl7.Fhir.Model.Integer)DestinationElement.DeepCopy();
                    if(ParameterElement != null) dest.ParameterElement = new List<Hl7.Fhir.Model.FhirString>(ParameterElement.DeepCopy());
                    if(ResponseIdElement != null) dest.ResponseIdElement = (Hl7.Fhir.Model.Id)ResponseIdElement.DeepCopy();
                    if(ContentTypeElement != null) dest.ContentTypeElement = (Code<Hl7.Fhir.Model.TestScript.ContentType>)ContentTypeElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TestScriptTeardownOperationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestScriptTeardownOperationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(SourceElement, otherT.SourceElement)) return false;
                if( !DeepComparable.Matches(TargetElement, otherT.TargetElement)) return false;
                if( !DeepComparable.Matches(DestinationElement, otherT.DestinationElement)) return false;
                if( !DeepComparable.Matches(ParameterElement, otherT.ParameterElement)) return false;
                if( !DeepComparable.Matches(ResponseIdElement, otherT.ResponseIdElement)) return false;
                if( !DeepComparable.Matches(ContentTypeElement, otherT.ContentTypeElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestScriptTeardownOperationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(SourceElement, otherT.SourceElement)) return false;
                if( !DeepComparable.IsExactly(TargetElement, otherT.TargetElement)) return false;
                if( !DeepComparable.IsExactly(DestinationElement, otherT.DestinationElement)) return false;
                if( !DeepComparable.IsExactly(ParameterElement, otherT.ParameterElement)) return false;
                if( !DeepComparable.IsExactly(ResponseIdElement, otherT.ResponseIdElement)) return false;
                if( !DeepComparable.IsExactly(ContentTypeElement, otherT.ContentTypeElement)) return false;
                
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
            /// A setup operation
            /// </summary>
            [FhirElement("operation", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestScript.TestScriptSetupOperationComponent> Operation
            {
                get { if(_Operation==null) _Operation = new List<Hl7.Fhir.Model.TestScript.TestScriptSetupOperationComponent>(); return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            
            private List<Hl7.Fhir.Model.TestScript.TestScriptSetupOperationComponent> _Operation;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestScriptSetupComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Operation != null) dest.Operation = new List<Hl7.Fhir.Model.TestScript.TestScriptSetupOperationComponent>(Operation.DeepCopy());
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
                if( !DeepComparable.Matches(Operation, otherT.Operation)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestScriptSetupComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Operation, otherT.Operation)) return false;
                
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
            /// URI of the fixture
            /// </summary>
            [FhirElement("uri", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UriElement
            {
                get { return _UriElement; }
                set { _UriElement = value; OnPropertyChanged("UriElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _UriElement;
            
            /// <summary>
            /// URI of the fixture
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Uri
            {
                get { return UriElement != null ? UriElement.Value : null; }
                set
                {
                    if(value == null)
                      UriElement = null; 
                    else
                      UriElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Uri");
                }
            }
            
            /// <summary>
            /// Fixture resource
            /// </summary>
            [FhirElement("resource", InSummary=true, Order=50, Choice=ChoiceType.ResourceChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Resource))]
            [DataMember]
            public Hl7.Fhir.Model.Resource Resource
            {
                get { return _Resource; }
                set { _Resource = value; OnPropertyChanged("Resource"); }
            }
            
            private Hl7.Fhir.Model.Resource _Resource;
            
            /// <summary>
            /// Whether or not to implicitly create the fixture during setup
            /// </summary>
            [FhirElement("autocreate", InSummary=true, Order=60)]
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
            [FhirElement("autodelete", InSummary=true, Order=70)]
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestScriptFixtureComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(UriElement != null) dest.UriElement = (Hl7.Fhir.Model.FhirUri)UriElement.DeepCopy();
                    if(Resource != null) dest.Resource = (Hl7.Fhir.Model.Resource)Resource.DeepCopy();
                    if(AutocreateElement != null) dest.AutocreateElement = (Hl7.Fhir.Model.FhirBoolean)AutocreateElement.DeepCopy();
                    if(AutodeleteElement != null) dest.AutodeleteElement = (Hl7.Fhir.Model.FhirBoolean)AutodeleteElement.DeepCopy();
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
                if( !DeepComparable.Matches(UriElement, otherT.UriElement)) return false;
                if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;
                if( !DeepComparable.Matches(AutocreateElement, otherT.AutocreateElement)) return false;
                if( !DeepComparable.Matches(AutodeleteElement, otherT.AutodeleteElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestScriptFixtureComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(UriElement, otherT.UriElement)) return false;
                if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
                if( !DeepComparable.IsExactly(AutocreateElement, otherT.AutocreateElement)) return false;
                if( !DeepComparable.IsExactly(AutodeleteElement, otherT.AutodeleteElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("TestScriptSetupOperationComponent")]
        [DataContract]
        public partial class TestScriptSetupOperationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TestScriptSetupOperationComponent"; } }
            
            /// <summary>
            /// read | vread | update | delete | history | create | search | transaction | conformance | tags | mailbox | document | assertion | assertion_false | assertion_warning
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestScript.TestOperationType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.TestScript.TestOperationType> _TypeElement;
            
            /// <summary>
            /// read | vread | update | delete | history | create | search | transaction | conformance | tags | mailbox | document | assertion | assertion_false | assertion_warning
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.TestScript.TestOperationType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.TestScript.TestOperationType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// The id of the fixture used as the body in a PUT or POST
            /// </summary>
            [FhirElement("source", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Id SourceElement
            {
                get { return _SourceElement; }
                set { _SourceElement = value; OnPropertyChanged("SourceElement"); }
            }
            
            private Hl7.Fhir.Model.Id _SourceElement;
            
            /// <summary>
            /// The id of the fixture used as the body in a PUT or POST
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Source
            {
                get { return SourceElement != null ? SourceElement.Value : null; }
                set
                {
                    if(value == null)
                      SourceElement = null; 
                    else
                      SourceElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Source");
                }
            }
            
            /// <summary>
            /// The id of the fixture used as the target of a PUT or POST, or the id of the fixture used to store the results of a GET
            /// </summary>
            [FhirElement("target", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Id TargetElement
            {
                get { return _TargetElement; }
                set { _TargetElement = value; OnPropertyChanged("TargetElement"); }
            }
            
            private Hl7.Fhir.Model.Id _TargetElement;
            
            /// <summary>
            /// The id of the fixture used as the target of a PUT or POST, or the id of the fixture used to store the results of a GET
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Target
            {
                get { return TargetElement != null ? TargetElement.Value : null; }
                set
                {
                    if(value == null)
                      TargetElement = null; 
                    else
                      TargetElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Target");
                }
            }
            
            /// <summary>
            /// Which server to perform the operation on
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
            /// Arguments to an operation
            /// </summary>
            [FhirElement("parameter", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> ParameterElement
            {
                get { if(_ParameterElement==null) _ParameterElement = new List<Hl7.Fhir.Model.FhirString>(); return _ParameterElement; }
                set { _ParameterElement = value; OnPropertyChanged("ParameterElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _ParameterElement;
            
            /// <summary>
            /// Arguments to an operation
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Parameter
            {
                get { return ParameterElement != null ? ParameterElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      ParameterElement = null; 
                    else
                      ParameterElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("Parameter");
                }
            }
            
            /// <summary>
            /// Response id
            /// </summary>
            [FhirElement("responseId", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Id ResponseIdElement
            {
                get { return _ResponseIdElement; }
                set { _ResponseIdElement = value; OnPropertyChanged("ResponseIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _ResponseIdElement;
            
            /// <summary>
            /// Response id
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
            /// xml | json
            /// </summary>
            [FhirElement("contentType", InSummary=true, Order=100)]
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestScriptSetupOperationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.TestScript.TestOperationType>)TypeElement.DeepCopy();
                    if(SourceElement != null) dest.SourceElement = (Hl7.Fhir.Model.Id)SourceElement.DeepCopy();
                    if(TargetElement != null) dest.TargetElement = (Hl7.Fhir.Model.Id)TargetElement.DeepCopy();
                    if(DestinationElement != null) dest.DestinationElement = (Hl7.Fhir.Model.Integer)DestinationElement.DeepCopy();
                    if(ParameterElement != null) dest.ParameterElement = new List<Hl7.Fhir.Model.FhirString>(ParameterElement.DeepCopy());
                    if(ResponseIdElement != null) dest.ResponseIdElement = (Hl7.Fhir.Model.Id)ResponseIdElement.DeepCopy();
                    if(ContentTypeElement != null) dest.ContentTypeElement = (Code<Hl7.Fhir.Model.TestScript.ContentType>)ContentTypeElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TestScriptSetupOperationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestScriptSetupOperationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(SourceElement, otherT.SourceElement)) return false;
                if( !DeepComparable.Matches(TargetElement, otherT.TargetElement)) return false;
                if( !DeepComparable.Matches(DestinationElement, otherT.DestinationElement)) return false;
                if( !DeepComparable.Matches(ParameterElement, otherT.ParameterElement)) return false;
                if( !DeepComparable.Matches(ResponseIdElement, otherT.ResponseIdElement)) return false;
                if( !DeepComparable.Matches(ContentTypeElement, otherT.ContentTypeElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestScriptSetupOperationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(SourceElement, otherT.SourceElement)) return false;
                if( !DeepComparable.IsExactly(TargetElement, otherT.TargetElement)) return false;
                if( !DeepComparable.IsExactly(DestinationElement, otherT.DestinationElement)) return false;
                if( !DeepComparable.IsExactly(ParameterElement, otherT.ParameterElement)) return false;
                if( !DeepComparable.IsExactly(ResponseIdElement, otherT.ResponseIdElement)) return false;
                if( !DeepComparable.IsExactly(ContentTypeElement, otherT.ContentTypeElement)) return false;
                
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
            /// A teardown operation
            /// </summary>
            [FhirElement("operation", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestScript.TestScriptTeardownOperationComponent> Operation
            {
                get { if(_Operation==null) _Operation = new List<Hl7.Fhir.Model.TestScript.TestScriptTeardownOperationComponent>(); return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            
            private List<Hl7.Fhir.Model.TestScript.TestScriptTeardownOperationComponent> _Operation;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestScriptTeardownComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Operation != null) dest.Operation = new List<Hl7.Fhir.Model.TestScript.TestScriptTeardownOperationComponent>(Operation.DeepCopy());
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
                if( !DeepComparable.Matches(Operation, otherT.Operation)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestScriptTeardownComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Operation, otherT.Operation)) return false;
                
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
            /// Metadata about this Test
            /// </summary>
            [FhirElement("metadata", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.TestScript.TestScriptTestMetadataComponent Metadata
            {
                get { return _Metadata; }
                set { _Metadata = value; OnPropertyChanged("Metadata"); }
            }
            
            private Hl7.Fhir.Model.TestScript.TestScriptTestMetadataComponent _Metadata;
            
            /// <summary>
            /// Each test must have at least one operation
            /// </summary>
            [FhirElement("operation", InSummary=true, Order=70)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestScript.TestScriptTestOperationComponent> Operation
            {
                get { if(_Operation==null) _Operation = new List<Hl7.Fhir.Model.TestScript.TestScriptTestOperationComponent>(); return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            
            private List<Hl7.Fhir.Model.TestScript.TestScriptTestOperationComponent> _Operation;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestScriptTestComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Metadata != null) dest.Metadata = (Hl7.Fhir.Model.TestScript.TestScriptTestMetadataComponent)Metadata.DeepCopy();
                    if(Operation != null) dest.Operation = new List<Hl7.Fhir.Model.TestScript.TestScriptTestOperationComponent>(Operation.DeepCopy());
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
                if( !DeepComparable.Matches(Operation, otherT.Operation)) return false;
                
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
                if( !DeepComparable.IsExactly(Operation, otherT.Operation)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("TestScriptTestOperationComponent")]
        [DataContract]
        public partial class TestScriptTestOperationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TestScriptTestOperationComponent"; } }
            
            /// <summary>
            /// read | vread | update | delete | history | create | search | transaction | conformance | tags | mailbox | document | assertion | assertion_false | assertion_warning
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestScript.TestOperationType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.TestScript.TestOperationType> _TypeElement;
            
            /// <summary>
            /// read | vread | update | delete | history | create | search | transaction | conformance | tags | mailbox | document | assertion | assertion_false | assertion_warning
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.TestScript.TestOperationType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.TestScript.TestOperationType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// The id of the fixture used as the body in a PUT or POST
            /// </summary>
            [FhirElement("source", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Id SourceElement
            {
                get { return _SourceElement; }
                set { _SourceElement = value; OnPropertyChanged("SourceElement"); }
            }
            
            private Hl7.Fhir.Model.Id _SourceElement;
            
            /// <summary>
            /// The id of the fixture used as the body in a PUT or POST
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Source
            {
                get { return SourceElement != null ? SourceElement.Value : null; }
                set
                {
                    if(value == null)
                      SourceElement = null; 
                    else
                      SourceElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Source");
                }
            }
            
            /// <summary>
            /// The id of the fixture used as the target of a PUT or POST, or the id of the fixture used to store the results of a GET
            /// </summary>
            [FhirElement("target", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Id TargetElement
            {
                get { return _TargetElement; }
                set { _TargetElement = value; OnPropertyChanged("TargetElement"); }
            }
            
            private Hl7.Fhir.Model.Id _TargetElement;
            
            /// <summary>
            /// The id of the fixture used as the target of a PUT or POST, or the id of the fixture used to store the results of a GET
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Target
            {
                get { return TargetElement != null ? TargetElement.Value : null; }
                set
                {
                    if(value == null)
                      TargetElement = null; 
                    else
                      TargetElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Target");
                }
            }
            
            /// <summary>
            /// Which server to perform the operation on
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
            /// Arguments to an operation
            /// </summary>
            [FhirElement("parameter", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> ParameterElement
            {
                get { if(_ParameterElement==null) _ParameterElement = new List<Hl7.Fhir.Model.FhirString>(); return _ParameterElement; }
                set { _ParameterElement = value; OnPropertyChanged("ParameterElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _ParameterElement;
            
            /// <summary>
            /// Arguments to an operation
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Parameter
            {
                get { return ParameterElement != null ? ParameterElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      ParameterElement = null; 
                    else
                      ParameterElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("Parameter");
                }
            }
            
            /// <summary>
            /// Response id
            /// </summary>
            [FhirElement("responseId", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Id ResponseIdElement
            {
                get { return _ResponseIdElement; }
                set { _ResponseIdElement = value; OnPropertyChanged("ResponseIdElement"); }
            }
            
            private Hl7.Fhir.Model.Id _ResponseIdElement;
            
            /// <summary>
            /// Response id
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
            /// xml | json
            /// </summary>
            [FhirElement("contentType", InSummary=true, Order=100)]
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestScriptTestOperationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.TestScript.TestOperationType>)TypeElement.DeepCopy();
                    if(SourceElement != null) dest.SourceElement = (Hl7.Fhir.Model.Id)SourceElement.DeepCopy();
                    if(TargetElement != null) dest.TargetElement = (Hl7.Fhir.Model.Id)TargetElement.DeepCopy();
                    if(DestinationElement != null) dest.DestinationElement = (Hl7.Fhir.Model.Integer)DestinationElement.DeepCopy();
                    if(ParameterElement != null) dest.ParameterElement = new List<Hl7.Fhir.Model.FhirString>(ParameterElement.DeepCopy());
                    if(ResponseIdElement != null) dest.ResponseIdElement = (Hl7.Fhir.Model.Id)ResponseIdElement.DeepCopy();
                    if(ContentTypeElement != null) dest.ContentTypeElement = (Code<Hl7.Fhir.Model.TestScript.ContentType>)ContentTypeElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TestScriptTestOperationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestScriptTestOperationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(SourceElement, otherT.SourceElement)) return false;
                if( !DeepComparable.Matches(TargetElement, otherT.TargetElement)) return false;
                if( !DeepComparable.Matches(DestinationElement, otherT.DestinationElement)) return false;
                if( !DeepComparable.Matches(ParameterElement, otherT.ParameterElement)) return false;
                if( !DeepComparable.Matches(ResponseIdElement, otherT.ResponseIdElement)) return false;
                if( !DeepComparable.Matches(ContentTypeElement, otherT.ContentTypeElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestScriptTestOperationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(SourceElement, otherT.SourceElement)) return false;
                if( !DeepComparable.IsExactly(TargetElement, otherT.TargetElement)) return false;
                if( !DeepComparable.IsExactly(DestinationElement, otherT.DestinationElement)) return false;
                if( !DeepComparable.IsExactly(ParameterElement, otherT.ParameterElement)) return false;
                if( !DeepComparable.IsExactly(ResponseIdElement, otherT.ResponseIdElement)) return false;
                if( !DeepComparable.IsExactly(ContentTypeElement, otherT.ContentTypeElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("TestScriptTestMetadataRequiresComponent")]
        [DataContract]
        public partial class TestScriptTestMetadataRequiresComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TestScriptTestMetadataRequiresComponent"; } }
            
            /// <summary>
            /// Required resource type
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Hl7.Fhir.Model.Code _TypeElement;
            
            /// <summary>
            /// Required resource type
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Required operations
            /// </summary>
            [FhirElement("operations", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString OperationsElement
            {
                get { return _OperationsElement; }
                set { _OperationsElement = value; OnPropertyChanged("OperationsElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _OperationsElement;
            
            /// <summary>
            /// Required operations
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Operations
            {
                get { return OperationsElement != null ? OperationsElement.Value : null; }
                set
                {
                    if(value == null)
                      OperationsElement = null; 
                    else
                      OperationsElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Operations");
                }
            }
            
            /// <summary>
            /// Which server this requirement applies to
            /// </summary>
            [FhirElement("destination", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Integer DestinationElement
            {
                get { return _DestinationElement; }
                set { _DestinationElement = value; OnPropertyChanged("DestinationElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _DestinationElement;
            
            /// <summary>
            /// Which server this requirement applies to
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestScriptTestMetadataRequiresComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.Code)TypeElement.DeepCopy();
                    if(OperationsElement != null) dest.OperationsElement = (Hl7.Fhir.Model.FhirString)OperationsElement.DeepCopy();
                    if(DestinationElement != null) dest.DestinationElement = (Hl7.Fhir.Model.Integer)DestinationElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TestScriptTestMetadataRequiresComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestScriptTestMetadataRequiresComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(OperationsElement, otherT.OperationsElement)) return false;
                if( !DeepComparable.Matches(DestinationElement, otherT.DestinationElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestScriptTestMetadataRequiresComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(OperationsElement, otherT.OperationsElement)) return false;
                if( !DeepComparable.IsExactly(DestinationElement, otherT.DestinationElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("TestScriptTestMetadataLinkComponent")]
        [DataContract]
        public partial class TestScriptTestMetadataLinkComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TestScriptTestMetadataLinkComponent"; } }
            
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
                var dest = other as TestScriptTestMetadataLinkComponent;
                
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
                return CopyTo(new TestScriptTestMetadataLinkComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestScriptTestMetadataLinkComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestScriptTestMetadataLinkComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("TestScriptTestMetadataValidatesComponent")]
        [DataContract]
        public partial class TestScriptTestMetadataValidatesComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TestScriptTestMetadataValidatesComponent"; } }
            
            /// <summary>
            /// Verified resource type
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Hl7.Fhir.Model.Code _TypeElement;
            
            /// <summary>
            /// Verified resource type
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Verified operations
            /// </summary>
            [FhirElement("operations", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString OperationsElement
            {
                get { return _OperationsElement; }
                set { _OperationsElement = value; OnPropertyChanged("OperationsElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _OperationsElement;
            
            /// <summary>
            /// Verified operations
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Operations
            {
                get { return OperationsElement != null ? OperationsElement.Value : null; }
                set
                {
                    if(value == null)
                      OperationsElement = null; 
                    else
                      OperationsElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Operations");
                }
            }
            
            /// <summary>
            /// Which server this validation applies to
            /// </summary>
            [FhirElement("destination", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Integer DestinationElement
            {
                get { return _DestinationElement; }
                set { _DestinationElement = value; OnPropertyChanged("DestinationElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _DestinationElement;
            
            /// <summary>
            /// Which server this validation applies to
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestScriptTestMetadataValidatesComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.Code)TypeElement.DeepCopy();
                    if(OperationsElement != null) dest.OperationsElement = (Hl7.Fhir.Model.FhirString)OperationsElement.DeepCopy();
                    if(DestinationElement != null) dest.DestinationElement = (Hl7.Fhir.Model.Integer)DestinationElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TestScriptTestMetadataValidatesComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestScriptTestMetadataValidatesComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(OperationsElement, otherT.OperationsElement)) return false;
                if( !DeepComparable.Matches(DestinationElement, otherT.DestinationElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestScriptTestMetadataValidatesComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(OperationsElement, otherT.OperationsElement)) return false;
                if( !DeepComparable.IsExactly(DestinationElement, otherT.DestinationElement)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Name
        /// </summary>
        [FhirElement("name", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Name
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
        /// Short description
        /// </summary>
        [FhirElement("description", Order=100)]
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
        
        /// <summary>
        /// If the tests apply to more than one FHIR server
        /// </summary>
        [FhirElement("multiserver", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean MultiserverElement
        {
            get { return _MultiserverElement; }
            set { _MultiserverElement = value; OnPropertyChanged("MultiserverElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _MultiserverElement;
        
        /// <summary>
        /// If the tests apply to more than one FHIR server
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
        /// Fixture in the test script - either by reference (uri) or embedded (Resource)
        /// </summary>
        [FhirElement("fixture", Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.TestScript.TestScriptFixtureComponent> Fixture
        {
            get { if(_Fixture==null) _Fixture = new List<Hl7.Fhir.Model.TestScript.TestScriptFixtureComponent>(); return _Fixture; }
            set { _Fixture = value; OnPropertyChanged("Fixture"); }
        }
        
        private List<Hl7.Fhir.Model.TestScript.TestScriptFixtureComponent> _Fixture;
        
        /// <summary>
        /// A series of required setup operations before tests are executed
        /// </summary>
        [FhirElement("setup", Order=130)]
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
        [FhirElement("test", Order=140)]
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
        [FhirElement("teardown", Order=150)]
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
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(MultiserverElement != null) dest.MultiserverElement = (Hl7.Fhir.Model.FhirBoolean)MultiserverElement.DeepCopy();
                if(Fixture != null) dest.Fixture = new List<Hl7.Fhir.Model.TestScript.TestScriptFixtureComponent>(Fixture.DeepCopy());
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
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(MultiserverElement, otherT.MultiserverElement)) return false;
            if( !DeepComparable.Matches(Fixture, otherT.Fixture)) return false;
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
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(MultiserverElement, otherT.MultiserverElement)) return false;
            if( !DeepComparable.IsExactly(Fixture, otherT.Fixture)) return false;
            if( !DeepComparable.IsExactly(Setup, otherT.Setup)) return false;
            if( !DeepComparable.IsExactly(Test, otherT.Test)) return false;
            if( !DeepComparable.IsExactly(Teardown, otherT.Teardown)) return false;
            
            return true;
        }
        
    }
    
}
