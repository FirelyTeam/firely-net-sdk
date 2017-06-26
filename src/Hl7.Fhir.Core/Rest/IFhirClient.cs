using System;
using System.Net;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

namespace Hl7.Fhir.Rest
{
    public interface IFhirClient
    {
        bool CompressRequestBody { get; set; }
        Uri Endpoint { get; }
        byte[] LastBody { get; }
        Resource LastBodyAsResource { get; }
        string LastBodyAsText { get; }
        HttpWebRequest LastRequest { get; }
        HttpWebResponse LastResponse { get; }
        Bundle.ResponseComponent LastResult { get; }
        ParserSettings ParserSettings { get; set; }
        bool PreferCompressedResponses { get; set; }
        ResourceFormat PreferredFormat { get; set; }
        bool ReturnFullResource { get; set; }
        int Timeout { get; set; }
        bool UseFormatParam { get; set; }
        bool VerifyFhirVersion { get; set; }

        event EventHandler<AfterResponseEventArgs> OnAfterResponse;
        event EventHandler<BeforeRequestEventArgs> OnBeforeRequest;

        Conformance Conformance(SummaryType? summary = default(SummaryType?));
        Bundle Continue(Bundle current, PageDirection direction = PageDirection.Next);
        TResource Create<TResource>(TResource resource) where TResource : Resource;
        TResource Create<TResource>(TResource resource, SearchParams condition) where TResource : Resource;
        void Delete(Resource resource);
        void Delete(string location);
        void Delete(string resourceType, SearchParams condition);
        void Delete(Uri location);
        Resource Get(string url);
        Resource Get(Uri url);
        Bundle History(string location, DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False);
        Bundle History(Uri location, DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False);
        Resource InstanceOperation(Uri location, string operationName, Parameters parameters = null, bool useGet = false);
        Resource Operation(Uri operation, Parameters parameters = null, bool useGet = false);
        Resource Operation(Uri location, string operationName, Parameters parameters = null, bool useGet = false);
        TResource Read<TResource>(string location, string ifNoneMatch = null, DateTimeOffset? ifModifiedSince = default(DateTimeOffset?)) where TResource : Resource;
        TResource Read<TResource>(Uri location, string ifNoneMatch = null, DateTimeOffset? ifModifiedSince = default(DateTimeOffset?)) where TResource : Resource;
        TResource Refresh<TResource>(TResource current) where TResource : Resource;
        Bundle Search(SearchParams q, string resourceType = null);
        Bundle Search(string resource, string[] criteria = null, string[] includes = null, int? pageSize = default(int?), SummaryType? summary = default(SummaryType?));
        Bundle Search<TResource>(string[] criteria = null, string[] includes = null, int? pageSize = default(int?), SummaryType? summary = default(SummaryType?)) where TResource : Resource, new();
        Bundle Search<TResource>(SearchParams q) where TResource : Resource;
        Bundle SearchById(string resource, string id, string[] includes = null, int? pageSize = default(int?));
        Bundle SearchById<TResource>(string id, string[] includes = null, int? pageSize = default(int?)) where TResource : Resource, new();
        Bundle Transaction(Bundle bundle);
        Bundle TypeHistory(string resourceType, DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False);
        Bundle TypeHistory<TResource>(DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False) where TResource : Resource, new();
        Resource TypeOperation(string operationName, string typeName, Parameters parameters = null, bool useGet = false);
        Resource TypeOperation<TResource>(string operationName, Parameters parameters = null, bool useGet = false) where TResource : Resource;
        TResource Update<TResource>(TResource resource, bool versionAware = false) where TResource : Resource;
        TResource Update<TResource>(TResource resource, SearchParams condition, bool versionAware = false) where TResource : Resource;
        Bundle WholeSystemHistory(DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False);
        Resource WholeSystemOperation(string operationName, Parameters parameters = null, bool useGet = false);
        Bundle WholeSystemSearch(string[] criteria = null, string[] includes = null, int? pageSize = default(int?), SummaryType? summary = default(SummaryType?));
    }
}