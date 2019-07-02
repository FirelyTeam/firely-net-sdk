/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

// [WMR 20161219] Save and reuse existing instance, so generator can detect & handle recursion
#define REUSE_SNAPSHOT_GENERATOR

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Schema;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Terminology;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using Hl7.FhirPath;
using Hl7.FhirPath.Expressions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hl7.Fhir.Validation
{
    public class Validator
    {
        public ValidationSettings Settings { get; private set; }

        public event EventHandler<OnSnapshotNeededEventArgs> OnSnapshotNeeded;
        public event EventHandler<OnResolveResourceReferenceEventArgs> OnExternalResolutionNeeded;

        private FhirPathCompiler _fpCompiler;

#if REUSE_SNAPSHOT_GENERATOR
        SnapshotGenerator _snapshotGenerator;

        internal SnapshotGenerator SnapshotGenerator
        {
            get
            {
                if (_snapshotGenerator == null)
                {
                    var resolver = Settings.ResourceResolver;
                    if (resolver != null)
                    {
                        SnapshotGeneratorSettings settings = Settings.GenerateSnapshotSettings ?? SnapshotGeneratorSettings.CreateDefault();
                        _snapshotGenerator = new SnapshotGenerator(resolver, settings);
                    }

                }
                return _snapshotGenerator;
            }
        }
#endif

        public Validator(ValidationSettings settings)
        {
            Settings = settings.Clone();
        }

        public Validator() : this(ValidationSettings.CreateDefault())
        {
        }

        internal Validator NewInstance()
        {
            return new Validator(Settings)
            {
                OnSnapshotNeeded = this.OnSnapshotNeeded,
                OnExternalResolutionNeeded = this.OnExternalResolutionNeeded,

#if REUSE_SNAPSHOT_GENERATOR
                _snapshotGenerator = this._snapshotGenerator
#endif
            };
        }


        public OperationOutcome Validate(ITypedElement instance)
        {
            return Validate(instance, declaredTypeProfile: null, statedCanonicals: null, statedProfiles: null);
        }

        public OperationOutcome Validate(ITypedElement instance, params string[] definitionUris)
        {
            return Validate(instance, (IEnumerable<string>)definitionUris);
        }

        public OperationOutcome Validate(ITypedElement instance, IEnumerable<string> definitionUris)
        {
            return Validate(instance, declaredTypeProfile: null, statedCanonicals: definitionUris, statedProfiles: null);
        }

        public OperationOutcome Validate(ITypedElement instance, params StructureDefinition[] structureDefinitions)
        {
            return Validate(instance, (IEnumerable<StructureDefinition>)structureDefinitions);
        }

        public OperationOutcome Validate(ITypedElement instance, IEnumerable<StructureDefinition> structureDefinitions)
        {
            return Validate(instance, declaredTypeProfile: null, statedCanonicals: null, statedProfiles: structureDefinitions);
        }

        #region Obsolete public methods
        [Obsolete("Use Validate(ITypedElement instance) instead")]
        public OperationOutcome Validate(IElementNavigator instance)
        {
            return Validate(instance.ToTypedElement(), declaredTypeProfile: null, statedCanonicals: null, statedProfiles: null);
        }

        [Obsolete("Use Validate(ITypedElement instance, params string[] definitionUris) instead")]
        public OperationOutcome Validate(IElementNavigator instance, params string[] definitionUris)
        {
            return Validate(instance.ToTypedElement(), (IEnumerable<string>)definitionUris);
        }

        [Obsolete("Use Validate(ITypedElement instance, IEnumerable<string> definitionUris) instead")]
        public OperationOutcome Validate(IElementNavigator instance, IEnumerable<string> definitionUris)
        {
            return Validate(instance.ToTypedElement(), declaredTypeProfile: null, statedCanonicals: definitionUris, statedProfiles: null);
        }

        [Obsolete("Use Validate(ITypedElement instance, params StructureDefinition[] structureDefinitions) instead")]
        public OperationOutcome Validate(IElementNavigator instance, params StructureDefinition[] structureDefinitions)
        {
            return Validate(instance.ToTypedElement(), (IEnumerable<StructureDefinition>)structureDefinitions);
        }

        [Obsolete("Use Validate(ITypedElement instance, IEnumerable<StructureDefinition> structureDefinitions) instead")]
        public OperationOutcome Validate(IElementNavigator instance, IEnumerable<StructureDefinition> structureDefinitions)
        {
            return Validate(instance.ToTypedElement(), declaredTypeProfile: null, statedCanonicals: null, statedProfiles: structureDefinitions);
        }
        #endregion

        // This is the one and only main entry point for all external validation calls (i.e. invoked by the user of the API)
        internal OperationOutcome Validate(ITypedElement instance, string declaredTypeProfile, IEnumerable<string> statedCanonicals, IEnumerable<StructureDefinition> statedProfiles, List<Tuple<string, string>> validatedResources = null)
        {
            var processor = new ProfilePreprocessor(profileResolutionNeeded, snapshotGenerationNeeded, instance, declaredTypeProfile, statedProfiles, statedCanonicals, Settings.ResourceMapping);
            var outcome = processor.Process();

            // Note: only start validating if the profiles are complete and consistent
            if (outcome.Success)
                outcome.Add(Validate(instance, processor.Result, validatedResources));

            return outcome;

            StructureDefinition profileResolutionNeeded(string canonical) =>
                Settings.ResourceResolver?.FindStructureDefinition(canonical);
        }

        internal OperationOutcome Validate(ITypedElement instance, ElementDefinitionNavigator definition, List<Tuple<string, string>> validatedResources = null)
        {
            return Validate(instance, new[] { definition }, validatedResources);
        }


        // This is the one and only main internal entry point for all validations, which in its term
        // will call step 1 in the validator, the function validateElement
        internal OperationOutcome Validate(ITypedElement elementNav, IEnumerable<ElementDefinitionNavigator> definitions, List<Tuple<string, string>> validatedResources = null)
        {
            var outcome = new OperationOutcome();

            IScopedNode instance = elementNav as ScopedNodeWrapper ?? new ScopedNodeWrapper(elementNav as ScopedNode ?? new ScopedNode(elementNav));

            validatedResources = validatedResources ?? new List<Tuple<string, string>>();

            try
            {
                List<ElementDefinitionNavigator> allDefinitions = new List<ElementDefinitionNavigator>(definitions);

                if (allDefinitions.Count() == 1)
                    outcome.Add(validateElement(allDefinitions.Single(), instance, validatedResources));
                else
                {
                    var validators = allDefinitions.Select(nav => createValidator(nav, instance, validatedResources));
                    outcome.Add(this.Combine(BatchValidationMode.All, instance, validators));
                }
            }
            catch (Exception e)
            {
                outcome.AddIssue($"Internal logic failure: {e.Message}", Issue.PROCESSING_CATASTROPHIC_FAILURE, instance);
            }

            return outcome;
        }


        private Func<OperationOutcome> createValidator(ElementDefinitionNavigator nav, IScopedNode instance, List<Tuple<string, string>> validatedResources)
        {
            return () => validateElement(nav, instance, validatedResources);
        }


        //   private OperationOutcome validateElement(ElementDefinitionNavigator definition, IElementNavigator instance)

        private OperationOutcome validateElement(ElementDefinitionNavigator definition, IScopedNode instance, List<Tuple<string, string>> validatedResources)
        {
            var outcome = new OperationOutcome();

            Trace(outcome, $"Start validation of ElementDefinition at path '{definition.QualifiedDefinitionPath()}'", Issue.PROCESSING_PROGRESS, instance);

            // If navigator cannot be moved to content, there's really nothing to validate against.
            if (definition.AtRoot && !definition.MoveToFirstChild())
            {
                outcome.AddIssue($"Snapshot component of profile '{definition.StructureDefinition?.Url}' has no content.", Issue.PROFILE_ELEMENTDEF_IS_EMPTY, instance);
                return outcome;
            }

            // This does not work, since the children might still be empty, we need something better
            //// Any node must either have a value, or children, or both (e.g. extensions on primitives)
            if (instance.Value == null && !instance.Children().Any())
            {
                outcome.AddIssue("Element must not be empty", Issue.CONTENT_ELEMENT_MUST_HAVE_VALUE_OR_CHILDREN, instance);
                return outcome;
            }

            var elementConstraints = definition.Current;

            if (elementConstraints.IsPrimitiveValueConstraint())
            {
                // The "value" property of a FHIR Primitive is the bottom of our recursion chain, it does not have a nameReference
                // nor a <type>, the only thing left to do to validate the content is to validate the string representation of the
                // primitive against the regex given in the core definition
                outcome.Add(VerifyPrimitiveContents(elementConstraints, instance));
            }
            else
            {
                bool isInlineChildren = !definition.Current.IsRootElement();

                // Now, validate the children
                if (definition.HasChildren)
                {
                    // If we are at the root of an abstract type (e.g. is this instance a Resource)?
                    // or we are at a nested resource, we may expect more children in the instance than
                    // we know about
                    bool allowAdditionalChildren = (isInlineChildren && elementConstraints.IsResourcePlaceholder()) ||
                                      (!isInlineChildren && definition.StructureDefinition.Abstract == true);

                    // Handle in-lined constraints on children. In a snapshot, these children should be exhaustive,
                    // so there's no point in also validating the <type> or <nameReference>
                    // TODO: Check whether this is even true when the <type> has a profile?
                    // Note: the snapshot is *not* exhaustive if the declared type is a base FHIR type (like Resource),
                    // in which case there may be additional children (verified in the next step)
                    outcome.Add(this.ValidateChildConstraints(definition, instance, allowAdditionalChildren: allowAdditionalChildren, validatedResources: validatedResources));

                    // Special case: if we are located at a nested resource (i.e. contained or Bundle.entry.resource),
                    // we need to validate based on the actual type of the instance
                    if (isInlineChildren && elementConstraints.IsResourcePlaceholder())
                    {
                        outcome.Add(this.ValidateType(elementConstraints, instance, validatedResources));
                    }
                }

                if (!definition.HasChildren)
                {
                    // No inline-children, so validation depends on the presence of a <type> or <contentReference>
                    if (elementConstraints.Type != null || elementConstraints.ContentReference != null)
                    {
                        outcome.Add(this.ValidateType(elementConstraints, instance, validatedResources));
                        outcome.Add(ValidateNameReference(elementConstraints, definition, instance, validatedResources));
                    }
                    else
                        Trace(outcome, "ElementDefinition has no child, nor does it specify a type or contentReference to validate the instance data against", Issue.PROFILE_ELEMENTDEF_CONTAINS_NO_TYPE_OR_NAMEREF, instance);
                }
            }

            outcome.Add(this.ValidateFixed(elementConstraints, instance));
            outcome.Add(this.ValidatePattern(elementConstraints, instance));
            outcome.Add(this.ValidateMinMaxValue(elementConstraints, instance));
            outcome.Add(ValidateMaxLength(elementConstraints, instance));
            outcome.Add(this.ValidateFp(definition.StructureDefinition.Url, elementConstraints, instance));
            outcome.Add(this.ValidateExtension(elementConstraints, instance, "http://hl7.org/fhir/StructureDefinition/regex"));

            // new style validator - has a configure and then execute step.
            // will be separated when all logic has been converted.
            var ts = Settings.TerminologyService;
            if (ts == null)
            {
                if (Settings.ResourceResolver == null)
                {
                    Trace(outcome, $"Cannot resolve binding references since neither TerminologyService nor ResourceResolver is given in the settings",
                        Issue.UNAVAILABLE_TERMINOLOGY_SERVER, instance);
                    return outcome;
                }

                ts = new LocalTerminologyService(Settings.ResourceResolver);
            }

            ValidationContext vc = new ValidationContext() { TerminologyService = ts };

            try
            {
                if (elementConstraints.Binding != null)
                {
                    Binding b = elementConstraints.Binding.ToValidatable();
                    outcome.Add(b.Validate(instance, vc));
                }
            }
            catch (IncorrectElementDefinitionException iede)
            {
                Trace(outcome, "Incorrect ElementDefinition: " + iede.Message, Issue.PROFILE_ELEMENTDEF_INCORRECT, elementConstraints.Path);
            }

            // If the report only has partial information, no use to show the hierarchy, so flatten it.
            if (Settings.Trace == false) outcome.Flatten();

            return outcome;
        }

        private OperationOutcome ValidateExtension(IExtendable elementDef, ITypedElement instance, string uri)
        {
            var outcome = new OperationOutcome();

            var pattern = elementDef.GetStringExtension(uri);
            if (pattern != null)
            {
                var regex = new Regex(pattern);
                var value = toStringRepresentation(instance);
                var success = Regex.Match(value, "^" + regex + "$").Success;

                if (!success)
                {
                    Trace(outcome, $"Value '{value}' does not match regex '{regex}'", Issue.CONTENT_ELEMENT_INVALID_PRIMITIVE_VALUE, instance);
                }
            }

            return outcome;
        }

        internal FhirPathCompiler FpCompiler
        {
            get
            {
                // Use a provided compiler
                if (Settings?.FhirPathCompiler != null)
                    return Settings.FhirPathCompiler;

                if (_fpCompiler == null)
                {
                    var symbolTable = new SymbolTable();
                    symbolTable.AddStandardFP();
                    symbolTable.AddFhirExtensions();

                    _fpCompiler = new FhirPathCompiler(symbolTable);

                    Settings.FhirPathCompiler = _fpCompiler;
                }

                return _fpCompiler;
            }
        }

        //internal OperationOutcome ValidateBinding(ElementDefinition definition, ITypedElement instance)
        //{
        //    var outcome = new OperationOutcome();
        //    if (definition.Binding == null) return outcome;

        //    var ts = Settings.TerminologyService;
        //    if (ts == null)
        //    {
        //        if (Settings.ResourceResolver == null)
        //        {
        //            Trace(outcome, $"Cannot resolve binding references since neither TerminologyService nor ResourceResolver is given in the settings",
        //                Issue.UNAVAILABLE_TERMINOLOGY_SERVER, instance);
        //            return outcome;
        //        }

        //        ts = new LocalTerminologyService(Settings.ResourceResolver);
        //    }

        //    var bindingValidator = new BindingValidator(ts, instance.Location);

        //    try
        //    {
        //        Element bindable = instance.ParseBindable();

        //        // If the instance is not bindeable, ignore the Binding specified on the element, 
        //        // it's simply not applicable
        //        if (bindable != null)
        //            return bindingValidator.ValidateBinding(bindable, definition.Binding);
        //    }
        //    catch (Exception e)
        //    {
        //        Trace(outcome, $"Terminology service call failed for binding at {definition.Path}: {e.Message}", Issue.TERMINOLOGY_SERVICE_FAILED, instance);
        //    }

        //    return outcome;
        //}

        internal OperationOutcome ValidateNameReference(ElementDefinition definition, ElementDefinitionNavigator allDefinitions, IScopedNode instance, List<Tuple<string, string>> validatedResources = null)
        {
            var outcome = new OperationOutcome();

            if (definition.ContentReference != null)
            {
                Trace(outcome, "Start validation of constraints referred to by nameReference '{0}'".FormatWith(definition.ContentReference), Issue.PROCESSING_PROGRESS, instance);

                var referencedPositionNav = allDefinitions.ShallowCopy();

                if (referencedPositionNav.JumpToNameReference(definition.ContentReference))
                    outcome.Include(Validate(instance, referencedPositionNav, validatedResources));
                else
                    Trace(outcome, $"ElementDefinition uses a non-existing nameReference '{definition.ContentReference}'", Issue.PROFILE_ELEMENTDEF_INVALID_NAMEREFERENCE, instance);

            }

            return outcome;
        }

        internal OperationOutcome VerifyPrimitiveContents(ElementDefinition definition, ITypedElement instance)
        {
            var outcome = new OperationOutcome();

            Trace(outcome, "Verifying content of the leaf primitive value attribute", Issue.PROCESSING_PROGRESS, instance);

            // Go look for the primitive type extensions
            //  <extension url="http://hl7.org/fhir/StructureDefinition/structuredefinition-regex">
            //        <valueString value="-?([0]|([1-9][0-9]*))"/>
            //      </extension>
            //      <code>
            //        <extension url="http://hl7.org/fhir/StructureDefinition/structuredefinition-json-type">
            //          <valueString value="number"/>
            //        </extension>
            //        <extension url="http://hl7.org/fhir/StructureDefinition/structuredefinition-xml-type">
            //          <valueString value="int"/>
            //        </extension>
            //      </code>
            // Note that the implementer of IValueProvider may already have outsmarted us and parsed
            // the wire representation (i.e. POCO). If the provider reads xml directly, would it know the
            // type? Would it convert it to a .NET native type? How to check?

            // The spec has no regexes for the primitives mentioned below, so don't check them
            return definition.Type.Count() == 1
                ? ValidateExtension(definition.Type.Single(), instance, "http://hl7.org/fhir/StructureDefinition/structuredefinition-regex")
                : outcome;
        }


        internal OperationOutcome ValidateMaxLength(ElementDefinition definition, ITypedElement instance)
        {
            var outcome = new OperationOutcome();

            if (definition.MaxLength != null)
            {
                var maxLength = definition.MaxLength.Value;

                if (maxLength > 0)
                {
                    if (instance.Value != null)
                    {
                        //TODO: Is ToString() really the right way to turn (Fhir?) Primitives back into their original representation?
                        //If the source is POCO, hopefully FHIR types have all overloaded ToString() 
                        var serializedValue = instance.Value.ToString();

                        if (serializedValue.Length > maxLength)
                            Trace(outcome, $"Value '{serializedValue}' is too long (maximum length is {maxLength})", Issue.CONTENT_ELEMENT_VALUE_TOO_LONG, instance);
                    }
                }
                else
                    Trace(outcome, $"MaxLength was given in ElementDefinition, but it has a negative value ({maxLength})", Issue.PROFILE_ELEMENTDEF_MAXLENGTH_NEGATIVE, instance);
            }

            return outcome;
        }


        internal OperationOutcome.IssueComponent Trace(OperationOutcome outcome, string message, Issue issue, string location)
        {
            return Settings.Trace || issue.Severity != OperationOutcome.IssueSeverity.Information
                ? outcome.AddIssue(message, issue, location)
                : null;
        }

        internal OperationOutcome.IssueComponent Trace(OperationOutcome outcome, string message, Issue issue, ITypedElement location)
        {
            return Settings.Trace || issue.Severity != OperationOutcome.IssueSeverity.Information
                ? Trace(outcome, message, issue, location.Location)
                : null;
        }

        private string toStringRepresentation(ITypedElement vp)
        {
            return vp == null || vp.Value == null ?
                null :
                PrimitiveTypeConverter.ConvertTo<string>(vp.Value);
        }

        internal ITypedElement ExternalReferenceResolutionNeeded(string reference, OperationOutcome outcome, string path)
        {
            if (!Settings.ResolveExteralReferences) return null;

            try
            {
                // Default implementation: call event
                if (OnExternalResolutionNeeded != null)
                {
                    var args = new OnResolveResourceReferenceEventArgs(reference);
                    OnExternalResolutionNeeded(this, args);
                    return args.Result;
                }
            }
            catch (Exception e)
            {
                Trace(outcome, "External resolution of '{reference}' caused an error: " + e.Message, Issue.UNAVAILABLE_REFERENCED_RESOURCE, path);
            }

            // Else, try to resolve using the given ResourceResolver 
            // (note: this also happens when the external resolution above threw an exception)
            if (Settings.ResourceResolver != null)
            {
                try
                {
                    var poco = Settings.ResourceResolver.ResolveByUri(reference);
                    if (poco != null)
                        return poco.ToTypedElement();
                }
                catch (Exception e)
                {
                    Trace(outcome, $"Resolution of reference '{reference}' using the Resolver API failed: " + e.Message, Issue.UNAVAILABLE_REFERENCED_RESOURCE, path);
                }
            }

            return null;        // Sorry, nothing worked
        }


        // Note: this modifies an SD that is passed to us and will alter a possibly cached
        // object shared amongst other threads. This is generally useful and saves considerable
        // time when the same snapshot is needed again, but may result in side-effects
        private OperationOutcome snapshotGenerationNeeded(StructureDefinition definition)
        {
            if (!Settings.GenerateSnapshot) return new OperationOutcome();

            // Default implementation: call event
            if (OnSnapshotNeeded != null)
            {
                var eventData = new OnSnapshotNeededEventArgs(definition, Settings.ResourceResolver);
                OnSnapshotNeeded(this, eventData);
                return eventData.Result;
            }

            // Else, expand, depending on our configuration
#if REUSE_SNAPSHOT_GENERATOR
            var generator = this.SnapshotGenerator;
            if (generator != null)
            {
                generator.Update(definition);

#if DEBUG
                string xml = (new FhirXmlSerializer()).SerializeToString(definition);
                string name = definition.Id ?? definition.Name.Replace(" ", "").Replace("/", "");
                var dir = Path.Combine(Path.GetTempPath(), "validation");

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                File.WriteAllText(Path.Combine(dir, name) + ".StructureDefinition.xml", xml);
#endif


                return generator.Outcome ?? new OperationOutcome();
#else
            if (Settings.ResourceResolver != null)
            {

                SnapshotGeneratorSettings settings = Settings.GenerateSnapshotSettings ?? SnapshotGeneratorSettings.Default;
                (new SnapshotGenerator(Settings.ResourceResolver, settings)).Update(definition);

#endif
            }

            return new OperationOutcome();
        }
    }



    internal static class TypeExtensions
    {
        // This is allowed for the types date, dateTime, instant, time, decimal, integer, and Quantity. string? why not?
        public static bool IsOrderedFhirType(this Type t)
        {
            return t == typeof(FhirDateTime) ||
                   t == typeof(Date) ||
                   t == typeof(Instant) ||
                   t == typeof(Model.Time) ||
                   t == typeof(FhirDecimal) ||
                   t == typeof(Integer) ||
                   t == typeof(PositiveInt) ||
                   t == typeof(UnsignedInt) ||
                   t == typeof(Model.Quantity) ||
                   t == typeof(FhirString);
        }

        public static bool IsBindeableFhirType(this FHIRAllTypes t)
        {
            return t == FHIRAllTypes.Code ||
                   t == FHIRAllTypes.Coding ||
                   t == FHIRAllTypes.CodeableConcept ||
                   t == FHIRAllTypes.Quantity ||
                   t == FHIRAllTypes.Extension ||
                   t == FHIRAllTypes.String ||
                   t == FHIRAllTypes.Uri;
        }
    }


    public class OnSnapshotNeededEventArgs : EventArgs
    {
        public OnSnapshotNeededEventArgs(StructureDefinition definition, IResourceResolver resolver)
        {
            Definition = definition;
            Resolver = resolver;
        }

        public StructureDefinition Definition { get; }

        public IResourceResolver Resolver { get; }

        public OperationOutcome Result { get; set; }
    }

    public class OnResolveResourceReferenceEventArgs : EventArgs
    {
        public OnResolveResourceReferenceEventArgs(string reference)
        {
            Reference = reference;
        }

        public string Reference { get; }

        public ITypedElement Result { get; set; }
    }


    public enum BatchValidationMode
    {
        All,
        Any,
        Once
    }


    internal class ScopedNodeWrapper : IScopedNode
    {
        private class Cache
        {
            public readonly object _lock = new object();

            public string Id;
            public IEnumerable<IScopedNode> ContainedResources;
            public IEnumerable<IBundledResource> BundledResources;
            public string FullUrl;
        }

        private Cache _cache = new Cache();

        private IScopedNode _current;

        public ScopedNodeWrapper(IScopedNode scopedNode)
        {
            ValidatedProfiles = new List<ValidatedProfile>();
            _current = scopedNode;
            ParentResource = scopedNode.ParentResource;
        }
        
        private ScopedNodeWrapper(IScopedNode parent, IScopedNode parentResource, IScopedNode scopedNode)
        {
            ValidatedProfiles = new List<ValidatedProfile>();
            _current = scopedNode;
            ParentResource = parent.AtResource ? parent : parentResource;
        }

        public ITypedElement Current => _current.Current;

        public string Name => _current.Name;

        public string InstanceType => _current.InstanceType;

        public object Value => _current.Value;

        public string Location => _current.Location;

        public IElementDefinitionSummary Definition => _current.Definition;

        public ExceptionNotificationHandler ExceptionHandler { get { return _current.ExceptionHandler; } set { _current.ExceptionHandler = value; } }

        //private IScopedNode parentResource;
        public IScopedNode ParentResource
        {
            get; internal set;
        }
        
        public string LocalLocation => _current.LocalLocation;

        public bool AtResource => _current.AtResource;
        public ITypedElement ResourceContext
        {
            get
            {
                IScopedNode scan = this;

                while (scan.ParentResource != null && scan.ParentResource.InstanceType != "Bundle")
                {
                    scan = scan.ParentResource;
                }

                return scan as ITypedElement;
            }
        }

        public IEnumerable<ITypedElement> Children(string name = null) => _current.Children(name).Select(t => new ScopedNodeWrapper(this, this.ParentResource, t as ScopedNode));
            //Current.Children(name).Select(c => new ScopedNodeWrapper(new ScopedNode(this as IScopedNode, this.ParentResource, c)));

        public IEnumerable<object> Annotations(Type type)
        {
            if (type == typeof(ScopedNodeWrapper) || type == typeof(IScopedNode))
                return new[] { this };
            else
                return Current.Annotations(type);
        }

        public IEnumerable<IBundledResource> BundledResources()
        {

            if (_cache.BundledResources == null)
            {
                if (InstanceType == "Bundle")
                    _cache.BundledResources = from e in this.Children("entry")
                                              let fullUrl = e.Children("fullUrl").FirstOrDefault()?.Value as string
                                              let resource = e.Children("resource").FirstOrDefault() as ScopedNodeWrapper
                                              select new BundledResource { FullUrl = fullUrl, Resource = resource };
                else
                    _cache.BundledResources = Enumerable.Empty<BundledResource>();
            }

            return _cache.BundledResources;
        }

        public string Id()
        {
            if (_cache.Id == null)
            {
                _cache.Id = AtResource ? "#" + Current.Children("id").FirstOrDefault()?.Value as string : null;
            }

            return _cache.Id;
        }

        public IEnumerable<IScopedNode> ParentResources()
        {
            var scan = this.ParentResource;

            while (scan != null)
            {
                yield return scan;

                scan = scan.ParentResource;
            }
        }

        public string FullUrl()
        {
            if (_cache.FullUrl == null)
            {
                foreach (var parent in ParentResources())
                {
                    if (parent.InstanceType == "Bundle")
                    {
                        var fullUrl = parent.BundledResources()
                            .SingleOrDefault(be => this.Location.StartsWith(be.Resource.Location))
                            ?.FullUrl;
                        if (fullUrl != null) _cache.FullUrl = fullUrl;
                    }
                }
            }

            return _cache.FullUrl;
        }

        public IEnumerable<IScopedNode> ContainedResources()
        {
            return _current.ContainedResources();
            //if (_cache.ContainedResources == null)
            //{
            //    _cache.ContainedResources = AtResource ?
            //        this.Children("contained").Cast<IScopedNode>() :
            //        Enumerable.Empty<IScopedNode>();
            //}
            //return _cache.ContainedResources;
        }
        
        public List<ValidatedProfile> ValidatedProfiles { get; set; }
        
        public class BundledResource : IBundledResource
        {
            public string FullUrl { get; set; }
            public IScopedNode Resource { get; set; }
        }
    }
    
    public class ValidatedProfile
    {
        public string Profile { get; }
        public bool Success { get; }

        public ValidatedProfile(string profile, bool success)
        {
            Profile = profile;
            Success = success;
        }
    }
}
