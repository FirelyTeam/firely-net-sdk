#if true

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Runtime.CompilerServices;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Model;

internal record ScopeInformation(IScopedNode? Parent, string Name, int? Index);

public abstract partial class Base
{

    // #region ScopeInformation
    //
    // [NonSerialized] private ScopeInformation? _scopeInfo;
    //
    // private ScopeInformation ScopeInfo
    // {
    //     get => LazyInitializer.EnsureInitialized(ref _scopeInfo, () => BuildRoot())!;
    //     set => _scopeInfo = value;
    // }
    //
    // internal ScopeInformation BuildRoot(string? rootName = null) => new(null, rootName ?? TypeName, null);
    //
    // internal Base WithScopeInfo(ScopeInformation info)
    // {
    //     this.ScopeInfo = info;
    //     return this;
    // }
    //
    // #endregion
    //
    //#region ITypedElement
    //
    // IEnumerable<ITypedElement> ITypedElement.Children(string? name) =>
    //     this.GetElementPairs()
    //         .Where(ep => (name == null || name == ep.Key))
    //         .SelectMany<KeyValuePair<string, object>, Base>(ep =>
    //             (ep.Key, ep.Value) switch
    //             {
    //                 (_, Base b) => (IEnumerable<Base>) [b.WithScopeInfo(new ScopeInformation(this, ep.Key, null))],
    //                 (_, IEnumerable<Base> list) => list.Select((item, idx) =>
    //                     item.WithScopeInfo(new ScopeInformation(this, ep.Key, idx))),
    //                 ("url", string s) when this is Extension =>
    //                     [new FhirUri(s).WithScopeInfo(new ScopeInformation(this, ep.Key, null))],
    //                 ("id", string s) when this is Element =>
    //                     [new FhirString(s).WithScopeInfo(new ScopeInformation(this, ep.Key, null))],
    //                 ("value", _) => [],
    //                 _ => throw new InvalidOperationException("Unexpected system primitive in child list")
    //             }
    //         );
    //
    // string ITypedElement.Name => ScopeInfo.Name;
    //
    // string ITypedElement.InstanceType
    // {
    //     get
    //     {
    //         if (this is BackboneElement)
    //             return "BackboneElement";
    //
    //         if (this is Element && TypeName.Contains('.'))
    //             return "Element";
    //
    //         return ScopeInfo switch
    //         {
    //             { Parent: Extension, Name: "url" } => "uri",
    //             { Parent: Element, Name: "id" } => "string",
    //             _ => TypeName
    //         };
    //     }
    // }
    //
    // object? ObjectValue
    // {
    //     get
    //     {
    //         if (this is not PrimitiveType { ObjectValue: { } ov }) return null;
    //         if (ov == _lastCachedValue) return _value;
    //         _value = ToITypedElementValue();
    //         _lastCachedValue = ov;
    //
    //         return _value;
    //     }
    // }
    //
    // string ITypedElement.Location =>
    //     (ScopeInfo.Index, ScopeInfo.Parent) switch
    //     {
    //         // if we have an index, write it
    //         ({ } idx, { } parent) => $"{parent.Location}.{ScopeInfo.Name}[{idx}]",
    //         // if we do not, write 0 as idx
    //         (_, { } parent) => $"{parent.Location}.{ScopeInfo.Name}[0]",
    //         // if we have neither, we are the root.
    //         _ => $"{ScopeInfo.Name}"
    //     };
    //
    // [TemporarilyChanged] // We need to use Children for now to preserve scope access, but we would really prefer poco-accesses here. When we refactor, we should change this back.
    // bool IScopedNode.TryResolveBundleEntry(string fullUrl, [NotNullWhen(true)] out IScopedNode? result)
    // {
    //     result = this is Bundle b ? (b as IScopedNode)
    //         .Children("entry").FirstOrDefault(entry => entry.Children("fullUrl")
    //             .SingleOrDefault()?.Value is string url && url == fullUrl)?
    //         .Children("resource").SingleOrDefault() : null;
    //     return result is not null;
    // }
    //
    // [TemporarilyChanged] // We need to use Children for now to preserve scope access, but we would really prefer poco-accesses here. When we refactor, we should change this back.
    // bool IScopedNode.TryResolveContainedEntry(string id, [NotNullWhen(true)] out IScopedNode? result)
    // {
    //     result = this is DomainResource dr ? (dr as IScopedNode).Children("contained").FirstOrDefault(contained => contained.Children("id").SingleOrDefault()?.Value is string containedId && $"#{containedId}" == id) : null;
    //     return result is not null;
    // }
    //
    // IElementDefinitionSummary? ITypedElement.Definition => null;
    //
    // #endregion
    //
    // #region IScopedNode
    //
    // string IScopedNode.Name => ScopeInfo.Name;
    //
    // NodeType IScopedNode.Type =>
    //     this switch
    //     {
    //         Bundle => NodeType.Bundle | NodeType.Resource,
    //         PrimitiveType => NodeType.Primitive,
    //         DomainResource => NodeType.DomainResource | NodeType.Resource,
    //         Resource => NodeType.Resource,
    //         ResourceReference or Canonical or CodeableReference => NodeType.Reference,
    //         Quantity => NodeType.Quantity,
    //         _ => 0
    //     };
    //
    // object? IScopedNode.Value
    // {
    //     get
    //     {
    //         if (this is not PrimitiveType { ObjectValue: { } ov }) return null;
    //         if (ov == _lastCachedValue) return _value;
    //         _value = ToITypedElementValue();
    //         _lastCachedValue = ov;
    //
    //         return _value;
    //     }
    // }
    //
    // string IScopedNode.Location =>
    //     (ScopeInfo.Index, ScopeInfo.Parent) switch
    //     {
    //         // if we have an index, write it
    //         ({ } idx, { } parent) => $"{parent.Location}.{ScopeInfo.Name}[{idx}]",
    //         // if we do not, write 0 as idx
    //         (_, { } parent) => $"{parent.Location}.{ScopeInfo.Name}[0]",
    //         // if we have neither, we are the root.
    //         _ => $"{ScopeInfo.Name}"
    //     };
    //
    // IScopedNode? IScopedNode.Parent => ScopeInfo.Parent;
    //
    // IEnumerable<IScopedNode> IScopedNode.Children(string? name) => this.GetElementPairs()
    //     .Where(ep => (name == null || name == ep.Key))
    //     .SelectMany<KeyValuePair<string, object>, Base>(ep =>
    //         (ep.Key, ep.Value) switch
    //         {
    //             (_, Base b) => (IEnumerable<Base>)[b.WithScopeInfo(new ScopeInformation(this, ep.Key, null))],
    //             (_, IEnumerable<Base> list) => list.Select((item, idx) => item.WithScopeInfo(new ScopeInformation(this, ep.Key, idx))),
    //             ("url", string s) when this is Extension => [new FhirUri(s).WithScopeInfo(new ScopeInformation(this, ep.Key, null))],
    //             ("id", string s) when this is Element => [new FhirString(s).WithScopeInfo(new ScopeInformation(this, ep.Key, null))],
    //             ("value", _) => [],
    //             _ => throw new InvalidOperationException("Unexpected system primitive in child list")
    //         }
    //     );
    //
    // #endregion
}

#endif