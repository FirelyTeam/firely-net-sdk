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
    /// The details of a healthcare service available at a location
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "HealthcareService", IsResource=true)]
    [DataContract]
    public partial class HealthcareService : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IHealthcareService, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.HealthcareService; } }
        [NotMapped]
        public override string TypeName { get { return "HealthcareService"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ServiceTypeComponent")]
        [DataContract]
        public partial class ServiceTypeComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ServiceTypeComponent"; } }
            
            /// <summary>
            /// Type of service delivered or performed
            /// </summary>
            [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Specialties handled by the Service Site
            /// </summary>
            [FhirElement("specialty", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Specialty
            {
                get { if(_Specialty==null) _Specialty = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Specialty; }
                set { _Specialty = value; OnPropertyChanged("Specialty"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Specialty;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ServiceTypeComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Type?.Serialize(sink);
                sink.BeginList("specialty", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Specialty)
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
                    case "type":
                        Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "specialty":
                        Specialty = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
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
                        Type = source.Populate(Type);
                        return true;
                    case "specialty":
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
                    case "specialty":
                        source.PopulateListItem(Specialty, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ServiceTypeComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Specialty != null) dest.Specialty = new List<Hl7.Fhir.Model.CodeableConcept>(Specialty.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ServiceTypeComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ServiceTypeComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Specialty, otherT.Specialty)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ServiceTypeComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Specialty, otherT.Specialty)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    foreach (var elem in Specialty) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    foreach (var elem in Specialty) { if (elem != null) yield return new ElementValue("specialty", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "AvailableTimeComponent")]
        [DataContract]
        public partial class AvailableTimeComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IHealthcareServiceAvailableTimeComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "AvailableTimeComponent"; } }
            
            /// <summary>
            /// mon | tue | wed | thu | fri | sat | sun
            /// </summary>
            [FhirElement("daysOfWeek", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Code<Hl7.Fhir.Model.DaysOfWeek>> DaysOfWeekElement
            {
                get { if(_DaysOfWeekElement==null) _DaysOfWeekElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DaysOfWeek>>(); return _DaysOfWeekElement; }
                set { _DaysOfWeekElement = value; OnPropertyChanged("DaysOfWeekElement"); }
            }
            
            private List<Code<Hl7.Fhir.Model.DaysOfWeek>> _DaysOfWeekElement;
            
            /// <summary>
            /// mon | tue | wed | thu | fri | sat | sun
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<Hl7.Fhir.Model.DaysOfWeek?> DaysOfWeek
            {
                get { return DaysOfWeekElement != null ? DaysOfWeekElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        DaysOfWeekElement = null;
                    else
                        DaysOfWeekElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DaysOfWeek>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DaysOfWeek>(elem)));
                    OnPropertyChanged("DaysOfWeek");
                }
            }
            
            /// <summary>
            /// Always available? e.g. 24 hour service
            /// </summary>
            [FhirElement("allDay", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean AllDayElement
            {
                get { return _AllDayElement; }
                set { _AllDayElement = value; OnPropertyChanged("AllDayElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _AllDayElement;
            
            /// <summary>
            /// Always available? e.g. 24 hour service
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? AllDay
            {
                get { return AllDayElement != null ? AllDayElement.Value : null; }
                set
                {
                    if (value == null)
                        AllDayElement = null;
                    else
                        AllDayElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("AllDay");
                }
            }
            
            /// <summary>
            /// Opening time of day (ignored if allDay = true)
            /// </summary>
            [FhirElement("availableStartTime", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Time AvailableStartTimeElement
            {
                get { return _AvailableStartTimeElement; }
                set { _AvailableStartTimeElement = value; OnPropertyChanged("AvailableStartTimeElement"); }
            }
            
            private Hl7.Fhir.Model.Time _AvailableStartTimeElement;
            
            /// <summary>
            /// Opening time of day (ignored if allDay = true)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string AvailableStartTime
            {
                get { return AvailableStartTimeElement != null ? AvailableStartTimeElement.Value : null; }
                set
                {
                    if (value == null)
                        AvailableStartTimeElement = null;
                    else
                        AvailableStartTimeElement = new Hl7.Fhir.Model.Time(value);
                    OnPropertyChanged("AvailableStartTime");
                }
            }
            
            /// <summary>
            /// Closing time of day (ignored if allDay = true)
            /// </summary>
            [FhirElement("availableEndTime", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Time AvailableEndTimeElement
            {
                get { return _AvailableEndTimeElement; }
                set { _AvailableEndTimeElement = value; OnPropertyChanged("AvailableEndTimeElement"); }
            }
            
            private Hl7.Fhir.Model.Time _AvailableEndTimeElement;
            
            /// <summary>
            /// Closing time of day (ignored if allDay = true)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string AvailableEndTime
            {
                get { return AvailableEndTimeElement != null ? AvailableEndTimeElement.Value : null; }
                set
                {
                    if (value == null)
                        AvailableEndTimeElement = null;
                    else
                        AvailableEndTimeElement = new Hl7.Fhir.Model.Time(value);
                    OnPropertyChanged("AvailableEndTime");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("AvailableTimeComponent");
                base.Serialize(sink);
                sink.BeginList("daysOfWeek", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(DaysOfWeekElement);
                sink.End();
                sink.Element("allDay", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); AllDayElement?.Serialize(sink);
                sink.Element("availableStartTime", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); AvailableStartTimeElement?.Serialize(sink);
                sink.Element("availableEndTime", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); AvailableEndTimeElement?.Serialize(sink);
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
                    case "daysOfWeek":
                        DaysOfWeekElement = source.GetList<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DaysOfWeek>>();
                        return true;
                    case "allDay":
                        AllDayElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "availableStartTime":
                        AvailableStartTimeElement = source.Get<Hl7.Fhir.Model.Time>();
                        return true;
                    case "availableEndTime":
                        AvailableEndTimeElement = source.Get<Hl7.Fhir.Model.Time>();
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
                    case "daysOfWeek":
                    case "_daysOfWeek":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "allDay":
                        AllDayElement = source.PopulateValue(AllDayElement);
                        return true;
                    case "_allDay":
                        AllDayElement = source.Populate(AllDayElement);
                        return true;
                    case "availableStartTime":
                        AvailableStartTimeElement = source.PopulateValue(AvailableStartTimeElement);
                        return true;
                    case "_availableStartTime":
                        AvailableStartTimeElement = source.Populate(AvailableStartTimeElement);
                        return true;
                    case "availableEndTime":
                        AvailableEndTimeElement = source.PopulateValue(AvailableEndTimeElement);
                        return true;
                    case "_availableEndTime":
                        AvailableEndTimeElement = source.Populate(AvailableEndTimeElement);
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
                    case "daysOfWeek":
                        source.PopulatePrimitiveListItemValue(DaysOfWeekElement, index);
                        return true;
                    case "_daysOfWeek":
                        source.PopulatePrimitiveListItem(DaysOfWeekElement, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AvailableTimeComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DaysOfWeekElement != null) dest.DaysOfWeekElement = new List<Code<Hl7.Fhir.Model.DaysOfWeek>>(DaysOfWeekElement.DeepCopy());
                    if(AllDayElement != null) dest.AllDayElement = (Hl7.Fhir.Model.FhirBoolean)AllDayElement.DeepCopy();
                    if(AvailableStartTimeElement != null) dest.AvailableStartTimeElement = (Hl7.Fhir.Model.Time)AvailableStartTimeElement.DeepCopy();
                    if(AvailableEndTimeElement != null) dest.AvailableEndTimeElement = (Hl7.Fhir.Model.Time)AvailableEndTimeElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new AvailableTimeComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AvailableTimeComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DaysOfWeekElement, otherT.DaysOfWeekElement)) return false;
                if( !DeepComparable.Matches(AllDayElement, otherT.AllDayElement)) return false;
                if( !DeepComparable.Matches(AvailableStartTimeElement, otherT.AvailableStartTimeElement)) return false;
                if( !DeepComparable.Matches(AvailableEndTimeElement, otherT.AvailableEndTimeElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AvailableTimeComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DaysOfWeekElement, otherT.DaysOfWeekElement)) return false;
                if( !DeepComparable.IsExactly(AllDayElement, otherT.AllDayElement)) return false;
                if( !DeepComparable.IsExactly(AvailableStartTimeElement, otherT.AvailableStartTimeElement)) return false;
                if( !DeepComparable.IsExactly(AvailableEndTimeElement, otherT.AvailableEndTimeElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in DaysOfWeekElement) { if (elem != null) yield return elem; }
                    if (AllDayElement != null) yield return AllDayElement;
                    if (AvailableStartTimeElement != null) yield return AvailableStartTimeElement;
                    if (AvailableEndTimeElement != null) yield return AvailableEndTimeElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in DaysOfWeekElement) { if (elem != null) yield return new ElementValue("daysOfWeek", elem); }
                    if (AllDayElement != null) yield return new ElementValue("allDay", AllDayElement);
                    if (AvailableStartTimeElement != null) yield return new ElementValue("availableStartTime", AvailableStartTimeElement);
                    if (AvailableEndTimeElement != null) yield return new ElementValue("availableEndTime", AvailableEndTimeElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "NotAvailableComponent")]
        [DataContract]
        public partial class NotAvailableComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IHealthcareServiceNotAvailableComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "NotAvailableComponent"; } }
            
            /// <summary>
            /// Reason presented to the user explaining why time not available
            /// </summary>
            [FhirElement("description", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Reason presented to the user explaining why time not available
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
            /// Service not availablefrom this date
            /// </summary>
            [FhirElement("during", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Period During
            {
                get { return _During; }
                set { _During = value; OnPropertyChanged("During"); }
            }
            
            private Hl7.Fhir.Model.Period _During;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("NotAvailableComponent");
                base.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); DescriptionElement?.Serialize(sink);
                sink.Element("during", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); During?.Serialize(sink);
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
                    case "description":
                        DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "during":
                        During = source.Get<Hl7.Fhir.Model.Period>();
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
                    case "description":
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description":
                        DescriptionElement = source.Populate(DescriptionElement);
                        return true;
                    case "during":
                        During = source.Populate(During);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as NotAvailableComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(During != null) dest.During = (Hl7.Fhir.Model.Period)During.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new NotAvailableComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as NotAvailableComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(During, otherT.During)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as NotAvailableComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(During, otherT.During)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (During != null) yield return During;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (During != null) yield return new ElementValue("during", During);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IContactPoint> Hl7.Fhir.Model.IHealthcareService.Telecom { get { return Telecom; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IHealthcareServiceAvailableTimeComponent> Hl7.Fhir.Model.IHealthcareService.AvailableTime { get { return AvailableTime; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IHealthcareServiceNotAvailableComponent> Hl7.Fhir.Model.IHealthcareService.NotAvailable { get { return NotAvailable; } }
    
        
        /// <summary>
        /// External identifiers for this item
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Organization that provides this service
        /// </summary>
        [FhirElement("providedBy", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ProvidedBy
        {
            get { return _ProvidedBy; }
            set { _ProvidedBy = value; OnPropertyChanged("ProvidedBy"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _ProvidedBy;
        
        /// <summary>
        /// Broad category of service being performed or delivered
        /// </summary>
        [FhirElement("serviceCategory", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept ServiceCategory
        {
            get { return _ServiceCategory; }
            set { _ServiceCategory = value; OnPropertyChanged("ServiceCategory"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _ServiceCategory;
        
        /// <summary>
        /// Specific service delivered or performed
        /// </summary>
        [FhirElement("serviceType", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ServiceTypeComponent> ServiceType
        {
            get { if(_ServiceType==null) _ServiceType = new List<ServiceTypeComponent>(); return _ServiceType; }
            set { _ServiceType = value; OnPropertyChanged("ServiceType"); }
        }
        
        private List<ServiceTypeComponent> _ServiceType;
        
        /// <summary>
        /// Location where service may be provided
        /// </summary>
        [FhirElement("location", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [References("Location")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Location
        {
            get { return _Location; }
            set { _Location = value; OnPropertyChanged("Location"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Location;
        
        /// <summary>
        /// Description of service as presented to a consumer while searching
        /// </summary>
        [FhirElement("serviceName", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ServiceNameElement
        {
            get { return _ServiceNameElement; }
            set { _ServiceNameElement = value; OnPropertyChanged("ServiceNameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ServiceNameElement;
        
        /// <summary>
        /// Description of service as presented to a consumer while searching
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ServiceName
        {
            get { return ServiceNameElement != null ? ServiceNameElement.Value : null; }
            set
            {
                if (value == null)
                    ServiceNameElement = null;
                else
                    ServiceNameElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("ServiceName");
            }
        }
        
        /// <summary>
        /// Additional description and/or any specific issues not covered elsewhere
        /// </summary>
        [FhirElement("comment", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CommentElement
        {
            get { return _CommentElement; }
            set { _CommentElement = value; OnPropertyChanged("CommentElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _CommentElement;
        
        /// <summary>
        /// Additional description and/or any specific issues not covered elsewhere
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Comment
        {
            get { return CommentElement != null ? CommentElement.Value : null; }
            set
            {
                if (value == null)
                    CommentElement = null;
                else
                    CommentElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Comment");
            }
        }
        
        /// <summary>
        /// Extra details about the service that can't be placed in the other fields
        /// </summary>
        [FhirElement("extraDetails", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ExtraDetailsElement
        {
            get { return _ExtraDetailsElement; }
            set { _ExtraDetailsElement = value; OnPropertyChanged("ExtraDetailsElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ExtraDetailsElement;
        
        /// <summary>
        /// Extra details about the service that can't be placed in the other fields
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ExtraDetails
        {
            get { return ExtraDetailsElement != null ? ExtraDetailsElement.Value : null; }
            set
            {
                if (value == null)
                    ExtraDetailsElement = null;
                else
                    ExtraDetailsElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("ExtraDetails");
            }
        }
        
        /// <summary>
        /// Facilitates quick identification of the service
        /// </summary>
        [FhirElement("photo", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Attachment Photo
        {
            get { return _Photo; }
            set { _Photo = value; OnPropertyChanged("Photo"); }
        }
        
        private Hl7.Fhir.Model.Attachment _Photo;
        
        /// <summary>
        /// Contacts related to the healthcare service
        /// </summary>
        [FhirElement("telecom", Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.DSTU2.ContactPoint> Telecom
        {
            get { if(_Telecom==null) _Telecom = new List<Hl7.Fhir.Model.DSTU2.ContactPoint>(); return _Telecom; }
            set { _Telecom = value; OnPropertyChanged("Telecom"); }
        }
        
        private List<Hl7.Fhir.Model.DSTU2.ContactPoint> _Telecom;
        
        /// <summary>
        /// Location(s) service is inteded for/available to
        /// </summary>
        [FhirElement("coverageArea", Order=190)]
        [CLSCompliant(false)]
        [References("Location")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> CoverageArea
        {
            get { if(_CoverageArea==null) _CoverageArea = new List<Hl7.Fhir.Model.ResourceReference>(); return _CoverageArea; }
            set { _CoverageArea = value; OnPropertyChanged("CoverageArea"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _CoverageArea;
        
        /// <summary>
        /// Conditions under which service is available/offered
        /// </summary>
        [FhirElement("serviceProvisionCode", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ServiceProvisionCode
        {
            get { if(_ServiceProvisionCode==null) _ServiceProvisionCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ServiceProvisionCode; }
            set { _ServiceProvisionCode = value; OnPropertyChanged("ServiceProvisionCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ServiceProvisionCode;
        
        /// <summary>
        /// Specific eligibility requirements required to use the service
        /// </summary>
        [FhirElement("eligibility", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Eligibility
        {
            get { return _Eligibility; }
            set { _Eligibility = value; OnPropertyChanged("Eligibility"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Eligibility;
        
        /// <summary>
        /// Describes the eligibility conditions for the service
        /// </summary>
        [FhirElement("eligibilityNote", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString EligibilityNoteElement
        {
            get { return _EligibilityNoteElement; }
            set { _EligibilityNoteElement = value; OnPropertyChanged("EligibilityNoteElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _EligibilityNoteElement;
        
        /// <summary>
        /// Describes the eligibility conditions for the service
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string EligibilityNote
        {
            get { return EligibilityNoteElement != null ? EligibilityNoteElement.Value : null; }
            set
            {
                if (value == null)
                    EligibilityNoteElement = null;
                else
                    EligibilityNoteElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("EligibilityNote");
            }
        }
        
        /// <summary>
        /// Program Names that categorize the service
        /// </summary>
        [FhirElement("programName", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> ProgramNameElement
        {
            get { if(_ProgramNameElement==null) _ProgramNameElement = new List<Hl7.Fhir.Model.FhirString>(); return _ProgramNameElement; }
            set { _ProgramNameElement = value; OnPropertyChanged("ProgramNameElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _ProgramNameElement;
        
        /// <summary>
        /// Program Names that categorize the service
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> ProgramName
        {
            get { return ProgramNameElement != null ? ProgramNameElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    ProgramNameElement = null;
                else
                    ProgramNameElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("ProgramName");
            }
        }
        
        /// <summary>
        /// Collection of characteristics (attributes)
        /// </summary>
        [FhirElement("characteristic", Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Characteristic
        {
            get { if(_Characteristic==null) _Characteristic = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Characteristic; }
            set { _Characteristic = value; OnPropertyChanged("Characteristic"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Characteristic;
        
        /// <summary>
        /// Ways that the service accepts referrals
        /// </summary>
        [FhirElement("referralMethod", Order=250)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReferralMethod
        {
            get { if(_ReferralMethod==null) _ReferralMethod = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReferralMethod; }
            set { _ReferralMethod = value; OnPropertyChanged("ReferralMethod"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ReferralMethod;
        
        /// <summary>
        /// PKI Public keys to support secure communications
        /// </summary>
        [FhirElement("publicKey", Order=260)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PublicKeyElement
        {
            get { return _PublicKeyElement; }
            set { _PublicKeyElement = value; OnPropertyChanged("PublicKeyElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PublicKeyElement;
        
        /// <summary>
        /// PKI Public keys to support secure communications
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string PublicKey
        {
            get { return PublicKeyElement != null ? PublicKeyElement.Value : null; }
            set
            {
                if (value == null)
                    PublicKeyElement = null;
                else
                    PublicKeyElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("PublicKey");
            }
        }
        
        /// <summary>
        /// If an appointment is required for access to this service
        /// </summary>
        [FhirElement("appointmentRequired", Order=270)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean AppointmentRequiredElement
        {
            get { return _AppointmentRequiredElement; }
            set { _AppointmentRequiredElement = value; OnPropertyChanged("AppointmentRequiredElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _AppointmentRequiredElement;
        
        /// <summary>
        /// If an appointment is required for access to this service
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? AppointmentRequired
        {
            get { return AppointmentRequiredElement != null ? AppointmentRequiredElement.Value : null; }
            set
            {
                if (value == null)
                    AppointmentRequiredElement = null;
                else
                    AppointmentRequiredElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("AppointmentRequired");
            }
        }
        
        /// <summary>
        /// Times the Service Site is available
        /// </summary>
        [FhirElement("availableTime", Order=280)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<AvailableTimeComponent> AvailableTime
        {
            get { if(_AvailableTime==null) _AvailableTime = new List<AvailableTimeComponent>(); return _AvailableTime; }
            set { _AvailableTime = value; OnPropertyChanged("AvailableTime"); }
        }
        
        private List<AvailableTimeComponent> _AvailableTime;
        
        /// <summary>
        /// Not available during this time due to provided reason
        /// </summary>
        [FhirElement("notAvailable", Order=290)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<NotAvailableComponent> NotAvailable
        {
            get { if(_NotAvailable==null) _NotAvailable = new List<NotAvailableComponent>(); return _NotAvailable; }
            set { _NotAvailable = value; OnPropertyChanged("NotAvailable"); }
        }
        
        private List<NotAvailableComponent> _NotAvailable;
        
        /// <summary>
        /// Description of availability exceptions
        /// </summary>
        [FhirElement("availabilityExceptions", Order=300)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString AvailabilityExceptionsElement
        {
            get { return _AvailabilityExceptionsElement; }
            set { _AvailabilityExceptionsElement = value; OnPropertyChanged("AvailabilityExceptionsElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _AvailabilityExceptionsElement;
        
        /// <summary>
        /// Description of availability exceptions
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string AvailabilityExceptions
        {
            get { return AvailabilityExceptionsElement != null ? AvailabilityExceptionsElement.Value : null; }
            set
            {
                if (value == null)
                    AvailabilityExceptionsElement = null;
                else
                    AvailabilityExceptionsElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("AvailabilityExceptions");
            }
        }
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as HealthcareService;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(ProvidedBy != null) dest.ProvidedBy = (Hl7.Fhir.Model.ResourceReference)ProvidedBy.DeepCopy();
                if(ServiceCategory != null) dest.ServiceCategory = (Hl7.Fhir.Model.CodeableConcept)ServiceCategory.DeepCopy();
                if(ServiceType != null) dest.ServiceType = new List<ServiceTypeComponent>(ServiceType.DeepCopy());
                if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                if(ServiceNameElement != null) dest.ServiceNameElement = (Hl7.Fhir.Model.FhirString)ServiceNameElement.DeepCopy();
                if(CommentElement != null) dest.CommentElement = (Hl7.Fhir.Model.FhirString)CommentElement.DeepCopy();
                if(ExtraDetailsElement != null) dest.ExtraDetailsElement = (Hl7.Fhir.Model.FhirString)ExtraDetailsElement.DeepCopy();
                if(Photo != null) dest.Photo = (Hl7.Fhir.Model.Attachment)Photo.DeepCopy();
                if(Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.DSTU2.ContactPoint>(Telecom.DeepCopy());
                if(CoverageArea != null) dest.CoverageArea = new List<Hl7.Fhir.Model.ResourceReference>(CoverageArea.DeepCopy());
                if(ServiceProvisionCode != null) dest.ServiceProvisionCode = new List<Hl7.Fhir.Model.CodeableConcept>(ServiceProvisionCode.DeepCopy());
                if(Eligibility != null) dest.Eligibility = (Hl7.Fhir.Model.CodeableConcept)Eligibility.DeepCopy();
                if(EligibilityNoteElement != null) dest.EligibilityNoteElement = (Hl7.Fhir.Model.FhirString)EligibilityNoteElement.DeepCopy();
                if(ProgramNameElement != null) dest.ProgramNameElement = new List<Hl7.Fhir.Model.FhirString>(ProgramNameElement.DeepCopy());
                if(Characteristic != null) dest.Characteristic = new List<Hl7.Fhir.Model.CodeableConcept>(Characteristic.DeepCopy());
                if(ReferralMethod != null) dest.ReferralMethod = new List<Hl7.Fhir.Model.CodeableConcept>(ReferralMethod.DeepCopy());
                if(PublicKeyElement != null) dest.PublicKeyElement = (Hl7.Fhir.Model.FhirString)PublicKeyElement.DeepCopy();
                if(AppointmentRequiredElement != null) dest.AppointmentRequiredElement = (Hl7.Fhir.Model.FhirBoolean)AppointmentRequiredElement.DeepCopy();
                if(AvailableTime != null) dest.AvailableTime = new List<AvailableTimeComponent>(AvailableTime.DeepCopy());
                if(NotAvailable != null) dest.NotAvailable = new List<NotAvailableComponent>(NotAvailable.DeepCopy());
                if(AvailabilityExceptionsElement != null) dest.AvailabilityExceptionsElement = (Hl7.Fhir.Model.FhirString)AvailabilityExceptionsElement.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new HealthcareService());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as HealthcareService;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(ProvidedBy, otherT.ProvidedBy)) return false;
            if( !DeepComparable.Matches(ServiceCategory, otherT.ServiceCategory)) return false;
            if( !DeepComparable.Matches(ServiceType, otherT.ServiceType)) return false;
            if( !DeepComparable.Matches(Location, otherT.Location)) return false;
            if( !DeepComparable.Matches(ServiceNameElement, otherT.ServiceNameElement)) return false;
            if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
            if( !DeepComparable.Matches(ExtraDetailsElement, otherT.ExtraDetailsElement)) return false;
            if( !DeepComparable.Matches(Photo, otherT.Photo)) return false;
            if( !DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
            if( !DeepComparable.Matches(CoverageArea, otherT.CoverageArea)) return false;
            if( !DeepComparable.Matches(ServiceProvisionCode, otherT.ServiceProvisionCode)) return false;
            if( !DeepComparable.Matches(Eligibility, otherT.Eligibility)) return false;
            if( !DeepComparable.Matches(EligibilityNoteElement, otherT.EligibilityNoteElement)) return false;
            if( !DeepComparable.Matches(ProgramNameElement, otherT.ProgramNameElement)) return false;
            if( !DeepComparable.Matches(Characteristic, otherT.Characteristic)) return false;
            if( !DeepComparable.Matches(ReferralMethod, otherT.ReferralMethod)) return false;
            if( !DeepComparable.Matches(PublicKeyElement, otherT.PublicKeyElement)) return false;
            if( !DeepComparable.Matches(AppointmentRequiredElement, otherT.AppointmentRequiredElement)) return false;
            if( !DeepComparable.Matches(AvailableTime, otherT.AvailableTime)) return false;
            if( !DeepComparable.Matches(NotAvailable, otherT.NotAvailable)) return false;
            if( !DeepComparable.Matches(AvailabilityExceptionsElement, otherT.AvailabilityExceptionsElement)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as HealthcareService;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(ProvidedBy, otherT.ProvidedBy)) return false;
            if( !DeepComparable.IsExactly(ServiceCategory, otherT.ServiceCategory)) return false;
            if( !DeepComparable.IsExactly(ServiceType, otherT.ServiceType)) return false;
            if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
            if( !DeepComparable.IsExactly(ServiceNameElement, otherT.ServiceNameElement)) return false;
            if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
            if( !DeepComparable.IsExactly(ExtraDetailsElement, otherT.ExtraDetailsElement)) return false;
            if( !DeepComparable.IsExactly(Photo, otherT.Photo)) return false;
            if( !DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
            if( !DeepComparable.IsExactly(CoverageArea, otherT.CoverageArea)) return false;
            if( !DeepComparable.IsExactly(ServiceProvisionCode, otherT.ServiceProvisionCode)) return false;
            if( !DeepComparable.IsExactly(Eligibility, otherT.Eligibility)) return false;
            if( !DeepComparable.IsExactly(EligibilityNoteElement, otherT.EligibilityNoteElement)) return false;
            if( !DeepComparable.IsExactly(ProgramNameElement, otherT.ProgramNameElement)) return false;
            if( !DeepComparable.IsExactly(Characteristic, otherT.Characteristic)) return false;
            if( !DeepComparable.IsExactly(ReferralMethod, otherT.ReferralMethod)) return false;
            if( !DeepComparable.IsExactly(PublicKeyElement, otherT.PublicKeyElement)) return false;
            if( !DeepComparable.IsExactly(AppointmentRequiredElement, otherT.AppointmentRequiredElement)) return false;
            if( !DeepComparable.IsExactly(AvailableTime, otherT.AvailableTime)) return false;
            if( !DeepComparable.IsExactly(NotAvailable, otherT.NotAvailable)) return false;
            if( !DeepComparable.IsExactly(AvailabilityExceptionsElement, otherT.AvailabilityExceptionsElement)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("HealthcareService");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("providedBy", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ProvidedBy?.Serialize(sink);
            sink.Element("serviceCategory", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ServiceCategory?.Serialize(sink);
            sink.BeginList("serviceType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in ServiceType)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("location", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Location?.Serialize(sink);
            sink.Element("serviceName", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ServiceNameElement?.Serialize(sink);
            sink.Element("comment", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CommentElement?.Serialize(sink);
            sink.Element("extraDetails", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ExtraDetailsElement?.Serialize(sink);
            sink.Element("photo", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Photo?.Serialize(sink);
            sink.BeginList("telecom", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Telecom)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("coverageArea", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in CoverageArea)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("serviceProvisionCode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in ServiceProvisionCode)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("eligibility", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Eligibility?.Serialize(sink);
            sink.Element("eligibilityNote", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); EligibilityNoteElement?.Serialize(sink);
            sink.BeginList("programName", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            sink.Serialize(ProgramNameElement);
            sink.End();
            sink.BeginList("characteristic", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Characteristic)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("referralMethod", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in ReferralMethod)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("publicKey", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PublicKeyElement?.Serialize(sink);
            sink.Element("appointmentRequired", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); AppointmentRequiredElement?.Serialize(sink);
            sink.BeginList("availableTime", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in AvailableTime)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("notAvailable", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in NotAvailable)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("availabilityExceptions", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); AvailabilityExceptionsElement?.Serialize(sink);
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
                case "providedBy":
                    ProvidedBy = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "serviceCategory":
                    ServiceCategory = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "serviceType":
                    ServiceType = source.GetList<ServiceTypeComponent>();
                    return true;
                case "location":
                    Location = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "serviceName":
                    ServiceNameElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "comment":
                    CommentElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "extraDetails":
                    ExtraDetailsElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "photo":
                    Photo = source.Get<Hl7.Fhir.Model.Attachment>();
                    return true;
                case "telecom":
                    Telecom = source.GetList<Hl7.Fhir.Model.DSTU2.ContactPoint>();
                    return true;
                case "coverageArea":
                    CoverageArea = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "serviceProvisionCode":
                    ServiceProvisionCode = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "eligibility":
                    Eligibility = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "eligibilityNote":
                    EligibilityNoteElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "programName":
                    ProgramNameElement = source.GetList<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "characteristic":
                    Characteristic = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "referralMethod":
                    ReferralMethod = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "publicKey":
                    PublicKeyElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "appointmentRequired":
                    AppointmentRequiredElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "availableTime":
                    AvailableTime = source.GetList<AvailableTimeComponent>();
                    return true;
                case "notAvailable":
                    NotAvailable = source.GetList<NotAvailableComponent>();
                    return true;
                case "availabilityExceptions":
                    AvailabilityExceptionsElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                case "providedBy":
                    ProvidedBy = source.Populate(ProvidedBy);
                    return true;
                case "serviceCategory":
                    ServiceCategory = source.Populate(ServiceCategory);
                    return true;
                case "serviceType":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "location":
                    Location = source.Populate(Location);
                    return true;
                case "serviceName":
                    ServiceNameElement = source.PopulateValue(ServiceNameElement);
                    return true;
                case "_serviceName":
                    ServiceNameElement = source.Populate(ServiceNameElement);
                    return true;
                case "comment":
                    CommentElement = source.PopulateValue(CommentElement);
                    return true;
                case "_comment":
                    CommentElement = source.Populate(CommentElement);
                    return true;
                case "extraDetails":
                    ExtraDetailsElement = source.PopulateValue(ExtraDetailsElement);
                    return true;
                case "_extraDetails":
                    ExtraDetailsElement = source.Populate(ExtraDetailsElement);
                    return true;
                case "photo":
                    Photo = source.Populate(Photo);
                    return true;
                case "telecom":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "coverageArea":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "serviceProvisionCode":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "eligibility":
                    Eligibility = source.Populate(Eligibility);
                    return true;
                case "eligibilityNote":
                    EligibilityNoteElement = source.PopulateValue(EligibilityNoteElement);
                    return true;
                case "_eligibilityNote":
                    EligibilityNoteElement = source.Populate(EligibilityNoteElement);
                    return true;
                case "programName":
                case "_programName":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "characteristic":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "referralMethod":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "publicKey":
                    PublicKeyElement = source.PopulateValue(PublicKeyElement);
                    return true;
                case "_publicKey":
                    PublicKeyElement = source.Populate(PublicKeyElement);
                    return true;
                case "appointmentRequired":
                    AppointmentRequiredElement = source.PopulateValue(AppointmentRequiredElement);
                    return true;
                case "_appointmentRequired":
                    AppointmentRequiredElement = source.Populate(AppointmentRequiredElement);
                    return true;
                case "availableTime":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "notAvailable":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "availabilityExceptions":
                    AvailabilityExceptionsElement = source.PopulateValue(AvailabilityExceptionsElement);
                    return true;
                case "_availabilityExceptions":
                    AvailabilityExceptionsElement = source.Populate(AvailabilityExceptionsElement);
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
                case "serviceType":
                    source.PopulateListItem(ServiceType, index);
                    return true;
                case "telecom":
                    source.PopulateListItem(Telecom, index);
                    return true;
                case "coverageArea":
                    source.PopulateListItem(CoverageArea, index);
                    return true;
                case "serviceProvisionCode":
                    source.PopulateListItem(ServiceProvisionCode, index);
                    return true;
                case "programName":
                    source.PopulatePrimitiveListItemValue(ProgramNameElement, index);
                    return true;
                case "_programName":
                    source.PopulatePrimitiveListItem(ProgramNameElement, index);
                    return true;
                case "characteristic":
                    source.PopulateListItem(Characteristic, index);
                    return true;
                case "referralMethod":
                    source.PopulateListItem(ReferralMethod, index);
                    return true;
                case "availableTime":
                    source.PopulateListItem(AvailableTime, index);
                    return true;
                case "notAvailable":
                    source.PopulateListItem(NotAvailable, index);
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
                if (ProvidedBy != null) yield return ProvidedBy;
                if (ServiceCategory != null) yield return ServiceCategory;
                foreach (var elem in ServiceType) { if (elem != null) yield return elem; }
                if (Location != null) yield return Location;
                if (ServiceNameElement != null) yield return ServiceNameElement;
                if (CommentElement != null) yield return CommentElement;
                if (ExtraDetailsElement != null) yield return ExtraDetailsElement;
                if (Photo != null) yield return Photo;
                foreach (var elem in Telecom) { if (elem != null) yield return elem; }
                foreach (var elem in CoverageArea) { if (elem != null) yield return elem; }
                foreach (var elem in ServiceProvisionCode) { if (elem != null) yield return elem; }
                if (Eligibility != null) yield return Eligibility;
                if (EligibilityNoteElement != null) yield return EligibilityNoteElement;
                foreach (var elem in ProgramNameElement) { if (elem != null) yield return elem; }
                foreach (var elem in Characteristic) { if (elem != null) yield return elem; }
                foreach (var elem in ReferralMethod) { if (elem != null) yield return elem; }
                if (PublicKeyElement != null) yield return PublicKeyElement;
                if (AppointmentRequiredElement != null) yield return AppointmentRequiredElement;
                foreach (var elem in AvailableTime) { if (elem != null) yield return elem; }
                foreach (var elem in NotAvailable) { if (elem != null) yield return elem; }
                if (AvailabilityExceptionsElement != null) yield return AvailabilityExceptionsElement;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (ProvidedBy != null) yield return new ElementValue("providedBy", ProvidedBy);
                if (ServiceCategory != null) yield return new ElementValue("serviceCategory", ServiceCategory);
                foreach (var elem in ServiceType) { if (elem != null) yield return new ElementValue("serviceType", elem); }
                if (Location != null) yield return new ElementValue("location", Location);
                if (ServiceNameElement != null) yield return new ElementValue("serviceName", ServiceNameElement);
                if (CommentElement != null) yield return new ElementValue("comment", CommentElement);
                if (ExtraDetailsElement != null) yield return new ElementValue("extraDetails", ExtraDetailsElement);
                if (Photo != null) yield return new ElementValue("photo", Photo);
                foreach (var elem in Telecom) { if (elem != null) yield return new ElementValue("telecom", elem); }
                foreach (var elem in CoverageArea) { if (elem != null) yield return new ElementValue("coverageArea", elem); }
                foreach (var elem in ServiceProvisionCode) { if (elem != null) yield return new ElementValue("serviceProvisionCode", elem); }
                if (Eligibility != null) yield return new ElementValue("eligibility", Eligibility);
                if (EligibilityNoteElement != null) yield return new ElementValue("eligibilityNote", EligibilityNoteElement);
                foreach (var elem in ProgramNameElement) { if (elem != null) yield return new ElementValue("programName", elem); }
                foreach (var elem in Characteristic) { if (elem != null) yield return new ElementValue("characteristic", elem); }
                foreach (var elem in ReferralMethod) { if (elem != null) yield return new ElementValue("referralMethod", elem); }
                if (PublicKeyElement != null) yield return new ElementValue("publicKey", PublicKeyElement);
                if (AppointmentRequiredElement != null) yield return new ElementValue("appointmentRequired", AppointmentRequiredElement);
                foreach (var elem in AvailableTime) { if (elem != null) yield return new ElementValue("availableTime", elem); }
                foreach (var elem in NotAvailable) { if (elem != null) yield return new ElementValue("notAvailable", elem); }
                if (AvailabilityExceptionsElement != null) yield return new ElementValue("availabilityExceptions", AvailabilityExceptionsElement);
            }
        }
    
    }

}
