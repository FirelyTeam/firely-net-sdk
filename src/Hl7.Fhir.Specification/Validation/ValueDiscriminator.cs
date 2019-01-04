/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Navigation.FhirPath;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using Hl7.FhirPath;
using System;
using System.Linq;

namespace Hl7.Fhir.Validation
{
    internal enum SlicingType
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


    internal static class DisciminatorFactory
    {
        public static IDiscriminator Build(string discriminator)
        {
            if (discriminator.EndsWith("@type"))
                throw Error.NotImplemented($"Slicing with an '@type' discriminator is not yet supported by this validator.");
            else if (discriminator.EndsWith("@profile"))
                throw Error.NotImplemented($"Slicing with an '@profile' discriminator is not yet supported by this validator.");

            throw new NotImplementedException();
        }

    }

    internal class ValueDiscriminator : IDiscriminator
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

        // Note: the discriminator (input) is just a string in DSTU2. Becomes a backbone element in STU3+
        public ValueDiscriminator(string discriminator, ElementDefinitionNavigator root, Validator validator) 
        {
            var resolver = validator?.Settings?.ResourceResolver ??
                throw Error.Argument("Discriminator validation needs a ResourceResolver to be set in the ValidationSettings.");
            
            if (discriminator.EndsWith("@type"))
                throw Error.NotImplemented($"Slicing with an '@type' discriminator is not yet supported by this validator.");
            else if (discriminator.EndsWith("@profile"))
                throw Error.NotImplemented($"Slicing with an '@profile' discriminator is not yet supported by this validator.");

            // just support "normal" value-based slicing for now.
            Method = SlicingType.Value;
            Condition = walkToCondition(root, discriminator, resolver);
            valueDiscriminatorPath = discriminator;
            this.validator = validator;
        }

        private ElementDefinition walkToCondition(ElementDefinitionNavigator root, string discriminator, IResourceResolver resolver)
        {
            var walker = new StructureDefinitionWalker(root, resolver);
            var conditions = walker.Walk(discriminator);

            // Well, we could check whether the conditions are Equal, since that's what really matters - they should not differ.
            // Also, this should not be a NotSupported - but the more specific InvalidStructureDefinitionContent (or something
            // like that) exception, but this new type has not yet been pulled in develop from PR#815. [20190104]
            if (conditions.Count > 1)
                throw Error.NotImplemented($"The discriminator path '{discriminator}' at {root.QualifiedDefinitionPath()} leads to multiple ElementDefinitions, which is not allowed");

            return conditions.Single().Current.Current;
        }

        // This is the ElementDefinition the discriminator (value, pattern, type) gets it's condition from
        public readonly ElementDefinition Condition;

        // This is the method of slicing, how the discriminator is used to slice
        public readonly SlicingType Method;

        private readonly string valueDiscriminatorPath;
        private readonly Validator validator;

        public bool Matches(ITypedElement candidate)
        {
            ITypedElement[] values = candidate.Select(valueDiscriminatorPath).ToArray();

            // Don't know how to handle a discriminating element that repeats - all? any?
            if(values.Length > 1)
                throw Error.NotImplemented($"The instance has multiple elements at '{candidate.Location}' " +
                    $"for the discriminator path '{valueDiscriminatorPath}'. Don't know how to handle that.");

            if (Condition.Fixed != null)
                return matchesFixed(values[0]);
            else
                return matchesValueSet(values[0]);
        }

        private bool matchesValueSet(ITypedElement instance)
        {
            //TODO: The reason why there's no match might be interesting for debugging purposes,
            //we need to add a way to Trace() that - or make it part of the new Validation outcome plans.
            var result = validator.ValidateBinding(Condition, instance);
            return result.Success;
        }
        private bool matchesFixed(ITypedElement instance)
        {
            //TODO: The reason why there's no match might be interesting for debugging purposes,
            //we need to add a way to Trace() that - or make it part of the new Validation outcome plans.
            var result = validator.ValidateFixed(Condition, instance);
            return result.Success;
        }
    }
}
