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
using System.Diagnostics;

namespace Hl7.Fhir.Specification.Snapshot
{
    internal static class MatchPrinter
    {
        public static void DumpMatches(this IEnumerable<ElementMatcher.MatchInfo> matches, ElementNavigator snapNav, ElementNavigator diffNav)
        {
            var sbm = snapNav.Bookmark();
            var dbm = diffNav.Bookmark();

            foreach(var match in matches)
            {
                if (!snapNav.ReturnToBookmark(match.BaseBookmark) || !diffNav.ReturnToBookmark(match.DiffBookmark))
                    throw Error.InvalidOperation("Found unreachable bookmark in matches");

                var bPos = snapNav.Path + "[{0}]".FormatWith(snapNav.OrdinalPosition);
                var dPos = diffNav.Path + "[{0}]".FormatWith(diffNav.OrdinalPosition);
                Debug.WriteLine("B:{0} <--{1}--> D:{2}".FormatWith(bPos, match.Action.ToString(), dPos));
            }

            snapNav.ReturnToBookmark(sbm);
            diffNav.ReturnToBookmark(dbm);
        }
    }


    internal class ElementMatcher
    {
        public struct MatchInfo
        {
            public Bookmark BaseBookmark;
            public Bookmark DiffBookmark;
            public MatchAction Action;
        }


        public enum MatchAction
        {
            Merge,          // Merge the elementdefinition in snap with the diff
            Add,            // Add the elementdefinition to slice (with diff merged to the slicing entry base definition)
            Slice           // Begin a new slice with this slice as slicing entry
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
            if (!snapNav.HasChildren) throw Error.Argument("snapNav", "Cannot match base to diff: element '{0}' in snap has no children".FormatWith(snapNav.PathName));
            if (!diffNav.HasChildren) throw Error.Argument("diffNav", "Cannot match base to diff: element '{0}' in diff has no children".FormatWith(diffNav.PathName));

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
                    if (snapNav.PathName != diffNav.PathName && !snapNav.MoveToNext(diffNav.PathName))
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
            return String.Compare(baseName, 0, diffName, 0, baseName.Length - 3) == 0 && diffName.Length > baseName.Length;
        }

        /// <summary>
        /// Creates matches between the elements pointed to by snapNav and diffNav. After returning, both
        /// navs will be located on the last element that was matched (e.g. in a slicing group)
        /// </summary>
        /// <param name="snapNav"></param>
        /// <param name="diffNav"></param>
        /// <returns></returns>
        private static List<MatchInfo> constructMatch(ElementNavigator snapNav, ElementNavigator diffNav)
        {
            var match = new MatchInfo() { BaseBookmark = snapNav.Bookmark(), DiffBookmark = diffNav.Bookmark() };

            bool baseIsSliced = snapNav.Current.Slicing != null;
            bool diffIsSliced = diffNav.Current.IsExtension() || nextChildName(diffNav) == diffNav.PathName;

            // Easiest case - one to one match, without slicing involved
            if (!baseIsSliced && !diffIsSliced)
            {
                match.Action = MatchAction.Merge;

                return new List<MatchInfo>() { match };
            }

            // Check whether this is a type-slice shorthand - only the most common usecase
            // is supported for this case, otherwise us a normal type-slice
            // See also gForge issues #8974 and #8973
            if (isPossibleTypeSlice(snapNav.PathName, diffNav.PathName))
            {
                if (baseIsSliced) throw Error.NotSupported("Using a slicing shorthand ({0}) is not supported for re-slicing".FormatWith(diffNav.Path));
                if (diffIsSliced) throw Error.NotSupported("Using a slicing shorthand ({0}) can only be used to introduce a single slice".FormatWith(diffNav.Path));

                match.Action = MatchAction.Merge;
                return new List<MatchInfo>() { match };
            }

            // Else, this is a slice match - process it separately
            return constructSliceMatch(snapNav, diffNav);
        }


        private void oldcode()
        {
            //do
            //{

            //The diff has a matching slice without a name - this adds new constraints to all slices in the base slice group
            //if (String.IsNullOrEmpty(diffNav.Current.Name))
            //{
            //    snapNav.ReturnToBookmark(bm);
            //    string baseName = snapNav.PathName;

            //    do
            //    {
            //        var match = new MatchInfo()
            //        {
            //            BaseBookmark = snapNav.Bookmark(),
            //            DiffBookmark = diffNav.Bookmark(),
            //            Kind = Matchkind.UpdateSlice,
            //            Action = MatchAction.Merge
            //        };

            //        result.Add(match);
            //    } while (nextChildName(snapNav) == baseName && snapNav.MoveToNext());  // Warning: Subtle use of short-cut evaluation
            //}
            //else
            //{

            //}
            //        } while (nextChildName(diffNav) == diffName && diffNav.MoveToNext());

        }


        private static List<MatchInfo> constructSliceMatch(ElementNavigator snapNav, ElementNavigator diffNav)
        {
            var result = new List<MatchInfo>();

            var bm = snapNav.Bookmark();
            var diffName = diffNav.PathName;
            bool baseIsSliced = snapNav.Current.Slicing != null;
            bool diffIsSliced = diffNav.Current.IsExtension() || nextChildName(diffNav) == diffNav.PathName;

            if (baseIsSliced)
                throw Error.NotSupported("Cannot yet handle re-slicing found at diff {0}".FormatWith(diffNav.Path));

            // For the first entries with explicit slicing information (or implicit if this is an extension),
            // generate a match between the base's unsliced element and the first entry in the diff
            if(diffNav.Current.Slicing != null || diffNav.Current.IsExtension() )
            {
                // Differential has information for the slicing entry
                result.Add(new MatchInfo()
                {
                    BaseBookmark = bm,
                    DiffBookmark = diffNav.Bookmark(),
                    Action = MatchAction.Slice
                });

                if(!diffNav.Current.IsExtension())
                {
                    if (!diffNav.MoveToNext())
                        throw Error.InvalidOperation("Differential has a slicing entry {0}, but no first actual slice", diffNav.Path);
                }
            }

            // Then, generate a match between the base's unsliced element and the slicing entries in the diff
            // Note that the first entry may serve a double role and have to result matches (one for the constraints, one as a slicing entry)
            do
            {
                result.Add(new MatchInfo()
                {
                    BaseBookmark = bm,
                    DiffBookmark = diffNav.Bookmark(),
                    Action = MatchAction.Add
                });
            } while (nextChildName(diffNav) == diffName && diffNav.MoveToNext());  // Warning: Subtle use of short-cut evaluation

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

            if (nav.MoveToNext())
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
