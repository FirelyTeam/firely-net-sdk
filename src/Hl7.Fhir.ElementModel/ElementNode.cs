/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Hl7.Fhir.ElementModel
{
#if FALSE
    // Not yet ready - removed for 1.2
    public class ElementNode : DomNode<ElementNode>, ITypedElement, IAnnotated, IAnnotatable
    {
        public IEnumerable<ITypedElement> Children(string name = null) => ChildrenByName(name);

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
            
            if(child.InstanceType == null)
            {
                if (child.Definition.IsResource || child.Definition.Type.Length > 1)
                    throw Error.Argument("The ElementNode given should have its InstanceType property set, since the element is a choice or resource.");

                child.InstanceType = child.Definition.Type.Single().GetTypeName();
            }
            children.Add(child);

            return this;
        }

        public ElementNode Add(IStructureDefinitionSummaryProvider provider, string name, object value=null, string instanceType = null)
        {
            var child = new ElementNode(name, value, instanceType, null);

            // Add() will supply the definition and the instanceType (if necessary)
            Add(provider, child); 

            return this;
        }

        public static ElementNode Root(IStructureDefinitionSummaryProvider provider, string type, string name=null)
        {
            if (provider == null) throw Error.ArgumentNull(nameof(provider));
            if (type == null) throw Error.ArgumentNull(nameof(type));

            var sd = provider.Provide(type);
            IElementDefinitionSummary definition = null;

            // Should we throw if type is not found?
            if (sd != null)
                definition = new TypeRootDefinitionSummary(sd);

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
                me.children.AddRange(node.Children().Select(c => buildNode(c, recursive: true, annotationsToCopy: annotationsToCopy, me)));

            return me;
        }

        private static ElementNode buildNode(ISourceNode node, bool recursive, IEnumerable<Type> annotationsToCopy, ElementNode parent)
        {
            // call some new method on TypedElementOnSourceNode to do the initial conversion 
            throw new NotImplementedException();
        }

        public ElementNode Clone()
        {
            var copy = new ElementNode(Name, Value, InstanceType, Definition)
            {
                Parent = Parent,
                children = children
            };

            if (HasAnnotations)
                copy.annotations.AddRange(annotations);

            return copy;
        }

        public IElementDefinitionSummary Definition { get; private set; }

        public string InstanceType { get; private set; }

        public object Value { get; set; }

        public IEnumerable<object> Annotations(Type type)
        {
            return (type == typeof(ElementNode) || type == typeof(ITypedElement))
                ? (new[] { this })
                : annotations.OfType(type);
        }
    }
#endif


}
