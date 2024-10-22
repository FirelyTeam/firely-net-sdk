#if true

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using P=Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Model;


/// <summary>
/// An element within a tree of typed FHIR data with also a parent element.
/// </summary>
/// <remarks>
/// This interface represents FHIR data as a tree of elements, including type information either present in
/// the instance or derived from fully aware of the FHIR definitions and types
/// </remarks>
#pragma warning disable CS0618 // Type or member is obsolete
public interface IScopedNode : ITypedElement, IShortPathGenerator
#pragma warning restore CS0618 // Type or member is obsolete
{
    /// <summary>
    /// The parent node of this node, or null if this is the root node.
    /// </summary>
    IScopedNode? Parent { get; }
}

internal record ScopeInformation(IScopedNode? Parent, string Name, int? Index);


public abstract partial class Base : IScopedNode,
    IFhirValueProvider, IResourceTypeSupplier
{
    // we set name to null by default, but it can never be null upon accessing it, as the setter will initialize it.
    [NonSerialized] private ScopeInformation? _scopeInfo;
    
    private ScopeInformation ScopeInfo
    {
        get => _scopeInfo ?? BuildRoot();
        set => _scopeInfo = value;
    } 

    internal ScopeInformation BuildRoot(string? rootName = null) => new(null, rootName ?? TypeName, null);
    
    internal Base WithScopeInfo(ScopeInformation info)
    {
        this.ScopeInfo = info;
        return this;
    }

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
        
    IScopedNode? IScopedNode.Parent => ScopeInfo.Parent;

    string ITypedElement.Name => ScopeInfo.Name;

    // TODO:
    //   Als wij een BackboneElement zijn, dan is onze naam niet this.TypeName maar "BackboneElement" of
    //   "Element", afhankelijk van waar hij in de .net inheritance hierarchie zit.
    //   HEt moet "code" zijn als dit een "Code<T>" is. Dat zijn geloof ik de afwijkingen.
    //   Wellioht is er ook nog iets met de directe properties "Extension.url" en "Element.id" die van een
    //   system type zijn ipv een FHIR type.
    
    [TemporarilyChanged] // TODO: This is a temporary change to make the tests pass. This should be removed. We are not planning to implement ITE.
    string? ITypedElement.InstanceType => 
        ((IStructureDefinitionSummary)
            ModelInspector
                .ForType(this.GetType())
                .FindOrImportClassMapping(this.GetType())!
        ).TypeName;

    private object? _value;
    private object? _lastCachedValue;


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
            ({} idx, {} parent) => $"{parent.Location}.{ScopeInfo.Name}[{idx}]",
            // if we do not, write 0 as idx
            (_, {} parent) => $"{parent.Location}.{ScopeInfo.Name}[0]",
            // if we have neither, we are the root.
            _ => $"{ScopeInfo.Name}"
        };

    IElementDefinitionSummary? ITypedElement.Definition => null;

    string IShortPathGenerator.ShortPath => 
        (ScopeInfo.Index, ScopeInfo.Parent) switch
        {
            // if we have an index, we have a parent.
            ({ } idx, {} parent) => $"{parent.ShortPath}.{ScopeInfo.Name}[{idx}]",
            // Note that we omit indices here.
            (_, { } parent) => $"{parent.ShortPath}.{ScopeInfo.Name}",
            // if we have neither, we are the root. Note that we omit indices here.
            _ => ScopeInfo.Name
        };
    
    public Base FhirValue => this;

    string? IResourceTypeSupplier.ResourceType =>
        this is Resource
            ? ((ITypedElement)this).InstanceType
            : null;
}

#endif