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
// Generated for FHIR v1.0.2, v4.0.1, v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Information about the success/failure of an action
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.All, "OperationOutcome", IsResource=true)]
    [DataContract]
    public partial class OperationOutcome : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.OperationOutcome; } }
        [NotMapped]
        public override string TypeName { get { return "OperationOutcome"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.All, "IssueComponent")]
        [DataContract]
        public partial class IssueComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "IssueComponent"; } }
            
            /// <summary>
            /// fatal | error | warning | information
            /// </summary>
            [FhirElement("severity", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.IssueSeverity> SeverityElement
            {
                get { return _SeverityElement; }
                set { _SeverityElement = value; OnPropertyChanged("SeverityElement"); }
            }
            
            private Code<Hl7.Fhir.Model.IssueSeverity> _SeverityElement;
            
            /// <summary>
            /// fatal | error | warning | information
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.IssueSeverity? Severity
            {
                get { return SeverityElement != null ? SeverityElement.Value : null; }
                set
                {
                    if (value == null)
                        SeverityElement = null;
                    else
                        SeverityElement = new Code<Hl7.Fhir.Model.IssueSeverity>(value);
                    OnPropertyChanged("Severity");
                }
            }
            
            /// <summary>
            /// Error or warning code
            /// </summary>
            [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Hl7.Fhir.Model.Code _CodeElement;
            
            /// <summary>
            /// Error or warning code
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (value == null)
                        CodeElement = null;
                    else
                        CodeElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Additional details about the error
            /// </summary>
            [FhirElement("details", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Details
            {
                get { return _Details; }
                set { _Details = value; OnPropertyChanged("Details"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Details;
            
            /// <summary>
            /// Additional diagnostic information about the issue
            /// </summary>
            [FhirElement("diagnostics", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DiagnosticsElement
            {
                get { return _DiagnosticsElement; }
                set { _DiagnosticsElement = value; OnPropertyChanged("DiagnosticsElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DiagnosticsElement;
            
            /// <summary>
            /// Additional diagnostic information about the issue
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Diagnostics
            {
                get { return DiagnosticsElement != null ? DiagnosticsElement.Value : null; }
                set
                {
                    if (value == null)
                        DiagnosticsElement = null;
                    else
                        DiagnosticsElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Diagnostics");
                }
            }
            
            /// <summary>
            /// XPath of element(s) related to issue
            /// </summary>
            [FhirElement("location", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> LocationElement
            {
                get { if(_LocationElement==null) _LocationElement = new List<Hl7.Fhir.Model.FhirString>(); return _LocationElement; }
                set { _LocationElement = value; OnPropertyChanged("LocationElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _LocationElement;
            
            /// <summary>
            /// XPath of element(s) related to issue
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Location
            {
                get { return LocationElement != null ? LocationElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        LocationElement = null;
                    else
                        LocationElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("Location");
                }
            }
            
            /// <summary>
            /// FHIRPath of element(s) related to issue
            /// </summary>
            [FhirElement("expression", Versions=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, InSummary=Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Order=90)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> ExpressionElement
            {
                get { if(_ExpressionElement==null) _ExpressionElement = new List<Hl7.Fhir.Model.FhirString>(); return _ExpressionElement; }
                set { _ExpressionElement = value; OnPropertyChanged("ExpressionElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _ExpressionElement;
            
            /// <summary>
            /// FHIRPath of element(s) related to issue
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Expression
            {
                get { return ExpressionElement != null ? ExpressionElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        ExpressionElement = null;
                    else
                        ExpressionElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("Expression");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("IssueComponent");
                base.Serialize(sink);
                sink.Element("severity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); SeverityElement?.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); CodeElement?.Serialize(sink);
                sink.Element("details", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Details?.Serialize(sink);
                sink.Element("diagnostics", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DiagnosticsElement?.Serialize(sink);
                sink.BeginList("location", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                sink.Serialize(LocationElement);
                sink.End();
                sink.BeginList("expression", Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3, false);
                sink.Serialize(ExpressionElement);
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
                    case "severity":
                        SeverityElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.IssueSeverity>>();
                        return true;
                    case "code":
                        CodeElement = source.Get<Hl7.Fhir.Model.Code>();
                        return true;
                    case "details":
                        Details = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "diagnostics":
                        DiagnosticsElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "location":
                        LocationElement = source.GetList<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "expression" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        ExpressionElement = source.GetList<Hl7.Fhir.Model.FhirString>();
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
                    case "severity":
                        SeverityElement = source.PopulateValue(SeverityElement);
                        return true;
                    case "_severity":
                        SeverityElement = source.Populate(SeverityElement);
                        return true;
                    case "code":
                        CodeElement = source.PopulateValue(CodeElement);
                        return true;
                    case "_code":
                        CodeElement = source.Populate(CodeElement);
                        return true;
                    case "details":
                        Details = source.Populate(Details);
                        return true;
                    case "diagnostics":
                        DiagnosticsElement = source.PopulateValue(DiagnosticsElement);
                        return true;
                    case "_diagnostics":
                        DiagnosticsElement = source.Populate(DiagnosticsElement);
                        return true;
                    case "location":
                    case "_location":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "expression" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                    case "_expression" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
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
                    case "location":
                        source.PopulatePrimitiveListItemValue(LocationElement, index);
                        return true;
                    case "_location":
                        source.PopulatePrimitiveListItem(LocationElement, index);
                        return true;
                    case "expression" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        source.PopulatePrimitiveListItemValue(ExpressionElement, index);
                        return true;
                    case "_expression" when source.IsVersion(Hl7.Fhir.Model.Version.R4|Hl7.Fhir.Model.Version.STU3):
                        source.PopulatePrimitiveListItem(ExpressionElement, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as IssueComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SeverityElement != null) dest.SeverityElement = (Code<Hl7.Fhir.Model.IssueSeverity>)SeverityElement.DeepCopy();
                    if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
                    if(Details != null) dest.Details = (Hl7.Fhir.Model.CodeableConcept)Details.DeepCopy();
                    if(DiagnosticsElement != null) dest.DiagnosticsElement = (Hl7.Fhir.Model.FhirString)DiagnosticsElement.DeepCopy();
                    if(LocationElement != null) dest.LocationElement = new List<Hl7.Fhir.Model.FhirString>(LocationElement.DeepCopy());
                    if(ExpressionElement != null) dest.ExpressionElement = new List<Hl7.Fhir.Model.FhirString>(ExpressionElement.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new IssueComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as IssueComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SeverityElement, otherT.SeverityElement)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(Details, otherT.Details)) return false;
                if( !DeepComparable.Matches(DiagnosticsElement, otherT.DiagnosticsElement)) return false;
                if( !DeepComparable.Matches(LocationElement, otherT.LocationElement)) return false;
                if( !DeepComparable.Matches(ExpressionElement, otherT.ExpressionElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as IssueComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SeverityElement, otherT.SeverityElement)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(Details, otherT.Details)) return false;
                if( !DeepComparable.IsExactly(DiagnosticsElement, otherT.DiagnosticsElement)) return false;
                if( !DeepComparable.IsExactly(LocationElement, otherT.LocationElement)) return false;
                if( !DeepComparable.IsExactly(ExpressionElement, otherT.ExpressionElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SeverityElement != null) yield return SeverityElement;
                    if (CodeElement != null) yield return CodeElement;
                    if (Details != null) yield return Details;
                    if (DiagnosticsElement != null) yield return DiagnosticsElement;
                    foreach (var elem in LocationElement) { if (elem != null) yield return elem; }
                    foreach (var elem in ExpressionElement) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SeverityElement != null) yield return new ElementValue("severity", SeverityElement);
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (Details != null) yield return new ElementValue("details", Details);
                    if (DiagnosticsElement != null) yield return new ElementValue("diagnostics", DiagnosticsElement);
                    foreach (var elem in LocationElement) { if (elem != null) yield return new ElementValue("location", elem); }
                    foreach (var elem in ExpressionElement) { if (elem != null) yield return new ElementValue("expression", elem); }
                }
            }
        
        
        }
    
        
        /// <summary>
        /// A single issue associated with the action
        /// </summary>
        [FhirElement("issue", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<IssueComponent> Issue
        {
            get { if(_Issue==null) _Issue = new List<IssueComponent>(); return _Issue; }
            set { _Issue = value; OnPropertyChanged("Issue"); }
        }
        
        private List<IssueComponent> _Issue;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as OperationOutcome;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Issue != null) dest.Issue = new List<IssueComponent>(Issue.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new OperationOutcome());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as OperationOutcome;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Issue, otherT.Issue)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as OperationOutcome;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Issue, otherT.Issue)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("OperationOutcome");
            base.Serialize(sink);
            sink.BeginList("issue", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true);
            foreach(var item in Issue)
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
                case "issue":
                    Issue = source.GetList<IssueComponent>();
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
                case "issue":
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
                case "issue":
                    source.PopulateListItem(Issue, index);
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
                foreach (var elem in Issue) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Issue) { if (elem != null) yield return new ElementValue("issue", elem); }
            }
        }
    
    }

}
