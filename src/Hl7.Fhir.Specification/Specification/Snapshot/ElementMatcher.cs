/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

// Accept multiple renamed choice type elements (Chris Grenz)
#define MULTIPLE_RENAMED_CHOICE_TYPES

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

using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;
using System.Diagnostics;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;

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
        /// <returns><c>true</c> if snapNav is positioned on macthing base element, or <c>false</c> if diffNav introduces a new element.</returns>
        static bool matchBase(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav, List<string> choiceNames)
        {
            // First, match directly -> try to find the child in base with the same name as the path in the diff
            if (StringComparer.Ordinal.Equals(snapNav.PathName, diffNav.PathName) || snapNav.MoveToNext(diffNav.PathName))
            {
                return true;
            }

            // Not found, maybe this is a type slice shorthand, look if we have a matching choice prefix in snap
            var typeSliceShorthand = diffNav.PathName;

            // Try to match nameXXXXX to name[x]
            var matchingChoice = choiceNames.SingleOrDefault(xName => ElementDefinitionNavigator.IsRenamedChoiceTypeElement(xName, typeSliceShorthand));

            if (matchingChoice != null)
            {
                // [WMR 20170406] Match on current element? Then don't advance to next!
                // return snapNav.MoveToNext(matchingChoice);
                return snapNav.PathName == matchingChoice || snapNav.MoveToNext(matchingChoice);
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

            var match = new MatchInfo()
            {
                Action = MatchAction.Merge,
                BaseBookmark = snapNav.Bookmark(),
                DiffBookmark = diffNav.Bookmark()
            };

            // Verify type slice constraints (e.g. value[x] => valueString)
            // - base element has a type choice (value[x])
            // - Single derived element is constrained to single type and renamed (valueString)
            // - This is NOT a type slice => derived profile cannot contain multiple renamed elements!
            var result = new List<MatchInfo>() { match };

            if (snapNav.Current.IsChoice())
            {
                var bm = diffNav.Bookmark();

                // [WMR 20170308] NEW - Clone slice base element
                var sliceBase = initSliceBase(snapNav);

                while (diffNav.MoveToNext())
                {
                    if (snapNav.IsRenamedChoiceTypeElement(diffNav.PathName))
                    {
                        match = new MatchInfo()
                        {
#if MULTIPLE_RENAMED_CHOICE_TYPES
                            Action = MatchAction.Add,
#else
                            Action = MatchAction.Invalid,
#endif
                            BaseBookmark = snapNav.Bookmark(),
                            DiffBookmark = diffNav.Bookmark(),

                            // [WMR 20170308] NEW - Slice base element
                            SliceBase = sliceBase,

                            Issue = SnapshotGenerator.CreateIssueInvalidChoiceConstraint(diffNav.Current)
                        };
                        result.Add(match);
                        bm = diffNav.Bookmark();
                    }
                    else
                    {
                        diffNav.ReturnToBookmark(bm);
                        break;
                    }
                }
            }
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
            var match = new MatchInfo()
            {
                Action = MatchAction.New,
                BaseBookmark = snapNav.Bookmark(),
                DiffBookmark = diffNav.Bookmark()
            };
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

        static List<MatchInfo> constructSliceMatch(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav, ElementDefinitionNavigator sliceBase = null)
        {
            var result = new List<MatchInfo>();

            // [WMR 20170406] NEW: Fix for Vadim issue - handle profile constraints on complex extension child elements 
            // Determine if the base profile is introducing a slice entry 
            var snapIsSliced = snapNav.Current.Slicing != null;

            // if diffNav specifies a slice name, then advance snapNav to matching base slice
            // Otherwise remain at the current slice entry or unsliced element
            if (diffNav.Current.Name != null)
            {
                snapNav.MoveToNextSliceAtAnyLevel(diffNav.Current.Name);
            }

            // Bookmark the initial slice base element
            var snapSliceBase = snapNav.Bookmark();
            var baseIsSliced = snapNav.Current.Slicing != null;

            // For the first entries with explicit slicing information (or implicit if this is an extension),
            // generate a match between the base's unsliced element and the first entry in the diff

            var isExtension = diffNav.Current.IsExtension();
            var diffIsSliced = diffNav.Current.Slicing != null;

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
                    SliceBase = sliceBase
                });

                // Skip any existing slicing entry in the differential; below we process the actual slices
                if (diffIsSliced && !diffNav.MoveToNextSliceAtAnyLevel())
                {
                    // Differential only contains a slice entry, but no actual slices
                    // Note: this is allowed, e.g. constrain rules = closed to disallow extensions in derived profile
                    return result;
                }
            }

            // [WMR 20170308] NEW - Clone slice base element
            if (sliceBase == null)
            {
                sliceBase = initSliceBase(snapNav);
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
                if (elem.Name == null && !isExtensionSlice(elem))
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
                // [WMR 20180604] Fix for issue #611
                // "Bug: Snapshot Generator fails for derived profiles with sparse constraints on _some_ existing named slices"
                // => First advance to matching named slice in snap;
                // skip (consume) all preceding (unconstrained) named slices in snap
                // Match => Merge named slice in diff to existing named slice in snap
                // No match => Add new named slice (after all existing slices in snap)
                // Only try to match named slices; always add unnamed (extension) slices in-order
                if (diffNav.Current.Name != null)
                {
                    while (!StringComparer.Ordinal.Equals(snapNav.Current.Name, diffNav.Current.Name)
                           && snapNav.MoveToNextSlice())
                    {
                        // Skip unconstrained base profile slice entry
                    }
                }


                // Named slice with a slice entry introduces a re-slice
                if (diffNav.Current.Slicing != null)
                {
                    // Recursively collect nested re-slice matches
                    // Re-use the existing slice base for the internal re-slices defined within the same profile
                    var reslices = constructSliceMatch(snapNav, diffNav, sliceBase);
                    result.AddRange(reslices);
                }
                else
                {
                    // Create a separate base instance for each slice
                    var match = new MatchInfo()
                    {
                        // BaseBookmark = snapNav.Bookmark(),
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

            } while (diffNav.MoveToNextSliceAtAnyLevel());

            return result;
        }

        /// <summary>Returns true if the element has type Extension and also specifies a custom type profile.</summary>
        static bool isExtensionSlice(ElementDefinition element) => isExtensionSlice(element.Type.FirstOrDefault());

        static bool isExtensionSlice(ElementDefinition.TypeRefComponent type)
            => type != null
               && type.Code == FHIRDefinedType.Extension
               && type.Profile != null;

        // Match current snapshot and differential slice elements
        // Returns an initialized MatchInfo with action = Merge | Add
        static void matchSlice(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav, List<string> discriminators, MatchInfo match)
        {
            Debug.Assert(match != null);                    // Caller should initialize match
            Debug.Assert(diffNav.Current.Slicing == null);  // Caller must handle reslicing

            // 1. If the diff slice has a name, than match base slice by name
            var diffSliceName = diffNav.Current.Name;
            if (!string.IsNullOrEmpty(diffSliceName))
            {
                // if (snapNav.PathName == diffSliceName)
                if (StringComparer.Ordinal.Equals(snapNav.Current.Name, diffSliceName))
                {
                    match.BaseBookmark = snapNav.Bookmark();
                    match.Action = MatchAction.Merge;
                }
                else
                {
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
                // Discriminator = url => match on ElementDefinition.Type[0].Profile
                matchExtensionSlice(snapNav, diffNav, discriminators, match);
                return;
            }

            if (discriminators.Count == 1 && isTypeDiscriminator(discriminators[0]))
            {
                // Discriminator = @type => match on ElementDefinition.Type[0].Code
                matchSliceByTypeCode(snapNav, diffNav, match);
                return;
            }

            if (isTypeProfileDiscriminator(discriminators))
            {
                // Discriminator = type@profile, { @type, @profile }
                matchSliceByTypeProfile(snapNav, diffNav, match);
                return;
            }

            // Error! Unsupported discriminator => slices must be named
            match.Action = MatchAction.Invalid;
            match.Issue = SnapshotGenerator.CreateIssueSliceWithoutName(diffNav.Current);
        }

        // Match current snapshot and differential extension slice elements on extension type profile
        // Returns an initialized MatchInfo with action = Merge | Add
        static void matchExtensionSlice(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav, List<string> discriminators, MatchInfo match)
        {
            // [WMR 20170110] Accept missing slicing component, e.g. to close the extension slice: Extension.extension { max = 0 }
            // if (discriminators == null || discriminators.Count > 1 || discriminators.FirstOrDefault() != "url")
            if (discriminators != null && (discriminators.Count != 1 || discriminators.FirstOrDefault() != "url"))
            {
                // Invalid extension discriminator; generate issue and ignore
                Debug.WriteLine($"[{nameof(ElementMatcher)}.{nameof(matchExtensionSlice)}] Warning! Invalid discriminator for extension slice (path = '{diffNav.Path}') - must be 'url'.");

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
                match.Issue = SnapshotGenerator.CreateIssueTypeSliceWithoutType(diffNav.Current);
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

        // Match current snapshot and differential slice elements on @type|@profile = Element.Type.Code and Element.Type.Profile
        // Returns an initialized MatchInfo with action = Merge | Add
        static void matchSliceByTypeProfile(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav, MatchInfo match)
        {
            matchSliceByTypeCode(snapNav, diffNav, match);
            if (match.Action == MatchAction.Merge)
            {
                // We have a match on type code(s); match type profiles
                var diffProfiles = diffNav.Current.PrimaryTypeProfiles().ToList();
                var snapProfiles = snapNav.Current.PrimaryTypeProfiles().ToList();

                // Handle Chris Grenz example http://example.com/fhir/SD/patient-research-auth-reslice
                if (diffProfiles.IsNullOrEmpty() && snapProfiles.IsNullOrEmpty())
                {
                    return;
                }

                var diffProfile = diffProfiles.FirstOrDefault();
                var profileRef = ProfileReference.Parse(diffProfile);
                var result = profileRef.IsComplex
                    // Match on element name (for complex extension elements)
                    ? StringComparer.Ordinal.Equals(snapNav.Current.Name, profileRef.ElementName)
                    // Match on type profile(s)
                    : snapProfiles.SequenceEqual(diffProfiles);

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
            if (elemType.Code != FHIRDefinedType.Extension) { return null; }
            var profiles = elemType.Profile.ToList();
            if (profiles.Count != 1) { return null; }
            return profiles[0];
        }

        /// <summary>Special predefined discriminator for slicing on element type.</summary>
        static readonly string TypeDiscriminator = "@type";

        /// <summary>Special predefined discriminator for slicing on element type profile.</summary>
        static readonly string ProfileDiscriminator = "@profile";

        /// <summary>Special predefined discriminator for slicing on element type and profile.</summary>
        static readonly string TypeAndProfileDiscriminator = "type@profile";

        /// <summary>Fixed default discriminator for slicing extension elements.</summary>
        static readonly string UrlDiscriminator = "url";

        /// <summary>Determines if the specified value equals the special predefined discriminator for slicing on element type profile.</summary>
        static bool isProfileDiscriminator(string discriminator) => StringComparer.Ordinal.Equals(discriminator, ProfileDiscriminator);

        /// <summary>Determines if the specified value equals the special predefined discriminator for slicing on element type.</summary>
        static bool isTypeDiscriminator(string discriminator) => StringComparer.Ordinal.Equals(discriminator, TypeDiscriminator);

        /// <summary>Determines if the specified value equals the special predefined discriminator for slicing on element type and profile.</summary>
        static bool isTypeAndProfileDiscriminator(string discriminator) => StringComparer.Ordinal.Equals(discriminator, TypeAndProfileDiscriminator);

        /// <summary>Determines if the specified value equals the fixed default discriminator for slicing extension elements.</summary>
        static bool isUrlDiscriminator(string discriminator) => StringComparer.Ordinal.Equals(discriminator, UrlDiscriminator);

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

                var bPos = snapNav.Path + $"[{snapNav.OrdinalPosition}]";
                var dPos = diffNav.Path + $"[{diffNav.OrdinalPosition}]";

                // [WMR 20160719] Add name, if not null
                if (snapNav.Current != null && snapNav.Current.Name != null) bPos += $" '{snapNav.Current.Name}'";
                if (diffNav.Current != null && diffNav.Current.Name != null) dPos += $" '{diffNav.Current.Name}'";

                Debug.WriteLine($"B:{bPos} <-- {match.Action.ToString()} --> D:{dPos}");
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

            var bPos = snapNav.Path + $"[{snapNav.OrdinalPosition}]";
            var dPos = diffNav.Path + $"[{diffNav.OrdinalPosition}]";

            // [WMR 20160719] Add name, if not null
            if (snapNav.Current != null && snapNav.Current.Name != null) bPos += $" '{snapNav.Current.Name}'";
            if (diffNav.Current != null && diffNav.Current.Name != null) dPos += $" '{diffNav.Current.Name}'";

            Debug.WriteLine($"B:{bPos} <-- {match.Action.ToString()} --> D:{dPos}");

            snapNav.ReturnToBookmark(sbm);
            diffNav.ReturnToBookmark(dbm);
        }
    }

}
