/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

// DEBUGGING
#define DUMPMATCHES

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

// [WMR 20170810] STU3 bug: SimpleQuantity root element introduces non-empty sliceName = "SliceName"
// Detect and fix invalid non-null sliceNames on specializations
// #define FIX_SLICENAMES_ON_SPECIALIZATIONS
// Detect and fix invalid non-null sliceNames on root elements
#define FIX_SLICENAMES_ON_ROOT_ELEMENTS

// [WMR 20180409] Resolve contentReference from core resource/datatype (StructureDefinition.type)
#define FIX_CONTENTREFERENCE

// [WMR 20190828] R4: Normalize renamed type slices in snapshot
// e.g. diff: "valueString" => snap: "value[x]:valueString"
#define NORMALIZE_RENAMED_TYPESLICE

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using T = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Snapshot
{
    // [WMR 20160927] Design considerations:
    // Functionality is implemented as a set of partial classes, in order to share common state and
    // minimize memory pressure while recursively generating snapshots of base and/or type profiles.

    /// <summary>
    /// Provides functionality to generate the snapshot component of a <see cref="StructureDefinition"/> resource.
    /// </summary>
    public sealed partial class SnapshotGenerator
    {
        // TODO: Properly test SnapshotGenerator for multi-threading support
        // Each thread should create a separate instance
        // But all instances should share a common IResourceResolver instance
        // TODO: Probably also need to share a common recursion stack...


        readonly SnapshotGeneratorSettings _settings;
        readonly SnapshotRecursionStack _stack = new SnapshotRecursionStack();

        /// <summary>
        /// Create a new instance of the <see cref="SnapshotGenerator"/> class
        /// for the specified resource resolver and configuration settings.
        /// </summary>
        /// <param name="resolver">A <see cref="IResourceResolver"/> or <see cref="IAsyncResourceResolver"/> instance.</param>
        /// <param name="settings">Configuration settings that control the behavior of the snapshot generator.</param>
        /// <exception cref="ArgumentNullException">One of the specified arguments is <c>null</c>.</exception>
#pragma warning disable CS0618 // Type or member is obsolete
        public SnapshotGenerator(ISyncOrAsyncResourceResolver resolver, SnapshotGeneratorSettings settings)
        {
            Source = resolver as IResourceResolver;
#pragma warning restore CS0618 // Type or member is obsolete
            AsyncResolver = verifySource(resolver.AsAsync(), nameof(resolver));

            if (settings == null) { throw Error.ArgumentNull(nameof(settings)); }
            // [WMR 20171023] Always copy the specified settings, to prevent shared state
            // Especially important to prevent corruption of the global SnapshotGeneratorSettings.Default instance.
            _settings = new SnapshotGeneratorSettings(settings);
        }

        /// <summary>
        /// Create a new instance of the <see cref="SnapshotGenerator"/> class
        /// for the specified resource resolver and with default configuration settings.
        /// </summary>
        /// <param name="resolver">A <see cref="IResourceResolver"/> or <see cref="IAsyncResourceResolver"/> instance.</param>
        /// <exception cref="ArgumentNullException">One of the specified arguments is <c>null</c>.</exception>
#pragma warning disable CS0618 // Type or member is obsolete
        public SnapshotGenerator(ISyncOrAsyncResourceResolver resolver)
        {
            Source = resolver as IResourceResolver;
#pragma warning restore CS0618 // Type or member is obsolete
            AsyncResolver = verifySource(resolver.AsAsync(), nameof(resolver));

            _settings = SnapshotGeneratorSettings.CreateDefault();
        }

        private static IAsyncResourceResolver verifySource(IAsyncResourceResolver resolver, string name = null)
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

        /// <summary>Returns a reference to the associated <see cref="IResourceResolver"/> instance, as specified in the 
        /// call to the constructor.</summary>
        /// <remarks>May return <code>null</code> if the source is an exclusively asynchronous resolver.</remarks>
        [Obsolete("SnapshotGenerator now works best with asynchronous resolvers. Use the AsyncResolver property instead.")]
        public IResourceResolver Source { get; private set; }

        /// <summary>Returns a reference to the associated <see cref="IAsyncResourceResolver"/> instance, as specified in the 
        /// call to the constructor.</summary>
        public IAsyncResourceResolver AsyncResolver { get; private set; }

        /// <summary>Returns a reference to the associated <see cref="IAsyncResourceResolver"/> instance, as specified in the 
        /// call to the constructor.</summary>
        /// <remarks>When the constructor was called with an <see cref="IResourceResolver"/>, this property will return
        /// an adapted resolver, which implements the async interface over the syncrho</remarks>
        /// <summary>Returns the snapshot generator configuration settings.</summary>
        public SnapshotGeneratorSettings Settings => _settings;

        /// <summary>Returns a reference to the profile uri of the currently generating snapshot, or <c>null</c>.</summary>
        string CurrentProfileUri => _stack.CurrentProfileUri;

        /// <summary>
        /// (Re-)generate the <see cref="StructureDefinition.Snapshot"/> component of the specified <see cref="StructureDefinition"/> instance.
        /// Resolve the associated base profile snapshot and merge the <see cref="StructureDefinition.Differential"/> component.
        /// </summary>
        /// <param name="structure">A <see cref="StructureDefinition"/> instance.</param>
        public async T.Task UpdateAsync(StructureDefinition structure)
        {
            structure.Snapshot = new StructureDefinition.SnapshotComponent()
            {
                Element = await GenerateAsync(structure).ConfigureAwait(false)
            };
            structure.Snapshot.SetCreatedBySnapshotGenerator();

            // [WMR 20170209] TODO: also merge global StructureDefinition.Mapping components
            // structure.Mappings = ElementDefnMerger.Merge(...)
        }

        /// <inheritdoc cref="UpdateAsync(StructureDefinition)"/>
        [Obsolete("SnapshotGenerator now works best with asynchronous resolvers. Use UpdateAsync() instead.")]
        public void Update(StructureDefinition structure)
            => TaskHelper.Await(() => UpdateAsync(structure));

        /// <summary>
        /// Generate the snapshot element list of the specified <see cref="StructureDefinition"/> instance.
        /// Resolve the associated base profile snapshot and merge the <see cref="StructureDefinition.Differential"/> component.
        /// Returns the expanded element list.
        /// Does not modify the <see cref="StructureDefinition.Snapshot"/> property of the specified instance.
        /// </summary>
        /// <param name="structure">A <see cref="StructureDefinition"/> instance.</param>
        public async T.Task<List<ElementDefinition>> GenerateAsync(StructureDefinition structure)
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
                result = await generate(structure).ConfigureAwait(false);
            }
            finally
            {
                // On complete expansion, the recursion stack should be empty
                Debug.Assert(result == null || _stack.RecursionDepth == 1);
                _stack.OnAfterGenerateSnapshot(structure.Url);
            }
            return result;
        }

        /// <inheritdoc cref="GenerateAsync(StructureDefinition)" />
        [Obsolete("SnapshotGenerator now works best with asynchronous resolvers. Use GenerateAsync() instead.")]
        public List<ElementDefinition> Generate(StructureDefinition structure)
            => TaskHelper.Await(() => GenerateAsync(structure));


        /// <summary>Recursively expand (the children of) a single element definition.</summary>
        /// <param name="elements">A <see cref="StructureDefinition.SnapshotComponent"/> or <see cref="StructureDefinition.DifferentialComponent"/> instance.</param>
        /// <param name="element">The element to expand. Should be part of <paramref name="elements"/>.</param>
        /// <returns>A new, expanded list of <see cref="ElementDefinition"/> instances.</returns>
        /// <exception cref="ArgumentException">The specified element is not contained in the list.</exception>
        public T.Task<IList<ElementDefinition>> ExpandElementAsync(IElementList elements, ElementDefinition element)
        {
            if (elements == null) { throw Error.ArgumentNull(nameof(elements)); }
            return ExpandElementAsync(elements.Element, element);
        }

        /// <inheritdoc cref="ExpandElementAsync(IElementList, ElementDefinition)" />
        [Obsolete("SnapshotGenerator now works best with asynchronous resolvers. Use ExpandElementAsync() instead.")]
        public IList<ElementDefinition> ExpandElement(IElementList elements, ElementDefinition element)
            => TaskHelper.Await(() => ExpandElementAsync(elements, element));

        /// <summary>Recursively expand (the children of) a single element definition.</summary>
        /// <param name="elements">A list of <see cref="ElementDefinition"/> instances, taken from snapshot or differential.</param>
        /// <param name="element">The element to expand. Should be part of <paramref name="elements"/>.</param>
        /// <returns>A new, expanded list of <see cref="ElementDefinition"/> instances.</returns>
        /// <exception cref="ArgumentException">The specified element is not contained in the list.</exception>
        public async T.Task<IList<ElementDefinition>> ExpandElementAsync(IList<ElementDefinition> elements, ElementDefinition element)
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
                await expandElement(nav).ConfigureAwait(false);
            }
            finally
            {
                _stack.OnFinishRecursion();
            }
            return nav.Elements;
        }

        /// <inheritdoc cref="ExpandElementAsync(IList{ElementDefinition}, ElementDefinition)" />
        [Obsolete("SnapshotGenerator now works best with asynchronous resolvers. Use ExpandElementAsync() instead.")]
        public IList<ElementDefinition> ExpandElement(IList<ElementDefinition> elements, ElementDefinition element)
            => TaskHelper.Await(() => ExpandElementAsync(elements, element));

        /// <summary>Recursively expand (the children of) a single element definition.</summary>
        /// <param name="nav">An <see cref="ElementDefinitionNavigator"/> instance positioned on the target element to be expanded.</param>
        /// <returns><c>true</c> if the element is succesfully expanded, or <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentException">The specified navigator is not positioned on an element.</exception>
        public async T.Task<bool> ExpandElementAsync(ElementDefinitionNavigator nav)
        {
            nav.ThrowIfNullOrNotPositioned(nameof(nav));

            clearIssues();

            // Must initialize recursion checker, because element expansion may recurse on external type profile
            _stack.OnStartRecursion();
            try
            {
                return await expandElement(nav).ConfigureAwait(false);
            }
            finally
            {
                _stack.OnFinishRecursion();
            }
        }

        /// <inheritdoc cref="ExpandElementAsync(ElementDefinitionNavigator)" />
        [Obsolete("SnapshotGenerator now works best with asynchronous resolvers. Use ExpandElementAsync() instead.")]
        public bool ExpandElement(ElementDefinitionNavigator nav)
                        => TaskHelper.Await(() => ExpandElementAsync(nav));

        /// <summary>Merge two sets of element constraints, e.g. base and differential.</summary>
        /// <param name="snap">A set of element constraints.</param>
        /// <param name="diff">Another set of element constraints to merge on top of the base.</param>
        /// <param name="mergeElementId">Determines if the snapshot should inherit Element.id values from the differential.</param>
        /// <returns>A new <see cref="ElementDefinition"/> instance.</returns>
        //public async T.Task<ElementDefinition> MergeElementDefinitionAsync(ElementDefinition snap, ElementDefinition diff, bool mergeElementId)
        public ElementDefinition MergeElementDefinition(ElementDefinition snap, ElementDefinition diff, bool mergeElementId)
        {
            var result = (ElementDefinition)snap.DeepCopy();
            ElementDefnMerger.Merge(this, result, diff, mergeElementId, _stack.CurrentProfileUri);
            return result;
        }

        ///// <inheritdoc cref="MergeElementDefinitionAsync(ElementDefinition, ElementDefinition, bool)" />
        //[Obsolete("SnapshotGenerator now works best with asynchronous resolvers. Use MergeElementDefinition() instead.")]
        //public ElementDefinition MergeElementDefinition(ElementDefinition snap, ElementDefinition diff, bool mergeElementId)
        //              => TaskHelper.Await(() => MergeElementDefinitionAsync(snap, diff, mergeElementId));

        // ***** Private Interface *****

        /// <summary>
        /// Expand the differential component of the specified structure and return the expanded element list.
        /// The given structure is not modified.
        /// </summary>
        private async T.Task<List<ElementDefinition>> generate(StructureDefinition structure)
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
            if (structure.IsConstraint && structure.BaseDefinition == null)
            {
                throw Error.Argument(nameof(structure), "Invalid argument. The specified StructureDefinition represents a constraint on another FHIR profile, but the base profile url is missing or empty. Derived profiles must have a base url.");
            }

            ElementDefinitionNavigator nav;
            // StructureDefinition.SnapshotComponent snapshot = null;
            if (structure.BaseDefinition != null)
            {
                var baseStructure = await AsyncResolver.FindStructureDefinitionAsync(structure.BaseDefinition).ConfigureAwait(false);

                // [WMR 20161208] Handle unresolved base profile
                if (baseStructure == null)
                {
                    addIssueProfileNotFound(structure.BaseDefinition);
                    // Fatal error...
                    return null;
                }

                // [WMR 20161208] Handle missing differential
                var location = differential.Element.Count > 0 ? differential.Element[0].Path : null;
                if (!await ensureSnapshot(baseStructure, structure.BaseDefinition, location).ConfigureAwait(false))
                {
                    // Fatal error...
                    return null;
                }

                // [WMR 20190806] Expanding the base profile *may* recurse on current structure
                // e.g. Extension : BaseDefinition => Element : Element.extension => Extension
                // Then snapshot root is already generated and annotated to differential.Element[0]
                //Debug.WriteLineIf(differential.Element[0].HasSnapshotElementAnnotation(), $"[{nameof(SnapshotGenerator)}.{nameof(generate)} (1)] diff[0] is annotated with cached snapshot root...");

                // [WMR 20160817] Notify client about the resolved, expanded base profile
                OnPrepareBaseProfile(structure, baseStructure);

                var snapshot = (StructureDefinition.SnapshotComponent)baseStructure.Snapshot.DeepCopy();

                // [WMR 20170616] NEVER inherit element IDs from base profile
                // Otherwise e.g. slices introduced in derived profile would inherit the original ID from unsliced parent element - WRONG!
                // WRONG! Must handle backward content references in derived profiles, e.g. Questionnaire
                // Following statement causes BasicValidationTests.ValidateOverNameRef to fail,
                // because snapshot generator can no longer expand Questionnaire.item.item
                // because element id of Questionnaire.item has been cleared...
                // => must immediately (re-)generate element ID after expansion of each element
                // ElementIdGenerator.Clear(snapshot.Element);
                // Debug.Fail("TODO");

                // [WMR 20190902] #1090 SnapshotGenerator should support logical models
                if (structure.Kind == StructureDefinition.StructureDefinitionKind.Logical)
                {
                    var rootPath = structure.Type;

                    // For logical models, StructureDefinition.type may return a fully qualified url
                    // Last segment should equal the name of the root element
                    // e.g. http://example.org/fhir/StructureDefinition/MyModel

                    if (string.IsNullOrEmpty(rootPath))
                    {
                        // Not a fatal error; parse root path from first element constraint
                        //throw Error.Argument(nameof(structure), $"Invalid argument. The StructureDefinition.type property value is empty or missing.");
                        addIssueStructureTypeMissing(structure);

                        // Derive from root element name
                        rootPath = structure.Differential?.Element?[0].GetNameFromPath();
                    }
                    else
                    {
                        var pos = rootPath.LastIndexOf("/");
                        if (pos > -1)
                        {
                            rootPath = rootPath.Substring(pos + 1);
                        }
                    }

                    // Abort if we failed to determine root path
                    if (string.IsNullOrEmpty(rootPath))
                    {
                        // Fatal error...
                        throw Error.Argument(nameof(structure), $"Invalid argument. The StructureDefinition.type property value is empty or missing.");

                    }

                    snapshot.Rebase(rootPath);
                }

                else if (!structure.IsConstraint)
                {
                    // [WMR 20160902] Rebase the cloned base profile (e.g. DomainResource)

                    // [WMR 20170426] Specializations (i.e. core resource definitions)
                    // do NOT inherit element IDs from (abstract) base class
                    // Clear all to force full re-generation
                    // ElementIdGenerator.Clear(snapshot.Element);

                    // [WMR 20170411] Updated for STU3 - root element is no longer required/ensured
                    // => Derive from StructureDefinition.type property
                    var rootPath = structure.Type;
                    if (string.IsNullOrEmpty(rootPath))
                    {
                        // Fatal error...
                        throw Error.Argument(nameof(structure), $"Invalid argument. The StructureDefinition.type property value is empty or missing.");
                    }

                    snapshot.Rebase(rootPath);

#if FIX_SLICENAMES_ON_SPECIALIZATIONS
                    // [WMR 20170810] Handle bug in STU3
                    // SimpleQuantity root element defines non-null sliceName = "SimpleQuantity" - WRONG!
                    fixInvalidSliceNamesInSpecialization(structure);
#endif
                }

                // [WMR 20170710] NEW: Generate element IDs while processing
                // First ensure IDs exist on all elements inherited from base profile
                // Then, while expanding the snapshot, generate ids for all new element constraints introduced by diff
                // This ensures that we can process back references to ids of preceding elements
                // e.g. Questionnaire.item.item => Questionnaire.item
                if (_settings.GenerateElementIds)
                {
                    // Always re-generate; never inherit element ids from base profile
                    ElementIdGenerator.Update(snapshot.Element, true);
                }

                // Ensure that ElementDefinition.Base components in base StructureDef are propertly initialized
                // [WMR 20170424] Inherit existing base components, generate if missing
                await ensureBaseComponents(snapshot.Element, structure.BaseDefinition, false).ConfigureAwait(false);

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
            var fullDifferential = differential.MakeTree();
            var diff = new ElementDefinitionNavigator(fullDifferential);

#if FIX_SLICENAMES_ON_ROOT_ELEMENTS
            var diffRoot = fullDifferential.GetRootElement();
            fixInvalidSliceNameOnRootElement(diffRoot, structure);
#endif

            // [WMR 20190806] Expanding the base profile *may* recurse on current structure
            // e.g. Extension : BaseDefinition => Element : Element.extension => Extension
            // Then snapshot root is already generated and annotated to differential.Element[0]
            //Debug.WriteLineIf(diffRoot.HasSnapshotElementAnnotation(), $"[{nameof(SnapshotGenerator)}.{nameof(generate)} (before merge)] diff root is annotated with cached snapshot root...");

            await merge(nav, diff).ConfigureAwait(false);

            result = nav.ToListOfElements();

            //Debug.WriteLineIf(diffRoot.HasSnapshotElementAnnotation(), $"[{nameof(SnapshotGenerator)}.{nameof(generate)} (after merge)] diff root is annotated with cached snapshot root...");

            // Ready! Snapshot has been generated

#if CACHE_ROOT_ELEMDEF
            // [WMR 20190806] Never expose/leak internal temporary annotations!
            // Only for handling internal profile recursion, e.g. Extension => Extension.extension
            // Remove the temporary annotation on differential root element
            // Cached root element annotation only applies to *this* instance
            // User could generate new structure by cloning Differential (esp. root element)
            // Cloning existing annotations would corrupt the new snapshot...
            diffRoot.RemoveSnapshotElementAnnotations();
#endif

            return result;
        }

#if FIX_SLICENAMES_ON_SPECIALIZATIONS
        void fixInvalidSliceNamesInSpecialization(StructureDefinition sd)
        {
            Debug.Assert(sd.Derivation == StructureDefinition.TypeDerivationRule.Specialization);
            var elems = sd.Differential.Element.Where(e => e.SliceName != null);
            foreach (var elem in elems)
            {
                if (!string.IsNullOrEmpty(elem.SliceName))
                {
                    addIssueInvalidSliceNameOnSpecialization(elem);
                    elem.SliceName = null;
                }
            }
        }
#endif

#if FIX_SLICENAMES_ON_ROOT_ELEMENTS
        void fixInvalidSliceNameOnRootElement(ElementDefinition elem, StructureDefinition sd)
        {
            if (elem != null)
            {
                Debug.Assert(elem.IsRootElement());
                if (!string.IsNullOrEmpty(elem.SliceName))
                {
                    if (sd.Url != ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.SimpleQuantity))
                    {
                        addIssueInvalidSliceNameOnRootElement(elem, sd);
                    }
                    elem.SliceName = null;
                }
            }
        }
#endif

        /// <summary>
        /// Expand the currently active element within the specified <see cref="ElementDefinitionNavigator"/> instance.
        /// If the element has a name reference, then merge from the targeted element.
        /// Otherwise, if the element has a custom type profile, then merge it.
        /// </summary>
        private async T.Task<bool> expandElement(ElementDefinitionNavigator nav)
        {
            // [WMR 20170614] NEW: keepElementId
            // Maintain existing root element ID if called from the public ExpandElement method

            if (nav.AtRoot)
            {
                throw Error.Argument(nameof(nav), $"Internal error in snapshot generator ({nameof(expandElement)}): Navigator is not positioned on an element");
            }

            if (nav.HasChildren)
            {
                return true;     // already has children, we're not doing anything extra
            }

            var defn = nav.Current;

            if (!String.IsNullOrEmpty(defn.ContentReference))
            {

#if FIX_CONTENTREFERENCE
                // [WMR 20180409] NEW
                // Resolve contentReference from core resource/datatype definition
                // Specified by StructureDefinition.type == root element name

                var coreStructure = await getStructureForContentReference(nav, true).ConfigureAwait(false);
                // getStructureForContentReference emits issue if profile cannot be resolved
                if (coreStructure == null) { return false; }

                var sourceNav = ElementDefinitionNavigator.ForSnapshot(coreStructure);
#else
                // [WMR 20180409] WRONG!
                // Resolves contentReference from current StructureDef
                // Recursive child items should NOT inherit constraints from parent in same profile

                var sourceNav = new ElementDefinitionNavigator(nav);
#endif
                if (!sourceNav.JumpToNameReference(defn.ContentReference))
                {
                    addIssueInvalidNameReference(defn);
                    return false;
                }

                // [WMR 20190926] #1123 Remove annotations and fix Base components!
                copyChildren(nav, sourceNav);

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

                // [WMR 20181212] R4 NEW - eld-13: Types must be unique by code
                // Gracefully handle non-distinct type codes; do not expand

                // [MS 20210614] When we can't find a CommonTypeCode assume "Element" for .id and .extension
                var distinctTypeCode = defn.CommonTypeCode() ?? FHIRAllTypes.Element.GetLiteral();

                // Different profiles for common base type => expand the common base type (w/o custom profile)
                // var typeRef = new ElementDefinition.TypeRefComponent() { Code = distinctTypeCodes[0] };
                var typeRef = new ElementDefinition.TypeRefComponent() { Code = distinctTypeCode };
                StructureDefinition typeStructure = await getStructureForTypeRef(defn, typeRef, true).ConfigureAwait(false);
                return await expandElementType(nav, typeStructure).ConfigureAwait(false);

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
                StructureDefinition typeStructure = await getStructureForElementType(defn, true).ConfigureAwait(false);

                return await expandElementType(nav, typeStructure).ConfigureAwait(false);
            }

            return true;
        }

        private async T.Task<bool> expandElementType(ElementDefinitionNavigator nav, StructureDefinition typeStructure)
        {
            // [WMR 20170208] TODO: Expand profile snapshot if necessary
            if (typeStructure != null && typeStructure.HasSnapshot)
            {
                var typeNav = ElementDefinitionNavigator.ForSnapshot(typeStructure);
                if (!typeNav.MoveToFirstChild())
                {
                    addIssueProfileHasNoSnapshot(nav.Current.Path, typeStructure.Url);
                }
                // [WMR 20170208] NEW - Move common logic to separate method, also used by mergeTypeProfiles

                // [WMR 20170220] If profile element has no children, then copy child elements from type structure into profile
                if (!copyChildren(nav, typeNav))
                {
                    // Otherwise merge type structure onto profile elements (cf. mergeTypeProfiles)

                    // [WMR 20170220] Can this happen?
                    // TODO: Create unit test to trigger this situation
                    Debug.Fail($"[{nameof(SnapshotGenerator)}.{nameof(expandElementType)}] TODO...");

                    // [WMR 20170220] WRONG...?
                    // Must merge nav on top of typeNav, not the other way around...
                    await mergeElement(nav, typeNav).ConfigureAwait(false);

                    // 1. Fully expand the snapshot of the external type profile
                    // 2. Clone, rebase and copy children into referencing profile below the referencing parent element
                    // 2. On top of that, merge base profile constraints (taken from the snapshot)
                    // 3. On top of that, merge profile differential constraints
                }

                // [WMR 20170711]
                // - Regenerate element IDs (NOT inherited from external rebased element type profiles!)
                // - Notify subscribers by calling OnPrepareBaseElement, before merging diff constraints
                prepareExpandedTypeProfileElements(nav, typeNav);

                return true;
            }
            return false;
        }

        /// <summary>Merge children of the currently selected element from differential into snapshot.</summary>
        private async T.Task merge(ElementDefinitionNavigator snap, ElementDefinitionNavigator diff)
        {
            var snapPos = snap.Bookmark();
            var diffPos = diff.Bookmark();

            try
            {
                var matches = ElementMatcher.Match(snap, diff);

#if DUMPMATCHES
                Debug.WriteLine($"Matches for children of '{snap.StructureDefinition?.Name}' : {(snap.AtRoot ? "(root)" : snap.Path ?? "/")} '{(snap.Current?.SliceName ?? snap.Current?.Type.FirstOrDefault()?.Profile?.FirstOrDefault() ?? snap.Current?.Type.FirstOrDefault()?.Code)}'");
                matches.DumpMatches(snap, diff);
#endif

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
                            await mergeElement(snap, diff).ConfigureAwait(false);
                            break;
                        case ElementMatcher.MatchAction.Add:
                            await addSlice(snap, diff, match.SliceBase).ConfigureAwait(false);
                            break;
                        case ElementMatcher.MatchAction.Slice:
                            await startSlice(snap, diff, match.SliceBase).ConfigureAwait(false);
                            break;
                        case ElementMatcher.MatchAction.New:
                            // No matching base element; this is a new element definition
                            // snap is positioned at the associated parent element
                            await createNewElement(snap, diff).ConfigureAwait(false);
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
        private async T.Task createNewElement(ElementDefinitionNavigator snap, ElementDefinitionNavigator diff)
        {
            var (targetElement, typeStructure) = await getBaseElementForElementType(diff.Current).ConfigureAwait(false);
            addConstraintSource(targetElement, typeStructure?.Url);

            if (targetElement != null)
            {
                // New element with type profile
                var newElement = (ElementDefinition)targetElement.DeepCopy();
                newElement.Path = ElementDefinitionNavigator.ReplacePathRoot(newElement.Path, diff.Path);

                // [WMR 20190130] STU3: Base component of new elements is empty
                // [WMR 20190130] R4: Base components of new elements refers to self (.Base.Path = .Path)
                // [WMR 20190723] FIX: Initialize base cardinality from current diff element
                // Do NOT inherit from target, e.g. Resource.id (0...1) inherits from id root (0...*)
                // [DEBUGGING] Debug.Assert(newElement.Path != "Resource.id");
                newElement.Base = new ElementDefinition.BaseComponent()
                {
                    Path = newElement.Path,
                    Min = diff.Current.Min, // newElement.Min,
                    Max = diff.Current.Max, // newElement.Max
                };              

                // [WMR 20160915] NEW: Notify subscribers
                OnPrepareElement(newElement, typeStructure, targetElement);

                // [WMR 20170421] Merge custom element Id from diff
                mergeElementDefinition(newElement, diff.Current, true);

                snap.AppendChild(newElement);
            }
            else
            {
                // New element w/o type profile
                // [WMR 20190131] Also for contentReferences, e.g. Questionnaire.item.item
                // Only inherits structure, not constraints, from referenced item (e.g. Questionnaire.item)
                // For example, constraint on .item.type does NOT apply to .item.item.type

                var clonedElem = (ElementDefinition)diff.Current.DeepCopy();

#if CACHE_ROOT_ELEMDEF
                // [WMR 20190806] Never clone temporary internal annotation!
                clonedElem.RemoveSnapshotElementAnnotations(); // Paranoia...
#endif

                // [WMR 20190131] R4: For new elements, base component should refer to element itself (.Base.Path = .Path)
                clonedElem.Base = new ElementDefinition.BaseComponent()
                {
                    Path = clonedElem.Path,
                    Min = clonedElem.Min,
                    Max = clonedElem.Max
                };

                // [WMR 20160915] NEW: Notify subscribers
                OnPrepareElement(clonedElem, null, null);

                snap.AppendChild(clonedElem);
            }

            // [WMR 20170710] NEW: Generate element IDs while processing
            if (_settings.GenerateElementIds)
            {
                // Always re-generate; never inherit element ids from base element
                ElementIdGenerator.Update(snap, true);
            }

            // Notify clients about a snapshot element with differential constraints
            OnConstraint(snap.Current);

            // Merge children
            await mergeElement(snap, diff).ConfigureAwait(false);
        }

        private void removeNewTypeConstraint(ElementDefinition element, StructureDefinition typeStructure)
        {
            if (typeStructure?.Differential?.Element != null && !element.Constraint.IsNullOrEmpty())
            {
                List<ElementDefinition.ConstraintComponent> newConstraints = null;

                //See if there are new constraints introduced by the type
                var nav = new ElementDefinitionNavigator(typeStructure.Differential.Element);
                if (nav.MoveToFirstChild())
                {
                    if (nav.Current.IsRootElement())
                        newConstraints = nav.Current.Constraint;
                }
                //If there are any newly introduced constraints, remove them from the new element.
                if (newConstraints != null)
                {
                    var keys = newConstraints.Select(c => c.Key);
                    element.Constraint.RemoveAll(c => keys.Contains(c.Key));
                }
            }
        }

        private static void addConstraintSource(ElementDefinition targetElement, string url)
        {
            foreach (var constraint in targetElement?.Constraint.Where(c => string.IsNullOrEmpty(c.Source)) ?? Enumerable.Empty<ElementDefinition.ConstraintComponent>())
            {
                constraint.Source = url;
            }
        }

        // Recursively merge the currently selected element and (grand)children from differential into snapshot
        private async T.Task mergeElement(ElementDefinitionNavigator snap, ElementDefinitionNavigator diff)
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

            if (isRoot && diffElem.GetSnapshotElementAnnotation() is ElementDefinition cachedRootElemDef)
            {

#if CACHE_ROOT_ELEMDEF_ASSERT
                // DEBUG / VERIFY: merge results should be equal to cached ElemDef instance
                var isValid = mergeTypeProfiles(snap, diff);
                mergeElementDefinition(snap.Current, diff.Current, true);
                var currentRootClone = (ElementDefinition)snap.Current.DeepCopy();
                var cachedRootClone = (ElementDefinition)cachedRootElemDef.DeepCopy();
                // Ignore Id, Path, Base and ChangedByDiff extension - they are expected to differ
                currentRootClone.ElementId = cachedRootClone.ElementId;
                currentRootClone.Path = cachedRootClone.Path;
                currentRootClone.Base = cachedRootClone.Base;
                currentRootClone.RemoveAllConstrainedByDiffAnnotations();
                cachedRootClone.RemoveAllConstrainedByDiffAnnotations();
                Debug.Assert(cachedRootClone.IsExactly(currentRootClone));
#endif

                // Found temporary annotation to generated snapshot root element on diff root element
                // (assigned previously by recursive snapshot expansions)
                // Insert snapshot root element into the specified snapshot element list
                // and remove the temporary annotation

                //Debug.WriteLine($"[{nameof(SnapshotGenerator)}.{nameof(mergeElement)} ANNOTATIONS] Replace original element at position {snap.OrdinalPosition.Value} #{snap.Elements[snap.OrdinalPosition.Value]?.GetHashCode()} with cached root element definition: '{cachedRootElemDef.Path}'  #{cachedRootElemDef.GetHashCode()}");
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
                    isMerged = await mergeTypeProfiles(snap, diff).ConfigureAwait(false);
                }

                // Then merge diff constraints from profile
                // [WMR 20170424] Merge custom element Id from diff, if specified
                mergeElementDefinition(snap.Current, diffElem, true);

                // [WMR 20170710] NEW: Generate element IDs while processing
                // Generate id if not explicitly specified in diff (don't inherit from base)
                if (_settings.GenerateElementIds)
                {
                    // Always generate new id's for child elements

                    // Also generate id for current element if not specified by diff
                    //ElementIdGenerator.Update(snap, true, !string.IsNullOrEmpty(diff.Current.ElementId));

                    // [WMR 20190822] R4: Always re-generate Element Ids according to standardized format
                    // http://hl7.org/fhir/elementdefinition.html#id
                    // Ignore user-specified element id's in the differential
                    ElementIdGenerator.Update(snap, true);
                }
            }
#else
            // First merge constraints from element type profile, if it exists
            var isMerged = true;
            isMerged = mergeTypeProfiles(snap, diff);

            // Then merge constraints from base profile
            mergeElementDefinition(snap.Current, diffElem, true);

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
                    // have reduced the number of types to 1. Still, if you don't do that, we cannot
                    // accept constraints on children, need to select a single type first...

                    // [WMR 20170227] REDUNDANT; checked by expandElement
                    //if (snap.Current.Type.Count > 1)
                    //{
                    //    addIssueInvalidChoiceConstraint(snap.Current);
                    //    return;
                    //}

                    // [WMR 20170711] Explicitly re-generate element ids
                    if (!await expandElement(snap).ConfigureAwait(false))
                    {
                        return;
                    }
                }

                // Now, recursively merge the children
                await merge(snap, diff).ConfigureAwait(false);

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
            ElementDefnMerger.Merge(this, snap, diff, mergeElementId, _stack.CurrentProfileUri);
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

        static readonly string DomainResource_Extension_Path = ModelInfo.FhirTypeToFhirTypeName(FHIRAllTypes.DomainResource) + ".extension";

        // Resolve the type profile of the currently selected element and merge into snapshot
        private async T.Task<bool> mergeTypeProfiles(ElementDefinitionNavigator snap, ElementDefinitionNavigator diff)
        {
            // Debug.Print("[mergeTypeProfiles] {0} : {1}", diff.Path, diff.Current.Type != null && diff.Current.Type.Count == 1 ? diff.Current.PrimaryTypeCode() : null);

            // [WMR 20160913] Possible improvements:
            // Don't recurse on special terminator elements, e.g. id, url, value
            // Note that all these element definitions are marked with: <representation value="xmlAttr"/>

            // [WMR 20171004] TODO: Only expand the element if:
            // 1) The element defines a single type code
            //    => expand core type profile
            // 2) The element defines a single type code and custom profile
            //    => expand custom type profile
            // 3) The element defines multiple types with same type code and different (custom) profiles
            //    => expand core type profile (or better: common base profile)
            //
            // Do NOT expand the element if:
            // 1) The element defines multiple different type codes

            var diffTypes = diff.Current.Type;
            var distinctTypeCodeCnt = diffTypes.DistinctTypeCodes().Count;
            if (distinctTypeCodeCnt == 0)
            {
                // Element has no type constraints, nothing to merge
                // return true to continue merging child constraints
                return true;
            }
            else if (distinctTypeCodeCnt > 1)
            {
                // Element specifies multiple type codes, cannot expand children
                // return false to prevent merging child constraints
                return false;
            }

            // [WMR 20171004] New
            //var distinctTypeProfiles = diffTypes.Where(t => t.Profile != null).Select(t => t.Profile).Distinct().ToList();
            // [WMR 20181212] R4 NEW
            var distinctTypeProfiles = diffTypes.SelectMany(t => t.Profile).Distinct().ToList();
            if (distinctTypeProfiles.Count > 1)
            {
                // Multiple type profiles, cannot expand children
                // return true to continue merging diff child constraints
                // expandElementType will first try to expand the (common) type code core profile on demand
                return true;
            }

            var primaryDiffType = diff.Current.PrimaryType();

            StructureDefinition typeStructure = null;
            if (!(primaryDiffType is null))
            {

                var primarySnapType = snap.Current.PrimaryType();
                // if (primarySnapType == null) { return true; }

                // [WMR 20181212] R4 NEW
                //var primaryDiffTypeProfile = primaryDiffType.Profile.FirstOrDefault();
                var primaryDiffTypeProfile = distinctTypeProfiles.FirstOrDefault();

                // [WMR 20170208] Ignore explicit diff profile if it matches the (implied) base type profile
                // e.g. if the differential specifies explicit core type profile url
                // Example: Patient.identifier type = { Code : Identifier, Profile : "http://hl7.org/fhir/StructureDefinition/Identifier" } }
                var primarySnapTypeProfile = primarySnapType.GetTypeProfile();
                if (string.IsNullOrEmpty(primaryDiffTypeProfile) || primaryDiffTypeProfile == primarySnapTypeProfile)
                {
                    // return false;
                    // [WMR 20171004] return true to continue merging diff child constraints
                    // expandElementType will first try to expand the (common) type code core profile on demand
                    return true;
                }

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

                typeStructure = await AsyncResolver.FindStructureDefinitionAsync(primaryDiffTypeProfile).ConfigureAwait(false);

                if (_settings.GenerateSnapshotForExternalProfiles)
                    await ensureSnapshot(typeStructure, primaryDiffTypeProfile).ConfigureAwait(false);

                // [WMR 20170224] Verify that the resolved StructureDefinition is compatible with the element type
                // [WMR 20170823] WRONG! Base element may specify multiple type options
                //if (!_resolver.IsValidTypeProfile(primarySnapType.Code, typeStructure))
                //{
                //    addIssueInvalidProfileType(diff.Current, typeStructure);
                //    return false;
                //}

                var diffNode = diff.Current.Path;

                // [WMR 20171004] NEW
                if (typeStructure == null)
                {
                    // Unresolved external type profile; emit issue and abort (nothing to merge)
                    addIssueProfileNotFound(diffNode, primaryDiffTypeProfile);
                    // [WMR 20171004] return true to continue merging diff child constraints
                    // expandElementType will first try to expand the (common) type code core profile on demand
                }
                else
                {
                    // The element type profile constraint must match one of the base types
                    var isCompatible = await snap.Current.Type.AnyAsync(async t => await isValidTypeProfile(AsyncResolver, t.Code, typeStructure).ConfigureAwait(false)).ConfigureAwait(false);
                    if (!isCompatible)
                    {
                        addIssueInvalidProfileType(diff.Current, typeStructure);
                        // [WMR 20171004] return true to continue merging diff child constraints
                        // expandElementType will first try to expand the (common) type code core profile on demand
                    }
                    else
                    {
                        // [WMR 20170207] Notify observers, allow event subscribers to force expansion (even if no diff constraints)
                        // Note: if the element is to be expanded, then always merge full snapshot of the external type profile (!)
                        if (mustExpandElement(diff))
                        {
                            if (!await ensureSnapshot(typeStructure, primaryDiffTypeProfile, diffNode).ConfigureAwait(false))
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

                            var typeNav = new ElementDefinitionNavigator(rebasedTypeSnapshot.Element, typeStructure);
                            if (!profileRef.IsComplex)
                            {
                                typeNav.MoveToFirstChild();

                                // [WMR 20170208] Update ElementDefinition.Base components
                                // ensureBaseComponents(typeNav, snap, true);

#if FIX_SLICENAMES_ON_ROOT_ELEMENTS
                                // [WMR 20170321] HACK: Never copy elements names from the root element (e.g. SimpleQuantity)
                                if (typeNav.Current.SliceNameElement != null)
                                {
                                    Debug.WriteLine($"[{nameof(SnapshotGenerator)}.{nameof(mergeTypeProfiles)}] Explicitly prevent copying of root element name: {typeNav.Path} : '{typeNav.Current.SliceName}'");
                                    typeNav.Current.SliceName = null;
                                }
#endif

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

                            copyChildren(snap, typeNav);

                            // But we also need to merge external type profile onto any existing inline snapshot constraints
                            // e.g. TestObservationProfileWithExtensions(_ExpandAll)

                            // [WMR 20170428] ISSUE
                            // typeNav refers to type Snapshot, e.g. { Address Snap + MyAddress Diff }
                            // snap may already include Address Snap + Diff
                            // We need to determine { Address Snap + Diff + MyAddress Diff }
                            // But this performs { Address Snap + Diff + Address Snap (WRONG!) + MyAddress Diff }
                            // i.e. any overriding diff constraints from base snapshot are reverted back to original Address constraints
                            // Gets even more complicated with higher order derived base/type profiles...

                            await mergeElement(snap, typeNav).ConfigureAwait(false);

                            // Now call prepareTypeProfileElements (below) to clear element IDs and notify event subscribers
                        }
                        else
                        {
                            // Resolve and merge referenced external type profile

                            // [WMR 20190723] NEW
                            // EXCEPT for complex reference to profile and (extension) child element:
                            // "{url}#{element}", e.g. http://hl7.org/fhir/StructureDefinition/patient-nationality#code
                            // Target extension profile has already been merged via (grand)parent extension element
                            // => Do nothing; continue merging profile constraints on extension child elements
                            //
                            // Note: profile constraint on complex extension child element only needs to specify
                            // the correct slice name of the child element. Complex reference to profile & element
                            // is allowed, but not required.
                            // Example:
                            //
                            //
                            // <!-- Introduce profile extension -->
                            // <element>
                            //   <path value="Patient.extension"/>
                            //   <type>
                            //     <code value="Extension"/>
                            //     <profile value="http://hl7.org/fhir/StructureDefinition/patient-nationality"/>
                            //   </type>
                            // </element>
                            // <!-- Slicing entry for extension child elements -->
                            // <element>
                            //   <slicing>
                            //     <discriminator>
                            //       <type value="value"/>
                            //       <path value="url"/>
                            //     </discriminator>
                            //     <ordered value="false"/>
                            //     <rules value="open"/>
                            //   </slicing>                         */
                            // </element>
                            // <!-- Profile constraint on extension child element "code" -->
                            // <element>
                            //   <path value="Patient.extension.extension"/>
                            //   <!-- REQUIRED: Slice name of the constrained extension child element -->
                            //   <sliceName value="code"/>
                            //   <!-- OPTIONAL: Complex reference to extension profile and element -->
                            //   <type>
                            //     <code value="Extension"/>
                            //     <profile value="http://hl7.org/fhir/StructureDefinition/patient-nationality#code"/>
                            //   </type>
                            // </element>

                            if (!profileRef.IsComplex)
                            {
                                // Expand and merge (only!) the root element of the external type profile
                                // Note: full expansion may trigger recursion, e.g. Element.id => identifier => string => Element
                                var typeRootElem = await getSnapshotRootElement(typeStructure, primaryDiffTypeProfile, diffNode).ConfigureAwait(false);
                                if (typeRootElem == null) { return false; }

                                // Rebase before merging
                                var rebasedRootElem = (ElementDefinition)typeRootElem.DeepCopy();
                                rebasedRootElem.Path = diff.Path;
                                rebasedRootElem.Min = diff.Current.Min;
                                rebasedRootElem.Max = diff.Current.Max;

                                // Merge the type profile root element; no need to expand children
                                mergeElementDefinition(snap.Current, rebasedRootElem, false);
                            }
                            else
                            {
                                // Url bookmark #name should match the specified sliceName
                                // Note: ignore bookmark; always use the specified sliceName
                                if (!StringComparer.Ordinal.Equals(diff.Current.SliceName, profileRef.ElementName))
                                {
                                    addIssueInvalidComplexProfileReference(diff.Current);
                                }
                            }
                        }
                    }
                }

                // [WMR 20171004] Only need to perform the following steps if a type profile was merged

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
                elem.Comment?.RemoveConstrainedByDiffAnnotation();
                elem.Definition?.RemoveConstrainedByDiffAnnotation();
            }
        }

        /// <summary>
        /// Copy child elements from <paramref name="typeNav"/> to <paramref name="nav"/>.
        /// Remove existing annotations, fix Base components
        /// </summary>
        // [WMR 20170501] OBSOLETE: notify listeners - moved to prepareTypeProfileChildren
        bool copyChildren(ElementDefinitionNavigator nav, ElementDefinitionNavigator typeNav) // , StructureDefinition typeStructure)
        {
            // [WMR 20170426] IMPORTANT!
            // Do NOT modify typeNav/typeStructure
            // Call by mergeTypeProfiles: typeNav/typeStructure refers to modified clone of global type profile
            // Call by expandElement:     typeNav/typeStructure refers to global cached type profile (!)

            Debug.Assert(!nav.AtRoot);
            Debug.Assert(!typeNav.AtRoot);

            // [WMR 20170220] CopyChildren returns false if nav already has children
            if (nav.CopyChildren(typeNav))
            {
                // Fix the copied elements and notify observers

                // [WMR 20190926] Also support contentReference
                // typeNav positioned at target element of base profile (not the root element)
                // => process only the current subtree, not the full structure

                var typeRootPath = typeNav.Path;
                var typeRootPos = typeNav.OrdinalPosition.Value; // 0 for element type, >0 for content reference
                var typeElems = typeNav.Elements;
                var elems = nav.Elements;

                for (int pos = nav.OrdinalPosition.Value + 1, i = typeRootPos + 1;
                    i < typeElems.Count && pos < elems.Count;
                    i++, pos++)
                {
                    var typeElem = typeElems[i];

                    // [WMR 20190926] For contentReference, only process partial subtree
                    // Proceed while current target element is a (grand)child of the start element

                    if (typeRootPos > 0 // If typeNav represents target of a contentReference...
                                        // and if this element is NOT a child of the target contentReference...
                        && !ElementDefinitionNavigator.IsChildPath(typeRootPath, typeElem.Path))
                    {
                        // Then we're done processing the subtree
                        break;
                    }

                    var elem = elems[pos];

                    // [WMR 20160826] Never inherit Changed extension from base profile!
                    elem.RemoveAllConstrainedByDiffExtensions();
                    elem.RemoveAllConstrainedByDiffAnnotations();

                    // [WMR 20160902] Initialize empty ElementDefinition.Base components if necessary
                    // [WMR 20170424] Inherit existing base components from type profile
                    elem.EnsureBaseComponent(typeElem, false);
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
            // Recursively re-generate IDs for elements inherited from external rebased type profile
            if (_settings.GenerateElementIds)
            {
                ElementIdGenerator.Update(snap, true);
            }

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
            // Recursively re-generate IDs for elements inherited from external rebased type profile
            if (_settings.GenerateElementIds)
            {
                // [WMR 20180116] Fix: only update child element ids
                ElementIdGenerator.Update(snap, true, true);
            }

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

        // Initialize [...].extension.url fixed url value, if missing
        static void fixExtensionUrl(ElementDefinitionNavigator nav)
        {
            // Case-insensitive comparison to match root "Extension" and child "extension" element
            if (StringComparer.OrdinalIgnoreCase.Equals("extension", nav.PathName) && nav.HasChildren)
            {
                // [WMR 20190919] Handle profile extensions & extension definitions
                var extElem = nav.Current;
                var bm = nav.Bookmark();
                if (nav.MoveToChild("url"))
                {
                    var urlElem = nav.Current;
                    if (!(urlElem is null) && urlElem.Fixed is null)
                    {
                        string profile = null;
                        if (extElem.IsRootElement())
                        {
                            // Initialize extension definitions, but exclude core Extension profile
                            var extDef = nav.StructureDefinition;
                            if (!(extDef is null) && extDef.Derivation == StructureDefinition.TypeDerivationRule.Constraint)
                            {
                                // Extension definition root element: initialize url from canonical
                                profile = nav.StructureDefinition?.Url;
                            }
                        }
                        else
                        {
                            // Profile extension element: initialize url from extension type profile
                            // Complex extension child element: initialize url from slice name
                            profile = extElem.PrimaryTypeProfile() ?? extElem.SliceName;
                        }

                        if (!string.IsNullOrEmpty(profile))
                        {
                            // "magic": Extension.url has system type "xsd:string" but requires a FixedUri (NOT FixedString)
                            // https://chat.fhir.org/#narrow/stream/179177-conformance/topic/Extension.2Eurl.20-.20fixedString.20or.20fixedUri.3F

                            urlElem.Fixed = new FhirUri(profile);
                        }
                    }
                    nav.ReturnToBookmark(bm);
                }
            }
        }

        private async T.Task startSlice(ElementDefinitionNavigator snap, ElementDefinitionNavigator diff, ElementDefinitionNavigator sliceBase)
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
            if (slicingEntry.SliceName != null && ElementDefinitionNavigator.IsSiblingSliceOf(snap.Current.SliceName, slicingEntry.SliceName))
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

                    // [WMR 20170711] Explicitly re-generate the extension element id
                    if (_settings.GenerateElementIds)
                    {
                        ElementIdGenerator.Update(snap, true);
                    }
                }
                else
                {
                    // [WMR 20161222] Recursively merge diff constraints on slicing entry and child elements (if any)
                    await mergeElement(snap, diff).ConfigureAwait(false);
                }
            }
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

        private async T.Task addSlice(ElementDefinitionNavigator snap, ElementDefinitionNavigator diff, ElementDefinitionNavigator sliceBase)
        {
            // Debug.Print($"[{nameof(SnapshotGenerator)}.{nameof(addSlice)}] Base Path = '{snap.Path}' Base Slice Name = '{snap.Current.Name}' Diff Slice Name = {sliceName}");

            // base has no name => diff is new slice; add after last existing slice (if any)
            // base is named => diff is new reslice; add after last existing reslice (if any)

            // Debug.Assert(diff.Current.Name != null);
            Debug.Assert(snap.Current.SliceName == null

                // [WMR 20190808] Handle new named slice in derived profile
                // - base profile is sliced, diff profile introduces another named slice
                // - snap is positioned on (last) named base slice
                // - diff is positioned on new named base slice (introduced by derived profile)
                // - sliceBase is positioned on base slice group
                || (!(sliceBase is null))

                // [WMR 20161219] Handle Composition.section - has default name 'section' in core resource (name reference target for Composition.section.section)
                || snap.Path == "Composition.section"
                || diff.Current.SliceName == null
                || ElementDefinitionNavigator.IsDirectResliceOf(diff.Current.SliceName, snap.Current.SliceName));


            bool isRenamed = !IsEqualPath(snap.PathName, diff.PathName);

            // [WMR 20190822] R4 TODO
            // Emit default Slicing component for type slices, if omitted
            if (isRenamed && snap.Current.Slicing is null)
            {
                snap.Current.Slicing = new ElementDefinition.SlicingComponent()
                {
                    Discriminator = new List<ElementDefinition.DiscriminatorComponent>()
                    {
                        ElementDefinition.DiscriminatorComponent.ForTypeSlice()
                    }
                };
            };

            // Append the new slice constraint to the existing slice group
            // Slice definitions in StructureDef are always ordered! (only instances may contain unordered slices)
            // diff must specify constraints on existing slices in original order (just like regular elements)
            // diff can only append new slices after all constraints on existing slices
            addSliceBase(snap, diff, sliceBase);

            // [WMR 20161219] Handle invalid multiple renamed choice type constraints, e.g. { valueString, valueInteger }
            // Snapshot base element has already been renamed by the first match => re-assign
            if (isRenamed)
            {
                // [WMR 20190819] NEW: #1074
                // Support implicit type constraints on renamed elements
                applyImplicitChoiceTypeConstraint(snap, diff);

                // [WMR 20190819] NEW: Auto-generate default slice name for (implicit) type slices
                if (string.IsNullOrEmpty(snap.Current.SliceName) && string.IsNullOrEmpty(diff.Current.SliceName))
                {
                    snap.Current.SliceName = diff.PathName;
                }

#if !NORMALIZE_RENAMED_TYPESLICE
                // [WMR 20190828] R4: Snapshot always represents type slice using full slicing notation
                // Must undo element renaming, e.g. "value[x]:valueString", not "valueString"
                snap.Current.Path = diff.Path; // Current.Path;
#endif
            }

            // Important: explicitly clear the slicing node in the copy!
            Debug.Assert(snap.Current.Slicing is null); // Taken care of by ElementMatcher.initSliceBase
                                                        // snap.Current.Slicing = null;

            // [WMR 20170718] NEW
            // Update base annotations for named slices
            // MUST replace existing annotations copied from sliceBase!
            // Named slices should get base with Min = 0
            prepareSliceElements(snap, sliceBase);

            // Notify clients about a snapshot element with differential constraints
            OnConstraint(snap.Current);

            // Merge differential
            await mergeElement(snap, diff).ConfigureAwait(false);
        }

        // [WMR 20190819] NEW
        // Renamed choice type element implies type constraint
        // e.g. "valueQuantity" implies type is constrained to Quantity

        // Parse type from renamed choice type element and constrain snap types.
        void applyImplicitChoiceTypeConstraint(ElementDefinitionNavigator snap, ElementDefinitionNavigator diff)
        {
            Debug.Assert(!IsEqualPath(snap.PathName, diff.PathName));

            var diffTypes = diff.Current.Type;
            if (diffTypes is null || diffTypes.Count == 0)
            {
                // [WMR 20190819] NEW: #1074
                // Parse type constraint from renamed element
                var primaryDiffType = ElementDefinitionNavigator.ParseTypeFromRenamedElement(diff.PathName, snap.PathName);

                if (!string.IsNullOrEmpty(primaryDiffType))
                {
                    // Try to find matching type in base element
                    var match = snap.Current.Type.FirstOrDefault(t => StringComparer.Ordinal.Equals(primaryDiffType, t.Code));
                    if (match is null)
                    {
                        // No match; error! Invalid choice type constraint
                        addIssueInvalidChoiceRename(diff.Current);
                    }
                    else if (snap.Current.Type.Count > 0)
                    {
                        // Found match; constrain to specified type
                        //snap.Current.Type.RemoveAll(t => !StringComparer.Ordinal.Equals(primaryDiffType, t.Code));
                        snap.Current.Type = new List<ElementDefinition.TypeRefComponent>() { match };
                    }
                }
            }
        }

        void addSliceBase(ElementDefinitionNavigator snap, ElementDefinitionNavigator diff, ElementDefinitionNavigator sliceBase)
        {
            var lastSlice = findSliceAddPosition(snap, diff);
            bool result = false;

            if (sliceBase == null || sliceBase.Current == null)
            {
                Debug.Fail("SHOULDN'T HAPPEN...");
                throw Error.InvalidOperation($"Internal error in snapshot generator ({nameof(addSlice)}): slice base element is unavailable.");
            }

            result = snap.ReturnToBookmark(lastSlice);
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
            var sliceName = diff.Current.SliceName;

            // [WMR 20190211] R4: Allow multiple renamed choice type element constraints
            // e.g. value[x], valueString, valueInteger
            if (string.IsNullOrEmpty(sliceName) //&& snap.IsRenamedChoiceTypeElement(diff.PathName))
                && ElementDefinitionNavigator.IsRenamedChoiceTypeElement(snap.PathName, diff.PathName))
            {
                // snap is positioned at a choice type element, e.g. "value[x]"
                // diff is positioned at a renamed choice type element constraint, e.g. "valueString"
                // Append new diff constraint after existing renamed choice type element constraints in snap
                var choiceName = snap.PathName;
                var bm2 = snap.Bookmark();
                while (snap.MoveToNext())
                {
                    // [WMR 20190828] Also handle non-renamed type slices in the snapshot
                    // R4: Type slices in snapshot must be normalized (not renamed)
                    // Still try to handle renamed elements, e.g. from externally generated snapshots
                    if (!IsEqualName(snap.PathName, choiceName)
                        && !ElementDefinitionNavigator.IsRenamedChoiceTypeElement(choiceName, snap.PathName))
                    {
                        // Not a choice type element constraint; rewind to last match
                        snap.ReturnToBookmark(bm2);
                        break;
                    }
                    bm2 = snap.Bookmark();
                }
            }
            else
            {
                //Debug.Assert(sliceName != null);
                var baseSliceName = ElementDefinitionNavigator.GetBaseSliceName(sliceName);
                do
                {
                    var snapSliceName = snap.Current.SliceName;
                    if (baseSliceName != null && StringComparer.Ordinal.Equals(baseSliceName, snapSliceName))
                    {
                        // Found a matching base slice; skip any children and reslices
                        var bm2 = snap.Bookmark();
                        while (snap.MoveToNext(name))
                        {
                            var snapSliceName2 = snap.Current.SliceName;
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
            }
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
                //Discriminator = new List<ElementDefinition.DiscriminatorComponent>()
                //{
                //    new ElementDefinition.DiscriminatorComponent
                //    {
                //        Type = ElementDefinition.DiscriminatorType.Value,
                //        Path = "url"
                //    }
                //},
                Discriminator = ElementDefinition.DiscriminatorComponent.ForExtensionSlice().ToList(),
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

        private async T.Task<StructureDefinition> getStructureForElementType(ElementDefinition elementDef, bool ensureSnapshot)
        {
            Debug.Assert(elementDef != null);
            // Debug.Assert(elementDef.Type.Count > 0);
            var primaryType = elementDef.PrimaryType(); // Ignore any other types
            return primaryType != null ? await getStructureForTypeRef(elementDef, primaryType, ensureSnapshot).ConfigureAwait(false) : null;
        }

        // Resolve StructureDefinition for the specified typeRef component
        // Expand snapshot and generate ElementDefinition.Base components if necessary
        private async T.Task<StructureDefinition> getStructureForTypeRef(ElementDefinition elementDef, ElementDefinition.TypeRefComponent typeRef, bool ensureSnapshot)
        {
            var location = elementDef.Path;
            StructureDefinition baseStructure = null;

            // [WMR 20160720] Handle custom type profiles (GForge #9791)
            bool isValidProfile = false;

            // [WMR 20181212] R4 NEW
            // Resolve target profile if the type specifies a _single_ profile.
            // Return null if the type specifies zero or multiple profiles.
            var typeProfile = typeRef.Profile.SafeSingleOrDefault();

            // [WMR 20161004] Remove configuration setting; always merge type profiles
            // [WMR 20180723] Also expand custom profile on Reference
            if (!string.IsNullOrEmpty(typeProfile)) // && !typeRef.IsReference()) // && _settings.MergeTypeProfiles
            {
                // Try to resolve the custom element type profile reference
                baseStructure = await AsyncResolver.FindStructureDefinitionAsync(typeProfile).ConfigureAwait(false);
                isValidProfile = ensureSnapshot
                    ? await this.ensureSnapshot(baseStructure, typeProfile, location).ConfigureAwait(false)
                    : this.verifyStructure(baseStructure, typeProfile, location);
            }

            // Otherwise, or if the custom type profile is missing, then try to resolve the core type profile
            // [MV 20191217] stop when it is a special type (System.*). Introduced in the technical correction 4.0.1
            var typeCodeElem = typeRef.CodeElement;
            if (!isValidProfile && typeCodeElem != null && typeCodeElem.ObjectValue is string typeName && !typeName.StartsWith("http://hl7.org/fhirpath/System."))
            {
                baseStructure = await getStructureDefinitionForTypeCode(AsyncResolver, typeCodeElem).ConfigureAwait(false);
                // [WMR 20160906] Check if element type equals path (e.g. Resource root element), prevent infinite recursion
                _ = (IsEqualPath(typeName, location)) ||
                    (
                        ensureSnapshot
                        ? await this.ensureSnapshot(baseStructure, typeName, location).ConfigureAwait(false)
                        : this.verifyStructure(baseStructure, typeName, location)
                    );

            }

            return baseStructure;
        }

        /// <summary>Resolve a <see cref="StructureDefinition"/> from a TypeRef.Code element, handle unknown/custom core types.</summary>
        /// <param name="resolver">An <see cref="IArtifactSource"/> reference.</param>
        /// <param name="typeCodeElement">A <see cref="ElementDefinition.TypeRefComponent.CodeElement"/> reference.</param>
        /// <returns>A <see cref="StructureDefinition"/> instance, or <c>null</c>.</returns>
        private async static T.Task<StructureDefinition> getStructureDefinitionForTypeCode(IAsyncResourceResolver resolver, FhirUri typeCodeElement)
        {
            StructureDefinition sd = null;
            var typeCode = typeCodeElement.Value;
            if (!string.IsNullOrEmpty(typeCode))
            {
                sd = await resolver.FindStructureDefinitionForCoreTypeAsync(typeCode).ConfigureAwait(false);
            }
            else
            {
                // Unknown/custom core type; try to resolve from raw object value
                var typeName = typeCodeElement.ObjectValue as string;
                if (!string.IsNullOrEmpty(typeName))
                {
                    sd = await resolver.FindStructureDefinitionForCoreTypeAsync(typeName).ConfigureAwait(false);
                }
            }
            return sd;
        }


#if FIX_CONTENTREFERENCE
        // [WMR 20180410] Resolve the defining target structure of a contentReference
        private async T.Task<StructureDefinition> getStructureForContentReference(ElementDefinitionNavigator nav, bool ensureSnapshot)
        {
            Debug.Assert(nav != null);
            Debug.Assert(nav.Current != null);

            var elementDef = nav.Current;
            var location = elementDef.Path;

            var contentReference = elementDef.ContentReference; // e.g. "#Questionnaire.item"

            // [WMR 20181212] TODO: Handle logical models, where StructureDefinition.type returns an uri

            var coreType = nav.StructureDefinition?.Type
                // Fall back to root element name...?
                ?? ElementDefinitionNavigator.GetPathRoot(contentReference.Substring(1));

            if (!string.IsNullOrEmpty(coreType))
            {
                // Try to resolve the custom element type profile reference
                var coreSd = await AsyncResolver.FindStructureDefinitionForCoreTypeAsync(coreType).ConfigureAwait(false);
                _ = ensureSnapshot
                    ? await this.ensureSnapshot(coreSd, coreType, location).ConfigureAwait(false)
                    : this.verifyStructure(coreSd, coreType, location);
                return coreSd;
            }

            return null;
        }
#endif

        bool verifyStructure(StructureDefinition sd, string profileUrl, string location = null)
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
        private async T.Task<bool> ensureSnapshot(StructureDefinition sd, string profileUri, string location = null)
        {
            if (!verifyStructure(sd, profileUri, location)) { return false; }
            profileUri = sd.Url;

            // Detect infinite recursion
            // Verify that the type profile is not already being expanded by a parent call higher up the call stack hierarchy
            // Special case: when recursing on Element, simply return true and continue; otherwise throw an exception
            var path = location ?? null;
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
                        Element = await generate(sd).ConfigureAwait(false)
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
                await ensureSnapshotBaseComponents(sd).ConfigureAwait(false);
            }
            finally
            {
                _stack.OnAfterExpandTypeProfile(profileUri, path);
            }

            return true;
        }

        // Resolve the base element definition for the specified element = the snapshot root element of the associated type profile
        private async T.Task<(ElementDefinition, StructureDefinition typeProfile)> getBaseElementForElementType(ElementDefinition elementDef)
        {
            Debug.Assert(elementDef != null);
            if (elementDef == null)
            {
                throw Error.ArgumentNull(nameof(elementDef), $"Internal error in Snapshot Generator ({nameof(getBaseElementForElementType)}): the specified element definition is null.");
            }

            // Debug.Assert(elementDef.Type.Count > 0);
            var primaryType = elementDef.PrimaryType(); // Ignore any other types
            return primaryType != null ? await getBaseElementForTypeRef(elementDef, primaryType).ConfigureAwait(false) : default;
        }

        // Resolve the base element definition for the specified element type = the snapshot root element of the associated type profile
        private async T.Task<(ElementDefinition, StructureDefinition typeProfile)> getBaseElementForTypeRef(ElementDefinition elementDef, ElementDefinition.TypeRefComponent typeRef)
        {
            var typeProfile = await getStructureForTypeRef(elementDef, typeRef, false).ConfigureAwait(false);

            return typeProfile != null ?
                (await getSnapshotRootElement(typeProfile, typeProfile.Url, elementDef.Path).ConfigureAwait(false), typeProfile)
                : default;
        }

        // Returns the snapshot root element of the specified profile
        // Try to resolve from existing snapshot, if it exists and is valid
        // Try to resolve from partial snapshot, if it is currently being generated (higher up on the stack)
        // Otherwise recursively resolve the associated base profile root element definition (if it exists) and merge with differential root
        private async T.Task<ElementDefinition> getSnapshotRootElement(StructureDefinition sd, string profileUri, string location)
        {
            // Debug.Print("[SnapshotGenerator.getSnapshotRootElement] profileUri = '{0}' - resolving root element definition...", profileUri);
            if (!verifyStructure(sd, profileUri, location)) { return null; }

            if (sd.Differential == null || sd.Differential.Element == null || sd.Differential.Element.Count == 0)
            {
                // TODO: Handle empty diff (=> return root element of base profile)
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

            // [WMR 20170524] In STU3, differential may be sparse
            // => first element is NOT guaranteed to be the root element!
            // var diffRoot = sd.Differential.Element[0];
            var diffRoot = sd.Differential.GetRootElement();

#if FIX_SLICENAMES_ON_ROOT_ELEMENTS
            // [WMR 20170810] Handle bug in STU3
            // SimpleQuantity root element defines non-null sliceName = "SimpleQuantity" - WRONG!
            fixInvalidSliceNameOnRootElement(diffRoot, sd);
#endif

            var baseProfileUri = sd.BaseDefinition;

            if (baseProfileUri == null)
            {
                if (diffRoot == null)
                {
                    addIssueProfileHasNoDifferential(location, profileUri);
                    return null;
                }

                // Structure has no base, i.e. core type definition => differential introduces & defines the root element
                // No need to rebase, nothing to merge
                var snapRoot = (ElementDefinition)diffRoot.DeepCopy();
                snapRoot.Extension.Clear();

#if CACHE_ROOT_ELEMDEF
                Debug.Assert(!snapRoot.HasSnapshotElementAnnotation());

                snapRoot.EnsureBaseComponent(null, true);
                sd.SetSnapshotRootElementAnnotation(snapRoot);

                //Debug.WriteLine($"[{nameof(SnapshotGenerator)}.{nameof(getSnapshotRootElement)} ANNOTATIONS] Cache root element definition for structure '{sd.Name}': '{clonedDiffRoot.Path}'  #{clonedDiffRoot.GetHashCode()}");
                // [WMR 20190805] IMPORTANT! Always explicitly clear the annotation before returning result to caller
                // Otherwise, cloning & expanding the result will pick up incorrect root element from original... WRONG!
#endif
                // [WMR 20190723] FIX #1052: Initialize ElementDefinition.constraint.source
                ElementDefnMerger.InitializeConstraintSource(snapRoot.Constraint, sd.Url);

                // Debug.Print($"[{nameof(SnapshotGenerator)}.{nameof(getSnapshotRootElement)}] {nameof(profileUri)} = '{profileUri}' - use root element definition from differential: #{clonedDiffRoot.GetHashCode()}");
                return snapRoot;
            }

            // Recursively resolve root element definition from base profile

            // TODO: Recursion detection / protection
            // e.g. detect cycle in StructureDefinition.Base references
            // FHIR types and resources have no such cycles, but an attacker could abuse this
            // Note that we need to use a separate stack, as we may need to expand the root element of a
            // profile that is currently being fully expanded, i.e. the url is already on the main stack.

            // Debug.Print($"[{nameof(SnapshotGenerator)}.{nameof(getSnapshotRootElement)}] {nameof(profileUri)} = '{profileUri}' - recursively resolve root element definition from base profile '{baseProfileUri}' ...");
            var sdBase = await AsyncResolver.FindStructureDefinitionAsync(baseProfileUri).ConfigureAwait(false);
            // [WMR 20180108] diffRoot may be null (sparse differential w/o root)
            var baseRoot = await getSnapshotRootElement(sdBase, baseProfileUri, diffRoot?.Path).ConfigureAwait(false); // Recursion!

            if (baseRoot == null)
            {
                addIssueSnapshotGenerationFailed(baseProfileUri);
                return null;
            }

            // Clone and rebase
            var rebasedRoot = (ElementDefinition)baseRoot.DeepCopy();

#if CACHE_ROOT_ELEMDEF
            // [WMR 20190806] Paranoia: never clone temporary internal annotation
            Debug.Assert(!rebasedRoot.HasSnapshotElementAnnotation());
            //rebasedRoot.RemoveSnapshotElementAnnotations(); // Paranoia...
#endif

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

            //Debug.WriteLine($"[{nameof(SnapshotGenerator)}.{nameof(getSnapshotRootElement)}] Annotation snapshot root element for structure '{sd.Name}': '{rebasedRoot.Path}'  #{rebasedRoot.GetHashCode()}");
#endif

            // Notify observers
            OnPrepareElement(rebasedRoot, sdBase, baseRoot);

            return rebasedRoot;
        }

        /// <summary>Determine if the specified element paths are equal. Performs an ordinal comparison.</summary>
        internal static bool IsEqualPath(string path, string other) => StringComparer.Ordinal.Equals(path, other);

        /// <summary>Determine if the specified element names are equal. Performs an ordinal comparison.</summary>
        internal static bool IsEqualName(string name, string other) => StringComparer.Ordinal.Equals(name, other);

        /// <summary>Determine if the specified uri strings are equal. Performs an ordinal comparison.</summary>
        internal static bool IsEqualUri(string uri, string other) => StringComparer.Ordinal.Equals(uri, other);

        /// <summary>Determine if the specified type codes are equal. Performs an ordinal comparison.</summary>
        internal static bool IsEqualType(string type, string other) => StringComparer.Ordinal.Equals(type, other);

        /// <summary>
        /// Determines if the specified <see cref="StructureDefinition"/> is compatible with <paramref name="type"/>.
        /// Walks up the profile hierarchy by resolving base profiles from the current <see cref="IResourceResolver"/> instance.
        /// </summary>
        /// <returns><c>true</c> if the profile type is equal to or derived from the specified type, or <c>false</c> otherwise.</returns>
        private static async T.Task<bool> isValidTypeProfile(IAsyncResourceResolver resolver, string type, StructureDefinition profile)
        {
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));

            return await isValidTypeProfile(resolver, new HashSet<string>(), type, profile).ConfigureAwait(false);
        }

        private static async T.Task<bool> isValidTypeProfile(IAsyncResourceResolver resolver, HashSet<string> recursionStack, string type, StructureDefinition profile)
        {
            // Recursively walk up the base profile hierarchy until we find a profile on baseType
            if (type == null) { return true; }
            if (profile == null) { return true; }

            var sdType = profile.Type;

            if (sdType == type) { return true; }
            if (profile.BaseDefinition == null) { return false; }
            var sdBase = await resolver.FindStructureDefinitionAsync(profile.BaseDefinition).ConfigureAwait(false);
            if (sdBase == null) { return false; }
            if (sdBase.Url == null) { return false; } // Shouldn't happen...

            // Detect/prevent endless recursion... e.g. X.Base = Y and Y.Base = X
            if (!recursionStack.Add(sdBase.Url))
            {
                throw Error.InvalidOperation(
                    $"Recursive profile dependency detected. Base profile hierarchy:\r\n{string.Join("\r\n", recursionStack)}"
                );
            }

            return await isValidTypeProfile(resolver, recursionStack, type, sdBase).ConfigureAwait(false);
        }
    }
}
