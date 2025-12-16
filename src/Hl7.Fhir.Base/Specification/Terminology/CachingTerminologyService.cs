using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
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
        return parameters.GetParametersHashCode() is { } hash
            ? _cache.GetOrCreate<Task<Parameters>>(hash, entry =>
                {
                    entry.SetOptions(_entryOptions);
                    return _terminologyService.ValueSetValidateCode(parameters, id, useGet);
                })!
            : _terminologyService.ValueSetValidateCode(parameters, id, useGet);
    } 

    public Task<Parameters> Subsumes(Parameters parameters, string? id = null, bool useGet = false)
    {
        return parameters.GetParametersHashCode() is { } hash
            ? _cache.GetOrCreate<Task<Parameters>>(hash, entry =>
                {
                    entry.SetOptions(_entryOptions);
                    return _terminologyService.Subsumes(parameters, id, useGet);
                })!
            : _terminologyService.Subsumes(parameters, id, useGet);
    }
    
    public Task<Parameters> CodeSystemValidateCode(Parameters parameters, string? id = null, bool useGet = false)
    {
        return parameters.GetParametersHashCode() is { } hash
            ? _cache.GetOrCreate<Task<Parameters>>(hash, entry =>
                {
                    entry.SetOptions(_entryOptions);
                    return _terminologyService.CodeSystemValidateCode(parameters, id, useGet);
                })!
            : _terminologyService.CodeSystemValidateCode(parameters, id, useGet);
    }

    public Task<Parameters> Lookup(Parameters parameters, bool useGet = false)
    {
        return parameters.GetParametersHashCode() is { } hash
            ? _cache.GetOrCreate<Task<Parameters>>(hash, entry =>
            {
                entry.SetOptions(_entryOptions);
                return _terminologyService.Lookup(parameters, useGet);
            })!
            : _terminologyService.Lookup(parameters, useGet);
    }

    public Task<Resource> Expand(Parameters parameters, string? id = null, bool useGet = false)
    {
        return parameters.GetParametersHashCode() is { } hash
            ? _cache.GetOrCreate<Task<Resource>>(hash, entry =>
                {
                    entry.SetOptions(_entryOptions);
                    return _terminologyService.Expand(parameters, id, useGet);
                })!
            : _terminologyService.Expand(parameters, id, useGet);
    }
    
    public Task<Parameters> Translate(Parameters parameters, string? id = null, bool useGet = false)
    {
        return parameters.GetParametersHashCode() is { } hash
            ? _cache.GetOrCreate<Task<Parameters>>(hash, entry =>
                {
                    entry.SetOptions(_entryOptions);
                    return _terminologyService.Translate(parameters, id, useGet);
                })!
            : _terminologyService.Translate(parameters, id, useGet);
    }
    
    public Task<Resource> Closure(Parameters parameters, bool useGet = false)
    {
        return parameters.GetParametersHashCode() is { } hash 
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
    public static int? GetParametersHashCode(this Parameters parameters)
    {
        var hash = new HashCode();
        foreach (var parameter in parameters.Parameter)
        {
            if (parameter.getPartHashCode() is { } hashPart)
                hash.Add(hashPart);
            else
                return null;
        }
        return hash.ToHashCode();
    }
    
    private static int? getPartHashCode(this Parameters.ParameterComponent part)
    {
        var hash = new HashCode();
        hash.Add(part.Name);
        if(part.Value != null)
            hash.Add(getTerminologyValueHashCode(part.Value));
        if(part.Resource != null)
            return null;
        foreach (var subpart in part.Part)
        {
            if(subpart.getPartHashCode() is { } subpartHash)
                hash.Add(subpartHash);
            else
                return null;
        }
        return hash.ToHashCode();
    }

    private static int? getTerminologyValueHashCode(DataType parameterValue)
    {
        var hash = new HashCode();
        switch (parameterValue)
        {
            case Canonical canonical:
                hash.Add(canonical.Value);
                break;
            case Code code:
                hash.Add(code.Value);
                break;
            case FhirBoolean boolean:
                hash.Add(boolean.Value);
                break;
            case FhirString fhirString:
                hash.Add(fhirString.Value);
                break;
            case FhirUri uri:
                hash.Add(uri.Value);
                break;
            case Coding coding:
                hash.Add(coding.System);
                hash.Add(coding.Version);
                hash.Add(coding.Code);
                hash.Add(coding.Display);
                break;
            case CodeableConcept concept:
                hash.Add(concept.Text);
                foreach (var coding in concept.Coding)
                {
                    hash.Add(coding.System);
                    hash.Add(coding.Version);
                    hash.Add(coding.Code);
                    hash.Add(coding.Display);
                }
                break;
            case FhirDateTime dt:
                hash.Add(dt.Value);
                break;
            default:
                return null;
        }
        
        return hash.ToHashCode();
    }
}