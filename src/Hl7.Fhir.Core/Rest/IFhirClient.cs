using System;
using System.Net;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

namespace Hl7.Fhir.Rest
{
    public interface IFhirClient
    {
#if NET_COMPRESSION
        bool PreferCompressedResponses { get; set; }
        bool CompressRequestBody { get; set; }
#endif

        Uri Endpoint { get; }
        byte[] LastBody { get; }
        Resource LastBodyAsResource { get; }
        string LastBodyAsText { get; }
        HttpWebRequest LastRequest { get; }
        HttpWebResponse LastResponse { get; }
        Bundle.ResponseComponent LastResult { get; }
        ParserSettings ParserSettings { get; set; }
        ResourceFormat PreferredFormat { get; set; }
        bool ReturnFullResource { get; set; }
        int Timeout { get; set; }
        bool UseFormatParam { get; set; }
        bool VerifyFhirVersion { get; set; }

        event EventHandler<AfterResponseEventArgs> OnAfterResponse;
        event EventHandler<BeforeRequestEventArgs> OnBeforeRequest;

        CapabilityStatement CapabilityStatement(SummaryType? summary = default(SummaryType?));
        Task<CapabilityStatement> CapabilityStatementAsync(SummaryType? summary = default(SummaryType?));
        Bundle Continue(Bundle current, PageDirection direction = PageDirection.Next);
        Task<Bundle> ContinueAsync(Bundle current, PageDirection direction = PageDirection.Next);
        TResource Create<TResource>(TResource resource) where TResource : Resource;
        TResource Create<TResource>(TResource resource, SearchParams condition) where TResource : Resource;
        Task<TResource> CreateAsync<TResource>(TResource resource) where TResource : Resource;
        Task<TResource> CreateAsync<TResource>(TResource resource, SearchParams condition) where TResource : Resource;
        void Delete(Resource resource);
        void Delete(string location);
        void Delete(string resourceType, SearchParams condition);
        void Delete(Uri location);
        System.Threading.Tasks.Task DeleteAsync(Resource resource);
        System.Threading.Tasks.Task DeleteAsync(string location);
        System.Threading.Tasks.Task DeleteAsync(string resourceType, SearchParams condition);
        System.Threading.Tasks.Task DeleteAsync(Uri location);
        Task<TResource> executeAsync<TResource>(Bundle tx, HttpStatusCode expect) where TResource : Resource;
        Resource Get(string url);
        Resource Get(Uri url);
        Task<Resource> GetAsync(string url);
        Task<Resource> GetAsync(Uri url);
        Bundle History(string location, DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False);
        Bundle History(Uri location, DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False);
        Task<Bundle> HistoryAsync(string location, DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False);
        Task<Bundle> HistoryAsync(Uri location, DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False);
        Resource InstanceOperation(Uri location, string operationName, Parameters parameters = null, bool useGet = false);
        Task<Resource> InstanceOperationAsync(Uri location, string operationName, Parameters parameters = null, bool useGet = false);
        Resource Operation(Uri operation, Parameters parameters = null, bool useGet = false);
        Resource Operation(Uri location, string operationName, Parameters parameters = null, bool useGet = false);
        Task<Resource> OperationAsync(Uri operation, Parameters parameters = null, bool useGet = false);
        Task<Resource> OperationAsync(Uri location, string operationName, Parameters parameters = null, bool useGet = false);
        TResource Read<TResource>(string location, string ifNoneMatch = null, DateTimeOffset? ifModifiedSince = default(DateTimeOffset?)) where TResource : Resource;
        TResource Read<TResource>(Uri location, string ifNoneMatch = null, DateTimeOffset? ifModifiedSince = default(DateTimeOffset?)) where TResource : Resource;
        Task<TResource> ReadAsync<TResource>(string location, string ifNoneMatch = null, DateTimeOffset? ifModifiedSince = default(DateTimeOffset?)) where TResource : Resource;
        Task<TResource> ReadAsync<TResource>(Uri location, string ifNoneMatch = null, DateTimeOffset? ifModifiedSince = default(DateTimeOffset?)) where TResource : Resource;
        TResource Refresh<TResource>(TResource current) where TResource : Resource;
        Task<TResource> RefreshAsync<TResource>(TResource current) where TResource : Resource;
        Bundle Search(SearchParams q, string resourceType = null);
        Bundle Search(string resource, string[] criteria = null, string[] includes = null, int? pageSize = default(int?), SummaryType? summary = default(SummaryType?), string[] revIncludes = null);
        Bundle Search<TResource>(string[] criteria = null, string[] includes = null, int? pageSize = default(int?), SummaryType? summary = default(SummaryType?), string[] revIncludes = null) where TResource : Resource, new();
        Bundle Search<TResource>(SearchParams q) where TResource : Resource;
        Task<Bundle> SearchAsync(SearchParams q, string resourceType = null);
        Task<Bundle> SearchAsync(string resource, string[] criteria = null, string[] includes = null, int? pageSize = default(int?), SummaryType? summary = default(SummaryType?), string[] revIncludes = null);
        Task<Bundle> SearchAsync<TResource>(string[] criteria = null, string[] includes = null, int? pageSize = default(int?), SummaryType? summary = default(SummaryType?), string[] revIncludes = null) where TResource : Resource, new();
        Task<Bundle> SearchAsync<TResource>(SearchParams q) where TResource : Resource;


        Bundle SearchUsingPost(SearchParams q, string resourceType = null);
        Bundle SearchUsingPost<TResource>(SearchParams q) where TResource : Resource;
        Task<Bundle> SearchUsingPostAsync(SearchParams q, string resourceType = null);
        Task<Bundle> SearchUsingPostAsync<TResource>(SearchParams q) where TResource : Resource;
        Task<Bundle> SearchUsingPostAsync<TResource>(string[] criteria = null, string[] includes = null, int? pageSize = null, SummaryType? summary = null, string[] revIncludes = null) where TResource : Resource, new();
        Bundle SearchUsingPost<TResource>(string[] criteria = null, string[] includes = null, int? pageSize = null, SummaryType? summary = null, string[] revIncludes = null) where TResource : Resource, new();
        Task<Bundle> SearchUsingPostAsync(string resource, string[] criteria = null, string[] includes = null, int? pageSize = null, SummaryType? summary = null, string[] revIncludes = null);
        Bundle SearchUsingPost(string resource, string[] criteria = null, string[] includes = null, int? pageSize = null, SummaryType? summary = null, string[] revIncludes = null);

        Bundle SearchById(string resource, string id, string[] includes = null, int? pageSize = default(int?), string[] revIncludes = null);
        Bundle SearchById<TResource>(string id, string[] includes = null, int? pageSize = default(int?), string[] revIncludes = null) where TResource : Resource, new();
        Task<Bundle> SearchByIdAsync(string resource, string id, string[] includes = null, int? pageSize = default(int?), string[] revIncludes = null);
        Task<Bundle> SearchByIdAsync<TResource>(string id, string[] includes = null, int? pageSize = default(int?), string[] revIncludes = null) where TResource : Resource, new();

        Bundle SearchByIdUsingPost(string resource, string id, string[] includes = null, int? pageSize = null, string[] revIncludes = null);
        Bundle SearchByIdUsingPost<TResource>(string id, string[] includes = null, int? pageSize = null, string[] revIncludes = null) where TResource : Resource, new();
        Task<Bundle> SearchByIdUsingPostAsync(string resource, string id, string[] includes = null, int? pageSize = null, string[] revIncludes = null);
        Task<Bundle> SearchByIdUsingPostAsync<TResource>(string id, string[] includes = null, int? pageSize = null, string[] revIncludes = null) where TResource : Resource, new();

        Bundle Transaction(Bundle bundle);
        Task<Bundle> TransactionAsync(Bundle bundle);
        Bundle TypeHistory(string resourceType, DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False);
        Bundle TypeHistory<TResource>(DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False) where TResource : Resource, new();
        Task<Bundle> TypeHistoryAsync(string resourceType, DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False);
        Task<Bundle> TypeHistoryAsync<TResource>(DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False) where TResource : Resource, new();
        Resource TypeOperation(string operationName, string typeName, Parameters parameters = null, bool useGet = false);
        Resource TypeOperation<TResource>(string operationName, Parameters parameters = null, bool useGet = false) where TResource : Resource;
        Task<Resource> TypeOperationAsync(string operationName, string typeName, Parameters parameters = null, bool useGet = false);
        Task<Resource> TypeOperationAsync<TResource>(string operationName, Parameters parameters = null, bool useGet = false) where TResource : Resource;
        TResource Update<TResource>(TResource resource, bool versionAware = false) where TResource : Resource;
        TResource Update<TResource>(TResource resource, SearchParams condition, bool versionAware = false) where TResource : Resource;
        Task<TResource> UpdateAsync<TResource>(TResource resource, bool versionAware = false) where TResource : Resource;
        Task<TResource> UpdateAsync<TResource>(TResource resource, SearchParams condition, bool versionAware = false) where TResource : Resource;
        Bundle WholeSystemHistory(DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False);
        Task<Bundle> WholeSystemHistoryAsync(DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False);
        Resource WholeSystemOperation(string operationName, Parameters parameters = null, bool useGet = false);
        Task<Resource> WholeSystemOperationAsync(string operationName, Parameters parameters = null, bool useGet = false);
        Bundle WholeSystemSearch(string[] criteria = null, string[] includes = null, int? pageSize = default(int?), SummaryType? summary = default(SummaryType?), string[] revIncludes = null);
        Task<Bundle> WholeSystemSearchAsync(string[] criteria = null, string[] includes = null, int? pageSize = default(int?), SummaryType? summary = default(SummaryType?), string[] revIncludes = null);

        Task<Bundle> WholeSystemSearchUsingPostAsync(string[] criteria = null, string[] includes = null, int? pageSize = null, SummaryType? summary = null, string[] revIncludes = null);
        Bundle WholeSystemSearchUsingPost(string[] criteria = null, string[] includes = null, int? pageSize = null, SummaryType? summary = null, string[] revIncludes = null);
    }
}