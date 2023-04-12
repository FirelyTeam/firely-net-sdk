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
// Generated for FHIR v4.0.1, v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Describes the results of a TestScript execution
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.All, "TestReport", IsResource=true)]
    [DataContract]
    public partial class TestReport : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.TestReport; } }
        [NotMapped]
        public override string TypeName { get { return "TestReport"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.All, "ParticipantComponent")]
        [DataContract]
        public partial class ParticipantComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ParticipantComponent"; } }
            
            /// <summary>
            /// test-engine | client | server
            /// </summary>
            [FhirElement("type", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestReportParticipantType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.TestReportParticipantType> _TypeElement;
            
            /// <summary>
            /// test-engine | client | server
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.TestReportParticipantType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (value == null)
                        TypeElement = null;
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.TestReportParticipantType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// The uri of the participant. An absolute URL is preferred
            /// </summary>
            [FhirElement("uri", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=50)]
            [CLSCompliant(false)]
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
            [FhirElement("display", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=60)]
            [CLSCompliant(false)]
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ParticipantComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, true, false); TypeElement?.Serialize(sink);
                sink.Element("uri", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, true, false); UriElement?.Serialize(sink);
                sink.Element("display", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, false, false); DisplayElement?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "type" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        TypeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.TestReportParticipantType>>();
                        return true;
                    case "uri" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        UriElement = source.Get<Hl7.Fhir.Model.FhirUri>();
                        return true;
                    case "display" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        DisplayElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "type" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        TypeElement = source.PopulateValue(TypeElement);
                        return true;
                    case "_type" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        TypeElement = source.Populate(TypeElement);
                        return true;
                    case "uri" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        UriElement = source.PopulateValue(UriElement);
                        return true;
                    case "_uri" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        UriElement = source.Populate(UriElement);
                        return true;
                    case "display" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        DisplayElement = source.PopulateValue(DisplayElement);
                        return true;
                    case "_display" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        DisplayElement = source.Populate(DisplayElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ParticipantComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.TestReportParticipantType>)TypeElement.DeepCopy();
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.All, "SetupComponent")]
        [DataContract]
        public partial class SetupComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SetupComponent"; } }
            
            /// <summary>
            /// A setup operation or assert that was executed
            /// </summary>
            [FhirElement("action", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<SetupActionComponent> Action
            {
                get { if(_Action==null) _Action = new List<SetupActionComponent>(); return _Action; }
                set { _Action = value; OnPropertyChanged("Action"); }
            }
            
            private List<SetupActionComponent> _Action;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SetupComponent");
                base.Serialize(sink);
                sink.BeginList("action", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, true);
                foreach(var item in Action)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "action" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        Action = source.GetList<SetupActionComponent>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "action" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        source.SetList(this, jsonPropertyName);
                        return true;
                }
                return false;
            }
            
            internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
            {
                if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "action" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        source.PopulateListItem(Action, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SetupComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Action != null) dest.Action = new List<SetupActionComponent>(Action.DeepCopy());
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.All, "SetupActionComponent")]
        [DataContract]
        public partial class SetupActionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SetupActionComponent"; } }
            
            /// <summary>
            /// The operation to perform
            /// </summary>
            [FhirElement("operation", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public OperationComponent Operation
            {
                get { return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            
            private OperationComponent _Operation;
            
            /// <summary>
            /// The assertion to perform
            /// </summary>
            [FhirElement("assert", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public AssertComponent Assert
            {
                get { return _Assert; }
                set { _Assert = value; OnPropertyChanged("Assert"); }
            }
            
            private AssertComponent _Assert;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SetupActionComponent");
                base.Serialize(sink);
                sink.Element("operation", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, false, false); Operation?.Serialize(sink);
                sink.Element("assert", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, false, false); Assert?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "operation" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        Operation = source.Get<OperationComponent>();
                        return true;
                    case "assert" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        Assert = source.Get<AssertComponent>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "operation" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        Operation = source.Populate(Operation);
                        return true;
                    case "assert" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        Assert = source.Populate(Assert);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SetupActionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Operation != null) dest.Operation = (OperationComponent)Operation.DeepCopy();
                    if(Assert != null) dest.Assert = (AssertComponent)Assert.DeepCopy();
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.All, "OperationComponent")]
        [DataContract]
        public partial class OperationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "OperationComponent"; } }
            
            /// <summary>
            /// pass | skip | fail | warning | error
            /// </summary>
            [FhirElement("result", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestReportActionResult> ResultElement
            {
                get { return _ResultElement; }
                set { _ResultElement = value; OnPropertyChanged("ResultElement"); }
            }
            
            private Code<Hl7.Fhir.Model.TestReportActionResult> _ResultElement;
            
            /// <summary>
            /// pass | skip | fail | warning | error
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.TestReportActionResult? Result
            {
                get { return ResultElement != null ? ResultElement.Value : null; }
                set
                {
                    if (value == null)
                        ResultElement = null;
                    else
                        ResultElement = new Code<Hl7.Fhir.Model.TestReportActionResult>(value);
                    OnPropertyChanged("Result");
                }
            }
            
            /// <summary>
            /// A message associated with the result
            /// </summary>
            [FhirElement("message", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown MessageElement
            {
                get { return _MessageElement; }
                set { _MessageElement = value; OnPropertyChanged("MessageElement"); }
            }
            
            private Hl7.Fhir.Model.Markdown _MessageElement;
            
            /// <summary>
            /// A message associated with the result
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Message
            {
                get { return MessageElement != null ? MessageElement.Value : null; }
                set
                {
                    if (value == null)
                        MessageElement = null;
                    else
                        MessageElement = new Hl7.Fhir.Model.Markdown(value);
                    OnPropertyChanged("Message");
                }
            }
            
            /// <summary>
            /// A link to further details on the result
            /// </summary>
            [FhirElement("detail", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=60)]
            [CLSCompliant(false)]
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("OperationComponent");
                base.Serialize(sink);
                sink.Element("result", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, true, false); ResultElement?.Serialize(sink);
                sink.Element("message", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, false, false); MessageElement?.Serialize(sink);
                sink.Element("detail", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, false, false); DetailElement?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "result" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        ResultElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.TestReportActionResult>>();
                        return true;
                    case "message" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        MessageElement = source.Get<Hl7.Fhir.Model.Markdown>();
                        return true;
                    case "detail" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        DetailElement = source.Get<Hl7.Fhir.Model.FhirUri>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "result" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        ResultElement = source.PopulateValue(ResultElement);
                        return true;
                    case "_result" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        ResultElement = source.Populate(ResultElement);
                        return true;
                    case "message" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        MessageElement = source.PopulateValue(MessageElement);
                        return true;
                    case "_message" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        MessageElement = source.Populate(MessageElement);
                        return true;
                    case "detail" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        DetailElement = source.PopulateValue(DetailElement);
                        return true;
                    case "_detail" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        DetailElement = source.Populate(DetailElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as OperationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ResultElement != null) dest.ResultElement = (Code<Hl7.Fhir.Model.TestReportActionResult>)ResultElement.DeepCopy();
                    if(MessageElement != null) dest.MessageElement = (Hl7.Fhir.Model.Markdown)MessageElement.DeepCopy();
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
                if( !DeepComparable.Matches(MessageElement, otherT.MessageElement)) return false;
                if( !DeepComparable.Matches(DetailElement, otherT.DetailElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as OperationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ResultElement, otherT.ResultElement)) return false;
                if( !DeepComparable.IsExactly(MessageElement, otherT.MessageElement)) return false;
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
                    if (MessageElement != null) yield return MessageElement;
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
                    if (MessageElement != null) yield return new ElementValue("message", MessageElement);
                    if (DetailElement != null) yield return new ElementValue("detail", DetailElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.All, "AssertComponent")]
        [DataContract]
        public partial class AssertComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "AssertComponent"; } }
            
            /// <summary>
            /// pass | skip | fail | warning | error
            /// </summary>
            [FhirElement("result", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestReportActionResult> ResultElement
            {
                get { return _ResultElement; }
                set { _ResultElement = value; OnPropertyChanged("ResultElement"); }
            }
            
            private Code<Hl7.Fhir.Model.TestReportActionResult> _ResultElement;
            
            /// <summary>
            /// pass | skip | fail | warning | error
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.TestReportActionResult? Result
            {
                get { return ResultElement != null ? ResultElement.Value : null; }
                set
                {
                    if (value == null)
                        ResultElement = null;
                    else
                        ResultElement = new Code<Hl7.Fhir.Model.TestReportActionResult>(value);
                    OnPropertyChanged("Result");
                }
            }
            
            /// <summary>
            /// A message associated with the result
            /// </summary>
            [FhirElement("message", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Markdown MessageElement
            {
                get { return _MessageElement; }
                set { _MessageElement = value; OnPropertyChanged("MessageElement"); }
            }
            
            private Hl7.Fhir.Model.Markdown _MessageElement;
            
            /// <summary>
            /// A message associated with the result
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Message
            {
                get { return MessageElement != null ? MessageElement.Value : null; }
                set
                {
                    if (value == null)
                        MessageElement = null;
                    else
                        MessageElement = new Hl7.Fhir.Model.Markdown(value);
                    OnPropertyChanged("Message");
                }
            }
            
            /// <summary>
            /// A link to further details on the result
            /// </summary>
            [FhirElement("detail", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=60)]
            [CLSCompliant(false)]
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("AssertComponent");
                base.Serialize(sink);
                sink.Element("result", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, true, false); ResultElement?.Serialize(sink);
                sink.Element("message", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, false, false); MessageElement?.Serialize(sink);
                sink.Element("detail", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, false, false); DetailElement?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "result" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        ResultElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.TestReportActionResult>>();
                        return true;
                    case "message" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        MessageElement = source.Get<Hl7.Fhir.Model.Markdown>();
                        return true;
                    case "detail" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        DetailElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "result" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        ResultElement = source.PopulateValue(ResultElement);
                        return true;
                    case "_result" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        ResultElement = source.Populate(ResultElement);
                        return true;
                    case "message" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        MessageElement = source.PopulateValue(MessageElement);
                        return true;
                    case "_message" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        MessageElement = source.Populate(MessageElement);
                        return true;
                    case "detail" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        DetailElement = source.PopulateValue(DetailElement);
                        return true;
                    case "_detail" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        DetailElement = source.Populate(DetailElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AssertComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ResultElement != null) dest.ResultElement = (Code<Hl7.Fhir.Model.TestReportActionResult>)ResultElement.DeepCopy();
                    if(MessageElement != null) dest.MessageElement = (Hl7.Fhir.Model.Markdown)MessageElement.DeepCopy();
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
                if( !DeepComparable.Matches(MessageElement, otherT.MessageElement)) return false;
                if( !DeepComparable.Matches(DetailElement, otherT.DetailElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AssertComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ResultElement, otherT.ResultElement)) return false;
                if( !DeepComparable.IsExactly(MessageElement, otherT.MessageElement)) return false;
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
                    if (MessageElement != null) yield return MessageElement;
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
                    if (MessageElement != null) yield return new ElementValue("message", MessageElement);
                    if (DetailElement != null) yield return new ElementValue("detail", DetailElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.All, "TestComponent")]
        [DataContract]
        public partial class TestComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "TestComponent"; } }
            
            /// <summary>
            /// Tracking/logging name of this test
            /// </summary>
            [FhirElement("name", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=40)]
            [CLSCompliant(false)]
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
            [FhirElement("description", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=50)]
            [CLSCompliant(false)]
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
            [FhirElement("action", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=60)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<TestActionComponent> Action
            {
                get { if(_Action==null) _Action = new List<TestActionComponent>(); return _Action; }
                set { _Action = value; OnPropertyChanged("Action"); }
            }
            
            private List<TestActionComponent> _Action;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("TestComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, false, false); NameElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
                sink.BeginList("action", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, true);
                foreach(var item in Action)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "name" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        NameElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "description" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "action" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        Action = source.GetList<TestActionComponent>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "name" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        NameElement = source.PopulateValue(NameElement);
                        return true;
                    case "_name" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        NameElement = source.Populate(NameElement);
                        return true;
                    case "description" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        DescriptionElement = source.Populate(DescriptionElement);
                        return true;
                    case "action" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        source.SetList(this, jsonPropertyName);
                        return true;
                }
                return false;
            }
            
            internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
            {
                if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "action" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        source.PopulateListItem(Action, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Action != null) dest.Action = new List<TestActionComponent>(Action.DeepCopy());
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.All, "TestActionComponent")]
        [DataContract]
        public partial class TestActionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "TestActionComponent"; } }
            
            /// <summary>
            /// The operation performed
            /// </summary>
            [FhirElement("operation", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public OperationComponent Operation
            {
                get { return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            
            private OperationComponent _Operation;
            
            /// <summary>
            /// The assertion performed
            /// </summary>
            [FhirElement("assert", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public AssertComponent Assert
            {
                get { return _Assert; }
                set { _Assert = value; OnPropertyChanged("Assert"); }
            }
            
            private AssertComponent _Assert;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("TestActionComponent");
                base.Serialize(sink);
                sink.Element("operation", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, false, false); Operation?.Serialize(sink);
                sink.Element("assert", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, false, false); Assert?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "operation" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        Operation = source.Get<OperationComponent>();
                        return true;
                    case "assert" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        Assert = source.Get<AssertComponent>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "operation" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        Operation = source.Populate(Operation);
                        return true;
                    case "assert" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        Assert = source.Populate(Assert);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TestActionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Operation != null) dest.Operation = (OperationComponent)Operation.DeepCopy();
                    if(Assert != null) dest.Assert = (AssertComponent)Assert.DeepCopy();
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.All, "TeardownComponent")]
        [DataContract]
        public partial class TeardownComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "TeardownComponent"; } }
            
            /// <summary>
            /// One or more teardown operations performed
            /// </summary>
            [FhirElement("action", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<TeardownActionComponent> Action
            {
                get { if(_Action==null) _Action = new List<TeardownActionComponent>(); return _Action; }
                set { _Action = value; OnPropertyChanged("Action"); }
            }
            
            private List<TeardownActionComponent> _Action;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("TeardownComponent");
                base.Serialize(sink);
                sink.BeginList("action", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, true);
                foreach(var item in Action)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "action" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        Action = source.GetList<TeardownActionComponent>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "action" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        source.SetList(this, jsonPropertyName);
                        return true;
                }
                return false;
            }
            
            internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
            {
                if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "action" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        source.PopulateListItem(Action, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TeardownComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Action != null) dest.Action = new List<TeardownActionComponent>(Action.DeepCopy());
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.All, "TeardownActionComponent")]
        [DataContract]
        public partial class TeardownActionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "TeardownActionComponent"; } }
            
            /// <summary>
            /// The teardown operation performed
            /// </summary>
            [FhirElement("operation", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public OperationComponent Operation
            {
                get { return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            
            private OperationComponent _Operation;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("TeardownActionComponent");
                base.Serialize(sink);
                sink.Element("operation", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, true, false); Operation?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "operation" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        Operation = source.Get<OperationComponent>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "operation" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        Operation = source.Populate(Operation);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TeardownActionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Operation != null) dest.Operation = (OperationComponent)Operation.DeepCopy();
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
        [FhirElement("identifier", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, InSummary=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=90)]
        [CLSCompliant(false)]
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
        [FhirElement("name", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, InSummary=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=100)]
        [CLSCompliant(false)]
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
        [FhirElement("status", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, InSummary=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=110)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.TestReportStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.TestReportStatus> _StatusElement;
        
        /// <summary>
        /// completed | in-progress | waiting | stopped | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.TestReportStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.TestReportStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Reference to the  version-specific TestScript that was executed to produce this TestReport
        /// </summary>
        [FhirElement("testScript", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, InSummary=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=120)]
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
        [FhirElement("result", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, InSummary=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=130)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.TestReportResult> ResultElement
        {
            get { return _ResultElement; }
            set { _ResultElement = value; OnPropertyChanged("ResultElement"); }
        }
        
        private Code<Hl7.Fhir.Model.TestReportResult> _ResultElement;
        
        /// <summary>
        /// pass | fail | pending
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.TestReportResult? Result
        {
            get { return ResultElement != null ? ResultElement.Value : null; }
            set
            {
                if (value == null)
                    ResultElement = null;
                else
                    ResultElement = new Code<Hl7.Fhir.Model.TestReportResult>(value);
                OnPropertyChanged("Result");
            }
        }
        
        /// <summary>
        /// The final score (percentage of tests passed) resulting from the execution of the TestScript
        /// </summary>
        [FhirElement("score", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, InSummary=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=140)]
        [CLSCompliant(false)]
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
                if (value == null)
                    ScoreElement = null;
                else
                    ScoreElement = new Hl7.Fhir.Model.FhirDecimal(value);
                OnPropertyChanged("Score");
            }
        }
        
        /// <summary>
        /// Name of the tester producing this report (Organization or individual)
        /// </summary>
        [FhirElement("tester", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, InSummary=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=150)]
        [CLSCompliant(false)]
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
        [FhirElement("issued", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, InSummary=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=160)]
        [CLSCompliant(false)]
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
        [FhirElement("participant", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=170)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ParticipantComponent> Participant
        {
            get { if(_Participant==null) _Participant = new List<ParticipantComponent>(); return _Participant; }
            set { _Participant = value; OnPropertyChanged("Participant"); }
        }
        
        private List<ParticipantComponent> _Participant;
        
        /// <summary>
        /// The results of the series of required setup operations before the tests were executed
        /// </summary>
        [FhirElement("setup", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=180)]
        [CLSCompliant(false)]
        [DataMember]
        public SetupComponent Setup
        {
            get { return _Setup; }
            set { _Setup = value; OnPropertyChanged("Setup"); }
        }
        
        private SetupComponent _Setup;
        
        /// <summary>
        /// A test executed from the test script
        /// </summary>
        [FhirElement("test", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=190)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<TestComponent> Test
        {
            get { if(_Test==null) _Test = new List<TestComponent>(); return _Test; }
            set { _Test = value; OnPropertyChanged("Test"); }
        }
        
        private List<TestComponent> _Test;
        
        /// <summary>
        /// The results of running the series of required clean up steps
        /// </summary>
        [FhirElement("teardown", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=200)]
        [CLSCompliant(false)]
        [DataMember]
        public TeardownComponent Teardown
        {
            get { return _Teardown; }
            set { _Teardown = value; OnPropertyChanged("Teardown"); }
        }
        
        private TeardownComponent _Teardown;
    
    
        public static ElementDefinitionConstraint[] TestReport_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4,Hl7.Fhir.Model.Version.STU3},
                key: "inv-1",
                severity: ConstraintSeverity.Warning,
                expression: "setup.action.all(operation.exists() xor assert.exists())",
                human: "Setup action SHALL contain either an operation or assert but not both.",
                xpath: "(f:operation or f:assert) and not(f:operation and f:assert)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4,Hl7.Fhir.Model.Version.STU3},
                key: "inv-2",
                severity: ConstraintSeverity.Warning,
                expression: "test.action.all(operation.exists() xor assert.exists())",
                human: "Test action SHALL contain either an operation or assert but not both.",
                xpath: "(f:operation or f:assert) and not(f:operation and f:assert)"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(TestReport_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as TestReport;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.TestReportStatus>)StatusElement.DeepCopy();
                if(TestScript != null) dest.TestScript = (Hl7.Fhir.Model.ResourceReference)TestScript.DeepCopy();
                if(ResultElement != null) dest.ResultElement = (Code<Hl7.Fhir.Model.TestReportResult>)ResultElement.DeepCopy();
                if(ScoreElement != null) dest.ScoreElement = (Hl7.Fhir.Model.FhirDecimal)ScoreElement.DeepCopy();
                if(TesterElement != null) dest.TesterElement = (Hl7.Fhir.Model.FhirString)TesterElement.DeepCopy();
                if(IssuedElement != null) dest.IssuedElement = (Hl7.Fhir.Model.FhirDateTime)IssuedElement.DeepCopy();
                if(Participant != null) dest.Participant = new List<ParticipantComponent>(Participant.DeepCopy());
                if(Setup != null) dest.Setup = (SetupComponent)Setup.DeepCopy();
                if(Test != null) dest.Test = new List<TestComponent>(Test.DeepCopy());
                if(Teardown != null) dest.Teardown = (TeardownComponent)Teardown.DeepCopy();
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
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("TestReport");
            base.Serialize(sink);
            sink.Element("identifier", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, false, false); Identifier?.Serialize(sink);
            sink.Element("name", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, false, false); NameElement?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, true, false); StatusElement?.Serialize(sink);
            sink.Element("testScript", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, true, false); TestScript?.Serialize(sink);
            sink.Element("result", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, true, false); ResultElement?.Serialize(sink);
            sink.Element("score", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, false, false); ScoreElement?.Serialize(sink);
            sink.Element("tester", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, false, false); TesterElement?.Serialize(sink);
            sink.Element("issued", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, false, false); IssuedElement?.Serialize(sink);
            sink.BeginList("participant", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Participant)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("setup", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, false, false); Setup?.Serialize(sink);
            sink.BeginList("test", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Test)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("teardown", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.None, false, false); Teardown?.Serialize(sink);
            sink.End();
        }
    
        internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
        {
            if (base.SetElementFromSource(elementName, source))
            {
                return true;
            }
            switch (elementName)
            {
                case "identifier" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    Identifier = source.Get<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "name" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    NameElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "status" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.TestReportStatus>>();
                    return true;
                case "testScript" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    TestScript = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "result" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    ResultElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.TestReportResult>>();
                    return true;
                case "score" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    ScoreElement = source.Get<Hl7.Fhir.Model.FhirDecimal>();
                    return true;
                case "tester" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    TesterElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "issued" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    IssuedElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "participant" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    Participant = source.GetList<ParticipantComponent>();
                    return true;
                case "setup" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    Setup = source.Get<SetupComponent>();
                    return true;
                case "test" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    Test = source.GetList<TestComponent>();
                    return true;
                case "teardown" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    Teardown = source.Get<TeardownComponent>();
                    return true;
            }
            return false;
        }
    
        internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
        {
            if (base.SetElementFromJson(jsonPropertyName, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "identifier" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    Identifier = source.Populate(Identifier);
                    return true;
                case "name" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    NameElement = source.PopulateValue(NameElement);
                    return true;
                case "_name" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    NameElement = source.Populate(NameElement);
                    return true;
                case "status" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "testScript" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    TestScript = source.Populate(TestScript);
                    return true;
                case "result" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    ResultElement = source.PopulateValue(ResultElement);
                    return true;
                case "_result" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    ResultElement = source.Populate(ResultElement);
                    return true;
                case "score" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    ScoreElement = source.PopulateValue(ScoreElement);
                    return true;
                case "_score" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    ScoreElement = source.Populate(ScoreElement);
                    return true;
                case "tester" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    TesterElement = source.PopulateValue(TesterElement);
                    return true;
                case "_tester" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    TesterElement = source.Populate(TesterElement);
                    return true;
                case "issued" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    IssuedElement = source.PopulateValue(IssuedElement);
                    return true;
                case "_issued" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    IssuedElement = source.Populate(IssuedElement);
                    return true;
                case "participant" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "setup" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    Setup = source.Populate(Setup);
                    return true;
                case "test" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "teardown" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    Teardown = source.Populate(Teardown);
                    return true;
            }
            return false;
        }
        
        internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
        {
            if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "participant" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    source.PopulateListItem(Participant, index);
                    return true;
                case "test" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    source.PopulateListItem(Test, index);
                    return true;
            }
            return false;
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
