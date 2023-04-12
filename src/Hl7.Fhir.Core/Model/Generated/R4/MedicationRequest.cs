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
    /// Ordering of medication for patient or group
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "MedicationRequest", IsResource=true)]
    [DataContract]
    public partial class MedicationRequest : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IMedicationRequest, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.MedicationRequest; } }
        [NotMapped]
        public override string TypeName { get { return "MedicationRequest"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "DispenseRequestComponent")]
        [DataContract]
        public partial class DispenseRequestComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IMedicationRequestDispenseRequestComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DispenseRequestComponent"; } }
            
            [NotMapped]
            Hl7.Fhir.Model.IDuration Hl7.Fhir.Model.IMedicationRequestDispenseRequestComponent.ExpectedSupplyDuration { get { return ExpectedSupplyDuration; } }
            
            /// <summary>
            /// First fill details
            /// </summary>
            [FhirElement("initialFill", Order=40)]
            [DataMember]
            public InitialFillComponent InitialFill
            {
                get { return _InitialFill; }
                set { _InitialFill = value; OnPropertyChanged("InitialFill"); }
            }
            
            private InitialFillComponent _InitialFill;
            
            /// <summary>
            /// Minimum period of time between dispenses
            /// </summary>
            [FhirElement("dispenseInterval", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.R4.Duration DispenseInterval
            {
                get { return _DispenseInterval; }
                set { _DispenseInterval = value; OnPropertyChanged("DispenseInterval"); }
            }
            
            private Hl7.Fhir.Model.R4.Duration _DispenseInterval;
            
            /// <summary>
            /// Time period supply is authorized for
            /// </summary>
            [FhirElement("validityPeriod", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Period ValidityPeriod
            {
                get { return _ValidityPeriod; }
                set { _ValidityPeriod = value; OnPropertyChanged("ValidityPeriod"); }
            }
            
            private Hl7.Fhir.Model.Period _ValidityPeriod;
            
            /// <summary>
            /// Number of refills authorized
            /// </summary>
            [FhirElement("numberOfRepeatsAllowed", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.UnsignedInt NumberOfRepeatsAllowedElement
            {
                get { return _NumberOfRepeatsAllowedElement; }
                set { _NumberOfRepeatsAllowedElement = value; OnPropertyChanged("NumberOfRepeatsAllowedElement"); }
            }
            
            private Hl7.Fhir.Model.UnsignedInt _NumberOfRepeatsAllowedElement;
            
            /// <summary>
            /// Number of refills authorized
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? NumberOfRepeatsAllowed
            {
                get { return NumberOfRepeatsAllowedElement != null ? NumberOfRepeatsAllowedElement.Value : null; }
                set
                {
                    if (value == null)
                        NumberOfRepeatsAllowedElement = null;
                    else
                        NumberOfRepeatsAllowedElement = new Hl7.Fhir.Model.UnsignedInt(value);
                    OnPropertyChanged("NumberOfRepeatsAllowed");
                }
            }
            
            /// <summary>
            /// Amount of medication to supply per dispense
            /// </summary>
            [FhirElement("quantity", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Quantity;
            
            /// <summary>
            /// Number of days supply per dispense
            /// </summary>
            [FhirElement("expectedSupplyDuration", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.R4.Duration ExpectedSupplyDuration
            {
                get { return _ExpectedSupplyDuration; }
                set { _ExpectedSupplyDuration = value; OnPropertyChanged("ExpectedSupplyDuration"); }
            }
            
            private Hl7.Fhir.Model.R4.Duration _ExpectedSupplyDuration;
            
            /// <summary>
            /// Intended dispenser
            /// </summary>
            [FhirElement("performer", Order=100)]
            [CLSCompliant(false)]
            [References("Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Performer
            {
                get { return _Performer; }
                set { _Performer = value; OnPropertyChanged("Performer"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Performer;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("DispenseRequestComponent");
                base.Serialize(sink);
                sink.Element("initialFill", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); InitialFill?.Serialize(sink);
                sink.Element("dispenseInterval", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DispenseInterval?.Serialize(sink);
                sink.Element("validityPeriod", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ValidityPeriod?.Serialize(sink);
                sink.Element("numberOfRepeatsAllowed", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); NumberOfRepeatsAllowedElement?.Serialize(sink);
                sink.Element("quantity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Quantity?.Serialize(sink);
                sink.Element("expectedSupplyDuration", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ExpectedSupplyDuration?.Serialize(sink);
                sink.Element("performer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Performer?.Serialize(sink);
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
                    case "initialFill":
                        InitialFill = source.Get<InitialFillComponent>();
                        return true;
                    case "dispenseInterval":
                        DispenseInterval = source.Get<Hl7.Fhir.Model.R4.Duration>();
                        return true;
                    case "validityPeriod":
                        ValidityPeriod = source.Get<Hl7.Fhir.Model.Period>();
                        return true;
                    case "numberOfRepeatsAllowed":
                        NumberOfRepeatsAllowedElement = source.Get<Hl7.Fhir.Model.UnsignedInt>();
                        return true;
                    case "quantity":
                        Quantity = source.Get<Hl7.Fhir.Model.SimpleQuantity>();
                        return true;
                    case "expectedSupplyDuration":
                        ExpectedSupplyDuration = source.Get<Hl7.Fhir.Model.R4.Duration>();
                        return true;
                    case "performer":
                        Performer = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                    case "initialFill":
                        InitialFill = source.Populate(InitialFill);
                        return true;
                    case "dispenseInterval":
                        DispenseInterval = source.Populate(DispenseInterval);
                        return true;
                    case "validityPeriod":
                        ValidityPeriod = source.Populate(ValidityPeriod);
                        return true;
                    case "numberOfRepeatsAllowed":
                        NumberOfRepeatsAllowedElement = source.PopulateValue(NumberOfRepeatsAllowedElement);
                        return true;
                    case "_numberOfRepeatsAllowed":
                        NumberOfRepeatsAllowedElement = source.Populate(NumberOfRepeatsAllowedElement);
                        return true;
                    case "quantity":
                        Quantity = source.Populate(Quantity);
                        return true;
                    case "expectedSupplyDuration":
                        ExpectedSupplyDuration = source.Populate(ExpectedSupplyDuration);
                        return true;
                    case "performer":
                        Performer = source.Populate(Performer);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DispenseRequestComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(InitialFill != null) dest.InitialFill = (InitialFillComponent)InitialFill.DeepCopy();
                    if(DispenseInterval != null) dest.DispenseInterval = (Hl7.Fhir.Model.R4.Duration)DispenseInterval.DeepCopy();
                    if(ValidityPeriod != null) dest.ValidityPeriod = (Hl7.Fhir.Model.Period)ValidityPeriod.DeepCopy();
                    if(NumberOfRepeatsAllowedElement != null) dest.NumberOfRepeatsAllowedElement = (Hl7.Fhir.Model.UnsignedInt)NumberOfRepeatsAllowedElement.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(ExpectedSupplyDuration != null) dest.ExpectedSupplyDuration = (Hl7.Fhir.Model.R4.Duration)ExpectedSupplyDuration.DeepCopy();
                    if(Performer != null) dest.Performer = (Hl7.Fhir.Model.ResourceReference)Performer.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new DispenseRequestComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DispenseRequestComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(InitialFill, otherT.InitialFill)) return false;
                if( !DeepComparable.Matches(DispenseInterval, otherT.DispenseInterval)) return false;
                if( !DeepComparable.Matches(ValidityPeriod, otherT.ValidityPeriod)) return false;
                if( !DeepComparable.Matches(NumberOfRepeatsAllowedElement, otherT.NumberOfRepeatsAllowedElement)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(ExpectedSupplyDuration, otherT.ExpectedSupplyDuration)) return false;
                if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DispenseRequestComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(InitialFill, otherT.InitialFill)) return false;
                if( !DeepComparable.IsExactly(DispenseInterval, otherT.DispenseInterval)) return false;
                if( !DeepComparable.IsExactly(ValidityPeriod, otherT.ValidityPeriod)) return false;
                if( !DeepComparable.IsExactly(NumberOfRepeatsAllowedElement, otherT.NumberOfRepeatsAllowedElement)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(ExpectedSupplyDuration, otherT.ExpectedSupplyDuration)) return false;
                if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (InitialFill != null) yield return InitialFill;
                    if (DispenseInterval != null) yield return DispenseInterval;
                    if (ValidityPeriod != null) yield return ValidityPeriod;
                    if (NumberOfRepeatsAllowedElement != null) yield return NumberOfRepeatsAllowedElement;
                    if (Quantity != null) yield return Quantity;
                    if (ExpectedSupplyDuration != null) yield return ExpectedSupplyDuration;
                    if (Performer != null) yield return Performer;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (InitialFill != null) yield return new ElementValue("initialFill", InitialFill);
                    if (DispenseInterval != null) yield return new ElementValue("dispenseInterval", DispenseInterval);
                    if (ValidityPeriod != null) yield return new ElementValue("validityPeriod", ValidityPeriod);
                    if (NumberOfRepeatsAllowedElement != null) yield return new ElementValue("numberOfRepeatsAllowed", NumberOfRepeatsAllowedElement);
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    if (ExpectedSupplyDuration != null) yield return new ElementValue("expectedSupplyDuration", ExpectedSupplyDuration);
                    if (Performer != null) yield return new ElementValue("performer", Performer);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "InitialFillComponent")]
        [DataContract]
        public partial class InitialFillComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "InitialFillComponent"; } }
            
            /// <summary>
            /// First fill quantity
            /// </summary>
            [FhirElement("quantity", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Quantity;
            
            /// <summary>
            /// First fill duration
            /// </summary>
            [FhirElement("duration", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.R4.Duration Duration
            {
                get { return _Duration; }
                set { _Duration = value; OnPropertyChanged("Duration"); }
            }
            
            private Hl7.Fhir.Model.R4.Duration _Duration;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("InitialFillComponent");
                base.Serialize(sink);
                sink.Element("quantity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Quantity?.Serialize(sink);
                sink.Element("duration", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Duration?.Serialize(sink);
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
                    case "quantity":
                        Quantity = source.Get<Hl7.Fhir.Model.SimpleQuantity>();
                        return true;
                    case "duration":
                        Duration = source.Get<Hl7.Fhir.Model.R4.Duration>();
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
                    case "quantity":
                        Quantity = source.Populate(Quantity);
                        return true;
                    case "duration":
                        Duration = source.Populate(Duration);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as InitialFillComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(Duration != null) dest.Duration = (Hl7.Fhir.Model.R4.Duration)Duration.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new InitialFillComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as InitialFillComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(Duration, otherT.Duration)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as InitialFillComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(Duration, otherT.Duration)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Quantity != null) yield return Quantity;
                    if (Duration != null) yield return Duration;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    if (Duration != null) yield return new ElementValue("duration", Duration);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "SubstitutionComponent")]
        [DataContract]
        public partial class SubstitutionComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IMedicationRequestSubstitutionComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SubstitutionComponent"; } }
            
            /// <summary>
            /// Whether substitution is allowed or not
            /// </summary>
            [FhirElement("allowed", Order=40, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.CodeableConcept))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Allowed
            {
                get { return _Allowed; }
                set { _Allowed = value; OnPropertyChanged("Allowed"); }
            }
            
            private Hl7.Fhir.Model.Element _Allowed;
            
            /// <summary>
            /// Why should (not) substitution be made
            /// </summary>
            [FhirElement("reason", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Reason
            {
                get { return _Reason; }
                set { _Reason = value; OnPropertyChanged("Reason"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Reason;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SubstitutionComponent");
                base.Serialize(sink);
                sink.Element("allowed", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, true); Allowed?.Serialize(sink);
                sink.Element("reason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Reason?.Serialize(sink);
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
                    case "allowedBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Allowed, "allowed");
                        Allowed = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "allowedCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Allowed, "allowed");
                        Allowed = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "reason":
                        Reason = source.Get<Hl7.Fhir.Model.CodeableConcept>();
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
                    case "allowedBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Allowed, "allowed");
                        Allowed = source.PopulateValue(Allowed as Hl7.Fhir.Model.FhirBoolean);
                        return true;
                    case "_allowedBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Allowed, "allowed");
                        Allowed = source.Populate(Allowed as Hl7.Fhir.Model.FhirBoolean);
                        return true;
                    case "allowedCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Allowed, "allowed");
                        Allowed = source.Populate(Allowed as Hl7.Fhir.Model.CodeableConcept);
                        return true;
                    case "reason":
                        Reason = source.Populate(Reason);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SubstitutionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Allowed != null) dest.Allowed = (Hl7.Fhir.Model.Element)Allowed.DeepCopy();
                    if(Reason != null) dest.Reason = (Hl7.Fhir.Model.CodeableConcept)Reason.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SubstitutionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SubstitutionComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Allowed, otherT.Allowed)) return false;
                if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SubstitutionComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Allowed, otherT.Allowed)) return false;
                if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Allowed != null) yield return Allowed;
                    if (Reason != null) yield return Reason;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Allowed != null) yield return new ElementValue("allowed", Allowed);
                    if (Reason != null) yield return new ElementValue("reason", Reason);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IDosage> Hl7.Fhir.Model.IMedicationRequest.DosageInstruction { get { return DosageInstruction; } }
        
        [NotMapped]
        Hl7.Fhir.Model.IMedicationRequestDispenseRequestComponent Hl7.Fhir.Model.IMedicationRequest.DispenseRequest { get { return DispenseRequest; } }
        
        [NotMapped]
        Hl7.Fhir.Model.IMedicationRequestSubstitutionComponent Hl7.Fhir.Model.IMedicationRequest.Substitution { get { return Substitution; } }
    
        
        /// <summary>
        /// External ids for this request
        /// </summary>
        [FhirElement("identifier", Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// active | on-hold | cancelled | completed | entered-in-error | stopped | draft | unknown
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.R4.medicationrequestStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.R4.medicationrequestStatus> _StatusElement;
        
        /// <summary>
        /// active | on-hold | cancelled | completed | entered-in-error | stopped | draft | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.R4.medicationrequestStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.R4.medicationrequestStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Reason for current status
        /// </summary>
        [FhirElement("statusReason", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept StatusReason
        {
            get { return _StatusReason; }
            set { _StatusReason = value; OnPropertyChanged("StatusReason"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _StatusReason;
        
        /// <summary>
        /// proposal | plan | order | original-order | reflex-order | filler-order | instance-order | option
        /// </summary>
        [FhirElement("intent", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.R4.medicationRequestIntent> IntentElement
        {
            get { return _IntentElement; }
            set { _IntentElement = value; OnPropertyChanged("IntentElement"); }
        }
        
        private Code<Hl7.Fhir.Model.R4.medicationRequestIntent> _IntentElement;
        
        /// <summary>
        /// proposal | plan | order | original-order | reflex-order | filler-order | instance-order | option
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.R4.medicationRequestIntent? Intent
        {
            get { return IntentElement != null ? IntentElement.Value : null; }
            set
            {
                if (value == null)
                    IntentElement = null;
                else
                    IntentElement = new Code<Hl7.Fhir.Model.R4.medicationRequestIntent>(value);
                OnPropertyChanged("Intent");
            }
        }
        
        /// <summary>
        /// Type of medication usage
        /// </summary>
        [FhirElement("category", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Category
        {
            get { if(_Category==null) _Category = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Category;
        
        /// <summary>
        /// routine | urgent | asap | stat
        /// </summary>
        [FhirElement("priority", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.RequestPriority> PriorityElement
        {
            get { return _PriorityElement; }
            set { _PriorityElement = value; OnPropertyChanged("PriorityElement"); }
        }
        
        private Code<Hl7.Fhir.Model.RequestPriority> _PriorityElement;
        
        /// <summary>
        /// routine | urgent | asap | stat
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.RequestPriority? Priority
        {
            get { return PriorityElement != null ? PriorityElement.Value : null; }
            set
            {
                if (value == null)
                    PriorityElement = null;
                else
                    PriorityElement = new Code<Hl7.Fhir.Model.RequestPriority>(value);
                OnPropertyChanged("Priority");
            }
        }
        
        /// <summary>
        /// True if request is prohibiting action
        /// </summary>
        [FhirElement("doNotPerform", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean DoNotPerformElement
        {
            get { return _DoNotPerformElement; }
            set { _DoNotPerformElement = value; OnPropertyChanged("DoNotPerformElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _DoNotPerformElement;
        
        /// <summary>
        /// True if request is prohibiting action
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? DoNotPerform
        {
            get { return DoNotPerformElement != null ? DoNotPerformElement.Value : null; }
            set
            {
                if (value == null)
                    DoNotPerformElement = null;
                else
                    DoNotPerformElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("DoNotPerform");
            }
        }
        
        /// <summary>
        /// Reported rather than primary record
        /// </summary>
        [FhirElement("reported", InSummary=Hl7.Fhir.Model.Version.All, Order=160, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Reported
        {
            get { return _Reported; }
            set { _Reported = value; OnPropertyChanged("Reported"); }
        }
        
        private Hl7.Fhir.Model.Element _Reported;
        
        /// <summary>
        /// Medication to be taken
        /// </summary>
        [FhirElement("medication", InSummary=Hl7.Fhir.Model.Version.All, Order=170, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Element Medication
        {
            get { return _Medication; }
            set { _Medication = value; OnPropertyChanged("Medication"); }
        }
        
        private Hl7.Fhir.Model.Element _Medication;
        
        /// <summary>
        /// Who or group medication request is for
        /// </summary>
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [References("Patient","Group")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Encounter created as part of encounter/admission/stay
        /// </summary>
        [FhirElement("encounter", Order=190)]
        [CLSCompliant(false)]
        [References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter
        {
            get { return _Encounter; }
            set { _Encounter = value; OnPropertyChanged("Encounter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Encounter;
        
        /// <summary>
        /// Information to support ordering of the medication
        /// </summary>
        [FhirElement("supportingInformation", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> SupportingInformation
        {
            get { if(_SupportingInformation==null) _SupportingInformation = new List<Hl7.Fhir.Model.ResourceReference>(); return _SupportingInformation; }
            set { _SupportingInformation = value; OnPropertyChanged("SupportingInformation"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _SupportingInformation;
        
        /// <summary>
        /// When request was initially authored
        /// </summary>
        [FhirElement("authoredOn", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime AuthoredOnElement
        {
            get { return _AuthoredOnElement; }
            set { _AuthoredOnElement = value; OnPropertyChanged("AuthoredOnElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _AuthoredOnElement;
        
        /// <summary>
        /// When request was initially authored
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string AuthoredOn
        {
            get { return AuthoredOnElement != null ? AuthoredOnElement.Value : null; }
            set
            {
                if (value == null)
                    AuthoredOnElement = null;
                else
                    AuthoredOnElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("AuthoredOn");
            }
        }
        
        /// <summary>
        /// Who/What requested the Request
        /// </summary>
        [FhirElement("requester", InSummary=Hl7.Fhir.Model.Version.All, Order=220)]
        [CLSCompliant(false)]
        [References("Practitioner","PractitionerRole","Organization","Patient","RelatedPerson","Device")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Requester
        {
            get { return _Requester; }
            set { _Requester = value; OnPropertyChanged("Requester"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Requester;
        
        /// <summary>
        /// Intended performer of administration
        /// </summary>
        [FhirElement("performer", Order=230)]
        [CLSCompliant(false)]
        [References("Practitioner","PractitionerRole","Organization","Patient","Device","RelatedPerson","CareTeam")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Performer
        {
            get { return _Performer; }
            set { _Performer = value; OnPropertyChanged("Performer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Performer;
        
        /// <summary>
        /// Desired kind of performer of the medication administration
        /// </summary>
        [FhirElement("performerType", InSummary=Hl7.Fhir.Model.Version.All, Order=240)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept PerformerType
        {
            get { return _PerformerType; }
            set { _PerformerType = value; OnPropertyChanged("PerformerType"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _PerformerType;
        
        /// <summary>
        /// Person who entered the request
        /// </summary>
        [FhirElement("recorder", Order=250)]
        [CLSCompliant(false)]
        [References("Practitioner","PractitionerRole")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Recorder
        {
            get { return _Recorder; }
            set { _Recorder = value; OnPropertyChanged("Recorder"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Recorder;
        
        /// <summary>
        /// Reason or indication for ordering or not ordering the medication
        /// </summary>
        [FhirElement("reasonCode", Order=260)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReasonCode
        {
            get { if(_ReasonCode==null) _ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReasonCode; }
            set { _ReasonCode = value; OnPropertyChanged("ReasonCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ReasonCode;
        
        /// <summary>
        /// Condition or observation that supports why the prescription is being written
        /// </summary>
        [FhirElement("reasonReference", Order=270)]
        [CLSCompliant(false)]
        [References("Condition","Observation")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ReasonReference
        {
            get { if(_ReasonReference==null) _ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(); return _ReasonReference; }
            set { _ReasonReference = value; OnPropertyChanged("ReasonReference"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _ReasonReference;
        
        /// <summary>
        /// Instantiates FHIR protocol or definition
        /// </summary>
        [FhirElement("instantiatesCanonical", InSummary=Hl7.Fhir.Model.Version.All, Order=280)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Canonical> InstantiatesCanonicalElement
        {
            get { if(_InstantiatesCanonicalElement==null) _InstantiatesCanonicalElement = new List<Hl7.Fhir.Model.Canonical>(); return _InstantiatesCanonicalElement; }
            set { _InstantiatesCanonicalElement = value; OnPropertyChanged("InstantiatesCanonicalElement"); }
        }
        
        private List<Hl7.Fhir.Model.Canonical> _InstantiatesCanonicalElement;
        
        /// <summary>
        /// Instantiates FHIR protocol or definition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> InstantiatesCanonical
        {
            get { return InstantiatesCanonicalElement != null ? InstantiatesCanonicalElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    InstantiatesCanonicalElement = null;
                else
                    InstantiatesCanonicalElement = new List<Hl7.Fhir.Model.Canonical>(value.Select(elem=>new Hl7.Fhir.Model.Canonical(elem)));
                OnPropertyChanged("InstantiatesCanonical");
            }
        }
        
        /// <summary>
        /// Instantiates external protocol or definition
        /// </summary>
        [FhirElement("instantiatesUri", InSummary=Hl7.Fhir.Model.Version.All, Order=290)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirUri> InstantiatesUriElement
        {
            get { if(_InstantiatesUriElement==null) _InstantiatesUriElement = new List<Hl7.Fhir.Model.FhirUri>(); return _InstantiatesUriElement; }
            set { _InstantiatesUriElement = value; OnPropertyChanged("InstantiatesUriElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirUri> _InstantiatesUriElement;
        
        /// <summary>
        /// Instantiates external protocol or definition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> InstantiatesUri
        {
            get { return InstantiatesUriElement != null ? InstantiatesUriElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    InstantiatesUriElement = null;
                else
                    InstantiatesUriElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                OnPropertyChanged("InstantiatesUri");
            }
        }
        
        /// <summary>
        /// What request fulfills
        /// </summary>
        [FhirElement("basedOn", InSummary=Hl7.Fhir.Model.Version.All, Order=300)]
        [CLSCompliant(false)]
        [References("CarePlan","MedicationRequest","ServiceRequest","ImmunizationRecommendation")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> BasedOn
        {
            get { if(_BasedOn==null) _BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(); return _BasedOn; }
            set { _BasedOn = value; OnPropertyChanged("BasedOn"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _BasedOn;
        
        /// <summary>
        /// Composite request this is part of
        /// </summary>
        [FhirElement("groupIdentifier", InSummary=Hl7.Fhir.Model.Version.All, Order=310)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier GroupIdentifier
        {
            get { return _GroupIdentifier; }
            set { _GroupIdentifier = value; OnPropertyChanged("GroupIdentifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _GroupIdentifier;
        
        /// <summary>
        /// Overall pattern of medication administration
        /// </summary>
        [FhirElement("courseOfTherapyType", Order=320)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept CourseOfTherapyType
        {
            get { return _CourseOfTherapyType; }
            set { _CourseOfTherapyType = value; OnPropertyChanged("CourseOfTherapyType"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _CourseOfTherapyType;
        
        /// <summary>
        /// Associated insurance coverage
        /// </summary>
        [FhirElement("insurance", Order=330)]
        [CLSCompliant(false)]
        [References("Coverage","ClaimResponse")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Insurance
        {
            get { if(_Insurance==null) _Insurance = new List<Hl7.Fhir.Model.ResourceReference>(); return _Insurance; }
            set { _Insurance = value; OnPropertyChanged("Insurance"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Insurance;
        
        /// <summary>
        /// Information about the prescription
        /// </summary>
        [FhirElement("note", Order=340)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        
        /// <summary>
        /// How the medication should be taken
        /// </summary>
        [FhirElement("dosageInstruction", Order=350)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.R4.Dosage> DosageInstruction
        {
            get { if(_DosageInstruction==null) _DosageInstruction = new List<Hl7.Fhir.Model.R4.Dosage>(); return _DosageInstruction; }
            set { _DosageInstruction = value; OnPropertyChanged("DosageInstruction"); }
        }
        
        private List<Hl7.Fhir.Model.R4.Dosage> _DosageInstruction;
        
        /// <summary>
        /// Medication supply authorization
        /// </summary>
        [FhirElement("dispenseRequest", Order=360)]
        [DataMember]
        public DispenseRequestComponent DispenseRequest
        {
            get { return _DispenseRequest; }
            set { _DispenseRequest = value; OnPropertyChanged("DispenseRequest"); }
        }
        
        private DispenseRequestComponent _DispenseRequest;
        
        /// <summary>
        /// Any restrictions on medication substitution
        /// </summary>
        [FhirElement("substitution", Order=370)]
        [DataMember]
        public SubstitutionComponent Substitution
        {
            get { return _Substitution; }
            set { _Substitution = value; OnPropertyChanged("Substitution"); }
        }
        
        private SubstitutionComponent _Substitution;
        
        /// <summary>
        /// An order/prescription that is being replaced
        /// </summary>
        [FhirElement("priorPrescription", Order=380)]
        [CLSCompliant(false)]
        [References("MedicationRequest")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference PriorPrescription
        {
            get { return _PriorPrescription; }
            set { _PriorPrescription = value; OnPropertyChanged("PriorPrescription"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _PriorPrescription;
        
        /// <summary>
        /// Clinical Issue with action
        /// </summary>
        [FhirElement("detectedIssue", Order=390)]
        [CLSCompliant(false)]
        [References("DetectedIssue")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> DetectedIssue
        {
            get { if(_DetectedIssue==null) _DetectedIssue = new List<Hl7.Fhir.Model.ResourceReference>(); return _DetectedIssue; }
            set { _DetectedIssue = value; OnPropertyChanged("DetectedIssue"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _DetectedIssue;
        
        /// <summary>
        /// A list of events of interest in the lifecycle
        /// </summary>
        [FhirElement("eventHistory", Order=400)]
        [CLSCompliant(false)]
        [References("Provenance")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> EventHistory
        {
            get { if(_EventHistory==null) _EventHistory = new List<Hl7.Fhir.Model.ResourceReference>(); return _EventHistory; }
            set { _EventHistory = value; OnPropertyChanged("EventHistory"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _EventHistory;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MedicationRequest;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.R4.medicationrequestStatus>)StatusElement.DeepCopy();
                if(StatusReason != null) dest.StatusReason = (Hl7.Fhir.Model.CodeableConcept)StatusReason.DeepCopy();
                if(IntentElement != null) dest.IntentElement = (Code<Hl7.Fhir.Model.R4.medicationRequestIntent>)IntentElement.DeepCopy();
                if(Category != null) dest.Category = new List<Hl7.Fhir.Model.CodeableConcept>(Category.DeepCopy());
                if(PriorityElement != null) dest.PriorityElement = (Code<Hl7.Fhir.Model.RequestPriority>)PriorityElement.DeepCopy();
                if(DoNotPerformElement != null) dest.DoNotPerformElement = (Hl7.Fhir.Model.FhirBoolean)DoNotPerformElement.DeepCopy();
                if(Reported != null) dest.Reported = (Hl7.Fhir.Model.Element)Reported.DeepCopy();
                if(Medication != null) dest.Medication = (Hl7.Fhir.Model.Element)Medication.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(SupportingInformation != null) dest.SupportingInformation = new List<Hl7.Fhir.Model.ResourceReference>(SupportingInformation.DeepCopy());
                if(AuthoredOnElement != null) dest.AuthoredOnElement = (Hl7.Fhir.Model.FhirDateTime)AuthoredOnElement.DeepCopy();
                if(Requester != null) dest.Requester = (Hl7.Fhir.Model.ResourceReference)Requester.DeepCopy();
                if(Performer != null) dest.Performer = (Hl7.Fhir.Model.ResourceReference)Performer.DeepCopy();
                if(PerformerType != null) dest.PerformerType = (Hl7.Fhir.Model.CodeableConcept)PerformerType.DeepCopy();
                if(Recorder != null) dest.Recorder = (Hl7.Fhir.Model.ResourceReference)Recorder.DeepCopy();
                if(ReasonCode != null) dest.ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonCode.DeepCopy());
                if(ReasonReference != null) dest.ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(ReasonReference.DeepCopy());
                if(InstantiatesCanonicalElement != null) dest.InstantiatesCanonicalElement = new List<Hl7.Fhir.Model.Canonical>(InstantiatesCanonicalElement.DeepCopy());
                if(InstantiatesUriElement != null) dest.InstantiatesUriElement = new List<Hl7.Fhir.Model.FhirUri>(InstantiatesUriElement.DeepCopy());
                if(BasedOn != null) dest.BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(BasedOn.DeepCopy());
                if(GroupIdentifier != null) dest.GroupIdentifier = (Hl7.Fhir.Model.Identifier)GroupIdentifier.DeepCopy();
                if(CourseOfTherapyType != null) dest.CourseOfTherapyType = (Hl7.Fhir.Model.CodeableConcept)CourseOfTherapyType.DeepCopy();
                if(Insurance != null) dest.Insurance = new List<Hl7.Fhir.Model.ResourceReference>(Insurance.DeepCopy());
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(DosageInstruction != null) dest.DosageInstruction = new List<Hl7.Fhir.Model.R4.Dosage>(DosageInstruction.DeepCopy());
                if(DispenseRequest != null) dest.DispenseRequest = (DispenseRequestComponent)DispenseRequest.DeepCopy();
                if(Substitution != null) dest.Substitution = (SubstitutionComponent)Substitution.DeepCopy();
                if(PriorPrescription != null) dest.PriorPrescription = (Hl7.Fhir.Model.ResourceReference)PriorPrescription.DeepCopy();
                if(DetectedIssue != null) dest.DetectedIssue = new List<Hl7.Fhir.Model.ResourceReference>(DetectedIssue.DeepCopy());
                if(EventHistory != null) dest.EventHistory = new List<Hl7.Fhir.Model.ResourceReference>(EventHistory.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new MedicationRequest());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as MedicationRequest;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(StatusReason, otherT.StatusReason)) return false;
            if( !DeepComparable.Matches(IntentElement, otherT.IntentElement)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(PriorityElement, otherT.PriorityElement)) return false;
            if( !DeepComparable.Matches(DoNotPerformElement, otherT.DoNotPerformElement)) return false;
            if( !DeepComparable.Matches(Reported, otherT.Reported)) return false;
            if( !DeepComparable.Matches(Medication, otherT.Medication)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(SupportingInformation, otherT.SupportingInformation)) return false;
            if( !DeepComparable.Matches(AuthoredOnElement, otherT.AuthoredOnElement)) return false;
            if( !DeepComparable.Matches(Requester, otherT.Requester)) return false;
            if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
            if( !DeepComparable.Matches(PerformerType, otherT.PerformerType)) return false;
            if( !DeepComparable.Matches(Recorder, otherT.Recorder)) return false;
            if( !DeepComparable.Matches(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.Matches(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.Matches(InstantiatesCanonicalElement, otherT.InstantiatesCanonicalElement)) return false;
            if( !DeepComparable.Matches(InstantiatesUriElement, otherT.InstantiatesUriElement)) return false;
            if( !DeepComparable.Matches(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.Matches(GroupIdentifier, otherT.GroupIdentifier)) return false;
            if( !DeepComparable.Matches(CourseOfTherapyType, otherT.CourseOfTherapyType)) return false;
            if( !DeepComparable.Matches(Insurance, otherT.Insurance)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(DosageInstruction, otherT.DosageInstruction)) return false;
            if( !DeepComparable.Matches(DispenseRequest, otherT.DispenseRequest)) return false;
            if( !DeepComparable.Matches(Substitution, otherT.Substitution)) return false;
            if( !DeepComparable.Matches(PriorPrescription, otherT.PriorPrescription)) return false;
            if( !DeepComparable.Matches(DetectedIssue, otherT.DetectedIssue)) return false;
            if( !DeepComparable.Matches(EventHistory, otherT.EventHistory)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MedicationRequest;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(StatusReason, otherT.StatusReason)) return false;
            if( !DeepComparable.IsExactly(IntentElement, otherT.IntentElement)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(PriorityElement, otherT.PriorityElement)) return false;
            if( !DeepComparable.IsExactly(DoNotPerformElement, otherT.DoNotPerformElement)) return false;
            if( !DeepComparable.IsExactly(Reported, otherT.Reported)) return false;
            if( !DeepComparable.IsExactly(Medication, otherT.Medication)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(SupportingInformation, otherT.SupportingInformation)) return false;
            if( !DeepComparable.IsExactly(AuthoredOnElement, otherT.AuthoredOnElement)) return false;
            if( !DeepComparable.IsExactly(Requester, otherT.Requester)) return false;
            if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
            if( !DeepComparable.IsExactly(PerformerType, otherT.PerformerType)) return false;
            if( !DeepComparable.IsExactly(Recorder, otherT.Recorder)) return false;
            if( !DeepComparable.IsExactly(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.IsExactly(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.IsExactly(InstantiatesCanonicalElement, otherT.InstantiatesCanonicalElement)) return false;
            if( !DeepComparable.IsExactly(InstantiatesUriElement, otherT.InstantiatesUriElement)) return false;
            if( !DeepComparable.IsExactly(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.IsExactly(GroupIdentifier, otherT.GroupIdentifier)) return false;
            if( !DeepComparable.IsExactly(CourseOfTherapyType, otherT.CourseOfTherapyType)) return false;
            if( !DeepComparable.IsExactly(Insurance, otherT.Insurance)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(DosageInstruction, otherT.DosageInstruction)) return false;
            if( !DeepComparable.IsExactly(DispenseRequest, otherT.DispenseRequest)) return false;
            if( !DeepComparable.IsExactly(Substitution, otherT.Substitution)) return false;
            if( !DeepComparable.IsExactly(PriorPrescription, otherT.PriorPrescription)) return false;
            if( !DeepComparable.IsExactly(DetectedIssue, otherT.DetectedIssue)) return false;
            if( !DeepComparable.IsExactly(EventHistory, otherT.EventHistory)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("MedicationRequest");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("statusReason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); StatusReason?.Serialize(sink);
            sink.Element("intent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); IntentElement?.Serialize(sink);
            sink.BeginList("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Category)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("priority", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PriorityElement?.Serialize(sink);
            sink.Element("doNotPerform", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DoNotPerformElement?.Serialize(sink);
            sink.Element("reported", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Reported?.Serialize(sink);
            sink.Element("medication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, true); Medication?.Serialize(sink);
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Subject?.Serialize(sink);
            sink.Element("encounter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Encounter?.Serialize(sink);
            sink.BeginList("supportingInformation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in SupportingInformation)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("authoredOn", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); AuthoredOnElement?.Serialize(sink);
            sink.Element("requester", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Requester?.Serialize(sink);
            sink.Element("performer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Performer?.Serialize(sink);
            sink.Element("performerType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PerformerType?.Serialize(sink);
            sink.Element("recorder", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Recorder?.Serialize(sink);
            sink.BeginList("reasonCode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in ReasonCode)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("reasonReference", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in ReasonReference)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("instantiatesCanonical", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(InstantiatesCanonicalElement);
            sink.End();
            sink.BeginList("instantiatesUri", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(InstantiatesUriElement);
            sink.End();
            sink.BeginList("basedOn", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in BasedOn)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("groupIdentifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); GroupIdentifier?.Serialize(sink);
            sink.Element("courseOfTherapyType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CourseOfTherapyType?.Serialize(sink);
            sink.BeginList("insurance", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Insurance)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("note", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Note)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("dosageInstruction", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in DosageInstruction)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("dispenseRequest", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DispenseRequest?.Serialize(sink);
            sink.Element("substitution", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Substitution?.Serialize(sink);
            sink.Element("priorPrescription", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PriorPrescription?.Serialize(sink);
            sink.BeginList("detectedIssue", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in DetectedIssue)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("eventHistory", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in EventHistory)
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
                case "identifier":
                    Identifier = source.GetList<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.medicationrequestStatus>>();
                    return true;
                case "statusReason":
                    StatusReason = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "intent":
                    IntentElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.medicationRequestIntent>>();
                    return true;
                case "category":
                    Category = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "priority":
                    PriorityElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.RequestPriority>>();
                    return true;
                case "doNotPerform":
                    DoNotPerformElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "reportedBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Reported, "reported");
                    Reported = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "reportedReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Reported, "reported");
                    Reported = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "medicationCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Medication, "medication");
                    Medication = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "medicationReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Medication, "medication");
                    Medication = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "subject":
                    Subject = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "encounter":
                    Encounter = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "supportingInformation":
                    SupportingInformation = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "authoredOn":
                    AuthoredOnElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "requester":
                    Requester = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "performer":
                    Performer = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "performerType":
                    PerformerType = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "recorder":
                    Recorder = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "reasonCode":
                    ReasonCode = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "reasonReference":
                    ReasonReference = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "instantiatesCanonical":
                    InstantiatesCanonicalElement = source.GetList<Hl7.Fhir.Model.Canonical>();
                    return true;
                case "instantiatesUri":
                    InstantiatesUriElement = source.GetList<Hl7.Fhir.Model.FhirUri>();
                    return true;
                case "basedOn":
                    BasedOn = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "groupIdentifier":
                    GroupIdentifier = source.Get<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "courseOfTherapyType":
                    CourseOfTherapyType = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "insurance":
                    Insurance = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "note":
                    Note = source.GetList<Hl7.Fhir.Model.Annotation>();
                    return true;
                case "dosageInstruction":
                    DosageInstruction = source.GetList<Hl7.Fhir.Model.R4.Dosage>();
                    return true;
                case "dispenseRequest":
                    DispenseRequest = source.Get<DispenseRequestComponent>();
                    return true;
                case "substitution":
                    Substitution = source.Get<SubstitutionComponent>();
                    return true;
                case "priorPrescription":
                    PriorPrescription = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "detectedIssue":
                    DetectedIssue = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "eventHistory":
                    EventHistory = source.GetList<Hl7.Fhir.Model.ResourceReference>();
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
                case "identifier":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "statusReason":
                    StatusReason = source.Populate(StatusReason);
                    return true;
                case "intent":
                    IntentElement = source.PopulateValue(IntentElement);
                    return true;
                case "_intent":
                    IntentElement = source.Populate(IntentElement);
                    return true;
                case "category":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "priority":
                    PriorityElement = source.PopulateValue(PriorityElement);
                    return true;
                case "_priority":
                    PriorityElement = source.Populate(PriorityElement);
                    return true;
                case "doNotPerform":
                    DoNotPerformElement = source.PopulateValue(DoNotPerformElement);
                    return true;
                case "_doNotPerform":
                    DoNotPerformElement = source.Populate(DoNotPerformElement);
                    return true;
                case "reportedBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Reported, "reported");
                    Reported = source.PopulateValue(Reported as Hl7.Fhir.Model.FhirBoolean);
                    return true;
                case "_reportedBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Reported, "reported");
                    Reported = source.Populate(Reported as Hl7.Fhir.Model.FhirBoolean);
                    return true;
                case "reportedReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Reported, "reported");
                    Reported = source.Populate(Reported as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "medicationCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Medication, "medication");
                    Medication = source.Populate(Medication as Hl7.Fhir.Model.CodeableConcept);
                    return true;
                case "medicationReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Medication, "medication");
                    Medication = source.Populate(Medication as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "subject":
                    Subject = source.Populate(Subject);
                    return true;
                case "encounter":
                    Encounter = source.Populate(Encounter);
                    return true;
                case "supportingInformation":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "authoredOn":
                    AuthoredOnElement = source.PopulateValue(AuthoredOnElement);
                    return true;
                case "_authoredOn":
                    AuthoredOnElement = source.Populate(AuthoredOnElement);
                    return true;
                case "requester":
                    Requester = source.Populate(Requester);
                    return true;
                case "performer":
                    Performer = source.Populate(Performer);
                    return true;
                case "performerType":
                    PerformerType = source.Populate(PerformerType);
                    return true;
                case "recorder":
                    Recorder = source.Populate(Recorder);
                    return true;
                case "reasonCode":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "reasonReference":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "instantiatesCanonical":
                case "_instantiatesCanonical":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "instantiatesUri":
                case "_instantiatesUri":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "basedOn":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "groupIdentifier":
                    GroupIdentifier = source.Populate(GroupIdentifier);
                    return true;
                case "courseOfTherapyType":
                    CourseOfTherapyType = source.Populate(CourseOfTherapyType);
                    return true;
                case "insurance":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "note":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "dosageInstruction":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "dispenseRequest":
                    DispenseRequest = source.Populate(DispenseRequest);
                    return true;
                case "substitution":
                    Substitution = source.Populate(Substitution);
                    return true;
                case "priorPrescription":
                    PriorPrescription = source.Populate(PriorPrescription);
                    return true;
                case "detectedIssue":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "eventHistory":
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
                case "identifier":
                    source.PopulateListItem(Identifier, index);
                    return true;
                case "category":
                    source.PopulateListItem(Category, index);
                    return true;
                case "supportingInformation":
                    source.PopulateListItem(SupportingInformation, index);
                    return true;
                case "reasonCode":
                    source.PopulateListItem(ReasonCode, index);
                    return true;
                case "reasonReference":
                    source.PopulateListItem(ReasonReference, index);
                    return true;
                case "instantiatesCanonical":
                    source.PopulatePrimitiveListItemValue(InstantiatesCanonicalElement, index);
                    return true;
                case "_instantiatesCanonical":
                    source.PopulatePrimitiveListItem(InstantiatesCanonicalElement, index);
                    return true;
                case "instantiatesUri":
                    source.PopulatePrimitiveListItemValue(InstantiatesUriElement, index);
                    return true;
                case "_instantiatesUri":
                    source.PopulatePrimitiveListItem(InstantiatesUriElement, index);
                    return true;
                case "basedOn":
                    source.PopulateListItem(BasedOn, index);
                    return true;
                case "insurance":
                    source.PopulateListItem(Insurance, index);
                    return true;
                case "note":
                    source.PopulateListItem(Note, index);
                    return true;
                case "dosageInstruction":
                    source.PopulateListItem(DosageInstruction, index);
                    return true;
                case "detectedIssue":
                    source.PopulateListItem(DetectedIssue, index);
                    return true;
                case "eventHistory":
                    source.PopulateListItem(EventHistory, index);
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
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                if (StatusElement != null) yield return StatusElement;
                if (StatusReason != null) yield return StatusReason;
                if (IntentElement != null) yield return IntentElement;
                foreach (var elem in Category) { if (elem != null) yield return elem; }
                if (PriorityElement != null) yield return PriorityElement;
                if (DoNotPerformElement != null) yield return DoNotPerformElement;
                if (Reported != null) yield return Reported;
                if (Medication != null) yield return Medication;
                if (Subject != null) yield return Subject;
                if (Encounter != null) yield return Encounter;
                foreach (var elem in SupportingInformation) { if (elem != null) yield return elem; }
                if (AuthoredOnElement != null) yield return AuthoredOnElement;
                if (Requester != null) yield return Requester;
                if (Performer != null) yield return Performer;
                if (PerformerType != null) yield return PerformerType;
                if (Recorder != null) yield return Recorder;
                foreach (var elem in ReasonCode) { if (elem != null) yield return elem; }
                foreach (var elem in ReasonReference) { if (elem != null) yield return elem; }
                foreach (var elem in InstantiatesCanonicalElement) { if (elem != null) yield return elem; }
                foreach (var elem in InstantiatesUriElement) { if (elem != null) yield return elem; }
                foreach (var elem in BasedOn) { if (elem != null) yield return elem; }
                if (GroupIdentifier != null) yield return GroupIdentifier;
                if (CourseOfTherapyType != null) yield return CourseOfTherapyType;
                foreach (var elem in Insurance) { if (elem != null) yield return elem; }
                foreach (var elem in Note) { if (elem != null) yield return elem; }
                foreach (var elem in DosageInstruction) { if (elem != null) yield return elem; }
                if (DispenseRequest != null) yield return DispenseRequest;
                if (Substitution != null) yield return Substitution;
                if (PriorPrescription != null) yield return PriorPrescription;
                foreach (var elem in DetectedIssue) { if (elem != null) yield return elem; }
                foreach (var elem in EventHistory) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (StatusReason != null) yield return new ElementValue("statusReason", StatusReason);
                if (IntentElement != null) yield return new ElementValue("intent", IntentElement);
                foreach (var elem in Category) { if (elem != null) yield return new ElementValue("category", elem); }
                if (PriorityElement != null) yield return new ElementValue("priority", PriorityElement);
                if (DoNotPerformElement != null) yield return new ElementValue("doNotPerform", DoNotPerformElement);
                if (Reported != null) yield return new ElementValue("reported", Reported);
                if (Medication != null) yield return new ElementValue("medication", Medication);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Encounter != null) yield return new ElementValue("encounter", Encounter);
                foreach (var elem in SupportingInformation) { if (elem != null) yield return new ElementValue("supportingInformation", elem); }
                if (AuthoredOnElement != null) yield return new ElementValue("authoredOn", AuthoredOnElement);
                if (Requester != null) yield return new ElementValue("requester", Requester);
                if (Performer != null) yield return new ElementValue("performer", Performer);
                if (PerformerType != null) yield return new ElementValue("performerType", PerformerType);
                if (Recorder != null) yield return new ElementValue("recorder", Recorder);
                foreach (var elem in ReasonCode) { if (elem != null) yield return new ElementValue("reasonCode", elem); }
                foreach (var elem in ReasonReference) { if (elem != null) yield return new ElementValue("reasonReference", elem); }
                foreach (var elem in InstantiatesCanonicalElement) { if (elem != null) yield return new ElementValue("instantiatesCanonical", elem); }
                foreach (var elem in InstantiatesUriElement) { if (elem != null) yield return new ElementValue("instantiatesUri", elem); }
                foreach (var elem in BasedOn) { if (elem != null) yield return new ElementValue("basedOn", elem); }
                if (GroupIdentifier != null) yield return new ElementValue("groupIdentifier", GroupIdentifier);
                if (CourseOfTherapyType != null) yield return new ElementValue("courseOfTherapyType", CourseOfTherapyType);
                foreach (var elem in Insurance) { if (elem != null) yield return new ElementValue("insurance", elem); }
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                foreach (var elem in DosageInstruction) { if (elem != null) yield return new ElementValue("dosageInstruction", elem); }
                if (DispenseRequest != null) yield return new ElementValue("dispenseRequest", DispenseRequest);
                if (Substitution != null) yield return new ElementValue("substitution", Substitution);
                if (PriorPrescription != null) yield return new ElementValue("priorPrescription", PriorPrescription);
                foreach (var elem in DetectedIssue) { if (elem != null) yield return new ElementValue("detectedIssue", elem); }
                foreach (var elem in EventHistory) { if (elem != null) yield return new ElementValue("eventHistory", elem); }
            }
        }
    
    }

}
