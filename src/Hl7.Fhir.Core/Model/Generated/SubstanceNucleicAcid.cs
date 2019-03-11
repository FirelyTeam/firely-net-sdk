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
    /// Nucleic acids are defined by three distinct elements: the base, sugar and linkage. Individual substance/moiety IDs will be created for each of these elements. The nucleotide sequence will be always entered in the 5’-3’ direction
    /// </summary>
    [FhirType("SubstanceNucleicAcid", IsResource=true)]
    [DataContract]
    public partial class SubstanceNucleicAcid : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.SubstanceNucleicAcid; } }
        [NotMapped]
        public override string TypeName { get { return "SubstanceNucleicAcid"; } }
        
        [FhirType("SubunitComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class SubunitComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "SubunitComponent"; } }
            
            /// <summary>
            /// Index of linear sequences of nucleic acids in order of decreasing length. Sequences of the same length will be ordered by molecular weight. Subunits that have identical sequences will be repeated and have sequential subscripts
            /// </summary>
            [FhirElement("subunit", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Integer SubunitElement
            {
                get { return _SubunitElement; }
                set { _SubunitElement = value; OnPropertyChanged("SubunitElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _SubunitElement;
            
            /// <summary>
            /// Index of linear sequences of nucleic acids in order of decreasing length. Sequences of the same length will be ordered by molecular weight. Subunits that have identical sequences will be repeated and have sequential subscripts
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Subunit
            {
                get { return SubunitElement != null ? SubunitElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        SubunitElement = null; 
                    else
                        SubunitElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Subunit");
                }
            }
            
            /// <summary>
            /// Actual nucleotide sequence notation from 5' to 3' end using standard single letter codes. In addition to the base sequence, sugar and type of phosphate or non-phosphate linkage should also be captured
            /// </summary>
            [FhirElement("sequence", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _SequenceElement;
            
            /// <summary>
            /// Actual nucleotide sequence notation from 5' to 3' end using standard single letter codes. In addition to the base sequence, sugar and type of phosphate or non-phosphate linkage should also be captured
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
            /// The length of the sequence shall be captured
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
            /// The length of the sequence shall be captured
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
            /// (TBC)
            /// </summary>
            [FhirElement("sequenceAttachment", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Attachment SequenceAttachment
            {
                get { return _SequenceAttachment; }
                set { _SequenceAttachment = value; OnPropertyChanged("SequenceAttachment"); }
            }
            
            private Hl7.Fhir.Model.Attachment _SequenceAttachment;
            
            /// <summary>
            /// The nucleotide present at the 5’ terminal shall be specified based on a controlled vocabulary. Since the sequence is represented from the 5' to the 3' end, the 5’ prime nucleotide is the letter at the first position in the sequence. A separate representation would be redundant
            /// </summary>
            [FhirElement("fivePrime", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept FivePrime
            {
                get { return _FivePrime; }
                set { _FivePrime = value; OnPropertyChanged("FivePrime"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _FivePrime;
            
            /// <summary>
            /// The nucleotide present at the 3’ terminal shall be specified based on a controlled vocabulary. Since the sequence is represented from the 5' to the 3' end, the 5’ prime nucleotide is the letter at the last position in the sequence. A separate representation would be redundant
            /// </summary>
            [FhirElement("threePrime", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept ThreePrime
            {
                get { return _ThreePrime; }
                set { _ThreePrime = value; OnPropertyChanged("ThreePrime"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _ThreePrime;
            
            /// <summary>
            /// The linkages between sugar residues will also be captured
            /// </summary>
            [FhirElement("linkage", InSummary=true, Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.SubstanceNucleicAcid.LinkageComponent> Linkage
            {
                get { if(_Linkage==null) _Linkage = new List<Hl7.Fhir.Model.SubstanceNucleicAcid.LinkageComponent>(); return _Linkage; }
                set { _Linkage = value; OnPropertyChanged("Linkage"); }
            }
            
            private List<Hl7.Fhir.Model.SubstanceNucleicAcid.LinkageComponent> _Linkage;
            
            /// <summary>
            /// 5.3.6.8.1 Sugar ID (Mandatory)
            /// </summary>
            [FhirElement("sugar", InSummary=true, Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.SubstanceNucleicAcid.SugarComponent> Sugar
            {
                get { if(_Sugar==null) _Sugar = new List<Hl7.Fhir.Model.SubstanceNucleicAcid.SugarComponent>(); return _Sugar; }
                set { _Sugar = value; OnPropertyChanged("Sugar"); }
            }
            
            private List<Hl7.Fhir.Model.SubstanceNucleicAcid.SugarComponent> _Sugar;
            
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
                    if(FivePrime != null) dest.FivePrime = (Hl7.Fhir.Model.CodeableConcept)FivePrime.DeepCopy();
                    if(ThreePrime != null) dest.ThreePrime = (Hl7.Fhir.Model.CodeableConcept)ThreePrime.DeepCopy();
                    if(Linkage != null) dest.Linkage = new List<Hl7.Fhir.Model.SubstanceNucleicAcid.LinkageComponent>(Linkage.DeepCopy());
                    if(Sugar != null) dest.Sugar = new List<Hl7.Fhir.Model.SubstanceNucleicAcid.SugarComponent>(Sugar.DeepCopy());
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
                if( !DeepComparable.Matches(FivePrime, otherT.FivePrime)) return false;
                if( !DeepComparable.Matches(ThreePrime, otherT.ThreePrime)) return false;
                if( !DeepComparable.Matches(Linkage, otherT.Linkage)) return false;
                if( !DeepComparable.Matches(Sugar, otherT.Sugar)) return false;
                
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
                if( !DeepComparable.IsExactly(FivePrime, otherT.FivePrime)) return false;
                if( !DeepComparable.IsExactly(ThreePrime, otherT.ThreePrime)) return false;
                if( !DeepComparable.IsExactly(Linkage, otherT.Linkage)) return false;
                if( !DeepComparable.IsExactly(Sugar, otherT.Sugar)) return false;
                
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
                    if (FivePrime != null) yield return FivePrime;
                    if (ThreePrime != null) yield return ThreePrime;
                    foreach (var elem in Linkage) { if (elem != null) yield return elem; }
                    foreach (var elem in Sugar) { if (elem != null) yield return elem; }
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
                    if (FivePrime != null) yield return new ElementValue("fivePrime", FivePrime);
                    if (ThreePrime != null) yield return new ElementValue("threePrime", ThreePrime);
                    foreach (var elem in Linkage) { if (elem != null) yield return new ElementValue("linkage", elem); }
                    foreach (var elem in Sugar) { if (elem != null) yield return new ElementValue("sugar", elem); }
                }
            }

            
        }
        
        
        [FhirType("LinkageComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class LinkageComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "LinkageComponent"; } }
            
            /// <summary>
            /// The entity that links the sugar residues together should also be captured for nearly all naturally occurring nucleic acid the linkage is a phosphate group. For many synthetic oligonucleotides phosphorothioate linkages are often seen. Linkage connectivity is assumed to be 3’-5’. If the linkage is either 3’-3’ or 5’-5’ this should be specified
            /// </summary>
            [FhirElement("connectivity", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ConnectivityElement
            {
                get { return _ConnectivityElement; }
                set { _ConnectivityElement = value; OnPropertyChanged("ConnectivityElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ConnectivityElement;
            
            /// <summary>
            /// The entity that links the sugar residues together should also be captured for nearly all naturally occurring nucleic acid the linkage is a phosphate group. For many synthetic oligonucleotides phosphorothioate linkages are often seen. Linkage connectivity is assumed to be 3’-5’. If the linkage is either 3’-3’ or 5’-5’ this should be specified
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Connectivity
            {
                get { return ConnectivityElement != null ? ConnectivityElement.Value : null; }
                set
                {
                    if (value == null)
                        ConnectivityElement = null; 
                    else
                        ConnectivityElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Connectivity");
                }
            }
            
            /// <summary>
            /// Each linkage will be registered as a fragment and have an ID
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// Each linkage will be registered as a fragment and have at least one name. A single name shall be assigned to each linkage
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
            /// Each linkage will be registered as a fragment and have at least one name. A single name shall be assigned to each linkage
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
            /// Residues shall be captured as described in 5.3.6.8.3
            /// </summary>
            [FhirElement("residueSite", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ResidueSiteElement
            {
                get { return _ResidueSiteElement; }
                set { _ResidueSiteElement = value; OnPropertyChanged("ResidueSiteElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ResidueSiteElement;
            
            /// <summary>
            /// Residues shall be captured as described in 5.3.6.8.3
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ResidueSite
            {
                get { return ResidueSiteElement != null ? ResidueSiteElement.Value : null; }
                set
                {
                    if (value == null)
                        ResidueSiteElement = null; 
                    else
                        ResidueSiteElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ResidueSite");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as LinkageComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ConnectivityElement != null) dest.ConnectivityElement = (Hl7.Fhir.Model.FhirString)ConnectivityElement.DeepCopy();
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(ResidueSiteElement != null) dest.ResidueSiteElement = (Hl7.Fhir.Model.FhirString)ResidueSiteElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new LinkageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as LinkageComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ConnectivityElement, otherT.ConnectivityElement)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(ResidueSiteElement, otherT.ResidueSiteElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as LinkageComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ConnectivityElement, otherT.ConnectivityElement)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(ResidueSiteElement, otherT.ResidueSiteElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (ConnectivityElement != null) yield return ConnectivityElement;
                    if (Identifier != null) yield return Identifier;
                    if (NameElement != null) yield return NameElement;
                    if (ResidueSiteElement != null) yield return ResidueSiteElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (ConnectivityElement != null) yield return new ElementValue("connectivity", ConnectivityElement);
                    if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (ResidueSiteElement != null) yield return new ElementValue("residueSite", ResidueSiteElement);
                }
            }

            
        }
        
        
        [FhirType("SugarComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class SugarComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "SugarComponent"; } }
            
            /// <summary>
            /// The Substance ID of the sugar or sugar-like component that make up the nucleotide
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// The name of the sugar or sugar-like component that make up the nucleotide
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
            /// The name of the sugar or sugar-like component that make up the nucleotide
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
            /// The residues that contain a given sugar will be captured. The order of given residues will be captured in the 5‘-3‘direction consistent with the base sequences listed above
            /// </summary>
            [FhirElement("residueSite", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ResidueSiteElement
            {
                get { return _ResidueSiteElement; }
                set { _ResidueSiteElement = value; OnPropertyChanged("ResidueSiteElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ResidueSiteElement;
            
            /// <summary>
            /// The residues that contain a given sugar will be captured. The order of given residues will be captured in the 5‘-3‘direction consistent with the base sequences listed above
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ResidueSite
            {
                get { return ResidueSiteElement != null ? ResidueSiteElement.Value : null; }
                set
                {
                    if (value == null)
                        ResidueSiteElement = null; 
                    else
                        ResidueSiteElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ResidueSite");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SugarComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(ResidueSiteElement != null) dest.ResidueSiteElement = (Hl7.Fhir.Model.FhirString)ResidueSiteElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SugarComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SugarComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(ResidueSiteElement, otherT.ResidueSiteElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SugarComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(ResidueSiteElement, otherT.ResidueSiteElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Identifier != null) yield return Identifier;
                    if (NameElement != null) yield return NameElement;
                    if (ResidueSiteElement != null) yield return ResidueSiteElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (ResidueSiteElement != null) yield return new ElementValue("residueSite", ResidueSiteElement);
                }
            }

            
        }
        
        
        /// <summary>
        /// The type of the sequence shall be specified based on a controlled vocabulary
        /// </summary>
        [FhirElement("sequenceType", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept SequenceType
        {
            get { return _SequenceType; }
            set { _SequenceType = value; OnPropertyChanged("SequenceType"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _SequenceType;
        
        /// <summary>
        /// The number of linear sequences of nucleotides linked through phosphodiester bonds shall be described. Subunits would be strands of nucleic acids that are tightly associated typically through Watson-Crick base pairing. NOTE: If not specified in the reference source, the assumption is that there is 1 subunit
        /// </summary>
        [FhirElement("numberOfSubunits", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.Integer NumberOfSubunitsElement
        {
            get { return _NumberOfSubunitsElement; }
            set { _NumberOfSubunitsElement = value; OnPropertyChanged("NumberOfSubunitsElement"); }
        }
        
        private Hl7.Fhir.Model.Integer _NumberOfSubunitsElement;
        
        /// <summary>
        /// The number of linear sequences of nucleotides linked through phosphodiester bonds shall be described. Subunits would be strands of nucleic acids that are tightly associated typically through Watson-Crick base pairing. NOTE: If not specified in the reference source, the assumption is that there is 1 subunit
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? NumberOfSubunits
        {
            get { return NumberOfSubunitsElement != null ? NumberOfSubunitsElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  NumberOfSubunitsElement = null; 
                else
                  NumberOfSubunitsElement = new Hl7.Fhir.Model.Integer(value);
                OnPropertyChanged("NumberOfSubunits");
            }
        }
        
        /// <summary>
        /// The area of hybridisation shall be described if applicable for double stranded RNA or DNA. The number associated with the subunit followed by the number associated to the residue shall be specified in increasing order. The underscore “” shall be used as separator as follows: “Subunitnumber Residue”
        /// </summary>
        [FhirElement("areaOfHybridisation", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString AreaOfHybridisationElement
        {
            get { return _AreaOfHybridisationElement; }
            set { _AreaOfHybridisationElement = value; OnPropertyChanged("AreaOfHybridisationElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _AreaOfHybridisationElement;
        
        /// <summary>
        /// The area of hybridisation shall be described if applicable for double stranded RNA or DNA. The number associated with the subunit followed by the number associated to the residue shall be specified in increasing order. The underscore “” shall be used as separator as follows: “Subunitnumber Residue”
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string AreaOfHybridisation
        {
            get { return AreaOfHybridisationElement != null ? AreaOfHybridisationElement.Value : null; }
            set
            {
                if (value == null)
                  AreaOfHybridisationElement = null; 
                else
                  AreaOfHybridisationElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("AreaOfHybridisation");
            }
        }
        
        /// <summary>
        /// (TBC)
        /// </summary>
        [FhirElement("oligoNucleotideType", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept OligoNucleotideType
        {
            get { return _OligoNucleotideType; }
            set { _OligoNucleotideType = value; OnPropertyChanged("OligoNucleotideType"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _OligoNucleotideType;
        
        /// <summary>
        /// Subunits are listed in order of decreasing length; sequences of the same length will be ordered by molecular weight; subunits that have identical sequences will be repeated multiple times
        /// </summary>
        [FhirElement("subunit", InSummary=true, Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.SubstanceNucleicAcid.SubunitComponent> Subunit
        {
            get { if(_Subunit==null) _Subunit = new List<Hl7.Fhir.Model.SubstanceNucleicAcid.SubunitComponent>(); return _Subunit; }
            set { _Subunit = value; OnPropertyChanged("Subunit"); }
        }
        
        private List<Hl7.Fhir.Model.SubstanceNucleicAcid.SubunitComponent> _Subunit;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as SubstanceNucleicAcid;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(SequenceType != null) dest.SequenceType = (Hl7.Fhir.Model.CodeableConcept)SequenceType.DeepCopy();
                if(NumberOfSubunitsElement != null) dest.NumberOfSubunitsElement = (Hl7.Fhir.Model.Integer)NumberOfSubunitsElement.DeepCopy();
                if(AreaOfHybridisationElement != null) dest.AreaOfHybridisationElement = (Hl7.Fhir.Model.FhirString)AreaOfHybridisationElement.DeepCopy();
                if(OligoNucleotideType != null) dest.OligoNucleotideType = (Hl7.Fhir.Model.CodeableConcept)OligoNucleotideType.DeepCopy();
                if(Subunit != null) dest.Subunit = new List<Hl7.Fhir.Model.SubstanceNucleicAcid.SubunitComponent>(Subunit.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new SubstanceNucleicAcid());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as SubstanceNucleicAcid;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(SequenceType, otherT.SequenceType)) return false;
            if( !DeepComparable.Matches(NumberOfSubunitsElement, otherT.NumberOfSubunitsElement)) return false;
            if( !DeepComparable.Matches(AreaOfHybridisationElement, otherT.AreaOfHybridisationElement)) return false;
            if( !DeepComparable.Matches(OligoNucleotideType, otherT.OligoNucleotideType)) return false;
            if( !DeepComparable.Matches(Subunit, otherT.Subunit)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as SubstanceNucleicAcid;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(SequenceType, otherT.SequenceType)) return false;
            if( !DeepComparable.IsExactly(NumberOfSubunitsElement, otherT.NumberOfSubunitsElement)) return false;
            if( !DeepComparable.IsExactly(AreaOfHybridisationElement, otherT.AreaOfHybridisationElement)) return false;
            if( !DeepComparable.IsExactly(OligoNucleotideType, otherT.OligoNucleotideType)) return false;
            if( !DeepComparable.IsExactly(Subunit, otherT.Subunit)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (SequenceType != null) yield return SequenceType;
				if (NumberOfSubunitsElement != null) yield return NumberOfSubunitsElement;
				if (AreaOfHybridisationElement != null) yield return AreaOfHybridisationElement;
				if (OligoNucleotideType != null) yield return OligoNucleotideType;
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
                if (AreaOfHybridisationElement != null) yield return new ElementValue("areaOfHybridisation", AreaOfHybridisationElement);
                if (OligoNucleotideType != null) yield return new ElementValue("oligoNucleotideType", OligoNucleotideType);
                foreach (var elem in Subunit) { if (elem != null) yield return new ElementValue("subunit", elem); }
            }
        }

    }
    
}
