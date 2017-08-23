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

        private Constraint(IEnumerable<ElementDefinition.TypeRefComponent> types, ElementDefinitionNavigator source)
        {
            Types = types.ToArray();
            Source = source.ShallowCopy();
        }

        public static Constraint Create(Constraint constraint)
        {
            return new Constraint(constraint.Types, constraint.Source);
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
                if (pointer.Current?.Type != null)
                {
                    var types = pointer.Current.Type
                            .Where(tr => tr.Code != null);

                    result.Add(new Constraint(types, pointer));
                }
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
                return navigator.Current.Type.ToArray();
        }

        public bool MatchesType(FHIRDefinedType type, string profile = null)
        {
            return Types.Any(tr => tr.Code == type && (profile == null || tr.Profile?.FirstOrDefault() == profile));
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
            return new ConstraintSet(
                this.Where(c => c.MatchesType(type, profile))
                   .Select(c => Constraint.Create(c))
                   );
        }

        public ConstraintSet IncludeReferencedTypes(IResourceResolver resolver)
        {
            var typesToInclude =
                this.SelectMany(c => c.Types).Distinct(new TypeRefEqualityComparer());

            var result = new ConstraintSet(this);

            foreach(var type in typesToInclude)
            {
                var profile = type.TypeProfile();
                var sd = resolver.FindStructureDefinition(profile);
                if (sd != null)
                    result.AddConstraint(sd);
            }

            return result;
        }

        private class TypeRefEqualityComparer : IEqualityComparer<ElementDefinition.TypeRefComponent>
        {
            public bool Equals(ElementDefinition.TypeRefComponent x, ElementDefinition.TypeRefComponent y)
            {
                return x.Code == y.Code && x.Profile == y.Profile;
            }

            public int GetHashCode(ElementDefinition.TypeRefComponent obj)
            {
                return (obj?.Code.GetHashCode() ?? 0) ^ (obj?.Profile?.GetHashCode() ?? 0);
            }
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
