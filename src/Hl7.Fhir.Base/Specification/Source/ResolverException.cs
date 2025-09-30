using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace Hl7.Fhir.Specification.Source;
/// <summary>
/// Exception reporting issues during resolving in <see cref="IResourceResolver"/> and <see cref="IAsyncResourceResolver"/>.
/// It is built on top of <see cref="CodedException"/> reporting errors as code and reacting to relevant issues appropriatelly.
/// </summary>
public class ResolverException : CodedException
{
    /// <summary>
    /// The used resolve operation was not implemented.
    /// </summary>
    public const string NOT_IMPLEMENTED = "RESOLVE101";
    /// <summary>
    /// The requested resource could not be found.
    /// </summary>
    public const string NOT_FOUND = "RESOLVE102";
    /// <summary>
    /// Failure during snapshot generation.
    /// </summary>
    public const string SNAPSHOT_FAILURE = "RESOLVE103";
    /// <summary>
    /// Failure during requested operation.
    /// </summary>
    public const string OPERATION_FAILURE = "RESOLVE104";
    /// <summary>
    /// No match has been found in the artifact summary for the requested uri.
    /// </summary>
    public const string ARTIFACT_SUMMARY_NO_MATCH = "RESOLVE105";
    /// <summary>
    /// Artifact summary has been found, but required information to resolve the resource were missing.
    /// </summary>
    public const string ARTIFACT_SUMMARY_ARGUMENT_EXCEPTION = "RESOLVE106";
    /// <summary>
    /// Resource identity does not represent a valid URI.
    /// </summary>
    public const string INVALID_RESOURCE_IDENTITY = "RESOLVE107";
    
    /// <summary>
    /// Constructor for <see cref="ResolverException"/>.
    /// </summary>
    /// <param name="errorCode">Code relevant for the issue the exception represents</param>
    /// <param name="message">Description of the issues</param>
    public ResolverException(string errorCode, string message) : base(errorCode, message)
    {
    }

    /// <summary>
    /// Constructor for <see cref="ResolverException"/>.
    /// </summary>
    /// <param name="errorCode">Code relevant for the issue the exception represents</param>
    /// <param name="message">Description of the issues</param>
    /// <param name="innerException">Inner exception that is being wrapped by resolver</param>
    public ResolverException(string errorCode, string message, Exception innerException) : base(errorCode, message, innerException)
    {
    }

    public static ResolverException NotImplemented(Exception ex) => new(NOT_IMPLEMENTED, "Resolver does not implement the used Resolve method.", ex);
    public static ResolverException NotFound(OperationOutcome? issues = null) => new(NOT_FOUND, 
        issues is null 
            ? "Resource could not be found." 
            : $"Resource could not be found. The operation outcome for this resource was: {issues}");
    internal static ResolverException MultiResolverNotFound(List<ResolverException> innerErrors)
    {
        var commaSeparatedErrors = string.Join(", ", innerErrors
            .OrderBy(x => x.ErrorCode)
            .Select(x => x.Message));

        return new ResolverException(NOT_FOUND, $"None of the resolvers could find the resource. Following errors reported: {Environment.NewLine}{commaSeparatedErrors}", new AggregateException(innerErrors));
    }
    internal static ResolverException SnapshotOutcome(OperationOutcome generatorOutcome)
    {
        var outcomeMessages = string.Join(Environment.NewLine, generatorOutcome.Issue.Select(x => x.ToString()));
        return new ResolverException(SNAPSHOT_FAILURE, outcomeMessages);
    }

    internal static ResolverException ArtifactSummaryNoMatch(string uri)
    {
        return new ResolverException(ARTIFACT_SUMMARY_NO_MATCH, $"No summary matching the provided {nameof(uri)}: {uri}");
    }
    
    internal static ResolverException ArtifactSummaryArgumentException(ArgumentException ex)
    {
        return new ResolverException(ARTIFACT_SUMMARY_ARGUMENT_EXCEPTION, ex.Message, ex);
    }

    internal static ResolverException NotValidResourceIdentity(string uri)
    {
        return new ResolverException(INVALID_RESOURCE_IDENTITY, $"Provided {nameof(uri)} is not a valid resource identity URI: {uri}");
    }
    internal static ResolverException OperationFailed(string message, Exception innerException)
    {
        return new ResolverException(OPERATION_FAILURE, message, innerException);
    }
}