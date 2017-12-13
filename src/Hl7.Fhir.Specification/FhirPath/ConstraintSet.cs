using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Hl7.Fhir.Specification.FhirPath
{
    internal class ConstraintSet : ReadOnlyCollection<Constraint>
    {
        readonly public ElementDefinition.TypeRefComponent[] CandidateTypes;

        public ConstraintSet(IList<Constraint> list) : base(list)
        {
            CandidateTypes = this.SelectMany(c => c.CandidateTypes).Distinct().ToArray();
        }

        public ConstraintSet(IEnumerable<Constraint> constraints) : this(constraints.ToList())
        {
        }

        public static ConstraintSet FromStructureDefinition(StructureDefinition definition) 
            => new ConstraintSet(new[] { Constraint.FromStructure(definition) });

        public static ConstraintSet FromDefinitionNavigator(ElementDefinitionNavigator navigator, bool includeSlices) 
            => new ConstraintSet(Constraint.FromDefinition(navigator, includeSlices));

        public ConstraintSet WithChild(string name, bool includeSlices=false)
        {
            var matches = this
                .Where(c => c.HasChildren)
                .Select(c => c.Source.ShallowCopy())
                .Where(nav => tryMoveToName(nav, name))
                .SelectMany(nav => FromDefinitionNavigator(nav, includeSlices));

            return new ConstraintSet(matches);
        }

        public ConstraintSet WithChild(string name, IResourceResolver resolver, bool includeSlices=false)
        {
            var withLocalConstraints = new ConstraintSet(this.Where(c => c.HasChildren));
            var withoutLocalConstraints = new ConstraintSet(this.Where(c => !c.HasChildren));
            var referencedConstraints = withoutLocalConstraints.ReferencedTypes(resolver);

            return withLocalConstraints.WithChild(name, includeSlices) + referencedConstraints.WithChild(name, includeSlices);
        }

        public ConstraintSet WithType(FHIRDefinedType type, string profile = null)
        {
            var newConstraints = this
                    .Select(c => c.WithTypes(type, profile))
                    .Where(c => c.HasCandidates);

            return new ConstraintSet(newConstraints);
        }

        public static ConstraintSet operator +(ConstraintSet left, ConstraintSet right)
            => new ConstraintSet(left.Union(right));

        private static StructureDefinition[] getStructures(IEnumerable<string> profiles, IResourceResolver resolver)
        {
            return profiles
                .Select(p => resolver.FindStructureDefinition(p))
                .Where(sd => sd != null)        // TODO: Don't ignore these, report errors about unresolved references!
                .ToArray();
        }

        internal ConstraintSet ReferencedTypes(IResourceResolver resolver)
        {
            var profiles = CandidateTypes.Select(tr => tr.TypeProfile());
            var sds = getStructures(profiles, resolver);
            var result = sds.Select(sd => Constraint.FromStructure(sd));

            return new ConstraintSet(result);
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
