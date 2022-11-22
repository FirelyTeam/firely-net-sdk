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
// Generated for FHIR v1.0.2
//
namespace Hl7.Fhir.Model.DSTU2
{
    /// <summary>
    /// A set of codes drawn from one or more code systems
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ValueSet", IsResource=true)]
    [DataContract]
    public partial class ValueSet : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IValueSet, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ValueSet; } }
        [NotMapped]
        public override string TypeName { get { return "ValueSet"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ContactComponent")]
        [DataContract]
        public partial class ContactComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ContactComponent"; } }
            
            /// <summary>
            /// Name of an individual to contact
            /// </summary>
            [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
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
            [FhirElement("telecom", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.DSTU2.ContactPoint> Telecom
            {
                get { if(_Telecom==null) _Telecom = new List<Hl7.Fhir.Model.DSTU2.ContactPoint>(); return _Telecom; }
                set { _Telecom = value; OnPropertyChanged("Telecom"); }
            }
            
            private List<Hl7.Fhir.Model.DSTU2.ContactPoint> _Telecom;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ContactComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NameElement?.Serialize(sink);
                sink.BeginList("telecom", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Telecom)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "name":
                        NameElement = source.PopulateValue(NameElement);
                        return true;
                    case "_name":
                        NameElement = source.Populate(NameElement);
                        return true;
                    case "telecom":
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
                    case "telecom":
                        source.PopulateListItem(Telecom, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ContactComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.DSTU2.ContactPoint>(Telecom.DeepCopy());
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
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    foreach (var elem in Telecom) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    foreach (var elem in Telecom) { if (elem != null) yield return new ElementValue("telecom", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "CodeSystemComponent")]
        [DataContract]
        public partial class CodeSystemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "CodeSystemComponent"; } }
            
            /// <summary>
            /// URI to identify the code system (e.g. in Coding.system)
            /// </summary>
            [FhirElement("system", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri SystemElement
            {
                get { return _SystemElement; }
                set { _SystemElement = value; OnPropertyChanged("SystemElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _SystemElement;
            
            /// <summary>
            /// URI to identify the code system (e.g. in Coding.system)
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
            /// Version (for use in Coding.version)
            /// </summary>
            [FhirElement("version", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VersionElement
            {
                get { return _VersionElement; }
                set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _VersionElement;
            
            /// <summary>
            /// Version (for use in Coding.version)
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
            /// If code comparison is case sensitive
            /// </summary>
            [FhirElement("caseSensitive", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean CaseSensitiveElement
            {
                get { return _CaseSensitiveElement; }
                set { _CaseSensitiveElement = value; OnPropertyChanged("CaseSensitiveElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _CaseSensitiveElement;
            
            /// <summary>
            /// If code comparison is case sensitive
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? CaseSensitive
            {
                get { return CaseSensitiveElement != null ? CaseSensitiveElement.Value : null; }
                set
                {
                    if (value == null)
                        CaseSensitiveElement = null;
                    else
                        CaseSensitiveElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("CaseSensitive");
                }
            }
            
            /// <summary>
            /// Concepts in the code system
            /// </summary>
            [FhirElement("concept", Order=70)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<ConceptDefinitionComponent> Concept
            {
                get { if(_Concept==null) _Concept = new List<ConceptDefinitionComponent>(); return _Concept; }
                set { _Concept = value; OnPropertyChanged("Concept"); }
            }
            
            private List<ConceptDefinitionComponent> _Concept;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("CodeSystemComponent");
                base.Serialize(sink);
                sink.Element("system", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); SystemElement?.Serialize(sink);
                sink.Element("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VersionElement?.Serialize(sink);
                sink.Element("caseSensitive", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CaseSensitiveElement?.Serialize(sink);
                sink.BeginList("concept", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true);
                foreach(var item in Concept)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "system":
                        SystemElement = source.PopulateValue(SystemElement);
                        return true;
                    case "_system":
                        SystemElement = source.Populate(SystemElement);
                        return true;
                    case "version":
                        VersionElement = source.PopulateValue(VersionElement);
                        return true;
                    case "_version":
                        VersionElement = source.Populate(VersionElement);
                        return true;
                    case "caseSensitive":
                        CaseSensitiveElement = source.PopulateValue(CaseSensitiveElement);
                        return true;
                    case "_caseSensitive":
                        CaseSensitiveElement = source.Populate(CaseSensitiveElement);
                        return true;
                    case "concept":
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
                    case "concept":
                        source.PopulateListItem(Concept, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CodeSystemComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SystemElement != null) dest.SystemElement = (Hl7.Fhir.Model.FhirUri)SystemElement.DeepCopy();
                    if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                    if(CaseSensitiveElement != null) dest.CaseSensitiveElement = (Hl7.Fhir.Model.FhirBoolean)CaseSensitiveElement.DeepCopy();
                    if(Concept != null) dest.Concept = new List<ConceptDefinitionComponent>(Concept.DeepCopy());
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
                if( !DeepComparable.Matches(SystemElement, otherT.SystemElement)) return false;
                if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
                if( !DeepComparable.Matches(CaseSensitiveElement, otherT.CaseSensitiveElement)) return false;
                if( !DeepComparable.Matches(Concept, otherT.Concept)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CodeSystemComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SystemElement, otherT.SystemElement)) return false;
                if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
                if( !DeepComparable.IsExactly(CaseSensitiveElement, otherT.CaseSensitiveElement)) return false;
                if( !DeepComparable.IsExactly(Concept, otherT.Concept)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SystemElement != null) yield return SystemElement;
                    if (VersionElement != null) yield return VersionElement;
                    if (CaseSensitiveElement != null) yield return CaseSensitiveElement;
                    foreach (var elem in Concept) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SystemElement != null) yield return new ElementValue("system", SystemElement);
                    if (VersionElement != null) yield return new ElementValue("version", VersionElement);
                    if (CaseSensitiveElement != null) yield return new ElementValue("caseSensitive", CaseSensitiveElement);
                    foreach (var elem in Concept) { if (elem != null) yield return new ElementValue("concept", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ConceptDefinitionComponent")]
        [DataContract]
        public partial class ConceptDefinitionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ConceptDefinitionComponent"; } }
            
            /// <summary>
            /// Code that identifies concept
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Hl7.Fhir.Model.Code _CodeElement;
            
            /// <summary>
            /// Code that identifies concept
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
            /// If this code is not for use as a real concept
            /// </summary>
            [FhirElement("abstract", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean AbstractElement
            {
                get { return _AbstractElement; }
                set { _AbstractElement = value; OnPropertyChanged("AbstractElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _AbstractElement;
            
            /// <summary>
            /// If this code is not for use as a real concept
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Abstract
            {
                get { return AbstractElement != null ? AbstractElement.Value : null; }
                set
                {
                    if (value == null)
                        AbstractElement = null;
                    else
                        AbstractElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Abstract");
                }
            }
            
            /// <summary>
            /// Text to display to the user
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
            /// Text to display to the user
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
            
            /// <summary>
            /// Formal definition
            /// </summary>
            [FhirElement("definition", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DefinitionElement
            {
                get { return _DefinitionElement; }
                set { _DefinitionElement = value; OnPropertyChanged("DefinitionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DefinitionElement;
            
            /// <summary>
            /// Formal definition
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Definition
            {
                get { return DefinitionElement != null ? DefinitionElement.Value : null; }
                set
                {
                    if (value == null)
                        DefinitionElement = null;
                    else
                        DefinitionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Definition");
                }
            }
            
            /// <summary>
            /// Additional representations for the concept
            /// </summary>
            [FhirElement("designation", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<DesignationComponent> Designation
            {
                get { if(_Designation==null) _Designation = new List<DesignationComponent>(); return _Designation; }
                set { _Designation = value; OnPropertyChanged("Designation"); }
            }
            
            private List<DesignationComponent> _Designation;
            
            /// <summary>
            /// Child Concepts (is-a/contains/categorizes)
            /// </summary>
            [FhirElement("concept", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ConceptDefinitionComponent> Concept
            {
                get { if(_Concept==null) _Concept = new List<ConceptDefinitionComponent>(); return _Concept; }
                set { _Concept = value; OnPropertyChanged("Concept"); }
            }
            
            private List<ConceptDefinitionComponent> _Concept;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ConceptDefinitionComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); CodeElement?.Serialize(sink);
                sink.Element("abstract", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); AbstractElement?.Serialize(sink);
                sink.Element("display", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DisplayElement?.Serialize(sink);
                sink.Element("definition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DefinitionElement?.Serialize(sink);
                sink.BeginList("designation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Designation)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("concept", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Concept)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "code":
                        CodeElement = source.PopulateValue(CodeElement);
                        return true;
                    case "_code":
                        CodeElement = source.Populate(CodeElement);
                        return true;
                    case "abstract":
                        AbstractElement = source.PopulateValue(AbstractElement);
                        return true;
                    case "_abstract":
                        AbstractElement = source.Populate(AbstractElement);
                        return true;
                    case "display":
                        DisplayElement = source.PopulateValue(DisplayElement);
                        return true;
                    case "_display":
                        DisplayElement = source.Populate(DisplayElement);
                        return true;
                    case "definition":
                        DefinitionElement = source.PopulateValue(DefinitionElement);
                        return true;
                    case "_definition":
                        DefinitionElement = source.Populate(DefinitionElement);
                        return true;
                    case "designation":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "concept":
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
                    case "designation":
                        source.PopulateListItem(Designation, index);
                        return true;
                    case "concept":
                        source.PopulateListItem(Concept, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConceptDefinitionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
                    if(AbstractElement != null) dest.AbstractElement = (Hl7.Fhir.Model.FhirBoolean)AbstractElement.DeepCopy();
                    if(DisplayElement != null) dest.DisplayElement = (Hl7.Fhir.Model.FhirString)DisplayElement.DeepCopy();
                    if(DefinitionElement != null) dest.DefinitionElement = (Hl7.Fhir.Model.FhirString)DefinitionElement.DeepCopy();
                    if(Designation != null) dest.Designation = new List<DesignationComponent>(Designation.DeepCopy());
                    if(Concept != null) dest.Concept = new List<ConceptDefinitionComponent>(Concept.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ConceptDefinitionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConceptDefinitionComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(AbstractElement, otherT.AbstractElement)) return false;
                if( !DeepComparable.Matches(DisplayElement, otherT.DisplayElement)) return false;
                if( !DeepComparable.Matches(DefinitionElement, otherT.DefinitionElement)) return false;
                if( !DeepComparable.Matches(Designation, otherT.Designation)) return false;
                if( !DeepComparable.Matches(Concept, otherT.Concept)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConceptDefinitionComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(AbstractElement, otherT.AbstractElement)) return false;
                if( !DeepComparable.IsExactly(DisplayElement, otherT.DisplayElement)) return false;
                if( !DeepComparable.IsExactly(DefinitionElement, otherT.DefinitionElement)) return false;
                if( !DeepComparable.IsExactly(Designation, otherT.Designation)) return false;
                if( !DeepComparable.IsExactly(Concept, otherT.Concept)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    if (AbstractElement != null) yield return AbstractElement;
                    if (DisplayElement != null) yield return DisplayElement;
                    if (DefinitionElement != null) yield return DefinitionElement;
                    foreach (var elem in Designation) { if (elem != null) yield return elem; }
                    foreach (var elem in Concept) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (AbstractElement != null) yield return new ElementValue("abstract", AbstractElement);
                    if (DisplayElement != null) yield return new ElementValue("display", DisplayElement);
                    if (DefinitionElement != null) yield return new ElementValue("definition", DefinitionElement);
                    foreach (var elem in Designation) { if (elem != null) yield return new ElementValue("designation", elem); }
                    foreach (var elem in Concept) { if (elem != null) yield return new ElementValue("concept", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "DesignationComponent")]
        [DataContract]
        public partial class DesignationComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IValueSetDesignationComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DesignationComponent"; } }
            
            /// <summary>
            /// Human language of the designation
            /// </summary>
            [FhirElement("language", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Code LanguageElement
            {
                get { return _LanguageElement; }
                set { _LanguageElement = value; OnPropertyChanged("LanguageElement"); }
            }
            
            private Hl7.Fhir.Model.Code _LanguageElement;
            
            /// <summary>
            /// Human language of the designation
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
            /// Details how this designation would be used
            /// </summary>
            [FhirElement("use", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Use
            {
                get { return _Use; }
                set { _Use = value; OnPropertyChanged("Use"); }
            }
            
            private Hl7.Fhir.Model.Coding _Use;
            
            /// <summary>
            /// The text value for this designation
            /// </summary>
            [FhirElement("value", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ValueElement;
            
            /// <summary>
            /// The text value for this designation
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Value
            {
                get { return ValueElement != null ? ValueElement.Value : null; }
                set
                {
                    if (value == null)
                        ValueElement = null;
                    else
                        ValueElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Value");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("DesignationComponent");
                base.Serialize(sink);
                sink.Element("language", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); LanguageElement?.Serialize(sink);
                sink.Element("use", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Use?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); ValueElement?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "language":
                        LanguageElement = source.PopulateValue(LanguageElement);
                        return true;
                    case "_language":
                        LanguageElement = source.Populate(LanguageElement);
                        return true;
                    case "use":
                        Use = source.Populate(Use);
                        return true;
                    case "value":
                        ValueElement = source.PopulateValue(ValueElement);
                        return true;
                    case "_value":
                        ValueElement = source.Populate(ValueElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DesignationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(LanguageElement != null) dest.LanguageElement = (Hl7.Fhir.Model.Code)LanguageElement.DeepCopy();
                    if(Use != null) dest.Use = (Hl7.Fhir.Model.Coding)Use.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
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
                if( !DeepComparable.Matches(LanguageElement, otherT.LanguageElement)) return false;
                if( !DeepComparable.Matches(Use, otherT.Use)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DesignationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(LanguageElement, otherT.LanguageElement)) return false;
                if( !DeepComparable.IsExactly(Use, otherT.Use)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (LanguageElement != null) yield return LanguageElement;
                    if (Use != null) yield return Use;
                    if (ValueElement != null) yield return ValueElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (LanguageElement != null) yield return new ElementValue("language", LanguageElement);
                    if (Use != null) yield return new ElementValue("use", Use);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ComposeComponent")]
        [DataContract]
        public partial class ComposeComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IValueSetComposeComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ComposeComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IValueSetConceptSetComponent> Hl7.Fhir.Model.IValueSetComposeComponent.Include { get { return Include; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IValueSetConceptSetComponent> Hl7.Fhir.Model.IValueSetComposeComponent.Exclude { get { return Exclude; } }
            
            /// <summary>
            /// Import the contents of another value set
            /// </summary>
            [FhirElement("import", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirUri> ImportElement
            {
                get { if(_ImportElement==null) _ImportElement = new List<Hl7.Fhir.Model.FhirUri>(); return _ImportElement; }
                set { _ImportElement = value; OnPropertyChanged("ImportElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirUri> _ImportElement;
            
            /// <summary>
            /// Import the contents of another value set
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Import
            {
                get { return ImportElement != null ? ImportElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        ImportElement = null;
                    else
                        ImportElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                    OnPropertyChanged("Import");
                }
            }
            
            /// <summary>
            /// Include one or more codes from a code system
            /// </summary>
            [FhirElement("include", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ConceptSetComponent> Include
            {
                get { if(_Include==null) _Include = new List<ConceptSetComponent>(); return _Include; }
                set { _Include = value; OnPropertyChanged("Include"); }
            }
            
            private List<ConceptSetComponent> _Include;
            
            /// <summary>
            /// Explicitly exclude codes
            /// </summary>
            [FhirElement("exclude", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ConceptSetComponent> Exclude
            {
                get { if(_Exclude==null) _Exclude = new List<ConceptSetComponent>(); return _Exclude; }
                set { _Exclude = value; OnPropertyChanged("Exclude"); }
            }
            
            private List<ConceptSetComponent> _Exclude;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ComposeComponent");
                base.Serialize(sink);
                sink.BeginList("import", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                sink.Serialize(ImportElement);
                sink.End();
                sink.BeginList("include", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Include)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("exclude", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Exclude)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "import":
                    case "_import":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "include":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "exclude":
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
                    case "import":
                        source.PopulatePrimitiveListItemValue(ImportElement, index);
                        return true;
                    case "_import":
                        source.PopulatePrimitiveListItem(ImportElement, index);
                        return true;
                    case "include":
                        source.PopulateListItem(Include, index);
                        return true;
                    case "exclude":
                        source.PopulateListItem(Exclude, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ComposeComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ImportElement != null) dest.ImportElement = new List<Hl7.Fhir.Model.FhirUri>(ImportElement.DeepCopy());
                    if(Include != null) dest.Include = new List<ConceptSetComponent>(Include.DeepCopy());
                    if(Exclude != null) dest.Exclude = new List<ConceptSetComponent>(Exclude.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ComposeComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ComposeComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ImportElement, otherT.ImportElement)) return false;
                if( !DeepComparable.Matches(Include, otherT.Include)) return false;
                if( !DeepComparable.Matches(Exclude, otherT.Exclude)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ComposeComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ImportElement, otherT.ImportElement)) return false;
                if( !DeepComparable.IsExactly(Include, otherT.Include)) return false;
                if( !DeepComparable.IsExactly(Exclude, otherT.Exclude)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in ImportElement) { if (elem != null) yield return elem; }
                    foreach (var elem in Include) { if (elem != null) yield return elem; }
                    foreach (var elem in Exclude) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in ImportElement) { if (elem != null) yield return new ElementValue("import", elem); }
                    foreach (var elem in Include) { if (elem != null) yield return new ElementValue("include", elem); }
                    foreach (var elem in Exclude) { if (elem != null) yield return new ElementValue("exclude", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ConceptSetComponent")]
        [DataContract]
        public partial class ConceptSetComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IValueSetConceptSetComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ConceptSetComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IValueSetConceptReferenceComponent> Hl7.Fhir.Model.IValueSetConceptSetComponent.Concept { get { return Concept; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IValueSetFilterComponent> Hl7.Fhir.Model.IValueSetConceptSetComponent.Filter { get { return Filter; } }
            
            /// <summary>
            /// The system the codes come from
            /// </summary>
            [FhirElement("system", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri SystemElement
            {
                get { return _SystemElement; }
                set { _SystemElement = value; OnPropertyChanged("SystemElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _SystemElement;
            
            /// <summary>
            /// The system the codes come from
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
            [FhirElement("version", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
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
            
            /// <summary>
            /// A concept defined in the system
            /// </summary>
            [FhirElement("concept", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ConceptReferenceComponent> Concept
            {
                get { if(_Concept==null) _Concept = new List<ConceptReferenceComponent>(); return _Concept; }
                set { _Concept = value; OnPropertyChanged("Concept"); }
            }
            
            private List<ConceptReferenceComponent> _Concept;
            
            /// <summary>
            /// Select codes/concepts by their properties (including relationships)
            /// </summary>
            [FhirElement("filter", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<FilterComponent> Filter
            {
                get { if(_Filter==null) _Filter = new List<FilterComponent>(); return _Filter; }
                set { _Filter = value; OnPropertyChanged("Filter"); }
            }
            
            private List<FilterComponent> _Filter;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ConceptSetComponent");
                base.Serialize(sink);
                sink.Element("system", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); SystemElement?.Serialize(sink);
                sink.Element("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VersionElement?.Serialize(sink);
                sink.BeginList("concept", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Concept)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("filter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Filter)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "system":
                        SystemElement = source.PopulateValue(SystemElement);
                        return true;
                    case "_system":
                        SystemElement = source.Populate(SystemElement);
                        return true;
                    case "version":
                        VersionElement = source.PopulateValue(VersionElement);
                        return true;
                    case "_version":
                        VersionElement = source.Populate(VersionElement);
                        return true;
                    case "concept":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "filter":
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
                    case "concept":
                        source.PopulateListItem(Concept, index);
                        return true;
                    case "filter":
                        source.PopulateListItem(Filter, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConceptSetComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SystemElement != null) dest.SystemElement = (Hl7.Fhir.Model.FhirUri)SystemElement.DeepCopy();
                    if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                    if(Concept != null) dest.Concept = new List<ConceptReferenceComponent>(Concept.DeepCopy());
                    if(Filter != null) dest.Filter = new List<FilterComponent>(Filter.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ConceptSetComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConceptSetComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SystemElement, otherT.SystemElement)) return false;
                if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
                if( !DeepComparable.Matches(Concept, otherT.Concept)) return false;
                if( !DeepComparable.Matches(Filter, otherT.Filter)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConceptSetComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SystemElement, otherT.SystemElement)) return false;
                if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
                if( !DeepComparable.IsExactly(Concept, otherT.Concept)) return false;
                if( !DeepComparable.IsExactly(Filter, otherT.Filter)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SystemElement != null) yield return SystemElement;
                    if (VersionElement != null) yield return VersionElement;
                    foreach (var elem in Concept) { if (elem != null) yield return elem; }
                    foreach (var elem in Filter) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SystemElement != null) yield return new ElementValue("system", SystemElement);
                    if (VersionElement != null) yield return new ElementValue("version", VersionElement);
                    foreach (var elem in Concept) { if (elem != null) yield return new ElementValue("concept", elem); }
                    foreach (var elem in Filter) { if (elem != null) yield return new ElementValue("filter", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ConceptReferenceComponent")]
        [DataContract]
        public partial class ConceptReferenceComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IValueSetConceptReferenceComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ConceptReferenceComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IValueSetDesignationComponent> Hl7.Fhir.Model.IValueSetConceptReferenceComponent.Designation { get { return Designation; } }
            
            /// <summary>
            /// Code or expression from system
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Hl7.Fhir.Model.Code _CodeElement;
            
            /// <summary>
            /// Code or expression from system
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
            /// Test to display for this code for this value set
            /// </summary>
            [FhirElement("display", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DisplayElement
            {
                get { return _DisplayElement; }
                set { _DisplayElement = value; OnPropertyChanged("DisplayElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DisplayElement;
            
            /// <summary>
            /// Test to display for this code for this value set
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
            
            /// <summary>
            /// Additional representations for this valueset
            /// </summary>
            [FhirElement("designation", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<DesignationComponent> Designation
            {
                get { if(_Designation==null) _Designation = new List<DesignationComponent>(); return _Designation; }
                set { _Designation = value; OnPropertyChanged("Designation"); }
            }
            
            private List<DesignationComponent> _Designation;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ConceptReferenceComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); CodeElement?.Serialize(sink);
                sink.Element("display", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DisplayElement?.Serialize(sink);
                sink.BeginList("designation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Designation)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "code":
                        CodeElement = source.PopulateValue(CodeElement);
                        return true;
                    case "_code":
                        CodeElement = source.Populate(CodeElement);
                        return true;
                    case "display":
                        DisplayElement = source.PopulateValue(DisplayElement);
                        return true;
                    case "_display":
                        DisplayElement = source.Populate(DisplayElement);
                        return true;
                    case "designation":
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
                    case "designation":
                        source.PopulateListItem(Designation, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConceptReferenceComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
                    if(DisplayElement != null) dest.DisplayElement = (Hl7.Fhir.Model.FhirString)DisplayElement.DeepCopy();
                    if(Designation != null) dest.Designation = new List<DesignationComponent>(Designation.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ConceptReferenceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConceptReferenceComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(DisplayElement, otherT.DisplayElement)) return false;
                if( !DeepComparable.Matches(Designation, otherT.Designation)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConceptReferenceComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(DisplayElement, otherT.DisplayElement)) return false;
                if( !DeepComparable.IsExactly(Designation, otherT.Designation)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    if (DisplayElement != null) yield return DisplayElement;
                    foreach (var elem in Designation) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (DisplayElement != null) yield return new ElementValue("display", DisplayElement);
                    foreach (var elem in Designation) { if (elem != null) yield return new ElementValue("designation", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "FilterComponent")]
        [DataContract]
        public partial class FilterComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IValueSetFilterComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "FilterComponent"; } }
            
            /// <summary>
            /// A property defined by the code system
            /// </summary>
            [FhirElement("property", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code PropertyElement
            {
                get { return _PropertyElement; }
                set { _PropertyElement = value; OnPropertyChanged("PropertyElement"); }
            }
            
            private Hl7.Fhir.Model.Code _PropertyElement;
            
            /// <summary>
            /// A property defined by the code system
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Property
            {
                get { return PropertyElement != null ? PropertyElement.Value : null; }
                set
                {
                    if (value == null)
                        PropertyElement = null;
                    else
                        PropertyElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Property");
                }
            }
            
            /// <summary>
            /// = | is-a | is-not-a | regex | in | not-in
            /// </summary>
            [FhirElement("op", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.DSTU2.FilterOperator> OpElement
            {
                get { return _OpElement; }
                set { _OpElement = value; OnPropertyChanged("OpElement"); }
            }
            
            private Code<Hl7.Fhir.Model.DSTU2.FilterOperator> _OpElement;
            
            /// <summary>
            /// = | is-a | is-not-a | regex | in | not-in
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.DSTU2.FilterOperator? Op
            {
                get { return OpElement != null ? OpElement.Value : null; }
                set
                {
                    if (value == null)
                        OpElement = null;
                    else
                        OpElement = new Code<Hl7.Fhir.Model.DSTU2.FilterOperator>(value);
                    OnPropertyChanged("Op");
                }
            }
            
            /// <summary>
            /// Code from the system, or regex criteria
            /// </summary>
            [FhirElement("value", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.Code _ValueElement;
            
            /// <summary>
            /// Code from the system, or regex criteria
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Value
            {
                get { return ValueElement != null ? ValueElement.Value : null; }
                set
                {
                    if (value == null)
                        ValueElement = null;
                    else
                        ValueElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Value");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("FilterComponent");
                base.Serialize(sink);
                sink.Element("property", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); PropertyElement?.Serialize(sink);
                sink.Element("op", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); OpElement?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); ValueElement?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "property":
                        PropertyElement = source.PopulateValue(PropertyElement);
                        return true;
                    case "_property":
                        PropertyElement = source.Populate(PropertyElement);
                        return true;
                    case "op":
                        OpElement = source.PopulateValue(OpElement);
                        return true;
                    case "_op":
                        OpElement = source.Populate(OpElement);
                        return true;
                    case "value":
                        ValueElement = source.PopulateValue(ValueElement);
                        return true;
                    case "_value":
                        ValueElement = source.Populate(ValueElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as FilterComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(PropertyElement != null) dest.PropertyElement = (Hl7.Fhir.Model.Code)PropertyElement.DeepCopy();
                    if(OpElement != null) dest.OpElement = (Code<Hl7.Fhir.Model.DSTU2.FilterOperator>)OpElement.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.Code)ValueElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new FilterComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as FilterComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(PropertyElement, otherT.PropertyElement)) return false;
                if( !DeepComparable.Matches(OpElement, otherT.OpElement)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as FilterComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(PropertyElement, otherT.PropertyElement)) return false;
                if( !DeepComparable.IsExactly(OpElement, otherT.OpElement)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (PropertyElement != null) yield return PropertyElement;
                    if (OpElement != null) yield return OpElement;
                    if (ValueElement != null) yield return ValueElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (PropertyElement != null) yield return new ElementValue("property", PropertyElement);
                    if (OpElement != null) yield return new ElementValue("op", OpElement);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ExpansionComponent")]
        [DataContract]
        public partial class ExpansionComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IValueSetExpansionComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ExpansionComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IValueSetParameterComponent> Hl7.Fhir.Model.IValueSetExpansionComponent.Parameter { get { return Parameter; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IValueSetContainsComponent> Hl7.Fhir.Model.IValueSetExpansionComponent.Contains { get { return Contains; } }
            
            /// <summary>
            /// Uniquely identifies this expansion
            /// </summary>
            [FhirElement("identifier", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri IdentifierElement
            {
                get { return _IdentifierElement; }
                set { _IdentifierElement = value; OnPropertyChanged("IdentifierElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _IdentifierElement;
            
            /// <summary>
            /// Uniquely identifies this expansion
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Identifier
            {
                get { return IdentifierElement != null ? IdentifierElement.Value : null; }
                set
                {
                    if (value == null)
                        IdentifierElement = null;
                    else
                        IdentifierElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Identifier");
                }
            }
            
            /// <summary>
            /// Time ValueSet expansion happened
            /// </summary>
            [FhirElement("timestamp", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime TimestampElement
            {
                get { return _TimestampElement; }
                set { _TimestampElement = value; OnPropertyChanged("TimestampElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _TimestampElement;
            
            /// <summary>
            /// Time ValueSet expansion happened
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Timestamp
            {
                get { return TimestampElement != null ? TimestampElement.Value : null; }
                set
                {
                    if (value == null)
                        TimestampElement = null;
                    else
                        TimestampElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("Timestamp");
                }
            }
            
            /// <summary>
            /// Total number of codes in the expansion
            /// </summary>
            [FhirElement("total", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Integer TotalElement
            {
                get { return _TotalElement; }
                set { _TotalElement = value; OnPropertyChanged("TotalElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _TotalElement;
            
            /// <summary>
            /// Total number of codes in the expansion
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Total
            {
                get { return TotalElement != null ? TotalElement.Value : null; }
                set
                {
                    if (value == null)
                        TotalElement = null;
                    else
                        TotalElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Total");
                }
            }
            
            /// <summary>
            /// Offset at which this resource starts
            /// </summary>
            [FhirElement("offset", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Integer OffsetElement
            {
                get { return _OffsetElement; }
                set { _OffsetElement = value; OnPropertyChanged("OffsetElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _OffsetElement;
            
            /// <summary>
            /// Offset at which this resource starts
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Offset
            {
                get { return OffsetElement != null ? OffsetElement.Value : null; }
                set
                {
                    if (value == null)
                        OffsetElement = null;
                    else
                        OffsetElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Offset");
                }
            }
            
            /// <summary>
            /// Parameter that controlled the expansion process
            /// </summary>
            [FhirElement("parameter", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ParameterComponent> Parameter
            {
                get { if(_Parameter==null) _Parameter = new List<ParameterComponent>(); return _Parameter; }
                set { _Parameter = value; OnPropertyChanged("Parameter"); }
            }
            
            private List<ParameterComponent> _Parameter;
            
            /// <summary>
            /// Codes in the value set
            /// </summary>
            [FhirElement("contains", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ContainsComponent> Contains
            {
                get { if(_Contains==null) _Contains = new List<ContainsComponent>(); return _Contains; }
                set { _Contains = value; OnPropertyChanged("Contains"); }
            }
            
            private List<ContainsComponent> _Contains;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ExpansionComponent");
                base.Serialize(sink);
                sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); IdentifierElement?.Serialize(sink);
                sink.Element("timestamp", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); TimestampElement?.Serialize(sink);
                sink.Element("total", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); TotalElement?.Serialize(sink);
                sink.Element("offset", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); OffsetElement?.Serialize(sink);
                sink.BeginList("parameter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Parameter)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("contains", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Contains)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "identifier":
                        IdentifierElement = source.PopulateValue(IdentifierElement);
                        return true;
                    case "_identifier":
                        IdentifierElement = source.Populate(IdentifierElement);
                        return true;
                    case "timestamp":
                        TimestampElement = source.PopulateValue(TimestampElement);
                        return true;
                    case "_timestamp":
                        TimestampElement = source.Populate(TimestampElement);
                        return true;
                    case "total":
                        TotalElement = source.PopulateValue(TotalElement);
                        return true;
                    case "_total":
                        TotalElement = source.Populate(TotalElement);
                        return true;
                    case "offset":
                        OffsetElement = source.PopulateValue(OffsetElement);
                        return true;
                    case "_offset":
                        OffsetElement = source.Populate(OffsetElement);
                        return true;
                    case "parameter":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "contains":
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
                    case "parameter":
                        source.PopulateListItem(Parameter, index);
                        return true;
                    case "contains":
                        source.PopulateListItem(Contains, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ExpansionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(IdentifierElement != null) dest.IdentifierElement = (Hl7.Fhir.Model.FhirUri)IdentifierElement.DeepCopy();
                    if(TimestampElement != null) dest.TimestampElement = (Hl7.Fhir.Model.FhirDateTime)TimestampElement.DeepCopy();
                    if(TotalElement != null) dest.TotalElement = (Hl7.Fhir.Model.Integer)TotalElement.DeepCopy();
                    if(OffsetElement != null) dest.OffsetElement = (Hl7.Fhir.Model.Integer)OffsetElement.DeepCopy();
                    if(Parameter != null) dest.Parameter = new List<ParameterComponent>(Parameter.DeepCopy());
                    if(Contains != null) dest.Contains = new List<ContainsComponent>(Contains.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ExpansionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ExpansionComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(IdentifierElement, otherT.IdentifierElement)) return false;
                if( !DeepComparable.Matches(TimestampElement, otherT.TimestampElement)) return false;
                if( !DeepComparable.Matches(TotalElement, otherT.TotalElement)) return false;
                if( !DeepComparable.Matches(OffsetElement, otherT.OffsetElement)) return false;
                if( !DeepComparable.Matches(Parameter, otherT.Parameter)) return false;
                if( !DeepComparable.Matches(Contains, otherT.Contains)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ExpansionComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(IdentifierElement, otherT.IdentifierElement)) return false;
                if( !DeepComparable.IsExactly(TimestampElement, otherT.TimestampElement)) return false;
                if( !DeepComparable.IsExactly(TotalElement, otherT.TotalElement)) return false;
                if( !DeepComparable.IsExactly(OffsetElement, otherT.OffsetElement)) return false;
                if( !DeepComparable.IsExactly(Parameter, otherT.Parameter)) return false;
                if( !DeepComparable.IsExactly(Contains, otherT.Contains)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (IdentifierElement != null) yield return IdentifierElement;
                    if (TimestampElement != null) yield return TimestampElement;
                    if (TotalElement != null) yield return TotalElement;
                    if (OffsetElement != null) yield return OffsetElement;
                    foreach (var elem in Parameter) { if (elem != null) yield return elem; }
                    foreach (var elem in Contains) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (IdentifierElement != null) yield return new ElementValue("identifier", IdentifierElement);
                    if (TimestampElement != null) yield return new ElementValue("timestamp", TimestampElement);
                    if (TotalElement != null) yield return new ElementValue("total", TotalElement);
                    if (OffsetElement != null) yield return new ElementValue("offset", OffsetElement);
                    foreach (var elem in Parameter) { if (elem != null) yield return new ElementValue("parameter", elem); }
                    foreach (var elem in Contains) { if (elem != null) yield return new ElementValue("contains", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ParameterComponent")]
        [DataContract]
        public partial class ParameterComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IValueSetParameterComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ParameterComponent"; } }
            
            /// <summary>
            /// Name as assigned by the server
            /// </summary>
            [FhirElement("name", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Name as assigned by the server
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
            /// Value of the named parameter
            /// </summary>
            [FhirElement("value", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Code))]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ParameterComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); NameElement?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Value?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "name":
                        NameElement = source.PopulateValue(NameElement);
                        return true;
                    case "_name":
                        NameElement = source.Populate(NameElement);
                        return true;
                    case "valueString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "_valueString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "valueBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.FhirBoolean);
                        return true;
                    case "_valueBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.FhirBoolean);
                        return true;
                    case "valueInteger":
                        source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Integer);
                        return true;
                    case "_valueInteger":
                        source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Integer);
                        return true;
                    case "valueDecimal":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.FhirDecimal);
                        return true;
                    case "_valueDecimal":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.FhirDecimal);
                        return true;
                    case "valueUri":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.FhirUri);
                        return true;
                    case "_valueUri":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.FhirUri);
                        return true;
                    case "valueCode":
                        source.CheckDuplicates<Hl7.Fhir.Model.Code>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Code);
                        return true;
                    case "_valueCode":
                        source.CheckDuplicates<Hl7.Fhir.Model.Code>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Code);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ParameterComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ParameterComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ParameterComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ParameterComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (Value != null) yield return Value;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (Value != null) yield return new ElementValue("value", Value);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ContainsComponent")]
        [DataContract]
        public partial class ContainsComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IValueSetContainsComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ContainsComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IValueSetContainsComponent> Hl7.Fhir.Model.IValueSetContainsComponent.Contains { get { return Contains; } }
            
            /// <summary>
            /// System value for the code
            /// </summary>
            [FhirElement("system", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri SystemElement
            {
                get { return _SystemElement; }
                set { _SystemElement = value; OnPropertyChanged("SystemElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _SystemElement;
            
            /// <summary>
            /// System value for the code
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
            /// If user cannot select this entry
            /// </summary>
            [FhirElement("abstract", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean AbstractElement
            {
                get { return _AbstractElement; }
                set { _AbstractElement = value; OnPropertyChanged("AbstractElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _AbstractElement;
            
            /// <summary>
            /// If user cannot select this entry
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Abstract
            {
                get { return AbstractElement != null ? AbstractElement.Value : null; }
                set
                {
                    if (value == null)
                        AbstractElement = null;
                    else
                        AbstractElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Abstract");
                }
            }
            
            /// <summary>
            /// Version in which this code/display is defined
            /// </summary>
            [FhirElement("version", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VersionElement
            {
                get { return _VersionElement; }
                set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _VersionElement;
            
            /// <summary>
            /// Version in which this code/display is defined
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
            /// Code - if blank, this is not a selectable code
            /// </summary>
            [FhirElement("code", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Code CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Hl7.Fhir.Model.Code _CodeElement;
            
            /// <summary>
            /// Code - if blank, this is not a selectable code
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
            /// User display for the concept
            /// </summary>
            [FhirElement("display", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DisplayElement
            {
                get { return _DisplayElement; }
                set { _DisplayElement = value; OnPropertyChanged("DisplayElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DisplayElement;
            
            /// <summary>
            /// User display for the concept
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
            
            /// <summary>
            /// Codes contained under this entry
            /// </summary>
            [FhirElement("contains", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ContainsComponent> Contains
            {
                get { if(_Contains==null) _Contains = new List<ContainsComponent>(); return _Contains; }
                set { _Contains = value; OnPropertyChanged("Contains"); }
            }
            
            private List<ContainsComponent> _Contains;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ContainsComponent");
                base.Serialize(sink);
                sink.Element("system", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); SystemElement?.Serialize(sink);
                sink.Element("abstract", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); AbstractElement?.Serialize(sink);
                sink.Element("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); VersionElement?.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CodeElement?.Serialize(sink);
                sink.Element("display", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DisplayElement?.Serialize(sink);
                sink.BeginList("contains", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Contains)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "system":
                        SystemElement = source.PopulateValue(SystemElement);
                        return true;
                    case "_system":
                        SystemElement = source.Populate(SystemElement);
                        return true;
                    case "abstract":
                        AbstractElement = source.PopulateValue(AbstractElement);
                        return true;
                    case "_abstract":
                        AbstractElement = source.Populate(AbstractElement);
                        return true;
                    case "version":
                        VersionElement = source.PopulateValue(VersionElement);
                        return true;
                    case "_version":
                        VersionElement = source.Populate(VersionElement);
                        return true;
                    case "code":
                        CodeElement = source.PopulateValue(CodeElement);
                        return true;
                    case "_code":
                        CodeElement = source.Populate(CodeElement);
                        return true;
                    case "display":
                        DisplayElement = source.PopulateValue(DisplayElement);
                        return true;
                    case "_display":
                        DisplayElement = source.Populate(DisplayElement);
                        return true;
                    case "contains":
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
                    case "contains":
                        source.PopulateListItem(Contains, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ContainsComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SystemElement != null) dest.SystemElement = (Hl7.Fhir.Model.FhirUri)SystemElement.DeepCopy();
                    if(AbstractElement != null) dest.AbstractElement = (Hl7.Fhir.Model.FhirBoolean)AbstractElement.DeepCopy();
                    if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                    if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
                    if(DisplayElement != null) dest.DisplayElement = (Hl7.Fhir.Model.FhirString)DisplayElement.DeepCopy();
                    if(Contains != null) dest.Contains = new List<ContainsComponent>(Contains.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ContainsComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ContainsComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SystemElement, otherT.SystemElement)) return false;
                if( !DeepComparable.Matches(AbstractElement, otherT.AbstractElement)) return false;
                if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(DisplayElement, otherT.DisplayElement)) return false;
                if( !DeepComparable.Matches(Contains, otherT.Contains)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ContainsComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SystemElement, otherT.SystemElement)) return false;
                if( !DeepComparable.IsExactly(AbstractElement, otherT.AbstractElement)) return false;
                if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(DisplayElement, otherT.DisplayElement)) return false;
                if( !DeepComparable.IsExactly(Contains, otherT.Contains)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SystemElement != null) yield return SystemElement;
                    if (AbstractElement != null) yield return AbstractElement;
                    if (VersionElement != null) yield return VersionElement;
                    if (CodeElement != null) yield return CodeElement;
                    if (DisplayElement != null) yield return DisplayElement;
                    foreach (var elem in Contains) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SystemElement != null) yield return new ElementValue("system", SystemElement);
                    if (AbstractElement != null) yield return new ElementValue("abstract", AbstractElement);
                    if (VersionElement != null) yield return new ElementValue("version", VersionElement);
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (DisplayElement != null) yield return new ElementValue("display", DisplayElement);
                    foreach (var elem in Contains) { if (elem != null) yield return new ElementValue("contains", elem); }
                }
            }
        
        
        }
        
        [NotMapped]
        Hl7.Fhir.Model.IValueSetComposeComponent Hl7.Fhir.Model.IValueSet.Compose { get { return Compose; } }
        
        [NotMapped]
        Hl7.Fhir.Model.IValueSetExpansionComponent Hl7.Fhir.Model.IValueSet.Expansion { get { return Expansion; } }
    
        
        /// <summary>
        /// Globally unique logical identifier for  value set
        /// </summary>
        [FhirElement("url", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _UrlElement;
        
        /// <summary>
        /// Globally unique logical identifier for  value set
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
        /// Additional identifier for the value set (e.g. HL7 v2 / CDA)
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// Logical identifier for this version of the value set
        /// </summary>
        [FhirElement("version", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// Logical identifier for this version of the value set
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
        /// Informal name for this value set
        /// </summary>
        [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Informal name for this value set
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
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DSTU2.ConformanceResourceStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DSTU2.ConformanceResourceStatus> _StatusElement;
        
        /// <summary>
        /// draft | active | retired
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DSTU2.ConformanceResourceStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.DSTU2.ConformanceResourceStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// If for testing purposes, not real usage
        /// </summary>
        [FhirElement("experimental", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
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
                if (value == null)
                    ExperimentalElement = null;
                else
                    ExperimentalElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Experimental");
            }
        }
        
        /// <summary>
        /// Name of the publisher (organization or individual)
        /// </summary>
        [FhirElement("publisher", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
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
        [FhirElement("contact", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContactComponent> Contact
        {
            get { if(_Contact==null) _Contact = new List<ContactComponent>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<ContactComponent> _Contact;
        
        /// <summary>
        /// Date for given status
        /// </summary>
        [FhirElement("date", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
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
        /// Fixed date for all referenced code systems and value sets
        /// </summary>
        [FhirElement("lockedDate", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Date LockedDateElement
        {
            get { return _LockedDateElement; }
            set { _LockedDateElement = value; OnPropertyChanged("LockedDateElement"); }
        }
        
        private Hl7.Fhir.Model.Date _LockedDateElement;
        
        /// <summary>
        /// Fixed date for all referenced code systems and value sets
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string LockedDate
        {
            get { return LockedDateElement != null ? LockedDateElement.Value : null; }
            set
            {
                if (value == null)
                    LockedDateElement = null;
                else
                    LockedDateElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("LockedDate");
            }
        }
        
        /// <summary>
        /// Human language description of the value set
        /// </summary>
        [FhirElement("description", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// Human language description of the value set
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
        /// Content intends to support these contexts
        /// </summary>
        [FhirElement("useContext", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> UseContext
        {
            get { if(_UseContext==null) _UseContext = new List<Hl7.Fhir.Model.CodeableConcept>(); return _UseContext; }
            set { _UseContext = value; OnPropertyChanged("UseContext"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _UseContext;
        
        /// <summary>
        /// Indicates whether or not any change to the content logical definition may occur
        /// </summary>
        [FhirElement("immutable", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ImmutableElement
        {
            get { return _ImmutableElement; }
            set { _ImmutableElement = value; OnPropertyChanged("ImmutableElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _ImmutableElement;
        
        /// <summary>
        /// Indicates whether or not any change to the content logical definition may occur
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Immutable
        {
            get { return ImmutableElement != null ? ImmutableElement.Value : null; }
            set
            {
                if (value == null)
                    ImmutableElement = null;
                else
                    ImmutableElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Immutable");
            }
        }
        
        /// <summary>
        /// Why needed
        /// </summary>
        [FhirElement("requirements", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString RequirementsElement
        {
            get { return _RequirementsElement; }
            set { _RequirementsElement = value; OnPropertyChanged("RequirementsElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _RequirementsElement;
        
        /// <summary>
        /// Why needed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Requirements
        {
            get { return RequirementsElement != null ? RequirementsElement.Value : null; }
            set
            {
                if (value == null)
                    RequirementsElement = null;
                else
                    RequirementsElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Requirements");
            }
        }
        
        /// <summary>
        /// Use and/or publishing restrictions
        /// </summary>
        [FhirElement("copyright", Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CopyrightElement
        {
            get { return _CopyrightElement; }
            set { _CopyrightElement = value; OnPropertyChanged("CopyrightElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _CopyrightElement;
        
        /// <summary>
        /// Use and/or publishing restrictions
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Copyright
        {
            get { return CopyrightElement != null ? CopyrightElement.Value : null; }
            set
            {
                if (value == null)
                    CopyrightElement = null;
                else
                    CopyrightElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Copyright");
            }
        }
        
        /// <summary>
        /// Whether this is intended to be used with an extensible binding
        /// </summary>
        [FhirElement("extensible", InSummary=Hl7.Fhir.Model.Version.All, Order=240)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ExtensibleElement
        {
            get { return _ExtensibleElement; }
            set { _ExtensibleElement = value; OnPropertyChanged("ExtensibleElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _ExtensibleElement;
        
        /// <summary>
        /// Whether this is intended to be used with an extensible binding
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Extensible
        {
            get { return ExtensibleElement != null ? ExtensibleElement.Value : null; }
            set
            {
                if (value == null)
                    ExtensibleElement = null;
                else
                    ExtensibleElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Extensible");
            }
        }
        
        /// <summary>
        /// An inline code system, which is part of this value set
        /// </summary>
        [FhirElement("codeSystem", InSummary=Hl7.Fhir.Model.Version.All, Order=250)]
        [CLSCompliant(false)]
        [DataMember]
        public CodeSystemComponent CodeSystem
        {
            get { return _CodeSystem; }
            set { _CodeSystem = value; OnPropertyChanged("CodeSystem"); }
        }
        
        private CodeSystemComponent _CodeSystem;
        
        /// <summary>
        /// When value set includes codes from elsewhere
        /// </summary>
        [FhirElement("compose", Order=260)]
        [DataMember]
        public ComposeComponent Compose
        {
            get { return _Compose; }
            set { _Compose = value; OnPropertyChanged("Compose"); }
        }
        
        private ComposeComponent _Compose;
        
        /// <summary>
        /// Used when the value set is "expanded"
        /// </summary>
        [FhirElement("expansion", Order=270)]
        [DataMember]
        public ExpansionComponent Expansion
        {
            get { return _Expansion; }
            set { _Expansion = value; OnPropertyChanged("Expansion"); }
        }
        
        private ExpansionComponent _Expansion;
    
    
        public static ElementDefinitionConstraint[] ValueSet_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "vsd-7",
                severity: ConstraintSeverity.Warning,
                expression: "codeSystem.empty() or (codeSystem.system != url)",
                human: "A defined code system (if present) SHALL have a different url than the value set url",
                xpath: "not(f:codeSystem/f:system/@value = f:url/@value)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "vsd-5",
                severity: ConstraintSeverity.Warning,
                expression: "codeSystem.exists() or compose.exists() or expansion.exists()",
                human: "Value set SHALL contain at least one of a codeSystem, a compose, or an expansion element",
                xpath: "exists(f:codeSystem) or exists(f:compose) or exists(f:expansion)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "vsd-2",
                severity: ConstraintSeverity.Warning,
                expression: "compose.import.count() != 1 or compose.include.exists() or compose.exclude.exists() or codeSystem.exists()",
                human: "A value set with only one import SHALL also have an include and/or an exclude unless the value set includes and inline code system",
                xpath: "not(exists(f:compose)) or (count(f:compose/f:import)!=1 or exists(f:compose/f:include) or exists(f:compose/f:exclude) or exists(f:codeSystem))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "vsd-8",
                severity: ConstraintSeverity.Warning,
                expression: "codeSystem.all(descendants().concept.code.isDistinct())",
                human: "Codes must be unique",
                xpath: "count(descendant::f:concept)=count(distinct-values(descendant::f:concept/f:code/@value))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "vsd-3",
                severity: ConstraintSeverity.Warning,
                expression: "codeSystem.all(descendants().concept.code.isDistinct())",
                human: "Within a code system definition, all the codes SHALL be unique",
                xpath: "count(distinct-values(descendant::f:concept/f:code/@value))=count(descendant::f:concept)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "vsd-1",
                severity: ConstraintSeverity.Warning,
                expression: "compose.all(include.exists() or import.exists())",
                human: "A value set composition SHALL have an include or an import",
                xpath: "exists(f:include) or exists(f:import)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "vsd-11",
                severity: ConstraintSeverity.Warning,
                expression: "compose.include.all(concept.empty() or filter.empty())",
                human: "Cannot have both concept and filter",
                xpath: "not(exists(f:concept)) or not(exists(f:filter))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "vsd-9",
                severity: ConstraintSeverity.Warning,
                expression: "expansion.contains.all(code.exists() or (abstract = 'true'))",
                human: "Must have a code if not abstract",
                xpath: "exists(f:code) or (f:abstract/@value = true())"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "vsd-6",
                severity: ConstraintSeverity.Warning,
                expression: "expansion.contains.all(code.exists() or display.exists())",
                human: "SHALL have a code or a display",
                xpath: "exists(f:code) or exists(f:display)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "vsd-10",
                severity: ConstraintSeverity.Warning,
                expression: "expansion.contains.all(code.empty() or system.exists())",
                human: "Must have a system if a code is present",
                xpath: "exists(f:system) or not(exists(f:code))"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(ValueSet_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ValueSet;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.DSTU2.ConformanceResourceStatus>)StatusElement.DeepCopy();
                if(ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
                if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
                if(Contact != null) dest.Contact = new List<ContactComponent>(Contact.DeepCopy());
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(LockedDateElement != null) dest.LockedDateElement = (Hl7.Fhir.Model.Date)LockedDateElement.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(UseContext != null) dest.UseContext = new List<Hl7.Fhir.Model.CodeableConcept>(UseContext.DeepCopy());
                if(ImmutableElement != null) dest.ImmutableElement = (Hl7.Fhir.Model.FhirBoolean)ImmutableElement.DeepCopy();
                if(RequirementsElement != null) dest.RequirementsElement = (Hl7.Fhir.Model.FhirString)RequirementsElement.DeepCopy();
                if(CopyrightElement != null) dest.CopyrightElement = (Hl7.Fhir.Model.FhirString)CopyrightElement.DeepCopy();
                if(ExtensibleElement != null) dest.ExtensibleElement = (Hl7.Fhir.Model.FhirBoolean)ExtensibleElement.DeepCopy();
                if(CodeSystem != null) dest.CodeSystem = (CodeSystemComponent)CodeSystem.DeepCopy();
                if(Compose != null) dest.Compose = (ComposeComponent)Compose.DeepCopy();
                if(Expansion != null) dest.Expansion = (ExpansionComponent)Expansion.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new ValueSet());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ValueSet;
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
            if( !DeepComparable.Matches(LockedDateElement, otherT.LockedDateElement)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.Matches(ImmutableElement, otherT.ImmutableElement)) return false;
            if( !DeepComparable.Matches(RequirementsElement, otherT.RequirementsElement)) return false;
            if( !DeepComparable.Matches(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.Matches(ExtensibleElement, otherT.ExtensibleElement)) return false;
            if( !DeepComparable.Matches(CodeSystem, otherT.CodeSystem)) return false;
            if( !DeepComparable.Matches(Compose, otherT.Compose)) return false;
            if( !DeepComparable.Matches(Expansion, otherT.Expansion)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ValueSet;
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
            if( !DeepComparable.IsExactly(LockedDateElement, otherT.LockedDateElement)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.IsExactly(ImmutableElement, otherT.ImmutableElement)) return false;
            if( !DeepComparable.IsExactly(RequirementsElement, otherT.RequirementsElement)) return false;
            if( !DeepComparable.IsExactly(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.IsExactly(ExtensibleElement, otherT.ExtensibleElement)) return false;
            if( !DeepComparable.IsExactly(CodeSystem, otherT.CodeSystem)) return false;
            if( !DeepComparable.IsExactly(Compose, otherT.Compose)) return false;
            if( !DeepComparable.IsExactly(Expansion, otherT.Expansion)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("ValueSet");
            base.Serialize(sink);
            sink.Element("url", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); UrlElement?.Serialize(sink);
            sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Identifier?.Serialize(sink);
            sink.Element("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VersionElement?.Serialize(sink);
            sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NameElement?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("experimental", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ExperimentalElement?.Serialize(sink);
            sink.Element("publisher", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PublisherElement?.Serialize(sink);
            sink.BeginList("contact", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Contact)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("date", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DateElement?.Serialize(sink);
            sink.Element("lockedDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); LockedDateElement?.Serialize(sink);
            sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DescriptionElement?.Serialize(sink);
            sink.BeginList("useContext", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in UseContext)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("immutable", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ImmutableElement?.Serialize(sink);
            sink.Element("requirements", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); RequirementsElement?.Serialize(sink);
            sink.Element("copyright", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CopyrightElement?.Serialize(sink);
            sink.Element("extensible", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ExtensibleElement?.Serialize(sink);
            sink.Element("codeSystem", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CodeSystem?.Serialize(sink);
            sink.Element("compose", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Compose?.Serialize(sink);
            sink.Element("expansion", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Expansion?.Serialize(sink);
            sink.End();
        }
    
        internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
        {
            if (base.SetElementFromJson(jsonPropertyName, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "url":
                    UrlElement = source.PopulateValue(UrlElement);
                    return true;
                case "_url":
                    UrlElement = source.Populate(UrlElement);
                    return true;
                case "identifier":
                    Identifier = source.Populate(Identifier);
                    return true;
                case "version":
                    VersionElement = source.PopulateValue(VersionElement);
                    return true;
                case "_version":
                    VersionElement = source.Populate(VersionElement);
                    return true;
                case "name":
                    NameElement = source.PopulateValue(NameElement);
                    return true;
                case "_name":
                    NameElement = source.Populate(NameElement);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "experimental":
                    ExperimentalElement = source.PopulateValue(ExperimentalElement);
                    return true;
                case "_experimental":
                    ExperimentalElement = source.Populate(ExperimentalElement);
                    return true;
                case "publisher":
                    PublisherElement = source.PopulateValue(PublisherElement);
                    return true;
                case "_publisher":
                    PublisherElement = source.Populate(PublisherElement);
                    return true;
                case "contact":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "date":
                    DateElement = source.PopulateValue(DateElement);
                    return true;
                case "_date":
                    DateElement = source.Populate(DateElement);
                    return true;
                case "lockedDate":
                    LockedDateElement = source.PopulateValue(LockedDateElement);
                    return true;
                case "_lockedDate":
                    LockedDateElement = source.Populate(LockedDateElement);
                    return true;
                case "description":
                    DescriptionElement = source.PopulateValue(DescriptionElement);
                    return true;
                case "_description":
                    DescriptionElement = source.Populate(DescriptionElement);
                    return true;
                case "useContext":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "immutable":
                    ImmutableElement = source.PopulateValue(ImmutableElement);
                    return true;
                case "_immutable":
                    ImmutableElement = source.Populate(ImmutableElement);
                    return true;
                case "requirements":
                    RequirementsElement = source.PopulateValue(RequirementsElement);
                    return true;
                case "_requirements":
                    RequirementsElement = source.Populate(RequirementsElement);
                    return true;
                case "copyright":
                    CopyrightElement = source.PopulateValue(CopyrightElement);
                    return true;
                case "_copyright":
                    CopyrightElement = source.Populate(CopyrightElement);
                    return true;
                case "extensible":
                    ExtensibleElement = source.PopulateValue(ExtensibleElement);
                    return true;
                case "_extensible":
                    ExtensibleElement = source.Populate(ExtensibleElement);
                    return true;
                case "codeSystem":
                    CodeSystem = source.Populate(CodeSystem);
                    return true;
                case "compose":
                    Compose = source.Populate(Compose);
                    return true;
                case "expansion":
                    Expansion = source.Populate(Expansion);
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
                case "contact":
                    source.PopulateListItem(Contact, index);
                    return true;
                case "useContext":
                    source.PopulateListItem(UseContext, index);
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
                if (UrlElement != null) yield return UrlElement;
                if (Identifier != null) yield return Identifier;
                if (VersionElement != null) yield return VersionElement;
                if (NameElement != null) yield return NameElement;
                if (StatusElement != null) yield return StatusElement;
                if (ExperimentalElement != null) yield return ExperimentalElement;
                if (PublisherElement != null) yield return PublisherElement;
                foreach (var elem in Contact) { if (elem != null) yield return elem; }
                if (DateElement != null) yield return DateElement;
                if (LockedDateElement != null) yield return LockedDateElement;
                if (DescriptionElement != null) yield return DescriptionElement;
                foreach (var elem in UseContext) { if (elem != null) yield return elem; }
                if (ImmutableElement != null) yield return ImmutableElement;
                if (RequirementsElement != null) yield return RequirementsElement;
                if (CopyrightElement != null) yield return CopyrightElement;
                if (ExtensibleElement != null) yield return ExtensibleElement;
                if (CodeSystem != null) yield return CodeSystem;
                if (Compose != null) yield return Compose;
                if (Expansion != null) yield return Expansion;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                if (VersionElement != null) yield return new ElementValue("version", VersionElement);
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (ExperimentalElement != null) yield return new ElementValue("experimental", ExperimentalElement);
                if (PublisherElement != null) yield return new ElementValue("publisher", PublisherElement);
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (LockedDateElement != null) yield return new ElementValue("lockedDate", LockedDateElement);
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                foreach (var elem in UseContext) { if (elem != null) yield return new ElementValue("useContext", elem); }
                if (ImmutableElement != null) yield return new ElementValue("immutable", ImmutableElement);
                if (RequirementsElement != null) yield return new ElementValue("requirements", RequirementsElement);
                if (CopyrightElement != null) yield return new ElementValue("copyright", CopyrightElement);
                if (ExtensibleElement != null) yield return new ElementValue("extensible", ExtensibleElement);
                if (CodeSystem != null) yield return new ElementValue("codeSystem", CodeSystem);
                if (Compose != null) yield return new ElementValue("compose", Compose);
                if (Expansion != null) yield return new ElementValue("expansion", Expansion);
            }
        }
    
    }

}
