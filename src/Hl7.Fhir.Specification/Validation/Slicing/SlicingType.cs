/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

namespace Hl7.Fhir.Validation
{
    public enum SlicingType
    {
        // the most commonly used discriminator type: to decide based on the value of an element. Elements used like this are mostly primitive types- code, uri. Typical example: slice on the value of Patient.telecom.system, for values phone, email etc.
        Value,

        // This is mostly used with elements of type CodeableConcept where the elements are distinguished by the presence of a particular code but other codes are expected to be present, and are irrelevant for the slice matching process. Typical example: slice on the value of Observation.code, for values LOINC codes 1234-5, 4235-8 etc
        Pattern,

        // This is not used commonly - it only has 2 values, so not much discrimination power. It's mainly used as an adjunct slicing criteria along with other discriminators. Elements used like this are mostly complex backbone elements. Typical example: slice on the pattern of Observation.code and the presence of Observation.component.
        Exists,

        // Used to match slices based on the type of the item. While it can be used with polymorphic elements such as Observation.value[x], mostly it is used with Resource types on references, to apply different profiles based on the different resource type. Typical example: slice on the type of List.item.resolve() for the types Patient, RelatedPerson.
        Type,

        // Used to match slices based on the whether the item conforms to the specified profile. This provides the most power, since the full range of profiling capabilities are available, but it is also the hardest to implement, and requires the most processing (>1000-fold compared to the others). Implementers should use this only where absolutely required. Typical example: slice on the type of Composition.section.entry() for the profiles Current-Clinical-Condition, Past-Medical-Event, etc
        Profile
    }
}
