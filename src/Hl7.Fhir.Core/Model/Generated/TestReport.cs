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
    /// Describes the results of a TestScript execution
    /// </summary>
    [FhirType("TestReport", IsResource=true)]
    [DataContract]
    public partial class TestReport : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.TestReport; } }
        [NotMapped]
        public override string TypeName { get { return "TestReport"; } }
        
        /// <summary>
        /// The current status of the test report.
        /// (url: http://hl7.org/fhir/ValueSet/report-status-codes)
        /// </summary>
        [FhirEnumeration("TestReportStatus")]
        public enum TestReportStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/report-status-codes)
            /// </summary>
            [EnumLiteral("completed", "http://hl7.org/fhir/report-status-codes"), Description("Completed")]
            Completed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/report-status-codes)
            /// </summary>
            [EnumLiteral("in-progress", "http://hl7.org/fhir/report-status-codes"), Description("In Progress")]
            InProgress,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/report-status-codes)
            /// </summary>
            [EnumLiteral("waiting", "http://hl7.org/fhir/report-status-codes"), Description("Waiting")]
            Waiting,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/report-status-codes)
            /// </summary>
            [EnumLiteral("stopped", "http://hl7.org/fhir/report-status-codes"), Description("Stopped")]
            Stopped,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/report-status-codes)
            /// </summary>
            [EnumLiteral("entered-in-error", "http://hl7.org/fhir/report-status-codes"), Description("Entered In Error")]
            EnteredInError,
        }

        /// <summary>
        /// The reported execution result.
        /// (url: http://hl7.org/fhir/ValueSet/report-result-codes)
        /// </summary>
        [FhirEnumeration("TestReportResult")]
        public enum TestReportResult
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/report-result-codes)
            /// </summary>
            [EnumLiteral("pass", "http://hl7.org/fhir/report-result-codes"), Description("Pass")]
            Pass,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/report-result-codes)
            /// </summary>
            [EnumLiteral("fail", "http://hl7.org/fhir/report-result-codes"), Description("Fail")]
            Fail,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/report-result-codes)
            /// </summary>
            [EnumLiteral("pending", "http://hl7.org/fhir/report-result-codes"), Description("Pending")]
            Pending,
        }

        /// <summary>
        /// The type of participant.
        /// (url: http://hl7.org/fhir/ValueSet/report-participant-type)
        /// </summary>
        [FhirEnumeration("TestReportParticipantType")]
        public enum TestReportParticipantType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/report-participant-type)
            /// </summary>
            [EnumLiteral("test-engine", "http://hl7.org/fhir/report-participant-type"), Description("Test Engine")]
            TestEngine,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/report-participant-type)
            /// </summary>
            [EnumLiteral("client", "http://hl7.org/fhir/report-participant-type"), Description("Client")]
            Client,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/report-participant-type)
            /// </summary>
            [EnumLiteral("server", "http://hl7.org/fhir/report-participant-type"), Description("Server")]
            Server,
        }

        /// <summary>
        /// The results of executing an action.
        /// (url: http://hl7.org/fhir/ValueSet/report-action-result-codes)
        /// </summary>
        [FhirEnumeration("TestReportActionResult")]
        public enum TestReportActionResult
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/report-action-result-codes)
            /// </summary>
            [EnumLiteral("pass", "http://hl7.org/fhir/report-action-result-codes"), Description("Pass")]
            Pass,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/report-action-result-codes)
            /// </summary>
            [EnumLiteral("skip", "http://hl7.org/fhir/report-action-result-codes"), Description("Skip")]
            Skip,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/report-action-result-codes)
            /// </summary>
            [EnumLiteral("fail", "http://hl7.org/fhir/report-action-result-codes"), Description("Fail")]
            Fail,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/report-action-result-codes)
            /// </summary>
            [EnumLiteral("warning", "http://hl7.org/fhir/report-action-result-codes"), Description("Warning")]
            Warning,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/report-action-result-codes)
            /// </summary>
            [EnumLiteral("error", "http://hl7.org/fhir/report-action-result-codes"), Description("Error")]
            Error,
        }

        [FhirType("ParticipantComponent")]
        [DataContract]
        public partial class ParticipantComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ParticipantComponent"; } }
            
            /// <summary>
            /// test-engine | client | server
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestReport.TestReportParticipantType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.TestReport.TestReportParticipantType> _TypeElement;
            
            /// <summary>
            /// test-engine | client | server
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.TestReport.TestReportParticipantType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        TypeElement = null; 
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.TestReport.TestReportParticipantType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// The uri of the participant. An absolute URL is preferred
            /// </summary>
            [FhirElement("uri", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UriElement
            {
                get { return _UriElement; }
                set { _UriElement = value; OnPropertyChanged("UriElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _UriElement;
            
            /// <summary>
            /// The uri of the participant. An absolute URL is preferred
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Uri
            {
                get { return UriElement != null ? UriElement.Value : null; }
                set
                {
                    if (value == null)
                        UriElement = null; 
                    else
                        UriElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Uri");
                }
            }
            
            /// <summary>
            /// The display name of the participant
            /// </summary>
            [FhirElement("display", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DisplayElement
            {
                get { return _DisplayElement; }
                set { _DisplayElement = value; OnPropertyChanged("DisplayElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DisplayElement;
            
            /// <summary>
            /// The display name of the participant
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Display
            {
                get { return DisplayElement != null ? DisplayElement.Value : null; }
                set
                {
                    if (value == null)
                        DisplayElement = null; 
                    else
                        DisplayElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Display");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ParticipantComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.TestReport.TestReportParticipantType>)TypeElement.DeepCopy();
                    if(UriElement != null) dest.UriElement = (Hl7.Fhir.Model.FhirUri)UriElement.DeepCopy();
                    if(DisplayElement != null) dest.DisplayElement = (Hl7.Fhir.Model.FhirString)DisplayElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ParticipantComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ParticipantComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(UriElement, otherT.UriElement)) return false;
                if( !DeepComparable.Matches(DisplayElement, otherT.DisplayElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ParticipantComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(UriElement, otherT.UriElement)) return false;
                if( !DeepComparable.IsExactly(DisplayElement, otherT.DisplayElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TypeElement != null) yield return TypeElement;
                    if (UriElement != null) yield return UriElement;
                    if (DisplayElement != null) yield return DisplayElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (UriElement != null) yield return new ElementValue("uri", UriElement);
                    if (DisplayElement != null) yield return new ElementValue("display", DisplayElement);
                }
            }

            
        }
        
        
        [FhirType("SetupComponent")]
        [DataContract]
        public partial class SetupComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "SetupComponent"; } }
            
            /// <summary>
            /// A setup operation or assert that was executed
            /// </summary>
            [FhirElement("action", Order=40)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestReport.SetupActionComponent> Action
            {
                get { if(_Action==null) _Action = new List<Hl7.Fhir.Model.TestReport.SetupActionComponent>(); return _Action; }
                set { _Action = value; OnPropertyChanged("Action"); }
            }
            
            private List<Hl7.Fhir.Model.TestReport.SetupActionComponent> _Action;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SetupComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Action != null) dest.Action = new List<Hl7.Fhir.Model.TestReport.SetupActionComponent>(Action.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SetupComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SetupComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Action, otherT.Action)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SetupComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Action) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Action) { if (elem != null) yield return new ElementValue("action", elem); }
                }
            }

            
        }
        
        
        [FhirType("SetupActionComponent")]
        [DataContract]
        public partial class SetupActionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "SetupActionComponent"; } }
            
            /// <summary>
            /// The operation to perform
            /// </summary>
            [FhirElement("operation", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.TestReport.OperationComponent Operation
            {
                get { return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            
            private Hl7.Fhir.Model.TestReport.OperationComponent _Operation;
            
            /// <summary>
            /// The assertion to perform
            /// </summary>
            [FhirElement("assert", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.TestReport.AssertComponent Assert
            {
                get { return _Assert; }
                set { _Assert = value; OnPropertyChanged("Assert"); }
            }
            
            private Hl7.Fhir.Model.TestReport.AssertComponent _Assert;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SetupActionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Operation != null) dest.Operation = (Hl7.Fhir.Model.TestReport.OperationComponent)Operation.DeepCopy();
                    if(Assert != null) dest.Assert = (Hl7.Fhir.Model.TestReport.AssertComponent)Assert.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SetupActionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SetupActionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Operation, otherT.Operation)) return false;
                if( !DeepComparable.Matches(Assert, otherT.Assert)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SetupActionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Operation, otherT.Operation)) return false;
                if( !DeepComparable.IsExactly(Assert, otherT.Assert)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Operation != null) yield return Operation;
                    if (Assert != null) yield return Assert;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Operation != null) yield return new ElementValue("operation", Operation);
                    if (Assert != null) yield return new ElementValue("assert", Assert);
                }
            }

            
        }
        
        
        [FhirType("OperationComponent")]
        [DataContract]
        public partial class OperationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "OperationComponent"; } }
            
            /// <summary>
            /// pass | skip | fail | warning | error
            /// </summary>
            [FhirElement("result", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestReport.TestReportActionResult> ResultElement
            {
                get { return _ResultElement; }
                set { _ResultElement = value; OnPropertyChanged("ResultElement"); }
            }
            
            private Code<Hl7.Fhir.Model.TestReport.TestReportActionResult> _ResultElement;
            
            /// <summary>
            /// pass | skip | fail | warning | error
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.TestReport.TestReportActionResult? Result
            {
                get { return ResultElement != null ? ResultElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ResultElement = null; 
                    else
                        ResultElement = new Code<Hl7.Fhir.Model.TestReport.TestReportActionResult>(value);
                    OnPropertyChanged("Result");
                }
            }
            
            /// <summary>
            /// A message associated with the result
            /// </summary>
            [FhirElement("message", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown Message
            {
                get { return _Message; }
                set { _Message = value; OnPropertyChanged("Message"); }
            }
            
            private Hl7.Fhir.Model.Markdown _Message;
            
            /// <summary>
            /// A link to further details on the result
            /// </summary>
            [FhirElement("detail", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri DetailElement
            {
                get { return _DetailElement; }
                set { _DetailElement = value; OnPropertyChanged("DetailElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _DetailElement;
            
            /// <summary>
            /// A link to further details on the result
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Detail
            {
                get { return DetailElement != null ? DetailElement.Value : null; }
                set
                {
                    if (value == null)
                        DetailElement = null; 
                    else
                        DetailElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Detail");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as OperationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ResultElement != null) dest.ResultElement = (Code<Hl7.Fhir.Model.TestReport.TestReportActionResult>)ResultElement.DeepCopy();
                    if(Message != null) dest.Message = (Hl7.Fhir.Model.Markdown)Message.DeepCopy();
                    if(DetailElement != null) dest.DetailElement = (Hl7.Fhir.Model.FhirUri)DetailElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new OperationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as OperationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ResultElement, otherT.ResultElement)) return false;
                if( !DeepComparable.Matches(Message, otherT.Message)) return false;
                if( !DeepComparable.Matches(DetailElement, otherT.DetailElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as OperationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ResultElement, otherT.ResultElement)) return false;
                if( !DeepComparable.IsExactly(Message, otherT.Message)) return false;
                if( !DeepComparable.IsExactly(DetailElement, otherT.DetailElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (ResultElement != null) yield return ResultElement;
                    if (Message != null) yield return Message;
                    if (DetailElement != null) yield return DetailElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (ResultElement != null) yield return new ElementValue("result", ResultElement);
                    if (Message != null) yield return new ElementValue("message", Message);
                    if (DetailElement != null) yield return new ElementValue("detail", DetailElement);
                }
            }

            
        }
        
        
        [FhirType("AssertComponent")]
        [DataContract]
        public partial class AssertComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "AssertComponent"; } }
            
            /// <summary>
            /// pass | skip | fail | warning | error
            /// </summary>
            [FhirElement("result", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestReport.TestReportActionResult> ResultElement
            {
                get { return _ResultElement; }
                set { _ResultElement = value; OnPropertyChanged("ResultElement"); }
            }
            
            private Code<Hl7.Fhir.Model.TestReport.TestReportActionResult> _ResultElement;
            
            /// <summary>
            /// pass | skip | fail | warning | error
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.TestReport.TestReportActionResult? Result
            {
                get { return ResultElement != null ? ResultElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ResultElement = null; 
                    else
                        ResultElement = new Code<Hl7.Fhir.Model.TestReport.TestReportActionResult>(value);
                    OnPropertyChanged("Result");
                }
            }
            
            /// <summary>
            /// A message associated with the result
            /// </summary>
            [FhirElement("message", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown Message
            {
                get { return _Message; }
                set { _Message = value; OnPropertyChanged("Message"); }
            }
            
            private Hl7.Fhir.Model.Markdown _Message;
            
            /// <summary>
            /// A link to further details on the result
            /// </summary>
            [FhirElement("detail", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DetailElement
            {
                get { return _DetailElement; }
                set { _DetailElement = value; OnPropertyChanged("DetailElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DetailElement;
            
            /// <summary>
            /// A link to further details on the result
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Detail
            {
                get { return DetailElement != null ? DetailElement.Value : null; }
                set
                {
                    if (value == null)
                        DetailElement = null; 
                    else
                        DetailElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Detail");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AssertComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ResultElement != null) dest.ResultElement = (Code<Hl7.Fhir.Model.TestReport.TestReportActionResult>)ResultElement.DeepCopy();
                    if(Message != null) dest.Message = (Hl7.Fhir.Model.Markdown)Message.DeepCopy();
                    if(DetailElement != null) dest.DetailElement = (Hl7.Fhir.Model.FhirString)DetailElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new AssertComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AssertComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ResultElement, otherT.ResultElement)) return false;
                if( !DeepComparable.Matches(Message, otherT.Message)) return false;
                if( !DeepComparable.Matches(DetailElement, otherT.DetailElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AssertComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ResultElement, otherT.ResultElement)) return false;
                if( !DeepComparable.IsExactly(Message, otherT.Message)) return false;
                if( !DeepComparable.IsExactly(DetailElement, otherT.DetailElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (ResultElement != null) yield return ResultElement;
                    if (Message != null) yield return Message;
                    if (DetailElement != null) yield return DetailElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (ResultElement != null) yield return new ElementValue("result", ResultElement);
                    if (Message != null) yield return new ElementValue("message", Message);
                    if (DetailElement != null) yield return new ElementValue("detail", DetailElement);
                }
            }

            
        }
        
        
        [FhirType("TestComponent")]
        [DataContract]
        public partial class TestComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "TestComponent"; } }
            
            /// <summary>
            /// Tracking/logging name of this test
            /// </summary>
            [FhirElement("name", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Tracking/logging name of this test
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
            /// Tracking/reporting short description of the test
            /// </summary>
            [FhirElement("description", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Tracking/reporting short description of the test
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Description
            {
                get { return DescriptionElement != null ? DescriptionElement.Value : null; }
                set
                {
                    if (value == null)
                        DescriptionElement = null; 
                    else
                        DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            /// <summary>
            /// A test operation or assert that was performed
            /// </summary>
            [FhirElement("action", Order=60)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestReport.TestActionComponent> Action
            {
                get { if(_Action==null) _Action = new List<Hl7.Fhir.Model.TestReport.TestActionComponent>(); return _Action; }
                set { _Action = value; OnPropertyChanged("Action"); }
            }
            
            private List<Hl7.Fhir.Model.TestReport.TestActionComponent> _Action;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Action != null) dest.Action = new List<Hl7.Fhir.Model.TestReport.TestActionComponent>(Action.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TestComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Action, otherT.Action)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    foreach (var elem in Action) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    foreach (var elem in Action) { if (elem != null) yield return new ElementValue("action", elem); }
                }
            }

            
        }
        
        
        [FhirType("TestActionComponent")]
        [DataContract]
        public partial class TestActionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "TestActionComponent"; } }
            
            /// <summary>
            /// The operation performed
            /// </summary>
            [FhirElement("operation", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.TestReport.OperationComponent Operation
            {
                get { return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            
            private Hl7.Fhir.Model.TestReport.OperationComponent _Operation;
            
            /// <summary>
            /// The assertion performed
            /// </summary>
            [FhirElement("assert", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.TestReport.AssertComponent Assert
            {
                get { return _Assert; }
                set { _Assert = value; OnPropertyChanged("Assert"); }
            }
            
            private Hl7.Fhir.Model.TestReport.AssertComponent _Assert;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestActionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Operation != null) dest.Operation = (Hl7.Fhir.Model.TestReport.OperationComponent)Operation.DeepCopy();
                    if(Assert != null) dest.Assert = (Hl7.Fhir.Model.TestReport.AssertComponent)Assert.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TestActionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TestActionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Operation, otherT.Operation)) return false;
                if( !DeepComparable.Matches(Assert, otherT.Assert)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TestActionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Operation, otherT.Operation)) return false;
                if( !DeepComparable.IsExactly(Assert, otherT.Assert)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Operation != null) yield return Operation;
                    if (Assert != null) yield return Assert;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Operation != null) yield return new ElementValue("operation", Operation);
                    if (Assert != null) yield return new ElementValue("assert", Assert);
                }
            }

            
        }
        
        
        [FhirType("TeardownComponent")]
        [DataContract]
        public partial class TeardownComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "TeardownComponent"; } }
            
            /// <summary>
            /// One or more teardown operations performed
            /// </summary>
            [FhirElement("action", Order=40)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestReport.TeardownActionComponent> Action
            {
                get { if(_Action==null) _Action = new List<Hl7.Fhir.Model.TestReport.TeardownActionComponent>(); return _Action; }
                set { _Action = value; OnPropertyChanged("Action"); }
            }
            
            private List<Hl7.Fhir.Model.TestReport.TeardownActionComponent> _Action;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TeardownComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Action != null) dest.Action = new List<Hl7.Fhir.Model.TestReport.TeardownActionComponent>(Action.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TeardownComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TeardownComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Action, otherT.Action)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TeardownComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Action) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Action) { if (elem != null) yield return new ElementValue("action", elem); }
                }
            }

            
        }
        
        
        [FhirType("TeardownActionComponent")]
        [DataContract]
        public partial class TeardownActionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "TeardownActionComponent"; } }
            
            /// <summary>
            /// The teardown operation performed
            /// </summary>
            [FhirElement("operation", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.TestReport.OperationComponent Operation
            {
                get { return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            
            private Hl7.Fhir.Model.TestReport.OperationComponent _Operation;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TeardownActionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Operation != null) dest.Operation = (Hl7.Fhir.Model.TestReport.OperationComponent)Operation.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TeardownActionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TeardownActionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Operation, otherT.Operation)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TeardownActionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Operation, otherT.Operation)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Operation != null) yield return Operation;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Operation != null) yield return new ElementValue("operation", Operation);
                }
            }

            
        }
        
        
        /// <summary>
        /// External identifier
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
        /// Informal name of the executed TestScript
        /// </summary>
        [FhirElement("name", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Informal name of the executed TestScript
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
        /// completed | in-progress | waiting | stopped | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=true, Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.TestReport.TestReportStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.TestReport.TestReportStatus> _StatusElement;
        
        /// <summary>
        /// completed | in-progress | waiting | stopped | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.TestReport.TestReportStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.TestReport.TestReportStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Reference to the  version-specific TestScript that was executed to produce this TestReport
        /// </summary>
        [FhirElement("testScript", InSummary=true, Order=120)]
        [CLSCompliant(false)]
		[References("TestScript")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference TestScript
        {
            get { return _TestScript; }
            set { _TestScript = value; OnPropertyChanged("TestScript"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _TestScript;
        
        /// <summary>
        /// pass | fail | pending
        /// </summary>
        [FhirElement("result", InSummary=true, Order=130)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.TestReport.TestReportResult> ResultElement
        {
            get { return _ResultElement; }
            set { _ResultElement = value; OnPropertyChanged("ResultElement"); }
        }
        
        private Code<Hl7.Fhir.Model.TestReport.TestReportResult> _ResultElement;
        
        /// <summary>
        /// pass | fail | pending
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.TestReport.TestReportResult? Result
        {
            get { return ResultElement != null ? ResultElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  ResultElement = null; 
                else
                  ResultElement = new Code<Hl7.Fhir.Model.TestReport.TestReportResult>(value);
                OnPropertyChanged("Result");
            }
        }
        
        /// <summary>
        /// The final score (percentage of tests passed) resulting from the execution of the TestScript
        /// </summary>
        [FhirElement("score", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDecimal ScoreElement
        {
            get { return _ScoreElement; }
            set { _ScoreElement = value; OnPropertyChanged("ScoreElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDecimal _ScoreElement;
        
        /// <summary>
        /// The final score (percentage of tests passed) resulting from the execution of the TestScript
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public decimal? Score
        {
            get { return ScoreElement != null ? ScoreElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  ScoreElement = null; 
                else
                  ScoreElement = new Hl7.Fhir.Model.FhirDecimal(value);
                OnPropertyChanged("Score");
            }
        }
        
        /// <summary>
        /// Name of the tester producing this report (Organization or individual)
        /// </summary>
        [FhirElement("tester", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TesterElement
        {
            get { return _TesterElement; }
            set { _TesterElement = value; OnPropertyChanged("TesterElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _TesterElement;
        
        /// <summary>
        /// Name of the tester producing this report (Organization or individual)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Tester
        {
            get { return TesterElement != null ? TesterElement.Value : null; }
            set
            {
                if (value == null)
                  TesterElement = null; 
                else
                  TesterElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Tester");
            }
        }
        
        /// <summary>
        /// When the TestScript was executed and this TestReport was generated
        /// </summary>
        [FhirElement("issued", InSummary=true, Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime IssuedElement
        {
            get { return _IssuedElement; }
            set { _IssuedElement = value; OnPropertyChanged("IssuedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _IssuedElement;
        
        /// <summary>
        /// When the TestScript was executed and this TestReport was generated
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Issued
        {
            get { return IssuedElement != null ? IssuedElement.Value : null; }
            set
            {
                if (value == null)
                  IssuedElement = null; 
                else
                  IssuedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Issued");
            }
        }
        
        /// <summary>
        /// A participant in the test execution, either the execution engine, a client, or a server
        /// </summary>
        [FhirElement("participant", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.TestReport.ParticipantComponent> Participant
        {
            get { if(_Participant==null) _Participant = new List<Hl7.Fhir.Model.TestReport.ParticipantComponent>(); return _Participant; }
            set { _Participant = value; OnPropertyChanged("Participant"); }
        }
        
        private List<Hl7.Fhir.Model.TestReport.ParticipantComponent> _Participant;
        
        /// <summary>
        /// The results of the series of required setup operations before the tests were executed
        /// </summary>
        [FhirElement("setup", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.TestReport.SetupComponent Setup
        {
            get { return _Setup; }
            set { _Setup = value; OnPropertyChanged("Setup"); }
        }
        
        private Hl7.Fhir.Model.TestReport.SetupComponent _Setup;
        
        /// <summary>
        /// A test executed from the test script
        /// </summary>
        [FhirElement("test", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.TestReport.TestComponent> Test
        {
            get { if(_Test==null) _Test = new List<Hl7.Fhir.Model.TestReport.TestComponent>(); return _Test; }
            set { _Test = value; OnPropertyChanged("Test"); }
        }
        
        private List<Hl7.Fhir.Model.TestReport.TestComponent> _Test;
        
        /// <summary>
        /// The results of running the series of required clean up steps
        /// </summary>
        [FhirElement("teardown", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.TestReport.TeardownComponent Teardown
        {
            get { return _Teardown; }
            set { _Teardown = value; OnPropertyChanged("Teardown"); }
        }
        
        private Hl7.Fhir.Model.TestReport.TeardownComponent _Teardown;
        

        public static ElementDefinition.ConstraintComponent TestReport_INV_1 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "setup.action.all(operation.exists() xor assert.exists())",
            Key = "inv-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Setup action SHALL contain either an operation or assert but not both.",
            Xpath = "(f:operation or f:assert) and not(f:operation and f:assert)"
        };

        public static ElementDefinition.ConstraintComponent TestReport_INV_2 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "test.action.all(operation.exists() xor assert.exists())",
            Key = "inv-2",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Test action SHALL contain either an operation or assert but not both.",
            Xpath = "(f:operation or f:assert) and not(f:operation and f:assert)"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(TestReport_INV_1);
            InvariantConstraints.Add(TestReport_INV_2);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as TestReport;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.TestReport.TestReportStatus>)StatusElement.DeepCopy();
                if(TestScript != null) dest.TestScript = (Hl7.Fhir.Model.ResourceReference)TestScript.DeepCopy();
                if(ResultElement != null) dest.ResultElement = (Code<Hl7.Fhir.Model.TestReport.TestReportResult>)ResultElement.DeepCopy();
                if(ScoreElement != null) dest.ScoreElement = (Hl7.Fhir.Model.FhirDecimal)ScoreElement.DeepCopy();
                if(TesterElement != null) dest.TesterElement = (Hl7.Fhir.Model.FhirString)TesterElement.DeepCopy();
                if(IssuedElement != null) dest.IssuedElement = (Hl7.Fhir.Model.FhirDateTime)IssuedElement.DeepCopy();
                if(Participant != null) dest.Participant = new List<Hl7.Fhir.Model.TestReport.ParticipantComponent>(Participant.DeepCopy());
                if(Setup != null) dest.Setup = (Hl7.Fhir.Model.TestReport.SetupComponent)Setup.DeepCopy();
                if(Test != null) dest.Test = new List<Hl7.Fhir.Model.TestReport.TestComponent>(Test.DeepCopy());
                if(Teardown != null) dest.Teardown = (Hl7.Fhir.Model.TestReport.TeardownComponent)Teardown.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new TestReport());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as TestReport;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(TestScript, otherT.TestScript)) return false;
            if( !DeepComparable.Matches(ResultElement, otherT.ResultElement)) return false;
            if( !DeepComparable.Matches(ScoreElement, otherT.ScoreElement)) return false;
            if( !DeepComparable.Matches(TesterElement, otherT.TesterElement)) return false;
            if( !DeepComparable.Matches(IssuedElement, otherT.IssuedElement)) return false;
            if( !DeepComparable.Matches(Participant, otherT.Participant)) return false;
            if( !DeepComparable.Matches(Setup, otherT.Setup)) return false;
            if( !DeepComparable.Matches(Test, otherT.Test)) return false;
            if( !DeepComparable.Matches(Teardown, otherT.Teardown)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as TestReport;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(TestScript, otherT.TestScript)) return false;
            if( !DeepComparable.IsExactly(ResultElement, otherT.ResultElement)) return false;
            if( !DeepComparable.IsExactly(ScoreElement, otherT.ScoreElement)) return false;
            if( !DeepComparable.IsExactly(TesterElement, otherT.TesterElement)) return false;
            if( !DeepComparable.IsExactly(IssuedElement, otherT.IssuedElement)) return false;
            if( !DeepComparable.IsExactly(Participant, otherT.Participant)) return false;
            if( !DeepComparable.IsExactly(Setup, otherT.Setup)) return false;
            if( !DeepComparable.IsExactly(Test, otherT.Test)) return false;
            if( !DeepComparable.IsExactly(Teardown, otherT.Teardown)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (Identifier != null) yield return Identifier;
				if (NameElement != null) yield return NameElement;
				if (StatusElement != null) yield return StatusElement;
				if (TestScript != null) yield return TestScript;
				if (ResultElement != null) yield return ResultElement;
				if (ScoreElement != null) yield return ScoreElement;
				if (TesterElement != null) yield return TesterElement;
				if (IssuedElement != null) yield return IssuedElement;
				foreach (var elem in Participant) { if (elem != null) yield return elem; }
				if (Setup != null) yield return Setup;
				foreach (var elem in Test) { if (elem != null) yield return elem; }
				if (Teardown != null) yield return Teardown;
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (TestScript != null) yield return new ElementValue("testScript", TestScript);
                if (ResultElement != null) yield return new ElementValue("result", ResultElement);
                if (ScoreElement != null) yield return new ElementValue("score", ScoreElement);
                if (TesterElement != null) yield return new ElementValue("tester", TesterElement);
                if (IssuedElement != null) yield return new ElementValue("issued", IssuedElement);
                foreach (var elem in Participant) { if (elem != null) yield return new ElementValue("participant", elem); }
                if (Setup != null) yield return new ElementValue("setup", Setup);
                foreach (var elem in Test) { if (elem != null) yield return new ElementValue("test", elem); }
                if (Teardown != null) yield return new ElementValue("teardown", Teardown);
            }
        }

    }
    
}
