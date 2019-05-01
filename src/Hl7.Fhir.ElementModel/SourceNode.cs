/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Hl7.Fhir.Specification;

namespace Hl7.Fhir.ElementModel
{
    public class SourceNode : DomNode<SourceNode>, ISourceNode, IAnnotated, IResourceTypeSupplier
    {
        public IEnumerable<ISourceNode> Children(string name = null) => ChildrenInternal(name);

        public string ResourceType { get; set; }

        public string Text { get; set; }

        private SourceNode(string name, string text, string resourceType = null)
        {
            Name = name;
            Text = text;
            ResourceType = resourceType;
        }

        public SourceNode Add(SourceNode child) => AddRange(new[] { child });

        public SourceNode AddRange(IEnumerable<SourceNode> children)
        {
            base.ChildList.AddRange(children);
            foreach (var c in base.ChildList) c.Parent = this;

            return this;
        }

        public static SourceNode Valued(string name, string value, params SourceNode[] children)
            => new SourceNode(name, value).AddRange(children);

        public static SourceNode Resource(string name, string type, params SourceNode[] children)
            => new SourceNode(name, null, type).AddRange(children);

        public static SourceNode Node(string name, params SourceNode[] children)
            => new SourceNode(name, null).AddRange(children);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="recursive"></param>
        /// <param name="annotationsToCopy">Maybe: if null - copy all, if empty, copy none, else specifcy which</param>
        /// <returns></returns>
        public static SourceNode FromNode(ISourceNode node, bool recursive = true, IEnumerable<Type> annotationsToCopy = null)
            => buildNode(node, recursive, annotationsToCopy);

        private static SourceNode buildNode(ISourceNode node, bool recursive, IEnumerable<Type> annotationsToCopy)
        {
            var me = new SourceNode(node.Name, node.Text);

            var rts = node.Annotation<IResourceTypeSupplier>();
            if (rts != null)
                me.ResourceType = rts.ResourceType;

            foreach (var t in annotationsToCopy ?? Enumerable.Empty<Type>())
                foreach (var ann in node.Annotations(t))
                    me.AddAnnotation(ann);

            if (recursive)
                me.AddRange(node.Children().Select(c => buildNode(c, recursive: true, annotationsToCopy: annotationsToCopy)));

            return me;
        }

        public SourceNode Clone()
        {
            var copy = new SourceNode(Name, Text, ResourceType)
            {
                Parent = Parent
            };

            copy.AddRange(Children().Cast<SourceNode>().Select(c => c.Clone()));

            if (HasAnnotations)
                copy.AnnotationsInternal.AddRange(AnnotationsInternal);

            return copy;
        }

        public IEnumerable<object> Annotations(Type type)
        {
            return type == typeof(SourceNode) || type == typeof(ISourceNode) || type == typeof(IResourceTypeSupplier)
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
                    var myIndex = Parent.ChildList.Where(c => c.Name == Name).ToList().IndexOf(this);
                    return $"{basePath}.{Name}[{myIndex}]";
                }
                else
                    return Name;
            }
        }
    }
}
