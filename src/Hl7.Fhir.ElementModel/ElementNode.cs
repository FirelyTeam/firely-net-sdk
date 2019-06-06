/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Specification;
using Hl7.Fhir.Support.Model;
using Hl7.Fhir.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Hl7.Fhir.ElementModel
{
    public class ElementNode : DomNode<ElementNode>, ITypedElement, IAnnotated, IAnnotatable
    {
        /// <summary>
        /// Creates an implementation of ITypedElement that represents a primitive value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ITypedElement ForPrimitive(object value) => new PrimitiveElement(value);

        /// <summary>
        /// Create a fixed length set of values (but also support variable number of parameter values)
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static IEnumerable<ITypedElement> CreateList(params object[] values) => values != null
                ? values.Select(value => value == null ? null : value is ITypedElement ? (ITypedElement)value : ForPrimitive(value))
                : EmptyList;

        /// <summary>
        /// Create a variable list of values using an enumeration
        /// - so doesn't have to be converted to an array in memory (issue with larger dynamic lists)
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static IEnumerable<ITypedElement> CreateList(IEnumerable<object> values) => values != null
                ? values.Select(value => value == null ? null : value is ITypedElement ? (ITypedElement)value : ForPrimitive(value))
                : EmptyList;

        public static readonly IEnumerable<ITypedElement> EmptyList = Enumerable.Empty<ITypedElement>();
        public IEnumerable<ITypedElement> Children(string name = null) => ChildrenInternal(name);

        internal ElementNode(string name, object value, string instanceType, IElementDefinitionSummary definition)
        {
            Name = name;
            InstanceType = instanceType;
            Value = value;
            Definition = definition;
        }

        private IReadOnlyCollection<IElementDefinitionSummary> _childDefinitions = null;

        private IReadOnlyCollection<IElementDefinitionSummary> getChildDefinitions(IStructureDefinitionSummaryProvider provider)
        {
            LazyInitializer.EnsureInitialized(ref _childDefinitions, () => this.ChildDefinitions(provider));

            return _childDefinitions;
        }

        public ElementNode Add(IStructureDefinitionSummaryProvider provider, ElementNode child)
        {
            if (child.Name == null) throw Error.Argument($"The ElementNode given should have its Name property set.");

            child.Parent = this;

            // If we add a child, we better overwrite it's definition with what
            // we think it should be - this way you can safely first create a node representing
            // an independently created root for a resource of datatype, and then add it to the tree.
            var childDefs = getChildDefinitions(provider ?? throw Error.ArgumentNull(nameof(provider)));
            var childDef = childDefs.Where(cd => cd.ElementName == child.Name).SingleOrDefault();

            child.Definition = childDef ?? child.Definition;    // if we don't know about the definition, stick with the old one (if any)
            
            if(child.InstanceType == null && child.Definition != null)
            {
                if (child.Definition.IsResource || child.Definition.Type.Length > 1)
                {
                    // We are in a situation where we are on an polymorphic element, but the caller did not specify
                    // the instance type.  We can try to auto-set it by deriving it from the instance's type, if it is a primitive
                    if (child.Value != null && Primitives.TryGetPrimitiveTypeName(child.Value, out string instanceType))
                        child.InstanceType = instanceType;
                    else
                        throw Error.Argument("The ElementNode given should have its InstanceType property set, since the element is a choice or resource.");
                }
                else
                    child.InstanceType = child.Definition.Type.Single().GetTypeName();
            }

            ChildList.Add(child);

            return child;
        }

        public ElementNode Add(IStructureDefinitionSummaryProvider provider, string name, object value=null, string instanceType = null)
        {
            var child = new ElementNode(name, value, instanceType, null);

            // Add() will supply the definition and the instanceType (if necessary)
            return Add(provider, child); 
        }

        public static ElementNode Root(IStructureDefinitionSummaryProvider provider, string type, string name=null)
        {
            if (provider == null) throw Error.ArgumentNull(nameof(provider));
            if (type == null) throw Error.ArgumentNull(nameof(type));

            var sd = provider.Provide(type);
            IElementDefinitionSummary definition = null;

            // Should we throw if type is not found?
            if (sd != null)
                definition = ElementDefinitionSummary.ForRoot(sd);

            return new ElementNode(name ?? type, null, type, definition);
        }

        public static ElementNode FromElement(ITypedElement node, bool recursive = true, IEnumerable<Type> annotationsToCopy = null)
            => buildNode(node, recursive, annotationsToCopy, null);

        private static ElementNode buildNode(ITypedElement node, bool recursive, IEnumerable<Type> annotationsToCopy, ElementNode parent)
        {
            var me = new ElementNode(node.Name, node.Value, node.InstanceType, node.Definition)
            {
                Parent = parent
            };

            foreach (var t in annotationsToCopy ?? Enumerable.Empty<Type>())
                foreach (var ann in node.Annotations(t))
                    me.AddAnnotation(ann);

            if (recursive)
                me.ChildList.AddRange(node.Children().Select(c => buildNode(c, recursive: true, annotationsToCopy: annotationsToCopy, me)));

            return me;
        }

        public ElementNode Clone()
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

        public IElementDefinitionSummary Definition { get; private set; }

        public string InstanceType { get; private set; }

        public object Value { get; set; }

        public IEnumerable<object> Annotations(Type type)
        {
            return (type == typeof(ElementNode) || type == typeof(ITypedElement))
                ? (new[] { this })
                : AnnotationsInternal.OfType(type);
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

                    if (Definition?.IsCollection == false)
                        return $"{basePath}.{Name}";
                    else
                    {
                        var myIndex = Parent.ChildList.Where(c => c.Name == Name).ToList().IndexOf(this);
                        return $"{basePath}.{Name}[{myIndex}]";
                    }
                    
                }
                else
                    return Name;
            }
        }

    }
}
