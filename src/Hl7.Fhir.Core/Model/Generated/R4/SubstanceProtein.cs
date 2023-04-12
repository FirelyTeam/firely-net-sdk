﻿using System;
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
    /// A SubstanceProtein is defined as a single unit of a linear amino acid sequence, or a combination of subunits that are either covalently linked or have a defined invariant stoichiometric relationship. This includes all synthetic, recombinant and purified SubstanceProteins of defined sequence, whether the use is therapeutic or prophylactic. This set of elements will be used to describe albumins, coagulation factors, cytokines, growth factors, peptide/SubstanceProtein hormones, enzymes, toxins, toxoids, recombinant vaccines, and immunomodulators
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "SubstanceProtein", IsResource=true)]
    [DataContract]
    public partial class SubstanceProtein : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.SubstanceProtein; } }
        [NotMapped]
        public override string TypeName { get { return "SubstanceProtein"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "SubunitComponent")]
        [DataContract]
        public partial class SubunitComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SubunitComponent"; } }
            
            /// <summary>
            /// Index of primary sequences of amino acids linked through peptide bonds in order of decreasing length. Sequences of the same length will be ordered by molecular weight. Subunits that have identical sequences will be repeated and have sequential subscripts
            /// </summary>
            [FhirElement("subunit", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Integer SubunitElement
            {
                get { return _SubunitElement; }
                set { _SubunitElement = value; OnPropertyChanged("SubunitElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _SubunitElement;
            
            /// <summary>
            /// Index of primary sequences of amino acids linked through peptide bonds in order of decreasing length. Sequences of the same length will be ordered by molecular weight. Subunits that have identical sequences will be repeated and have sequential subscripts
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Subunit
            {
                get { return SubunitElement != null ? SubunitElement.Value : null; }
                set
                {
                    if (value == null)
                        SubunitElement = null;
                    else
                        SubunitElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Subunit");
                }
            }
            
            /// <summary>
            /// The sequence information shall be provided enumerating the amino acids from N- to C-terminal end using standard single-letter amino acid codes. Uppercase shall be used for L-amino acids and lowercase for D-amino acids. Transcribed SubstanceProteins will always be described using the translated sequence; for synthetic peptide containing amino acids that are not represented with a single letter code an X should be used within the sequence. The modified amino acids will be distinguished by their position in the sequence
            /// </summary>
            [FhirElement("sequence", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _SequenceElement;
            
            /// <summary>
            /// The sequence information shall be provided enumerating the amino acids from N- to C-terminal end using standard single-letter amino acid codes. Uppercase shall be used for L-amino acids and lowercase for D-amino acids. Transcribed SubstanceProteins will always be described using the translated sequence; for synthetic peptide containing amino acids that are not represented with a single letter code an X should be used within the sequence. The modified amino acids will be distinguished by their position in the sequence
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if (value == null)
                        SequenceElement = null;
                    else
                        SequenceElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// Length of linear sequences of amino acids contained in the subunit
            /// </summary>
            [FhirElement("length", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Integer LengthElement
            {
                get { return _LengthElement; }
                set { _LengthElement = value; OnPropertyChanged("LengthElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _LengthElement;
            
            /// <summary>
            /// Length of linear sequences of amino acids contained in the subunit
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Length
            {
                get { return LengthElement != null ? LengthElement.Value : null; }
                set
                {
                    if (value == null)
                        LengthElement = null;
                    else
                        LengthElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Length");
                }
            }
            
            /// <summary>
            /// The sequence information shall be provided enumerating the amino acids from N- to C-terminal end using standard single-letter amino acid codes. Uppercase shall be used for L-amino acids and lowercase for D-amino acids. Transcribed SubstanceProteins will always be described using the translated sequence; for synthetic peptide containing amino acids that are not represented with a single letter code an X should be used within the sequence. The modified amino acids will be distinguished by their position in the sequence
            /// </summary>
            [FhirElement("sequenceAttachment", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Attachment SequenceAttachment
            {
                get { return _SequenceAttachment; }
                set { _SequenceAttachment = value; OnPropertyChanged("SequenceAttachment"); }
            }
            
            private Hl7.Fhir.Model.Attachment _SequenceAttachment;
            
            /// <summary>
            /// Unique identifier for molecular fragment modification based on the ISO 11238 Substance ID
            /// </summary>
            [FhirElement("nTerminalModificationId", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier NTerminalModificationId
            {
                get { return _NTerminalModificationId; }
                set { _NTerminalModificationId = value; OnPropertyChanged("NTerminalModificationId"); }
            }
            
            private Hl7.Fhir.Model.Identifier _NTerminalModificationId;
            
            /// <summary>
            /// The name of the fragment modified at the N-terminal of the SubstanceProtein shall be specified
            /// </summary>
            [FhirElement("nTerminalModification", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NTerminalModificationElement
            {
                get { return _NTerminalModificationElement; }
                set { _NTerminalModificationElement = value; OnPropertyChanged("NTerminalModificationElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NTerminalModificationElement;
            
            /// <summary>
            /// The name of the fragment modified at the N-terminal of the SubstanceProtein shall be specified
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string NTerminalModification
            {
                get { return NTerminalModificationElement != null ? NTerminalModificationElement.Value : null; }
                set
                {
                    if (value == null)
                        NTerminalModificationElement = null;
                    else
                        NTerminalModificationElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("NTerminalModification");
                }
            }
            
            /// <summary>
            /// Unique identifier for molecular fragment modification based on the ISO 11238 Substance ID
            /// </summary>
            [FhirElement("cTerminalModificationId", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier CTerminalModificationId
            {
                get { return _CTerminalModificationId; }
                set { _CTerminalModificationId = value; OnPropertyChanged("CTerminalModificationId"); }
            }
            
            private Hl7.Fhir.Model.Identifier _CTerminalModificationId;
            
            /// <summary>
            /// The modification at the C-terminal shall be specified
            /// </summary>
            [FhirElement("cTerminalModification", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CTerminalModificationElement
            {
                get { return _CTerminalModificationElement; }
                set { _CTerminalModificationElement = value; OnPropertyChanged("CTerminalModificationElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CTerminalModificationElement;
            
            /// <summary>
            /// The modification at the C-terminal shall be specified
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string CTerminalModification
            {
                get { return CTerminalModificationElement != null ? CTerminalModificationElement.Value : null; }
                set
                {
                    if (value == null)
                        CTerminalModificationElement = null;
                    else
                        CTerminalModificationElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("CTerminalModification");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SubunitComponent");
                base.Serialize(sink);
                sink.Element("subunit", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); SubunitElement?.Serialize(sink);
                sink.Element("sequence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); SequenceElement?.Serialize(sink);
                sink.Element("length", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); LengthElement?.Serialize(sink);
                sink.Element("sequenceAttachment", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); SequenceAttachment?.Serialize(sink);
                sink.Element("nTerminalModificationId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NTerminalModificationId?.Serialize(sink);
                sink.Element("nTerminalModification", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NTerminalModificationElement?.Serialize(sink);
                sink.Element("cTerminalModificationId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CTerminalModificationId?.Serialize(sink);
                sink.Element("cTerminalModification", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CTerminalModificationElement?.Serialize(sink);
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
                    case "subunit":
                        SubunitElement = source.Get<Hl7.Fhir.Model.Integer>();
                        return true;
                    case "sequence":
                        SequenceElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "length":
                        LengthElement = source.Get<Hl7.Fhir.Model.Integer>();
                        return true;
                    case "sequenceAttachment":
                        SequenceAttachment = source.Get<Hl7.Fhir.Model.Attachment>();
                        return true;
                    case "nTerminalModificationId":
                        NTerminalModificationId = source.Get<Hl7.Fhir.Model.Identifier>();
                        return true;
                    case "nTerminalModification":
                        NTerminalModificationElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "cTerminalModificationId":
                        CTerminalModificationId = source.Get<Hl7.Fhir.Model.Identifier>();
                        return true;
                    case "cTerminalModification":
                        CTerminalModificationElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "subunit":
                        SubunitElement = source.PopulateValue(SubunitElement);
                        return true;
                    case "_subunit":
                        SubunitElement = source.Populate(SubunitElement);
                        return true;
                    case "sequence":
                        SequenceElement = source.PopulateValue(SequenceElement);
                        return true;
                    case "_sequence":
                        SequenceElement = source.Populate(SequenceElement);
                        return true;
                    case "length":
                        LengthElement = source.PopulateValue(LengthElement);
                        return true;
                    case "_length":
                        LengthElement = source.Populate(LengthElement);
                        return true;
                    case "sequenceAttachment":
                        SequenceAttachment = source.Populate(SequenceAttachment);
                        return true;
                    case "nTerminalModificationId":
                        NTerminalModificationId = source.Populate(NTerminalModificationId);
                        return true;
                    case "nTerminalModification":
                        NTerminalModificationElement = source.PopulateValue(NTerminalModificationElement);
                        return true;
                    case "_nTerminalModification":
                        NTerminalModificationElement = source.Populate(NTerminalModificationElement);
                        return true;
                    case "cTerminalModificationId":
                        CTerminalModificationId = source.Populate(CTerminalModificationId);
                        return true;
                    case "cTerminalModification":
                        CTerminalModificationElement = source.PopulateValue(CTerminalModificationElement);
                        return true;
                    case "_cTerminalModification":
                        CTerminalModificationElement = source.Populate(CTerminalModificationElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SubunitComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SubunitElement != null) dest.SubunitElement = (Hl7.Fhir.Model.Integer)SubunitElement.DeepCopy();
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.FhirString)SequenceElement.DeepCopy();
                    if(LengthElement != null) dest.LengthElement = (Hl7.Fhir.Model.Integer)LengthElement.DeepCopy();
                    if(SequenceAttachment != null) dest.SequenceAttachment = (Hl7.Fhir.Model.Attachment)SequenceAttachment.DeepCopy();
                    if(NTerminalModificationId != null) dest.NTerminalModificationId = (Hl7.Fhir.Model.Identifier)NTerminalModificationId.DeepCopy();
                    if(NTerminalModificationElement != null) dest.NTerminalModificationElement = (Hl7.Fhir.Model.FhirString)NTerminalModificationElement.DeepCopy();
                    if(CTerminalModificationId != null) dest.CTerminalModificationId = (Hl7.Fhir.Model.Identifier)CTerminalModificationId.DeepCopy();
                    if(CTerminalModificationElement != null) dest.CTerminalModificationElement = (Hl7.Fhir.Model.FhirString)CTerminalModificationElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SubunitComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SubunitComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SubunitElement, otherT.SubunitElement)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(LengthElement, otherT.LengthElement)) return false;
                if( !DeepComparable.Matches(SequenceAttachment, otherT.SequenceAttachment)) return false;
                if( !DeepComparable.Matches(NTerminalModificationId, otherT.NTerminalModificationId)) return false;
                if( !DeepComparable.Matches(NTerminalModificationElement, otherT.NTerminalModificationElement)) return false;
                if( !DeepComparable.Matches(CTerminalModificationId, otherT.CTerminalModificationId)) return false;
                if( !DeepComparable.Matches(CTerminalModificationElement, otherT.CTerminalModificationElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SubunitComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SubunitElement, otherT.SubunitElement)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(LengthElement, otherT.LengthElement)) return false;
                if( !DeepComparable.IsExactly(SequenceAttachment, otherT.SequenceAttachment)) return false;
                if( !DeepComparable.IsExactly(NTerminalModificationId, otherT.NTerminalModificationId)) return false;
                if( !DeepComparable.IsExactly(NTerminalModificationElement, otherT.NTerminalModificationElement)) return false;
                if( !DeepComparable.IsExactly(CTerminalModificationId, otherT.CTerminalModificationId)) return false;
                if( !DeepComparable.IsExactly(CTerminalModificationElement, otherT.CTerminalModificationElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SubunitElement != null) yield return SubunitElement;
                    if (SequenceElement != null) yield return SequenceElement;
                    if (LengthElement != null) yield return LengthElement;
                    if (SequenceAttachment != null) yield return SequenceAttachment;
                    if (NTerminalModificationId != null) yield return NTerminalModificationId;
                    if (NTerminalModificationElement != null) yield return NTerminalModificationElement;
                    if (CTerminalModificationId != null) yield return CTerminalModificationId;
                    if (CTerminalModificationElement != null) yield return CTerminalModificationElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SubunitElement != null) yield return new ElementValue("subunit", SubunitElement);
                    if (SequenceElement != null) yield return new ElementValue("sequence", SequenceElement);
                    if (LengthElement != null) yield return new ElementValue("length", LengthElement);
                    if (SequenceAttachment != null) yield return new ElementValue("sequenceAttachment", SequenceAttachment);
                    if (NTerminalModificationId != null) yield return new ElementValue("nTerminalModificationId", NTerminalModificationId);
                    if (NTerminalModificationElement != null) yield return new ElementValue("nTerminalModification", NTerminalModificationElement);
                    if (CTerminalModificationId != null) yield return new ElementValue("cTerminalModificationId", CTerminalModificationId);
                    if (CTerminalModificationElement != null) yield return new ElementValue("cTerminalModification", CTerminalModificationElement);
                }
            }
        
        
        }
    
        
        /// <summary>
        /// The SubstanceProtein descriptive elements will only be used when a complete or partial amino acid sequence is available or derivable from a nucleic acid sequence
        /// </summary>
        [FhirElement("sequenceType", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept SequenceType
        {
            get { return _SequenceType; }
            set { _SequenceType = value; OnPropertyChanged("SequenceType"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _SequenceType;
        
        /// <summary>
        /// Number of linear sequences of amino acids linked through peptide bonds. The number of subunits constituting the SubstanceProtein shall be described. It is possible that the number of subunits can be variable
        /// </summary>
        [FhirElement("numberOfSubunits", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Integer NumberOfSubunitsElement
        {
            get { return _NumberOfSubunitsElement; }
            set { _NumberOfSubunitsElement = value; OnPropertyChanged("NumberOfSubunitsElement"); }
        }
        
        private Hl7.Fhir.Model.Integer _NumberOfSubunitsElement;
        
        /// <summary>
        /// Number of linear sequences of amino acids linked through peptide bonds. The number of subunits constituting the SubstanceProtein shall be described. It is possible that the number of subunits can be variable
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? NumberOfSubunits
        {
            get { return NumberOfSubunitsElement != null ? NumberOfSubunitsElement.Value : null; }
            set
            {
                if (value == null)
                    NumberOfSubunitsElement = null;
                else
                    NumberOfSubunitsElement = new Hl7.Fhir.Model.Integer(value);
                OnPropertyChanged("NumberOfSubunits");
            }
        }
        
        /// <summary>
        /// The disulphide bond between two cysteine residues either on the same subunit or on two different subunits shall be described. The position of the disulfide bonds in the SubstanceProtein shall be listed in increasing order of subunit number and position within subunit followed by the abbreviation of the amino acids involved. The disulfide linkage positions shall actually contain the amino acid Cysteine at the respective positions
        /// </summary>
        [FhirElement("disulfideLinkage", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> DisulfideLinkageElement
        {
            get { if(_DisulfideLinkageElement==null) _DisulfideLinkageElement = new List<Hl7.Fhir.Model.FhirString>(); return _DisulfideLinkageElement; }
            set { _DisulfideLinkageElement = value; OnPropertyChanged("DisulfideLinkageElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _DisulfideLinkageElement;
        
        /// <summary>
        /// The disulphide bond between two cysteine residues either on the same subunit or on two different subunits shall be described. The position of the disulfide bonds in the SubstanceProtein shall be listed in increasing order of subunit number and position within subunit followed by the abbreviation of the amino acids involved. The disulfide linkage positions shall actually contain the amino acid Cysteine at the respective positions
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> DisulfideLinkage
        {
            get { return DisulfideLinkageElement != null ? DisulfideLinkageElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    DisulfideLinkageElement = null;
                else
                    DisulfideLinkageElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("DisulfideLinkage");
            }
        }
        
        /// <summary>
        /// This subclause refers to the description of each subunit constituting the SubstanceProtein. A subunit is a linear sequence of amino acids linked through peptide bonds. The Subunit information shall be provided when the finished SubstanceProtein is a complex of multiple sequences; subunits are not used to delineate domains within a single sequence. Subunits are listed in order of decreasing length; sequences of the same length will be ordered by decreasing molecular weight; subunits that have identical sequences will be repeated multiple times
        /// </summary>
        [FhirElement("subunit", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<SubunitComponent> Subunit
        {
            get { if(_Subunit==null) _Subunit = new List<SubunitComponent>(); return _Subunit; }
            set { _Subunit = value; OnPropertyChanged("Subunit"); }
        }
        
        private List<SubunitComponent> _Subunit;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as SubstanceProtein;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(SequenceType != null) dest.SequenceType = (Hl7.Fhir.Model.CodeableConcept)SequenceType.DeepCopy();
                if(NumberOfSubunitsElement != null) dest.NumberOfSubunitsElement = (Hl7.Fhir.Model.Integer)NumberOfSubunitsElement.DeepCopy();
                if(DisulfideLinkageElement != null) dest.DisulfideLinkageElement = new List<Hl7.Fhir.Model.FhirString>(DisulfideLinkageElement.DeepCopy());
                if(Subunit != null) dest.Subunit = new List<SubunitComponent>(Subunit.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new SubstanceProtein());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as SubstanceProtein;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(SequenceType, otherT.SequenceType)) return false;
            if( !DeepComparable.Matches(NumberOfSubunitsElement, otherT.NumberOfSubunitsElement)) return false;
            if( !DeepComparable.Matches(DisulfideLinkageElement, otherT.DisulfideLinkageElement)) return false;
            if( !DeepComparable.Matches(Subunit, otherT.Subunit)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as SubstanceProtein;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(SequenceType, otherT.SequenceType)) return false;
            if( !DeepComparable.IsExactly(NumberOfSubunitsElement, otherT.NumberOfSubunitsElement)) return false;
            if( !DeepComparable.IsExactly(DisulfideLinkageElement, otherT.DisulfideLinkageElement)) return false;
            if( !DeepComparable.IsExactly(Subunit, otherT.Subunit)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("SubstanceProtein");
            base.Serialize(sink);
            sink.Element("sequenceType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); SequenceType?.Serialize(sink);
            sink.Element("numberOfSubunits", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NumberOfSubunitsElement?.Serialize(sink);
            sink.BeginList("disulfideLinkage", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(DisulfideLinkageElement);
            sink.End();
            sink.BeginList("subunit", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Subunit)
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
                case "sequenceType":
                    SequenceType = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "numberOfSubunits":
                    NumberOfSubunitsElement = source.Get<Hl7.Fhir.Model.Integer>();
                    return true;
                case "disulfideLinkage":
                    DisulfideLinkageElement = source.GetList<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "subunit":
                    Subunit = source.GetList<SubunitComponent>();
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
                case "sequenceType":
                    SequenceType = source.Populate(SequenceType);
                    return true;
                case "numberOfSubunits":
                    NumberOfSubunitsElement = source.PopulateValue(NumberOfSubunitsElement);
                    return true;
                case "_numberOfSubunits":
                    NumberOfSubunitsElement = source.Populate(NumberOfSubunitsElement);
                    return true;
                case "disulfideLinkage":
                case "_disulfideLinkage":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "subunit":
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
                case "disulfideLinkage":
                    source.PopulatePrimitiveListItemValue(DisulfideLinkageElement, index);
                    return true;
                case "_disulfideLinkage":
                    source.PopulatePrimitiveListItem(DisulfideLinkageElement, index);
                    return true;
                case "subunit":
                    source.PopulateListItem(Subunit, index);
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
                if (SequenceType != null) yield return SequenceType;
                if (NumberOfSubunitsElement != null) yield return NumberOfSubunitsElement;
                foreach (var elem in DisulfideLinkageElement) { if (elem != null) yield return elem; }
                foreach (var elem in Subunit) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (SequenceType != null) yield return new ElementValue("sequenceType", SequenceType);
                if (NumberOfSubunitsElement != null) yield return new ElementValue("numberOfSubunits", NumberOfSubunitsElement);
                foreach (var elem in DisulfideLinkageElement) { if (elem != null) yield return new ElementValue("disulfideLinkage", elem); }
                foreach (var elem in Subunit) { if (elem != null) yield return new ElementValue("subunit", elem); }
            }
        }
    
    }

}
