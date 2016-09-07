using System;
using Hl7.Fhir.Model;
using Hl7.Fhir.Validation;
using Hl7.ElementModel;
using Hl7.Fhir.Support;

// [WMR 20160907] TODO: Add StructureDefinition.url value to issue data
// Requires global access to the current StructDef url - recursion checker could provide this

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
        public OperationOutcome Outcome { get { return _outcome; } }

        // [WMR 20160905] TODO
        // Temporary adapter for ElementDefinition to support INamedNode
        // TODO: ElementDefinition should properly implement INamedNode
        // INamedNode.Path should also return indices, e.g. root.elem[0].elem[1]
        class ElementDefinitionNamedNode : INamedNode
        {
            private readonly ElementDefinition _elemDef;
            public ElementDefinitionNamedNode(ElementDefinition elementDef)
            {
                if (elementDef == null) { throw Error.ArgumentNull("elementDef"); }
                _elemDef = elementDef;
            }
            public string Name { get { return _elemDef.Name; } }
            public string Path { get { return _elemDef.Path; } }
        }

        static INamedNode ToNamedNode(ElementDefinition elementDef) { return new ElementDefinitionNamedNode(elementDef); }

        void clearIssues() { _outcome = null; }

        void addIssue(Issue issue, string message, INamedNode location)
        {
            if (issue == null) { throw Error.ArgumentNull("issue"); }
            if (_outcome == null) { _outcome = new OperationOutcome(); }
            _outcome.AddIssue(issue.ToIssueComponent(message, location));
        }

        void addIssue(Issue issue, string message, INamedNode location, Extension extension)
        {
            if (issue == null) { throw Error.ArgumentNull("issue"); }
            if (extension == null) { throw Error.ArgumentNull("extension"); }
            if (_outcome == null) { _outcome = new OperationOutcome(); }
            var component = issue.ToIssueComponent(message, location);
            component.Extension.Add(extension);
            _outcome.AddIssue(component);
        }

        // Content errors

        // "Differential has a constraint on a choice element '{0}', but does so without using a type slice"
        // Differential specifies a constraint on a child element of a choice type element
        // This is not allowed if an element supports multiple element types; must use slicing!
        public static readonly Issue PROFILE_ELEMENTDEF_INVALID_CHOICE_CONSTRAINT = Issue.Create(10000, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        void addIssueInvalidChoiceConstraint(ElementDefinition elementDef) { addIssueInvalidChoiceConstraint(ToNamedNode(elementDef)); }
        void addIssueInvalidChoiceConstraint(INamedNode location)
        {
            addIssue(
                PROFILE_ELEMENTDEF_INVALID_CHOICE_CONSTRAINT,
                "Differential specifies constraint on choice element '{0}' without using type slice.".FormatWith(location != null ? location.Path : null),
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
        //        "Differential specifies invalid child constraints on leaf element '{0}'.".FormatWith(location != null ? location.Path : null),
        //        location
        //    );
        //}
        
        void addIssueInvalidNameReference(ElementDefinition elementDef, string name) { addIssueInvalidNameReference(ToNamedNode(elementDef), name); }
        void addIssueInvalidNameReference(INamedNode location, string name)
        {
            addIssue(
                PROFILE_ELEMENTDEF_INVALID_TYPEPROFILE_NAMEREF,
                "Element '{0}' has a nameReference to '{0}', which cannot be found in the StructureDefinition.".FormatWith(location != null ? location.Path : null, name),
                location
            );
        }

        void addIssueNoTypeOrNameReference(ElementDefinition elementDef) { addIssueNoTypeOrNameReference(ToNamedNode(elementDef)); }
        void addIssueNoTypeOrNameReference(INamedNode location)
        {
            addIssue(
                Issue.PROFILE_ELEMENTDEF_CONTAINS_NO_TYPE_OR_NAMEREF,
                "Element '{0}' has neither a type nor a nameReference.".FormatWith(location != null ? location.Path : null),
                location
            );
        }

        // "Type profile '{0}' has an invalid name reference. The base profile does not contain an element with name '{1}'"
        public static readonly Issue PROFILE_ELEMENTDEF_INVALID_TYPEPROFILE_NAMEREF = Issue.Create(10002, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        void addIssueInvalidProfileNameReference(ElementDefinition elementDef, string name, string profileRef) { addIssueInvalidProfileNameReference(ToNamedNode(elementDef), name, profileRef); }
        void addIssueInvalidProfileNameReference(INamedNode location, string name, string profileRef)
        {
            addIssue(
                PROFILE_ELEMENTDEF_INVALID_TYPEPROFILE_NAMEREF,
                "Element type profile reference '{0}' has an invalid name reference. The target profile does not contain an element with name '{1}'".FormatWith(profileRef, name),
                location
            );
        }

        // "The slicing entry in the differential at '{0}' indicates an slice, but the base element is not a repeating or choice element"
        public static readonly Issue PROFILE_ELEMENTDEF_INVALID_SLICE = Issue.Create(10003, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        void addIssueInvalidSlice(ElementDefinition elementDef) { addIssueInvalidSlice(ToNamedNode(elementDef)); }
        void addIssueInvalidSlice(INamedNode location)
        {
            addIssue(
                PROFILE_ELEMENTDEF_INVALID_SLICE,
                "The slicing entry in the differential at '{0}' indicates an slice, but the base element is not a repeating or choice element".FormatWith(location != null ? location.Path : null),
                location
            );
        }

        // "The slice group at '{0}' does not start with a slice entry element"
        public static readonly Issue PROFILE_ELEMENTDEF_MISSING_SLICE_ENTRY = Issue.Create(10004, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Required);

        void addIssueMissingSliceEntry(ElementDefinition elementDef) { addIssueMissingSliceEntry(ToNamedNode(elementDef)); }
        void addIssueMissingSliceEntry(INamedNode location)
        {
            addIssue(
                PROFILE_ELEMENTDEF_INVALID_TYPEPROFILE_NAMEREF,
                "The slice group at '{0}' does not start with a slice entry element".FormatWith(location != null ? location.Path : null),
                location
            );
        }

        // "Differential specification for core resource or datatype definitions does not start with root element definition."
        // public static readonly Issue PROFILE_NO_ROOT = Issue.Create(10004, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        // "Element at path '{0}' has a choice of types, cannot expand"
        public static readonly Issue PROFILE_ELEMENTDEF_CANNOT_EXPAND_CHOICE = Issue.Create(10005, OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid);

        // Dependency errors

        // "Unresolved profile reference. Cannot locate the type profile for element '{0}'.\r\nProfile url = '{1}'".FormatWith(diff.Path, primaryDiffTypeProfile)
        // Issue.UNAVAILABLE_REFERENCED_PROFILE_UNAVAILABLE
        void addIssueProfileNotFound(ElementDefinition elementDef, string profileUrl) { addIssueProfileNotFound(ToNamedNode(elementDef), profileUrl); }
        void addIssueProfileNotFound(INamedNode location, string profileUrl)
        {
            if (profileUrl == null) { throw Error.ArgumentNull("profileUrl"); }
            var ext = new Extension(PROFILE_URL_EXT, new FhirUri(profileUrl));
            if (location != null)
            {
                addIssue(Issue.UNAVAILABLE_REFERENCED_PROFILE_UNAVAILABLE, "Unable to resolve reference to profile '{0}' for element '{1}'".FormatWith(profileUrl, location.Path), location, ext);
            }
            else
            {
                addIssue(Issue.UNAVAILABLE_REFERENCED_PROFILE_UNAVAILABLE, "Unable to resolve reference to profile '{0}'".FormatWith(profileUrl), location, ext);
            }
        }

        /// <summary>
        /// Canonical url of custom extension definition for OperationOutcome.IssueComponent; specifies the canonical url of an unresolved profile
        /// </summary>
        public static readonly string PROFILE_URL_EXT = "http://hl7.org/fhir/StructureDefinition/profileUrl";

        // "Resolved the external profile with url '{0}', but it does not contain a snapshot representation."
        // Issue.UNAVAILABLE_NEED_SNAPSHOT

        void addIssueProfileHasNoSnapshot(ElementDefinition elementDef, string profileUrl) { addIssueProfileHasNoSnapshot(ToNamedNode(elementDef), profileUrl); }
        void addIssueProfileHasNoSnapshot(INamedNode location, string profileUrl)
        {
            if (profileUrl == null) { throw Error.ArgumentNull("profileUrl"); }
            var ext = new Extension(PROFILE_URL_EXT, new FhirUri(profileUrl));
            addIssue(Issue.UNAVAILABLE_NEED_SNAPSHOT, "The resolved external profile with url '{0}' has no snapshot.".FormatWith(profileUrl), location, ext);
        }

        // Issue.UNAVAILABLE_SNAPSHOT_GENERATION_FAILED
    }

    public static class SnapshotGeneratorOperationOutcomeIssueExtensions
    {
        /// <summary>
        /// Extract the target profile url from the specified <see cref="OperationOutcome.IssueComponent"/> instance, if specified.
        /// The value is resolved from a custom extension with a canonical url defined by <see cref="SnapshotGenerator.PROFILE_URL_EXT"/>.
        /// </summary>
        /// <param name="issue"></param>
        /// <returns></returns>
        public static string GetProfileUrl(this OperationOutcome.IssueComponent issue)
        {
            if (issue == null) { return null; } // { throw Error.ArgumentNull(nameof(issue)); }
            var urlElem = issue.GetExtensionValue<FhirUri>(SnapshotGenerator.PROFILE_URL_EXT);
            return urlElem != null ? urlElem.Value : null;
        }
    }

}
