using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Model
{
    public interface IBundle
    {
        BundleType? Type { get; }
        int? Total { get; }
        Uri FirstLink { get; }
        Uri PreviousLink { get; }
        Uri NextLink { get; }
        Uri LastLink { get; }
        IEnumerable<IBundleEntry> Entries { get; }
    }

    public interface IBundleEntry
    {
        string FullUrl { get; }
        Resource Resource { get;  }
        IBundleSearch Search { get; }
        IBundleRequest Request { get; }
        IBundleResponse Response { get; }
    }

    public interface IBundleSearch
    {
        SearchEntryMode? Mode { get; }
        decimal? Score { get; }
    }

    public interface IBundleRequest
    {
        HTTPVerb? Method { get; }
        string Url { get; }
        string IfNoneMatch { get; }
        DateTimeOffset? IfModifiedSince { get; }
        string IfMatch { get; }
        string IfNoneExist { get; }
    }

    public interface IBundleResponse
    {
        string Status { get; }
        string Location { get; }
        string Etag { get; }
        DateTimeOffset? LastModified { get; }
    }
}
