using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Source;

public class ResolverException : CodedException
{
    public const string NOT_IMPLEMENTED = "RESOLVE101";
    public const string NOT_FOUND = "RESOLVE102";
    public const string SNAPSHOT_FAILURE = "RESOLVE103";
    public const string ARTIFACT_SUMMARY_NO_MATCH = "RESOLVE104";
    
    public ResolverException(string errorCode, string message) : base(errorCode, message)
    {
    }

    public ResolverException(string errorCode, string message, Exception innerException) : base(errorCode, message, innerException)
    {
    }

    internal static ResolverException NotImplemented(Exception ex) => new(NOT_IMPLEMENTED, "Resolver does not implement the used Resolve method.", ex);
    internal static ResolverException NotFound() => new(NOT_FOUND, "Resource could not be found.");
    internal static ResolverException MultiResolverNotFound(List<ResolverException> innerErrors)
    {
        var commaSeparatedErrors = string.Join(", ", innerErrors
            .OrderBy(x => x.ErrorCode)
            .Select(x => x.Message));

        return new ResolverException(NOT_FOUND, $"None of the resolvers could find the resource. Following errors reported: {Environment.NewLine}{commaSeparatedErrors}", new AggregateException(innerErrors));
    }
    public static ResolverException SnapshotOutcome(OperationOutcome generatorOutcome)
    {
        var outcomeMessages = string.Join(Environment.NewLine, generatorOutcome.Issue.Select(x => x.ToString()));
        return new ResolverException(SNAPSHOT_FAILURE, outcomeMessages);
    }
    public static ResolverException ArtifactSummaryNoMatch(string uri)
    {
        return new ResolverException(ARTIFACT_SUMMARY_NO_MATCH, $"No summary matching the provided {nameof(uri)}: {uri}");
    }
}