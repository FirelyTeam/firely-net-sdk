#define HANDLE_COMPLEX_REFERENCES

/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Support;
using System.Diagnostics;

namespace Hl7.Fhir.Specification.Snapshot
{
    public class SnapshotGeneratorSettings
    {
        public static readonly SnapshotGeneratorSettings Default = new SnapshotGeneratorSettings()
        {
            MarkChanges = false,
            ExpandTypeProfiles = true,
            IgnoreMissingTypeProfiles = false
        };

        /// <summary>
        /// Mark all elements in the snapshot that are constrained with respect to the base profile.
        /// The snapshot generator will decorate all changed elements with a special extension
        /// (canonical url "http://hl7.org/fhir/StructureDefinition/changedByDifferential").
        /// </summary>
        public bool MarkChanges { get; set; }

        /// <summary>
        /// EXPERIMENTAL!
        /// Enable this setting in order to merge custom element type profiles.
        /// If enabled (default), the snapshot generator first merges constraints from custom type profiles before merging constraints from the base profile.
        /// If disabled, the snapshot generator ignores custom type profiles and merges constraints from the base profile.
        /// </summary>
        /// <remarks>See GForge #9791</remarks>
        public bool ExpandTypeProfiles { get; set; }

        /// <summary>
        /// EXPERIMENTAL!
        /// Enable this setting to ignore unknown or invalid element type profiles.
        /// If disabled (default), throw an exception for unknown or invalid element type profiles.
        /// </summary>
        public bool IgnoreMissingTypeProfiles { get; set; }

    }

    public class SnapshotGenerator
    {
        public const string CHANGED_BY_DIFF_EXT = "http://hl7.org/fhir/StructureDefinition/changedByDifferential";
        
        private ArtifactResolver _resolver;

        // [WMR 20160720] Changed, use SnapshotGeneratorSettings
        // private bool _markChanges;

        //public SnapshotGenerator(ArtifactResolver resolver, bool markChanges=false)
        //{
        //    _resolver = resolver;
        //    _markChanges = markChanges;
        //}

        private SnapshotGeneratorSettings _settings;

        public SnapshotGenerator(ArtifactResolver resolver, SnapshotGeneratorSettings settings)
        {
            if (resolver == null) throw Error.ArgumentNull("resolver");
            if (settings == null) throw Error.ArgumentNull("settings");
            _resolver = resolver;
            _settings = settings;
        }

        public SnapshotGenerator(ArtifactResolver resolver) : this(resolver, SnapshotGeneratorSettings.Default) { }

        public void Generate(StructureDefinition structure)
        {
            if (structure.Differential == null) throw Error.Argument("structure", "structure does not contain a differential specification");

            // [WMR 20160718] Also accept extension definitions (IsConstraint == false)
            if (!structure.IsConstraint && !structure.IsExtension) throw Error.Argument("structure", "structure is not a constraint or extension");

            if(structure.Base == null) throw Error.Argument("structure", "structure is a constraint, but no base has been specified");

            var differential = structure.Differential;

            var baseStructure = _resolver.GetStructureDefinition(structure.Base);

            if (baseStructure == null) throw Error.InvalidOperation("Could not locate the base StructureDefinition for url " + structure.Base);
            if (baseStructure.Snapshot == null) throw Error.InvalidOperation("Snapshot generator required the base at '{0}' to have a snapshot representation", structure.Base);

            var snapshot = (StructureDefinition.SnapshotComponent)baseStructure.Snapshot.DeepCopy();
            generateBaseElements(snapshot.Element);
            var snapNav = new ElementNavigator(snapshot.Element);

            // Fill out the gaps (mostly missing parents) in the differential representation
            var fullDifferential = new DifferentialTreeConstructor(differential.Element).MakeTree();
            var diffNav = new ElementNavigator(fullDifferential);

            merge(snapNav, diffNav);
           
            structure.Snapshot = new StructureDefinition.SnapshotComponent() { Element = snapNav.ToListOfElements() };
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

            var nav = new ElementNavigator(elements);
            if (!nav.MoveTo(element))
            {
                throw Error.Argument("element", "The specified element is not contained in the list.");
            }

            expandElement(nav, _resolver, _settings);

            return nav.Elements;
        }

        private void merge(ElementNavigator snapNav, ElementNavigator diffNav)
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


        private void mergeElement(ElementNavigator snap, ElementNavigator diff)
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

                    expandElement(snap, _resolver, _settings);

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
        private void ExpandTypeProfiles(ElementNavigator snap, ElementNavigator diff)
        {
            // [WMR 20160721] Note that we also try to resolve and expand extension definitions!
            var primaryDiffType = diff.Current.Type.FirstOrDefault();
            if (primaryDiffType != null && primaryDiffType.Code != FHIRDefinedType.Reference)
            {
                var primaryDiffTypeProfile = primaryDiffType.Profile.FirstOrDefault();
                var primarySnapType = snap.Current.Type.FirstOrDefault();
                var primarySnapTypeProfile = primarySnapType != null ? primarySnapType.Profile.FirstOrDefault() : null;
                if (!string.IsNullOrEmpty(primaryDiffTypeProfile) && primaryDiffTypeProfile != primarySnapTypeProfile)
                {
                    // Debug.Print("Path = '{0}' - Merge custom type profile '{1}'".FormatWith(diff.Path, primaryTypeProfile));

                    // cf. ExpandElement

                    // [WMR 20160721] NEW: Handle type profiles with name references
                    // e.g. profile "http://hl7.org/fhir/StructureDefinition/qicore-adverseevent"
                    // Extension element "cause" => "http://hl7.org/fhir/StructureDefinition/qicore-adverseevent-cause"
                    // Constraint on extension child element "certainty" => "http://hl7.org/fhir/StructureDefinition/qicore-adverseevent-cause#certainty"
                    // This means: 
                    // - First inherit child element constraints from extension definition, element with name "certainty"
                    // - Then override inherited constraints by explicit element constraints in profile differential

                    string profileUrl, elementName;
                    var isComplex = IsComplexProfileReference(primaryDiffTypeProfile, out profileUrl, out elementName);
                    if (isComplex)
                    {
                        primaryDiffTypeProfile = profileUrl;
                    }

                    var baseType = _resolver.GetStructureDefinition(primaryDiffTypeProfile);

                    if (baseType != null)
                    {
                        if (baseType.Snapshot != null)
                        {
                            // Clone and rebase
                            baseType = (StructureDefinition)baseType.DeepCopy();

                            var rebasePath = diff.Path;
                            if (isComplex)
                            {
                                rebasePath = ElementNavigator.GetParentPath(rebasePath);
                            }
                            baseType.Snapshot.Rebase(rebasePath);

                            generateBaseElements(baseType.Snapshot.Element);
                            var baseNav = new ElementNavigator(baseType.Snapshot.Element);

                            if (elementName == null)
                            {
                                baseNav.MoveToFirstChild();
                            }
                            else
                            {
                                if (!baseNav.JumpToNameReference(elementName))
                                {
                                    throw Error.InvalidOperation("Found type profile with invalid name reference '{0}' - the base profile does not contain an element with name '{1}'".FormatWith(primaryDiffTypeProfile, elementName));
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
                        else if (!_settings.IgnoreMissingTypeProfiles)
                        {
                            throw Error.NotSupported("Found definition of type profile '{0}', but is does not contain a snapshot representation.".FormatWith(primaryDiffTypeProfile));
                        }
                    }
                    else
                    {
                        Debug.Print("Warning! Unresolved external type profile reference: '{0}' - Ignore, skip expansion...".FormatWith(primaryDiffTypeProfile));
                        if (!_settings.IgnoreMissingTypeProfiles)
                        {
                            // throw Error.NotSupported("Trying to navigate down a node that has a declared type profile of '{0}', which is unknown".FormatWith(primaryDiffTypeProfile));
                            throw Error.ResourceReferenceNotFoundException(
                                primaryDiffTypeProfile,
                                "The profile contains an unresolved reference to an external type profile with url '{0}'".FormatWith(primaryDiffTypeProfile)
                            );
                        }
                    }
                }
            }
        }

        // [WMR 20160720] NEW
        private void fixExtensionUrl(ElementNavigator nav)
        {
            var extElem = nav.Current;
            if (extElem.IsExtension() && nav.HasChildren)
            {
                // Resolve the canonical url of the extension definition from type[0]/profile[0]
                var primaryType = extElem.Type.FirstOrDefault();
                if (primaryType != null)
                {
                    var profile = primaryType.Profile.FirstOrDefault();
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
        }

        private void makeSlice(ElementNavigator snap, ElementNavigator diff)
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

        private void markChange(Element snap)
        {
            // [WMR 20160720] Changed, use SnapshotGeneratorSettings
            // if (_markChanges)
            if (_settings.MarkChanges)
            {
                snap.SetExtension(CHANGED_BY_DIFF_EXT, new FhirBoolean(true));
            }
        }


        private static ElementDefinition createExtensionSlicingEntry()
        {
            // Create a pre-fab extension slice, filled with sensible defaults

            //var slicingDiff = new ElementDefinition();
            //slicingDiff.Slicing = new ElementDefinition.SlicingComponent();
            //slicingDiff.Slicing.Discriminator = new[] { "url" };
            //slicingDiff.Slicing.Ordered = false;
            //slicingDiff.Slicing.Rules = ElementDefinition.SlicingRules.Open;
            // return slicingDiff;

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


        internal static bool expandElement(ElementNavigator nav, ArtifactResolver resolver, SnapshotGeneratorSettings settings)
        {
            if (resolver == null) throw Error.ArgumentNull("source");
            if (nav.Current == null) throw Error.ArgumentNull("Navigator is not positioned on an element");

            if (nav.HasChildren) return true;     // already has children, we're not doing anything extra

            var defn = nav.Current;

            if (!String.IsNullOrEmpty(defn.NameReference))
            {
                var sourceNav = new ElementNavigator(nav);
                var success = sourceNav.JumpToNameReference(defn.NameReference);

                if (!success)
                    throw Error.InvalidOperation("Trying to navigate down a node that has a nameReference of '{0}', which cannot be found in the StructureDefinition".FormatWith(defn.NameReference));

                nav.CopyChildren(sourceNav);
            }
            else if (defn.Type != null && defn.Type.Count > 0)
            {
                if (defn.Type.Count > 1)
                    throw new NotSupportedException("Element at path '{0}' has a choice of types, cannot expand".FormatWith(nav.Path));
                else
                {
                    // [WMR 20160720] Handle custom type profiles (GForge #9791)
                    // var coreType = resolver.GetStructureDefinitionForCoreType(defn.Type[0].Code.Value);
                    var primaryType = defn.Type[0];
                    var typeProfile = primaryType.Profile.FirstOrDefault();
                    StructureDefinition coreType = null;
                    if (!defn.IsExtension() && !defn.IsReference() && !string.IsNullOrEmpty(typeProfile) && settings.ExpandTypeProfiles)
                    {
                        coreType = resolver.GetStructureDefinition(typeProfile);
                        if ((coreType == null || coreType.Snapshot == null) && settings.IgnoreMissingTypeProfiles)
                        {
                            coreType = resolver.GetStructureDefinitionForCoreType(primaryType.Code.Value);
                        }
                    }
                    else
                    {
                        coreType = resolver.GetStructureDefinitionForCoreType(primaryType.Code.Value);
                    }

                    if (coreType == null) throw Error.NotSupported("Trying to navigate down a node that has a declared base type of '{0}', which is unknown".FormatWith(defn.Type[0].Code));
                    if (coreType.Snapshot == null) throw Error.NotSupported("Found definition of base type '{0}', but is does not contain a snapshot representation".FormatWith(defn.Type[0].Code));

                    generateBaseElements(coreType.Snapshot.Element);
                    var sourceNav = new ElementNavigator(coreType.Snapshot.Element);
                    sourceNav.MoveToFirstChild();
                    nav.CopyChildren(sourceNav);
                }
            }

            return true;
        }

        private static void generateBaseElements(IEnumerable<ElementDefinition> elements)
        {
            foreach(var element in elements)
            {
                if (element.Base == null)
                {
                    element.Base = new ElementDefinition.BaseComponent()
                    {
                        MaxElement = (FhirString)element.MaxElement.DeepCopy(),
                        MinElement = (Integer)element.MinElement.DeepCopy(),
                        PathElement = (FhirString)element.PathElement.DeepCopy()
                    };
                }
            }
        }

        // [WMR 20160802] NEW

        // Determines if the specified type profile url is of the form 'baseProfileUrl#elementName'
        // If so, then extract separate components and return true
        internal static bool IsComplexProfileReference(string url, out string profileUrl, out string elementName)
        {
            if (url == null) { throw new ArgumentNullException("url"); }
            var pos = url.IndexOf('#');
            if (pos > 0 && pos < url.Length)
            {
                profileUrl = url.Substring(0, pos);
                elementName = url.Substring(pos + 1);
                return true;
            }
            profileUrl = url;
            elementName = null;
            return false;
        }

    }
}
