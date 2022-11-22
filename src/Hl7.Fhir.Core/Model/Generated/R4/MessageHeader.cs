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
    /// A resource that describes a message that is exchanged between systems
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "MessageHeader", IsResource=true)]
    [DataContract]
    public partial class MessageHeader : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IMessageHeader, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.MessageHeader; } }
        [NotMapped]
        public override string TypeName { get { return "MessageHeader"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "MessageDestinationComponent")]
        [DataContract]
        public partial class MessageDestinationComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IMessageHeaderMessageDestinationComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "MessageDestinationComponent"; } }
            
            /// <summary>
            /// Name of system
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
            /// Name of system
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
            /// Particular delivery destination within the destination
            /// </summary>
            [FhirElement("target", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [References("Device")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Target
            {
                get { return _Target; }
                set { _Target = value; OnPropertyChanged("Target"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Target;
            
            /// <summary>
            /// Actual destination address or id
            /// </summary>
            [FhirElement("endpoint", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Url EndpointElement
            {
                get { return _EndpointElement; }
                set { _EndpointElement = value; OnPropertyChanged("EndpointElement"); }
            }
            
            private Hl7.Fhir.Model.Url _EndpointElement;
            
            /// <summary>
            /// Actual destination address or id
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Endpoint
            {
                get { return EndpointElement != null ? EndpointElement.Value : null; }
                set
                {
                    if (value == null)
                        EndpointElement = null;
                    else
                        EndpointElement = new Hl7.Fhir.Model.Url(value);
                    OnPropertyChanged("Endpoint");
                }
            }
            
            /// <summary>
            /// Intended "real-world" recipient for the data
            /// </summary>
            [FhirElement("receiver", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [References("Practitioner","PractitionerRole","Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Receiver
            {
                get { return _Receiver; }
                set { _Receiver = value; OnPropertyChanged("Receiver"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Receiver;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("MessageDestinationComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NameElement?.Serialize(sink);
                sink.Element("target", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Target?.Serialize(sink);
                sink.Element("endpoint", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); EndpointElement?.Serialize(sink);
                sink.Element("receiver", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Receiver?.Serialize(sink);
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
                    case "target":
                        Target = source.Populate(Target);
                        return true;
                    case "endpoint":
                        EndpointElement = source.PopulateValue(EndpointElement);
                        return true;
                    case "_endpoint":
                        EndpointElement = source.Populate(EndpointElement);
                        return true;
                    case "receiver":
                        Receiver = source.Populate(Receiver);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MessageDestinationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(Target != null) dest.Target = (Hl7.Fhir.Model.ResourceReference)Target.DeepCopy();
                    if(EndpointElement != null) dest.EndpointElement = (Hl7.Fhir.Model.Url)EndpointElement.DeepCopy();
                    if(Receiver != null) dest.Receiver = (Hl7.Fhir.Model.ResourceReference)Receiver.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new MessageDestinationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as MessageDestinationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(Target, otherT.Target)) return false;
                if( !DeepComparable.Matches(EndpointElement, otherT.EndpointElement)) return false;
                if( !DeepComparable.Matches(Receiver, otherT.Receiver)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as MessageDestinationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
                if( !DeepComparable.IsExactly(EndpointElement, otherT.EndpointElement)) return false;
                if( !DeepComparable.IsExactly(Receiver, otherT.Receiver)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (Target != null) yield return Target;
                    if (EndpointElement != null) yield return EndpointElement;
                    if (Receiver != null) yield return Receiver;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (Target != null) yield return new ElementValue("target", Target);
                    if (EndpointElement != null) yield return new ElementValue("endpoint", EndpointElement);
                    if (Receiver != null) yield return new ElementValue("receiver", Receiver);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "MessageSourceComponent")]
        [DataContract]
        public partial class MessageSourceComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IMessageHeaderMessageSourceComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "MessageSourceComponent"; } }
            
            [NotMapped]
            Hl7.Fhir.Model.IContactPoint Hl7.Fhir.Model.IMessageHeaderMessageSourceComponent.Contact { get { return Contact; } }
            
            /// <summary>
            /// Name of system
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
            /// Name of system
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
            /// Name of software running the system
            /// </summary>
            [FhirElement("software", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SoftwareElement
            {
                get { return _SoftwareElement; }
                set { _SoftwareElement = value; OnPropertyChanged("SoftwareElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _SoftwareElement;
            
            /// <summary>
            /// Name of software running the system
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Software
            {
                get { return SoftwareElement != null ? SoftwareElement.Value : null; }
                set
                {
                    if (value == null)
                        SoftwareElement = null;
                    else
                        SoftwareElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Software");
                }
            }
            
            /// <summary>
            /// Version of software running
            /// </summary>
            [FhirElement("version", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VersionElement
            {
                get { return _VersionElement; }
                set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _VersionElement;
            
            /// <summary>
            /// Version of software running
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
            /// Human contact for problems
            /// </summary>
            [FhirElement("contact", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.R4.ContactPoint Contact
            {
                get { return _Contact; }
                set { _Contact = value; OnPropertyChanged("Contact"); }
            }
            
            private Hl7.Fhir.Model.R4.ContactPoint _Contact;
            
            /// <summary>
            /// Actual message source address or id
            /// </summary>
            [FhirElement("endpoint", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Url EndpointElement
            {
                get { return _EndpointElement; }
                set { _EndpointElement = value; OnPropertyChanged("EndpointElement"); }
            }
            
            private Hl7.Fhir.Model.Url _EndpointElement;
            
            /// <summary>
            /// Actual message source address or id
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Endpoint
            {
                get { return EndpointElement != null ? EndpointElement.Value : null; }
                set
                {
                    if (value == null)
                        EndpointElement = null;
                    else
                        EndpointElement = new Hl7.Fhir.Model.Url(value);
                    OnPropertyChanged("Endpoint");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("MessageSourceComponent");
                base.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NameElement?.Serialize(sink);
                sink.Element("software", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); SoftwareElement?.Serialize(sink);
                sink.Element("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VersionElement?.Serialize(sink);
                sink.Element("contact", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Contact?.Serialize(sink);
                sink.Element("endpoint", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); EndpointElement?.Serialize(sink);
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
                    case "software":
                        SoftwareElement = source.PopulateValue(SoftwareElement);
                        return true;
                    case "_software":
                        SoftwareElement = source.Populate(SoftwareElement);
                        return true;
                    case "version":
                        VersionElement = source.PopulateValue(VersionElement);
                        return true;
                    case "_version":
                        VersionElement = source.Populate(VersionElement);
                        return true;
                    case "contact":
                        Contact = source.Populate(Contact);
                        return true;
                    case "endpoint":
                        EndpointElement = source.PopulateValue(EndpointElement);
                        return true;
                    case "_endpoint":
                        EndpointElement = source.Populate(EndpointElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as MessageSourceComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(SoftwareElement != null) dest.SoftwareElement = (Hl7.Fhir.Model.FhirString)SoftwareElement.DeepCopy();
                    if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                    if(Contact != null) dest.Contact = (Hl7.Fhir.Model.R4.ContactPoint)Contact.DeepCopy();
                    if(EndpointElement != null) dest.EndpointElement = (Hl7.Fhir.Model.Url)EndpointElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new MessageSourceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as MessageSourceComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(SoftwareElement, otherT.SoftwareElement)) return false;
                if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
                if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
                if( !DeepComparable.Matches(EndpointElement, otherT.EndpointElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as MessageSourceComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(SoftwareElement, otherT.SoftwareElement)) return false;
                if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
                if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
                if( !DeepComparable.IsExactly(EndpointElement, otherT.EndpointElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (SoftwareElement != null) yield return SoftwareElement;
                    if (VersionElement != null) yield return VersionElement;
                    if (Contact != null) yield return Contact;
                    if (EndpointElement != null) yield return EndpointElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (SoftwareElement != null) yield return new ElementValue("software", SoftwareElement);
                    if (VersionElement != null) yield return new ElementValue("version", VersionElement);
                    if (Contact != null) yield return new ElementValue("contact", Contact);
                    if (EndpointElement != null) yield return new ElementValue("endpoint", EndpointElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ResponseComponent")]
        [DataContract]
        public partial class ResponseComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IMessageHeaderResponseComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ResponseComponent"; } }
            
            /// <summary>
            /// Id of original message
            /// </summary>
            [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id IdentifierElement
            {
                get { return _IdentifierElement; }
                set { _IdentifierElement = value; OnPropertyChanged("IdentifierElement"); }
            }
            
            private Hl7.Fhir.Model.Id _IdentifierElement;
            
            /// <summary>
            /// Id of original message
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
                        IdentifierElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Identifier");
                }
            }
            
            /// <summary>
            /// ok | transient-error | fatal-error
            /// </summary>
            [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ResponseType> CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ResponseType> _CodeElement;
            
            /// <summary>
            /// ok | transient-error | fatal-error
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ResponseType? Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (value == null)
                        CodeElement = null;
                    else
                        CodeElement = new Code<Hl7.Fhir.Model.ResponseType>(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Specific list of hints/warnings/errors
            /// </summary>
            [FhirElement("details", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [References("OperationOutcome")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Details
            {
                get { return _Details; }
                set { _Details = value; OnPropertyChanged("Details"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Details;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ResponseComponent");
                base.Serialize(sink);
                sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); IdentifierElement?.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); CodeElement?.Serialize(sink);
                sink.Element("details", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Details?.Serialize(sink);
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
                    case "code":
                        CodeElement = source.PopulateValue(CodeElement);
                        return true;
                    case "_code":
                        CodeElement = source.Populate(CodeElement);
                        return true;
                    case "details":
                        Details = source.Populate(Details);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ResponseComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(IdentifierElement != null) dest.IdentifierElement = (Hl7.Fhir.Model.Id)IdentifierElement.DeepCopy();
                    if(CodeElement != null) dest.CodeElement = (Code<Hl7.Fhir.Model.ResponseType>)CodeElement.DeepCopy();
                    if(Details != null) dest.Details = (Hl7.Fhir.Model.ResourceReference)Details.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ResponseComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ResponseComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(IdentifierElement, otherT.IdentifierElement)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(Details, otherT.Details)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ResponseComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(IdentifierElement, otherT.IdentifierElement)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(Details, otherT.Details)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (IdentifierElement != null) yield return IdentifierElement;
                    if (CodeElement != null) yield return CodeElement;
                    if (Details != null) yield return Details;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (IdentifierElement != null) yield return new ElementValue("identifier", IdentifierElement);
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (Details != null) yield return new ElementValue("details", Details);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IMessageHeaderMessageDestinationComponent> Hl7.Fhir.Model.IMessageHeader.Destination { get { return Destination; } }
        
        [NotMapped]
        Hl7.Fhir.Model.IMessageHeaderMessageSourceComponent Hl7.Fhir.Model.IMessageHeader.Source { get { return Source; } }
        
        [NotMapped]
        Hl7.Fhir.Model.IMessageHeaderResponseComponent Hl7.Fhir.Model.IMessageHeader.Response { get { return Response; } }
    
        
        /// <summary>
        /// Code for the event this message represents or link to event definition
        /// </summary>
        [FhirElement("event", InSummary=Hl7.Fhir.Model.Version.All, Order=90, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.FhirUri))]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Element Event
        {
            get { return _Event; }
            set { _Event = value; OnPropertyChanged("Event"); }
        }
        
        private Hl7.Fhir.Model.Element _Event;
        
        /// <summary>
        /// Message destination application(s)
        /// </summary>
        [FhirElement("destination", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<MessageDestinationComponent> Destination
        {
            get { if(_Destination==null) _Destination = new List<MessageDestinationComponent>(); return _Destination; }
            set { _Destination = value; OnPropertyChanged("Destination"); }
        }
        
        private List<MessageDestinationComponent> _Destination;
        
        /// <summary>
        /// Real world sender of the message
        /// </summary>
        [FhirElement("sender", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [References("Practitioner","PractitionerRole","Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Sender
        {
            get { return _Sender; }
            set { _Sender = value; OnPropertyChanged("Sender"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Sender;
        
        /// <summary>
        /// The source of the data entry
        /// </summary>
        [FhirElement("enterer", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [References("Practitioner","PractitionerRole")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Enterer
        {
            get { return _Enterer; }
            set { _Enterer = value; OnPropertyChanged("Enterer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Enterer;
        
        /// <summary>
        /// The source of the decision
        /// </summary>
        [FhirElement("author", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [References("Practitioner","PractitionerRole")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Author
        {
            get { return _Author; }
            set { _Author = value; OnPropertyChanged("Author"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Author;
        
        /// <summary>
        /// Message source application
        /// </summary>
        [FhirElement("source", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public MessageSourceComponent Source
        {
            get { return _Source; }
            set { _Source = value; OnPropertyChanged("Source"); }
        }
        
        private MessageSourceComponent _Source;
        
        /// <summary>
        /// Final responsibility for event
        /// </summary>
        [FhirElement("responsible", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [References("Practitioner","PractitionerRole","Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Responsible
        {
            get { return _Responsible; }
            set { _Responsible = value; OnPropertyChanged("Responsible"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Responsible;
        
        /// <summary>
        /// Cause of event
        /// </summary>
        [FhirElement("reason", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Reason
        {
            get { return _Reason; }
            set { _Reason = value; OnPropertyChanged("Reason"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Reason;
        
        /// <summary>
        /// If this is a reply to prior message
        /// </summary>
        [FhirElement("response", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [DataMember]
        public ResponseComponent Response
        {
            get { return _Response; }
            set { _Response = value; OnPropertyChanged("Response"); }
        }
        
        private ResponseComponent _Response;
        
        /// <summary>
        /// The actual content of the message
        /// </summary>
        [FhirElement("focus", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Focus
        {
            get { if(_Focus==null) _Focus = new List<Hl7.Fhir.Model.ResourceReference>(); return _Focus; }
            set { _Focus = value; OnPropertyChanged("Focus"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Focus;
        
        /// <summary>
        /// Link to the definition for this message
        /// </summary>
        [FhirElement("definition", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Canonical DefinitionElement
        {
            get { return _DefinitionElement; }
            set { _DefinitionElement = value; OnPropertyChanged("DefinitionElement"); }
        }
        
        private Hl7.Fhir.Model.Canonical _DefinitionElement;
        
        /// <summary>
        /// Link to the definition for this message
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
                    DefinitionElement = new Hl7.Fhir.Model.Canonical(value);
                OnPropertyChanged("Definition");
            }
        }
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MessageHeader;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Event != null) dest.Event = (Hl7.Fhir.Model.Element)Event.DeepCopy();
                if(Destination != null) dest.Destination = new List<MessageDestinationComponent>(Destination.DeepCopy());
                if(Sender != null) dest.Sender = (Hl7.Fhir.Model.ResourceReference)Sender.DeepCopy();
                if(Enterer != null) dest.Enterer = (Hl7.Fhir.Model.ResourceReference)Enterer.DeepCopy();
                if(Author != null) dest.Author = (Hl7.Fhir.Model.ResourceReference)Author.DeepCopy();
                if(Source != null) dest.Source = (MessageSourceComponent)Source.DeepCopy();
                if(Responsible != null) dest.Responsible = (Hl7.Fhir.Model.ResourceReference)Responsible.DeepCopy();
                if(Reason != null) dest.Reason = (Hl7.Fhir.Model.CodeableConcept)Reason.DeepCopy();
                if(Response != null) dest.Response = (ResponseComponent)Response.DeepCopy();
                if(Focus != null) dest.Focus = new List<Hl7.Fhir.Model.ResourceReference>(Focus.DeepCopy());
                if(DefinitionElement != null) dest.DefinitionElement = (Hl7.Fhir.Model.Canonical)DefinitionElement.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new MessageHeader());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as MessageHeader;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Event, otherT.Event)) return false;
            if( !DeepComparable.Matches(Destination, otherT.Destination)) return false;
            if( !DeepComparable.Matches(Sender, otherT.Sender)) return false;
            if( !DeepComparable.Matches(Enterer, otherT.Enterer)) return false;
            if( !DeepComparable.Matches(Author, otherT.Author)) return false;
            if( !DeepComparable.Matches(Source, otherT.Source)) return false;
            if( !DeepComparable.Matches(Responsible, otherT.Responsible)) return false;
            if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
            if( !DeepComparable.Matches(Response, otherT.Response)) return false;
            if( !DeepComparable.Matches(Focus, otherT.Focus)) return false;
            if( !DeepComparable.Matches(DefinitionElement, otherT.DefinitionElement)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MessageHeader;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Event, otherT.Event)) return false;
            if( !DeepComparable.IsExactly(Destination, otherT.Destination)) return false;
            if( !DeepComparable.IsExactly(Sender, otherT.Sender)) return false;
            if( !DeepComparable.IsExactly(Enterer, otherT.Enterer)) return false;
            if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
            if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
            if( !DeepComparable.IsExactly(Responsible, otherT.Responsible)) return false;
            if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
            if( !DeepComparable.IsExactly(Response, otherT.Response)) return false;
            if( !DeepComparable.IsExactly(Focus, otherT.Focus)) return false;
            if( !DeepComparable.IsExactly(DefinitionElement, otherT.DefinitionElement)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("MessageHeader");
            base.Serialize(sink);
            sink.Element("event", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, true); Event?.Serialize(sink);
            sink.BeginList("destination", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Destination)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("sender", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Sender?.Serialize(sink);
            sink.Element("enterer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Enterer?.Serialize(sink);
            sink.Element("author", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Author?.Serialize(sink);
            sink.Element("source", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Source?.Serialize(sink);
            sink.Element("responsible", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Responsible?.Serialize(sink);
            sink.Element("reason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Reason?.Serialize(sink);
            sink.Element("response", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Response?.Serialize(sink);
            sink.BeginList("focus", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Focus)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("definition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DefinitionElement?.Serialize(sink);
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
                case "eventCoding":
                    source.CheckDuplicates<Hl7.Fhir.Model.Coding>(Event, "event");
                    Event = source.Populate(Event as Hl7.Fhir.Model.Coding);
                    return true;
                case "eventUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Event, "event");
                    Event = source.PopulateValue(Event as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "_eventUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Event, "event");
                    Event = source.Populate(Event as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "destination":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "sender":
                    Sender = source.Populate(Sender);
                    return true;
                case "enterer":
                    Enterer = source.Populate(Enterer);
                    return true;
                case "author":
                    Author = source.Populate(Author);
                    return true;
                case "source":
                    Source = source.Populate(Source);
                    return true;
                case "responsible":
                    Responsible = source.Populate(Responsible);
                    return true;
                case "reason":
                    Reason = source.Populate(Reason);
                    return true;
                case "response":
                    Response = source.Populate(Response);
                    return true;
                case "focus":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "definition":
                    DefinitionElement = source.PopulateValue(DefinitionElement);
                    return true;
                case "_definition":
                    DefinitionElement = source.Populate(DefinitionElement);
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
                case "destination":
                    source.PopulateListItem(Destination, index);
                    return true;
                case "focus":
                    source.PopulateListItem(Focus, index);
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
                if (Event != null) yield return Event;
                foreach (var elem in Destination) { if (elem != null) yield return elem; }
                if (Sender != null) yield return Sender;
                if (Enterer != null) yield return Enterer;
                if (Author != null) yield return Author;
                if (Source != null) yield return Source;
                if (Responsible != null) yield return Responsible;
                if (Reason != null) yield return Reason;
                if (Response != null) yield return Response;
                foreach (var elem in Focus) { if (elem != null) yield return elem; }
                if (DefinitionElement != null) yield return DefinitionElement;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Event != null) yield return new ElementValue("event", Event);
                foreach (var elem in Destination) { if (elem != null) yield return new ElementValue("destination", elem); }
                if (Sender != null) yield return new ElementValue("sender", Sender);
                if (Enterer != null) yield return new ElementValue("enterer", Enterer);
                if (Author != null) yield return new ElementValue("author", Author);
                if (Source != null) yield return new ElementValue("source", Source);
                if (Responsible != null) yield return new ElementValue("responsible", Responsible);
                if (Reason != null) yield return new ElementValue("reason", Reason);
                if (Response != null) yield return new ElementValue("response", Response);
                foreach (var elem in Focus) { if (elem != null) yield return new ElementValue("focus", elem); }
                if (DefinitionElement != null) yield return new ElementValue("definition", DefinitionElement);
            }
        }
    
    }

}
