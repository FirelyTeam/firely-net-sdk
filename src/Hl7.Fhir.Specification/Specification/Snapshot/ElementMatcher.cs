/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

// [WMR 20181212] Match type slice on all specified profiles
#define MULTIPLE_SLICE_PROFILES

// [WMR 20190827] Auto-generate slice names for type slices if missing from the diff
// Note: this modifies the Differential component itself!
#define GENERATE_MISSING_TYPE_SLICE_NAMES

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hl7.Fhir.Specification.Snapshot
{
    internal static class ElementMatcher
    {
        /// <summary>Constants that indicate how a match should be processed.</summary>
        public enum MatchAction
        {
            /// <summary>Merge the elementdefinition in snap with the diff</summary>
            Merge,
            /// <summary>Add the elementdefinition to slice (with diff merged to the slicing entry base definition)</summary>
            Add,
            /// <summary>Begin a new slice with this slice as slicing entry</summary>
            Slice,
            /// <summary>Introduce a new element (for core resource and datatype definitions).</summary>
            New,

            // [WMR 20161212] NEW
            /// <summary>The element constraint is invalid and should be discarded.</summary>
            Invalid
        }

        // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
        [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
        public class MatchInfo
        {
            /// <summary>Indicates how to handle this match: Merge | Add | Slice</summary>
            public MatchAction Action;

            /// <summary>Represents an element in the base profile.</summary>
            public Bookmark BaseBookmark;

            /// <summary>Represents a matching element in the differential.</summary>
            public Bookmark DiffBookmark;

            // [WMR 20170306] NEW - For sliced elements, returns a clone of snap.Base
            public ElementDefinitionNavigator SliceBase;

            /// <summary>Returns optional matching error information.</summary>
            public OperationOutcome.IssueComponent Issue;

            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            string DebuggerDisplay => $"B:{BaseBookmark.DebuggerDisplay} <-- {Action} --> D:{DiffBookmark.DebuggerDisplay}";
        }

        /// <summary>Match the children of the current element in diffNav to the children of the current element in snapNav.</summary>
        /// <param name="snapNav">A navigator for the user profile differential.</param>
        /// <param name="diffNav">A navigator for the base profile snapshot.</param>
        /// <returns>
        /// Returns a list of Bookmark combinations, the first bookmark pointing to an element in the base,
        /// the second a bookmark in the diff that matches the bookmark in the base.
        /// </returns>
        /// <remarks>
        /// Will match slices to base elements, re-sliced slices to slices and type-slice shorthands to choice elements.
        /// Note that this function may expand snapNav when it encounters paths in the differential that move into the complex types
        /// of one of snap's elements. (NO NEED, it just has to match direct children, not deeper)
        /// This function assumes the differential is not sparse: it must have parent nodes for all child constraint paths.
        /// </remarks>
        public static List<MatchInfo> Match(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav)
        {
            // if (!snapNav.HasChildren) { throw Error.Argument(nameof(snapNav), $"Cannot match base to diff: element '{snapNav.Path}' in snap has no children"); }
            // if (!diffNav.HasChildren) { throw Error.Argument(nameof(diffNav), $"Cannot match base to diff: element '{diffNav.Path}' in diff has no children"); }
            if (!diffNav.HasChildren) { return new List<MatchInfo>(); } // [WMR 20161208] Gracefully handle missing differential

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
                    var match = matchBase(snapNav, diffNav, choiceNames);
                    if (match)
                    {
                        result.AddRange(constructMatch(snapNav, diffNav));
                    }
                    else
                    {
                        // No matching base element; this is a new element (core resource definitions)
                        // Note: this loop consumes all new diffNav elements when processing the first element from snapNav
                        // When Match is called for remaining snapNav (base) elements, all new diffNav elements will already have been merged
                        result.Add(constructNew(snapNav, diffNav));
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

        /// <summary>Move snapNav to matching base element for diffNav, if it exists. Otherwise diffNav introduces a new element.</summary>
        /// <returns><c>true</c> if snapNav is positioned on matching base element, or <c>false</c> if diffNav introduces a new element.</returns>
        static bool matchBase(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav, List<string> choiceNames)
        {
            var bm = snapNav.Bookmark();

            var diffName = diffNav.PathName;

            // First, match directly -> try to find the child in base with the same name as the path in the diff
            if (SnapshotGenerator.IsEqualName(snapNav.PathName, diffName) || snapNav.MoveToNext(diffName))
            {
                return true;
            }

            // Not found, maybe this is a type slice shorthand, look if we have a matching choice prefix in snap

            // Try to match nameXXXXX to name[x]
            var matchingChoice = choiceNames.SingleOrDefault(xName => ElementDefinitionNavigator.IsRenamedChoiceTypeElement(xName, diffName));

            if (!(matchingChoice is null))
            {
                // [WMR 20170406] Match on current element? Then don't advance to next!
                return SnapshotGenerator.IsEqualName(snapNav.PathName, matchingChoice)
                    || snapNav.MoveToNext(matchingChoice);
            }

            // No match; consider this to be a new element definition
            // This is allowed for core resource & datatype definitions
            // Note that the SnapshotGenerator does not verify correctness; that is the responsibility of the Validator!
            // SnapshotGenerator should never throw, unless there is faulty logic
            // Instead, emit a list of OperationDefinitions to describe issues (TODO)
            // Ewout: also annotate ElementDefinitions with associated OperationDefinitions
            // Validator is responsible for verifying correctness
            return false;
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

            // [WMR 20160801] Only emit slicing entry for actual extension elements (with extension profile url)
            // Do not emit slicing entry for abstract Extension base element definition (inherited from external profiles)
            // [WMR 20160906] WRONG! Also need to handle complex extensions
            var diffIsExtension = diffNav.Current.IsExtension() &&
                (
                    // Extension element in a profile
                    !(diffNav.Current.PrimaryTypeProfile() is null)
                    // Complex extension child element
                    || SnapshotGenerator.IsEqualName(ElementDefinitionNavigator.GetPathRoot(diffNav.Path), EXTENSION_TYPENAME)
                );
            bool baseIsSliced = snapNav.Current.HasSlicingComponent();
            bool diffIsSliced = diffIsExtension || diffNav.Current.HasSlicingComponent();

            var isChoice = diffNav.Current.IsChoice() || snapNav.Current.IsChoice();

            if (baseIsSliced || diffIsSliced)
            {
                // [WMR 20190827] FIXED
                // For renamed type choice elements, continue below
                if (!isChoice && SnapshotGenerator.IsEqualName(diffNav.PathName, snapNav.PathName))
                {
                    // This is a slice match - process it separately
                    return constructSliceMatch(snapNav, diffNav);
                }
            }

            var match = new MatchInfo()
            {
                Action = MatchAction.Merge,
                BaseBookmark = snapNav.Bookmark(),
                DiffBookmark = diffNav.Bookmark()
            };

            // Verify type slice constraints (e.g. value[x] => valueString)
            // - base element has a type choice (value[x])
            // - Single derived element is constrained to single type and renamed (valueString)
            // - This is NOT a type slice
            //   STU3: derived profile cannot contain multiple renamed elements!
            //   R4: derived profile can contain multiple renamed elements
            var result = new List<MatchInfo>() { match };

            //if (snapNav.Current.IsChoice())
            if (isChoice)
            {
                constructChoiceTypeMatch(snapNav, diffNav, result);
            }
            return result;
        }


        // [WMR 20190827] Derive missing (implicit) type slice names
        // Type slices should have a pre-defined sliceName, e.g. "valueString"
        // However if missing (and not slice entry), we can derive slice name from single type constraint
        // Note: only applies to concrete type slices, NOT to the slice entry (with Slicing component)

        // Generate a custom slice name for a type slice (type choice element constrained to a single type)
#if GENERATE_MISSING_TYPE_SLICE_NAMES
        static string SliceNameForTypeSlice(ElementDefinition elem)
        {
            var elemName = elem.GetNameFromPath();
            if (elem.IsChoice() && elem.Type.Count == 1)
            {
                var typeName = elem.Type[0].Code;
                var rename = elemName.Substring(0, elemName.Length - 3) + typeName.Capitalize();
                return rename;
            }
            // Leave SliceName empty if we cannot generate a value
            return null;
        }
#endif

        // Initialize SliceName if:
        // * SliceName is empty
        // * Path ends with "[x]"
        // * Element is constrained to single type
        static string EnsureSliceNameForTypeSlice(ElementDefinition elem, MatchInfo match)
        {
#if GENERATE_MISSING_TYPE_SLICE_NAMES
            if (!elem.HasSlicingComponent()
                && elem.IsChoice()
                && string.IsNullOrEmpty(elem.SliceName))
            {
                // Q: Are we allowed to update the diff itself...?
                // Otherwise, SnapGen class needs to repeat this logic on the snap
                elem.SliceName = SliceNameForTypeSlice(elem);

                // [WMR 2019028] Emit informational output message
                match.Issue = SnapshotGenerator.CreateIssueSliceNameGenerated(elem);
            }
#endif
            return elem.SliceName;
        }

        static List<MatchInfo> constructChoiceTypeMatch(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav, List<MatchInfo> matches)
        {
            var bm = diffNav.Bookmark();

            // [WMR 20170308] NEW - Clone slice base element
            var sliceBase = initSliceBase(snapNav);
            var match = matches[0];

            // [WMR 20190204] NEW
            // STU3: rename "[x]" element in snapshot if constrained to single type
            // R4: Always include original "[x]" element in snapshot, followed by type constraints
            // * Accept multiple renamed elements in diff
            // * Accept diff constraint on "value[x]" element (must be first, before renamed elements)
            // * Match renamed diff element to renamed base element, if exist, otherwise [x] base element

            // local helper methods with access to parent stack

            // Merge or add, starting at current element
            void AddOrMergeCurrentOrNext(string sliceName)
            {
                if (SnapshotGenerator.IsEqualName(snapNav.PathName, sliceName)
                    || SnapshotGenerator.IsEqualName(snapNav.Current.SliceName, sliceName))
                {
                    // OK, Found match
                }
                else
                {
                    AddOrMergeNext(sliceName);
                }
            }

            // Merge or add, starting at next element
            void AddOrMergeNext(string sliceName)
            {
                // R4: try to find matching renamed base element
                var bmSnap = snapNav.Bookmark();
                if (!string.IsNullOrEmpty(sliceName) &&
                        (snapNav.MoveToNext(sliceName)
                        // [WMR 20190827] Match renamed element to named type slice in base
                        // e.g. match "valueString" to "value[x]:valueString"
                        || snapNav.MoveToNextSlice(sliceName))
                    )
                {
                    match.BaseBookmark = snapNav.Bookmark();
                    match.Action = MatchAction.Merge;
                    snapNav.ReturnToBookmark(bmSnap);
                }

                else
                {
                    // Otherwise, match to "[x]" element
                    match.Action = MatchAction.Add;
                    match.SliceBase = sliceBase;

                    //Debug.Assert(match.BaseBookmark.Equals(snapNav.Bookmark()));
                    match.BaseBookmark = snapNav.Bookmark();
                }
            }

            if (SnapshotGenerator.IsEqualName(snapNav.PathName, diffNav.PathName))
            {
                // diff represents type slice entry or explicit type slice, e.g. "value[x]:valueString"

                // [WMR 20190828] Empty sliceName indicates the slice entry; do NOT generate missing sliceName!
                var sliceName = diffNav.Current.SliceName;
                if (!string.IsNullOrEmpty(sliceName))
                {
                    AddOrMergeCurrentOrNext(sliceName);
                }

                if (!TypeIsSubSetOf(diffNav, snapNav))
                {
                    // diff.Types is not a subset of snap.Types, which is not allowed
                    throw Error.InvalidOperation($"Internal error in snapshot generator ({nameof(ElementMatcher)}.{nameof(constructChoiceTypeMatch)}): choice type of diff does not occur in snap, snap = '{snapNav.Path}', diff = '{diffNav.Path}'.");
                }

                // Diff and snap both represent common constraints for all types ("value[x]"); merge

            }
            else if (snapNav.IsRenamedChoiceTypeElement(diffNav.PathName))
            {
                // diff is renamed, represents type slice for single type ("valueString")
                // Match renamed element to named type slice in base
                // e.g. match "valueString" to "value[x]:valueString"
                AddOrMergeCurrentOrNext(diffNav.PathName);
            }
            else
            {
                // diff is neither match for snap ("value[x]") nor rename ("valueString")
                // Shouldn't happen...
                Debug.Fail("SHOULDN'T HAPPEN...");
                throw Error.InvalidOperation($"Internal error in snapshot generator ({nameof(ElementMatcher)}.{nameof(constructMatch)}): invalid choice type match, snap = '{snapNav.Path}', diff = '{diffNav.Path}'.");
            }

            // Also consume and match all following associated renamed element constraints
            // [WMR 20190204] R4 allows multiple renamed type-specific constraints
            // e.g. both valueString and valueBoolean element constraints
            while (diffNav.MoveToNext())
            {

                // Local helper method to append a match to the result
                void AddMatch(string sliceName)
                {
                    match = new MatchInfo()
                    {
                        DiffBookmark = diffNav.Bookmark(),
                    };

                    // R4: try to find matching renamed base element
                    AddOrMergeNext(sliceName);
                    matches.Add(match);
                    bm = diffNav.Bookmark();
                }

                if (snapNav.IsRenamedChoiceTypeElement(diffNav.PathName))
                {
                    AddMatch(diffNav.PathName);
                }

                else if (SnapshotGenerator.IsEqualName(snapNav.PathName, diffNav.PathName))
                {
                    var sliceName = EnsureSliceNameForTypeSlice(diffNav.Current, match);
                    AddMatch(sliceName);
                }

                else
                {
                    // Finished, return to the last matched element
                    diffNav.ReturnToBookmark(bm);
                    break;
                }
            }

            return matches;
        }

        private static bool TypeIsSubSetOf(ElementDefinitionNavigator diffNav, ElementDefinitionNavigator snapNav)
            => !diffNav.Current.Type.Except(snapNav.Current.Type, new TypeRefEqualityComparer()).Any();

        private class TypeRefEqualityComparer : IEqualityComparer<ElementDefinition.TypeRefComponent>
        {
            public bool Equals(ElementDefinition.TypeRefComponent x, ElementDefinition.TypeRefComponent y)
            {
                if (x is null && y is null)
                {
                    return true;
                }
                if (x is null || y is null)
                {
                    return false;
                }
                return x.Code.Equals(y.Code);
            }

            public int GetHashCode(ElementDefinition.TypeRefComponent obj)
                => obj?.Code?.GetHashCode() ?? 1;
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
            var match = new MatchInfo()
            {
                Action = MatchAction.New,
                BaseBookmark = snapNav.Bookmark(),
                DiffBookmark = diffNav.Bookmark()
            };

            // [WMR 20170928] Special case
            // STU3:
            // - A profile shall rename a choice type element if the element is constrained to a single type
            // - A derived profile SHALL maintain the new name of inherited renamed elements
            var diffName = diffNav.PathName;
            if (ElementDefinitionNavigator.IsChoiceTypeElement(diffName))
            {
                // Profile specifies constraint on choice type element (name ending with "[x]")
                // but there is no matching constraint in base profile for that path
                // Detect if the base profile has renamed the choice type element
                var candidate = findRenamedChoiceElement(snapNav, diffName);
                if (!(candidate is null))
                {
                    // Generate warning that a profile further constraining a
                    // renamed choice type element cannot refer to the original element name,
                    // but should use the inherited element name.
                    match.Issue = SnapshotGenerator.CreateIssueInvalidChoiceTypeName(diffNav.Current, candidate);
                }
            }


            snapNav.ReturnToBookmark(bm);
            return match;
        }

        // [WMR 20170308] The snapshot generator initializes snapNav with base profile elements, then merges diff constraints on top of that.
        // So after processing an element in snapNav, the original base element is no longer available.
        // However for sliced elements, we need to initialize the slice entry and all following named slices from the same base element.
        // Therefore we first clone the original, unmerged base element and it's children (recursively).
        // Now each slice match return a reference to the associated original base element, unaffected by further processing.
        static ElementDefinitionNavigator initSliceBase(ElementDefinitionNavigator snapNav)
        {
            var sliceBase = snapNav.CloneSubtree();

            // Named slices never inherit a slicing component
            sliceBase.Current.Slicing = null;

            // Special rule for named slices: always reset minimum cardinality to 0 (don't inherit from base)
            // Because even though slice entry may be required (min=1), a profile can still define optional named slices (min = 0); must specify at least one
            sliceBase.Current.Min = 0;

            return sliceBase;
        }

        /// <summary>Returns <c>true</c> if the specified element has a slicing component (<see cref="ElementDefinition.Slicing"/> is not <c>null</c>).</summary>
        static bool HasSlicingComponent(this ElementDefinition elem) => !(elem.Slicing is null);

        static List<MatchInfo> constructSliceMatch(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav, ElementDefinitionNavigator sliceBase = null)
        {
            var result = new List<MatchInfo>();

            // [WMR 20170406] NEW: Fix for Vadim issue - handle profile constraints on complex extension child elements 
            // Determine if the base profile is introducing a slice entry 
            var snapIsSliced = snapNav.Current.HasSlicingComponent();

            // [WMR 20181212] NEW - Initialize default slice base element *before* repositoning snapNav
            // By default, new/unmatched named slices inherit from the associated sliced element
            if (sliceBase is null)
            {
                sliceBase = initSliceBase(snapNav);
            }
            // Save reference to the original, unnamed slice base element in snapNav
            // Any following new (unmatched) slices introduced by diffNav inherit from this
            var orgSliceBase = sliceBase;

            // if diffNav specifies a slice name, then advance snapNav to matching base slice
            // Otherwise remain at the current slice entry or unsliced element
            var sliceName = diffNav.Current.SliceName;
            if (!string.IsNullOrEmpty(sliceName))
            {

                // [WMR 20181212] R4: Fixed
                if (snapNav.IsSliceBase(sliceName))
                {
                    // Match/merge with current snapNav
                }
                else if (snapNav.MoveToSliceBase(sliceName))
                {
                    // Found matching named slice in base, overrides default base element
                    sliceBase = initSliceBase(snapNav);
                }

#if false
                // [WMR 20190827] Redundant? See "matchSlice"
                // [WMR 20190813] TODO: Handle SliceIsConstraining
                // If true, indicates that this slice definition is constraining a slice definition with the same name in an inherited profile.
                // If false, the slice is not overriding any slice in an inherited profile.
                // If missing, the slice might or might not be overriding a slice in an inherited profile, depending on the sliceName.
                // If set to true, an ancestor profile SHALL have a slicing definition with this name.
                // If set to false, no ancestor profile is permitted to have a slicing definition with this name.
                // => Verify and emit issues if invalid

                var isConstraining = diffNav.Current.SliceIsConstraining;
                if (isConstraining.HasValue)
                {
                    var isMatch = SnapshotGenerator.IsEqualName(snapNav.Current.SliceName, sliceName);
                    if (isConstraining.Value && !isMatch)
                    {
                        var issue = SnapshotGenerator.CreateIssueSliceNameIllegalMatch(diffNav.Current);
                    }
                    else if (!isConstraining.Value && isMatch)
                    {
                        var issue = SnapshotGenerator.CreateIssueSliceNameNoMatch(diffNav.Current);
                    }
                }
#endif
            }

            // Bookmark the initial slice base element
            var snapSliceBase = snapNav.Bookmark();
            var baseIsSliced = snapNav.Current.HasSlicingComponent();

            // For the first entries with explicit slicing information (or implicit if this is an extension),
            // generate a match between the base's unsliced element and the first entry in the diff

            var isExtension = diffNav.Current.IsExtension();
            var diffIsSliced = diffNav.Current.HasSlicingComponent();

            // Extract the discriminator from diff or base slice entry
            var discriminator = diffIsSliced ? diffNav.Current.Slicing.Discriminator.ToList() : snapNav.Current.Slicing?.Discriminator.ToList();

            // [WMR 20170406] Differential allows extensions w/o slice entry
            // => Add new slice entry to snapshot if (and only if!) not already defined by the base profile
            if (diffIsSliced || (isExtension && !snapIsSliced))
            {
                // Differential has information for the slicing entry
                result.Add(new MatchInfo()
                {
                    Action = MatchAction.Slice,
                    BaseBookmark = snapSliceBase,
                    DiffBookmark = diffIsSliced ? diffNav.Bookmark() : Bookmark.Empty,
                    // [WMR 20170308] If this is a recursive call (e.g. named slice with slicing component = reslice),
                    // then explicitly override default base with specified slice base element
                    SliceBase = sliceBase,
                });

                // Skip any existing slicing entry in the differential; below we process the actual slices
                if (diffIsSliced && !diffNav.MoveToNextSliceAtAnyLevel())
                {
                    // Differential only contains a slice entry, but no actual slices
                    // Note: this is allowed, e.g. constrain rules = closed to disallow extensions in derived profile
                    return result;
                }
            }

            // Note: if slice entry is missing from diff, then we fall back to the inherited base slicing entry
            // Strictly not valid according to FHIR rules, but we can cope
            if (baseIsSliced)
            {
                // Always consume the base slice entry
                snapNav.MoveToNextSliceAtAnyLevel();

                // [WMR 20170718] NEW - Accept & handle diff constraints on base slice entry
                // Note: snapSliceBase still points to slice entry in snapNav base profile
                // [WMR 20170727] Special case: extensions
                // We must determine if the current diff element represents either a constraint
                // on the (inherited) extension slicing entry, or a concrete extension slice.
                // Extension slicing entry is implicit and optional in diff, may be omitted (or further constrained!)
                // Extension slices are not guaranteed to have a slice name.
                // However in extension slices, type[0].Profile != null per definition
                // In theory, a derived profile could introduce a type profile on the extension
                // slice entry element, however this is very obscure.
                // => if the element has type extension and a type profile, we assume it represents
                // a concrete extension slice and not the extension slicing entry.
                var elem = diffNav.Current;
                if (elem.SliceName is null && !isExtensionSlice(elem))
                {
                    // Generate match for constraint on existing slice entry
                    var match = new MatchInfo()
                    {
                        Action = MatchAction.Merge,
                        BaseBookmark = snapSliceBase,
                        DiffBookmark = diffNav.Bookmark(),
                        SliceBase = sliceBase
                    };
                    result.Add(match);

                    // Consume the diff constraint
                    if (!diffNav.MoveToNextSliceAtAnyLevel())
                    {
                        // No more named slices in diff; done!
                        return result;
                    }
                    // Merge named slices in diff
                }
            }

            // snapNav and diffNav are now positioned on the first concrete slices, if they exist (?)
            // Match remaining concrete slices to base, in order
            // slice definitions in StructureDef are always ordered; diff cannot re-order existing slices or insert new slices

            do
            {
                var diffElem = diffNav.Current;

                // [WMR 20190813] FIXED
                // Initialize SliceBase for re-slices to matching named slice in base (A/B => A)
                sliceName = diffNav.Current.SliceName;
                if (snapNav.MoveToSliceBase(sliceName))
                {
                    // Found matching named slice in base, overrides default base element
                    sliceBase = initSliceBase(snapNav);
                }
                else
                {
                    sliceBase = orgSliceBase;
                }

                // Named slice with a slice entry introduces a re-slice
                if (diffElem.HasSlicingComponent())
                {
                    // Recursively collect nested re-slice matches
                    // Re-use the existing slice base for the internal re-slices defined within the same profile
                    var bm = diffNav.Bookmark();
                    var reslices = constructSliceMatch(snapNav, diffNav, sliceBase);
                    result.AddRange(reslices);

                    // diffNav may have moved forward into child re-slices
                    // Move back up to re-sliced parent; loop will advance to the next sibling slice
                    // Important: After exiting loop, explicitly skip any remaining, already processed re-slices (below)
                    diffNav.ReturnToBookmark(bm);
                }
                else
                {
                    // Create a separate base instance for each slice
                    var match = new MatchInfo()
                    {
                        //BaseBookmark = snapNav.Bookmark(),
                        BaseBookmark = snapSliceBase,

                        DiffBookmark = diffNav.Bookmark(),
                        SliceBase = sliceBase
                    };
                    matchSlice(snapNav, diffNav, discriminator, match);
                    result.Add(match);

                    // Match to base slice? Then consume and advance to next
                    if (match.Action == MatchAction.Merge)
                    {
                        snapNav.MoveToNextSlice();
                    }
                }

                // Scan only sibling slices at the current level
                // Child re-slices are handled by recursive call above
            } while (diffNav.MoveToNextSlice());

            // Consume any remaining re-slices
            // Allready processed by recursive call above
            while (diffNav.MoveToNextSliceAtAnyLevel()) { }

            return result;
        }

        /// <summary>
        /// Returns the snapshot base bookmarked by <paramref name="originalSnapshotBase"/>
        /// </summary>
        /// <param name="snapNav">the snapshot navigator</param>
        /// <param name="originalSnapshotBase"></param>
        /// <returns></returns>
        private static ElementDefinitionNavigator returnToOriginalSliceBase(ElementDefinitionNavigator snapNav, Bookmark originalSnapshotBase)
        {
            ElementDefinitionNavigator sliceBase;
            // remember current position of SnapNav
            var tempSnapNav = snapNav.Bookmark();
            snapNav.ReturnToBookmark(originalSnapshotBase);
            sliceBase = initSliceBase(snapNav);
            // back to current position
            snapNav.ReturnToBookmark(tempSnapNav);
            return sliceBase;
        }

        /// <summary>Returns true if the element has type Extension and also specifies a custom type profile.</summary>
        static bool isExtensionSlice(ElementDefinition element) => isExtensionSlice(element.Type.FirstOrDefault());

        static readonly string EXTENSION_TYPENAME = FHIRAllTypes.Extension.GetLiteral();

        static bool isExtensionSlice(ElementDefinition.TypeRefComponent type)
            => !(type is null)
               && SnapshotGenerator.IsEqualType(type.Code, EXTENSION_TYPENAME)
               && !(type.Profile is null);

        // Match current snapshot and differential slice elements
        // Returns an initialized MatchInfo with action = Merge | Add
        // defaultBase represents the base element for newly introduced slices
        static void matchSlice(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav,
                    List<ElementDefinition.DiscriminatorComponent> discriminators, MatchInfo match)
        {
            Debug.Assert(!(match is null));                         // Caller should initialize match
            Debug.Assert(!diffNav.Current.HasSlicingComponent());   // Caller must handle reslicing

            // 1. If the diff slice has a name, than match base slice by name
            var diffSliceName = diffNav.Current.SliceName;
            if (!string.IsNullOrEmpty(diffSliceName))
            {
                var isMatch = SnapshotGenerator.IsEqualName(snapNav.Current.SliceName, diffSliceName);

                // [WMR 20181211] R4: inspect ElementDefinition.sliceIsConstraining
                // "If set to true, an ancestor profile SHALL have a slicing definition with this name.
                // If set to false, no ancestor profile is permitted to have a slicing definition with this name."
                // Note: if value is missing (null), then fall back to original (STU3) behavior,
                // i.e. match implies constraint on existing slice, otherwise new slice
                var isConstraining = diffNav.Current.SliceIsConstraining;
                if (!(isConstraining is null) && isConstraining != isMatch)
                {
                    // Invalid slice name
                    // - Either a constraining named slice WITHOUT matching named slice in base profile
                    // - Or a new named slice WITH conflicting named slice in base profile
                    match.Action = MatchAction.Invalid;
                    if (isMatch)
                    {
                        match.BaseBookmark = snapNav.Bookmark();
                        match.Issue = SnapshotGenerator.CreateIssueSliceNameConflict(diffNav.Current);
                    }
                    else
                    {
                        match.Issue = SnapshotGenerator.CreateIssueSliceNameNoMatch(diffNav.Current);
                    }
                    return;
                }

                if (isMatch)
                {
                    // Constrain an existing named slice
                    match.BaseBookmark = snapNav.Bookmark();
                    match.Action = MatchAction.Merge;
                }
                else
                {
                    // Introduce a new named slice
                    match.Action = MatchAction.Add;
                }

                return;
            }

            // Slice has no name
            // Allowed for:
            // - Extensions => discriminator = url
            // - type slices => discriminator = @type / @profile

            if (diffNav.Current.IsExtension())
            {
                // Extension discriminator (on .url value)
                // => match on ElementDefinition.Type[0].Profile
                matchExtensionSlice(snapNav, diffNav, discriminators, match);
                return;
            }

            else if (discriminators.Count == 1 && isTypeDiscriminator(discriminators[0]))
            {
                // Type discriminator
                // => match on ElementDefinition.Type[0].Code
                matchSliceByTypeCode(snapNav, diffNav, match);
                return;
            }

            if (isTypeProfileDiscriminator(discriminators))
            {
                // Type & Profile discriminator
                // => match on ElementDefinition.Type[0].Code & .Profile
                matchSliceByTypeProfile(snapNav, diffNav, match);
                return;
            }

            // Error! Unsupported discriminator => slices must be named
            match.Action = MatchAction.Invalid;
            match.Issue = SnapshotGenerator.CreateIssueSliceWithoutName(diffNav.Current);
        }

        // Match current snapshot and differential extension slice elements on extension type profile
        // Returns an initialized MatchInfo with action = Merge | Add
        // defaultBase represents the base element for newly introduced slices
        static void matchExtensionSlice(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav,
            List<ElementDefinition.DiscriminatorComponent> discriminators, MatchInfo match)
        {
            // [WMR 20170110] Accept missing slicing component, e.g. to close the extension slice: Extension.extension { max = 0 }
            // if (discriminators == null || discriminators.Count > 1 || discriminators.FirstOrDefault() != "url")
            if (!(discriminators is null) && (discriminators.Count != 1 || !isUrlDiscriminator(discriminators.FirstOrDefault())))
            {
                // Invalid extension discriminator; generate issue and ignore
                Debug.WriteLine($"[{nameof(ElementMatcher)}.{nameof(matchExtensionSlice)}] Warning! Invalid discriminator for extension slice (path = '{diffNav.Path}') - must be 'url'.");

                match.Issue = SnapshotGenerator.CreateIssueInvalidExtensionSlicingDiscriminator(diffNav.Current);
            }

            // Ignore the specified discriminator, always match on url
            var snapExtensionUri = getExtensionProfileUri(snapNav.Current);
            var diffExtensionUri = getExtensionProfileUri(diffNav.Current);
            // if (snapExtensionUri == diffExtensionUri)
            if (SnapshotGenerator.IsEqualUri(snapExtensionUri, diffExtensionUri))
            {
                match.BaseBookmark = snapNav.Bookmark();
                match.Action = MatchAction.Merge;
            }
            else
            {
                match.Action = MatchAction.Add;
            }
        }

        // Match current snapshot and differential slice elements on @type = Element.Type.Code
        // Returns an initialized MatchInfo with action = Merge | Add
        static void matchSliceByTypeCode(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav, MatchInfo match)
        {
            var diffTypeCodes = diffNav.Current.Type.Select(t => t.Code).ToList();
            if (diffTypeCodes.Count == 0)
            {
                Debug.WriteLine($"[{nameof(ElementMatcher)}.{nameof(matchSliceByTypeCode)}] Error! Element '{diffNav.Path}' is part of a @type slice group, but the element itself has no type.");

                match.Action = MatchAction.Invalid;
                match.Issue = SnapshotGenerator.createIssueTypeSliceWithoutType(diffNav.Current);
                return;
            }

            var snapTypeCodes = snapNav.Current.Type.Select(t => t.Code);
            if (snapTypeCodes.SequenceEqual(diffTypeCodes))
            {
                match.BaseBookmark = snapNav.Bookmark();
                match.Action = MatchAction.Merge;
                return;
            }

            match.Action = MatchAction.Add;
        }

        class SliceByTypeProfileEqualityComparer : IEqualityComparer<string>
        {
            readonly string _sliceName;

            public SliceByTypeProfileEqualityComparer(string sliceName)
            {
                _sliceName = sliceName ?? throw new ArgumentNullException(nameof(sliceName));
            }

            public bool Equals(string snapProfile, string diffProfile)
            {
                var profileRef = ProfileReference.Parse(diffProfile);
                if (profileRef.IsComplex)
                {
                    // Match on element name (for complex extension elements)
                    return SnapshotGenerator.IsEqualName(_sliceName, profileRef.ElementName);
                }
                else
                {
                    // Match on type profile(s)
                    return SnapshotGenerator.IsEqualUri(snapProfile, diffProfile);
                }
            }

            public int GetHashCode(string obj)
            {
                //throw new NotImplementedException();

                // Force the use of Equals
                return 0;
            }
        }

        // Match current snapshot and differential slice elements on @type|@profile = Element.Type.Code and Element.Type.Profile
        // Returns an initialized MatchInfo with action = Merge | Add
        static void matchSliceByTypeProfile(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav, MatchInfo match)
        {
            matchSliceByTypeCode(snapNav, diffNav, match);
            if (match.Action == MatchAction.Merge)
            {
                // We have a match on type code(s); match type profiles
#if MULTIPLE_SLICE_PROFILES
                // [WMR 20181212] Match type slice on all specified profiles
                var diffProfiles = diffNav.Current.PrimaryTypeProfiles();
                var snapProfiles = snapNav.Current.PrimaryTypeProfiles();

                // Handle Chris Grenz example http://example.com/fhir/SD/patient-research-auth-reslice
                // [WMR 20181212] Not used anymore?
                if (!diffProfiles.Any() && !snapProfiles.Any())
                {
                    return;
                }

                var comparer = new SliceByTypeProfileEqualityComparer(snapNav.Current.SliceName);
                var result = snapProfiles.SequenceEqual(diffProfiles, comparer);
#else
                var diffProfile = diffNav.Current.PrimaryTypeProfile();
                var snapProfile = snapNav.Current.PrimaryTypeProfile();

                // Handle Chris Grenz example http://example.com/fhir/SD/patient-research-auth-reslice
                // [WMR 20181212] Not used anymore?
                if (string.IsNullOrEmpty(diffProfile) && string.IsNullOrEmpty(snapProfile))
                {
                    return;
                }

                var profileRef = ProfileReference.Parse(diffProfile);
                var result = profileRef.IsComplex
                    // Match on element name (for complex extension elements)
                    ? SnapshotGenerator.IsEqualName(snapNav.Current.SliceName, profileRef.ElementName)
                    // Match on type profile(s)
                    : SnapshotGenerator.IsEqualUri(snapProfile, diffProfile);
#endif
                if (!result)
                {
                    match.Action = MatchAction.Add;
                }
            }
        }

        /// <summary>Given an extension element, return the canonical url of the associated extension definition, or <c>null</c>.</summary>
        static string getExtensionProfileUri(ElementDefinition elem)
        {
            Debug.Assert(elem.IsExtension());
            var elemTypes = elem.Type;
            if (elemTypes.Count != 1) { return null; }
            var elemType = elemTypes.FirstOrDefault();
            //if (!SnapshotGenerator.IsEqualType(elemType.Code, EXTENSION_TYPENAME)) { return null; }
            if (!IsExtensionType(elemType.Code)) { return null; }
            return elemType.Profile.FirstOrDefault();
        }

        /// <summary>Determines if the specified element has single type code "Extension".</summary>
        static bool IsExtensionType(ElementDefinition elem) => elem.Type.Count == 1 && IsExtensionType(elem.Type[0].Code);

        /// <summary>Returns <c>true</c> if the specified <paramref name="typeName"/> equals <c>Extension</c>, or <c>false</c> otherwise.</summary>
        static bool IsExtensionType(string typeName) => SnapshotGenerator.IsEqualType(typeName, EXTENSION_TYPENAME);

        /// <summary>Determines if the specified value equals the special predefined discriminator for slicing on element type profile.</summary>
        static bool isProfileDiscriminator(ElementDefinition.DiscriminatorComponent discriminator) => discriminator?.Type == ElementDefinition.DiscriminatorType.Profile;

        /// <summary>Determines if the specified value equals the special predefined discriminator for slicing on element type.</summary>
        static bool isTypeDiscriminator(ElementDefinition.DiscriminatorComponent discriminator) => discriminator?.Type == ElementDefinition.DiscriminatorType.Type;

        //EK: Commented out since this combination is not valid/has never been valid?  In any case we did not consider it
        //when composing the new DiscriminatorType valueset.
        ///// <summary>Determines if the specified value equals the special predefined discriminator for slicing on element type and profile.</summary>
        //static bool isTypeAndProfileDiscriminator(string discriminator) => StringComparer.Ordinal.Equals(discriminator, TypeAndProfileDiscriminator);

        /// <summary>Determines if the specified value equals the fixed default discriminator for slicing extension elements.</summary>
        static bool isUrlDiscriminator(ElementDefinition.DiscriminatorComponent discriminator)
            => !(discriminator is null)
               && discriminator.Type == ElementDefinition.DiscriminatorType.Value
               && SnapshotGenerator.IsEqualPath(discriminator.Path, ElementDefinition.DiscriminatorComponent.ExtensionDiscriminatorPath);

        // [WMR 20160801]
        // Determine if the specified discriminator(s) match on (type and) profile
        static bool isTypeProfileDiscriminator(IEnumerable<ElementDefinition.DiscriminatorComponent> discriminators)
        {
            // [WMR 20170411] TODO: update for STU3?

            if (!(discriminators is null))
            {
                var ar = discriminators.ToArray();
                if (ar.Length == 1)
                {
                    // return isUrlDiscriminator(ar[0]) || isTypeAndProfileDiscriminator(ar[0]) || isProfileDiscriminator(ar[0]);
                    //return isTypeAndProfileDiscriminator(ar[0]) || isProfileDiscriminator(ar[0]);
                    //EK: isTypeAndProfileDescriminator can no longer appear since the new valueset for discriminator type
                    //does not include that combination
                    return isProfileDiscriminator(ar[0]);
                }
                else if (ar.Length == 2)
                {
                    return (isProfileDiscriminator(ar[0]) && isTypeDiscriminator(ar[1]))
                        || (isProfileDiscriminator(ar[1]) && isTypeDiscriminator(ar[0]));
                }
            }
            return false;
        }

        /// <summary>List names of all following choice type elements ('[x]').</summary>
        static List<string> listChoiceElements(ElementDefinitionNavigator nav)
        {
            var bm = nav.Bookmark();

            // [WMR 20190826] Use HashSet to remove duplicates
            //var result = new List<string>();
            var elemNames = new HashSet<string>(StringComparer.Ordinal);

            do
            {
                if (!(nav.Current is null) && nav.Current.IsChoice())
                {
                    //result.Add(nav.PathName);
                    elemNames.Add(nav.PathName);
                }
            } while (nav.MoveToNext());

            nav.ReturnToBookmark(bm);

            //return result;
            return elemNames.ToList();
        }

        /// <summary>Find name of child element that represent a rename of the specified choice type element name.</summary>
        /// <param name="nav">An <see cref="ElementDefinitionNavigator "/> instance.</param>
        /// <param name="choiceName">Original choice type element name ending with "[x]".</param>
        static string findRenamedChoiceElement(ElementDefinitionNavigator nav, string choiceName)
        {
            var bm = nav.Bookmark();
            var result = new List<string>();

            if (nav.MoveToFirstChild())
            {
                do
                {
                    if (ElementDefinitionNavigator.IsRenamedChoiceTypeElement(choiceName, nav.PathName))
                    {
                        // Found match
                        // Renaming is only allowed for a single type constraint,
                        // so we're not expecting other matches (...)
                        return nav.PathName;
                    }

                } while (nav.MoveToNext());
            }

            nav.ReturnToBookmark(bm);

            return null;
        }
    }

    // For debugging purposes
    internal static class MatchPrinter
    {
        // [WMR 20160719] Add conditional compilation attribute
        [Conditional("DEBUG")]
        public static void DumpMatches(this IEnumerable<ElementMatcher.MatchInfo> matches, ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav)
        {
            var sbm = snapNav.Bookmark();
            var dbm = diffNav.Bookmark();

            foreach (var match in matches)
            {
                if (!snapNav.ReturnToBookmark(match.BaseBookmark) || !diffNav.ReturnToBookmark(match.DiffBookmark))
                {
                    throw Error.InvalidOperation("Found unreachable bookmark in matches");
                }
                var msg = FormatMatch(match, snapNav, diffNav);
                Debug.WriteLine(msg);
            }

            snapNav.ReturnToBookmark(sbm);
            diffNav.ReturnToBookmark(dbm);
        }

        [Conditional("DEBUG")]
        public static void DumpMatch(this ElementMatcher.MatchInfo match, ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav)
        {
            var sbm = snapNav.Bookmark();
            var dbm = diffNav.Bookmark();

            if (!snapNav.ReturnToBookmark(match.BaseBookmark) || !diffNav.ReturnToBookmark(match.DiffBookmark))
            {
                throw Error.InvalidOperation("Found unreachable bookmark in matches");
            }
            var msg = FormatMatch(match, snapNav, diffNav);
            Debug.WriteLine(msg);

            snapNav.ReturnToBookmark(sbm);
            diffNav.ReturnToBookmark(dbm);
        }

        public static string FormatMatch(ElementMatcher.MatchInfo match, ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav)
        {
            var msg = $"B:{FormatPath(snapNav)} <-- {match.Action.ToString()} --> D:{FormatPath(diffNav)}";
            if (!(match.SliceBase is null))
            {
                msg += $" -- SliceBase: {FormatPath(match.SliceBase)}";
            }
            if (!(match.Issue is null))
            {
                msg += $" -- Issue: {match.Issue}";
            }
            return msg;
        }

        static string FormatPath(ElementDefinitionNavigator nav)
        {
            if (nav.Current is null) { return "(none)"; }
            var result = nav.Path;
            if (!string.IsNullOrEmpty(nav.Current.SliceName))
            {
                result += " '" + nav.Current.SliceName + "'";
            }
            return result;
        }

    }

}
