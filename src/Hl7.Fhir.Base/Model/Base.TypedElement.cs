#if true

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Hl7.Fhir.Model;

public abstract partial class Base : ITypedElement, IExceptionSource, IShortPathGenerator,
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

    IEnumerable<ITypedElement> IBaseElementNavigator<ITypedElement>.Children(string? name)
    {
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

    string IBaseElementNavigator<ITypedElement>.Name => ElementNode.Name;

    string? IBaseElementNavigator<ITypedElement>.InstanceType => ElementNode.InstanceType;

    object? IBaseElementNavigator<ITypedElement>.Value => ElementNode.Value;

    string ITypedElement.Location => ElementNode.Location;

    IElementDefinitionSummary? ITypedElement.Definition => ElementNode.Definition;
    ExceptionNotificationHandler IExceptionSource.ExceptionHandler
    {
        get => ElementNode.ExceptionHandler;
        set => ElementNode.ExceptionHandler = value;
    }

    string IShortPathGenerator.ShortPath => ElementNode.ShortPath;

    Base IFhirValueProvider.FhirValue => ElementNode.FhirValue;

    string IResourceTypeSupplier.ResourceType => ElementNode.ResourceType;
}

#endif