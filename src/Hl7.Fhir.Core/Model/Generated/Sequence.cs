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
// Generated for FHIR v1.2.0
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
        
        [FhirType("CoordinateComponent")]
        [DataContract]
        public partial class CoordinateComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CoordinateComponent"; } }
            
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
            /// 0-based start position (inclusive) of the sequence
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
            [FhirElement("end", InSummary=true, Order=60)]
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
            /// The Genome Build used for reference, following GRCh build versions e.g. 'GRCh 37'
            /// </summary>
            [FhirElement("genomeBuild", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept GenomeBuild
            {
                get { return _GenomeBuild; }
                set { _GenomeBuild = value; OnPropertyChanged("GenomeBuild"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _GenomeBuild;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CoordinateComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Chromosome != null) dest.Chromosome = (Hl7.Fhir.Model.CodeableConcept)Chromosome.DeepCopy();
                    if(StartElement != null) dest.StartElement = (Hl7.Fhir.Model.Integer)StartElement.DeepCopy();
                    if(EndElement != null) dest.EndElement = (Hl7.Fhir.Model.Integer)EndElement.DeepCopy();
                    if(GenomeBuild != null) dest.GenomeBuild = (Hl7.Fhir.Model.CodeableConcept)GenomeBuild.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CoordinateComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CoordinateComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Chromosome, otherT.Chromosome)) return false;
                if( !DeepComparable.Matches(StartElement, otherT.StartElement)) return false;
                if( !DeepComparable.Matches(EndElement, otherT.EndElement)) return false;
                if( !DeepComparable.Matches(GenomeBuild, otherT.GenomeBuild)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CoordinateComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Chromosome, otherT.Chromosome)) return false;
                if( !DeepComparable.IsExactly(StartElement, otherT.StartElement)) return false;
                if( !DeepComparable.IsExactly(EndElement, otherT.EndElement)) return false;
                if( !DeepComparable.IsExactly(GenomeBuild, otherT.GenomeBuild)) return false;
                
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
            /// Platform
            /// </summary>
            [FhirElement("platform", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Platform
            {
                get { return _Platform; }
                set { _Platform = value; OnPropertyChanged("Platform"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Platform;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as QualityComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(StartElement != null) dest.StartElement = (Hl7.Fhir.Model.Integer)StartElement.DeepCopy();
                    if(EndElement != null) dest.EndElement = (Hl7.Fhir.Model.Integer)EndElement.DeepCopy();
                    if(Score != null) dest.Score = (Quantity)Score.DeepCopy();
                    if(Platform != null) dest.Platform = (Hl7.Fhir.Model.CodeableConcept)Platform.DeepCopy();
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
                if( !DeepComparable.Matches(Platform, otherT.Platform)) return false;
                
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
                if( !DeepComparable.IsExactly(Platform, otherT.Platform)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ChipComponent")]
        [DataContract]
        public partial class ChipComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ChipComponent"; } }
            
            /// <summary>
            /// Chip id
            /// </summary>
            [FhirElement("chipId", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ChipIdElement
            {
                get { return _ChipIdElement; }
                set { _ChipIdElement = value; OnPropertyChanged("ChipIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ChipIdElement;
            
            /// <summary>
            /// Chip id
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ChipId
            {
                get { return ChipIdElement != null ? ChipIdElement.Value : null; }
                set
                {
                    if(value == null)
                      ChipIdElement = null; 
                    else
                      ChipIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ChipId");
                }
            }
            
            /// <summary>
            /// Chip manufacturer id
            /// </summary>
            [FhirElement("manufacturerId", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ManufacturerIdElement
            {
                get { return _ManufacturerIdElement; }
                set { _ManufacturerIdElement = value; OnPropertyChanged("ManufacturerIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ManufacturerIdElement;
            
            /// <summary>
            /// Chip manufacturer id
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ManufacturerId
            {
                get { return ManufacturerIdElement != null ? ManufacturerIdElement.Value : null; }
                set
                {
                    if(value == null)
                      ManufacturerIdElement = null; 
                    else
                      ManufacturerIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ManufacturerId");
                }
            }
            
            /// <summary>
            /// Chip version
            /// </summary>
            [FhirElement("version", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VersionElement
            {
                get { return _VersionElement; }
                set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _VersionElement;
            
            /// <summary>
            /// Chip version
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Version
            {
                get { return VersionElement != null ? VersionElement.Value : null; }
                set
                {
                    if(value == null)
                      VersionElement = null; 
                    else
                      VersionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Version");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ChipComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ChipIdElement != null) dest.ChipIdElement = (Hl7.Fhir.Model.FhirString)ChipIdElement.DeepCopy();
                    if(ManufacturerIdElement != null) dest.ManufacturerIdElement = (Hl7.Fhir.Model.FhirString)ManufacturerIdElement.DeepCopy();
                    if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ChipComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ChipComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ChipIdElement, otherT.ChipIdElement)) return false;
                if( !DeepComparable.Matches(ManufacturerIdElement, otherT.ManufacturerIdElement)) return false;
                if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ChipComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ChipIdElement, otherT.ChipIdElement)) return false;
                if( !DeepComparable.IsExactly(ManufacturerIdElement, otherT.ManufacturerIdElement)) return false;
                if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
                
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
            /// URI of the page containing information about the structure of the repository
            /// </summary>
            [FhirElement("structure", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri StructureElement
            {
                get { return _StructureElement; }
                set { _StructureElement = value; OnPropertyChanged("StructureElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _StructureElement;
            
            /// <summary>
            /// URI of the page containing information about the structure of the repository
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Structure
            {
                get { return StructureElement != null ? StructureElement.Value : null; }
                set
                {
                    if(value == null)
                      StructureElement = null; 
                    else
                      StructureElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Structure");
                }
            }
            
            /// <summary>
            /// Id of a GA4GH variant
            /// </summary>
            [FhirElement("variantId", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VariantIdElement
            {
                get { return _VariantIdElement; }
                set { _VariantIdElement = value; OnPropertyChanged("VariantIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _VariantIdElement;
            
            /// <summary>
            /// Id of a GA4GH variant
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
            /// Id of a GA4GH read group
            /// </summary>
            [FhirElement("readGroupSetId", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ReadGroupSetIdElement
            {
                get { return _ReadGroupSetIdElement; }
                set { _ReadGroupSetIdElement = value; OnPropertyChanged("ReadGroupSetIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ReadGroupSetIdElement;
            
            /// <summary>
            /// Id of a GA4GH read group
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ReadGroupSetId
            {
                get { return ReadGroupSetIdElement != null ? ReadGroupSetIdElement.Value : null; }
                set
                {
                    if(value == null)
                      ReadGroupSetIdElement = null; 
                    else
                      ReadGroupSetIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ReadGroupSetId");
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
                    if(StructureElement != null) dest.StructureElement = (Hl7.Fhir.Model.FhirUri)StructureElement.DeepCopy();
                    if(VariantIdElement != null) dest.VariantIdElement = (Hl7.Fhir.Model.FhirString)VariantIdElement.DeepCopy();
                    if(ReadGroupSetIdElement != null) dest.ReadGroupSetIdElement = (Hl7.Fhir.Model.FhirString)ReadGroupSetIdElement.DeepCopy();
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
                if( !DeepComparable.Matches(StructureElement, otherT.StructureElement)) return false;
                if( !DeepComparable.Matches(VariantIdElement, otherT.VariantIdElement)) return false;
                if( !DeepComparable.Matches(ReadGroupSetIdElement, otherT.ReadGroupSetIdElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RepositoryComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(StructureElement, otherT.StructureElement)) return false;
                if( !DeepComparable.IsExactly(VariantIdElement, otherT.VariantIdElement)) return false;
                if( !DeepComparable.IsExactly(ReadGroupSetIdElement, otherT.ReadGroupSetIdElement)) return false;
                
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
        /// Identifier for variant and ClinVar, dbSNP or COSMIC identifier should be used
        /// </summary>
        [FhirElement("variationID", InSummary=true, Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> VariationID
        {
            get { if(_VariationID==null) _VariationID = new List<Hl7.Fhir.Model.CodeableConcept>(); return _VariationID; }
            set { _VariationID = value; OnPropertyChanged("VariationID"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _VariationID;
        
        /// <summary>
        /// Reference identifier.  It must match the type in the Sequence.type field
        /// </summary>
        [FhirElement("referenceSeq", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept ReferenceSeq
        {
            get { return _ReferenceSeq; }
            set { _ReferenceSeq = value; OnPropertyChanged("ReferenceSeq"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _ReferenceSeq;
        
        /// <summary>
        /// Quantity of the sequence
        /// </summary>
        [FhirElement("quantity", InSummary=true, Order=120)]
        [DataMember]
        public Quantity Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; OnPropertyChanged("Quantity"); }
        }
        
        private Quantity _Quantity;
        
        /// <summary>
        /// The coordinate of the variant
        /// </summary>
        [FhirElement("coordinate", InSummary=true, Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Sequence.CoordinateComponent> Coordinate
        {
            get { if(_Coordinate==null) _Coordinate = new List<Hl7.Fhir.Model.Sequence.CoordinateComponent>(); return _Coordinate; }
            set { _Coordinate = value; OnPropertyChanged("Coordinate"); }
        }
        
        private List<Hl7.Fhir.Model.Sequence.CoordinateComponent> _Coordinate;
        
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
        /// Nucleotide(s)/amino acids from start position of sequence to stop position of observed sequence
        /// </summary>
        [FhirElement("observedAllele", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ObservedAlleleElement
        {
            get { return _ObservedAlleleElement; }
            set { _ObservedAlleleElement = value; OnPropertyChanged("ObservedAlleleElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ObservedAlleleElement;
        
        /// <summary>
        /// Nucleotide(s)/amino acids from start position of sequence to stop position of observed sequence
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
        /// Nucleotide(s)/amino acids from start position of sequence to stop position of reference sequence
        /// </summary>
        [FhirElement("referenceAllele", InSummary=true, Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ReferenceAlleleElement
        {
            get { return _ReferenceAlleleElement; }
            set { _ReferenceAlleleElement = value; OnPropertyChanged("ReferenceAlleleElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ReferenceAlleleElement;
        
        /// <summary>
        /// Nucleotide(s)/amino acids from start position of sequence to stop position of reference sequence
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
        [FhirElement("cigar", InSummary=true, Order=170)]
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
        
        /// <summary>
        /// Sequence Quality
        /// </summary>
        [FhirElement("quality", InSummary=true, Order=180)]
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
        [FhirElement("allelicState", InSummary=true, Order=190)]
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
        [FhirElement("allelicFrequency", InSummary=true, Order=200)]
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
        [FhirElement("copyNumberEvent", InSummary=true, Order=210)]
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
        [FhirElement("readCoverage", InSummary=true, Order=220)]
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
        /// Information of chip
        /// </summary>
        [FhirElement("chip", InSummary=true, Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.Sequence.ChipComponent Chip
        {
            get { return _Chip; }
            set { _Chip = value; OnPropertyChanged("Chip"); }
        }
        
        private Hl7.Fhir.Model.Sequence.ChipComponent _Chip;
        
        /// <summary>
        /// External repository
        /// </summary>
        [FhirElement("repository", InSummary=true, Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Sequence.RepositoryComponent> Repository
        {
            get { if(_Repository==null) _Repository = new List<Hl7.Fhir.Model.Sequence.RepositoryComponent>(); return _Repository; }
            set { _Repository = value; OnPropertyChanged("Repository"); }
        }
        
        private List<Hl7.Fhir.Model.Sequence.RepositoryComponent> _Repository;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Sequence;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.Code)TypeElement.DeepCopy();
                if(VariationID != null) dest.VariationID = new List<Hl7.Fhir.Model.CodeableConcept>(VariationID.DeepCopy());
                if(ReferenceSeq != null) dest.ReferenceSeq = (Hl7.Fhir.Model.CodeableConcept)ReferenceSeq.DeepCopy();
                if(Quantity != null) dest.Quantity = (Quantity)Quantity.DeepCopy();
                if(Coordinate != null) dest.Coordinate = new List<Hl7.Fhir.Model.Sequence.CoordinateComponent>(Coordinate.DeepCopy());
                if(Species != null) dest.Species = (Hl7.Fhir.Model.CodeableConcept)Species.DeepCopy();
                if(ObservedAlleleElement != null) dest.ObservedAlleleElement = (Hl7.Fhir.Model.FhirString)ObservedAlleleElement.DeepCopy();
                if(ReferenceAlleleElement != null) dest.ReferenceAlleleElement = (Hl7.Fhir.Model.FhirString)ReferenceAlleleElement.DeepCopy();
                if(CigarElement != null) dest.CigarElement = (Hl7.Fhir.Model.FhirString)CigarElement.DeepCopy();
                if(Quality != null) dest.Quality = new List<Hl7.Fhir.Model.Sequence.QualityComponent>(Quality.DeepCopy());
                if(AllelicState != null) dest.AllelicState = (Hl7.Fhir.Model.CodeableConcept)AllelicState.DeepCopy();
                if(AllelicFrequencyElement != null) dest.AllelicFrequencyElement = (Hl7.Fhir.Model.FhirDecimal)AllelicFrequencyElement.DeepCopy();
                if(CopyNumberEvent != null) dest.CopyNumberEvent = (Hl7.Fhir.Model.CodeableConcept)CopyNumberEvent.DeepCopy();
                if(ReadCoverageElement != null) dest.ReadCoverageElement = (Hl7.Fhir.Model.Integer)ReadCoverageElement.DeepCopy();
                if(Chip != null) dest.Chip = (Hl7.Fhir.Model.Sequence.ChipComponent)Chip.DeepCopy();
                if(Repository != null) dest.Repository = new List<Hl7.Fhir.Model.Sequence.RepositoryComponent>(Repository.DeepCopy());
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
            if( !DeepComparable.Matches(VariationID, otherT.VariationID)) return false;
            if( !DeepComparable.Matches(ReferenceSeq, otherT.ReferenceSeq)) return false;
            if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.Matches(Coordinate, otherT.Coordinate)) return false;
            if( !DeepComparable.Matches(Species, otherT.Species)) return false;
            if( !DeepComparable.Matches(ObservedAlleleElement, otherT.ObservedAlleleElement)) return false;
            if( !DeepComparable.Matches(ReferenceAlleleElement, otherT.ReferenceAlleleElement)) return false;
            if( !DeepComparable.Matches(CigarElement, otherT.CigarElement)) return false;
            if( !DeepComparable.Matches(Quality, otherT.Quality)) return false;
            if( !DeepComparable.Matches(AllelicState, otherT.AllelicState)) return false;
            if( !DeepComparable.Matches(AllelicFrequencyElement, otherT.AllelicFrequencyElement)) return false;
            if( !DeepComparable.Matches(CopyNumberEvent, otherT.CopyNumberEvent)) return false;
            if( !DeepComparable.Matches(ReadCoverageElement, otherT.ReadCoverageElement)) return false;
            if( !DeepComparable.Matches(Chip, otherT.Chip)) return false;
            if( !DeepComparable.Matches(Repository, otherT.Repository)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Sequence;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(VariationID, otherT.VariationID)) return false;
            if( !DeepComparable.IsExactly(ReferenceSeq, otherT.ReferenceSeq)) return false;
            if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.IsExactly(Coordinate, otherT.Coordinate)) return false;
            if( !DeepComparable.IsExactly(Species, otherT.Species)) return false;
            if( !DeepComparable.IsExactly(ObservedAlleleElement, otherT.ObservedAlleleElement)) return false;
            if( !DeepComparable.IsExactly(ReferenceAlleleElement, otherT.ReferenceAlleleElement)) return false;
            if( !DeepComparable.IsExactly(CigarElement, otherT.CigarElement)) return false;
            if( !DeepComparable.IsExactly(Quality, otherT.Quality)) return false;
            if( !DeepComparable.IsExactly(AllelicState, otherT.AllelicState)) return false;
            if( !DeepComparable.IsExactly(AllelicFrequencyElement, otherT.AllelicFrequencyElement)) return false;
            if( !DeepComparable.IsExactly(CopyNumberEvent, otherT.CopyNumberEvent)) return false;
            if( !DeepComparable.IsExactly(ReadCoverageElement, otherT.ReadCoverageElement)) return false;
            if( !DeepComparable.IsExactly(Chip, otherT.Chip)) return false;
            if( !DeepComparable.IsExactly(Repository, otherT.Repository)) return false;
            
            return true;
        }
        
    }
    
}
