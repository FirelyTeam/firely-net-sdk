/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.ElementModel;

public class ElementNode : DomNode<ElementNode>, ITypedElement, IShortPathGenerator
{
    /// <summary>
    /// Creates an implementation of ITypedElement that represents a primitive value
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    // HACK: For now, allow a Quantity (which is NOT a primitive) in the .Value property
    // of ITypedElement. This is a temporary situation to make a quick & dirty upgrade of
    // FP to Normative (with Quantity support) possible.
    public static ITypedElement ForPrimitive(object value)
    {
        return value switch
        {
            P.Quantity q => PrimitiveElement.ForQuantity(q),
            _ => new PrimitiveElement(value, useFullTypeName: true)
        };
    }

    /// <summary>
    /// Converts a .NET primitive to the expected object value to use in the
    /// value property of ITypedElement.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="primitiveValue"></param>
    /// <returns></returns>
    public static bool TryConvertToElementValue(object? value, [NotNullWhen(true)] out object? primitiveValue)
    {
        primitiveValue = conv();
        return primitiveValue is not null;

        object? conv()
        {
            // NOTE: Keep Any.TryConvertToSystemValue, TypeSpecifier.TryGetNativeType and TypeSpecifier.ForNativeType in sync
            switch (value)
            {
                case P.Any a:
                    return a;
                case bool b:
                    return b;
                case string s:
                    return s;
                case char c:
                    return new string(c, 1);
                case int _:
                case short _:
                case ushort _:
                case uint _:
                    return Convert.ToInt32(value);
                case long _:
                case ulong _:
                    return Convert.ToInt64(value);
                case DateTimeOffset dto:
                    return P.DateTime.FromDateTimeOffset(dto);
                case float _:
                case double _:
                case decimal _:
                    return Convert.ToDecimal(value);
                case Enum en:
                    return en.GetLiteral();
                case Uri u:
                    return u.OriginalString;
                default:
                    return null;
            }
        }
    }

    /// <summary>
    /// Create a fixed length set of values (but also support variable number of parameter values)
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static IEnumerable<ITypedElement> CreateList(params object[] values) =>
        values switch
        {
            null => EmptyList,
            [var one] => [toTe(one)!],
            _ => values.Select(toTe).ToList()!
        };

    /// <summary>
    /// Create a variable list of values using an enumeration
    /// - so doesn't have to be converted to an array in memory (issue with larger dynamic lists)
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static IEnumerable<ITypedElement> CreateList(IEnumerable<object> values) => values switch
    {
        null => EmptyList,
        _ => values.Select(toTe).ToList()!
    };

    private static ITypedElement? toTe(object? value) => value switch
    {
        null => null,
        ITypedElement element => element,
        _ => ForPrimitive(value)
    };


    public static readonly IEnumerable<ITypedElement> EmptyList = [];
    public IEnumerable<ITypedElement> Children(string? name = null) => ChildrenInternal(name);

    private ElementNode(string name, object? value, string? instanceType, IElementDefinitionSummary? definition)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        InstanceType = instanceType;
        Value = value;
        Definition = definition;
    }

    private IReadOnlyCollection<IElementDefinitionSummary> _childDefinitions = null!;

    private IReadOnlyCollection<IElementDefinitionSummary> getChildDefinitions(IStructureDefinitionSummaryProvider provider)
    {
        LazyInitializer.EnsureInitialized(ref _childDefinitions, () => this.ChildDefinitions(provider));

        return _childDefinitions;
    }

    public ElementNode Add(IStructureDefinitionSummaryProvider provider, ElementNode child, string? name = null)
    {
        if (provider == null) throw new ArgumentNullException(nameof(provider));
        if (child == null) throw new ArgumentNullException(nameof(child));

        importChild(provider, child, name);
        return child;
    }

    public ElementNode Add(IStructureDefinitionSummaryProvider provider, string name, object? value = null, string? instanceType = null)
    {
        if (provider == null) throw new ArgumentNullException(nameof(provider));
        if (name == null) throw new ArgumentNullException(nameof(name));

        var child = new ElementNode(name, value, instanceType, null);

        // Add() will supply the definition and the instanceType (if necessary)
        return Add(provider, child);
    }

    public void ReplaceWith(IStructureDefinitionSummaryProvider provider, ElementNode node)
    {
        if (provider == null) throw new ArgumentNullException(nameof(provider));
        if (node == null) throw new ArgumentNullException(nameof(node));

        if (Parent == null) throw Error.Argument("Current node is a root node and cannot be replaced.");
        Parent.Replace(provider, this, node);
    }

    public void Replace(IStructureDefinitionSummaryProvider provider, ElementNode oldChild, ElementNode newChild)
    {
        if (provider == null) throw new ArgumentNullException(nameof(provider));
        if (oldChild == null) throw new ArgumentNullException(nameof(oldChild));
        if (newChild == null) throw new ArgumentNullException(nameof(newChild));

        int childIndex = ChildList.IndexOf(oldChild);
        if (childIndex == -1) throw Error.Argument("Node to be replaced is not one of the children of this node");
        importChild(provider, newChild, oldChild.Name, childIndex);
        Remove(oldChild);
    }

    /// <summary>
    /// Will update the child to reflect it being a child of this element, but will not yet add the child at any position within this element
    /// </summary>
    private void importChild(IStructureDefinitionSummaryProvider provider, ElementNode child, string? name, int? position = null)
    {
        child.Name = name ?? child.Name;
        if (child.Name == null) throw Error.Argument($"The ElementNode given should have its Name property set or the '{nameof(name)}' parameter should be given.");

        // Remove this child from the current parent (if any), then reassign to me
        if (child.Parent != null) child.Parent.Remove(child);
        child.Parent = this;

        // If we add a child, we better overwrite it's definition with what
        // we think it should be - this way you can safely first create a node representing
        // an independently created root for a resource of datatype, and then add it to the tree.
        var childDefs = getChildDefinitions(provider ?? throw Error.ArgumentNull(nameof(provider)));
        var childDef = childDefs.SingleOrDefault(cd => cd.ElementName == child.Name);

        child.Definition = childDef ?? child.Definition;    // if we don't know about the definition, stick with the old one (if any)

        if (child.InstanceType == null && child.Definition != null)
        {
            if (child.Definition.IsResource || child.Definition.IsChoiceElement)
            {
                // Note that we just demand InstanceType to be set on any kind of choice, even if
                // some profile has limited the choice to a single type. Too hard to figure out
                // whether it actually allows more than one choice, since the single type might
                // also be abstract, and still allow choices.
                throw Error.Argument("The ElementNode given should have its InstanceType property set, since the element is a choice or resource.");

                // [EK20190822] This functionality has been removed since it heavily depends on knowledge about
                // FHIR types, it would automatically try to derive a *FHIR* type from the given child.Value,
                // however, this would not work correctly if the model used is something else than FHIR,
                // so this cannot be expected to work correctly in general, and I have chosen to remove
                // this.
                //// We are in a situation where we are on an polymorphic element, but the caller did not specify
                //// the instance type.  We can try to auto-set it by deriving it from the instance's type, if it is a primitive
                //if (child.Value != null && IsSupportedValue(child.Value))
                //    child.InstanceType = TypeSpecifier.ForNativeType(child.Value.GetType()).Name;
            }
            else
                child.InstanceType = child.Definition.Type.Single().GetTypeName();
        }

        if (position == null || position >= ChildList.Count)
            ChildList.Add(child);
        else
            ChildList.Insert(position.Value, child);

    }

    public static ElementNode Root(IStructureDefinitionSummaryProvider provider, string type, string? name = null, object? value = null)
    {
        if (provider == null) throw Error.ArgumentNull(nameof(provider));
        if (type == null) throw Error.ArgumentNull(nameof(type));

        var sd = provider.Provide(type);
        var definition = sd is not null ? ElementDefinitionSummary.ForRoot(sd) : null;

        return new ElementNode(name ?? type, value, type, definition);
    }

    public static ElementNode FromElement(ITypedElement node, bool recursive = true, IEnumerable<Type>? annotationsToCopy = null)
    {
        if (node == null) throw new ArgumentNullException(nameof(node));
        return buildNode(node, recursive, annotationsToCopy, null);
    }

    private static ElementNode buildNode(ITypedElement node, bool recursive, IEnumerable<Type>? annotationsToCopy, ElementNode? parent)
    {
        var me = new ElementNode(node.Name, node.Value, node.InstanceType, node.Definition)
        {
            Parent = parent
        };

        foreach (var t in annotationsToCopy ?? [])
        foreach (var ann in node.Annotations(t))
            me.AddAnnotation(ann);

        if (recursive)
            me.ChildList.AddRange(node.Children().Select(c => buildNode(c, recursive: true, annotationsToCopy: annotationsToCopy, me)));

        return me;
    }

    public bool Remove(ElementNode child)
    {
        if (child == null) throw new ArgumentNullException(nameof(child));

        var success = ChildList.Remove(child);
        if (success) child.Parent = null;

        return success;
    }

    public ElementNode ShallowCopy()
    {
        var copy = new ElementNode(Name, Value, InstanceType, Definition)
        {
            Parent = Parent,
            ChildList = ChildList
        };

        if (HasAnnotations)
            copy.AnnotationsInternal.AddRange(AnnotationsInternal);

        return copy;
    }

    public IElementDefinitionSummary? Definition { get; private set; }

    public string? InstanceType { get; private set; }

    public object? Value { get; set; }

    public override IEnumerable<object> Annotations(Type type)
    {
        if (type == null) throw new ArgumentNullException(nameof(type));

        return (type == typeof(ElementNode) || type == typeof(ITypedElement) || type == typeof(IShortPathGenerator))
            ? [this] : base.Annotations(type);
    }

    public string Location
    {
        get
        {
            if (Parent != null)
            {
                //TODO: Slow - but since we'll change the use of this property to informational
                //(i.e. for error messages), it may not be necessary to improve it.
                var basePath = Parent.Location;
                var myIndex = Parent.ChildList.Where(c => c.Name == Name).ToList().IndexOf(this);
                return $"{basePath}.{Name}[{myIndex}]";

            }
            else
                return Name;
        }
    }

    public string ShortPath
    {
        get
        {
            if (Parent == null) return Name;

            //TODO: Slow - but since we'll change the use of this property to informational
            //(i.e. for error messages), it may not be necessary to improve it.
            var basePath = Parent.ShortPath;

            if (Definition?.IsCollection == false)
                return $"{basePath}.{Name}";

            var myIndex = Parent.ChildList.Where(c => c.Name == Name).ToList().IndexOf(this);
            return $"{basePath}.{Name}[{myIndex}]";
        }
    }
}