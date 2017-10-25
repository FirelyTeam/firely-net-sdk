using Hl7.Fhir.Model;
using System;

namespace Hl7.Fhir.Specification.Tests.Source.Summary
{
    public class ArtifactSummary
    {
        public const string OriginKey = nameof(Origin);
        public const string PositionKey = nameof(Position);
        public const string ResourceUriKey = nameof(ResourceUri);
        public const string ResourceTypeKey = nameof(ResourceType);

        protected readonly ArtifactSummaryProperties _properties;

        public ArtifactSummary(ArtifactSummaryProperties properties) { _properties = properties; }

        ArtifactSummary(ArtifactSummaryProperties properties, Exception error) : this(properties) { Error = error; }

        public static ArtifactSummary FromException(ArtifactSummaryProperties properties, Exception ex)
            => new ArtifactSummary(properties, ex);
        public static ArtifactSummary FromException(string origin, Exception ex)
        {
            var props = new ArtifactSummaryProperties();
            props[OriginKey] = origin;
            return new ArtifactSummary(props, ex);
        }

        public Exception Error { get; }
        public object this[string key] => _properties[key];

        public string Origin => _properties[OriginKey] as string;
        public string Position => _properties[PositionKey] as string;
        public string ResourceUri => _properties[ResourceUriKey] as string;
        public string ResourceType => _properties[ResourceTypeKey] as string;
        public FHIRDefinedType? Type => ModelInfo.FhirTypeNameToFhirType(ResourceType);
    }
}
