/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;
using System.Diagnostics;

namespace Hl7.Fhir.Specification.Snapshot
{
    internal static class MatchPrinter
    {
        // [WMR 20160719] Add conditional compilation attribute
        [Conditional("DEBUG")]
        public static void DumpMatches(this IEnumerable<ElementMatcher.MatchInfo> matches, ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav)
        {
            var sbm = snapNav.Bookmark();
            var dbm = diffNav.Bookmark();

            foreach(var match in matches)
            {
                if (!snapNav.ReturnToBookmark(match.BaseBookmark) || !diffNav.ReturnToBookmark(match.DiffBookmark))
                {
                    throw Error.InvalidOperation("Found unreachable bookmark in matches");
                }

                var bPos = snapNav.Path + $"[{snapNav.OrdinalPosition}]";
                var dPos = diffNav.Path + $"[{diffNav.OrdinalPosition}]";

                // [WMR 20160719] Add name, if not null
                if (snapNav.Current != null && snapNav.Current.Name != null) bPos += $" '{snapNav.Current.Name}'";
                if (diffNav.Current != null && diffNav.Current.Name != null) dPos += $" '{diffNav.Current.Name}'";

                Debug.WriteLine($"B:{bPos} <--{match.Action.ToString()}--> D:{dPos}");
            }

            snapNav.ReturnToBookmark(sbm);
            diffNav.ReturnToBookmark(dbm);
        }
    }


    internal static class ElementMatcher
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
            Slice,
            /// <summary>Introduce a new element (for core resource and datatype definitions).</summary>
            New
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
        public static List<MatchInfo> Match(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav)
        {
            // if (!snapNav.HasChildren) throw Error.Argument(nameof(snapNav), $"Cannot match base to diff: element '{snapNav.Path}' in snap has no children");
            if (!diffNav.HasChildren) throw Error.Argument(nameof(diffNav), $"Cannot match base to diff: element '{diffNav.Path}' in diff has no children");

            // These bookmarks are used only in the finally {} to make sure we don't alter the position of the navs when leaving the merger
            var baseStartBM = snapNav.Bookmark();
            var diffStartBM = diffNav.Bookmark();

            snapNav.MoveToFirstChild();
            diffNav.MoveToFirstChild();

            var choiceNames = listChoiceElements(snapNav);

            var result = new List<MatchInfo>();

            // [WMR 20160906] DEBUG: http://example.com/fhir/StructureDefinition/patient-research-authorization
            //if (diffNav.Elements.Count == 11
            //    && snapNav.Elements.Count == 5
            //    && diffNav.Elements[1].Path == "Extension.url"
            //    && snapNav.Elements[1].Path == "Extension.id")
            //{
            //    Debug.Fail("");
            //}

            try
            {
                do
                {
                    bool isNewElement = false;

                    // First, match directly -> try to find the child in base with the same name as the path in the diff
                    if (snapNav.PathName != diffNav.PathName && !snapNav.MoveToNext(diffNav.PathName))
                    {
                        // Not found, maybe this is a type slice shorthand, look if we have a matching choice prefix in snap
                        var typeSliceShorthand = diffNav.PathName;

                        // Try to match nameXXXXX to name[x]
                        var matchingChoice = choiceNames.SingleOrDefault(xName => ElementDefinitionNavigator.IsRenamedChoiceElement(xName, typeSliceShorthand));

                        if (matchingChoice != null)
                        {
                            snapNav.MoveToNext(matchingChoice);
                        }
                        else
                        {
                            // No match; consider this to be a new element definition
                            // This is allowed for core resource & datatype definitions
                            // Note that the SnapshotGenerator does not verify correctness; that is the responsibility of the Validator!
                            // SnapshotGenerator should never throw, unless there is faulty logic
                            // Instead, emit a list of OperationDefinitions to describe issues (TODO)
                            // Ewout: also annotate ElementDefinitions with associated OperationDefinitions
                            // Validator is responsible for verifying correctness
                            isNewElement = true;
                        }
                    }
                    if (isNewElement)
                    {
                        // Note: this loop consumes all new diffNav elements when processing the first element from snapNav
                        // When Match is called for remaining snapNav (base) elements, all new diffNav elements will already have been merged
                        result.Add(constructNew(snapNav, diffNav));
                    }
                    else
                    {
                        result.AddRange(constructMatch(snapNav, diffNav));
                    }
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
        static List<MatchInfo> constructMatch(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav)
        {
            // [WMR 20160802] snapNav and diffNav point to matching elements
            // Determine the associated action (Add, Merge, Slice)
            // If this represents a slice, then also process all the slice elements
            // Note that in case of a slice, both snapNav and diffNav will always point to the first element in the slice group

            var match = new MatchInfo() { BaseBookmark = snapNav.Bookmark(), DiffBookmark = diffNav.Bookmark() };

            bool baseIsSliced = snapNav.Current.Slicing != null;

            // [WMR 20160801] Only emit slicing entry for actual extension elements (with extension profile url)
            // Do not emit slicing entry for abstract Extension base element definition (inherited from external profiles)
            // [WMR 20160906] WRONG! Also need to handle complex extensions
            // bool diffIsExtension = diffNav.Current.IsExtension();
            bool diffIsExtension = // diffNav.Current.IsMappedExtension()
                diffNav.Current.IsExtension() &&
                (
                    diffNav.Current.PrimaryTypeProfile() != null                            // Extension element in a profile
                    || ElementDefinitionNavigator.GetPathRoot(diffNav.Path) == "Extension"  // Complex extension child element
                );


            var nextDiffChildName = nextChildName(diffNav);
            bool diffIsSliced = diffIsExtension || nextDiffChildName == diffNav.PathName;
            bool diffIsTypeSlice = snapNav.Current.IsChoice() && snapNav.IsCandidateTypeSlice(diffNav.PathName);

            if (diffIsTypeSlice)
            {
                if (baseIsSliced)
                {
                    // TODO...?
                    throw Error.NotSupported("Reslicing of type slices is not supported (path = '{0}').", diffNav.Path);
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

        static List<MatchInfo> constructSliceMatch(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav)
        {
            var result = new List<MatchInfo>();

            var bm = snapNav.Bookmark();
            var diffName = diffNav.PathName;
            bool baseIsSliced = snapNav.Current.Slicing != null;

            // For the first entries with explicit slicing information (or implicit if this is an extension),
            // generate a match between the base's unsliced element and the first entry in the diff

            bool isExtension = diffNav.Current.IsExtension();
            bool diffIsSliced = diffNav.Current.Slicing != null;

            // Handle constraints on inherited sliced elements in base profile (incl. reslicing)
            // If the differential element represents a named slice, then try to position onto the matching base slice
            // If match, then merge slice introduction and append new slice entries after the matching base slice
            // Otherwise the diff introduces a new slice
            var diffSliceName = diffNav.Current.Name;
            if (baseIsSliced && diffSliceName != null)
            {
                // Constraints on existing slice in base? Then merge
                if (snapNav.MoveToNextSlice(diffSliceName))
                {
                    // Add any re-slices after the associated base slice
                    bm = snapNav.Bookmark();
                }
            }

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
                    {
                        throw Error.InvalidOperation($"Differential has a slicing entry for path '{diffNav.Path}', but no first actual slice");
                    }
                }
            }

            // Then, generate a match between the base element(s) and the slicing entries in the diff
            // Note that the first entry may serve a double role and have to result matches (one for the constraints, one as a slicing entry)
            do
            {
                Bookmark matchingSlice;
                if (baseIsSliced && findBaseSlice(snapNav, diffNav, out matchingSlice))
                {
                    result.Add(new MatchInfo()
                    {
                        BaseBookmark = matchingSlice,   // Merge with matching base slice
                        DiffBookmark = diffNav.Bookmark(),
                        Action = MatchAction.Merge,
                    });
                }
                else
                {
                    result.Add(new MatchInfo()
                    {
                        // - New slice: merge with default (unsliced) element definition
                        // - Reslice: merge with associated base slice
                        BaseBookmark = bm,              
                        DiffBookmark = diffNav.Bookmark(),
                        Action = MatchAction.Add
                    });
                }

                // TODO: Handle re-slicing child diff constraints
                // e.g. patient-careprovider-type-reslice, organizationCare/teamCare
                // => Must process while matching base slice "organizationCare"

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
        static bool findBaseSlice(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav, out Bookmark matchingSlice)
        {
            var result = false;

            var bm = snapNav.Bookmark();
            matchingSlice = Bookmark.Empty;

            // If the diff slice has a name, than match base slice by name
            var diffSliceName = diffNav.Current.Name;
            if (!string.IsNullOrEmpty(diffSliceName))
            {
                result = snapNav.MoveToNextSlice(diffSliceName);
                if (result)
                {
                    matchingSlice = snapNav.Bookmark();
                    snapNav.ReturnToBookmark(bm);
                }
                // No match; this represents a new slice
                return result;
            }

            // Slice has no name
            // This is expected for extensions => slice by url
            Debug.WriteLineIf(!diffNav.Current.IsExtension(), $"Warning! Unnamed slice for path = '{diffNav.Path}'");
            Debug.Assert(diffNav.Current.IsExtension());

            // Try to match base slice by discriminator
            // Q: Is this still necessary? e.g. extensions

            var slicing = snapNav.Current.Slicing;
            Debug.Assert(slicing != null);

            var slicingIntro = matchingSlice = snapNav.Bookmark();

            // url, type@profile, @type + @profile
            if (isTypeProfileDiscriminator(slicing.Discriminator))
            {
                // [WMR 20160802] Handle complex extension constraints
                // e.g. sdc-questionnaire, Path = 'Questionnaire.group.question.extension.extension', name = 'question'
                // Type.Profile = 'http://hl7.org/fhir/StructureDefinition/questionnaire-enableWhen#question'
                // snapNav has already expanded target extension definition 'questionnaire-enableWhen'
                // => Match to base profile on child element with name 'question'

                var diffProfiles = diffNav.Current.PrimaryTypeProfiles().ToArray();
                // Handle Chris Grenz example http://example.com/fhir/SD/patient-research-auth-reslice
                if (diffProfiles == null || diffProfiles.Length == 0)
                {
                    throw Error.InvalidOperation($"Differential is reslicing on url, but resliced element has no type profile (path = '{diffNav.Path}').");
                }
                if (diffProfiles != null && diffProfiles.Length > 1)
                {
                    throw Error.NotSupported($"Reslicing on complex discriminator is not supported (path = '{diffNav.Path}').");
                }


                var diffProfile = diffProfiles.FirstOrDefault();
                var profileRef = ProfileReference.FromUrl(diffProfile);
                while (snapNav.MoveToNext(snapNav.PathName))
                {
                    var baseProfiles = snapNav.Current.PrimaryTypeProfiles().ToArray();
                    result = profileRef.IsComplex
                        // Match on element name
                        ? snapNav.Current.Name == profileRef.ElementName
                        // Match on type profile(s)
                        : baseProfiles.SequenceEqual(diffProfiles);
                    if (result)
                    {
                        matchingSlice = snapNav.Bookmark();
                        break;
                    }
                }

            }

            // TODO: Support other discriminators
            // http://hl7.org/fhir/profiling.html#discriminator

            else
            {
                throw Error.NotSupported($"Reslicing on discriminator '{string.Join("|", slicing.Discriminator)}' is not supported. Path = '{snapNav.Path}'.");
            }

            snapNav.ReturnToBookmark(slicingIntro);
            return result;
        }

        // [WMR 20160902] Represents a new element definition with no matching base element (for core resource & datatype definitions)
        static MatchInfo constructNew(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav)
        {
            // Called by Match when the current diffNav does not match any following sibling of snapNav (base)
            // This happens when merging a core definition (e.g. Patient) with a base type (e.g. Resource)
            // Return reference to *parent* element as BaseBookmark!
            // Exception: when snapNav = {} | {Resource} e.g. Resource & Element
            var bm = snapNav.Bookmark();
            if (!string.IsNullOrEmpty(snapNav.ParentPath))
            {
                snapNav.MoveToParent();
            }
            var match = new MatchInfo() { BaseBookmark = snapNav.Bookmark(), DiffBookmark = diffNav.Bookmark(), Action = MatchAction.New };
            snapNav.ReturnToBookmark(bm);
            return match;
        }

        static bool isProfileDiscriminator(string discriminator) => discriminator == "@profile";
        static bool isTypeDiscriminator(string discriminator) => discriminator == "@type";
        static bool isTypeAndProfileDiscriminator(string discriminator) => discriminator == "type@profile";
        static bool isUrlDiscriminator(string discriminator) => discriminator == "url";

        // [WMR 20160801]
        // Determine if the specified discriminator(s) match on type/profile
        static bool isTypeProfileDiscriminator(IEnumerable<string> discriminators)
        {
            if (discriminators != null)
            {
                var ar = discriminators.ToArray();
                if (ar.Length == 1)
                {
                    return isUrlDiscriminator(ar[0]) || isTypeAndProfileDiscriminator(ar[0]) || isProfileDiscriminator(ar[0]);
                }
                else if (ar.Length == 2)
                {
                    return (isProfileDiscriminator(ar[0]) && isTypeDiscriminator(ar[1]))
                        || (isProfileDiscriminator(ar[1]) && isTypeDiscriminator(ar[0]));
                }
            }
            return false;
        }

        // [WMR 20160720] NEW
        // Handle type slices
        // Difference with regular slices:
        // - Don't need to handle extensions
        // - Match renamed elements, i.e. value[x] => valueBoolean
        private static List<MatchInfo> constructTypeSliceMatch(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav)
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
                {
                    throw Error.InvalidOperation($"Differential has a slicing entry {diffNav.Path}, but no first actual slice");
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
            } while (diffNav.MoveToNextTypeSlice(snapNav.PathName));

            return result;
        }

        /// <summary>
        /// List all names of nodes in the current navigator that are choice ('[x]') elements
        /// </summary>
        /// <param name="snapNav"></param>
        /// <returns></returns>
        private static List<string> listChoiceElements(ElementDefinitionNavigator snapNav)
        {
            var bm = snapNav.Bookmark();
            var result = new List<string>();

            do
            {
                if (snapNav.Current != null && snapNav.Current.IsChoice())
                {
                    result.Add(snapNav.PathName);
                }
            } while (snapNav.MoveToNext());

            snapNav.ReturnToBookmark(bm);

            return result;
        }

        private static string nextChildName(ElementDefinitionNavigator nav)
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
