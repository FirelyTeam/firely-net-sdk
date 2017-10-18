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

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Common summary information for generic FHIR artifacts.</summary>
    public class ArtifactSummary
    {
        // [WMR 20171016] TODO: Determine Resource Uri
        // Can't assume we can always initialize Resource Uri from Position...?

        public ArtifactSummary(INavigatorStream stream)
            : this(stream.Path, stream.Position, stream.Position, stream.Current) { }

        /// <summary>Constructor for subclasses.</summary>
        /// <remarks>
        /// The IElementNavigator.Current property always returns a new navigator instance.
        /// This constructor allows subclass and base class to share a common navigator instance.
        /// </remarks>
        public ArtifactSummary(INavigatorStream stream, IElementNavigator current)
            : this(stream.Path, stream.Position, stream.Position, current) { }

        ArtifactSummary(string origin, string position, string uri, IElementNavigator nav)
        {
            Origin = origin;
            Position = position;
            ResourceUri = uri;
            ResourceTypeName = nav.Type;
            // Try to decode the typename, if well-known
            ResourceType = EnumUtility.ParseLiteral<ResourceType>(nav.Type);
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

        public override string ToString()
            => $"{GetType().Name} for {ResourceType} | Uri: {ResourceUri ?? "(unknown)"} | Origin: {Origin}";
    }

    /// <summary>Extended summary information for FHIR NamingSystem resources.</summary>
    public class NamingSystemSummary : ArtifactSummary
    {
        public const ResourceType SupportedType = Model.ResourceType.NamingSystem;

        public NamingSystemSummary(INavigatorStream stream) : this(stream, stream.Current) { }

        public NamingSystemSummary(INavigatorStream stream, IElementNavigator current) : base(stream, current)
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

        public override string ToString()
            => $"{GetType().Name} for {ResourceType} | Uri: {ResourceUri ?? "(unknown)"} | UniqueIds: {string.Join(",", UniqueIds)} | Origin: {Origin}";

    }

    /// <summary>Extended summary information for FHIR conformance resources.</summary>
    public class ConformanceResourceSummary : ArtifactSummary
    {
        public static bool IsSupported(string typeName) => ModelInfo.IsConformanceResource(typeName);

        public ConformanceResourceSummary(INavigatorStream stream) : this(stream, stream.Current) { }

        public ConformanceResourceSummary(INavigatorStream stream, IElementNavigator current) : base(stream, current)
        {
            Debug.Assert(IsSupported(ResourceTypeName));

            if (current.MoveToFirstChild("url"))
            {
                Canonical = current.Value?.ToString();
            }
        }

        /// <summary>The canonical url, or <c>null</c> if not a conformance resource.</summary>
        public string Canonical { get; }

        public override string ToString()
            => $"{GetType().Name} for {ResourceType} | Canonical: {Canonical ?? "(unknown)"}) | Origin: {Origin}";
    }


    /// <summary>Extended summary information for FHIR ValueSet resources.</summary>
    public class ValueSetSummary : ConformanceResourceSummary
    {
        public const ResourceType SupportedType = Model.ResourceType.ValueSet;

        public ValueSetSummary(INavigatorStream stream) : this(stream, stream.Current) { }

        public ValueSetSummary(INavigatorStream stream, IElementNavigator current) : base(stream, current)
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

        public override string ToString()
            => $"{GetType().Name} for {ResourceType} | Canonical: {Canonical ?? "(unknown)"}) | System: {ValueSetSystem ?? "(unknown)"} | Origin: {Origin}";
    }

    /// <summary>Extended summary information for FHIR ConceptMap resources.</summary>
    public class ConceptMapSummary : ConformanceResourceSummary
    {
        public const ResourceType SupportedType = Model.ResourceType.ConceptMap;

        public ConceptMapSummary(INavigatorStream stream) : this(stream, stream.Current) { }

        public ConceptMapSummary(INavigatorStream stream, IElementNavigator current) : base(stream, current)
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

        public override string ToString()
            => $"{GetType().Name} for {ResourceType} | Canonical: {Canonical ?? "(unknown)"}) | Source: {ConceptMapSource ?? "(unknown)"} | Target: {ConceptMapTarget ?? "(unknown)"} | Origin: {Origin}";
    }
}
