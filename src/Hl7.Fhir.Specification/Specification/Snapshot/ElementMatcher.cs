/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Support;


namespace Hl7.Fhir.Specification.Snapshot
{

    internal class ElementMatcher
    {
        public struct MatchInfo
        {
            public Bookmark BaseBookmark;
            public Bookmark DiffBookmark;
            public Matchkind Kind;
        }

        public enum Matchkind
        {
            Direct,         // a match between a single element in diff and base or between a single slice in diff and base
            SliceAll,       // a match where the diff introduces a slice with new constraints for all slices
            NewSlice,       // a match where the diff introduces a new slice (or first slice) to an element in the base
            Reslice,        // a match where the diff slices a single slice in the base
            SingleTypeSlice,// a match where the diff does a type slice for a single type (nameXXXX where XXX is the type for a choice)
            ExtensionSlice  // a match where the diff introduces a new extension
        }

        /// <summary>
        /// Will match up the children of the current element in diffNav to the children of the element in snapNav.
        /// </summary>
        /// <param name="snapNav"></param>
        /// <param name="diffNav"></param>
        /// <returns>Returns a list of Bookmark combinations, the first bookmark pointing to an element in the base,
        /// the second a bookmark in the diff that matches the bookmark in the base.</returns>
        /// <remarks>Will match slices to base elements, re-sliced slices to slices and type-slice shorthands to choie elements.
        /// Note that this function may expand snapNav when it encounters paths in the differential that move into the complex types
        /// of one of snap's elements.  (NO NEED, it just has to match direct children, not deeper)
        /// This function assumes the differential is not sparse: it must have parent nodes for all child constraint paths.
        /// </remarks>
        public List<MatchInfo> Match(ElementNavigator snapNav, ElementNavigator diffNav)
        {
            if (!snapNav.HasChildren) throw Error.Argument("snapNav", "Cannot match base to diff: element {0} in snap has no children".FormatWith(snapNav.PathName));
            if (!diffNav.HasChildren) throw Error.Argument("diffNav", "Cannot match base to diff: element {0} diff has no children".FormatWith(diffNav.PathName));

            // These bookmarks are used only in the finally {} to make sure we don't alter the position of the navs when leaving the merger
            var baseStartBM = snapNav.Bookmark();
            var diffStartBM = diffNav.Bookmark();

            snapNav.MoveToFirstChild();
            diffNav.MoveToFirstChild();

            var choiceNames = listChoiceElements(snapNav);

            var result = new List<MatchInfo>();

            try
            {
                do
                {
                    // First, match directly -> try to find the child in base with the same name as the path in the diff
                    if (!snapNav.MoveToNext(diffNav.PathName))
                    {
                        // Not found, maybe this is a type slice shorthand, look if we have a matching choice prefix in snap
                        var typeSliceShorthand = diffNav.PathName;

                        // Try to match nameXXXXX to name[x]
                        var matchingChoice = choiceNames.SingleOrDefault(prefix => isPossibleTypeSlice(prefix, typeSliceShorthand));
                        
                        if (matchingChoice != null)
                            snapNav.MoveToNext(matchingChoice);
                        else
                            throw Error.InvalidOperation("Differential has a constraint for path '{0}', which does not exist in its base".FormatWith(diffNav.Path));
                    }

                    result.AddRange(constructMatch(snapNav, diffNav));
                }
                while (diffNav.MoveToNext());
            }
            finally
            {
                snapNav.ReturnToBookmark(baseStartBM);
                diffNav.ReturnToBookmark(diffStartBM);
            }

            return result;
        }

        // Try to match nameXXXXX to name[x]
        private static bool isPossibleTypeSlice(string baseName, string diffName)
        {
            return String.Compare(baseName, 0, diffName, 0, baseName.Length - 3) == 0;
        }

        private static List<MatchInfo> constructMatch(ElementNavigator snapNav, ElementNavigator diffNav)
        {
            var match = new MatchInfo() { BaseBookmark = snapNav.Bookmark(), DiffBookmark = diffNav.Bookmark() };

            bool baseIsSliced = snapNav.Current.Slicing != null;
            bool diffIsSliced = diffNav.Current.IsExtension() || nextChildName(diffNav) == diffNav.PathName;

            // Easiest case - one to one match, without slicing involved
            if(!baseIsSliced && !diffIsSliced)
            {
                match.Kind = Matchkind.Direct;
                return new List<MatchInfo>() { match };
            }

            // Check whether this is a type-slice shorthand - only the most common usecase
            // is supported for this case, otherwise us a normal type-slice
            // See also gForge issues #8974 and #8973
            if (isPossibleTypeSlice(snapNav.PathName,diffNav.PathName))
            {
                if (baseIsSliced) throw Error.NotSupported("Using a slicing shorthand ({0}) is not supported for re-slicing".FormatWith(diffNav.Path));
                if (diffIsSliced) throw Error.NotSupported("Using a slicing shorthand ({0}) can only be used to introduce a single slice".FormatWith(diffNav.Path));

                match.Kind = Matchkind.SingleTypeSlice;
                return new List<MatchInfo>() { match };
            }

            // Else, this is a slice match - process it separately
            return constructSliceMatch(snapNav, diffNav);
        }

        private static List<MatchInfo> constructSliceMatch(ElementNavigator snapNav, ElementNavigator diffNav)
        {
            var result = new List<MatchInfo>();

            // The diff has a matching slice without a name
            //if (String.IsNullOrEmpty(diffNav.Current.Name))

           return result;
        }

        /// <summary>
        /// List all names of nodes in the current navigator that are choice ('[x]') elements
        /// </summary>
        /// <param name="snapNav"></param>
        /// <returns></returns>
        private List<string> listChoiceElements(ElementNavigator snapNav)
        {
            var bm = snapNav.Bookmark();
            var result = new List<string>();

            do
            {
                if (snapNav.Current.IsChoice()) result.Add(snapNav.PathName);
            } while (snapNav.MoveToNext());

            snapNav.ReturnToBookmark(bm);

            return result;
        }

        private static string nextChildName(ElementNavigator nav)
        {
            string result = null;

            if(nav.MoveToNext())
            {
                result = nav.PathName;
                nav.MoveToPrevious();
            }

            return result;
        }

        public List<Tuple<ElementDefinition, ElementDefinition>> Match(IList<ElementDefinition> baseElems, IList<ElementDefinition> diffElems)
        {
            throw new NotImplementedException();
        }
    }
}
