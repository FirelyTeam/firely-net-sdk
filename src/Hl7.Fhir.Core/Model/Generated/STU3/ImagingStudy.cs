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
    /// A set of images produced in single study (one or more series of references images)
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "ImagingStudy", IsResource=true)]
    [DataContract]
    public partial class ImagingStudy : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IImagingStudy, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ImagingStudy; } }
        [NotMapped]
        public override string TypeName { get { return "ImagingStudy"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "SeriesComponent")]
        [DataContract]
        public partial class SeriesComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IImagingStudySeriesComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SeriesComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IImagingStudyInstanceComponent> Hl7.Fhir.Model.IImagingStudySeriesComponent.Instance { get { return Instance; } }
            
            /// <summary>
            /// Formal DICOM identifier for this series
            /// </summary>
            [FhirElement("uid", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Oid UidElement
            {
                get { return _UidElement; }
                set { _UidElement = value; OnPropertyChanged("UidElement"); }
            }
            
            private Hl7.Fhir.Model.Oid _UidElement;
            
            /// <summary>
            /// Formal DICOM identifier for this series
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Uid
            {
                get { return UidElement != null ? UidElement.Value : null; }
                set
                {
                    if (value == null)
                        UidElement = null;
                    else
                        UidElement = new Hl7.Fhir.Model.Oid(value);
                    OnPropertyChanged("Uid");
                }
            }
            
            /// <summary>
            /// Numeric identifier of this series
            /// </summary>
            [FhirElement("number", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.UnsignedInt NumberElement
            {
                get { return _NumberElement; }
                set { _NumberElement = value; OnPropertyChanged("NumberElement"); }
            }
            
            private Hl7.Fhir.Model.UnsignedInt _NumberElement;
            
            /// <summary>
            /// Numeric identifier of this series
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Number
            {
                get { return NumberElement != null ? NumberElement.Value : null; }
                set
                {
                    if (value == null)
                        NumberElement = null;
                    else
                        NumberElement = new Hl7.Fhir.Model.UnsignedInt(value);
                    OnPropertyChanged("Number");
                }
            }
            
            /// <summary>
            /// The modality of the instances in the series
            /// </summary>
            [FhirElement("modality", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Modality
            {
                get { return _Modality; }
                set { _Modality = value; OnPropertyChanged("Modality"); }
            }
            
            private Hl7.Fhir.Model.Coding _Modality;
            
            /// <summary>
            /// A short human readable summary of the series
            /// </summary>
            [FhirElement("description", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// A short human readable summary of the series
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
            /// Number of Series Related Instances
            /// </summary>
            [FhirElement("numberOfInstances", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.UnsignedInt NumberOfInstancesElement
            {
                get { return _NumberOfInstancesElement; }
                set { _NumberOfInstancesElement = value; OnPropertyChanged("NumberOfInstancesElement"); }
            }
            
            private Hl7.Fhir.Model.UnsignedInt _NumberOfInstancesElement;
            
            /// <summary>
            /// Number of Series Related Instances
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? NumberOfInstances
            {
                get { return NumberOfInstancesElement != null ? NumberOfInstancesElement.Value : null; }
                set
                {
                    if (value == null)
                        NumberOfInstancesElement = null;
                    else
                        NumberOfInstancesElement = new Hl7.Fhir.Model.UnsignedInt(value);
                    OnPropertyChanged("NumberOfInstances");
                }
            }
            
            /// <summary>
            /// ONLINE | OFFLINE | NEARLINE | UNAVAILABLE
            /// </summary>
            [FhirElement("availability", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
            [CLSCompliant(false)]
            [DataMember]
            public Code<Hl7.Fhir.Model.InstanceAvailability> AvailabilityElement
            {
                get { return _AvailabilityElement; }
                set { _AvailabilityElement = value; OnPropertyChanged("AvailabilityElement"); }
            }
            
            private Code<Hl7.Fhir.Model.InstanceAvailability> _AvailabilityElement;
            
            /// <summary>
            /// ONLINE | OFFLINE | NEARLINE | UNAVAILABLE
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.InstanceAvailability? Availability
            {
                get { return AvailabilityElement != null ? AvailabilityElement.Value : null; }
                set
                {
                    if (value == null)
                        AvailabilityElement = null;
                    else
                        AvailabilityElement = new Code<Hl7.Fhir.Model.InstanceAvailability>(value);
                    OnPropertyChanged("Availability");
                }
            }
            
            /// <summary>
            /// Series access endpoint
            /// </summary>
            [FhirElement("endpoint", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
            [CLSCompliant(false)]
            [References("Endpoint")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Endpoint
            {
                get { if(_Endpoint==null) _Endpoint = new List<Hl7.Fhir.Model.ResourceReference>(); return _Endpoint; }
                set { _Endpoint = value; OnPropertyChanged("Endpoint"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Endpoint;
            
            /// <summary>
            /// Body part examined
            /// </summary>
            [FhirElement("bodySite", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Coding BodySite
            {
                get { return _BodySite; }
                set { _BodySite = value; OnPropertyChanged("BodySite"); }
            }
            
            private Hl7.Fhir.Model.Coding _BodySite;
            
            /// <summary>
            /// Body part laterality
            /// </summary>
            [FhirElement("laterality", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Laterality
            {
                get { return _Laterality; }
                set { _Laterality = value; OnPropertyChanged("Laterality"); }
            }
            
            private Hl7.Fhir.Model.Coding _Laterality;
            
            /// <summary>
            /// When the series started
            /// </summary>
            [FhirElement("started", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime StartedElement
            {
                get { return _StartedElement; }
                set { _StartedElement = value; OnPropertyChanged("StartedElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _StartedElement;
            
            /// <summary>
            /// When the series started
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Started
            {
                get { return StartedElement != null ? StartedElement.Value : null; }
                set
                {
                    if (value == null)
                        StartedElement = null;
                    else
                        StartedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("Started");
                }
            }
            
            /// <summary>
            /// Who performed the series
            /// </summary>
            [FhirElement("performer", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
            [CLSCompliant(false)]
            [References("Practitioner")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Performer
            {
                get { if(_Performer==null) _Performer = new List<Hl7.Fhir.Model.ResourceReference>(); return _Performer; }
                set { _Performer = value; OnPropertyChanged("Performer"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Performer;
            
            /// <summary>
            /// A single SOP instance from the series
            /// </summary>
            [FhirElement("instance", Order=150)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<InstanceComponent> Instance
            {
                get { if(_Instance==null) _Instance = new List<InstanceComponent>(); return _Instance; }
                set { _Instance = value; OnPropertyChanged("Instance"); }
            }
            
            private List<InstanceComponent> _Instance;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SeriesComponent");
                base.Serialize(sink);
                sink.Element("uid", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); UidElement?.Serialize(sink);
                sink.Element("number", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NumberElement?.Serialize(sink);
                sink.Element("modality", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Modality?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DescriptionElement?.Serialize(sink);
                sink.Element("numberOfInstances", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NumberOfInstancesElement?.Serialize(sink);
                sink.Element("availability", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); AvailabilityElement?.Serialize(sink);
                sink.BeginList("endpoint", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Endpoint)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("bodySite", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); BodySite?.Serialize(sink);
                sink.Element("laterality", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Laterality?.Serialize(sink);
                sink.Element("started", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StartedElement?.Serialize(sink);
                sink.BeginList("performer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Performer)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("instance", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Instance)
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
                    case "uid":
                        UidElement = source.Get<Hl7.Fhir.Model.Oid>();
                        return true;
                    case "number":
                        NumberElement = source.Get<Hl7.Fhir.Model.UnsignedInt>();
                        return true;
                    case "modality":
                        Modality = source.Get<Hl7.Fhir.Model.Coding>();
                        return true;
                    case "description":
                        DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "numberOfInstances":
                        NumberOfInstancesElement = source.Get<Hl7.Fhir.Model.UnsignedInt>();
                        return true;
                    case "availability":
                        AvailabilityElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.InstanceAvailability>>();
                        return true;
                    case "endpoint":
                        Endpoint = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "bodySite":
                        BodySite = source.Get<Hl7.Fhir.Model.Coding>();
                        return true;
                    case "laterality":
                        Laterality = source.Get<Hl7.Fhir.Model.Coding>();
                        return true;
                    case "started":
                        StartedElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                        return true;
                    case "performer":
                        Performer = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "instance":
                        Instance = source.GetList<InstanceComponent>();
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
                    case "uid":
                        UidElement = source.PopulateValue(UidElement);
                        return true;
                    case "_uid":
                        UidElement = source.Populate(UidElement);
                        return true;
                    case "number":
                        NumberElement = source.PopulateValue(NumberElement);
                        return true;
                    case "_number":
                        NumberElement = source.Populate(NumberElement);
                        return true;
                    case "modality":
                        Modality = source.Populate(Modality);
                        return true;
                    case "description":
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description":
                        DescriptionElement = source.Populate(DescriptionElement);
                        return true;
                    case "numberOfInstances":
                        NumberOfInstancesElement = source.PopulateValue(NumberOfInstancesElement);
                        return true;
                    case "_numberOfInstances":
                        NumberOfInstancesElement = source.Populate(NumberOfInstancesElement);
                        return true;
                    case "availability":
                        AvailabilityElement = source.PopulateValue(AvailabilityElement);
                        return true;
                    case "_availability":
                        AvailabilityElement = source.Populate(AvailabilityElement);
                        return true;
                    case "endpoint":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "bodySite":
                        BodySite = source.Populate(BodySite);
                        return true;
                    case "laterality":
                        Laterality = source.Populate(Laterality);
                        return true;
                    case "started":
                        StartedElement = source.PopulateValue(StartedElement);
                        return true;
                    case "_started":
                        StartedElement = source.Populate(StartedElement);
                        return true;
                    case "performer":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "instance":
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
                    case "endpoint":
                        source.PopulateListItem(Endpoint, index);
                        return true;
                    case "performer":
                        source.PopulateListItem(Performer, index);
                        return true;
                    case "instance":
                        source.PopulateListItem(Instance, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SeriesComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(UidElement != null) dest.UidElement = (Hl7.Fhir.Model.Oid)UidElement.DeepCopy();
                    if(NumberElement != null) dest.NumberElement = (Hl7.Fhir.Model.UnsignedInt)NumberElement.DeepCopy();
                    if(Modality != null) dest.Modality = (Hl7.Fhir.Model.Coding)Modality.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(NumberOfInstancesElement != null) dest.NumberOfInstancesElement = (Hl7.Fhir.Model.UnsignedInt)NumberOfInstancesElement.DeepCopy();
                    if(AvailabilityElement != null) dest.AvailabilityElement = (Code<Hl7.Fhir.Model.InstanceAvailability>)AvailabilityElement.DeepCopy();
                    if(Endpoint != null) dest.Endpoint = new List<Hl7.Fhir.Model.ResourceReference>(Endpoint.DeepCopy());
                    if(BodySite != null) dest.BodySite = (Hl7.Fhir.Model.Coding)BodySite.DeepCopy();
                    if(Laterality != null) dest.Laterality = (Hl7.Fhir.Model.Coding)Laterality.DeepCopy();
                    if(StartedElement != null) dest.StartedElement = (Hl7.Fhir.Model.FhirDateTime)StartedElement.DeepCopy();
                    if(Performer != null) dest.Performer = new List<Hl7.Fhir.Model.ResourceReference>(Performer.DeepCopy());
                    if(Instance != null) dest.Instance = new List<InstanceComponent>(Instance.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SeriesComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SeriesComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(UidElement, otherT.UidElement)) return false;
                if( !DeepComparable.Matches(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.Matches(Modality, otherT.Modality)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(NumberOfInstancesElement, otherT.NumberOfInstancesElement)) return false;
                if( !DeepComparable.Matches(AvailabilityElement, otherT.AvailabilityElement)) return false;
                if( !DeepComparable.Matches(Endpoint, otherT.Endpoint)) return false;
                if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
                if( !DeepComparable.Matches(Laterality, otherT.Laterality)) return false;
                if( !DeepComparable.Matches(StartedElement, otherT.StartedElement)) return false;
                if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
                if( !DeepComparable.Matches(Instance, otherT.Instance)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SeriesComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(UidElement, otherT.UidElement)) return false;
                if( !DeepComparable.IsExactly(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.IsExactly(Modality, otherT.Modality)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(NumberOfInstancesElement, otherT.NumberOfInstancesElement)) return false;
                if( !DeepComparable.IsExactly(AvailabilityElement, otherT.AvailabilityElement)) return false;
                if( !DeepComparable.IsExactly(Endpoint, otherT.Endpoint)) return false;
                if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
                if( !DeepComparable.IsExactly(Laterality, otherT.Laterality)) return false;
                if( !DeepComparable.IsExactly(StartedElement, otherT.StartedElement)) return false;
                if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
                if( !DeepComparable.IsExactly(Instance, otherT.Instance)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (UidElement != null) yield return UidElement;
                    if (NumberElement != null) yield return NumberElement;
                    if (Modality != null) yield return Modality;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (NumberOfInstancesElement != null) yield return NumberOfInstancesElement;
                    if (AvailabilityElement != null) yield return AvailabilityElement;
                    foreach (var elem in Endpoint) { if (elem != null) yield return elem; }
                    if (BodySite != null) yield return BodySite;
                    if (Laterality != null) yield return Laterality;
                    if (StartedElement != null) yield return StartedElement;
                    foreach (var elem in Performer) { if (elem != null) yield return elem; }
                    foreach (var elem in Instance) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (UidElement != null) yield return new ElementValue("uid", UidElement);
                    if (NumberElement != null) yield return new ElementValue("number", NumberElement);
                    if (Modality != null) yield return new ElementValue("modality", Modality);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (NumberOfInstancesElement != null) yield return new ElementValue("numberOfInstances", NumberOfInstancesElement);
                    if (AvailabilityElement != null) yield return new ElementValue("availability", AvailabilityElement);
                    foreach (var elem in Endpoint) { if (elem != null) yield return new ElementValue("endpoint", elem); }
                    if (BodySite != null) yield return new ElementValue("bodySite", BodySite);
                    if (Laterality != null) yield return new ElementValue("laterality", Laterality);
                    if (StartedElement != null) yield return new ElementValue("started", StartedElement);
                    foreach (var elem in Performer) { if (elem != null) yield return new ElementValue("performer", elem); }
                    foreach (var elem in Instance) { if (elem != null) yield return new ElementValue("instance", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "InstanceComponent")]
        [DataContract]
        public partial class InstanceComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IImagingStudyInstanceComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "InstanceComponent"; } }
            
            /// <summary>
            /// Formal DICOM identifier for this instance
            /// </summary>
            [FhirElement("uid", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Oid UidElement
            {
                get { return _UidElement; }
                set { _UidElement = value; OnPropertyChanged("UidElement"); }
            }
            
            private Hl7.Fhir.Model.Oid _UidElement;
            
            /// <summary>
            /// Formal DICOM identifier for this instance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Uid
            {
                get { return UidElement != null ? UidElement.Value : null; }
                set
                {
                    if (value == null)
                        UidElement = null;
                    else
                        UidElement = new Hl7.Fhir.Model.Oid(value);
                    OnPropertyChanged("Uid");
                }
            }
            
            /// <summary>
            /// The number of this instance in the series
            /// </summary>
            [FhirElement("number", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.UnsignedInt NumberElement
            {
                get { return _NumberElement; }
                set { _NumberElement = value; OnPropertyChanged("NumberElement"); }
            }
            
            private Hl7.Fhir.Model.UnsignedInt _NumberElement;
            
            /// <summary>
            /// The number of this instance in the series
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Number
            {
                get { return NumberElement != null ? NumberElement.Value : null; }
                set
                {
                    if (value == null)
                        NumberElement = null;
                    else
                        NumberElement = new Hl7.Fhir.Model.UnsignedInt(value);
                    OnPropertyChanged("Number");
                }
            }
            
            /// <summary>
            /// DICOM class type
            /// </summary>
            [FhirElement("sopClass", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Oid SopClassElement
            {
                get { return _SopClassElement; }
                set { _SopClassElement = value; OnPropertyChanged("SopClassElement"); }
            }
            
            private Hl7.Fhir.Model.Oid _SopClassElement;
            
            /// <summary>
            /// DICOM class type
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string SopClass
            {
                get { return SopClassElement != null ? SopClassElement.Value : null; }
                set
                {
                    if (value == null)
                        SopClassElement = null;
                    else
                        SopClassElement = new Hl7.Fhir.Model.Oid(value);
                    OnPropertyChanged("SopClass");
                }
            }
            
            /// <summary>
            /// Description of instance
            /// </summary>
            [FhirElement("title", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TitleElement
            {
                get { return _TitleElement; }
                set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TitleElement;
            
            /// <summary>
            /// Description of instance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Title
            {
                get { return TitleElement != null ? TitleElement.Value : null; }
                set
                {
                    if (value == null)
                        TitleElement = null;
                    else
                        TitleElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Title");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("InstanceComponent");
                base.Serialize(sink);
                sink.Element("uid", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); UidElement?.Serialize(sink);
                sink.Element("number", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); NumberElement?.Serialize(sink);
                sink.Element("sopClass", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); SopClassElement?.Serialize(sink);
                sink.Element("title", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); TitleElement?.Serialize(sink);
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
                    case "uid":
                        UidElement = source.Get<Hl7.Fhir.Model.Oid>();
                        return true;
                    case "number":
                        NumberElement = source.Get<Hl7.Fhir.Model.UnsignedInt>();
                        return true;
                    case "sopClass":
                        SopClassElement = source.Get<Hl7.Fhir.Model.Oid>();
                        return true;
                    case "title":
                        TitleElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "uid":
                        UidElement = source.PopulateValue(UidElement);
                        return true;
                    case "_uid":
                        UidElement = source.Populate(UidElement);
                        return true;
                    case "number":
                        NumberElement = source.PopulateValue(NumberElement);
                        return true;
                    case "_number":
                        NumberElement = source.Populate(NumberElement);
                        return true;
                    case "sopClass":
                        SopClassElement = source.PopulateValue(SopClassElement);
                        return true;
                    case "_sopClass":
                        SopClassElement = source.Populate(SopClassElement);
                        return true;
                    case "title":
                        TitleElement = source.PopulateValue(TitleElement);
                        return true;
                    case "_title":
                        TitleElement = source.Populate(TitleElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as InstanceComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(UidElement != null) dest.UidElement = (Hl7.Fhir.Model.Oid)UidElement.DeepCopy();
                    if(NumberElement != null) dest.NumberElement = (Hl7.Fhir.Model.UnsignedInt)NumberElement.DeepCopy();
                    if(SopClassElement != null) dest.SopClassElement = (Hl7.Fhir.Model.Oid)SopClassElement.DeepCopy();
                    if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new InstanceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as InstanceComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(UidElement, otherT.UidElement)) return false;
                if( !DeepComparable.Matches(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.Matches(SopClassElement, otherT.SopClassElement)) return false;
                if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as InstanceComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(UidElement, otherT.UidElement)) return false;
                if( !DeepComparable.IsExactly(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.IsExactly(SopClassElement, otherT.SopClassElement)) return false;
                if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (UidElement != null) yield return UidElement;
                    if (NumberElement != null) yield return NumberElement;
                    if (SopClassElement != null) yield return SopClassElement;
                    if (TitleElement != null) yield return TitleElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (UidElement != null) yield return new ElementValue("uid", UidElement);
                    if (NumberElement != null) yield return new ElementValue("number", NumberElement);
                    if (SopClassElement != null) yield return new ElementValue("sopClass", SopClassElement);
                    if (TitleElement != null) yield return new ElementValue("title", TitleElement);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IImagingStudySeriesComponent> Hl7.Fhir.Model.IImagingStudy.Series { get { return Series; } }
    
        
        /// <summary>
        /// Formal DICOM identifier for the study
        /// </summary>
        [FhirElement("uid", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Oid UidElement
        {
            get { return _UidElement; }
            set { _UidElement = value; OnPropertyChanged("UidElement"); }
        }
        
        private Hl7.Fhir.Model.Oid _UidElement;
        
        /// <summary>
        /// Formal DICOM identifier for the study
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Uid
        {
            get { return UidElement != null ? UidElement.Value : null; }
            set
            {
                if (value == null)
                    UidElement = null;
                else
                    UidElement = new Hl7.Fhir.Model.Oid(value);
                OnPropertyChanged("Uid");
            }
        }
        
        /// <summary>
        /// Related workflow identifier ("Accession Number")
        /// </summary>
        [FhirElement("accession", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Accession
        {
            get { return _Accession; }
            set { _Accession = value; OnPropertyChanged("Accession"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Accession;
        
        /// <summary>
        /// Other identifiers for the study
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
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
        /// ONLINE | OFFLINE | NEARLINE | UNAVAILABLE
        /// </summary>
        [FhirElement("availability", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.InstanceAvailability> AvailabilityElement
        {
            get { return _AvailabilityElement; }
            set { _AvailabilityElement = value; OnPropertyChanged("AvailabilityElement"); }
        }
        
        private Code<Hl7.Fhir.Model.InstanceAvailability> _AvailabilityElement;
        
        /// <summary>
        /// ONLINE | OFFLINE | NEARLINE | UNAVAILABLE
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.InstanceAvailability? Availability
        {
            get { return AvailabilityElement != null ? AvailabilityElement.Value : null; }
            set
            {
                if (value == null)
                    AvailabilityElement = null;
                else
                    AvailabilityElement = new Code<Hl7.Fhir.Model.InstanceAvailability>(value);
                OnPropertyChanged("Availability");
            }
        }
        
        /// <summary>
        /// All series modality if actual acquisition modalities
        /// </summary>
        [FhirElement("modalityList", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> ModalityList
        {
            get { if(_ModalityList==null) _ModalityList = new List<Hl7.Fhir.Model.Coding>(); return _ModalityList; }
            set { _ModalityList = value; OnPropertyChanged("ModalityList"); }
        }
        
        private List<Hl7.Fhir.Model.Coding> _ModalityList;
        
        /// <summary>
        /// Who the images are of
        /// </summary>
        [FhirElement("patient", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [References("Patient")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// Originating context
        /// </summary>
        [FhirElement("context", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [References("Encounter","EpisodeOfCare")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Context
        {
            get { return _Context; }
            set { _Context = value; OnPropertyChanged("Context"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Context;
        
        /// <summary>
        /// When the study was started
        /// </summary>
        [FhirElement("started", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime StartedElement
        {
            get { return _StartedElement; }
            set { _StartedElement = value; OnPropertyChanged("StartedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _StartedElement;
        
        /// <summary>
        /// When the study was started
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Started
        {
            get { return StartedElement != null ? StartedElement.Value : null; }
            set
            {
                if (value == null)
                    StartedElement = null;
                else
                    StartedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Started");
            }
        }
        
        /// <summary>
        /// Request fulfilled
        /// </summary>
        [FhirElement("basedOn", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [References("ReferralRequest","CarePlan","ProcedureRequest")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> BasedOn
        {
            get { if(_BasedOn==null) _BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(); return _BasedOn; }
            set { _BasedOn = value; OnPropertyChanged("BasedOn"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _BasedOn;
        
        /// <summary>
        /// Referring physician
        /// </summary>
        [FhirElement("referrer", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Referrer
        {
            get { return _Referrer; }
            set { _Referrer = value; OnPropertyChanged("Referrer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Referrer;
        
        /// <summary>
        /// Who interpreted images
        /// </summary>
        [FhirElement("interpreter", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
        [References("Practitioner")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Interpreter
        {
            get { if(_Interpreter==null) _Interpreter = new List<Hl7.Fhir.Model.ResourceReference>(); return _Interpreter; }
            set { _Interpreter = value; OnPropertyChanged("Interpreter"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Interpreter;
        
        /// <summary>
        /// Study access endpoint
        /// </summary>
        [FhirElement("endpoint", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
        [CLSCompliant(false)]
        [References("Endpoint")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Endpoint
        {
            get { if(_Endpoint==null) _Endpoint = new List<Hl7.Fhir.Model.ResourceReference>(); return _Endpoint; }
            set { _Endpoint = value; OnPropertyChanged("Endpoint"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Endpoint;
        
        /// <summary>
        /// Number of Study Related Series
        /// </summary>
        [FhirElement("numberOfSeries", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.UnsignedInt NumberOfSeriesElement
        {
            get { return _NumberOfSeriesElement; }
            set { _NumberOfSeriesElement = value; OnPropertyChanged("NumberOfSeriesElement"); }
        }
        
        private Hl7.Fhir.Model.UnsignedInt _NumberOfSeriesElement;
        
        /// <summary>
        /// Number of Study Related Series
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? NumberOfSeries
        {
            get { return NumberOfSeriesElement != null ? NumberOfSeriesElement.Value : null; }
            set
            {
                if (value == null)
                    NumberOfSeriesElement = null;
                else
                    NumberOfSeriesElement = new Hl7.Fhir.Model.UnsignedInt(value);
                OnPropertyChanged("NumberOfSeries");
            }
        }
        
        /// <summary>
        /// Number of Study Related Instances
        /// </summary>
        [FhirElement("numberOfInstances", InSummary=Hl7.Fhir.Model.Version.All, Order=220)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.UnsignedInt NumberOfInstancesElement
        {
            get { return _NumberOfInstancesElement; }
            set { _NumberOfInstancesElement = value; OnPropertyChanged("NumberOfInstancesElement"); }
        }
        
        private Hl7.Fhir.Model.UnsignedInt _NumberOfInstancesElement;
        
        /// <summary>
        /// Number of Study Related Instances
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? NumberOfInstances
        {
            get { return NumberOfInstancesElement != null ? NumberOfInstancesElement.Value : null; }
            set
            {
                if (value == null)
                    NumberOfInstancesElement = null;
                else
                    NumberOfInstancesElement = new Hl7.Fhir.Model.UnsignedInt(value);
                OnPropertyChanged("NumberOfInstances");
            }
        }
        
        /// <summary>
        /// The performed Procedure reference
        /// </summary>
        [FhirElement("procedureReference", InSummary=Hl7.Fhir.Model.Version.All, Order=230)]
        [CLSCompliant(false)]
        [References("Procedure")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ProcedureReference
        {
            get { if(_ProcedureReference==null) _ProcedureReference = new List<Hl7.Fhir.Model.ResourceReference>(); return _ProcedureReference; }
            set { _ProcedureReference = value; OnPropertyChanged("ProcedureReference"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _ProcedureReference;
        
        /// <summary>
        /// The performed procedure code
        /// </summary>
        [FhirElement("procedureCode", InSummary=Hl7.Fhir.Model.Version.All, Order=240)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ProcedureCode
        {
            get { if(_ProcedureCode==null) _ProcedureCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ProcedureCode; }
            set { _ProcedureCode = value; OnPropertyChanged("ProcedureCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ProcedureCode;
        
        /// <summary>
        /// Why the study was requested
        /// </summary>
        [FhirElement("reason", InSummary=Hl7.Fhir.Model.Version.All, Order=250)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Reason
        {
            get { return _Reason; }
            set { _Reason = value; OnPropertyChanged("Reason"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Reason;
        
        /// <summary>
        /// Institution-generated description
        /// </summary>
        [FhirElement("description", InSummary=Hl7.Fhir.Model.Version.All, Order=260)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// Institution-generated description
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
        /// Each study has one or more series of instances
        /// </summary>
        [FhirElement("series", InSummary=Hl7.Fhir.Model.Version.All, Order=270)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<SeriesComponent> Series
        {
            get { if(_Series==null) _Series = new List<SeriesComponent>(); return _Series; }
            set { _Series = value; OnPropertyChanged("Series"); }
        }
        
        private List<SeriesComponent> _Series;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ImagingStudy;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UidElement != null) dest.UidElement = (Hl7.Fhir.Model.Oid)UidElement.DeepCopy();
                if(Accession != null) dest.Accession = (Hl7.Fhir.Model.Identifier)Accession.DeepCopy();
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(AvailabilityElement != null) dest.AvailabilityElement = (Code<Hl7.Fhir.Model.InstanceAvailability>)AvailabilityElement.DeepCopy();
                if(ModalityList != null) dest.ModalityList = new List<Hl7.Fhir.Model.Coding>(ModalityList.DeepCopy());
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Context != null) dest.Context = (Hl7.Fhir.Model.ResourceReference)Context.DeepCopy();
                if(StartedElement != null) dest.StartedElement = (Hl7.Fhir.Model.FhirDateTime)StartedElement.DeepCopy();
                if(BasedOn != null) dest.BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(BasedOn.DeepCopy());
                if(Referrer != null) dest.Referrer = (Hl7.Fhir.Model.ResourceReference)Referrer.DeepCopy();
                if(Interpreter != null) dest.Interpreter = new List<Hl7.Fhir.Model.ResourceReference>(Interpreter.DeepCopy());
                if(Endpoint != null) dest.Endpoint = new List<Hl7.Fhir.Model.ResourceReference>(Endpoint.DeepCopy());
                if(NumberOfSeriesElement != null) dest.NumberOfSeriesElement = (Hl7.Fhir.Model.UnsignedInt)NumberOfSeriesElement.DeepCopy();
                if(NumberOfInstancesElement != null) dest.NumberOfInstancesElement = (Hl7.Fhir.Model.UnsignedInt)NumberOfInstancesElement.DeepCopy();
                if(ProcedureReference != null) dest.ProcedureReference = new List<Hl7.Fhir.Model.ResourceReference>(ProcedureReference.DeepCopy());
                if(ProcedureCode != null) dest.ProcedureCode = new List<Hl7.Fhir.Model.CodeableConcept>(ProcedureCode.DeepCopy());
                if(Reason != null) dest.Reason = (Hl7.Fhir.Model.CodeableConcept)Reason.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(Series != null) dest.Series = new List<SeriesComponent>(Series.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new ImagingStudy());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ImagingStudy;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(UidElement, otherT.UidElement)) return false;
            if( !DeepComparable.Matches(Accession, otherT.Accession)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(AvailabilityElement, otherT.AvailabilityElement)) return false;
            if( !DeepComparable.Matches(ModalityList, otherT.ModalityList)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Context, otherT.Context)) return false;
            if( !DeepComparable.Matches(StartedElement, otherT.StartedElement)) return false;
            if( !DeepComparable.Matches(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.Matches(Referrer, otherT.Referrer)) return false;
            if( !DeepComparable.Matches(Interpreter, otherT.Interpreter)) return false;
            if( !DeepComparable.Matches(Endpoint, otherT.Endpoint)) return false;
            if( !DeepComparable.Matches(NumberOfSeriesElement, otherT.NumberOfSeriesElement)) return false;
            if( !DeepComparable.Matches(NumberOfInstancesElement, otherT.NumberOfInstancesElement)) return false;
            if( !DeepComparable.Matches(ProcedureReference, otherT.ProcedureReference)) return false;
            if( !DeepComparable.Matches(ProcedureCode, otherT.ProcedureCode)) return false;
            if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(Series, otherT.Series)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ImagingStudy;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(UidElement, otherT.UidElement)) return false;
            if( !DeepComparable.IsExactly(Accession, otherT.Accession)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(AvailabilityElement, otherT.AvailabilityElement)) return false;
            if( !DeepComparable.IsExactly(ModalityList, otherT.ModalityList)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
            if( !DeepComparable.IsExactly(StartedElement, otherT.StartedElement)) return false;
            if( !DeepComparable.IsExactly(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.IsExactly(Referrer, otherT.Referrer)) return false;
            if( !DeepComparable.IsExactly(Interpreter, otherT.Interpreter)) return false;
            if( !DeepComparable.IsExactly(Endpoint, otherT.Endpoint)) return false;
            if( !DeepComparable.IsExactly(NumberOfSeriesElement, otherT.NumberOfSeriesElement)) return false;
            if( !DeepComparable.IsExactly(NumberOfInstancesElement, otherT.NumberOfInstancesElement)) return false;
            if( !DeepComparable.IsExactly(ProcedureReference, otherT.ProcedureReference)) return false;
            if( !DeepComparable.IsExactly(ProcedureCode, otherT.ProcedureCode)) return false;
            if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(Series, otherT.Series)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("ImagingStudy");
            base.Serialize(sink);
            sink.Element("uid", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); UidElement?.Serialize(sink);
            sink.Element("accession", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Accession?.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("availability", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); AvailabilityElement?.Serialize(sink);
            sink.BeginList("modalityList", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in ModalityList)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("patient", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Patient?.Serialize(sink);
            sink.Element("context", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Context?.Serialize(sink);
            sink.Element("started", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StartedElement?.Serialize(sink);
            sink.BeginList("basedOn", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in BasedOn)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("referrer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Referrer?.Serialize(sink);
            sink.BeginList("interpreter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Interpreter)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("endpoint", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Endpoint)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("numberOfSeries", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NumberOfSeriesElement?.Serialize(sink);
            sink.Element("numberOfInstances", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NumberOfInstancesElement?.Serialize(sink);
            sink.BeginList("procedureReference", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in ProcedureReference)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("procedureCode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in ProcedureCode)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("reason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Reason?.Serialize(sink);
            sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DescriptionElement?.Serialize(sink);
            sink.BeginList("series", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Series)
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
                case "uid":
                    UidElement = source.Get<Hl7.Fhir.Model.Oid>();
                    return true;
                case "accession":
                    Accession = source.Get<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "identifier":
                    Identifier = source.GetList<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "availability":
                    AvailabilityElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.InstanceAvailability>>();
                    return true;
                case "modalityList":
                    ModalityList = source.GetList<Hl7.Fhir.Model.Coding>();
                    return true;
                case "patient":
                    Patient = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "context":
                    Context = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "started":
                    StartedElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "basedOn":
                    BasedOn = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "referrer":
                    Referrer = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "interpreter":
                    Interpreter = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "endpoint":
                    Endpoint = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "numberOfSeries":
                    NumberOfSeriesElement = source.Get<Hl7.Fhir.Model.UnsignedInt>();
                    return true;
                case "numberOfInstances":
                    NumberOfInstancesElement = source.Get<Hl7.Fhir.Model.UnsignedInt>();
                    return true;
                case "procedureReference":
                    ProcedureReference = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "procedureCode":
                    ProcedureCode = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "reason":
                    Reason = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "description":
                    DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "series":
                    Series = source.GetList<SeriesComponent>();
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
                case "uid":
                    UidElement = source.PopulateValue(UidElement);
                    return true;
                case "_uid":
                    UidElement = source.Populate(UidElement);
                    return true;
                case "accession":
                    Accession = source.Populate(Accession);
                    return true;
                case "identifier":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "availability":
                    AvailabilityElement = source.PopulateValue(AvailabilityElement);
                    return true;
                case "_availability":
                    AvailabilityElement = source.Populate(AvailabilityElement);
                    return true;
                case "modalityList":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "patient":
                    Patient = source.Populate(Patient);
                    return true;
                case "context":
                    Context = source.Populate(Context);
                    return true;
                case "started":
                    StartedElement = source.PopulateValue(StartedElement);
                    return true;
                case "_started":
                    StartedElement = source.Populate(StartedElement);
                    return true;
                case "basedOn":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "referrer":
                    Referrer = source.Populate(Referrer);
                    return true;
                case "interpreter":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "endpoint":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "numberOfSeries":
                    NumberOfSeriesElement = source.PopulateValue(NumberOfSeriesElement);
                    return true;
                case "_numberOfSeries":
                    NumberOfSeriesElement = source.Populate(NumberOfSeriesElement);
                    return true;
                case "numberOfInstances":
                    NumberOfInstancesElement = source.PopulateValue(NumberOfInstancesElement);
                    return true;
                case "_numberOfInstances":
                    NumberOfInstancesElement = source.Populate(NumberOfInstancesElement);
                    return true;
                case "procedureReference":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "procedureCode":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "reason":
                    Reason = source.Populate(Reason);
                    return true;
                case "description":
                    DescriptionElement = source.PopulateValue(DescriptionElement);
                    return true;
                case "_description":
                    DescriptionElement = source.Populate(DescriptionElement);
                    return true;
                case "series":
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
                case "modalityList":
                    source.PopulateListItem(ModalityList, index);
                    return true;
                case "basedOn":
                    source.PopulateListItem(BasedOn, index);
                    return true;
                case "interpreter":
                    source.PopulateListItem(Interpreter, index);
                    return true;
                case "endpoint":
                    source.PopulateListItem(Endpoint, index);
                    return true;
                case "procedureReference":
                    source.PopulateListItem(ProcedureReference, index);
                    return true;
                case "procedureCode":
                    source.PopulateListItem(ProcedureCode, index);
                    return true;
                case "series":
                    source.PopulateListItem(Series, index);
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
                if (UidElement != null) yield return UidElement;
                if (Accession != null) yield return Accession;
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                if (AvailabilityElement != null) yield return AvailabilityElement;
                foreach (var elem in ModalityList) { if (elem != null) yield return elem; }
                if (Patient != null) yield return Patient;
                if (Context != null) yield return Context;
                if (StartedElement != null) yield return StartedElement;
                foreach (var elem in BasedOn) { if (elem != null) yield return elem; }
                if (Referrer != null) yield return Referrer;
                foreach (var elem in Interpreter) { if (elem != null) yield return elem; }
                foreach (var elem in Endpoint) { if (elem != null) yield return elem; }
                if (NumberOfSeriesElement != null) yield return NumberOfSeriesElement;
                if (NumberOfInstancesElement != null) yield return NumberOfInstancesElement;
                foreach (var elem in ProcedureReference) { if (elem != null) yield return elem; }
                foreach (var elem in ProcedureCode) { if (elem != null) yield return elem; }
                if (Reason != null) yield return Reason;
                if (DescriptionElement != null) yield return DescriptionElement;
                foreach (var elem in Series) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (UidElement != null) yield return new ElementValue("uid", UidElement);
                if (Accession != null) yield return new ElementValue("accession", Accession);
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (AvailabilityElement != null) yield return new ElementValue("availability", AvailabilityElement);
                foreach (var elem in ModalityList) { if (elem != null) yield return new ElementValue("modalityList", elem); }
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (Context != null) yield return new ElementValue("context", Context);
                if (StartedElement != null) yield return new ElementValue("started", StartedElement);
                foreach (var elem in BasedOn) { if (elem != null) yield return new ElementValue("basedOn", elem); }
                if (Referrer != null) yield return new ElementValue("referrer", Referrer);
                foreach (var elem in Interpreter) { if (elem != null) yield return new ElementValue("interpreter", elem); }
                foreach (var elem in Endpoint) { if (elem != null) yield return new ElementValue("endpoint", elem); }
                if (NumberOfSeriesElement != null) yield return new ElementValue("numberOfSeries", NumberOfSeriesElement);
                if (NumberOfInstancesElement != null) yield return new ElementValue("numberOfInstances", NumberOfInstancesElement);
                foreach (var elem in ProcedureReference) { if (elem != null) yield return new ElementValue("procedureReference", elem); }
                foreach (var elem in ProcedureCode) { if (elem != null) yield return new ElementValue("procedureCode", elem); }
                if (Reason != null) yield return new ElementValue("reason", Reason);
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                foreach (var elem in Series) { if (elem != null) yield return new ElementValue("series", elem); }
            }
        }
    
    }

}
