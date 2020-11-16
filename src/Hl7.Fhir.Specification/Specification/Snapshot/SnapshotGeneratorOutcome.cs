/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

// #define FIX_SLICENAMES_ON_SPECIALIZATIONS

using System;
using System.Diagnostics;
using System.Linq;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;

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

        void clearIssues() { _outcome = null; }

        OperationOutcome.IssueComponent addIssue(Issue issue, string message, string location = null, string profileUrl = null)
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

        static string FormatLocation(ElementDefinition elemDef)
        {
            if (!string.IsNullOrEmpty(elemDef.ElementId)) { return elemDef.ElementId; }
            if (string.IsNullOrEmpty(elemDef.SliceName)) { return elemDef.Path; }
            return elemDef.Path + ":" + elemDef.SliceName;
        }

        // Content errors

        // [WMR 20190819] OBSOLETE

        // "Differential has a constraint on a choice element '{0}', but does so without using a type slice"
        // Differential specifies a constraint on a child element of a choice type element
        // This is not allowed if an element supports multiple element types; must use slicing!

        //public static readonly Issue PROFILE_ELEMENTDEF_INVALID_CHOICE_CONSTRAINT = Issue.Create(10000, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        //void addIssueInvalidChoiceConstraint(ElementDefinition elementDef) => addIssue(CreateIssueInvalidChoiceConstraint(elementDef));

        //internal static OperationOutcome.IssueComponent CreateIssueInvalidChoiceConstraint(ElementDefinition elementDef)
        //{
        //    var location = FormatLocation(elementDef);
        //    return PROFILE_ELEMENTDEF_INVALID_CHOICE_CONSTRAINT.ToIssueComponent(
        //        $"Differential specifies constraint on choice element {location} without using type slice.",
        //        location
        //    );
        //}

        // [WMR 20170928] NEW
        // Profile introduces constraint on choice type element ("value[x]")
        // while base profile has constrained and renamed the element ("valueString")
        public static readonly Issue PROFILE_ELEMENTDEF_INVALID_CHOICETYPE_NAME = Issue.Create(10012, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        internal static OperationOutcome.IssueComponent CreateIssueInvalidChoiceTypeName(ElementDefinition elementDef, string baseName)
        {
            Debug.Assert(ElementDefinitionNavigator.IsChoiceTypeElement(elementDef.Path));
            Debug.Assert(baseName != null);
            var location = FormatLocation(elementDef);
            return PROFILE_ELEMENTDEF_INVALID_CHOICETYPE_NAME.ToIssueComponent(
                $"Element {location} has an invalid name. The profile should specify the inherited element name '{baseName}'.", 
                location
            );
        }

        // Snapshot's element turns out not to be expandable, so we can't move to the desired path
        // "Differential has nested constraints for node '{0}', but this is a leaf node in base"
        // Differential specifies constraint on child element of a leaf node in the base, i.e. node without child elements
        //public static readonly Issue PROFILE_ELEMENTDEF_INVALID_CHILD_CONSTRAINT = Issue.Create(10001, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        //void addIssueInvalidChildConstraint(ElementDefinition elementDef) { addIssueInvalidChildConstraint(ToNamedNode(elementDef)); }
        //void addIssueInvalidChildConstraint(INamedNode location)
        //    => addIssue(
        //        PROFILE_ELEMENTDEF_INVALID_CHOICE_CONSTRAINT,
        //        $"Differential specifies invalid child constraints on leaf element {location}.",
        //        location
        //    );

        void addIssueInvalidNameReference(ElementDefinition elementDef) => addIssue(CreateIssueInvalidNameReference(elementDef));

        static OperationOutcome.IssueComponent CreateIssueInvalidNameReference(ElementDefinition elementDef)
        {
            var location = FormatLocation(elementDef);
            var nameRef = elementDef.ContentReference;
            return PROFILE_ELEMENTDEF_INVALID_TYPEPROFILE_NAMEREF.ToIssueComponent(
                $"Element {location} has a nameReference to '{nameRef}', which cannot be found in the StructureDefinition.",
                location
            );
        }

        void addIssueNoTypeOrNameReference(ElementDefinition elementDef) { addIssueNoTypeOrNameReference(elementDef.Path); }
        void addIssueNoTypeOrNameReference(string location)
            => addIssue(
                Issue.PROFILE_ELEMENTDEF_CONTAINS_NO_TYPE_OR_NAMEREF,
                $"Element {location} has neither a type nor a nameReference.",
                location
            );

        // "Type profile '{0}' has an invalid name reference. The base profile does not contain an element with name '{1}'"
        public static readonly Issue PROFILE_ELEMENTDEF_INVALID_TYPEPROFILE_NAMEREF = Issue.Create(10002, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        void addIssueInvalidProfileNameReference(ElementDefinition elementDef, string name, string profileRef) { addIssueInvalidProfileNameReference(elementDef.Path, name, profileRef); }
        void addIssueInvalidProfileNameReference(string location, string name, string profileRef)
        {
            addIssue(
                PROFILE_ELEMENTDEF_INVALID_TYPEPROFILE_NAMEREF,
                $"Element type profile reference '{profileRef}' has an invalid name reference. The target profile does not contain an element with name '{name}'",
                location
            );
        }

        // "The slicing entry in the differential at '{0}' indicates a slice, but the base element is not a repeating or choice element"
        public static readonly Issue PROFILE_ELEMENTDEF_INVALID_SLICE = Issue.Create(10003, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        void addIssueInvalidSlice(ElementDefinition elementDef) { addIssueInvalidSlice(elementDef.Path); }
        void addIssueInvalidSlice(string location)
            => addIssue(
                PROFILE_ELEMENTDEF_INVALID_SLICE,
                $"The slicing entry in the differential at {location} indicates a slice, but the base element is not a repeating or choice element",
                location
            );

        // "The slice group at '{0}' does not start with a slice entry element"
        public static readonly Issue PROFILE_ELEMENTDEF_MISSING_SLICE_ENTRY = Issue.Create(10004, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Required);

        internal static OperationOutcome.IssueComponent CreateIssueMissingSliceEntry(ElementDefinition elementDef)
        {
            var location = FormatLocation(elementDef);
            return PROFILE_ELEMENTDEF_MISSING_SLICE_ENTRY.ToIssueComponent(
                $"The slice group at {location} does not start with a slice entry element",
                location
            );
        }

        void addIssueMissingSliceEntry(ElementDefinition elementDef) => addIssue(CreateIssueMissingSliceEntry(elementDef));


        // "Differential specification for core resource or datatype definitions does not start with root element definition."
        // public static readonly Issue PROFILE_NO_ROOT = Issue.Create(10004, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        // [WMR 20190819] OBSOLETE
        // "Element at path '{0}' has a choice of types, cannot expand"
        //public static readonly Issue PROFILE_ELEMENTDEF_CANNOT_EXPAND_CHOICE = Issue.Create(10005, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        // Dependency errors

        // "Unresolved profile reference. Cannot locate the type profile for element '{0}'.\r\nProfile url = '{1}'".FormatWith(diff.Path, primaryDiffTypeProfile)
        // Issue.UNAVAILABLE_REFERENCED_PROFILE_UNAVAILABLE
        void addIssueProfileNotFound(string profileUrl) { addIssueProfileNotFound(null, profileUrl); }
        // void addIssueProfileNotFound(ElementDefinition elementDef, string profileUrl) { addIssueProfileNotFound(ToNamedNode(elementDef), profileUrl); }
        void addIssueProfileNotFound(string location, string profileUrl)
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
        void addIssueProfileHasNoSnapshot(string location, string profileUrl)
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
        void addIssueProfileHasNoDifferential(string location, string profileUrl)
        {
            if (profileUrl == null) { throw Error.ArgumentNull(nameof(profileUrl)); }
            addIssue(Issue.UNAVAILABLE_NEED_DIFFERENTIAL, $"The resolved external profile with url '{profileUrl}' has no differential.", location, profileUrl);
        }

        public static readonly Issue PROFILE_ELEMENTDEF_INVALID_EXTENSION_DISCRIMINATOR = Issue.Create(10006, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        internal static OperationOutcome.IssueComponent CreateIssueInvalidExtensionSlicingDiscriminator(ElementDefinition elementDef)
        {
            var location = FormatLocation(elementDef);
            var discriminators = elementDef.Slicing?.Discriminator;
            var sDiscriminators = discriminators != null && discriminators.Any() ? string.Join("|", elementDef.Slicing?.Discriminator) : "(missing)";
            return PROFILE_ELEMENTDEF_INVALID_EXTENSION_DISCRIMINATOR.ToIssueComponent(
                $"Extension element {location} defines an invalid slicing discriminator: '{sDiscriminators}'. Extensions are always sliced on 'url'.",
                location
            );
        }

        public static readonly Issue PROFILE_ELEMENTDEF_TYPESLICE_WITHOUT_TYPE = Issue.Create(10007, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Required);

        internal static OperationOutcome.IssueComponent createIssueTypeSliceWithoutType(ElementDefinition elementDef)
        {
            var location = FormatLocation(elementDef);
            return PROFILE_ELEMENTDEF_TYPESLICE_WITHOUT_TYPE.ToIssueComponent(
                $"Element {location} is part of a @type slice group, but the element itself has no type.",
                location
            );
        }

        public static readonly Issue PROFILE_ELEMENTDEF_INVALID_SLICE_WITHOUT_NAME = Issue.Create(10008, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Required);

        internal static OperationOutcome.IssueComponent CreateIssueSliceWithoutName(ElementDefinition elementDef)
        {
            var location = FormatLocation(elementDef);
            return PROFILE_ELEMENTDEF_INVALID_SLICE_WITHOUT_NAME.ToIssueComponent(
                $"Element {location} defines a slice without a name. Individual slices must always have a unique name, except extensions.",
                location
            );
        }

        // [WMR 20170224] NEW - ElementDefinition.Type.Profile target profile has the wrong type
        // e.g. if a profile extension references a StructureDefinition that is not an Extension Definition.
        // or if Identifier element type references a Location profile
        public static readonly Issue PROFILE_ELEMENTDEF_INVALID_PROFILE_TYPE = Issue.Create(10009, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        OperationOutcome.IssueComponent addIssueInvalidProfileType(ElementDefinition elementDef, StructureDefinition profile)
        {
            var location = FormatLocation(elementDef);
            var elemType = elementDef.PrimaryTypeCode();
            var profileType = profile.Type;
            return addIssue(
                PROFILE_ELEMENTDEF_INVALID_PROFILE_TYPE.ToIssueComponent(
                    $"Element {location} has an invalid type profile constraint '{profile.Url}'. The target represents a profile on '{profileType}' which is incompatible with the element type '{elemType}'.",
                    location
                )
            );
        }

        // [WMR 20170810] NEW - found a non-empty sliceName on root element
        // STU3 bug: SimpleQuantity root element definition has non-empty sliceName = "SimpleQuantity"

        public static readonly Issue PROFILE_ELEMENTDEF_INVALID_SLICENAME_ON_ROOT = Issue.Create(10010, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        OperationOutcome.IssueComponent addIssueInvalidSliceNameOnRootElement(ElementDefinition elementDef, StructureDefinition profile)
        {
            Debug.Assert(!string.IsNullOrEmpty(elementDef.SliceName));
            Debug.Assert(elementDef.IsRootElement());
            var location = FormatLocation(elementDef);
            return addIssue(
                PROFILE_ELEMENTDEF_INVALID_SLICENAME_ON_ROOT.ToIssueComponent(
                    $"Element {location} has an invalid non-empty sliceName '{elementDef.SliceName}'. Root element definitions cannot introduce slice names.",
                    location
                ),
                profile.Url
            );
        }

        // [WMR 20170810] NEW
        // Found a non-empty sliceName on core resource or datatype definition
#if FIX_SLICENAMES_ON_SPECIALIZATIONS
        public static readonly Issue PROFILE_ELEMENTDEF_INVALID_SLICENAME_ON_SPECIALIZATION = Issue.Create(10011, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        OperationOutcome.IssueComponent addIssueInvalidSliceNameOnSpecialization(ElementDefinition elementDef)
        {
            Debug.Assert(!string.IsNullOrEmpty(elementDef.SliceName));
            var location = FormatLocation(elementDef);
            return addIssue(
                PROFILE_ELEMENTDEF_INVALID_SLICENAME_ON_SPECIALIZATION.ToIssueComponent(
                    $"Element {location} has an invalid non-empty sliceName '{elementDef.SliceName}'. Core resource and datatype definitions cannot introduce slice names.",
                    location
                )
            );
        }
#endif

        // [WMR 20181211] R4: NEW

        // Constraining named slice w/o matching slice in base profile
        public static readonly Issue PROFILE_ELEMENTDEF_SLICENAME_NOMATCH = Issue.Create(10012, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        internal static OperationOutcome.IssueComponent CreateIssueSliceNameNoMatch(ElementDefinition elementDef)
        {
            var sliceName = elementDef.SliceName;
            Debug.Assert(!string.IsNullOrEmpty(sliceName));
            Debug.Assert(elementDef.SliceIsConstraining == true);
            var location = FormatLocation(elementDef);
            return PROFILE_ELEMENTDEF_SLICENAME_NOMATCH.ToIssueComponent(
                $"Element '{location}' with slice name '{sliceName}' constrains an existing slice, but the base profile does not include a matching named slice.",
                location
            );
        }

        // New named slice conflicting with existing named slice in base profile
        public static readonly Issue PROFILE_ELEMENTDEF_SLICENAME_CONFLICT = Issue.Create(10013, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);
        internal static OperationOutcome.IssueComponent CreateIssueSliceNameConflict(ElementDefinition elementDef)
        {
            var sliceName = elementDef.SliceName;
            Debug.Assert(!string.IsNullOrEmpty(sliceName));
            Debug.Assert(elementDef.SliceIsConstraining != true);
            var location = FormatLocation(elementDef);
            return PROFILE_ELEMENTDEF_SLICENAME_CONFLICT.ToIssueComponent(
                $"Element '{location}' with slice name '{sliceName}' introduces a new slice, but the name conflicts with an existing slice in the base profile.",
                location
            );
        }

        // [WMR 20190819] NEW
        // "Differential specifies a renamed choice type element '{0}' for an invalid type that is not supported by the base element."
        public static readonly Issue PROFILE_ELEMENTDEF_INVALID_CHOICE_RENAME = Issue.Create(10014, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        void addIssueInvalidChoiceRename(ElementDefinition elementDef) => addIssue(CreateIssueInvalidChoiceRename(elementDef));

        internal static OperationOutcome.IssueComponent CreateIssueInvalidChoiceRename(ElementDefinition elementDef)
        {
            var location = FormatLocation(elementDef);
            return PROFILE_ELEMENTDEF_INVALID_CHOICE_RENAME.ToIssueComponent(
                $"Differential specifies an invalid renamed choice type element '{location}'. The specified type constraint is not supported by the base element.",
                location
            );
        }

        public static readonly Issue PROFILE_ELEMENTDEF_INVALID_COMPLEX_REFERENCE = Issue.Create(10015, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        void addIssueInvalidComplexProfileReference(ElementDefinition elementDef) => addIssue(CreateIssueInvalidComplexProfileReference(elementDef));

        static OperationOutcome.IssueComponent CreateIssueInvalidComplexProfileReference(ElementDefinition elementDef)
        {
            var location = FormatLocation(elementDef);
            return PROFILE_ELEMENTDEF_INVALID_COMPLEX_REFERENCE.ToIssueComponent(
                $"Differential specifies an invalid deep profile reference at element '{location}'. Url bookmark should match the slice name '{elementDef.SliceName}' of the constrained child element.",
                location
            );
        }

        // [WMR 20190828] Informational message to indicate generated SliceName

        public static readonly Issue PROFILE_ELEMENTDEF_SLICENAME_GENERATED = Issue.Create(10016, OperationOutcome.IssueSeverity.Information, OperationOutcome.IssueType.Incomplete);

        void addIssueSliceNameGenerated(ElementDefinition elementDef) => addIssue(CreateIssueSliceNameGenerated(elementDef));

        internal static OperationOutcome.IssueComponent CreateIssueSliceNameGenerated(ElementDefinition elementDef)
        {
            var location = FormatLocation(elementDef);
            return PROFILE_ELEMENTDEF_SLICENAME_GENERATED.ToIssueComponent(
                $"Generated missing slice name '{elementDef.SliceName}' for element '{location}'",
                location
            );
        }

        // [WMR 20190902] #1090 SnapshotGenerator should support logical models
        // StructureDefinition.type (1...1) is empty or missing
        // However for logical models we only need root element name, can parse from first element constraint
        public static readonly Issue PROFILE_STRUCTURE_TYPE_MISSING = Issue.Create(10014, OperationOutcome.IssueSeverity.Warning, OperationOutcome.IssueType.Required);

        internal OperationOutcome.IssueComponent addIssueStructureTypeMissing(StructureDefinition sd)
        {
            return addIssue(PROFILE_STRUCTURE_TYPE_MISSING.ToIssueComponent(
                $"The StructureDefinition.type property is empty or missing.",
                sd.Name
            ));
        }
    }
}