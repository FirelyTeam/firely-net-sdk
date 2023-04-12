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
    /// Base for elements defined inside a resource
    /// </summary>
    [DataContract]
    public abstract partial class BackboneElement : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "BackboneElement"; } }
    
        
        /// <summary>
        /// Extensions that cannot be ignored
        /// </summary>
        [FhirElement("modifierExtension", InSummary=Hl7.Fhir.Model.Version.All, Order=30)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Extension> ModifierExtension
        {
            get { if(_ModifierExtension==null) _ModifierExtension = new List<Hl7.Fhir.Model.Extension>(); return _ModifierExtension; }
            set { _ModifierExtension = value; OnPropertyChanged("ModifierExtension"); }
        }
        
        private List<Hl7.Fhir.Model.Extension> _ModifierExtension;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as BackboneElement;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(ModifierExtension != null) dest.ModifierExtension = new List<Hl7.Fhir.Model.Extension>(ModifierExtension.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as BackboneElement;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(ModifierExtension, otherT.ModifierExtension)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as BackboneElement;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(ModifierExtension, otherT.ModifierExtension)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            base.Serialize(sink);
            sink.BeginList("modifierExtension", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in ModifierExtension)
            {
                item?.Serialize(sink);
            }
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
                case "modifierExtension":
                    ModifierExtension = source.GetList<Hl7.Fhir.Model.Extension>();
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
                case "modifierExtension":
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
                case "modifierExtension":
                    source.PopulateListItem(ModifierExtension, index);
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
                foreach (var elem in ModifierExtension) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in ModifierExtension) { if (elem != null) yield return new ElementValue("modifierExtension", elem); }
            }
        }
    
    }

}
