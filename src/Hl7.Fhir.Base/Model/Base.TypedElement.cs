#if true

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Runtime.CompilerServices;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Model;

internal record ScopeInformation(IScopedNode? Parent, string Name, int? Index);

public abstract partial class Base : IScopedNode,
    IFhirValueProvider, IResourceTypeSupplier
{
    private object? _value;
    private object? _lastCachedValue;

    public Base FhirValue => this;

    internal object? ToITypedElementValue()
    {
        try
        {
            return this switch
            {
                Instant { Value: { } ins } => P.DateTime.FromDateTimeOffset(ins),
                Time { Value: { } time } => P.Time.Parse(time),
                Date { Value: { } dt } => P.Date.Parse(dt),
                FhirDateTime { Value: { } fdt } => P.DateTime.Parse(fdt),
                Integer fint => fint.Value,
                Integer64 fint64 => fint64.Value,
                PositiveInt pint => pint.Value,
                UnsignedInt unsint => unsint.Value,
                Base64Binary { Value: { } b64 } => PrimitiveTypeConverter.ConvertTo<string>(b64),
                PrimitiveType prim => prim.ObjectValue,
                _ => null
            };
        }
        catch (FormatException)
        {
            // If it fails, just return the unparsed contents
            return (this as PrimitiveType)?.ObjectValue;
        }
    }

    string? IResourceTypeSupplier.ResourceType =>
        this is Resource
            ? ((ITypedElement)this).InstanceType
            : null;

    string IShortPathGenerator.ShortPath =>
        (ScopeInfo.Index, ScopeInfo.Parent) switch
        {
            // if we have an index, we have a parent.
            ({ } idx, { } parent) => $"{parent.ShortPath}.{ScopeInfo.Name}[{idx}]",
            // Note that we omit indices here.
            (_, { } parent) => $"{parent.ShortPath}.{ScopeInfo.Name}",
            // if we have neither, we are the root. Note that we omit indices here.
            _ => ScopeInfo.Name
        };

    #region ScopeInformation

    [NonSerialized] private ScopeInformation? _scopeInfo;

    private ScopeInformation ScopeInfo
    {
        get => LazyInitializer.EnsureInitialized(ref _scopeInfo, () => BuildRoot())!;
        set => _scopeInfo = value;
    } 

    internal ScopeInformation BuildRoot(string? rootName = null) => new(null, rootName ?? TypeName, null);
    
    internal Base WithScopeInfo(ScopeInformation info)
    {
        this.ScopeInfo = info;
        return this;
    }

    #endregion

    #region ITypedElement

    IEnumerable<ITypedElement> ITypedElement.Children(string? name) =>
        this.GetElementPairs()
            .Where(ep => (name == null || name == ep.Key))
            .SelectMany<KeyValuePair<string, object>, Base>(ep => 
                (ep.Key, ep.Value) switch
                {
                    (_, Base b) => (IEnumerable<Base>)[b.WithScopeInfo(new ScopeInformation(this, ep.Key, null))],
                    (_, IEnumerable<Base> list) => list.Select((item, idx) => item.WithScopeInfo(new ScopeInformation(this, ep.Key, idx))),
                    ("url", string s) when this is Extension => [new FhirUri(s).WithScopeInfo(new ScopeInformation(this, ep.Key, null))],
                    ("id", string s) when this is Element => [new FhirString(s).WithScopeInfo(new ScopeInformation(this, ep.Key, null))],
                    ("value", _) => [],
                    _ => throw new InvalidOperationException("Unexpected system primitive in child list")
                }
            );

    string ITypedElement.Name => ScopeInfo.Name;
    
    [TemporarilyChanged] // TODO: This is a temporary change to make the tests pass. This should be removed. We are not planning to implement ITE.
    string? ITypedElement.InstanceType => 
        ((IStructureDefinitionSummary)
            ModelInspector
                .ForType(this.GetType())
                .FindOrImportClassMapping(this.GetType())!
        ).TypeName;

    object? ITypedElement.Value
    {
        get
        {
            if (this is not PrimitiveType { ObjectValue: { } ov }) return null;
            if (ov == _lastCachedValue) return _value;
            _value = ToITypedElementValue();
            _lastCachedValue = ov;

            return _value;
        }
    }

    string ITypedElement.Location =>
        (ScopeInfo.Index, ScopeInfo.Parent) switch
        {
            // if we have an index, write it
            ({ } idx, { } parent) => $"{parent.Location}.{ScopeInfo.Name}[{idx}]",
            // if we do not, write 0 as idx
            (_, { } parent) => $"{parent.Location}.{ScopeInfo.Name}[0]",
            // if we have neither, we are the root.
            _ => $"{ScopeInfo.Name}"
        };

    bool IScopedNode.TryResolveBundleEntry(string fullUrl, [NotNullWhen(true)] out IScopedNode? result)
    {
        result = this is Bundle b ? b.Entry.FirstOrDefault(entry => entry.FullUrl == fullUrl) : null;
        return result is not null;
    }

    bool IScopedNode.TryResolveContainedEntry(string id, [NotNullWhen(true)] out IScopedNode? result)
    {
        result = this is DomainResource dr ? dr.Contained.FirstOrDefault(contained => contained.Id == id) : null;
        return result is not null;
    }

    IElementDefinitionSummary? ITypedElement.Definition => null;

    #endregion

    #region IScopedNode

    string IScopedNode.Name => ScopeInfo.Name;

    NodeType IScopedNode.Type =>
        this switch
        {
            Bundle => NodeType.Bundle | NodeType.Resource,
            PrimitiveType => NodeType.Primitive,
            DomainResource => NodeType.DomainResource | NodeType.Resource,
            Resource => NodeType.Resource,
            ResourceReference or Canonical or CodeableReference => NodeType.Reference,
            _ => 0
        };
    
    object? IScopedNode.Value
    {
        get
        {
            if (this is not PrimitiveType { ObjectValue: { } ov }) return null;
            if (ov == _lastCachedValue) return _value;
            _value = ToITypedElementValue();
            _lastCachedValue = ov;

            return _value;
        }
    }

    string IScopedNode.Location =>
        (ScopeInfo.Index, ScopeInfo.Parent) switch
        {
            // if we have an index, write it
            ({ } idx, { } parent) => $"{parent.Location}.{ScopeInfo.Name}[{idx}]",
            // if we do not, write 0 as idx
            (_, { } parent) => $"{parent.Location}.{ScopeInfo.Name}[0]",
            // if we have neither, we are the root.
            _ => $"{ScopeInfo.Name}"
        };

    IScopedNode? IScopedNode.Parent => ScopeInfo.Parent;

    IEnumerable<IScopedNode> IScopedNode.Children(string? name) => this.GetElementPairs()
        .Where(ep => (name == null || name == ep.Key))
        .SelectMany<KeyValuePair<string, object>, Base>(ep =>
            (ep.Key, ep.Value) switch
            {
                (_, Base b) => (IEnumerable<Base>)[b.WithScopeInfo(new ScopeInformation(this, ep.Key, null))],
                (_, IEnumerable<Base> list) => list.Select((item, idx) => item.WithScopeInfo(new ScopeInformation(this, ep.Key, idx))),
                ("url", string s) when this is Extension => [new FhirUri(s).WithScopeInfo(new ScopeInformation(this, ep.Key, null))],
                ("id", string s) when this is Element => [new FhirString(s).WithScopeInfo(new ScopeInformation(this, ep.Key, null))],
                ("value", _) => [],
                _ => throw new InvalidOperationException("Unexpected system primitive in child list")
            }
        );

    #endregion
}

#endif