/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

// [WMR 20161208] NEW
// Ewout:
// (1) Type slice: slicing constraints on choice type element "value[x]"
//     * Slicing introduction is required
//     * Elements are NOT renamed, e.g. value[x], Type.Code = "String"
//     * Use this format when you want to allow multiple element types with separate type-specific constraints
//     * Ewout: do NOT mix type slicing & element renaming (below)
//
//     Example:
//       element { path = "Observation.value[x]", slicing.discriminator = "@type" }
//       element { path = "Observation.value[x]", type.code = "String" }
//       element { path = "Observation.value[x]", type.code = "Integer" }
//
// (2) Type constraint: single type constraint on choice type element "value[x]"
//     * Only accepts a single type
//     * Element is renamed to "value{TYPE}"
//     * Cannot have multiple element constraints - this is NOT a slice!
//     * Snapshot: copy as-is
//       Theoretically, we could transform this to a type slice (1) with a single slice
//       However this form is harder to interpret and process (e.g. code generators), so keep simple syntax when possible
//
//     Example:
//       element { path = "Observation.valueString", type.code = "String" }


#define NEW_TYPE_SLICE


using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;
using System.Diagnostics;
using Hl7.Fhir.Model;
using Hl7.ElementModel;

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

            // [WMR 20161212] NEW
            public OperationOutcome.IssueComponent Issue { get; set; }

            //IEnumerable<OperationOutcome.IssueComponent> Issues => _issues;
            //List<OperationOutcome.IssueComponent> _issues;
            //public void AddIssue(OperationOutcome.IssueComponent component) //, profileUrl
            //{
            //    if (component == null) { throw Error.ArgumentNull(nameof(component)); }
            //    // component.Diagnostics = profileUrl ?? CurrentProfileUri;
            //    if (_issues == null) { _issues = new List<OperationOutcome.IssueComponent>(); }
            //    _issues.Add(component);
            //}
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
            New,

            // [WMR 20161212] NEW
            /// <summary>The element constraint is invalid and should be discarded.</summary>
            Invalid
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

#if NEW_TYPE_SLICE

            // [WMR 20160801] Only emit slicing entry for actual extension elements (with extension profile url)
            // Do not emit slicing entry for abstract Extension base element definition (inherited from external profiles)
            // [WMR 20160906] WRONG! Also need to handle complex extensions
            var diffIsExtension = diffNav.Current.IsExtension() &&
                (
                    diffNav.Current.PrimaryTypeProfile() != null                            // Extension element in a profile
                    || ElementDefinitionNavigator.GetPathRoot(diffNav.Path) == "Extension"  // Complex extension child element
                );
            bool baseIsSliced = snapNav.Current.Slicing != null;
            bool diffIsSliced = diffIsExtension || diffNav.Current.Slicing != null;

            if (baseIsSliced || diffIsSliced)
            {
                // This is a slice match - process it separately
                return constructSliceMatch(snapNav, diffNav);
            }

            var match = new MatchInfo() { BaseBookmark = snapNav.Bookmark(), DiffBookmark = diffNav.Bookmark() };

            // Verify type slice constraints (e.g. value[x] => valueString)
            // - base element has a type choice (value[x])
            // - Single derived element is constrained to single type and renamed (valueString)
            // - This is NOT a type slice => derived profile cannot contain multiple renamed elements!
            if (snapNav.Current.IsChoice() && (snapNav.PathName == diffNav.PathName || snapNav.IsRenamedChoiceElement(diffNav.PathName)))
            {
                // Verify that the differential contains only a single type slice constraint
                // Ignore any additional (invalid) type slice constraints

                var prevDiffElemName = previousElementName(diffNav);
                if (snapNav.PathName == prevDiffElemName || snapNav.IsRenamedChoiceElement(prevDiffElemName))
                {
                    // [WMR 20161212] Shouldn't happen, as the matching base element is consumed by the first diff constraint
                    Debug.Fail("Shouldn't happen...");

                    Debug.Print($"[{nameof(ElementMatcher)}.{nameof(constructMatch)}] Warning! Differential contains multiple constraints on choice type, path = '{diffNav.Path}'. Shortcut notation only allows a single type constraint, otherwise must use type slicing.");

                    match.Action = MatchAction.Invalid;
                    match.Issue = SnapshotGenerator.CreateIssueInvalidChoiceConstraint(diffNav.Current);
                    return new List<MatchInfo>() { match };
                }
            }

            // Easiest case - one to one match, without slicing involved
            match.Action = MatchAction.Merge;
            return new List<MatchInfo>() { match };

#else
            // [WMR 20161207] WRONG!
            var match = new MatchInfo() { BaseBookmark = snapNav.Bookmark(), DiffBookmark = diffNav.Bookmark() };

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



            var nextDiffChildName = nextElementName(diffNav);
            bool diffIsSliced = diffIsExtension || nextDiffChildName == diffNav.PathName;
            bool baseIsSliced = snapNav.Current.Slicing != null;

            bool diffIsTypeSlice = snapNav.Current.IsChoice() && (snapNav.PathName == diffNav.PathName || snapNav.IsCandidateTypeSlice(diffNav.PathName));

            if (diffIsTypeSlice)
            {
                // [WMR 20161208] Moved into constructTypeSliceMatch

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
                // Also handles type slice constraints (e.g. value[x] => valueString)
                match.Action = MatchAction.Merge;
                return new List<MatchInfo>() { match };
            }
#endif


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

        static List<MatchInfo> constructSliceMatch(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav)
        {
            var result = new List<MatchInfo>();

            var defaultBase = snapNav.Bookmark();
            var baseIsSliced = snapNav.Current.Slicing != null;

            // For the first entries with explicit slicing information (or implicit if this is an extension),
            // generate a match between the base's unsliced element and the first entry in the diff

            var isExtension = diffNav.Current.IsExtension();
            var diffIsSliced = diffNav.Current.Slicing != null;

            var discriminator = diffIsSliced ? diffNav.Current.Slicing.Discriminator.ToList() : snapNav.Current.Slicing?.Discriminator.ToList();

#if NEW_TYPE_SLICE
            if (diffIsSliced || isExtension)
            {
                // Differential has information for the slicing entry
                result.Add(new MatchInfo()
                {
                    BaseBookmark = defaultBase,
                    DiffBookmark = diffNav.Bookmark(),
                    Action = MatchAction.Slice
                });

                // Skip any existing slicing entry in the differential; below we process the actual slices
                if (diffIsSliced && !diffNav.MoveToNextSlice())
                {
                    // Differential only contains a slice entry, but no actual slices
                    // Note: this is allowed, e.g. constrain rules = closed to disallow extensions in derived profile
                    return result;
                }

                if (baseIsSliced)
                {
                    // Base slice entry will be merged with diff slice entry => advance to concrete base slices
                    // May return false if the base doesn't define any concrete slices...?
                    snapNav.MoveToNextSlice();
                }
            }
            else if (baseIsSliced)
            {
                // Base is sliced, but diff is not...?
                // Merge diff constraints with base slice entry
                // Differential has information for the slicing entry
                result.Add(new MatchInfo()
                {
                    BaseBookmark = defaultBase,
                    DiffBookmark = diffNav.Bookmark(),
                    Action = MatchAction.Slice
                });
                return result;
            }

            // snapNav and diffNav are now positioned on the first concrete slices, if they exist (?)
            // Match remaining concrete slices, in order
            // Note: if Slicing.Rules = OpenAtEnd, then diff may inject slices inbetween existing base slices

            // Merge concrete diff slices to base
            do
            {
                if (diffNav.Current.Slicing != null)
                {
                    // Current slice introduces a re-slice; recursively collect
                    var reslices = constructSliceMatch(snapNav, diffNav);
                    result.AddRange(reslices);
                }
                else
                {
                    var match = matchSlice(snapNav, diffNav, discriminator, defaultBase);
                    result.Add(match);
                }

            } while (diffNav.MoveToNextSlice());

#else

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
                // If the differential contains a slicing entry, then it should also define at least a single slice.
                if (diffIsSliced && !diffNav.MoveToNext())
                {
                    Debug.Print($"[{nameof(ElementMatcher)}] Warning! Differential contains a slicing entry for path '{diffNav.Path}' without any slices.");
                    return result;
                    // throw Error.InvalidOperation($"Differential has a slicing entry for path '{diffNav.Path}', but no first actual slice");
                }
            }

            // Then, generate a match between the base element(s) and the slicing entries in the diff
            // Note that the first entry may serve a double role and have two result matches (one for the constraints, one as a slicing entry)
            var diffName = diffNav.PathName;
            do
            {
                Bookmark matchingSlice;
                bool isReslice = false;
                if (baseIsSliced && findBaseSlice(snapNav, diffNav, out matchingSlice, out isReslice))
                {
                    result.Add(new MatchInfo()
                    {
                        BaseBookmark = matchingSlice,
                        DiffBookmark = diffNav.Bookmark(),
                        Action = isReslice ? MatchAction.Add : MatchAction.Merge
                    });
                }
                else
                {
                    result.Add(new MatchInfo()
                    {
                        BaseBookmark = bm,              
                        DiffBookmark = diffNav.Bookmark(),
                        Action = MatchAction.Add
                    });
                }
            } while (diffNav.MoveToNext(diffName));

#endif
            return result;
        }

#if NEW_TYPE_SLICE

        // Match current snapshot and differential slice elements
        // Returns an initialized MatchInfo with action = Merge | Add
        // defaultBase represents the base element for newly introduced slices
        static MatchInfo matchSlice(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav, List<string> discriminators, Bookmark defaultBase)
        {
            Debug.Assert(diffNav.Current.Slicing == null); // Callker must handle reslicing

            var match = new MatchInfo() { DiffBookmark = diffNav.Bookmark() };

            // 1. If the diff slice has a name, than match base slice by name
            var diffSliceName = diffNav.Current.Name;
            if (!string.IsNullOrEmpty(diffSliceName))
            {
                // if (snapNav.PathName == diffSliceName)
                if (StringComparer.Ordinal.Equals(snapNav.Current.Name, diffSliceName))
                {
                    match.BaseBookmark = snapNav.Bookmark();
                    match.Action = MatchAction.Merge;
                    return match;
                }
                else
                {
                    match.BaseBookmark = defaultBase;
                    match.Action = MatchAction.Add;
                }
                return match;
            }

            // Slice has no name
            // Allowed for:
            // - Extensions => discriminator = url
            // - type slices => discriminator = @type / @profile

            if (diffNav.Current.IsExtension())
            {
                // Discriminator = url => match on ElementDefinition.Type[0].Profile
                return matchExtensionSlice(snapNav, diffNav, discriminators, defaultBase);
            }

            else if (discriminators.Count == 1 && isTypeDiscriminator(discriminators[0]))
            {
                // Discriminator = @type => match on ElementDefinition.Type[0].Code
                return matchSliceByTypeCode(snapNav, diffNav, defaultBase);
            }

            else if (isTypeProfileDiscriminator(discriminators))
            {
                // Discriminator = type@profile, { @type, @profile }
                return matchSliceByTypeProfile(snapNav, diffNav, defaultBase);
            }

            // Error! Unsupported discriminator => Author must define slice names
            match.BaseBookmark = defaultBase;
            match.Action = MatchAction.Invalid;
            match.Issue = SnapshotGenerator.CreateIssueSliceWithoutName(diffNav.Current);
            return match;
        }

        // Match current snapshot and differential extension slice elements on extension type profile
        // Returns an initialized MatchInfo with action = Merge | Add
        // defaultBase represents the base element for newly introduced slices
        static MatchInfo matchExtensionSlice(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav, List<string> discriminators, Bookmark defaultBase)
        {
            var match = new MatchInfo() { DiffBookmark = diffNav.Bookmark() };

            if (discriminators == null || discriminators.Count > 1 || discriminators.FirstOrDefault() != "url")
            {
                // Invalid extension discriminator; generate issue and ignore
                Debug.Print($"[{nameof(ElementMatcher)}.{nameof(matchExtensionSlice)}] Warning! Invalid discriminator for extension slice (path = '{diffNav.Path}') - must be 'url'.");

                match.Issue = SnapshotGenerator.CreateIssueInvalidExtensionSlicingDiscriminator(diffNav.Current);
            }

            // Ignore the specified discriminator, always match on url
            var snapExtensionUri = getExtensionProfileUri(snapNav.Current);
            var diffExtensionUri = getExtensionProfileUri(diffNav.Current);
            // if (snapExtensionUri == diffExtensionUri)
            if (StringComparer.Ordinal.Equals(snapExtensionUri, diffExtensionUri))
            {
                match.BaseBookmark = snapNav.Bookmark();
                match.Action = MatchAction.Merge;
            }
            else
            {
                match.BaseBookmark = defaultBase;
                match.Action = MatchAction.Add;
            }

            return match;
        }

        // Match current snapshot and differential slice elements on @type = Element.Type.Code
        // Returns an initialized MatchInfo with action = Merge | Add
        // defaultBase represents the base element for newly introduced slices
        static MatchInfo matchSliceByTypeCode(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav, Bookmark defaultBase)
        {
            var match = new MatchInfo() { DiffBookmark = diffNav.Bookmark() };

            var diffTypeCodes = diffNav.Current.Type.Select(t => t.Code).ToList();
            if (diffTypeCodes.Count == 0)
            {
                Debug.Print($"[{nameof(ElementMatcher)}.{nameof(matchSliceByTypeCode)}] Error! Element '{diffNav.Path}' is part of a @type slice group, but the element itself has no type.");

                match.BaseBookmark = defaultBase;
                match.Action = MatchAction.Invalid;
                match.Issue = SnapshotGenerator.CreateIssueTypeSliceWithoutType(diffNav.Current);
                return match;
            }

            var snapTypeCodes = snapNav.Current.Type.Select(t => t.Code);
            if (snapTypeCodes.SequenceEqual(diffTypeCodes))
            {
                match.BaseBookmark = snapNav.Bookmark();
                match.Action = MatchAction.Merge;
                return match;
            }

            match.BaseBookmark = defaultBase;
            match.Action = MatchAction.Add;
            return match;
        }

        // Match current snapshot and differential slice elements on @type|@profile = Element.Type.Code and Element.Type.Profile
        // Returns an initialized MatchInfo with action = Merge | Add
        // defaultBase represents the base element for newly introduced slices
        static MatchInfo matchSliceByTypeProfile(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav, Bookmark defaultBase)
        {
            var match = matchSliceByTypeCode(snapNav, diffNav, defaultBase);
            if (match.Action == MatchAction.Merge)
            {
                // We have a match on type code(s); match type profiles
                var diffProfiles = diffNav.Current.PrimaryTypeProfiles().ToList();
                var snapProfiles = snapNav.Current.PrimaryTypeProfiles().ToList();

                // Handle Chris Grenz example http://example.com/fhir/SD/patient-research-auth-reslice
                if (diffProfiles.IsNullOrEmpty() && snapProfiles.IsNullOrEmpty())
                {
                    return match;
                }

                var diffProfile = diffProfiles.FirstOrDefault();
                var profileRef = ProfileReference.FromUrl(diffProfile);
                var result = profileRef.IsComplex
                    // Match on element name (for complex extension elements)
                    // ? snapNav.Current.Name == profileRef.ElementName
                    ? StringComparer.Ordinal.Equals(snapNav.Current.Name, profileRef.ElementName)
                    // Match on type profile(s)
                    : snapProfiles.SequenceEqual(diffProfiles);

                if (!result)
                {
                    match.Action = MatchAction.Add;
                }
            }
            return match;
        }

#else
        // [WMR 20160801] NEW
        // Try to find matching slice element in base profile
        // Assume snapNav is positioned on slicing entry node
        // Assume diffNav is positioned on a resliced element node
        // Returns true when match is found, matchingSlice points to match in base (merge here)
        // Returns false otherwise, matchingSlice points to current node in base
        // Maintain snapNav current position
        static bool findBaseSlice(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav, out Bookmark matchingSlice, out bool isReslice)
        {
            var result = false;
            isReslice = false;

            var bm = snapNav.Bookmark();
            matchingSlice = Bookmark.Empty;

            // 1. If the diff slice has a name, than match base slice by name
            var diffSliceName = diffNav.Current.Name;
            if (!string.IsNullOrEmpty(diffSliceName))
            {
                // [WMR 20161208] First try to match associated base slice
                result = snapNav.MoveToNextSlice(diffSliceName);

                // [WMR 20161208] Then match re-slice: {baseSliceName}/{resliceName}
                if (!result && ElementDefinitionNavigator.IsResliceName(diffSliceName))
                {
                    var baseSliceName = ElementDefinitionNavigator.GetBaseSliceName(diffSliceName);
                    result = snapNav.MoveToNextSlice(baseSliceName);
                    isReslice = true;
                }

                if (result)
                {
                    matchingSlice = snapNav.Bookmark();
                    snapNav.ReturnToBookmark(bm);
                }

                // No match; this represents a new named slice
                return result;
            }

            // Slice has no name
            // Allowed for:
            // - Extensions => discriminator = url
            // - type slices => discriminator = @type / @profile

            var slicing = snapNav.Current.Slicing;
            Debug.Assert(slicing != null);
            if (slicing == null) { return false; }

            var slicingIntro = matchingSlice = snapNav.Bookmark();
            var discriminators = slicing?.Discriminator.ToList();

            if (snapNav.Current.IsExtension())
            {
                if (discriminators.Count > 1 || discriminators.FirstOrDefault() != "url")
                {
                    // TODO: add issue
                    Debug.Print($"[{nameof(ElementMatcher)}.{nameof(findBaseSlice)}] Warning! Invalid discriminator for extension slice (path = '{diffNav.Path}') - must be 'url'.");
                }
                // Ignore the specified discriminator, always match on url
                var elemName = snapNav.PathName;
                var diffExtensionUri = getExtensionProfileUri(diffNav.Current);
                if (!string.IsNullOrEmpty(diffExtensionUri))
                {
                    do
                    {
                        var snapExtensionUri = getExtensionProfileUri(snapNav.Current);
                        if (StringComparer.Ordinal.Equals(diffExtensionUri, snapExtensionUri))
                        {
                            // Match!
                            matchingSlice = snapNav.Bookmark();
                            result = true;
                            break;
                        }
                    } while (snapNav.MoveToNext(elemName));
                }
            }

            else if (discriminators.Count == 1 && isTypeDiscriminator(discriminators[0]))
            {
                // Type slice

                // Skip the slicing introduction
                result = findBaseSliceByTypeCode(snapNav, diffNav, out matchingSlice);
            }

            // type@profile, { @type, @profile }
            else if (isTypeProfileDiscriminator(discriminators))
            {
                // [WMR 20160802] Handle complex extension constraints
                // e.g. sdc-questionnaire, Path = 'Questionnaire.group.question.extension.extension', name = 'question'
                // Type.Profile = 'http://hl7.org/fhir/StructureDefinition/questionnaire-enableWhen#question'
                // snapNav has already expanded target extension definition 'questionnaire-enableWhen'
                // => Match to base profile on child element with name 'question'

                result = findBaseSliceByTypeProfile(snapNav, diffNav, out matchingSlice);

            }
            else
            {
                throw Error.NotSupported($"Reslicing on discriminator '{string.Join("|", slicing.Discriminator)}' is not supported. Path = '{snapNav.Path}'.");
            }

            snapNav.ReturnToBookmark(slicingIntro);
            return result;
        }

        static bool findBaseSliceByTypeProfile(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav, out Bookmark matchingSlice)
        {
            var diffProfiles = diffNav.Current.PrimaryTypeProfiles().ToArray();

            // Handle Chris Grenz example http://example.com/fhir/SD/patient-research-auth-reslice
            if (diffProfiles == null || diffProfiles.Length == 0)
            {
                // throw Error.InvalidOperation($"Differential is reslicing on type/profile, but resliced element has no type profile (path = '{diffNav.Path}').");
                return findBaseSliceByTypeCode(snapNav, diffNav, out matchingSlice);
            }
            if (diffProfiles != null && diffProfiles.Length > 1)
            {
                throw Error.NotSupported($"Reslice on @profile with multiple type profiles is not supported (path = '{diffNav.Path}').");
            }


            var diffProfile = diffProfiles.FirstOrDefault();
            var profileRef = ProfileReference.FromUrl(diffProfile);
            while (snapNav.MoveToNext(snapNav.PathName))
            {
                var baseProfiles = snapNav.Current.PrimaryTypeProfiles().ToArray();
                var result = profileRef.IsComplex
                    // Match on element name (for complex extension elements)
                    ? snapNav.Current.Name == profileRef.ElementName
                    // Match on type profile(s)
                    : baseProfiles.SequenceEqual(diffProfiles);
                if (result)
                {
                    matchingSlice = snapNav.Bookmark();
                    return true;
                }
            }

            matchingSlice = Bookmark.Empty;
            return false;
        }

        static bool findBaseSliceByTypeCode(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav, out Bookmark matchingSlice)
        {
            var diffTypeCode = diffNav.Current.Type.FirstOrDefault().Code;
            if (diffTypeCode == null)
            {
                throw Error.InvalidOperation($"Differential contains a type slice constraint without a type (path = '{diffNav.Path}').");
            }

            var elemName = snapNav.PathName;
            while (snapNav.MoveToNext(elemName))
            {
                if (snapNav.Current.Type.FirstOrDefault().Code == diffTypeCode)
                {
                    matchingSlice = snapNav.Bookmark();
                    return true;
                }
            }

            matchingSlice = Bookmark.Empty;
            return false;
        }
#endif

        // Given an extension element, return the canonical url of the associated extension definition, or null
        static string getExtensionProfileUri(ElementDefinition elem)
        {
            Debug.Assert(elem.IsExtension());
            var elemTypes = elem.Type;
            if (elemTypes.Count != 1) { return null; }
            var elemType = elemTypes.FirstOrDefault();
            if (elemType.Code != FHIRDefinedType.Extension) { return null; }
            var profiles = elemType.Profile.ToList();
            if (profiles.Count != 1) { return null; }
            return profiles[0];
        }

        static bool isProfileDiscriminator(string discriminator) => discriminator == "@profile";
        static bool isTypeDiscriminator(string discriminator) => discriminator == "@type";
        static bool isTypeAndProfileDiscriminator(string discriminator) => discriminator == "type@profile";
        static bool isUrlDiscriminator(string discriminator) => discriminator == "url";

        // [WMR 20160801]
        // Determine if the specified discriminator(s) match on (type and) profile
        static bool isTypeProfileDiscriminator(IEnumerable<string> discriminators)
        {
            if (discriminators != null)
            {
                var ar = discriminators.ToArray();
                if (ar.Length == 1)
                {
                    // return isUrlDiscriminator(ar[0]) || isTypeAndProfileDiscriminator(ar[0]) || isProfileDiscriminator(ar[0]);
                    return isTypeAndProfileDiscriminator(ar[0]) || isProfileDiscriminator(ar[0]);
                }
                else if (ar.Length == 2)
                {
                    return (isProfileDiscriminator(ar[0]) && isTypeDiscriminator(ar[1]))
                        || (isProfileDiscriminator(ar[1]) && isTypeDiscriminator(ar[0]));
                }
            }
            return false;
        }

#if !NEW_TYPE_SLICE
        // [WMR 20160720] NEW
        // Handle type slices
        // Difference with regular slices:
        // - Don't need to handle extensions
        // - Match renamed elements, i.e. value[x] => valueBoolean
        static List<MatchInfo> constructTypeSliceMatch(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav)
        {
            var result = new List<MatchInfo>();

            var bm = snapNav.Bookmark();

            // [WMR 20161208] OLD
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
                    // [WMR 20161013] Do we need to throw? Snapshot generator could simply accept this, return empty list
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
            } while (diffNav.MoveToNextRenamedChoiceElement(snapNav.PathName));

            return result;
        }
#endif

        /// <summary>List all names of nodes in the current navigator that are choice ('[x]') elements.</summary>
        static List<string> listChoiceElements(ElementDefinitionNavigator snapNav)
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

        //static string nextElementName(ElementDefinitionNavigator nav)
        //{
        //    string result = null;

        //    var bm = nav.Bookmark();
        //    if (nav.MoveToNext())
        //    {
        //        result = nav.PathName;
        //        nav.ReturnToBookmark(bm);
        //    }

        //    return result;
        //}

        static string previousElementName(ElementDefinitionNavigator nav)
        {
            string result = null;

            var bm = nav.Bookmark();
            if (nav.MoveToPrevious())
            {
                result = nav.PathName;
                nav.ReturnToBookmark(bm);
            }

            return result;

        }

    }
}
