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

//
// Generated for FHIR v3.5.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Chemical substances are a single substance type whose primary defining element is the molecular structure. Chemical substances shall be defined on the basis of their complete covalent molecular structure; the presence of a salt (counter-ion) and/or solvates (water, alcohols) is also captured. Purity, grade, physical form or particle size are not taken into account in the definition of a chemical substance or in the assignment of a Substance ID
    /// </summary>
    [FhirType("SubstanceMoiety")]
    [DataContract]
    public partial class SubstanceMoiety : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "SubstanceMoiety"; } }
        
        /// <summary>
        /// The role of the moiety should be specified if there is a specific role the moiety is playing
        /// </summary>
        [FhirElement("role", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Role
        {
            get { return _Role; }
            set { _Role = value; OnPropertyChanged("Role"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Role;
        
        /// <summary>
        /// The unique identifier assigned to the substance representing the moiety based on the ISO 11238 substance controlled vocabulary
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// The name of the moiety shall be provided
        /// </summary>
        [FhirElement("name", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// The name of the moiety shall be provided
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
        /// Stereochemistry shall be captured as described in 4.7.1
        /// </summary>
        [FhirElement("stereochemistry", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Stereochemistry
        {
            get { return _Stereochemistry; }
            set { _Stereochemistry = value; OnPropertyChanged("Stereochemistry"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Stereochemistry;
        
        /// <summary>
        /// Optical activity shall be captured as described in 4.7.2
        /// </summary>
        [FhirElement("opticalActivity", InSummary=true, Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept OpticalActivity
        {
            get { return _OpticalActivity; }
            set { _OpticalActivity = value; OnPropertyChanged("OpticalActivity"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _OpticalActivity;
        
        /// <summary>
        /// Molecular formula shall be captured as described in 4.7.3
        /// </summary>
        [FhirElement("molecularFormula", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString MolecularFormulaElement
        {
            get { return _MolecularFormulaElement; }
            set { _MolecularFormulaElement = value; OnPropertyChanged("MolecularFormulaElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _MolecularFormulaElement;
        
        /// <summary>
        /// Molecular formula shall be captured as described in 4.7.3
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string MolecularFormula
        {
            get { return MolecularFormulaElement != null ? MolecularFormulaElement.Value : null; }
            set
            {
                if (value == null)
                  MolecularFormulaElement = null; 
                else
                  MolecularFormulaElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("MolecularFormula");
            }
        }
        
        /// <summary>
        /// Used to capture quantitative values for a variety of elements. If only limits are given, the arithmetic mean would be the average. If only a single definite value for a given element is given, it would be captured in this field
        /// </summary>
        [FhirElement("amount", InSummary=true, Order=150)]
        [DataMember]
        public SubstanceAmount Amount
        {
            get { return _Amount; }
            set { _Amount = value; OnPropertyChanged("Amount"); }
        }
        
        private SubstanceAmount _Amount;
        

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as SubstanceMoiety;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Role != null) dest.Role = (Hl7.Fhir.Model.CodeableConcept)Role.DeepCopy();
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(Stereochemistry != null) dest.Stereochemistry = (Hl7.Fhir.Model.CodeableConcept)Stereochemistry.DeepCopy();
                if(OpticalActivity != null) dest.OpticalActivity = (Hl7.Fhir.Model.CodeableConcept)OpticalActivity.DeepCopy();
                if(MolecularFormulaElement != null) dest.MolecularFormulaElement = (Hl7.Fhir.Model.FhirString)MolecularFormulaElement.DeepCopy();
                if(Amount != null) dest.Amount = (SubstanceAmount)Amount.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new SubstanceMoiety());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as SubstanceMoiety;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Role, otherT.Role)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(Stereochemistry, otherT.Stereochemistry)) return false;
            if( !DeepComparable.Matches(OpticalActivity, otherT.OpticalActivity)) return false;
            if( !DeepComparable.Matches(MolecularFormulaElement, otherT.MolecularFormulaElement)) return false;
            if( !DeepComparable.Matches(Amount, otherT.Amount)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as SubstanceMoiety;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(Stereochemistry, otherT.Stereochemistry)) return false;
            if( !DeepComparable.IsExactly(OpticalActivity, otherT.OpticalActivity)) return false;
            if( !DeepComparable.IsExactly(MolecularFormulaElement, otherT.MolecularFormulaElement)) return false;
            if( !DeepComparable.IsExactly(Amount, otherT.Amount)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                if (Role != null) yield return Role;
                if (Identifier != null) yield return Identifier;
                if (NameElement != null) yield return NameElement;
                if (Stereochemistry != null) yield return Stereochemistry;
                if (OpticalActivity != null) yield return OpticalActivity;
                if (MolecularFormulaElement != null) yield return MolecularFormulaElement;
                if (Amount != null) yield return Amount;
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren 
        { 
            get 
            { 
                foreach (var item in base.NamedChildren) yield return item; 
                if (Role != null) yield return new ElementValue("role", false, Role);
                if (Identifier != null) yield return new ElementValue("identifier", false, Identifier);
                if (NameElement != null) yield return new ElementValue("name", false, NameElement);
                if (Stereochemistry != null) yield return new ElementValue("stereochemistry", false, Stereochemistry);
                if (OpticalActivity != null) yield return new ElementValue("opticalActivity", false, OpticalActivity);
                if (MolecularFormulaElement != null) yield return new ElementValue("molecularFormula", false, MolecularFormulaElement);
                if (Amount != null) yield return new ElementValue("amount", false, Amount);
 
            } 
        } 
    
    
    }
    
}
