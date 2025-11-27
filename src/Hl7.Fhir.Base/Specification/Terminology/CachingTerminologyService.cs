using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

#nullable enable

namespace Hl7.Fhir.Specification.Terminology;

public class CachingTerminologyService(ITerminologyService terminologyService, MemoryCacheOptions? options = null, MemoryCacheEntryOptions? entryOptions = null) : ITerminologyService
{
    private readonly IMemoryCache _cache = new MemoryCache(
        options ?? new MemoryCacheOptions()
        {
            SizeLimit = 1024 * 1024 * 100, // 100 MB
        }
    );
    
    private readonly MemoryCacheEntryOptions _entryOptions = entryOptions ?? new MemoryCacheEntryOptions()
    {
        SlidingExpiration = TimeSpan.FromMinutes(30),
        Size = 1 // Each entry is counted as size 1
    };
    
    public Task<Parameters> ValueSetValidateCode(Parameters parameters, string? id = null, bool useGet = false)
    {
        return _cache.GetOrCreate<Task<Parameters>>((parameters.GetHashCode(), id), entry =>
        {
            entry.SetOptions(_entryOptions);
            return terminologyService.ValueSetValidateCode(parameters, id, useGet);
        })!;
    } 

    public Task<Parameters> Subsumes(Parameters parameters, string? id = null, bool useGet = false)
    {
        return _cache.GetOrCreate<Task<Parameters>>((parameters.GetHashCode(), id), entry =>
        {
            entry.SetOptions(_entryOptions);
            return terminologyService.Subsumes(parameters, id, useGet);
        })!;
    }
    
    public Task<Parameters> CodeSystemValidateCode(Parameters parameters, string? id = null, bool useGet = false)
    {
        return _cache.GetOrCreate<Task<Parameters>>((parameters.GetHashCode(), id), entry =>
        {
            entry.SetOptions(_entryOptions);
            return terminologyService.CodeSystemValidateCode(parameters, id, useGet);
        })!;
    }

    public Task<Resource> Expand(Parameters parameters, string? id = null, bool useGet = false)
    {
        return _cache.GetOrCreate<Task<Resource>>((parameters.GetHashCode(), id), entry =>
        {
            entry.SetOptions(_entryOptions);
            return terminologyService.Expand(parameters, id, useGet);
        })!;
    }
    
    public Task<Parameters> Translate(Parameters parameters, string? id = null, bool useGet = false)
    {
        return _cache.GetOrCreate<Task<Parameters>>((parameters.GetHashCode(), id), entry =>
        {
            entry.SetOptions(_entryOptions);
            return terminologyService.Translate(parameters, id, useGet);
        })!;
    }
    
    public Task<Resource> Closure(Parameters parameters, bool useGet = false)
    {
        return _cache.GetOrCreate<Task<Resource>>(parameters.GetHashCode(), entry =>
        {
            entry.SetOptions(_entryOptions);
            return terminologyService.Closure(parameters, useGet);
        })!;
    }
}