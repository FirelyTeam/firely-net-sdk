using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

#nullable enable

namespace Hl7.Fhir.Specification.Terminology;

public class CachingTerminologyService : ITerminologyService
{
    public CachingTerminologyService(ITerminologyService terminologyService, IMemoryCache cache, MemoryCacheEntryOptions? entryOptions = null)
    {
        _terminologyService = terminologyService;
        _entryOptions = entryOptions ?? DEFAULT_ENTRY_OPTIONS;
        _cache = cache;
    }
    
    public CachingTerminologyService(ITerminologyService terminologyService, MemoryCacheOptions? cacheOptions = null, MemoryCacheEntryOptions? entryOptions = null)
    {
        _terminologyService = terminologyService;
        _entryOptions = entryOptions ?? DEFAULT_ENTRY_OPTIONS;
        _cache = new MemoryCache(cacheOptions ?? DEFAULT_CACHE_OPTIONS);
    }

    private static readonly MemoryCacheOptions DEFAULT_CACHE_OPTIONS = new MemoryCacheOptions()
    {
        SizeLimit = 1024 // Limit cache to 1024 entries
    };
    
    private static readonly MemoryCacheEntryOptions DEFAULT_ENTRY_OPTIONS = new MemoryCacheEntryOptions()
    {
        SlidingExpiration = TimeSpan.FromMinutes(30), Size = 1 // Each entry is counted as size 1
    };
    
    private readonly ITerminologyService _terminologyService;
    
    private readonly IMemoryCache _cache;

    private readonly MemoryCacheEntryOptions _entryOptions;

    public Task<Parameters> ValueSetValidateCode(Parameters parameters, string? id = null, bool useGet = false)
    {
        return parameters.GetParameterComponentHashCode() is { } hash
            ? _cache.GetOrCreate<Task<Parameters>>(hash, entry =>
                {
                    entry.SetOptions(_entryOptions);
                    return _terminologyService.ValueSetValidateCode(parameters, id, useGet);
                })!
            : _terminologyService.ValueSetValidateCode(parameters, id, useGet);
    } 

    public Task<Parameters> Subsumes(Parameters parameters, string? id = null, bool useGet = false)
    {
        return parameters.GetParameterComponentHashCode() is { } hash
            ? _cache.GetOrCreate<Task<Parameters>>(hash, entry =>
                {
                    entry.SetOptions(_entryOptions);
                    return _terminologyService.Subsumes(parameters, id, useGet);
                })!
            : _terminologyService.Subsumes(parameters, id, useGet);
    }
    
    public Task<Parameters> CodeSystemValidateCode(Parameters parameters, string? id = null, bool useGet = false)
    {
        return parameters.GetParameterComponentHashCode() is { } hash
            ? _cache.GetOrCreate<Task<Parameters>>(hash, entry =>
                {
                    entry.SetOptions(_entryOptions);
                    return _terminologyService.CodeSystemValidateCode(parameters, id, useGet);
                })!
            : _terminologyService.CodeSystemValidateCode(parameters, id, useGet);
    }

    public Task<Resource> Expand(Parameters parameters, string? id = null, bool useGet = false)
    {
        return parameters.GetParameterComponentHashCode() is { } hash
            ? _cache.GetOrCreate<Task<Resource>>(hash, entry =>
                {
                    entry.SetOptions(_entryOptions);
                    return _terminologyService.Expand(parameters, id, useGet);
                })!
            : _terminologyService.Expand(parameters, id, useGet);
    }
    
    public Task<Parameters> Translate(Parameters parameters, string? id = null, bool useGet = false)
    {
        return parameters.GetParameterComponentHashCode() is { } hash
            ? _cache.GetOrCreate<Task<Parameters>>(hash, entry =>
                {
                    entry.SetOptions(_entryOptions);
                    return _terminologyService.Translate(parameters, id, useGet);
                })!
            : _terminologyService.Translate(parameters, id, useGet);
    }
    
    public Task<Resource> Closure(Parameters parameters, bool useGet = false)
    {
        return parameters.GetParameterComponentHashCode() is { } hash 
            ? _cache.GetOrCreate<Task<Resource>>(hash, entry =>
                {
                    entry.SetOptions(_entryOptions);
                    return _terminologyService.Closure(parameters, useGet);
                })!
            : _terminologyService.Closure(parameters, useGet);
    }
    
}

internal static class ParametersExtensions
{
    // returns null on resource parameters to avoid caching those. these are too complex for now.
    public static int? GetParameterComponentHashCode(this Parameters parameters)
    {
        var hash = new HashCode();
        foreach (var parameter in parameters.Parameter)
        {
            hash.Add(parameter.Name);
            if (parameter.Value != null)
                hash.Add(parameter.Value.GetHashCode());
            if (parameter.Resource != null)
                return null;
            foreach (var part in parameter.Part)
            {
                hash.Add(part.Name);
                if (part.Value != null)
                    hash.Add(part.Value.GetHashCode());
                if (part.Resource != null)
                    return null;
            }
        }
        return hash.ToHashCode();
    }
}

