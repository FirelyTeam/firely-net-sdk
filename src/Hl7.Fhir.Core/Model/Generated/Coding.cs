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
    /// A reference to a code defined by a terminology system
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.All, "Coding")]
    [DataContract]
    public partial class Coding : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "Coding"; } }
    
        
        /// <summary>
        /// Identity of the terminology system
        /// </summary>
        [FhirElement("system", InSummary=Hl7.Fhir.Model.Version.All, Order=30)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri SystemElement
        {
            get { return _SystemElement; }
            set { _SystemElement = value; OnPropertyChanged("SystemElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _SystemElement;
        
        /// <summary>
        /// Identity of the terminology system
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
        /// Version of the system - if relevant
        /// </summary>
        [FhirElement("version", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// Version of the system - if relevant
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
        /// Symbol in syntax defined by the system
        /// </summary>
        [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Code CodeElement
        {
            get { return _CodeElement; }
            set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
        }
        
        private Hl7.Fhir.Model.Code _CodeElement;
        
        /// <summary>
        /// Symbol in syntax defined by the system
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
        /// Representation defined by the system
        /// </summary>
        [FhirElement("display", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DisplayElement
        {
            get { return _DisplayElement; }
            set { _DisplayElement = value; OnPropertyChanged("DisplayElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DisplayElement;
        
        /// <summary>
        /// Representation defined by the system
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
        /// If this coding was chosen directly by the user
        /// </summary>
        [FhirElement("userSelected", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean UserSelectedElement
        {
            get { return _UserSelectedElement; }
            set { _UserSelectedElement = value; OnPropertyChanged("UserSelectedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _UserSelectedElement;
        
        /// <summary>
        /// If this coding was chosen directly by the user
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? UserSelected
        {
            get { return UserSelectedElement != null ? UserSelectedElement.Value : null; }
            set
            {
                if (value == null)
                    UserSelectedElement = null;
                else
                    UserSelectedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("UserSelected");
            }
        }
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Coding;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(SystemElement != null) dest.SystemElement = (Hl7.Fhir.Model.FhirUri)SystemElement.DeepCopy();
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
                if(DisplayElement != null) dest.DisplayElement = (Hl7.Fhir.Model.FhirString)DisplayElement.DeepCopy();
                if(UserSelectedElement != null) dest.UserSelectedElement = (Hl7.Fhir.Model.FhirBoolean)UserSelectedElement.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Coding());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Coding;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(SystemElement, otherT.SystemElement)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
            if( !DeepComparable.Matches(DisplayElement, otherT.DisplayElement)) return false;
            if( !DeepComparable.Matches(UserSelectedElement, otherT.UserSelectedElement)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Coding;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(SystemElement, otherT.SystemElement)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
            if( !DeepComparable.IsExactly(DisplayElement, otherT.DisplayElement)) return false;
            if( !DeepComparable.IsExactly(UserSelectedElement, otherT.UserSelectedElement)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginDataType("Coding");
            base.Serialize(sink);
            sink.Element("system", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); SystemElement?.Serialize(sink);
            sink.Element("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VersionElement?.Serialize(sink);
            sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CodeElement?.Serialize(sink);
            sink.Element("display", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DisplayElement?.Serialize(sink);
            sink.Element("userSelected", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); UserSelectedElement?.Serialize(sink);
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
                case "system":
                    SystemElement = source.Get<Hl7.Fhir.Model.FhirUri>();
                    return true;
                case "version":
                    VersionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "code":
                    CodeElement = source.Get<Hl7.Fhir.Model.Code>();
                    return true;
                case "display":
                    DisplayElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "userSelected":
                    UserSelectedElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
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
                case "userSelected":
                    UserSelectedElement = source.PopulateValue(UserSelectedElement);
                    return true;
                case "_userSelected":
                    UserSelectedElement = source.Populate(UserSelectedElement);
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
                if (SystemElement != null) yield return SystemElement;
                if (VersionElement != null) yield return VersionElement;
                if (CodeElement != null) yield return CodeElement;
                if (DisplayElement != null) yield return DisplayElement;
                if (UserSelectedElement != null) yield return UserSelectedElement;
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
                if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                if (DisplayElement != null) yield return new ElementValue("display", DisplayElement);
                if (UserSelectedElement != null) yield return new ElementValue("userSelected", UserSelectedElement);
            }
        }
    
    }

}
