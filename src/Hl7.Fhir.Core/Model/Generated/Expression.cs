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
// Generated for FHIR v4.0.1, v1.0.2, v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// An expression that can be used to generate a value
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.All, "Expression")]
    [DataContract]
    public partial class Expression : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "Expression"; } }
    
        
        /// <summary>
        /// Natural language description of the condition
        /// </summary>
        [FhirElement("description", Versions=Hl7.Fhir.Model.Version.R4, InSummary=Hl7.Fhir.Model.Version.R4, Order=30)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// Natural language description of the condition
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
        /// Short name assigned to expression for reuse
        /// </summary>
        [FhirElement("name", Versions=Hl7.Fhir.Model.Version.R4, InSummary=Hl7.Fhir.Model.Version.R4, Order=40)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Id NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.Id _NameElement;
        
        /// <summary>
        /// Short name assigned to expression for reuse
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
                    NameElement = new Hl7.Fhir.Model.Id(value);
                OnPropertyChanged("Name");
            }
        }
        
        /// <summary>
        /// text/cql | text/fhirpath | application/x-fhir-query | etc.
        /// </summary>
        [FhirElement("language", Versions=Hl7.Fhir.Model.Version.R4, InSummary=Hl7.Fhir.Model.Version.R4, Order=50)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Code LanguageElement
        {
            get { return _LanguageElement; }
            set { _LanguageElement = value; OnPropertyChanged("LanguageElement"); }
        }
        
        private Hl7.Fhir.Model.Code _LanguageElement;
        
        /// <summary>
        /// text/cql | text/fhirpath | application/x-fhir-query | etc.
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
        /// Expression in specified language
        /// </summary>
        [FhirElement("expression", Versions=Hl7.Fhir.Model.Version.R4, InSummary=Hl7.Fhir.Model.Version.R4, Order=60)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString Expression_Element
        {
            get { return _Expression_Element; }
            set { _Expression_Element = value; OnPropertyChanged("Expression_Element"); }
        }
        
        private Hl7.Fhir.Model.FhirString _Expression_Element;
        
        /// <summary>
        /// Expression in specified language
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Expression_
        {
            get { return Expression_Element != null ? Expression_Element.Value : null; }
            set
            {
                if (value == null)
                    Expression_Element = null;
                else
                    Expression_Element = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Expression_");
            }
        }
        
        /// <summary>
        /// Where the expression is found
        /// </summary>
        [FhirElement("reference", Versions=Hl7.Fhir.Model.Version.R4, InSummary=Hl7.Fhir.Model.Version.R4, Order=70)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri ReferenceElement
        {
            get { return _ReferenceElement; }
            set { _ReferenceElement = value; OnPropertyChanged("ReferenceElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _ReferenceElement;
        
        /// <summary>
        /// Where the expression is found
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Reference
        {
            get { return ReferenceElement != null ? ReferenceElement.Value : null; }
            set
            {
                if (value == null)
                    ReferenceElement = null;
                else
                    ReferenceElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Reference");
            }
        }
    
    
        public static ElementDefinitionConstraint[] Expression_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "exp-1",
                severity: ConstraintSeverity.Warning,
                expression: "expression.exists() or reference.exists()",
                human: "An expression or a reference must be provided",
                xpath: "exists(f:expression) or exists(f:reference)"
            ),
        };
    
        // TODO: Add code to enforce the above constraints
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Expression;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.Id)NameElement.DeepCopy();
                if(LanguageElement != null) dest.LanguageElement = (Hl7.Fhir.Model.Code)LanguageElement.DeepCopy();
                if(Expression_Element != null) dest.Expression_Element = (Hl7.Fhir.Model.FhirString)Expression_Element.DeepCopy();
                if(ReferenceElement != null) dest.ReferenceElement = (Hl7.Fhir.Model.FhirUri)ReferenceElement.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Expression());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Expression;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(LanguageElement, otherT.LanguageElement)) return false;
            if( !DeepComparable.Matches(Expression_Element, otherT.Expression_Element)) return false;
            if( !DeepComparable.Matches(ReferenceElement, otherT.ReferenceElement)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Expression;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(LanguageElement, otherT.LanguageElement)) return false;
            if( !DeepComparable.IsExactly(Expression_Element, otherT.Expression_Element)) return false;
            if( !DeepComparable.IsExactly(ReferenceElement, otherT.ReferenceElement)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginDataType("Expression");
            base.Serialize(sink);
            sink.Element("description", Hl7.Fhir.Model.Version.R4, Hl7.Fhir.Model.Version.R4, false, false); DescriptionElement?.Serialize(sink);
            sink.Element("name", Hl7.Fhir.Model.Version.R4, Hl7.Fhir.Model.Version.R4, false, false); NameElement?.Serialize(sink);
            sink.Element("language", Hl7.Fhir.Model.Version.R4, Hl7.Fhir.Model.Version.R4, true, false); LanguageElement?.Serialize(sink);
            sink.Element("expression", Hl7.Fhir.Model.Version.R4, Hl7.Fhir.Model.Version.R4, false, false); Expression_Element?.Serialize(sink);
            sink.Element("reference", Hl7.Fhir.Model.Version.R4, Hl7.Fhir.Model.Version.R4, false, false); ReferenceElement?.Serialize(sink);
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
                case "description" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    DescriptionElement = source.PopulateValue(DescriptionElement);
                    return true;
                case "_description" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    DescriptionElement = source.Populate(DescriptionElement);
                    return true;
                case "name" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    NameElement = source.PopulateValue(NameElement);
                    return true;
                case "_name" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    NameElement = source.Populate(NameElement);
                    return true;
                case "language" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    LanguageElement = source.PopulateValue(LanguageElement);
                    return true;
                case "_language" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    LanguageElement = source.Populate(LanguageElement);
                    return true;
                case "expression" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    Expression_Element = source.PopulateValue(Expression_Element);
                    return true;
                case "_expression" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    Expression_Element = source.Populate(Expression_Element);
                    return true;
                case "reference" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    ReferenceElement = source.PopulateValue(ReferenceElement);
                    return true;
                case "_reference" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    ReferenceElement = source.Populate(ReferenceElement);
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
                if (DescriptionElement != null) yield return DescriptionElement;
                if (NameElement != null) yield return NameElement;
                if (LanguageElement != null) yield return LanguageElement;
                if (Expression_Element != null) yield return Expression_Element;
                if (ReferenceElement != null) yield return ReferenceElement;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (LanguageElement != null) yield return new ElementValue("language", LanguageElement);
                if (Expression_Element != null) yield return new ElementValue("expression", Expression_Element);
                if (ReferenceElement != null) yield return new ElementValue("reference", ReferenceElement);
            }
        }
    
    }

}
