/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System.Collections.Generic;
using System.Diagnostics;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Introspection;
using System.Linq;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Extension methods for the <see cref="ArtifactSummary"/> class.</summary>
    public static class ArtifactSummaryExtensions
    {
        /// <summary>
        /// Filter the specified sequence of <see cref="ArtifactSummary"/> instances
        /// to only invalid summaries with <see cref="ArtifactSummary.Error"/> information.
        /// </summary>
        /// <param name="summaries"></param>
        /// <returns></returns>
        public static IEnumerable<ArtifactSummary> Errors(this IEnumerable<ArtifactSummary> summaries)
            => summaries.Where(s => !s.IsValid);
    }

    /// <summary>
    /// Common base class for summary information about a generic FHIR artifact.
    /// The API also defines a number of specialized subclasses
    /// with additional summary information for specific resource types.
    /// </summary>
    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
    public class ArtifactSummary
    {
        /// <summary>
        /// Create a new <see cref="ArtifactSummary"/> for the current resource
        /// of the specified <see cref="INavigatorStream"/>.
        /// </summary>
        /// <param name="origin">The original location of the underlying resource (container).</param>
        /// <param name="stream">An <see cref="INavigatorStream"/> instance.</param>
        public ArtifactSummary(string origin, INavigatorStream stream)
            : this(origin, stream.Position, stream.Position, stream.Current, null) { }

        /// <summary>
        /// Create a new <see cref="ArtifactSummary"/> to represent an exception
        /// that occured while harvesting the summary information of a resource.
        /// </summary>
        /// <param name="origin">The original location of the underlying resource (container).</param>
        /// <param name="error">The <see cref="Exception"/> that occured while harvesting the summary.</param>
        public static ArtifactSummary FromException(string origin, Exception error) => new ArtifactSummary(origin, error);

        /// <summary>
        /// Create a new <see cref="ArtifactSummary"/> to represent an exception
        /// that occured while harvesting the summary information of a resource.
        /// </summary>
        /// <param name="origin">The original location of the underlying resource (container).</param>
        /// <param name="error">The <see cref="Exception"/> that occured while harvesting the summary.</param>
        ArtifactSummary(string origin, Exception error)
            : this(origin, null, null, null, error) { }

        /// <summary>Constructor for subclasses.</summary>
        /// <remarks>
        /// The IElementNavigator.Current property always returns a new navigator instance.
        /// This constructor allows subclass and base class to share a common navigator instance.
        /// It allows consumers to efficiently scan resources using a forward-only stream.
        /// <para>
        /// A subclass constructor should first call a base class constructor with an explicit reference
        /// to the current <see cref="IElementNavigator"/> instance, as returned by IElementNavigator.Current.
        /// The base class will use the navigator to harvest some summary information, while advancing the
        /// navigator position. Afterwards, the subclass can harvest additional summary information from
        /// the same navigator, starting at the current navitor position.
        /// </para>
        /// </remarks>
        /// <param name="origin">The original location of the underlying resource (container).</param>
        /// <param name="stream">An <see cref="INavigatorStream"/> instance.</param>
        /// <param name="current">Reference to the current <see cref="IElementNavigator"/> instance, returned by IElementNavigator.Current.</param>
        public ArtifactSummary(string origin, INavigatorStream stream, IElementNavigator current)
            : this(origin, stream.Position, stream.Position, current, null) { }

        /// <summary>
        /// Create a new <see cref="ArtifactSummary"/> to represent an exception
        /// that occured while harvesting the summary information of a bundle entry.
        /// </summary>
        /// <param name="origin">The original location of the underlying resource (container).</param>
        /// <param name="stream">An <see cref="INavigatorStream"/> instance.</param>
        /// <param name="current">Reference to the current <see cref="IElementNavigator"/> instance, returned by IElementNavigator.Current.</param>
        /// <param name="error">The <see cref="Exception"/> that occured while harvesting the summary.</param>
        public ArtifactSummary(string origin, INavigatorStream stream, IElementNavigator current, Exception error)
            : this(origin, stream.Position, stream.Position, current, error) { }

        // Private ctor; initialize all properties
        ArtifactSummary(string origin, string position, string uri, IElementNavigator nav, Exception error)
        {
            Origin = origin;
            Position = position;
            ResourceUri = uri;
            var t = ResourceTypeName = nav?.Type;
            // Try to decode the typename, if well-known
            ResourceType = t != null ? EnumUtility.ParseLiteral<ResourceType>(t) : null;
            Error = error;
        }

        /// <summary>Original location of the underlying resource (container).</summary>
        public string Origin { get; }

        /// <summary>
        /// An opaque identifier that allows the generating <see cref="IElementNavigator"/> to retrieve the associated resource.
        /// The actual value is implementation-dependent.
        /// </summary>
        public string Position { get; }

        /// <summary>The resource Uri.</summary>
        public string ResourceUri { get; }

        /// <summary>The (raw) resource type name.</summary>
        public string ResourceTypeName { get; }

        /// <summary>The decoded resource type, or <c>null</c> if the type is unknown.</summary>
        public ResourceType? ResourceType { get; }

        /// <summary>Error that occured while harvesting information, if any, or <c>null</c> otherwise.</summary>
        public Exception Error { get; }

        /// <summary>
        /// Determines if the summary information was harvested succesfully.
        /// If <c>false</c>, the <see cref="Error"/> property returns error information.
        /// </summary>
        [NotMapped]
        public bool IsValid => Error == null;

        // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [NotMapped]
        string DebuggerDisplay
            => $"{GetType().Name} for {ResourceTypeName} | Origin: {Origin} | "
                + (IsValid ? DebuggerDisplayInfo : $"Error: {Error.Message}");

        /// <summary>
        /// Returns a formatted description string for debugging purposes.
        /// Override in subclass to provide additional and/or custom debug information.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [NotMapped]
        protected virtual string DebuggerDisplayInfo => $"Uri: {ResourceUri ?? "(unknown)"} ";

        /// <summary>Utility method to convert the specified raw object value into an enumeration value.</summary>
        /// <typeparam name="T">An <see cref="Enum"/> type.</typeparam>
        /// <param name="value">A deserialized object value.</param>
        /// <returns>An enumeration value of type <typeparamref name="T"/>, or <c>null</c>.</returns>
        protected static T? ParseLiteral<T>(object value) where T : struct
        {
            var s = value?.ToString();
            if (!string.IsNullOrEmpty(s))
            {
                return EnumUtility.ParseLiteral<T>(s);
            }
            return null;
        }
    }


    /// <summary>Extended summary information for FHIR <see cref="NamingSystem"/> resources.</summary>
    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
    public class NamingSystemSummary : ArtifactSummary
    {
        public const ResourceType SupportedType = Model.ResourceType.NamingSystem;

        public NamingSystemSummary(string origin, INavigatorStream stream) : this(origin, stream, stream.Current) { }

        public NamingSystemSummary(string origin, INavigatorStream stream, IElementNavigator current) : base(origin, stream, current)
        {
            Debug.Assert(ResourceType == SupportedType);

            var list = new List<string>();

            if (current.MoveToFirstChild("uniqueId"))
            {
                do
                {
                    var child = current.Clone();
                    if (child.MoveToFirstChild("value"))
                    {
                        list.Add(child.Value?.ToString());
                    }
                } while (current.MoveToNext("uniqueId"));
            }
            UniqueIds = list.ToArray();
        }

        /// <summary>Unique identifiers of the naming system.</summary>
        public string[] UniqueIds { get; }

        protected override string DebuggerDisplayInfo => $"{base.DebuggerDisplayInfo} | UniqueIds: {string.Join(",", UniqueIds)}";
    }

    /// <summary>Extended summary information for FHIR conformance resources (see <seealso cref="IConformanceResource"/>).</summary>
    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
    public class ConformanceResourceSummary : ArtifactSummary
    {
        public static bool IsSupported(string typeName) => ModelInfo.IsConformanceResource(typeName);

        public ConformanceResourceSummary(string origin, INavigatorStream stream) : this(origin, stream, stream.Current) { }

        public ConformanceResourceSummary(string origin, INavigatorStream stream, IElementNavigator current) : base(origin, stream, current)
        {
            Debug.Assert(IsSupported(ResourceTypeName));

            if (current.MoveToFirstChild())
            {
                if (current.MoveToNext("url"))
                {
                    Canonical = current.Value?.ToString();
                }
                if (current.MoveToNext("name"))
                {
                    Name = current.Value?.ToString();
                }
                if (current.MoveToNext("status"))
                {
                    Status = ParseLiteral<ConformanceResourceStatus>(current.Value);
                }
            }

        }

        /// <summary>The canonical url, or <c>null</c> if not a conformance resource.</summary>
        public string Canonical { get; }

        /// <summary>The name of the conformance resource.</summary>
        public string Name { get; }

        /// <summary>The publication status of the conformance resource.</summary>
        public ConformanceResourceStatus? Status { get; }

        protected override string DebuggerDisplayInfo => $"{base.DebuggerDisplayInfo} | Canonical: {Canonical ?? "(unknown)"}";
    }


    /// <summary>Extended summary information for FHIR <see cref="ValueSet"/> resources.</summary>
    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
    public class ValueSetSummary : ConformanceResourceSummary
    {
        public const ResourceType SupportedType = Model.ResourceType.ValueSet;

        public ValueSetSummary(string origin, INavigatorStream stream) : this(origin, stream, stream.Current) { }

        public ValueSetSummary(string origin, INavigatorStream stream, IElementNavigator current) : base(origin, stream, current)
        {
            Debug.Assert(ResourceType == SupportedType);

            if (current.MoveToNext("codeSystem"))
            {
                var child = current.Clone();
                if (child.MoveToFirstChild("system"))
                {
                    ValueSetSystem = child.Value?.ToString();
                }
            }
        }

        // [WMR 20171011] DSTU2!

        /// <summary>The Uri of the inline code system.</summary>
        public string ValueSetSystem { get; }

        protected override string DebuggerDisplayInfo => $"{base.DebuggerDisplayInfo} | System: {ValueSetSystem ?? "(unknown)"}";
    }

    /// <summary>Extended summary information for FHIR <see cref="ConceptMap"/> resources.</summary>
    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
    public class ConceptMapSummary : ConformanceResourceSummary
    {
        public const ResourceType SupportedType = Model.ResourceType.ConceptMap;

        public ConceptMapSummary(string origin, INavigatorStream stream) : this(origin, stream, stream.Current) { }

        public ConceptMapSummary(string origin, INavigatorStream stream, IElementNavigator current) : base(origin, stream, current)
        {
            Debug.Assert(ResourceType == SupportedType);

            if (current.MoveToNext("sourceUri"))
            {
                ConceptMapSource = current.Value?.ToString();
            }
            else if (current.MoveToNext("sourceReference"))
            {
                var child = current.Clone();
                if (child.MoveToFirstChild("reference"))
                {
                    ConceptMapSource = child.Value?.ToString();
                }

            }

            if (current.MoveToNext("targetUri"))
            {
                ConceptMapTarget = current.Value?.ToString();
            }
            else if (current.MoveToNext("targetReference"))
            {
                var child = current.Clone();
                if (child.MoveToFirstChild("reference"))
                {
                    ConceptMapTarget = child.Value?.ToString();
                }

            }
        }

        /// <summary>Url that identifies the source of the mapping.</summary>
        public string ConceptMapSource { get; }

        /// <summary>Url that identifies the target of the mapping.</summary>
        public string ConceptMapTarget { get; }

        protected override string DebuggerDisplayInfo => $"{base.DebuggerDisplayInfo} | Source: {ConceptMapSource ?? "(unknown)"} | Target: {ConceptMapTarget ?? "(unknown)"}";
    }

    /// <summary>Extended summary information for FHIR <see cref="StructureDefinition"/> resources.</summary>
    [DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")]
    public class StructureDefinitionSummary : ConformanceResourceSummary
    {
        public const ResourceType SupportedType = Model.ResourceType.StructureDefinition;

        public StructureDefinitionSummary(string origin, INavigatorStream stream) : this(origin, stream, stream.Current) { }

        public StructureDefinitionSummary(string origin, INavigatorStream stream, IElementNavigator current) : base(origin, stream, current)
        {
            Debug.Assert(ResourceType == SupportedType);

            if (current.MoveToNext("kind"))
            {
                Kind = ParseLiteral<StructureDefinition.StructureDefinitionKind>(current.Value);
            }

            // DSTU2
            if (current.MoveToNext("constrainedType"))
            {
                var v = current.Value;
                ConstrainedTypeName = v?.ToString();
                ConstrainedType = ParseLiteral<FHIRDefinedType>(v);
            }

            if (current.MoveToNext("contextType"))
            {
                ContextType = ParseLiteral<StructureDefinition.ExtensionContext>(current.Value);
            }

            // STU3: type
            // ... TODO ...

            // STU3: baseDefinition
            if (current.MoveToNext("base"))
            {
                Base = current.Value?.ToString();
            }
            // STU3: Derivation
            // ... TODO ...
        }

        public StructureDefinition.StructureDefinitionKind? Kind { get; }

        // DSTU2
        public string ConstrainedTypeName { get; }

        // DSTU2
        public FHIRDefinedType? ConstrainedType { get; }

        public StructureDefinition.ExtensionContext? ContextType { get; }

        // STU3: TypeName
        // STU3: Type
        // STU3: BaseDefinition
        public string Base { get; }

        // STU3
        // public StructureDefinition.XXX Derivation { get; }

        protected override string DebuggerDisplayInfo => $"{base.DebuggerDisplayInfo} | Kind: {Kind}";
    }


}
