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
    /// Source material shall capture information on the taxonomic and anatomical origins as well as the fraction of a material that can result in or can be modified to form a substance. This set of data elements shall be used to define polymer substances isolated from biological matrices. Taxonomic and anatomical origins shall be described using a controlled vocabulary as required. This information is captured for naturally derived polymers ( . starch) and structurally diverse substances. For Organisms belonging to the Kingdom Plantae the Substance level defines the fresh material of a single species or infraspecies, the Herbal Drug and the Herbal preparation. For Herbal preparations, the fraction information will be captured at the Substance information level and additional information for herbal extracts will be captured at the Specified Substance Group 1 information level. See for further explanation the Substance Class: Structurally Diverse and the herbal annex
    /// </summary>
    [FhirType("SubstanceSourceMaterial", IsResource=true)]
    [DataContract]
    public partial class SubstanceSourceMaterial : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.SubstanceSourceMaterial; } }
        [NotMapped]
        public override string TypeName { get { return "SubstanceSourceMaterial"; } }
        
        [FhirType("FractionDescriptionComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class FractionDescriptionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "FractionDescriptionComponent"; } }
            
            /// <summary>
            /// This element is capturing information about the fraction of a plant part, or human plasma for fractionation
            /// </summary>
            [FhirElement("fraction", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString FractionElement
            {
                get { return _FractionElement; }
                set { _FractionElement = value; OnPropertyChanged("FractionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _FractionElement;
            
            /// <summary>
            /// This element is capturing information about the fraction of a plant part, or human plasma for fractionation
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Fraction
            {
                get { return FractionElement != null ? FractionElement.Value : null; }
                set
                {
                    if (value == null)
                        FractionElement = null; 
                    else
                        FractionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Fraction");
                }
            }
            
            /// <summary>
            /// The specific type of the material constituting the component. For Herbal preparations the particulars of the extracts (liquid/dry) is described in Specified Substance Group 1
            /// </summary>
            [FhirElement("materialType", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept MaterialType
            {
                get { return _MaterialType; }
                set { _MaterialType = value; OnPropertyChanged("MaterialType"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _MaterialType;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as FractionDescriptionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(FractionElement != null) dest.FractionElement = (Hl7.Fhir.Model.FhirString)FractionElement.DeepCopy();
                    if(MaterialType != null) dest.MaterialType = (Hl7.Fhir.Model.CodeableConcept)MaterialType.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new FractionDescriptionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as FractionDescriptionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(FractionElement, otherT.FractionElement)) return false;
                if( !DeepComparable.Matches(MaterialType, otherT.MaterialType)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as FractionDescriptionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(FractionElement, otherT.FractionElement)) return false;
                if( !DeepComparable.IsExactly(MaterialType, otherT.MaterialType)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (FractionElement != null) yield return FractionElement;
                    if (MaterialType != null) yield return MaterialType;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (FractionElement != null) yield return new ElementValue("fraction", FractionElement);
                    if (MaterialType != null) yield return new ElementValue("materialType", MaterialType);
                }
            }

            
        }
        
        
        [FhirType("OrganismComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class OrganismComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "OrganismComponent"; } }
            
            /// <summary>
            /// The family of an organism shall be specified
            /// </summary>
            [FhirElement("family", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Family
            {
                get { return _Family; }
                set { _Family = value; OnPropertyChanged("Family"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Family;
            
            /// <summary>
            /// The genus of an organism shall be specified; refers to the Latin epithet of the genus element of the plant/animal scientific name; it is present in names for genera, species and infraspecies
            /// </summary>
            [FhirElement("genus", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Genus
            {
                get { return _Genus; }
                set { _Genus = value; OnPropertyChanged("Genus"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Genus;
            
            /// <summary>
            /// The species of an organism shall be specified; refers to the Latin epithet of the species of the plant/animal; it is present in names for species and infraspecies
            /// </summary>
            [FhirElement("species", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Species
            {
                get { return _Species; }
                set { _Species = value; OnPropertyChanged("Species"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Species;
            
            /// <summary>
            /// The Intraspecific type of an organism shall be specified
            /// </summary>
            [FhirElement("intraspecificType", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept IntraspecificType
            {
                get { return _IntraspecificType; }
                set { _IntraspecificType = value; OnPropertyChanged("IntraspecificType"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _IntraspecificType;
            
            /// <summary>
            /// The intraspecific description of an organism shall be specified based on a controlled vocabulary. For Influenza Vaccine, the intraspecific description shall contain the syntax of the antigen in line with the WHO convention
            /// </summary>
            [FhirElement("intraspecificDescription", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString IntraspecificDescriptionElement
            {
                get { return _IntraspecificDescriptionElement; }
                set { _IntraspecificDescriptionElement = value; OnPropertyChanged("IntraspecificDescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _IntraspecificDescriptionElement;
            
            /// <summary>
            /// The intraspecific description of an organism shall be specified based on a controlled vocabulary. For Influenza Vaccine, the intraspecific description shall contain the syntax of the antigen in line with the WHO convention
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string IntraspecificDescription
            {
                get { return IntraspecificDescriptionElement != null ? IntraspecificDescriptionElement.Value : null; }
                set
                {
                    if (value == null)
                        IntraspecificDescriptionElement = null; 
                    else
                        IntraspecificDescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("IntraspecificDescription");
                }
            }
            
            /// <summary>
            /// 4.9.13.6.1 Author type (Conditional)
            /// </summary>
            [FhirElement("author", InSummary=true, Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.SubstanceSourceMaterial.AuthorComponent> Author
            {
                get { if(_Author==null) _Author = new List<Hl7.Fhir.Model.SubstanceSourceMaterial.AuthorComponent>(); return _Author; }
                set { _Author = value; OnPropertyChanged("Author"); }
            }
            
            private List<Hl7.Fhir.Model.SubstanceSourceMaterial.AuthorComponent> _Author;
            
            /// <summary>
            /// 4.9.13.8.1 Hybrid species maternal organism ID (Optional)
            /// </summary>
            [FhirElement("hybrid", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.SubstanceSourceMaterial.HybridComponent Hybrid
            {
                get { return _Hybrid; }
                set { _Hybrid = value; OnPropertyChanged("Hybrid"); }
            }
            
            private Hl7.Fhir.Model.SubstanceSourceMaterial.HybridComponent _Hybrid;
            
            /// <summary>
            /// 4.9.13.7.1 Kingdom (Conditional)
            /// </summary>
            [FhirElement("organismGeneral", InSummary=true, Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.SubstanceSourceMaterial.OrganismGeneralComponent OrganismGeneral
            {
                get { return _OrganismGeneral; }
                set { _OrganismGeneral = value; OnPropertyChanged("OrganismGeneral"); }
            }
            
            private Hl7.Fhir.Model.SubstanceSourceMaterial.OrganismGeneralComponent _OrganismGeneral;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as OrganismComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Family != null) dest.Family = (Hl7.Fhir.Model.CodeableConcept)Family.DeepCopy();
                    if(Genus != null) dest.Genus = (Hl7.Fhir.Model.CodeableConcept)Genus.DeepCopy();
                    if(Species != null) dest.Species = (Hl7.Fhir.Model.CodeableConcept)Species.DeepCopy();
                    if(IntraspecificType != null) dest.IntraspecificType = (Hl7.Fhir.Model.CodeableConcept)IntraspecificType.DeepCopy();
                    if(IntraspecificDescriptionElement != null) dest.IntraspecificDescriptionElement = (Hl7.Fhir.Model.FhirString)IntraspecificDescriptionElement.DeepCopy();
                    if(Author != null) dest.Author = new List<Hl7.Fhir.Model.SubstanceSourceMaterial.AuthorComponent>(Author.DeepCopy());
                    if(Hybrid != null) dest.Hybrid = (Hl7.Fhir.Model.SubstanceSourceMaterial.HybridComponent)Hybrid.DeepCopy();
                    if(OrganismGeneral != null) dest.OrganismGeneral = (Hl7.Fhir.Model.SubstanceSourceMaterial.OrganismGeneralComponent)OrganismGeneral.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new OrganismComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as OrganismComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Family, otherT.Family)) return false;
                if( !DeepComparable.Matches(Genus, otherT.Genus)) return false;
                if( !DeepComparable.Matches(Species, otherT.Species)) return false;
                if( !DeepComparable.Matches(IntraspecificType, otherT.IntraspecificType)) return false;
                if( !DeepComparable.Matches(IntraspecificDescriptionElement, otherT.IntraspecificDescriptionElement)) return false;
                if( !DeepComparable.Matches(Author, otherT.Author)) return false;
                if( !DeepComparable.Matches(Hybrid, otherT.Hybrid)) return false;
                if( !DeepComparable.Matches(OrganismGeneral, otherT.OrganismGeneral)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as OrganismComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Family, otherT.Family)) return false;
                if( !DeepComparable.IsExactly(Genus, otherT.Genus)) return false;
                if( !DeepComparable.IsExactly(Species, otherT.Species)) return false;
                if( !DeepComparable.IsExactly(IntraspecificType, otherT.IntraspecificType)) return false;
                if( !DeepComparable.IsExactly(IntraspecificDescriptionElement, otherT.IntraspecificDescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
                if( !DeepComparable.IsExactly(Hybrid, otherT.Hybrid)) return false;
                if( !DeepComparable.IsExactly(OrganismGeneral, otherT.OrganismGeneral)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Family != null) yield return Family;
                    if (Genus != null) yield return Genus;
                    if (Species != null) yield return Species;
                    if (IntraspecificType != null) yield return IntraspecificType;
                    if (IntraspecificDescriptionElement != null) yield return IntraspecificDescriptionElement;
                    foreach (var elem in Author) { if (elem != null) yield return elem; }
                    if (Hybrid != null) yield return Hybrid;
                    if (OrganismGeneral != null) yield return OrganismGeneral;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Family != null) yield return new ElementValue("family", Family);
                    if (Genus != null) yield return new ElementValue("genus", Genus);
                    if (Species != null) yield return new ElementValue("species", Species);
                    if (IntraspecificType != null) yield return new ElementValue("intraspecificType", IntraspecificType);
                    if (IntraspecificDescriptionElement != null) yield return new ElementValue("intraspecificDescription", IntraspecificDescriptionElement);
                    foreach (var elem in Author) { if (elem != null) yield return new ElementValue("author", elem); }
                    if (Hybrid != null) yield return new ElementValue("hybrid", Hybrid);
                    if (OrganismGeneral != null) yield return new ElementValue("organismGeneral", OrganismGeneral);
                }
            }

            
        }
        
        
        [FhirType("AuthorComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class AuthorComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "AuthorComponent"; } }
            
            /// <summary>
            /// The type of author of an organism species shall be specified. The parenthetical author of an organism species refers to the first author who published the plant/animal name (of any rank). The primary author of an organism species refers to the first author(s), who validly published the plant/animal name
            /// </summary>
            [FhirElement("authorType", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept AuthorType
            {
                get { return _AuthorType; }
                set { _AuthorType = value; OnPropertyChanged("AuthorType"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _AuthorType;
            
            /// <summary>
            /// The author of an organism species shall be specified. The author year of an organism shall also be specified when applicable; refers to the year in which the first author(s) published the infraspecific plant/animal name (of any rank)
            /// </summary>
            [FhirElement("authorDescription", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString AuthorDescriptionElement
            {
                get { return _AuthorDescriptionElement; }
                set { _AuthorDescriptionElement = value; OnPropertyChanged("AuthorDescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _AuthorDescriptionElement;
            
            /// <summary>
            /// The author of an organism species shall be specified. The author year of an organism shall also be specified when applicable; refers to the year in which the first author(s) published the infraspecific plant/animal name (of any rank)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string AuthorDescription
            {
                get { return AuthorDescriptionElement != null ? AuthorDescriptionElement.Value : null; }
                set
                {
                    if (value == null)
                        AuthorDescriptionElement = null; 
                    else
                        AuthorDescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("AuthorDescription");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AuthorComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(AuthorType != null) dest.AuthorType = (Hl7.Fhir.Model.CodeableConcept)AuthorType.DeepCopy();
                    if(AuthorDescriptionElement != null) dest.AuthorDescriptionElement = (Hl7.Fhir.Model.FhirString)AuthorDescriptionElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new AuthorComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AuthorComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(AuthorType, otherT.AuthorType)) return false;
                if( !DeepComparable.Matches(AuthorDescriptionElement, otherT.AuthorDescriptionElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AuthorComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(AuthorType, otherT.AuthorType)) return false;
                if( !DeepComparable.IsExactly(AuthorDescriptionElement, otherT.AuthorDescriptionElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (AuthorType != null) yield return AuthorType;
                    if (AuthorDescriptionElement != null) yield return AuthorDescriptionElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (AuthorType != null) yield return new ElementValue("authorType", AuthorType);
                    if (AuthorDescriptionElement != null) yield return new ElementValue("authorDescription", AuthorDescriptionElement);
                }
            }

            
        }
        
        
        [FhirType("HybridComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class HybridComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "HybridComponent"; } }
            
            /// <summary>
            /// The identifier of the maternal species constituting the hybrid organism shall be specified based on a controlled vocabulary. For plants, the parents aren’t always known, and it is unlikely that it will be known which is maternal and which is paternal
            /// </summary>
            [FhirElement("maternalOrganismId", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString MaternalOrganismIdElement
            {
                get { return _MaternalOrganismIdElement; }
                set { _MaternalOrganismIdElement = value; OnPropertyChanged("MaternalOrganismIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _MaternalOrganismIdElement;
            
            /// <summary>
            /// The identifier of the maternal species constituting the hybrid organism shall be specified based on a controlled vocabulary. For plants, the parents aren’t always known, and it is unlikely that it will be known which is maternal and which is paternal
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string MaternalOrganismId
            {
                get { return MaternalOrganismIdElement != null ? MaternalOrganismIdElement.Value : null; }
                set
                {
                    if (value == null)
                        MaternalOrganismIdElement = null; 
                    else
                        MaternalOrganismIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("MaternalOrganismId");
                }
            }
            
            /// <summary>
            /// The name of the maternal species constituting the hybrid organism shall be specified. For plants, the parents aren’t always known, and it is unlikely that it will be known which is maternal and which is paternal
            /// </summary>
            [FhirElement("maternalOrganismName", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString MaternalOrganismNameElement
            {
                get { return _MaternalOrganismNameElement; }
                set { _MaternalOrganismNameElement = value; OnPropertyChanged("MaternalOrganismNameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _MaternalOrganismNameElement;
            
            /// <summary>
            /// The name of the maternal species constituting the hybrid organism shall be specified. For plants, the parents aren’t always known, and it is unlikely that it will be known which is maternal and which is paternal
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string MaternalOrganismName
            {
                get { return MaternalOrganismNameElement != null ? MaternalOrganismNameElement.Value : null; }
                set
                {
                    if (value == null)
                        MaternalOrganismNameElement = null; 
                    else
                        MaternalOrganismNameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("MaternalOrganismName");
                }
            }
            
            /// <summary>
            /// The identifier of the paternal species constituting the hybrid organism shall be specified based on a controlled vocabulary
            /// </summary>
            [FhirElement("paternalOrganismId", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PaternalOrganismIdElement
            {
                get { return _PaternalOrganismIdElement; }
                set { _PaternalOrganismIdElement = value; OnPropertyChanged("PaternalOrganismIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PaternalOrganismIdElement;
            
            /// <summary>
            /// The identifier of the paternal species constituting the hybrid organism shall be specified based on a controlled vocabulary
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string PaternalOrganismId
            {
                get { return PaternalOrganismIdElement != null ? PaternalOrganismIdElement.Value : null; }
                set
                {
                    if (value == null)
                        PaternalOrganismIdElement = null; 
                    else
                        PaternalOrganismIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("PaternalOrganismId");
                }
            }
            
            /// <summary>
            /// The name of the paternal species constituting the hybrid organism shall be specified
            /// </summary>
            [FhirElement("paternalOrganismName", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PaternalOrganismNameElement
            {
                get { return _PaternalOrganismNameElement; }
                set { _PaternalOrganismNameElement = value; OnPropertyChanged("PaternalOrganismNameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PaternalOrganismNameElement;
            
            /// <summary>
            /// The name of the paternal species constituting the hybrid organism shall be specified
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string PaternalOrganismName
            {
                get { return PaternalOrganismNameElement != null ? PaternalOrganismNameElement.Value : null; }
                set
                {
                    if (value == null)
                        PaternalOrganismNameElement = null; 
                    else
                        PaternalOrganismNameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("PaternalOrganismName");
                }
            }
            
            /// <summary>
            /// The hybrid type of an organism shall be specified
            /// </summary>
            [FhirElement("hybridType", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept HybridType
            {
                get { return _HybridType; }
                set { _HybridType = value; OnPropertyChanged("HybridType"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _HybridType;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as HybridComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(MaternalOrganismIdElement != null) dest.MaternalOrganismIdElement = (Hl7.Fhir.Model.FhirString)MaternalOrganismIdElement.DeepCopy();
                    if(MaternalOrganismNameElement != null) dest.MaternalOrganismNameElement = (Hl7.Fhir.Model.FhirString)MaternalOrganismNameElement.DeepCopy();
                    if(PaternalOrganismIdElement != null) dest.PaternalOrganismIdElement = (Hl7.Fhir.Model.FhirString)PaternalOrganismIdElement.DeepCopy();
                    if(PaternalOrganismNameElement != null) dest.PaternalOrganismNameElement = (Hl7.Fhir.Model.FhirString)PaternalOrganismNameElement.DeepCopy();
                    if(HybridType != null) dest.HybridType = (Hl7.Fhir.Model.CodeableConcept)HybridType.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new HybridComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as HybridComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(MaternalOrganismIdElement, otherT.MaternalOrganismIdElement)) return false;
                if( !DeepComparable.Matches(MaternalOrganismNameElement, otherT.MaternalOrganismNameElement)) return false;
                if( !DeepComparable.Matches(PaternalOrganismIdElement, otherT.PaternalOrganismIdElement)) return false;
                if( !DeepComparable.Matches(PaternalOrganismNameElement, otherT.PaternalOrganismNameElement)) return false;
                if( !DeepComparable.Matches(HybridType, otherT.HybridType)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as HybridComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(MaternalOrganismIdElement, otherT.MaternalOrganismIdElement)) return false;
                if( !DeepComparable.IsExactly(MaternalOrganismNameElement, otherT.MaternalOrganismNameElement)) return false;
                if( !DeepComparable.IsExactly(PaternalOrganismIdElement, otherT.PaternalOrganismIdElement)) return false;
                if( !DeepComparable.IsExactly(PaternalOrganismNameElement, otherT.PaternalOrganismNameElement)) return false;
                if( !DeepComparable.IsExactly(HybridType, otherT.HybridType)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (MaternalOrganismIdElement != null) yield return MaternalOrganismIdElement;
                    if (MaternalOrganismNameElement != null) yield return MaternalOrganismNameElement;
                    if (PaternalOrganismIdElement != null) yield return PaternalOrganismIdElement;
                    if (PaternalOrganismNameElement != null) yield return PaternalOrganismNameElement;
                    if (HybridType != null) yield return HybridType;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (MaternalOrganismIdElement != null) yield return new ElementValue("maternalOrganismId", MaternalOrganismIdElement);
                    if (MaternalOrganismNameElement != null) yield return new ElementValue("maternalOrganismName", MaternalOrganismNameElement);
                    if (PaternalOrganismIdElement != null) yield return new ElementValue("paternalOrganismId", PaternalOrganismIdElement);
                    if (PaternalOrganismNameElement != null) yield return new ElementValue("paternalOrganismName", PaternalOrganismNameElement);
                    if (HybridType != null) yield return new ElementValue("hybridType", HybridType);
                }
            }

            
        }
        
        
        [FhirType("OrganismGeneralComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class OrganismGeneralComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "OrganismGeneralComponent"; } }
            
            /// <summary>
            /// The kingdom of an organism shall be specified
            /// </summary>
            [FhirElement("kingdom", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Kingdom
            {
                get { return _Kingdom; }
                set { _Kingdom = value; OnPropertyChanged("Kingdom"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Kingdom;
            
            /// <summary>
            /// The phylum of an organism shall be specified
            /// </summary>
            [FhirElement("phylum", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Phylum
            {
                get { return _Phylum; }
                set { _Phylum = value; OnPropertyChanged("Phylum"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Phylum;
            
            /// <summary>
            /// The class of an organism shall be specified
            /// </summary>
            [FhirElement("class", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Class
            {
                get { return _Class; }
                set { _Class = value; OnPropertyChanged("Class"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Class;
            
            /// <summary>
            /// The order of an organism shall be specified,
            /// </summary>
            [FhirElement("order", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Order
            {
                get { return _Order; }
                set { _Order = value; OnPropertyChanged("Order"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Order;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as OrganismGeneralComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Kingdom != null) dest.Kingdom = (Hl7.Fhir.Model.CodeableConcept)Kingdom.DeepCopy();
                    if(Phylum != null) dest.Phylum = (Hl7.Fhir.Model.CodeableConcept)Phylum.DeepCopy();
                    if(Class != null) dest.Class = (Hl7.Fhir.Model.CodeableConcept)Class.DeepCopy();
                    if(Order != null) dest.Order = (Hl7.Fhir.Model.CodeableConcept)Order.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new OrganismGeneralComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as OrganismGeneralComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Kingdom, otherT.Kingdom)) return false;
                if( !DeepComparable.Matches(Phylum, otherT.Phylum)) return false;
                if( !DeepComparable.Matches(Class, otherT.Class)) return false;
                if( !DeepComparable.Matches(Order, otherT.Order)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as OrganismGeneralComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Kingdom, otherT.Kingdom)) return false;
                if( !DeepComparable.IsExactly(Phylum, otherT.Phylum)) return false;
                if( !DeepComparable.IsExactly(Class, otherT.Class)) return false;
                if( !DeepComparable.IsExactly(Order, otherT.Order)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Kingdom != null) yield return Kingdom;
                    if (Phylum != null) yield return Phylum;
                    if (Class != null) yield return Class;
                    if (Order != null) yield return Order;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Kingdom != null) yield return new ElementValue("kingdom", Kingdom);
                    if (Phylum != null) yield return new ElementValue("phylum", Phylum);
                    if (Class != null) yield return new ElementValue("class", Class);
                    if (Order != null) yield return new ElementValue("order", Order);
                }
            }

            
        }
        
        
        [FhirType("PartDescriptionComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class PartDescriptionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "PartDescriptionComponent"; } }
            
            /// <summary>
            /// Entity of anatomical origin of source material within an organism
            /// </summary>
            [FhirElement("part", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Part
            {
                get { return _Part; }
                set { _Part = value; OnPropertyChanged("Part"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Part;
            
            /// <summary>
            /// The detailed anatomic location when the part can be extracted from different anatomical locations of the organism. Multiple alternative locations may apply
            /// </summary>
            [FhirElement("partLocation", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept PartLocation
            {
                get { return _PartLocation; }
                set { _PartLocation = value; OnPropertyChanged("PartLocation"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _PartLocation;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PartDescriptionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Part != null) dest.Part = (Hl7.Fhir.Model.CodeableConcept)Part.DeepCopy();
                    if(PartLocation != null) dest.PartLocation = (Hl7.Fhir.Model.CodeableConcept)PartLocation.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PartDescriptionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PartDescriptionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Part, otherT.Part)) return false;
                if( !DeepComparable.Matches(PartLocation, otherT.PartLocation)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PartDescriptionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Part, otherT.Part)) return false;
                if( !DeepComparable.IsExactly(PartLocation, otherT.PartLocation)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Part != null) yield return Part;
                    if (PartLocation != null) yield return PartLocation;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Part != null) yield return new ElementValue("part", Part);
                    if (PartLocation != null) yield return new ElementValue("partLocation", PartLocation);
                }
            }

            
        }
        
        
        /// <summary>
        /// General high level classification of the source material specific to the origin of the material
        /// </summary>
        [FhirElement("sourceMaterialClass", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept SourceMaterialClass
        {
            get { return _SourceMaterialClass; }
            set { _SourceMaterialClass = value; OnPropertyChanged("SourceMaterialClass"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _SourceMaterialClass;
        
        /// <summary>
        /// The type of the source material shall be specified based on a controlled vocabulary. For vaccines, this subclause refers to the class of infectious agent
        /// </summary>
        [FhirElement("sourceMaterialType", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept SourceMaterialType
        {
            get { return _SourceMaterialType; }
            set { _SourceMaterialType = value; OnPropertyChanged("SourceMaterialType"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _SourceMaterialType;
        
        /// <summary>
        /// The state of the source material when extracted
        /// </summary>
        [FhirElement("sourceMaterialState", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept SourceMaterialState
        {
            get { return _SourceMaterialState; }
            set { _SourceMaterialState = value; OnPropertyChanged("SourceMaterialState"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _SourceMaterialState;
        
        /// <summary>
        /// The unique identifier associated with the source material parent organism shall be specified
        /// </summary>
        [FhirElement("organismId", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier OrganismId
        {
            get { return _OrganismId; }
            set { _OrganismId = value; OnPropertyChanged("OrganismId"); }
        }
        
        private Hl7.Fhir.Model.Identifier _OrganismId;
        
        /// <summary>
        /// The organism accepted Scientific name shall be provided based on the organism taxonomy
        /// </summary>
        [FhirElement("organismName", InSummary=true, Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString OrganismNameElement
        {
            get { return _OrganismNameElement; }
            set { _OrganismNameElement = value; OnPropertyChanged("OrganismNameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _OrganismNameElement;
        
        /// <summary>
        /// The organism accepted Scientific name shall be provided based on the organism taxonomy
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string OrganismName
        {
            get { return OrganismNameElement != null ? OrganismNameElement.Value : null; }
            set
            {
                if (value == null)
                  OrganismNameElement = null; 
                else
                  OrganismNameElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("OrganismName");
            }
        }
        
        /// <summary>
        /// The parent of the herbal drug Ginkgo biloba, Leaf is the substance ID of the substance (fresh) of Ginkgo biloba L. or Ginkgo biloba L. (Whole plant)
        /// </summary>
        [FhirElement("parentSubstanceId", InSummary=true, Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> ParentSubstanceId
        {
            get { if(_ParentSubstanceId==null) _ParentSubstanceId = new List<Hl7.Fhir.Model.Identifier>(); return _ParentSubstanceId; }
            set { _ParentSubstanceId = value; OnPropertyChanged("ParentSubstanceId"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _ParentSubstanceId;
        
        /// <summary>
        /// The parent substance of the Herbal Drug, or Herbal preparation
        /// </summary>
        [FhirElement("parentSubstanceName", InSummary=true, Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> ParentSubstanceNameElement
        {
            get { if(_ParentSubstanceNameElement==null) _ParentSubstanceNameElement = new List<Hl7.Fhir.Model.FhirString>(); return _ParentSubstanceNameElement; }
            set { _ParentSubstanceNameElement = value; OnPropertyChanged("ParentSubstanceNameElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _ParentSubstanceNameElement;
        
        /// <summary>
        /// The parent substance of the Herbal Drug, or Herbal preparation
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> ParentSubstanceName
        {
            get { return ParentSubstanceNameElement != null ? ParentSubstanceNameElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  ParentSubstanceNameElement = null; 
                else
                  ParentSubstanceNameElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("ParentSubstanceName");
            }
        }
        
        /// <summary>
        /// The country where the plant material is harvested or the countries where the plasma is sourced from as laid down in accordance with the Plasma Master File. For “Plasma-derived substances” the attribute country of origin provides information about the countries used for the manufacturing of the Cryopoor plama or Crioprecipitate
        /// </summary>
        [FhirElement("countryOfOrigin", InSummary=true, Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> CountryOfOrigin
        {
            get { if(_CountryOfOrigin==null) _CountryOfOrigin = new List<Hl7.Fhir.Model.CodeableConcept>(); return _CountryOfOrigin; }
            set { _CountryOfOrigin = value; OnPropertyChanged("CountryOfOrigin"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _CountryOfOrigin;
        
        /// <summary>
        /// The place/region where the plant is harvested or the places/regions where the animal source material has its habitat
        /// </summary>
        [FhirElement("geographicalLocation", InSummary=true, Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> GeographicalLocationElement
        {
            get { if(_GeographicalLocationElement==null) _GeographicalLocationElement = new List<Hl7.Fhir.Model.FhirString>(); return _GeographicalLocationElement; }
            set { _GeographicalLocationElement = value; OnPropertyChanged("GeographicalLocationElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _GeographicalLocationElement;
        
        /// <summary>
        /// The place/region where the plant is harvested or the places/regions where the animal source material has its habitat
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> GeographicalLocation
        {
            get { return GeographicalLocationElement != null ? GeographicalLocationElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  GeographicalLocationElement = null; 
                else
                  GeographicalLocationElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("GeographicalLocation");
            }
        }
        
        /// <summary>
        /// Stage of life for animals, plants, insects and microorganisms. This information shall be provided only when the substance is significantly different in these stages (e.g. foetal bovine serum)
        /// </summary>
        [FhirElement("developmentStage", InSummary=true, Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept DevelopmentStage
        {
            get { return _DevelopmentStage; }
            set { _DevelopmentStage = value; OnPropertyChanged("DevelopmentStage"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _DevelopmentStage;
        
        /// <summary>
        /// Many complex materials are fractions of parts of plants, animals, or minerals. Fraction elements are often necessary to define both Substances and Specified Group 1 Substances. For substances derived from Plants, fraction information will be captured at the Substance information level ( . Oils, Juices and Exudates). Additional information for Extracts, such as extraction solvent composition, will be captured at the Specified Substance Group 1 information level. For plasma-derived products fraction information will be captured at the Substance and the Specified Substance Group 1 levels
        /// </summary>
        [FhirElement("fractionDescription", InSummary=true, Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.SubstanceSourceMaterial.FractionDescriptionComponent> FractionDescription
        {
            get { if(_FractionDescription==null) _FractionDescription = new List<Hl7.Fhir.Model.SubstanceSourceMaterial.FractionDescriptionComponent>(); return _FractionDescription; }
            set { _FractionDescription = value; OnPropertyChanged("FractionDescription"); }
        }
        
        private List<Hl7.Fhir.Model.SubstanceSourceMaterial.FractionDescriptionComponent> _FractionDescription;
        
        /// <summary>
        /// This subclause describes the organism which the substance is derived from. For vaccines, the parent organism shall be specified based on these subclause elements. As an example, full taxonomy will be described for the Substance Name: ., Leaf
        /// </summary>
        [FhirElement("organism", InSummary=true, Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.SubstanceSourceMaterial.OrganismComponent Organism
        {
            get { return _Organism; }
            set { _Organism = value; OnPropertyChanged("Organism"); }
        }
        
        private Hl7.Fhir.Model.SubstanceSourceMaterial.OrganismComponent _Organism;
        
        /// <summary>
        /// To do
        /// </summary>
        [FhirElement("partDescription", InSummary=true, Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.SubstanceSourceMaterial.PartDescriptionComponent> PartDescription
        {
            get { if(_PartDescription==null) _PartDescription = new List<Hl7.Fhir.Model.SubstanceSourceMaterial.PartDescriptionComponent>(); return _PartDescription; }
            set { _PartDescription = value; OnPropertyChanged("PartDescription"); }
        }
        
        private List<Hl7.Fhir.Model.SubstanceSourceMaterial.PartDescriptionComponent> _PartDescription;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as SubstanceSourceMaterial;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(SourceMaterialClass != null) dest.SourceMaterialClass = (Hl7.Fhir.Model.CodeableConcept)SourceMaterialClass.DeepCopy();
                if(SourceMaterialType != null) dest.SourceMaterialType = (Hl7.Fhir.Model.CodeableConcept)SourceMaterialType.DeepCopy();
                if(SourceMaterialState != null) dest.SourceMaterialState = (Hl7.Fhir.Model.CodeableConcept)SourceMaterialState.DeepCopy();
                if(OrganismId != null) dest.OrganismId = (Hl7.Fhir.Model.Identifier)OrganismId.DeepCopy();
                if(OrganismNameElement != null) dest.OrganismNameElement = (Hl7.Fhir.Model.FhirString)OrganismNameElement.DeepCopy();
                if(ParentSubstanceId != null) dest.ParentSubstanceId = new List<Hl7.Fhir.Model.Identifier>(ParentSubstanceId.DeepCopy());
                if(ParentSubstanceNameElement != null) dest.ParentSubstanceNameElement = new List<Hl7.Fhir.Model.FhirString>(ParentSubstanceNameElement.DeepCopy());
                if(CountryOfOrigin != null) dest.CountryOfOrigin = new List<Hl7.Fhir.Model.CodeableConcept>(CountryOfOrigin.DeepCopy());
                if(GeographicalLocationElement != null) dest.GeographicalLocationElement = new List<Hl7.Fhir.Model.FhirString>(GeographicalLocationElement.DeepCopy());
                if(DevelopmentStage != null) dest.DevelopmentStage = (Hl7.Fhir.Model.CodeableConcept)DevelopmentStage.DeepCopy();
                if(FractionDescription != null) dest.FractionDescription = new List<Hl7.Fhir.Model.SubstanceSourceMaterial.FractionDescriptionComponent>(FractionDescription.DeepCopy());
                if(Organism != null) dest.Organism = (Hl7.Fhir.Model.SubstanceSourceMaterial.OrganismComponent)Organism.DeepCopy();
                if(PartDescription != null) dest.PartDescription = new List<Hl7.Fhir.Model.SubstanceSourceMaterial.PartDescriptionComponent>(PartDescription.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new SubstanceSourceMaterial());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as SubstanceSourceMaterial;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(SourceMaterialClass, otherT.SourceMaterialClass)) return false;
            if( !DeepComparable.Matches(SourceMaterialType, otherT.SourceMaterialType)) return false;
            if( !DeepComparable.Matches(SourceMaterialState, otherT.SourceMaterialState)) return false;
            if( !DeepComparable.Matches(OrganismId, otherT.OrganismId)) return false;
            if( !DeepComparable.Matches(OrganismNameElement, otherT.OrganismNameElement)) return false;
            if( !DeepComparable.Matches(ParentSubstanceId, otherT.ParentSubstanceId)) return false;
            if( !DeepComparable.Matches(ParentSubstanceNameElement, otherT.ParentSubstanceNameElement)) return false;
            if( !DeepComparable.Matches(CountryOfOrigin, otherT.CountryOfOrigin)) return false;
            if( !DeepComparable.Matches(GeographicalLocationElement, otherT.GeographicalLocationElement)) return false;
            if( !DeepComparable.Matches(DevelopmentStage, otherT.DevelopmentStage)) return false;
            if( !DeepComparable.Matches(FractionDescription, otherT.FractionDescription)) return false;
            if( !DeepComparable.Matches(Organism, otherT.Organism)) return false;
            if( !DeepComparable.Matches(PartDescription, otherT.PartDescription)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as SubstanceSourceMaterial;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(SourceMaterialClass, otherT.SourceMaterialClass)) return false;
            if( !DeepComparable.IsExactly(SourceMaterialType, otherT.SourceMaterialType)) return false;
            if( !DeepComparable.IsExactly(SourceMaterialState, otherT.SourceMaterialState)) return false;
            if( !DeepComparable.IsExactly(OrganismId, otherT.OrganismId)) return false;
            if( !DeepComparable.IsExactly(OrganismNameElement, otherT.OrganismNameElement)) return false;
            if( !DeepComparable.IsExactly(ParentSubstanceId, otherT.ParentSubstanceId)) return false;
            if( !DeepComparable.IsExactly(ParentSubstanceNameElement, otherT.ParentSubstanceNameElement)) return false;
            if( !DeepComparable.IsExactly(CountryOfOrigin, otherT.CountryOfOrigin)) return false;
            if( !DeepComparable.IsExactly(GeographicalLocationElement, otherT.GeographicalLocationElement)) return false;
            if( !DeepComparable.IsExactly(DevelopmentStage, otherT.DevelopmentStage)) return false;
            if( !DeepComparable.IsExactly(FractionDescription, otherT.FractionDescription)) return false;
            if( !DeepComparable.IsExactly(Organism, otherT.Organism)) return false;
            if( !DeepComparable.IsExactly(PartDescription, otherT.PartDescription)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (SourceMaterialClass != null) yield return SourceMaterialClass;
				if (SourceMaterialType != null) yield return SourceMaterialType;
				if (SourceMaterialState != null) yield return SourceMaterialState;
				if (OrganismId != null) yield return OrganismId;
				if (OrganismNameElement != null) yield return OrganismNameElement;
				foreach (var elem in ParentSubstanceId) { if (elem != null) yield return elem; }
				foreach (var elem in ParentSubstanceNameElement) { if (elem != null) yield return elem; }
				foreach (var elem in CountryOfOrigin) { if (elem != null) yield return elem; }
				foreach (var elem in GeographicalLocationElement) { if (elem != null) yield return elem; }
				if (DevelopmentStage != null) yield return DevelopmentStage;
				foreach (var elem in FractionDescription) { if (elem != null) yield return elem; }
				if (Organism != null) yield return Organism;
				foreach (var elem in PartDescription) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (SourceMaterialClass != null) yield return new ElementValue("sourceMaterialClass", SourceMaterialClass);
                if (SourceMaterialType != null) yield return new ElementValue("sourceMaterialType", SourceMaterialType);
                if (SourceMaterialState != null) yield return new ElementValue("sourceMaterialState", SourceMaterialState);
                if (OrganismId != null) yield return new ElementValue("organismId", OrganismId);
                if (OrganismNameElement != null) yield return new ElementValue("organismName", OrganismNameElement);
                foreach (var elem in ParentSubstanceId) { if (elem != null) yield return new ElementValue("parentSubstanceId", elem); }
                foreach (var elem in ParentSubstanceNameElement) { if (elem != null) yield return new ElementValue("parentSubstanceName", elem); }
                foreach (var elem in CountryOfOrigin) { if (elem != null) yield return new ElementValue("countryOfOrigin", elem); }
                foreach (var elem in GeographicalLocationElement) { if (elem != null) yield return new ElementValue("geographicalLocation", elem); }
                if (DevelopmentStage != null) yield return new ElementValue("developmentStage", DevelopmentStage);
                foreach (var elem in FractionDescription) { if (elem != null) yield return new ElementValue("fractionDescription", elem); }
                if (Organism != null) yield return new ElementValue("organism", Organism);
                foreach (var elem in PartDescription) { if (elem != null) yield return new ElementValue("partDescription", elem); }
            }
        }

    }
    
}
