// [WMR 201608]1 NEW Support sliced base profile
#define RESLICING

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
        // [WMR 20160719] Add conditional compilation attribute
        [Conditional("DEBUG")]
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

                // [WMR 20160719] Add name, if not null
                if (snapNav.Current.Name != null) bPos += " '{0}'".FormatWith(snapNav.Current.Name);
                if (diffNav.Current.Name != null) dPos += " '{0}'".FormatWith(diffNav.Current.Name);

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
            
            public Bookmark BaseBookmark;   // Represents a base element to add/merge
            public Bookmark DiffBookmark;   // Represents a diff element to add/merge
            public MatchAction Action;      // Add | Merge | Slice
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
        /// <remarks>Will match slices to base elements, re-sliced slices to slices and type-slice shorthands to choice elements.
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
            return baseName != null
                && diffName != null
                && baseName.EndsWith("[x]")
                && String.Compare(baseName, 0, diffName, 0, baseName.Length - 3) == 0 && diffName.Length > baseName.Length;
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

            // [WMR 20160719] WORK IN PROGRESS
            // TODO: test sliced base

            bool baseIsSliced = snapNav.Current.Slicing != null;
            var nextDiffChildName = nextChildName(diffNav);
            bool diffIsExtension = diffNav.Current.IsExtension();
            bool diffIsSliced = diffIsExtension || nextDiffChildName == diffNav.PathName;

            // [WMR 20160720] New logic

            // TODO: Support sliced base profile
#if !RESLICING
            if (baseIsSliced)
            {
                throw Error.NotSupported("Snapshot expansion does not yet support sliced base profiles.");
            }
#endif

            bool diffIsTypeSlice = snapNav.Current.IsChoice() && isPossibleTypeSlice(snapNav.PathName, diffNav.PathName);

            if (diffIsTypeSlice)
            {
#if RESLICING
                if (baseIsSliced)
                {
                    // TODO
                    throw Error.NotSupported("Snapshot expansion does not yet support reslicing type slices, path = '{0}'.", diffNav.Path);
                }
#endif

                // Only a single type slice? Then merge
                // e.g. base:value[x] <=> diff:valueString
                if (!isPossibleTypeSlice(snapNav.PathName, nextDiffChildName))
                {
                    match.Action = MatchAction.Merge;
                    return new List<MatchInfo>() { match };
                }

                // Multiple type slices
                // e.g. base:value[x] <=> diff:valueString + diff:valueBoolean
                return constructTypeSliceMatch(snapNav, diffNav);
            }
#if RESLICING
            else if (baseIsSliced || diffIsSliced)
#else
            else if (diffIsSliced)
#endif
            {
                // This is a slice match - process it separately
                return constructSliceMatch(snapNav, diffNav);
            }
            else
            {
                // Easiest case - one to one match, without slicing involved
                match.Action = MatchAction.Merge;
                return new List<MatchInfo>() { match };
            }

        }

        private static List<MatchInfo> constructSliceMatch(ElementNavigator snapNav, ElementNavigator diffNav)
        {
            var result = new List<MatchInfo>();

            var bm = snapNav.Bookmark();
            var diffName = diffNav.PathName;
            bool baseIsSliced = snapNav.Current.Slicing != null;

#if !RESLICING
            if (baseIsSliced)
            {
                throw Error.NotSupported("Cannot yet handle re-slicing found at diff {0}".FormatWith(diffNav.Path));
            }
#endif

            // For the first entries with explicit slicing information (or implicit if this is an extension),
            // generate a match between the base's unsliced element and the first entry in the diff
            if (diffNav.Current.Slicing != null || diffNav.Current.IsExtension() )
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
#if RESLICING
                // Find matching slice in base profile
                if (!baseIsSliced)
                {
                    result.Add(new MatchInfo()
                    {
                        BaseBookmark = bm,
                        DiffBookmark = diffNav.Bookmark(),
                        Action = MatchAction.Add
                    });
                }
                else
                {
                    Bookmark matchingOrLastSlice;
                    if (FindBaseSlice(snapNav, diffNav, out matchingOrLastSlice))
                    {
                        result.Add(new MatchInfo()
                        {
                            BaseBookmark = matchingOrLastSlice,
                            DiffBookmark = diffNav.Bookmark(),
                            Action = MatchAction.Merge,
                        });
                    }
                    else
                    {
                        result.Add(new MatchInfo()
                        {
                            // BaseBookmark = bm,
                            BaseBookmark = matchingOrLastSlice,
                            DiffBookmark = diffNav.Bookmark(),
                            Action = MatchAction.Add
                        });
                    }
                }

#else
                result.Add(new MatchInfo()
                {
                    BaseBookmark = bm,
                    DiffBookmark = diffNav.Bookmark(),
                    Action = MatchAction.Add
                });
#endif

            } while (diffNav.MoveToNext(diffName));

            return result;
        }

#if RESLICING
        // [WMR 20160801] NEW
        // Try to find matching slice element in base profile
        // Assume snapNav is positioned on slicing introduction node
        // Assume diffNav is positioned on a resliced element node
        // Returns true when match is found, matchingOrLastSlice points to match in base (merge here)
        // Returns false otherwise, matchingOrLastSlice points to last slice in base (add afterwards)
        // Maintain snapNav current position
        private static bool FindBaseSlice(ElementNavigator snapNav, ElementNavigator diffNav, out Bookmark matchingOrLastSlice)
        {
            var slicing = snapNav.Current.Slicing;
            Debug.Assert(slicing != null);

            var slicingIntro = matchingOrLastSlice = snapNav.Bookmark();
            var result = false;

            // url, type@profile, @type + @profile
            if (IsTypeProfileDiscriminator(slicing.Discriminator))
            {
                // Slice on Element.Type.Profile
                var diffSliceId = diffNav.Current.Type.FirstOrDefault().Profile;
                if (diffSliceId == null)
                {
                    throw Error.NotSupported("Differential is reslicing on url, but resliced element has no type profile, path = '{0}'.", diffNav.Path);
                }


                while (snapNav.MoveToNext(snapNav.PathName))
                {
                    matchingOrLastSlice = snapNav.Bookmark();
                    var baseSliceId = snapNav.Current.Type.FirstOrDefault().Profile;
                    result = baseSliceId.SequenceEqual(diffSliceId);
                    if (result) { break; }
                };
            }
            // TODO: Support other discriminators
            else
            {
                throw Error.NotSupported("Unsupported slicing discriminator '{0}', path = '{1}'.", string.Join("|", slicing.Discriminator), snapNav.Path);
            }

            snapNav.ReturnToBookmark(slicingIntro);

            return result;
        }
#endif

        // [WMR 20160801]
        // Determine if the specified discriminator(s) match on type/profile
        private static bool IsTypeProfileDiscriminator(IEnumerable<string> discriminators)
        {
            if (discriminators != null)
            {
                var ar = discriminators.ToArray();
                if (ar.Length == 1)
                {
                    return ar[0] == "url" || ar[0] == "type@profile";
                }
                else if (ar.Length == 2)
                {
                    return (ar[0] == "@profile" && ar[1] == "@type")
                        || (ar[1] == "@profile" && ar[0] == "@type");
                }
            }
            return false;
        }

        // [WMR 20160720] NEW
        // Handle type slices
        // Difference with regular slices:
        // - Don't need to handle extensions
        // - Match renamed elements, i.e. value[x] => valueBoolean
        private static List<MatchInfo> constructTypeSliceMatch(ElementNavigator snapNav, ElementNavigator diffNav)
        {
            var result = new List<MatchInfo>();

            var bm = snapNav.Bookmark();
            var diffName = diffNav.PathName;
            bool baseIsSliced = snapNav.Current.Slicing != null;

            if (baseIsSliced)
            {
                throw Error.NotSupported("Cannot yet handle re-slicing found at diff {0}".FormatWith(diffNav.Path));
            }

            // For the first entries with explicit slicing information,
            // generate a match between the base's unsliced element and the first entry in the diff
            if (diffNav.Current.Slicing != null)
            {
                // Differential has information for the slicing entry
                result.Add(new MatchInfo()
                {
                    BaseBookmark = bm,
                    DiffBookmark = diffNav.Bookmark(),
                    Action = MatchAction.Slice
                });

                if (!diffNav.MoveToNext())
                    throw Error.InvalidOperation("Differential has a slicing entry {0}, but no first actual slice", diffNav.Path);
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
            } while (moveToNextTypeSlice(diffNav, snapNav.PathName));

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

        // [WMR 20160720] NEW
        private static bool moveToNextTypeSlice(BaseElementNavigator nav, string name)
        {
            var bm = nav.Bookmark();

            while (nav.MoveToNext())
            {
                // if (nav.PathName == name) return true;
                if (isPossibleTypeSlice(name, nav.PathName)) return true;
            }

            nav.ReturnToBookmark(bm);
            return false;
        }

        public List<Tuple<ElementDefinition, ElementDefinition>> Match(IList<ElementDefinition> baseElems, IList<ElementDefinition> diffElems)
        {
            throw new NotImplementedException();
        }
    }
}
