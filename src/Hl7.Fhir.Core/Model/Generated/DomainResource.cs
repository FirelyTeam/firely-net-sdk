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
    /// A resource with narrative, extensions, and contained resources
    /// </summary>
    [DataContract]
    public abstract partial class DomainResource : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.DomainResource; } }
        [NotMapped]
        public override string TypeName { get { return "DomainResource"; } }
    
        
        /// <summary>
        /// Text summary of the resource, for human interpretation
        /// </summary>
        [FhirElement("text", Order=50)]
        [DataMember]
        public Hl7.Fhir.Model.Narrative Text
        {
            get { return _Text; }
            set { _Text = value; OnPropertyChanged("Text"); }
        }
        
        private Hl7.Fhir.Model.Narrative _Text;
        
        /// <summary>
        /// Contained, inline Resources
        /// </summary>
        [FhirElement("contained", Order=60, Choice=ChoiceType.ResourceChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.Resource))]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Resource> Contained
        {
            get { if(_Contained==null) _Contained = new List<Hl7.Fhir.Model.Resource>(); return _Contained; }
            set { _Contained = value; OnPropertyChanged("Contained"); }
        }
        
        private List<Hl7.Fhir.Model.Resource> _Contained;
        
        /// <summary>
        /// Additional Content defined by implementations
        /// </summary>
        [FhirElement("extension", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Extension> Extension
        {
            get { if(_Extension==null) _Extension = new List<Hl7.Fhir.Model.Extension>(); return _Extension; }
            set { _Extension = value; OnPropertyChanged("Extension"); }
        }
        
        private List<Hl7.Fhir.Model.Extension> _Extension;
        
        /// <summary>
        /// Extensions that cannot be ignored
        /// </summary>
        [FhirElement("modifierExtension", Order=80)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Extension> ModifierExtension
        {
            get { if(_ModifierExtension==null) _ModifierExtension = new List<Hl7.Fhir.Model.Extension>(); return _ModifierExtension; }
            set { _ModifierExtension = value; OnPropertyChanged("ModifierExtension"); }
        }
        
        private List<Hl7.Fhir.Model.Extension> _ModifierExtension;
    
    
        public static ElementDefinitionConstraint[] DomainResource_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2,Hl7.Fhir.Model.Version.R4,Hl7.Fhir.Model.Version.STU3},
                key: "dom-4",
                severity: ConstraintSeverity.Warning,
                expression: "contained.meta.versionId.empty() and contained.meta.lastUpdated.empty()",
                human: "If a resource is contained in another resource, it SHALL NOT have a meta.versionId or a meta.lastUpdated",
                xpath: "not(exists(f:contained/*/f:meta/f:versionId)) and not(exists(f:contained/*/f:meta/f:lastUpdated))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2,Hl7.Fhir.Model.Version.STU3},
                key: "dom-3",
                severity: ConstraintSeverity.Warning,
                expression: "contained.where(('#'+id in %resource.descendants().reference).not()).empty()",
                human: "If the resource is contained in another resource, it SHALL be referred to from elsewhere in the resource",
                xpath: "not(exists(for $id in f:contained/*/@id return $id[not(ancestor::f:contained/parent::*/descendant::f:reference/@value=concat('#', $id))]))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2,Hl7.Fhir.Model.Version.R4,Hl7.Fhir.Model.Version.STU3},
                key: "dom-2",
                severity: ConstraintSeverity.Warning,
                expression: "contained.contained.empty()",
                human: "If the resource is contained in another resource, it SHALL NOT contain nested Resources",
                xpath: "not(parent::f:contained and f:contained)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2,Hl7.Fhir.Model.Version.STU3},
                key: "dom-1",
                severity: ConstraintSeverity.Warning,
                expression: "contained.text.empty()",
                human: "If the resource is contained in another resource, it SHALL NOT contain any narrative",
                xpath: "not(parent::f:contained and f:text)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "dom-3",
                severity: ConstraintSeverity.Warning,
                expression: "contained.where((('#'+id in (%resource.descendants().reference | %resource.descendants().as(canonical) | %resource.descendants().as(uri) | %resource.descendants().as(url))) or descendants().where(reference = '#').exists() or descendants().where(as(canonical) = '#').exists() or descendants().where(as(canonical) = '#').exists()).not()).trace('unmatched', id).empty()",
                human: "If the resource is contained in another resource, it SHALL be referred to from elsewhere in the resource or SHALL refer to the containing resource",
                xpath: "not(exists(for $id in f:contained/*/f:id/@value return $contained[not(parent::*/descendant::f:reference/@value=concat('#', $contained/*/id/@value) or descendant::f:reference[@value='#'])]))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "dom-6",
                severity: ConstraintSeverity.Warning,
                expression: "text.`div`.exists()",
                human: "A resource should have narrative for robust management",
                xpath: "exists(f:text/h:div)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "dom-5",
                severity: ConstraintSeverity.Warning,
                expression: "contained.meta.security.empty()",
                human: "If a resource is contained in another resource, it SHALL NOT have a security label",
                xpath: "not(exists(f:contained/*/f:meta/f:security))"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(DomainResource_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as DomainResource;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Text != null) dest.Text = (Hl7.Fhir.Model.Narrative)Text.DeepCopy();
                if(Contained != null) dest.Contained = new List<Hl7.Fhir.Model.Resource>(Contained.DeepCopy());
                if(Extension != null) dest.Extension = new List<Hl7.Fhir.Model.Extension>(Extension.DeepCopy());
                if(ModifierExtension != null) dest.ModifierExtension = new List<Hl7.Fhir.Model.Extension>(ModifierExtension.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as DomainResource;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Text, otherT.Text)) return false;
            if( !DeepComparable.Matches(Contained, otherT.Contained)) return false;
            if( !DeepComparable.Matches(Extension, otherT.Extension)) return false;
            if( !DeepComparable.Matches(ModifierExtension, otherT.ModifierExtension)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as DomainResource;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Text, otherT.Text)) return false;
            if( !DeepComparable.IsExactly(Contained, otherT.Contained)) return false;
            if( !DeepComparable.IsExactly(Extension, otherT.Extension)) return false;
            if( !DeepComparable.IsExactly(ModifierExtension, otherT.ModifierExtension)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            base.Serialize(sink);
            sink.Element("text", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Text?.Serialize(sink);
            sink.BeginList("contained", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Contained)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("extension", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Extension)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("modifierExtension", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in ModifierExtension)
            {
                item?.Serialize(sink);
            }
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
                case "text":
                    Text = source.Populate(Text);
                    return true;
                case "contained":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "extension":
                    source.SetList(this, jsonPropertyName);
                    return true;
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
                case "contained":
                    source.PopulateListItem(Contained, index);
                    return true;
                case "extension":
                    source.PopulateListItem(Extension, index);
                    return true;
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
                if (Text != null) yield return Text;
                foreach (var elem in Contained) { if (elem != null) yield return elem; }
                foreach (var elem in Extension) { if (elem != null) yield return elem; }
                foreach (var elem in ModifierExtension) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Text != null) yield return new ElementValue("text", Text);
                foreach (var elem in Contained) { if (elem != null) yield return new ElementValue("contained", elem); }
                foreach (var elem in Extension) { if (elem != null) yield return new ElementValue("extension", elem); }
                foreach (var elem in ModifierExtension) { if (elem != null) yield return new ElementValue("modifierExtension", elem); }
            }
        }
    
    }

}
