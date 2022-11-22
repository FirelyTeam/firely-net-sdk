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
    /// Information about a biological sequence
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "Sequence", IsResource=true)]
    [DataContract]
    public partial class Sequence : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Sequence; } }
        [NotMapped]
        public override string TypeName { get { return "Sequence"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "ReferenceSeqComponent")]
        [DataContract]
        public partial class ReferenceSeqComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ReferenceSeqComponent"; } }
            
            /// <summary>
            /// Chromosome containing genetic finding
            /// </summary>
            [FhirElement("chromosome", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Chromosome
            {
                get { return _Chromosome; }
                set { _Chromosome = value; OnPropertyChanged("Chromosome"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Chromosome;
            
            /// <summary>
            /// The Genome Build used for reference, following GRCh build versions e.g. 'GRCh 37'
            /// </summary>
            [FhirElement("genomeBuild", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString GenomeBuildElement
            {
                get { return _GenomeBuildElement; }
                set { _GenomeBuildElement = value; OnPropertyChanged("GenomeBuildElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _GenomeBuildElement;
            
            /// <summary>
            /// The Genome Build used for reference, following GRCh build versions e.g. 'GRCh 37'
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string GenomeBuild
            {
                get { return GenomeBuildElement != null ? GenomeBuildElement.Value : null; }
                set
                {
                    if (value == null)
                        GenomeBuildElement = null;
                    else
                        GenomeBuildElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("GenomeBuild");
                }
            }
            
            /// <summary>
            /// Reference identifier
            /// </summary>
            [FhirElement("referenceSeqId", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept ReferenceSeqId
            {
                get { return _ReferenceSeqId; }
                set { _ReferenceSeqId = value; OnPropertyChanged("ReferenceSeqId"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _ReferenceSeqId;
            
            /// <summary>
            /// A Pointer to another Sequence entity as reference sequence
            /// </summary>
            [FhirElement("referenceSeqPointer", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [References("Sequence")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference ReferenceSeqPointer
            {
                get { return _ReferenceSeqPointer; }
                set { _ReferenceSeqPointer = value; OnPropertyChanged("ReferenceSeqPointer"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _ReferenceSeqPointer;
            
            /// <summary>
            /// A string to represent reference sequence
            /// </summary>
            [FhirElement("referenceSeqString", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ReferenceSeqStringElement
            {
                get { return _ReferenceSeqStringElement; }
                set { _ReferenceSeqStringElement = value; OnPropertyChanged("ReferenceSeqStringElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ReferenceSeqStringElement;
            
            /// <summary>
            /// A string to represent reference sequence
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ReferenceSeqString
            {
                get { return ReferenceSeqStringElement != null ? ReferenceSeqStringElement.Value : null; }
                set
                {
                    if (value == null)
                        ReferenceSeqStringElement = null;
                    else
                        ReferenceSeqStringElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ReferenceSeqString");
                }
            }
            
            /// <summary>
            /// Directionality of DNA ( +1/-1)
            /// </summary>
            [FhirElement("strand", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Integer StrandElement
            {
                get { return _StrandElement; }
                set { _StrandElement = value; OnPropertyChanged("StrandElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _StrandElement;
            
            /// <summary>
            /// Directionality of DNA ( +1/-1)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Strand
            {
                get { return StrandElement != null ? StrandElement.Value : null; }
                set
                {
                    if (value == null)
                        StrandElement = null;
                    else
                        StrandElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Strand");
                }
            }
            
            /// <summary>
            /// Start position of the window on the  reference sequence
            /// </summary>
            [FhirElement("windowStart", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Integer WindowStartElement
            {
                get { return _WindowStartElement; }
                set { _WindowStartElement = value; OnPropertyChanged("WindowStartElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _WindowStartElement;
            
            /// <summary>
            /// Start position of the window on the  reference sequence
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? WindowStart
            {
                get { return WindowStartElement != null ? WindowStartElement.Value : null; }
                set
                {
                    if (value == null)
                        WindowStartElement = null;
                    else
                        WindowStartElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("WindowStart");
                }
            }
            
            /// <summary>
            /// End position of the window on the reference sequence
            /// </summary>
            [FhirElement("windowEnd", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Integer WindowEndElement
            {
                get { return _WindowEndElement; }
                set { _WindowEndElement = value; OnPropertyChanged("WindowEndElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _WindowEndElement;
            
            /// <summary>
            /// End position of the window on the reference sequence
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? WindowEnd
            {
                get { return WindowEndElement != null ? WindowEndElement.Value : null; }
                set
                {
                    if (value == null)
                        WindowEndElement = null;
                    else
                        WindowEndElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("WindowEnd");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ReferenceSeqComponent");
                base.Serialize(sink);
                sink.Element("chromosome", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Chromosome?.Serialize(sink);
                sink.Element("genomeBuild", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); GenomeBuildElement?.Serialize(sink);
                sink.Element("referenceSeqId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ReferenceSeqId?.Serialize(sink);
                sink.Element("referenceSeqPointer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ReferenceSeqPointer?.Serialize(sink);
                sink.Element("referenceSeqString", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ReferenceSeqStringElement?.Serialize(sink);
                sink.Element("strand", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StrandElement?.Serialize(sink);
                sink.Element("windowStart", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); WindowStartElement?.Serialize(sink);
                sink.Element("windowEnd", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); WindowEndElement?.Serialize(sink);
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
                    case "chromosome":
                        Chromosome = source.Populate(Chromosome);
                        return true;
                    case "genomeBuild":
                        GenomeBuildElement = source.PopulateValue(GenomeBuildElement);
                        return true;
                    case "_genomeBuild":
                        GenomeBuildElement = source.Populate(GenomeBuildElement);
                        return true;
                    case "referenceSeqId":
                        ReferenceSeqId = source.Populate(ReferenceSeqId);
                        return true;
                    case "referenceSeqPointer":
                        ReferenceSeqPointer = source.Populate(ReferenceSeqPointer);
                        return true;
                    case "referenceSeqString":
                        ReferenceSeqStringElement = source.PopulateValue(ReferenceSeqStringElement);
                        return true;
                    case "_referenceSeqString":
                        ReferenceSeqStringElement = source.Populate(ReferenceSeqStringElement);
                        return true;
                    case "strand":
                        StrandElement = source.PopulateValue(StrandElement);
                        return true;
                    case "_strand":
                        StrandElement = source.Populate(StrandElement);
                        return true;
                    case "windowStart":
                        WindowStartElement = source.PopulateValue(WindowStartElement);
                        return true;
                    case "_windowStart":
                        WindowStartElement = source.Populate(WindowStartElement);
                        return true;
                    case "windowEnd":
                        WindowEndElement = source.PopulateValue(WindowEndElement);
                        return true;
                    case "_windowEnd":
                        WindowEndElement = source.Populate(WindowEndElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ReferenceSeqComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Chromosome != null) dest.Chromosome = (Hl7.Fhir.Model.CodeableConcept)Chromosome.DeepCopy();
                    if(GenomeBuildElement != null) dest.GenomeBuildElement = (Hl7.Fhir.Model.FhirString)GenomeBuildElement.DeepCopy();
                    if(ReferenceSeqId != null) dest.ReferenceSeqId = (Hl7.Fhir.Model.CodeableConcept)ReferenceSeqId.DeepCopy();
                    if(ReferenceSeqPointer != null) dest.ReferenceSeqPointer = (Hl7.Fhir.Model.ResourceReference)ReferenceSeqPointer.DeepCopy();
                    if(ReferenceSeqStringElement != null) dest.ReferenceSeqStringElement = (Hl7.Fhir.Model.FhirString)ReferenceSeqStringElement.DeepCopy();
                    if(StrandElement != null) dest.StrandElement = (Hl7.Fhir.Model.Integer)StrandElement.DeepCopy();
                    if(WindowStartElement != null) dest.WindowStartElement = (Hl7.Fhir.Model.Integer)WindowStartElement.DeepCopy();
                    if(WindowEndElement != null) dest.WindowEndElement = (Hl7.Fhir.Model.Integer)WindowEndElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ReferenceSeqComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ReferenceSeqComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Chromosome, otherT.Chromosome)) return false;
                if( !DeepComparable.Matches(GenomeBuildElement, otherT.GenomeBuildElement)) return false;
                if( !DeepComparable.Matches(ReferenceSeqId, otherT.ReferenceSeqId)) return false;
                if( !DeepComparable.Matches(ReferenceSeqPointer, otherT.ReferenceSeqPointer)) return false;
                if( !DeepComparable.Matches(ReferenceSeqStringElement, otherT.ReferenceSeqStringElement)) return false;
                if( !DeepComparable.Matches(StrandElement, otherT.StrandElement)) return false;
                if( !DeepComparable.Matches(WindowStartElement, otherT.WindowStartElement)) return false;
                if( !DeepComparable.Matches(WindowEndElement, otherT.WindowEndElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ReferenceSeqComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Chromosome, otherT.Chromosome)) return false;
                if( !DeepComparable.IsExactly(GenomeBuildElement, otherT.GenomeBuildElement)) return false;
                if( !DeepComparable.IsExactly(ReferenceSeqId, otherT.ReferenceSeqId)) return false;
                if( !DeepComparable.IsExactly(ReferenceSeqPointer, otherT.ReferenceSeqPointer)) return false;
                if( !DeepComparable.IsExactly(ReferenceSeqStringElement, otherT.ReferenceSeqStringElement)) return false;
                if( !DeepComparable.IsExactly(StrandElement, otherT.StrandElement)) return false;
                if( !DeepComparable.IsExactly(WindowStartElement, otherT.WindowStartElement)) return false;
                if( !DeepComparable.IsExactly(WindowEndElement, otherT.WindowEndElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Chromosome != null) yield return Chromosome;
                    if (GenomeBuildElement != null) yield return GenomeBuildElement;
                    if (ReferenceSeqId != null) yield return ReferenceSeqId;
                    if (ReferenceSeqPointer != null) yield return ReferenceSeqPointer;
                    if (ReferenceSeqStringElement != null) yield return ReferenceSeqStringElement;
                    if (StrandElement != null) yield return StrandElement;
                    if (WindowStartElement != null) yield return WindowStartElement;
                    if (WindowEndElement != null) yield return WindowEndElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Chromosome != null) yield return new ElementValue("chromosome", Chromosome);
                    if (GenomeBuildElement != null) yield return new ElementValue("genomeBuild", GenomeBuildElement);
                    if (ReferenceSeqId != null) yield return new ElementValue("referenceSeqId", ReferenceSeqId);
                    if (ReferenceSeqPointer != null) yield return new ElementValue("referenceSeqPointer", ReferenceSeqPointer);
                    if (ReferenceSeqStringElement != null) yield return new ElementValue("referenceSeqString", ReferenceSeqStringElement);
                    if (StrandElement != null) yield return new ElementValue("strand", StrandElement);
                    if (WindowStartElement != null) yield return new ElementValue("windowStart", WindowStartElement);
                    if (WindowEndElement != null) yield return new ElementValue("windowEnd", WindowEndElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "VariantComponent")]
        [DataContract]
        public partial class VariantComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "VariantComponent"; } }
            
            /// <summary>
            /// Start position of the variant on the  reference sequence
            /// </summary>
            [FhirElement("start", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Integer StartElement
            {
                get { return _StartElement; }
                set { _StartElement = value; OnPropertyChanged("StartElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _StartElement;
            
            /// <summary>
            /// Start position of the variant on the  reference sequence
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Start
            {
                get { return StartElement != null ? StartElement.Value : null; }
                set
                {
                    if (value == null)
                        StartElement = null;
                    else
                        StartElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Start");
                }
            }
            
            /// <summary>
            /// End position of the variant on the reference sequence
            /// </summary>
            [FhirElement("end", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Integer EndElement
            {
                get { return _EndElement; }
                set { _EndElement = value; OnPropertyChanged("EndElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _EndElement;
            
            /// <summary>
            /// End position of the variant on the reference sequence
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? End
            {
                get { return EndElement != null ? EndElement.Value : null; }
                set
                {
                    if (value == null)
                        EndElement = null;
                    else
                        EndElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("End");
                }
            }
            
            /// <summary>
            /// Allele that was observed
            /// </summary>
            [FhirElement("observedAllele", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ObservedAlleleElement
            {
                get { return _ObservedAlleleElement; }
                set { _ObservedAlleleElement = value; OnPropertyChanged("ObservedAlleleElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ObservedAlleleElement;
            
            /// <summary>
            /// Allele that was observed
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ObservedAllele
            {
                get { return ObservedAlleleElement != null ? ObservedAlleleElement.Value : null; }
                set
                {
                    if (value == null)
                        ObservedAlleleElement = null;
                    else
                        ObservedAlleleElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ObservedAllele");
                }
            }
            
            /// <summary>
            /// Allele in the reference sequence
            /// </summary>
            [FhirElement("referenceAllele", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ReferenceAlleleElement
            {
                get { return _ReferenceAlleleElement; }
                set { _ReferenceAlleleElement = value; OnPropertyChanged("ReferenceAlleleElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ReferenceAlleleElement;
            
            /// <summary>
            /// Allele in the reference sequence
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ReferenceAllele
            {
                get { return ReferenceAlleleElement != null ? ReferenceAlleleElement.Value : null; }
                set
                {
                    if (value == null)
                        ReferenceAlleleElement = null;
                    else
                        ReferenceAlleleElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ReferenceAllele");
                }
            }
            
            /// <summary>
            /// Extended CIGAR string for aligning the sequence with reference bases
            /// </summary>
            [FhirElement("cigar", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CigarElement
            {
                get { return _CigarElement; }
                set { _CigarElement = value; OnPropertyChanged("CigarElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CigarElement;
            
            /// <summary>
            /// Extended CIGAR string for aligning the sequence with reference bases
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Cigar
            {
                get { return CigarElement != null ? CigarElement.Value : null; }
                set
                {
                    if (value == null)
                        CigarElement = null;
                    else
                        CigarElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Cigar");
                }
            }
            
            /// <summary>
            /// Pointer to observed variant information
            /// </summary>
            [FhirElement("variantPointer", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
            [CLSCompliant(false)]
            [References("Observation")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference VariantPointer
            {
                get { return _VariantPointer; }
                set { _VariantPointer = value; OnPropertyChanged("VariantPointer"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _VariantPointer;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("VariantComponent");
                base.Serialize(sink);
                sink.Element("start", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StartElement?.Serialize(sink);
                sink.Element("end", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); EndElement?.Serialize(sink);
                sink.Element("observedAllele", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ObservedAlleleElement?.Serialize(sink);
                sink.Element("referenceAllele", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ReferenceAlleleElement?.Serialize(sink);
                sink.Element("cigar", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CigarElement?.Serialize(sink);
                sink.Element("variantPointer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VariantPointer?.Serialize(sink);
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
                    case "start":
                        StartElement = source.PopulateValue(StartElement);
                        return true;
                    case "_start":
                        StartElement = source.Populate(StartElement);
                        return true;
                    case "end":
                        EndElement = source.PopulateValue(EndElement);
                        return true;
                    case "_end":
                        EndElement = source.Populate(EndElement);
                        return true;
                    case "observedAllele":
                        ObservedAlleleElement = source.PopulateValue(ObservedAlleleElement);
                        return true;
                    case "_observedAllele":
                        ObservedAlleleElement = source.Populate(ObservedAlleleElement);
                        return true;
                    case "referenceAllele":
                        ReferenceAlleleElement = source.PopulateValue(ReferenceAlleleElement);
                        return true;
                    case "_referenceAllele":
                        ReferenceAlleleElement = source.Populate(ReferenceAlleleElement);
                        return true;
                    case "cigar":
                        CigarElement = source.PopulateValue(CigarElement);
                        return true;
                    case "_cigar":
                        CigarElement = source.Populate(CigarElement);
                        return true;
                    case "variantPointer":
                        VariantPointer = source.Populate(VariantPointer);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as VariantComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(StartElement != null) dest.StartElement = (Hl7.Fhir.Model.Integer)StartElement.DeepCopy();
                    if(EndElement != null) dest.EndElement = (Hl7.Fhir.Model.Integer)EndElement.DeepCopy();
                    if(ObservedAlleleElement != null) dest.ObservedAlleleElement = (Hl7.Fhir.Model.FhirString)ObservedAlleleElement.DeepCopy();
                    if(ReferenceAlleleElement != null) dest.ReferenceAlleleElement = (Hl7.Fhir.Model.FhirString)ReferenceAlleleElement.DeepCopy();
                    if(CigarElement != null) dest.CigarElement = (Hl7.Fhir.Model.FhirString)CigarElement.DeepCopy();
                    if(VariantPointer != null) dest.VariantPointer = (Hl7.Fhir.Model.ResourceReference)VariantPointer.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new VariantComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as VariantComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(StartElement, otherT.StartElement)) return false;
                if( !DeepComparable.Matches(EndElement, otherT.EndElement)) return false;
                if( !DeepComparable.Matches(ObservedAlleleElement, otherT.ObservedAlleleElement)) return false;
                if( !DeepComparable.Matches(ReferenceAlleleElement, otherT.ReferenceAlleleElement)) return false;
                if( !DeepComparable.Matches(CigarElement, otherT.CigarElement)) return false;
                if( !DeepComparable.Matches(VariantPointer, otherT.VariantPointer)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as VariantComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(StartElement, otherT.StartElement)) return false;
                if( !DeepComparable.IsExactly(EndElement, otherT.EndElement)) return false;
                if( !DeepComparable.IsExactly(ObservedAlleleElement, otherT.ObservedAlleleElement)) return false;
                if( !DeepComparable.IsExactly(ReferenceAlleleElement, otherT.ReferenceAlleleElement)) return false;
                if( !DeepComparable.IsExactly(CigarElement, otherT.CigarElement)) return false;
                if( !DeepComparable.IsExactly(VariantPointer, otherT.VariantPointer)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (StartElement != null) yield return StartElement;
                    if (EndElement != null) yield return EndElement;
                    if (ObservedAlleleElement != null) yield return ObservedAlleleElement;
                    if (ReferenceAlleleElement != null) yield return ReferenceAlleleElement;
                    if (CigarElement != null) yield return CigarElement;
                    if (VariantPointer != null) yield return VariantPointer;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (StartElement != null) yield return new ElementValue("start", StartElement);
                    if (EndElement != null) yield return new ElementValue("end", EndElement);
                    if (ObservedAlleleElement != null) yield return new ElementValue("observedAllele", ObservedAlleleElement);
                    if (ReferenceAlleleElement != null) yield return new ElementValue("referenceAllele", ReferenceAlleleElement);
                    if (CigarElement != null) yield return new ElementValue("cigar", CigarElement);
                    if (VariantPointer != null) yield return new ElementValue("variantPointer", VariantPointer);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "QualityComponent")]
        [DataContract]
        public partial class QualityComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "QualityComponent"; } }
            
            /// <summary>
            /// indel | snp | unknown
            /// </summary>
            [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.qualityType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.qualityType> _TypeElement;
            
            /// <summary>
            /// indel | snp | unknown
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.qualityType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (value == null)
                        TypeElement = null;
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.qualityType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Standard sequence for comparison
            /// </summary>
            [FhirElement("standardSequence", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept StandardSequence
            {
                get { return _StandardSequence; }
                set { _StandardSequence = value; OnPropertyChanged("StandardSequence"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _StandardSequence;
            
            /// <summary>
            /// Start position of the sequence
            /// </summary>
            [FhirElement("start", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Integer StartElement
            {
                get { return _StartElement; }
                set { _StartElement = value; OnPropertyChanged("StartElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _StartElement;
            
            /// <summary>
            /// Start position of the sequence
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Start
            {
                get { return StartElement != null ? StartElement.Value : null; }
                set
                {
                    if (value == null)
                        StartElement = null;
                    else
                        StartElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Start");
                }
            }
            
            /// <summary>
            /// End position of the sequence
            /// </summary>
            [FhirElement("end", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Integer EndElement
            {
                get { return _EndElement; }
                set { _EndElement = value; OnPropertyChanged("EndElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _EndElement;
            
            /// <summary>
            /// End position of the sequence
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? End
            {
                get { return EndElement != null ? EndElement.Value : null; }
                set
                {
                    if (value == null)
                        EndElement = null;
                    else
                        EndElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("End");
                }
            }
            
            /// <summary>
            /// Quality score for the comparison
            /// </summary>
            [FhirElement("score", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Score
            {
                get { return _Score; }
                set { _Score = value; OnPropertyChanged("Score"); }
            }
            
            private Hl7.Fhir.Model.Quantity _Score;
            
            /// <summary>
            /// Method to get quality
            /// </summary>
            [FhirElement("method", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Method
            {
                get { return _Method; }
                set { _Method = value; OnPropertyChanged("Method"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Method;
            
            /// <summary>
            /// True positives from the perspective of the truth data
            /// </summary>
            [FhirElement("truthTP", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal TruthTPElement
            {
                get { return _TruthTPElement; }
                set { _TruthTPElement = value; OnPropertyChanged("TruthTPElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _TruthTPElement;
            
            /// <summary>
            /// True positives from the perspective of the truth data
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? TruthTP
            {
                get { return TruthTPElement != null ? TruthTPElement.Value : null; }
                set
                {
                    if (value == null)
                        TruthTPElement = null;
                    else
                        TruthTPElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("TruthTP");
                }
            }
            
            /// <summary>
            /// True positives from the perspective of the query data
            /// </summary>
            [FhirElement("queryTP", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal QueryTPElement
            {
                get { return _QueryTPElement; }
                set { _QueryTPElement = value; OnPropertyChanged("QueryTPElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _QueryTPElement;
            
            /// <summary>
            /// True positives from the perspective of the query data
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? QueryTP
            {
                get { return QueryTPElement != null ? QueryTPElement.Value : null; }
                set
                {
                    if (value == null)
                        QueryTPElement = null;
                    else
                        QueryTPElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("QueryTP");
                }
            }
            
            /// <summary>
            /// False negatives
            /// </summary>
            [FhirElement("truthFN", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal TruthFNElement
            {
                get { return _TruthFNElement; }
                set { _TruthFNElement = value; OnPropertyChanged("TruthFNElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _TruthFNElement;
            
            /// <summary>
            /// False negatives
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? TruthFN
            {
                get { return TruthFNElement != null ? TruthFNElement.Value : null; }
                set
                {
                    if (value == null)
                        TruthFNElement = null;
                    else
                        TruthFNElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("TruthFN");
                }
            }
            
            /// <summary>
            /// False positives
            /// </summary>
            [FhirElement("queryFP", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal QueryFPElement
            {
                get { return _QueryFPElement; }
                set { _QueryFPElement = value; OnPropertyChanged("QueryFPElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _QueryFPElement;
            
            /// <summary>
            /// False positives
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? QueryFP
            {
                get { return QueryFPElement != null ? QueryFPElement.Value : null; }
                set
                {
                    if (value == null)
                        QueryFPElement = null;
                    else
                        QueryFPElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("QueryFP");
                }
            }
            
            /// <summary>
            /// False positives where the non-REF alleles in the Truth and Query Call Sets match
            /// </summary>
            [FhirElement("gtFP", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal GtFPElement
            {
                get { return _GtFPElement; }
                set { _GtFPElement = value; OnPropertyChanged("GtFPElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _GtFPElement;
            
            /// <summary>
            /// False positives where the non-REF alleles in the Truth and Query Call Sets match
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? GtFP
            {
                get { return GtFPElement != null ? GtFPElement.Value : null; }
                set
                {
                    if (value == null)
                        GtFPElement = null;
                    else
                        GtFPElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("GtFP");
                }
            }
            
            /// <summary>
            /// Precision of comparison
            /// </summary>
            [FhirElement("precision", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal PrecisionElement
            {
                get { return _PrecisionElement; }
                set { _PrecisionElement = value; OnPropertyChanged("PrecisionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _PrecisionElement;
            
            /// <summary>
            /// Precision of comparison
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Precision
            {
                get { return PrecisionElement != null ? PrecisionElement.Value : null; }
                set
                {
                    if (value == null)
                        PrecisionElement = null;
                    else
                        PrecisionElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Precision");
                }
            }
            
            /// <summary>
            /// Recall of comparison
            /// </summary>
            [FhirElement("recall", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal RecallElement
            {
                get { return _RecallElement; }
                set { _RecallElement = value; OnPropertyChanged("RecallElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _RecallElement;
            
            /// <summary>
            /// Recall of comparison
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Recall
            {
                get { return RecallElement != null ? RecallElement.Value : null; }
                set
                {
                    if (value == null)
                        RecallElement = null;
                    else
                        RecallElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Recall");
                }
            }
            
            /// <summary>
            /// F-score
            /// </summary>
            [FhirElement("fScore", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal FScoreElement
            {
                get { return _FScoreElement; }
                set { _FScoreElement = value; OnPropertyChanged("FScoreElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _FScoreElement;
            
            /// <summary>
            /// F-score
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? FScore
            {
                get { return FScoreElement != null ? FScoreElement.Value : null; }
                set
                {
                    if (value == null)
                        FScoreElement = null;
                    else
                        FScoreElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("FScore");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("QualityComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); TypeElement?.Serialize(sink);
                sink.Element("standardSequence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StandardSequence?.Serialize(sink);
                sink.Element("start", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StartElement?.Serialize(sink);
                sink.Element("end", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); EndElement?.Serialize(sink);
                sink.Element("score", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Score?.Serialize(sink);
                sink.Element("method", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Method?.Serialize(sink);
                sink.Element("truthTP", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TruthTPElement?.Serialize(sink);
                sink.Element("queryTP", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); QueryTPElement?.Serialize(sink);
                sink.Element("truthFN", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TruthFNElement?.Serialize(sink);
                sink.Element("queryFP", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); QueryFPElement?.Serialize(sink);
                sink.Element("gtFP", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); GtFPElement?.Serialize(sink);
                sink.Element("precision", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PrecisionElement?.Serialize(sink);
                sink.Element("recall", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); RecallElement?.Serialize(sink);
                sink.Element("fScore", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); FScoreElement?.Serialize(sink);
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
                    case "type":
                        TypeElement = source.PopulateValue(TypeElement);
                        return true;
                    case "_type":
                        TypeElement = source.Populate(TypeElement);
                        return true;
                    case "standardSequence":
                        StandardSequence = source.Populate(StandardSequence);
                        return true;
                    case "start":
                        StartElement = source.PopulateValue(StartElement);
                        return true;
                    case "_start":
                        StartElement = source.Populate(StartElement);
                        return true;
                    case "end":
                        EndElement = source.PopulateValue(EndElement);
                        return true;
                    case "_end":
                        EndElement = source.Populate(EndElement);
                        return true;
                    case "score":
                        Score = source.Populate(Score);
                        return true;
                    case "method":
                        Method = source.Populate(Method);
                        return true;
                    case "truthTP":
                        TruthTPElement = source.PopulateValue(TruthTPElement);
                        return true;
                    case "_truthTP":
                        TruthTPElement = source.Populate(TruthTPElement);
                        return true;
                    case "queryTP":
                        QueryTPElement = source.PopulateValue(QueryTPElement);
                        return true;
                    case "_queryTP":
                        QueryTPElement = source.Populate(QueryTPElement);
                        return true;
                    case "truthFN":
                        TruthFNElement = source.PopulateValue(TruthFNElement);
                        return true;
                    case "_truthFN":
                        TruthFNElement = source.Populate(TruthFNElement);
                        return true;
                    case "queryFP":
                        QueryFPElement = source.PopulateValue(QueryFPElement);
                        return true;
                    case "_queryFP":
                        QueryFPElement = source.Populate(QueryFPElement);
                        return true;
                    case "gtFP":
                        GtFPElement = source.PopulateValue(GtFPElement);
                        return true;
                    case "_gtFP":
                        GtFPElement = source.Populate(GtFPElement);
                        return true;
                    case "precision":
                        PrecisionElement = source.PopulateValue(PrecisionElement);
                        return true;
                    case "_precision":
                        PrecisionElement = source.Populate(PrecisionElement);
                        return true;
                    case "recall":
                        RecallElement = source.PopulateValue(RecallElement);
                        return true;
                    case "_recall":
                        RecallElement = source.Populate(RecallElement);
                        return true;
                    case "fScore":
                        FScoreElement = source.PopulateValue(FScoreElement);
                        return true;
                    case "_fScore":
                        FScoreElement = source.Populate(FScoreElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as QualityComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.qualityType>)TypeElement.DeepCopy();
                    if(StandardSequence != null) dest.StandardSequence = (Hl7.Fhir.Model.CodeableConcept)StandardSequence.DeepCopy();
                    if(StartElement != null) dest.StartElement = (Hl7.Fhir.Model.Integer)StartElement.DeepCopy();
                    if(EndElement != null) dest.EndElement = (Hl7.Fhir.Model.Integer)EndElement.DeepCopy();
                    if(Score != null) dest.Score = (Hl7.Fhir.Model.Quantity)Score.DeepCopy();
                    if(Method != null) dest.Method = (Hl7.Fhir.Model.CodeableConcept)Method.DeepCopy();
                    if(TruthTPElement != null) dest.TruthTPElement = (Hl7.Fhir.Model.FhirDecimal)TruthTPElement.DeepCopy();
                    if(QueryTPElement != null) dest.QueryTPElement = (Hl7.Fhir.Model.FhirDecimal)QueryTPElement.DeepCopy();
                    if(TruthFNElement != null) dest.TruthFNElement = (Hl7.Fhir.Model.FhirDecimal)TruthFNElement.DeepCopy();
                    if(QueryFPElement != null) dest.QueryFPElement = (Hl7.Fhir.Model.FhirDecimal)QueryFPElement.DeepCopy();
                    if(GtFPElement != null) dest.GtFPElement = (Hl7.Fhir.Model.FhirDecimal)GtFPElement.DeepCopy();
                    if(PrecisionElement != null) dest.PrecisionElement = (Hl7.Fhir.Model.FhirDecimal)PrecisionElement.DeepCopy();
                    if(RecallElement != null) dest.RecallElement = (Hl7.Fhir.Model.FhirDecimal)RecallElement.DeepCopy();
                    if(FScoreElement != null) dest.FScoreElement = (Hl7.Fhir.Model.FhirDecimal)FScoreElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new QualityComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as QualityComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(StandardSequence, otherT.StandardSequence)) return false;
                if( !DeepComparable.Matches(StartElement, otherT.StartElement)) return false;
                if( !DeepComparable.Matches(EndElement, otherT.EndElement)) return false;
                if( !DeepComparable.Matches(Score, otherT.Score)) return false;
                if( !DeepComparable.Matches(Method, otherT.Method)) return false;
                if( !DeepComparable.Matches(TruthTPElement, otherT.TruthTPElement)) return false;
                if( !DeepComparable.Matches(QueryTPElement, otherT.QueryTPElement)) return false;
                if( !DeepComparable.Matches(TruthFNElement, otherT.TruthFNElement)) return false;
                if( !DeepComparable.Matches(QueryFPElement, otherT.QueryFPElement)) return false;
                if( !DeepComparable.Matches(GtFPElement, otherT.GtFPElement)) return false;
                if( !DeepComparable.Matches(PrecisionElement, otherT.PrecisionElement)) return false;
                if( !DeepComparable.Matches(RecallElement, otherT.RecallElement)) return false;
                if( !DeepComparable.Matches(FScoreElement, otherT.FScoreElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as QualityComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(StandardSequence, otherT.StandardSequence)) return false;
                if( !DeepComparable.IsExactly(StartElement, otherT.StartElement)) return false;
                if( !DeepComparable.IsExactly(EndElement, otherT.EndElement)) return false;
                if( !DeepComparable.IsExactly(Score, otherT.Score)) return false;
                if( !DeepComparable.IsExactly(Method, otherT.Method)) return false;
                if( !DeepComparable.IsExactly(TruthTPElement, otherT.TruthTPElement)) return false;
                if( !DeepComparable.IsExactly(QueryTPElement, otherT.QueryTPElement)) return false;
                if( !DeepComparable.IsExactly(TruthFNElement, otherT.TruthFNElement)) return false;
                if( !DeepComparable.IsExactly(QueryFPElement, otherT.QueryFPElement)) return false;
                if( !DeepComparable.IsExactly(GtFPElement, otherT.GtFPElement)) return false;
                if( !DeepComparable.IsExactly(PrecisionElement, otherT.PrecisionElement)) return false;
                if( !DeepComparable.IsExactly(RecallElement, otherT.RecallElement)) return false;
                if( !DeepComparable.IsExactly(FScoreElement, otherT.FScoreElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TypeElement != null) yield return TypeElement;
                    if (StandardSequence != null) yield return StandardSequence;
                    if (StartElement != null) yield return StartElement;
                    if (EndElement != null) yield return EndElement;
                    if (Score != null) yield return Score;
                    if (Method != null) yield return Method;
                    if (TruthTPElement != null) yield return TruthTPElement;
                    if (QueryTPElement != null) yield return QueryTPElement;
                    if (TruthFNElement != null) yield return TruthFNElement;
                    if (QueryFPElement != null) yield return QueryFPElement;
                    if (GtFPElement != null) yield return GtFPElement;
                    if (PrecisionElement != null) yield return PrecisionElement;
                    if (RecallElement != null) yield return RecallElement;
                    if (FScoreElement != null) yield return FScoreElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (StandardSequence != null) yield return new ElementValue("standardSequence", StandardSequence);
                    if (StartElement != null) yield return new ElementValue("start", StartElement);
                    if (EndElement != null) yield return new ElementValue("end", EndElement);
                    if (Score != null) yield return new ElementValue("score", Score);
                    if (Method != null) yield return new ElementValue("method", Method);
                    if (TruthTPElement != null) yield return new ElementValue("truthTP", TruthTPElement);
                    if (QueryTPElement != null) yield return new ElementValue("queryTP", QueryTPElement);
                    if (TruthFNElement != null) yield return new ElementValue("truthFN", TruthFNElement);
                    if (QueryFPElement != null) yield return new ElementValue("queryFP", QueryFPElement);
                    if (GtFPElement != null) yield return new ElementValue("gtFP", GtFPElement);
                    if (PrecisionElement != null) yield return new ElementValue("precision", PrecisionElement);
                    if (RecallElement != null) yield return new ElementValue("recall", RecallElement);
                    if (FScoreElement != null) yield return new ElementValue("fScore", FScoreElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "RepositoryComponent")]
        [DataContract]
        public partial class RepositoryComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "RepositoryComponent"; } }
            
            /// <summary>
            /// directlink | openapi | login | oauth | other
            /// </summary>
            [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.repositoryType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.repositoryType> _TypeElement;
            
            /// <summary>
            /// directlink | openapi | login | oauth | other
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.repositoryType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (value == null)
                        TypeElement = null;
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.repositoryType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// URI of the repository
            /// </summary>
            [FhirElement("url", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UrlElement
            {
                get { return _UrlElement; }
                set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _UrlElement;
            
            /// <summary>
            /// URI of the repository
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Url
            {
                get { return UrlElement != null ? UrlElement.Value : null; }
                set
                {
                    if (value == null)
                        UrlElement = null;
                    else
                        UrlElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Url");
                }
            }
            
            /// <summary>
            /// Repository's name
            /// </summary>
            [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Repository's name
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
            /// Id of the dataset that used to call for dataset in repository
            /// </summary>
            [FhirElement("datasetId", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DatasetIdElement
            {
                get { return _DatasetIdElement; }
                set { _DatasetIdElement = value; OnPropertyChanged("DatasetIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DatasetIdElement;
            
            /// <summary>
            /// Id of the dataset that used to call for dataset in repository
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string DatasetId
            {
                get { return DatasetIdElement != null ? DatasetIdElement.Value : null; }
                set
                {
                    if (value == null)
                        DatasetIdElement = null;
                    else
                        DatasetIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("DatasetId");
                }
            }
            
            /// <summary>
            /// Id of the variantset that used to call for variantset in repository
            /// </summary>
            [FhirElement("variantsetId", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VariantsetIdElement
            {
                get { return _VariantsetIdElement; }
                set { _VariantsetIdElement = value; OnPropertyChanged("VariantsetIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _VariantsetIdElement;
            
            /// <summary>
            /// Id of the variantset that used to call for variantset in repository
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string VariantsetId
            {
                get { return VariantsetIdElement != null ? VariantsetIdElement.Value : null; }
                set
                {
                    if (value == null)
                        VariantsetIdElement = null;
                    else
                        VariantsetIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("VariantsetId");
                }
            }
            
            /// <summary>
            /// Id of the read
            /// </summary>
            [FhirElement("readsetId", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ReadsetIdElement
            {
                get { return _ReadsetIdElement; }
                set { _ReadsetIdElement = value; OnPropertyChanged("ReadsetIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ReadsetIdElement;
            
            /// <summary>
            /// Id of the read
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ReadsetId
            {
                get { return ReadsetIdElement != null ? ReadsetIdElement.Value : null; }
                set
                {
                    if (value == null)
                        ReadsetIdElement = null;
                    else
                        ReadsetIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ReadsetId");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("RepositoryComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); TypeElement?.Serialize(sink);
                sink.Element("url", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); UrlElement?.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NameElement?.Serialize(sink);
                sink.Element("datasetId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DatasetIdElement?.Serialize(sink);
                sink.Element("variantsetId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VariantsetIdElement?.Serialize(sink);
                sink.Element("readsetId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ReadsetIdElement?.Serialize(sink);
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
                    case "type":
                        TypeElement = source.PopulateValue(TypeElement);
                        return true;
                    case "_type":
                        TypeElement = source.Populate(TypeElement);
                        return true;
                    case "url":
                        UrlElement = source.PopulateValue(UrlElement);
                        return true;
                    case "_url":
                        UrlElement = source.Populate(UrlElement);
                        return true;
                    case "name":
                        NameElement = source.PopulateValue(NameElement);
                        return true;
                    case "_name":
                        NameElement = source.Populate(NameElement);
                        return true;
                    case "datasetId":
                        DatasetIdElement = source.PopulateValue(DatasetIdElement);
                        return true;
                    case "_datasetId":
                        DatasetIdElement = source.Populate(DatasetIdElement);
                        return true;
                    case "variantsetId":
                        VariantsetIdElement = source.PopulateValue(VariantsetIdElement);
                        return true;
                    case "_variantsetId":
                        VariantsetIdElement = source.Populate(VariantsetIdElement);
                        return true;
                    case "readsetId":
                        ReadsetIdElement = source.PopulateValue(ReadsetIdElement);
                        return true;
                    case "_readsetId":
                        ReadsetIdElement = source.Populate(ReadsetIdElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RepositoryComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.repositoryType>)TypeElement.DeepCopy();
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DatasetIdElement != null) dest.DatasetIdElement = (Hl7.Fhir.Model.FhirString)DatasetIdElement.DeepCopy();
                    if(VariantsetIdElement != null) dest.VariantsetIdElement = (Hl7.Fhir.Model.FhirString)VariantsetIdElement.DeepCopy();
                    if(ReadsetIdElement != null) dest.ReadsetIdElement = (Hl7.Fhir.Model.FhirString)ReadsetIdElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new RepositoryComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RepositoryComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DatasetIdElement, otherT.DatasetIdElement)) return false;
                if( !DeepComparable.Matches(VariantsetIdElement, otherT.VariantsetIdElement)) return false;
                if( !DeepComparable.Matches(ReadsetIdElement, otherT.ReadsetIdElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RepositoryComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DatasetIdElement, otherT.DatasetIdElement)) return false;
                if( !DeepComparable.IsExactly(VariantsetIdElement, otherT.VariantsetIdElement)) return false;
                if( !DeepComparable.IsExactly(ReadsetIdElement, otherT.ReadsetIdElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TypeElement != null) yield return TypeElement;
                    if (UrlElement != null) yield return UrlElement;
                    if (NameElement != null) yield return NameElement;
                    if (DatasetIdElement != null) yield return DatasetIdElement;
                    if (VariantsetIdElement != null) yield return VariantsetIdElement;
                    if (ReadsetIdElement != null) yield return ReadsetIdElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (DatasetIdElement != null) yield return new ElementValue("datasetId", DatasetIdElement);
                    if (VariantsetIdElement != null) yield return new ElementValue("variantsetId", VariantsetIdElement);
                    if (ReadsetIdElement != null) yield return new ElementValue("readsetId", ReadsetIdElement);
                }
            }
        
        
        }
    
        
        /// <summary>
        /// Unique ID for this particular sequence. This is a FHIR-defined id
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
        /// aa | dna | rna
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Code TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Hl7.Fhir.Model.Code _TypeElement;
        
        /// <summary>
        /// aa | dna | rna
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if (value == null)
                    TypeElement = null;
                else
                    TypeElement = new Hl7.Fhir.Model.Code(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// Base number of coordinate system (0 for 0-based numbering or coordinates, inclusive start, exclusive end, 1 for 1-based numbering, inclusive start, inclusive end)
        /// </summary>
        [FhirElement("coordinateSystem", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Integer CoordinateSystemElement
        {
            get { return _CoordinateSystemElement; }
            set { _CoordinateSystemElement = value; OnPropertyChanged("CoordinateSystemElement"); }
        }
        
        private Hl7.Fhir.Model.Integer _CoordinateSystemElement;
        
        /// <summary>
        /// Base number of coordinate system (0 for 0-based numbering or coordinates, inclusive start, exclusive end, 1 for 1-based numbering, inclusive start, inclusive end)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? CoordinateSystem
        {
            get { return CoordinateSystemElement != null ? CoordinateSystemElement.Value : null; }
            set
            {
                if (value == null)
                    CoordinateSystemElement = null;
                else
                    CoordinateSystemElement = new Hl7.Fhir.Model.Integer(value);
                OnPropertyChanged("CoordinateSystem");
            }
        }
        
        /// <summary>
        /// Who and/or what this is about
        /// </summary>
        [FhirElement("patient", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [References("Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// Specimen used for sequencing
        /// </summary>
        [FhirElement("specimen", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [References("Specimen")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Specimen
        {
            get { return _Specimen; }
            set { _Specimen = value; OnPropertyChanged("Specimen"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Specimen;
        
        /// <summary>
        /// The method for sequencing
        /// </summary>
        [FhirElement("device", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [References("Device")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Device
        {
            get { return _Device; }
            set { _Device = value; OnPropertyChanged("Device"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Device;
        
        /// <summary>
        /// Who should be responsible for test result
        /// </summary>
        [FhirElement("performer", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Performer
        {
            get { return _Performer; }
            set { _Performer = value; OnPropertyChanged("Performer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Performer;
        
        /// <summary>
        /// The number of copies of the seqeunce of interest.  (RNASeq)
        /// </summary>
        [FhirElement("quantity", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Quantity Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; OnPropertyChanged("Quantity"); }
        }
        
        private Hl7.Fhir.Model.Quantity _Quantity;
        
        /// <summary>
        /// A sequence used as reference
        /// </summary>
        [FhirElement("referenceSeq", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [DataMember]
        public ReferenceSeqComponent ReferenceSeq
        {
            get { return _ReferenceSeq; }
            set { _ReferenceSeq = value; OnPropertyChanged("ReferenceSeq"); }
        }
        
        private ReferenceSeqComponent _ReferenceSeq;
        
        /// <summary>
        /// Variant in sequence
        /// </summary>
        [FhirElement("variant", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<VariantComponent> Variant
        {
            get { if(_Variant==null) _Variant = new List<VariantComponent>(); return _Variant; }
            set { _Variant = value; OnPropertyChanged("Variant"); }
        }
        
        private List<VariantComponent> _Variant;
        
        /// <summary>
        /// Sequence that was observed
        /// </summary>
        [FhirElement("observedSeq", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ObservedSeqElement
        {
            get { return _ObservedSeqElement; }
            set { _ObservedSeqElement = value; OnPropertyChanged("ObservedSeqElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ObservedSeqElement;
        
        /// <summary>
        /// Sequence that was observed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ObservedSeq
        {
            get { return ObservedSeqElement != null ? ObservedSeqElement.Value : null; }
            set
            {
                if (value == null)
                    ObservedSeqElement = null;
                else
                    ObservedSeqElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("ObservedSeq");
            }
        }
        
        /// <summary>
        /// An set of value as quality of sequence
        /// </summary>
        [FhirElement("quality", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<QualityComponent> Quality
        {
            get { if(_Quality==null) _Quality = new List<QualityComponent>(); return _Quality; }
            set { _Quality = value; OnPropertyChanged("Quality"); }
        }
        
        private List<QualityComponent> _Quality;
        
        /// <summary>
        /// Average number of reads representing a given nucleotide in the reconstructed sequence
        /// </summary>
        [FhirElement("readCoverage", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Integer ReadCoverageElement
        {
            get { return _ReadCoverageElement; }
            set { _ReadCoverageElement = value; OnPropertyChanged("ReadCoverageElement"); }
        }
        
        private Hl7.Fhir.Model.Integer _ReadCoverageElement;
        
        /// <summary>
        /// Average number of reads representing a given nucleotide in the reconstructed sequence
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? ReadCoverage
        {
            get { return ReadCoverageElement != null ? ReadCoverageElement.Value : null; }
            set
            {
                if (value == null)
                    ReadCoverageElement = null;
                else
                    ReadCoverageElement = new Hl7.Fhir.Model.Integer(value);
                OnPropertyChanged("ReadCoverage");
            }
        }
        
        /// <summary>
        /// External repository which contains detailed report related with observedSeq in this resource
        /// </summary>
        [FhirElement("repository", InSummary=Hl7.Fhir.Model.Version.All, Order=220)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<RepositoryComponent> Repository
        {
            get { if(_Repository==null) _Repository = new List<RepositoryComponent>(); return _Repository; }
            set { _Repository = value; OnPropertyChanged("Repository"); }
        }
        
        private List<RepositoryComponent> _Repository;
        
        /// <summary>
        /// Pointer to next atomic sequence
        /// </summary>
        [FhirElement("pointer", InSummary=Hl7.Fhir.Model.Version.All, Order=230)]
        [CLSCompliant(false)]
        [References("Sequence")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Pointer
        {
            get { if(_Pointer==null) _Pointer = new List<Hl7.Fhir.Model.ResourceReference>(); return _Pointer; }
            set { _Pointer = value; OnPropertyChanged("Pointer"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Pointer;
    
    
        public static ElementDefinitionConstraint[] Sequence_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "seq-3",
                severity: ConstraintSeverity.Warning,
                expression: "coordinateSystem = 1 or coordinateSystem = 0",
                human: "Only 0 and 1 are valid for coordinateSystem",
                xpath: "count(f:coordinateSystem[@value=0 and @value=1]) = 1"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "seq-4",
                severity: ConstraintSeverity.Warning,
                expression: "referenceSeq.all(strand.empty() or strand = 1 or strand = -1)",
                human: "Only +1 and -1 are valid for strand",
                xpath: "not(exists(f:strand)) or count(f:strand[@value=-1 and @value=1]) = 1"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "seq-5",
                severity: ConstraintSeverity.Warning,
                expression: "referenceSeq.all((chromosome.empty() and genomeBuild.empty()) or (chromosome.exists() and genomeBuild.exists()))",
                human: "GenomeBuild and chromosome must be both contained if either one of them is contained",
                xpath: "(exists(f:chromosome) and exists(f:genomeBuild)) or (not(exists(f:chromosome)) and not(exists(f:genomeBuild)))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "seq-6",
                severity: ConstraintSeverity.Warning,
                expression: "referenceSeq.all((genomeBuild.count()+referenceSeqId.count()+ referenceSeqPointer.count()+ referenceSeqString.count()) = 1)",
                human: "Have and only have one of the following elements in referenceSeq : 1. genomeBuild ; 2 referenceSeqId; 3. referenceSeqPointer;  4. referenceSeqString;",
                xpath: "count(f:genomeBuild)+count(f:referenceSeqId)+count(f:referenceSeqPointer)+count(f:referenceSeqString)=1"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(Sequence_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Sequence;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.Code)TypeElement.DeepCopy();
                if(CoordinateSystemElement != null) dest.CoordinateSystemElement = (Hl7.Fhir.Model.Integer)CoordinateSystemElement.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Specimen != null) dest.Specimen = (Hl7.Fhir.Model.ResourceReference)Specimen.DeepCopy();
                if(Device != null) dest.Device = (Hl7.Fhir.Model.ResourceReference)Device.DeepCopy();
                if(Performer != null) dest.Performer = (Hl7.Fhir.Model.ResourceReference)Performer.DeepCopy();
                if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.Quantity)Quantity.DeepCopy();
                if(ReferenceSeq != null) dest.ReferenceSeq = (ReferenceSeqComponent)ReferenceSeq.DeepCopy();
                if(Variant != null) dest.Variant = new List<VariantComponent>(Variant.DeepCopy());
                if(ObservedSeqElement != null) dest.ObservedSeqElement = (Hl7.Fhir.Model.FhirString)ObservedSeqElement.DeepCopy();
                if(Quality != null) dest.Quality = new List<QualityComponent>(Quality.DeepCopy());
                if(ReadCoverageElement != null) dest.ReadCoverageElement = (Hl7.Fhir.Model.Integer)ReadCoverageElement.DeepCopy();
                if(Repository != null) dest.Repository = new List<RepositoryComponent>(Repository.DeepCopy());
                if(Pointer != null) dest.Pointer = new List<Hl7.Fhir.Model.ResourceReference>(Pointer.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Sequence());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Sequence;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(CoordinateSystemElement, otherT.CoordinateSystemElement)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Specimen, otherT.Specimen)) return false;
            if( !DeepComparable.Matches(Device, otherT.Device)) return false;
            if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
            if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.Matches(ReferenceSeq, otherT.ReferenceSeq)) return false;
            if( !DeepComparable.Matches(Variant, otherT.Variant)) return false;
            if( !DeepComparable.Matches(ObservedSeqElement, otherT.ObservedSeqElement)) return false;
            if( !DeepComparable.Matches(Quality, otherT.Quality)) return false;
            if( !DeepComparable.Matches(ReadCoverageElement, otherT.ReadCoverageElement)) return false;
            if( !DeepComparable.Matches(Repository, otherT.Repository)) return false;
            if( !DeepComparable.Matches(Pointer, otherT.Pointer)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Sequence;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(CoordinateSystemElement, otherT.CoordinateSystemElement)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Specimen, otherT.Specimen)) return false;
            if( !DeepComparable.IsExactly(Device, otherT.Device)) return false;
            if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
            if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.IsExactly(ReferenceSeq, otherT.ReferenceSeq)) return false;
            if( !DeepComparable.IsExactly(Variant, otherT.Variant)) return false;
            if( !DeepComparable.IsExactly(ObservedSeqElement, otherT.ObservedSeqElement)) return false;
            if( !DeepComparable.IsExactly(Quality, otherT.Quality)) return false;
            if( !DeepComparable.IsExactly(ReadCoverageElement, otherT.ReadCoverageElement)) return false;
            if( !DeepComparable.IsExactly(Repository, otherT.Repository)) return false;
            if( !DeepComparable.IsExactly(Pointer, otherT.Pointer)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Sequence");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TypeElement?.Serialize(sink);
            sink.Element("coordinateSystem", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); CoordinateSystemElement?.Serialize(sink);
            sink.Element("patient", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Patient?.Serialize(sink);
            sink.Element("specimen", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Specimen?.Serialize(sink);
            sink.Element("device", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Device?.Serialize(sink);
            sink.Element("performer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Performer?.Serialize(sink);
            sink.Element("quantity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Quantity?.Serialize(sink);
            sink.Element("referenceSeq", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ReferenceSeq?.Serialize(sink);
            sink.BeginList("variant", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Variant)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("observedSeq", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ObservedSeqElement?.Serialize(sink);
            sink.BeginList("quality", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Quality)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("readCoverage", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ReadCoverageElement?.Serialize(sink);
            sink.BeginList("repository", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Repository)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("pointer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Pointer)
            {
                item?.Serialize(sink);
            }
            sink.End();
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
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "type":
                    TypeElement = source.PopulateValue(TypeElement);
                    return true;
                case "_type":
                    TypeElement = source.Populate(TypeElement);
                    return true;
                case "coordinateSystem":
                    CoordinateSystemElement = source.PopulateValue(CoordinateSystemElement);
                    return true;
                case "_coordinateSystem":
                    CoordinateSystemElement = source.Populate(CoordinateSystemElement);
                    return true;
                case "patient":
                    Patient = source.Populate(Patient);
                    return true;
                case "specimen":
                    Specimen = source.Populate(Specimen);
                    return true;
                case "device":
                    Device = source.Populate(Device);
                    return true;
                case "performer":
                    Performer = source.Populate(Performer);
                    return true;
                case "quantity":
                    Quantity = source.Populate(Quantity);
                    return true;
                case "referenceSeq":
                    ReferenceSeq = source.Populate(ReferenceSeq);
                    return true;
                case "variant":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "observedSeq":
                    ObservedSeqElement = source.PopulateValue(ObservedSeqElement);
                    return true;
                case "_observedSeq":
                    ObservedSeqElement = source.Populate(ObservedSeqElement);
                    return true;
                case "quality":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "readCoverage":
                    ReadCoverageElement = source.PopulateValue(ReadCoverageElement);
                    return true;
                case "_readCoverage":
                    ReadCoverageElement = source.Populate(ReadCoverageElement);
                    return true;
                case "repository":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "pointer":
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
                case "variant":
                    source.PopulateListItem(Variant, index);
                    return true;
                case "quality":
                    source.PopulateListItem(Quality, index);
                    return true;
                case "repository":
                    source.PopulateListItem(Repository, index);
                    return true;
                case "pointer":
                    source.PopulateListItem(Pointer, index);
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
                if (TypeElement != null) yield return TypeElement;
                if (CoordinateSystemElement != null) yield return CoordinateSystemElement;
                if (Patient != null) yield return Patient;
                if (Specimen != null) yield return Specimen;
                if (Device != null) yield return Device;
                if (Performer != null) yield return Performer;
                if (Quantity != null) yield return Quantity;
                if (ReferenceSeq != null) yield return ReferenceSeq;
                foreach (var elem in Variant) { if (elem != null) yield return elem; }
                if (ObservedSeqElement != null) yield return ObservedSeqElement;
                foreach (var elem in Quality) { if (elem != null) yield return elem; }
                if (ReadCoverageElement != null) yield return ReadCoverageElement;
                foreach (var elem in Repository) { if (elem != null) yield return elem; }
                foreach (var elem in Pointer) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                if (CoordinateSystemElement != null) yield return new ElementValue("coordinateSystem", CoordinateSystemElement);
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (Specimen != null) yield return new ElementValue("specimen", Specimen);
                if (Device != null) yield return new ElementValue("device", Device);
                if (Performer != null) yield return new ElementValue("performer", Performer);
                if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                if (ReferenceSeq != null) yield return new ElementValue("referenceSeq", ReferenceSeq);
                foreach (var elem in Variant) { if (elem != null) yield return new ElementValue("variant", elem); }
                if (ObservedSeqElement != null) yield return new ElementValue("observedSeq", ObservedSeqElement);
                foreach (var elem in Quality) { if (elem != null) yield return new ElementValue("quality", elem); }
                if (ReadCoverageElement != null) yield return new ElementValue("readCoverage", ReadCoverageElement);
                foreach (var elem in Repository) { if (elem != null) yield return new ElementValue("repository", elem); }
                foreach (var elem in Pointer) { if (elem != null) yield return new ElementValue("pointer", elem); }
            }
        }
    
    }

}
