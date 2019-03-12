/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Validation
{

    internal class ChildNameMatcher
    {
        public static MatchResult Match(ElementDefinitionNavigator definitionParent, ITypedElement instanceParent)
        {
            var definitionElements = harvestDefinitionNames(definitionParent);
            var elementsToMatch = instanceParent.Children().Cast<ScopedNode>().ToList();

            List<Match> matches = new List<Match>();

            foreach(var definitionElement in definitionElements)
            {
                var match = new Match() { Definition = definitionElement, InstanceElements = new List<ITypedElement>() };

                // Special case is the .value of a primitive fhir type, this is represented
                // as the "Value" of the IValueProvider interface, not as a real child
                if (definitionElement.Current.IsPrimitiveValueConstraint())
                {
                    if (instanceParent.Value != null)
                        match.InstanceElements.Add( instanceParent );
                }
                else
                {
                    var definitionPath = ProfileNavigationExtensions.GetNameFromPath(definitionElement.Current?.Base?.Path ?? definitionElement.Path);
                    var found = elementsToMatch.Where(ie => NameMatches(definitionPath, ie)).ToList();

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
                        if (!definitionElements.Any() || definitionElements.Last().PathName != nav.PathName)
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

        public static bool NameMatches(string name, ITypedElement instance)
        {
            var definedName = name;
            // simple direct match
            if (definedName == instance.Name) return true;   

            // match where definition path includes a type suffix (typeslice shorthand)
            // example: path Patient.deceasedBoolean matches Patient.deceased (with type 'boolean')
            if (definedName == instance.Name + instance.InstanceType.Capitalize()) return true;

            // match where definition path is a choice (suffix '[x]'), in this case
            // match the path without the suffix against the name
            if(definedName.EndsWith("[x]"))
            {
                if (definedName.Substring(0, definedName.Length - 3) == instance.Name) return true;
            }

            return false;
        }
    }

    internal class MatchResult
    {
        public List<Match> Matches;
        public List<ScopedNode> UnmatchedInstanceElements;
    }

    [System.Diagnostics.DebuggerDisplay(@"\{{Definition.DebuggerDisplay,nq}}")] // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
    internal class Match
    {
        public ElementDefinitionNavigator Definition;
        public List<ITypedElement> InstanceElements;
    }
}