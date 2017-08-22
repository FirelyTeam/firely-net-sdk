using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.FhirPath
{
    internal class TypeCandidates : List<ElementDefinitionNavigator>
    {
        public TypeCandidates()
        {
        }

        public TypeCandidates(StructureDefinition definition)
        {
            AddCandidateType(definition);
        }

        public TypeCandidates(TypeCandidates candidates)
        {
            foreach (var candidate in candidates)
                this.AddCandidateType(candidate);
        }

        // Constructor is private, since it will *not* clone the navigators passed in.
        private TypeCandidates(IEnumerable<ElementDefinitionNavigator> navigators)
        {
            this.AddRange(navigators);
        }

        public void AddCandidateType(StructureDefinition definition)
        {
            var nav = new ElementDefinitionNavigator(definition);
            nav.MoveToFirstChild();
            AddCandidateType(nav);
        }

        public void AddCandidateType(ElementDefinitionNavigator navigator)
        {
            base.Add(navigator.ShallowCopy());
        }

        public TypeCandidates WithChild(string name)
        {
            return new TypeCandidates(
                this.Select(c => c.ShallowCopy())
                    .Where(c => tryMoveToName(c, name)));
        }

        public TypeCandidates WithType(FHIRDefinedType type, string profile = null)
        {
            return new TypeCandidates(
                this.Where(c => matchesType(c))
                    .Select(c => c.ShallowCopy()));

            bool matchesType(ElementDefinitionNavigator definition)
            {
                //In DSTU2, if this is the root of a type, we need to look at the constrainedType or else the id
                if (ElementDefinitionNavigator.IsRootPath(definition.Path))
                {
                    var myType = definition.StructureDefinition.ConstrainedType?.GetLiteral() ??
                                    definition.StructureDefinition.Id;

                    return myType == type.GetLiteral() &&
                        (profile == null || definition.StructureDefinition.Url == profile);
                }
                else
                    return definition.Current.Type.Any(tr => tr.Code == type && (profile == null || tr.Profile?.FirstOrDefault() == profile));
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
