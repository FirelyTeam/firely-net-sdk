using Hl7.Fhir.Model;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Hl7.Fhir.Rest
{
    public interface IAsyncFhirClient
    {
        Uri Endpoint { get; }
        byte[] LastBody { get; }
        Resource LastBodyAsResource { get; }
        string LastBodyAsText { get; }
        Bundle.ResponseComponent LastResult { get; }
        Task<CapabilityStatement> CapabilityStatementAsync(SummaryType? summary = default(SummaryType?));
        Task<Bundle> ContinueAsync(Bundle current, PageDirection direction = PageDirection.Next);
        Task<TResource> CreateAsync<TResource>(TResource resource) where TResource : Resource;
        Task<TResource> CreateAsync<TResource>(TResource resource, SearchParams condition) where TResource : Resource;
        System.Threading.Tasks.Task DeleteAsync(Resource resource);
        System.Threading.Tasks.Task DeleteAsync(string location);
        System.Threading.Tasks.Task DeleteAsync(string resourceType, SearchParams condition);
        System.Threading.Tasks.Task DeleteAsync(Uri location);
        Task<TResource> executeAsync<TResource>(Bundle tx, HttpStatusCode expect) where TResource : Resource;
        Task<Resource> GetAsync(string url);
        Task<Resource> GetAsync(Uri url);
        Task<Bundle> HistoryAsync(string location, DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False);
        Task<Bundle> HistoryAsync(Uri location, DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False);
        Task<Resource> InstanceOperationAsync(Uri location, string operationName, Parameters parameters = null, bool useGet = false);
        Task<Resource> OperationAsync(Uri operation, Parameters parameters = null, bool useGet = false);
        Task<Resource> OperationAsync(Uri location, string operationName, Parameters parameters = null, bool useGet = false);
        Task<TResource> ReadAsync<TResource>(string location, string ifNoneMatch = null, DateTimeOffset? ifModifiedSince = default(DateTimeOffset?)) where TResource : Resource;
        Task<TResource> ReadAsync<TResource>(Uri location, string ifNoneMatch = null, DateTimeOffset? ifModifiedSince = default(DateTimeOffset?)) where TResource : Resource;
        Task<TResource> RefreshAsync<TResource>(TResource current) where TResource : Resource;
        Task<Bundle> SearchAsync(SearchParams q, string resourceType = null);
        Task<Bundle> SearchAsync(string resource, string[] criteria = null, string[] includes = null, int? pageSize = default(int?), SummaryType? summary = default(SummaryType?), string[] revIncludes = null);
        Task<Bundle> SearchAsync<TResource>(string[] criteria = null, string[] includes = null, int? pageSize = default(int?), SummaryType? summary = default(SummaryType?), string[] revIncludes = null) where TResource : Resource, new();
        Task<Bundle> SearchAsync<TResource>(SearchParams q) where TResource : Resource;
        Task<Bundle> SearchByIdAsync(string resource, string id, string[] includes = null, int? pageSize = default(int?), string[] revIncludes = null);
        Task<Bundle> SearchByIdAsync<TResource>(string id, string[] includes = null, int? pageSize = default(int?), string[] revIncludes = null) where TResource : Resource, new();
        Bundle SearchByIdUsingPost(string resource, string id, string[] includes = null, int? pageSize = null, string[] revIncludes = null);
        Bundle SearchByIdUsingPost<TResource>(string id, string[] includes = null, int? pageSize = null, string[] revIncludes = null) where TResource : Resource, new();
        Task<Bundle> SearchByIdUsingPostAsync(string resource, string id, string[] includes = null, int? pageSize = null, string[] revIncludes = null);
        Task<Bundle> SearchByIdUsingPostAsync<TResource>(string id, string[] includes = null, int? pageSize = null, string[] revIncludes = null) where TResource : Resource, new();
        Task<Bundle> TransactionAsync(Bundle bundle);
        Task<Bundle> TypeHistoryAsync(string resourceType, DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False);
        Task<Bundle> TypeHistoryAsync<TResource>(DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False) where TResource : Resource, new();
        Task<Resource> TypeOperationAsync(string operationName, string typeName, Parameters parameters = null, bool useGet = false);
        Task<Resource> TypeOperationAsync<TResource>(string operationName, Parameters parameters = null, bool useGet = false) where TResource : Resource;
        Task<TResource> UpdateAsync<TResource>(TResource resource, bool versionAware = false) where TResource : Resource;
        Task<TResource> UpdateAsync<TResource>(TResource resource, SearchParams condition, bool versionAware = false) where TResource : Resource;
        Task<Bundle> WholeSystemHistoryAsync(DateTimeOffset? since = default(DateTimeOffset?), int? pageSize = default(int?), SummaryType summary = SummaryType.False);
        Task<Resource> WholeSystemOperationAsync(string operationName, Parameters parameters = null, bool useGet = false);
        Task<Bundle> WholeSystemSearchAsync(string[] criteria = null, string[] includes = null, int? pageSize = default(int?), SummaryType? summary = default(SummaryType?), string[] revIncludes = null);
        Task<Bundle> WholeSystemSearchUsingPostAsync(string[] criteria = null, string[] includes = null, int? pageSize = null, SummaryType? summary = null, string[] revIncludes = null);

    }
}