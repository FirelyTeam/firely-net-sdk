using System;
using System.Net;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

namespace Hl7.Fhir.Rest
{
    public interface IFhirClient<TBundle, TMetadata> 
        where TBundle : Model.Resource 
        where TMetadata : Model.Resource
    {
#if !NETSTANDARD1_1
        bool PreferCompressedResponses { get; set; }
        bool CompressRequestBody { get; set; }
#endif
        Uri Endpoint { get; }
        byte[] LastBody { get; }
        Resource LastBodyAsResource { get; }
        string LastBodyAsText { get; }
        Response LastResult { get; }
        ParserSettings ParserSettings { get; }
        ResourceFormat PreferredFormat { get; set; }
        bool ReturnFullResource { get; set; }
        int Timeout { get; set; }
        bool UseFormatParam { get; set; }
        bool VerifyFhirVersion { get; set; }

        event EventHandler<AfterResponseEventArgs> OnAfterResponse;
        event EventHandler<BeforeRequestEventArgs> OnBeforeRequest;

        TMetadata Metadata(SummaryType? summary = default(SummaryType?));
        Task<TMetadata> MetadataAsync(SummaryType? summary = default(SummaryType?));
        TBundle Continue(TBundle current, PageDirection direction = PageDirection.Next);
        Task<TBundle> ContinueAsync(TBundle current, PageDirection direction = PageDirection.Next);
        TResource Create<TResource>(TResource resource) where TResource : Resource;
        TResource Create<TResource>(TResource resource, SearchParams condition) where TResource : Resource;
        Task<TResource> CreateAsync<TResource>(TResource resource) where TResource : Resource;
        Task<TResource> CreateAsync<TResource>(TResource resource, SearchParams condition) where TResource : Resource;
        void Delete(Resource resource);
        void Delete(string location);
        void Delete(string resourceType, SearchParams condition);
        void Delete(Uri location);
        Task DeleteAsync(Resource resource);
        Task DeleteAsync(string location);
        Task DeleteAsync(string resourceType, SearchParams condition);
        Task DeleteAsync(Uri location);
        Task<TResource> executeAsync<TResource>(Request request, HttpStatusCode expect) where TResource : Resource;
        Resource Get(string url);
        Resource Get(Uri url);
        Task<Resource> GetAsync(string url);
        Task<Resource> GetAsync(Uri url);
        TBundle History(string location, DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False);
        TBundle History(Uri location, DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False);
        Task<TBundle> HistoryAsync(string location, DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False);
        Task<TBundle> HistoryAsync(Uri location, DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False);
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
        TBundle Search(SearchParams q, string resourceType = null);
        TBundle Search(string resource, string[] criteria = null, string[] includes = null, int? pageSize = default(int?), SummaryType? summary = default(SummaryType?), string[] revIncludes = null);
        TBundle Search<TResource>(string[] criteria = null, string[] includes = null, int? pageSize = default(int?), SummaryType? summary = default(SummaryType?), string[] revIncludes = null) where TResource : Resource, new();
        TBundle Search<TResource>(SearchParams q) where TResource : Resource;
        Task<TBundle> SearchAsync(SearchParams q, string resourceType = null);
        Task<TBundle> SearchAsync(string resource, string[] criteria = null, string[] includes = null, int? pageSize = default(int?), SummaryType? summary = default(SummaryType?), string[] revIncludes = null, string[] iterativeIncludes = null);
        Task<TBundle> SearchAsync<TResource>(string[] criteria = null, string[] includes = null, int? pageSize = default(int?), SummaryType? summary = default(SummaryType?), string[] revIncludes = null) where TResource : Resource, new();
        Task<TBundle> SearchAsync<TResource>(SearchParams q) where TResource : Resource;


        TBundle SearchUsingPost(SearchParams q, string resourceType = null);
        TBundle SearchUsingPost<TResource>(SearchParams q) where TResource : Resource;
        Task<TBundle> SearchUsingPostAsync(SearchParams q, string resourceType = null);
        Task<TBundle> SearchUsingPostAsync<TResource>(SearchParams q) where TResource : Resource;
        Task<TBundle> SearchUsingPostAsync<TResource>(string[] criteria = null, string[] includes = null, int? pageSize = null, SummaryType? summary = null, string[] revIncludes = null) where TResource : Resource, new();
        TBundle SearchUsingPost<TResource>(string[] criteria = null, string[] includes = null, int? pageSize = null, SummaryType? summary = null, string[] revIncludes = null) where TResource : Resource, new();
        Task<TBundle> SearchUsingPostAsync(string resource, string[] criteria = null, string[] includes = null, int? pageSize = null, SummaryType? summary = null, string[] revIncludes = null, string[] iterativeIncludes = null);
        TBundle SearchUsingPost(string resource, string[] criteria = null, string[] includes = null, int? pageSize = null, SummaryType? summary = null, string[] revIncludes = null);

        TBundle SearchById(string resource, string id, string[] includes = null, int? pageSize = default(int?), string[] revIncludes = null);
        TBundle SearchById<TResource>(string id, string[] includes = null, int? pageSize = default(int?), string[] revIncludes = null) where TResource : Resource, new();
        Task<TBundle> SearchByIdAsync(string resource, string id, string[] includes = null, int? pageSize = default(int?), string[] revIncludes = null, string[] iterativeIncludes = null);
        Task<TBundle> SearchByIdAsync<TResource>(string id, string[] includes = null, int? pageSize = default(int?), string[] revIncludes = null) where TResource : Resource, new();

        TBundle SearchByIdUsingPost(string resource, string id, string[] includes = null, int? pageSize = null, string[] revIncludes = null);
        TBundle SearchByIdUsingPost<TResource>(string id, string[] includes = null, int? pageSize = null, string[] revIncludes = null) where TResource : Resource, new();
        Task<TBundle> SearchByIdUsingPostAsync(string resource, string id, string[] includes = null, int? pageSize = null, string[] revIncludes = null, string[] iterativeIncludes = null);
        Task<TBundle> SearchByIdUsingPostAsync<TResource>(string id, string[] includes = null, int? pageSize = null, string[] revIncludes = null) where TResource : Resource, new();

        TBundle Transaction(TBundle bundle);
        Task<TBundle> TransactionAsync(TBundle bundle);
        TBundle TypeHistory(string resourceType, DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False);
        TBundle TypeHistory<TResource>(DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False) where TResource : Resource, new();
        Task<TBundle> TypeHistoryAsync(string resourceType, DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False);
        Task<TBundle> TypeHistoryAsync<TResource>(DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False) where TResource : Resource, new();
        Resource TypeOperation(string operationName, string typeName, Parameters parameters = null, bool useGet = false);
        Resource TypeOperation<TResource>(string operationName, Parameters parameters = null, bool useGet = false) where TResource : Resource;
        Task<Resource> TypeOperationAsync(string operationName, string typeName, Parameters parameters = null, bool useGet = false);
        Task<Resource> TypeOperationAsync<TResource>(string operationName, Parameters parameters = null, bool useGet = false) where TResource : Resource;
        TResource Update<TResource>(TResource resource, bool versionAware = false) where TResource : Resource;
        TResource Update<TResource>(TResource resource, SearchParams condition, bool versionAware = false) where TResource : Resource;
        Task<TResource> UpdateAsync<TResource>(TResource resource, bool versionAware = false) where TResource : Resource;
        Task<TResource> UpdateAsync<TResource>(TResource resource, SearchParams condition, bool versionAware = false) where TResource : Resource;
        TBundle WholeSystemHistory(DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False);
        Task<TBundle> WholeSystemHistoryAsync(DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False);
        Resource WholeSystemOperation(string operationName, Parameters parameters = null, bool useGet = false);
        Task<Resource> WholeSystemOperationAsync(string operationName, Parameters parameters = null, bool useGet = false);
        TBundle WholeSystemSearch(string[] criteria = null, string[] includes = null, int? pageSize = default(int?), SummaryType? summary = default(SummaryType?), string[] revIncludes = null);
        Task<TBundle> WholeSystemSearchAsync(string[] criteria = null, string[] includes = null, int? pageSize = default(int?), SummaryType? summary = default(SummaryType?), string[] revIncludes = null, string[] iterativeIncludes = null);

        Task<TBundle> WholeSystemSearchUsingPostAsync(string[] criteria = null, string[] includes = null, int? pageSize = null, SummaryType? summary = null, string[] revIncludes = null, string[] iterativeIncludes = null);
        TBundle WholeSystemSearchUsingPost(string[] criteria = null, string[] includes = null, int? pageSize = null, SummaryType? summary = null, string[] revIncludes = null);
    }
}