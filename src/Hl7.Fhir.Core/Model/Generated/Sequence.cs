using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.ComponentModel;

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

//
// Generated for FHIR v1.7.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Information about a biological sequence
    /// </summary>
    [FhirType("Sequence", IsResource=true)]
    [DataContract]
    public partial class Sequence : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Sequence; } }
        [NotMapped]
        public override string TypeName { get { return "Sequence"; } }
        
        [FhirType("ReferenceSeqComponent")]
        [DataContract]
        public partial class ReferenceSeqComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ReferenceSeqComponent"; } }
            
            /// <summary>
            /// Chromosome containing genetic finding
            /// </summary>
            [FhirElement("chromosome", InSummary=true, Order=40)]
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
            [FhirElement("genomeBuild", InSummary=true, Order=50)]
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
            [FhirElement("referenceSeqId", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept ReferenceSeqId
            {
                get { return _ReferenceSeqId; }
                set { _ReferenceSeqId = value; OnPropertyChanged("ReferenceSeqId"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _ReferenceSeqId;
            
            /// <summary>
            /// A Pointer to another Sequence entity as refence sequence
            /// </summary>
            [FhirElement("referenceSeqPointer", InSummary=true, Order=70)]
            [References("Sequence")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference ReferenceSeqPointer
            {
                get { return _ReferenceSeqPointer; }
                set { _ReferenceSeqPointer = value; OnPropertyChanged("ReferenceSeqPointer"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _ReferenceSeqPointer;
            
            /// <summary>
            /// A Reference Sequence string
            /// </summary>
            [FhirElement("referenceSeqString", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ReferenceSeqStringElement
            {
                get { return _ReferenceSeqStringElement; }
                set { _ReferenceSeqStringElement = value; OnPropertyChanged("ReferenceSeqStringElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ReferenceSeqStringElement;
            
            /// <summary>
            /// A Reference Sequence string
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
            /// Strand of DNA
            /// </summary>
            [FhirElement("strand", InSummary=true, Order=90)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Integer StrandElement
            {
                get { return _StrandElement; }
                set { _StrandElement = value; OnPropertyChanged("StrandElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _StrandElement;
            
            /// <summary>
            /// Strand of DNA
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Strand
            {
                get { return StrandElement != null ? StrandElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        StrandElement = null; 
                    else
                        StrandElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Strand");
                }
            }
            
            /// <summary>
            /// Start position (inclusive) of the window on the  reference sequence
            /// </summary>
            [FhirElement("windowStart", InSummary=true, Order=100)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Integer WindowStartElement
            {
                get { return _WindowStartElement; }
                set { _WindowStartElement = value; OnPropertyChanged("WindowStartElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _WindowStartElement;
            
            /// <summary>
            /// Start position (inclusive) of the window on the  reference sequence
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? WindowStart
            {
                get { return WindowStartElement != null ? WindowStartElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        WindowStartElement = null; 
                    else
                        WindowStartElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("WindowStart");
                }
            }
            
            /// <summary>
            /// End position (exclusive) of the window on the reference sequence
            /// </summary>
            [FhirElement("windowEnd", InSummary=true, Order=110)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Integer WindowEndElement
            {
                get { return _WindowEndElement; }
                set { _WindowEndElement = value; OnPropertyChanged("WindowEndElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _WindowEndElement;
            
            /// <summary>
            /// End position (exclusive) of the window on the reference sequence
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? WindowEnd
            {
                get { return WindowEndElement != null ? WindowEndElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        WindowEndElement = null; 
                    else
                        WindowEndElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("WindowEnd");
                }
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
                    // BackboneElement elements
                    foreach (var elem in ModifierExtension) { if (elem != null) yield return elem; }
                    // ReferenceSeqComponent elements
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
            
        }
        
        
        [FhirType("VariantComponent")]
        [DataContract]
        public partial class VariantComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "VariantComponent"; } }
            
            /// <summary>
            /// Start position (inclusive) of the variant on the  reference sequence
            /// </summary>
            [FhirElement("start", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Integer StartElement
            {
                get { return _StartElement; }
                set { _StartElement = value; OnPropertyChanged("StartElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _StartElement;
            
            /// <summary>
            /// Start position (inclusive) of the variant on the  reference sequence
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Start
            {
                get { return StartElement != null ? StartElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        StartElement = null; 
                    else
                        StartElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Start");
                }
            }
            
            /// <summary>
            /// End position (exclusive) of the variant on the reference sequence
            /// </summary>
            [FhirElement("end", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Integer EndElement
            {
                get { return _EndElement; }
                set { _EndElement = value; OnPropertyChanged("EndElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _EndElement;
            
            /// <summary>
            /// End position (exclusive) of the variant on the reference sequence
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? End
            {
                get { return EndElement != null ? EndElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        EndElement = null; 
                    else
                        EndElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("End");
                }
            }
            
            /// <summary>
            /// Allele that was observed
            /// </summary>
            [FhirElement("observedAllele", InSummary=true, Order=60)]
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
            /// Allele of reference sequence
            /// </summary>
            [FhirElement("referenceAllele", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ReferenceAlleleElement
            {
                get { return _ReferenceAlleleElement; }
                set { _ReferenceAlleleElement = value; OnPropertyChanged("ReferenceAlleleElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ReferenceAlleleElement;
            
            /// <summary>
            /// Allele of reference sequence
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
            [FhirElement("cigar", InSummary=true, Order=80)]
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
            [FhirElement("variantPointer", InSummary=true, Order=90)]
            [References("Observation")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference VariantPointer
            {
                get { return _VariantPointer; }
                set { _VariantPointer = value; OnPropertyChanged("VariantPointer"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _VariantPointer;
            
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
                    // BackboneElement elements
                    foreach (var elem in ModifierExtension) { if (elem != null) yield return elem; }
                    // VariantComponent elements
                    if (StartElement != null) yield return StartElement;
                    if (EndElement != null) yield return EndElement;
                    if (ObservedAlleleElement != null) yield return ObservedAlleleElement;
                    if (ReferenceAlleleElement != null) yield return ReferenceAlleleElement;
                    if (CigarElement != null) yield return CigarElement;
                    if (VariantPointer != null) yield return VariantPointer;
                }
            }
            
        }
        
        
        [FhirType("QualityComponent")]
        [DataContract]
        public partial class QualityComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "QualityComponent"; } }
            
            /// <summary>
            /// Standard sequence for comparison
            /// </summary>
            [FhirElement("standardSequence", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept StandardSequence
            {
                get { return _StandardSequence; }
                set { _StandardSequence = value; OnPropertyChanged("StandardSequence"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _StandardSequence;
            
            /// <summary>
            /// Start position (inclusive) of the sequence
            /// </summary>
            [FhirElement("start", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Integer StartElement
            {
                get { return _StartElement; }
                set { _StartElement = value; OnPropertyChanged("StartElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _StartElement;
            
            /// <summary>
            /// Start position (inclusive) of the sequence
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Start
            {
                get { return StartElement != null ? StartElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        StartElement = null; 
                    else
                        StartElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Start");
                }
            }
            
            /// <summary>
            /// End position (exclusive) of the sequence
            /// </summary>
            [FhirElement("end", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Integer EndElement
            {
                get { return _EndElement; }
                set { _EndElement = value; OnPropertyChanged("EndElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _EndElement;
            
            /// <summary>
            /// End position (exclusive) of the sequence
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? End
            {
                get { return EndElement != null ? EndElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        EndElement = null; 
                    else
                        EndElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("End");
                }
            }
            
            /// <summary>
            /// Quality score
            /// </summary>
            [FhirElement("score", InSummary=true, Order=70)]
            [DataMember]
            public Quantity Score
            {
                get { return _Score; }
                set { _Score = value; OnPropertyChanged("Score"); }
            }
            
            private Quantity _Score;
            
            /// <summary>
            /// Method for quality
            /// </summary>
            [FhirElement("method", InSummary=true, Order=80)]
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
            [FhirElement("truthTP", InSummary=true, Order=90)]
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
                    if (!value.HasValue)
                        TruthTPElement = null; 
                    else
                        TruthTPElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("TruthTP");
                }
            }
            
            /// <summary>
            /// True positives from the perspective of the query data
            /// </summary>
            [FhirElement("queryTP", InSummary=true, Order=100)]
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
                    if (!value.HasValue)
                        QueryTPElement = null; 
                    else
                        QueryTPElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("QueryTP");
                }
            }
            
            /// <summary>
            /// False negatives
            /// </summary>
            [FhirElement("truthFN", InSummary=true, Order=110)]
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
                    if (!value.HasValue)
                        TruthFNElement = null; 
                    else
                        TruthFNElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("TruthFN");
                }
            }
            
            /// <summary>
            /// False positives
            /// </summary>
            [FhirElement("queryFP", InSummary=true, Order=120)]
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
                    if (!value.HasValue)
                        QueryFPElement = null; 
                    else
                        QueryFPElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("QueryFP");
                }
            }
            
            /// <summary>
            /// False positives where the non-REF alleles in the Truth and Query Call Sets match
            /// </summary>
            [FhirElement("gtFP", InSummary=true, Order=130)]
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
                    if (!value.HasValue)
                        GtFPElement = null; 
                    else
                        GtFPElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("GtFP");
                }
            }
            
            /// <summary>
            /// Precision
            /// </summary>
            [FhirElement("precision", InSummary=true, Order=140)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal PrecisionElement
            {
                get { return _PrecisionElement; }
                set { _PrecisionElement = value; OnPropertyChanged("PrecisionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _PrecisionElement;
            
            /// <summary>
            /// Precision
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Precision
            {
                get { return PrecisionElement != null ? PrecisionElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        PrecisionElement = null; 
                    else
                        PrecisionElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Precision");
                }
            }
            
            /// <summary>
            /// Recall
            /// </summary>
            [FhirElement("recall", InSummary=true, Order=150)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal RecallElement
            {
                get { return _RecallElement; }
                set { _RecallElement = value; OnPropertyChanged("RecallElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _RecallElement;
            
            /// <summary>
            /// Recall
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Recall
            {
                get { return RecallElement != null ? RecallElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        RecallElement = null; 
                    else
                        RecallElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Recall");
                }
            }
            
            /// <summary>
            /// F-score
            /// </summary>
            [FhirElement("fScore", InSummary=true, Order=160)]
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
                    if (!value.HasValue)
                        FScoreElement = null; 
                    else
                        FScoreElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("FScore");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as QualityComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(StandardSequence != null) dest.StandardSequence = (Hl7.Fhir.Model.CodeableConcept)StandardSequence.DeepCopy();
                    if(StartElement != null) dest.StartElement = (Hl7.Fhir.Model.Integer)StartElement.DeepCopy();
                    if(EndElement != null) dest.EndElement = (Hl7.Fhir.Model.Integer)EndElement.DeepCopy();
                    if(Score != null) dest.Score = (Quantity)Score.DeepCopy();
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
                    // BackboneElement elements
                    foreach (var elem in ModifierExtension) { if (elem != null) yield return elem; }
                    // QualityComponent elements
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
            
        }
        
        
        [FhirType("RepositoryComponent")]
        [DataContract]
        public partial class RepositoryComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "RepositoryComponent"; } }
            
            /// <summary>
            /// URI of the repository
            /// </summary>
            [FhirElement("url", InSummary=true, Order=40)]
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
            /// Name of the repository
            /// </summary>
            [FhirElement("name", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Name of the repository
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
            /// Id of the variant
            /// </summary>
            [FhirElement("variantId", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VariantIdElement
            {
                get { return _VariantIdElement; }
                set { _VariantIdElement = value; OnPropertyChanged("VariantIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _VariantIdElement;
            
            /// <summary>
            /// Id of the variant
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string VariantId
            {
                get { return VariantIdElement != null ? VariantIdElement.Value : null; }
                set
                {
                    if (value == null)
                        VariantIdElement = null; 
                    else
                        VariantIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("VariantId");
                }
            }
            
            /// <summary>
            /// Id of the read
            /// </summary>
            [FhirElement("readId", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ReadIdElement
            {
                get { return _ReadIdElement; }
                set { _ReadIdElement = value; OnPropertyChanged("ReadIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ReadIdElement;
            
            /// <summary>
            /// Id of the read
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ReadId
            {
                get { return ReadIdElement != null ? ReadIdElement.Value : null; }
                set
                {
                    if (value == null)
                        ReadIdElement = null; 
                    else
                        ReadIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ReadId");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RepositoryComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(VariantIdElement != null) dest.VariantIdElement = (Hl7.Fhir.Model.FhirString)VariantIdElement.DeepCopy();
                    if(ReadIdElement != null) dest.ReadIdElement = (Hl7.Fhir.Model.FhirString)ReadIdElement.DeepCopy();
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
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(VariantIdElement, otherT.VariantIdElement)) return false;
                if( !DeepComparable.Matches(ReadIdElement, otherT.ReadIdElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RepositoryComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(VariantIdElement, otherT.VariantIdElement)) return false;
                if( !DeepComparable.IsExactly(ReadIdElement, otherT.ReadIdElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    // BackboneElement elements
                    foreach (var elem in ModifierExtension) { if (elem != null) yield return elem; }
                    // RepositoryComponent elements
                    if (UrlElement != null) yield return UrlElement;
                    if (NameElement != null) yield return NameElement;
                    if (VariantIdElement != null) yield return VariantIdElement;
                    if (ReadIdElement != null) yield return ReadIdElement;
                }
            }
            
        }
        
        
        [FhirType("StructureVariantComponent")]
        [DataContract]
        public partial class StructureVariantComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "StructureVariantComponent"; } }
            
            /// <summary>
            /// Precision of boundaries
            /// </summary>
            [FhirElement("precisionOfBoundaries", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PrecisionOfBoundariesElement
            {
                get { return _PrecisionOfBoundariesElement; }
                set { _PrecisionOfBoundariesElement = value; OnPropertyChanged("PrecisionOfBoundariesElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PrecisionOfBoundariesElement;
            
            /// <summary>
            /// Precision of boundaries
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string PrecisionOfBoundaries
            {
                get { return PrecisionOfBoundariesElement != null ? PrecisionOfBoundariesElement.Value : null; }
                set
                {
                    if (value == null)
                        PrecisionOfBoundariesElement = null; 
                    else
                        PrecisionOfBoundariesElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("PrecisionOfBoundaries");
                }
            }
            
            /// <summary>
            /// Structural Variant reported aCGH ratio
            /// </summary>
            [FhirElement("reportedaCGHRatio", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal ReportedaCGHRatioElement
            {
                get { return _ReportedaCGHRatioElement; }
                set { _ReportedaCGHRatioElement = value; OnPropertyChanged("ReportedaCGHRatioElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _ReportedaCGHRatioElement;
            
            /// <summary>
            /// Structural Variant reported aCGH ratio
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? ReportedaCGHRatio
            {
                get { return ReportedaCGHRatioElement != null ? ReportedaCGHRatioElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ReportedaCGHRatioElement = null; 
                    else
                        ReportedaCGHRatioElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("ReportedaCGHRatio");
                }
            }
            
            /// <summary>
            /// Structural Variant Length
            /// </summary>
            [FhirElement("length", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Integer LengthElement
            {
                get { return _LengthElement; }
                set { _LengthElement = value; OnPropertyChanged("LengthElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _LengthElement;
            
            /// <summary>
            /// Structural Variant Length
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Length
            {
                get { return LengthElement != null ? LengthElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        LengthElement = null; 
                    else
                        LengthElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Length");
                }
            }
            
            /// <summary>
            /// Structural variant outer
            /// </summary>
            [FhirElement("outer", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Sequence.OuterComponent Outer
            {
                get { return _Outer; }
                set { _Outer = value; OnPropertyChanged("Outer"); }
            }
            
            private Hl7.Fhir.Model.Sequence.OuterComponent _Outer;
            
            /// <summary>
            /// Structural variant inner
            /// </summary>
            [FhirElement("inner", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Sequence.InnerComponent Inner
            {
                get { return _Inner; }
                set { _Inner = value; OnPropertyChanged("Inner"); }
            }
            
            private Hl7.Fhir.Model.Sequence.InnerComponent _Inner;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StructureVariantComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(PrecisionOfBoundariesElement != null) dest.PrecisionOfBoundariesElement = (Hl7.Fhir.Model.FhirString)PrecisionOfBoundariesElement.DeepCopy();
                    if(ReportedaCGHRatioElement != null) dest.ReportedaCGHRatioElement = (Hl7.Fhir.Model.FhirDecimal)ReportedaCGHRatioElement.DeepCopy();
                    if(LengthElement != null) dest.LengthElement = (Hl7.Fhir.Model.Integer)LengthElement.DeepCopy();
                    if(Outer != null) dest.Outer = (Hl7.Fhir.Model.Sequence.OuterComponent)Outer.DeepCopy();
                    if(Inner != null) dest.Inner = (Hl7.Fhir.Model.Sequence.InnerComponent)Inner.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new StructureVariantComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as StructureVariantComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(PrecisionOfBoundariesElement, otherT.PrecisionOfBoundariesElement)) return false;
                if( !DeepComparable.Matches(ReportedaCGHRatioElement, otherT.ReportedaCGHRatioElement)) return false;
                if( !DeepComparable.Matches(LengthElement, otherT.LengthElement)) return false;
                if( !DeepComparable.Matches(Outer, otherT.Outer)) return false;
                if( !DeepComparable.Matches(Inner, otherT.Inner)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as StructureVariantComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(PrecisionOfBoundariesElement, otherT.PrecisionOfBoundariesElement)) return false;
                if( !DeepComparable.IsExactly(ReportedaCGHRatioElement, otherT.ReportedaCGHRatioElement)) return false;
                if( !DeepComparable.IsExactly(LengthElement, otherT.LengthElement)) return false;
                if( !DeepComparable.IsExactly(Outer, otherT.Outer)) return false;
                if( !DeepComparable.IsExactly(Inner, otherT.Inner)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    // BackboneElement elements
                    foreach (var elem in ModifierExtension) { if (elem != null) yield return elem; }
                    // StructureVariantComponent elements
                    if (PrecisionOfBoundariesElement != null) yield return PrecisionOfBoundariesElement;
                    if (ReportedaCGHRatioElement != null) yield return ReportedaCGHRatioElement;
                    if (LengthElement != null) yield return LengthElement;
                    if (Outer != null) yield return Outer;
                    if (Inner != null) yield return Inner;
                }
            }
            
        }
        
        
        [FhirType("OuterComponent")]
        [DataContract]
        public partial class OuterComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "OuterComponent"; } }
            
            /// <summary>
            /// Structural Variant Outer Start-End
            /// </summary>
            [FhirElement("start", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Integer StartElement
            {
                get { return _StartElement; }
                set { _StartElement = value; OnPropertyChanged("StartElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _StartElement;
            
            /// <summary>
            /// Structural Variant Outer Start-End
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Start
            {
                get { return StartElement != null ? StartElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        StartElement = null; 
                    else
                        StartElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Start");
                }
            }
            
            /// <summary>
            /// Structural Variant Outer Start-End
            /// </summary>
            [FhirElement("end", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Integer EndElement
            {
                get { return _EndElement; }
                set { _EndElement = value; OnPropertyChanged("EndElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _EndElement;
            
            /// <summary>
            /// Structural Variant Outer Start-End
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? End
            {
                get { return EndElement != null ? EndElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        EndElement = null; 
                    else
                        EndElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("End");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as OuterComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(StartElement != null) dest.StartElement = (Hl7.Fhir.Model.Integer)StartElement.DeepCopy();
                    if(EndElement != null) dest.EndElement = (Hl7.Fhir.Model.Integer)EndElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new OuterComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as OuterComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(StartElement, otherT.StartElement)) return false;
                if( !DeepComparable.Matches(EndElement, otherT.EndElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as OuterComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(StartElement, otherT.StartElement)) return false;
                if( !DeepComparable.IsExactly(EndElement, otherT.EndElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    // BackboneElement elements
                    foreach (var elem in ModifierExtension) { if (elem != null) yield return elem; }
                    // OuterComponent elements
                    if (StartElement != null) yield return StartElement;
                    if (EndElement != null) yield return EndElement;
                }
            }
            
        }
        
        
        [FhirType("InnerComponent")]
        [DataContract]
        public partial class InnerComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "InnerComponent"; } }
            
            /// <summary>
            /// Structural Variant Inner Start-End
            /// </summary>
            [FhirElement("start", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Integer StartElement
            {
                get { return _StartElement; }
                set { _StartElement = value; OnPropertyChanged("StartElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _StartElement;
            
            /// <summary>
            /// Structural Variant Inner Start-End
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Start
            {
                get { return StartElement != null ? StartElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        StartElement = null; 
                    else
                        StartElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Start");
                }
            }
            
            /// <summary>
            /// Structural Variant Inner Start-End
            /// </summary>
            [FhirElement("end", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Integer EndElement
            {
                get { return _EndElement; }
                set { _EndElement = value; OnPropertyChanged("EndElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _EndElement;
            
            /// <summary>
            /// Structural Variant Inner Start-End
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? End
            {
                get { return EndElement != null ? EndElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        EndElement = null; 
                    else
                        EndElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("End");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as InnerComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(StartElement != null) dest.StartElement = (Hl7.Fhir.Model.Integer)StartElement.DeepCopy();
                    if(EndElement != null) dest.EndElement = (Hl7.Fhir.Model.Integer)EndElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new InnerComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as InnerComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(StartElement, otherT.StartElement)) return false;
                if( !DeepComparable.Matches(EndElement, otherT.EndElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as InnerComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(StartElement, otherT.StartElement)) return false;
                if( !DeepComparable.IsExactly(EndElement, otherT.EndElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    // BackboneElement elements
                    foreach (var elem in ModifierExtension) { if (elem != null) yield return elem; }
                    // InnerComponent elements
                    if (StartElement != null) yield return StartElement;
                    if (EndElement != null) yield return EndElement;
                }
            }
            
        }
        
        
        /// <summary>
        /// Unique ID for this particular sequence
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// AA | DNA | RNA
        /// </summary>
        [FhirElement("type", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Code TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Hl7.Fhir.Model.Code _TypeElement;
        
        /// <summary>
        /// AA | DNA | RNA
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
        /// Numbering used for sequence (0-based or 1-based)
        /// </summary>
        [FhirElement("coordinateSystem", InSummary=true, Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Integer CoordinateSystemElement
        {
            get { return _CoordinateSystemElement; }
            set { _CoordinateSystemElement = value; OnPropertyChanged("CoordinateSystemElement"); }
        }
        
        private Hl7.Fhir.Model.Integer _CoordinateSystemElement;
        
        /// <summary>
        /// Numbering used for sequence (0-based or 1-based)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? CoordinateSystem
        {
            get { return CoordinateSystemElement != null ? CoordinateSystemElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  CoordinateSystemElement = null; 
                else
                  CoordinateSystemElement = new Hl7.Fhir.Model.Integer(value);
                OnPropertyChanged("CoordinateSystem");
            }
        }
        
        /// <summary>
        /// Who and/or what this is about
        /// </summary>
        [FhirElement("patient", InSummary=true, Order=120)]
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
        [FhirElement("specimen", InSummary=true, Order=130)]
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
        [FhirElement("device", InSummary=true, Order=140)]
        [References("Device")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Device
        {
            get { return _Device; }
            set { _Device = value; OnPropertyChanged("Device"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Device;
        
        /// <summary>
        /// Quantity of the sequence
        /// </summary>
        [FhirElement("quantity", InSummary=true, Order=150)]
        [DataMember]
        public Quantity Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; OnPropertyChanged("Quantity"); }
        }
        
        private Quantity _Quantity;
        
        /// <summary>
        /// Reference sequence
        /// </summary>
        [FhirElement("referenceSeq", InSummary=true, Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.Sequence.ReferenceSeqComponent ReferenceSeq
        {
            get { return _ReferenceSeq; }
            set { _ReferenceSeq = value; OnPropertyChanged("ReferenceSeq"); }
        }
        
        private Hl7.Fhir.Model.Sequence.ReferenceSeqComponent _ReferenceSeq;
        
        /// <summary>
        /// Sequence variant
        /// </summary>
        [FhirElement("variant", InSummary=true, Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Sequence.VariantComponent> Variant
        {
            get { if(_Variant==null) _Variant = new List<Hl7.Fhir.Model.Sequence.VariantComponent>(); return _Variant; }
            set { _Variant = value; OnPropertyChanged("Variant"); }
        }
        
        private List<Hl7.Fhir.Model.Sequence.VariantComponent> _Variant;
        
        /// <summary>
        /// Observed sequence
        /// </summary>
        [FhirElement("observedSeq", InSummary=true, Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ObservedSeqElement
        {
            get { return _ObservedSeqElement; }
            set { _ObservedSeqElement = value; OnPropertyChanged("ObservedSeqElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ObservedSeqElement;
        
        /// <summary>
        /// Observed sequence
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
        /// Sequence quality
        /// </summary>
        [FhirElement("quality", InSummary=true, Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Sequence.QualityComponent> Quality
        {
            get { if(_Quality==null) _Quality = new List<Hl7.Fhir.Model.Sequence.QualityComponent>(); return _Quality; }
            set { _Quality = value; OnPropertyChanged("Quality"); }
        }
        
        private List<Hl7.Fhir.Model.Sequence.QualityComponent> _Quality;
        
        /// <summary>
        /// Average number of reads representing a given nucleotide in the reconstructed sequence
        /// </summary>
        [FhirElement("readCoverage", InSummary=true, Order=200)]
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
                if (!value.HasValue)
                  ReadCoverageElement = null; 
                else
                  ReadCoverageElement = new Hl7.Fhir.Model.Integer(value);
                OnPropertyChanged("ReadCoverage");
            }
        }
        
        /// <summary>
        /// External repository
        /// </summary>
        [FhirElement("repository", InSummary=true, Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Sequence.RepositoryComponent> Repository
        {
            get { if(_Repository==null) _Repository = new List<Hl7.Fhir.Model.Sequence.RepositoryComponent>(); return _Repository; }
            set { _Repository = value; OnPropertyChanged("Repository"); }
        }
        
        private List<Hl7.Fhir.Model.Sequence.RepositoryComponent> _Repository;
        
        /// <summary>
        /// Pointer to next atomic sequence
        /// </summary>
        [FhirElement("pointer", InSummary=true, Order=220)]
        [References("Sequence")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Pointer
        {
            get { if(_Pointer==null) _Pointer = new List<Hl7.Fhir.Model.ResourceReference>(); return _Pointer; }
            set { _Pointer = value; OnPropertyChanged("Pointer"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Pointer;
        
        /// <summary>
        /// Structural variant
        /// </summary>
        [FhirElement("structureVariant", InSummary=true, Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Sequence.StructureVariantComponent> StructureVariant
        {
            get { if(_StructureVariant==null) _StructureVariant = new List<Hl7.Fhir.Model.Sequence.StructureVariantComponent>(); return _StructureVariant; }
            set { _StructureVariant = value; OnPropertyChanged("StructureVariant"); }
        }
        
        private List<Hl7.Fhir.Model.Sequence.StructureVariantComponent> _StructureVariant;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

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
                if(Quantity != null) dest.Quantity = (Quantity)Quantity.DeepCopy();
                if(ReferenceSeq != null) dest.ReferenceSeq = (Hl7.Fhir.Model.Sequence.ReferenceSeqComponent)ReferenceSeq.DeepCopy();
                if(Variant != null) dest.Variant = new List<Hl7.Fhir.Model.Sequence.VariantComponent>(Variant.DeepCopy());
                if(ObservedSeqElement != null) dest.ObservedSeqElement = (Hl7.Fhir.Model.FhirString)ObservedSeqElement.DeepCopy();
                if(Quality != null) dest.Quality = new List<Hl7.Fhir.Model.Sequence.QualityComponent>(Quality.DeepCopy());
                if(ReadCoverageElement != null) dest.ReadCoverageElement = (Hl7.Fhir.Model.Integer)ReadCoverageElement.DeepCopy();
                if(Repository != null) dest.Repository = new List<Hl7.Fhir.Model.Sequence.RepositoryComponent>(Repository.DeepCopy());
                if(Pointer != null) dest.Pointer = new List<Hl7.Fhir.Model.ResourceReference>(Pointer.DeepCopy());
                if(StructureVariant != null) dest.StructureVariant = new List<Hl7.Fhir.Model.Sequence.StructureVariantComponent>(StructureVariant.DeepCopy());
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
            if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.Matches(ReferenceSeq, otherT.ReferenceSeq)) return false;
            if( !DeepComparable.Matches(Variant, otherT.Variant)) return false;
            if( !DeepComparable.Matches(ObservedSeqElement, otherT.ObservedSeqElement)) return false;
            if( !DeepComparable.Matches(Quality, otherT.Quality)) return false;
            if( !DeepComparable.Matches(ReadCoverageElement, otherT.ReadCoverageElement)) return false;
            if( !DeepComparable.Matches(Repository, otherT.Repository)) return false;
            if( !DeepComparable.Matches(Pointer, otherT.Pointer)) return false;
            if( !DeepComparable.Matches(StructureVariant, otherT.StructureVariant)) return false;
            
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
            if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.IsExactly(ReferenceSeq, otherT.ReferenceSeq)) return false;
            if( !DeepComparable.IsExactly(Variant, otherT.Variant)) return false;
            if( !DeepComparable.IsExactly(ObservedSeqElement, otherT.ObservedSeqElement)) return false;
            if( !DeepComparable.IsExactly(Quality, otherT.Quality)) return false;
            if( !DeepComparable.IsExactly(ReadCoverageElement, otherT.ReadCoverageElement)) return false;
            if( !DeepComparable.IsExactly(Repository, otherT.Repository)) return false;
            if( !DeepComparable.IsExactly(Pointer, otherT.Pointer)) return false;
            if( !DeepComparable.IsExactly(StructureVariant, otherT.StructureVariant)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
				// Sequence elements
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (TypeElement != null) yield return TypeElement;
				if (CoordinateSystemElement != null) yield return CoordinateSystemElement;
				if (Patient != null) yield return Patient;
				if (Specimen != null) yield return Specimen;
				if (Device != null) yield return Device;
				if (Quantity != null) yield return Quantity;
				if (ReferenceSeq != null) yield return ReferenceSeq;
				foreach (var elem in Variant) { if (elem != null) yield return elem; }
				if (ObservedSeqElement != null) yield return ObservedSeqElement;
				foreach (var elem in Quality) { if (elem != null) yield return elem; }
				if (ReadCoverageElement != null) yield return ReadCoverageElement;
				foreach (var elem in Repository) { if (elem != null) yield return elem; }
				foreach (var elem in Pointer) { if (elem != null) yield return elem; }
				foreach (var elem in StructureVariant) { if (elem != null) yield return elem; }
            }
        }
    }
    
}
