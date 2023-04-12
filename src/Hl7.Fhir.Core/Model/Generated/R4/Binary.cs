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
    /// Pure binary content defined by a format other than FHIR
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "Binary", IsResource=true)]
    [DataContract]
    public partial class Binary : Hl7.Fhir.Model.Resource, Hl7.Fhir.Model.IBinary, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Binary; } }
        [NotMapped]
        public override string TypeName { get { return "Binary"; } }
    
        
        /// <summary>
        /// MimeType of the binary content
        /// </summary>
        [FhirElement("contentType", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Code ContentTypeElement
        {
            get { return _ContentTypeElement; }
            set { _ContentTypeElement = value; OnPropertyChanged("ContentTypeElement"); }
        }
        
        private Hl7.Fhir.Model.Code _ContentTypeElement;
        
        /// <summary>
        /// MimeType of the binary content
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ContentType
        {
            get { return ContentTypeElement != null ? ContentTypeElement.Value : null; }
            set
            {
                if (value == null)
                    ContentTypeElement = null;
                else
                    ContentTypeElement = new Hl7.Fhir.Model.Code(value);
                OnPropertyChanged("ContentType");
            }
        }
        
        /// <summary>
        /// Identifies another resource to use as proxy when enforcing access control
        /// </summary>
        [FhirElement("securityContext", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference SecurityContext
        {
            get { return _SecurityContext; }
            set { _SecurityContext = value; OnPropertyChanged("SecurityContext"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _SecurityContext;
        
        /// <summary>
        /// The actual content
        /// </summary>
        [FhirElement("data", Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.Base64Binary DataElement
        {
            get { return _DataElement; }
            set { _DataElement = value; OnPropertyChanged("DataElement"); }
        }
        
        private Hl7.Fhir.Model.Base64Binary _DataElement;
        
        /// <summary>
        /// The actual content
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public byte[] Data
        {
            get { return DataElement != null ? DataElement.Value : null; }
            set
            {
                if (value == null)
                    DataElement = null;
                else
                    DataElement = new Hl7.Fhir.Model.Base64Binary(value);
                OnPropertyChanged("Data");
            }
        }
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Binary;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(ContentTypeElement != null) dest.ContentTypeElement = (Hl7.Fhir.Model.Code)ContentTypeElement.DeepCopy();
                if(SecurityContext != null) dest.SecurityContext = (Hl7.Fhir.Model.ResourceReference)SecurityContext.DeepCopy();
                if(DataElement != null) dest.DataElement = (Hl7.Fhir.Model.Base64Binary)DataElement.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Binary());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Binary;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(ContentTypeElement, otherT.ContentTypeElement)) return false;
            if( !DeepComparable.Matches(SecurityContext, otherT.SecurityContext)) return false;
            if( !DeepComparable.Matches(DataElement, otherT.DataElement)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Binary;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(ContentTypeElement, otherT.ContentTypeElement)) return false;
            if( !DeepComparable.IsExactly(SecurityContext, otherT.SecurityContext)) return false;
            if( !DeepComparable.IsExactly(DataElement, otherT.DataElement)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Binary");
            base.Serialize(sink);
            sink.Element("contentType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); ContentTypeElement?.Serialize(sink);
            sink.Element("securityContext", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); SecurityContext?.Serialize(sink);
            sink.Element("data", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DataElement?.Serialize(sink);
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
                case "contentType":
                    ContentTypeElement = source.Get<Hl7.Fhir.Model.Code>();
                    return true;
                case "securityContext":
                    SecurityContext = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "data":
                    DataElement = source.Get<Hl7.Fhir.Model.Base64Binary>();
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
                case "contentType":
                    ContentTypeElement = source.PopulateValue(ContentTypeElement);
                    return true;
                case "_contentType":
                    ContentTypeElement = source.Populate(ContentTypeElement);
                    return true;
                case "securityContext":
                    SecurityContext = source.Populate(SecurityContext);
                    return true;
                case "data":
                    DataElement = source.PopulateValue(DataElement);
                    return true;
                case "_data":
                    DataElement = source.Populate(DataElement);
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
                if (ContentTypeElement != null) yield return ContentTypeElement;
                if (SecurityContext != null) yield return SecurityContext;
                if (DataElement != null) yield return DataElement;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (ContentTypeElement != null) yield return new ElementValue("contentType", ContentTypeElement);
                if (SecurityContext != null) yield return new ElementValue("securityContext", SecurityContext);
                if (DataElement != null) yield return new ElementValue("data", DataElement);
            }
        }
    
    }

}
