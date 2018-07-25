/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

// Cache pre-generated snapshot root ElementDefinition instance as an annotation on the associated differential root ElementDefinition
// When subsequently expanding the full type profile snapshot, re-use the cached root ElementDefinition instance
// This ensures that the snapshot ElementDefinition instances are stable (and equal to OnPrepareBaseProfile event parameters)
// Without caching, the root element in the type profile snapshot would be a different instance as the reference specified by the OnPrepareBaseProfile event
#define CACHE_ROOT_ELEMDEF
// [WMR 20161004] Only enable this for debugging & verification (costly...)
// #define CACHE_ROOT_ELEMDEF_ASSERT

// TODO:
// - Merge global StructureDefinition.mapping definitions
// - Enforce/verify Slicing.Rule = Closed / OpenAtEnd
// - Test error handling: gracefully handle invalid input, report issue

// [WMR 20170216] Prevent slices on non-repeating non-choice type elements (max = 1)
// #define REJECT_SLICE_NONREPEATING_ELEMENT

// [WMR 20180409] Resolve contentReference from core resource/datatype (StructureDefinition.type)
#define FIX_CONTENTREFERENCE

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Specification.Snapshot
{
    // [WMR 20160927] Design considerations:
    // Functionality is implemented as a set of partial classes, in order to share common state and
    // minimize memory pressure while recursively generating snapshots of base and/or type profiles.

    public sealed partial class SnapshotGenerator
    {
        // TODO: Properly test SnapshotGenerator for multi-threading support
        // Each thread should create a separate instance
        // But all instances should share a common IResourceResolver instance
        // TODO: Probably also need to share a common recursion stack...

        readonly IResourceResolver _resolver;
        readonly SnapshotGeneratorSettings _settings;
        readonly SnapshotRecursionStack _stack = new SnapshotRecursionStack();

        /// <summary>
        /// Create a new instance of the <see cref="SnapshotGenerator"/>
        /// for the specified resource resolver and configuration settings.
        /// </summary>
        /// <param name="resolver">A <see cref="IResourceResolver"/> instance.</param>
        /// <exception cref="ArgumentNullException">The specified argument is <c>null</c>.</exception>
        public SnapshotGenerator(IResourceResolver resolver)
        {
            _resolver = verifySource(resolver, nameof(resolver));
            _settings = SnapshotGeneratorSettings.CreateDefault();
        }

        /// <summary>
        /// Create a new instance of the <see cref="SnapshotGenerator"/>
        /// for the specified resource resolver and configuration settings.
        /// </summary>
        /// <param name="resolver">A <see cref="IResourceResolver"/> instance.</param>
        /// <param name="settings">A <see cref="SnapshotGeneratorSettings"/> instance.</param>
        /// <exception cref="ArgumentNullException">One of the specified arguments is <c>null</c>.</exception>
        public SnapshotGenerator(IResourceResolver resolver, SnapshotGeneratorSettings settings)
        {
            _resolver = verifySource(resolver, nameof(resolver));
            if (settings == null) { throw Error.ArgumentNull(nameof(settings)); }
            // [WMR 20171023] Always copy the specified settings, to prevent shared state
            // Especially important to prevent corruption of the global SnapshotGeneratorSettings.Default instance.
            _settings = new SnapshotGeneratorSettings(settings);
        }

        static IResourceResolver verifySource(IResourceResolver resolver, string name = null)
        {
            if (resolver == null) { throw Error.ArgumentNull(name ?? nameof(resolver)); }
            if (resolver is SnapshotSource) { throw Error.Argument(name ?? nameof(resolver), $"Invalid argument. Cannot create a new {nameof(SnapshotGenerator)} instance from an existing {nameof(SnapshotSource)}."); }

            // TODO: Verify that the specified resolver is idempotent (i.e. caching)
            // Maybe add some interface property to detect behavior?
            // i.e. IsIdemPotent (repeated calls return same instance)
            // Note: cannot add new property to IResourceResolver, breaking change...
            // Alternatively, add new secondary interface IResourceResolverProperties

            return resolver;
        }

        /// <summary>Returns a reference to the associated <see cref="IResourceResolver"/> instance, as specified in the call to the constructor.</summary>
        public IResourceResolver Source => _resolver;

        /// <summary>Returns the snapshot generator configuration settings.</summary>
        public SnapshotGeneratorSettings Settings => _settings;

        /// <summary>Returns a reference to the profile uri of the currently generating snapshot, or <c>null</c>.</summary>
        string CurrentProfileUri => _stack.CurrentProfileUri;

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

            // [WMR 20170209] TODO: also merge global StructureDefinition.Mapping components
            // structure.Mappings = ElementDefnMerger.Merge(...)
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
                throw Error.Argument(nameof(structure), "Invalid argument. The specified StructureDefinition has no url. Each StructureDefinition must have a unique canonical url, for identification purposes.");
            }

            // Clear the OperationOutcome issues
            clearIssues();

            List<ElementDefinition> result = null;
            _stack.OnBeforeGenerateSnapshot(structure.Url);
            try
            {
                result = generate(structure);
            }
            finally
            {
                // On complete expansion, the recursion stack should be empty
                Debug.Assert(result == null || _stack.RecursionDepth == 1);
                _stack.OnAfterGenerateSnapshot(structure.Url);
            }
            return result;
        }

        /// <summary>Recursively expand (the children of) a single element definition.</summary>
        /// <param name="elements">A <see cref="StructureDefinition.SnapshotComponent"/> or <see cref="StructureDefinition.DifferentialComponent"/> instance.</param>
        /// <param name="element">The element to expand. Should be part of <paramref name="elements"/>.</param>
        /// <returns>A new, expanded list of <see cref="ElementDefinition"/> instances.</returns>
        /// <exception cref="ArgumentException">The specified element is not contained in the list.</exception>
        public IList<ElementDefinition> ExpandElement(IElementList elements, ElementDefinition element)
        {
            if (elements == null) { throw Error.ArgumentNull(nameof(elements)); }
            return ExpandElement(elements.Element, element);
        }

        /// <summary>Recursively expand (the children of) a single element definition.</summary>
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
            _stack.OnStartRecursion();
            try
            {
                expandElement(nav);
            }
            finally
            {
                _stack.OnFinishRecursion();
            }
            return nav.Elements;
        }

        /// <summary>Recursively expand (the children of) a single element definition.</summary>
        /// <param name="nav">An <see cref="ElementDefinitionNavigator"/> instance positioned on the target element to be expanded.</param>
        /// <returns><c>true</c> if the element is succesfully expanded, or <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentException">The specified navigator is not positioned on an element.</exception>
        public bool ExpandElement(ElementDefinitionNavigator nav)
        {
            if (nav == null) { throw Error.ArgumentNull(nameof(nav)); }
            if (nav.Current == null) { throw Error.Argument(nameof(nav), "The specified navigator is not positioned on an element."); }

            clearIssues();

            // Must initialize recursion checker, because element expansion may recurse on external type profile
            _stack.OnStartRecursion();
            try
            {
                return expandElement(nav);
            }
            finally
            {
                _stack.OnFinishRecursion();
            }
        }

        // ***** Private Interface *****


        /// <summary>
        /// Expand the differential component of the specified structure and return the expanded element list.
        /// The given structure is not modified.
        /// </summary>
        List<ElementDefinition> generate(StructureDefinition structure)
        {
            Debug.WriteLine($"[{nameof(SnapshotGenerator)}.{nameof(generate)}] Generate snapshot for profile '{structure.Name}' : '{structure.Url}' (#{structure.GetHashCode()}) ...");

            List<ElementDefinition> result;
            var differential = structure.Differential;
            if (differential == null)
            {
                // [WMR 20161208] Handle missing differential
                differential = structure.Differential = new StructureDefinition.DifferentialComponent()
                {
                    Element = new List<ElementDefinition>()
                };
            }

            // [WMR 20160718] Also accept extension definitions (IsConstraint == false)
            if (structure.IsConstraint && structure.Base == null)
            {
                throw Error.Argument(nameof(structure), "Invalid argument. The specified StructureDefinition represents a constraint on another FHIR profile, but the base profile url is missing or empty. Derived profiles must have a base url.");
            }

            ElementDefinitionNavigator nav;
            // StructureDefinition.SnapshotComponent snapshot = null;
            if (structure.Base != null)
            {
                var baseStructure = _resolver.FindStructureDefinition(structure.Base);

                // [WMR 20161208] Handle unresolved base profile
                if (baseStructure == null)
                {
                    addIssueProfileNotFound(structure.Base);
                    // Fatal error...
                    return null;
                }

                // [WMR 20161208] Handle missing differential
                var location = differential.Element.Count > 0 ? differential.Element[0].ToNamedNode() : null;
                if (!ensureSnapshot(baseStructure, structure.Base, location))
                {
                    // Fatal error...
                    return null;
                }

                // [WMR 20160817] Notify client about the resolved, expanded base profile
                OnPrepareBaseProfile(structure, baseStructure);

                var snapshot = (StructureDefinition.SnapshotComponent)baseStructure.Snapshot.DeepCopy();

                // [WMR 20160902] Rebase the cloned base profile (e.g. DomainResource)
                if (!structure.IsConstraint)
                {
                    var rootElem = differential.Element.FirstOrDefault();
                    if (rootElem != null)
                    {
                        if (!rootElem.IsRootElement())
                        {
                            // Fatal error...
                            throw Error.Argument(nameof(structure), $"Invalid argument. The specified StructureDefinition defines a new model (not a constraint on another profile), but the differential component does not start at the root element definition.");
                        }
                        snapshot.Rebase(rootElem.Path);
                    }
                }

                // Ensure that ElementDefinition.Base components in base StructureDef are propertly initialized
                // Always regenerate Base component! Cannot reuse cloned values
                // ensureBaseComponents(snapshot.Element, structure.Base, true);
                // [WMR 20170424] WRONG! Must inherit existing base components
                ensureBaseComponents(snapshot.Element, structure.Base, false);

                // [WMR 20170208] Moved to *AFTER* ensureBaseComponents - emits annotations...
                // [WMR 20160915] Derived profiles should never inherit the ChangedByDiff extension from the base structure
                snapshot.Element.RemoveAllConstrainedByDiffExtensions();
                snapshot.Element.RemoveAllConstrainedByDiffAnnotations();

                // Notify observers
                for (int i = 0; i < snapshot.Element.Count; i++)
                {
                    OnPrepareElement(snapshot.Element[i], baseStructure, baseStructure.Snapshot.Element[i]);
                }

                nav = new ElementDefinitionNavigator(snapshot.Element, structure);
            }
            else
            {
                // No base; input is a core resource or datatype definition
                var snapshot = new StructureDefinition.SnapshotComponent();
                nav = new ElementDefinitionNavigator(snapshot.Element, structure);
            }

            // var nav = new ElementDefinitionNavigator(snapshot.Element);

#if CACHE_ROOT_ELEMDEF
            _stack.RegisterSnapshotNavigator(structure.Url, nav);
#endif

            // Fill out the gaps (mostly missing parents) in the differential representation
            var fullDifferential = (new DifferentialTreeConstructor()).MakeTree(differential.Element);
            var diff = new ElementDefinitionNavigator(fullDifferential);

            merge(nav, diff);

            result = nav.ToListOfElements();

            // [WMR 20160917] NEW: Re-generate all ElementId values
            if (_settings.GenerateElementIds)
            {
                generateElementsId(result, true);
            }

            return result;
        }

        /// <summary>
        /// Expand the currently active element within the specified <see cref="ElementDefinitionNavigator"/> instance.
        /// If the element has a name reference, then merge from the targeted element.
        /// Otherwise, if the element has a custom type profile, then merge it.
        /// </summary>
        bool expandElement(ElementDefinitionNavigator nav)
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

#if FIX_CONTENTREFERENCE
                // [WMR 20180409] NEW
                // Resolve contentReference from core resource/datatype definition
                // Specified by StructureDefinition.type == root element name

                var coreStructure = getStructureForContentReference(nav, true);
                // getStructureForContentReference emits issue if profile cannot be resolved
                if (coreStructure == null) { return false; }

                var sourceNav = ElementDefinitionNavigator.ForSnapshot(coreStructure);
#else
                // [WMR 20180409] WRONG!
                // Resolves contentReference from current StructureDef
                // Recursive child items should NOT inherit constraints from parent in same profile
                var sourceNav = new ElementDefinitionNavigator(nav);
#endif

                if (!sourceNav.JumpToNameReference(defn.NameReference))
                {
                    addIssueInvalidNameReference(defn);
                    return false;
                }
                nav.CopyChildren(sourceNav);

                // [WMR 20180410]
                // - Regenerate element IDs
                // - Notify subscribers by calling OnPrepareBaseElement, before merging diff constraints
                prepareExpandedTypeProfileElements(nav, sourceNav);

            }
            else if (defn.Type == null || defn.Type.Count == 0)
            {
                // Element has neither a name reference or a type...?
                if (!nav.Current.IsRootElement())
                {
                    addIssueNoTypeOrNameReference(defn);
                    return false;
                }
            }
            else if (defn.Type.Count > 1)
            {
                // [WMR 20170227] NEW: Allow common extension constraints on choice type w/o type slice

                // Ewout: allow if all the specified type references share a common type code
                // WMR: Only expand common elements, i.e. .id | .extension | .modifierExtension
                // Also verify that diff only specifies child constraints on common elements (.extension | .modifierExtension) ... ?
                // Actually, we should determine the intersection of the specified type profiles... ouch

                var distinctTypeCodes = defn.Type.Select(t => t.Code).Distinct().ToList();
                if (distinctTypeCodes.Count == 1)
                {
                    // Different profiles for common base type => expand the common base type (w/o custom profile)
                    var typeRef = new ElementDefinition.TypeRefComponent() { Code = distinctTypeCodes[0] };
                    StructureDefinition typeStructure = getStructureForTypeRef(defn, typeRef, true);
                    return expandElementType(nav, typeStructure);
                }
                // Alternatively, we could try to expand the most specific common base profile, e.g. (Backbone)Element
                // TODO: Determine the intersection, i.e. the most specific common type that all types are derived from

                // [WMR 20170321] WRONG! Differential is allowed to repeat the element types defined by the base profile
                // Here, we don't have access to the associated base type, so we cannot reliable determine if the types are valid
                // => Don't expand element and return true to continue processing; caller is responsible for validating the actual types
                //else
                //{
                //    addIssueInvalidChoiceConstraint(defn);
                //    return false;
                //}
            }

            else // if (defn.Type.Count == 1)
            {
                // [WMR 20160720] Handle custom type profiles (GForge #9791)
                StructureDefinition typeStructure = getStructureForElementType(defn, true);

                return expandElementType(nav, typeStructure);
            }

            return true;
        }

        bool expandElementType(ElementDefinitionNavigator nav, StructureDefinition typeStructure)
        {
            // [WMR 20170208] TODO: Expand profile snapshot if necessary
            if (typeStructure != null && typeStructure.HasSnapshot)
            {
                var typeSnap = typeStructure.Snapshot;
                var typeNav = new ElementDefinitionNavigator(typeSnap.Element);
                if (!typeNav.MoveToFirstChild())
                {
                    addIssueProfileHasNoSnapshot(nav.Current.ToNamedNode(), typeStructure.Url);
                }
                // [WMR 20170208] NEW - Move common logic to separate method, also used by mergeTypeProfiles

                // [WMR 20170220] If profile element has no children, then copy child elements from type structure into profile
                if (!copyChildren(nav, typeNav, typeStructure))
                {
                    // Otherwise merge type structure onto profile elements (cf. mergeTypeProfiles)

                    // [WMR 20170220] Can this happen?
                    // TODO: Create unit test to trigger this situation
                    Debug.Fail($"[{nameof(SnapshotGenerator)}.{nameof(expandElementType)}] TODO...");

                    // [WMR 20170220] WRONG...?
                    // Must merge nav on top of typeNav, not the other way around...
                    mergeElement(nav, typeNav);

                    // 1. Fully expand the snapshot of the external type profile
                    // 2. Clone, rebase and copy children into referencing profile below the referencing parent element
                    // 2. On top of that, merge base profile constraints (taken from the snapshot)
                    // 3. On top of that, merge profile differential constraints

                    // mergeElementDefinition(snap.Current, diffElem);
                }
                return true;
            }
            return false;
        }

        /// <summary>Merge children of the currently selected element from differential into snapshot.</summary>
        void merge(ElementDefinitionNavigator snap, ElementDefinitionNavigator diff)
        {
            var snapPos = snap.Bookmark();
            var diffPos = diff.Bookmark();

            try
            {
                var matches = ElementMatcher.Match(snap, diff);

                // Debug.WriteLine($"Matches for children of {(snap.Path ?? "/")} '{(snap.Current?.Name ?? snap.Current?.Type.FirstOrDefault()?.Profile.FirstOrDefault() ?? snap.Current?.Type.FirstOrDefault()?.Code.GetLiteral())}'");
                // matches.DumpMatches(snap, diff);

                foreach (var match in matches)
                {
                    // Navigate to the matched elements
                    if (!snap.ReturnToBookmark(match.BaseBookmark))
                    {
                        throw Error.InvalidOperation($"Internal error in snapshot generator ({nameof(merge)}): bookmark '{match.BaseBookmark}' in snap is no longer available");
                    }
                    if (!diff.ReturnToBookmark(match.DiffBookmark))
                    {
                        throw Error.InvalidOperation($"Internal error in snapshot generator ({nameof(merge)}): bookmark '{match.DiffBookmark}' in diff is no longer available");
                    }

                    // Collect any reported issue
                    if (match.Issue != null)
                    {
                        addIssue(match.Issue);
                    }

                    // Process the match, depending on the result
                    switch (match.Action)
                    {
                        case ElementMatcher.MatchAction.Merge:
                            mergeElement(snap, diff);
                            break;
                        case ElementMatcher.MatchAction.Add:
                            addSlice(snap, diff, match.SliceBase);
                            break;
                        case ElementMatcher.MatchAction.Slice:
                            startSlice(snap, diff, match.SliceBase);
                            break;
                        case ElementMatcher.MatchAction.New:
                            // No matching base element; this is a new element definition
                            // snap is positioned at the associated parent element
                            createNewElement(snap, diff);
                            break;
                        case ElementMatcher.MatchAction.Invalid:
                            // Collect issue and ignore invalid element
                            break;
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

                mergeElementDefinition(newElement, diff.Current, true);

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
            var isMerged = true;
            var diffElem = diff.Current;
            var isRoot = diffElem.IsRootElement();

            ElementDefinition cachedRootElemDef = null;
            if (isRoot && (cachedRootElemDef = diffElem.GetSnapshotElementAnnotation()) != null)
            {

#if CACHE_ROOT_ELEMDEF_ASSERT
                // DEBUG / VERIFY: merge results should be equal to cached ElemDef instance
                isValid = mergeTypeProfiles(snap, diff);
                mergeElementDefinition(snap.Current, elem);
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

                // Debug.Print($"[{nameof(SnapshotGenerator)}.{nameof(mergeElement)}] Re-use cached root element definition: '{cachedRootElemDef.Path}'  #{cachedRootElemDef.GetHashCode()}");
                snap.Elements[snap.OrdinalPosition.Value] = cachedRootElemDef;
                diffElem.RemoveSnapshotElementAnnotations();
            }
            else
            {
                // First merge constraints from element type profile, if it exists
                // [WMR 20161004] Remove configuration setting; always merge type profiles
                // if (_settings.MergeTypeProfiles) 
                // [WMR 20170714] Can safely skip this step for the root node
                if (!isRoot)
                {
                    isMerged = mergeTypeProfiles(snap, diff);
                }

                // Then merge constraints from base profile
                mergeElementDefinition(snap.Current, diffElem, true);
            }
#else
            // First merge constraints from element type profile, if it exists
            var isValid = true;
            isValid = mergeTypeProfiles(snap, diff);

            // Then merge constraints from base profile
            mergeElementDefinition(snap.Current, diffElem);

#endif

            if (!isMerged)
            {
                // [WMR 20160905] If we failed to merge the type profile, then don't try to expand & merge any child constraints
            }
            else if (mustExpandElement(diff))
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

                    // [WMR 20170227] REDUNDANT; checked by expandElement
                    //if (snap.Current.Type.Count > 1)
                    //{
                    //    addIssueInvalidChoiceConstraint(snap.Current);
                    //    return;
                    //}

                    if (!expandElement(snap))
                    {
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
        }

        // [WMR 20170105] New: determine wether to expand the current element
        // Notify client to allow overriding the default behavior
        bool mustExpandElement(ElementDefinitionNavigator diffNav)
        {
            var hasChildren = diffNav.HasChildren;
            bool mustExpand = hasChildren;
            OnBeforeExpandElement(diffNav.Current, hasChildren, ref mustExpand);
            return mustExpand;
        }

        /// <summary>Merge a differential ElementDefinition constraint into a snapshot ElementDefinition instance.</summary>
        /// <param name="snap"></param>
        /// <param name="diff"></param>
        /// <param name="mergeElementId">Determines if the snapshot should inherit Element.id values from the differential.</param>
        void mergeElementDefinition(ElementDefinition snap, ElementDefinition diff, bool mergeElementId)
        {

            // [WMR 20170421] Add parameter to control when (not) to inherit Element.id
            ElementDefnMerger.Merge(this, snap, diff, mergeElementId);
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

        static readonly string DomainResource_Extension_Path = ModelInfo.FhirTypeToFhirTypeName(FHIRDefinedType.DomainResource) + ".extension";

        // Resolve the type profile of the currently selected element and merge into snapshot
        bool mergeTypeProfiles(ElementDefinitionNavigator snap, ElementDefinitionNavigator diff)
        {
            // Debug.Print("[mergeTypeProfiles] {0} : {1}", diff.Path, diff.Current.Type != null && diff.Current.Type.Count == 1 ? diff.Current.PrimaryTypeCode() : null);

            // [WMR 20160913] Possible improvements:
            // Don't recurse on special terminator elements, e.g. id, url, value
            // Note that all these element definitions are marked with: <representation value="xmlAttr"/>

            var primaryDiffType = diff.Current.PrimaryType();
            if (primaryDiffType != null && !primaryDiffType.IsReference())
            {

                var primarySnapType = snap.Current.PrimaryType();
                // if (primarySnapType == null) { return true; }

                var primaryDiffTypeProfile = primaryDiffType.Profile.FirstOrDefault();

                // [WMR 20170208] Ignore explicit diff profile if it matches the (implied) base type profile
                // e.g. if the differential specifies explicit core type profile url
                // Example: Patient.identifier type = { Code : Identifier, Profile : "http://hl7.org/fhir/StructureDefinition/Identifier" } }
                var primarySnapTypeProfile = primarySnapType.TypeProfile();

                if (string.IsNullOrEmpty(primaryDiffTypeProfile) || primaryDiffTypeProfile == primarySnapTypeProfile) { return true; }

                // [WMR 20160721] NEW: Handle type profiles with name references
                // e.g. profile "http://hl7.org/fhir/StructureDefinition/qicore-adverseevent"
                // Extension element "cause" => "http://hl7.org/fhir/StructureDefinition/qicore-adverseevent-cause"
                // Constraint on extension child element "certainty" => "http://hl7.org/fhir/StructureDefinition/qicore-adverseevent-cause#certainty"
                // This means: 
                // - First inherit child element constraints from extension definition, element with name "certainty"
                // - Then override inherited constraints by explicit element constraints in profile differential

                var profileRef = ProfileReference.Parse(primaryDiffTypeProfile);
                if (profileRef.IsComplex)
                {
                    primaryDiffTypeProfile = profileRef.CanonicalUrl;
                }

                var typeStructure = _resolver.FindStructureDefinition(primaryDiffTypeProfile);

                // [WMR 20170224] Verify that the resolved StructureDefinition is compatible with the element type
                // [WMR 20170823] WRONG! Base element may specify multiple type options
                //if (!_resolver.IsValidTypeProfile(primarySnapType.Code, typeStructure))
                //{
                //    addIssueInvalidProfileType(diff.Current, typeStructure);
                //    return false;
                //}

                // The element type profile constraint must match at least one base type
                var isCompatible = snap.Current.Type.Any(t => _resolver.IsValidTypeProfile(t.Code, typeStructure));
                if (!isCompatible)
                {
                    addIssueInvalidProfileType(diff.Current, typeStructure);
                    // [WMR 20170823] Emit warning, but continue
                    // return false;
                }

                var diffNode = diff.Current.ToNamedNode();

                // [WMR 20170207] Notify observers, allow event subscribers to force expansion (even if no diff constraints)
                // Note: if the element is to be expanded, then always merge full snapshot of the external type profile (!)
                if (mustExpandElement(diff))  // if (diff.HasChildren)
                {
                    if (!ensureSnapshot(typeStructure, primaryDiffTypeProfile, diffNode))
                    {
                        return false;
                    }

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

                        // [WMR 20170208] Update ElementDefinition.Base components
                        // ensureBaseComponents(typeNav, snap, true);

                        // [WMR 20170321] HACK: Never copy elements names from the root element (e.g. SimpleQuantity)
                        if (typeNav.Current.NameElement != null)
                        {
                            Debug.WriteLine($"[{nameof(SnapshotGenerator)}.{nameof(mergeTypeProfiles)}] Explicitly prevent copying of root element name: {typeNav.Path} : '{typeNav.Current.Name}'");
                            typeNav.Current.Name = null;
                        }

                    }
                    else
                    {
                        if (!typeNav.JumpToNameReference(profileRef.ElementName))
                        {
                            addIssueInvalidProfileNameReference(snap.Current, profileRef.ElementName, primaryDiffTypeProfile);
                            return false;
                        }
                    }

                    // [WMR 20170321] Handle element renaming
                    // diff can rename choice type element, e.g. Observation.valueQuantity
                    // snap may contain the original element paths, e.g. Observation.value[x]
                    // In that case, diff overrides snap; i.e. snap should also be renamed
                    // => First renamed snap before merging diff
                    if (diff.PathName != snap.PathName)
                    {
                        Debug.WriteLine($"[{nameof(SnapshotGenerator)}.{nameof(mergeTypeProfiles)}] Rename snapshot element(s): {snap.Path} => '{diff.Path}'");
                        Debug.Assert(!snap.HasChildren);
                        snap.Current.Path = diff.Path;
                    }

                    // [WMR 20170208] Merge order is important!
                    // Profile may specify inline constraints to override aspects of the external type profile
                    // 1. Fully expand the snapshot of the external type profile
                    // 2. Clone, rebase and copy children into referencing profile below the referencing parent element
                    // 2. On top of that, merge base profile constraints (taken from the snapshot)
                    // 3. On top of that, merge profile differential constraints
                    // Example:
                    //   diff (element type profile constraint) : Patient.identifier::type = { Identifier, "http://example.org/fhir/StructureDefinition/MyCustomIdentifier" }
                    //   snap (default type from base profile)  : Patient.identifier::type = { Identifier }
                    //   typeNav (Identifier root element type) : Patient.identifier::type = { Element }

                    // [WMR 20170501] Must handle two different situations:
                    // 1. Element type is NOT expanded in the base profile
                    //    => Expand now by calling copyChildren
                    // 2. Element (base) type IS expanded in the base profile, i.e. base profile has child elements
                    //    => call mergeElement to merge diff (derived) type profile onto snapshot (base) type profile
                    copyChildren(snap, typeNav, typeStructure);

                    // But we also need to merge external type profile onto any existing inline snapshot constraints
                    // e.g. TestObservationProfileWithExtensions(_ExpandAll)

                    // [WMR 20170428] ISSUE
                    // typeNav refers to type Snapshot, e.g. { Address Snap + MyAddress Diff }
                    // snap may already include Address Snap + Diff
                    // We need to determine { Address Snap + Diff + MyAddress Diff }
                    // But this performs { Address Snap + Diff + Address Snap (WRONG!) + MyAddress Diff }
                    // i.e. any overriding diff constraints from base snapshot are reverted back to original Address constraints
                    // Gets even more complicated with higher order derived base/type profiles...

                    mergeElement(snap, typeNav);    // Merge result back into profile

                    // Now call prepareTypeProfileElements (below) to clear element IDs and notify event subscribers
                }
                else
                {
                    // Expand and merge (only!) the root element of the external type profile
                    // Note: full expansion may trigger recursion, e.g. Element.id => identifier => string => Element
                    var typeRootElem = getSnapshotRootElement(typeStructure, primaryDiffTypeProfile, diffNode);
                    if (typeRootElem == null) { return false; }

                    // Rebase before merging
                    var rebasedRootElem = (ElementDefinition)typeRootElem.DeepCopy();
                    rebasedRootElem.Path = diff.Path;

                    // Merge the type profile root element; no need to expand children
                    mergeElementDefinition(snap.Current, rebasedRootElem, false);
                }

                // [WMR 20170209] Remove invalid annotations after merging an extension definition
                fixExtensionAnnotationsAfterMerge(snap.Current);

                // [WMR 20170711]
                // - Regenerate element IDs (NOT inherited from external rebased element type profiles!)
                // - Notify subscribers by calling OnPrepareBaseElement, before merging diff constraints
                prepareMergedTypeProfileElements(snap, typeStructure);
            }

            return true;
        }

        // [WMR 20170209] HACK
        // Problem: DomainResource.extension defines some default values for Short, Definition & Comments:
        // <element>
        //   <path value="DomainResource.extension"/>
        //   <short value="Additional Content defined by implementations"/>
        //   <definition value="May be used to represent additional information that is not part of the basic definition of the resource. In order to make the use of extensions safe and manageable, there is a strict set of governance  applied to the definition and use of extensions. Though any implementer is allowed to define an extension, there is a set of requirements that SHALL be met as part of the definition of the extension."/>
        //   <comments value="There can be no stigma associated with the use of extensions by any application, project, or standard - regardless of the institution or jurisdiction that uses or defines the extensions.  The use of extensions is what allows the FHIR specification to retain a core level of simplicity for everyone."/>
        //   <!-- ... -->
        // </element>
        // These properties may be overriden when we merge the external extension definition into the profile (by extension root element property values)
        // By default, the ElementDefnMerger annotates overridden profile properties to indicate they have been constrained.
        // However these properties are not constrained by the referencing profile itself, but inherited from the referenced extension definition.
        // So we actually should NOT emit these annotations on the referencing profile properties.
        // Call this method after merging an external extension definition to remove incorrect annotations from the target profile extension element
        static void fixExtensionAnnotationsAfterMerge(ElementDefinition elem)
        {
            if (IsEqualPath(elem.Base?.Path, DomainResource_Extension_Path))
            {
                elem.ShortElement?.RemoveConstrainedByDiffAnnotation();
                elem.CommentsElement?.RemoveConstrainedByDiffAnnotation();
                elem.DefinitionElement?.RemoveConstrainedByDiffAnnotation();
            }
        }

        /// <summary>
        /// Copy child elements from <paramref name="typeNav"/> to <paramref name="nav"/>.
        /// Remove existing annotations, fix Base components, notify listeners.
        /// </summary>
        bool copyChildren(ElementDefinitionNavigator nav, ElementDefinitionNavigator typeNav, StructureDefinition typeStructure)
        {
            // [WMR 20170220] CopyChildren returns false if nav already has children
            if (nav.CopyChildren(typeNav))
            {
                // Fix the copied elements and notify observers
                var pos = nav.OrdinalPosition.Value;
                for (int i = 1; i < typeNav.Elements.Count; i++)
                {
                    // [WMR 20170220] Problem: nav and typeNav structure can differ
                    // e.g. both can have separate extensions, slices etc.

                    var elem = nav.Elements[pos + i];
                    var typeElem = typeNav.Elements[i];

                    // [WMR 20160826] Never inherit Changed extension from base profile!
                    elem.RemoveAllConstrainedByDiffExtensions();
                    elem.RemoveAllConstrainedByDiffAnnotations();

                    // [WMR 20160902] Initialize empty ElementDefinition.Base components if necessary
                    // [WMR 20160906] Always regenerate! Cannot reuse cloned base components
                    // elem.EnsureBaseComponent(elem, true);
                    // [WMR 20170424] WRONG! Inherit base components from type profile!
                    elem.EnsureBaseComponent(typeElem, false);

                    OnPrepareElement(elem, typeStructure, typeElem);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Process child element definitions inherited from a merged external element type profile.
        /// For each element, raise the <see cref="OnPrepareElement(ElementDefinition, StructureDefinition, ElementDefinition)"/> event
        /// and ensure that the element id is assigned.
        /// </summary>
        void prepareMergedTypeProfileElements(ElementDefinitionNavigator snap, StructureDefinition typeProfile)
        {
            // [EK 20170802] Code removed for DSTU2
            // Recursively re-generate IDs for elements inherited from external rebased type profile
            //if (_settings.GenerateElementIds)
            //{
            //    ElementIdGenerator.Update(snap, true);
            //}

            if (MustRaisePrepareElement)
            {
                var elems = snap.Elements;
                var parentPath = snap.Path;
                var start = snap.OrdinalPosition.Value;
                for (int i = start; i < elems.Count && (i == start || ElementDefinitionNavigator.IsChildPath(parentPath, elems[i].Path)); i++)
                {
                    var elem = elems[i];

                    // Important! Must clone the current snapshot element to create a separate base instance
                    var baseElem = (ElementDefinition)elem.DeepCopy();

                    // Inform subscribers about the prepared merged base element
                    // nav.Current now represents the merged base element, including
                    // constraints from base profile and external element type profile.
                    // Next we are going to merge profile diff constraints.
                    OnPrepareElement(elem, typeProfile, baseElem);
                }
            }
        }

        // [WMR 20170713] NEW - for expandElementType
        // Raise OnPrepareElement event and provide matching base elements from typeNav
        // Cannot use prepareMergedTypeProfileElements, as the provided base element is incorrect in this case
        void prepareExpandedTypeProfileElements(ElementDefinitionNavigator snap, ElementDefinitionNavigator typeNav)
        {
            // [EK 20170802] Code removed for DSTU2
            // Recursively re-generate IDs for elements inherited from external rebased type profile
            //if (_settings.GenerateElementIds)
            //{
            //    ElementIdGenerator.Update(snap, true);
            //}

            if (MustRaisePrepareElement)
            {
                Debug.Assert(typeNav.StructureDefinition != null);
                prepareExpandedElementsInternal(snap, typeNav, false);
            }
        }

        // [WMR 20170718] NEW - for addSlice
        void prepareSliceElements(ElementDefinitionNavigator snap, ElementDefinitionNavigator sliceBase)
        {
            if (MustRaisePrepareElement)
            {
                prepareExpandedElementsInternal(snap, sliceBase, true);
            }
        }

        // Raise OnPrepareElement event for all elements in snap subtree
        // Recurse all elements and find matching base element in typeNav
        void prepareExpandedElementsInternal(ElementDefinitionNavigator snap, ElementDefinitionNavigator typeNav, bool prepareRoot)
        {
            Debug.Assert(MustRaisePrepareElement);

            // [WMR 20170718] Note: typeProfile is null when called from addSlice / prepareSliceElements 
            var typeProfile = typeNav.StructureDefinition;

            // [WMR 20170718] Explicitly process the (relative) root element
            // ElementMatcher only operates on children
            // Necessary for addSlice, but NOT for expandElementType
            if (prepareRoot)
            {
                OnPrepareElement(snap.Current, typeProfile, typeNav.Current);
            }

            // Optimization
            if (typeNav.HasChildren)
            {
                var snapPos = snap.Bookmark();
                var typePos = typeNav.Bookmark();

                try
                {
                    var matches = ElementMatcher.Match(snap, typeNav);

                    // Debug.WriteLine($"Type profile matches for children of {(snap.Path ?? "/")} '{(snap.Current?.SliceName ?? snap.Current?.Type.FirstOrDefault()?.Profile ?? snap.Current?.Type.FirstOrDefault()?.Code)}'");
                    // matches.DumpMatches(snap, typeNav);

                    foreach (var match in matches)
                    {
                        // Navigate to the matched elements
                        if (!snap.ReturnToBookmark(match.BaseBookmark))
                        {
                            throw Error.InvalidOperation($"Internal error in snapshot generator ({nameof(merge)}): bookmark '{match.BaseBookmark}' in snap is no longer available");
                        }
                        if (!typeNav.ReturnToBookmark(match.DiffBookmark))
                        {
                            throw Error.InvalidOperation($"Internal error in snapshot generator ({nameof(merge)}): bookmark '{match.DiffBookmark}' in typeNav is no longer available");
                        }

                        // Collect any reported issue
                        if (match.Issue != null)
                        {
                            addIssue(match.Issue);
                        }

                        // Process the match, depending on the result
                        switch (match.Action)
                        {
                            case ElementMatcher.MatchAction.Merge:
                                OnPrepareElement(snap.Current, typeProfile, typeNav.Current);
                                break;
                            case ElementMatcher.MatchAction.Add:
                                // var sliceBase = match.SliceBase?.Current ?? createExtensionSlicingEntry(snap.Current);
                                Debug.Assert(match.SliceBase?.Current != null);
                                OnPrepareElement(snap.Current, typeProfile, match.SliceBase.Current);
                                break;
                            case ElementMatcher.MatchAction.Slice:
                                // For extensions, match.SliceBase may be null (slice entry is implicit and may be omitted)
                                var sliceBase = match.SliceBase?.Current ?? createExtensionSlicingEntry(snap.Current);
                                OnPrepareElement(snap.Current, typeProfile, sliceBase);
                                break;
                            case ElementMatcher.MatchAction.New:
                                // No matching base element; this is a new element definition
                                // snap is positioned at the associated parent element
                                OnPrepareElement(snap.Current, null, null);
                                break;
                            case ElementMatcher.MatchAction.Invalid:
                                // Collect issue and ignore invalid element
                                break;
                        }

                        // Recurse children
                        if (snap.HasChildren)
                        {
                            prepareExpandedElementsInternal(snap, typeNav, true);
                        }

                    }
                }
                finally
                {
                    snap.ReturnToBookmark(snapPos);
                    typeNav.ReturnToBookmark(typePos);
                }
            }
        }

        static void fixExtensionUrl(ElementDefinitionNavigator nav)
        {
            var extElem = nav.Current;
            if (extElem.IsExtension() && nav.HasChildren)
            {
                // Resolve the canonical url of the extension definition from type[0]/profile[0]
                var profile = extElem.PrimaryTypeProfile();
                if (profile != null)
                {
                    var snapExtPos = nav.Bookmark();
                    if (nav.MoveToChild("url"))
                    {
                        var urlElem = nav.Current;
                        if (urlElem != null && urlElem.Fixed == null)
                        {
                            urlElem.Fixed = new FhirUri(profile);
                        }
                        nav.ReturnToBookmark(snapExtPos);
                    }
                }
            }
        }

        void startSlice(ElementDefinitionNavigator snap, ElementDefinitionNavigator diff, ElementDefinitionNavigator sliceBase)
        {
            // diff is now located at the first repeat of a slice, which is normally the slice entry
            // (Extension slices need not have a slicing entry)
            // snap is located at the base definition of the element that will become sliced. But snap is not yet sliced.

            // Before we start, is the base element sliceable?
            // [WMR 20170216] Accept slices on non-repeating non-choice elements
            // Ewout: no reason to reject; e.g. derived profile can limit sliced base element cardinality to 0...1
            // Vadim: may introduce issues for code generators...
#if REJECT_SLICE_NONREPEATING_ELEMENT
            if (!snap.Current.IsRepeating() && !snap.Current.IsChoice())
            {
                var location = getSliceLocation(diff, diff.Current) ?? snap.Current;
                addIssueInvalidSlice(location);
                return;
            }
#endif

            // Note: slicing introduction element may be missing from differential => null
            ElementDefinition slicingEntry = diff.Current;

            // Mmmm....no slicing entry in the differential. This is only alloweable for extension slices, as a shorthand notation.
            if (slicingEntry == null || slicingEntry.Slicing == null)
            {
                // if (slicingEntry.IsExtension())
                if (snap.Current.IsExtension())
                {
                    // In this case we create a "prefab" extension slice (with just slicing info)
                    // that's simply merged with the original element in base
                    slicingEntry = createExtensionSlicingEntry(snap.Current);
                }
                else
                {
                    // Slicing entry is missing from diff
                    // Note: ElementMatcher will still try to match diff slices to base slices
                    var location = getSliceLocation(diff, slicingEntry);
                    addIssueMissingSliceEntry(location);
                    return;
                }
            }

            // [WMR 20161219] Handle Composition.section - has default name 'section' in core resource (name reference target for Composition.section.section)
            // Ambiguous in DSTU2... usually this would introduce a reslice of named slice 'section'
            // Note that STU3 introduces contentReference
            //
            //   Base           Diff          Operation
            //   --------------------------------------
            //   'section'      'section'     Slice
            //                  'mySection'   Add
            // 
            // => If slicing entry has no name, or same name as current slice, then merge with current snap; otherwise add
            if (slicingEntry.Name != null && ElementDefinitionNavigator.IsSiblingSliceOf(snap.Current.Name, slicingEntry.Name))
            {
                // Append the new slice constraint to the existing slice group
                if (sliceBase != null)
                {
                    addSliceBase(snap, diff, sliceBase);
                }
                else
                {
                    var lastSlice = findSliceAddPosition(snap, diff);
                    snap.DuplicateAfter(lastSlice);
                }
            }

            if (slicingEntry != null)
            {
                if (diff.Current == null || diff.Current.IsExtension())
                {
                    // Merge newly created slicing entry onto snap
                    // [WMR 20170421] Don't merge element Id from slice entry
                    mergeElementDefinition(snap.Current, slicingEntry, false);
                }
                else
                {
                    // [WMR 20161222] Recursively merge diff constraints on slicing entry and child elements (if any)
                    mergeElement(snap, diff);
                }
            }

            // TODO: update / check the slice entry's min/max property to match what we've found in the slice group
        }

        static ElementDefinition getSliceLocation(ElementDefinitionNavigator diff, ElementDefinition location)
        {
            if (location == null)
            {
                var bm = diff.Bookmark();
                if (diff.MoveToNextSliceAtAnyLevel())
                {
                    location = diff.Current;
                    diff.ReturnToBookmark(bm);
                }
            }
            return location;
        }

        // diff is positioned on a new element constraint in a slicing group
        // snap is positioned on the matching base element
        // - first element in (re)slicing group
        // - contains slicing component (always present in snapshot)
        // Requirement: diff (re)slicing constraints must be in same order as base!

        // Example 1: base is unsliced, diff defines new slices and reslices
        //
        //   Base     Diff       Operation
        //   -----------------------------
        //   ''       ''         Slice
        //            'A'        Add
        //            'A/1'      Add
        //            'A/2'      Add
        //            'B'        Add
        //
        // Example 2: base is sliced, diff defines new slices and reslices
        //
        //   Base     Diff       Operation
        //   -----------------------------
        //   ''
        //   'A'      'A'        Slice
        //            'A/1'      Add
        //            'A/1/1'    Add
        //            'A/1/2'    Add
        //            'A/2'      Add
        //   'B'      'B'        Merge
        //   'C'                 
        //            'D'        Add
        // 
        // Example 3: base is resliced, diff defines new slices and reslices
        //
        //   Base     Diff       Operation
        //   -----------------------------
        //   ''       ''         Slice
        //   'A'      
        //   'A/1'    'A/1'      Slice
        //            'A/1/1'    Add
        //            'A/1/2'    Add
        //   'A/2'
        //            'A/1/3'    Add
        //   'C'
        //

        void addSlice(ElementDefinitionNavigator snap, ElementDefinitionNavigator diff, ElementDefinitionNavigator sliceBase)
        {
            // Debug.Print($"[{nameof(SnapshotGenerator)}.{nameof(addSlice)}] Base Path = '{snap.Path}' Base Slice Name = '{snap.Current.Name}' Diff Slice Name = {sliceName}");

            // base has no name => diff is new slice; add after last existing slice (if any)
            // base is named => diff is new reslice; add after last existing reslice (if any)

            // Debug.Assert(diff.Current.Name != null);
            Debug.Assert(snap.Current.Name == null
                // [WMR 20161219] Handle Composition.section - has default name 'section' in core resource (name reference target for Composition.section.section)
                || IsEqualPath(snap.Path, "Composition.section")
                || diff.Current.Name == null
                || ElementDefinitionNavigator.IsDirectResliceOf(diff.Current.Name, snap.Current.Name));

            // Append the new slice constraint to the existing slice group
            // Slice definitions in StructureDef are always ordered! (only instances may contain unordered slices)
            // diff must specify constraints on existing slices in original order (just like regular elements)
            // diff can only append new slices after all constraints on existing slices
            addSliceBase(snap, diff, sliceBase);

            // [WMR 20161219] Handle invalid multiple renamed choice type constraints, e.g. { valueString, valueInteger }
            // Snapshot base element has already been renamed by the first match => re-assign
            if (!IsEqualPath(snap.PathName, diff.PathName))
            {
                snap.Current.Path = diff.Current.Path;
            }

            // Important: explicitly clear the slicing node in the copy!
            Debug.Assert(snap.Current.Slicing == null); // Taken care of by ElementMatcher.initSliceBase
                                                        // snap.Current.Slicing = null;

            // [WMR 20170718] NEW
            // Update base annotations for named slices
            // MUST replace existing annotations copied from sliceBase!
            // Named slices should get base with Min = 0
            prepareSliceElements(snap, sliceBase);

            // Notify clients about a snapshot element with differential constraints
            OnConstraint(snap.Current);

            // Merge differential
            mergeElement(snap, diff);

        }

        static void addSliceBase(ElementDefinitionNavigator snap, ElementDefinitionNavigator diff, ElementDefinitionNavigator sliceBase)
        {
            if (sliceBase == null || sliceBase.Current == null)
            {
                Debug.Fail("SHOULDN'T HAPPEN...");
                throw Error.InvalidOperation($"Internal error in snapshot generator ({nameof(addSlice)}): slice base element is unavailable.");
            }

            var lastSlice = findSliceAddPosition(snap, diff);
            bool result = snap.ReturnToBookmark(lastSlice);
            // Copy the original (unmerged) slice base element to snapshot
            if (result) { result = snap.InsertAfter((ElementDefinition)sliceBase.Current.DeepCopy()); }
            // Recursively copy the original (unmerged) child elements, if necessary
            if (result && sliceBase.HasChildren)
            {
                // Always copy all the existing constraints on child elements to snapshot ?
                // if (mustExpandElement(diff))
                // {
                result = snap.CopyChildren(sliceBase);
                // }
            }

            if (!result)
            {
                throw Error.InvalidOperation($"Internal error in snapshot generator ({nameof(addSlice)}): cannot add slice '{diff.Current}' to snapshot.");
            }
        }

        // Search snapshot slice group for suitable position to add new diff slice
        // If the snapshot contains a matching base slice element, then append after reslice group
        // Otherwise append after last slice
        static Bookmark findSliceAddPosition(ElementDefinitionNavigator snap, ElementDefinitionNavigator diff)
        {
            var bm = snap.Bookmark();
            var name = snap.PathName;
            var sliceName = diff.Current.Name;
            //Debug.Assert(sliceName != null);
            var baseSliceName = ElementDefinitionNavigator.GetBaseSliceName(sliceName);
            do
            {
                var snapSliceName = snap.Current.Name;
                if (baseSliceName != null && IsEqualName(baseSliceName, snapSliceName))
                {
                    // Found a matching base slice; skip any children and reslices
                    var bm2 = snap.Bookmark();
                    while (snap.MoveToNext(name))
                    {
                        var snapSliceName2 = snap.Current.Name;
                        if (!ElementDefinitionNavigator.IsResliceOf(snapSliceName2, snapSliceName))
                        {
                            // Not a reslice; add diff slice after the previous match
                            break;
                        }
                        bm2 = snap.Bookmark();
                    }
                    snap.ReturnToBookmark(bm2);
                    break;
                }
            } while (snap.MoveToNext(name));
            var result = snap.Bookmark();
            snap.ReturnToBookmark(bm);
            return result;
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
            if (_settings.GenerateExtensionsOnConstraints)
            {
                element.SetConstrainedByDiffExtension();
            }
            if (_settings.GenerateAnnotationsOnConstraints)
            {
                element.SetConstrainedByDiffAnnotation();
            }
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
            var location = elementDef.ToNamedNode();
            StructureDefinition baseStructure = null;

            // [WMR 20160720] Handle custom type profiles (GForge #9791)
            bool isValidProfile = false;

            // First try to resolve the custom element type profile, if specified
            var typeProfile = typeRef.Profile.FirstOrDefault();
            
            // [WMR 20161004] Remove configuration setting; always merge type profiles
            if (!string.IsNullOrEmpty(typeProfile) && !typeRef.IsReference()) // && _settings.MergeTypeProfiles
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
                // [WMR 20160906] Check if element type equals path (e.g. Resource root element), prevent infinite recursion
                isValidProfile = (IsEqualPath(typeName, location.Location)) ||
                    (
                        ensureSnapshot
                        ? this.ensureSnapshot(baseStructure, typeName, location)
                        : this.verifyStructure(baseStructure, typeName, location)
                    );

            }

            return baseStructure;
        }

#if FIX_CONTENTREFERENCE
        // [WMR 20180410] Resolve the defining target structure of a contentReference
        StructureDefinition getStructureForContentReference(ElementDefinitionNavigator nav, bool ensureSnapshot)
        {
            Debug.Assert(nav != null);
            Debug.Assert(nav.Current != null);

            var elementDef = nav.Current;
            var location = elementDef.ToNamedNode();

            var contentReference = elementDef.NameReference; // e.g. "group"

            var coreType = nav.StructureDefinition?.ConstrainedType.GetLiteral()
                // Fall back to root element name...?
                ?? ElementDefinitionNavigator.GetPathRoot(elementDef.Path);

            if (!string.IsNullOrEmpty(coreType))
            {
                // Try to resolve the custom element type profile reference
                var coreSd = _resolver.FindStructureDefinitionForCoreType(coreType);
                var isValidProfile = ensureSnapshot
                    ? this.ensureSnapshot(coreSd, coreType, location)
                    : this.verifyStructure(coreSd, coreType, location);
                return coreSd;
            }

            return null;
        }
#endif

        bool verifyStructure(StructureDefinition sd, string profileUrl, IElementNavigator location = null)
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
        bool ensureSnapshot(StructureDefinition sd, string profileUri, IElementNavigator location = null)
        {
            if (!verifyStructure(sd, profileUri, location)) { return false; }
            profileUri = sd.Url;

            // Detect infinite recursion
            // Verify that the type profile is not already being expanded by a parent call higher up the call stack hierarchy
            // Special case: when recursing on Element, simply return true and continue; otherwise throw an exception
            var path = location != null ? location.Location : null;
            _stack.OnBeforeExpandTypeProfile(profileUri, path);

            try
            {
                if (_settings.GenerateSnapshotForExternalProfiles
                    && (!sd.HasSnapshot || (_settings.ForceRegenerateSnapshots && !sd.Snapshot.IsCreatedBySnapshotGenerator()))
                )
                {
                    // Automatically expand external profiles on demand
                    // Debug.Print($"[{nameof(SnapshotGenerator)}.{nameof(ensureSnapshot)}] Recursively generate snapshot for type profile with url: '{sd.Url}' ...");

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
                _stack.OnAfterExpandTypeProfile(profileUri, path);
            }

            return true;
        }

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
                return getSnapshotRootElement(typeProfile, typeProfile.Url, elementDef.ToNamedNode());
            }
            return null;
        }

        // Returns the snapshot root element of the specified profile
        // Try to resolve from existing snapshot, if it exists and is valid
        // Try to resolve from partial snapshot, if it is currently being generated (higher up on the stack)
        // Otherwise recursively resolve the associated base profile root element definition (if it exists) and merge with differential root
        ElementDefinition getSnapshotRootElement(StructureDefinition sd, string profileUri, IElementNavigator location)
        {
            // Debug.Print("[SnapshotGenerator.getSnapshotRootElement] profileUri = '{0}' - resolving root element definition...", profileUri);
            if (!verifyStructure(sd, profileUri, location)) { return null; }

            if (sd.Differential == null || sd.Differential.Element == null || sd.Differential.Element.Count == 0)
            {
                addIssueProfileHasNoDifferential(location, profileUri);
                return null;
            }

#if CACHE_ROOT_ELEMDEF
            // 1. Return previously generated, cached root element definition, if it exists
            var cachedRoot = sd.GetSnapshotRootElementAnnotation();
            if (cachedRoot != null) { return cachedRoot; }
#endif

            // 2. Return root element definition from existing (pre-generated) snapshot, if it exists
            if (sd.HasSnapshot && (sd.Snapshot.IsCreatedBySnapshotGenerator() || !_settings.ForceRegenerateSnapshots))
            {
                // Debug.Print($"[{nameof(SnapshotGenerator)}.{nameof(getSnapshotRootElement)}] {nameof(profileUri)} = '{profileUri}' - use existing root element definition from snapshot: #{sd.Snapshot.Element[0].GetHashCode()}");
                // No need to save root ElemDef annotation, as the snapshot has already been fully expanded
                return sd.Snapshot.Element[0];
            }

            // 3. Try to resolve root element definition from currently generating (partial) snapshot on recursion stack
            // If the recursion stack contains the target profile, then the snapshot root element has already been generated
            var nav = _stack.ResolveSnapshotNavigator(sd.Url);
            if (nav != null && nav.Elements != null && nav.Elements.Count > 0)
            {
                var recursiveRootElemDef = nav.Elements[0];
                // Debug.Print($"[{nameof(SnapshotGenerator)}.{nameof(getSnapshotRootElement)}] {nameof(profileUri)} = '{profileUri}' - use existing root element definition from generating snapshot: #{recursiveRootElemDef.GetHashCode()}");
                // No need to save root ElemDef annotation, as the snapshot root element has already been expanded
                return recursiveRootElemDef;
            }

            // 4. We still need to expand the root element definition
            // Resolve root element definition from base profile and merge differential constraints (recursively)
            var diffRoot = sd.Differential.Element[0];

            var baseProfileUri = sd.Base;
            if (baseProfileUri == null)
            {
                // Structure has no base, i.e. core type definition => differential introduces & defines the root element
                // No need to rebase, nothing to merge
                var clonedDiffRoot = (ElementDefinition)diffRoot.DeepCopy();
#if CACHE_ROOT_ELEMDEF
                clonedDiffRoot.EnsureBaseComponent(null, true);
                sd.SetSnapshotRootElementAnnotation(clonedDiffRoot);
#endif
                // Debug.Print($"[{nameof(SnapshotGenerator)}.{nameof(getSnapshotRootElement)}] {nameof(profileUri)} = '{profileUri}' - use root element definition from differential: #{clonedDiffRoot.GetHashCode()}");
                return clonedDiffRoot;
            }

            // Recursively resolve root element definition from base profile

            // TODO: Recursion detection / protection
            // e.g. detect cycle in StructureDefinition.Base references
            // FHIR types and resources have no such cycles, but an attacker could abuse this
            // Note that we need to use a separate stack, as we may need to expand the root element of a
            // profile that is currently being fully expanded, i.e. the url is already on the main stack.

            // Debug.Print($"[{nameof(SnapshotGenerator)}.{nameof(getSnapshotRootElement)}] {nameof(profileUri)} = '{profileUri}' - recursively resolve root element definition from base profile '{baseProfileUri}' ...");
            var sdBase = _resolver.FindStructureDefinition(baseProfileUri);
            var baseRoot = getSnapshotRootElement(sdBase, baseProfileUri, diffRoot.ToNamedNode()); // Recursion!
            if (baseRoot == null)
            {
                addIssueSnapshotGenerationFailed(baseProfileUri);
                return null;
            }

            // Clone and rebase
            var rebasedRoot = (ElementDefinition)baseRoot.DeepCopy();

            if (diffRoot != null)
            {
                rebasedRoot.Path = diffRoot.Path;

                // Merge differential constraints onto base root element definition
                // [WMR 20170421] Merge element Id from differential
                mergeElementDefinition(rebasedRoot, diffRoot, true);
            }

            // Debug.Print($"[{nameof(SnapshotGenerator)}.{nameof(getSnapshotRootElement)}] {nameof(profileUri)} = '{profileUri}' - succesfully resolved root element definition: #{rebasedRoot.GetHashCode()}");

#if CACHE_ROOT_ELEMDEF
            // Save the generated snapshot root element definition as annotation on StructureDefinition.Snapshot component
            // When generating the full snapshot, re-use the previously generated root element definition
            rebasedRoot.EnsureBaseComponent(baseRoot, true);
            sd.SetSnapshotRootElementAnnotation(rebasedRoot);
#endif

            // Notify observers
            OnPrepareElement(rebasedRoot, sdBase, baseRoot);

            return rebasedRoot;
        }

        /// <summary>Determine if the specified element paths are equal. Performs an ordinal comparison.</summary>
        static bool IsEqualPath(string path, string other) => StringComparer.Ordinal.Equals(path, other);

        /// <summary>Determine if the specified element names are equal. Performs an ordinal comparison.</summary>
        static bool IsEqualName(string name, string other) => StringComparer.Ordinal.Equals(name, other);

        /// <summary>Create a fully connected element tree from a sparse (differential) element list by adding missing parent element definitions.</summary>
        /// <returns>A list of elements that represents a fully connected element tree.</returns>
        /// <remarks>This method returns a new list of element definitions. The input elements list is not modified.</remarks>
        public static List<ElementDefinition> ConstructFullTree(List<ElementDefinition> source) => 
            (new DifferentialTreeConstructor()).MakeTree(source);
    }
}