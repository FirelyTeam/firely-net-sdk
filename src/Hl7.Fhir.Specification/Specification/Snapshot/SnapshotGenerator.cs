#define DETECT_RECURSION

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
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Support;
using System.Diagnostics;
using Hl7.ElementModel;
using Hl7.Fhir.Introspection;

namespace Hl7.Fhir.Specification.Snapshot
{
    public sealed partial class SnapshotGenerator
    {
        // [WMR] Note: instance data is reused over multiple snapshot generations
        private readonly IResourceResolver _resolver;
        private readonly SnapshotGeneratorSettings _settings;
#if DETECT_RECURSION
        private readonly SnapshotRecursionChecker _recursionChecker = new SnapshotRecursionChecker();
#endif

        public SnapshotGenerator(IResourceResolver resolver, SnapshotGeneratorSettings settings) // : this()
        {
            if (resolver == null) { throw Error.ArgumentNull("source"); }
            if (settings == null) { throw Error.ArgumentNull("settings"); }
            _resolver = resolver;
            _settings = settings;
        }

        public SnapshotGenerator(IResourceResolver source) : this(source, SnapshotGeneratorSettings.Default)
        {
            // ...
        }

        /// <summary>Returns the snapshot generator configuration settings.</summary>
        public SnapshotGeneratorSettings Settings { get { return _settings; } }

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
            if (structure == null)
            {
                throw Error.ArgumentNull("structure");
            }
            if (string.IsNullOrEmpty(structure.Url))
            {
                throw Error.Argument("structure", "The specified StructureDefinition resource has no canonical url.");
            }

            // Clear the OperationOutcome issues
            clearIssues();

#if DETECT_RECURSION
            List<ElementDefinition> result = null;
            _recursionChecker.StartExpansion(structure.Url);
            try
            {
                result = generate(structure);
            }
            finally
            {
                // On complete expansion, the recursion stack should be empty
                Debug.Assert(_recursionChecker.IsCompleted || result == null);
                _recursionChecker.FinishExpansion();
            }
#else
            result = generate(structure);
#endif
            return result;
        }

        // [WMR 20160802] NEW - TODO
        // For partial expansion of a single (complex) element

        /// <summary>Given a list of element definitions, expand the definition of a single element.</summary>
        /// <param name="elements">A <see cref="StructureDefinition.SnapshotComponent"/> or <see cref="StructureDefinition.DifferentialComponent"/> instance.</param>
        /// <param name="element">The element to expand. Should be part of <paramref name="elements"/>.</param>
        /// <returns>A new, expanded list of <see cref="ElementDefinition"/> instances.</returns>
        /// <exception cref="ArgumentException">The specified element is not contained in the list.</exception>
        public IList<ElementDefinition> ExpandElement(IElementList elements, ElementDefinition element)
        {
            if (elements == null) { throw Error.ArgumentNull("elements"); }
            return ExpandElement(elements.Element, element);
        }

        /// <summary>Given a list of element definitions, expand the definition of a single element.</summary>
        /// <param name="elements">A list of <see cref="ElementDefinition"/> instances, taken from snapshot or differential.</param>
        /// <param name="element">The element to expand. Should be part of <paramref name="elements"/>.</param>
        /// <returns>A new, expanded list of <see cref="ElementDefinition"/> instances.</returns>
        /// <exception cref="ArgumentException">The specified element is not contained in the list.</exception>
        public IList<ElementDefinition> ExpandElement(IList<ElementDefinition> elements, ElementDefinition element)
        {
            if (elements == null) { throw Error.ArgumentNull("elements"); }
            if (element == null) { throw Error.ArgumentNull("element"); }

            var nav = new ElementDefinitionNavigator(elements);
            if (!nav.MoveTo(element))
            {
                throw Error.Argument("element", "The specified target element not included in the element list.");
            }

            clearIssues();

            expandElement(nav);

            return nav.Elements;
        }

        // ***** Private Interface *****

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
                        throw Error.InvalidOperation("Internal error in snapshot generator (merge): bookmark '{0}' in snap is no longer available", match.BaseBookmark);
                    }
                    if (!diff.ReturnToBookmark(match.DiffBookmark))
                    {
                        throw Error.InvalidOperation("Internal error in snapshot generator (merge): bookmark '{0}' in diff is no longer available", match.DiffBookmark);
                    }

                    if (match.Action == ElementMatcher.MatchAction.Add)
                    {
                        // Add new slice after the last existing slice in base profile
                        snap.MoveToLastSlice();
                        var lastSlice = snap.Bookmark();

                        // Initialize slice by duplicating base slice entry
                        snap.ReturnToBookmark(match.BaseBookmark);
                        snap.DuplicateAfter(lastSlice);
                        // Important: explicitly clear the slicing node in the copy!
                        snap.Current.Slicing = null;

                        // Notify clients about a snapshot element with differential constraints
                        OnConstraint(snap.Current);

                        // Merge differential
                        mergeElement(snap, diff);
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

                        // Copy the element definition from differential to snapshot
                        var clone = (ElementDefinition)diff.Current.DeepCopy();

                        snap.AppendChild(clone);

                        // Notify clients about a snapshot element with differential constraints
                        OnConstraint(snap.Current);

                        // Merge children
                        mergeElement(snap, diff);
                    }

                }
            }
            finally
            {
                snap.ReturnToBookmark(snapPos);
                diff.ReturnToBookmark(diffPos);
            }
        }


        void mergeElement(ElementDefinitionNavigator snap, ElementDefinitionNavigator diff)
        {
            // [WMR 20160816] Multiple inheritance - diamond problem
            // Element inherits constraints from base profile and also from any local type profile
            // Which constraints have priority?
            // Ewout: not defined yet, under discussion; use cases exist for both options
            // In this implementation, constraints from type profiles get priority over base

            // TODO: Merge multiple base element definitions...

            // First merge constraints from element type profile, if it exists
            var isValid = true;
            if (_settings.MergeTypeProfiles)
            {
                isValid = mergeTypeProfiles(snap, diff);
            }

            // Then merge constraints from base profile
            mergeElementDefinition(snap.Current, diff.Current);

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

                    if (!snap.HasChildren)
                    {
                        // Snapshot's element turns out not to be expandable, so we can't move to the desired path
                        addIssueInvalidChildConstraint(snap.Current);
                        return;
                    }
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
            else if (_settings.ExpandUnconstrainedElements)
            {
                var types = snap.Current.Type;
                if (types.Count == 1)
                {
                    var primaryType = types[0];
                    var typeCode = primaryType.Code;
                    if (!string.IsNullOrEmpty(typeCode) && ModelInfo.IsDataType(typeCode))
                    {
                        expandElement(snap);
                    }
                    // [WMR 20160903] Handle unknown/custom core types
                    else if (string.IsNullOrEmpty(typeCode) && primaryType.CodeElement != null)
                    {
                        var typeName = primaryType.CodeElement.ObjectValue as string;
                        if (!string.IsNullOrEmpty(typeName))
                        {
                            // Assume DataType; no way to determine...
                            expandElement(snap);
                        }
                    }
                }
            }
        }

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
        bool mergeTypeProfiles(ElementDefinitionNavigator snap, ElementDefinitionNavigator diff)
        {
            var primaryDiffType = diff.Current.PrimaryType();
            if (primaryDiffType == null || primaryDiffType.Code == FHIRAllTypes.Reference.GetLiteral())
            {
                return true;
            }

            var primarySnapType = snap.Current.PrimaryType();
            if (primarySnapType == null)
            {
                return true;
            }

            var primaryDiffTypeProfile = primaryDiffType.Profile;
            var primarySnapTypeProfile = primarySnapType.Profile;
            if (string.IsNullOrEmpty(primaryDiffTypeProfile) || primaryDiffTypeProfile == primarySnapTypeProfile)
            {
                return true;
            }

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

            var baseStructure = _resolver.FindStructureDefinition(primaryDiffTypeProfile);
            if (verifyStructureDef(baseStructure, primaryDiffTypeProfile, ToNamedNode(diff.Current)))
            {
                // Clone and rebase
                var rebasePath = diff.Path;
                if (profileRef.IsComplex)
                {
                    rebasePath = ElementDefinitionNavigator.GetParentPath(rebasePath);
                }
                var rebasedBaseSnapshot = (StructureDefinition.SnapshotComponent)baseStructure.Snapshot.DeepCopy();
                rebasedBaseSnapshot.Rebase(rebasePath);

                generateElementsBase(rebasedBaseSnapshot.Element, baseStructure.BaseDefinition);

                var baseNav = new ElementDefinitionNavigator(rebasedBaseSnapshot.Element);

                if (!profileRef.IsComplex)
                {
                    baseNav.MoveToFirstChild();
                }
                else
                {
                    if (!baseNav.JumpToNameReference(profileRef.ElementName))
                    {
                        addIssueInvalidProfileNameReference(diff.Current, profileRef.ElementName, primaryDiffTypeProfile);
                        return false;
                    }
                }

                // Merge the external type profile
                if (diff.HasChildren)
                {
                    // Recursively merge the full profile, then merge overriding constraints from differential
                    mergeElement(snap, baseNav);
                }
                else
                {
                    // Only merge the profile root element; no need to expand children
                    mergeElementDefinition(snap.Current, baseNav.Current);
                }

                return true;
            }

            return false;
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

            //Mmmm....no slicing entry in the differential. This is only alloweable for extension slices, as a shorthand notation.
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


        static ElementDefinition createExtensionSlicingEntry(ElementDefinition baseExtensionElement)
        {
            // Create a pre-fab extension slice, filled with sensible defaults
            // TODO: Derive from base extension element
#if true
            var elem = baseExtensionElement != null ? (ElementDefinition)baseExtensionElement.DeepCopy() : new ElementDefinition();
            elem.Slicing = new ElementDefinition.SlicingComponent()
            {
                Discriminator = new[] { "url" },
                Ordered = false,
                Rules = ElementDefinition.SlicingRules.Open
            };
            return elem;
#else
            return new ElementDefinition()
            {
                Slicing = new ElementDefinition.SlicingComponent()
                {
                    Discriminator = new[] { "url" },
                    Ordered = false,
                    Rules = ElementDefinition.SlicingRules.Open
                }
            };
#endif
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
                throw Error.Argument("structure", "structure does not contain a differential component.");
            }

            // [WMR 20160902] Also handle core resource / datatype definitions
            if (differential.Element == null || differential.Element.Count == 0)
            {
                // [WMR 20160905] Or simply return the expanded base profile?
                throw Error.Argument("structure", "Differential component does not contain any element constraints.");
            }

            // [WMR 20160718] Also accept extension definitions (IsConstraint == false)
            if (structure.IsConstraint && structure.BaseDefinition == null)
            {
                throw Error.Argument("structure", "Structure is a constraint, but does not specify a base profile url.");
            }

            StructureDefinition.SnapshotComponent snapshot = null;
            if (structure.BaseDefinition != null)
            {
                var baseStructure = _resolver.FindStructureDefinition(structure.BaseDefinition);
                if (!verifyStructureDef(baseStructure, structure.BaseDefinition))
                {
                    // Fatal error...
                    return null;
                }

                // [WMR 20160817] Notify client about the resolved, expanded base profile
                OnPrepareBaseProfile(structure, baseStructure);

                snapshot = (StructureDefinition.SnapshotComponent)baseStructure.Snapshot.DeepCopy();

                // [WMR 20160902] Rebase the cloned base profile (e.g. DomainResource)
                if (!structure.IsConstraint)
                {
                    var rootElem = differential.Element.FirstOrDefault();
                    if (!rootElem.IsRootElement())
                    {
                        // Fatal error...
                        throw Error.Argument("structure", "Base profile '{0}' is not a constraint and the differential component does not start at the root element definition.".FormatWith(baseStructure.Url));
                    }
                    snapshot.Rebase(rootElem.Path);
                }

                // Ensure that ElementDefinition.Base components in base StructureDef are propertly initialized
                // Always regenerate Base component! Cannot reuse cloned values
                generateElementsBase(snapshot.Element, structure.BaseDefinition, true);

                // Notify observers
                for (int i = 0; i < snapshot.Element.Count; i++)
                {
                    OnPrepareElement(snapshot.Element[i], baseStructure.Snapshot.Element[i]);
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
                throw Error.Argument("nav", "Internal error in snapshot generator (expandElement): Navigator is not positioned on an element");
            }

            if (nav.HasChildren)
            {
                return true;     // already has children, we're not doing anything extra
            }

            var defn = nav.Current;

            if (!String.IsNullOrEmpty(defn.ContentReference))
            {
                var sourceNav = new ElementDefinitionNavigator(nav);
                var success = sourceNav.JumpToNameReference(defn.ContentReference);

                if (!success)
                {
                    addIssueInvalidNameReference(defn, defn.ContentReference);
                    return false;
                }

                nav.CopyChildren(sourceNav);
            }
            else if (defn.Type == null || defn.Type.Count == 0)
            {
                // Element has neither a name reference or a type...?
                addIssueNoTypeOrNameReference(defn);
            }
            else if (defn.Type.Count > 1)
            {
                addIssueInvalidChoiceConstraint(defn);
            }
            else // if (defn.Type.Count == 1)
            {
                // [WMR 20160720] Handle custom type profiles (GForge #9791)
                StructureDefinition baseStructure = getStructureForElementType(defn);
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
                        elem.ClearAllChangedByDiff();

                        // [WMR 20160902] Initialize empty ElementDefinition.Base components if necessary
                        // [WMR 20160906] Always regenerate! Cannot reuse cloned base components
                        ensureElementBase(elem, baseElem, true);

                        OnPrepareElement(elem, baseElem);
                    }
                }
            }

            return true;
        }

        StructureDefinition getStructureForElementType(ElementDefinition elementDef)
        {
            Debug.Assert(elementDef != null);
            Debug.Assert(elementDef.Type.Count > 0);
            var primaryType = elementDef.Type[0]; // Ignore any other types
            if (primaryType != null)
            {
                return getStructureForTypeRef(ToNamedNode(elementDef), primaryType);
            }
            return null;
        }

        // Resolve StructureDefinition for the specified typeRef component
        // Expand snapshot and generate ElementDefinition.Base components if necessary
        StructureDefinition getStructureForTypeRef(INamedNode location, ElementDefinition.TypeRefComponent typeRef)
        {
            StructureDefinition baseStructure = null;

            // [WMR 20160720] Handle custom type profiles (GForge #9791)
            bool isValidProfile = false;

            // First try to resolve the custom element type profile, if specified
            var typeProfile = typeRef.Profile;
            if (!string.IsNullOrEmpty(typeProfile) && _settings.MergeTypeProfiles && !typeRef.IsReference())
                // && !defn.IsExtension()
            {
                // Try to resolve the custom element type profile reference
                baseStructure = _resolver.FindStructureDefinition(typeProfile);
                isValidProfile = verifyStructureDef(baseStructure, typeProfile, location);
            }

            // Otherwise, or if the custom type profile is missing, then try to resolve the core type profile
            var typeCodeElem = typeRef.CodeElement;
            if (!isValidProfile && typeCodeElem != null && typeCodeElem.ObjectValue is string)
            {
                baseStructure = _resolver.GetStructureDefinitionForTypeCode(typeCodeElem);
                isValidProfile = verifyStructureDef(baseStructure, typeCodeElem.ObjectValue as string, location);
            }

            return baseStructure;
        }

        // Ensure that:
        // - the specified StructureDef is not null
        // - the snapshot component is not empty (expand on demand if necessary)
        // - The ElementDefinition.Base components are propertly initialized (regenerate if necessary)
        bool verifyStructureDef(StructureDefinition sd, string profileUrl, INamedNode location = null)
        {
            if (sd == null)
            {
                // Emit operation outcome issue and continue
                addIssueProfileNotFound(location, profileUrl);
                return false;
            }

            if (sd.Snapshot == null && _settings.ExpandExternalProfiles) // || _settings.ForceExpandAll
            {
                // Automatically expand external profiles on demand
                Debug.Print("Recursively expand snapshot of external profile with url: '{0}' ...".FormatWith(sd.Url));

#if DETECT_RECURSION
                // Detect endless recursion
                // Verify that the type profile is not already being expanded by a parent call higher up the call stack hierarchy
                _recursionChecker.OnBeforeExpandType(profileUrl, location != null ? location.Path : null);
#endif
                try
                {
                    // TODO: support SnapshotGeneratorSettings.ForceExpandAll
                    // Use (timestamp) annotation to mark & detect already (forceably) re-expanded profiles!
                    sd.Snapshot = new StructureDefinition.SnapshotComponent()
                    {
                        Element = generate(sd)
                    };
                }
                finally
                {
#if DETECT_RECURSION
                    _recursionChecker.OnAfterExpandType(profileUrl);
#endif
                }
                if (!sd.HasSnapshot)
                {
                    addIssueProfileHasNoSnapshot(location, profileUrl);
                    return false;
                }
            }

            generateSnapshotElementsBase(sd);
            return true;
        }

    }
}