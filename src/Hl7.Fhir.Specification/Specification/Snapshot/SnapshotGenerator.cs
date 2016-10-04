/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

// WRONG - type properties should not override
// #define MERGE_PRIMITIVE_TYPES

// HACK: first ensure snapshots for Element & Extension, to prevent recursion
// #define PREPARE_CORE

#define CACHE_ROOT_ELEMDEF
// [WMR 20161004] Only enable this for debugging & verification (costly...)
// #define CACHE_ROOT_ELEMDEF_ASSERT

// Known issues:
// - Reslicing is only supported for complex extensions
// - Only supports a few hardcoded discriminator paths (url, @type, @profile)

using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Support;
using System.Diagnostics;
using Hl7.ElementModel;

namespace Hl7.Fhir.Specification.Snapshot
{
    // [WMR 20160927] Design considerations:
    // Functionality is implemented as a set of partial classes, in order to share common state and
    // minimize memory pressure while recursively generating snapshots of base and/or type profiles.

    public sealed partial class SnapshotGenerator
    {
        static readonly string ELEMENT_STRUCTURE_URI = ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Element);
#if PREPARE_CORE
        static readonly string EXTENSION_STRUCTURE_URI = ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Extension);
#endif

        // [WMR] Note: instance data is reused over multiple snapshot generations
        private readonly IResourceResolver _resolver;
        private readonly SnapshotGeneratorSettings _settings;
        private readonly SnapshotRecursionStack _state = new SnapshotRecursionStack();

        public SnapshotGenerator(IResourceResolver resolver, SnapshotGeneratorSettings settings) // : this()
        {
            if (resolver == null) { throw Error.ArgumentNull(nameof(resolver)); }
            if (settings == null) { throw Error.ArgumentNull(nameof(settings)); }
            _resolver = resolver;
            _settings = settings;
        }

        public SnapshotGenerator(IResourceResolver source) : this(source, SnapshotGeneratorSettings.Default)
        {
            // ...
        }

        /// <summary>Returns the snapshot generator configuration settings.</summary>
        public SnapshotGeneratorSettings Settings => _settings;

        /// <summary>Returns a reference to the profile uri of the currently generating snapshot, or <c>null</c>.</summary>
        string CurrentProfileUri => _state.CurrentProfileUri;

        /// <summary>
        /// (Re-)generate the <see cref="StructureDefinition.Snapshot"/> component of the specified <see cref="StructureDefinition"/> instance.
        /// Resolve the associated base profile snapshot and merge the <see cref="StructureDefinition.Differential"/> component.
        /// </summary>
        /// <param name="structure">A <see cref="StructureDefinition"/> instance.</param>
        public void Update(StructureDefinition structure)
        {
            structure.Snapshot = new StructureDefinition.SnapshotComponent()
            {
                Element = Generate(structure)
            };
            structure.Snapshot.SetCreatedBySnapshotGenerator();
        }

        /// <summary>
        /// Generate the snapshot element list of the specified <see cref="StructureDefinition"/> instance.
        /// Resolve the associated base profile snapshot and merge the <see cref="StructureDefinition.Differential"/> component.
        /// Returns the expanded element list.
        /// Does not modify the <see cref="StructureDefinition.Snapshot"/> property of the specified instance.
        /// </summary>
        /// <param name="structure">A <see cref="StructureDefinition"/> instance.</param>
        public List<ElementDefinition> Generate(StructureDefinition structure)
        {
            if (structure == null) { throw Error.ArgumentNull(nameof(structure)); }
            if (string.IsNullOrEmpty(structure.Url))
            {
                throw Error.Argument(nameof(structure), "The url of the specified StructureDefinition is missing or empty.");
            }

#if PREPARE_CORE
            if (structure.Url != ELEMENT_STRUCTURE_URI)
            {
                if (structure.Url != EXTENSION_STRUCTURE_URI)
                {
                    var extensionStructure = _resolver.FindStructureDefinitionForCoreType(FHIRDefinedType.Extension);
                    Debug.Print("[SnapshotGenerator.Generate] elementStructure: #{0}", extensionStructure.GetHashCode());
                    prepareCoreSnapshot(extensionStructure);
                }
                else
                {
                    var elementStructure = _resolver.FindStructureDefinitionForCoreType(FHIRDefinedType.Element);
                    Debug.Print("[SnapshotGenerator.Generate] elementStructure: #{0}", elementStructure.GetHashCode());
                    prepareCoreSnapshot(elementStructure);
                }
            }

#endif


            // Clear the OperationOutcome issues
            clearIssues();

            List<ElementDefinition> result = null;
            _state.OnBeforeGenerateSnapshot(structure.Url);
            try
            {
                result = generate(structure);
            }
            finally
            {
                // On complete expansion, the recursion stack should be empty
                Debug.Assert(result == null || _state.RecursionDepth == 1);
                _state.OnAfterGenerateSnapshot(structure.Url);
            }
            return result;
        }

        /// <summary>Given a list of element definitions, expand the definition of a single element.</summary>
        /// <param name="elements">A <see cref="StructureDefinition.SnapshotComponent"/> or <see cref="StructureDefinition.DifferentialComponent"/> instance.</param>
        /// <param name="element">The element to expand. Should be part of <paramref name="elements"/>.</param>
        /// <returns>A new, expanded list of <see cref="ElementDefinition"/> instances.</returns>
        /// <exception cref="ArgumentException">The specified element is not contained in the list.</exception>
        public IList<ElementDefinition> ExpandElement(IElementList elements, ElementDefinition element)
        {
            if (elements == null) { throw Error.ArgumentNull(nameof(elements)); }
            return ExpandElement(elements.Element, element);
        }

        /// <summary>Given a list of element definitions, expand the definition of a single element.</summary>
        /// <param name="elements">A list of <see cref="ElementDefinition"/> instances, taken from snapshot or differential.</param>
        /// <param name="element">The element to expand. Should be part of <paramref name="elements"/>.</param>
        /// <returns>A new, expanded list of <see cref="ElementDefinition"/> instances.</returns>
        /// <exception cref="ArgumentException">The specified element is not contained in the list.</exception>
        public IList<ElementDefinition> ExpandElement(IList<ElementDefinition> elements, ElementDefinition element)
        {
            if (elements == null) { throw Error.ArgumentNull(nameof(elements)); }
            if (element == null) { throw Error.ArgumentNull(nameof(element)); }

            var nav = new ElementDefinitionNavigator(elements);
            if (!nav.MoveTo(element))
            {
                throw Error.Argument(nameof(element), "Invalid argument for snapshot generator. The specified element list does not contain the target element to expand.");
            }

            clearIssues();

            // Must initialize recursion checker, because element expansion may recurse on external type profile
            _state.OnStartRecursion();
            try
            {
                expandElement(nav);
            }
            finally
            {
                _state.OnFinishRecursion();
            }
            return nav.Elements;
        }

        // ***** Private Interface *****

        // Merge children of the currently selected element from differential into snapshot
        void merge(ElementDefinitionNavigator snap, ElementDefinitionNavigator diff)
        {
            var snapPos = snap.Bookmark();
            var diffPos = diff.Bookmark();

            try
            {
                var matches = ElementMatcher.Match(snap, diff);

                // Debug.WriteLine("Matches for children of " + snap.Path + (snap.Current != null && snap.Current.Name != null ? " '" + snap.Current.Name + "'" : null));
                // matches.DumpMatches(snap, diff);

                foreach (var match in matches)
                {
                    if (!snap.ReturnToBookmark(match.BaseBookmark))
                    {
                        throw Error.InvalidOperation($"Internal error in snapshot generator ({nameof(merge)}): bookmark '{match.BaseBookmark}' in snap is no longer available");
                    }
                    if (!diff.ReturnToBookmark(match.DiffBookmark))
                    {
                        throw Error.InvalidOperation($"Internal error in snapshot generator ({nameof(merge)}): bookmark '{match.DiffBookmark}' in diff is no longer available");
                    }

                    if (match.Action == ElementMatcher.MatchAction.Add)
                    {
                        addSlice(snap, diff, match.BaseBookmark);
                    }
                    else if (match.Action == ElementMatcher.MatchAction.Merge)
                    {
                        mergeElement(snap, diff);
                    }
                    else if (match.Action == ElementMatcher.MatchAction.Slice)
                    {
                        makeSlice(snap, diff);
                    }
                    // For expanding core resource & datatype definitions
                    else if (match.Action == ElementMatcher.MatchAction.New)
                    {
                        // No matching base element; this is a new element definition
                        // snap is positioned at the associated parent element
                        // [WMR 20160907] NEW: For new elements, use the associated type profile as the base for each element type
                        createNewElement(snap, diff);
                    }

                }
            }
            finally
            {
                snap.ReturnToBookmark(snapPos);
                diff.ReturnToBookmark(diffPos);
            }
        }

        // Create a new resource element without a base element definition (for core type & resource profiles)
        void createNewElement(ElementDefinitionNavigator snap, ElementDefinitionNavigator diff)
        {
            StructureDefinition typeStructure = null;
            ElementDefinition baseElement = getBaseElementForElementType(diff.Current, out typeStructure);
            if (baseElement != null)
            {
                var newElement = (ElementDefinition)baseElement.DeepCopy();
                newElement.Path = ElementDefinitionNavigator.ReplacePathRoot(newElement.Path, diff.Path);
                newElement.Base = null;

                // [WMR 20160915] NEW: Notify subscribers
                OnPrepareElement(newElement, typeStructure, baseElement);

                mergeElementDefinition(newElement, diff.Current);

                snap.AppendChild(newElement);
            }
            else
            {
                var clonedElem = (ElementDefinition)diff.Current.DeepCopy();

                // [WMR 20160915] NEW: Notify subscribers
                OnPrepareElement(clonedElem, null, null);

                snap.AppendChild(clonedElem);
            }

            // Notify clients about a snapshot element with differential constraints
            OnConstraint(snap.Current);

            // Merge children
            mergeElement(snap, diff);
        }

        // Recursively merge the currently selected element and (grand)children from differential into snapshot
        void mergeElement(ElementDefinitionNavigator snap, ElementDefinitionNavigator diff)
        {
            // [WMR 20160816] Multiple inheritance - diamond problem
            // Element inherits constraints from base profile and also from any local type profile
            // Which constraints have priority?
            // Ewout: not defined yet, under discussion; use cases exist for both options
            // In this implementation, constraints from type profiles get priority over base

            // [WMR 20161003] Re-use any previously generated and annotated root element definition, if it exists
#if CACHE_ROOT_ELEMDEF
            var isValid = true;
            ElementDefinition cachedRootElemDef = null;
            if (diff.Current.IsRootElement() && (cachedRootElemDef = diff.Current.GetSnapshotElementAnnotation()) != null)
            {

#if CACHE_ROOT_ELEMDEF_ASSERT
                // DEBUG / VERIFY: merge results should be equal to cached ElemDef instance
                if (_settings.MergeTypeProfiles)
                {
                    isValid = mergeTypeProfiles(snap, diff);
                }

                mergeElementDefinition(snap.Current, diff.Current);
                var currentRootClone = (ElementDefinition)snap.Current.DeepCopy();
                var cachedRootClone = (ElementDefinition)cachedRootElemDef.DeepCopy();
                // Ignore Id, Path, Base and ChangedByDiff extension - they are expected to differ
                currentRootClone.ElementId = cachedRootClone.ElementId;
                currentRootClone.Path = cachedRootClone.Path;
                currentRootClone.Base = cachedRootClone.Base;
                currentRootClone.RemoveAllChangedByDiff();
                cachedRootClone.RemoveAllChangedByDiff();
                Debug.Assert(cachedRootClone.IsExactly(currentRootClone));
#endif

                Debug.Print($"[{nameof(SnapshotGenerator)}.{nameof(mergeElement)}] Re-use cached root element definition: '{cachedRootElemDef.Path}'  #{cachedRootElemDef.GetHashCode()}");
                snap.Elements[snap.OrdinalPosition.Value] = cachedRootElemDef;
                diff.Current.ClearSnapshotElementAnnotations();
            }
            else
            {
                // First merge constraints from element type profile, if it exists
                if (_settings.MergeTypeProfiles)
                {
                    isValid = mergeTypeProfiles(snap, diff);
                }

                // Then merge constraints from base profile
                mergeElementDefinition(snap.Current, diff.Current);
            }
#else
            // First merge constraints from element type profile, if it exists
            var isValid = true;
            if (_settings.MergeTypeProfiles)
            {
                isValid = mergeTypeProfiles(snap, diff);
            }

            // Then merge constraints from base profile
            mergeElementDefinition(snap.Current, diff.Current);

#endif

            if (!isValid)
            {
                // [WMR 20160905] If we failed to merge the type profile, then don't try to expand & merge any child constraints
            }
            else if (diff.HasChildren)
            {
                if (!snap.HasChildren)
                {
                    // The differential moves into an element that has no children in the base.
                    // This is allowable if the base's element has a nameReference or a TypeRef,
                    // in which case needs to be expanded before we can move to the path indicated
                    // by the differential

                    // Note that since we merged the parent, a (shorthand) typeslice will already
                    // have reduced the numer of types to 1. Still, if you don't do that, we cannot
                    // accept constraints on children, need to select a single type first...

                    if (snap.Current.Type.Count > 1)
                    {
                        addIssueInvalidChoiceConstraint(snap.Current);
                        return;
                    }

                    expandElement(snap);

                    // [WMR 20160906] WRONG! Allowed for core resource & datatype definitions...
                    //if (!snap.HasChildren)
                    //{
                    //    // Snapshot's element turns out not to be expandable, so we can't move to the desired path
                    //    addIssueInvalidChildConstraint(snap.Current);
                    //    return;
                    //}
                }

                // Now, recursively merge the children
                merge(snap, diff);

                // [WMR 20160720] NEW
                // generate [...]extension.url/fixedUri if missing
                // Ewout: [...]extension.url may be missing from differential
                // Information is redundant (same value as [...]extension/type/profile)
                // => snapshot generator should add this
                fixExtensionUrl(snap);
            }
        }

        // Merge a differential ElementDefinition constraint into a snapshot ElementDefinition instance.
        void mergeElementDefinition(ElementDefinition snap, ElementDefinition diff)
        {
            ElementDefnMerger.Merge(this, snap, diff);
        }

        // [WMR 20160720] Merge custom element type profiles, e.g. Patient.name with type.profile = "MyHumanName"
        // Also for extensions, i.e. an extension element in a profile inherits constraints from the extension definition
        // Specifically, the profile extension element inherits the cardinality from the extension definition root element (unless overridden in differential)
        //
        // Controversial - see GForge #9791
        //
        // How to merge elements with a custom type profile constraint?
        //
        // Example 1: Patient.Address with type.profile = AddressNL
        //            A. Merge constraints from base profile element Patient.Address
        //            B. Merge constraints from external profile AddressNL
        //
        // Example 2: slice value[x] + valueQuantity(Age)
        //            A. Merge constraints from value[x] into valueQuantity
        //            B. Merge constraints from Age profile into valueQuantity
        //
        // Ewout: no clear answer, valid use cases exist for both options
        //
        // Following logic is configurable
        // By default, use strategy (A): ignore custom type profile, merge from base
        // If mergeTypeProfiles is enabled, then first merge custom type profile before merging base

        // Resolve the type profile of the currently selected element and merge into snapshot
        bool mergeTypeProfiles(ElementDefinitionNavigator snap, ElementDefinitionNavigator diff)
        {
            // Debug.Print("[mergeTypeProfiles] {0} : {1}", diff.Path, diff.Current.Type != null && diff.Current.Type.Count == 1 ? diff.Current.PrimaryTypeCode() : null);

            // [WMR 20160913] Possible improvements:
            // Don't recurse on special terminator elements, e.g. id, url, value
            // Note that all these element definitions are marked with: <representation value="xmlAttr"/>

            var primaryDiffType = diff.Current.PrimaryType();
            if (primaryDiffType == null || primaryDiffType.IsReference()) { return true; }

            var primarySnapType = snap.Current.PrimaryType();
            // if (primarySnapType == null) { return true; }

            var primaryDiffTypeProfile = primaryDiffType.Profile.FirstOrDefault();
            var primarySnapTypeProfile = primarySnapType != null ? primarySnapType.Profile.FirstOrDefault() : null;

#if MERGE_PRIMITIVE_TYPES
            // [WMR 20161004] WRONG! The inherited type profile does NOT override existing snapshot constraints...!
            // Assuming that external type snapshots are 100% valid (or forceably regenerated), then the associated
            // type constraints should already have been merged from the underlying type profile by createNewElement.

            // If the differential overrides base type code, then merge primitive type profile (root elem only)
            // e.g. Extension.url : uri => Element : Element
            // Not necessary to expand the primitive structure, merge root differential element
            // Exclude root element, base has already been merged via StructureDef.Base
            if (!snap.Current.IsRootElement())
            {
                var primaryDiffTypeCode = primaryDiffType != null ? primaryDiffType.Code : null;
                var primarySnapTypeCode = primarySnapType != null ? primarySnapType.Code : null;
                if (string.IsNullOrEmpty(primaryDiffTypeProfile) && primaryDiffTypeCode.HasValue)
                {
                    primaryDiffTypeProfile = ModelInfo.CanonicalUriForFhirCoreType(primaryDiffTypeCode.Value);
                }
                if (string.IsNullOrEmpty(primarySnapTypeProfile) && primarySnapTypeCode.HasValue)
                {
                    primarySnapTypeProfile = ModelInfo.CanonicalUriForFhirCoreType(primarySnapTypeCode.Value);
                }
            }
#endif

            if (string.IsNullOrEmpty(primaryDiffTypeProfile) || primaryDiffTypeProfile == primarySnapTypeProfile) { return true; }

            // cf. ExpandElement

            // [WMR 20160721] NEW: Handle type profiles with name references
            // e.g. profile "http://hl7.org/fhir/StructureDefinition/qicore-adverseevent"
            // Extension element "cause" => "http://hl7.org/fhir/StructureDefinition/qicore-adverseevent-cause"
            // Constraint on extension child element "certainty" => "http://hl7.org/fhir/StructureDefinition/qicore-adverseevent-cause#certainty"
            // This means: 
            // - First inherit child element constraints from extension definition, element with name "certainty"
            // - Then override inherited constraints by explicit element constraints in profile differential

            var profileRef = ProfileReference.FromUrl(primaryDiffTypeProfile);
            if (profileRef.IsComplex)
            {
                primaryDiffTypeProfile = profileRef.CanonicalUrl;
            }

            var typeStructure = _resolver.FindStructureDefinition(primaryDiffTypeProfile);

            var diffNode = ToNamedNode(diff.Current);
            if (diff.HasChildren)
            {
                if (!ensureSnapshot(typeStructure, primaryDiffTypeProfile, diffNode))
                {
                    return false;
                }

                // [WMR 20160915] Also notify about type profiles?
                // OnPrepareElement(snap.Current, typeStructure, typeStructure.Snapshot.Element[0]);

                // Clone and rebase
                var rebasePath = diff.Path;
                if (profileRef.IsComplex)
                {
                    rebasePath = ElementDefinitionNavigator.GetParentPath(rebasePath);
                }
                var rebasedTypeSnapshot = (StructureDefinition.SnapshotComponent)typeStructure.Snapshot.DeepCopy();
                rebasedTypeSnapshot.Rebase(rebasePath);

                var typeNav = new ElementDefinitionNavigator(rebasedTypeSnapshot.Element);

                if (!profileRef.IsComplex)
                {
                    typeNav.MoveToFirstChild();
                }
                else
                {
                    if (!typeNav.JumpToNameReference(profileRef.ElementName))
                    {
                        addIssueInvalidProfileNameReference(snap.Current, profileRef.ElementName, primaryDiffTypeProfile);
                        return false;
                    }
                }

                // Recursively merge the full profile, then merge overriding constraints from differential
                mergeElement(snap, typeNav);
            }
            else
            {
                // Expand and merge (only!) the root element of the external type profile
                // Note: full expansion may trigger recursion, e.g. Element.id => identifier => string => Element
                // => only expand snapshot if required, otherwise just resolve/generate the root element
                var typeRootElem = getSnapshotRootElement(typeStructure, primaryDiffTypeProfile, diffNode);
                if (typeRootElem == null) { return false; }

                // Merge the type profile root element; no need to expand children
                mergeElementDefinition(snap.Current, typeRootElem);

            }
            return true;
        }

        // [WMR 20160720] NEW
        void fixExtensionUrl(ElementDefinitionNavigator nav)
        {
            var extElem = nav.Current;
            if (extElem.IsExtension() && nav.HasChildren)
            {
                // Resolve the canonical url of the extension definition from type[0]/profile[0]
                var profile = extElem.PrimaryTypeProfile();
                if (profile != null)
                {
                    var snapExtPos = nav.Bookmark();
                    try
                    {
                        if (nav.MoveToChild("url"))
                        {
                            var urlElem = nav.Current;
                            if (urlElem != null && urlElem.Fixed == null)
                            {
                                urlElem.Fixed = new FhirUri(profile);
                            }
                        }
                    }
                    finally
                    {
                        nav.ReturnToBookmark(snapExtPos);
                    }
                }
            }
        }

        void makeSlice(ElementDefinitionNavigator snap, ElementDefinitionNavigator diff)
        {
            // diff is now located at the first repeat of a slice, which is normally the slice entry (Extension slices need
            // not have a slicing entry)
            // snap is located at the base definition of the element that will become sliced. But snap is not yet sliced.

            // Before we start, is the base element sliceable?
            if (!snap.Current.IsRepeating() && !snap.Current.IsChoice())
            {
                addIssueInvalidSlice(diff.Current);
                return;
            }

            ElementDefinition slicingEntry = diff.Current;

            // Mmmm....no slicing entry in the differential. This is only alloweable for extension slices, as a shorthand notation.
            if (slicingEntry.Slicing == null)
            {
                if (slicingEntry.IsExtension())
                {
                    // In this case we create a "prefab" extension slice (with just slicing info)
                    // that's simply merged with the original element in base
                    slicingEntry = createExtensionSlicingEntry(snap.Current);
                }
                else
                {
                    addIssueMissingSliceEntry(diff.Current);
                    return;
                }
            }

            mergeElementDefinition(snap.Current, slicingEntry);

            ////TODO: update / check the slice entry's min/max property to match what we've found in the slice group
        }

        void addSlice(ElementDefinitionNavigator snap, ElementDefinitionNavigator diff, Bookmark baseBookmark)
        {
            // Add new slice after the last existing slice in base profile
            snap.MoveToLastSlice();
            var lastSlice = snap.Bookmark();

            // Initialize slice by duplicating base slice entry
            snap.ReturnToBookmark(baseBookmark);
            snap.DuplicateAfter(lastSlice);
            // Important: explicitly clear the slicing node in the copy!
            snap.Current.Slicing = null;

            // Notify clients about a snapshot element with differential constraints
            OnConstraint(snap.Current);

            // Merge differential
            mergeElement(snap, diff);
        }

        static ElementDefinition createExtensionSlicingEntry(ElementDefinition baseExtensionElement)
        {
            // Create the slicing entry by cloning the base Extension element
            var elem = baseExtensionElement != null ? (ElementDefinition)baseExtensionElement.DeepCopy() : new ElementDefinition();
            // Initialize slicing component to sensible defaults
            elem.Slicing = new ElementDefinition.SlicingComponent()
            {
                Discriminator = new[] { "url" }, // nameof(Extension.Url).ToLowerInvariant()
                Ordered = false,
                Rules = ElementDefinition.SlicingRules.Open
            };
            return elem;
        }

        void markChangedByDiff(Element element)
        {
            if (_settings.MarkChanges)
            {
                element.SetChangedByDiff();
            }
        }

        /// <summary>
        /// Expand the differential component of the specified structure and return the expanded element list.
        /// The given structure is not modified.
        /// </summary>
        List<ElementDefinition> generate(StructureDefinition structure)
        {
            List<ElementDefinition> result;
            var differential = structure.Differential;
            if (differential == null)
            {
                // [WMR 20160905] Or simply return the expanded base profile?
                throw Error.Argument(nameof(structure), "Invalid input for snapshot generator. The specified StructureDefinition does not contain a differential component.");
            }

            // [WMR 20160902] Also handle core resource / datatype definitions
            if (differential.Element == null || differential.Element.Count == 0)
            {
                // [WMR 20160905] Or simply return the expanded base profile?
                throw Error.Argument(nameof(structure), "Invalid input for snapshot generator. The differential component of the specified StructureDefinition is empty.");
            }

            // [WMR 20160718] Also accept extension definitions (IsConstraint == false)
            if (structure.IsConstraint && structure.Base == null)
            {
                throw Error.Argument(nameof(structure), "Invalid input for snapshot generator. The specified StructureDefinition represents a constraint on a FHIR core type or resource, but it does not specify a base profile url.");
            }

            StructureDefinition.SnapshotComponent snapshot = null;
            if (structure.Base != null)
            {
                var baseStructure = _resolver.FindStructureDefinition(structure.Base);
                if (!ensureSnapshot(baseStructure, structure.Base, ToNamedNode(differential.Element[0])))
                {
                    // Fatal error...
                    return null;
                }

                // [WMR 20160817] Notify client about the resolved, expanded base profile
                OnPrepareBaseProfile(structure, baseStructure);

                snapshot = (StructureDefinition.SnapshotComponent)baseStructure.Snapshot.DeepCopy();

                // [WMR 20160915] Derived profiles should never inherit the ChangedByDiff extension from the base structure
                snapshot.Element.RemoveAllChangedByDiff();

                // [WMR 20160902] Rebase the cloned base profile (e.g. DomainResource)
                if (!structure.IsConstraint)
                {
                    var rootElem = differential.Element.FirstOrDefault();
                    if (!rootElem.IsRootElement())
                    {
                        // Fatal error...
                        throw Error.Argument(nameof(structure), $"Internal error in snapshot generator. Base profile '{baseStructure.Url}' is not a constraint and the differential component does not start at the root element definition.");
                    }
                    snapshot.Rebase(rootElem.Path);
                }

                // Ensure that ElementDefinition.Base components in base StructureDef are propertly initialized
                // Always regenerate Base component! Cannot reuse cloned values
                ensureBaseComponents(snapshot.Element, structure.Base, true);

                // Notify observers
                for (int i = 0; i < snapshot.Element.Count; i++)
                {
                    OnPrepareElement(snapshot.Element[i], baseStructure, baseStructure.Snapshot.Element[i]);
                }
            }
            else
            {
                // No base; input is a core resource or datatype definition
                snapshot = new StructureDefinition.SnapshotComponent();
            }

            var snap = new ElementDefinitionNavigator(snapshot.Element);

            // Fill out the gaps (mostly missing parents) in the differential representation
            var fullDifferential = new DifferentialTreeConstructor(differential.Element).MakeTree();
            var diff = new ElementDefinitionNavigator(fullDifferential);

            merge(snap, diff);

            result = snap.ToListOfElements();

            // [WMR 20160917] NEW: Re-generate all ElementId values
            generateElementsId(result, true);

            return result;
        }

        /// <summary>
        /// Expand the currently active element within the specified <see cref="ElementDefinitionNavigator"/> instance.
        /// If the element has a name reference, then merge from the targeted element.
        /// Otherwise, if the element has a custom type profile, then merge it.
        /// </summary>
        internal bool expandElement(ElementDefinitionNavigator nav)
        {
            if (nav.Current == null)
            {
                throw Error.Argument(nameof(nav), $"Internal error in snapshot generator ({nameof(expandElement)}): Navigator is not positioned on an element");
            }

            if (nav.HasChildren)
            {
                return true;     // already has children, we're not doing anything extra
            }

            var defn = nav.Current;

            if (!String.IsNullOrEmpty(defn.NameReference))
            {
                var sourceNav = new ElementDefinitionNavigator(nav);
                var success = sourceNav.JumpToNameReference(defn.NameReference);

                if (!success)
                {
                    addIssueInvalidNameReference(defn, defn.NameReference);
                    return false;
                }

                nav.CopyChildren(sourceNav);
            }
            else if (defn.Type == null || defn.Type.Count == 0)
            {
                // Element has neither a name reference or a type...?
                if (!nav.Current.IsRootElement())
                {
                    addIssueNoTypeOrNameReference(defn);
                }
            }
            else if (defn.Type.Count > 1)
            {
                addIssueInvalidChoiceConstraint(defn);
            }

            else // if (defn.Type.Count == 1)
            {
                // [WMR 20160720] Handle custom type profiles (GForge #9791)
                StructureDefinition baseStructure = getStructureForElementType(defn, true);
                if (baseStructure != null && baseStructure.HasSnapshot)
                {
                    var baseSnap = baseStructure.Snapshot;
                    var baseNav = new ElementDefinitionNavigator(baseSnap.Element);
                    baseNav.MoveToFirstChild();
                    nav.CopyChildren(baseNav);

                    // Fix the copied elements and notify observers
                    var pos = nav.OrdinalPosition.Value;
                    for (int i = 1; i < baseSnap.Element.Count; i++)
                    {
                        var elem = nav.Elements[pos + i];
                        var baseElem = baseSnap.Element[i];

                        // [WMR 20160826] Never inherit Changed extension from base profile!
                        elem.RemoveAllChangedByDiff();

                        // [WMR 20160902] Initialize empty ElementDefinition.Base components if necessary
                        // [WMR 20160906] Always regenerate! Cannot reuse cloned base components
                        elem.EnsureBaseComponent(baseElem, true);

                        OnPrepareElement(elem, baseStructure, baseElem);
                    }
                }
            }

            return true;
        }

        StructureDefinition getStructureForElementType(ElementDefinition elementDef, bool ensureSnapshot)
        {
            Debug.Assert(elementDef != null);
            // Debug.Assert(elementDef.Type.Count > 0);
            var primaryType = elementDef.PrimaryType(); // Ignore any other types
            if (primaryType != null)
            {
                return getStructureForTypeRef(elementDef, primaryType, ensureSnapshot);
            }
            return null;
        }

        // Resolve StructureDefinition for the specified typeRef component
        // Expand snapshot and generate ElementDefinition.Base components if necessary
        StructureDefinition getStructureForTypeRef(ElementDefinition elementDef, ElementDefinition.TypeRefComponent typeRef, bool ensureSnapshot)
        {
            var location = ToNamedNode(elementDef);
            StructureDefinition baseStructure = null;

            // [WMR 20160720] Handle custom type profiles (GForge #9791)
            bool isValidProfile = false;

            // First try to resolve the custom element type profile, if specified
            var typeProfile = typeRef.Profile.FirstOrDefault();
            if (!string.IsNullOrEmpty(typeProfile) && _settings.MergeTypeProfiles && !typeRef.IsReference())
            // && !defn.IsExtension()
            {
                // Try to resolve the custom element type profile reference
                baseStructure = _resolver.FindStructureDefinition(typeProfile);
                isValidProfile = ensureSnapshot
                    ? this.ensureSnapshot(baseStructure, typeProfile, location)
                    : this.verifyStructure(baseStructure, typeProfile, location);
            }

            // Otherwise, or if the custom type profile is missing, then try to resolve the core type profile
            var typeCodeElem = typeRef.CodeElement;
            string typeName;
            if (!isValidProfile && typeCodeElem != null && (typeName = typeCodeElem.ObjectValue as string) != null)
            {
                baseStructure = _resolver.GetStructureDefinitionForTypeCode(typeCodeElem);
                // [WMR 20160906] Check if element type equals path (e.g. Resource root element), prevent endless recursion
                isValidProfile = (typeName == location.Path) ||
                    (
                        ensureSnapshot
                        ? this.ensureSnapshot(baseStructure, typeName, location)
                        : this.verifyStructure(baseStructure, typeName, location)
                    );

            }

            return baseStructure;
        }

        bool verifyStructure(StructureDefinition sd, string profileUrl, INamedNode location = null)
        {
            if (sd == null)
            {
                // Emit operation outcome issue and continue
                addIssueProfileNotFound(location, profileUrl);
                return false;
            }
            return true;
        }

        // Ensure that:
        // - the specified StructureDef is not null
        // - the snapshot component is not empty (expand on demand if necessary)
        // - The ElementDefinition.Base components are propertly initialized (regenerate if necessary)
        bool ensureSnapshot(StructureDefinition sd, string profileUri, INamedNode location = null)
        {
            if (!verifyStructure(sd, profileUri, location)) { return false; }
            profileUri = sd.Url;

            // Prevent endless recursion on root Element type definition
            //if (profileUri == ELEMENT_STRUCTURE_URI)
            //{
            //    Debug.Print("[SnapshotGenerator.ensureSnapshot] skip recursive Element expansion...");
            //    return true;
            //}

            // Detect endless recursion
            // Verify that the type profile is not already being expanded by a parent call higher up the call stack hierarchy
            // Special case: when recursing on Element, simply return true and continue; otherwise throw an exception
            var path = location != null ? location.Path : null;
            _state.OnBeforeExpandTypeProfile(profileUri, path);

            try
            {
                if (_settings.ExpandExternalProfiles
                    && (sd.Snapshot == null || (_settings.ForceExpandAll && !sd.Snapshot.IsCreatedBySnapshotGenerator()))
                )
                {
                    // Automatically expand external profiles on demand
                    Debug.Print($"[{nameof(SnapshotGenerator)}.{nameof(ensureSnapshot)}] Recursively generate snapshot for type profile with url: '{sd.Url}' ...");

                    sd.Snapshot = new StructureDefinition.SnapshotComponent()
                    {
                        Element = generate(sd)
                    };

                    if (!sd.HasSnapshot)
                    {
                        addIssueSnapshotGenerationFailed(profileUri);
                        return false;
                    }

                    // Add in-memory annotation to prevent repeated expansion
                    sd.Snapshot.SetCreatedBySnapshotGenerator();
                }

                if (!sd.HasSnapshot)
                {
                    addIssueProfileHasNoSnapshot(location, profileUri);
                    return false;
                }

                // Generating the element base components may also resolve StructureDefinitions and cause recursion!
                ensureSnapshotBaseComponents(sd);

                // [WMR 20160906] TODO: Generate ElementDefinition.id
            }
            finally
            {
                _state.OnAfterExpandTypeProfile(profileUri, path);
            }

            return true;
        }

#if PREPARE_CORE
        void prepareCoreSnapshot(StructureDefinition sd)
        {
            if (_settings.ExpandExternalProfiles
                && (sd.Snapshot == null || (_settings.ForceExpandAll && !isCreatedBySnapshotGenerator(sd.Snapshot)))
            )
            {
                Debug.Print($"[{nameof(SnapshotGenerator)}.{nameof(prepareCoreSnapshot)}] Generate snapshot for '{0}'", sd.Url);
                Update(sd);
            }
        }
#endif

        // Resolve the base element definition for the specified element = the snapshot root element of the associated type profile
        ElementDefinition getBaseElementForElementType(ElementDefinition elementDef, out StructureDefinition typeProfile)
        {
            Debug.Assert(elementDef != null);
            if (elementDef == null)
            {
                throw Error.ArgumentNull(nameof(elementDef), $"Internal error in Snapshot Generator ({nameof(getBaseElementForElementType)}): the specified element definition is null.");
            }

            typeProfile = null;
            // Debug.Assert(elementDef.Type.Count > 0);
            var primaryType = elementDef.PrimaryType(); // Ignore any other types
            if (primaryType != null)
            {
                return getBaseElementForTypeRef(elementDef, primaryType, out typeProfile);
            }
            return null;
        }

        // Resolve the base element definition for the specified element type = the snapshot root element of the associated type profile
        ElementDefinition getBaseElementForTypeRef(ElementDefinition elementDef, ElementDefinition.TypeRefComponent typeRef, out StructureDefinition typeProfile)
        {
            typeProfile = getStructureForTypeRef(elementDef, typeRef, false);
            if (typeProfile != null)
            {
                return getSnapshotRootElement(typeProfile, typeProfile.Url, ToNamedNode(elementDef));
            }
            return null;
        }

        // Returns the snapshot root element of the specified profile
        // Resolve from snapshot, if it exists and is valid
        // Otherwise recursively resolve the associated base profile root element definition (if it exists) and merge with differential root
        ElementDefinition getSnapshotRootElement(StructureDefinition sd, string profileUri, INamedNode location)
        {
            // TODO: Save root elements as annotation, use pre-generated root when generating full snapshot

            // Debug.Print("[SnapshotGenerator.getSnapshotRootElement] profileUri = '{0}' - resolving root element definition...", profileUri);
            if (!verifyStructure(sd, profileUri, location)) { return null; }

            if (sd.Differential == null || sd.Differential.Element == null || sd.Differential.Element.Count == 0)
            {
                addIssueProfileHasNoDifferential(location, profileUri);
                return null;
            }

            // Return previously generated, cached root element definition, if present
            var cachedRoot = sd.GetSnapshotRootElementAnnotation();
            if (cachedRoot != null) { return cachedRoot; }

            // Return root element definition from existing snapshot, if it exists
            if (sd.HasSnapshot && (sd.Snapshot.IsCreatedBySnapshotGenerator() || !_settings.ForceExpandAll))
            {
                Debug.Print($"[{nameof(SnapshotGenerator)}.{nameof(getSnapshotRootElement)}] {nameof(profileUri)} = '{profileUri}' - use existing root element definition from snapshot");
                return sd.Snapshot.Element[0];
            }

            // Resolve root element definition from base profile and merge from differential
            var diffRoot = sd.Differential.Element[0];

            var baseProfileUri = sd.Base;
            if (baseProfileUri == null)
            {
                // Structure has no base, i.e. core type definition => differential introduces & defines the root element
                Debug.Print($"[{nameof(SnapshotGenerator)}.{nameof(getSnapshotRootElement)}] {nameof(profileUri)} = '{profileUri}' - use root element definition from differential");
                var clonedDiffRoot = (ElementDefinition)diffRoot.DeepCopy();
#if CACHE_ROOT_ELEMDEF
                clonedDiffRoot.EnsureBaseComponent(null, true);
                sd.SetSnapshotRootElementAnnotation(clonedDiffRoot);
#endif
                return clonedDiffRoot;
            }

            // Resolve root element definition from base profile
            Debug.Print($"[{nameof(SnapshotGenerator)}.{nameof(getSnapshotRootElement)}] {nameof(profileUri)} = '{profileUri}' - recursively resolve root element definition from base profile '{baseProfileUri}' ...");
            var sdBase = _resolver.FindStructureDefinition(baseProfileUri);
            var baseRoot = getSnapshotRootElement(sdBase, baseProfileUri, ToNamedNode(diffRoot));
            if (baseRoot == null)
            {
                addIssueSnapshotGenerationFailed(baseProfileUri);
                return null;
            }

            // Clone and rebase
            var rebasedRoot = (ElementDefinition)baseRoot.DeepCopy();
            rebasedRoot.Path = diffRoot.Path;

            // Merge differential constraints onto base root element definition
            mergeElementDefinition(rebasedRoot, diffRoot);

            // TODO: Save the generated snapshot root element definition as annotation on StructureDefinition.Snapshot component
            // When generating the full snapshot, re-use the previously generated root element definition

            Debug.Print($"[{nameof(SnapshotGenerator)}.{nameof(getSnapshotRootElement)}] {nameof(profileUri)} = '{profileUri}' - succesfully resolved root element definition: #{rebasedRoot.GetHashCode()}");
#if CACHE_ROOT_ELEMDEF
            rebasedRoot.EnsureBaseComponent(baseRoot, true);
            sd.SetSnapshotRootElementAnnotation(rebasedRoot);
#endif
            return rebasedRoot;
        }

    }
}