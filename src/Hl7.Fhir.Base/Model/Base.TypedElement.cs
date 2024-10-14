#if true

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
public interface IScopedNode : IBaseElementNavigator<IScopedNode>
#pragma warning restore CS0618 // Type or member is obsolete
{
    /// <summary>
    /// The parent node of this node, or null if this is the root node.
    /// </summary>
    IScopedNode? Parent { get; }   // We don't need this probably.

    /// <summary>
    /// An indication of the location of this node within the data represented by the <c>ITypedElement</c>.
    /// </summary>
    /// <remarks>The format of the location is the dotted name of the property, including indices to make
    /// sure repeated occurrences of an element can be distinguished. It needs to be sufficiently precise to aid
    /// the user in locating issues in the data.</remarks>
    string Location { get; }
}



public abstract partial class Base : IScopedNode, IShortPathGenerator,
    IFhirValueProvider, IResourceTypeSupplier
{
    [NonSerialized]
    private PocoElementNode? _elementNode = null;

    internal Base ForTypedElement(ModelInspector inspector, string? rootName = null)
    {
        ElementNode = new PocoElementNode(inspector, this, rootName);
        return this;
    }

    private PocoElementNode buildRootElementNode() => ForTypedElement(
        ModelInspector.ForType(this.GetType())).ElementNode;

    private PocoElementNode ElementNode
    {
        get => LazyInitializer.EnsureInitialized(ref _elementNode, buildRootElementNode)!;

        set => _elementNode = value;
    }

    IEnumerable<IScopedNode> IBaseElementNavigator<IScopedNode>.Children(string? name)
    {
        // TODO:  Base.Children gebruiken om ITypedElement.Children() te implementeren.
        // ITypedElement stopt bij PrimitiveType, terwijl Children() verder gaat (er is dus ook een kind dat
        // value heet in IReadOnlyDict, maar dat bestaat niet in ITYpedElement.    ITypedElement.Value vervult deze rol,
        // dus het "value" kind mogen we nog niet doorgeven.
        // We're also going to set Name, Parent and Index (as private fields)
        // var elements = this.GetElementPairs();  Zie verder PocoElementNode, want we moeten ook filteren
        // op naam.
        var elements = ElementNode.Children(name).Cast<PocoElementNode>();
        foreach (var element in elements)
        {
            if (element.Current is not null)
            {
                element.Current.ElementNode = element;
                yield return element.Current;
            }
            else
            {
                // Mmmmm....there is something like a "null" ITypedElement,
                // but we don't have a "null" POCO.
                yield return element;
            }
        }
    }

    string IBaseElementNavigator<IScopedNode>.Name => ElementNode.Name;

    // TODO:
    //   Als wij een BackboneElement zijn, dan is onze naam niet this.TypeName maar "BackboneElement" of
    //   "Element", afhankelijk van waar hij in de .net inheritance hierarchie zit.
    //   HEt moet "code" zijn als dit een "Code<T>" is. Dat zijn geloof ik de afwijkingen.
    //   Wellioht is er ook nog iets met de directe properties "Extension.url" en "Element.id" die van een
    //   system type zijn ipv een FHIR type.
    string? IBaseElementNavigator<IScopedNode>.InstanceType => ElementNode.InstanceType;

    private object? _value = null;
    private object? _lastCachedValue = null;


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

    object? IBaseElementNavigator<IScopedNode>.Value
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

    string IScopedNode.Location => ElementNode.Location;

    string IShortPathGenerator.ShortPath => ElementNode.ShortPath;

    Base IFhirValueProvider.FhirValue => ElementNode.FhirValue;

    string IResourceTypeSupplier.ResourceType => ElementNode.ResourceType;
}

#endif