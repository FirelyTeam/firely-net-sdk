#define DETECT_RECURSION
// #define MAX_PATH_DEPTH

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

namespace Hl7.Fhir.Specification.Snapshot
{


    public sealed class SnapshotGenerator
    {
        // public const string CHANGED_BY_DIFF_EXT = "http://hl7.org/fhir/StructureDefinition/changedByDifferential";
        public static readonly string CHANGED_BY_DIFF_EXT = "http://hl7.org/fhir/StructureDefinition/changedByDifferential";

        private ArtifactResolver _resolver;
        private SnapshotGeneratorSettings _settings;
#if DETECT_RECURSION
        private SnapshotRecursionChecker _recursionChecker = new SnapshotRecursionChecker();
#endif

        public SnapshotGenerator(ArtifactResolver resolver, SnapshotGeneratorSettings settings)
        {
            if (resolver == null) throw Error.ArgumentNull("resolver");
            if (settings == null) throw Error.ArgumentNull("settings");
            _resolver = resolver;
            _settings = settings;
        }

        public SnapshotGenerator(ArtifactResolver resolver) : this(resolver, SnapshotGeneratorSettings.Default) { }

        private SnapshotGenerator Clone()
        {
            return new SnapshotGenerator(_resolver, _settings);
        }

        /// <summary>
        /// (Re-)generate the <see cref="StructureDefinition.Snapshot"/> component of the specified <see cref="StructureDefinition"/> instance.
        /// Expand the <see cref="StructureDefinition.Differential"/> component, merge base profile and type profiles.
        /// </summary>
        /// <param name="structure">A <see cref="StructureDefinition"/> instance.</param>
        public void Generate(StructureDefinition structure)
        {
            var expanded = Expand(structure);
            structure.Snapshot = new StructureDefinition.SnapshotComponent() { Element = expanded };
        }

        /// <summary>
        /// Expand the <see cref="StructureDefinition.Differential"/> component, merge base profile and type profiles.
        /// Returns the expanded element list.
        /// The given <see cref="StructureDefinition"/> instance is not modified.
        /// </summary>
        /// <param name="structure">A <see cref="StructureDefinition"/> instance.</param>
        public List<ElementDefinition> Expand(StructureDefinition structure)
        {
            if (structure == null)
            {
                throw Error.ArgumentNull("structure");
            }
            if (string.IsNullOrEmpty(structure.Url))
            {
                throw Error.Argument("structure", "The specified StructureDefinition resource has no canonical url.");
            }

#if DETECT_RECURSION
            _recursionChecker.StartExpansion(structure.Url);
            List<ElementDefinition> result = null;
            try
            {
                result = expand(structure);
            }
            finally
            {
                // On complete expansion, the recursion stack should be empty
                Debug.Assert(_recursionChecker.IsCompleted || result == null);
                _recursionChecker.FinishExpansion();
            }
#else
            result = expand(structure);
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
                throw Error.Argument("element", "The element to expand is not included in the given element list.");
            }

            expandElement(nav);

            return nav.Elements;
        }

        private void merge(ElementDefinitionNavigator snapNav, ElementDefinitionNavigator diffNav)
        {
            var snapPos = snapNav.Bookmark();
            var diffPos = diffNav.Bookmark();

            try
            {
                var matches = (new ElementMatcher()).Match(snapNav, diffNav);

                // Debug.WriteLine("Matches for children of " + snapNav.Path + (snapNav.Current != null && snapNav.Current.Name != null ? " '" + snapNav.Current.Name + "'" : null));
                // matches.DumpMatches(snapNav, diffNav);

                foreach (var match in matches)
                {
                    if (!snapNav.ReturnToBookmark(match.BaseBookmark))
                        throw Error.InvalidOperation("Internal merging error: bookmark '{0}' in snap is no longer available", match.BaseBookmark);
                    if (!diffNav.ReturnToBookmark(match.DiffBookmark))
                        throw Error.InvalidOperation("Internal merging error: bookmark '{0}' in diff is no longer available", match.DiffBookmark);

                    if (match.Action == ElementMatcher.MatchAction.Add)
                    {
                        // Add new slice after the last existing slice in base profile
                        snapNav.MoveToLastSlice();
                        var lastSlice = snapNav.Bookmark();

                        // Initialize slice by duplicating base slice entry
                        snapNav.ReturnToBookmark(match.BaseBookmark);
                        snapNav.DuplicateAfter(lastSlice);
                        // Important: explicitly clear the slicing node in the copy!
                        snapNav.Current.Slicing = null;

                        markChange(snapNav.Current);

                        // Merge differential
                        mergeElement(snapNav, diffNav);

                    }
                    else if (match.Action == ElementMatcher.MatchAction.Merge)
                    {
                        mergeElement(snapNav, diffNav);
                    }
                    else if (match.Action == ElementMatcher.MatchAction.Slice)
                    {
                        makeSlice(snapNav, diffNav);
                    }
                }
            }
            finally
            {
                snapNav.ReturnToBookmark(snapPos);
                diffNav.ReturnToBookmark(diffPos);
            }
        }


        private void mergeElement(ElementDefinitionNavigator snap, ElementDefinitionNavigator diff)
        {
            if (_settings.ExpandTypeProfiles)
            {
                ExpandTypeProfiles(snap, diff);
            }

            // [WMR 20160720] Changed, use SnapshotGeneratorSettings
            // (new ElementDefnMerger(_markChanges)).Merge(snap.Current, diff.Current);
            (new ElementDefnMerger(_settings.MarkChanges)).Merge(snap.Current, diff.Current);

            if (diff.HasChildren)
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
                        throw Error.InvalidOperation("Differential has a constraint on a choice element '{0}', but does so without using a type slice", diff.Path);
                    }

                    expandElement(snap);

                    if (!snap.HasChildren)
                    {
                        // Snapshot's element turns out not to be expandable, so we can't move to the desired path
                        throw Error.InvalidOperation("Differential has nested constraints for node '{0}', but this is a leaf node in base", diff.Path);
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
        // If ExpandTypeProfiles is enabled, then first merge custom type profile before merging base
        private void ExpandTypeProfiles(ElementDefinitionNavigator snap, ElementDefinitionNavigator diff)
        {
            var primaryDiffType = diff.Current.Type.FirstOrDefault();
            if (primaryDiffType == null || primaryDiffType.Code == FHIRDefinedType.Reference)
            {
                return;
            }

            var primarySnapType = snap.Current.Type.FirstOrDefault();
            if (primarySnapType == null)
            {
                return;
            }

            var primaryDiffTypeProfile = primaryDiffType.Profile.FirstOrDefault();
            var primarySnapTypeProfile = primarySnapType.Profile.FirstOrDefault();
            if (string.IsNullOrEmpty(primaryDiffTypeProfile) || primaryDiffTypeProfile == primarySnapTypeProfile)
            {
                return;
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

            // [WMR 20160809] TODO: Prevent recursion
            // primaryDiffTypeProfile could point to the original StructureDef itself...
            // Or to a parent element type that references itself
            // Need to keep a stack of already expanding url's...

            var baseStructure = _resolver.GetStructureDefinition(primaryDiffTypeProfile);
            if (baseStructure == null)
            {
                if (!_settings.IgnoreUnresolvedProfiles)
                {
                    // throw Error.NotSupported("Trying to navigate down a node that has a declared type profile of '{0}', which is unknown".FormatWith(primaryDiffTypeProfile));
                    throw Error.ResourceReferenceNotFoundException(
                        primaryDiffTypeProfile,
                        "Unresolved profile reference. Cannot locate the type profile for element '{0}'.\r\nProfile url = '{1}'".FormatWith(diff.Path, primaryDiffTypeProfile)
                    );
                }
                // Otherwise silently ignore and continue expansion
                Debug.Print("Warning! Unresolved type profile for element '{0}' with url '{1}' - continue expansion...".FormatWith(diff.Path, primaryDiffTypeProfile));
            }
            else
            {

#if DETECT_RECURSION
                // Detect endless recursion
                // Verify that the type profile is not already being expanded by a parent call higher up the call stack hierarchy
                _recursionChecker.OnBeforeExpandType(primaryDiffTypeProfile, diff.Path);
#endif

                if (baseStructure.Snapshot == null)
                {
                    if (_settings.ExpandExternalProfiles)
                    {
                        // Automatically expand external profiles on demand
                        Debug.Print("Recursively expand external profile with url: '{0}' ...".FormatWith(baseStructure.Url));
                        Clone().Generate(baseStructure);
                    }
                    else if (!_settings.IgnoreUnresolvedProfiles)
                    {
                        throw Error.NotSupported("Resolved profile for url '{0}' does not contain a snapshot representation.".FormatWith(primaryDiffTypeProfile));
                    }
                    // Otherwise silently ignore and continue expansion
                    Debug.Print("Warning! Resolved profile for url '{0}' has no snapshot - continue expansion...".FormatWith(primaryDiffTypeProfile));
                }
                else
                {
                    // Clone and rebase
                    baseStructure = (StructureDefinition)baseStructure.DeepCopy();

                    var rebasePath = diff.Path;
                    if (profileRef.IsComplex)
                    {
                        rebasePath = ElementDefinitionNavigator.GetParentPath(rebasePath);
                    }
                    baseStructure.Snapshot.Rebase(rebasePath);

                    generateBaseElements(baseStructure.Snapshot.Element);
                    var baseNav = new ElementDefinitionNavigator(baseStructure.Snapshot.Element);

                    if (!profileRef.IsComplex)
                    {
                        baseNav.MoveToFirstChild();
                    }
                    else
                    {
                        if (!baseNav.JumpToNameReference(profileRef.ElementName))
                        {
                            throw Error.InvalidOperation("Type profile '{0}' has an invalid name reference. The base profile does not contain an element with name '{1}'".FormatWith(primaryDiffTypeProfile, profileRef.ElementName));
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
                        (new ElementDefnMerger(_settings.MarkChanges)).Merge(snap.Current, baseNav.Current);
                    }
                }

#if DETECT_RECURSION
                _recursionChecker.OnAfterExpandType(primaryDiffTypeProfile);
#endif

            }
        }

        // [WMR 20160720] NEW
        private void fixExtensionUrl(ElementDefinitionNavigator nav)
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

        private void makeSlice(ElementDefinitionNavigator snap, ElementDefinitionNavigator diff)
        {
            // diff is now located at the first repeat of a slice, which is normally the slice entry (Extension slices need
            // not have a slicing entry)
            // snap is located at the base definition of the element that will become sliced. But snap is not yet sliced.

            // Before we start, is the base element sliceable?
            if (!snap.Current.IsRepeating() && !snap.Current.IsChoice())
                throw Error.InvalidOperation("The slicing entry in the differential at '{0}' indicates an slice, but the base element is not a repeating or choice element",
                   diff.Current.Path);

            ElementDefinition slicingEntry = diff.Current;

            //Mmmm....no slicing entry in the differential. This is only alloweable for extension slices, as a shorthand notation.
            if (slicingEntry.Slicing == null)
            {
                if (slicingEntry.IsExtension())
                {
                    // In this case we create a "prefab" extension slice (with just slincing info)
                    // that's simply merged with the original element in base
                    slicingEntry = createExtensionSlicingEntry();
                }
                else
                {
                    throw Error.InvalidOperation("The slice group at '{0}' does not start with a slice entry element", diff.Current.Path);
                }
            }

            // [WMR 20160720] Changed, use SnapshotGeneratorSettings
            // (new ElementDefnMerger(_markChanges)).Merge(snap.Current, slicingEntry);
            (new ElementDefnMerger(_settings.MarkChanges)).Merge(snap.Current, slicingEntry);

            ////TODO: update / check the slice entry's min/max property to match what we've found in the slice group
        }


        private static ElementDefinition createExtensionSlicingEntry()
        {
            // Create a pre-fab extension slice, filled with sensible defaults
            return new ElementDefinition()
            {
                Slicing = new ElementDefinition.SlicingComponent()
                {
                    Discriminator = new[] { "url" },
                    Ordered = false,
                    Rules = ElementDefinition.SlicingRules.Open
                }
            };
        }

        private List<ElementDefinition> expand(StructureDefinition structure)
        {
            List<ElementDefinition> result;
            var differential = structure.Differential;
            if (differential == null)
            {
                throw Error.Argument("structure", "structure does not contain a differential specification");
            }

            // [WMR 20160718] Also accept extension definitions (IsConstraint == false)
            if (!structure.IsConstraint && !structure.IsExtension)
            {
                throw Error.Argument("structure", "structure is not a constraint or extension");
            }

            if (structure.Base == null)
            {
                throw Error.Argument("structure", "structure is a constraint, but no base has been specified");
            }

            var baseStructure = _resolver.GetStructureDefinition(structure.Base);

            if (baseStructure == null)
            {
                // throw Error.InvalidOperation("Could not locate the base StructureDefinition for url " + structure.Base);
                throw Error.ResourceReferenceNotFoundException(
                    structure.Base,
                    "Unresolved profile reference. Cannot locate the base profile with url '{0}'".FormatWith(structure.Base)
                );
            }

            if (baseStructure.Snapshot == null)
            {
                if (_settings.ExpandExternalProfiles)
                {
                    // Automatically expand external profiles on demand
                    Debug.Print("Recursively expand base profile with url: '{0}' ...".FormatWith(baseStructure.Url));
                    Clone().Generate(baseStructure);
                }
                else
                {
                    throw Error.InvalidOperation("Resolved base profile for url '{0}' does not contain a snapshot representation.", structure.Base);
                }
            }

            var snapshot = (StructureDefinition.SnapshotComponent)baseStructure.Snapshot.DeepCopy();
            generateBaseElements(snapshot.Element);
            var snapNav = new ElementDefinitionNavigator(snapshot.Element);

            // Fill out the gaps (mostly missing parents) in the differential representation
            var fullDifferential = new DifferentialTreeConstructor(differential.Element).MakeTree();
            var diffNav = new ElementDefinitionNavigator(fullDifferential);

            merge(snapNav, diffNav);

            result = snapNav.ToListOfElements();
            return result;
        }

        // internal static bool expandElement(ElementNavigator nav, ArtifactResolver resolver, SnapshotGeneratorSettings settings)
        internal bool expandElement(ElementDefinitionNavigator nav)
        {
            if (nav.Current == null)
            {
                throw Error.ArgumentNull("Navigator is not positioned on an element");
            }

            if (nav.HasChildren) return true;     // already has children, we're not doing anything extra

            var defn = nav.Current;

            if (!String.IsNullOrEmpty(defn.NameReference))
            {
                var sourceNav = new ElementDefinitionNavigator(nav);
                var success = sourceNav.JumpToNameReference(defn.NameReference);

                if (!success)
                    throw Error.InvalidOperation("Trying to navigate down a node that has a nameReference of '{0}', which cannot be found in the StructureDefinition".FormatWith(defn.NameReference));

                nav.CopyChildren(sourceNav);
            }
            else if (defn.Type != null && defn.Type.Count > 0)
            {
                if (defn.Type.Count > 1)
                {
                    throw new NotSupportedException("Element at path '{0}' has a choice of types, cannot expand".FormatWith(nav.Path));
                }
                else
                {
                    // [WMR 20160720] Handle custom type profiles (GForge #9791)
                    var primaryType = defn.Type[0];
                    if (!primaryType.Code.HasValue)
                    {
                        // Element has no type code, cannot expand...
                        return false;
                    }

                    var typeCode = primaryType.Code.Value;
                    var typeProfile = primaryType.Profile.FirstOrDefault();
                    StructureDefinition baseStructure = null;
                    if (!defn.IsExtension() && !defn.IsReference() && !string.IsNullOrEmpty(typeProfile) && _settings.ExpandTypeProfiles)
                    {
                        // Try to resolve the custom element type profile reference
                        baseStructure = _resolver.GetStructureDefinition(typeProfile);
                        //if ((baseStructure == null || baseStructure.Snapshot == null) && _settings.IgnoreUnresolvedTypeProfiles)
                        //{
                        //    // Ignore unresolved external type profile reference; expand the underlying standard core type
                        //    baseStructure = _resolver.GetStructureDefinitionForCoreType(typeCode);
                        //}
                        if (baseStructure == null)
                        {
                            if (_settings.IgnoreUnresolvedProfiles)
                            {
                                // Ignore unresolved external type profile reference; expand the underlying standard core type
                                baseStructure = _resolver.GetStructureDefinitionForCoreType(typeCode);
                            }
                        }
                    }
                    else
                    {
                        baseStructure = _resolver.GetStructureDefinitionForCoreType(typeCode);
                    }

                    if (baseStructure == null)
                    {
                        // throw Error.NotSupported("Trying to navigate down a node that has a declared base type of '{0}', which is unknown".FormatWith(typeCode));
                        if (typeProfile != null)
                        {
                            throw Error.ResourceReferenceNotFoundException(
                                typeProfile,
                                "Unresolved profile reference. Cannot locate the element type profile for url '{0}'".FormatWith(typeProfile)
                            );
                        }
                        else
                        {
                            throw Error.NotSupported("Unresolved element type. Cannot locate the profile for element type '{0}'.".FormatWith(typeCode));
                        }
                    }

                    if (baseStructure.Snapshot == null)
                    {
                        if (_settings.ExpandExternalProfiles)
                        {
                            // Automatically expand external profiles on demand
                            Debug.Print("Recursively expand type profile with url: '{0}' ...".FormatWith(baseStructure.Url));
                            Clone().Generate(baseStructure);
                        }
                        else if (typeProfile != null)
                        {
                            throw Error.NotSupported("Resolved profile for url '{0}' does not contain a snapshot representation.".FormatWith(typeProfile));
                        }
                        else
                        {
                            throw Error.NotSupported("Resolved profile for type '{0}' does not contain a snapshot representation.".FormatWith(typeCode));
                        }
                    }

                    generateBaseElements(baseStructure.Snapshot.Element);
                    var sourceNav = new ElementDefinitionNavigator(baseStructure.Snapshot.Element);
                    sourceNav.MoveToFirstChild();
                    nav.CopyChildren(sourceNav);
                }
            }

            return true;
        }

        private void markChange(Element snap)
        {
            // [WMR 20160720] Changed, use SnapshotGeneratorSettings
            // if (_markChanges)
            if (_settings.MarkChanges)
            {
                snap.SetExtension(CHANGED_BY_DIFF_EXT, new FhirBoolean(true));
            }
        }

        private void generateBaseElements(IEnumerable<ElementDefinition> elements)
        {
            // [WMR 20160805] NEW
            // Base path should be derived from the original profile that introduces the element definition
            // e.g. Patient.id => Base.path = "Resource.id"
            // Issue: in DSTU2, core resource definitions (in profiles-resources.xml) don't specify ANY base components
            // Suggestion: core resource definitions should specify Base component for all elements inherited from (Domain)Resource
            // For now, we could hard-code the elements derived from (Domain)Resource

            // Inspect root element to determine base type (Element/Resource/DomainResource)
            var rootElem = elements.FirstOrDefault();
            var primaryTypeCode = rootElem.PrimaryTypeCode();
            var baseTypeDefs = getBaseTypeDefs(primaryTypeCode);

            foreach (var element in elements)
            {
                if (_settings.RewriteElementBase || element.Base == null)
                {
                    var baseElem = element;
                    if (_settings.NormalizeElementBase)
                    {
                        var elemName = element.GetNameFromPath();
                        baseElem = baseTypeDefs.SelectMany(sd => sd.Snapshot.Element).FirstOrDefault(e => e.GetNameFromPath() == elemName) ?? element;
                    }

                    element.Base = new ElementDefinition.BaseComponent()
                    {
                        MaxElement = (FhirString)baseElem.MaxElement.DeepCopy(),
                        MinElement = (Integer)baseElem.MinElement.DeepCopy(),
                        PathElement = (FhirString)baseElem.PathElement.DeepCopy()
                    };

                    //element.Base = new ElementDefinition.BaseComponent()
                    //{
                    //    MaxElement = (FhirString)element.MaxElement.DeepCopy(),
                    //    MinElement = (Integer)element.MinElement.DeepCopy(),
                    //    PathElement = (FhirString)element.PathElement.DeepCopy()
                    //};

                }
            }
        }

        private StructureDefinition[] getBaseTypeDefs(FHIRDefinedType? type)
        {
            var result = new Stack<StructureDefinition>();
            while (type.HasValue)
            {
                var sd = _resolver.GetStructureDefinitionForCoreType(type.Value);
                if (sd == null)
                {
                    throw Error.InvalidOperation("Cannot resolve core profile for type '{0}'".FormatWith(type.Value));
                }
                result.Push(sd);
                type = sd.Snapshot.Element.FirstOrDefault().PrimaryTypeCode();
            }
            return result.ToArray();
        }

        //[Conditional("MAX_PATH_DEPTH")]
        //private void VerifyMaxPathDepth(string path)
        //{
        //    const int maxDepth = 3;
        //    Debug.Print("VerifyMaxPathDepth: '{0}' : level {1}".FormatWith(nav.Path, nav.Path.Count(c => c == '.')));
        //    var depth = path.Count(c => c == '.');
        //    if (depth > maxDepth)
        //    {
        //        throw Error.InvalidOperation("Invalid operation. Snapshot expansion of element '{0}' has exceeded the maximum path depth ({1}).".FormatWith(path, maxDepth));
        //    }
        //}

    }
}
