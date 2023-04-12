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
// Generated for FHIR v4.0.1
//
namespace Hl7.Fhir.Model.R4
{
    /// <summary>
    /// Definition of a parameter to a module
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "ParameterDefinition")]
    [DataContract]
    public partial class ParameterDefinition : Hl7.Fhir.Model.Element, Hl7.Fhir.Model.IParameterDefinition, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "ParameterDefinition"; } }
    
        
        /// <summary>
        /// Name used to access the parameter value
        /// </summary>
        [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=30)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Code NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.Code _NameElement;
        
        /// <summary>
        /// Name used to access the parameter value
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
                    NameElement = new Hl7.Fhir.Model.Code(value);
                OnPropertyChanged("Name");
            }
        }
        
        /// <summary>
        /// in | out
        /// </summary>
        [FhirElement("use", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.OperationParameterUse> UseElement
        {
            get { return _UseElement; }
            set { _UseElement = value; OnPropertyChanged("UseElement"); }
        }
        
        private Code<Hl7.Fhir.Model.OperationParameterUse> _UseElement;
        
        /// <summary>
        /// in | out
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.OperationParameterUse? Use
        {
            get { return UseElement != null ? UseElement.Value : null; }
            set
            {
                if (value == null)
                    UseElement = null;
                else
                    UseElement = new Code<Hl7.Fhir.Model.OperationParameterUse>(value);
                OnPropertyChanged("Use");
            }
        }
        
        /// <summary>
        /// Minimum cardinality
        /// </summary>
        [FhirElement("min", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Integer MinElement
        {
            get { return _MinElement; }
            set { _MinElement = value; OnPropertyChanged("MinElement"); }
        }
        
        private Hl7.Fhir.Model.Integer _MinElement;
        
        /// <summary>
        /// Minimum cardinality
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? Min
        {
            get { return MinElement != null ? MinElement.Value : null; }
            set
            {
                if (value == null)
                    MinElement = null;
                else
                    MinElement = new Hl7.Fhir.Model.Integer(value);
                OnPropertyChanged("Min");
            }
        }
        
        /// <summary>
        /// Maximum cardinality (a number of *)
        /// </summary>
        [FhirElement("max", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString MaxElement
        {
            get { return _MaxElement; }
            set { _MaxElement = value; OnPropertyChanged("MaxElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _MaxElement;
        
        /// <summary>
        /// Maximum cardinality (a number of *)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Max
        {
            get { return MaxElement != null ? MaxElement.Value : null; }
            set
            {
                if (value == null)
                    MaxElement = null;
                else
                    MaxElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Max");
            }
        }
        
        /// <summary>
        /// A brief description of the parameter
        /// </summary>
        [FhirElement("documentation", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DocumentationElement
        {
            get { return _DocumentationElement; }
            set { _DocumentationElement = value; OnPropertyChanged("DocumentationElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DocumentationElement;
        
        /// <summary>
        /// A brief description of the parameter
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Documentation
        {
            get { return DocumentationElement != null ? DocumentationElement.Value : null; }
            set
            {
                if (value == null)
                    DocumentationElement = null;
                else
                    DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Documentation");
            }
        }
        
        /// <summary>
        /// What type of value
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.R4.FHIRAllTypes> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.R4.FHIRAllTypes> _TypeElement;
        
        /// <summary>
        /// What type of value
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.R4.FHIRAllTypes? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if (value == null)
                    TypeElement = null;
                else
                    TypeElement = new Code<Hl7.Fhir.Model.R4.FHIRAllTypes>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// What profile the value is expected to be
        /// </summary>
        [FhirElement("profile", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Canonical ProfileElement
        {
            get { return _ProfileElement; }
            set { _ProfileElement = value; OnPropertyChanged("ProfileElement"); }
        }
        
        private Hl7.Fhir.Model.Canonical _ProfileElement;
        
        /// <summary>
        /// What profile the value is expected to be
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Profile
        {
            get { return ProfileElement != null ? ProfileElement.Value : null; }
            set
            {
                if (value == null)
                    ProfileElement = null;
                else
                    ProfileElement = new Hl7.Fhir.Model.Canonical(value);
                OnPropertyChanged("Profile");
            }
        }
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ParameterDefinition;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.Code)NameElement.DeepCopy();
                if(UseElement != null) dest.UseElement = (Code<Hl7.Fhir.Model.OperationParameterUse>)UseElement.DeepCopy();
                if(MinElement != null) dest.MinElement = (Hl7.Fhir.Model.Integer)MinElement.DeepCopy();
                if(MaxElement != null) dest.MaxElement = (Hl7.Fhir.Model.FhirString)MaxElement.DeepCopy();
                if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.R4.FHIRAllTypes>)TypeElement.DeepCopy();
                if(ProfileElement != null) dest.ProfileElement = (Hl7.Fhir.Model.Canonical)ProfileElement.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new ParameterDefinition());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ParameterDefinition;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(UseElement, otherT.UseElement)) return false;
            if( !DeepComparable.Matches(MinElement, otherT.MinElement)) return false;
            if( !DeepComparable.Matches(MaxElement, otherT.MaxElement)) return false;
            if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(ProfileElement, otherT.ProfileElement)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ParameterDefinition;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(UseElement, otherT.UseElement)) return false;
            if( !DeepComparable.IsExactly(MinElement, otherT.MinElement)) return false;
            if( !DeepComparable.IsExactly(MaxElement, otherT.MaxElement)) return false;
            if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(ProfileElement, otherT.ProfileElement)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginDataType("ParameterDefinition");
            base.Serialize(sink);
            sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NameElement?.Serialize(sink);
            sink.Element("use", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); UseElement?.Serialize(sink);
            sink.Element("min", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); MinElement?.Serialize(sink);
            sink.Element("max", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); MaxElement?.Serialize(sink);
            sink.Element("documentation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DocumentationElement?.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); TypeElement?.Serialize(sink);
            sink.Element("profile", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ProfileElement?.Serialize(sink);
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
                case "name":
                    NameElement = source.Get<Hl7.Fhir.Model.Code>();
                    return true;
                case "use":
                    UseElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.OperationParameterUse>>();
                    return true;
                case "min":
                    MinElement = source.Get<Hl7.Fhir.Model.Integer>();
                    return true;
                case "max":
                    MaxElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "documentation":
                    DocumentationElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "type":
                    TypeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.FHIRAllTypes>>();
                    return true;
                case "profile":
                    ProfileElement = source.Get<Hl7.Fhir.Model.Canonical>();
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
                case "name":
                    NameElement = source.PopulateValue(NameElement);
                    return true;
                case "_name":
                    NameElement = source.Populate(NameElement);
                    return true;
                case "use":
                    UseElement = source.PopulateValue(UseElement);
                    return true;
                case "_use":
                    UseElement = source.Populate(UseElement);
                    return true;
                case "min":
                    MinElement = source.PopulateValue(MinElement);
                    return true;
                case "_min":
                    MinElement = source.Populate(MinElement);
                    return true;
                case "max":
                    MaxElement = source.PopulateValue(MaxElement);
                    return true;
                case "_max":
                    MaxElement = source.Populate(MaxElement);
                    return true;
                case "documentation":
                    DocumentationElement = source.PopulateValue(DocumentationElement);
                    return true;
                case "_documentation":
                    DocumentationElement = source.Populate(DocumentationElement);
                    return true;
                case "type":
                    TypeElement = source.PopulateValue(TypeElement);
                    return true;
                case "_type":
                    TypeElement = source.Populate(TypeElement);
                    return true;
                case "profile":
                    ProfileElement = source.PopulateValue(ProfileElement);
                    return true;
                case "_profile":
                    ProfileElement = source.Populate(ProfileElement);
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
                if (NameElement != null) yield return NameElement;
                if (UseElement != null) yield return UseElement;
                if (MinElement != null) yield return MinElement;
                if (MaxElement != null) yield return MaxElement;
                if (DocumentationElement != null) yield return DocumentationElement;
                if (TypeElement != null) yield return TypeElement;
                if (ProfileElement != null) yield return ProfileElement;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (UseElement != null) yield return new ElementValue("use", UseElement);
                if (MinElement != null) yield return new ElementValue("min", MinElement);
                if (MaxElement != null) yield return new ElementValue("max", MaxElement);
                if (DocumentationElement != null) yield return new ElementValue("documentation", DocumentationElement);
                if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                if (ProfileElement != null) yield return new ElementValue("profile", ProfileElement);
            }
        }
    
    }

}
