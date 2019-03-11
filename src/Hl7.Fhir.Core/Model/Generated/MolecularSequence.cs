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
// Generated for FHIR v4.0.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Information about a biological sequence
    /// </summary>
    [FhirType("MolecularSequence", IsResource=true)]
    [DataContract]
    public partial class MolecularSequence : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.MolecularSequence; } }
        [NotMapped]
        public override string TypeName { get { return "MolecularSequence"; } }
        
        /// <summary>
        /// Type if a sequence -- DNA, RNA, or amino acid sequence.
        /// (url: http://hl7.org/fhir/ValueSet/sequence-type)
        /// </summary>
        [FhirEnumeration("sequenceType")]
        public enum sequenceType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/sequence-type)
            /// </summary>
            [EnumLiteral("aa", "http://hl7.org/fhir/sequence-type"), Description("AA Sequence")]
            Aa,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/sequence-type)
            /// </summary>
            [EnumLiteral("dna", "http://hl7.org/fhir/sequence-type"), Description("DNA Sequence")]
            Dna,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/sequence-type)
            /// </summary>
            [EnumLiteral("rna", "http://hl7.org/fhir/sequence-type"), Description("RNA Sequence")]
            Rna,
        }

        /// <summary>
        /// Type for orientation.
        /// (url: http://hl7.org/fhir/ValueSet/orientation-type)
        /// </summary>
        [FhirEnumeration("orientationType")]
        public enum orientationType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/orientation-type)
            /// </summary>
            [EnumLiteral("sense", "http://hl7.org/fhir/orientation-type"), Description("Sense orientation of referenceSeq")]
            Sense,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/orientation-type)
            /// </summary>
            [EnumLiteral("antisense", "http://hl7.org/fhir/orientation-type"), Description("Antisense orientation of referenceSeq")]
            Antisense,
        }

        /// <summary>
        /// Type for strand.
        /// (url: http://hl7.org/fhir/ValueSet/strand-type)
        /// </summary>
        [FhirEnumeration("strandType")]
        public enum strandType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/strand-type)
            /// </summary>
            [EnumLiteral("watson", "http://hl7.org/fhir/strand-type"), Description("Watson strand of referenceSeq")]
            Watson,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/strand-type)
            /// </summary>
            [EnumLiteral("crick", "http://hl7.org/fhir/strand-type"), Description("Crick strand of referenceSeq")]
            Crick,
        }

        /// <summary>
        /// Type for quality report.
        /// (url: http://hl7.org/fhir/ValueSet/quality-type)
        /// </summary>
        [FhirEnumeration("qualityType")]
        public enum qualityType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/quality-type)
            /// </summary>
            [EnumLiteral("indel", "http://hl7.org/fhir/quality-type"), Description("INDEL Comparison")]
            Indel,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/quality-type)
            /// </summary>
            [EnumLiteral("snp", "http://hl7.org/fhir/quality-type"), Description("SNP Comparison")]
            Snp,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/quality-type)
            /// </summary>
            [EnumLiteral("unknown", "http://hl7.org/fhir/quality-type"), Description("UNKNOWN Comparison")]
            Unknown,
        }

        /// <summary>
        /// Type for access of external URI.
        /// (url: http://hl7.org/fhir/ValueSet/repository-type)
        /// </summary>
        [FhirEnumeration("repositoryType")]
        public enum repositoryType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/repository-type)
            /// </summary>
            [EnumLiteral("directlink", "http://hl7.org/fhir/repository-type"), Description("Click and see")]
            Directlink,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/repository-type)
            /// </summary>
            [EnumLiteral("openapi", "http://hl7.org/fhir/repository-type"), Description("The URL is the RESTful or other kind of API that can access to the result.")]
            Openapi,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/repository-type)
            /// </summary>
            [EnumLiteral("login", "http://hl7.org/fhir/repository-type"), Description("Result cannot be access unless an account is logged in")]
            Login,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/repository-type)
            /// </summary>
            [EnumLiteral("oauth", "http://hl7.org/fhir/repository-type"), Description("Result need to be fetched with API and need LOGIN( or cookies are required when visiting the link of resource)")]
            Oauth,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/repository-type)
            /// </summary>
            [EnumLiteral("other", "http://hl7.org/fhir/repository-type"), Description("Some other complicated or particular way to get resource from URL.")]
            Other,
        }

        [FhirType("ReferenceSeqComponent", NamedBackboneElement=true)]
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
            /// sense | antisense
            /// </summary>
            [FhirElement("orientation", InSummary=true, Order=60)]
            [DataMember]
            public Code<Hl7.Fhir.Model.MolecularSequence.orientationType> OrientationElement
            {
                get { return _OrientationElement; }
                set { _OrientationElement = value; OnPropertyChanged("OrientationElement"); }
            }
            
            private Code<Hl7.Fhir.Model.MolecularSequence.orientationType> _OrientationElement;
            
            /// <summary>
            /// sense | antisense
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.MolecularSequence.orientationType? Orientation
            {
                get { return OrientationElement != null ? OrientationElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        OrientationElement = null; 
                    else
                        OrientationElement = new Code<Hl7.Fhir.Model.MolecularSequence.orientationType>(value);
                    OnPropertyChanged("Orientation");
                }
            }
            
            /// <summary>
            /// Reference identifier
            /// </summary>
            [FhirElement("referenceSeqId", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept ReferenceSeqId
            {
                get { return _ReferenceSeqId; }
                set { _ReferenceSeqId = value; OnPropertyChanged("ReferenceSeqId"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _ReferenceSeqId;
            
            /// <summary>
            /// A pointer to another MolecularSequence entity as reference sequence
            /// </summary>
            [FhirElement("referenceSeqPointer", InSummary=true, Order=80)]
            [CLSCompliant(false)]
			[References("MolecularSequence")]
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
            [FhirElement("referenceSeqString", InSummary=true, Order=90)]
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
            /// watson | crick
            /// </summary>
            [FhirElement("strand", InSummary=true, Order=100)]
            [DataMember]
            public Code<Hl7.Fhir.Model.MolecularSequence.strandType> StrandElement
            {
                get { return _StrandElement; }
                set { _StrandElement = value; OnPropertyChanged("StrandElement"); }
            }
            
            private Code<Hl7.Fhir.Model.MolecularSequence.strandType> _StrandElement;
            
            /// <summary>
            /// watson | crick
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.MolecularSequence.strandType? Strand
            {
                get { return StrandElement != null ? StrandElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        StrandElement = null; 
                    else
                        StrandElement = new Code<Hl7.Fhir.Model.MolecularSequence.strandType>(value);
                    OnPropertyChanged("Strand");
                }
            }
            
            /// <summary>
            /// Start position of the window on the  reference sequence
            /// </summary>
            [FhirElement("windowStart", InSummary=true, Order=110)]
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
                    if (!value.HasValue)
                        WindowStartElement = null; 
                    else
                        WindowStartElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("WindowStart");
                }
            }
            
            /// <summary>
            /// End position of the window on the reference sequence
            /// </summary>
            [FhirElement("windowEnd", InSummary=true, Order=120)]
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
                    if(OrientationElement != null) dest.OrientationElement = (Code<Hl7.Fhir.Model.MolecularSequence.orientationType>)OrientationElement.DeepCopy();
                    if(ReferenceSeqId != null) dest.ReferenceSeqId = (Hl7.Fhir.Model.CodeableConcept)ReferenceSeqId.DeepCopy();
                    if(ReferenceSeqPointer != null) dest.ReferenceSeqPointer = (Hl7.Fhir.Model.ResourceReference)ReferenceSeqPointer.DeepCopy();
                    if(ReferenceSeqStringElement != null) dest.ReferenceSeqStringElement = (Hl7.Fhir.Model.FhirString)ReferenceSeqStringElement.DeepCopy();
                    if(StrandElement != null) dest.StrandElement = (Code<Hl7.Fhir.Model.MolecularSequence.strandType>)StrandElement.DeepCopy();
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
                if( !DeepComparable.Matches(OrientationElement, otherT.OrientationElement)) return false;
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
                if( !DeepComparable.IsExactly(OrientationElement, otherT.OrientationElement)) return false;
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
                    if (OrientationElement != null) yield return OrientationElement;
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
                    if (OrientationElement != null) yield return new ElementValue("orientation", OrientationElement);
                    if (ReferenceSeqId != null) yield return new ElementValue("referenceSeqId", ReferenceSeqId);
                    if (ReferenceSeqPointer != null) yield return new ElementValue("referenceSeqPointer", ReferenceSeqPointer);
                    if (ReferenceSeqStringElement != null) yield return new ElementValue("referenceSeqString", ReferenceSeqStringElement);
                    if (StrandElement != null) yield return new ElementValue("strand", StrandElement);
                    if (WindowStartElement != null) yield return new ElementValue("windowStart", WindowStartElement);
                    if (WindowEndElement != null) yield return new ElementValue("windowEnd", WindowEndElement);
                }
            }

            
        }
        
        
        [FhirType("VariantComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class VariantComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "VariantComponent"; } }
            
            /// <summary>
            /// Start position of the variant on the  reference sequence
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
                    if (!value.HasValue)
                        StartElement = null; 
                    else
                        StartElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Start");
                }
            }
            
            /// <summary>
            /// End position of the variant on the reference sequence
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
            /// Allele in the reference sequence
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
            [CLSCompliant(false)]
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
        
        
        [FhirType("QualityComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class QualityComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "QualityComponent"; } }
            
            /// <summary>
            /// indel | snp | unknown
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.MolecularSequence.qualityType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.MolecularSequence.qualityType> _TypeElement;
            
            /// <summary>
            /// indel | snp | unknown
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.MolecularSequence.qualityType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        TypeElement = null; 
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.MolecularSequence.qualityType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Standard sequence for comparison
            /// </summary>
            [FhirElement("standardSequence", InSummary=true, Order=50)]
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
            [FhirElement("start", InSummary=true, Order=60)]
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
                    if (!value.HasValue)
                        StartElement = null; 
                    else
                        StartElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Start");
                }
            }
            
            /// <summary>
            /// End position of the sequence
            /// </summary>
            [FhirElement("end", InSummary=true, Order=70)]
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
                    if (!value.HasValue)
                        EndElement = null; 
                    else
                        EndElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("End");
                }
            }
            
            /// <summary>
            /// Quality score for the comparison
            /// </summary>
            [FhirElement("score", InSummary=true, Order=80)]
            [DataMember]
            public Quantity Score
            {
                get { return _Score; }
                set { _Score = value; OnPropertyChanged("Score"); }
            }
            
            private Quantity _Score;
            
            /// <summary>
            /// Method to get quality
            /// </summary>
            [FhirElement("method", InSummary=true, Order=90)]
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
            [FhirElement("truthTP", InSummary=true, Order=100)]
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
            [FhirElement("queryTP", InSummary=true, Order=110)]
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
            [FhirElement("truthFN", InSummary=true, Order=120)]
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
            [FhirElement("queryFP", InSummary=true, Order=130)]
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
            [FhirElement("gtFP", InSummary=true, Order=140)]
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
            /// Precision of comparison
            /// </summary>
            [FhirElement("precision", InSummary=true, Order=150)]
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
                    if (!value.HasValue)
                        PrecisionElement = null; 
                    else
                        PrecisionElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Precision");
                }
            }
            
            /// <summary>
            /// Recall of comparison
            /// </summary>
            [FhirElement("recall", InSummary=true, Order=160)]
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
            [FhirElement("fScore", InSummary=true, Order=170)]
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
            
            /// <summary>
            /// Receiver Operator Characteristic (ROC) Curve
            /// </summary>
            [FhirElement("roc", InSummary=true, Order=180)]
            [DataMember]
            public Hl7.Fhir.Model.MolecularSequence.RocComponent Roc
            {
                get { return _Roc; }
                set { _Roc = value; OnPropertyChanged("Roc"); }
            }
            
            private Hl7.Fhir.Model.MolecularSequence.RocComponent _Roc;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as QualityComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.MolecularSequence.qualityType>)TypeElement.DeepCopy();
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
                    if(Roc != null) dest.Roc = (Hl7.Fhir.Model.MolecularSequence.RocComponent)Roc.DeepCopy();
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
                if( !DeepComparable.Matches(Roc, otherT.Roc)) return false;
                
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
                if( !DeepComparable.IsExactly(Roc, otherT.Roc)) return false;
                
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
                    if (Roc != null) yield return Roc;
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
                    if (Roc != null) yield return new ElementValue("roc", Roc);
                }
            }

            
        }
        
        
        [FhirType("RocComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class RocComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "RocComponent"; } }
            
            /// <summary>
            /// Genotype quality score
            /// </summary>
            [FhirElement("score", InSummary=true, Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Integer> ScoreElement
            {
                get { if(_ScoreElement==null) _ScoreElement = new List<Hl7.Fhir.Model.Integer>(); return _ScoreElement; }
                set { _ScoreElement = value; OnPropertyChanged("ScoreElement"); }
            }
            
            private List<Hl7.Fhir.Model.Integer> _ScoreElement;
            
            /// <summary>
            /// Genotype quality score
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> Score
            {
                get { return ScoreElement != null ? ScoreElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        ScoreElement = null; 
                    else
                        ScoreElement = new List<Hl7.Fhir.Model.Integer>(value.Select(elem=>new Hl7.Fhir.Model.Integer(elem)));
                    OnPropertyChanged("Score");
                }
            }
            
            /// <summary>
            /// Roc score true positive numbers
            /// </summary>
            [FhirElement("numTP", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Integer> NumTPElement
            {
                get { if(_NumTPElement==null) _NumTPElement = new List<Hl7.Fhir.Model.Integer>(); return _NumTPElement; }
                set { _NumTPElement = value; OnPropertyChanged("NumTPElement"); }
            }
            
            private List<Hl7.Fhir.Model.Integer> _NumTPElement;
            
            /// <summary>
            /// Roc score true positive numbers
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> NumTP
            {
                get { return NumTPElement != null ? NumTPElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        NumTPElement = null; 
                    else
                        NumTPElement = new List<Hl7.Fhir.Model.Integer>(value.Select(elem=>new Hl7.Fhir.Model.Integer(elem)));
                    OnPropertyChanged("NumTP");
                }
            }
            
            /// <summary>
            /// Roc score false positive numbers
            /// </summary>
            [FhirElement("numFP", InSummary=true, Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Integer> NumFPElement
            {
                get { if(_NumFPElement==null) _NumFPElement = new List<Hl7.Fhir.Model.Integer>(); return _NumFPElement; }
                set { _NumFPElement = value; OnPropertyChanged("NumFPElement"); }
            }
            
            private List<Hl7.Fhir.Model.Integer> _NumFPElement;
            
            /// <summary>
            /// Roc score false positive numbers
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> NumFP
            {
                get { return NumFPElement != null ? NumFPElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        NumFPElement = null; 
                    else
                        NumFPElement = new List<Hl7.Fhir.Model.Integer>(value.Select(elem=>new Hl7.Fhir.Model.Integer(elem)));
                    OnPropertyChanged("NumFP");
                }
            }
            
            /// <summary>
            /// Roc score false negative numbers
            /// </summary>
            [FhirElement("numFN", InSummary=true, Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Integer> NumFNElement
            {
                get { if(_NumFNElement==null) _NumFNElement = new List<Hl7.Fhir.Model.Integer>(); return _NumFNElement; }
                set { _NumFNElement = value; OnPropertyChanged("NumFNElement"); }
            }
            
            private List<Hl7.Fhir.Model.Integer> _NumFNElement;
            
            /// <summary>
            /// Roc score false negative numbers
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> NumFN
            {
                get { return NumFNElement != null ? NumFNElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        NumFNElement = null; 
                    else
                        NumFNElement = new List<Hl7.Fhir.Model.Integer>(value.Select(elem=>new Hl7.Fhir.Model.Integer(elem)));
                    OnPropertyChanged("NumFN");
                }
            }
            
            /// <summary>
            /// Precision of the GQ score
            /// </summary>
            [FhirElement("precision", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirDecimal> PrecisionElement
            {
                get { if(_PrecisionElement==null) _PrecisionElement = new List<Hl7.Fhir.Model.FhirDecimal>(); return _PrecisionElement; }
                set { _PrecisionElement = value; OnPropertyChanged("PrecisionElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirDecimal> _PrecisionElement;
            
            /// <summary>
            /// Precision of the GQ score
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<decimal?> Precision
            {
                get { return PrecisionElement != null ? PrecisionElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        PrecisionElement = null; 
                    else
                        PrecisionElement = new List<Hl7.Fhir.Model.FhirDecimal>(value.Select(elem=>new Hl7.Fhir.Model.FhirDecimal(elem)));
                    OnPropertyChanged("Precision");
                }
            }
            
            /// <summary>
            /// Sensitivity of the GQ score
            /// </summary>
            [FhirElement("sensitivity", InSummary=true, Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirDecimal> SensitivityElement
            {
                get { if(_SensitivityElement==null) _SensitivityElement = new List<Hl7.Fhir.Model.FhirDecimal>(); return _SensitivityElement; }
                set { _SensitivityElement = value; OnPropertyChanged("SensitivityElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirDecimal> _SensitivityElement;
            
            /// <summary>
            /// Sensitivity of the GQ score
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<decimal?> Sensitivity
            {
                get { return SensitivityElement != null ? SensitivityElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        SensitivityElement = null; 
                    else
                        SensitivityElement = new List<Hl7.Fhir.Model.FhirDecimal>(value.Select(elem=>new Hl7.Fhir.Model.FhirDecimal(elem)));
                    OnPropertyChanged("Sensitivity");
                }
            }
            
            /// <summary>
            /// FScore of the GQ score
            /// </summary>
            [FhirElement("fMeasure", InSummary=true, Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirDecimal> FMeasureElement
            {
                get { if(_FMeasureElement==null) _FMeasureElement = new List<Hl7.Fhir.Model.FhirDecimal>(); return _FMeasureElement; }
                set { _FMeasureElement = value; OnPropertyChanged("FMeasureElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirDecimal> _FMeasureElement;
            
            /// <summary>
            /// FScore of the GQ score
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<decimal?> FMeasure
            {
                get { return FMeasureElement != null ? FMeasureElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        FMeasureElement = null; 
                    else
                        FMeasureElement = new List<Hl7.Fhir.Model.FhirDecimal>(value.Select(elem=>new Hl7.Fhir.Model.FhirDecimal(elem)));
                    OnPropertyChanged("FMeasure");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RocComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ScoreElement != null) dest.ScoreElement = new List<Hl7.Fhir.Model.Integer>(ScoreElement.DeepCopy());
                    if(NumTPElement != null) dest.NumTPElement = new List<Hl7.Fhir.Model.Integer>(NumTPElement.DeepCopy());
                    if(NumFPElement != null) dest.NumFPElement = new List<Hl7.Fhir.Model.Integer>(NumFPElement.DeepCopy());
                    if(NumFNElement != null) dest.NumFNElement = new List<Hl7.Fhir.Model.Integer>(NumFNElement.DeepCopy());
                    if(PrecisionElement != null) dest.PrecisionElement = new List<Hl7.Fhir.Model.FhirDecimal>(PrecisionElement.DeepCopy());
                    if(SensitivityElement != null) dest.SensitivityElement = new List<Hl7.Fhir.Model.FhirDecimal>(SensitivityElement.DeepCopy());
                    if(FMeasureElement != null) dest.FMeasureElement = new List<Hl7.Fhir.Model.FhirDecimal>(FMeasureElement.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new RocComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RocComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ScoreElement, otherT.ScoreElement)) return false;
                if( !DeepComparable.Matches(NumTPElement, otherT.NumTPElement)) return false;
                if( !DeepComparable.Matches(NumFPElement, otherT.NumFPElement)) return false;
                if( !DeepComparable.Matches(NumFNElement, otherT.NumFNElement)) return false;
                if( !DeepComparable.Matches(PrecisionElement, otherT.PrecisionElement)) return false;
                if( !DeepComparable.Matches(SensitivityElement, otherT.SensitivityElement)) return false;
                if( !DeepComparable.Matches(FMeasureElement, otherT.FMeasureElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RocComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ScoreElement, otherT.ScoreElement)) return false;
                if( !DeepComparable.IsExactly(NumTPElement, otherT.NumTPElement)) return false;
                if( !DeepComparable.IsExactly(NumFPElement, otherT.NumFPElement)) return false;
                if( !DeepComparable.IsExactly(NumFNElement, otherT.NumFNElement)) return false;
                if( !DeepComparable.IsExactly(PrecisionElement, otherT.PrecisionElement)) return false;
                if( !DeepComparable.IsExactly(SensitivityElement, otherT.SensitivityElement)) return false;
                if( !DeepComparable.IsExactly(FMeasureElement, otherT.FMeasureElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in ScoreElement) { if (elem != null) yield return elem; }
                    foreach (var elem in NumTPElement) { if (elem != null) yield return elem; }
                    foreach (var elem in NumFPElement) { if (elem != null) yield return elem; }
                    foreach (var elem in NumFNElement) { if (elem != null) yield return elem; }
                    foreach (var elem in PrecisionElement) { if (elem != null) yield return elem; }
                    foreach (var elem in SensitivityElement) { if (elem != null) yield return elem; }
                    foreach (var elem in FMeasureElement) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in ScoreElement) { if (elem != null) yield return new ElementValue("score", elem); }
                    foreach (var elem in NumTPElement) { if (elem != null) yield return new ElementValue("numTP", elem); }
                    foreach (var elem in NumFPElement) { if (elem != null) yield return new ElementValue("numFP", elem); }
                    foreach (var elem in NumFNElement) { if (elem != null) yield return new ElementValue("numFN", elem); }
                    foreach (var elem in PrecisionElement) { if (elem != null) yield return new ElementValue("precision", elem); }
                    foreach (var elem in SensitivityElement) { if (elem != null) yield return new ElementValue("sensitivity", elem); }
                    foreach (var elem in FMeasureElement) { if (elem != null) yield return new ElementValue("fMeasure", elem); }
                }
            }

            
        }
        
        
        [FhirType("RepositoryComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class RepositoryComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "RepositoryComponent"; } }
            
            /// <summary>
            /// directlink | openapi | login | oauth | other
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.MolecularSequence.repositoryType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.MolecularSequence.repositoryType> _TypeElement;
            
            /// <summary>
            /// directlink | openapi | login | oauth | other
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.MolecularSequence.repositoryType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        TypeElement = null; 
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.MolecularSequence.repositoryType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// URI of the repository
            /// </summary>
            [FhirElement("url", InSummary=true, Order=50)]
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
            [FhirElement("name", InSummary=true, Order=60)]
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
            [FhirElement("datasetId", InSummary=true, Order=70)]
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
            [FhirElement("variantsetId", InSummary=true, Order=80)]
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
            [FhirElement("readsetId", InSummary=true, Order=90)]
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RepositoryComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.MolecularSequence.repositoryType>)TypeElement.DeepCopy();
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
        
        
        [FhirType("StructureVariantComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class StructureVariantComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "StructureVariantComponent"; } }
            
            /// <summary>
            /// Structural variant change type
            /// </summary>
            [FhirElement("variantType", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept VariantType
            {
                get { return _VariantType; }
                set { _VariantType = value; OnPropertyChanged("VariantType"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _VariantType;
            
            /// <summary>
            /// Does the structural variant have base pair resolution breakpoints?
            /// </summary>
            [FhirElement("exact", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ExactElement
            {
                get { return _ExactElement; }
                set { _ExactElement = value; OnPropertyChanged("ExactElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _ExactElement;
            
            /// <summary>
            /// Does the structural variant have base pair resolution breakpoints?
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Exact
            {
                get { return ExactElement != null ? ExactElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ExactElement = null; 
                    else
                        ExactElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Exact");
                }
            }
            
            /// <summary>
            /// Structural variant length
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
            /// Structural variant length
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
            public Hl7.Fhir.Model.MolecularSequence.OuterComponent Outer
            {
                get { return _Outer; }
                set { _Outer = value; OnPropertyChanged("Outer"); }
            }
            
            private Hl7.Fhir.Model.MolecularSequence.OuterComponent _Outer;
            
            /// <summary>
            /// Structural variant inner
            /// </summary>
            [FhirElement("inner", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.MolecularSequence.InnerComponent Inner
            {
                get { return _Inner; }
                set { _Inner = value; OnPropertyChanged("Inner"); }
            }
            
            private Hl7.Fhir.Model.MolecularSequence.InnerComponent _Inner;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StructureVariantComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(VariantType != null) dest.VariantType = (Hl7.Fhir.Model.CodeableConcept)VariantType.DeepCopy();
                    if(ExactElement != null) dest.ExactElement = (Hl7.Fhir.Model.FhirBoolean)ExactElement.DeepCopy();
                    if(LengthElement != null) dest.LengthElement = (Hl7.Fhir.Model.Integer)LengthElement.DeepCopy();
                    if(Outer != null) dest.Outer = (Hl7.Fhir.Model.MolecularSequence.OuterComponent)Outer.DeepCopy();
                    if(Inner != null) dest.Inner = (Hl7.Fhir.Model.MolecularSequence.InnerComponent)Inner.DeepCopy();
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
                if( !DeepComparable.Matches(VariantType, otherT.VariantType)) return false;
                if( !DeepComparable.Matches(ExactElement, otherT.ExactElement)) return false;
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
                if( !DeepComparable.IsExactly(VariantType, otherT.VariantType)) return false;
                if( !DeepComparable.IsExactly(ExactElement, otherT.ExactElement)) return false;
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
                    foreach (var item in base.Children) yield return item;
                    if (VariantType != null) yield return VariantType;
                    if (ExactElement != null) yield return ExactElement;
                    if (LengthElement != null) yield return LengthElement;
                    if (Outer != null) yield return Outer;
                    if (Inner != null) yield return Inner;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (VariantType != null) yield return new ElementValue("variantType", VariantType);
                    if (ExactElement != null) yield return new ElementValue("exact", ExactElement);
                    if (LengthElement != null) yield return new ElementValue("length", LengthElement);
                    if (Outer != null) yield return new ElementValue("outer", Outer);
                    if (Inner != null) yield return new ElementValue("inner", Inner);
                }
            }

            
        }
        
        
        [FhirType("OuterComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class OuterComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "OuterComponent"; } }
            
            /// <summary>
            /// Structural variant outer start
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
            /// Structural variant outer start
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
            /// Structural variant outer end
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
            /// Structural variant outer end
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
                    foreach (var item in base.Children) yield return item;
                    if (StartElement != null) yield return StartElement;
                    if (EndElement != null) yield return EndElement;
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
                }
            }

            
        }
        
        
        [FhirType("InnerComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class InnerComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "InnerComponent"; } }
            
            /// <summary>
            /// Structural variant inner start
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
            /// Structural variant inner start
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
            /// Structural variant inner end
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
            /// Structural variant inner end
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
                    foreach (var item in base.Children) yield return item;
                    if (StartElement != null) yield return StartElement;
                    if (EndElement != null) yield return EndElement;
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
                }
            }

            
        }
        
        
        /// <summary>
        /// Unique ID for this particular sequence. This is a FHIR-defined id
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
        /// aa | dna | rna
        /// </summary>
        [FhirElement("type", InSummary=true, Order=100)]
        [DataMember]
        public Code<Hl7.Fhir.Model.MolecularSequence.sequenceType> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.MolecularSequence.sequenceType> _TypeElement;
        
        /// <summary>
        /// aa | dna | rna
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.MolecularSequence.sequenceType? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  TypeElement = null; 
                else
                  TypeElement = new Code<Hl7.Fhir.Model.MolecularSequence.sequenceType>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// Base number of coordinate system (0 for 0-based numbering or coordinates, inclusive start, exclusive end, 1 for 1-based numbering, inclusive start, inclusive end)
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
        [FhirElement("specimen", InSummary=true, Order=130)]
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
        [FhirElement("device", InSummary=true, Order=140)]
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
        [FhirElement("performer", InSummary=true, Order=150)]
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
        /// The number of copies of the sequence of interest.  (RNASeq)
        /// </summary>
        [FhirElement("quantity", InSummary=true, Order=160)]
        [DataMember]
        public Quantity Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; OnPropertyChanged("Quantity"); }
        }
        
        private Quantity _Quantity;
        
        /// <summary>
        /// A sequence used as reference
        /// </summary>
        [FhirElement("referenceSeq", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.MolecularSequence.ReferenceSeqComponent ReferenceSeq
        {
            get { return _ReferenceSeq; }
            set { _ReferenceSeq = value; OnPropertyChanged("ReferenceSeq"); }
        }
        
        private Hl7.Fhir.Model.MolecularSequence.ReferenceSeqComponent _ReferenceSeq;
        
        /// <summary>
        /// Variant in sequence
        /// </summary>
        [FhirElement("variant", InSummary=true, Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MolecularSequence.VariantComponent> Variant
        {
            get { if(_Variant==null) _Variant = new List<Hl7.Fhir.Model.MolecularSequence.VariantComponent>(); return _Variant; }
            set { _Variant = value; OnPropertyChanged("Variant"); }
        }
        
        private List<Hl7.Fhir.Model.MolecularSequence.VariantComponent> _Variant;
        
        /// <summary>
        /// Sequence that was observed
        /// </summary>
        [FhirElement("observedSeq", InSummary=true, Order=190)]
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
        [FhirElement("quality", InSummary=true, Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MolecularSequence.QualityComponent> Quality
        {
            get { if(_Quality==null) _Quality = new List<Hl7.Fhir.Model.MolecularSequence.QualityComponent>(); return _Quality; }
            set { _Quality = value; OnPropertyChanged("Quality"); }
        }
        
        private List<Hl7.Fhir.Model.MolecularSequence.QualityComponent> _Quality;
        
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
                if (!value.HasValue)
                  ReadCoverageElement = null; 
                else
                  ReadCoverageElement = new Hl7.Fhir.Model.Integer(value);
                OnPropertyChanged("ReadCoverage");
            }
        }
        
        /// <summary>
        /// External repository which contains detailed report related with observedSeq in this resource
        /// </summary>
        [FhirElement("repository", InSummary=true, Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MolecularSequence.RepositoryComponent> Repository
        {
            get { if(_Repository==null) _Repository = new List<Hl7.Fhir.Model.MolecularSequence.RepositoryComponent>(); return _Repository; }
            set { _Repository = value; OnPropertyChanged("Repository"); }
        }
        
        private List<Hl7.Fhir.Model.MolecularSequence.RepositoryComponent> _Repository;
        
        /// <summary>
        /// Pointer to next atomic sequence
        /// </summary>
        [FhirElement("pointer", InSummary=true, Order=230)]
        [CLSCompliant(false)]
		[References("MolecularSequence")]
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
        [FhirElement("structureVariant", InSummary=true, Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MolecularSequence.StructureVariantComponent> StructureVariant
        {
            get { if(_StructureVariant==null) _StructureVariant = new List<Hl7.Fhir.Model.MolecularSequence.StructureVariantComponent>(); return _StructureVariant; }
            set { _StructureVariant = value; OnPropertyChanged("StructureVariant"); }
        }
        
        private List<Hl7.Fhir.Model.MolecularSequence.StructureVariantComponent> _StructureVariant;
        

        public static ElementDefinition.ConstraintComponent MolecularSequence_MSQ_3 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "coordinateSystem = 1 or coordinateSystem = 0",
            Key = "msq-3",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Only 0 and 1 are valid for coordinateSystem",
            Xpath = "count(f:coordinateSystem[@value=0 and @value=1]) = 1"
        };

        public static ElementDefinition.ConstraintComponent MolecularSequence_MSQ_6 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "referenceSeq.all((genomeBuild.count()+referenceSeqId.count()+ referenceSeqPointer.count()+ referenceSeqString.count()) = 1)",
            Key = "msq-6",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Have and only have one of the following elements in referenceSeq : 1. genomeBuild ; 2 referenceSeqId; 3. referenceSeqPointer;  4. referenceSeqString;",
            Xpath = "count(f:genomeBuild)+count(f:referenceSeqId)+count(f:referenceSeqPointer)+count(f:referenceSeqString)=1"
        };

        public static ElementDefinition.ConstraintComponent MolecularSequence_MSQ_5 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "referenceSeq.all((chromosome.empty() and genomeBuild.empty()) or (chromosome.exists() and genomeBuild.exists()))",
            Key = "msq-5",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "GenomeBuild and chromosome must be both contained if either one of them is contained",
            Xpath = "(exists(f:chromosome) and exists(f:genomeBuild)) or (not(exists(f:chromosome)) and not(exists(f:genomeBuild)))"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(MolecularSequence_MSQ_3);
            InvariantConstraints.Add(MolecularSequence_MSQ_6);
            InvariantConstraints.Add(MolecularSequence_MSQ_5);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MolecularSequence;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.MolecularSequence.sequenceType>)TypeElement.DeepCopy();
                if(CoordinateSystemElement != null) dest.CoordinateSystemElement = (Hl7.Fhir.Model.Integer)CoordinateSystemElement.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Specimen != null) dest.Specimen = (Hl7.Fhir.Model.ResourceReference)Specimen.DeepCopy();
                if(Device != null) dest.Device = (Hl7.Fhir.Model.ResourceReference)Device.DeepCopy();
                if(Performer != null) dest.Performer = (Hl7.Fhir.Model.ResourceReference)Performer.DeepCopy();
                if(Quantity != null) dest.Quantity = (Quantity)Quantity.DeepCopy();
                if(ReferenceSeq != null) dest.ReferenceSeq = (Hl7.Fhir.Model.MolecularSequence.ReferenceSeqComponent)ReferenceSeq.DeepCopy();
                if(Variant != null) dest.Variant = new List<Hl7.Fhir.Model.MolecularSequence.VariantComponent>(Variant.DeepCopy());
                if(ObservedSeqElement != null) dest.ObservedSeqElement = (Hl7.Fhir.Model.FhirString)ObservedSeqElement.DeepCopy();
                if(Quality != null) dest.Quality = new List<Hl7.Fhir.Model.MolecularSequence.QualityComponent>(Quality.DeepCopy());
                if(ReadCoverageElement != null) dest.ReadCoverageElement = (Hl7.Fhir.Model.Integer)ReadCoverageElement.DeepCopy();
                if(Repository != null) dest.Repository = new List<Hl7.Fhir.Model.MolecularSequence.RepositoryComponent>(Repository.DeepCopy());
                if(Pointer != null) dest.Pointer = new List<Hl7.Fhir.Model.ResourceReference>(Pointer.DeepCopy());
                if(StructureVariant != null) dest.StructureVariant = new List<Hl7.Fhir.Model.MolecularSequence.StructureVariantComponent>(StructureVariant.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new MolecularSequence());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as MolecularSequence;
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
            if( !DeepComparable.Matches(StructureVariant, otherT.StructureVariant)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MolecularSequence;
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
            if( !DeepComparable.IsExactly(StructureVariant, otherT.StructureVariant)) return false;
            
            return true;
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
				foreach (var elem in StructureVariant) { if (elem != null) yield return elem; }
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
                foreach (var elem in StructureVariant) { if (elem != null) yield return new ElementValue("structureVariant", elem); }
            }
        }

    }
    
}
