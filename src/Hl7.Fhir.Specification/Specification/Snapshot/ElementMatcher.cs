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
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
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
            /// <summary>Represents an element in the base profile.</summary>
            public Bookmark BaseBookmark;
            /// <summary>Represents a matching element in the differential.</summary>
            public Bookmark DiffBookmark;
            /// <summary>Indicates how to handle this match: Merge | Add | Slice</summary>
            public MatchAction Action;
        }


        public enum MatchAction
        {
            /// <summary>Merge the elementdefinition in snap with the diff</summary>
            Merge,
            /// <summary>Add the elementdefinition to slice (with diff merged to the slicing entry base definition)</summary>
            Add,
            /// <summary>Begin a new slice with this slice as slicing entry</summary>
            Slice
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
            if (!snapNav.HasChildren) throw Error.Argument("snapNav", "Cannot match base to diff: element '{0}' in snap has no children".FormatWith(snapNav.Path));
            if (!diffNav.HasChildren) throw Error.Argument("diffNav", "Cannot match base to diff: element '{0}' in diff has no children".FormatWith(diffNav.Path));

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
                        var matchingChoice = choiceNames.SingleOrDefault(prefix => NamedNavigation.IsRenamedChoiceElement(prefix, typeSliceShorthand));

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

        /// <summary>
        /// Creates matches between the elements pointed to by snapNav and diffNav. After returning, both
        /// navs will be located on the last element that was matched (e.g. in a slicing group)
        /// </summary>
        /// <param name="snapNav"></param>
        /// <param name="diffNav"></param>
        /// <returns></returns>
        private static List<MatchInfo> constructMatch(ElementNavigator snapNav, ElementNavigator diffNav)
        {
            // [WMR 20160802] snapNav and diffNav point to matching elements
            // Determine the associated action (Add, Merge, Slice)
            // If this represents a slice, then also process all the slice elements
            // Note that in case of a slice, both snapNav and diffNav will always point to the first element in the slice group

            var match = new MatchInfo() { BaseBookmark = snapNav.Bookmark(), DiffBookmark = diffNav.Bookmark() };

            bool baseIsSliced = snapNav.Current.Slicing != null;

            // [WMR 20160801] Only emit slicing entry for actual extension elements (with extension profile url)
            // Do not emit slicing entry for abstract Extension base element definition (inherited from external profiles)
            // bool diffIsExtension = diffNav.Current.IsExtension();
            bool diffIsExtension = diffNav.Current.IsMappedExtension();
            var nextDiffChildName = nextChildName(diffNav);
            bool diffIsSliced = diffIsExtension || nextDiffChildName == diffNav.PathName;
            bool diffIsTypeSlice = snapNav.Current.IsChoice() && snapNav.IsCandidateTypeSlice(diffNav.PathName);

            if (diffIsTypeSlice)
            {
                if (baseIsSliced)
                {
                    // TODO...?
                    throw Error.NotSupported("Cannot expand snapshot. Reslicing type slices is not yet supported (path = '{0}').", diffNav.Path);
                }

                // Only a single type slice? Then merge
                // e.g. base:value[x] <=> diff:valueString
                if (!snapNav.IsCandidateTypeSlice(nextDiffChildName))
                {
                    match.Action = MatchAction.Merge;
                    return new List<MatchInfo>() { match };
                }

                // Multiple type slices
                // e.g. base:value[x] <=> diff:valueString + diff:valueBoolean
                return constructTypeSliceMatch(snapNav, diffNav);
            }
            else if (baseIsSliced || diffIsSliced)
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

            // For the first entries with explicit slicing information (or implicit if this is an extension),
            // generate a match between the base's unsliced element and the first entry in the diff

            bool isExtension = diffNav.Current.IsExtension();
            bool diffIsSliced = diffNav.Current.Slicing != null;
            if (diffIsSliced || isExtension)
                {
                // Differential has information for the slicing entry
                result.Add(new MatchInfo()
                {
                    BaseBookmark = bm,
                    DiffBookmark = diffNav.Bookmark(),
                    Action = MatchAction.Slice
                });

                // Skip any existing slicing entry in the differential; below we process the actual slices
                if (diffIsSliced)
                {
                    // If the differential contains a slicing entry, then it should also define at least a single slice.
                    if (!diffNav.MoveToNext())
                        throw Error.InvalidOperation("Differential has a slicing entry for path '{0}', but no first actual slice", diffNav.Path);
                }
            }

            // Then, generate a match between the base element(s) and the slicing entries in the diff
            // Note that the first entry may serve a double role and have to result matches (one for the constraints, one as a slicing entry)
            do
            {
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
                    Bookmark matchingSlice;
                    if (FindBaseSlice(snapNav, diffNav, out matchingSlice))
                    {
                        result.Add(new MatchInfo()
                        {
                            BaseBookmark = matchingSlice,
                            DiffBookmark = diffNav.Bookmark(),
                            Action = MatchAction.Merge,
                        });
                    }
                    else
                    {
                        result.Add(new MatchInfo()
                        {
                            // BaseBookmark = bm,
                            BaseBookmark = matchingSlice,
                            DiffBookmark = diffNav.Bookmark(),
                            Action = MatchAction.Add
                        });
                    }
                }
            } while (diffNav.MoveToNext(diffName));

            return result;
        }

        // [WMR 20160801] NEW
        // Try to find matching slice element in base profile
        // Assume snapNav is positioned on slicing entry node
        // Assume diffNav is positioned on a resliced element node
        // Returns true when match is found, matchingSlice points to match in base (merge here)
        // Returns false otherwise, matchingSlice points to current node in base
        // Maintain snapNav current position
        private static bool FindBaseSlice(ElementNavigator snapNav, ElementNavigator diffNav, out Bookmark matchingSlice)
        {
            var slicing = snapNav.Current.Slicing;
            Debug.Assert(slicing != null);

            var slicingIntro = matchingSlice = snapNav.Bookmark();
            var result = false;

            // url, type@profile, @type + @profile
            if (IsTypeProfileDiscriminator(slicing.Discriminator))
            {
                // [WMR 20160802] Handle complex extension constraints
                // e.g. sdc-questionnaire, Path = 'Questionnaire.group.question.extension.extension', name = 'question'
                // Type.Profile = 'http://hl7.org/fhir/StructureDefinition/questionnaire-enableWhen#question'
                // snapNav has already expanded target extension definition 'questionnaire-enableWhen'
                // => Match to base profile on child element with name 'question'

                var diffProfiles = diffNav.Current.Type.FirstOrDefault().Profile.ToArray();
                if (diffProfiles == null || diffProfiles.Length == 0)
                {
                    throw Error.InvalidOperation("Differential is reslicing on url, but resliced element has no type profile (path = '{0}').", diffNav.Path);
                }
                if (diffProfiles.Length > 1)
                {
                    throw Error.NotSupported("Cannot expand snapshot. Reslicing on complex discriminator is not supported (path = '{0}').", diffNav.Path);
                }

                var diffProfile = diffProfiles.FirstOrDefault();
                string profileUrl, elementName;
                var isComplex = SnapshotGenerator.IsComplexProfileReference(diffProfile, out profileUrl, out elementName);
                while (snapNav.MoveToNext(snapNav.PathName))
                {
                    var baseProfiles = snapNav.Current.Type.FirstOrDefault().Profile;
                    result = isComplex
                        // Match on element name
                        ? snapNav.Current.Name == elementName
                        // Match on profile(s)
                        : baseProfiles.SequenceEqual(diffProfiles);
                    if (result)
                    {
                        matchingSlice = snapNav.Bookmark();
                        break;
                    }
                }

            }
            // TODO: Support other discriminators
            else
            {
                throw Error.NotSupported("Cannot expand snapshot. Reslicing on discriminator '{0}' is not supported yet (path = '{1}').", string.Join("|", slicing.Discriminator), snapNav.Path);
            }

            snapNav.ReturnToBookmark(slicingIntro);

            return result;
        }

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

            Debug.Assert(snapNav.Current.Slicing == null);

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
            } while (diffNav.MoveToNextTypeSlice(snapNav.PathName));

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
                if (snapNav.Current.IsChoice())
                {
                    result.Add(snapNav.PathName);
                }
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

    }
}
