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

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Common summary information for generic FHIR artifacts.</summary>
    public class ArtifactSummary
    {
        public ArtifactSummary(string path, string url, IElementNavigator input)
        {
            Origin = path;
            ResourceUri = url;
            ResourceTypeName = input.Name;
            // Try to decode the typename, if well-known
            ResourceType = EnumUtility.ParseLiteral<ResourceType>(input.Name);
        }

        /// <summary>The original location of the artifact (container).</summary>
        public string Origin { get; }

        /// <summary>The resource Uri.</summary>
        public string ResourceUri { get; }

        /// <summary>The (raw) resource type name.</summary>
        public string ResourceTypeName { get; }

        /// <summary>The decoded resource type, or <c>null</c> if the type is unknown.</summary>
        public ResourceType? ResourceType { get; }
    }


    /// <summary>Extended summary information for FHIR conformance resources.</summary>
    public class ConformanceResourceSummary : ArtifactSummary
    {
        public static bool IsSupported(string typeName) => ModelInfo.IsConformanceResource(typeName);

        public ConformanceResourceSummary(string path, string url, IElementNavigator input) : base(path, url, input)
        {
            Debug.Assert(IsSupported(ResourceTypeName));

            if (input.MoveToFirstChild("url"))
            {
                Canonical = input.Value?.ToString();
            }
        }

        /// <summary>The canonical url, or <c>null</c> if not a conformance resource.</summary>
        public string Canonical { get; }

        public override string ToString()
            => $"{ResourceType} resource with uri {ResourceUri ?? "(unknown)"} (canonical {Canonical ?? "(unknown)"}), read from {Origin}";
    }


    /// <summary>Extended summary information for FHIR ValueSet resources.</summary>
    public class ValueSetSummary : ConformanceResourceSummary
    {
        public const ResourceType SupportedType = Model.ResourceType.ValueSet;

        public ValueSetSummary(string path, string url, IElementNavigator input) : base(path, url, input)
        {
            Debug.Assert(ResourceType == SupportedType);

            if (input.MoveToNext("codeSystem"))
            {
                var nav = input.Clone();
                if (nav.MoveToFirstChild("system"))
                {
                    ValueSetSystem = nav.Value?.ToString();
                }
            }
        }

        // [WMR 20171011] DSTU2!

        /// <summary>The Uri of the inline code system.</summary>
        public string ValueSetSystem { get; }
    }


    /// <summary>Extended summary information for FHIR NamingSystem resources.</summary>
    public class NamingSystemSummary : ArtifactSummary
    {
        public const ResourceType SupportedType = Model.ResourceType.NamingSystem;

        public NamingSystemSummary(string path, string url, IElementNavigator input) : base(path, url, input)
        {
            Debug.Assert(ResourceType == SupportedType);

            var list = new List<string>();
            if (input.MoveToFirstChild("uniqueId"))
            {
                do
                {
                    var nav = input.Clone();
                    if (nav.MoveToFirstChild("value"))
                    {
                        list.Add(nav.Value?.ToString());
                    }
                } while (input.MoveToNext("uniqueId"));
            }
            UniqueIds = list.ToArray();
        }

        /// <summary>Unique identifiers of the naming system.</summary>
        public string[] UniqueIds { get; }
    }


    /// <summary>Extended summary information for FHIR ConceptMap resources.</summary>
    public class ConceptMapSummary : ConformanceResourceSummary
    {
        public const ResourceType SupportedType = Model.ResourceType.ConceptMap;

        public ConceptMapSummary(string path, string url, IElementNavigator input) : base(path, url, input)
        {
            Debug.Assert(ResourceType == SupportedType);

            if (input.MoveToNext("sourceUri"))
            {
                ConceptMapSource = input.Value?.ToString();
            }
            else if (input.MoveToNext("sourceReference"))
            {
                var nav = input.Clone();
                if (nav.MoveToFirstChild("reference"))
                {
                    ConceptMapSource = nav.Value?.ToString();
                }

            }

            if (input.MoveToNext("targetUri"))
            {
                ConceptMapTarget = input.Value?.ToString();
            }
            else if (input.MoveToNext("targetReference"))
            {
                var nav = input.Clone();
                if (nav.MoveToFirstChild("reference"))
                {
                    ConceptMapTarget = nav.Value?.ToString();
                }

            }
        }

        /// <summary>Url that identifies the source of the mapping.</summary>
        public string ConceptMapSource { get; }

        /// <summary>Url that identifies the target of the mapping.</summary>
        public string ConceptMapTarget { get; }
    }
}
