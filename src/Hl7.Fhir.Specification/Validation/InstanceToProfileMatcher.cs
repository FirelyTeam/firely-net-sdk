using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.Core.ElementModel;
using Hl7.Fhir.FluentPath;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Validation
{

    internal class InstanceToProfileMatcher
    {
        public static MatchResult Match(ElementDefinitionNavigator definitionParent, IElementNavigator instanceParent)
        {
            List<ElementDefinitionNavigator> definitionElements = harvestDefinitionNames(definitionParent);
            List<IElementNavigator> elementsToMatch = instanceParent.Children().ToList();

            List<Match> matches = new List<Match>();

            foreach(var definitionElement in definitionElements)
            {
                var match = new Match() { Definition = definitionElement };

                // Special case is the .value of a primitive fhir type, this is represented
                // as the "Value" of the IValueProvider interface, not as a real child
                if (definitionElement.Current.IsPrimitiveValuePath())
                {
                    if (instanceParent.Value != null)
                        match.InstanceElements = new List<IElementNavigator> { instanceParent };
                    else
                        match.InstanceElements = new List<IElementNavigator>();
                }
                else
                {
                    var found = elementsToMatch.Where(ie => NameMatches(definitionElement, ie)).ToList();

                    match.InstanceElements.AddRange(found);
                    elementsToMatch.RemoveAll(e => found.Contains(e));
                }

                matches.Add(match);
            }

            MatchResult result = new MatchResult();
            result.Matches = matches;
            result.UnmatchedInstanceElements = elementsToMatch;

            return result;
        }

        private static List<ElementDefinitionNavigator> harvestDefinitionNames(ElementDefinitionNavigator nav)
        {
            Bookmark bm = nav.Bookmark();
            var definitionElements = new List<ElementDefinitionNavigator>();

            try
            {
                if (nav.MoveToFirstChild())
                {
                    do
                    {
                        // If a name appears twice, it's a slice child, and we can skip it: we will just
                        // match an instance element to a single definition element, which is the slicing entry
                        // if we're dealing with a slice
                        if (nav.PathName != definitionElements.Last().PathName)
                            definitionElements.Add(nav.ShallowCopy());
                    } while (nav.MoveToNext());
                }
            }
            finally
            {
                nav.ReturnToBookmark(bm);
            }

            return definitionElements;
        }

        public static bool NameMatches(ElementDefinitionNavigator definition, IElementNavigator instance)
        {
            var name = definition.PathName;
            
            // simple direct match
            if (name == instance.Name) return true;   

            // match where definition path includes a type suffix (typeslice shorthand)
            // example: path Patient.deceasedBoolean matches Patient.deceased (with type 'boolean')
            if (name == instance.Name + instance.TypeName.Capitalize()) return true;

            // match where definition path is a choice (suffix '[x]'), in this case
            // match the path without the suffix against the name
            if(name.EndsWith("[x]"))
            {
                if (name.Substring(0, name.Length - 3) == instance.Name) return true;
            }

            return false;
        }
    }

    internal class MatchResult
    {
        public List<Match> Matches;
        public List<IElementNavigator> UnmatchedInstanceElements;
    }

    internal class Match
    {
        public ElementDefinitionNavigator Definition;
        public List<IElementNavigator> InstanceElements = new List<IElementNavigator>();
    }
}