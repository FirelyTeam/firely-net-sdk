using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.FhirPath
{
    /// <summary>
    /// A Constraint is a tuple consisting of an ElementDefinitionNavigator (representing an ElementDefinition) and a list of 
    /// candidate types.
    /// </summary>
    /// <remarks>
    /// The list of candidate types in Types may be a subset of the types in ElementDefinition.TypeRef, and is used to indicate which of the
    /// types permitted by the definition are actually applicable. The set is initially the same as ElementDefinition.TypeRef,
    /// but can be constrained by the <see cref="WithTypes(FHIRDefinedType, string)"/> member.
    /// </remarks>
    internal class Constraint
    {
        /// <summary>
        /// The subset of "active" types within the complete set of types in ElementDefinition.TypeRef
        /// </summary>
        readonly public ElementDefinition.TypeRefComponent[] CandidateTypes;

        /// <summary>
        /// The ElementDefinition that this Constraint represents (Source.Current)
        /// </summary>
        readonly public ElementDefinitionNavigator Source;

        private Constraint(ElementDefinitionNavigator source, ElementDefinition.TypeRefComponent[] types)
        {
            CandidateTypes = types;
            Source = source.ShallowCopy();
        }

        public static Constraint FromStructure(StructureDefinition definition)
        {
            var nav = new ElementDefinitionNavigator(definition);
            nav.MoveToFirstChild();

            // The root cannot be sliced, so don't look for them
            return FromDefinition(nav, includeSlices: false).Single();
        }

        public static Constraint[] FromDefinition(ElementDefinitionNavigator definition, bool includeSlices)
        {
            var result = new List<Constraint>();
            var scan = definition.ShallowCopy();

            do
            {
                result.Add(new Constraint(scan, typesFromDefinition(scan)));
            }
            while (includeSlices && scan.MoveToNextSliceAtAnyLevel());

            return result.ToArray();
        }

        private static ElementDefinition.TypeRefComponent[] typesFromDefinition(ElementDefinitionNavigator navigator)
        {
            //In DSTU2, if this is the root of a type, we need to look at the constrainedType or else the id
            if (ElementDefinitionNavigator.IsRootPath(navigator.Path))
            {
                bool isConstrained = navigator.StructureDefinition.ConstrainedType != null;
                var myType = isConstrained ? navigator.StructureDefinition.ConstrainedType.GetLiteral() :
                                navigator.StructureDefinition.Id;
                var myProfile = isConstrained ? navigator.StructureDefinition.Url : null;

                var result = new ElementDefinition.TypeRefComponent { Code = ModelInfo.FhirTypeNameToFhirType(myType) };
                if (myProfile != null) result.Profile = new string[] { myProfile };

                return new[] { result };
            }
            else
            {
                return navigator.Current?.Type?.ToArray() ?? new ElementDefinition.TypeRefComponent[0];
            }
        }

        public Constraint WithTypes(FHIRDefinedType type, string profile = null) => new Constraint(this.Source, MatchingTypeRefs(type, profile));

        public bool HasCandidates => CandidateTypes.Any();

        public bool HasChildren => Source.HasChildren;

        public ElementDefinition.TypeRefComponent[] MatchingTypeRefs(FHIRDefinedType type, string profile = null)
        {
            return CandidateTypes.Where(tr => tr.Code == type && (profile == null || tr.Profile?.FirstOrDefault() == profile)).ToArray();
        }
    }
}
