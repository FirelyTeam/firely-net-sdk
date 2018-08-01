/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

// [WMR 20170810] STU3 bug: SimpleQuantity root element introduces non-empty sliceName = "SliceName"
// Detect and fix invalid non-null sliceNames on root elements
#define FIX_SLICENAMES_ON_ROOT_ELEMENTS

using System;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System.Linq;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System.Diagnostics;

#pragma warning disable 1591 // suppress XML summary warnings

// [WMR 20160907] TODO: Create unit tests to evaluate behavior for different kinds of errors, e.g.
// - unresolved external (type/extension) profile
// - invalid element order
// - invalid constraint element (no base)
// - ...

namespace Hl7.Fhir.Specification.Snapshot
{
    // Static OperationOutcome.IssueComponent definitions for the SnapshotGenerator
    // Requires Hl7.Fhir.Validation.Issue

    static class SnapshotGeneratorExtensionMethods
    {
        /// <summary>Returns a new <see cref="SnapshotGenerator.ElementDefinitionNamedNode"/> wrapper for the specified <see cref="ElementDefinition"/> instance.</summary>
        public static IElementNavigator ToNamedNode(this ElementDefinition elementDef) => new SnapshotGenerator.ElementDefinitionNamedNode(elementDef);
    }

    public partial class SnapshotGenerator
    {
        // [WMR 20160905] Note: if we call ourselves recursively, all the child issues are added to the same shared OutCome instance
        // Alternatively, aggregate child issues into a child outcome...
        private OperationOutcome _outcome;

        /// <summary>
        /// Returns <c>null</c> if the snapshot generation completed without issues.
        /// Otherwise returns a descriptive <see cref="OperationOutcome"/> instance with a list of encountered issues.
        /// </summary>
        public OperationOutcome Outcome => _outcome;

        // [WMR 20160905] TODO
        // Temporary adapter for ElementDefinition to support INamedNode
        // TODO: ElementDefinition should properly implement INamedNode
        // INamedNode.Path should also return indices, e.g. root.elem[0].elem[1]
        internal sealed class ElementDefinitionNamedNode : IElementNavigator
        {
            // [WMR 20161213] Don't save reference to ElementDefinition, don't keep instance alive
            readonly string _path;
            readonly string _name;

            public ElementDefinitionNamedNode(ElementDefinition elementDef)
            {
                if (elementDef == null) { throw Error.ArgumentNull(nameof(elementDef)); }
                _path = elementDef.Path;
                _name = elementDef.Name;
            }
            public string Name => _name;
            public string Location => _path;

            public string Type => ModelInfo.FhirTypeToFhirTypeName(FHIRDefinedType.ElementDefinition); // _elemDef.TypeName;

            public object Value { get { throw new NotImplementedException(); } }

            public IElementNavigator Clone() { throw new NotImplementedException(); }

            public bool MoveToFirstChild(string nameFilter = null) { throw new NotImplementedException(); }

            public bool MoveToNext(string nameFilter = null) { throw new NotImplementedException(); }

            public override string ToString() => string.IsNullOrEmpty(Name) ? $"'{Location}'" : $"'{Location}' : '{Name}'";
        }

        // static IElementNavigator ToNamedNode(ElementDefinition elementDef) => new ElementDefinitionNamedNode(elementDef);

        void clearIssues() { _outcome = null; }

        OperationOutcome.IssueComponent addIssue(Issue issue, string message, IElementNavigator location = null, string profileUrl = null)
        {
            if (issue == null) { throw Error.ArgumentNull(nameof(issue)); }
            return addIssue(issue.ToIssueComponent(message, location), profileUrl);
        }

        OperationOutcome.IssueComponent addIssue(OperationOutcome.IssueComponent component, string profileUrl = null)
        {
            if (component == null) { throw Error.ArgumentNull(nameof(component)); }
            if (_outcome == null) { _outcome = new OperationOutcome(); }
            // Return current profile url in Diagnostics field
            component.Diagnostics = profileUrl ?? CurrentProfileUri;
            _outcome.AddIssue(component);
            return component;
        }

        // Content errors

        // "Differential has a constraint on a choice element '{0}', but does so without using a type slice"
        // Differential specifies a constraint on a child element of a choice type element
        // This is not allowed if an element supports multiple element types; must use slicing!
        public static readonly Issue PROFILE_ELEMENTDEF_INVALID_CHOICE_CONSTRAINT = Issue.Create(10000, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        void addIssueInvalidChoiceConstraint(ElementDefinition elementDef) { addIssue(CreateIssueInvalidChoiceConstraint(elementDef)); }

        internal static OperationOutcome.IssueComponent CreateIssueInvalidChoiceConstraint(ElementDefinition elementDef)
        {
            var location = elementDef.ToNamedNode();
            return PROFILE_ELEMENTDEF_INVALID_CHOICE_CONSTRAINT.ToIssueComponent(
                $"Differential specifies constraint on choice element {location} without using type slice.",
                location
            );
        }

        // Snapshot's element turns out not to be expandable, so we can't move to the desired path
        // "Differential has nested constraints for node '{0}', but this is a leaf node in base"
        // Differential specifies constraint on child element of a leaf node in the base, i.e. node without child elements
        //public static readonly Issue PROFILE_ELEMENTDEF_INVALID_CHILD_CONSTRAINT = Issue.Create(10001, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        //void addIssueInvalidChildConstraint(ElementDefinition elementDef) { addIssueInvalidChildConstraint(ToNamedNode(elementDef)); }
        //void addIssueInvalidChildConstraint(INamedNode location)
        //{
        //    addIssue(
        //        PROFILE_ELEMENTDEF_INVALID_CHOICE_CONSTRAINT,
        //        $"Differential specifies invalid child constraints on leaf element {location}.",
        //        location
        //    );
        //}

        void addIssueInvalidNameReference(ElementDefinition elementDef) { addIssue(CreateIssueInvalidNameReference(elementDef)); }

        internal static OperationOutcome.IssueComponent CreateIssueInvalidNameReference(ElementDefinition elementDef)
        {
            var location = elementDef.ToNamedNode();
            var nameRef = elementDef.NameReference;
            return PROFILE_ELEMENTDEF_INVALID_TYPEPROFILE_NAMEREF.ToIssueComponent(
                $"Element {location} has a nameReference to '{nameRef}', which cannot be found in the StructureDefinition.",
                location
            );
        }

        void addIssueNoTypeOrNameReference(ElementDefinition elementDef) { addIssueNoTypeOrNameReference(elementDef.ToNamedNode()); }
        void addIssueNoTypeOrNameReference(IElementNavigator location)
        {
            addIssue(
                Issue.PROFILE_ELEMENTDEF_CONTAINS_NO_TYPE_OR_NAMEREF,
                $"Element {location} has neither a type nor a nameReference.",
                location
            );
        }

        // "Type profile '{0}' has an invalid name reference. The base profile does not contain an element with name '{1}'"
        public static readonly Issue PROFILE_ELEMENTDEF_INVALID_TYPEPROFILE_NAMEREF = Issue.Create(10002, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        void addIssueInvalidProfileNameReference(ElementDefinition elementDef, string name, string profileRef) { addIssueInvalidProfileNameReference(elementDef.ToNamedNode(), name, profileRef); }
        void addIssueInvalidProfileNameReference(IElementNavigator location, string name, string profileRef)
        {
            addIssue(
                PROFILE_ELEMENTDEF_INVALID_TYPEPROFILE_NAMEREF,
                $"Element type profile reference '{profileRef}' has an invalid name reference. The target profile does not contain an element with name '{name}'",
                location
            );
        }

        // "The slicing entry in the differential at '{0}' indicates a slice, but the base element is not a repeating or choice element"
        public static readonly Issue PROFILE_ELEMENTDEF_INVALID_SLICE = Issue.Create(10003, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        void addIssueInvalidSlice(ElementDefinition elementDef) { addIssueInvalidSlice(elementDef.ToNamedNode()); }
        void addIssueInvalidSlice(IElementNavigator location)
        {
            addIssue(
                PROFILE_ELEMENTDEF_INVALID_SLICE,
                $"The slicing entry in the differential at {location} indicates a slice, but the base element is not a repeating or choice element",
                location
            );
        }

        // "The slice group at '{0}' does not start with a slice entry element"
        public static readonly Issue PROFILE_ELEMENTDEF_MISSING_SLICE_ENTRY = Issue.Create(10004, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Required);

        internal static OperationOutcome.IssueComponent CreateIssueMissingSliceEntry(ElementDefinition elementDef)
        {
            var location = elementDef.ToNamedNode();
            return PROFILE_ELEMENTDEF_INVALID_CHOICE_CONSTRAINT.ToIssueComponent(
                $"The slice group at {location} does not start with a slice entry element",
                location
            );
        }

        void addIssueMissingSliceEntry(ElementDefinition elementDef) { addIssue(CreateIssueMissingSliceEntry(elementDef)); }


        // "Differential specification for core resource or datatype definitions does not start with root element definition."
        // public static readonly Issue PROFILE_NO_ROOT = Issue.Create(10004, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        // "Element at path '{0}' has a choice of types, cannot expand"
        public static readonly Issue PROFILE_ELEMENTDEF_CANNOT_EXPAND_CHOICE = Issue.Create(10005, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        // Dependency errors

        // "Unresolved profile reference. Cannot locate the type profile for element '{0}'.\r\nProfile url = '{1}'".FormatWith(diff.Path, primaryDiffTypeProfile)
        // Issue.UNAVAILABLE_REFERENCED_PROFILE_UNAVAILABLE
        void addIssueProfileNotFound(string profileUrl) { addIssueProfileNotFound(null, profileUrl); }
        // void addIssueProfileNotFound(ElementDefinition elementDef, string profileUrl) { addIssueProfileNotFound(ToNamedNode(elementDef), profileUrl); }
        void addIssueProfileNotFound(IElementNavigator location, string profileUrl)
        {
            if (profileUrl == null) { throw Error.ArgumentNull(nameof(profileUrl)); }
            if (location != null)
            {
                addIssue(Issue.UNAVAILABLE_REFERENCED_PROFILE, $"Unable to resolve reference to profile '{profileUrl}' for element {location}", location, profileUrl);
            }
            else
            {
                addIssue(Issue.UNAVAILABLE_REFERENCED_PROFILE, $"Unable to resolve reference to profile '{profileUrl}'", null, profileUrl);
            }
        }

        // "Resolved the external profile with url '{0}', but it does not contain a snapshot representation."
        // Issue.UNAVAILABLE_NEED_SNAPSHOT

        //void addIssueProfileHasNoSnapshot(ElementDefinition elementDef, string profileUrl) { addIssueProfileHasNoSnapshot(ToNamedNode(elementDef), profileUrl); }
        void addIssueProfileHasNoSnapshot(IElementNavigator location, string profileUrl)
        {
            if (profileUrl == null) { throw Error.ArgumentNull(nameof(profileUrl)); }
            addIssue(Issue.UNAVAILABLE_NEED_SNAPSHOT, $"The resolved external profile with url '{profileUrl}' has no snapshot.", location, profileUrl);
        }

        // Issue.UNAVAILABLE_SNAPSHOT_GENERATION_FAILED
        void addIssueSnapshotGenerationFailed(string profileUrl = null)
        {
            if (profileUrl == null) { profileUrl = CurrentProfileUri; } // throw Error.ArgumentNull(nameof(profileUrl));
            addIssue(Issue.UNAVAILABLE_SNAPSHOT_GENERATION_FAILED, $"Snapshot generation failed for profile with url '{profileUrl}'.", null, profileUrl);
        }

        //void addIssueProfileHasNoDifferential(ElementDefinition elementDef, string profileUrl) { addIssueProfileHasNoDifferential(ToNamedNode(elementDef), profileUrl); }
        void addIssueProfileHasNoDifferential(IElementNavigator location, string profileUrl)
        {
            if (profileUrl == null) { throw Error.ArgumentNull(nameof(profileUrl)); }
            addIssue(Issue.UNAVAILABLE_NEED_DIFFERENTIAL, $"The resolved external profile with url '{profileUrl}' has no differential.", location, profileUrl);
        }

        public static readonly Issue PROFILE_ELEMENTDEF_INVALID_EXTENSION_DISCRIMINATOR = Issue.Create(10006, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        internal static OperationOutcome.IssueComponent CreateIssueInvalidExtensionSlicingDiscriminator(ElementDefinition elementDef)
        {
            var location = elementDef.ToNamedNode();
            var discriminators = elementDef.Slicing?.Discriminator;
            var sDiscriminators = discriminators != null && discriminators.Any() ? string.Join("|", elementDef.Slicing?.Discriminator) : "(missing)";
            return PROFILE_ELEMENTDEF_INVALID_EXTENSION_DISCRIMINATOR.ToIssueComponent(
                $"Extension element {location} defines an invalid slicing discriminator: '{sDiscriminators}'. Extensions are always sliced on 'url'.",
                location
            );
        }

        public static readonly Issue PROFILE_ELEMENTDEF_TYPESLICE_WITHOUT_TYPE = Issue.Create(10007, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Required);

        internal static OperationOutcome.IssueComponent CreateIssueTypeSliceWithoutType(ElementDefinition elementDef)
        {
            var location = elementDef.ToNamedNode();
            return PROFILE_ELEMENTDEF_TYPESLICE_WITHOUT_TYPE.ToIssueComponent(
                $"Element {location} is part of a @type slice group, but the element itself has no type.",
                location
            );
        }

        public static readonly Issue PROFILE_ELEMENTDEF_INVALID_SLICE_WITHOUT_NAME = Issue.Create(10008, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Required);

        internal static OperationOutcome.IssueComponent CreateIssueSliceWithoutName(ElementDefinition elementDef)
        {
            var location = elementDef.ToNamedNode();
            return PROFILE_ELEMENTDEF_INVALID_SLICE_WITHOUT_NAME.ToIssueComponent(
                $"Element {location} defines a slice without a name. Individual slices must always have a unique name, except extensions.",
                location
            );
        }

        // [WMR 20170224] NEW - ElementDefinition.Type.Profile target profile has the wrong type
        // e.g. if a profile extension references a StructureDefinition that is not an Extension Definition.
        // or if Identifier element type references a Location profile
        public static readonly Issue PROFILE_ELEMENTDEF_INVALID_PROFILE_TYPE = Issue.Create(10009, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        internal OperationOutcome.IssueComponent addIssueInvalidProfileType(ElementDefinition elementDef, StructureDefinition profile)
        {
            var location = elementDef.ToNamedNode();
            var elemType = elementDef.PrimaryTypeCode();
            var profileType = profile.ConstrainedType ?? ModelInfo.FhirTypeNameToFhirType(profile?.Name);
            return addIssue(
                PROFILE_ELEMENTDEF_INVALID_PROFILE_TYPE.ToIssueComponent(
                    $"Element {location} has an invalid type profile constraint '{profile.Url}'. The target represents a profile on '{profileType}' which is incompatible with the element type '{elemType}'.",
                    location
                )
            );
        }

#if FIX_SLICENAMES_ON_ROOT_ELEMENTS
        // [WMR 20170810] NEW - found a non-empty sliceName on root element
        // STU3 bug: SimpleQuantity root element definition has non-empty sliceName = "SimpleQuantity"

        public static readonly Issue PROFILE_ELEMENTDEF_INVALID_SLICENAME_ON_ROOT = Issue.Create(10010, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        internal OperationOutcome.IssueComponent addIssueInvalidSliceNameOnRootElement(ElementDefinition elementDef, StructureDefinition profile)
        {
            Debug.Assert(!string.IsNullOrEmpty(elementDef.Name));
            Debug.Assert(elementDef.IsRootElement());
            var location = elementDef.ToNamedNode();
            return addIssue(
                PROFILE_ELEMENTDEF_INVALID_SLICENAME_ON_ROOT.ToIssueComponent(
                    $"Element {location} has an invalid non-empty sliceName '{elementDef.Name}'. Root element definitions cannot introduce slice names.",
                    location
                ),
                profile.Url
            );
        }
#endif

    }

}
