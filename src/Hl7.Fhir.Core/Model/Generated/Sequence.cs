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
// Generated for FHIR v1.3.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A Sequence
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
            /// The chromosome containing the genetic finding
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
                    if(value == null)
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
                    if(value == null)
                      ReferenceSeqStringElement = null; 
                    else
                      ReferenceSeqStringElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ReferenceSeqString");
                }
            }
            
            /// <summary>
            /// 0-based start position (inclusive) of the window on the  reference sequence
            /// </summary>
            [FhirElement("windowStart", InSummary=true, Order=90)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Integer WindowStartElement
            {
                get { return _WindowStartElement; }
                set { _WindowStartElement = value; OnPropertyChanged("WindowStartElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _WindowStartElement;
            
            /// <summary>
            /// 0-based start position (inclusive) of the window on the  reference sequence
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? WindowStart
            {
                get { return WindowStartElement != null ? WindowStartElement.Value : null; }
                set
                {
                    if(value == null)
                      WindowStartElement = null; 
                    else
                      WindowStartElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("WindowStart");
                }
            }
            
            /// <summary>
            /// 0-based end position (exclusive) of the window on the reference sequence
            /// </summary>
            [FhirElement("windowEnd", InSummary=true, Order=100)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Integer WindowEndElement
            {
                get { return _WindowEndElement; }
                set { _WindowEndElement = value; OnPropertyChanged("WindowEndElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _WindowEndElement;
            
            /// <summary>
            /// 0-based end position (exclusive) of the window on the reference sequence
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? WindowEnd
            {
                get { return WindowEndElement != null ? WindowEndElement.Value : null; }
                set
                {
                    if(value == null)
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
                if( !DeepComparable.IsExactly(WindowStartElement, otherT.WindowStartElement)) return false;
                if( !DeepComparable.IsExactly(WindowEndElement, otherT.WindowEndElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("VariationComponent")]
        [DataContract]
        public partial class VariationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "VariationComponent"; } }
            
            /// <summary>
            /// 0-based start position (inclusive) of the variation on the  reference sequence
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
            /// 0-based start position (inclusive) of the variation on the  reference sequence
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Start
            {
                get { return StartElement != null ? StartElement.Value : null; }
                set
                {
                    if(value == null)
                      StartElement = null; 
                    else
                      StartElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Start");
                }
            }
            
            /// <summary>
            /// 0-based end position (exclusive) of the variation on the reference sequence
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
            /// 0-based end position (exclusive) of the variation on the reference sequence
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? End
            {
                get { return EndElement != null ? EndElement.Value : null; }
                set
                {
                    if(value == null)
                      EndElement = null; 
                    else
                      EndElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("End");
                }
            }
            
            /// <summary>
            /// Nucleotide(s)/amino acids from start position to stop position of observed variation
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
            /// Nucleotide(s)/amino acids from start position to stop position of observed variation
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ObservedAllele
            {
                get { return ObservedAlleleElement != null ? ObservedAlleleElement.Value : null; }
                set
                {
                    if(value == null)
                      ObservedAlleleElement = null; 
                    else
                      ObservedAlleleElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ObservedAllele");
                }
            }
            
            /// <summary>
            /// Nucleotide(s)/amino acids from start position to stop position of reference variation
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
            /// Nucleotide(s)/amino acids from start position to stop position of reference variation
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ReferenceAllele
            {
                get { return ReferenceAlleleElement != null ? ReferenceAlleleElement.Value : null; }
                set
                {
                    if(value == null)
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
                    if(value == null)
                      CigarElement = null; 
                    else
                      CigarElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Cigar");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as VariationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(StartElement != null) dest.StartElement = (Hl7.Fhir.Model.Integer)StartElement.DeepCopy();
                    if(EndElement != null) dest.EndElement = (Hl7.Fhir.Model.Integer)EndElement.DeepCopy();
                    if(ObservedAlleleElement != null) dest.ObservedAlleleElement = (Hl7.Fhir.Model.FhirString)ObservedAlleleElement.DeepCopy();
                    if(ReferenceAlleleElement != null) dest.ReferenceAlleleElement = (Hl7.Fhir.Model.FhirString)ReferenceAlleleElement.DeepCopy();
                    if(CigarElement != null) dest.CigarElement = (Hl7.Fhir.Model.FhirString)CigarElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new VariationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as VariationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(StartElement, otherT.StartElement)) return false;
                if( !DeepComparable.Matches(EndElement, otherT.EndElement)) return false;
                if( !DeepComparable.Matches(ObservedAlleleElement, otherT.ObservedAlleleElement)) return false;
                if( !DeepComparable.Matches(ReferenceAlleleElement, otherT.ReferenceAlleleElement)) return false;
                if( !DeepComparable.Matches(CigarElement, otherT.CigarElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as VariationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(StartElement, otherT.StartElement)) return false;
                if( !DeepComparable.IsExactly(EndElement, otherT.EndElement)) return false;
                if( !DeepComparable.IsExactly(ObservedAlleleElement, otherT.ObservedAlleleElement)) return false;
                if( !DeepComparable.IsExactly(ReferenceAlleleElement, otherT.ReferenceAlleleElement)) return false;
                if( !DeepComparable.IsExactly(CigarElement, otherT.CigarElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("QualityComponent")]
        [DataContract]
        public partial class QualityComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "QualityComponent"; } }
            
            /// <summary>
            /// 0-based start position (inclusive) of the sequence
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
            /// 0-based start position (inclusive) of the sequence
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Start
            {
                get { return StartElement != null ? StartElement.Value : null; }
                set
                {
                    if(value == null)
                      StartElement = null; 
                    else
                      StartElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Start");
                }
            }
            
            /// <summary>
            /// 0-based end position (exclusive) of the sequence
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
            /// 0-based end position (exclusive) of the sequence
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? End
            {
                get { return EndElement != null ? EndElement.Value : null; }
                set
                {
                    if(value == null)
                      EndElement = null; 
                    else
                      EndElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("End");
                }
            }
            
            /// <summary>
            /// Quality score
            /// </summary>
            [FhirElement("score", InSummary=true, Order=60)]
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
            [FhirElement("method", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString MethodElement
            {
                get { return _MethodElement; }
                set { _MethodElement = value; OnPropertyChanged("MethodElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _MethodElement;
            
            /// <summary>
            /// Method for quality
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Method
            {
                get { return MethodElement != null ? MethodElement.Value : null; }
                set
                {
                    if(value == null)
                      MethodElement = null; 
                    else
                      MethodElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Method");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as QualityComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(StartElement != null) dest.StartElement = (Hl7.Fhir.Model.Integer)StartElement.DeepCopy();
                    if(EndElement != null) dest.EndElement = (Hl7.Fhir.Model.Integer)EndElement.DeepCopy();
                    if(Score != null) dest.Score = (Quantity)Score.DeepCopy();
                    if(MethodElement != null) dest.MethodElement = (Hl7.Fhir.Model.FhirString)MethodElement.DeepCopy();
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
                if( !DeepComparable.Matches(StartElement, otherT.StartElement)) return false;
                if( !DeepComparable.Matches(EndElement, otherT.EndElement)) return false;
                if( !DeepComparable.Matches(Score, otherT.Score)) return false;
                if( !DeepComparable.Matches(MethodElement, otherT.MethodElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as QualityComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(StartElement, otherT.StartElement)) return false;
                if( !DeepComparable.IsExactly(EndElement, otherT.EndElement)) return false;
                if( !DeepComparable.IsExactly(Score, otherT.Score)) return false;
                if( !DeepComparable.IsExactly(MethodElement, otherT.MethodElement)) return false;
                
                return true;
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
                    if(value == null)
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
                    if(value == null)
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
                    if(value == null)
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
                    if(value == null)
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
            
        }
        
        
        [FhirType("StructureVariationComponent")]
        [DataContract]
        public partial class StructureVariationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "StructureVariationComponent"; } }
            
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
                    if(value == null)
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
                    if(value == null)
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
                    if(value == null)
                      LengthElement = null; 
                    else
                      LengthElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Length");
                }
            }
            
            /// <summary>
            /// 
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
            /// 
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
                var dest = other as StructureVariationComponent;
                
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
                return CopyTo(new StructureVariationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as StructureVariationComponent;
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
                var otherT = other as StructureVariationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(PrecisionOfBoundariesElement, otherT.PrecisionOfBoundariesElement)) return false;
                if( !DeepComparable.IsExactly(ReportedaCGHRatioElement, otherT.ReportedaCGHRatioElement)) return false;
                if( !DeepComparable.IsExactly(LengthElement, otherT.LengthElement)) return false;
                if( !DeepComparable.IsExactly(Outer, otherT.Outer)) return false;
                if( !DeepComparable.IsExactly(Inner, otherT.Inner)) return false;
                
                return true;
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
                    if(value == null)
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
                    if(value == null)
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
                    if(value == null)
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
                    if(value == null)
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
            
        }
        
        
        /// <summary>
        /// AA | DNA | RNA
        /// </summary>
        [FhirElement("type", InSummary=true, Order=90)]
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
                if(value == null)
                  TypeElement = null; 
                else
                  TypeElement = new Hl7.Fhir.Model.Code(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// Who and/or what this is about
        /// </summary>
        [FhirElement("patient", InSummary=true, Order=100)]
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
        [FhirElement("specimen", InSummary=true, Order=110)]
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
        [FhirElement("device", InSummary=true, Order=120)]
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
        [FhirElement("quantity", InSummary=true, Order=130)]
        [DataMember]
        public Quantity Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; OnPropertyChanged("Quantity"); }
        }
        
        private Quantity _Quantity;
        
        /// <summary>
        /// Supporting tests of human, viruses, and bacteria
        /// </summary>
        [FhirElement("species", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Species
        {
            get { return _Species; }
            set { _Species = value; OnPropertyChanged("Species"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Species;
        
        /// <summary>
        /// Reference sequence
        /// </summary>
        [FhirElement("referenceSeq", InSummary=true, Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Sequence.ReferenceSeqComponent> ReferenceSeq
        {
            get { if(_ReferenceSeq==null) _ReferenceSeq = new List<Hl7.Fhir.Model.Sequence.ReferenceSeqComponent>(); return _ReferenceSeq; }
            set { _ReferenceSeq = value; OnPropertyChanged("ReferenceSeq"); }
        }
        
        private List<Hl7.Fhir.Model.Sequence.ReferenceSeqComponent> _ReferenceSeq;
        
        /// <summary>
        /// Variation info in this sequence
        /// </summary>
        [FhirElement("variation", InSummary=true, Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.Sequence.VariationComponent Variation
        {
            get { return _Variation; }
            set { _Variation = value; OnPropertyChanged("Variation"); }
        }
        
        private Hl7.Fhir.Model.Sequence.VariationComponent _Variation;
        
        /// <summary>
        /// Sequence Quality
        /// </summary>
        [FhirElement("quality", InSummary=true, Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Sequence.QualityComponent> Quality
        {
            get { if(_Quality==null) _Quality = new List<Hl7.Fhir.Model.Sequence.QualityComponent>(); return _Quality; }
            set { _Quality = value; OnPropertyChanged("Quality"); }
        }
        
        private List<Hl7.Fhir.Model.Sequence.QualityComponent> _Quality;
        
        /// <summary>
        /// The level of occurrence of a single DNA Sequence Variation within a set of chromosomes: Heteroplasmic / Homoplasmic / Homozygous / Heterozygous / Hemizygous
        /// </summary>
        [FhirElement("allelicState", InSummary=true, Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept AllelicState
        {
            get { return _AllelicState; }
            set { _AllelicState = value; OnPropertyChanged("AllelicState"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _AllelicState;
        
        /// <summary>
        /// Allele frequencies
        /// </summary>
        [FhirElement("allelicFrequency", InSummary=true, Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDecimal AllelicFrequencyElement
        {
            get { return _AllelicFrequencyElement; }
            set { _AllelicFrequencyElement = value; OnPropertyChanged("AllelicFrequencyElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDecimal _AllelicFrequencyElement;
        
        /// <summary>
        /// Allele frequencies
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public decimal? AllelicFrequency
        {
            get { return AllelicFrequencyElement != null ? AllelicFrequencyElement.Value : null; }
            set
            {
                if(value == null)
                  AllelicFrequencyElement = null; 
                else
                  AllelicFrequencyElement = new Hl7.Fhir.Model.FhirDecimal(value);
                OnPropertyChanged("AllelicFrequency");
            }
        }
        
        /// <summary>
        /// Copy Number Event: Values: amplificaiton / deletion / LOH
        /// </summary>
        [FhirElement("copyNumberEvent", InSummary=true, Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept CopyNumberEvent
        {
            get { return _CopyNumberEvent; }
            set { _CopyNumberEvent = value; OnPropertyChanged("CopyNumberEvent"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _CopyNumberEvent;
        
        /// <summary>
        /// Average number of reads representing a given nucleotide in the reconstructed sequence
        /// </summary>
        [FhirElement("readCoverage", InSummary=true, Order=210)]
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
                if(value == null)
                  ReadCoverageElement = null; 
                else
                  ReadCoverageElement = new Hl7.Fhir.Model.Integer(value);
                OnPropertyChanged("ReadCoverage");
            }
        }
        
        /// <summary>
        /// External repository
        /// </summary>
        [FhirElement("repository", InSummary=true, Order=220)]
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
        [FhirElement("pointer", InSummary=true, Order=230)]
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
        /// Observed Sequence
        /// </summary>
        [FhirElement("observedSeq", InSummary=true, Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ObservedSeqElement
        {
            get { return _ObservedSeqElement; }
            set { _ObservedSeqElement = value; OnPropertyChanged("ObservedSeqElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ObservedSeqElement;
        
        /// <summary>
        /// Observed Sequence
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ObservedSeq
        {
            get { return ObservedSeqElement != null ? ObservedSeqElement.Value : null; }
            set
            {
                if(value == null)
                  ObservedSeqElement = null; 
                else
                  ObservedSeqElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("ObservedSeq");
            }
        }
        
        /// <summary>
        /// Observation-genetics
        /// </summary>
        [FhirElement("observation", InSummary=true, Order=250)]
        [References("Observation")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Observation
        {
            get { return _Observation; }
            set { _Observation = value; OnPropertyChanged("Observation"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Observation;
        
        /// <summary>
        /// 
        /// </summary>
        [FhirElement("structureVariation", InSummary=true, Order=260)]
        [DataMember]
        public Hl7.Fhir.Model.Sequence.StructureVariationComponent StructureVariation
        {
            get { return _StructureVariation; }
            set { _StructureVariation = value; OnPropertyChanged("StructureVariation"); }
        }
        
        private Hl7.Fhir.Model.Sequence.StructureVariationComponent _StructureVariation;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Sequence;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.Code)TypeElement.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Specimen != null) dest.Specimen = (Hl7.Fhir.Model.ResourceReference)Specimen.DeepCopy();
                if(Device != null) dest.Device = (Hl7.Fhir.Model.ResourceReference)Device.DeepCopy();
                if(Quantity != null) dest.Quantity = (Quantity)Quantity.DeepCopy();
                if(Species != null) dest.Species = (Hl7.Fhir.Model.CodeableConcept)Species.DeepCopy();
                if(ReferenceSeq != null) dest.ReferenceSeq = new List<Hl7.Fhir.Model.Sequence.ReferenceSeqComponent>(ReferenceSeq.DeepCopy());
                if(Variation != null) dest.Variation = (Hl7.Fhir.Model.Sequence.VariationComponent)Variation.DeepCopy();
                if(Quality != null) dest.Quality = new List<Hl7.Fhir.Model.Sequence.QualityComponent>(Quality.DeepCopy());
                if(AllelicState != null) dest.AllelicState = (Hl7.Fhir.Model.CodeableConcept)AllelicState.DeepCopy();
                if(AllelicFrequencyElement != null) dest.AllelicFrequencyElement = (Hl7.Fhir.Model.FhirDecimal)AllelicFrequencyElement.DeepCopy();
                if(CopyNumberEvent != null) dest.CopyNumberEvent = (Hl7.Fhir.Model.CodeableConcept)CopyNumberEvent.DeepCopy();
                if(ReadCoverageElement != null) dest.ReadCoverageElement = (Hl7.Fhir.Model.Integer)ReadCoverageElement.DeepCopy();
                if(Repository != null) dest.Repository = new List<Hl7.Fhir.Model.Sequence.RepositoryComponent>(Repository.DeepCopy());
                if(Pointer != null) dest.Pointer = new List<Hl7.Fhir.Model.ResourceReference>(Pointer.DeepCopy());
                if(ObservedSeqElement != null) dest.ObservedSeqElement = (Hl7.Fhir.Model.FhirString)ObservedSeqElement.DeepCopy();
                if(Observation != null) dest.Observation = (Hl7.Fhir.Model.ResourceReference)Observation.DeepCopy();
                if(StructureVariation != null) dest.StructureVariation = (Hl7.Fhir.Model.Sequence.StructureVariationComponent)StructureVariation.DeepCopy();
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
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Specimen, otherT.Specimen)) return false;
            if( !DeepComparable.Matches(Device, otherT.Device)) return false;
            if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.Matches(Species, otherT.Species)) return false;
            if( !DeepComparable.Matches(ReferenceSeq, otherT.ReferenceSeq)) return false;
            if( !DeepComparable.Matches(Variation, otherT.Variation)) return false;
            if( !DeepComparable.Matches(Quality, otherT.Quality)) return false;
            if( !DeepComparable.Matches(AllelicState, otherT.AllelicState)) return false;
            if( !DeepComparable.Matches(AllelicFrequencyElement, otherT.AllelicFrequencyElement)) return false;
            if( !DeepComparable.Matches(CopyNumberEvent, otherT.CopyNumberEvent)) return false;
            if( !DeepComparable.Matches(ReadCoverageElement, otherT.ReadCoverageElement)) return false;
            if( !DeepComparable.Matches(Repository, otherT.Repository)) return false;
            if( !DeepComparable.Matches(Pointer, otherT.Pointer)) return false;
            if( !DeepComparable.Matches(ObservedSeqElement, otherT.ObservedSeqElement)) return false;
            if( !DeepComparable.Matches(Observation, otherT.Observation)) return false;
            if( !DeepComparable.Matches(StructureVariation, otherT.StructureVariation)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Sequence;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Specimen, otherT.Specimen)) return false;
            if( !DeepComparable.IsExactly(Device, otherT.Device)) return false;
            if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.IsExactly(Species, otherT.Species)) return false;
            if( !DeepComparable.IsExactly(ReferenceSeq, otherT.ReferenceSeq)) return false;
            if( !DeepComparable.IsExactly(Variation, otherT.Variation)) return false;
            if( !DeepComparable.IsExactly(Quality, otherT.Quality)) return false;
            if( !DeepComparable.IsExactly(AllelicState, otherT.AllelicState)) return false;
            if( !DeepComparable.IsExactly(AllelicFrequencyElement, otherT.AllelicFrequencyElement)) return false;
            if( !DeepComparable.IsExactly(CopyNumberEvent, otherT.CopyNumberEvent)) return false;
            if( !DeepComparable.IsExactly(ReadCoverageElement, otherT.ReadCoverageElement)) return false;
            if( !DeepComparable.IsExactly(Repository, otherT.Repository)) return false;
            if( !DeepComparable.IsExactly(Pointer, otherT.Pointer)) return false;
            if( !DeepComparable.IsExactly(ObservedSeqElement, otherT.ObservedSeqElement)) return false;
            if( !DeepComparable.IsExactly(Observation, otherT.Observation)) return false;
            if( !DeepComparable.IsExactly(StructureVariation, otherT.StructureVariation)) return false;
            
            return true;
        }
        
    }
    
}
