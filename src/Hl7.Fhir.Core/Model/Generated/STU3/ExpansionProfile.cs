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
namespace Hl7.Fhir.Model.STU3
{
    /// <summary>
    /// Defines behaviour and contraints on the ValueSet Expansion operation
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "ExpansionProfile", IsResource=true)]
    [DataContract]
    public partial class ExpansionProfile : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ExpansionProfile; } }
        [NotMapped]
        public override string TypeName { get { return "ExpansionProfile"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "FixedVersionComponent")]
        [DataContract]
        public partial class FixedVersionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "FixedVersionComponent"; } }
            
            /// <summary>
            /// System to have its version fixed
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
            /// System to have its version fixed
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
            [Cardinality(Min=1,Max=1)]
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
            /// default | check | override
            /// </summary>
            [FhirElement("mode", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.STU3.SystemVersionProcessingMode> ModeElement
            {
                get { return _ModeElement; }
                set { _ModeElement = value; OnPropertyChanged("ModeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.STU3.SystemVersionProcessingMode> _ModeElement;
            
            /// <summary>
            /// default | check | override
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.STU3.SystemVersionProcessingMode? Mode
            {
                get { return ModeElement != null ? ModeElement.Value : null; }
                set
                {
                    if (value == null)
                        ModeElement = null;
                    else
                        ModeElement = new Code<Hl7.Fhir.Model.STU3.SystemVersionProcessingMode>(value);
                    OnPropertyChanged("Mode");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("FixedVersionComponent");
                base.Serialize(sink);
                sink.Element("system", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); SystemElement?.Serialize(sink);
                sink.Element("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); VersionElement?.Serialize(sink);
                sink.Element("mode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); ModeElement?.Serialize(sink);
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
                    case "mode":
                        ModeElement = source.PopulateValue(ModeElement);
                        return true;
                    case "_mode":
                        ModeElement = source.Populate(ModeElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as FixedVersionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SystemElement != null) dest.SystemElement = (Hl7.Fhir.Model.FhirUri)SystemElement.DeepCopy();
                    if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                    if(ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.STU3.SystemVersionProcessingMode>)ModeElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new FixedVersionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as FixedVersionComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SystemElement, otherT.SystemElement)) return false;
                if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
                if( !DeepComparable.Matches(ModeElement, otherT.ModeElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as FixedVersionComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SystemElement, otherT.SystemElement)) return false;
                if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
                if( !DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;
            
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
                    if (ModeElement != null) yield return ModeElement;
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
                    if (ModeElement != null) yield return new ElementValue("mode", ModeElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "ExcludedSystemComponent")]
        [DataContract]
        public partial class ExcludedSystemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ExcludedSystemComponent"; } }
            
            /// <summary>
            /// The specific code system to be excluded
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ExcludedSystemComponent");
                base.Serialize(sink);
                sink.Element("system", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); SystemElement?.Serialize(sink);
                sink.Element("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VersionElement?.Serialize(sink);
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
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ExcludedSystemComponent;
            
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
                 return CopyTo(new ExcludedSystemComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ExcludedSystemComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SystemElement, otherT.SystemElement)) return false;
                if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ExcludedSystemComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SystemElement, otherT.SystemElement)) return false;
                if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            
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
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "DesignationComponent")]
        [DataContract]
        public partial class DesignationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DesignationComponent"; } }
            
            /// <summary>
            /// Designations to be included
            /// </summary>
            [FhirElement("include", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public DesignationIncludeComponent Include
            {
                get { return _Include; }
                set { _Include = value; OnPropertyChanged("Include"); }
            }
            
            private DesignationIncludeComponent _Include;
            
            /// <summary>
            /// Designations to be excluded
            /// </summary>
            [FhirElement("exclude", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public DesignationExcludeComponent Exclude
            {
                get { return _Exclude; }
                set { _Exclude = value; OnPropertyChanged("Exclude"); }
            }
            
            private DesignationExcludeComponent _Exclude;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("DesignationComponent");
                base.Serialize(sink);
                sink.Element("include", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Include?.Serialize(sink);
                sink.Element("exclude", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Exclude?.Serialize(sink);
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
                    case "include":
                        Include = source.Populate(Include);
                        return true;
                    case "exclude":
                        Exclude = source.Populate(Exclude);
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
                    if(Include != null) dest.Include = (DesignationIncludeComponent)Include.DeepCopy();
                    if(Exclude != null) dest.Exclude = (DesignationExcludeComponent)Exclude.DeepCopy();
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
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Include != null) yield return Include;
                    if (Exclude != null) yield return Exclude;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Include != null) yield return new ElementValue("include", Include);
                    if (Exclude != null) yield return new ElementValue("exclude", Exclude);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "DesignationIncludeComponent")]
        [DataContract]
        public partial class DesignationIncludeComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DesignationIncludeComponent"; } }
            
            /// <summary>
            /// The designation to be included
            /// </summary>
            [FhirElement("designation", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<DesignationIncludeDesignationComponent> Designation
            {
                get { if(_Designation==null) _Designation = new List<DesignationIncludeDesignationComponent>(); return _Designation; }
                set { _Designation = value; OnPropertyChanged("Designation"); }
            }
            
            private List<DesignationIncludeDesignationComponent> _Designation;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("DesignationIncludeComponent");
                base.Serialize(sink);
                sink.BeginList("designation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
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
                var dest = other as DesignationIncludeComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Designation != null) dest.Designation = new List<DesignationIncludeDesignationComponent>(Designation.DeepCopy());
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
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Designation) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Designation) { if (elem != null) yield return new ElementValue("designation", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "DesignationIncludeDesignationComponent")]
        [DataContract]
        public partial class DesignationIncludeDesignationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DesignationIncludeDesignationComponent"; } }
            
            /// <summary>
            /// Human language of the designation to be included
            /// </summary>
            [FhirElement("language", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
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
            /// What kind of Designation to include
            /// </summary>
            [FhirElement("use", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Use
            {
                get { return _Use; }
                set { _Use = value; OnPropertyChanged("Use"); }
            }
            
            private Hl7.Fhir.Model.Coding _Use;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("DesignationIncludeDesignationComponent");
                base.Serialize(sink);
                sink.Element("language", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); LanguageElement?.Serialize(sink);
                sink.Element("use", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Use?.Serialize(sink);
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
                }
                return false;
            }
        
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
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (LanguageElement != null) yield return LanguageElement;
                    if (Use != null) yield return Use;
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
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "DesignationExcludeComponent")]
        [DataContract]
        public partial class DesignationExcludeComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DesignationExcludeComponent"; } }
            
            /// <summary>
            /// The designation to be excluded
            /// </summary>
            [FhirElement("designation", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<DesignationExcludeDesignationComponent> Designation
            {
                get { if(_Designation==null) _Designation = new List<DesignationExcludeDesignationComponent>(); return _Designation; }
                set { _Designation = value; OnPropertyChanged("Designation"); }
            }
            
            private List<DesignationExcludeDesignationComponent> _Designation;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("DesignationExcludeComponent");
                base.Serialize(sink);
                sink.BeginList("designation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
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
                var dest = other as DesignationExcludeComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Designation != null) dest.Designation = new List<DesignationExcludeDesignationComponent>(Designation.DeepCopy());
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
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Designation) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Designation) { if (elem != null) yield return new ElementValue("designation", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "DesignationExcludeDesignationComponent")]
        [DataContract]
        public partial class DesignationExcludeDesignationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DesignationExcludeDesignationComponent"; } }
            
            /// <summary>
            /// Human language of the designation to be excluded
            /// </summary>
            [FhirElement("language", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
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
            /// What kind of Designation to exclude
            /// </summary>
            [FhirElement("use", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Use
            {
                get { return _Use; }
                set { _Use = value; OnPropertyChanged("Use"); }
            }
            
            private Hl7.Fhir.Model.Coding _Use;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("DesignationExcludeDesignationComponent");
                base.Serialize(sink);
                sink.Element("language", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); LanguageElement?.Serialize(sink);
                sink.Element("use", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Use?.Serialize(sink);
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
                }
                return false;
            }
        
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
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (LanguageElement != null) yield return LanguageElement;
                    if (Use != null) yield return Use;
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
                }
            }
        
        
        }
    
        
        /// <summary>
        /// Logical URI to reference this expansion profile (globally unique)
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
        /// Logical URI to reference this expansion profile (globally unique)
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
        /// Additional identifier for the expansion profile
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
        /// Business version of the expansion profile
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
        /// Business version of the expansion profile
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
        /// Name for this expansion profile (computer friendly)
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
        /// Name for this expansion profile (computer friendly)
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
        /// draft | active | retired | unknown
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.PublicationStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.PublicationStatus> _StatusElement;
        
        /// <summary>
        /// draft | active | retired | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.PublicationStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.PublicationStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// For testing purposes, not real usage
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
        /// For testing purposes, not real usage
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
        /// Date this was last changed
        /// </summary>
        [FhirElement("date", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// Date this was last changed
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
        /// Name of the publisher (organization or individual)
        /// </summary>
        [FhirElement("publisher", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
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
        /// Contact details for the publisher
        /// </summary>
        [FhirElement("contact", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.STU3.ContactDetail> Contact
        {
            get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.STU3.ContactDetail>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<Hl7.Fhir.Model.STU3.ContactDetail> _Contact;
        
        /// <summary>
        /// Natural language description of the expansion profile
        /// </summary>
        [FhirElement("description", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _DescriptionElement;
        
        /// <summary>
        /// Natural language description of the expansion profile
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
                    DescriptionElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Description");
            }
        }
        
        /// <summary>
        /// Context the content is intended to support
        /// </summary>
        [FhirElement("useContext", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.UsageContext> UseContext
        {
            get { if(_UseContext==null) _UseContext = new List<Hl7.Fhir.Model.UsageContext>(); return _UseContext; }
            set { _UseContext = value; OnPropertyChanged("UseContext"); }
        }
        
        private List<Hl7.Fhir.Model.UsageContext> _UseContext;
        
        /// <summary>
        /// Intended jurisdiction for expansion profile (if applicable)
        /// </summary>
        [FhirElement("jurisdiction", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Jurisdiction
        {
            get { if(_Jurisdiction==null) _Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Jurisdiction; }
            set { _Jurisdiction = value; OnPropertyChanged("Jurisdiction"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Jurisdiction;
        
        /// <summary>
        /// Fix use of a code system to a particular version
        /// </summary>
        [FhirElement("fixedVersion", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<FixedVersionComponent> FixedVersion
        {
            get { if(_FixedVersion==null) _FixedVersion = new List<FixedVersionComponent>(); return _FixedVersion; }
            set { _FixedVersion = value; OnPropertyChanged("FixedVersion"); }
        }
        
        private List<FixedVersionComponent> _FixedVersion;
        
        /// <summary>
        /// Systems/Versions to be exclude
        /// </summary>
        [FhirElement("excludedSystem", InSummary=Hl7.Fhir.Model.Version.All, Order=220)]
        [CLSCompliant(false)]
        [DataMember]
        public ExcludedSystemComponent ExcludedSystem
        {
            get { return _ExcludedSystem; }
            set { _ExcludedSystem = value; OnPropertyChanged("ExcludedSystem"); }
        }
        
        private ExcludedSystemComponent _ExcludedSystem;
        
        /// <summary>
        /// Whether the expansion should include concept designations
        /// </summary>
        [FhirElement("includeDesignations", InSummary=Hl7.Fhir.Model.Version.All, Order=230)]
        [CLSCompliant(false)]
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
                if (value == null)
                    IncludeDesignationsElement = null;
                else
                    IncludeDesignationsElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("IncludeDesignations");
            }
        }
        
        /// <summary>
        /// When the expansion profile imposes designation contraints
        /// </summary>
        [FhirElement("designation", InSummary=Hl7.Fhir.Model.Version.All, Order=240)]
        [CLSCompliant(false)]
        [DataMember]
        public DesignationComponent Designation
        {
            get { return _Designation; }
            set { _Designation = value; OnPropertyChanged("Designation"); }
        }
        
        private DesignationComponent _Designation;
        
        /// <summary>
        /// Include or exclude the value set definition in the expansion
        /// </summary>
        [FhirElement("includeDefinition", InSummary=Hl7.Fhir.Model.Version.All, Order=250)]
        [CLSCompliant(false)]
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
                if (value == null)
                    IncludeDefinitionElement = null;
                else
                    IncludeDefinitionElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("IncludeDefinition");
            }
        }
        
        /// <summary>
        /// Include or exclude inactive concepts in the expansion
        /// </summary>
        [FhirElement("activeOnly", InSummary=Hl7.Fhir.Model.Version.All, Order=260)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ActiveOnlyElement
        {
            get { return _ActiveOnlyElement; }
            set { _ActiveOnlyElement = value; OnPropertyChanged("ActiveOnlyElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _ActiveOnlyElement;
        
        /// <summary>
        /// Include or exclude inactive concepts in the expansion
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? ActiveOnly
        {
            get { return ActiveOnlyElement != null ? ActiveOnlyElement.Value : null; }
            set
            {
                if (value == null)
                    ActiveOnlyElement = null;
                else
                    ActiveOnlyElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("ActiveOnly");
            }
        }
        
        /// <summary>
        /// Nested codes in the expansion or not
        /// </summary>
        [FhirElement("excludeNested", InSummary=Hl7.Fhir.Model.Version.All, Order=270)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ExcludeNestedElement
        {
            get { return _ExcludeNestedElement; }
            set { _ExcludeNestedElement = value; OnPropertyChanged("ExcludeNestedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _ExcludeNestedElement;
        
        /// <summary>
        /// Nested codes in the expansion or not
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? ExcludeNested
        {
            get { return ExcludeNestedElement != null ? ExcludeNestedElement.Value : null; }
            set
            {
                if (value == null)
                    ExcludeNestedElement = null;
                else
                    ExcludeNestedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("ExcludeNested");
            }
        }
        
        /// <summary>
        /// Include or exclude codes which cannot be rendered in user interfaces in the value set expansion
        /// </summary>
        [FhirElement("excludeNotForUI", InSummary=Hl7.Fhir.Model.Version.All, Order=280)]
        [CLSCompliant(false)]
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
                if (value == null)
                    ExcludeNotForUIElement = null;
                else
                    ExcludeNotForUIElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("ExcludeNotForUI");
            }
        }
        
        /// <summary>
        /// Include or exclude codes which are post coordinated expressions in the value set expansion
        /// </summary>
        [FhirElement("excludePostCoordinated", InSummary=Hl7.Fhir.Model.Version.All, Order=290)]
        [CLSCompliant(false)]
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
                if (value == null)
                    ExcludePostCoordinatedElement = null;
                else
                    ExcludePostCoordinatedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("ExcludePostCoordinated");
            }
        }
        
        /// <summary>
        /// Specify the language for the display element of codes in the value set expansion
        /// </summary>
        [FhirElement("displayLanguage", InSummary=Hl7.Fhir.Model.Version.All, Order=300)]
        [CLSCompliant(false)]
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
        [FhirElement("limitedExpansion", InSummary=Hl7.Fhir.Model.Version.All, Order=310)]
        [CLSCompliant(false)]
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
                if (value == null)
                    LimitedExpansionElement = null;
                else
                    LimitedExpansionElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("LimitedExpansion");
            }
        }
    
    
        public static ElementDefinitionConstraint[] ExpansionProfile_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "exp-3",
                severity: ConstraintSeverity.Warning,
                expression: "designation.include.designation.all(language.exists() or use.exists())",
                human: "SHALL have at least one of language or use",
                xpath: "exists(f:language) or exists(f:use)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "exp-4",
                severity: ConstraintSeverity.Warning,
                expression: "designation.exclude.designation.all(language.exists() or use.exists())",
                human: "SHALL have at least one of language or use",
                xpath: "exists(f:language) or exists(f:use)"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(ExpansionProfile_Constraints);
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
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.PublicationStatus>)StatusElement.DeepCopy();
                if(ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.STU3.ContactDetail>(Contact.DeepCopy());
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.Markdown)DescriptionElement.DeepCopy();
                if(UseContext != null) dest.UseContext = new List<Hl7.Fhir.Model.UsageContext>(UseContext.DeepCopy());
                if(Jurisdiction != null) dest.Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(Jurisdiction.DeepCopy());
                if(FixedVersion != null) dest.FixedVersion = new List<FixedVersionComponent>(FixedVersion.DeepCopy());
                if(ExcludedSystem != null) dest.ExcludedSystem = (ExcludedSystemComponent)ExcludedSystem.DeepCopy();
                if(IncludeDesignationsElement != null) dest.IncludeDesignationsElement = (Hl7.Fhir.Model.FhirBoolean)IncludeDesignationsElement.DeepCopy();
                if(Designation != null) dest.Designation = (DesignationComponent)Designation.DeepCopy();
                if(IncludeDefinitionElement != null) dest.IncludeDefinitionElement = (Hl7.Fhir.Model.FhirBoolean)IncludeDefinitionElement.DeepCopy();
                if(ActiveOnlyElement != null) dest.ActiveOnlyElement = (Hl7.Fhir.Model.FhirBoolean)ActiveOnlyElement.DeepCopy();
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
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.Matches(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.Matches(FixedVersion, otherT.FixedVersion)) return false;
            if( !DeepComparable.Matches(ExcludedSystem, otherT.ExcludedSystem)) return false;
            if( !DeepComparable.Matches(IncludeDesignationsElement, otherT.IncludeDesignationsElement)) return false;
            if( !DeepComparable.Matches(Designation, otherT.Designation)) return false;
            if( !DeepComparable.Matches(IncludeDefinitionElement, otherT.IncludeDefinitionElement)) return false;
            if( !DeepComparable.Matches(ActiveOnlyElement, otherT.ActiveOnlyElement)) return false;
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
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.IsExactly(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.IsExactly(FixedVersion, otherT.FixedVersion)) return false;
            if( !DeepComparable.IsExactly(ExcludedSystem, otherT.ExcludedSystem)) return false;
            if( !DeepComparable.IsExactly(IncludeDesignationsElement, otherT.IncludeDesignationsElement)) return false;
            if( !DeepComparable.IsExactly(Designation, otherT.Designation)) return false;
            if( !DeepComparable.IsExactly(IncludeDefinitionElement, otherT.IncludeDefinitionElement)) return false;
            if( !DeepComparable.IsExactly(ActiveOnlyElement, otherT.ActiveOnlyElement)) return false;
            if( !DeepComparable.IsExactly(ExcludeNestedElement, otherT.ExcludeNestedElement)) return false;
            if( !DeepComparable.IsExactly(ExcludeNotForUIElement, otherT.ExcludeNotForUIElement)) return false;
            if( !DeepComparable.IsExactly(ExcludePostCoordinatedElement, otherT.ExcludePostCoordinatedElement)) return false;
            if( !DeepComparable.IsExactly(DisplayLanguageElement, otherT.DisplayLanguageElement)) return false;
            if( !DeepComparable.IsExactly(LimitedExpansionElement, otherT.LimitedExpansionElement)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("ExpansionProfile");
            base.Serialize(sink);
            sink.Element("url", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); UrlElement?.Serialize(sink);
            sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Identifier?.Serialize(sink);
            sink.Element("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VersionElement?.Serialize(sink);
            sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NameElement?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("experimental", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ExperimentalElement?.Serialize(sink);
            sink.Element("date", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DateElement?.Serialize(sink);
            sink.Element("publisher", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PublisherElement?.Serialize(sink);
            sink.BeginList("contact", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Contact)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
            sink.BeginList("useContext", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in UseContext)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("jurisdiction", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Jurisdiction)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("fixedVersion", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in FixedVersion)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("excludedSystem", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ExcludedSystem?.Serialize(sink);
            sink.Element("includeDesignations", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); IncludeDesignationsElement?.Serialize(sink);
            sink.Element("designation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Designation?.Serialize(sink);
            sink.Element("includeDefinition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); IncludeDefinitionElement?.Serialize(sink);
            sink.Element("activeOnly", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ActiveOnlyElement?.Serialize(sink);
            sink.Element("excludeNested", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ExcludeNestedElement?.Serialize(sink);
            sink.Element("excludeNotForUI", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ExcludeNotForUIElement?.Serialize(sink);
            sink.Element("excludePostCoordinated", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ExcludePostCoordinatedElement?.Serialize(sink);
            sink.Element("displayLanguage", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DisplayLanguageElement?.Serialize(sink);
            sink.Element("limitedExpansion", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); LimitedExpansionElement?.Serialize(sink);
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
                case "date":
                    DateElement = source.PopulateValue(DateElement);
                    return true;
                case "_date":
                    DateElement = source.Populate(DateElement);
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
                case "description":
                    DescriptionElement = source.PopulateValue(DescriptionElement);
                    return true;
                case "_description":
                    DescriptionElement = source.Populate(DescriptionElement);
                    return true;
                case "useContext":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "jurisdiction":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "fixedVersion":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "excludedSystem":
                    ExcludedSystem = source.Populate(ExcludedSystem);
                    return true;
                case "includeDesignations":
                    IncludeDesignationsElement = source.PopulateValue(IncludeDesignationsElement);
                    return true;
                case "_includeDesignations":
                    IncludeDesignationsElement = source.Populate(IncludeDesignationsElement);
                    return true;
                case "designation":
                    Designation = source.Populate(Designation);
                    return true;
                case "includeDefinition":
                    IncludeDefinitionElement = source.PopulateValue(IncludeDefinitionElement);
                    return true;
                case "_includeDefinition":
                    IncludeDefinitionElement = source.Populate(IncludeDefinitionElement);
                    return true;
                case "activeOnly":
                    ActiveOnlyElement = source.PopulateValue(ActiveOnlyElement);
                    return true;
                case "_activeOnly":
                    ActiveOnlyElement = source.Populate(ActiveOnlyElement);
                    return true;
                case "excludeNested":
                    ExcludeNestedElement = source.PopulateValue(ExcludeNestedElement);
                    return true;
                case "_excludeNested":
                    ExcludeNestedElement = source.Populate(ExcludeNestedElement);
                    return true;
                case "excludeNotForUI":
                    ExcludeNotForUIElement = source.PopulateValue(ExcludeNotForUIElement);
                    return true;
                case "_excludeNotForUI":
                    ExcludeNotForUIElement = source.Populate(ExcludeNotForUIElement);
                    return true;
                case "excludePostCoordinated":
                    ExcludePostCoordinatedElement = source.PopulateValue(ExcludePostCoordinatedElement);
                    return true;
                case "_excludePostCoordinated":
                    ExcludePostCoordinatedElement = source.Populate(ExcludePostCoordinatedElement);
                    return true;
                case "displayLanguage":
                    DisplayLanguageElement = source.PopulateValue(DisplayLanguageElement);
                    return true;
                case "_displayLanguage":
                    DisplayLanguageElement = source.Populate(DisplayLanguageElement);
                    return true;
                case "limitedExpansion":
                    LimitedExpansionElement = source.PopulateValue(LimitedExpansionElement);
                    return true;
                case "_limitedExpansion":
                    LimitedExpansionElement = source.Populate(LimitedExpansionElement);
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
                case "jurisdiction":
                    source.PopulateListItem(Jurisdiction, index);
                    return true;
                case "fixedVersion":
                    source.PopulateListItem(FixedVersion, index);
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
                if (DateElement != null) yield return DateElement;
                if (PublisherElement != null) yield return PublisherElement;
                foreach (var elem in Contact) { if (elem != null) yield return elem; }
                if (DescriptionElement != null) yield return DescriptionElement;
                foreach (var elem in UseContext) { if (elem != null) yield return elem; }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return elem; }
                foreach (var elem in FixedVersion) { if (elem != null) yield return elem; }
                if (ExcludedSystem != null) yield return ExcludedSystem;
                if (IncludeDesignationsElement != null) yield return IncludeDesignationsElement;
                if (Designation != null) yield return Designation;
                if (IncludeDefinitionElement != null) yield return IncludeDefinitionElement;
                if (ActiveOnlyElement != null) yield return ActiveOnlyElement;
                if (ExcludeNestedElement != null) yield return ExcludeNestedElement;
                if (ExcludeNotForUIElement != null) yield return ExcludeNotForUIElement;
                if (ExcludePostCoordinatedElement != null) yield return ExcludePostCoordinatedElement;
                if (DisplayLanguageElement != null) yield return DisplayLanguageElement;
                if (LimitedExpansionElement != null) yield return LimitedExpansionElement;
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
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (PublisherElement != null) yield return new ElementValue("publisher", PublisherElement);
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                foreach (var elem in UseContext) { if (elem != null) yield return new ElementValue("useContext", elem); }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return new ElementValue("jurisdiction", elem); }
                foreach (var elem in FixedVersion) { if (elem != null) yield return new ElementValue("fixedVersion", elem); }
                if (ExcludedSystem != null) yield return new ElementValue("excludedSystem", ExcludedSystem);
                if (IncludeDesignationsElement != null) yield return new ElementValue("includeDesignations", IncludeDesignationsElement);
                if (Designation != null) yield return new ElementValue("designation", Designation);
                if (IncludeDefinitionElement != null) yield return new ElementValue("includeDefinition", IncludeDefinitionElement);
                if (ActiveOnlyElement != null) yield return new ElementValue("activeOnly", ActiveOnlyElement);
                if (ExcludeNestedElement != null) yield return new ElementValue("excludeNested", ExcludeNestedElement);
                if (ExcludeNotForUIElement != null) yield return new ElementValue("excludeNotForUI", ExcludeNotForUIElement);
                if (ExcludePostCoordinatedElement != null) yield return new ElementValue("excludePostCoordinated", ExcludePostCoordinatedElement);
                if (DisplayLanguageElement != null) yield return new ElementValue("displayLanguage", DisplayLanguageElement);
                if (LimitedExpansionElement != null) yield return new ElementValue("limitedExpansion", LimitedExpansionElement);
            }
        }
    
    }

}
