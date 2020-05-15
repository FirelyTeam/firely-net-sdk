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
    /// A request for a diet, formula or nutritional supplement
    /// </summary>
    public partial interface INutritionOrder : Hl7.Fhir.Model.IDomainResource
    {
    
        /// <summary>
        /// Identifiers assigned to this order
        /// </summary>
        List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
    
        /// <summary>
        /// The person who requires the diet, formula or nutritional supplement
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Patient { get; set; }
    
        /// <summary>
        /// The encounter associated with this nutrition order
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Encounter { get; set; }
    
        /// <summary>
        /// Date and time the nutrition order was requested
        /// </summary>
        Hl7.Fhir.Model.FhirDateTime DateTimeElement { get; set; }
        
        /// <summary>
        /// Date and time the nutrition order was requested
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string DateTime { get; set; }
    
        /// <summary>
        /// Who ordered the diet, formula or nutritional supplement
        /// </summary>
        Hl7.Fhir.Model.ResourceReference Orderer { get; set; }
    
        /// <summary>
        /// List of the patient's food and nutrition-related allergies and intolerances
        /// </summary>
        List<Hl7.Fhir.Model.ResourceReference> AllergyIntolerance { get; set; }
    
        /// <summary>
        /// Order-specific modifier about the type of food that should be given
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> FoodPreferenceModifier { get; set; }
    
        /// <summary>
        /// Order-specific modifier about the type of food that should not be given
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> ExcludeFoodModifier { get; set; }
    
        /// <summary>
        /// Oral diet components
        /// </summary>
        Hl7.Fhir.Model.INutritionOrderOralDietComponent OralDiet { get; }
    
        /// <summary>
        /// Supplement components
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.INutritionOrderSupplementComponent> Supplement { get; }
    
        /// <summary>
        /// Enteral formula components
        /// </summary>
        Hl7.Fhir.Model.INutritionOrderEnteralFormulaComponent EnteralFormula { get; }
    
    }
    
    public partial interface INutritionOrderOralDietComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Type of oral diet or diet restrictions that describe what can be consumed orally
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> Type { get; set; }
    
        /// <summary>
        /// Scheduled frequency of diet
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ITiming> Schedule { get; }
    
        /// <summary>
        /// Required  nutrient modifications
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.INutritionOrderNutrientComponent> Nutrient { get; }
    
        /// <summary>
        /// Required  texture modifications
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.INutritionOrderTextureComponent> Texture { get; }
    
        /// <summary>
        /// The required consistency of fluids and liquids provided to the patient
        /// </summary>
        List<Hl7.Fhir.Model.CodeableConcept> FluidConsistencyType { get; set; }
    
        /// <summary>
        /// Instructions or additional information about the oral diet
        /// </summary>
        Hl7.Fhir.Model.FhirString InstructionElement { get; set; }
        
        /// <summary>
        /// Instructions or additional information about the oral diet
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Instruction { get; set; }
    
    }
    
    public partial interface INutritionOrderNutrientComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Type of nutrient that is being modified
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Modifier { get; set; }
    
        /// <summary>
        /// Quantity of the specified nutrient
        /// </summary>
        Hl7.Fhir.Model.SimpleQuantity Amount { get; set; }
    
    }
    
    public partial interface INutritionOrderTextureComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Code to indicate how to alter the texture of the foods, e.g. pureed
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Modifier { get; set; }
    
        /// <summary>
        /// Concepts that are used to identify an entity that is ingested for nutritional purposes
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept FoodType { get; set; }
    
    }
    
    public partial interface INutritionOrderSupplementComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Type of supplement product requested
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept Type { get; set; }
    
        /// <summary>
        /// Product or brand name of the nutritional supplement
        /// </summary>
        Hl7.Fhir.Model.FhirString ProductNameElement { get; set; }
        
        /// <summary>
        /// Product or brand name of the nutritional supplement
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string ProductName { get; set; }
    
        /// <summary>
        /// Scheduled frequency of supplement
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.ITiming> Schedule { get; }
    
        /// <summary>
        /// Amount of the nutritional supplement
        /// </summary>
        Hl7.Fhir.Model.SimpleQuantity Quantity { get; set; }
    
        /// <summary>
        /// Instructions or additional information about the oral supplement
        /// </summary>
        Hl7.Fhir.Model.FhirString InstructionElement { get; set; }
        
        /// <summary>
        /// Instructions or additional information about the oral supplement
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string Instruction { get; set; }
    
    }
    
    public partial interface INutritionOrderEnteralFormulaComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Type of enteral or infant formula
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept BaseFormulaType { get; set; }
    
        /// <summary>
        /// Product or brand name of the enteral or infant formula
        /// </summary>
        Hl7.Fhir.Model.FhirString BaseFormulaProductNameElement { get; set; }
        
        /// <summary>
        /// Product or brand name of the enteral or infant formula
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string BaseFormulaProductName { get; set; }
    
        /// <summary>
        /// Type of modular component to add to the feeding
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept AdditiveType { get; set; }
    
        /// <summary>
        /// Product or brand name of the modular additive
        /// </summary>
        Hl7.Fhir.Model.FhirString AdditiveProductNameElement { get; set; }
        
        /// <summary>
        /// Product or brand name of the modular additive
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string AdditiveProductName { get; set; }
    
        /// <summary>
        /// Amount of energy per specified volume that is required
        /// </summary>
        Hl7.Fhir.Model.SimpleQuantity CaloricDensity { get; set; }
    
        /// <summary>
        /// How the formula should enter the patient's gastrointestinal tract
        /// </summary>
        Hl7.Fhir.Model.CodeableConcept RouteofAdministration { get; set; }
    
        /// <summary>
        /// Formula feeding instruction as structured data
        /// </summary>
        IEnumerable<Hl7.Fhir.Model.INutritionOrderAdministrationComponent> Administration { get; }
    
        /// <summary>
        /// Upper limit on formula volume per unit of time
        /// </summary>
        Hl7.Fhir.Model.SimpleQuantity MaxVolumeToDeliver { get; set; }
    
        /// <summary>
        /// Formula feeding instructions expressed as text
        /// </summary>
        Hl7.Fhir.Model.FhirString AdministrationInstructionElement { get; set; }
        
        /// <summary>
        /// Formula feeding instructions expressed as text
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        string AdministrationInstruction { get; set; }
    
    }
    
    public partial interface INutritionOrderAdministrationComponent : Hl7.Fhir.Model.IBackboneElement
    {
    
        /// <summary>
        /// Scheduled frequency of enteral feeding
        /// </summary>
        Hl7.Fhir.Model.ITiming Schedule { get; }
    
        /// <summary>
        /// The volume of formula to provide
        /// </summary>
        Hl7.Fhir.Model.SimpleQuantity Quantity { get; set; }
    
        /// <summary>
        /// Speed with which the formula is provided per period of time
        /// </summary>
        Hl7.Fhir.Model.Element Rate { get; set; }
    
    }

}
