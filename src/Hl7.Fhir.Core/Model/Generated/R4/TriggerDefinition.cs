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
    /// Defines an expected trigger for a module
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "TriggerDefinition")]
    [DataContract]
    public partial class TriggerDefinition : Hl7.Fhir.Model.Element, Hl7.Fhir.Model.ITriggerDefinition, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "TriggerDefinition"; } }
    
        
        /// <summary>
        /// named-event | periodic | data-changed | data-added | data-modified | data-removed | data-accessed | data-access-ended
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=30)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.R4.TriggerType> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.R4.TriggerType> _TypeElement;
        
        /// <summary>
        /// named-event | periodic | data-changed | data-added | data-modified | data-removed | data-accessed | data-access-ended
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.R4.TriggerType? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if (value == null)
                    TypeElement = null;
                else
                    TypeElement = new Code<Hl7.Fhir.Model.R4.TriggerType>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// Name or URI that identifies the event
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
        /// Name or URI that identifies the event
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
        /// Timing of the event
        /// </summary>
        [FhirElement("timing", InSummary=Hl7.Fhir.Model.Version.All, Order=50, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.R4.Timing),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime))]
        [DataMember]
        public Hl7.Fhir.Model.Element Timing
        {
            get { return _Timing; }
            set { _Timing = value; OnPropertyChanged("Timing"); }
        }
        
        private Hl7.Fhir.Model.Element _Timing;
        
        /// <summary>
        /// Triggering data of the event (multiple = 'and')
        /// </summary>
        [FhirElement("data", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.R4.DataRequirement> Data
        {
            get { if(_Data==null) _Data = new List<Hl7.Fhir.Model.R4.DataRequirement>(); return _Data; }
            set { _Data = value; OnPropertyChanged("Data"); }
        }
        
        private List<Hl7.Fhir.Model.R4.DataRequirement> _Data;
        
        /// <summary>
        /// Whether the event triggers (boolean expression)
        /// </summary>
        [FhirElement("condition", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Expression Condition
        {
            get { return _Condition; }
            set { _Condition = value; OnPropertyChanged("Condition"); }
        }
        
        private Hl7.Fhir.Model.Expression _Condition;
    
    
        public static ElementDefinitionConstraint[] TriggerDefinition_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "trd-3",
                severity: ConstraintSeverity.Warning,
                expression: "(type = 'named-event' implies name.exists()) and (type = 'periodic' implies timing.exists()) and (type.startsWith('data-') implies data.exists())",
                human: "A named event requires a name, a periodic event requires timing, and a data event requires data",
                xpath: "((not(f:type/@value = 'named-event')) or name.exists()) and (not(f:type/@value = 'periodic') or timing.exists()) and (not(starts-with(f:type/@value, 'data-')) or data.exists())"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "trd-2",
                severity: ConstraintSeverity.Warning,
                expression: "condition.exists() implies data.exists()",
                human: "A condition only if there is a data requirement",
                xpath: "not(exists(f:condition)) or exists(f:data)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "trd-1",
                severity: ConstraintSeverity.Warning,
                expression: "data.empty() or timing.empty()",
                human: "Either timing, or a data requirement, but not both",
                xpath: "not(exists(f:data)) or not(exists(*[starts-with(local-name(.), 'timing')]))"
            ),
        };
    
        // TODO: Add code to enforce the above constraints
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as TriggerDefinition;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.R4.TriggerType>)TypeElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(Timing != null) dest.Timing = (Hl7.Fhir.Model.Element)Timing.DeepCopy();
                if(Data != null) dest.Data = new List<Hl7.Fhir.Model.R4.DataRequirement>(Data.DeepCopy());
                if(Condition != null) dest.Condition = (Hl7.Fhir.Model.Expression)Condition.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new TriggerDefinition());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as TriggerDefinition;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(Timing, otherT.Timing)) return false;
            if( !DeepComparable.Matches(Data, otherT.Data)) return false;
            if( !DeepComparable.Matches(Condition, otherT.Condition)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as TriggerDefinition;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(Timing, otherT.Timing)) return false;
            if( !DeepComparable.IsExactly(Data, otherT.Data)) return false;
            if( !DeepComparable.IsExactly(Condition, otherT.Condition)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginDataType("TriggerDefinition");
            base.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); TypeElement?.Serialize(sink);
            sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NameElement?.Serialize(sink);
            sink.Element("timing", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Timing?.Serialize(sink);
            sink.BeginList("data", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Data)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("condition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Condition?.Serialize(sink);
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
                case "type":
                    TypeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.TriggerType>>();
                    return true;
                case "name":
                    NameElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "timingTiming":
                    source.CheckDuplicates<Hl7.Fhir.Model.R4.Timing>(Timing, "timing");
                    Timing = source.Get<Hl7.Fhir.Model.R4.Timing>();
                    return true;
                case "timingReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Timing, "timing");
                    Timing = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "timingDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(Timing, "timing");
                    Timing = source.Get<Hl7.Fhir.Model.Date>();
                    return true;
                case "timingDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Timing, "timing");
                    Timing = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "data":
                    Data = source.GetList<Hl7.Fhir.Model.R4.DataRequirement>();
                    return true;
                case "condition":
                    Condition = source.Get<Hl7.Fhir.Model.Expression>();
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
                case "type":
                    TypeElement = source.PopulateValue(TypeElement);
                    return true;
                case "_type":
                    TypeElement = source.Populate(TypeElement);
                    return true;
                case "name":
                    NameElement = source.PopulateValue(NameElement);
                    return true;
                case "_name":
                    NameElement = source.Populate(NameElement);
                    return true;
                case "timingTiming":
                    source.CheckDuplicates<Hl7.Fhir.Model.R4.Timing>(Timing, "timing");
                    Timing = source.Populate(Timing as Hl7.Fhir.Model.R4.Timing);
                    return true;
                case "timingReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Timing, "timing");
                    Timing = source.Populate(Timing as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "timingDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(Timing, "timing");
                    Timing = source.PopulateValue(Timing as Hl7.Fhir.Model.Date);
                    return true;
                case "_timingDate":
                    source.CheckDuplicates<Hl7.Fhir.Model.Date>(Timing, "timing");
                    Timing = source.Populate(Timing as Hl7.Fhir.Model.Date);
                    return true;
                case "timingDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Timing, "timing");
                    Timing = source.PopulateValue(Timing as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "_timingDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Timing, "timing");
                    Timing = source.Populate(Timing as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "data":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "condition":
                    Condition = source.Populate(Condition);
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
                case "data":
                    source.PopulateListItem(Data, index);
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
                if (TypeElement != null) yield return TypeElement;
                if (NameElement != null) yield return NameElement;
                if (Timing != null) yield return Timing;
                foreach (var elem in Data) { if (elem != null) yield return elem; }
                if (Condition != null) yield return Condition;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (Timing != null) yield return new ElementValue("timing", Timing);
                foreach (var elem in Data) { if (elem != null) yield return new ElementValue("data", elem); }
                if (Condition != null) yield return new ElementValue("condition", Condition);
            }
        }
    
    }

}
