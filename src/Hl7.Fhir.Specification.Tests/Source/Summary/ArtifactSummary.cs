using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Tests.Source.Summary
{
    public class ArtifactSummary
    {
        public const string OriginKey = nameof(Origin);
        public const string PositionKey = nameof(Position);
        public const string ResourceUriKey = nameof(ResourceUri);
        public const string ResourceTypeKey = nameof(ResourceType);

        protected readonly ArtifactSummaryPropertyBag _properties;

        public ArtifactSummary(ArtifactSummaryPropertyBag properties) { _properties = properties; }

        ArtifactSummary(ArtifactSummaryPropertyBag properties, Exception error) : this(properties) { Error = error; }

        public static ArtifactSummary FromException(ArtifactSummaryPropertyBag properties, Exception ex)
            => new ArtifactSummary(properties, ex);
        public static ArtifactSummary FromException(string origin, Exception ex)
        {
            var props = new ArtifactSummaryPropertyBag();
            props[OriginKey] = origin;
            return new ArtifactSummary(props, ex);
        }

        public Exception Error { get; }
        public string Origin => _properties[OriginKey] as string;
        public string Position => _properties[PositionKey] as string;
        public string ResourceUri => _properties[ResourceUriKey] as string;
        public string ResourceType => _properties[ResourceTypeKey] as string;
        public FHIRDefinedType? Type => ModelInfo.FhirTypeNameToFhirType(ResourceType);
    }
}
