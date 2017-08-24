using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.FhirPath
{
    internal class Constraint
    {
        readonly public ElementDefinition.TypeRefComponent[] Types;
        readonly public ElementDefinitionNavigator Source;

        internal Constraint(IEnumerable<ElementDefinition.TypeRefComponent> types, ElementDefinitionNavigator source)
        {
            Types = types.ToArray();
            Source = source.ShallowCopy();
        }

        public static Constraint Create(StructureDefinition definition)
        {
            var nav = new ElementDefinitionNavigator(definition);
            nav.MoveToFirstChild();
            return Create(nav).Single();
        }

        public static Constraint[] Create(ElementDefinitionNavigator definition)
        {
            var pointer = definition.ShallowCopy();
            var result = new List<Constraint>();

            do
            {
                result.Add(new Constraint(typesFromDefinition(pointer), pointer));
            }
            while (pointer.MoveToNextSliceAtAnyLevel());

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

                return new[]
                {
                    new ElementDefinition.TypeRefComponent { Code = ModelInfo.FhirTypeNameToFhirType(myType), Profile = new string[] { myProfile } }
                };
            }
            else
            {
                return navigator.Current?.Type?.ToArray() ?? new ElementDefinition.TypeRefComponent[0];
            }
        }

        public ElementDefinition.TypeRefComponent[] MatchingTypeRefs(FHIRDefinedType type, string profile = null)
        {
            return Types.Where(tr => tr.Code == type && (profile == null || tr.Profile?.FirstOrDefault() == profile)).ToArray();
        }
    }

    internal class ConstraintSet : List<Constraint>
    {
        public ConstraintSet()
        {
        }

        public ConstraintSet(IEnumerable<Constraint> constraints)
        {
            this.AddRange(constraints);
        }

        public ConstraintSet(StructureDefinition definition)
        {
            this.AddConstraint(definition);
        }

        public void AddConstraint(StructureDefinition definition)
        {
            this.Add(Constraint.Create(definition));
        }

        public void AddConstraints(ElementDefinitionNavigator navigator)
        {
            this.AddRange(Constraint.Create(navigator));
        }

        public ConstraintSet WithChild(string name)
        {
            var result = new List<Constraint>();

            foreach (var constraint in this)
            {
                var childNav = constraint.Source.ShallowCopy();
                if (tryMoveToName(childNav, name))
                    result.AddRange(Constraint.Create(childNav));
            }

            return new ConstraintSet(result);
        }

        public ConstraintSet WithType(FHIRDefinedType type, string profile = null)
        {
            var newConstraints =
                from c in this
                let matchingRefs = c.MatchingTypeRefs(type, profile)
                where matchingRefs.Any()
                select new Constraint(matchingRefs, c.Source);

            return new ConstraintSet(newConstraints);
        }

        public ElementDefinition.TypeRefComponent[] CandidateTypeRefs()
        {
            return  this.SelectMany(c => c.Types).Distinct().ToArray();
        }

        public FHIRDefinedType[] CandidateTypes()
        {
            return this.SelectMany(c => c.Types)
                .Where(t=>t.Code != null)
                .Select(t=> t.Code.Value)
                .Distinct().ToArray();
        }


        public ConstraintSet IncludeReferencedTypes(IResourceResolver resolver)
        {
            var result = new ConstraintSet(this);

            foreach(var type in result.CandidateTypeRefs())
            {
                var profile = type.TypeProfile();
                var sd = resolver.FindStructureDefinition(profile);
                if (sd != null)
                    result.AddConstraint(sd);
            }

            return result;
        }

        // Note: alters the position of the navigator passed into it
        private static bool tryMoveToName(ElementDefinitionNavigator navigator, string name)
        {
            if (navigator.MoveToFirstChild())
            {
                do
                {
                    var currentPath = navigator.PathName;

                    // matches names exactly
                    if (currentPath == name) return true;

                    // matches value[x] or valueBoolean on value
                    if (atChoice(navigator) && String.Compare(currentPath, 0, name, 0, name.Length - 3) == 0) return true;

                    bool atChoice(ElementDefinitionNavigator nav)
                    {
                        if (ElementDefinitionNavigator.IsChoiceTypeElement(nav.Path)) return true;

                        var basePath = navigator?.Current?.Base?.Path;
                        return basePath != null && ElementDefinitionNavigator.IsChoiceTypeElement(basePath);
                    }
                }
                while (navigator.MoveToNext());
            }

            return false;
        }
    }
}
